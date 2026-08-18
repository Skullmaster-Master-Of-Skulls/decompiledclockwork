using System;

namespace System.ServiceModel.Security
{
	// Token: 0x020002AB RID: 683
	internal enum ReceiveSecurityHeaderElementCategory
	{
		// Token: 0x04001B14 RID: 6932
		Signature,
		// Token: 0x04001B15 RID: 6933
		EncryptedData,
		// Token: 0x04001B16 RID: 6934
		EncryptedKey,
		// Token: 0x04001B17 RID: 6935
		SignatureConfirmation,
		// Token: 0x04001B18 RID: 6936
		ReferenceList,
		// Token: 0x04001B19 RID: 6937
		SecurityTokenReference,
		// Token: 0x04001B1A RID: 6938
		Timestamp,
		// Token: 0x04001B1B RID: 6939
		Token
	}
}
