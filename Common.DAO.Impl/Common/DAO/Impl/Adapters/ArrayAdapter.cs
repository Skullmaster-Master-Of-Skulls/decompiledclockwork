using System;
using System.Collections.Generic;

namespace TechnoPro.Common.DAO.Impl.Adapters
{
	// Token: 0x02000179 RID: 377
	public static class ArrayAdapter
	{
		// Token: 0x06000B59 RID: 2905 RVA: 0x000789B8 File Offset: 0x00076BB8
		public static IList<int> ToIntList(this string commaSeparatedList)
		{
			string[] array = commaSeparatedList.Split(new char[]
			{
				','
			}, StringSplitOptions.RemoveEmptyEntries);
			List<int> list = new List<int>();
			foreach (string s in array)
			{
				int item;
				bool flag = int.TryParse(s, out item);
				if (flag)
				{
					list.Add(item);
				}
			}
			return list;
		}
	}
}
