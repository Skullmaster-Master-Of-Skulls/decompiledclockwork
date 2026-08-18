using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Security;

namespace System.IdentityModel
{
	// Token: 0x020000C2 RID: 194
	internal static class X509Util
	{
		// Token: 0x060005E3 RID: 1507 RVA: 0x000155F8 File Offset: 0x000137F8
		internal static RSA EnsureAndGetPrivateRSAKey(X509Certificate2 certificate)
		{
			if (!certificate.HasPrivateKey)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("ID1001", new object[]
				{
					certificate.Thumbprint
				})));
			}
			RSA rsa;
			try
			{
				if (LocalAppContextSwitches.DisableCngCertificates)
				{
					rsa = (certificate.PrivateKey as RSA);
				}
				else
				{
					rsa = CngLightup.GetRSAPrivateKey(certificate);
				}
			}
			catch (CryptographicException innerException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("ID1039", new object[]
				{
					certificate.Thumbprint
				}), innerException));
			}
			if (rsa == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("ID1002", new object[]
				{
					certificate.Thumbprint
				})));
			}
			return rsa;
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x000156C0 File Offset: 0x000138C0
		internal static X509Certificate2 ResolveCertificate(StoreName storeName, StoreLocation storeLocation, X509FindType findType, object findValue)
		{
			X509Certificate2 result = null;
			if (!X509Util.TryResolveCertificate(storeName, storeLocation, findType, findValue, out result))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID1025", new object[]
				{
					storeName,
					storeLocation,
					findType,
					findValue
				})));
			}
			return result;
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x0001571C File Offset: 0x0001391C
		internal static bool TryResolveCertificate(StoreName storeName, StoreLocation storeLocation, X509FindType findType, object findValue, out X509Certificate2 certificate)
		{
			X509Store x509Store = new X509Store(storeName, storeLocation);
			x509Store.Open(OpenFlags.ReadOnly);
			certificate = null;
			X509Certificate2Collection x509Certificate2Collection = null;
			X509Certificate2Collection x509Certificate2Collection2 = null;
			try
			{
				x509Certificate2Collection = x509Store.Certificates;
				x509Certificate2Collection2 = x509Certificate2Collection.Find(findType, findValue, false);
				if (x509Certificate2Collection2.Count == 1)
				{
					certificate = new X509Certificate2(x509Certificate2Collection2[0]);
					return true;
				}
			}
			finally
			{
				CryptoHelper.ResetAllCertificates(x509Certificate2Collection2);
				CryptoHelper.ResetAllCertificates(x509Certificate2Collection);
				x509Store.Close();
			}
			return false;
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x00015798 File Offset: 0x00013998
		internal static string GetCertificateId(X509Certificate2 certificate)
		{
			if (certificate == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("certificate");
			}
			string text = certificate.SubjectName.Name;
			if (string.IsNullOrEmpty(text))
			{
				text = certificate.Thumbprint;
			}
			return text;
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x000157D4 File Offset: 0x000139D4
		internal static string GetCertificateIssuerName(X509Certificate2 certificate, IssuerNameRegistry issuerNameRegistry)
		{
			if (certificate == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("certificate");
			}
			if (issuerNameRegistry == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("issuerNameRegistry");
			}
			X509Chain x509Chain = new X509Chain();
			x509Chain.ChainPolicy.RevocationMode = X509RevocationMode.NoCheck;
			x509Chain.Build(certificate);
			X509ChainElementCollection chainElements = x509Chain.ChainElements;
			string result = null;
			if (chainElements.Count > 1)
			{
				using (X509SecurityToken x509SecurityToken = new X509SecurityToken(chainElements[1].Certificate))
				{
					result = issuerNameRegistry.GetIssuerName(x509SecurityToken);
					goto IL_97;
				}
			}
			using (X509SecurityToken x509SecurityToken2 = new X509SecurityToken(certificate))
			{
				result = issuerNameRegistry.GetIssuerName(x509SecurityToken2);
			}
			IL_97:
			for (int i = 1; i < chainElements.Count; i++)
			{
				chainElements[i].Certificate.Reset();
			}
			return result;
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x000158BC File Offset: 0x00013ABC
		internal static X509CertificateValidator CreateCertificateValidator(X509CertificateValidationMode certificateValidationMode, X509RevocationMode revocationMode, StoreLocation trustedStoreLocation)
		{
			return new X509CertificateValidatorEx(certificateValidationMode, revocationMode, trustedStoreLocation);
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x000158C8 File Offset: 0x00013AC8
		public static IEnumerable<Claim> GetClaimsFromCertificate(X509Certificate2 certificate, string issuer)
		{
			if (certificate == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("certificate");
			}
			ICollection<Claim> collection = new Collection<Claim>();
			string value = Convert.ToBase64String(certificate.GetCertHash());
			collection.Add(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/thumbprint", value, "http://www.w3.org/2001/XMLSchema#base64Binary", issuer));
			string value2 = certificate.SubjectName.Name;
			if (!string.IsNullOrEmpty(value2))
			{
				collection.Add(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/x500distinguishedname", value2, "http://www.w3.org/2001/XMLSchema#string", issuer));
			}
			value2 = certificate.GetNameInfo(X509NameType.DnsName, false);
			if (!string.IsNullOrEmpty(value2))
			{
				collection.Add(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/dns", value2, "http://www.w3.org/2001/XMLSchema#string", issuer));
			}
			value2 = certificate.GetNameInfo(X509NameType.SimpleName, false);
			if (!string.IsNullOrEmpty(value2))
			{
				collection.Add(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", value2, "http://www.w3.org/2001/XMLSchema#string", issuer));
			}
			value2 = certificate.GetNameInfo(X509NameType.EmailName, false);
			if (!string.IsNullOrEmpty(value2))
			{
				collection.Add(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress", value2, "http://www.w3.org/2001/XMLSchema#string", issuer));
			}
			value2 = certificate.GetNameInfo(X509NameType.UpnName, false);
			if (!string.IsNullOrEmpty(value2))
			{
				collection.Add(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/upn", value2, "http://www.w3.org/2001/XMLSchema#string", issuer));
			}
			value2 = certificate.GetNameInfo(X509NameType.UrlName, false);
			if (!string.IsNullOrEmpty(value2))
			{
				collection.Add(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/uri", value2, "http://www.w3.org/2001/XMLSchema#string", issuer));
			}
			RSA rsa;
			if (LocalAppContextSwitches.DisableCngCertificates)
			{
				rsa = (certificate.PublicKey.Key as RSA);
			}
			else
			{
				rsa = CngLightup.GetRSAPublicKey(certificate);
			}
			if (rsa != null)
			{
				collection.Add(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/rsa", rsa.ToXmlString(false), "http://www.w3.org/2000/09/xmldsig#RSAKeyValue", issuer));
			}
			DSA dsa;
			if (LocalAppContextSwitches.DisableCngCertificates)
			{
				dsa = (certificate.PublicKey.Key as DSA);
			}
			else
			{
				dsa = CngLightup.GetDSAPublicKey(certificate);
			}
			if (dsa != null)
			{
				collection.Add(new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/dsa", dsa.ToXmlString(false), "http://www.w3.org/2000/09/xmldsig#DSAKeyValue", issuer));
			}
			value2 = certificate.SerialNumber;
			if (!string.IsNullOrEmpty(value2))
			{
				collection.Add(new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/serialnumber", value2, "http://www.w3.org/2001/XMLSchema#string", issuer));
			}
			return collection;
		}
	}
}
