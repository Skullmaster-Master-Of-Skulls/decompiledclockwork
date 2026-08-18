using System;
using System.ComponentModel;

namespace System.Diagnostics
{
	// Token: 0x020004AF RID: 1199
	public enum TraceEventType
	{
		// Token: 0x040026D7 RID: 9943
		Critical = 1,
		// Token: 0x040026D8 RID: 9944
		Error,
		// Token: 0x040026D9 RID: 9945
		Warning = 4,
		// Token: 0x040026DA RID: 9946
		Information = 8,
		// Token: 0x040026DB RID: 9947
		Verbose = 16,
		// Token: 0x040026DC RID: 9948
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		Start = 256,
		// Token: 0x040026DD RID: 9949
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		Stop = 512,
		// Token: 0x040026DE RID: 9950
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		Suspend = 1024,
		// Token: 0x040026DF RID: 9951
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		Resume = 2048,
		// Token: 0x040026E0 RID: 9952
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		Transfer = 4096
	}
}
