using System;
using System.ComponentModel;

namespace System.Diagnostics
{
	// Token: 0x020004A4 RID: 1188
	[Flags]
	public enum SourceLevels
	{
		// Token: 0x040026AC RID: 9900
		Off = 0,
		// Token: 0x040026AD RID: 9901
		Critical = 1,
		// Token: 0x040026AE RID: 9902
		Error = 3,
		// Token: 0x040026AF RID: 9903
		Warning = 7,
		// Token: 0x040026B0 RID: 9904
		Information = 15,
		// Token: 0x040026B1 RID: 9905
		Verbose = 31,
		// Token: 0x040026B2 RID: 9906
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		ActivityTracing = 65280,
		// Token: 0x040026B3 RID: 9907
		All = -1
	}
}
