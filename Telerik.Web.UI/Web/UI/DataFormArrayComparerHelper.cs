using System;

namespace Telerik.Web.UI
{
	// Token: 0x020001E4 RID: 484
	internal static class DataFormArrayComparerHelper
	{
		// Token: 0x06001121 RID: 4385 RVA: 0x0003EB88 File Offset: 0x0003CD88
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
