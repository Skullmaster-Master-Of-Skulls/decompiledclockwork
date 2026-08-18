using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.Mvc.Properties;
using System.Web.Mvc.Routing;
using System.Web.Routing;
using System.Web.WebPages;

namespace System.Web.Mvc.Html
{
	// Token: 0x02000129 RID: 297
	public static class ChildActionExtensions
	{
		// Token: 0x060007C7 RID: 1991 RVA: 0x00014F76 File Offset: 0x00013176
		public static MvcHtmlString Action(this HtmlHelper htmlHelper, string actionName)
		{
			return htmlHelper.Action(actionName, null, null);
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x00014F81 File Offset: 0x00013181
		public static MvcHtmlString Action(this HtmlHelper htmlHelper, string actionName, object routeValues)
		{
			return htmlHelper.Action(actionName, null, TypeHelper.ObjectToDictionary(routeValues));
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x00014F91 File Offset: 0x00013191
		public static MvcHtmlString Action(this HtmlHelper htmlHelper, string actionName, RouteValueDictionary routeValues)
		{
			return htmlHelper.Action(actionName, null, routeValues);
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x00014F9C File Offset: 0x0001319C
		public static MvcHtmlString Action(this HtmlHelper htmlHelper, string actionName, string controllerName)
		{
			return htmlHelper.Action(actionName, controllerName, null);
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x00014FA7 File Offset: 0x000131A7
		public static MvcHtmlString Action(this HtmlHelper htmlHelper, string actionName, string controllerName, object routeValues)
		{
			return htmlHelper.Action(actionName, controllerName, TypeHelper.ObjectToDictionary(routeValues));
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x00014FB8 File Offset: 0x000131B8
		public static MvcHtmlString Action(this HtmlHelper htmlHelper, string actionName, string controllerName, RouteValueDictionary routeValues)
		{
			MvcHtmlString result;
			using (StringWriter stringWriter = new StringWriter(CultureInfo.CurrentCulture))
			{
				ChildActionExtensions.ActionHelper(htmlHelper, actionName, controllerName, routeValues, stringWriter);
				result = MvcHtmlString.Create(stringWriter.ToString());
			}
			return result;
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x00015004 File Offset: 0x00013204
		public static void RenderAction(this HtmlHelper htmlHelper, string actionName)
		{
			htmlHelper.RenderAction(actionName, null, null);
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x0001500F File Offset: 0x0001320F
		public static void RenderAction(this HtmlHelper htmlHelper, string actionName, object routeValues)
		{
			htmlHelper.RenderAction(actionName, null, TypeHelper.ObjectToDictionary(routeValues));
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x0001501F File Offset: 0x0001321F
		public static void RenderAction(this HtmlHelper htmlHelper, string actionName, RouteValueDictionary routeValues)
		{
			htmlHelper.RenderAction(actionName, null, routeValues);
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x0001502A File Offset: 0x0001322A
		public static void RenderAction(this HtmlHelper htmlHelper, string actionName, string controllerName)
		{
			htmlHelper.RenderAction(actionName, controllerName, null);
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x00015035 File Offset: 0x00013235
		public static void RenderAction(this HtmlHelper htmlHelper, string actionName, string controllerName, object routeValues)
		{
			htmlHelper.RenderAction(actionName, controllerName, TypeHelper.ObjectToDictionary(routeValues));
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x00015045 File Offset: 0x00013245
		public static void RenderAction(this HtmlHelper htmlHelper, string actionName, string controllerName, RouteValueDictionary routeValues)
		{
			ChildActionExtensions.ActionHelper(htmlHelper, actionName, controllerName, routeValues, htmlHelper.ViewContext.Writer);
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x0001505C File Offset: 0x0001325C
		internal static void ActionHelper(HtmlHelper htmlHelper, string actionName, string controllerName, RouteValueDictionary routeValues, TextWriter textWriter)
		{
			if (htmlHelper == null)
			{
				throw new ArgumentNullException("htmlHelper");
			}
			if (string.IsNullOrEmpty(actionName))
			{
				throw new ArgumentException(MvcResources.Common_NullOrEmpty, "actionName");
			}
			RouteValueDictionary routeValueDictionary = routeValues;
			routeValues = ChildActionExtensions.MergeDictionaries(new RouteValueDictionary[]
			{
				routeValues,
				htmlHelper.ViewContext.RouteData.Values
			});
			routeValues["action"] = actionName;
			if (!string.IsNullOrEmpty(controllerName))
			{
				routeValues["controller"] = controllerName;
			}
			bool flag;
			VirtualPathData virtualPathForArea = htmlHelper.RouteCollection.GetVirtualPathForArea(htmlHelper.ViewContext.RequestContext, null, routeValues, out flag);
			if (virtualPathForArea == null)
			{
				throw new InvalidOperationException(MvcResources.Common_NoRouteMatched);
			}
			if (flag)
			{
				routeValues.Remove("area");
				if (routeValueDictionary != null)
				{
					routeValueDictionary.Remove("area");
				}
			}
			if (routeValueDictionary != null)
			{
				routeValues[ChildActionValueProvider.ChildActionValuesKey] = new DictionaryValueProvider<object>(routeValueDictionary, CultureInfo.InvariantCulture);
			}
			RouteData routeData = ChildActionExtensions.CreateRouteData(virtualPathForArea.Route, routeValues, virtualPathForArea.DataTokens, htmlHelper.ViewContext);
			HttpContextBase httpContext = htmlHelper.ViewContext.HttpContext;
			RequestContext context = new RequestContext(httpContext, routeData);
			ChildActionExtensions.ChildActionMvcHandler httpHandler = new ChildActionExtensions.ChildActionMvcHandler(context);
			httpContext.Server.Execute(HttpHandlerUtil.WrapForServerExecute(httpHandler), textWriter, true);
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x0001518C File Offset: 0x0001338C
		private static RouteData CreateRouteData(RouteBase route, RouteValueDictionary routeValues, RouteValueDictionary dataTokens, ViewContext parentViewContext)
		{
			RouteData routeData = new RouteData();
			foreach (KeyValuePair<string, object> keyValuePair in routeValues)
			{
				routeData.Values.Add(keyValuePair.Key, keyValuePair.Value);
			}
			foreach (KeyValuePair<string, object> keyValuePair2 in dataTokens)
			{
				routeData.DataTokens.Add(keyValuePair2.Key, keyValuePair2.Value);
			}
			routeData.Route = route;
			routeData.DataTokens["ParentActionViewContext"] = parentViewContext;
			if (route.IsDirectRoute())
			{
				RouteData routeData2 = RouteCollectionRoute.CreateDirectRouteMatch(route, new List<RouteData>
				{
					routeData
				});
				routeData2.DataTokens["ParentActionViewContext"] = parentViewContext;
				return routeData2;
			}
			return routeData;
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x0001529C File Offset: 0x0001349C
		private static RouteValueDictionary MergeDictionaries(params RouteValueDictionary[] dictionaries)
		{
			RouteValueDictionary routeValueDictionary = new RouteValueDictionary();
			foreach (RouteValueDictionary routeValueDictionary2 in from d in dictionaries
			where d != null
			select d)
			{
				foreach (KeyValuePair<string, object> keyValuePair in routeValueDictionary2)
				{
					if (!routeValueDictionary.ContainsKey(keyValuePair.Key))
					{
						routeValueDictionary.Add(keyValuePair.Key, keyValuePair.Value);
					}
				}
			}
			return routeValueDictionary;
		}

		// Token: 0x0200012C RID: 300
		internal class ChildActionMvcHandler : MvcHandler
		{
			// Token: 0x060007F1 RID: 2033 RVA: 0x000157A1 File Offset: 0x000139A1
			public ChildActionMvcHandler(RequestContext context) : base(context)
			{
			}

			// Token: 0x060007F2 RID: 2034 RVA: 0x000157AA File Offset: 0x000139AA
			protected internal override void AddVersionHeader(HttpContextBase httpContext)
			{
			}
		}
	}
}
