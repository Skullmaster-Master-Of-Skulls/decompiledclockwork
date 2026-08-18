using System;
using System.Collections.Generic;
using System.Web.Mvc.Properties;
using System.Web.Routing;
using System.Web.WebPages;

namespace System.Web.Mvc.Html
{
	// Token: 0x020001BE RID: 446
	public static class LinkExtensions
	{
		// Token: 0x06000D21 RID: 3361 RVA: 0x00022F7C File Offset: 0x0002117C
		public static MvcHtmlString ActionLink(this HtmlHelper htmlHelper, string linkText, string actionName)
		{
			return htmlHelper.ActionLink(linkText, actionName, null, new RouteValueDictionary(), new RouteValueDictionary());
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x00022F91 File Offset: 0x00021191
		public static MvcHtmlString ActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, object routeValues)
		{
			return htmlHelper.ActionLink(linkText, actionName, null, TypeHelper.ObjectToDictionary(routeValues), new RouteValueDictionary());
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x00022FA7 File Offset: 0x000211A7
		public static MvcHtmlString ActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, object routeValues, object htmlAttributes)
		{
			return htmlHelper.ActionLink(linkText, actionName, null, TypeHelper.ObjectToDictionary(routeValues), HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x00022FBF File Offset: 0x000211BF
		public static MvcHtmlString ActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, RouteValueDictionary routeValues)
		{
			return htmlHelper.ActionLink(linkText, actionName, null, routeValues, new RouteValueDictionary());
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x00022FD0 File Offset: 0x000211D0
		public static MvcHtmlString ActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
		{
			return htmlHelper.ActionLink(linkText, actionName, null, routeValues, htmlAttributes);
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x00022FDE File Offset: 0x000211DE
		public static MvcHtmlString ActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName)
		{
			return htmlHelper.ActionLink(linkText, actionName, controllerName, new RouteValueDictionary(), new RouteValueDictionary());
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x00022FF3 File Offset: 0x000211F3
		public static MvcHtmlString ActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, object routeValues, object htmlAttributes)
		{
			return htmlHelper.ActionLink(linkText, actionName, controllerName, TypeHelper.ObjectToDictionary(routeValues), HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x0002300C File Offset: 0x0002120C
		public static MvcHtmlString ActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
		{
			if (string.IsNullOrEmpty(linkText))
			{
				throw new ArgumentException(MvcResources.Common_NullOrEmpty, "linkText");
			}
			return MvcHtmlString.Create(HtmlHelper.GenerateLink(htmlHelper.ViewContext.RequestContext, htmlHelper.RouteCollection, linkText, null, actionName, controllerName, routeValues, htmlAttributes));
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x0002304C File Offset: 0x0002124C
		public static MvcHtmlString ActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, string protocol, string hostName, string fragment, object routeValues, object htmlAttributes)
		{
			return htmlHelper.ActionLink(linkText, actionName, controllerName, protocol, hostName, fragment, TypeHelper.ObjectToDictionary(routeValues), HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		// Token: 0x06000D2A RID: 3370 RVA: 0x00023078 File Offset: 0x00021278
		public static MvcHtmlString ActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, string protocol, string hostName, string fragment, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
		{
			if (string.IsNullOrEmpty(linkText))
			{
				throw new ArgumentException(MvcResources.Common_NullOrEmpty, "linkText");
			}
			return MvcHtmlString.Create(HtmlHelper.GenerateLink(htmlHelper.ViewContext.RequestContext, htmlHelper.RouteCollection, linkText, null, actionName, controllerName, protocol, hostName, fragment, routeValues, htmlAttributes));
		}

		// Token: 0x06000D2B RID: 3371 RVA: 0x000230C6 File Offset: 0x000212C6
		public static MvcHtmlString RouteLink(this HtmlHelper htmlHelper, string linkText, object routeValues)
		{
			return htmlHelper.RouteLink(linkText, TypeHelper.ObjectToDictionary(routeValues));
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x000230D5 File Offset: 0x000212D5
		public static MvcHtmlString RouteLink(this HtmlHelper htmlHelper, string linkText, RouteValueDictionary routeValues)
		{
			return htmlHelper.RouteLink(linkText, routeValues, new RouteValueDictionary());
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x000230E4 File Offset: 0x000212E4
		public static MvcHtmlString RouteLink(this HtmlHelper htmlHelper, string linkText, string routeName)
		{
			return htmlHelper.RouteLink(linkText, routeName, null);
		}

		// Token: 0x06000D2E RID: 3374 RVA: 0x000230EF File Offset: 0x000212EF
		public static MvcHtmlString RouteLink(this HtmlHelper htmlHelper, string linkText, string routeName, object routeValues)
		{
			return htmlHelper.RouteLink(linkText, routeName, TypeHelper.ObjectToDictionary(routeValues));
		}

		// Token: 0x06000D2F RID: 3375 RVA: 0x000230FF File Offset: 0x000212FF
		public static MvcHtmlString RouteLink(this HtmlHelper htmlHelper, string linkText, string routeName, RouteValueDictionary routeValues)
		{
			return htmlHelper.RouteLink(linkText, routeName, routeValues, new RouteValueDictionary());
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x0002310F File Offset: 0x0002130F
		public static MvcHtmlString RouteLink(this HtmlHelper htmlHelper, string linkText, object routeValues, object htmlAttributes)
		{
			return htmlHelper.RouteLink(linkText, TypeHelper.ObjectToDictionary(routeValues), HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x00023124 File Offset: 0x00021324
		public static MvcHtmlString RouteLink(this HtmlHelper htmlHelper, string linkText, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
		{
			return htmlHelper.RouteLink(linkText, null, routeValues, htmlAttributes);
		}

		// Token: 0x06000D32 RID: 3378 RVA: 0x00023130 File Offset: 0x00021330
		public static MvcHtmlString RouteLink(this HtmlHelper htmlHelper, string linkText, string routeName, object routeValues, object htmlAttributes)
		{
			return htmlHelper.RouteLink(linkText, routeName, TypeHelper.ObjectToDictionary(routeValues), HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x00023147 File Offset: 0x00021347
		public static MvcHtmlString RouteLink(this HtmlHelper htmlHelper, string linkText, string routeName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
		{
			if (string.IsNullOrEmpty(linkText))
			{
				throw new ArgumentException(MvcResources.Common_NullOrEmpty, "linkText");
			}
			return MvcHtmlString.Create(HtmlHelper.GenerateRouteLink(htmlHelper.ViewContext.RequestContext, htmlHelper.RouteCollection, linkText, routeName, routeValues, htmlAttributes));
		}

		// Token: 0x06000D34 RID: 3380 RVA: 0x00023181 File Offset: 0x00021381
		public static MvcHtmlString RouteLink(this HtmlHelper htmlHelper, string linkText, string routeName, string protocol, string hostName, string fragment, object routeValues, object htmlAttributes)
		{
			return htmlHelper.RouteLink(linkText, routeName, protocol, hostName, fragment, TypeHelper.ObjectToDictionary(routeValues), HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		// Token: 0x06000D35 RID: 3381 RVA: 0x000231A0 File Offset: 0x000213A0
		public static MvcHtmlString RouteLink(this HtmlHelper htmlHelper, string linkText, string routeName, string protocol, string hostName, string fragment, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
		{
			if (string.IsNullOrEmpty(linkText))
			{
				throw new ArgumentException(MvcResources.Common_NullOrEmpty, "linkText");
			}
			return MvcHtmlString.Create(HtmlHelper.GenerateRouteLink(htmlHelper.ViewContext.RequestContext, htmlHelper.RouteCollection, linkText, routeName, protocol, hostName, fragment, routeValues, htmlAttributes));
		}
	}
}
