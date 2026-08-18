using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Security;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Win32;

namespace Microsoft.Owin.Security
{
	// Token: 0x02000037 RID: 55
	public class CertificateSubjectPublicKeyInfoValidator : ICertificateValidator
	{
		// Token: 0x060000E7 RID: 231 RVA: 0x000049B0 File Offset: 0x00002BB0
		public CertificateSubjectPublicKeyInfoValidator(IEnumerable<string> validBase64EncodedSubjectPublicKeyInfoHashes, SubjectPublicKeyInfoAlgorithm algorithm)
		{
			if (validBase64EncodedSubjectPublicKeyInfoHashes == null)
			{
				throw new ArgumentNullException("validBase64EncodedSubjectPublicKeyInfoHashes");
			}
			this._validBase64EncodedSubjectPublicKeyInfoHashes = new HashSet<string>(validBase64EncodedSubjectPublicKeyInfoHashes);
			if (this._validBase64EncodedSubjectPublicKeyInfoHashes.Count == 0)
			{
				throw new ArgumentOutOfRangeException("validBase64EncodedSubjectPublicKeyInfoHashes");
			}
			if (this._algorithm != SubjectPublicKeyInfoAlgorithm.Sha1 && this._algorithm != SubjectPublicKeyInfoAlgorithm.Sha256)
			{
				throw new ArgumentOutOfRangeException("algorithm");
			}
			this._algorithm = algorithm;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00004A18 File Offset: 0x00002C18
		public bool Validate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			if (sslPolicyErrors != SslPolicyErrors.None)
			{
				return false;
			}
			if (chain == null)
			{
				throw new ArgumentNullException("chain");
			}
			if (chain.ChainElements.Count < 2)
			{
				return false;
			}
			using (HashAlgorithm hashAlgorithm = this.CreateHashAlgorithm())
			{
				foreach (X509ChainElement x509ChainElement in chain.ChainElements)
				{
					X509Certificate2 certificate2 = x509ChainElement.Certificate;
					string item = Convert.ToBase64String(hashAlgorithm.ComputeHash(CertificateSubjectPublicKeyInfoValidator.ExtractSpkiBlob(certificate2)));
					if (this._validBase64EncodedSubjectPublicKeyInfoHashes.Contains(item))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00004ABC File Offset: 0x00002CBC
		private static byte[] ExtractSpkiBlob(X509Certificate2 certificate)
		{
			NativeMethods.CERT_INFO cert_INFO = (NativeMethods.CERT_INFO)Marshal.PtrToStructure(((NativeMethods.CERT_CONTEXT)Marshal.PtrToStructure(certificate.Handle, typeof(NativeMethods.CERT_CONTEXT))).pCertInfo, typeof(NativeMethods.CERT_INFO));
			NativeMethods.CERT_PUBLIC_KEY_INFO subjectPublicKeyInfo = cert_INFO.SubjectPublicKeyInfo;
			uint num = 0U;
			IntPtr lpszStructType = new IntPtr(8);
			if (!NativeMethods.CryptEncodeObject(1U, lpszStructType, ref subjectPublicKeyInfo, null, ref num))
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				throw new Win32Exception(lastWin32Error);
			}
			byte[] array = new byte[num];
			if (!NativeMethods.CryptEncodeObject(1U, lpszStructType, ref subjectPublicKeyInfo, array, ref num))
			{
				int lastWin32Error2 = Marshal.GetLastWin32Error();
				throw new Win32Exception(lastWin32Error2);
			}
			return array;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00004B59 File Offset: 0x00002D59
		private HashAlgorithm CreateHashAlgorithm()
		{
			if (this._algorithm != SubjectPublicKeyInfoAlgorithm.Sha1)
			{
				return new SHA256CryptoServiceProvider();
			}
			return new SHA1CryptoServiceProvider();
		}

		// Token: 0x04000058 RID: 88
		private readonly HashSet<string> _validBase64EncodedSubjectPublicKeyInfoHashes;

		// Token: 0x04000059 RID: 89
		private readonly SubjectPublicKeyInfoAlgorithm _algorithm;
	}
}
