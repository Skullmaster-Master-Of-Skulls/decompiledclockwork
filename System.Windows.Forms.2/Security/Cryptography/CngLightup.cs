using System;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace System.Security.Cryptography
{
	// Token: 0x02000109 RID: 265
	internal static class CngLightup
	{
		// Token: 0x06000463 RID: 1123 RVA: 0x000106E0 File Offset: 0x0000E8E0
		internal static RSA GetRSAPublicKey(X509Certificate2 cert)
		{
			if (CngLightup.s_getRsaPublicKey == null)
			{
				if (CngLightup.s_preferRsaCng.Value)
				{
					CngLightup.s_getRsaPublicKey = (CngLightup.BindCoreDelegate<RSA>("RSA", true) ?? CngLightup.BindGetCapiPublicKey<RSA, RSACryptoServiceProvider>("1.2.840.113549.1.1.1"));
				}
				else
				{
					CngLightup.s_getRsaPublicKey = CngLightup.BindGetCapiPublicKey<RSA, RSACryptoServiceProvider>("1.2.840.113549.1.1.1");
				}
			}
			return CngLightup.s_getRsaPublicKey(cert);
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x00010744 File Offset: 0x0000E944
		internal static RSA GetRSAPrivateKey(X509Certificate2 cert)
		{
			if (CngLightup.s_getRsaPrivateKey == null)
			{
				if (CngLightup.s_preferRsaCng.Value)
				{
					Func<X509Certificate2, RSA> func;
					if ((func = CngLightup.BindCoreDelegate<RSA>("RSA", false)) == null)
					{
						func = CngLightup.BindGetCapiPrivateKey<RSA>("1.2.840.113549.1.1.1", (CspParameters csp) => new RSACryptoServiceProvider(csp));
					}
					CngLightup.s_getRsaPrivateKey = func;
				}
				else
				{
					CngLightup.s_getRsaPrivateKey = CngLightup.BindGetCapiPrivateKey<RSA>("1.2.840.113549.1.1.1", (CspParameters csp) => new RSACryptoServiceProvider(csp));
				}
			}
			return CngLightup.s_getRsaPrivateKey(cert);
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x000107E4 File Offset: 0x0000E9E4
		internal static DSA GetDSAPublicKey(X509Certificate2 cert)
		{
			if (CngLightup.s_getDsaPublicKey == null)
			{
				CngLightup.s_getDsaPublicKey = (CngLightup.BindCoreDelegate<DSA>("DSA", true) ?? CngLightup.BindGetCapiPublicKey<DSA, DSACryptoServiceProvider>("1.2.840.10040.4.1"));
			}
			return CngLightup.s_getDsaPublicKey(cert);
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x0001081C File Offset: 0x0000EA1C
		internal static DSA GetDSAPrivateKey(X509Certificate2 cert)
		{
			if (CngLightup.s_getDsaPrivateKey == null)
			{
				Func<X509Certificate2, DSA> func;
				if ((func = CngLightup.BindCoreDelegate<DSA>("DSA", false)) == null)
				{
					func = CngLightup.BindGetCapiPrivateKey<DSA>("1.2.840.10040.4.1", (CspParameters csp) => new DSACryptoServiceProvider(csp));
				}
				CngLightup.s_getDsaPrivateKey = func;
			}
			return CngLightup.s_getDsaPrivateKey(cert);
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x00010880 File Offset: 0x0000EA80
		internal static ECDsa GetECDsaPublicKey(X509Certificate2 cert)
		{
			if (CngLightup.s_getECDsaPublicKey == null)
			{
				Func<X509Certificate2, ECDsa> func;
				if ((func = CngLightup.BindCoreDelegate<ECDsa>("ECDsa", true)) == null && (func = CngLightup.<>c.<>9__30_0) == null)
				{
					func = (CngLightup.<>c.<>9__30_0 = ((X509Certificate2 c) => null));
				}
				CngLightup.s_getECDsaPublicKey = func;
			}
			return CngLightup.s_getECDsaPublicKey(cert);
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x000108D8 File Offset: 0x0000EAD8
		internal static ECDsa GetECDsaPrivateKey(X509Certificate2 cert)
		{
			if (CngLightup.s_getECDsaPrivateKey == null)
			{
				Func<X509Certificate2, ECDsa> func;
				if ((func = CngLightup.BindCoreDelegate<ECDsa>("ECDsa", false)) == null && (func = CngLightup.<>c.<>9__31_0) == null)
				{
					func = (CngLightup.<>c.<>9__31_0 = ((X509Certificate2 c) => null));
				}
				CngLightup.s_getECDsaPrivateKey = func;
			}
			return CngLightup.s_getECDsaPrivateKey(cert);
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x00010930 File Offset: 0x0000EB30
		internal static byte[] Pkcs1SignData(RSA rsa, byte[] data, string hashAlgorithmName)
		{
			RSACryptoServiceProvider rsacryptoServiceProvider = rsa as RSACryptoServiceProvider;
			if (rsacryptoServiceProvider != null)
			{
				return rsacryptoServiceProvider.SignData(data, hashAlgorithmName);
			}
			if (CngLightup.s_rsaPkcs1SignMethod == null)
			{
				Type[] types = new Type[]
				{
					typeof(byte[]),
					CngLightup.s_hashAlgorithmNameType,
					CngLightup.s_rsaSignaturePaddingType
				};
				MethodInfo method = typeof(RSA).GetMethod("SignData", BindingFlags.Instance | BindingFlags.Public, null, types, null);
				Type type = typeof(Func<, , , , >).MakeGenericType(new Type[]
				{
					typeof(RSA),
					typeof(byte[]),
					CngLightup.s_hashAlgorithmNameType,
					CngLightup.s_rsaSignaturePaddingType,
					typeof(byte[])
				});
				Delegate openDelegate = Delegate.CreateDelegate(type, method);
				CngLightup.s_rsaPkcs1SignMethod = delegate(RSA delegateRsa, byte[] delegateData, string delegateAlgorithm)
				{
					object obj = Activator.CreateInstance(CngLightup.s_hashAlgorithmNameType, new object[]
					{
						delegateAlgorithm
					});
					object[] args = new object[]
					{
						delegateRsa,
						delegateData,
						obj,
						CngLightup.s_pkcs1SignaturePadding
					};
					return (byte[])openDelegate.DynamicInvoke(args);
				};
			}
			return CngLightup.s_rsaPkcs1SignMethod(rsa, data, hashAlgorithmName);
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x00010A20 File Offset: 0x0000EC20
		internal static bool Pkcs1VerifyData(RSA rsa, byte[] data, byte[] signature, string hashAlgorithmName)
		{
			RSACryptoServiceProvider rsacryptoServiceProvider = rsa as RSACryptoServiceProvider;
			if (rsacryptoServiceProvider != null)
			{
				return rsacryptoServiceProvider.VerifyData(data, hashAlgorithmName, signature);
			}
			if (CngLightup.s_rsaPkcs1VerifyMethod == null)
			{
				Type[] types = new Type[]
				{
					typeof(byte[]),
					typeof(byte[]),
					CngLightup.s_hashAlgorithmNameType,
					CngLightup.s_rsaSignaturePaddingType
				};
				MethodInfo method = typeof(RSA).GetMethod("VerifyData", BindingFlags.Instance | BindingFlags.Public, null, types, null);
				Type type = typeof(Func<, , , , , >).MakeGenericType(new Type[]
				{
					typeof(RSA),
					typeof(byte[]),
					typeof(byte[]),
					CngLightup.s_hashAlgorithmNameType,
					CngLightup.s_rsaSignaturePaddingType,
					typeof(bool)
				});
				Delegate openDelegate = Delegate.CreateDelegate(type, method);
				CngLightup.s_rsaPkcs1VerifyMethod = delegate(RSA delegateRsa, byte[] delegateData, byte[] delegateSignature, string delegateAlgorithm)
				{
					object obj = Activator.CreateInstance(CngLightup.s_hashAlgorithmNameType, new object[]
					{
						delegateAlgorithm
					});
					object[] args = new object[]
					{
						delegateRsa,
						delegateData,
						delegateSignature,
						obj,
						CngLightup.s_pkcs1SignaturePadding
					};
					return (bool)openDelegate.DynamicInvoke(args);
				};
			}
			return CngLightup.s_rsaPkcs1VerifyMethod(rsa, data, signature, hashAlgorithmName);
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x00010B2C File Offset: 0x0000ED2C
		internal static byte[] Pkcs1Encrypt(RSA rsa, byte[] data)
		{
			RSACryptoServiceProvider rsacryptoServiceProvider = rsa as RSACryptoServiceProvider;
			if (rsacryptoServiceProvider != null)
			{
				return rsacryptoServiceProvider.Encrypt(data, false);
			}
			if (CngLightup.s_rsaPkcs1EncryptMethod == null)
			{
				Delegate openDelegate = CngLightup.BindRsaCryptMethod("Encrypt");
				CngLightup.s_rsaPkcs1EncryptMethod = ((RSA delegateRsa, byte[] delegateData) => (byte[])openDelegate.DynamicInvoke(new object[]
				{
					delegateRsa,
					delegateData,
					CngLightup.s_pkcs1EncryptionPadding
				}));
			}
			return CngLightup.s_rsaPkcs1EncryptMethod(rsa, data);
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00010B8C File Offset: 0x0000ED8C
		internal static byte[] Pkcs1Decrypt(RSA rsa, byte[] data)
		{
			RSACryptoServiceProvider rsacryptoServiceProvider = rsa as RSACryptoServiceProvider;
			if (rsacryptoServiceProvider != null)
			{
				return rsacryptoServiceProvider.Decrypt(data, false);
			}
			if (CngLightup.s_rsaPkcs1DecryptMethod == null)
			{
				Delegate openDelegate = CngLightup.BindRsaCryptMethod("Decrypt");
				CngLightup.s_rsaPkcs1DecryptMethod = ((RSA delegateRsa, byte[] delegateData) => (byte[])openDelegate.DynamicInvoke(new object[]
				{
					delegateRsa,
					delegateData,
					CngLightup.s_pkcs1EncryptionPadding
				}));
			}
			return CngLightup.s_rsaPkcs1DecryptMethod(rsa, data);
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x00010BEC File Offset: 0x0000EDEC
		internal static byte[] OaepSha1Encrypt(RSA rsa, byte[] data)
		{
			RSACryptoServiceProvider rsacryptoServiceProvider = rsa as RSACryptoServiceProvider;
			if (rsacryptoServiceProvider != null)
			{
				return rsacryptoServiceProvider.Encrypt(data, true);
			}
			if (CngLightup.s_rsaOaepSha1EncryptMethod == null)
			{
				Delegate openDelegate = CngLightup.BindRsaCryptMethod("Encrypt");
				CngLightup.s_rsaOaepSha1EncryptMethod = ((RSA delegateRsa, byte[] delegateData) => (byte[])openDelegate.DynamicInvoke(new object[]
				{
					delegateRsa,
					delegateData,
					CngLightup.s_oaepSha1EncryptionPadding
				}));
			}
			return CngLightup.s_rsaOaepSha1EncryptMethod(rsa, data);
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x00010C4C File Offset: 0x0000EE4C
		internal static byte[] OaepSha1Decrypt(RSA rsa, byte[] data)
		{
			RSACryptoServiceProvider rsacryptoServiceProvider = rsa as RSACryptoServiceProvider;
			if (rsacryptoServiceProvider != null)
			{
				return rsacryptoServiceProvider.Decrypt(data, true);
			}
			if (CngLightup.s_rsaOaepSha1DecryptMethod == null)
			{
				Delegate openDelegate = CngLightup.BindRsaCryptMethod("Decrypt");
				CngLightup.s_rsaOaepSha1DecryptMethod = ((RSA delegateRsa, byte[] delegateData) => (byte[])openDelegate.DynamicInvoke(new object[]
				{
					delegateRsa,
					delegateData,
					CngLightup.s_oaepSha1EncryptionPadding
				}));
			}
			return CngLightup.s_rsaOaepSha1DecryptMethod(rsa, data);
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x00010CAC File Offset: 0x0000EEAC
		private static Delegate BindRsaCryptMethod(string methodName)
		{
			Type[] types = new Type[]
			{
				typeof(byte[]),
				CngLightup.s_rsaEncryptionPaddingType
			};
			MethodInfo method = typeof(RSA).GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public, null, types, null);
			Type type = typeof(Func<, , , >).MakeGenericType(new Type[]
			{
				typeof(RSA),
				typeof(byte[]),
				CngLightup.s_rsaEncryptionPaddingType,
				typeof(byte[])
			});
			return Delegate.CreateDelegate(type, method);
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x00010D38 File Offset: 0x0000EF38
		private static bool DetectRsaCngSupport()
		{
			Type systemCoreType = CngLightup.GetSystemCoreType("System.Security.Cryptography.RSACng", false);
			if (systemCoreType == null)
			{
				return false;
			}
			Type systemCoreType2 = CngLightup.GetSystemCoreType("System.Security.Cryptography.DSACng", false);
			if (systemCoreType2 == null)
			{
				return false;
			}
			Type[] types = new Type[]
			{
				typeof(byte[]),
				CngLightup.s_hashAlgorithmNameType
			};
			MethodInfo method = typeof(DSA).GetMethod("SignData", BindingFlags.Instance | BindingFlags.Public, null, types, null);
			return !(method == null);
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x00010DB8 File Offset: 0x0000EFB8
		private static Func<X509Certificate2, T> BindGetCapiPublicKey<T, TCryptoServiceProvider>(string algorithmOid) where T : AsymmetricAlgorithm where TCryptoServiceProvider : T, ICspAsymmetricAlgorithm, new()
		{
			return delegate(X509Certificate2 cert)
			{
				PublicKey publicKey = cert.PublicKey;
				if (publicKey.Oid.Value != algorithmOid)
				{
					return default(T);
				}
				AsymmetricAlgorithm key = publicKey.Key;
				ICspAsymmetricAlgorithm cspAsymmetricAlgorithm = (ICspAsymmetricAlgorithm)key;
				byte[] rawData = cspAsymmetricAlgorithm.ExportCspBlob(false);
				TCryptoServiceProvider tcryptoServiceProvider = Activator.CreateInstance<TCryptoServiceProvider>();
				tcryptoServiceProvider.ImportCspBlob(rawData);
				return (T)((object)tcryptoServiceProvider);
			};
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x00010DE0 File Offset: 0x0000EFE0
		private static Func<X509Certificate2, T> BindGetCapiPrivateKey<T>(string algorithmOid, Func<CspParameters, T> instanceFactory) where T : AsymmetricAlgorithm
		{
			return delegate(X509Certificate2 cert)
			{
				if (!cert.HasPrivateKey)
				{
					return default(T);
				}
				PublicKey publicKey = cert.PublicKey;
				if (publicKey.Oid.Value != algorithmOid)
				{
					return default(T);
				}
				AsymmetricAlgorithm privateKey = cert.PrivateKey;
				ICspAsymmetricAlgorithm cspAlgorithm = (ICspAsymmetricAlgorithm)privateKey;
				CspParameters arg = CngLightup.CopyCspParameters(cspAlgorithm);
				return instanceFactory(arg);
			};
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x00010E10 File Offset: 0x0000F010
		private static Func<X509Certificate2, T> BindCoreDelegate<T>(string algorithmName, bool isPublic)
		{
			string namespaceQualifiedTypeName = "System.Security.Cryptography.X509Certificates." + algorithmName + "CertificateExtensions";
			Type systemCoreType = CngLightup.GetSystemCoreType(namespaceQualifiedTypeName, false);
			if (systemCoreType == null)
			{
				return null;
			}
			string name = "Get" + algorithmName + (isPublic ? "Public" : "Private") + "Key";
			MethodInfo method = systemCoreType.GetMethod(name, BindingFlags.Static | BindingFlags.Public, null, new Type[]
			{
				typeof(X509Certificate2)
			}, null);
			return (Func<X509Certificate2, T>)method.CreateDelegate(typeof(Func<X509Certificate2, T>));
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x00010E98 File Offset: 0x0000F098
		private static CspParameters CopyCspParameters(ICspAsymmetricAlgorithm cspAlgorithm)
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

		// Token: 0x06000475 RID: 1141 RVA: 0x00010EF0 File Offset: 0x0000F0F0
		private static Type GetSystemCoreType(string namespaceQualifiedTypeName, bool throwOnError = true)
		{
			Assembly assembly = typeof(CngKey).Assembly;
			return assembly.GetType(namespaceQualifiedTypeName, throwOnError);
		}

		// Token: 0x04000491 RID: 1169
		private const string DsaOid = "1.2.840.10040.4.1";

		// Token: 0x04000492 RID: 1170
		private const string RsaOid = "1.2.840.113549.1.1.1";

		// Token: 0x04000493 RID: 1171
		private const string HashAlgorithmNameTypeName = "System.Security.Cryptography.HashAlgorithmName";

		// Token: 0x04000494 RID: 1172
		private const string RSASignaturePaddingTypeName = "System.Security.Cryptography.RSASignaturePadding";

		// Token: 0x04000495 RID: 1173
		private const string RSAEncryptionPaddingTypeName = "System.Security.Cryptography.RSAEncryptionPadding";

		// Token: 0x04000496 RID: 1174
		private const string RSACngTypeName = "System.Security.Cryptography.RSACng";

		// Token: 0x04000497 RID: 1175
		private const string DSACngTypeName = "System.Security.Cryptography.DSACng";

		// Token: 0x04000498 RID: 1176
		private static readonly Type s_hashAlgorithmNameType = typeof(object).Assembly.GetType("System.Security.Cryptography.HashAlgorithmName", false);

		// Token: 0x04000499 RID: 1177
		private static readonly Type s_rsaSignaturePaddingType = typeof(object).Assembly.GetType("System.Security.Cryptography.RSASignaturePadding", false);

		// Token: 0x0400049A RID: 1178
		private static readonly Type s_rsaEncryptionPaddingType = typeof(object).Assembly.GetType("System.Security.Cryptography.RSAEncryptionPadding", false);

		// Token: 0x0400049B RID: 1179
		private static readonly object s_pkcs1SignaturePadding = (CngLightup.s_rsaSignaturePaddingType == null) ? null : CngLightup.s_rsaSignaturePaddingType.GetProperty("Pkcs1", BindingFlags.Static | BindingFlags.Public).GetValue(null);

		// Token: 0x0400049C RID: 1180
		private static readonly object s_pkcs1EncryptionPadding = (CngLightup.s_rsaEncryptionPaddingType == null) ? null : CngLightup.s_rsaEncryptionPaddingType.GetProperty("Pkcs1", BindingFlags.Static | BindingFlags.Public).GetValue(null);

		// Token: 0x0400049D RID: 1181
		private static readonly object s_oaepSha1EncryptionPadding = (CngLightup.s_rsaEncryptionPaddingType == null) ? null : CngLightup.s_rsaEncryptionPaddingType.GetProperty("OaepSHA1", BindingFlags.Static | BindingFlags.Public).GetValue(null);

		// Token: 0x0400049E RID: 1182
		private static readonly Lazy<bool> s_preferRsaCng = new Lazy<bool>(new Func<bool>(CngLightup.DetectRsaCngSupport));

		// Token: 0x0400049F RID: 1183
		private static volatile Func<X509Certificate2, DSA> s_getDsaPublicKey;

		// Token: 0x040004A0 RID: 1184
		private static volatile Func<X509Certificate2, DSA> s_getDsaPrivateKey;

		// Token: 0x040004A1 RID: 1185
		private static volatile Func<X509Certificate2, RSA> s_getRsaPublicKey;

		// Token: 0x040004A2 RID: 1186
		private static volatile Func<X509Certificate2, RSA> s_getRsaPrivateKey;

		// Token: 0x040004A3 RID: 1187
		private static volatile Func<RSA, byte[], string, byte[]> s_rsaPkcs1SignMethod;

		// Token: 0x040004A4 RID: 1188
		private static volatile Func<RSA, byte[], byte[], string, bool> s_rsaPkcs1VerifyMethod;

		// Token: 0x040004A5 RID: 1189
		private static volatile Func<RSA, byte[], byte[]> s_rsaPkcs1EncryptMethod;

		// Token: 0x040004A6 RID: 1190
		private static volatile Func<RSA, byte[], byte[]> s_rsaPkcs1DecryptMethod;

		// Token: 0x040004A7 RID: 1191
		private static volatile Func<RSA, byte[], byte[]> s_rsaOaepSha1EncryptMethod;

		// Token: 0x040004A8 RID: 1192
		private static volatile Func<RSA, byte[], byte[]> s_rsaOaepSha1DecryptMethod;

		// Token: 0x040004A9 RID: 1193
		private static volatile Func<X509Certificate2, ECDsa> s_getECDsaPublicKey;

		// Token: 0x040004AA RID: 1194
		private static volatile Func<X509Certificate2, ECDsa> s_getECDsaPrivateKey;
	}
}
