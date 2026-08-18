using System;

namespace System.Web.Mvc
{
	// Token: 0x02000195 RID: 405
	public interface IViewLocationCache
	{
		// Token: 0x06000B7E RID: 2942
		string GetViewLocation(HttpContextBase httpContext, string key);

		// Token: 0x06000B7F RID: 2943
		void InsertViewLocation(HttpContextBase httpContext, string key, string virtualPath);
	}
}
