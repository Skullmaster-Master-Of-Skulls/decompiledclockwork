using System;
using System.Web.Compilation;

namespace System.Web.UI
{
	// Token: 0x0200045E RID: 1118
	internal class SimpleHandlerFactory : IHttpHandlerFactory2, IHttpHandlerFactory
	{
		// Token: 0x060034F2 RID: 13554 RVA: 0x000E54CB File Offset: 0x000E44CB
		internal SimpleHandlerFactory()
		{
		}

		// Token: 0x060034F3 RID: 13555 RVA: 0x000E54D3 File Offset: 0x000E44D3
		public virtual IHttpHandler GetHandler(HttpContext context, string requestType, string virtualPath, string path)
		{
			return ((IHttpHandlerFactory2)this).GetHandler(context, requestType, VirtualPath.CreateNonRelative(virtualPath), path);
		}

		// Token: 0x060034F4 RID: 13556 RVA: 0x000E54E8 File Offset: 0x000E44E8
		IHttpHandler IHttpHandlerFactory2.GetHandler(HttpContext context, string requestType, VirtualPath virtualPath, string physicalPath)
		{
			BuildResultCompiledType buildResultCompiledType = (BuildResultCompiledType)BuildManager.GetVPathBuildResult(context, virtualPath);
			Util.CheckAssignableType(typeof(IHttpHandler), buildResultCompiledType.ResultType);
			return (IHttpHandler)buildResultCompiledType.CreateInstance();
		}

		// Token: 0x060034F5 RID: 13557 RVA: 0x000E5522 File Offset: 0x000E4522
		public virtual void ReleaseHandler(IHttpHandler handler)
		{
		}
	}
}
