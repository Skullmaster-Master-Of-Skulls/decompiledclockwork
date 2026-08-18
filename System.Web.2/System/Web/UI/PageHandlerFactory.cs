using System;
using System.Security.Permissions;
using System.Web.Compilation;

namespace System.Web.UI
{
	// Token: 0x020002D8 RID: 728
	[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
	[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
	public class PageHandlerFactory : IHttpHandlerFactory2, IHttpHandlerFactory
	{
		// Token: 0x060021E2 RID: 8674 RVA: 0x0006EADF File Offset: 0x0006CCDF
		protected internal PageHandlerFactory()
		{
			this._isInheritedInstance = (base.GetType() != typeof(PageHandlerFactory));
		}

		// Token: 0x060021E3 RID: 8675 RVA: 0x0006EB02 File Offset: 0x0006CD02
		public virtual IHttpHandler GetHandler(HttpContext context, string requestType, string virtualPath, string path)
		{
			return this.GetHandlerHelper(context, requestType, VirtualPath.CreateNonRelative(virtualPath), path);
		}

		// Token: 0x060021E4 RID: 8676 RVA: 0x0006EB14 File Offset: 0x0006CD14
		IHttpHandler IHttpHandlerFactory2.GetHandler(HttpContext context, string requestType, VirtualPath virtualPath, string physicalPath)
		{
			if (this._isInheritedInstance)
			{
				return this.GetHandler(context, requestType, virtualPath.VirtualPathString, physicalPath);
			}
			return this.GetHandlerHelper(context, requestType, virtualPath, physicalPath);
		}

		// Token: 0x060021E5 RID: 8677 RVA: 0x00006164 File Offset: 0x00004364
		public virtual void ReleaseHandler(IHttpHandler handler)
		{
		}

		// Token: 0x060021E6 RID: 8678 RVA: 0x0006EB3C File Offset: 0x0006CD3C
		private IHttpHandler GetHandlerHelper(HttpContext context, string requestType, VirtualPath virtualPath, string physicalPath)
		{
			Page page = BuildManager.CreateInstanceFromVirtualPath(virtualPath, typeof(Page), context, true) as Page;
			if (page == null)
			{
				return null;
			}
			page.TemplateControlVirtualPath = virtualPath;
			return page;
		}

		// Token: 0x04001BFD RID: 7165
		private bool _isInheritedInstance;
	}
}
