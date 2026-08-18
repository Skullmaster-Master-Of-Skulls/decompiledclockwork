using System;

namespace System.Runtime.Diagnostics
{
	// Token: 0x02000045 RID: 69
	[AttributeUsage(AttributeTargets.Field, Inherited = false)]
	internal sealed class PerformanceCounterNameAttribute : Attribute
	{
		// Token: 0x060002C2 RID: 706 RVA: 0x0000BD0A File Offset: 0x00009F0A
		public PerformanceCounterNameAttribute(string name)
		{
			this.Name = name;
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x0000BD19 File Offset: 0x00009F19
		// (set) Token: 0x060002C4 RID: 708 RVA: 0x0000BD21 File Offset: 0x00009F21
		public string Name { get; set; }
	}
}
