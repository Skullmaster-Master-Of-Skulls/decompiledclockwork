using System;
using NLog.Config;
using NLog.Layouts;

namespace NLog.Filters
{
	// Token: 0x02000060 RID: 96
	public abstract class LayoutBasedFilter : Filter
	{
		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000226 RID: 550 RVA: 0x000086F1 File Offset: 0x000068F1
		// (set) Token: 0x06000227 RID: 551 RVA: 0x000086F9 File Offset: 0x000068F9
		[RequiredParameter]
		public Layout Layout { get; set; }
	}
}
