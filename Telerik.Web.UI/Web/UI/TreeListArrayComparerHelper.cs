using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001232 RID: 4658
	internal static class TreeListArrayComparerHelper
	{
		// Token: 0x0600C029 RID: 49193 RVA: 0x002AA67C File Offset: 0x002A887C
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
