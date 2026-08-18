using System;
using System.Web.Mvc.Properties;

namespace System.Web.Mvc
{
	// Token: 0x020001C7 RID: 455
	public class HandleErrorInfo
	{
		// Token: 0x06000D73 RID: 3443 RVA: 0x00023900 File Offset: 0x00021B00
		public HandleErrorInfo(Exception exception, string controllerName, string actionName)
		{
			if (exception == null)
			{
				throw new ArgumentNullException("exception");
			}
			if (string.IsNullOrEmpty(controllerName))
			{
				throw new ArgumentException(MvcResources.Common_NullOrEmpty, "controllerName");
			}
			if (string.IsNullOrEmpty(actionName))
			{
				throw new ArgumentException(MvcResources.Common_NullOrEmpty, "actionName");
			}
			this.Exception = exception;
			this.ControllerName = controllerName;
			this.ActionName = actionName;
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000D74 RID: 3444 RVA: 0x00023966 File Offset: 0x00021B66
		// (set) Token: 0x06000D75 RID: 3445 RVA: 0x0002396E File Offset: 0x00021B6E
		public string ActionName { get; private set; }

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000D76 RID: 3446 RVA: 0x00023977 File Offset: 0x00021B77
		// (set) Token: 0x06000D77 RID: 3447 RVA: 0x0002397F File Offset: 0x00021B7F
		public string ControllerName { get; private set; }

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000D78 RID: 3448 RVA: 0x00023988 File Offset: 0x00021B88
		// (set) Token: 0x06000D79 RID: 3449 RVA: 0x00023990 File Offset: 0x00021B90
		public Exception Exception { get; private set; }
	}
}
