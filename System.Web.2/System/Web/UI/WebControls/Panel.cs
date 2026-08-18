using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web.Util;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200049A RID: 1178
	[Designer("System.Web.UI.Design.WebControls.PanelContainerDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ParseChildren(false)]
	[PersistChildren(true)]
	public class Panel : WebControl
	{
		// Token: 0x06003A83 RID: 14979 RVA: 0x000BDC06 File Offset: 0x000BBE06
		public Panel() : base(HtmlTextWriterTag.Div)
		{
		}

		// Token: 0x17001116 RID: 4374
		// (get) Token: 0x06003A84 RID: 14980 RVA: 0x000BDC10 File Offset: 0x000BBE10
		// (set) Token: 0x06003A85 RID: 14981 RVA: 0x000BDC64 File Offset: 0x000BBE64
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
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

		// Token: 0x17001117 RID: 4375
		// (get) Token: 0x06003A86 RID: 14982 RVA: 0x000BDC99 File Offset: 0x000BBE99
		// (set) Token: 0x06003A87 RID: 14983 RVA: 0x000BDCAF File Offset: 0x000BBEAF
		[DefaultValue("")]
		[Themeable(false)]
		[WebCategory("Behavior")]
		[WebSysDescription("Panel_DefaultButton")]
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

		// Token: 0x17001118 RID: 4376
		// (get) Token: 0x06003A88 RID: 14984 RVA: 0x000BDCB8 File Offset: 0x000BBEB8
		// (set) Token: 0x06003A89 RID: 14985 RVA: 0x000BDD04 File Offset: 0x000BBF04
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

		// Token: 0x17001119 RID: 4377
		// (get) Token: 0x06003A8A RID: 14986 RVA: 0x000BDD40 File Offset: 0x000BBF40
		// (set) Token: 0x06003A8B RID: 14987 RVA: 0x000BDD6D File Offset: 0x000BBF6D
		[Localizable(true)]
		[DefaultValue("")]
		[WebCategory("Appearance")]
		[WebSysDescription("Panel_GroupingText")]
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

		// Token: 0x1700111A RID: 4378
		// (get) Token: 0x06003A8C RID: 14988 RVA: 0x000BDD80 File Offset: 0x000BBF80
		// (set) Token: 0x06003A8D RID: 14989 RVA: 0x000BDDCC File Offset: 0x000BBFCC
		[WebCategory("Layout")]
		[DefaultValue(HorizontalAlign.NotSet)]
		[WebSysDescription("Panel_HorizontalAlign")]
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

		// Token: 0x1700111B RID: 4379
		// (get) Token: 0x06003A8E RID: 14990 RVA: 0x000853AC File Offset: 0x000835AC
		public override bool SupportsDisabledAttribute
		{
			get
			{
				return this.RenderingCompatibility < VersionUtil.Framework40;
			}
		}

		// Token: 0x1700111C RID: 4380
		// (get) Token: 0x06003A8F RID: 14991 RVA: 0x000BDE1C File Offset: 0x000BC01C
		// (set) Token: 0x06003A90 RID: 14992 RVA: 0x000BDE68 File Offset: 0x000BC068
		[DefaultValue(ScrollBars.None)]
		[WebCategory("Layout")]
		[WebSysDescription("Panel_ScrollBars")]
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

		// Token: 0x1700111D RID: 4381
		// (get) Token: 0x06003A91 RID: 14993 RVA: 0x000BDEA4 File Offset: 0x000BC0A4
		// (set) Token: 0x06003A92 RID: 14994 RVA: 0x000BDEF0 File Offset: 0x000BC0F0
		[WebCategory("Layout")]
		[DefaultValue(true)]
		[WebSysDescription("Panel_Wrap")]
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

		// Token: 0x06003A93 RID: 14995 RVA: 0x000BDF2C File Offset: 0x000BC12C
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
			Control control = base.FindControlFromPageIfNecessary(this.DefaultButton);
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

		// Token: 0x06003A94 RID: 14996 RVA: 0x000BE0C4 File Offset: 0x000BC2C4
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

		// Token: 0x06003A95 RID: 14997 RVA: 0x000BE121 File Offset: 0x000BC321
		protected override Style CreateControlStyle()
		{
			return new PanelStyle(this.ViewState);
		}

		// Token: 0x06003A96 RID: 14998 RVA: 0x000BE130 File Offset: 0x000BC330
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

		// Token: 0x06003A97 RID: 14999 RVA: 0x000BE1A9 File Offset: 0x000BC3A9
		public override void RenderEndTag(HtmlTextWriter writer)
		{
			if (this._renderedFieldSet)
			{
				writer.RenderEndTag();
			}
			base.RenderEndTag(writer);
		}

		// Token: 0x04002302 RID: 8962
		private string _defaultButton;

		// Token: 0x04002303 RID: 8963
		private bool _renderedFieldSet;
	}
}
