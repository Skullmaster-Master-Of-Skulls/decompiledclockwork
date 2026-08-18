using System;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x02000124 RID: 292
	public static class ECDsaCertificateExtensions
	{
		// Token: 0x060009A4 RID: 2468 RVA: 0x00022890 File Offset: 0x00020A90
		[SecuritySafeCritical]
		public static ECDsa GetECDsaPrivateKey(this X509Certificate2 certificate)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			if (!certificate.HasPrivateKey || !ECDsaCertificateExtensions.IsECDsa(certificate))
			{
				return null;
			}
			ECDsa result;
			using (SafeCertContextHandle certificateContext = X509Native.GetCertificateContext(certificate))
			{
				CngKeyHandleOpenOptions keyHandleOpenOptions;
				using (SafeNCryptKeyHandle safeNCryptKeyHandle = X509Native.TryAcquireCngPrivateKey(certificateContext, out keyHandleOpenOptions))
				{
					CngKey key = CngKey.Open(safeNCryptKeyHandle, keyHandleOpenOptions);
					result = new ECDsaCng(key);
				}
			}
			return result;
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x00022914 File Offset: 0x00020B14
		[SecuritySafeCritical]
		private static SafeBCryptKeyHandle ImportPublicKeyInfo(SafeCertContextHandle certContext)
		{
			IntPtr ptr = certContext.DangerousGetHandle();
			X509Native.CERT_CONTEXT cert_CONTEXT = (X509Native.CERT_CONTEXT)Marshal.PtrToStructure(ptr, typeof(X509Native.CERT_CONTEXT));
			IntPtr ptr2 = new IntPtr((long)cert_CONTEXT.pCertInfo + (long)Marshal.OffsetOf(typeof(X509Native.CERT_INFO), "SubjectPublicKeyInfo"));
			X509Native.CERT_PUBLIC_KEY_INFO certPublicKeyInfo = (X509Native.CERT_PUBLIC_KEY_INFO)Marshal.PtrToStructure(ptr2, typeof(X509Native.CERT_PUBLIC_KEY_INFO));
			SafeBCryptKeyHandle result = BCryptNative.ImportAsymmetricPublicKey(certPublicKeyInfo, 0);
			GC.KeepAlive(certContext);
			return result;
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x00022994 File Offset: 0x00020B94
		[SecuritySafeCritical]
		public static ECDsa GetECDsaPublicKey(this X509Certificate2 certificate)
		{
			if (LocalAppContextSwitches.UseLegacyPublicKeyBehavior)
			{
				return ECDsaCertificateExtensions.LegacyGetECDsaPublicKey(certificate);
			}
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			if (!ECDsaCertificateExtensions.IsECDsa(certificate))
			{
				return null;
			}
			ECDsa result;
			using (SafeCertContextHandle certificateContext = X509Native.GetCertificateContext(certificate))
			{
				using (SafeBCryptKeyHandle safeBCryptKeyHandle = ECDsaCertificateExtensions.ImportPublicKeyInfo(certificateContext))
				{
					if (safeBCryptKeyHandle.IsInvalid)
					{
						throw new CryptographicException("SR.GetString(SR.Cryptography_OpenInvalidHandle)");
					}
					string curveName = ECDsaCertificateExtensions.GetCurveName(safeBCryptKeyHandle);
					if (curveName == null)
					{
						CngKeyBlobFormat cngKeyBlobFormat = ECDsaCertificateExtensions.HasExplicitParameters(safeBCryptKeyHandle) ? CngKeyBlobFormat.EccFullPublicBlob : CngKeyBlobFormat.EccPublicBlob;
						byte[] keyBlob = BCryptNative.ExportBCryptKey(safeBCryptKeyHandle, cngKeyBlobFormat.Format);
						using (CngKey cngKey = CngKey.Import(keyBlob, cngKeyBlobFormat))
						{
							return new ECDsaCng(cngKey);
						}
					}
					CngKeyBlobFormat eccPublicBlob = CngKeyBlobFormat.EccPublicBlob;
					byte[] ecBlob = BCryptNative.ExportBCryptKey(safeBCryptKeyHandle, eccPublicBlob.Format);
					ECParameters parameters = default(ECParameters);
					ECDsaCertificateExtensions.ExportNamedCurveParameters(ref parameters, ecBlob, false);
					parameters.Curve = ECCurve.CreateFromFriendlyName(curveName);
					ECDsaCng ecdsaCng = new ECDsaCng();
					ecdsaCng.ImportParameters(parameters);
					result = ecdsaCng;
				}
			}
			return result;
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x00022AC0 File Offset: 0x00020CC0
		[SecuritySafeCritical]
		private static ECDsa LegacyGetECDsaPublicKey(X509Certificate2 certificate)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			if (!ECDsaCertificateExtensions.IsECDsa(certificate))
			{
				return null;
			}
			SafeCertContextHandle certificateContext = X509Native.GetCertificateContext(certificate);
			IntPtr ptr = certificateContext.DangerousGetHandle();
			X509Native.CERT_CONTEXT cert_CONTEXT = (X509Native.CERT_CONTEXT)Marshal.PtrToStructure(ptr, typeof(X509Native.CERT_CONTEXT));
			IntPtr ptr2 = new IntPtr((long)cert_CONTEXT.pCertInfo + (long)Marshal.OffsetOf(typeof(X509Native.CERT_INFO), "SubjectPublicKeyInfo"));
			X509Native.CERT_PUBLIC_KEY_INFO certPublicKeyInfo = (X509Native.CERT_PUBLIC_KEY_INFO)Marshal.PtrToStructure(ptr2, typeof(X509Native.CERT_PUBLIC_KEY_INFO));
			CngKey key;
			using (SafeBCryptKeyHandle safeBCryptKeyHandle = BCryptNative.ImportAsymmetricPublicKey(certPublicKeyInfo, 0))
			{
				if (safeBCryptKeyHandle.IsInvalid)
				{
					throw new CryptographicException("SR.GetString(SR.Cryptography_OpenInvalidHandle)");
				}
				key = ECDsaCertificateExtensions.LegacyBCryptHandleToNCryptHandle(safeBCryptKeyHandle);
			}
			GC.KeepAlive(certificateContext);
			return new ECDsaCng(key);
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x00022BA0 File Offset: 0x00020DA0
		[SecuritySafeCritical]
		public static X509Certificate2 CopyWithPrivateKey(this X509Certificate2 certificate, ECDsa privateKey)
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
			using (ECDsa ecdsaPublicKey = certificate.GetECDsaPublicKey())
			{
				if (ecdsaPublicKey == null)
				{
					throw new ArgumentException(System.SR.GetString("Cryptography_PrivateKey_WrongAlgorithm"));
				}
				if (!ECDsaCertificateExtensions.IsSameKey(ecdsaPublicKey, privateKey))
				{
					throw new ArgumentException(System.SR.GetString("Cryptography_PrivateKey_DoesNotMatch"), "privateKey");
				}
			}
			ECDsaCng ecdsaCng = privateKey as ECDsaCng;
			X509Certificate2 x509Certificate = null;
			if (ecdsaCng != null)
			{
				x509Certificate = CertificateExtensionsCommon.CopyWithPersistedCngKey(certificate, ecdsaCng.Key);
			}
			if (x509Certificate == null)
			{
				ECParameters ecparameters = privateKey.ExportParameters(true);
				using (PinAndClear.Track(ecparameters.D))
				{
					ECDsaCng ecdsaCng2;
					ecdsaCng = (ecdsaCng2 = new ECDsaCng());
					try
					{
						ecdsaCng.ImportParameters(ecparameters);
						x509Certificate = CertificateExtensionsCommon.CopyWithEphemeralCngKey(certificate, ecdsaCng.Key);
					}
					finally
					{
						if (ecdsaCng2 != null)
						{
							((IDisposable)ecdsaCng2).Dispose();
						}
					}
				}
			}
			return x509Certificate;
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x00022CB8 File Offset: 0x00020EB8
		private static bool IsSameKey(ECDsa a, ECDsa b)
		{
			ECParameters ecparameters = a.ExportParameters(false);
			ECParameters ecparameters2 = b.ExportParameters(false);
			if (ecparameters.Curve.CurveType != ecparameters2.Curve.CurveType)
			{
				return false;
			}
			if (!ecparameters.Q.X.SequenceEqual(ecparameters2.Q.X) || !ecparameters.Q.Y.SequenceEqual(ecparameters2.Q.Y))
			{
				return false;
			}
			ECCurve curve = ecparameters.Curve;
			ECCurve curve2 = ecparameters2.Curve;
			if (curve.IsNamed)
			{
				return curve.Oid.Value == curve2.Oid.Value && curve.Oid.FriendlyName == curve2.Oid.FriendlyName;
			}
			if (!curve.IsExplicit)
			{
				return false;
			}
			if (!curve.G.X.SequenceEqual(curve2.G.X) || !curve.G.Y.SequenceEqual(curve2.G.Y) || !curve.Order.SequenceEqual(curve2.Order) || !curve.A.SequenceEqual(curve2.A) || !curve.B.SequenceEqual(curve2.B))
			{
				return false;
			}
			if (curve.IsPrime)
			{
				return curve.Prime.SequenceEqual(curve2.Prime);
			}
			return curve.IsCharacteristic2 && curve.Polynomial.SequenceEqual(curve2.Polynomial);
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x00022E38 File Offset: 0x00021038
		private static bool HasExplicitParameters(SafeBCryptKeyHandle bcryptHandle)
		{
			return ECDsaCertificateExtensions.HasProperty(bcryptHandle, "ECCParameters");
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x00022E45 File Offset: 0x00021045
		private static string GetCurveName(SafeBCryptKeyHandle bcryptHandle)
		{
			return ECDsaCertificateExtensions.GetPropertyAsString(bcryptHandle, "ECCCurveName");
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x00022E54 File Offset: 0x00021054
		[SecuritySafeCritical]
		private unsafe static string GetPropertyAsString(SafeBCryptKeyHandle cryptHandle, string propertyName)
		{
			byte[] property = ECDsaCertificateExtensions.GetProperty(cryptHandle, propertyName);
			if (property == null || property.Length == 0)
			{
				return null;
			}
			fixed (byte* ptr = &property[0])
			{
				byte* value = ptr;
				return Marshal.PtrToStringUni((IntPtr)((void*)value));
			}
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x00022E8C File Offset: 0x0002108C
		[SecuritySafeCritical]
		private unsafe static void ExportNamedCurveParameters(ref ECParameters ecParams, byte[] ecBlob, bool includePrivateParameters)
		{
			fixed (byte* ptr = &ecBlob[0])
			{
				byte* ptr2 = ptr;
				Interop.BCrypt.BCRYPT_ECCKEY_BLOB* ptr3 = (Interop.BCrypt.BCRYPT_ECCKEY_BLOB*)ptr2;
				int num = sizeof(Interop.BCrypt.BCRYPT_ECCKEY_BLOB);
				ecParams.Q = new ECPoint
				{
					X = Interop.BCrypt.Consume(ecBlob, ref num, ptr3->cbKey),
					Y = Interop.BCrypt.Consume(ecBlob, ref num, ptr3->cbKey)
				};
				if (includePrivateParameters)
				{
					ecParams.D = Interop.BCrypt.Consume(ecBlob, ref num, ptr3->cbKey);
				}
			}
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x00022F04 File Offset: 0x00021104
		[SecuritySafeCritical]
		private static byte[] GetProperty(SafeBCryptKeyHandle cryptHandle, string propertyName)
		{
			int num;
			BCryptNative.ErrorCode errorCode = BCryptNative.UnsafeNativeMethods.BCryptGetProperty(cryptHandle, propertyName, null, 0, out num, 0);
			if (errorCode != BCryptNative.ErrorCode.Success)
			{
				return null;
			}
			byte[] array = new byte[num];
			errorCode = BCryptNative.UnsafeNativeMethods.BCryptGetProperty(cryptHandle, propertyName, array, array.Length, out num, 0);
			if (errorCode != BCryptNative.ErrorCode.Success)
			{
				return null;
			}
			Array.Resize<byte>(ref array, num);
			return array;
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x00022F48 File Offset: 0x00021148
		[SecuritySafeCritical]
		private static bool HasProperty(SafeBCryptKeyHandle cryptHandle, string propertyName)
		{
			int num;
			return BCryptNative.UnsafeNativeMethods.BCryptGetProperty(cryptHandle, propertyName, null, 0, out num, 0) == BCryptNative.ErrorCode.Success && num > 0;
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x00022F6C File Offset: 0x0002116C
		private static CngKey LegacyBCryptHandleToNCryptHandle(SafeBCryptKeyHandle bcryptKeyHandle)
		{
			byte[] keyBlob = BCryptNative.ExportBCryptKey(bcryptKeyHandle, "ECCPUBLICBLOB");
			return CngKey.Import(keyBlob, CngKeyBlobFormat.EccPublicBlob);
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x00022F94 File Offset: 0x00021194
		private static bool IsECDsa(X509Certificate2 certificate)
		{
			string friendlyName = certificate.PublicKey.Oid.FriendlyName;
			string value = certificate.PublicKey.Oid.Value;
			if (value != "1.2.840.10045.2.1")
			{
				return false;
			}
			foreach (X509Extension x509Extension in certificate.Extensions)
			{
				if (x509Extension.Oid.Value == "2.5.29.15")
				{
					X509KeyUsageExtension x509KeyUsageExtension = (X509KeyUsageExtension)x509Extension;
					return !x509KeyUsageExtension.KeyUsages.HasFlag(X509KeyUsageFlags.KeyAgreement) || (x509KeyUsageExtension.KeyUsages.HasFlag(X509KeyUsageFlags.DigitalSignature) || x509KeyUsageExtension.KeyUsages.HasFlag(X509KeyUsageFlags.NonRepudiation) || x509KeyUsageExtension.KeyUsages.HasFlag(X509KeyUsageFlags.KeyCertSign) || x509KeyUsageExtension.KeyUsages.HasFlag(X509KeyUsageFlags.CrlSign));
				}
			}
			return true;
		}
	}
}
