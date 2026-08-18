using System;

namespace System.Security.Permissions
{
	// Token: 0x02000486 RID: 1158
	[Flags]
	[Serializable]
	public enum StorePermissionFlags
	{
		// Token: 0x04002663 RID: 9827
		NoFlags = 0,
		// Token: 0x04002664 RID: 9828
		CreateStore = 1,
		// Token: 0x04002665 RID: 9829
		DeleteStore = 2,
		// Token: 0x04002666 RID: 9830
		EnumerateStores = 4,
		// Token: 0x04002667 RID: 9831
		OpenStore = 16,
		// Token: 0x04002668 RID: 9832
		AddToStore = 32,
		// Token: 0x04002669 RID: 9833
		RemoveFromStore = 64,
		// Token: 0x0400266A RID: 9834
		EnumerateCertificates = 128,
		// Token: 0x0400266B RID: 9835
		AllFlags = 247
	}
}
