using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Security.Permissions;
using System.Web.UI.WebControls.WebParts;

namespace System.Web.UI.Design.WebControls.WebParts
{
	// Token: 0x0200014E RID: 334
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public abstract class PartDesigner : CompositeControlDesigner
	{
		// Token: 0x06000BCD RID: 3021 RVA: 0x00029614 File Offset: 0x00027814
		internal PartDesigner()
		{
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000BCE RID: 3022 RVA: 0x00003B0F File Offset: 0x00001D0F
		protected override bool UsePreviewControl
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x0004B3A4 File Offset: 0x000495A4
		internal static Control GetViewControl(Control control)
		{
			ControlDesigner designer = PartDesigner.GetDesigner(control);
			if (designer != null)
			{
				return designer.ViewControl;
			}
			return control;
		}

		// Token: 0x06000BD0 RID: 3024 RVA: 0x0004B3C4 File Offset: 0x000495C4
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

		// Token: 0x06000BD1 RID: 3025 RVA: 0x0004B401 File Offset: 0x00049601
		public override void Initialize(IComponent component)
		{
			ControlDesigner.VerifyInitializeArgument(component, typeof(Part));
			base.Initialize(component);
		}
	}
}
