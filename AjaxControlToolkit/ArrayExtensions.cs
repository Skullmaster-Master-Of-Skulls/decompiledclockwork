using System;
using System.Collections.Generic;
using System.Linq;

namespace AjaxControlToolkit
{
	// Token: 0x0200003A RID: 58
	public static class ArrayExtensions
	{
		// Token: 0x060001FF RID: 511 RVA: 0x000071A4 File Offset: 0x000053A4
		public static IEnumerable<int> StartingIndex(this byte[] x, byte[] y)
		{
			ArrayExtensions.<>c__DisplayClass1 CS$<>8__locals1 = new ArrayExtensions.<>c__DisplayClass1();
			CS$<>8__locals1.x = x;
			CS$<>8__locals1.y = y;
			IEnumerable<int> enumerable = Enumerable.Range(0, CS$<>8__locals1.x.Length - CS$<>8__locals1.y.Length + 1);
			int i;
			for (i = 0; i < CS$<>8__locals1.y.Length; i++)
			{
				enumerable = (from n in enumerable
				where CS$<>8__locals1.x[n + i] == CS$<>8__locals1.y[i]
				select n).ToArray<int>();
			}
			return enumerable;
		}
	}
}
