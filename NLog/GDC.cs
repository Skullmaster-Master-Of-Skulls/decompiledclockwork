using System;

namespace NLog
{
	// Token: 0x02000068 RID: 104
	[Obsolete("Use GlobalDiagnosticsContext instead")]
	public static class GDC
	{
		// Token: 0x06000264 RID: 612 RVA: 0x00008DDA File Offset: 0x00006FDA
		public static void Set(string item, string value)
		{
			GlobalDiagnosticsContext.Set(item, value);
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00008DE3 File Offset: 0x00006FE3
		public static string Get(string item)
		{
			return GlobalDiagnosticsContext.Get(item);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00008DEB File Offset: 0x00006FEB
		public static string Get(string item, IFormatProvider formatProvider)
		{
			return GlobalDiagnosticsContext.Get(item, formatProvider);
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00008DF4 File Offset: 0x00006FF4
		public static object GetObject(string item)
		{
			return GlobalDiagnosticsContext.GetObject(item);
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00008DFC File Offset: 0x00006FFC
		public static bool Contains(string item)
		{
			return GlobalDiagnosticsContext.Contains(item);
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00008E04 File Offset: 0x00007004
		public static void Remove(string item)
		{
			GlobalDiagnosticsContext.Remove(item);
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00008E0C File Offset: 0x0000700C
		public static void Clear()
		{
			GlobalDiagnosticsContext.Clear();
		}
	}
}
