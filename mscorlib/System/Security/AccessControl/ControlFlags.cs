using System;

namespace System.Security.AccessControl
{
	// Token: 0x0200093A RID: 2362
	[Flags]
	public enum ControlFlags
	{
		// Token: 0x04002C3F RID: 11327
		None = 0,
		// Token: 0x04002C40 RID: 11328
		OwnerDefaulted = 1,
		// Token: 0x04002C41 RID: 11329
		GroupDefaulted = 2,
		// Token: 0x04002C42 RID: 11330
		DiscretionaryAclPresent = 4,
		// Token: 0x04002C43 RID: 11331
		DiscretionaryAclDefaulted = 8,
		// Token: 0x04002C44 RID: 11332
		SystemAclPresent = 16,
		// Token: 0x04002C45 RID: 11333
		SystemAclDefaulted = 32,
		// Token: 0x04002C46 RID: 11334
		DiscretionaryAclUntrusted = 64,
		// Token: 0x04002C47 RID: 11335
		ServerSecurity = 128,
		// Token: 0x04002C48 RID: 11336
		DiscretionaryAclAutoInheritRequired = 256,
		// Token: 0x04002C49 RID: 11337
		SystemAclAutoInheritRequired = 512,
		// Token: 0x04002C4A RID: 11338
		DiscretionaryAclAutoInherited = 1024,
		// Token: 0x04002C4B RID: 11339
		SystemAclAutoInherited = 2048,
		// Token: 0x04002C4C RID: 11340
		DiscretionaryAclProtected = 4096,
		// Token: 0x04002C4D RID: 11341
		SystemAclProtected = 8192,
		// Token: 0x04002C4E RID: 11342
		RMControlValid = 16384,
		// Token: 0x04002C4F RID: 11343
		SelfRelative = 32768
	}
}
