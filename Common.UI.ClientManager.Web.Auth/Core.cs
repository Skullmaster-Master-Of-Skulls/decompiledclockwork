using System;

namespace TechnoPro.Common.UI.ClientManager.Web.Auth
{
	// Token: 0x0200000B RID: 11
	public class Core
	{
		// Token: 0x06000053 RID: 83 RVA: 0x0000485C File Offset: 0x00002A5C
		public static bool ParseBooleanAttribute(string s, bool defaultValue)
		{
			bool flag = string.IsNullOrEmpty(s);
			bool result;
			if (flag)
			{
				result = defaultValue;
			}
			else
			{
				bool flag2 = s.Equals("0");
				if (flag2)
				{
					result = false;
				}
				else
				{
					bool flag3 = s.Equals("1");
					bool flag4;
					result = (flag3 || (bool.TryParse(s, out flag4) ? flag4 : defaultValue));
				}
			}
			return result;
		}
	}
}
