// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GenerateCerfificateRequest.cs" company="GSD Logic">
//   Copyright © 2020 GSD Logic. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace CryptUtil
{
    using System.Security.Cryptography;
    using System.Security.Cryptography.X509Certificates;

    public static class GenerateCerfificateRequest
    {
        public static void Run()
        {
            var request = new CertificateRequest(
                new X500DistinguishedName("CN=example.org"),
                ECDsa.Create(),
                HashAlgorithmName.SHA256);

            var csr = request.CreateSigningRequest();
            PEM.Write("example.org.csr", csr, "CERTIFICATE REQUEST");
        }
    }
}