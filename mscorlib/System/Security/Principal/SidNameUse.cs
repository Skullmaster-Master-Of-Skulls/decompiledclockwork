using System;

namespace System.Security.Principal
{
	// Token: 0x02000944 RID: 2372
	internal enum SidNameUse
	{
		// Token: 0x04002C74 RID: 11380
		User = 1,
		// Token: 0x04002C75 RID: 11381
		Group,
		// Token: 0x04002C76 RID: 11382
		Domain,
		// Token: 0x04002C77 RID: 11383
		Alias,
		// Token: 0x04002C78 RID: 11384
		WellKnownGroup,
		// Token: 0x04002C79 RID: 11385
		DeletedAccount,
		// Token: 0x04002C7A RID: 11386
		Invalid,
		// Token: 0x04002C7B RID: 11387
		Unknown,
		// Token: 0x04002C7C RID: 11388
		Computer
	}
}
