using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000531 RID: 1329
	[DefaultProperty("ImageUrl")]
	[Designer("System.Web.UI.Design.WebControls.PreviewControlDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class Image : WebControl
	{
		// Token: 0x06004152 RID: 16722 RVA: 0x0010F0BA File Offset: 0x0010E0BA
		public Image() : base(HtmlTextWriterTag.Img)
		{
		}

		// Token: 0x17000FB8 RID: 4024
		// (get) Token: 0x06004153 RID: 16723 RVA: 0x0010F0C4 File Offset: 0x0010E0C4
		// (set) Token: 0x06004154 RID: 16724 RVA: 0x0010F0F1 File Offset: 0x0010E0F1
		[WebSysDescription("Image_AlternateText")]
		[Localizable(true)]
		[Bindable(true)]
		[WebCategory("Appearance")]
		[DefaultValue("")]
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

		// Token: 0x17000FB9 RID: 4025
		// (get) Token: 0x06004155 RID: 16725 RVA: 0x0010F104 File Offset: 0x0010E104
		// (set) Token: 0x06004156 RID: 16726 RVA: 0x0010F131 File Offset: 0x0010E131
		[UrlProperty]
		[WebSysDescription("Image_DescriptionUrl")]
		[WebCategory("Accessibility")]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
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

		// Token: 0x17000FBA RID: 4026
		// (get) Token: 0x06004157 RID: 16727 RVA: 0x0010F144 File Offset: 0x0010E144
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

		// Token: 0x17000FBB RID: 4027
		// (get) Token: 0x06004158 RID: 16728 RVA: 0x0010F14C File Offset: 0x0010E14C
		// (set) Token: 0x06004159 RID: 16729 RVA: 0x0010F154 File Offset: 0x0010E154
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

		// Token: 0x17000FBC RID: 4028
		// (get) Token: 0x0600415A RID: 16730 RVA: 0x0010F160 File Offset: 0x0010E160
		// (set) Token: 0x0600415B RID: 16731 RVA: 0x0010F189 File Offset: 0x0010E189
		[WebCategory("Accessibility")]
		[DefaultValue(false)]
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

		// Token: 0x17000FBD RID: 4029
		// (get) Token: 0x0600415C RID: 16732 RVA: 0x0010F1A4 File Offset: 0x0010E1A4
		// (set) Token: 0x0600415D RID: 16733 RVA: 0x0010F1CD File Offset: 0x0010E1CD
		[DefaultValue(ImageAlign.NotSet)]
		[WebSysDescription("Image_ImageAlign")]
		[WebCategory("Layout")]
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

		// Token: 0x17000FBE RID: 4030
		// (get) Token: 0x0600415E RID: 16734 RVA: 0x0010F1FC File Offset: 0x0010E1FC
		// (set) Token: 0x0600415F RID: 16735 RVA: 0x0010F229 File Offset: 0x0010E229
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		[WebSysDescription("Image_ImageUrl")]
		[Bindable(true)]
		[WebCategory("Appearance")]
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

		// Token: 0x17000FBF RID: 4031
		// (get) Token: 0x06004160 RID: 16736 RVA: 0x0010F23C File Offset: 0x0010E23C
		// (set) Token: 0x06004161 RID: 16737 RVA: 0x0010F244 File Offset: 0x0010E244
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

		// Token: 0x06004162 RID: 16738 RVA: 0x0010F250 File Offset: 0x0010E250
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
			string text = this.ImageUrl;
			if (!this.UrlResolved)
			{
				text = base.ResolveClientUrl(text);
			}
			if (text.Length > 0 || !base.EnableLegacyRendering)
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
			if (this.BorderWidth.IsEmpty)
			{
				if (base.EnableLegacyRendering)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Border, "0", false);
					return;
				}
				writer.AddStyleAttribute(HtmlTextWriterStyle.BorderWidth, "0px");
			}
		}

		// Token: 0x06004163 RID: 16739 RVA: 0x0010F38A File Offset: 0x0010E38A
		protected internal override void RenderContents(HtmlTextWriter writer)
		{
		}

		// Token: 0x040028AF RID: 10415
		private bool _urlResolved;
	}
}
