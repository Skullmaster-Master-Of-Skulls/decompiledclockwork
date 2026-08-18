using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Web.Security.AntiXss.CodeCharts
{
	// Token: 0x0200061E RID: 1566
	internal static class CodeChartHelper
	{
		// Token: 0x06004E29 RID: 20009 RVA: 0x00111030 File Offset: 0x0010F230
		internal static IEnumerable<int> GetRange(int min, int max, Func<int, bool> exclusionFilter)
		{
			IEnumerable<int> enumerable = Enumerable.Range(min, max - min + 1);
			if (exclusionFilter != null)
			{
				enumerable = from i in enumerable
				where !exclusionFilter(i)
				select i;
			}
			return enumerable;
		}

		// Token: 0x06004E2A RID: 20010 RVA: 0x00111072 File Offset: 0x0010F272
		internal static IEnumerable<int> GetRange(int min, int max)
		{
			return CodeChartHelper.GetRange(min, max, null);
		}
	}
}
