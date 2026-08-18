using System;

namespace TechnoPro.Common.Rest.ClientManager.Core.Adapters
{
	// Token: 0x0200008F RID: 143
	public static class ConnectionStringAdapter
	{
		// Token: 0x060005E9 RID: 1513 RVA: 0x000106E8 File Offset: 0x0000E8E8
		public static string ParseConnectionString(this string cs)
		{
			if (string.IsNullOrEmpty(cs))
			{
				return "";
			}
			if (cs.StartsWith("provider=", StringComparison.OrdinalIgnoreCase))
			{
				int num = cs.IndexOf(';');
				if (num > 0)
				{
					return cs.Substring(num + 1);
				}
			}
			return cs;
		}
	}
}
