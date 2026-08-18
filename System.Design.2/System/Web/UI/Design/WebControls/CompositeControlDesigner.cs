using System;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000AB RID: 171
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class CompositeControlDesigner : ControlDesigner
	{
		// Token: 0x0600052E RID: 1326 RVA: 0x00018FE4 File Offset: 0x000171E4
		protected virtual void CreateChildControls()
		{
			ICompositeControlDesignerAccessor compositeControlDesignerAccessor = (ICompositeControlDesignerAccessor)base.ViewControl;
			compositeControlDesignerAccessor.RecreateChildControls();
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x00019003 File Offset: 0x00017203
		public override string GetDesignTimeHtml()
		{
			this.CreateChildControls();
			return base.GetDesignTimeHtml();
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x00019011 File Offset: 0x00017211
		public override void Initialize(IComponent component)
		{
			ControlDesigner.VerifyInitializeArgument(component, typeof(INamingContainer));
			base.Initialize(component);
		}
	}
}
