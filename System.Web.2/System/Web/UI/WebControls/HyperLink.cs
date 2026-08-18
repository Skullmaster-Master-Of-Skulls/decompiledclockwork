using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web.Util;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000436 RID: 1078
	[ControlBuilder(typeof(HyperLinkControlBuilder))]
	[DataBindingHandler("System.Web.UI.Design.HyperLinkDataBindingHandler, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultProperty("Text")]
	[Designer("System.Web.UI.Design.WebControls.HyperLinkDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ToolboxData("<{0}:HyperLink runat=\"server\">HyperLink</{0}:HyperLink>")]
	[ParseChildren(false)]
	public class HyperLink : WebControl
	{
		// Token: 0x0600342F RID: 13359 RVA: 0x000A9D56 File Offset: 0x000A7F56
		public HyperLink() : base(HtmlTextWriterTag.A)
		{
		}

		// Token: 0x17000F1C RID: 3868
		// (get) Token: 0x06003430 RID: 13360 RVA: 0x000A9D60 File Offset: 0x000A7F60
		// (set) Token: 0x06003431 RID: 13361 RVA: 0x000A9D8D File Offset: 0x000A7F8D
		[Bindable(true)]
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		[WebSysDescription("HyperLink_ImageUrl")]
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

		// Token: 0x17000F1D RID: 3869
		// (get) Token: 0x06003432 RID: 13362 RVA: 0x000A9DA0 File Offset: 0x000A7FA0
		// (set) Token: 0x06003433 RID: 13363 RVA: 0x000A9DCD File Offset: 0x000A7FCD
		[WebCategory("Appearance")]
		[DefaultValue(typeof(Unit), "")]
		[WebSysDescription("HyperLink_ImageHeight")]
		public virtual Unit ImageHeight
		{
			get
			{
				object obj = this.ViewState["ImageHeight"];
				if (obj != null)
				{
					return (Unit)obj;
				}
				return Unit.Empty;
			}
			set
			{
				this.ViewState["ImageHeight"] = value;
			}
		}

		// Token: 0x17000F1E RID: 3870
		// (get) Token: 0x06003434 RID: 13364 RVA: 0x000A9DE8 File Offset: 0x000A7FE8
		// (set) Token: 0x06003435 RID: 13365 RVA: 0x000A9E15 File Offset: 0x000A8015
		[WebCategory("Appearance")]
		[DefaultValue(typeof(Unit), "")]
		[WebSysDescription("HyperLink_ImageWidth")]
		public virtual Unit ImageWidth
		{
			get
			{
				object obj = this.ViewState["ImageWidth"];
				if (obj != null)
				{
					return (Unit)obj;
				}
				return Unit.Empty;
			}
			set
			{
				this.ViewState["ImageWidth"] = value;
			}
		}

		// Token: 0x17000F1F RID: 3871
		// (get) Token: 0x06003436 RID: 13366 RVA: 0x000A9E30 File Offset: 0x000A8030
		// (set) Token: 0x06003437 RID: 13367 RVA: 0x000A9E5D File Offset: 0x000A805D
		[Bindable(true)]
		[WebCategory("Navigation")]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		[WebSysDescription("HyperLink_NavigateUrl")]
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

		// Token: 0x17000F20 RID: 3872
		// (get) Token: 0x06003438 RID: 13368 RVA: 0x000853AC File Offset: 0x000835AC
		public override bool SupportsDisabledAttribute
		{
			get
			{
				return this.RenderingCompatibility < VersionUtil.Framework40;
			}
		}

		// Token: 0x17000F21 RID: 3873
		// (get) Token: 0x06003439 RID: 13369 RVA: 0x000097B7 File Offset: 0x000079B7
		internal override bool RequiresLegacyRendering
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000F22 RID: 3874
		// (get) Token: 0x0600343A RID: 13370 RVA: 0x000A9E70 File Offset: 0x000A8070
		// (set) Token: 0x0600343B RID: 13371 RVA: 0x000835A9 File Offset: 0x000817A9
		[WebCategory("Navigation")]
		[DefaultValue("")]
		[WebSysDescription("HyperLink_Target")]
		[TypeConverter(typeof(TargetConverter))]
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

		// Token: 0x17000F23 RID: 3875
		// (get) Token: 0x0600343C RID: 13372 RVA: 0x000A9EA0 File Offset: 0x000A80A0
		// (set) Token: 0x0600343D RID: 13373 RVA: 0x000A9ECD File Offset: 0x000A80CD
		[Localizable(true)]
		[Bindable(true)]
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[WebSysDescription("HyperLink_Text")]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
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

		// Token: 0x0600343E RID: 13374 RVA: 0x000A9EF4 File Offset: 0x000A80F4
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			if (this.Enabled && !base.IsEnabled && this.SupportsDisabledAttribute)
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

		// Token: 0x0600343F RID: 13375 RVA: 0x000A9F70 File Offset: 0x000A8170
		protected override void AddParsedSubObject(object obj)
		{
			if (this.HasControls())
			{
				base.AddParsedSubObject(obj);
				return;
			}
			if (obj is LiteralControl)
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
			string text = this.Text;
			if (text.Length != 0)
			{
				this.Text = null;
				base.AddParsedSubObject(new LiteralControl(text));
			}
			base.AddParsedSubObject(obj);
		}

		// Token: 0x06003440 RID: 13376 RVA: 0x000AA000 File Offset: 0x000A8200
		protected override void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				base.LoadViewState(savedState);
				string text = (string)this.ViewState["Text"];
				if (text != null && this.HasControls())
				{
					this.Controls.Clear();
				}
			}
		}

		// Token: 0x06003441 RID: 13377 RVA: 0x000AA044 File Offset: 0x000A8244
		protected internal override void RenderContents(HtmlTextWriter writer)
		{
			string text = this.ImageUrl;
			if (text.Length > 0)
			{
				Image image = new Image();
				image.ImageUrl = base.ResolveClientUrl(text);
				image.UrlResolved = true;
				image.GenerateEmptyAlternateText = true;
				if (this.ImageHeight != Unit.Empty)
				{
					image.Height = this.ImageHeight;
				}
				if (this.ImageWidth != Unit.Empty)
				{
					image.Width = this.ImageWidth;
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

		// Token: 0x04002198 RID: 8600
		private bool _textSetByAddParsedSubObject;
	}
}
