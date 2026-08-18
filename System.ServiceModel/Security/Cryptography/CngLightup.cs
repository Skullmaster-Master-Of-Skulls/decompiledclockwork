using System;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace System.Security.Cryptography
{
	// Token: 0x0200001B RID: 27
	internal static class CngLightup
	{
		// Token: 0x060000D2 RID: 210 RVA: 0x00006830 File Offset: 0x00004A30
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

		// Token: 0x060000D3 RID: 211 RVA: 0x00006894 File Offset: 0x00004A94
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

		// Token: 0x060000D4 RID: 212 RVA: 0x00006934 File Offset: 0x00004B34
		internal static DSA GetDSAPublicKey(X509Certificate2 cert)
		{
			if (CngLightup.s_getDsaPublicKey == null)
			{
				CngLightup.s_getDsaPublicKey = (CngLightup.BindCoreDelegate<DSA>("DSA", true) ?? CngLightup.BindGetCapiPublicKey<DSA, DSACryptoServiceProvider>("1.2.840.10040.4.1"));
			}
			return CngLightup.s_getDsaPublicKey(cert);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x0000696C File Offset: 0x00004B6C
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

		// Token: 0x060000D6 RID: 214 RVA: 0x000069D0 File Offset: 0x00004BD0
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

		// Token: 0x060000D7 RID: 215 RVA: 0x00006A28 File Offset: 0x00004C28
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

		// Token: 0x060000D8 RID: 216 RVA: 0x00006A80 File Offset: 0x00004C80
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

		// Token: 0x060000D9 RID: 217 RVA: 0x00006B70 File Offset: 0x00004D70
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

		// Token: 0x060000DA RID: 218 RVA: 0x00006C7C File Offset: 0x00004E7C
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

		// Token: 0x060000DB RID: 219 RVA: 0x00006CDC File Offset: 0x00004EDC
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

		// Token: 0x060000DC RID: 220 RVA: 0x00006D3C File Offset: 0x00004F3C
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

		// Token: 0x060000DD RID: 221 RVA: 0x00006D9C File Offset: 0x00004F9C
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

		// Token: 0x060000DE RID: 222 RVA: 0x00006DFC File Offset: 0x00004FFC
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

		// Token: 0x060000DF RID: 223 RVA: 0x00006E88 File Offset: 0x00005088
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

		// Token: 0x060000E0 RID: 224 RVA: 0x00006F08 File Offset: 0x00005108
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

		// Token: 0x060000E1 RID: 225 RVA: 0x00006F30 File Offset: 0x00005130
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

		// Token: 0x060000E2 RID: 226 RVA: 0x00006F60 File Offset: 0x00005160
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

		// Token: 0x060000E3 RID: 227 RVA: 0x00006FE8 File Offset: 0x000051E8
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

		// Token: 0x060000E4 RID: 228 RVA: 0x00007040 File Offset: 0x00005240
		private static Type GetSystemCoreType(string namespaceQualifiedTypeName, bool throwOnError = true)
		{
			Assembly assembly = typeof(CngKey).Assembly;
			return assembly.GetType(namespaceQualifiedTypeName, throwOnError);
		}

		// Token: 0x040000A1 RID: 161
		private const string DsaOid = "1.2.840.10040.4.1";

		// Token: 0x040000A2 RID: 162
		private const string RsaOid = "1.2.840.113549.1.1.1";

		// Token: 0x040000A3 RID: 163
		private const string HashAlgorithmNameTypeName = "System.Security.Cryptography.HashAlgorithmName";

		// Token: 0x040000A4 RID: 164
		private const string RSASignaturePaddingTypeName = "System.Security.Cryptography.RSASignaturePadding";

		// Token: 0x040000A5 RID: 165
		private const string RSAEncryptionPaddingTypeName = "System.Security.Cryptography.RSAEncryptionPadding";

		// Token: 0x040000A6 RID: 166
		private const string RSACngTypeName = "System.Security.Cryptography.RSACng";

		// Token: 0x040000A7 RID: 167
		private const string DSACngTypeName = "System.Security.Cryptography.DSACng";

		// Token: 0x040000A8 RID: 168
		private static readonly Type s_hashAlgorithmNameType = typeof(object).Assembly.GetType("System.Security.Cryptography.HashAlgorithmName", false);

		// Token: 0x040000A9 RID: 169
		private static readonly Type s_rsaSignaturePaddingType = typeof(object).Assembly.GetType("System.Security.Cryptography.RSASignaturePadding", false);

		// Token: 0x040000AA RID: 170
		private static readonly Type s_rsaEncryptionPaddingType = typeof(object).Assembly.GetType("System.Security.Cryptography.RSAEncryptionPadding", false);

		// Token: 0x040000AB RID: 171
		private static readonly object s_pkcs1SignaturePadding = (CngLightup.s_rsaSignaturePaddingType == null) ? null : CngLightup.s_rsaSignaturePaddingType.GetProperty("Pkcs1", BindingFlags.Static | BindingFlags.Public).GetValue(null);

		// Token: 0x040000AC RID: 172
		private static readonly object s_pkcs1EncryptionPadding = (CngLightup.s_rsaEncryptionPaddingType == null) ? null : CngLightup.s_rsaEncryptionPaddingType.GetProperty("Pkcs1", BindingFlags.Static | BindingFlags.Public).GetValue(null);

		// Token: 0x040000AD RID: 173
		private static readonly object s_oaepSha1EncryptionPadding = (CngLightup.s_rsaEncryptionPaddingType == null) ? null : CngLightup.s_rsaEncryptionPaddingType.GetProperty("OaepSHA1", BindingFlags.Static | BindingFlags.Public).GetValue(null);

		// Token: 0x040000AE RID: 174
		private static readonly Lazy<bool> s_preferRsaCng = new Lazy<bool>(new Func<bool>(CngLightup.DetectRsaCngSupport));

		// Token: 0x040000AF RID: 175
		private static volatile Func<X509Certificate2, DSA> s_getDsaPublicKey;

		// Token: 0x040000B0 RID: 176
		private static volatile Func<X509Certificate2, DSA> s_getDsaPrivateKey;

		// Token: 0x040000B1 RID: 177
		private static volatile Func<X509Certificate2, RSA> s_getRsaPublicKey;

		// Token: 0x040000B2 RID: 178
		private static volatile Func<X509Certificate2, RSA> s_getRsaPrivateKey;

		// Token: 0x040000B3 RID: 179
		private static volatile Func<RSA, byte[], string, byte[]> s_rsaPkcs1SignMethod;

		// Token: 0x040000B4 RID: 180
		private static volatile Func<RSA, byte[], byte[], string, bool> s_rsaPkcs1VerifyMethod;

		// Token: 0x040000B5 RID: 181
		private static volatile Func<RSA, byte[], byte[]> s_rsaPkcs1EncryptMethod;

		// Token: 0x040000B6 RID: 182
		private static volatile Func<RSA, byte[], byte[]> s_rsaPkcs1DecryptMethod;

		// Token: 0x040000B7 RID: 183
		private static volatile Func<RSA, byte[], byte[]> s_rsaOaepSha1EncryptMethod;

		// Token: 0x040000B8 RID: 184
		private static volatile Func<RSA, byte[], byte[]> s_rsaOaepSha1DecryptMethod;

		// Token: 0x040000B9 RID: 185
		private static volatile Func<X509Certificate2, ECDsa> s_getECDsaPublicKey;

		// Token: 0x040000BA RID: 186
		private static volatile Func<X509Certificate2, ECDsa> s_getECDsaPrivateKey;
	}
}
