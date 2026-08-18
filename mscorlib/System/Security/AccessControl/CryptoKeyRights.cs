using System;

namespace System.Security.AccessControl
{
	// Token: 0x0200090D RID: 2317
	[Flags]
	public enum CryptoKeyRights
	{
		// Token: 0x04002B74 RID: 11124
		ReadData = 1,
		// Token: 0x04002B75 RID: 11125
		WriteData = 2,
		// Token: 0x04002B76 RID: 11126
		ReadExtendedAttributes = 8,
		// Token: 0x04002B77 RID: 11127
		WriteExtendedAttributes = 16,
		// Token: 0x04002B78 RID: 11128
		ReadAttributes = 128,
		// Token: 0x04002B79 RID: 11129
		WriteAttributes = 256,
		// Token: 0x04002B7A RID: 11130
		Delete = 65536,
		// Token: 0x04002B7B RID: 11131
		ReadPermissions = 131072,
		// Token: 0x04002B7C RID: 11132
		ChangePermissions = 262144,
		// Token: 0x04002B7D RID: 11133
		TakeOwnership = 524288,
		// Token: 0x04002B7E RID: 11134
		Synchronize = 1048576,
		// Token: 0x04002B7F RID: 11135
		FullControl = 2032027,
		// Token: 0x04002B80 RID: 11136
		GenericAll = 268435456,
		// Token: 0x04002B81 RID: 11137
		GenericExecute = 536870912,
		// Token: 0x04002B82 RID: 11138
		GenericWrite = 1073741824,
		// Token: 0x04002B83 RID: 11139
		GenericRead = -2147483648
	}
}
