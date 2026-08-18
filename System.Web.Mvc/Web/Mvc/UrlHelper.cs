using System;
using System.Globalization;
using System.Web.Mvc.Properties;
using System.Web.Routing;
using System.Web.WebPages;

namespace System.Web.Mvc
{
	// Token: 0x020001ED RID: 493
	public class UrlHelper
	{
		// Token: 0x06000EED RID: 3821 RVA: 0x00027614 File Offset: 0x00025814
		public UrlHelper()
		{
		}

		// Token: 0x06000EEE RID: 3822 RVA: 0x0002761C File Offset: 0x0002581C
		public UrlHelper(RequestContext requestContext) : this(requestContext, RouteTable.Routes)
		{
		}

		// Token: 0x06000EEF RID: 3823 RVA: 0x0002762A File Offset: 0x0002582A
		public UrlHelper(RequestContext requestContext, RouteCollection routeCollection)
		{
			if (requestContext == null)
			{
				throw new ArgumentNullException("requestContext");
			}
			if (routeCollection == null)
			{
				throw new ArgumentNullException("routeCollection");
			}
			this.RequestContext = requestContext;
			this.RouteCollection = routeCollection;
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000EF0 RID: 3824 RVA: 0x0002765C File Offset: 0x0002585C
		// (set) Token: 0x06000EF1 RID: 3825 RVA: 0x00027664 File Offset: 0x00025864
		public RequestContext RequestContext { get; private set; }

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000EF2 RID: 3826 RVA: 0x0002766D File Offset: 0x0002586D
		// (set) Token: 0x06000EF3 RID: 3827 RVA: 0x00027675 File Offset: 0x00025875
		public RouteCollection RouteCollection { get; private set; }

		// Token: 0x06000EF4 RID: 3828 RVA: 0x0002767E File Offset: 0x0002587E
		public virtual string Action()
		{
			return this.RequestContext.HttpContext.Request.RawUrl;
		}

		// Token: 0x06000EF5 RID: 3829 RVA: 0x00027695 File Offset: 0x00025895
		public virtual string Action(string actionName)
		{
			return this.GenerateUrl(null, actionName, null, null);
		}

		// Token: 0x06000EF6 RID: 3830 RVA: 0x000276A1 File Offset: 0x000258A1
		public virtual string Action(string actionName, object routeValues)
		{
			return this.GenerateUrl(null, actionName, null, TypeHelper.ObjectToDictionary(routeValues));
		}

		// Token: 0x06000EF7 RID: 3831 RVA: 0x000276B2 File Offset: 0x000258B2
		public virtual string Action(string actionName, RouteValueDictionary routeValues)
		{
			return this.GenerateUrl(null, actionName, null, routeValues);
		}

		// Token: 0x06000EF8 RID: 3832 RVA: 0x000276BE File Offset: 0x000258BE
		public virtual string Action(string actionName, string controllerName)
		{
			return this.GenerateUrl(null, actionName, controllerName, null);
		}

		// Token: 0x06000EF9 RID: 3833 RVA: 0x000276CA File Offset: 0x000258CA
		public virtual string Action(string actionName, string controllerName, object routeValues)
		{
			return this.GenerateUrl(null, actionName, controllerName, TypeHelper.ObjectToDictionary(routeValues));
		}

		// Token: 0x06000EFA RID: 3834 RVA: 0x000276DB File Offset: 0x000258DB
		public virtual string Action(string actionName, string controllerName, RouteValueDictionary routeValues)
		{
			return this.GenerateUrl(null, actionName, controllerName, routeValues);
		}

		// Token: 0x06000EFB RID: 3835 RVA: 0x000276E8 File Offset: 0x000258E8
		public virtual string Action(string actionName, string controllerName, RouteValueDictionary routeValues, string protocol)
		{
			return UrlHelper.GenerateUrl(null, actionName, controllerName, protocol, null, null, routeValues, this.RouteCollection, this.RequestContext, true);
		}

		// Token: 0x06000EFC RID: 3836 RVA: 0x00027710 File Offset: 0x00025910
		public virtual string Action(string actionName, string controllerName, object routeValues, string protocol)
		{
			return UrlHelper.GenerateUrl(null, actionName, controllerName, protocol, null, null, TypeHelper.ObjectToDictionary(routeValues), this.RouteCollection, this.RequestContext, true);
		}

		// Token: 0x06000EFD RID: 3837 RVA: 0x0002773C File Offset: 0x0002593C
		public virtual string Action(string actionName, string controllerName, RouteValueDictionary routeValues, string protocol, string hostName)
		{
			return UrlHelper.GenerateUrl(null, actionName, controllerName, protocol, hostName, null, routeValues, this.RouteCollection, this.RequestContext, true);
		}

		// Token: 0x06000EFE RID: 3838 RVA: 0x00027764 File Offset: 0x00025964
		public virtual string Content(string contentPath)
		{
			return UrlHelper.GenerateContentUrl(contentPath, this.RequestContext.HttpContext);
		}

		// Token: 0x06000EFF RID: 3839 RVA: 0x00027777 File Offset: 0x00025977
		public static string GenerateContentUrl(string contentPath, HttpContextBase httpContext)
		{
			if (string.IsNullOrEmpty(contentPath))
			{
				throw new ArgumentException(MvcResources.Common_NullOrEmpty, "contentPath");
			}
			if (httpContext == null)
			{
				throw new ArgumentNullException("httpContext");
			}
			if (contentPath[0] == '~')
			{
				return UrlUtil.GenerateClientUrl(httpContext, contentPath);
			}
			return contentPath;
		}

		// Token: 0x06000F00 RID: 3840 RVA: 0x000277B3 File Offset: 0x000259B3
		public virtual string Encode(string url)
		{
			return HttpUtility.UrlEncode(url);
		}

		// Token: 0x06000F01 RID: 3841 RVA: 0x000277BB File Offset: 0x000259BB
		private string GenerateUrl(string routeName, string actionName, string controllerName, RouteValueDictionary routeValues)
		{
			return UrlHelper.GenerateUrl(routeName, actionName, controllerName, routeValues, this.RouteCollection, this.RequestContext, true);
		}

		// Token: 0x06000F02 RID: 3842 RVA: 0x000277D4 File Offset: 0x000259D4
		public static string GenerateUrl(string routeName, string actionName, string controllerName, string protocol, string hostName, string fragment, RouteValueDictionary routeValues, RouteCollection routeCollection, RequestContext requestContext, bool includeImplicitMvcValues)
		{
			string text = UrlHelper.GenerateUrl(routeName, actionName, controllerName, routeValues, routeCollection, requestContext, includeImplicitMvcValues);
			if (text != null)
			{
				if (!string.IsNullOrEmpty(fragment))
				{
					text = text + "#" + fragment;
				}
				if (!string.IsNullOrEmpty(protocol) || !string.IsNullOrEmpty(hostName))
				{
					Uri url = requestContext.HttpContext.Request.Url;
					protocol = ((!string.IsNullOrEmpty(protocol)) ? protocol : Uri.UriSchemeHttp);
					hostName = ((!string.IsNullOrEmpty(hostName)) ? hostName : url.Host);
					string text2 = string.Empty;
					string scheme = url.Scheme;
					if (string.Equals(protocol, scheme, StringComparison.OrdinalIgnoreCase))
					{
						text2 = (url.IsDefaultPort ? string.Empty : (":" + Convert.ToString(url.Port, CultureInfo.InvariantCulture)));
					}
					text = string.Concat(new string[]
					{
						protocol,
						Uri.SchemeDelimiter,
						hostName,
						text2,
						text
					});
				}
			}
			return text;
		}

		// Token: 0x06000F03 RID: 3843 RVA: 0x000278CC File Offset: 0x00025ACC
		public static string GenerateUrl(string routeName, string actionName, string controllerName, RouteValueDictionary routeValues, RouteCollection routeCollection, RequestContext requestContext, bool includeImplicitMvcValues)
		{
			if (routeCollection == null)
			{
				throw new ArgumentNullException("routeCollection");
			}
			if (requestContext == null)
			{
				throw new ArgumentNullException("requestContext");
			}
			RouteValueDictionary values = RouteValuesHelpers.MergeRouteValues(actionName, controllerName, requestContext.RouteData.Values, routeValues, includeImplicitMvcValues);
			VirtualPathData virtualPathForArea = routeCollection.GetVirtualPathForArea(requestContext, routeName, values);
			if (virtualPathForArea == null)
			{
				return null;
			}
			return UrlUtil.GenerateClientUrl(requestContext.HttpContext, virtualPathForArea.VirtualPath);
		}

		// Token: 0x06000F04 RID: 3844 RVA: 0x00027933 File Offset: 0x00025B33
		public virtual bool IsLocalUrl(string url)
		{
			return this.RequestContext.HttpContext.Request.IsUrlLocalToHost(url);
		}

		// Token: 0x06000F05 RID: 3845 RVA: 0x0002794B File Offset: 0x00025B4B
		public virtual string RouteUrl(object routeValues)
		{
			return this.RouteUrl(null, routeValues);
		}

		// Token: 0x06000F06 RID: 3846 RVA: 0x00027955 File Offset: 0x00025B55
		public virtual string RouteUrl(RouteValueDictionary routeValues)
		{
			return this.RouteUrl(null, routeValues);
		}

		// Token: 0x06000F07 RID: 3847 RVA: 0x0002795F File Offset: 0x00025B5F
		public virtual string RouteUrl(string routeName)
		{
			return this.RouteUrl(routeName, null);
		}

		// Token: 0x06000F08 RID: 3848 RVA: 0x00027969 File Offset: 0x00025B69
		public virtual string RouteUrl(string routeName, object routeValues)
		{
			return this.RouteUrl(routeName, routeValues, null);
		}

		// Token: 0x06000F09 RID: 3849 RVA: 0x00027974 File Offset: 0x00025B74
		public virtual string RouteUrl(string routeName, RouteValueDictionary routeValues)
		{
			return this.RouteUrl(routeName, routeValues, null, null);
		}

		// Token: 0x06000F0A RID: 3850 RVA: 0x00027980 File Offset: 0x00025B80
		public virtual string RouteUrl(string routeName, object routeValues, string protocol)
		{
			return UrlHelper.GenerateUrl(routeName, null, null, protocol, null, null, TypeHelper.ObjectToDictionary(routeValues), this.RouteCollection, this.RequestContext, false);
		}

		// Token: 0x06000F0B RID: 3851 RVA: 0x000279AC File Offset: 0x00025BAC
		public virtual string RouteUrl(string routeName, RouteValueDictionary routeValues, string protocol, string hostName)
		{
			return UrlHelper.GenerateUrl(routeName, null, null, protocol, hostName, null, routeValues, this.RouteCollection, this.RequestContext, false);
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x000279D3 File Offset: 0x00025BD3
		public virtual string HttpRouteUrl(string routeName, object routeValues)
		{
			return this.HttpRouteUrl(routeName, TypeHelper.ObjectToDictionary(routeValues));
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x000279E4 File Offset: 0x00025BE4
		public virtual string HttpRouteUrl(string routeName, RouteValueDictionary routeValues)
		{
			if (routeValues == null)
			{
				routeValues = new RouteValueDictionary();
				routeValues.Add("httproute", true);
			}
			else
			{
				routeValues = new RouteValueDictionary(routeValues);
				if (!routeValues.ContainsKey("httproute"))
				{
					routeValues.Add("httproute", true);
				}
			}
			return UrlHelper.GenerateUrl(routeName, null, null, null, null, null, routeValues, this.RouteCollection, this.RequestContext, false);
		}

		// Token: 0x040003EA RID: 1002
		private const string HttpRouteKey = "httproute";
	}
}
