using System;
using System.Collections.Generic;
using System.Web.Routing;
using System.Web.WebPages;

namespace System.Web.Mvc.Html
{
	// Token: 0x020001B8 RID: 440
	public static class FormExtensions
	{
		// Token: 0x06000C6A RID: 3178 RVA: 0x00020E20 File Offset: 0x0001F020
		public static MvcForm BeginForm(this HtmlHelper htmlHelper)
		{
			string rawUrl = htmlHelper.ViewContext.HttpContext.Request.RawUrl;
			return htmlHelper.FormHelper(rawUrl, FormMethod.Post, new RouteValueDictionary());
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x00020E50 File Offset: 0x0001F050
		public static MvcForm BeginForm(this HtmlHelper htmlHelper, object routeValues)
		{
			return htmlHelper.BeginForm(null, null, TypeHelper.ObjectToDictionary(routeValues), FormMethod.Post, new RouteValueDictionary());
		}

		// Token: 0x06000C6C RID: 3180 RVA: 0x00020E66 File Offset: 0x0001F066
		public static MvcForm BeginForm(this HtmlHelper htmlHelper, RouteValueDictionary routeValues)
		{
			return htmlHelper.BeginForm(null, null, routeValues, FormMethod.Post, new RouteValueDictionary());
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x00020E77 File Offset: 0x0001F077
		public static MvcForm BeginForm(this HtmlHelper htmlHelper, string actionName, string controllerName)
		{
			return htmlHelper.BeginForm(actionName, controllerName, new RouteValueDictionary(), FormMethod.Post, new RouteValueDictionary());
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x00020E8C File Offset: 0x0001F08C
		public static MvcForm BeginForm(this HtmlHelper htmlHelper, string actionName, string controllerName, object routeValues)
		{
			return htmlHelper.BeginForm(actionName, controllerName, TypeHelper.ObjectToDictionary(routeValues), FormMethod.Post, new RouteValueDictionary());
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x00020EA2 File Offset: 0x0001F0A2
		public static MvcForm BeginForm(this HtmlHelper htmlHelper, string actionName, string controllerName, RouteValueDictionary routeValues)
		{
			return htmlHelper.BeginForm(actionName, controllerName, routeValues, FormMethod.Post, new RouteValueDictionary());
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x00020EB3 File Offset: 0x0001F0B3
		public static MvcForm BeginForm(this HtmlHelper htmlHelper, string actionName, string controllerName, FormMethod method)
		{
			return htmlHelper.BeginForm(actionName, controllerName, new RouteValueDictionary(), method, new RouteValueDictionary());
		}

		// Token: 0x06000C71 RID: 3185 RVA: 0x00020EC8 File Offset: 0x0001F0C8
		public static MvcForm BeginForm(this HtmlHelper htmlHelper, string actionName, string controllerName, object routeValues, FormMethod method)
		{
			return htmlHelper.BeginForm(actionName, controllerName, TypeHelper.ObjectToDictionary(routeValues), method, new RouteValueDictionary());
		}

		// Token: 0x06000C72 RID: 3186 RVA: 0x00020EDF File Offset: 0x0001F0DF
		public static MvcForm BeginForm(this HtmlHelper htmlHelper, string actionName, string controllerName, RouteValueDictionary routeValues, FormMethod method)
		{
			return htmlHelper.BeginForm(actionName, controllerName, routeValues, method, new RouteValueDictionary());
		}

		// Token: 0x06000C73 RID: 3187 RVA: 0x00020EF1 File Offset: 0x0001F0F1
		public static MvcForm BeginForm(this HtmlHelper htmlHelper, string actionName, string controllerName, FormMethod method, object htmlAttributes)
		{
			return htmlHelper.BeginForm(actionName, controllerName, new RouteValueDictionary(), method, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		// Token: 0x06000C74 RID: 3188 RVA: 0x00020F08 File Offset: 0x0001F108
		public static MvcForm BeginForm(this HtmlHelper htmlHelper, string actionName, string controllerName, FormMethod method, IDictionary<string, object> htmlAttributes)
		{
			return htmlHelper.BeginForm(actionName, controllerName, new RouteValueDictionary(), method, htmlAttributes);
		}

		// Token: 0x06000C75 RID: 3189 RVA: 0x00020F1A File Offset: 0x0001F11A
		public static MvcForm BeginForm(this HtmlHelper htmlHelper, string actionName, string controllerName, object routeValues, FormMethod method, object htmlAttributes)
		{
			return htmlHelper.BeginForm(actionName, controllerName, TypeHelper.ObjectToDictionary(routeValues), method, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x00020F34 File Offset: 0x0001F134
		public static MvcForm BeginForm(this HtmlHelper htmlHelper, string actionName, string controllerName, RouteValueDictionary routeValues, FormMethod method, IDictionary<string, object> htmlAttributes)
		{
			string formAction = UrlHelper.GenerateUrl(null, actionName, controllerName, routeValues, htmlHelper.RouteCollection, htmlHelper.ViewContext.RequestContext, true);
			return htmlHelper.FormHelper(formAction, method, htmlAttributes);
		}

		// Token: 0x06000C77 RID: 3191 RVA: 0x00020F68 File Offset: 0x0001F168
		public static MvcForm BeginRouteForm(this HtmlHelper htmlHelper, object routeValues)
		{
			return htmlHelper.BeginRouteForm(null, TypeHelper.ObjectToDictionary(routeValues), FormMethod.Post, new RouteValueDictionary());
		}

		// Token: 0x06000C78 RID: 3192 RVA: 0x00020F7D File Offset: 0x0001F17D
		public static MvcForm BeginRouteForm(this HtmlHelper htmlHelper, RouteValueDictionary routeValues)
		{
			return htmlHelper.BeginRouteForm(null, routeValues, FormMethod.Post, new RouteValueDictionary());
		}

		// Token: 0x06000C79 RID: 3193 RVA: 0x00020F8D File Offset: 0x0001F18D
		public static MvcForm BeginRouteForm(this HtmlHelper htmlHelper, string routeName)
		{
			return htmlHelper.BeginRouteForm(routeName, new RouteValueDictionary(), FormMethod.Post, new RouteValueDictionary());
		}

		// Token: 0x06000C7A RID: 3194 RVA: 0x00020FA1 File Offset: 0x0001F1A1
		public static MvcForm BeginRouteForm(this HtmlHelper htmlHelper, string routeName, object routeValues)
		{
			return htmlHelper.BeginRouteForm(routeName, TypeHelper.ObjectToDictionary(routeValues), FormMethod.Post, new RouteValueDictionary());
		}

		// Token: 0x06000C7B RID: 3195 RVA: 0x00020FB6 File Offset: 0x0001F1B6
		public static MvcForm BeginRouteForm(this HtmlHelper htmlHelper, string routeName, RouteValueDictionary routeValues)
		{
			return htmlHelper.BeginRouteForm(routeName, routeValues, FormMethod.Post, new RouteValueDictionary());
		}

		// Token: 0x06000C7C RID: 3196 RVA: 0x00020FC6 File Offset: 0x0001F1C6
		public static MvcForm BeginRouteForm(this HtmlHelper htmlHelper, string routeName, FormMethod method)
		{
			return htmlHelper.BeginRouteForm(routeName, new RouteValueDictionary(), method, new RouteValueDictionary());
		}

		// Token: 0x06000C7D RID: 3197 RVA: 0x00020FDA File Offset: 0x0001F1DA
		public static MvcForm BeginRouteForm(this HtmlHelper htmlHelper, string routeName, object routeValues, FormMethod method)
		{
			return htmlHelper.BeginRouteForm(routeName, TypeHelper.ObjectToDictionary(routeValues), method, new RouteValueDictionary());
		}

		// Token: 0x06000C7E RID: 3198 RVA: 0x00020FEF File Offset: 0x0001F1EF
		public static MvcForm BeginRouteForm(this HtmlHelper htmlHelper, string routeName, RouteValueDictionary routeValues, FormMethod method)
		{
			return htmlHelper.BeginRouteForm(routeName, routeValues, method, new RouteValueDictionary());
		}

		// Token: 0x06000C7F RID: 3199 RVA: 0x00020FFF File Offset: 0x0001F1FF
		public static MvcForm BeginRouteForm(this HtmlHelper htmlHelper, string routeName, FormMethod method, object htmlAttributes)
		{
			return htmlHelper.BeginRouteForm(routeName, new RouteValueDictionary(), method, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		// Token: 0x06000C80 RID: 3200 RVA: 0x00021014 File Offset: 0x0001F214
		public static MvcForm BeginRouteForm(this HtmlHelper htmlHelper, string routeName, FormMethod method, IDictionary<string, object> htmlAttributes)
		{
			return htmlHelper.BeginRouteForm(routeName, new RouteValueDictionary(), method, htmlAttributes);
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x00021024 File Offset: 0x0001F224
		public static MvcForm BeginRouteForm(this HtmlHelper htmlHelper, string routeName, object routeValues, FormMethod method, object htmlAttributes)
		{
			return htmlHelper.BeginRouteForm(routeName, TypeHelper.ObjectToDictionary(routeValues), method, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		// Token: 0x06000C82 RID: 3202 RVA: 0x0002103C File Offset: 0x0001F23C
		public static MvcForm BeginRouteForm(this HtmlHelper htmlHelper, string routeName, RouteValueDictionary routeValues, FormMethod method, IDictionary<string, object> htmlAttributes)
		{
			string formAction = UrlHelper.GenerateUrl(routeName, null, null, routeValues, htmlHelper.RouteCollection, htmlHelper.ViewContext.RequestContext, false);
			return htmlHelper.FormHelper(formAction, method, htmlAttributes);
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x0002106F File Offset: 0x0001F26F
		public static void EndForm(this HtmlHelper htmlHelper)
		{
			FormExtensions.EndForm(htmlHelper.ViewContext);
		}

		// Token: 0x06000C84 RID: 3204 RVA: 0x0002107C File Offset: 0x0001F27C
		internal static void EndForm(ViewContext viewContext)
		{
			viewContext.Writer.Write("</form>");
			viewContext.OutputClientValidation();
			viewContext.FormContext = null;
		}

		// Token: 0x06000C85 RID: 3205 RVA: 0x0002109C File Offset: 0x0001F29C
		private static MvcForm FormHelper(this HtmlHelper htmlHelper, string formAction, FormMethod method, IDictionary<string, object> htmlAttributes)
		{
			TagBuilder tagBuilder = new TagBuilder("form");
			tagBuilder.MergeAttributes<string, object>(htmlAttributes);
			tagBuilder.MergeAttribute("action", formAction);
			tagBuilder.MergeAttribute("method", HtmlHelper.GetFormMethodString(method), true);
			bool flag = htmlHelper.ViewContext.ClientValidationEnabled && !htmlHelper.ViewContext.UnobtrusiveJavaScriptEnabled;
			if (flag)
			{
				tagBuilder.GenerateId(htmlHelper.ViewContext.FormIdGenerator());
			}
			htmlHelper.ViewContext.Writer.Write(tagBuilder.ToString(TagRenderMode.StartTag));
			MvcForm result = new MvcForm(htmlHelper.ViewContext);
			if (flag)
			{
				htmlHelper.ViewContext.FormContext.FormId = tagBuilder.Attributes["id"];
			}
			return result;
		}
	}
}
