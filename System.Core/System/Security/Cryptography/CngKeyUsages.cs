using System;

namespace System.Security.Cryptography
{
	// Token: 0x0200010A RID: 266
	[Flags]
	public enum CngKeyUsages
	{
		// Token: 0x04000696 RID: 1686
		None = 0,
		// Token: 0x04000697 RID: 1687
		Decryption = 1,
		// Token: 0x04000698 RID: 1688
		Signing = 2,
		// Token: 0x04000699 RID: 1689
		KeyAgreement = 4,
		// Token: 0x0400069A RID: 1690
		AllUsages = 16777215
	}
}
