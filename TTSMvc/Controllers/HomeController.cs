using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace TTSMvc.Controllers
{
    
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        //[Route("TTS1/{text=}")]
        //public ActionResult Mp3Synt(string text)
        //{
        //    return File(new TextToMp3Synthesis().Convert(text), "audio/mpeg", "audio.mp3");
        //}
        
        public async Task<FileResult> TTS1(string text)
        {
            var t1 = await Task.Run(() => new TextToMp3Synthesis().Convert(text));
            return File(t1, "audio/wav");
        }
    }
}