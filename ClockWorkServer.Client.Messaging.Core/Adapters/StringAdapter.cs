using System;
using System.Collections.Generic;
using System.Linq;

namespace TechnoPro.ClockWorkServer.Client.Messaging.Core.Adapters
{
	// Token: 0x0200000B RID: 11
	public static class StringAdapter
	{
		// Token: 0x0600003C RID: 60 RVA: 0x00002A01 File Offset: 0x00000C01
		public static IList<string> SplitCommaSeparatedStrings(this string s)
		{
			List<string> list = s.Split(new char[]
			{
				','
			}).ToList<string>();
			list.ForEach(delegate(string item)
			{
				item.Trim();
			});
			return list;
		}
	}
}
