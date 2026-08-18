using System;

namespace TechnoPro.Common.Public.Adapters
{
	// Token: 0x020005E8 RID: 1512
	public static class BoolAdapter
	{
		// Token: 0x060030C4 RID: 12484 RVA: 0x00042640 File Offset: 0x00040840
		public static bool? ConvertStringToBool(this string s)
		{
			bool flag = string.IsNullOrEmpty(s);
			bool? result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool value;
				bool flag2 = !bool.TryParse(s, out value);
				if (flag2)
				{
					result = null;
				}
				else
				{
					result = new bool?(value);
				}
			}
			return result;
		}

		// Token: 0x060030C5 RID: 12485 RVA: 0x0004268C File Offset: 0x0004088C
		public static bool ConvertStringToBool(this string s, bool defaultValue)
		{
			return s.ConvertStringToBool() ?? defaultValue;
		}
	}
}
