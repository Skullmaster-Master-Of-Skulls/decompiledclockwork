using System;
using log4net.Util;

namespace log4net
{
	// Token: 0x02000123 RID: 291
	public sealed class LogicalThreadContext
	{
		// Token: 0x06000873 RID: 2163 RVA: 0x0001A054 File Offset: 0x00018254
		private LogicalThreadContext()
		{
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000874 RID: 2164 RVA: 0x0001A05C File Offset: 0x0001825C
		public static LogicalThreadContextProperties Properties
		{
			get
			{
				return LogicalThreadContext.s_properties;
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000875 RID: 2165 RVA: 0x0001A063 File Offset: 0x00018263
		public static LogicalThreadContextStacks Stacks
		{
			get
			{
				return LogicalThreadContext.s_stacks;
			}
		}

		// Token: 0x04000318 RID: 792
		private static readonly LogicalThreadContextProperties s_properties = new LogicalThreadContextProperties();

		// Token: 0x04000319 RID: 793
		private static readonly LogicalThreadContextStacks s_stacks = new LogicalThreadContextStacks(LogicalThreadContext.s_properties);
	}
}
