using System;
using System.Web.Compilation;

namespace System.Web.UI
{
	// Token: 0x020002F8 RID: 760
	internal class SimpleHandlerFactory : IHttpHandlerFactory2, IHttpHandlerFactory
	{
		// Token: 0x06002325 RID: 8997 RVA: 0x000030B5 File Offset: 0x000012B5
		internal SimpleHandlerFactory()
		{
		}

		// Token: 0x06002326 RID: 8998 RVA: 0x000727F4 File Offset: 0x000709F4
		public virtual IHttpHandler GetHandler(HttpContext context, string requestType, string virtualPath, string path)
		{
			return ((IHttpHandlerFactory2)this).GetHandler(context, requestType, VirtualPath.CreateNonRelative(virtualPath), path);
		}

		// Token: 0x06002327 RID: 8999 RVA: 0x00072808 File Offset: 0x00070A08
		IHttpHandler IHttpHandlerFactory2.GetHandler(HttpContext context, string requestType, VirtualPath virtualPath, string physicalPath)
		{
			BuildResultCompiledType buildResultCompiledType = (BuildResultCompiledType)BuildManager.GetVPathBuildResult(context, virtualPath);
			Util.CheckAssignableType(typeof(IHttpHandler), buildResultCompiledType.ResultType);
			return (IHttpHandler)buildResultCompiledType.CreateInstance();
		}

		// Token: 0x06002328 RID: 9000 RVA: 0x00006164 File Offset: 0x00004364
		public virtual void ReleaseHandler(IHttpHandler handler)
		{
		}
	}
}
