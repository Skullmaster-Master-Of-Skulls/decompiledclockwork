using System;

namespace Telerik.Web.UI
{
	// Token: 0x0200199F RID: 6559
	internal static class ListViewArrayComparerHelper
	{
		// Token: 0x0600FDB1 RID: 64945 RVA: 0x0038FBC4 File Offset: 0x0038DDC4
		public static bool CompareStringArrays(string[] first, string[] second)
		{
			if (first == null && second == null)
			{
				return true;
			}
			if (first == null || second == null)
			{
				return false;
			}
			if (first.Length != second.Length)
			{
				return false;
			}
			for (int i = 0; i < first.Length; i++)
			{
				if (string.CompareOrdinal(first[i], second[i]) != 0)
				{
					return false;
				}
			}
			return true;
		}
	}
}
