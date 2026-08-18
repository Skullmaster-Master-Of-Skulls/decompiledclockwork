using System;
using System.ComponentModel;
using System.Diagnostics;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000141 RID: 321
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DebuggerStepThrough]
	[__DynamicallyInvokable]
	public sealed class Closure
	{
		// Token: 0x06000A64 RID: 2660 RVA: 0x00025BD5 File Offset: 0x00023DD5
		[__DynamicallyInvokable]
		public Closure(object[] constants, object[] locals)
		{
			this.Constants = constants;
			this.Locals = locals;
		}

		// Token: 0x04000771 RID: 1905
		[__DynamicallyInvokable]
		public readonly object[] Constants;

		// Token: 0x04000772 RID: 1906
		[__DynamicallyInvokable]
		public readonly object[] Locals;
	}
}
