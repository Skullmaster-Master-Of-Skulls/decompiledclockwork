using System;

namespace System.ServiceModel.Security
{
	// Token: 0x020002A3 RID: 675
	internal enum MessagePartProtectionMode
	{
		// Token: 0x04001ABA RID: 6842
		None,
		// Token: 0x04001ABB RID: 6843
		Sign,
		// Token: 0x04001ABC RID: 6844
		Encrypt,
		// Token: 0x04001ABD RID: 6845
		SignThenEncrypt,
		// Token: 0x04001ABE RID: 6846
		EncryptThenSign
	}
}
