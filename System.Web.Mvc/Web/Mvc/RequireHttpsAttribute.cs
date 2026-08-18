using System;
using System.Web.Mvc.Properties;

namespace System.Web.Mvc
{
	// Token: 0x0200014C RID: 332
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
	public class RequireHttpsAttribute : FilterAttribute, IAuthorizationFilter
	{
		// Token: 0x0600088E RID: 2190 RVA: 0x0001797A File Offset: 0x00015B7A
		public virtual void OnAuthorization(AuthorizationContext filterContext)
		{
			if (filterContext == null)
			{
				throw new ArgumentNullException("filterContext");
			}
			if (!filterContext.HttpContext.Request.IsSecureConnection)
			{
				this.HandleNonHttpsRequest(filterContext);
			}
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x000179A4 File Offset: 0x00015BA4
		protected virtual void HandleNonHttpsRequest(AuthorizationContext filterContext)
		{
			if (!string.Equals(filterContext.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
			{
				throw new InvalidOperationException(MvcResources.RequireHttpsAttribute_MustUseSsl);
			}
			string url = "https://" + filterContext.HttpContext.Request.Url.Host + filterContext.HttpContext.Request.RawUrl;
			filterContext.Result = new RedirectResult(url);
		}
	}
}
