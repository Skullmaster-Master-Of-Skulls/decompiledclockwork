using System;
using System.ComponentModel.DataAnnotations;

namespace System.Data
{
	// Token: 0x0200001A RID: 26
	[Flags]
	[BindableType(IsBindable = false)]
	public enum EntityState
	{
		// Token: 0x040000A5 RID: 165
		Detached = 1,
		// Token: 0x040000A6 RID: 166
		Unchanged = 2,
		// Token: 0x040000A7 RID: 167
		Added = 4,
		// Token: 0x040000A8 RID: 168
		Deleted = 8,
		// Token: 0x040000A9 RID: 169
		Modified = 16
	}
}
