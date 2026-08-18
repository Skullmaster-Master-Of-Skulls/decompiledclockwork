using System;
using System.Globalization;
using System.IO;

namespace System.Web.Mvc.Html
{
	// Token: 0x02000157 RID: 343
	public static class PartialExtensions
	{
		// Token: 0x060008CC RID: 2252 RVA: 0x00018362 File Offset: 0x00016562
		public static MvcHtmlString Partial(this HtmlHelper htmlHelper, string partialViewName)
		{
			return htmlHelper.Partial(partialViewName, null, htmlHelper.ViewData);
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x00018372 File Offset: 0x00016572
		public static MvcHtmlString Partial(this HtmlHelper htmlHelper, string partialViewName, ViewDataDictionary viewData)
		{
			return htmlHelper.Partial(partialViewName, null, viewData);
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x0001837D File Offset: 0x0001657D
		public static MvcHtmlString Partial(this HtmlHelper htmlHelper, string partialViewName, object model)
		{
			return htmlHelper.Partial(partialViewName, model, htmlHelper.ViewData);
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x00018390 File Offset: 0x00016590
		public static MvcHtmlString Partial(this HtmlHelper htmlHelper, string partialViewName, object model, ViewDataDictionary viewData)
		{
			MvcHtmlString result;
			using (StringWriter stringWriter = new StringWriter(CultureInfo.CurrentCulture))
			{
				htmlHelper.RenderPartialInternal(partialViewName, viewData, model, stringWriter, ViewEngines.Engines);
				result = MvcHtmlString.Create(stringWriter.ToString());
			}
			return result;
		}
	}
}
