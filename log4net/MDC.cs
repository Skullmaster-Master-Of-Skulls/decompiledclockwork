using System;

namespace log4net
{
	// Token: 0x02000125 RID: 293
	public sealed class MDC
	{
		// Token: 0x0600089F RID: 2207 RVA: 0x0001A28C File Offset: 0x0001848C
		private MDC()
		{
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x0001A294 File Offset: 0x00018494
		public static string Get(string key)
		{
			object obj = ThreadContext.Properties[key];
			if (obj == null)
			{
				return null;
			}
			return obj.ToString();
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x0001A2B8 File Offset: 0x000184B8
		public static void Set(string key, string value)
		{
			ThreadContext.Properties[key] = value;
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x0001A2C6 File Offset: 0x000184C6
		public static void Remove(string key)
		{
			ThreadContext.Properties.Remove(key);
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x0001A2D3 File Offset: 0x000184D3
		public static void Clear()
		{
			ThreadContext.Properties.Clear();
		}
	}
}
