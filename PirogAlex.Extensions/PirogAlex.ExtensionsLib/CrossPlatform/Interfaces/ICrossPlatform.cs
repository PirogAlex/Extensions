namespace PirogAlex.ExtensionsLib.CrossPlatform.Interfaces
{
    public interface ICrossPlatform
    {
        /// <summary>
        /// Convert input path to target platform path "as is"
        /// </summary>
        /// <param name="inputPath"></param>
        /// <returns></returns>
        string GetAsPlatformPath(string inputPath);

        /// <summary>
        /// System.IO.Path.Combine for All OSVersion
        /// </summary>
        /// <param name="basePath"></param>
        /// <param name="additional"></param>
        /// <returns></returns>
        string PathCombine(string basePath, params string[] additional);
    }
}