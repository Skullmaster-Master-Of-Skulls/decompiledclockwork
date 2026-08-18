using System;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x02000121 RID: 289
	public static class DSACertificateExtensions
	{
		// Token: 0x06000952 RID: 2386 RVA: 0x00020DC8 File Offset: 0x0001EFC8
		[SecuritySafeCritical]
		public unsafe static DSA GetDSAPublicKey(this X509Certificate2 certificate)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			if (!DSACertificateExtensions.IsDSA(certificate))
			{
				return null;
			}
			DSAParameters parameters = default(DSAParameters);
			SafeLocalAllocHandle safeLocalAllocHandle = null;
			try
			{
				byte[] rawData = certificate.PublicKey.EncodedKeyValue.RawData;
				uint num;
				if (!CapiNative.DecodeObject((IntPtr)((long)((ulong)38)), rawData, out safeLocalAllocHandle, out num))
				{
					throw new CryptographicException(Marshal.GetLastWin32Error());
				}
				if ((ulong)num < (ulong)((long)Marshal.SizeOf(typeof(CapiNative.CRYPTOAPI_BLOB))))
				{
					throw new CryptographicException();
				}
				CapiNative.CRYPTOAPI_BLOB* ptr = (CapiNative.CRYPTOAPI_BLOB*)((void*)safeLocalAllocHandle.DangerousGetHandle());
				parameters.Y = DSACertificateExtensions.ToBigEndianByteArray(*ptr);
			}
			finally
			{
				if (safeLocalAllocHandle != null)
				{
					safeLocalAllocHandle.Dispose();
					safeLocalAllocHandle = null;
				}
			}
			SafeLocalAllocHandle safeLocalAllocHandle2 = null;
			try
			{
				byte[] keyAlgorithmParameters = certificate.GetKeyAlgorithmParameters();
				uint num2;
				if (!CapiNative.DecodeObject((IntPtr)((long)((ulong)39)), keyAlgorithmParameters, out safeLocalAllocHandle2, out num2))
				{
					throw new CryptographicException(Marshal.GetLastWin32Error());
				}
				if ((ulong)num2 < (ulong)((long)Marshal.SizeOf(typeof(CapiNative.CERT_DSS_PARAMETERS))))
				{
					throw new CryptographicException();
				}
				CapiNative.CERT_DSS_PARAMETERS* ptr2 = (CapiNative.CERT_DSS_PARAMETERS*)((void*)safeLocalAllocHandle2.DangerousGetHandle());
				parameters.P = DSACertificateExtensions.ToBigEndianByteArray(ptr2->p);
				parameters.Q = DSACertificateExtensions.ToBigEndianByteArray(ptr2->q);
				parameters.G = DSACertificateExtensions.ToBigEndianByteArray(ptr2->g);
			}
			finally
			{
				if (safeLocalAllocHandle2 != null)
				{
					safeLocalAllocHandle2.Dispose();
					safeLocalAllocHandle2 = null;
				}
			}
			DSACng dsacng = new DSACng();
			dsacng.ImportParameters(parameters);
			return dsacng;
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x00020F38 File Offset: 0x0001F138
		[SecuritySafeCritical]
		public static DSA GetDSAPrivateKey(this X509Certificate2 certificate)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			if (!certificate.HasPrivateKey || !DSACertificateExtensions.IsDSA(certificate))
			{
				return null;
			}
			DSA result;
			using (SafeCertContextHandle certificateContext = X509Native.GetCertificateContext(certificate))
			{
				CngKeyHandleOpenOptions keyHandleOpenOptions;
				using (SafeNCryptKeyHandle safeNCryptKeyHandle = X509Native.TryAcquireCngPrivateKey(certificateContext, out keyHandleOpenOptions))
				{
					if (safeNCryptKeyHandle == null)
					{
						DSACryptoServiceProvider cspAlgorithm = (DSACryptoServiceProvider)certificate.PrivateKey;
						CspParameters parameters = DSACertificateExtensions.CopyCspParameters(cspAlgorithm);
						DSACryptoServiceProvider dsacryptoServiceProvider = new DSACryptoServiceProvider(parameters);
						result = dsacryptoServiceProvider;
					}
					else
					{
						CngKey key = CngKey.Open(safeNCryptKeyHandle, keyHandleOpenOptions);
						result = new DSACng(key);
					}
				}
			}
			return result;
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x00020FE4 File Offset: 0x0001F1E4
		[SecuritySafeCritical]
		public static X509Certificate2 CopyWithPrivateKey(this X509Certificate2 certificate, DSA privateKey)
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
			using (DSA dsapublicKey = certificate.GetDSAPublicKey())
			{
				if (dsapublicKey == null)
				{
					throw new ArgumentException(System.SR.GetString("Cryptography_PrivateKey_WrongAlgorithm"));
				}
				DSAParameters dsaparameters = dsapublicKey.ExportParameters(false);
				DSAParameters dsaparameters2 = privateKey.ExportParameters(false);
				if (!dsaparameters.G.SequenceEqual(dsaparameters2.G) || !dsaparameters.P.SequenceEqual(dsaparameters2.P) || !dsaparameters.Q.SequenceEqual(dsaparameters2.Q) || !dsaparameters.Y.SequenceEqual(dsaparameters2.Y))
				{
					throw new ArgumentException(System.SR.GetString("Cryptography_PrivateKey_DoesNotMatch"), "privateKey");
				}
			}
			DSACng dsacng = privateKey as DSACng;
			X509Certificate2 x509Certificate = null;
			if (dsacng != null)
			{
				x509Certificate = CertificateExtensionsCommon.CopyWithPersistedCngKey(certificate, dsacng.Key);
			}
			if (x509Certificate == null)
			{
				DSACryptoServiceProvider dsacryptoServiceProvider = privateKey as DSACryptoServiceProvider;
				if (dsacryptoServiceProvider != null)
				{
					x509Certificate = CertificateExtensionsCommon.CopyWithPersistedCapiKey(certificate, dsacryptoServiceProvider.CspKeyContainerInfo);
				}
			}
			if (x509Certificate == null)
			{
				DSAParameters dsaparameters3 = privateKey.ExportParameters(true);
				using (PinAndClear.Track(dsaparameters3.X))
				{
					DSACng dsacng2;
					dsacng = (dsacng2 = new DSACng());
					try
					{
						dsacng.ImportParameters(dsaparameters3);
						x509Certificate = CertificateExtensionsCommon.CopyWithEphemeralCngKey(certificate, dsacng.Key);
					}
					finally
					{
						if (dsacng2 != null)
						{
							((IDisposable)dsacng2).Dispose();
						}
					}
				}
			}
			return x509Certificate;
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x00021174 File Offset: 0x0001F374
		private static bool IsDSA(X509Certificate2 certificate)
		{
			return certificate.PublicKey.Oid.Value == "1.2.840.10040.4.1";
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x00021190 File Offset: 0x0001F390
		internal static CspParameters CopyCspParameters(ICspAsymmetricAlgorithm cspAlgorithm)
		{
			CspKeyContainerInfo cspKeyContainerInfo = cspAlgorithm.CspKeyContainerInfo;
			CspParameters cspParameters = new CspParameters(cspKeyContainerInfo.ProviderType, cspKeyContainerInfo.ProviderName, cspKeyContainerInfo.KeyContainerName)
			{
				Flags = CspProviderFlags.UseExistingKey,
				KeyNumber = (int)cspKeyContainerInfo.KeyNumber
			};
			if (cspKeyContainerInfo.MachineKeyStore)
			{
				cspParameters.Flags |= CspProviderFlags.UseMachineKeyStore;
			}
			return cspParameters;
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x000211E8 File Offset: 0x0001F3E8
		[SecuritySafeCritical]
		private static byte[] ToBigEndianByteArray(CapiNative.CRYPTOAPI_BLOB blob)
		{
			int cbData = blob.cbData;
			byte[] array = new byte[cbData];
			Marshal.Copy(blob.pbData, array, 0, cbData);
			Array.Reverse(array);
			return array;
		}
	}
}
