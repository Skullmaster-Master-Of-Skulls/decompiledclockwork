using System;

namespace System.Net.Security
{
	// Token: 0x02000362 RID: 866
	internal enum TlsAlertMessage
	{
		// Token: 0x04001D61 RID: 7521
		CloseNotify,
		// Token: 0x04001D62 RID: 7522
		UnexpectedMessage = 10,
		// Token: 0x04001D63 RID: 7523
		BadRecordMac = 20,
		// Token: 0x04001D64 RID: 7524
		DecryptionFailed,
		// Token: 0x04001D65 RID: 7525
		RecordOverflow,
		// Token: 0x04001D66 RID: 7526
		DecompressionFail = 30,
		// Token: 0x04001D67 RID: 7527
		HandshakeFailure = 40,
		// Token: 0x04001D68 RID: 7528
		BadCertificate = 42,
		// Token: 0x04001D69 RID: 7529
		UnsupportedCert,
		// Token: 0x04001D6A RID: 7530
		CertificateRevoked,
		// Token: 0x04001D6B RID: 7531
		CertificateExpired,
		// Token: 0x04001D6C RID: 7532
		CertificateUnknown,
		// Token: 0x04001D6D RID: 7533
		IllegalParameter,
		// Token: 0x04001D6E RID: 7534
		UnknownCA,
		// Token: 0x04001D6F RID: 7535
		AccessDenied,
		// Token: 0x04001D70 RID: 7536
		DecodeError,
		// Token: 0x04001D71 RID: 7537
		DecryptError,
		// Token: 0x04001D72 RID: 7538
		ExportRestriction = 60,
		// Token: 0x04001D73 RID: 7539
		ProtocolVersion = 70,
		// Token: 0x04001D74 RID: 7540
		InsuffientSecurity,
		// Token: 0x04001D75 RID: 7541
		InternalError = 80,
		// Token: 0x04001D76 RID: 7542
		UserCanceled = 90,
		// Token: 0x04001D77 RID: 7543
		NoRenegotiation = 100,
		// Token: 0x04001D78 RID: 7544
		UnsupportedExt = 110
	}
}
