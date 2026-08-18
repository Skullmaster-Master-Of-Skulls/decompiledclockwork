using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;

namespace System.Web.Routing
{
	// Token: 0x0200013F RID: 319
	[TypeForwardedFrom("System.Web.Routing, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public class HttpMethodConstraint : IRouteConstraint
	{
		// Token: 0x060012FA RID: 4858 RVA: 0x00036887 File Offset: 0x00034A87
		public HttpMethodConstraint(params string[] allowedMethods)
		{
			if (allowedMethods == null)
			{
				throw new ArgumentNullException("allowedMethods");
			}
			this.AllowedMethods = allowedMethods.ToList<string>().AsReadOnly();
		}

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x060012FB RID: 4859 RVA: 0x000368AE File Offset: 0x00034AAE
		// (set) Token: 0x060012FC RID: 4860 RVA: 0x000368B6 File Offset: 0x00034AB6
		public ICollection<string> AllowedMethods { get; private set; }

		// Token: 0x060012FD RID: 4861 RVA: 0x000368C0 File Offset: 0x00034AC0
		protected virtual bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
		{
			if (httpContext == null)
			{
				throw new ArgumentNullException("httpContext");
			}
			if (route == null)
			{
				throw new ArgumentNullException("route");
			}
			if (parameterName == null)
			{
				throw new ArgumentNullException("parameterName");
			}
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			if (routeDirection == RouteDirection.IncomingRequest)
			{
				return this.AllowedMethods.Any((string method) => string.Equals(method, httpContext.Request.HttpMethod, StringComparison.OrdinalIgnoreCase));
			}
			if (routeDirection != RouteDirection.UrlGeneration)
			{
				return true;
			}
			object obj;
			if (!values.TryGetValue(parameterName, out obj))
			{
				return true;
			}
			string parameterValueString = obj as string;
			if (parameterValueString == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentUICulture, SR.GetString("HttpMethodConstraint_ParameterValueMustBeString"), new object[]
				{
					parameterName,
					route.Url
				}));
			}
			return this.AllowedMethods.Any((string method) => string.Equals(method, parameterValueString, StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x060012FE RID: 4862 RVA: 0x000369A9 File Offset: 0x00034BA9
		bool IRouteConstraint.Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
		{
			return this.Match(httpContext, route, parameterName, values, routeDirection);
		}
	}
}
