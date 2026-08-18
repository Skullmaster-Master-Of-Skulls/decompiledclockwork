using System;
using System.Web;
using System.Web.UI;

namespace TechnoPro.Common.UI.Web.Entity.Adapters
{
	// Token: 0x02000059 RID: 89
	public static class WebContextAdapter
	{
		// Token: 0x06000282 RID: 642 RVA: 0x00005FCC File Offset: 0x000041CC
		public static int GetIntegerFromQueryString(this Page page, string itemName)
		{
			bool flag = page == null;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				HttpRequest request = HttpContext.Current.Request;
				string text = request[itemName] ?? "";
				int num;
				bool flag2 = string.IsNullOrEmpty(text) || !int.TryParse(text, out num);
				if (flag2)
				{
					num = 0;
				}
				bool flag3 = num > 0;
				if (flag3)
				{
					result = num;
				}
				else
				{
					object obj = page.RouteData.Values[itemName];
					bool flag4 = obj == null;
					if (flag4)
					{
						result = 0;
					}
					else
					{
						try
						{
							return Convert.ToInt32(obj);
						}
						catch
						{
						}
						result = 0;
					}
				}
			}
			return result;
		}
	}
}
