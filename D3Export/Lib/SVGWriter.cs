using System;
using System.IO;
using System.Text;
using System.Web;

namespace D3Export.Lib
{
    public class SVGWriter : IDisposable
    {
        private const string EXTENSION = ".svg";
        private readonly FileStream fileStream;

        public SVGWriter(string filenameWithoutExtension = null, string saveDirectory = null)
        {
            filenameWithoutExtension = filenameWithoutExtension ?? Guid.NewGuid().ToString("N");
            saveDirectory = saveDirectory ?? HttpContext.Current.Server.MapPath("~/Content/Images");
            var filename = filenameWithoutExtension + EXTENSION;
            var path = Path.Combine(saveDirectory, filename);
            fileStream = new FileStream(path, FileMode.Create);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Write(string svgContent)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(svgContent);
            int length = buffer.Length;
            fileStream.Write(buffer, 0, length);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                if (fileStream != null)
                {
                    fileStream.Dispose();
                }
            }
        }
    }
}