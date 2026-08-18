using System;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x0200012B RID: 299
	public static class RSACertificateExtensions
	{
		// Token: 0x060009CD RID: 2509 RVA: 0x0002384C File Offset: 0x00021A4C
		[SecuritySafeCritical]
		public static RSA GetRSAPublicKey(this X509Certificate2 certificate)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			if (!RSACertificateExtensions.IsRSA(certificate))
			{
				return null;
			}
			PublicKey publicKey = certificate.PublicKey;
			AsnEncodedData encodedKeyValue = publicKey.EncodedKeyValue;
			IntPtr pszStructType = new IntPtr(72L);
			SafeLocalAllocHandle safeLocalAllocHandle;
			uint num;
			if (!CapiNative.DecodeObject(pszStructType, encodedKeyValue.RawData, out safeLocalAllocHandle, out num))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			byte[] array = new byte[num];
			using (safeLocalAllocHandle)
			{
				Marshal.Copy(safeLocalAllocHandle.DangerousGetHandle(), array, 0, array.Length);
			}
			CngKey key = CngKey.Import(array, CngKeyBlobFormat.GenericPublicBlob);
			return new RSACng(key);
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x000238FC File Offset: 0x00021AFC
		[SecuritySafeCritical]
		public static RSA GetRSAPrivateKey(this X509Certificate2 certificate)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			if (!certificate.HasPrivateKey || !RSACertificateExtensions.IsRSA(certificate))
			{
				return null;
			}
			RSA result;
			using (SafeCertContextHandle certificateContext = X509Native.GetCertificateContext(certificate))
			{
				CngKeyHandleOpenOptions keyHandleOpenOptions;
				using (SafeNCryptKeyHandle safeNCryptKeyHandle = X509Native.TryAcquireCngPrivateKey(certificateContext, out keyHandleOpenOptions))
				{
					if (safeNCryptKeyHandle == null)
					{
						if (LocalAppContextSwitches.DontReliablyClonePrivateKey)
						{
							result = (RSA)certificate.PrivateKey;
						}
						else
						{
							RSACryptoServiceProvider cspAlgorithm = (RSACryptoServiceProvider)certificate.PrivateKey;
							CspParameters parameters = DSACertificateExtensions.CopyCspParameters(cspAlgorithm);
							RSACryptoServiceProvider rsacryptoServiceProvider = new RSACryptoServiceProvider(parameters);
							result = rsacryptoServiceProvider;
						}
					}
					else
					{
						CngKey key = CngKey.Open(safeNCryptKeyHandle, keyHandleOpenOptions);
						result = new RSACng(key);
					}
				}
			}
			return result;
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x000239BC File Offset: 0x00021BBC
		[SecuritySafeCritical]
		public static X509Certificate2 CopyWithPrivateKey(this X509Certificate2 certificate, RSA privateKey)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			if (privateKey == null)
			{
				throw new ArgumentNullException("privateKey");
			}
			if (certificate.HasPrivateKey)
			{
				throw new InvalidOperationException(System.SR.GetString("Cryptography_Cert_AlreadyHasPrivateKey"));
			}
			using (RSA rsapublicKey = certificate.GetRSAPublicKey())
			{
				if (rsapublicKey == null)
				{
					throw new ArgumentException(System.SR.GetString("Cryptography_PrivateKey_WrongAlgorithm"));
				}
				RSAParameters rsaparameters = rsapublicKey.ExportParameters(false);
				RSAParameters rsaparameters2 = privateKey.ExportParameters(false);
				if (!rsaparameters.Modulus.SequenceEqual(rsaparameters2.Modulus) || !rsaparameters.Exponent.SequenceEqual(rsaparameters2.Exponent))
				{
					throw new ArgumentException(System.SR.GetString("Cryptography_PrivateKey_DoesNotMatch"), "privateKey");
				}
			}
			RSACng rsacng = privateKey as RSACng;
			X509Certificate2 x509Certificate = null;
			if (rsacng != null)
			{
				x509Certificate = CertificateExtensionsCommon.CopyWithPersistedCngKey(certificate, rsacng.Key);
			}
			if (x509Certificate == null)
			{
				RSACryptoServiceProvider rsacryptoServiceProvider = privateKey as RSACryptoServiceProvider;
				if (rsacryptoServiceProvider != null)
				{
					x509Certificate = CertificateExtensionsCommon.CopyWithPersistedCapiKey(certificate, rsacryptoServiceProvider.CspKeyContainerInfo);
				}
			}
			if (x509Certificate == null)
			{
				RSAParameters rsaparameters3 = privateKey.ExportParameters(true);
				using (PinAndClear.Track(rsaparameters3.D))
				{
					using (PinAndClear.Track(rsaparameters3.P))
					{
						using (PinAndClear.Track(rsaparameters3.Q))
						{
							using (PinAndClear.Track(rsaparameters3.DP))
							{
								using (PinAndClear.Track(rsaparameters3.DQ))
								{
									using (PinAndClear.Track(rsaparameters3.InverseQ))
									{
										RSACng rsacng2;
										rsacng = (rsacng2 = new RSACng());
										try
										{
											rsacng.ImportParameters(rsaparameters3);
											x509Certificate = CertificateExtensionsCommon.CopyWithEphemeralCngKey(certificate, rsacng.Key);
										}
										finally
										{
											if (rsacng2 != null)
											{
												((IDisposable)rsacng2).Dispose();
											}
										}
									}
								}
							}
						}
					}
				}
			}
			return x509Certificate;
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x00023BF0 File Offset: 0x00021DF0
		private static bool IsRSA(X509Certificate2 certificate)
		{
			uint num = RSACertificateExtensions.OidToAlgorithmId(certificate.PublicKey.Oid);
			return num == 9216U || num == 41984U;
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x00023C24 File Offset: 0x00021E24
		private static uint OidToAlgorithmId(Oid oid)
		{
			uint algid;
			using (SafeLocalAllocHandle safeLocalAllocHandle = X509Utils.StringToAnsiPtr(oid.Value))
			{
				CapiNative.CRYPT_OID_INFO crypt_OID_INFO = CapiNative.CryptFindOIDInfo(1U, safeLocalAllocHandle, OidGroup.All);
				algid = crypt_OID_INFO.Algid;
			}
			return algid;
		}
	}
}
