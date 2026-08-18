using System;
using System.Collections;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web.UI.WebControls.WebParts;

namespace System.Web.UI.Design.WebControls.WebParts
{
	// Token: 0x0200052B RID: 1323
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class CatalogPartDesigner : PartDesigner
	{
		// Token: 0x06002F16 RID: 12054 RVA: 0x0010D654 File Offset: 0x0010C654
		protected override Control CreateViewControl()
		{
			Control control = base.CreateViewControl();
			IDictionary designModeState = ((IControlDesignerAccessor)this._catalogPart).GetDesignModeState();
			((IControlDesignerAccessor)control).SetDesignModeState(designModeState);
			return control;
		}

		// Token: 0x06002F17 RID: 12055 RVA: 0x0010D67C File Offset: 0x0010C67C
		public override void Initialize(IComponent component)
		{
			ControlDesigner.VerifyInitializeArgument(component, typeof(CatalogPart));
			this._catalogPart = (CatalogPart)component;
			base.Initialize(component);
		}

		// Token: 0x06002F18 RID: 12056 RVA: 0x0010D6A1 File Offset: 0x0010C6A1
		public override string GetDesignTimeHtml()
		{
			if (!(this._catalogPart.Parent is CatalogZoneBase))
			{
				return base.CreateInvalidParentDesignTimeHtml(typeof(CatalogPart), typeof(CatalogZoneBase));
			}
			return base.GetDesignTimeHtml();
		}

		// Token: 0x0400202B RID: 8235
		private CatalogPart _catalogPart;
	}
}
