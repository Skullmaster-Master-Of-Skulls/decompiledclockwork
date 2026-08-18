using System;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using TechnoPro.ClockWorkWeb.Models;

namespace TechnoPro.ClockWorkWeb.AjaxHelpers
{
	// Token: 0x0200018D RID: 397
	public static class AjaxPagingHelpers
	{
		// Token: 0x06000BB5 RID: 2997 RVA: 0x0004BEBC File Offset: 0x0004A0BC
		public static MvcHtmlString PageLinks(this AjaxHelper ajax, PagingInfo pagingInfo, string actionName, string controllerName, string updatingTargetId, Func<int, string> pageUrl, Func<int, object> routeValues = null, Func<int, object> htmlAttributes = null, string onSuccess = null)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 1; i <= pagingInfo.TotalPages; i++)
			{
				AjaxOptions ajaxOptions = new AjaxOptions
				{
					UpdateTargetId = updatingTargetId,
					Url = pageUrl(i),
					OnSuccess = onSuccess
				};
				MvcHtmlString mvcHtmlString = ajax.ActionLink(i.ToString(), actionName, controllerName, (routeValues != null) ? routeValues(i) : null, ajaxOptions, (htmlAttributes != null) ? htmlAttributes(i) : null);
				stringBuilder.Append(mvcHtmlString.ToString());
			}
			return MvcHtmlString.Create(stringBuilder.ToString());
		}
	}
}
