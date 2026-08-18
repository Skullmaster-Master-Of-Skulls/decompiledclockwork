using System;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web.UI.WebControls.WebParts;

namespace System.Web.UI.Design.WebControls.WebParts
{
	// Token: 0x0200014D RID: 333
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class PageCatalogPartDesigner : CatalogPartDesigner
	{
		// Token: 0x06000BCA RID: 3018 RVA: 0x0004B349 File Offset: 0x00049549
		public override string GetDesignTimeHtml()
		{
			if (!(this._catalogPart.Parent is CatalogZoneBase))
			{
				return base.CreateInvalidParentDesignTimeHtml(typeof(CatalogPart), typeof(CatalogZoneBase));
			}
			return string.Empty;
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x0004B37D File Offset: 0x0004957D
		public override void Initialize(IComponent component)
		{
			ControlDesigner.VerifyInitializeArgument(component, typeof(PageCatalogPart));
			this._catalogPart = (PageCatalogPart)component;
			base.Initialize(component);
		}

		// Token: 0x04000714 RID: 1812
		private PageCatalogPart _catalogPart;
	}
}
