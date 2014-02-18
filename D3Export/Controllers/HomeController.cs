﻿using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.Mvc;
using D3Export.Lib;
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
            using (var svgWriter = new SVGWriter())
            {
                svgWriter.Write(form.SVG);
                var path = svgWriter.FilenameWithPath;
                new PDFExporter().ExportSVG(path);
            }

            return Json(new
            {
                timestamp = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)
            });
        }
	}
}