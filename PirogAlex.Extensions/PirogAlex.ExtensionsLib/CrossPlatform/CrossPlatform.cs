using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PirogAlex.ExtensionsLib.CrossPlatform.Interfaces;

namespace PirogAlex.ExtensionsLib.CrossPlatform
{
    public class CrossPlatform : ICrossPlatform
    {
        private readonly char LinuxPathDelimiter = '/';
        private readonly char WindowsPathDelimiter = '\\';

        private readonly TargetPlatform _targetPlatform;

        public static bool IsLinux
        {
            get
            {
                int p = (int)Environment.OSVersion.Platform;
                return (p == 4) || (p == 6) || (p == 128);
            }
        }

        public CrossPlatform(TargetPlatform targetPlatform = TargetPlatform.Linux)
        {
            _targetPlatform = targetPlatform;
        }

        public string GetAsPlatformPath(string inputPath)
        {
            return GetAsPlatformPathWithCorrectDelimiter(inputPath, false, false);
        }

        public string PathCombine(string basePath, params string[] additional)
        {
            var segments = new string[additional.Length + 1];

            segments[0] = GetAsPlatformPathWithCorrectDelimiter(basePath, false, additional.Length > 0);
            for (int i = 1; i <= additional.Length; i++)
            {
                segments[i] = GetAsPlatformPathWithCorrectDelimiter(additional[i - 1], true, i != additional.Length);
            }

            var combine = Path.Combine(segments);
            //В Linux у Path.Combine есть особенность. Перед каждым сегментом автоматически и всегда добавляется косая черта. Получается лишний уровень вложенности и кривой путь.
            //      Этот лайфхак исправляет ситуацию
            if (_targetPlatform == TargetPlatform.Windows)
                return combine.Replace(LinuxPathDelimiter.ToString(), string.Empty);

            return combine;
        }

        private string GetAsPlatformPathWithCorrectDelimiter(string inputPath, bool correctStartDelimiter, bool correctEndDelimiter)
        {
            string asPlatformPath;
            if (_targetPlatform == TargetPlatform.Linux)
            {
                asPlatformPath = inputPath.Replace(WindowsPathDelimiter, LinuxPathDelimiter);
                asPlatformPath = ManipulationWithDelimiter(asPlatformPath, LinuxPathDelimiter, correctStartDelimiter, correctEndDelimiter);

            }
            else
            {
                asPlatformPath = inputPath.Replace(LinuxPathDelimiter, WindowsPathDelimiter);
                asPlatformPath = ManipulationWithDelimiter(asPlatformPath, WindowsPathDelimiter, correctStartDelimiter, correctEndDelimiter);
            }

            return asPlatformPath;
        }

        private string ManipulationWithDelimiter(string asPlatformPath, char delimiter, bool correctStartDelimiter, bool correctEndDelimiter)
        {
            if (correctStartDelimiter && asPlatformPath.StartsWith(delimiter))
                asPlatformPath = asPlatformPath.TrimStart(delimiter);

            if (correctEndDelimiter && !asPlatformPath.EndsWith(delimiter))
                asPlatformPath += delimiter;

            return asPlatformPath;
        }
    }
}
