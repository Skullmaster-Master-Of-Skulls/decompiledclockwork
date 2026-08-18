using System;

namespace NLog
{
	// Token: 0x0200013D RID: 317
	[Obsolete("Use MappedDiagnosticsContext instead")]
	public static class MDC
	{
		// Token: 0x06000B2D RID: 2861 RVA: 0x00019B3C File Offset: 0x00017D3C
		public static void Set(string item, string value)
		{
			MappedDiagnosticsContext.Set(item, value);
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x00019B45 File Offset: 0x00017D45
		public static string Get(string item)
		{
			return MappedDiagnosticsContext.Get(item);
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x00019B4D File Offset: 0x00017D4D
		public static object GetObject(string item)
		{
			return MappedDiagnosticsContext.GetObject(item);
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x00019B55 File Offset: 0x00017D55
		public static bool Contains(string item)
		{
			return MappedDiagnosticsContext.Contains(item);
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x00019B5D File Offset: 0x00017D5D
		public static void Remove(string item)
		{
			MappedDiagnosticsContext.Remove(item);
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x00019B65 File Offset: 0x00017D65
		public static void Clear()
		{
			MappedDiagnosticsContext.Clear();
		}
	}
}
