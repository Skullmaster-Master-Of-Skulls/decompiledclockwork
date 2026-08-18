using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200060D RID: 1549
	[Designer("System.Web.UI.Design.WebControls.PanelContainerDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ParseChildren(false)]
	[PersistChildren(true)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class Panel : WebControl
	{
		// Token: 0x06004C86 RID: 19590 RVA: 0x001368CA File Offset: 0x001358CA
		public Panel() : base(HtmlTextWriterTag.Div)
		{
		}

		// Token: 0x1700133C RID: 4924
		// (get) Token: 0x06004C87 RID: 19591 RVA: 0x001368D4 File Offset: 0x001358D4
		// (set) Token: 0x06004C88 RID: 19592 RVA: 0x00136928 File Offset: 0x00135928
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		[WebSysDescription("Panel_BackImageUrl")]
		public virtual string BackImageUrl
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return string.Empty;
				}
				PanelStyle panelStyle = base.ControlStyle as PanelStyle;
				if (panelStyle != null)
				{
					return panelStyle.BackImageUrl;
				}
				string text = (string)this.ViewState["BackImageUrl"];
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				PanelStyle panelStyle = base.ControlStyle as PanelStyle;
				if (panelStyle != null)
				{
					panelStyle.BackImageUrl = value;
					return;
				}
				this.ViewState["BackImageUrl"] = value;
			}
		}

		// Token: 0x1700133D RID: 4925
		// (get) Token: 0x06004C89 RID: 19593 RVA: 0x0013695D File Offset: 0x0013595D
		// (set) Token: 0x06004C8A RID: 19594 RVA: 0x00136973 File Offset: 0x00135973
		[DefaultValue("")]
		[WebSysDescription("Panel_DefaultButton")]
		[Themeable(false)]
		[WebCategory("Behavior")]
		public virtual string DefaultButton
		{
			get
			{
				if (this._defaultButton == null)
				{
					return string.Empty;
				}
				return this._defaultButton;
			}
			set
			{
				this._defaultButton = value;
			}
		}

		// Token: 0x1700133E RID: 4926
		// (get) Token: 0x06004C8B RID: 19595 RVA: 0x0013697C File Offset: 0x0013597C
		// (set) Token: 0x06004C8C RID: 19596 RVA: 0x001369C8 File Offset: 0x001359C8
		[DefaultValue(ContentDirection.NotSet)]
		[WebCategory("Layout")]
		[WebSysDescription("Panel_Direction")]
		public virtual ContentDirection Direction
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return ContentDirection.NotSet;
				}
				PanelStyle panelStyle = base.ControlStyle as PanelStyle;
				if (panelStyle != null)
				{
					return panelStyle.Direction;
				}
				object obj = this.ViewState["Direction"];
				if (obj != null)
				{
					return (ContentDirection)obj;
				}
				return ContentDirection.NotSet;
			}
			set
			{
				PanelStyle panelStyle = base.ControlStyle as PanelStyle;
				if (panelStyle != null)
				{
					panelStyle.Direction = value;
					return;
				}
				this.ViewState["Direction"] = value;
			}
		}

		// Token: 0x1700133F RID: 4927
		// (get) Token: 0x06004C8D RID: 19597 RVA: 0x00136A04 File Offset: 0x00135A04
		// (set) Token: 0x06004C8E RID: 19598 RVA: 0x00136A31 File Offset: 0x00135A31
		[WebCategory("Appearance")]
		[Localizable(true)]
		[WebSysDescription("Panel_GroupingText")]
		[DefaultValue("")]
		public virtual string GroupingText
		{
			get
			{
				string text = (string)this.ViewState["GroupingText"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				this.ViewState["GroupingText"] = value;
			}
		}

		// Token: 0x17001340 RID: 4928
		// (get) Token: 0x06004C8F RID: 19599 RVA: 0x00136A44 File Offset: 0x00135A44
		// (set) Token: 0x06004C90 RID: 19600 RVA: 0x00136A90 File Offset: 0x00135A90
		[WebSysDescription("Panel_HorizontalAlign")]
		[WebCategory("Layout")]
		[DefaultValue(HorizontalAlign.NotSet)]
		public virtual HorizontalAlign HorizontalAlign
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return HorizontalAlign.NotSet;
				}
				PanelStyle panelStyle = base.ControlStyle as PanelStyle;
				if (panelStyle != null)
				{
					return panelStyle.HorizontalAlign;
				}
				object obj = this.ViewState["HorizontalAlign"];
				if (obj != null)
				{
					return (HorizontalAlign)obj;
				}
				return HorizontalAlign.NotSet;
			}
			set
			{
				PanelStyle panelStyle = base.ControlStyle as PanelStyle;
				if (panelStyle != null)
				{
					panelStyle.HorizontalAlign = value;
					return;
				}
				if (value < HorizontalAlign.NotSet || value > HorizontalAlign.Justify)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["HorizontalAlign"] = value;
			}
		}

		// Token: 0x17001341 RID: 4929
		// (get) Token: 0x06004C91 RID: 19601 RVA: 0x00136AE0 File Offset: 0x00135AE0
		// (set) Token: 0x06004C92 RID: 19602 RVA: 0x00136B2C File Offset: 0x00135B2C
		[WebCategory("Layout")]
		[WebSysDescription("Panel_ScrollBars")]
		[DefaultValue(ScrollBars.None)]
		public virtual ScrollBars ScrollBars
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return ScrollBars.None;
				}
				PanelStyle panelStyle = base.ControlStyle as PanelStyle;
				if (panelStyle != null)
				{
					return panelStyle.ScrollBars;
				}
				object obj = this.ViewState["ScrollBars"];
				if (obj != null)
				{
					return (ScrollBars)obj;
				}
				return ScrollBars.None;
			}
			set
			{
				PanelStyle panelStyle = base.ControlStyle as PanelStyle;
				if (panelStyle != null)
				{
					panelStyle.ScrollBars = value;
					return;
				}
				this.ViewState["ScrollBars"] = value;
			}
		}

		// Token: 0x17001342 RID: 4930
		// (get) Token: 0x06004C93 RID: 19603 RVA: 0x00136B68 File Offset: 0x00135B68
		// (set) Token: 0x06004C94 RID: 19604 RVA: 0x00136BB4 File Offset: 0x00135BB4
		[WebSysDescription("Panel_Wrap")]
		[DefaultValue(true)]
		[WebCategory("Layout")]
		public virtual bool Wrap
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return true;
				}
				PanelStyle panelStyle = base.ControlStyle as PanelStyle;
				if (panelStyle != null)
				{
					return panelStyle.Wrap;
				}
				object obj = this.ViewState["Wrap"];
				return obj == null || (bool)obj;
			}
			set
			{
				PanelStyle panelStyle = base.ControlStyle as PanelStyle;
				if (panelStyle != null)
				{
					panelStyle.Wrap = value;
					return;
				}
				this.ViewState["Wrap"] = value;
			}
		}

		// Token: 0x06004C95 RID: 19605 RVA: 0x00136BF0 File Offset: 0x00135BF0
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
			string backImageUrl = this.BackImageUrl;
			if (backImageUrl.Trim().Length > 0)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.BackgroundImage, "url(" + base.ResolveClientUrl(backImageUrl) + ")");
			}
			this.AddScrollingAttribute(this.ScrollBars, writer);
			HorizontalAlign horizontalAlign = this.HorizontalAlign;
			if (horizontalAlign != HorizontalAlign.NotSet)
			{
				TypeConverter converter = TypeDescriptor.GetConverter(typeof(HorizontalAlign));
				writer.AddStyleAttribute(HtmlTextWriterStyle.TextAlign, converter.ConvertToInvariantString(horizontalAlign).ToLowerInvariant());
			}
			if (!this.Wrap)
			{
				if (base.EnableLegacyRendering)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Nowrap, "nowrap", false);
				}
				else
				{
					writer.AddStyleAttribute(HtmlTextWriterStyle.WhiteSpace, "nowrap");
				}
			}
			if (this.Direction == ContentDirection.LeftToRight)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Dir, "ltr");
			}
			else if (this.Direction == ContentDirection.RightToLeft)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Dir, "rtl");
			}
			if (base.DesignMode || this.Page == null || this.Page.RequestInternal == null || this.Page.Request.Browser.EcmaScriptVersion.Major <= 0 || this.Page.Request.Browser.W3CDomVersion.Major <= 0 || this.DefaultButton.Length <= 0)
			{
				return;
			}
			Control control = this.FindControl(this.DefaultButton);
			if (control is IButtonControl)
			{
				this.Page.ClientScript.RegisterDefaultButtonScript(control, writer, true);
				return;
			}
			throw new InvalidOperationException(SR.GetString("HtmlForm_OnlyIButtonControlCanBeDefaultButton", new object[]
			{
				this.ID
			}));
		}

		// Token: 0x06004C96 RID: 19606 RVA: 0x00136D8C File Offset: 0x00135D8C
		private void AddScrollingAttribute(ScrollBars scrollBars, HtmlTextWriter writer)
		{
			switch (scrollBars)
			{
			case ScrollBars.Horizontal:
				writer.AddStyleAttribute(HtmlTextWriterStyle.OverflowX, "scroll");
				return;
			case ScrollBars.Vertical:
				writer.AddStyleAttribute(HtmlTextWriterStyle.OverflowY, "scroll");
				return;
			case ScrollBars.Both:
				writer.AddStyleAttribute(HtmlTextWriterStyle.Overflow, "scroll");
				return;
			case ScrollBars.Auto:
				writer.AddStyleAttribute(HtmlTextWriterStyle.Overflow, "auto");
				return;
			default:
				return;
			}
		}

		// Token: 0x06004C97 RID: 19607 RVA: 0x00136DEB File Offset: 0x00135DEB
		protected override Style CreateControlStyle()
		{
			return new PanelStyle(this.ViewState);
		}

		// Token: 0x06004C98 RID: 19608 RVA: 0x00136DF8 File Offset: 0x00135DF8
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			this.AddAttributesToRender(writer);
			HtmlTextWriterTag tagKey = this.TagKey;
			if (tagKey != HtmlTextWriterTag.Unknown)
			{
				writer.RenderBeginTag(tagKey);
			}
			else
			{
				writer.RenderBeginTag(this.TagName);
			}
			string groupingText = this.GroupingText;
			bool flag = groupingText.Length != 0 && !(writer is Html32TextWriter);
			if (flag)
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Fieldset);
				this._renderedFieldSet = true;
				writer.RenderBeginTag(HtmlTextWriterTag.Legend);
				writer.Write(groupingText);
				writer.RenderEndTag();
			}
		}

		// Token: 0x06004C99 RID: 19609 RVA: 0x00136E71 File Offset: 0x00135E71
		public override void RenderEndTag(HtmlTextWriter writer)
		{
			if (this._renderedFieldSet)
			{
				writer.RenderEndTag();
			}
			base.RenderEndTag(writer);
		}

		// Token: 0x04002C08 RID: 11272
		private string _defaultButton;

		// Token: 0x04002C09 RID: 11273
		private bool _renderedFieldSet;
	}
}
