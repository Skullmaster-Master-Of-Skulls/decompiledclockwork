using System;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020005E7 RID: 1511
	internal static class AuthenticationModeHelper
	{
		// Token: 0x06003A67 RID: 14951 RVA: 0x000E0C28 File Offset: 0x000DEE28
		public static bool IsDefined(AuthenticationMode value)
		{
			return value == AuthenticationMode.AnonymousForCertificate || value == AuthenticationMode.AnonymousForSslNegotiated || value == AuthenticationMode.CertificateOverTransport || value == AuthenticationMode.IssuedToken || value == AuthenticationMode.IssuedTokenForCertificate || value == AuthenticationMode.IssuedTokenForSslNegotiated || value == AuthenticationMode.IssuedTokenOverTransport || value == AuthenticationMode.Kerberos || value == AuthenticationMode.KerberosOverTransport || value == AuthenticationMode.MutualCertificate || value == AuthenticationMode.MutualCertificateDuplex || value == AuthenticationMode.MutualSslNegotiated || value == AuthenticationMode.SecureConversation || value == AuthenticationMode.SspiNegotiated || value == AuthenticationMode.UserNameForCertificate || value == AuthenticationMode.UserNameForSslNegotiated || value == AuthenticationMode.UserNameOverTransport || value == AuthenticationMode.SspiNegotiatedOverTransport;
		}
	}
}
