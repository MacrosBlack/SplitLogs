using System;
using System.IO;
using System.IO.Compression;
using System.Text.RegularExpressions;

namespace SplitLogs
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Please specify the name of a zip file containing ED Journal logfiles");
                return;
            }

            var zipFile = args[0];
            if( !File.Exists(zipFile))
            {
                Console.WriteLine($"The specified name '{zipFile}' cannot be found");
                return;
            }

            var cmdrNameRe = new Regex("\"event\":\"LoadGame\", \"Commander\":\"([^\"]+)\"", RegexOptions.Multiline);
            var fi = new FileInfo(zipFile);

            using (var archive = ZipFile.OpenRead(zipFile))
            {
                foreach (var entry in archive.Entries)
                {
                    if (!entry.FullName.EndsWith(".log", StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    var tempFile = Path.GetTempFileName();
                    entry.ExtractToFile(tempFile, true);
                    var content = File.ReadAllText(tempFile);
                    var match = cmdrNameRe.Match(content);
                    if (match.Success)
                    {
                        // EdAstro does not like spaces.
                        var cmdrName = match.Groups[1].Value.Replace(" ","_");
                        var cmdrNameDir = Path.Combine(fi.DirectoryName, cmdrName);
                        if (!Directory.Exists(cmdrNameDir))
                        {
                            Directory.CreateDirectory(cmdrNameDir);
                        }

                        var destFileName = Path.Combine(cmdrNameDir, entry.Name + "." + cmdrName + ".log");
                        if (!File.Exists(destFileName))
                        {
                            File.Copy(tempFile, destFileName);
                        }

                        File.Delete(tempFile);
                    }
                }
            }
        }
    }
}
