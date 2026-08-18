using System;

namespace System.Web.Mvc
{
	// Token: 0x0200019D RID: 413
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
	public class ValidateInputAttribute : FilterAttribute, IAuthorizationFilter
	{
		// Token: 0x06000B9B RID: 2971 RVA: 0x0001E77B File Offset: 0x0001C97B
		public ValidateInputAttribute(bool enableValidation)
		{
			this.EnableValidation = enableValidation;
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000B9C RID: 2972 RVA: 0x0001E78A File Offset: 0x0001C98A
		// (set) Token: 0x06000B9D RID: 2973 RVA: 0x0001E792 File Offset: 0x0001C992
		public bool EnableValidation { get; private set; }

		// Token: 0x06000B9E RID: 2974 RVA: 0x0001E79B File Offset: 0x0001C99B
		public virtual void OnAuthorization(AuthorizationContext filterContext)
		{
			if (filterContext == null)
			{
				throw new ArgumentNullException("filterContext");
			}
			filterContext.Controller.ValidateRequest = this.EnableValidation;
		}
	}
}
