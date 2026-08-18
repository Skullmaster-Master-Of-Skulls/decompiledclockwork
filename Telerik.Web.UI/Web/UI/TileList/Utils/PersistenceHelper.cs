using System;
using System.Collections;
using System.Collections.Generic;

namespace Telerik.Web.UI.TileList.Utils
{
	// Token: 0x0200091F RID: 2335
	internal class PersistenceHelper
	{
		// Token: 0x06005866 RID: 22630 RVA: 0x0010DE1C File Offset: 0x0010C01C
		public static List<int[]> GetTileGroupIndicesAsList(ArrayList input)
		{
			if (input == null)
			{
				return new List<int[]>(0);
			}
			List<int[]> list = new List<int[]>(input.Count);
			foreach (object obj in input)
			{
				ArrayList arrayList = (ArrayList)obj;
				List<int> list2 = new List<int>(arrayList.Count);
				foreach (object obj2 in arrayList)
				{
					int item = (int)obj2;
					list2.Add(item);
				}
				list.Add(list2.ToArray());
			}
			return list;
		}

		// Token: 0x06005867 RID: 22631 RVA: 0x0010DEEC File Offset: 0x0010C0EC
		public static ArrayList GetTileGroupIndicesAsArrayList(List<int[]> input)
		{
			ArrayList arrayList = new ArrayList();
			if (input != null)
			{
				foreach (int[] c in input)
				{
					ArrayList value = new ArrayList(c);
					arrayList.Add(value);
				}
			}
			return arrayList;
		}
	}
}
