using System;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web.UI.WebControls.WebParts;

namespace System.Web.UI.Design.WebControls.WebParts
{
	// Token: 0x02000152 RID: 338
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class WebPartManagerDesigner : ControlDesigner
	{
		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000BDD RID: 3037 RVA: 0x00003B0F File Offset: 0x00001D0F
		protected override bool UsePreviewControl
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x0000C5B3 File Offset: 0x0000A7B3
		public override string GetDesignTimeHtml()
		{
			return base.CreatePlaceHolderDesignTimeHtml();
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x0004B4EE File Offset: 0x000496EE
		public override void Initialize(IComponent component)
		{
			ControlDesigner.VerifyInitializeArgument(component, typeof(WebPartManager));
			base.Initialize(component);
		}
	}
}
