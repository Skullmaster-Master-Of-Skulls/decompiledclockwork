using System;
using System.Security.Cryptography;

namespace System.Deployment.Internal.CodeSigning
{
	// Token: 0x02000018 RID: 24
	internal class ManifestUtilities
	{
		// Token: 0x060000B0 RID: 176 RVA: 0x00006B40 File Offset: 0x00004D40
		internal static SupportedHashAlgorithm GetSupportedHashAlgorithmFromSignatureMethod(string signatureMethodUri, bool useSha256OrHigher, Action onError)
		{
			if (useSha256OrHigher)
			{
				if (string.Equals(signatureMethodUri, "http://www.w3.org/2000/09/xmldsig#rsa-sha512", StringComparison.OrdinalIgnoreCase))
				{
					return SupportedHashAlgorithm.SHA512;
				}
				if (string.Equals(signatureMethodUri, "http://www.w3.org/2000/09/xmldsig#rsa-sha384", StringComparison.OrdinalIgnoreCase))
				{
					return SupportedHashAlgorithm.SHA384;
				}
				if (string.Equals(signatureMethodUri, "http://www.w3.org/2000/09/xmldsig#rsa-sha256", StringComparison.OrdinalIgnoreCase))
				{
					return SupportedHashAlgorithm.SHA256;
				}
			}
			else if (string.Equals(signatureMethodUri, "http://www.w3.org/2000/09/xmldsig#rsa-sha1", StringComparison.OrdinalIgnoreCase))
			{
				return SupportedHashAlgorithm.SHA1;
			}
			onError();
			return SupportedHashAlgorithm.INVALID;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00006B97 File Offset: 0x00004D97
		internal static HashAlgorithm CreateHashAlgorithmInstance(SupportedHashAlgorithm algorithmId)
		{
			switch (algorithmId)
			{
			case SupportedHashAlgorithm.SHA256:
				return SHA256.Create();
			case SupportedHashAlgorithm.SHA384:
				return SHA384.Create();
			case SupportedHashAlgorithm.SHA512:
				return SHA512.Create();
			default:
				return SHA1.Create();
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00006BC6 File Offset: 0x00004DC6
		internal static string GetSignatureMethodUri(SupportedHashAlgorithm hashAlgorithm, bool useSha256OrHigher, int errorCodeForException)
		{
			if (useSha256OrHigher)
			{
				switch (hashAlgorithm)
				{
				case SupportedHashAlgorithm.SHA256:
					return "http://www.w3.org/2000/09/xmldsig#rsa-sha256";
				case SupportedHashAlgorithm.SHA384:
					return "http://www.w3.org/2000/09/xmldsig#rsa-sha384";
				case SupportedHashAlgorithm.SHA512:
					return "http://www.w3.org/2000/09/xmldsig#rsa-sha512";
				}
			}
			else if (hashAlgorithm == SupportedHashAlgorithm.SHA1)
			{
				return "http://www.w3.org/2000/09/xmldsig#rsa-sha1";
			}
			throw new CryptographicException(errorCodeForException);
		}

		// Token: 0x04000104 RID: 260
		public const string Sha1SignatureMethodUri = "http://www.w3.org/2000/09/xmldsig#rsa-sha1";

		// Token: 0x04000105 RID: 261
		public const string Sha256SignatureMethodUri = "http://www.w3.org/2000/09/xmldsig#rsa-sha256";

		// Token: 0x04000106 RID: 262
		public const string Sha384SignatureMethodUri = "http://www.w3.org/2000/09/xmldsig#rsa-sha384";

		// Token: 0x04000107 RID: 263
		public const string Sha512SignatureMethodUri = "http://www.w3.org/2000/09/xmldsig#rsa-sha512";

		// Token: 0x04000108 RID: 264
		public const string Sha256DigestMethod = "http://www.w3.org/2000/09/xmldsig#sha256";

		// Token: 0x04000109 RID: 265
		public const string Sha384DigestMethod = "http://www.w3.org/2000/09/xmldsig#sha384";

		// Token: 0x0400010A RID: 266
		public const string Sha512DigestMethod = "http://www.w3.org/2000/09/xmldsig#sha512";
	}
}
