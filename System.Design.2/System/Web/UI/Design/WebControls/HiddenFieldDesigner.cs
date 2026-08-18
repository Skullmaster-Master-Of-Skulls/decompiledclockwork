using System;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000CE RID: 206
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class HiddenFieldDesigner : ControlDesigner
	{
		// Token: 0x06000713 RID: 1811 RVA: 0x0000C5B3 File Offset: 0x0000A7B3
		public override string GetDesignTimeHtml()
		{
			return base.CreatePlaceHolderDesignTimeHtml();
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x00027448 File Offset: 0x00025648
		public override void Initialize(IComponent component)
		{
			ControlDesigner.VerifyInitializeArgument(component, typeof(HiddenField));
			base.Initialize(component);
		}
	}
}
