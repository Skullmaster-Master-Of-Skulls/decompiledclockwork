using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Security.Permissions;
using System.Web.UI.WebControls.WebParts;

namespace System.Web.UI.Design.WebControls.WebParts
{
	// Token: 0x0200052A RID: 1322
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public abstract class PartDesigner : CompositeControlDesigner
	{
		// Token: 0x06002F11 RID: 12049 RVA: 0x0010D5D0 File Offset: 0x0010C5D0
		internal PartDesigner()
		{
		}

		// Token: 0x170008E4 RID: 2276
		// (get) Token: 0x06002F12 RID: 12050 RVA: 0x0010D5D8 File Offset: 0x0010C5D8
		protected override bool UsePreviewControl
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002F13 RID: 12051 RVA: 0x0010D5DC File Offset: 0x0010C5DC
		internal static Control GetViewControl(Control control)
		{
			ControlDesigner designer = PartDesigner.GetDesigner(control);
			if (designer != null)
			{
				return designer.ViewControl;
			}
			return control;
		}

		// Token: 0x06002F14 RID: 12052 RVA: 0x0010D5FC File Offset: 0x0010C5FC
		private static ControlDesigner GetDesigner(Control control)
		{
			ControlDesigner result = null;
			ISite site = control.Site;
			if (site != null)
			{
				IDesignerHost designerHost = (IDesignerHost)site.GetService(typeof(IDesignerHost));
				result = (designerHost.GetDesigner(control) as ControlDesigner);
			}
			return result;
		}

		// Token: 0x06002F15 RID: 12053 RVA: 0x0010D639 File Offset: 0x0010C639
		public override void Initialize(IComponent component)
		{
			ControlDesigner.VerifyInitializeArgument(component, typeof(Part));
			base.Initialize(component);
		}
	}
}
