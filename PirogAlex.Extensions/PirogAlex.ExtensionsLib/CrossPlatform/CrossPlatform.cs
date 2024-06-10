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

        public bool IsLinux
        {
            get
            {
                int p = (int)Environment.OSVersion.Platform;
                return (p == 4) || (p == 6) || (p == 128);
            }
        }

        public CrossPlatform()
        {
            _targetPlatform = IsLinux ? TargetPlatform.Linux : TargetPlatform.Windows;
        }

        public CrossPlatform(TargetPlatform targetPlatform)
        {
            _targetPlatform = targetPlatform;
        }

        public string GetAsPlatformPath(string inputPath)
        {
            return GetAsPlatformPathWithCorrectDelimiter(inputPath, false, false);
        }

        public string GetAsPlatformPath(string inputPath, TargetPlatform targetPlatform)
        {
            return GetAsPlatformPathWithCorrectDelimiter(inputPath, false, false, targetPlatform);
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
            if (IsLinux && _targetPlatform == TargetPlatform.Windows)
            {
                //В Linux у Path.Combine есть особенность. Перед каждым сегментом автоматически и всегда добавляется косая черта. Получается лишний уровень вложенности и кривой путь.
                //      Этот лайфхак исправляет ситуацию
                return combine.Replace(LinuxPathDelimiter.ToString(), string.Empty);
            }

            return combine;
        }

        private string GetAsPlatformPathWithCorrectDelimiter(string inputPath, bool correctStartDelimiter, bool correctEndDelimiter)
        {
            return GetAsPlatformPathWithCorrectDelimiter(inputPath, correctStartDelimiter, correctEndDelimiter, _targetPlatform);
        }

        private string GetAsPlatformPathWithCorrectDelimiter(string inputPath, bool correctStartDelimiter, bool correctEndDelimiter, TargetPlatform targetPlatform)
        {
            string asPlatformPath;
            if (targetPlatform == TargetPlatform.Linux)
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

        public string PathGetFileName(string fullPath)
        {
            var str = GetAsPlatformPath(fullPath);
            var fileName = Path.GetFileName(str);
            if (IsLinux && _targetPlatform == TargetPlatform.Windows)
            {
                //В Linux с путями для Windows платформы у Path.GetFileName поведение сбоит. Потому обрабатываем иначе...
                str = GetAsPlatformPath(fullPath, TargetPlatform.Linux);
                fileName = Path.GetFileName(str);
            }

            return fileName;
        }

        public string PathGetDirectoryName(string fullPath)
        {
            return PathGetDirectoryName(fullPath, false);
        }

        public string PathGetDirectoryName(string fullPath, bool returnPathLikeSourcePlatform)
        {
            var str = GetAsPlatformPath(fullPath);
            var directoryName = Path.GetDirectoryName(str);
            if (IsLinux && _targetPlatform == TargetPlatform.Windows)
            {
                //В Linux с путями для Windows платформы у Path.GetDirectoryName поведение сбоит. Потому обрабатываем иначе...
                str = GetAsPlatformPath(fullPath, TargetPlatform.Linux);
                directoryName = Path.GetDirectoryName(str);
                if (string.IsNullOrEmpty(directoryName))
                    return string.Empty;

                directoryName = GetAsPlatformPath(directoryName);
            }

            if (!string.IsNullOrEmpty(directoryName) && returnPathLikeSourcePlatform)
            {
                if (fullPath.Contains(LinuxPathDelimiter))
                    directoryName = GetAsPlatformPath(directoryName, TargetPlatform.Linux);
                else if (fullPath.Contains(WindowsPathDelimiter))
                    directoryName = GetAsPlatformPath(directoryName, TargetPlatform.Windows);
            }

            return directoryName ?? string.Empty;
        }
    }
}
