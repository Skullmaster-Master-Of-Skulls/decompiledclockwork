using System;

namespace System.Web.Mvc
{
	// Token: 0x02000131 RID: 305
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public sealed class ChildActionOnlyAttribute : FilterAttribute, IAuthorizationFilter
	{
		// Token: 0x060007FD RID: 2045 RVA: 0x000159E6 File Offset: 0x00013BE6
		public void OnAuthorization(AuthorizationContext filterContext)
		{
			if (filterContext == null)
			{
				throw new ArgumentNullException("filterContext");
			}
			if (!filterContext.IsChildAction)
			{
				throw Error.ChildActionOnlyAttribute_MustBeInChildRequest(filterContext.ActionDescriptor);
			}
		}
	}
}
