using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;
using Telerik.Licensing;

namespace Telerik.Web.UI
{
	// Token: 0x02000DA5 RID: 3493
	[LightweightRendering]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadWindow))]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[TelerikToolboxCategory("Container")]
	[ParseChildren(true)]
	[ToolboxBitmap(typeof(RadWindow), "Telerik.Web.UI.Window.png")]
	[Designer("Telerik.Web.Design.RadWindowDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[DefaultProperty("NavigateUrl")]
	public class RadWindow : RadWindowBase, INamingContainer, IMarkableStateManager, IStateManager
	{
		// Token: 0x17002948 RID: 10568
		// (get) Token: 0x06008298 RID: 33432 RVA: 0x001DC582 File Offset: 0x001DA782
		// (set) Token: 0x06008299 RID: 33433 RVA: 0x001DC5A2 File Offset: 0x001DA7A2
		[DefaultValue("")]
		[Category("Behavior")]
		[ClientControlProperty]
		public string OpenerElementID
		{
			get
			{
				return ((string)this.ViewState["OpenerElementID"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OpenerElementID"] = value;
			}
		}

		// Token: 0x17002949 RID: 10569
		// (get) Token: 0x0600829A RID: 33434 RVA: 0x001DC5B5 File Offset: 0x001DA7B5
		// (set) Token: 0x0600829B RID: 33435 RVA: 0x001DC5D5 File Offset: 0x001DA7D5
		[UrlProperty]
		[Description("Specifies the URL that will be loaded in the RadWindow")]
		[ClientControlProperty]
		[Browsable(true)]
		[Bindable(true)]
		[Category("Navigation")]
		[DefaultValue("")]
		public string NavigateUrl
		{
			get
			{
				return ((string)this.ViewState["NavigateUrl"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["NavigateUrl"] = value;
			}
		}

		// Token: 0x1700294A RID: 10570
		// (get) Token: 0x0600829C RID: 33436 RVA: 0x001DC5E8 File Offset: 0x001DA7E8
		private bool _dockMode
		{
			get
			{
				return this.ContentContainer.HasControls();
			}
		}

		// Token: 0x1700294B RID: 10571
		// (get) Token: 0x0600829D RID: 33437 RVA: 0x001DC5F5 File Offset: 0x001DA7F5
		[Browsable(false)]
		public SingleTemplateContainer ContentContainer
		{
			get
			{
				this.EnsureChildControls();
				return this._contentContainer;
			}
		}

		// Token: 0x0600829E RID: 33438 RVA: 0x001DC604 File Offset: 0x001DA804
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			this._contentContainer = new SingleTemplateContainer(this);
			this._contentContainer.ID = "C";
			this._contentContainer.Style.Add("display", "none");
			this.Controls.Add(this._contentContainer);
		}

		// Token: 0x1700294C RID: 10572
		// (get) Token: 0x0600829F RID: 33439 RVA: 0x001DC65E File Offset: 0x001DA85E
		// (set) Token: 0x060082A0 RID: 33440 RVA: 0x001DC671 File Offset: 0x001DA871
		[TemplateInstance(TemplateInstance.Single)]
		[TemplateContainer(typeof(SingleTemplateContainer))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Bindable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Browsable(false)]
		public ITemplate ContentTemplate
		{
			get
			{
				this.EnsureChildControls();
				return this.ContentContainer.Template;
			}
			set
			{
				this.EnsureChildControls();
				this.ContentContainer.Template = value;
			}
		}

		// Token: 0x060082A1 RID: 33441 RVA: 0x001DC688 File Offset: 0x001DA888
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			if (base.Style["z-index"] != null)
			{
				descriptor.AddProperty("_stylezindex", base.Style["z-index"]);
			}
			descriptor.AddProperty("_dockMode", this._dockMode);
		}

		// Token: 0x060082A2 RID: 33442 RVA: 0x001DC6E0 File Offset: 0x001DA8E0
		protected override void ControlPreRender()
		{
			if (!this._dockMode)
			{
				this.DataBind();
			}
			base.ControlPreRender();
			bool flag = (WindowBehaviors.Reload & base.Behaviors) == WindowBehaviors.Reload && this._dockMode;
			if (flag)
			{
				base.Behaviors &= ~WindowBehaviors.Reload;
			}
		}

		// Token: 0x1700294D RID: 10573
		// (get) Token: 0x060082A3 RID: 33443 RVA: 0x001DC72A File Offset: 0x001DA92A
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return base.IsTrackingViewState;
			}
		}

		// Token: 0x060082A4 RID: 33444 RVA: 0x001DC732 File Offset: 0x001DA932
		void IStateManager.LoadViewState(object state)
		{
			this.LoadViewState(state);
		}

		// Token: 0x060082A5 RID: 33445 RVA: 0x001DC73B File Offset: 0x001DA93B
		object IStateManager.SaveViewState()
		{
			return this.SaveViewState();
		}

		// Token: 0x060082A6 RID: 33446 RVA: 0x001DC743 File Offset: 0x001DA943
		void IStateManager.TrackViewState()
		{
			this.TrackViewState();
		}

		// Token: 0x060082A7 RID: 33447 RVA: 0x001DC74B File Offset: 0x001DA94B
		void IMarkableStateManager.SetDirty()
		{
			this.ViewState.SetDirty(true);
		}

		// Token: 0x060082A8 RID: 33448 RVA: 0x001DC759 File Offset: 0x001DA959
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<string>(descriptor, "navigateUrl", base.ResolveClientUrl(this.NavigateUrl), "");
			base.DescribeProperty<string>(descriptor, "openerElementID", this.OpenerElementID, "");
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x060082A9 RID: 33449 RVA: 0x001DC796 File Offset: 0x001DA996
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x040023FC RID: 9212
		private SingleTemplateContainer _contentContainer;
	}
}
