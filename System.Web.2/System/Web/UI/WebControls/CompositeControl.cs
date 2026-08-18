using System;
using System.ComponentModel;
using System.Web.Util;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200039D RID: 925
	[Designer("System.Web.UI.Design.WebControls.CompositeControlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public abstract class CompositeControl : WebControl, INamingContainer, ICompositeControlDesignerAccessor
	{
		// Token: 0x17000C8D RID: 3213
		// (get) Token: 0x06002C3D RID: 11325 RVA: 0x000856CA File Offset: 0x000838CA
		public override ControlCollection Controls
		{
			get
			{
				this.EnsureChildControls();
				return base.Controls;
			}
		}

		// Token: 0x17000C8E RID: 3214
		// (get) Token: 0x06002C3E RID: 11326 RVA: 0x000853AC File Offset: 0x000835AC
		public override bool SupportsDisabledAttribute
		{
			get
			{
				return this.RenderingCompatibility < VersionUtil.Framework40;
			}
		}

		// Token: 0x06002C3F RID: 11327 RVA: 0x000906F4 File Offset: 0x0008E8F4
		public override void DataBind()
		{
			this.OnDataBinding(EventArgs.Empty);
			this.EnsureChildControls();
			this.DataBindChildren();
		}

		// Token: 0x06002C40 RID: 11328 RVA: 0x0009070D File Offset: 0x0008E90D
		protected virtual void RecreateChildControls()
		{
			base.ChildControlsCreated = false;
			this.EnsureChildControls();
		}

		// Token: 0x06002C41 RID: 11329 RVA: 0x0009071C File Offset: 0x0008E91C
		protected internal override void Render(HtmlTextWriter writer)
		{
			if (base.DesignMode)
			{
				this.EnsureChildControls();
			}
			base.Render(writer);
		}

		// Token: 0x06002C42 RID: 11330 RVA: 0x00090733 File Offset: 0x0008E933
		void ICompositeControlDesignerAccessor.RecreateChildControls()
		{
			this.RecreateChildControls();
		}
	}
}
