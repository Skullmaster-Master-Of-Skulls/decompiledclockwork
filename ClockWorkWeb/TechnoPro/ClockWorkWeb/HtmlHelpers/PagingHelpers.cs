using System;
using System.Text;
using System.Web.Mvc;
using TechnoPro.ClockWorkWeb.Models;

namespace TechnoPro.ClockWorkWeb.HtmlHelpers
{
	// Token: 0x02000117 RID: 279
	public static class PagingHelpers
	{
		// Token: 0x0600081D RID: 2077 RVA: 0x0003B1D4 File Offset: 0x000393D4
		public static MvcHtmlString PageLinks(this HtmlHelper html, PagingInfo pagingInfo, Func<int, string> pageUrl)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 1; i <= pagingInfo.TotalPages; i++)
			{
				TagBuilder tagBuilder = new TagBuilder("a");
				tagBuilder.MergeAttribute("href", pageUrl(i));
				tagBuilder.InnerHtml = i.ToString();
				bool flag = i == pagingInfo.CurrentPage;
				if (flag)
				{
					tagBuilder.AddCssClass("selected");
				}
				stringBuilder.Append(tagBuilder.ToString());
			}
			return MvcHtmlString.Create(stringBuilder.ToString());
		}
	}
}
