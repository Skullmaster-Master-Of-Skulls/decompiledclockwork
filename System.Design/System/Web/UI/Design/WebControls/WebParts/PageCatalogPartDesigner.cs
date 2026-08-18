using System;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web.UI.WebControls.WebParts;

namespace System.Web.UI.Design.WebControls.WebParts
{
	// Token: 0x02000541 RID: 1345
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class PageCatalogPartDesigner : CatalogPartDesigner
	{
		// Token: 0x06002F6A RID: 12138 RVA: 0x0010E62E File Offset: 0x0010D62E
		public override string GetDesignTimeHtml()
		{
			if (!(this._catalogPart.Parent is CatalogZoneBase))
			{
				return base.CreateInvalidParentDesignTimeHtml(typeof(CatalogPart), typeof(CatalogZoneBase));
			}
			return string.Empty;
		}

		// Token: 0x06002F6B RID: 12139 RVA: 0x0010E662 File Offset: 0x0010D662
		public override void Initialize(IComponent component)
		{
			ControlDesigner.VerifyInitializeArgument(component, typeof(PageCatalogPart));
			this._catalogPart = (PageCatalogPart)component;
			base.Initialize(component);
		}

		// Token: 0x04002046 RID: 8262
		private PageCatalogPart _catalogPart;
	}
}
