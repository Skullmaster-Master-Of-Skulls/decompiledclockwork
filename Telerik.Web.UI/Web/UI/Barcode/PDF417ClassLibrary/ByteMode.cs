using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Barcode.PDF417ClassLibrary
{
	// Token: 0x02000097 RID: 151
	internal class ByteMode
	{
		// Token: 0x06000598 RID: 1432 RVA: 0x0000E07C File Offset: 0x0000C27C
		internal static List<long> EncodeText(string text)
		{
			List<long> list = new List<long>();
			foreach (char c in text)
			{
				if (SpecificationData.ByteModeValues.Contains((int)c))
				{
					list.Add((long)((ulong)c));
				}
			}
			return ByteMode.EncodeData(list);
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x0000E0C8 File Offset: 0x0000C2C8
		internal static List<long> EncodeData(List<long> values)
		{
			List<long> list = new List<long>();
			if (values.Count == 0)
			{
				return list;
			}
			if (values.Count % 6 == 0)
			{
				list.Add(924L);
			}
			else
			{
				list.Add(901L);
			}
			for (int i = 0; i < values.Count; i += 6)
			{
				List<long> list2 = new List<long>();
				int num = 0;
				while (i + num < values.Count)
				{
					list2.Add(values[i + num]);
					if (list2.Count == 6)
					{
						break;
					}
					num++;
				}
				if (list2.Count == 6)
				{
					list.AddRange(ByteMode.GetValuesForLongRange(list2));
				}
				else
				{
					list.AddRange(list2);
				}
			}
			return list;
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x0000E16C File Offset: 0x0000C36C
		internal static List<long> GetValuesForLongRange(List<long> localValues)
		{
			List<long> list = new List<long>(5);
			long num = (long)((double)localValues[0] * Math.Pow(256.0, 5.0) + (double)localValues[1] * Math.Pow(256.0, 4.0) + (double)localValues[2] * Math.Pow(256.0, 3.0) + (double)localValues[3] * Math.Pow(256.0, 2.0) + (double)localValues[4] * Math.Pow(256.0, 1.0) + (double)localValues[5] * Math.Pow(256.0, 0.0));
			for (int i = 0; i < list.Capacity; i++)
			{
				long item = num % 900L;
				num /= 900L;
				list.Add(item);
			}
			return ByteMode.Reorder(list);
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x0000E27C File Offset: 0x0000C47C
		private static List<long> Reorder(List<long> values)
		{
			List<long> list = new List<long>();
			list.AddRange(values);
			int num = 0;
			for (int i = values.Count - 1; i >= 0; i--)
			{
				list[i] = values[num];
				num++;
			}
			return list;
		}
	}
}
