using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.ExceptionServices;
using System.Web.Http.Routing;
using System.Web.Http.WebHost.Properties;
using System.Web.Routing;

namespace System.Web.Http.WebHost.Routing
{
	// Token: 0x02000020 RID: 32
	internal class HttpWebRoute : Route
	{
		// Token: 0x060000D8 RID: 216 RVA: 0x00004A5F File Offset: 0x00002C5F
		public HttpWebRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, RouteValueDictionary dataTokens, IRouteHandler routeHandler, IHttpRoute httpRoute) : base(url, defaults, constraints, dataTokens, routeHandler)
		{
			if (httpRoute == null)
			{
				throw Error.ArgumentNull("httpRoute");
			}
			this.HttpRoute = httpRoute;
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00004A85 File Offset: 0x00002C85
		// (set) Token: 0x060000DA RID: 218 RVA: 0x00004A8D File Offset: 0x00002C8D
		public IHttpRoute HttpRoute { get; private set; }

		// Token: 0x060000DB RID: 219 RVA: 0x00004A98 File Offset: 0x00002C98
		protected override bool ProcessConstraint(HttpContextBase httpContext, object constraint, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
		{
			HttpWebRoute.ValidateConstraint(this.HttpRoute.RouteTemplate, parameterName, constraint);
			IHttpRouteConstraint httpRouteConstraint = constraint as IHttpRouteConstraint;
			if (httpRouteConstraint != null)
			{
				HttpRequestMessage orCreateHttpRequestMessage = httpContext.GetOrCreateHttpRequestMessage();
				return httpRouteConstraint.Match(orCreateHttpRequestMessage, this.HttpRoute, parameterName, values, HttpWebRoute.ConvertRouteDirection(routeDirection));
			}
			return base.ProcessConstraint(httpContext, constraint, parameterName, values, routeDirection);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00004AF0 File Offset: 0x00002CF0
		public override RouteData GetRouteData(HttpContextBase httpContext)
		{
			RouteData result;
			try
			{
				if (this.HttpRoute is HostedHttpRoute)
				{
					result = base.GetRouteData(httpContext);
				}
				else
				{
					HttpRequestMessage orCreateHttpRequestMessage = httpContext.GetOrCreateHttpRequestMessage();
					IHttpRouteData routeData = this.HttpRoute.GetRouteData(httpContext.Request.ApplicationPath, orCreateHttpRequestMessage);
					result = ((routeData == null) ? null : routeData.ToRouteData());
				}
			}
			catch (Exception source)
			{
				ExceptionDispatchInfo exceptionInfo = ExceptionDispatchInfo.Capture(source);
				result = new RouteData(this, new HttpRouteExceptionRouteHandler(exceptionInfo));
			}
			return result;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00004B70 File Offset: 0x00002D70
		public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
		{
			if (!values.ContainsKey("httproute"))
			{
				return null;
			}
			RouteValueDictionary routeDictionaryWithoutHttpRouteKey = HttpWebRoute.GetRouteDictionaryWithoutHttpRouteKey(values);
			if (this.HttpRoute is HostedHttpRoute)
			{
				return base.GetVirtualPath(requestContext, routeDictionaryWithoutHttpRouteKey);
			}
			HttpRequestMessage orCreateHttpRequestMessage = requestContext.HttpContext.GetOrCreateHttpRequestMessage();
			IHttpVirtualPathData virtualPath = this.HttpRoute.GetVirtualPath(orCreateHttpRequestMessage, values);
			if (virtualPath != null)
			{
				return new VirtualPathData(this, virtualPath.VirtualPath);
			}
			return null;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00004BD4 File Offset: 0x00002DD4
		private static RouteValueDictionary GetRouteDictionaryWithoutHttpRouteKey(IDictionary<string, object> routeValues)
		{
			RouteValueDictionary routeValueDictionary = new RouteValueDictionary();
			foreach (KeyValuePair<string, object> keyValuePair in routeValues)
			{
				if (!string.Equals(keyValuePair.Key, "httproute", StringComparison.OrdinalIgnoreCase))
				{
					routeValueDictionary.Add(keyValuePair.Key, keyValuePair.Value);
				}
			}
			return routeValueDictionary;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00004C44 File Offset: 0x00002E44
		private static HttpRouteDirection ConvertRouteDirection(RouteDirection routeDirection)
		{
			if (routeDirection == RouteDirection.IncomingRequest)
			{
				return HttpRouteDirection.UriResolution;
			}
			if (routeDirection == RouteDirection.UrlGeneration)
			{
				return HttpRouteDirection.UriGeneration;
			}
			throw Error.InvalidEnumArgument("routeDirection", (int)routeDirection, typeof(RouteDirection));
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00004C66 File Offset: 0x00002E66
		internal static void ValidateConstraint(string routeTemplate, string name, object constraint)
		{
			if (constraint is IHttpRouteConstraint)
			{
				return;
			}
			if (constraint is IRouteConstraint)
			{
				return;
			}
			if (constraint is string)
			{
				return;
			}
			throw HttpWebRoute.CreateInvalidConstraintTypeException(routeTemplate, name);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00004C8C File Offset: 0x00002E8C
		private static Exception CreateInvalidConstraintTypeException(string routeTemplate, string name)
		{
			return Error.InvalidOperation(SRResources.Route_ValidationMustBeStringOrCustomConstraint, new object[]
			{
				name,
				routeTemplate,
				typeof(IHttpRouteConstraint).FullName,
				typeof(IRouteConstraint).FullName
			});
		}

		// Token: 0x04000036 RID: 54
		internal const string HttpRouteKey = "httproute";
	}
}
