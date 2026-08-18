using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	// Token: 0x0200066B RID: 1643
	[ComVisible(true)]
	[Serializable]
	public enum MemberAttributes
	{
		// Token: 0x04002C59 RID: 11353
		Abstract = 1,
		// Token: 0x04002C5A RID: 11354
		Final,
		// Token: 0x04002C5B RID: 11355
		Static,
		// Token: 0x04002C5C RID: 11356
		Override,
		// Token: 0x04002C5D RID: 11357
		Const,
		// Token: 0x04002C5E RID: 11358
		New = 16,
		// Token: 0x04002C5F RID: 11359
		Overloaded = 256,
		// Token: 0x04002C60 RID: 11360
		Assembly = 4096,
		// Token: 0x04002C61 RID: 11361
		FamilyAndAssembly = 8192,
		// Token: 0x04002C62 RID: 11362
		Family = 12288,
		// Token: 0x04002C63 RID: 11363
		FamilyOrAssembly = 16384,
		// Token: 0x04002C64 RID: 11364
		Private = 20480,
		// Token: 0x04002C65 RID: 11365
		Public = 24576,
		// Token: 0x04002C66 RID: 11366
		AccessMask = 61440,
		// Token: 0x04002C67 RID: 11367
		ScopeMask = 15,
		// Token: 0x04002C68 RID: 11368
		VTableMask = 240
	}
}
