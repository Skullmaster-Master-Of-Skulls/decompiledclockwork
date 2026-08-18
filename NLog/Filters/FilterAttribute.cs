using System;
using NLog.Config;

namespace NLog.Filters
{
	// Token: 0x0200005E RID: 94
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class FilterAttribute : NameBaseAttribute
	{
		// Token: 0x06000224 RID: 548 RVA: 0x000086E0 File Offset: 0x000068E0
		public FilterAttribute(string name) : base(name)
		{
		}
	}
}
