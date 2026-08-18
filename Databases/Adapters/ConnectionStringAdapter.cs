using System;

namespace Databases.Adapters
{
	// Token: 0x0200000C RID: 12
	public static class ConnectionStringAdapter
	{
		// Token: 0x060000B7 RID: 183 RVA: 0x00005F90 File Offset: 0x00004190
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
