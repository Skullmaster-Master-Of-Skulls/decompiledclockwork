using System;
using log4net.Util;

namespace log4net
{
	// Token: 0x02000127 RID: 295
	public sealed class ThreadContext
	{
		// Token: 0x060008AE RID: 2222 RVA: 0x0001A3BB File Offset: 0x000185BB
		private ThreadContext()
		{
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060008AF RID: 2223 RVA: 0x0001A3C3 File Offset: 0x000185C3
		public static ThreadContextProperties Properties
		{
			get
			{
				return ThreadContext.s_properties;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x060008B0 RID: 2224 RVA: 0x0001A3CA File Offset: 0x000185CA
		public static ThreadContextStacks Stacks
		{
			get
			{
				return ThreadContext.s_stacks;
			}
		}

		// Token: 0x0400031B RID: 795
		private static readonly ThreadContextProperties s_properties = new ThreadContextProperties();

		// Token: 0x0400031C RID: 796
		private static readonly ThreadContextStacks s_stacks = new ThreadContextStacks(ThreadContext.s_properties);
	}
}
