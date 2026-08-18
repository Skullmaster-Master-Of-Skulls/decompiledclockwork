using System;

namespace System.Web.Mvc
{
	// Token: 0x0200019C RID: 412
	internal sealed class NullViewLocationCache : IViewLocationCache
	{
		// Token: 0x06000B98 RID: 2968 RVA: 0x0001E76E File Offset: 0x0001C96E
		public string GetViewLocation(HttpContextBase httpContext, string key)
		{
			return null;
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x0001E771 File Offset: 0x0001C971
		public void InsertViewLocation(HttpContextBase httpContext, string key, string virtualPath)
		{
		}
	}
}
