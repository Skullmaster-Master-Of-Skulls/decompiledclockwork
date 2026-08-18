using System;
using System.Diagnostics;

namespace System.Collections.Immutable
{
	// Token: 0x0200003A RID: 58
	[DebuggerDisplay("{Value,nq}")]
	internal struct RefAsValueType<T>
	{
		// Token: 0x06000371 RID: 881 RVA: 0x000094A4 File Offset: 0x000076A4
		internal RefAsValueType(T value)
		{
			this.Value = value;
		}

		// Token: 0x04000047 RID: 71
		internal T Value;
	}
}
