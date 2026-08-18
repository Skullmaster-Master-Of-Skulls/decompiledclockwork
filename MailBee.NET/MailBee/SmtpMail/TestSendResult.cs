using System;

namespace MailBee.SmtpMail
{
	// Token: 0x02000142 RID: 322
	public enum TestSendResult
	{
		// Token: 0x0400082D RID: 2093
		OK,
		// Token: 0x0400082E RID: 2094
		NoSender,
		// Token: 0x0400082F RID: 2095
		NoRecipients,
		// Token: 0x04000830 RID: 2096
		BadSender,
		// Token: 0x04000831 RID: 2097
		BadRecipient,
		// Token: 0x04000832 RID: 2098
		NoAcceptedRecipients,
		// Token: 0x04000833 RID: 2099
		NoCredentials,
		// Token: 0x04000834 RID: 2100
		NoSupportedAuth,
		// Token: 0x04000835 RID: 2101
		BadAuthMethod,
		// Token: 0x04000836 RID: 2102
		BadCredentials,
		// Token: 0x04000837 RID: 2103
		NegativeSmtpResponse,
		// Token: 0x04000838 RID: 2104
		NegativePop3Response,
		// Token: 0x04000839 RID: 2105
		SmtpResolveHostError,
		// Token: 0x0400083A RID: 2106
		SmtpConnectionError,
		// Token: 0x0400083B RID: 2107
		Pop3ResolveHostError,
		// Token: 0x0400083C RID: 2108
		Pop3ConnectionError,
		// Token: 0x0400083D RID: 2109
		NoDomainInRecipientEmail,
		// Token: 0x0400083E RID: 2110
		NoMXRecord,
		// Token: 0x0400083F RID: 2111
		DnsProtocolError,
		// Token: 0x04000840 RID: 2112
		DnsConnectionError,
		// Token: 0x04000841 RID: 2113
		UnknownError
	}
}
