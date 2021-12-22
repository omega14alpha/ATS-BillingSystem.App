using System;
using System.Security.Cryptography;
using System.Text;

namespace ATS_BillingSystem.App.Infrastructure
{
    internal class MD5Generator : IGeneratorId
    {
        public string GetId(string encodedData)
        {
            if (string.IsNullOrWhiteSpace(encodedData))
            {
                throw new ArgumentException("Paremeter 'message' cannot be empty or equals null!", nameof(encodedData));
            }

            using MD5 mD5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(encodedData);
            byte[] heshBytes = mD5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            foreach (var item in heshBytes)
            {
                sb.Append(item.ToString("X2"));
            }

            return sb.ToString();
        }
    }
}
