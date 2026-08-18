using System;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000CA3 RID: 3235
	public static class PivotTrace
	{
		// Token: 0x06007970 RID: 31088 RVA: 0x001BE897 File Offset: 0x001BCA97
		public static void SetTraceWriter(IPivotTraceWriter newWriter)
		{
			if (newWriter != null)
			{
				PivotTrace.writer = newWriter;
			}
		}

		// Token: 0x06007971 RID: 31089 RVA: 0x001BE8A2 File Offset: 0x001BCAA2
		internal static void WriteTraceForDataProvider(string line)
		{
			PivotTrace.writer.WriteLine(line);
		}

		// Token: 0x0400212C RID: 8492
		private static IPivotTraceWriter writer = new DefaultTraceWriter();
	}
}
