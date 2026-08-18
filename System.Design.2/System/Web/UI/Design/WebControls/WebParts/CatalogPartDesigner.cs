using System;
using System.Collections;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web.UI.WebControls.WebParts;

namespace System.Web.UI.Design.WebControls.WebParts
{
	// Token: 0x02000140 RID: 320
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class CatalogPartDesigner : PartDesigner
	{
		// Token: 0x06000B94 RID: 2964 RVA: 0x0004A754 File Offset: 0x00048954
		protected override Control CreateViewControl()
		{
			Control control = base.CreateViewControl();
			IDictionary designModeState = ((IControlDesignerAccessor)this._catalogPart).GetDesignModeState();
			((IControlDesignerAccessor)control).SetDesignModeState(designModeState);
			return control;
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x0004A77C File Offset: 0x0004897C
		public override void Initialize(IComponent component)
		{
			ControlDesigner.VerifyInitializeArgument(component, typeof(CatalogPart));
			this._catalogPart = (CatalogPart)component;
			base.Initialize(component);
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x0004A7A1 File Offset: 0x000489A1
		public override string GetDesignTimeHtml()
		{
			if (!(this._catalogPart.Parent is CatalogZoneBase))
			{
				return base.CreateInvalidParentDesignTimeHtml(typeof(CatalogPart), typeof(CatalogZoneBase));
			}
			return base.GetDesignTimeHtml();
		}

		// Token: 0x04000700 RID: 1792
		private CatalogPart _catalogPart;
	}
}
