using System;

namespace TechnoPro.Common.DAO.Impl.Adapters
{
	// Token: 0x0200017F RID: 383
	internal static class FullTextSearchAdapter
	{
		// Token: 0x06000B64 RID: 2916 RVA: 0x00078F88 File Offset: 0x00077188
		internal static string ProccessSearchText(this string searchText)
		{
			bool flag = string.IsNullOrEmpty(searchText);
			string result;
			if (flag)
			{
				result = searchText;
			}
			else
			{
				string[] array = searchText.Split(new string[]
				{
					" "
				}, StringSplitOptions.RemoveEmptyEntries);
				result = ((array == null || array.Length == 0) ? string.Empty : string.Join(" OR ", array));
			}
			return result;
		}
	}
}
