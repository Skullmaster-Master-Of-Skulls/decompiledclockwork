using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x020005B9 RID: 1465
	[DataBindingHandler("System.Web.UI.Design.HyperLinkDataBindingHandler, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultProperty("Text")]
	[Designer("System.Web.UI.Design.WebControls.HyperLinkDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ParseChildren(false)]
	[ToolboxData("<{0}:HyperLink runat=\"server\">HyperLink</{0}:HyperLink>")]
	[ControlBuilder(typeof(HyperLinkControlBuilder))]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HyperLink : WebControl
	{
		// Token: 0x06004792 RID: 18322 RVA: 0x001247B4 File Offset: 0x001237B4
		public HyperLink() : base(HtmlTextWriterTag.A)
		{
		}

		// Token: 0x170011A0 RID: 4512
		// (get) Token: 0x06004793 RID: 18323 RVA: 0x001247C0 File Offset: 0x001237C0
		// (set) Token: 0x06004794 RID: 18324 RVA: 0x001247ED File Offset: 0x001237ED
		[WebSysDescription("HyperLink_ImageUrl")]
		[UrlProperty]
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[Bindable(true)]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public virtual string ImageUrl
		{
			get
			{
				string text = (string)this.ViewState["ImageUrl"];
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["ImageUrl"] = value;
			}
		}

		// Token: 0x170011A1 RID: 4513
		// (get) Token: 0x06004795 RID: 18325 RVA: 0x00124800 File Offset: 0x00123800
		// (set) Token: 0x06004796 RID: 18326 RVA: 0x0012482D File Offset: 0x0012382D
		[WebSysDescription("HyperLink_NavigateUrl")]
		[UrlProperty]
		[Bindable(true)]
		[WebCategory("Navigation")]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public string NavigateUrl
		{
			get
			{
				string text = (string)this.ViewState["NavigateUrl"];
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["NavigateUrl"] = value;
			}
		}

		// Token: 0x170011A2 RID: 4514
		// (get) Token: 0x06004797 RID: 18327 RVA: 0x00124840 File Offset: 0x00123840
		internal override bool RequiresLegacyRendering
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170011A3 RID: 4515
		// (get) Token: 0x06004798 RID: 18328 RVA: 0x00124844 File Offset: 0x00123844
		// (set) Token: 0x06004799 RID: 18329 RVA: 0x00124871 File Offset: 0x00123871
		[TypeConverter(typeof(TargetConverter))]
		[DefaultValue("")]
		[WebCategory("Navigation")]
		[WebSysDescription("HyperLink_Target")]
		public string Target
		{
			get
			{
				string text = (string)this.ViewState["Target"];
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["Target"] = value;
			}
		}

		// Token: 0x170011A4 RID: 4516
		// (get) Token: 0x0600479A RID: 18330 RVA: 0x00124884 File Offset: 0x00123884
		// (set) Token: 0x0600479B RID: 18331 RVA: 0x001248B1 File Offset: 0x001238B1
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		[DefaultValue("")]
		[WebSysDescription("HyperLink_Text")]
		[Localizable(true)]
		[Bindable(true)]
		[WebCategory("Appearance")]
		public virtual string Text
		{
			get
			{
				object obj = this.ViewState["Text"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				if (this.HasControls())
				{
					this.Controls.Clear();
				}
				this.ViewState["Text"] = value;
			}
		}

		// Token: 0x0600479C RID: 18332 RVA: 0x001248D8 File Offset: 0x001238D8
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			if (this.Enabled && !base.IsEnabled)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");
			}
			base.AddAttributesToRender(writer);
			string text = this.NavigateUrl;
			if (text.Length > 0 && base.IsEnabled)
			{
				string value = base.ResolveClientUrl(text);
				writer.AddAttribute(HtmlTextWriterAttribute.Href, value);
			}
			text = this.Target;
			if (text.Length > 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Target, text);
			}
		}

		// Token: 0x0600479D RID: 18333 RVA: 0x0012494C File Offset: 0x0012394C
		protected override void AddParsedSubObject(object obj)
		{
			if (this.HasControls())
			{
				base.AddParsedSubObject(obj);
				return;
			}
			if (!(obj is LiteralControl))
			{
				string text = this.Text;
				if (text.Length != 0)
				{
					this.Text = null;
					base.AddParsedSubObject(new LiteralControl(text));
				}
				base.AddParsedSubObject(obj);
				return;
			}
			if (base.DesignMode)
			{
				if (this._textSetByAddParsedSubObject)
				{
					this.Text += ((LiteralControl)obj).Text;
				}
				else
				{
					this.Text = ((LiteralControl)obj).Text;
				}
				this._textSetByAddParsedSubObject = true;
				return;
			}
			this.Text = ((LiteralControl)obj).Text;
		}

		// Token: 0x0600479E RID: 18334 RVA: 0x001249F4 File Offset: 0x001239F4
		protected override void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				base.LoadViewState(savedState);
				string text = (string)this.ViewState["Text"];
				if (text != null)
				{
					this.Text = text;
				}
			}
		}

		// Token: 0x0600479F RID: 18335 RVA: 0x00124A2C File Offset: 0x00123A2C
		protected internal override void RenderContents(HtmlTextWriter writer)
		{
			string text = this.ImageUrl;
			if (text.Length > 0)
			{
				Image image = new Image();
				image.ImageUrl = base.ResolveClientUrl(text);
				if (this.Context != null && this.Context.WorkerRequest != null && this.Context.WorkerRequest.IsRewriteModuleEnabled)
				{
					image.UrlResolved = true;
				}
				text = this.ToolTip;
				if (text.Length != 0)
				{
					image.ToolTip = text;
				}
				text = this.Text;
				if (text.Length != 0)
				{
					image.AlternateText = text;
				}
				image.RenderControl(writer);
				return;
			}
			if (base.HasRenderingData())
			{
				base.RenderContents(writer);
				return;
			}
			writer.Write(this.Text);
		}

		// Token: 0x04002AA9 RID: 10921
		private bool _textSetByAddParsedSubObject;
	}
}
