using System.Web.Mvc;

namespace D3Export.ViewModels
{
    public class PostSvg
    {
        [AllowHtml]
        public string SVG { get; set; }
    }
}