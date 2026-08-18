using System;

namespace Spire.CompoundFile.Doc.Native
{
	// Token: 0x02000480 RID: 1152
	[Flags]
	internal enum STGC
	{
		// Token: 0x04002E96 RID: 11926
		STGC_DEFAULT = 0,
		// Token: 0x04002E97 RID: 11927
		STGC_OVERWRITE = 1,
		// Token: 0x04002E98 RID: 11928
		STGC_ONLYIFCURRENT = 2,
		// Token: 0x04002E99 RID: 11929
		STGC_DANGEROUSLYCOMMITMERELYTODISKCACHE = 4,
		// Token: 0x04002E9A RID: 11930
		STGC_CONSOLIDATE = 8
	}
}
