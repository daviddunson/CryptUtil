// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PEM.cs" company="GSD Logic">
//   Copyright © 2020 GSD Logic. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace CryptUtil
{
    using System;
    using System.IO;

    public static class PEM
    {
        public static byte[] Read(string path, string name)
        {
            var beginDelimiter = $"-----BEGIN {name}-----";
            var endDelimiter = $"-----END {name}-----";

            var text = File.ReadAllText(path);
            var beginIndex = text.IndexOf(beginDelimiter, StringComparison.Ordinal);
            var endIndex = text.IndexOf(endDelimiter, beginIndex, StringComparison.Ordinal);

            if ((beginIndex < 0) || (endIndex <= beginIndex))
            {
                throw new Exception();
            }

            beginIndex += beginDelimiter.Length;
            var base64 = text.Substring(beginIndex, endIndex - beginIndex);
            return Convert.FromBase64String(base64);
        }

        public static void Write(string path, byte[] bytes, string name)
        {
            using var stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Read);
            using var writer = new StreamWriter(stream);
            writer.WriteLine($"-----BEGIN {name}-----");
            writer.WriteLine(Convert.ToBase64String(bytes, Base64FormattingOptions.InsertLineBreaks));
            writer.WriteLine($"-----END {name}-----");
        }
    }
}