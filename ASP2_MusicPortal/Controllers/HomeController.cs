using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ASP2_MusicPortal.Models.Entities;
using ASP2_MusicPortal.Models;
using Id3;
using System.Drawing;
using Id3.Frames;

namespace ASP2_MusicPortal.Controllers
{
    public class HomeController : Controller
    {
        private IMPRepository repos;// =  new MusicPortalModel();

        string MusicDir = "~/MusicFiles";

        public HomeController(IMPRepository repository)
        {
            repos = repository;
        }


        [HttpGet]
        public ActionResult Index()
        {
            if (Session["Name"] != null && Session["isActive"] != "Disabled")
            {
                ViewBag.Text = "Downloaded files: 0";
                ViewBag.MyGenres = new SelectList(repos.Genre, "ID", "Genre");
                return View(GetPhonoteka());
            }
            else if (Session["isActive"] == "Disabled")
                return RedirectToAction("Logged", "Home");
            else
                return RedirectToAction("Authorize", "Account");
        }


        [HttpPost]
        public async Task<ActionResult> Index(IEnumerable<HttpPostedFileBase> fileUpload, Genres mygenre)
        {

               /*
             * параметр IEnumerable<HttpPostedFileBase> fileUpload представляет перечисление файлов, 
             * которые были отправлены пользователем. Важно, что имя fileUpload совпадает со значением 
             * атрибута name элементов формы, в том числе, с теми которые содержат индексаторы [№]. 
             * Именно наличие индексаторов позволяет MVC автоматически присваивать в параметр перечисление.
             */
            int count = 0;
            try
            {
                if(Request.Form["Submit"] != null)
                {
                    foreach (var file in fileUpload)
                    {
                        if (file == null) continue;
                        string filename = Path.GetFileName(file.FileName);
                        string tempfolder = Server.MapPath(MusicDir);
                        string fullpath = MusicDir.Substring(2) + "/" + filename;
                        if (filename != null && repos.Phonotrack.Where(t => t.TrackURL == fullpath).FirstOrDefault() == null)
                        {
                            
                            file.SaveAs(Path.Combine(tempfolder, filename));
                            count++;
                            
                            Id3.Mp3 mp3file = new Mp3(Path.Combine(tempfolder, filename));
                            Id3Tag mp3tags = null;

                            if (mp3file != null && mp3file.HasTags)
                            {
                                mp3tags = mp3file.GetTag(Id3TagFamily.Version2X);
                                mp3tags = mp3tags ?? mp3file.GetTag(Id3TagFamily.Version1X);
                                AudioStreamProperties prop = mp3file.Audio;
                                Genres genres = repos.Genre.Where(g => g.Genre == mp3tags.Genre).FirstOrDefault();
                                if(genres == null)
                                {
                                    genres = new Genres { Genre = mp3tags.Genre };
                                    repos.Genre.Add(genres);
                                    await repos.SaveChangesAsync();
                                }

                                string duration = String.Format("{0:hh\\:mm\\:ss}", prop.Duration);

                                Phonoteka phonotek = new Phonoteka
                                {
                                    TrackName = filename,    TrackURL = fullpath,
                                    Album = mp3tags.Album,   Artist = mp3tags.Artists,
                                    Title = mp3tags.Title,   Year = mp3tags.Year,
                                    Genres = genres,         Bitrate = prop.Bitrate,
                                    Duration = duration,     Frequency = prop.Frequency,
                                    Mode = prop.Mode.ToString()
                                };

                                repos.Phonotrack.Add(phonotek);
                            }

                        }
                    }
                    await repos.SaveChangesAsync();
                }
               

               

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            
               
            ViewBag.Text = "Downloaded files: " + count;
            ViewBag.MyGenres = new SelectList(repos.Genre, "ID", "Genre");
            if (Request.Form["Filter"] != null)
                return View(GetPhonoteka(mygenre?.ID));
            else
                return View(GetPhonoteka());
        }

      
        

        private IEnumerable<Phonoteka> GetPhonoteka(int? condition = null)
        {
            List<Phonoteka> tracks = null;
            
            if(condition == null)
                tracks = repos.Phonotrack.Include("Genres").ToList();
            else
            {
                tracks = repos.Phonotrack.Include("Genres").Where(p => p.Genres.ID == condition).ToList();
            }
            
            return tracks;
        }



        public ActionResult Logout()
        {
            Session.Abandon(); //bye bye
            return RedirectToAction("Authorize", "Account");
        }


        public ActionResult Logged()
        {
            if (Session["Name"] != null)
            {
                ViewBag.Message = "You was logged as - ";
                return View();
            }
            else
                return RedirectToAction("Authorize", "Account");
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phonoteka track = await repos.Phonotrack.FindAsync(id);
            if (track == null)
            {
                return HttpNotFound();
            }
            return View(track);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Phonoteka track = await repos.Phonotrack.FindAsync(id);
            
            try
            {
                string tempfolder = Server.MapPath(MusicDir);
                string filename = track.TrackURL.Substring(track.TrackURL.LastIndexOf('/') + 1);
                string filepath = Path.Combine(tempfolder, filename);
                if (System.IO.File.Exists(filepath))
                    System.IO.File.Delete(filepath);
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            repos.Phonotrack.Remove(track);
             await repos.SaveChangesAsync();

            return RedirectToAction("Index");
        }


        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        repos.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}