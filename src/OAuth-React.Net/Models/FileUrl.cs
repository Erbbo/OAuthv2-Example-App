using System;
using System.Collections.Generic;

namespace OAuth_React.Net.Models
{
    public class File
    {
        public IList<File> FileUrls { get; set; }
        public string FileType { get; set; }
        public string FileUrl { get; set; }
    }

    public static class Extentions
    {
        public static void AddUrls<T>(this ICollection<T> listOfFiles, Action<T> f)
        {
            listOfFiles.ForEach(x =>
            {
                f(x);
            });
        }

        public static void ForEach<T>(this IEnumerable<T> xs, Action<T> f)
        {
            foreach (var x in xs) f(x);
        }

        public static void ExtractFileUrls(this List<File> listOffileUrls, IList<Google.Apis.Drive.v2.Data.File> files)
        {
            files.ForEach(x =>
            {
                if (x.ExportLinks != null)
                {
                    x.ExportLinks.AddUrls(y =>
                    {
                        listOffileUrls.Add(new File
                        {
                            FileType = y.Key,
                            FileUrl = y.Value
                        });
                    });
                }
            });
        }
    }
}