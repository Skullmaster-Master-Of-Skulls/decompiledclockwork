using System;
using System.Reflection;
using System.Threading;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x02000069 RID: 105
	internal static class X509CertificateExtensions
	{
		// Token: 0x06000403 RID: 1027 RVA: 0x00014366 File Offset: 0x00012566
		public static RSA GetRSAPublicKey(this X509Certificate2 certificate)
		{
			return CngLightup.GetRSAPublicKey(certificate);
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0001436E File Offset: 0x0001256E
		public static RSA GetRSAPrivateKey(this X509Certificate2 certificate)
		{
			return CngLightup.GetRSAPrivateKey(certificate);
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x00014376 File Offset: 0x00012576
		public static DSA GetDSAPublicKey(this X509Certificate2 certificate)
		{
			return CngLightup.GetDSAPublicKey(certificate);
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0001437E File Offset: 0x0001257E
		public static DSA GetDSAPrivateKey(this X509Certificate2 certificate)
		{
			return CngLightup.GetDSAPrivateKey(certificate);
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x00014386 File Offset: 0x00012586
		public static AsymmetricAlgorithm GetECDsaPublicKey(this X509Certificate2 certificate)
		{
			return X509CertificateExtensions.s_getEcdsaPublicKey.Value(certificate);
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x00014398 File Offset: 0x00012598
		public static AsymmetricAlgorithm GetECDsaPrivateKey(this X509Certificate2 certificate)
		{
			return X509CertificateExtensions.s_getEcdsaPrivateKey.Value(certificate);
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x000143AC File Offset: 0x000125AC
		public static AsymmetricAlgorithm GetAnyPublicKey(this X509Certificate2 c)
		{
			AsymmetricAlgorithm asymmetricAlgorithm = c.GetRSAPublicKey();
			if (asymmetricAlgorithm != null)
			{
				return asymmetricAlgorithm;
			}
			asymmetricAlgorithm = c.GetDSAPublicKey();
			if (asymmetricAlgorithm != null)
			{
				return asymmetricAlgorithm;
			}
			asymmetricAlgorithm = c.GetECDsaPublicKey();
			if (asymmetricAlgorithm != null)
			{
				return asymmetricAlgorithm;
			}
			throw new NotSupportedException(SecurityResources.GetResourceString("NotSupported_KeyAlgorithm"));
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x000143EC File Offset: 0x000125EC
		private static Lazy<Func<X509Certificate2, T>> CreateLazyInvoker<T>(string algorithmName, bool isPublic) where T : AsymmetricAlgorithm
		{
			Func<Func<X509Certificate2, T>> valueFactory = delegate()
			{
				string typeName = "System.Security.Cryptography.X509Certificates." + algorithmName + "CertificateExtensions, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
				Type type = Type.GetType(typeName, false, false);
				if (type == null)
				{
					return (X509Certificate2 x509) => default(T);
				}
				string name = "Get" + algorithmName + (isPublic ? "Public" : "Private") + "Key";
				MethodInfo method = type.GetMethod(name, BindingFlags.Static | BindingFlags.Public, null, new Type[]
				{
					typeof(X509Certificate2)
				}, null);
				return (Func<X509Certificate2, T>)method.CreateDelegate(typeof(Func<X509Certificate2, T>));
			};
			return new Lazy<Func<X509Certificate2, T>>(valueFactory, LazyThreadSafetyMode.PublicationOnly);
		}

		// Token: 0x040004B3 RID: 1203
		private static Lazy<Func<X509Certificate2, AsymmetricAlgorithm>> s_getEcdsaPublicKey = X509CertificateExtensions.CreateLazyInvoker<AsymmetricAlgorithm>("ECDsa", true);

		// Token: 0x040004B4 RID: 1204
		private static Lazy<Func<X509Certificate2, AsymmetricAlgorithm>> s_getEcdsaPrivateKey = X509CertificateExtensions.CreateLazyInvoker<AsymmetricAlgorithm>("ECDsa", false);
	}
}
