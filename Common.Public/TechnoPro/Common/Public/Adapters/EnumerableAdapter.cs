using System;
using System.Collections.Generic;
using System.Linq;

namespace TechnoPro.Common.Public.Adapters
{
	// Token: 0x020005EE RID: 1518
	public static class EnumerableAdapter
	{
		// Token: 0x060030D3 RID: 12499 RVA: 0x00042BE0 File Offset: 0x00040DE0
		public static TU MaxWithValue<TU, TV>(this IEnumerable<TU> values, Func<TU, TV> compProjection) where TV : IComparable<TV>
		{
			TU[] array = values.ToArray<TU>();
			bool flag = array.Length == 0;
			TU result;
			if (flag)
			{
				result = default(TU);
			}
			else
			{
				int num = 0;
				TV tv = compProjection(array[0]);
				for (int i = 1; i < array.Length; i++)
				{
					TV tv2 = compProjection(array[i]);
					bool flag2 = tv.CompareTo(tv2) < 0;
					if (flag2)
					{
						tv = tv2;
						num = i;
					}
				}
				result = array[num];
			}
			return result;
		}

		// Token: 0x060030D4 RID: 12500 RVA: 0x00042C74 File Offset: 0x00040E74
		public static TU MinWithValue<TU, TV>(this IEnumerable<TU> values, Func<TU, TV> compProjection) where TV : IComparable<TV>
		{
			TU[] array = values.ToArray<TU>();
			bool flag = array.Length == 0;
			TU result;
			if (flag)
			{
				result = default(TU);
			}
			else
			{
				int num = 0;
				TV tv = compProjection(array[0]);
				for (int i = 1; i < array.Length; i++)
				{
					TV tv2 = compProjection(array[i]);
					bool flag2 = tv.CompareTo(tv2) > 0;
					if (flag2)
					{
						tv = tv2;
						num = i;
					}
				}
				result = array[num];
			}
			return result;
		}
	}
}
