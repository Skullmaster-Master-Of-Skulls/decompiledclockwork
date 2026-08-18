using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http.Properties;

namespace System.Web.Http.Routing
{
	// Token: 0x02000100 RID: 256
	public class HttpRoute : IHttpRoute
	{
		// Token: 0x06000635 RID: 1589 RVA: 0x0001484E File Offset: 0x00012A4E
		public HttpRoute() : this(null, null, null, null, null, null)
		{
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x0001485C File Offset: 0x00012A5C
		public HttpRoute(string routeTemplate) : this(routeTemplate, null, null, null, null, null)
		{
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x0001486A File Offset: 0x00012A6A
		public HttpRoute(string routeTemplate, HttpRouteValueDictionary defaults) : this(routeTemplate, defaults, null, null, null, null)
		{
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x00014878 File Offset: 0x00012A78
		public HttpRoute(string routeTemplate, HttpRouteValueDictionary defaults, HttpRouteValueDictionary constraints) : this(routeTemplate, defaults, constraints, null, null, null)
		{
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x00014886 File Offset: 0x00012A86
		public HttpRoute(string routeTemplate, HttpRouteValueDictionary defaults, HttpRouteValueDictionary constraints, HttpRouteValueDictionary dataTokens) : this(routeTemplate, defaults, constraints, dataTokens, null, null)
		{
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x00014895 File Offset: 0x00012A95
		public HttpRoute(string routeTemplate, HttpRouteValueDictionary defaults, HttpRouteValueDictionary constraints, HttpRouteValueDictionary dataTokens, HttpMessageHandler handler) : this(routeTemplate, defaults, constraints, dataTokens, handler, null)
		{
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x000148A8 File Offset: 0x00012AA8
		internal HttpRoute(string routeTemplate, HttpRouteValueDictionary defaults, HttpRouteValueDictionary constraints, HttpRouteValueDictionary dataTokens, HttpMessageHandler handler, HttpParsedRoute parsedRoute)
		{
			this._routeTemplate = ((routeTemplate == null) ? string.Empty : routeTemplate);
			this._defaults = (defaults ?? new HttpRouteValueDictionary());
			this._constraints = (constraints ?? new HttpRouteValueDictionary());
			this._dataTokens = (dataTokens ?? new HttpRouteValueDictionary());
			this.Handler = handler;
			if (parsedRoute == null)
			{
				this.ParsedRoute = RouteParser.Parse(routeTemplate);
				return;
			}
			this.ParsedRoute = parsedRoute;
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x0600063C RID: 1596 RVA: 0x0001491E File Offset: 0x00012B1E
		public IDictionary<string, object> Defaults
		{
			get
			{
				return this._defaults;
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x0600063D RID: 1597 RVA: 0x00014926 File Offset: 0x00012B26
		public IDictionary<string, object> Constraints
		{
			get
			{
				return this._constraints;
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x0600063E RID: 1598 RVA: 0x0001492E File Offset: 0x00012B2E
		public IDictionary<string, object> DataTokens
		{
			get
			{
				return this._dataTokens;
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x0600063F RID: 1599 RVA: 0x00014936 File Offset: 0x00012B36
		// (set) Token: 0x06000640 RID: 1600 RVA: 0x0001493E File Offset: 0x00012B3E
		public HttpMessageHandler Handler { get; private set; }

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000641 RID: 1601 RVA: 0x00014947 File Offset: 0x00012B47
		public string RouteTemplate
		{
			get
			{
				return this._routeTemplate;
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000642 RID: 1602 RVA: 0x0001494F File Offset: 0x00012B4F
		// (set) Token: 0x06000643 RID: 1603 RVA: 0x00014957 File Offset: 0x00012B57
		internal HttpParsedRoute ParsedRoute { get; private set; }

		// Token: 0x06000644 RID: 1604 RVA: 0x00014960 File Offset: 0x00012B60
		public virtual IHttpRouteData GetRouteData(string virtualPathRoot, HttpRequestMessage request)
		{
			if (virtualPathRoot == null)
			{
				throw Error.ArgumentNull("virtualPathRoot");
			}
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			RoutingContext orCreateRoutingContext = HttpRoute.GetOrCreateRoutingContext(virtualPathRoot, request);
			if (!orCreateRoutingContext.IsValid)
			{
				return null;
			}
			HttpRouteValueDictionary httpRouteValueDictionary = this.ParsedRoute.Match(orCreateRoutingContext, this._defaults);
			if (httpRouteValueDictionary == null)
			{
				return null;
			}
			if (!this.ProcessConstraints(request, httpRouteValueDictionary, HttpRouteDirection.UriResolution))
			{
				return null;
			}
			return new HttpRouteData(this, httpRouteValueDictionary);
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x000149C8 File Offset: 0x00012BC8
		private static RoutingContext GetOrCreateRoutingContext(string virtualPathRoot, HttpRequestMessage request)
		{
			RoutingContext routingContext;
			if (!request.Properties.TryGetValue("MS_RoutingContext", out routingContext))
			{
				routingContext = HttpRoute.CreateRoutingContext(virtualPathRoot, request);
				request.Properties["MS_RoutingContext"] = routingContext;
			}
			return routingContext;
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x00014A04 File Offset: 0x00012C04
		private static RoutingContext CreateRoutingContext(string virtualPathRoot, HttpRequestMessage request)
		{
			string text = "/" + request.RequestUri.GetComponents(UriComponents.Path, UriFormat.Unescaped);
			if (!text.StartsWith(virtualPathRoot, StringComparison.Ordinal) && !text.StartsWith(virtualPathRoot, StringComparison.OrdinalIgnoreCase))
			{
				return RoutingContext.Invalid();
			}
			int length = virtualPathRoot.Length;
			string uri;
			if (text.Length > length && text[length] == '/')
			{
				uri = text.Substring(length + 1);
			}
			else
			{
				uri = text.Substring(length);
			}
			return RoutingContext.Valid(RouteParser.SplitUriToPathSegmentStrings(uri));
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x00014A80 File Offset: 0x00012C80
		public virtual IHttpVirtualPathData GetVirtualPath(HttpRequestMessage request, IDictionary<string, object> values)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			if (values != null && !values.Keys.Contains(HttpRoute.HttpRouteKey, StringComparer.OrdinalIgnoreCase))
			{
				return null;
			}
			IDictionary<string, object> routeDictionaryWithoutHttpRouteKey = HttpRoute.GetRouteDictionaryWithoutHttpRouteKey(values);
			IHttpRouteData routeData = request.GetRouteData();
			IDictionary<string, object> currentValues = (routeData == null) ? null : routeData.Values;
			BoundRouteTemplate boundRouteTemplate = this.ParsedRoute.Bind(currentValues, routeDictionaryWithoutHttpRouteKey, this._defaults, this._constraints);
			if (boundRouteTemplate == null)
			{
				return null;
			}
			if (!this.ProcessConstraints(request, boundRouteTemplate.Values, HttpRouteDirection.UriGeneration))
			{
				return null;
			}
			return new HttpVirtualPathData(this, boundRouteTemplate.BoundTemplate);
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x00014B10 File Offset: 0x00012D10
		private static IDictionary<string, object> GetRouteDictionaryWithoutHttpRouteKey(IDictionary<string, object> routeValues)
		{
			HttpRouteValueDictionary httpRouteValueDictionary = new HttpRouteValueDictionary();
			if (routeValues != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair in routeValues)
				{
					if (!string.Equals(keyValuePair.Key, HttpRoute.HttpRouteKey, StringComparison.OrdinalIgnoreCase))
					{
						httpRouteValueDictionary.Add(keyValuePair.Key, keyValuePair.Value);
					}
				}
			}
			return httpRouteValueDictionary;
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x00014B84 File Offset: 0x00012D84
		protected virtual bool ProcessConstraint(HttpRequestMessage request, object constraint, string parameterName, HttpRouteValueDictionary values, HttpRouteDirection routeDirection)
		{
			IHttpRouteConstraint httpRouteConstraint = constraint as IHttpRouteConstraint;
			if (httpRouteConstraint != null)
			{
				return httpRouteConstraint.Match(request, this, parameterName, values, routeDirection);
			}
			string text = constraint as string;
			if (text == null)
			{
				throw Error.InvalidOperation(SRResources.Route_ValidationMustBeStringOrCustomConstraint, new object[]
				{
					parameterName,
					this.RouteTemplate,
					typeof(IHttpRouteConstraint).Name
				});
			}
			object value;
			values.TryGetValue(parameterName, out value);
			string input = Convert.ToString(value, CultureInfo.InvariantCulture);
			string pattern = "^(" + text + ")$";
			return Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x00014C20 File Offset: 0x00012E20
		private bool ProcessConstraints(HttpRequestMessage request, HttpRouteValueDictionary values, HttpRouteDirection routeDirection)
		{
			if (this.Constraints != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair in this.Constraints)
				{
					if (!this.ProcessConstraint(request, keyValuePair.Value, keyValuePair.Key, values, routeDirection))
					{
						return false;
					}
				}
				return true;
			}
			return true;
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x00014C90 File Offset: 0x00012E90
		internal static void ValidateConstraint(string routeTemplate, string name, object constraint)
		{
			if (constraint is IHttpRouteConstraint)
			{
				return;
			}
			if (constraint is string)
			{
				return;
			}
			throw HttpRoute.CreateInvalidConstraintTypeException(routeTemplate, name);
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x00014CAC File Offset: 0x00012EAC
		private static Exception CreateInvalidConstraintTypeException(string routeTemplate, string name)
		{
			return Error.InvalidOperation(SRResources.Route_ValidationMustBeStringOrCustomConstraint, new object[]
			{
				name,
				routeTemplate,
				typeof(IHttpRouteConstraint).FullName
			});
		}

		// Token: 0x040001BC RID: 444
		internal const string RoutingContextKey = "MS_RoutingContext";

		// Token: 0x040001BD RID: 445
		public static readonly string HttpRouteKey = "httproute";

		// Token: 0x040001BE RID: 446
		private string _routeTemplate;

		// Token: 0x040001BF RID: 447
		private HttpRouteValueDictionary _defaults;

		// Token: 0x040001C0 RID: 448
		private HttpRouteValueDictionary _constraints;

		// Token: 0x040001C1 RID: 449
		private HttpRouteValueDictionary _dataTokens;
	}
}
