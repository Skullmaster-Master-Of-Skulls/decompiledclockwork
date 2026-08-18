using System;
using System.Diagnostics;

namespace System.ComponentModel.Design
{
	// Token: 0x020001B0 RID: 432
	public interface IComponentDesignerDebugService
	{
		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06000FC9 RID: 4041
		// (set) Token: 0x06000FCA RID: 4042
		int IndentLevel { get; set; }

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06000FCB RID: 4043
		TraceListenerCollection Listeners { get; }

		// Token: 0x06000FCC RID: 4044
		void Assert(bool condition, string message);

		// Token: 0x06000FCD RID: 4045
		void Fail(string message);

		// Token: 0x06000FCE RID: 4046
		void Trace(string message, string category);
	}
}
