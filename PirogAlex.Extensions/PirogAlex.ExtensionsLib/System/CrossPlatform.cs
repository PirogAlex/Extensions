using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PirogAlex.ExtensionsLib.System
{
    public static class CrossPlatform
    {
        private static char LinuxPathDelimeter = '/';
        private static char WindowsPathDelimeter = '\\';

        public static string PathCombine(this string basePath, params string[] additional)
        {
            var pathSplitCharacters = new[]
            {
                LinuxPathDelimeter,
                WindowsPathDelimeter
            };
            var splits = additional.Select(s => s.Split(pathSplitCharacters))
                .ToArray();
            var totalLength = splits.Sum(arr => arr.Length);
            var segments = new string[totalLength + 1];

            if (!basePath.EndsWith(LinuxPathDelimeter) && !basePath.EndsWith(WindowsPathDelimeter))
                basePath += LinuxPathDelimeter;

            segments[0] = basePath;
            var i = 0;
            foreach (var split in splits)
            {
                foreach (var value in split)
                {
                    i++;
                    segments[i] = value;
                }
            }

            return Path.Combine(segments);
        }
    }
}
