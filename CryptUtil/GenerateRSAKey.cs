// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GenerateRSAKey.cs" company="GSD Logic">
//   Copyright © 2020 GSD Logic. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace CryptUtil
{
    using System;
    using System.IO;
    using System.Security.Cryptography;

    public static class GenerateRSAKey
    {
        public static void Run()
        {
            var importedKey = PEM.Read(@"resources\example.org.key", "RSA PRIVATE KEY");
            Console.WriteLine("Imported key length: {0}", importedKey.Length);

            using var rsa = new RSACryptoServiceProvider();
            rsa.ImportRSAPrivateKey(importedKey, out _);

            var parameters = rsa.ExportParameters(true);
            var parametersLength = parameters.D.Length +
                                   parameters.DP.Length +
                                   parameters.DQ.Length +
                                   parameters.Exponent.Length +
                                   parameters.InverseQ.Length +
                                   parameters.Modulus.Length +
                                   parameters.P.Length +
                                   parameters.Q.Length;

            Console.WriteLine();
            Console.WriteLine("Parameters length: {0}", parametersLength);
            Console.WriteLine("Modulus: {0} ({1})", parameters.Modulus.Length, importedKey.IndexOf(parameters.Modulus));
            Console.WriteLine("Exponent: {0} ({1})", parameters.Exponent.Length, importedKey.IndexOf(parameters.Exponent));
            Console.WriteLine("D: {0} ({1})", parameters.D.Length, importedKey.IndexOf(parameters.D));
            Console.WriteLine("P: {0} ({1})", parameters.P.Length, importedKey.IndexOf(parameters.P));
            Console.WriteLine("Q: {0} ({1})", parameters.Q.Length, importedKey.IndexOf(parameters.Q));
            Console.WriteLine("DP: {0} ({1})", parameters.DP.Length, importedKey.IndexOf(parameters.DP));
            Console.WriteLine("DQ: {0} ({1})", parameters.DQ.Length, importedKey.IndexOf(parameters.DQ));
            Console.WriteLine("InverseQ: {0} ({1})", parameters.InverseQ.Length, importedKey.IndexOf(parameters.InverseQ));

            var exportedKey = rsa.ExportRSAPrivateKey();

            Console.WriteLine();
            Console.WriteLine("Exported key length: {0}", exportedKey.Length);
            Console.WriteLine("Matches exported key: {0}", importedKey.IndexOf(exportedKey) == 0);
        }
    }
}