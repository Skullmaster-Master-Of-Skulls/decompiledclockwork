using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x02000476 RID: 1142
	[Flags]
	public enum X509KeyUsageFlags
	{
		// Token: 0x0400262F RID: 9775
		None = 0,
		// Token: 0x04002630 RID: 9776
		EncipherOnly = 1,
		// Token: 0x04002631 RID: 9777
		CrlSign = 2,
		// Token: 0x04002632 RID: 9778
		KeyCertSign = 4,
		// Token: 0x04002633 RID: 9779
		KeyAgreement = 8,
		// Token: 0x04002634 RID: 9780
		DataEncipherment = 16,
		// Token: 0x04002635 RID: 9781
		KeyEncipherment = 32,
		// Token: 0x04002636 RID: 9782
		NonRepudiation = 64,
		// Token: 0x04002637 RID: 9783
		DigitalSignature = 128,
		// Token: 0x04002638 RID: 9784
		DecipherOnly = 32768
	}
}
