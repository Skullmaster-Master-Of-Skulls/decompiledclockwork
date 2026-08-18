using System;

namespace System.Web.Mvc.Html
{
	// Token: 0x020001BA RID: 442
	public static class RenderPartialExtensions
	{
		// Token: 0x06000CBA RID: 3258 RVA: 0x000218FB File Offset: 0x0001FAFB
		public static void RenderPartial(this HtmlHelper htmlHelper, string partialViewName)
		{
			htmlHelper.RenderPartialInternal(partialViewName, htmlHelper.ViewData, null, htmlHelper.ViewContext.Writer, ViewEngines.Engines);
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x0002191B File Offset: 0x0001FB1B
		public static void RenderPartial(this HtmlHelper htmlHelper, string partialViewName, ViewDataDictionary viewData)
		{
			htmlHelper.RenderPartialInternal(partialViewName, viewData, null, htmlHelper.ViewContext.Writer, ViewEngines.Engines);
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x00021936 File Offset: 0x0001FB36
		public static void RenderPartial(this HtmlHelper htmlHelper, string partialViewName, object model)
		{
			htmlHelper.RenderPartialInternal(partialViewName, htmlHelper.ViewData, model, htmlHelper.ViewContext.Writer, ViewEngines.Engines);
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x00021956 File Offset: 0x0001FB56
		public static void RenderPartial(this HtmlHelper htmlHelper, string partialViewName, object model, ViewDataDictionary viewData)
		{
			htmlHelper.RenderPartialInternal(partialViewName, viewData, model, htmlHelper.ViewContext.Writer, ViewEngines.Engines);
		}
	}
}
