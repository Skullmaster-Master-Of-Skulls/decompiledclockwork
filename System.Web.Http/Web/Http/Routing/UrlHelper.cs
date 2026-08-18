using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Properties;

namespace System.Web.Http.Routing
{
	// Token: 0x02000114 RID: 276
	public class UrlHelper
	{
		// Token: 0x06000695 RID: 1685 RVA: 0x00015F52 File Offset: 0x00014152
		public UrlHelper()
		{
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x00015F5A File Offset: 0x0001415A
		public UrlHelper(HttpRequestMessage request)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			this.Request = request;
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000697 RID: 1687 RVA: 0x00015F77 File Offset: 0x00014177
		// (set) Token: 0x06000698 RID: 1688 RVA: 0x00015F7F File Offset: 0x0001417F
		public HttpRequestMessage Request
		{
			get
			{
				return this._request;
			}
			set
			{
				if (value == null)
				{
					throw Error.PropertyNull();
				}
				this._request = value;
			}
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x00015F94 File Offset: 0x00014194
		public virtual string Content(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				throw Error.ArgumentNullOrEmpty("path");
			}
			if (this.Request == null)
			{
				throw Error.InvalidOperation(SRResources.RequestIsNull, new object[]
				{
					"UrlHelper"
				});
			}
			if (path.StartsWith("~/", StringComparison.Ordinal))
			{
				HttpRequestContext requestContext = this.Request.GetRequestContext();
				string text;
				if (requestContext != null)
				{
					text = requestContext.VirtualPathRoot;
				}
				else
				{
					HttpConfiguration configuration = this.Request.GetConfiguration();
					if (configuration == null)
					{
						throw Error.InvalidOperation(SRResources.HttpRequestMessageExtensions_NoConfiguration, new object[0]);
					}
					text = configuration.VirtualPathRoot;
				}
				if (text == null)
				{
					text = "/";
				}
				if (!text.StartsWith("/", StringComparison.Ordinal))
				{
					text = "/" + text;
				}
				if (!text.EndsWith("/", StringComparison.Ordinal))
				{
					text += "/";
				}
				return new Uri(this.Request.RequestUri, text + path.Substring("~/".Length)).AbsoluteUri;
			}
			return new Uri(this.Request.RequestUri, path).AbsoluteUri;
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x000160A6 File Offset: 0x000142A6
		public virtual string Route(string routeName, object routeValues)
		{
			return this.Route(routeName, new HttpRouteValueDictionary(routeValues));
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x000160B5 File Offset: 0x000142B5
		public virtual string Route(string routeName, IDictionary<string, object> routeValues)
		{
			return UrlHelper.GetVirtualPath(this.Request, routeName, routeValues);
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x000160C4 File Offset: 0x000142C4
		public virtual string Link(string routeName, object routeValues)
		{
			return this.Link(routeName, new HttpRouteValueDictionary(routeValues));
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x000160D4 File Offset: 0x000142D4
		public virtual string Link(string routeName, IDictionary<string, object> routeValues)
		{
			string text = this.Route(routeName, routeValues);
			if (!string.IsNullOrEmpty(text))
			{
				text = new Uri(this.Request.RequestUri, text).AbsoluteUri;
			}
			return text;
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x0001610C File Offset: 0x0001430C
		private static string GetVirtualPath(HttpRequestMessage request, string routeName, IDictionary<string, object> routeValues)
		{
			if (routeValues == null)
			{
				routeValues = new HttpRouteValueDictionary();
				routeValues.Add(HttpRoute.HttpRouteKey, true);
			}
			else
			{
				routeValues = new HttpRouteValueDictionary(routeValues);
				if (!routeValues.ContainsKey(HttpRoute.HttpRouteKey))
				{
					routeValues.Add(HttpRoute.HttpRouteKey, true);
				}
			}
			HttpConfiguration configuration = request.GetConfiguration();
			if (configuration == null)
			{
				throw Error.InvalidOperation(SRResources.HttpRequestMessageExtensions_NoConfiguration, new object[0]);
			}
			IHttpVirtualPathData virtualPath = configuration.Routes.GetVirtualPath(request, routeName, routeValues);
			if (virtualPath == null)
			{
				return null;
			}
			return virtualPath.VirtualPath;
		}

		// Token: 0x040001D6 RID: 470
		private HttpRequestMessage _request;
	}
}
