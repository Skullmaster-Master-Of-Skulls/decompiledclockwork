using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x02000842 RID: 2114
	[ComVisible(true)]
	[Serializable]
	public enum FlowControl
	{
		// Token: 0x040027C1 RID: 10177
		Branch,
		// Token: 0x040027C2 RID: 10178
		Break,
		// Token: 0x040027C3 RID: 10179
		Call,
		// Token: 0x040027C4 RID: 10180
		Cond_Branch,
		// Token: 0x040027C5 RID: 10181
		Meta,
		// Token: 0x040027C6 RID: 10182
		Next,
		// Token: 0x040027C7 RID: 10183
		[Obsolete("This API has been deprecated. http://go.microsoft.com/fwlink/?linkid=14202")]
		Phi,
		// Token: 0x040027C8 RID: 10184
		Return,
		// Token: 0x040027C9 RID: 10185
		Throw
	}
}
