using System;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace System.Security.Cryptography
{
	// Token: 0x0200000C RID: 12
	internal static class CngLightup
	{
		// Token: 0x0600002C RID: 44 RVA: 0x00002904 File Offset: 0x00000B04
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

		// Token: 0x0600002D RID: 45 RVA: 0x00002968 File Offset: 0x00000B68
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

		// Token: 0x0600002E RID: 46 RVA: 0x00002A08 File Offset: 0x00000C08
		internal static DSA GetDSAPublicKey(X509Certificate2 cert)
		{
			if (CngLightup.s_getDsaPublicKey == null)
			{
				CngLightup.s_getDsaPublicKey = (CngLightup.BindCoreDelegate<DSA>("DSA", true) ?? CngLightup.BindGetCapiPublicKey<DSA, DSACryptoServiceProvider>("1.2.840.10040.4.1"));
			}
			return CngLightup.s_getDsaPublicKey(cert);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002A40 File Offset: 0x00000C40
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

		// Token: 0x06000030 RID: 48 RVA: 0x00002AA4 File Offset: 0x00000CA4
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

		// Token: 0x06000031 RID: 49 RVA: 0x00002B94 File Offset: 0x00000D94
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

		// Token: 0x06000032 RID: 50 RVA: 0x00002CA0 File Offset: 0x00000EA0
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

		// Token: 0x06000033 RID: 51 RVA: 0x00002D00 File Offset: 0x00000F00
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

		// Token: 0x06000034 RID: 52 RVA: 0x00002D60 File Offset: 0x00000F60
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

		// Token: 0x06000035 RID: 53 RVA: 0x00002DC0 File Offset: 0x00000FC0
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

		// Token: 0x06000036 RID: 54 RVA: 0x00002E20 File Offset: 0x00001020
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

		// Token: 0x06000037 RID: 55 RVA: 0x00002EAC File Offset: 0x000010AC
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

		// Token: 0x06000038 RID: 56 RVA: 0x00002F2C File Offset: 0x0000112C
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

		// Token: 0x06000039 RID: 57 RVA: 0x00002F54 File Offset: 0x00001154
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

		// Token: 0x0600003A RID: 58 RVA: 0x00002F84 File Offset: 0x00001184
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

		// Token: 0x0600003B RID: 59 RVA: 0x0000300C File Offset: 0x0000120C
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

		// Token: 0x0600003C RID: 60 RVA: 0x00003064 File Offset: 0x00001264
		private static Type GetSystemCoreType(string namespaceQualifiedTypeName, bool throwOnError = true)
		{
			string typeName = namespaceQualifiedTypeName + ", System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
			return Type.GetType(typeName, false, false);
		}

		// Token: 0x04000067 RID: 103
		private const string DsaOid = "1.2.840.10040.4.1";

		// Token: 0x04000068 RID: 104
		private const string RsaOid = "1.2.840.113549.1.1.1";

		// Token: 0x04000069 RID: 105
		private const string HashAlgorithmNameTypeName = "System.Security.Cryptography.HashAlgorithmName";

		// Token: 0x0400006A RID: 106
		private const string RSASignaturePaddingTypeName = "System.Security.Cryptography.RSASignaturePadding";

		// Token: 0x0400006B RID: 107
		private const string RSAEncryptionPaddingTypeName = "System.Security.Cryptography.RSAEncryptionPadding";

		// Token: 0x0400006C RID: 108
		private const string RSACngTypeName = "System.Security.Cryptography.RSACng";

		// Token: 0x0400006D RID: 109
		private const string DSACngTypeName = "System.Security.Cryptography.DSACng";

		// Token: 0x0400006E RID: 110
		private static readonly Type s_hashAlgorithmNameType = typeof(object).Assembly.GetType("System.Security.Cryptography.HashAlgorithmName", false);

		// Token: 0x0400006F RID: 111
		private static readonly Type s_rsaSignaturePaddingType = typeof(object).Assembly.GetType("System.Security.Cryptography.RSASignaturePadding", false);

		// Token: 0x04000070 RID: 112
		private static readonly Type s_rsaEncryptionPaddingType = typeof(object).Assembly.GetType("System.Security.Cryptography.RSAEncryptionPadding", false);

		// Token: 0x04000071 RID: 113
		private static readonly object s_pkcs1SignaturePadding = (CngLightup.s_rsaSignaturePaddingType == null) ? null : CngLightup.s_rsaSignaturePaddingType.GetProperty("Pkcs1", BindingFlags.Static | BindingFlags.Public).GetValue(null);

		// Token: 0x04000072 RID: 114
		private static readonly object s_pkcs1EncryptionPadding = (CngLightup.s_rsaEncryptionPaddingType == null) ? null : CngLightup.s_rsaEncryptionPaddingType.GetProperty("Pkcs1", BindingFlags.Static | BindingFlags.Public).GetValue(null);

		// Token: 0x04000073 RID: 115
		private static readonly object s_oaepSha1EncryptionPadding = (CngLightup.s_rsaEncryptionPaddingType == null) ? null : CngLightup.s_rsaEncryptionPaddingType.GetProperty("OaepSHA1", BindingFlags.Static | BindingFlags.Public).GetValue(null);

		// Token: 0x04000074 RID: 116
		private static readonly Lazy<bool> s_preferRsaCng = new Lazy<bool>(new Func<bool>(CngLightup.DetectRsaCngSupport));

		// Token: 0x04000075 RID: 117
		private static volatile Func<X509Certificate2, DSA> s_getDsaPublicKey;

		// Token: 0x04000076 RID: 118
		private static volatile Func<X509Certificate2, DSA> s_getDsaPrivateKey;

		// Token: 0x04000077 RID: 119
		private static volatile Func<X509Certificate2, RSA> s_getRsaPublicKey;

		// Token: 0x04000078 RID: 120
		private static volatile Func<X509Certificate2, RSA> s_getRsaPrivateKey;

		// Token: 0x04000079 RID: 121
		private static volatile Func<RSA, byte[], string, byte[]> s_rsaPkcs1SignMethod;

		// Token: 0x0400007A RID: 122
		private static volatile Func<RSA, byte[], byte[], string, bool> s_rsaPkcs1VerifyMethod;

		// Token: 0x0400007B RID: 123
		private static volatile Func<RSA, byte[], byte[]> s_rsaPkcs1EncryptMethod;

		// Token: 0x0400007C RID: 124
		private static volatile Func<RSA, byte[], byte[]> s_rsaPkcs1DecryptMethod;

		// Token: 0x0400007D RID: 125
		private static volatile Func<RSA, byte[], byte[]> s_rsaOaepSha1EncryptMethod;

		// Token: 0x0400007E RID: 126
		private static volatile Func<RSA, byte[], byte[]> s_rsaOaepSha1DecryptMethod;
	}
}
