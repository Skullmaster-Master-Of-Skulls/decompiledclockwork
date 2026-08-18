using System;

namespace System.ServiceModel.Security
{
	// Token: 0x020002D7 RID: 727
	public enum MessageProtectionOrder
	{
		// Token: 0x04001C3A RID: 7226
		SignBeforeEncrypt,
		// Token: 0x04001C3B RID: 7227
		SignBeforeEncryptAndEncryptSignature,
		// Token: 0x04001C3C RID: 7228
		EncryptBeforeSign
	}
}
