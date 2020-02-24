// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GenerateCertificateChain.cs" company="GSD Logic">
//   Copyright © 2020 GSD Logic. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace CryptUtil
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Security.Cryptography.X509Certificates;

    public static class GenerateCertificateChain
    {
        public static void Run()
        {
            var time = DateTimeOffset.Now;
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var epochTime = (long)(time - epoch).TotalSeconds;

            var caKey = ECDsa.Create();
            var caRequest = new CertificateRequest(new X500DistinguishedName("CN=ca.example.org"), caKey, HashAlgorithmName.SHA256);
            caRequest.CertificateExtensions.Add(new X509BasicConstraintsExtension(true, true, 2, true));
            var caCert = caRequest.CreateSelfSigned(time, time.AddDays(365));
            File.WriteAllBytes("ca.example.org.cer", caCert.Export(X509ContentType.Cert));

            var subKey = ECDsa.Create();
            var subRequest = new CertificateRequest(new X500DistinguishedName("CN=sub.example.org"), subKey, HashAlgorithmName.SHA256);
            subRequest.CertificateExtensions.Add(new X509BasicConstraintsExtension(true, true, 1, true));
            var subCert = subRequest.Create(caCert, time, time.AddDays(365), BitConverter.GetBytes(epochTime + 1)).CopyWithPrivateKey(subKey);
            File.WriteAllBytes("sub.example.org.cer", subCert.Export(X509ContentType.Cert));

            var key = RSA.Create(2048);
            var request = new CertificateRequest(new X500DistinguishedName("CN=example.org"), key, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
            var cert = request.Create(subCert.SubjectName, X509SignatureGenerator.CreateForECDsa(subCert.GetECDsaPrivateKey()), time, time.AddDays(365), BitConverter.GetBytes(epochTime + 2)).CopyWithPrivateKey(key);
            File.WriteAllBytes("example.org.cer", cert.Export(X509ContentType.Cert));
        }
    }
}