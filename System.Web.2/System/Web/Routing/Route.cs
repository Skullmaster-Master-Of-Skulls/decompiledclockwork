using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace System.Web.Routing
{
	// Token: 0x02000148 RID: 328
	[TypeForwardedFrom("System.Web.Routing, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public class Route : RouteBase
	{
		// Token: 0x0600131F RID: 4895 RVA: 0x00037637 File Offset: 0x00035837
		public Route(string url, IRouteHandler routeHandler)
		{
			this.Url = url;
			this.RouteHandler = routeHandler;
		}

		// Token: 0x06001320 RID: 4896 RVA: 0x0003764D File Offset: 0x0003584D
		public Route(string url, RouteValueDictionary defaults, IRouteHandler routeHandler)
		{
			this.Url = url;
			this.Defaults = defaults;
			this.RouteHandler = routeHandler;
		}

		// Token: 0x06001321 RID: 4897 RVA: 0x0003766A File Offset: 0x0003586A
		public Route(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, IRouteHandler routeHandler)
		{
			this.Url = url;
			this.Defaults = defaults;
			this.Constraints = constraints;
			this.RouteHandler = routeHandler;
		}

		// Token: 0x06001322 RID: 4898 RVA: 0x0003768F File Offset: 0x0003588F
		public Route(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, RouteValueDictionary dataTokens, IRouteHandler routeHandler)
		{
			this.Url = url;
			this.Defaults = defaults;
			this.Constraints = constraints;
			this.DataTokens = dataTokens;
			this.RouteHandler = routeHandler;
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x06001323 RID: 4899 RVA: 0x000376BC File Offset: 0x000358BC
		// (set) Token: 0x06001324 RID: 4900 RVA: 0x000376C4 File Offset: 0x000358C4
		public RouteValueDictionary Constraints { get; set; }

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x06001325 RID: 4901 RVA: 0x000376CD File Offset: 0x000358CD
		// (set) Token: 0x06001326 RID: 4902 RVA: 0x000376D5 File Offset: 0x000358D5
		public RouteValueDictionary DataTokens { get; set; }

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x06001327 RID: 4903 RVA: 0x000376DE File Offset: 0x000358DE
		// (set) Token: 0x06001328 RID: 4904 RVA: 0x000376E6 File Offset: 0x000358E6
		public RouteValueDictionary Defaults { get; set; }

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x06001329 RID: 4905 RVA: 0x000376EF File Offset: 0x000358EF
		// (set) Token: 0x0600132A RID: 4906 RVA: 0x000376F7 File Offset: 0x000358F7
		public IRouteHandler RouteHandler { get; set; }

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x0600132B RID: 4907 RVA: 0x00037700 File Offset: 0x00035900
		// (set) Token: 0x0600132C RID: 4908 RVA: 0x00037711 File Offset: 0x00035911
		public string Url
		{
			get
			{
				return this._url ?? string.Empty;
			}
			set
			{
				this._parsedRoute = RouteParser.Parse(value);
				this._url = value;
			}
		}

		// Token: 0x0600132D RID: 4909 RVA: 0x00037728 File Offset: 0x00035928
		public override RouteData GetRouteData(HttpContextBase httpContext)
		{
			string virtualPath = httpContext.Request.AppRelativeCurrentExecutionFilePath.Substring(2) + httpContext.Request.PathInfo;
			RouteValueDictionary routeValueDictionary = this._parsedRoute.Match(virtualPath, this.Defaults);
			if (routeValueDictionary == null)
			{
				return null;
			}
			RouteData routeData = new RouteData(this, this.RouteHandler);
			if (!this.ProcessConstraints(httpContext, routeValueDictionary, RouteDirection.IncomingRequest))
			{
				return null;
			}
			foreach (KeyValuePair<string, object> keyValuePair in routeValueDictionary)
			{
				routeData.Values.Add(keyValuePair.Key, keyValuePair.Value);
			}
			if (this.DataTokens != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair2 in this.DataTokens)
				{
					routeData.DataTokens[keyValuePair2.Key] = keyValuePair2.Value;
				}
			}
			return routeData;
		}

		// Token: 0x0600132E RID: 4910 RVA: 0x0003783C File Offset: 0x00035A3C
		public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
		{
			BoundUrl boundUrl = this._parsedRoute.Bind(requestContext.RouteData.Values, values, this.Defaults, this.Constraints);
			if (boundUrl == null)
			{
				return null;
			}
			if (!this.ProcessConstraints(requestContext.HttpContext, boundUrl.Values, RouteDirection.UrlGeneration))
			{
				return null;
			}
			VirtualPathData virtualPathData = new VirtualPathData(this, boundUrl.Url);
			if (this.DataTokens != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair in this.DataTokens)
				{
					virtualPathData.DataTokens[keyValuePair.Key] = keyValuePair.Value;
				}
			}
			return virtualPathData;
		}

		// Token: 0x0600132F RID: 4911 RVA: 0x000378F8 File Offset: 0x00035AF8
		protected virtual bool ProcessConstraint(HttpContextBase httpContext, object constraint, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
		{
			IRouteConstraint routeConstraint = constraint as IRouteConstraint;
			if (routeConstraint != null)
			{
				return routeConstraint.Match(httpContext, this, parameterName, values, routeDirection);
			}
			string text = constraint as string;
			if (text == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentUICulture, SR.GetString("Route_ValidationMustBeStringOrCustomConstraint"), new object[]
				{
					parameterName,
					this.Url
				}));
			}
			object value;
			values.TryGetValue(parameterName, out value);
			string input = Convert.ToString(value, CultureInfo.InvariantCulture);
			string pattern = "^(" + text + ")$";
			return Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
		}

		// Token: 0x06001330 RID: 4912 RVA: 0x0003798C File Offset: 0x00035B8C
		private bool ProcessConstraints(HttpContextBase httpContext, RouteValueDictionary values, RouteDirection routeDirection)
		{
			if (this.Constraints != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair in this.Constraints)
				{
					if (!this.ProcessConstraint(httpContext, keyValuePair.Value, keyValuePair.Key, values, routeDirection))
					{
						return false;
					}
				}
				return true;
			}
			return true;
		}

		// Token: 0x040014C9 RID: 5321
		private const string HttpMethodParameterName = "httpMethod";

		// Token: 0x040014CA RID: 5322
		private string _url;

		// Token: 0x040014CB RID: 5323
		private ParsedRoute _parsedRoute;
	}
}
