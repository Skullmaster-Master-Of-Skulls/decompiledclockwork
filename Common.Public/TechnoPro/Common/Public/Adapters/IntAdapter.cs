using System;

namespace TechnoPro.Common.Public.Adapters
{
	// Token: 0x020005EF RID: 1519
	public static class IntAdapter
	{
		// Token: 0x060030D5 RID: 12501 RVA: 0x00042D08 File Offset: 0x00040F08
		public static int? ConvertStringToInt(this string s)
		{
			bool flag = string.IsNullOrEmpty(s);
			int? result;
			if (flag)
			{
				result = null;
			}
			else
			{
				int value;
				bool flag2 = !int.TryParse(s, out value);
				if (flag2)
				{
					result = null;
				}
				else
				{
					result = new int?(value);
				}
			}
			return result;
		}

		// Token: 0x060030D6 RID: 12502 RVA: 0x00042D54 File Offset: 0x00040F54
		public static int ConvertStringToInt(this string s, int defaultValue)
		{
			return s.ConvertStringToInt() ?? defaultValue;
		}
	}
}
