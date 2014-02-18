using System;
using System.IO;
using System.Text;
using System.Web;

namespace D3Export.Lib
{
    public class SVGWriter : IDisposable
    {
        private const string EXTENSION = ".svg";
        private readonly string filename;
        private readonly FileStream fileStream;
        private readonly string path;

        public SVGWriter(string filenameWithoutExtension = null, string saveDirectory = null)
        {
            filenameWithoutExtension = filenameWithoutExtension ?? Guid.NewGuid().ToString("N");
            saveDirectory = saveDirectory ?? HttpContext.Current.Server.MapPath("~/Content/Images");
            filename = filenameWithoutExtension + EXTENSION;
            path = Path.Combine(saveDirectory, filename);
            fileStream = new FileStream(path, FileMode.Create);
        }

        public string Filename
        {
            get { return filename; }
        }

        public string FilenameWithPath
        {
            get { return path; }
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
            if (!isDisposing)
                return;

            if (fileStream != null)
            {
                fileStream.Dispose();
            }
        }
    }
}