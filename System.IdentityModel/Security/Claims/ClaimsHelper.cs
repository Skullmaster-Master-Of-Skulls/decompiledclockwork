using System;
using System.IdentityModel;
using System.IdentityModel.Tokens;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;

namespace System.Security.Claims
{
	// Token: 0x0200001F RID: 31
	internal static class ClaimsHelper
	{
		// Token: 0x060000D9 RID: 217 RVA: 0x0000494C File Offset: 0x00002B4C
		public static WindowsIdentity CertificateLogon(X509Certificate2 x509Certificate)
		{
			if (Environment.OSVersion.Version.Major >= 6)
			{
				return X509SecurityTokenHandler.KerberosCertificateLogon(x509Certificate);
			}
			string nameInfo = x509Certificate.GetNameInfo(X509NameType.UpnName, false);
			if (string.IsNullOrEmpty(nameInfo))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenValidationException(SR.GetString("ID4067", new object[]
				{
					X509Util.GetCertificateId(x509Certificate)
				})));
			}
			return new WindowsIdentity(nameInfo);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x000049B4 File Offset: 0x00002BB4
		public static string FindUpn(ClaimsIdentity claimsIdentity)
		{
			string text = null;
			foreach (Claim claim in claimsIdentity.Claims)
			{
				if (StringComparer.Ordinal.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/upn", claim.Type))
				{
					if (text != null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("ID1053")));
					}
					text = claim.Value;
				}
			}
			if (string.IsNullOrEmpty(text))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("ID1054")));
			}
			return text;
		}
	}
}
