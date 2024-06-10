namespace PirogAlex.ExtensionsLib.CrossPlatform.Interfaces
{
    public interface ICrossPlatform
    {
        bool IsLinux { get; }

        /// <summary>
        /// Convert input path to target platform path "as is"
        /// </summary>
        /// <param name="inputPath"></param>
        /// <returns></returns>
        string GetAsPlatformPath(string inputPath);
        /// <summary>
        /// Convert input path to target platform path "as is"
        /// </summary>
        /// <param name="inputPath"></param>
        /// <param name="targetPlatform">Target OS platform</param>
        /// <returns></returns>
        string GetAsPlatformPath(string inputPath, TargetPlatform targetPlatform);


        /// <summary>
        /// System.IO.Path.Combine for All OSVersion
        /// </summary>
        /// <param name="basePath"></param>
        /// <param name="additional"></param>
        /// <returns></returns>
        string PathCombine(string basePath, params string[] additional);

        /// <summary>
        /// System.IO.Path.GetFileName for All OSVersion
        /// </summary>
        /// <param name="fullPath"></param>
        /// <returns></returns>
        string PathGetFileName(string fullPath);

        /// <summary>
        /// System.IO.Path.GetDirectoryName for All OSVersion
        /// </summary>
        /// <param name="fullPath"></param>
        /// <returns></returns>
        string PathGetDirectoryName(string fullPath);
        /// <summary>
        /// System.IO.Path.GetDirectoryName for All OSVersion
        /// </summary>
        /// <param name="fullPath"></param>
        /// <param name="returnPathLikeSourcePlatform">Convert result to sourcePlatform path based from <paramref name="fullPath"/> delimiter</param>
        /// <returns></returns>
        string PathGetDirectoryName(string fullPath, bool returnPathLikeSourcePlatform);
    }
}