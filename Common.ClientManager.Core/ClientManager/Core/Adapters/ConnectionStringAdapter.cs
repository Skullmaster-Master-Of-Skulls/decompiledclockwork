using System;

namespace TechnoPro.Common.ClientManager.Core.Adapters
{
	// Token: 0x020000A7 RID: 167
	public static class ConnectionStringAdapter
	{
		// Token: 0x0600065D RID: 1629 RVA: 0x0001BE24 File Offset: 0x0001A024
		public static string ParseConnectionString(this string cs)
		{
			bool flag = string.IsNullOrEmpty(cs);
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				bool flag2 = cs.StartsWith("provider=", StringComparison.OrdinalIgnoreCase);
				if (flag2)
				{
					int num = cs.IndexOf(';');
					bool flag3 = num > 0;
					if (flag3)
					{
						return cs.Substring(num + 1);
					}
				}
				result = cs;
			}
			return result;
		}
	}
}
