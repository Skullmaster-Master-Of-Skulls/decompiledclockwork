using System;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020005E6 RID: 1510
	public enum AuthenticationMode
	{
		// Token: 0x04002A5C RID: 10844
		AnonymousForCertificate,
		// Token: 0x04002A5D RID: 10845
		AnonymousForSslNegotiated,
		// Token: 0x04002A5E RID: 10846
		CertificateOverTransport,
		// Token: 0x04002A5F RID: 10847
		IssuedToken,
		// Token: 0x04002A60 RID: 10848
		IssuedTokenForCertificate,
		// Token: 0x04002A61 RID: 10849
		IssuedTokenForSslNegotiated,
		// Token: 0x04002A62 RID: 10850
		IssuedTokenOverTransport,
		// Token: 0x04002A63 RID: 10851
		Kerberos,
		// Token: 0x04002A64 RID: 10852
		KerberosOverTransport,
		// Token: 0x04002A65 RID: 10853
		MutualCertificate,
		// Token: 0x04002A66 RID: 10854
		MutualCertificateDuplex,
		// Token: 0x04002A67 RID: 10855
		MutualSslNegotiated,
		// Token: 0x04002A68 RID: 10856
		SecureConversation,
		// Token: 0x04002A69 RID: 10857
		SspiNegotiated,
		// Token: 0x04002A6A RID: 10858
		UserNameForCertificate,
		// Token: 0x04002A6B RID: 10859
		UserNameForSslNegotiated,
		// Token: 0x04002A6C RID: 10860
		UserNameOverTransport,
		// Token: 0x04002A6D RID: 10861
		SspiNegotiatedOverTransport
	}
}
