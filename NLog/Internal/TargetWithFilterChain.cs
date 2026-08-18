using System;
using System.Collections.Generic;
using System.Linq;
using NLog.Config;
using NLog.Filters;
using NLog.Targets;

namespace NLog.Internal
{
	// Token: 0x020000B4 RID: 180
	[NLogConfigurationItem]
	internal class TargetWithFilterChain
	{
		// Token: 0x0600056A RID: 1386 RVA: 0x0000C37B File Offset: 0x0000A57B
		public TargetWithFilterChain(Target target, IList<Filter> filterChain)
		{
			this.Target = target;
			this.FilterChain = filterChain;
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600056B RID: 1387 RVA: 0x0000C391 File Offset: 0x0000A591
		// (set) Token: 0x0600056C RID: 1388 RVA: 0x0000C399 File Offset: 0x0000A599
		public Target Target { get; private set; }

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600056D RID: 1389 RVA: 0x0000C3A2 File Offset: 0x0000A5A2
		// (set) Token: 0x0600056E RID: 1390 RVA: 0x0000C3AA File Offset: 0x0000A5AA
		public IList<Filter> FilterChain { get; private set; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600056F RID: 1391 RVA: 0x0000C3B3 File Offset: 0x0000A5B3
		// (set) Token: 0x06000570 RID: 1392 RVA: 0x0000C3BB File Offset: 0x0000A5BB
		public TargetWithFilterChain NextInChain { get; set; }

		// Token: 0x06000571 RID: 1393 RVA: 0x0000C3C4 File Offset: 0x0000A5C4
		public StackTraceUsage GetStackTraceUsage()
		{
			StackTraceUsage? stackTraceUsage = this._stackTraceUsage;
			if (stackTraceUsage == null)
			{
				return StackTraceUsage.None;
			}
			return stackTraceUsage.GetValueOrDefault();
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x0000C3F8 File Offset: 0x0000A5F8
		internal StackTraceUsage PrecalculateStackTraceUsage()
		{
			StackTraceUsage stackTraceUsage = StackTraceUsage.None;
			if (this.Target != null)
			{
				stackTraceUsage = this.Target.GetAllLayouts().OfType<IUsesStackTrace>().DefaultIfEmpty<IUsesStackTrace>().Max(delegate(IUsesStackTrace usage)
				{
					if (usage != null)
					{
						return usage.StackTraceUsage;
					}
					return StackTraceUsage.None;
				});
			}
			if (this.NextInChain != null && stackTraceUsage != StackTraceUsage.WithSource)
			{
				StackTraceUsage stackTraceUsage2 = this.NextInChain.PrecalculateStackTraceUsage();
				if (stackTraceUsage2 > stackTraceUsage)
				{
					stackTraceUsage = stackTraceUsage2;
				}
			}
			this._stackTraceUsage = new StackTraceUsage?(stackTraceUsage);
			return stackTraceUsage;
		}

		// Token: 0x04000121 RID: 289
		private StackTraceUsage? _stackTraceUsage;
	}
}
