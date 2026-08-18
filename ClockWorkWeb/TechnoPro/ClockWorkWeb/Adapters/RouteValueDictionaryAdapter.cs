using System;
using System.Collections.Generic;
using System.Web.Routing;

namespace TechnoPro.ClockWorkWeb.Adapters
{
	// Token: 0x02000196 RID: 406
	public static class RouteValueDictionaryAdapter
	{
		// Token: 0x06000BDC RID: 3036 RVA: 0x0004D544 File Offset: 0x0004B744
		public static RouteValueDictionary FixListRouteDataValues(this RouteValueDictionary routes)
		{
			RouteValueDictionary routeValueDictionary = new RouteValueDictionary();
			foreach (string text in routes.Keys)
			{
				object obj = routes[text];
				bool flag = obj is string;
				if (flag)
				{
					routeValueDictionary.Add(text, obj);
				}
				else
				{
					bool flag2 = obj is IEnumerable<object>;
					if (flag2)
					{
						int num = 0;
						foreach (object value in ((IEnumerable<object>)obj))
						{
							routeValueDictionary.Add(string.Format("{0}[{1}]", text, num), value);
							num++;
						}
					}
					else
					{
						routeValueDictionary.Add(text, obj);
					}
				}
			}
			return routeValueDictionary;
		}
	}
}
