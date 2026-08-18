using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web.Util;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000441 RID: 1089
	[DefaultProperty("ImageUrl")]
	[Designer("System.Web.UI.Design.WebControls.PreviewControlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class Image : WebControl
	{
		// Token: 0x0600349B RID: 13467 RVA: 0x000AAF3C File Offset: 0x000A913C
		public Image() : base(HtmlTextWriterTag.Img)
		{
		}

		// Token: 0x17000F47 RID: 3911
		// (get) Token: 0x0600349C RID: 13468 RVA: 0x000AAF48 File Offset: 0x000A9148
		// (set) Token: 0x0600349D RID: 13469 RVA: 0x000AAF75 File Offset: 0x000A9175
		[Localizable(true)]
		[Bindable(true)]
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[WebSysDescription("Image_AlternateText")]
		public virtual string AlternateText
		{
			get
			{
				string text = (string)this.ViewState["AlternateText"];
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["AlternateText"] = value;
			}
		}

		// Token: 0x17000F48 RID: 3912
		// (get) Token: 0x0600349E RID: 13470 RVA: 0x000AAF88 File Offset: 0x000A9188
		// (set) Token: 0x0600349F RID: 13471 RVA: 0x000AAFB5 File Offset: 0x000A91B5
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		[WebCategory("Accessibility")]
		[WebSysDescription("Image_DescriptionUrl")]
		public virtual string DescriptionUrl
		{
			get
			{
				string text = (string)this.ViewState["DescriptionUrl"];
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["DescriptionUrl"] = value;
			}
		}

		// Token: 0x17000F49 RID: 3913
		// (get) Token: 0x060034A0 RID: 13472 RVA: 0x00083455 File Offset: 0x00081655
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override FontInfo Font
		{
			get
			{
				return base.Font;
			}
		}

		// Token: 0x17000F4A RID: 3914
		// (get) Token: 0x060034A1 RID: 13473 RVA: 0x00085F4D File Offset: 0x0008414D
		// (set) Token: 0x060034A2 RID: 13474 RVA: 0x000AAFC8 File Offset: 0x000A91C8
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Enabled
		{
			get
			{
				return base.Enabled;
			}
			set
			{
				base.Enabled = value;
			}
		}

		// Token: 0x17000F4B RID: 3915
		// (get) Token: 0x060034A3 RID: 13475 RVA: 0x000AAFD4 File Offset: 0x000A91D4
		// (set) Token: 0x060034A4 RID: 13476 RVA: 0x000AAFFD File Offset: 0x000A91FD
		[DefaultValue(false)]
		[WebCategory("Accessibility")]
		[WebSysDescription("Image_GenerateEmptyAlternateText")]
		public virtual bool GenerateEmptyAlternateText
		{
			get
			{
				object obj = this.ViewState["GenerateEmptyAlternateText"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["GenerateEmptyAlternateText"] = value;
			}
		}

		// Token: 0x17000F4C RID: 3916
		// (get) Token: 0x060034A5 RID: 13477 RVA: 0x000AB018 File Offset: 0x000A9218
		// (set) Token: 0x060034A6 RID: 13478 RVA: 0x000AB041 File Offset: 0x000A9241
		[WebCategory("Layout")]
		[DefaultValue(ImageAlign.NotSet)]
		[WebSysDescription("Image_ImageAlign")]
		public virtual ImageAlign ImageAlign
		{
			get
			{
				object obj = this.ViewState["ImageAlign"];
				if (obj != null)
				{
					return (ImageAlign)obj;
				}
				return ImageAlign.NotSet;
			}
			set
			{
				if (value < ImageAlign.NotSet || value > ImageAlign.TextTop)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["ImageAlign"] = value;
			}
		}

		// Token: 0x17000F4D RID: 3917
		// (get) Token: 0x060034A7 RID: 13479 RVA: 0x000AB070 File Offset: 0x000A9270
		// (set) Token: 0x060034A8 RID: 13480 RVA: 0x000A9D8D File Offset: 0x000A7F8D
		[Bindable(true)]
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		[WebSysDescription("Image_ImageUrl")]
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

		// Token: 0x17000F4E RID: 3918
		// (get) Token: 0x060034A9 RID: 13481 RVA: 0x000853AC File Offset: 0x000835AC
		public override bool SupportsDisabledAttribute
		{
			get
			{
				return this.RenderingCompatibility < VersionUtil.Framework40;
			}
		}

		// Token: 0x17000F4F RID: 3919
		// (get) Token: 0x060034AA RID: 13482 RVA: 0x000AB09D File Offset: 0x000A929D
		// (set) Token: 0x060034AB RID: 13483 RVA: 0x000AB0A5 File Offset: 0x000A92A5
		internal bool UrlResolved
		{
			get
			{
				return this._urlResolved;
			}
			set
			{
				this._urlResolved = value;
			}
		}

		// Token: 0x060034AC RID: 13484 RVA: 0x000AB0B0 File Offset: 0x000A92B0
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
			string text = this.ImageUrl;
			if (!this.UrlResolved)
			{
				text = base.ResolveClientUrl(text);
			}
			if (this.RenderingCompatibility >= VersionUtil.Framework45)
			{
				if (!string.IsNullOrEmpty(text) || base.DesignMode)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Src, text);
				}
			}
			else if (text.Length > 0 || !base.EnableLegacyRendering)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Src, text);
			}
			text = this.DescriptionUrl;
			if (text.Length != 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Longdesc, base.ResolveClientUrl(text));
			}
			text = this.AlternateText;
			if (text.Length > 0 || this.GenerateEmptyAlternateText)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Alt, text);
			}
			ImageAlign imageAlign = this.ImageAlign;
			if (imageAlign != ImageAlign.NotSet)
			{
				string value;
				switch (imageAlign)
				{
				case ImageAlign.Left:
					value = "left";
					break;
				case ImageAlign.Right:
					value = "right";
					break;
				case ImageAlign.Baseline:
					value = "baseline";
					break;
				case ImageAlign.Top:
					value = "top";
					break;
				case ImageAlign.Middle:
					value = "middle";
					break;
				case ImageAlign.Bottom:
					value = "bottom";
					break;
				case ImageAlign.AbsBottom:
					value = "absbottom";
					break;
				case ImageAlign.AbsMiddle:
					value = "absmiddle";
					break;
				default:
					value = "texttop";
					break;
				}
				writer.AddAttribute(HtmlTextWriterAttribute.Align, value);
			}
			if (this.BorderWidth.IsEmpty && this.RenderingCompatibility < VersionUtil.Framework40)
			{
				if (base.EnableLegacyRendering)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Border, "0", false);
					return;
				}
				writer.AddStyleAttribute(HtmlTextWriterStyle.BorderWidth, "0px");
			}
		}

		// Token: 0x060034AD RID: 13485 RVA: 0x00006164 File Offset: 0x00004364
		protected internal override void RenderContents(HtmlTextWriter writer)
		{
		}

		// Token: 0x0400219F RID: 8607
		private bool _urlResolved;
	}
}
