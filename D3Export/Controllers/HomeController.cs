using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.Mvc;
using D3Export.ViewModels;

namespace D3Export.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Save(PostSvg form)
        {
            var filePath = GetRandomFilePath();
            using (var file = new FileStream(filePath, FileMode.Create))
            {
                byte[] buffer = Encoding.UTF8.GetBytes(form.SVG);
                int length = buffer.Length;
                file.Write(buffer, 0, length);
            }

            return Json(new
            {
                timestamp = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)
            });
        }

        public static string Extension
        {
            get { return ".svg"; }
        }

        public string Folder
        {
            get { return Server.MapPath("~/Content/Images"); }
        }

        public string GetRandomFilePath()
        {
            var filename = Guid.NewGuid().ToString("N") + Extension;
            return Path.Combine(Folder, filename);
        }
	}
}