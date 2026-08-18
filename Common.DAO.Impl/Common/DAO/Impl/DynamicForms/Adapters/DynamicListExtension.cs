using System;
using System.Collections.Generic;
using System.Linq;

namespace TechnoPro.Common.DAO.Impl.DynamicForms.Adapters
{
	// Token: 0x020000F0 RID: 240
	public static class DynamicListExtension
	{
		// Token: 0x060006E2 RID: 1762 RVA: 0x00048278 File Offset: 0x00046478
		public static List<string[]> DecodeDocumentsList(this string list)
		{
			List<string[]> list2 = new List<string[]>();
			bool flag = string.IsNullOrEmpty(list);
			List<string[]> result;
			if (flag)
			{
				result = list2;
			}
			else
			{
				string[] array = list.Split(new char[]
				{
					'\t'
				});
				string[] array2 = new string[0];
				foreach (string text in array)
				{
					string[] array4 = text.Split(new char[1]);
					array2 = new string[array4.Length];
					for (int j = 0; j < array4.Length; j++)
					{
						array2[j] = array4[j];
					}
					list2.Add(array2);
				}
				result = list2;
			}
			return result;
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x00048324 File Offset: 0x00046524
		public static string EncodeDocumentsList<T>(this IList<T> rowItems, Func<T, int, string[]> rowItemToColList, int numItems)
		{
			bool flag = rowItems == null || rowItems.Count < 1;
			string result;
			if (flag)
			{
				result = string.Empty;
			}
			else
			{
				result = string.Join('\t'.ToString(), from g in rowItems
				select string.Join('\0'.ToString(), rowItemToColList(g, numItems)));
			}
			return result;
		}
	}
}
