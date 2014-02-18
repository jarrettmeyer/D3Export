using System;
using System.Configuration;
using System.Diagnostics;

namespace D3Export.Lib
{
    public class PDFExporter
    {
        public static string InkscapeArguments
        {
            get { return string.Format("--without-gui --export-pdf={0} {1}"); }
        }

        public static string PathToInkscape
        {
            get { return ConfigurationManager.AppSettings["PathToInkscapeExecutable"]; }
        }

        public void ExportSVG(string svgPath)
        {
            var processStartInfo = new ProcessStartInfo();
            processStartInfo.FileName = PathToInkscape;
            processStartInfo.Arguments = GetInkscapeArguments(svgPath);
            processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            processStartInfo.CreateNoWindow = true;
            processStartInfo.UseShellExecute = true;

            using (var process = Process.Start(processStartInfo))
            {
                if (process == null)
                    throw new NullReferenceException("Process is null.");

                process.WaitForExit();
            }
        }

        private static string GetInkscapeArguments(string svgPath)
        {
            var pdfPath = svgPath.Substring(0, svgPath.Length - 4) + ".pdf";
            return string.Format("--without-gui --export-pdf=\"{0}\" \"{1}\"", pdfPath, svgPath);
        }
    }
}