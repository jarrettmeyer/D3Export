using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.Mvc;

namespace D3Export.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Save(string svg)
        {
            var filePath = GetRandomFilePath();
            using (var file = new FileStream(filePath, FileMode.Create))
            {
                byte[] buffer = Encoding.UTF8.GetBytes(svg);
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