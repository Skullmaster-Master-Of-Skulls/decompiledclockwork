using System;

namespace System.Security.AccessControl
{
	// Token: 0x02000932 RID: 2354
	[Flags]
	public enum RegistryRights
	{
		// Token: 0x04002C27 RID: 11303
		QueryValues = 1,
		// Token: 0x04002C28 RID: 11304
		SetValue = 2,
		// Token: 0x04002C29 RID: 11305
		CreateSubKey = 4,
		// Token: 0x04002C2A RID: 11306
		EnumerateSubKeys = 8,
		// Token: 0x04002C2B RID: 11307
		Notify = 16,
		// Token: 0x04002C2C RID: 11308
		CreateLink = 32,
		// Token: 0x04002C2D RID: 11309
		ExecuteKey = 131097,
		// Token: 0x04002C2E RID: 11310
		ReadKey = 131097,
		// Token: 0x04002C2F RID: 11311
		WriteKey = 131078,
		// Token: 0x04002C30 RID: 11312
		Delete = 65536,
		// Token: 0x04002C31 RID: 11313
		ReadPermissions = 131072,
		// Token: 0x04002C32 RID: 11314
		ChangePermissions = 262144,
		// Token: 0x04002C33 RID: 11315
		TakeOwnership = 524288,
		// Token: 0x04002C34 RID: 11316
		FullControl = 983103
	}
}
