using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc.Html;
using System.Web.Mvc.Properties;
using System.Web.Routing;
using System.Web.WebPages;

namespace System.Web.Mvc.Ajax
{
	// Token: 0x02000177 RID: 375
	public static class AjaxExtensions
	{
		// Token: 0x060009E2 RID: 2530 RVA: 0x0001B69E File Offset: 0x0001989E
		public static MvcHtmlString ActionLink(this AjaxHelper ajaxHelper, string linkText, string actionName, AjaxOptions ajaxOptions)
		{
			return ajaxHelper.ActionLink(linkText, actionName, null, ajaxOptions);
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x0001B6AA File Offset: 0x000198AA
		public static MvcHtmlString ActionLink(this AjaxHelper ajaxHelper, string linkText, string actionName, object routeValues, AjaxOptions ajaxOptions)
		{
			return ajaxHelper.ActionLink(linkText, actionName, null, routeValues, ajaxOptions);
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x0001B6B8 File Offset: 0x000198B8
		public static MvcHtmlString ActionLink(this AjaxHelper ajaxHelper, string linkText, string actionName, object routeValues, AjaxOptions ajaxOptions, object htmlAttributes)
		{
			return ajaxHelper.ActionLink(linkText, actionName, null, routeValues, ajaxOptions, htmlAttributes);
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x0001B6C8 File Offset: 0x000198C8
		public static MvcHtmlString ActionLink(this AjaxHelper ajaxHelper, string linkText, string actionName, RouteValueDictionary routeValues, AjaxOptions ajaxOptions)
		{
			return ajaxHelper.ActionLink(linkText, actionName, null, routeValues, ajaxOptions);
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x0001B6D6 File Offset: 0x000198D6
		public static MvcHtmlString ActionLink(this AjaxHelper ajaxHelper, string linkText, string actionName, RouteValueDictionary routeValues, AjaxOptions ajaxOptions, IDictionary<string, object> htmlAttributes)
		{
			return ajaxHelper.ActionLink(linkText, actionName, null, routeValues, ajaxOptions, htmlAttributes);
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x0001B6E6 File Offset: 0x000198E6
		public static MvcHtmlString ActionLink(this AjaxHelper ajaxHelper, string linkText, string actionName, string controllerName, AjaxOptions ajaxOptions)
		{
			return ajaxHelper.ActionLink(linkText, actionName, controllerName, null, ajaxOptions, null);
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x0001B6F5 File Offset: 0x000198F5
		public static MvcHtmlString ActionLink(this AjaxHelper ajaxHelper, string linkText, string actionName, string controllerName, object routeValues, AjaxOptions ajaxOptions)
		{
			return ajaxHelper.ActionLink(linkText, actionName, controllerName, routeValues, ajaxOptions, null);
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x0001B708 File Offset: 0x00019908
		public static MvcHtmlString ActionLink(this AjaxHelper ajaxHelper, string linkText, string actionName, string controllerName, object routeValues, AjaxOptions ajaxOptions, object htmlAttributes)
		{
			RouteValueDictionary routeValues2 = TypeHelper.ObjectToDictionary(routeValues);
			RouteValueDictionary htmlAttributes2 = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
			return ajaxHelper.ActionLink(linkText, actionName, controllerName, routeValues2, ajaxOptions, htmlAttributes2);
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x0001B732 File Offset: 0x00019932
		public static MvcHtmlString ActionLink(this AjaxHelper ajaxHelper, string linkText, string actionName, string controllerName, RouteValueDictionary routeValues, AjaxOptions ajaxOptions)
		{
			return ajaxHelper.ActionLink(linkText, actionName, controllerName, routeValues, ajaxOptions, null);
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x0001B744 File Offset: 0x00019944
		public static MvcHtmlString ActionLink(this AjaxHelper ajaxHelper, string linkText, string actionName, string controllerName, RouteValueDictionary routeValues, AjaxOptions ajaxOptions, IDictionary<string, object> htmlAttributes)
		{
			if (string.IsNullOrEmpty(linkText))
			{
				throw new ArgumentException(MvcResources.Common_NullOrEmpty, "linkText");
			}
			string targetUrl = UrlHelper.GenerateUrl(null, actionName, controllerName, routeValues, ajaxHelper.RouteCollection, ajaxHelper.ViewContext.RequestContext, true);
			return MvcHtmlString.Create(AjaxExtensions.GenerateLink(ajaxHelper, linkText, targetUrl, AjaxExtensions.GetAjaxOptions(ajaxOptions), htmlAttributes));
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x0001B79C File Offset: 0x0001999C
		public static MvcHtmlString ActionLink(this AjaxHelper ajaxHelper, string linkText, string actionName, string controllerName, string protocol, string hostName, string fragment, object routeValues, AjaxOptions ajaxOptions, object htmlAttributes)
		{
			RouteValueDictionary routeValues2 = TypeHelper.ObjectToDictionary(routeValues);
			RouteValueDictionary htmlAttributes2 = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
			return ajaxHelper.ActionLink(linkText, actionName, controllerName, protocol, hostName, fragment, routeValues2, ajaxOptions, htmlAttributes2);
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x0001B7CC File Offset: 0x000199CC
		public static MvcHtmlString ActionLink(this AjaxHelper ajaxHelper, string linkText, string actionName, string controllerName, string protocol, string hostName, string fragment, RouteValueDictionary routeValues, AjaxOptions ajaxOptions, IDictionary<string, object> htmlAttributes)
		{
			if (string.IsNullOrEmpty(linkText))
			{
				throw new ArgumentException(MvcResources.Common_NullOrEmpty, "linkText");
			}
			string targetUrl = UrlHelper.GenerateUrl(null, actionName, controllerName, protocol, hostName, fragment, routeValues, ajaxHelper.RouteCollection, ajaxHelper.ViewContext.RequestContext, true);
			return MvcHtmlString.Create(AjaxExtensions.GenerateLink(ajaxHelper, linkText, targetUrl, ajaxOptions, htmlAttributes));
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x0001B828 File Offset: 0x00019A28
		public static MvcForm BeginForm(this AjaxHelper ajaxHelper, AjaxOptions ajaxOptions)
		{
			string rawUrl = ajaxHelper.ViewContext.HttpContext.Request.RawUrl;
			return ajaxHelper.FormHelper(rawUrl, ajaxOptions, new RouteValueDictionary());
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x0001B858 File Offset: 0x00019A58
		public static MvcForm BeginForm(this AjaxHelper ajaxHelper, string actionName, AjaxOptions ajaxOptions)
		{
			return ajaxHelper.BeginForm(actionName, null, ajaxOptions);
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x0001B863 File Offset: 0x00019A63
		public static MvcForm BeginForm(this AjaxHelper ajaxHelper, string actionName, object routeValues, AjaxOptions ajaxOptions)
		{
			return ajaxHelper.BeginForm(actionName, null, routeValues, ajaxOptions);
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x0001B86F File Offset: 0x00019A6F
		public static MvcForm BeginForm(this AjaxHelper ajaxHelper, string actionName, object routeValues, AjaxOptions ajaxOptions, object htmlAttributes)
		{
			return ajaxHelper.BeginForm(actionName, null, routeValues, ajaxOptions, htmlAttributes);
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x0001B87D File Offset: 0x00019A7D
		public static MvcForm BeginForm(this AjaxHelper ajaxHelper, string actionName, RouteValueDictionary routeValues, AjaxOptions ajaxOptions)
		{
			return ajaxHelper.BeginForm(actionName, null, routeValues, ajaxOptions);
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x0001B889 File Offset: 0x00019A89
		public static MvcForm BeginForm(this AjaxHelper ajaxHelper, string actionName, RouteValueDictionary routeValues, AjaxOptions ajaxOptions, IDictionary<string, object> htmlAttributes)
		{
			return ajaxHelper.BeginForm(actionName, null, routeValues, ajaxOptions, htmlAttributes);
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x0001B897 File Offset: 0x00019A97
		public static MvcForm BeginForm(this AjaxHelper ajaxHelper, string actionName, string controllerName, AjaxOptions ajaxOptions)
		{
			return ajaxHelper.BeginForm(actionName, controllerName, null, ajaxOptions, null);
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x0001B8A4 File Offset: 0x00019AA4
		public static MvcForm BeginForm(this AjaxHelper ajaxHelper, string actionName, string controllerName, object routeValues, AjaxOptions ajaxOptions)
		{
			return ajaxHelper.BeginForm(actionName, controllerName, routeValues, ajaxOptions, null);
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x0001B8B4 File Offset: 0x00019AB4
		public static MvcForm BeginForm(this AjaxHelper ajaxHelper, string actionName, string controllerName, object routeValues, AjaxOptions ajaxOptions, object htmlAttributes)
		{
			RouteValueDictionary routeValues2 = new RouteValueDictionary(routeValues);
			RouteValueDictionary htmlAttributes2 = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
			return ajaxHelper.BeginForm(actionName, controllerName, routeValues2, ajaxOptions, htmlAttributes2);
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x0001B8DC File Offset: 0x00019ADC
		public static MvcForm BeginForm(this AjaxHelper ajaxHelper, string actionName, string controllerName, RouteValueDictionary routeValues, AjaxOptions ajaxOptions)
		{
			return ajaxHelper.BeginForm(actionName, controllerName, routeValues, ajaxOptions, null);
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x0001B8EC File Offset: 0x00019AEC
		public static MvcForm BeginForm(this AjaxHelper ajaxHelper, string actionName, string controllerName, RouteValueDictionary routeValues, AjaxOptions ajaxOptions, IDictionary<string, object> htmlAttributes)
		{
			string formAction = UrlHelper.GenerateUrl(null, actionName, controllerName, routeValues ?? new RouteValueDictionary(), ajaxHelper.RouteCollection, ajaxHelper.ViewContext.RequestContext, true);
			return ajaxHelper.FormHelper(formAction, ajaxOptions, htmlAttributes);
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x0001B929 File Offset: 0x00019B29
		public static MvcForm BeginRouteForm(this AjaxHelper ajaxHelper, string routeName, AjaxOptions ajaxOptions)
		{
			return ajaxHelper.BeginRouteForm(routeName, null, ajaxOptions, null);
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x0001B935 File Offset: 0x00019B35
		public static MvcForm BeginRouteForm(this AjaxHelper ajaxHelper, string routeName, object routeValues, AjaxOptions ajaxOptions)
		{
			return ajaxHelper.BeginRouteForm(routeName, routeValues, ajaxOptions, null);
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x0001B944 File Offset: 0x00019B44
		public static MvcForm BeginRouteForm(this AjaxHelper ajaxHelper, string routeName, object routeValues, AjaxOptions ajaxOptions, object htmlAttributes)
		{
			RouteValueDictionary htmlAttributes2 = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
			return ajaxHelper.BeginRouteForm(routeName, new RouteValueDictionary(routeValues), ajaxOptions, htmlAttributes2);
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x0001B968 File Offset: 0x00019B68
		public static MvcForm BeginRouteForm(this AjaxHelper ajaxHelper, string routeName, RouteValueDictionary routeValues, AjaxOptions ajaxOptions)
		{
			return ajaxHelper.BeginRouteForm(routeName, routeValues, ajaxOptions, null);
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x0001B974 File Offset: 0x00019B74
		public static MvcForm BeginRouteForm(this AjaxHelper ajaxHelper, string routeName, RouteValueDictionary routeValues, AjaxOptions ajaxOptions, IDictionary<string, object> htmlAttributes)
		{
			string formAction = UrlHelper.GenerateUrl(routeName, null, null, routeValues ?? new RouteValueDictionary(), ajaxHelper.RouteCollection, ajaxHelper.ViewContext.RequestContext, false);
			return ajaxHelper.FormHelper(formAction, ajaxOptions, htmlAttributes);
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x0001B9B0 File Offset: 0x00019BB0
		private static MvcForm FormHelper(this AjaxHelper ajaxHelper, string formAction, AjaxOptions ajaxOptions, IDictionary<string, object> htmlAttributes)
		{
			TagBuilder tagBuilder = new TagBuilder("form");
			tagBuilder.MergeAttributes<string, object>(htmlAttributes);
			tagBuilder.MergeAttribute("action", formAction);
			tagBuilder.MergeAttribute("method", "post");
			ajaxOptions = AjaxExtensions.GetAjaxOptions(ajaxOptions);
			if (ajaxHelper.ViewContext.UnobtrusiveJavaScriptEnabled)
			{
				tagBuilder.MergeAttributes<string, object>(ajaxOptions.ToUnobtrusiveHtmlAttributes());
			}
			else
			{
				tagBuilder.MergeAttribute("onclick", "Sys.Mvc.AsyncForm.handleClick(this, new Sys.UI.DomEvent(event));");
				tagBuilder.MergeAttribute("onsubmit", AjaxExtensions.GenerateAjaxScript(ajaxOptions, "Sys.Mvc.AsyncForm.handleSubmit(this, new Sys.UI.DomEvent(event), {0});"));
			}
			if (ajaxHelper.ViewContext.ClientValidationEnabled)
			{
				tagBuilder.GenerateId(ajaxHelper.ViewContext.FormIdGenerator());
			}
			ajaxHelper.ViewContext.Writer.Write(tagBuilder.ToString(TagRenderMode.StartTag));
			MvcForm result = new MvcForm(ajaxHelper.ViewContext);
			if (ajaxHelper.ViewContext.ClientValidationEnabled)
			{
				ajaxHelper.ViewContext.FormContext.FormId = tagBuilder.Attributes["id"];
			}
			return result;
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x0001BAA8 File Offset: 0x00019CA8
		public static MvcHtmlString GlobalizationScript(this AjaxHelper ajaxHelper)
		{
			return ajaxHelper.GlobalizationScript(CultureInfo.CurrentCulture);
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x0001BAB5 File Offset: 0x00019CB5
		public static MvcHtmlString GlobalizationScript(this AjaxHelper ajaxHelper, CultureInfo cultureInfo)
		{
			return AjaxExtensions.GlobalizationScriptHelper(AjaxHelper.GlobalizationScriptPath, cultureInfo);
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x0001BAC4 File Offset: 0x00019CC4
		internal static MvcHtmlString GlobalizationScriptHelper(string scriptPath, CultureInfo cultureInfo)
		{
			if (cultureInfo == null)
			{
				throw new ArgumentNullException("cultureInfo");
			}
			TagBuilder tagBuilder = new TagBuilder("script");
			tagBuilder.MergeAttribute("type", "text/javascript");
			string value = VirtualPathUtility.AppendTrailingSlash(scriptPath) + HttpUtility.UrlEncode(cultureInfo.Name) + ".js";
			tagBuilder.MergeAttribute("src", value);
			return tagBuilder.ToMvcHtmlString(TagRenderMode.Normal);
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x0001BB29 File Offset: 0x00019D29
		public static MvcHtmlString RouteLink(this AjaxHelper ajaxHelper, string linkText, object routeValues, AjaxOptions ajaxOptions)
		{
			return ajaxHelper.RouteLink(linkText, null, new RouteValueDictionary(routeValues), ajaxOptions, new Dictionary<string, object>());
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x0001BB3F File Offset: 0x00019D3F
		public static MvcHtmlString RouteLink(this AjaxHelper ajaxHelper, string linkText, object routeValues, AjaxOptions ajaxOptions, object htmlAttributes)
		{
			return ajaxHelper.RouteLink(linkText, null, new RouteValueDictionary(routeValues), ajaxOptions, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x0001BB57 File Offset: 0x00019D57
		public static MvcHtmlString RouteLink(this AjaxHelper ajaxHelper, string linkText, RouteValueDictionary routeValues, AjaxOptions ajaxOptions)
		{
			return ajaxHelper.RouteLink(linkText, null, routeValues, ajaxOptions, new Dictionary<string, object>());
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x0001BB68 File Offset: 0x00019D68
		public static MvcHtmlString RouteLink(this AjaxHelper ajaxHelper, string linkText, RouteValueDictionary routeValues, AjaxOptions ajaxOptions, IDictionary<string, object> htmlAttributes)
		{
			return ajaxHelper.RouteLink(linkText, null, routeValues, ajaxOptions, htmlAttributes);
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x0001BB76 File Offset: 0x00019D76
		public static MvcHtmlString RouteLink(this AjaxHelper ajaxHelper, string linkText, string routeName, AjaxOptions ajaxOptions)
		{
			return ajaxHelper.RouteLink(linkText, routeName, new RouteValueDictionary(), ajaxOptions, new Dictionary<string, object>());
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x0001BB8B File Offset: 0x00019D8B
		public static MvcHtmlString RouteLink(this AjaxHelper ajaxHelper, string linkText, string routeName, AjaxOptions ajaxOptions, object htmlAttributes)
		{
			return ajaxHelper.RouteLink(linkText, routeName, new RouteValueDictionary(), ajaxOptions, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x0001BBA2 File Offset: 0x00019DA2
		public static MvcHtmlString RouteLink(this AjaxHelper ajaxHelper, string linkText, string routeName, AjaxOptions ajaxOptions, IDictionary<string, object> htmlAttributes)
		{
			return ajaxHelper.RouteLink(linkText, routeName, new RouteValueDictionary(), ajaxOptions, htmlAttributes);
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x0001BBB4 File Offset: 0x00019DB4
		public static MvcHtmlString RouteLink(this AjaxHelper ajaxHelper, string linkText, string routeName, object routeValues, AjaxOptions ajaxOptions)
		{
			return ajaxHelper.RouteLink(linkText, routeName, new RouteValueDictionary(routeValues), ajaxOptions, new Dictionary<string, object>());
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x0001BBCB File Offset: 0x00019DCB
		public static MvcHtmlString RouteLink(this AjaxHelper ajaxHelper, string linkText, string routeName, object routeValues, AjaxOptions ajaxOptions, object htmlAttributes)
		{
			return ajaxHelper.RouteLink(linkText, routeName, new RouteValueDictionary(routeValues), ajaxOptions, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x0001BBE4 File Offset: 0x00019DE4
		public static MvcHtmlString RouteLink(this AjaxHelper ajaxHelper, string linkText, string routeName, RouteValueDictionary routeValues, AjaxOptions ajaxOptions)
		{
			return ajaxHelper.RouteLink(linkText, routeName, routeValues, ajaxOptions, new Dictionary<string, object>());
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x0001BBF8 File Offset: 0x00019DF8
		public static MvcHtmlString RouteLink(this AjaxHelper ajaxHelper, string linkText, string routeName, RouteValueDictionary routeValues, AjaxOptions ajaxOptions, IDictionary<string, object> htmlAttributes)
		{
			if (string.IsNullOrEmpty(linkText))
			{
				throw new ArgumentException(MvcResources.Common_NullOrEmpty, "linkText");
			}
			string targetUrl = UrlHelper.GenerateUrl(routeName, null, null, routeValues ?? new RouteValueDictionary(), ajaxHelper.RouteCollection, ajaxHelper.ViewContext.RequestContext, false);
			return MvcHtmlString.Create(AjaxExtensions.GenerateLink(ajaxHelper, linkText, targetUrl, AjaxExtensions.GetAjaxOptions(ajaxOptions), htmlAttributes));
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x0001BC58 File Offset: 0x00019E58
		public static MvcHtmlString RouteLink(this AjaxHelper ajaxHelper, string linkText, string routeName, string protocol, string hostName, string fragment, RouteValueDictionary routeValues, AjaxOptions ajaxOptions, IDictionary<string, object> htmlAttributes)
		{
			if (string.IsNullOrEmpty(linkText))
			{
				throw new ArgumentException(MvcResources.Common_NullOrEmpty, "linkText");
			}
			string targetUrl = UrlHelper.GenerateUrl(routeName, null, null, protocol, hostName, fragment, routeValues ?? new RouteValueDictionary(), ajaxHelper.RouteCollection, ajaxHelper.ViewContext.RequestContext, false);
			return MvcHtmlString.Create(AjaxExtensions.GenerateLink(ajaxHelper, linkText, targetUrl, AjaxExtensions.GetAjaxOptions(ajaxOptions), htmlAttributes));
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x0001BCC0 File Offset: 0x00019EC0
		private static string GenerateLink(AjaxHelper ajaxHelper, string linkText, string targetUrl, AjaxOptions ajaxOptions, IDictionary<string, object> htmlAttributes)
		{
			TagBuilder tagBuilder = new TagBuilder("a")
			{
				InnerHtml = HttpUtility.HtmlEncode(linkText)
			};
			tagBuilder.MergeAttributes<string, object>(htmlAttributes);
			tagBuilder.MergeAttribute("href", targetUrl);
			if (ajaxHelper.ViewContext.UnobtrusiveJavaScriptEnabled)
			{
				tagBuilder.MergeAttributes<string, object>(ajaxOptions.ToUnobtrusiveHtmlAttributes());
			}
			else
			{
				tagBuilder.MergeAttribute("onclick", AjaxExtensions.GenerateAjaxScript(ajaxOptions, "Sys.Mvc.AsyncHyperlink.handleClick(this, new Sys.UI.DomEvent(event), {0});"));
			}
			return tagBuilder.ToString(TagRenderMode.Normal);
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x0001BD34 File Offset: 0x00019F34
		private static string GenerateAjaxScript(AjaxOptions ajaxOptions, string scriptFormat)
		{
			string text = ajaxOptions.ToJavascriptString();
			return string.Format(CultureInfo.InvariantCulture, scriptFormat, new object[]
			{
				text
			});
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x0001BD5F File Offset: 0x00019F5F
		private static AjaxOptions GetAjaxOptions(AjaxOptions ajaxOptions)
		{
			if (ajaxOptions == null)
			{
				return new AjaxOptions();
			}
			return ajaxOptions;
		}

		// Token: 0x040002AF RID: 687
		private const string LinkOnClickFormat = "Sys.Mvc.AsyncHyperlink.handleClick(this, new Sys.UI.DomEvent(event), {0});";

		// Token: 0x040002B0 RID: 688
		private const string FormOnClickValue = "Sys.Mvc.AsyncForm.handleClick(this, new Sys.UI.DomEvent(event));";

		// Token: 0x040002B1 RID: 689
		private const string FormOnSubmitFormat = "Sys.Mvc.AsyncForm.handleSubmit(this, new Sys.UI.DomEvent(event), {0});";
	}
}
