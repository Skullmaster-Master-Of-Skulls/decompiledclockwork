using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;

namespace TechnoPro.ClockWorkWeb.HtmlHelpers
{
	// Token: 0x02000116 RID: 278
	public static class HtmlHelperAdapter
	{
		// Token: 0x0600081C RID: 2076 RVA: 0x0003B00C File Offset: 0x0003920C
		public static string ActionLinkWithList(this HtmlHelper helper, string text, string action, string controller, object routeData, object htmlAttributes)
		{
			UrlHelper urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
			string text2 = urlHelper.Action(action, controller);
			bool flag = routeData != null;
			if (flag)
			{
				RouteValueDictionary routeValueDictionary = new RouteValueDictionary(routeData);
				List<string> list = new List<string>();
				foreach (string text3 in routeValueDictionary.Keys)
				{
					object obj = routeValueDictionary[text3];
					bool flag2 = obj is IEnumerable && !(obj is string);
					if (flag2)
					{
						int num = 0;
						foreach (object arg in ((IEnumerable)obj))
						{
							list.Add(string.Format("{0}[{2}]={1}", text3, arg, num));
							num++;
						}
					}
					else
					{
						bool flag3 = obj != null;
						if (flag3)
						{
							list.Add(string.Format("{0}={1}", text3, obj));
						}
					}
				}
				string text4 = string.Join("&", list.ToArray());
				bool flag4 = !string.IsNullOrEmpty(text4);
				if (flag4)
				{
					text2 = text2 + "?" + text4;
				}
			}
			TagBuilder tagBuilder = new TagBuilder("a");
			tagBuilder.Attributes.Add("href", text2);
			tagBuilder.MergeAttributes<string, object>(new RouteValueDictionary(htmlAttributes));
			tagBuilder.SetInnerText(text);
			return tagBuilder.ToString(TagRenderMode.Normal);
		}
	}
}
