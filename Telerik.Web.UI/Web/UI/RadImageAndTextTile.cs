using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Licensing;

namespace Telerik.Web.UI
{
	// Token: 0x02000902 RID: 2306
	[Designer("Telerik.Web.Design.RadImageAndTextTileDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[ClientScriptResource("Telerik.Web.UI.RadImageAndTextTile", "Telerik.Web.UI.Tile.RadTileScripts.js")]
	[TelerikToolboxCategory("Navigation")]
	[ToolboxBitmap(typeof(RadImageAndTextTile), "Telerik.Web.UI.TileList.png")]
	public class RadImageAndTextTile : RadBaseTile
	{
		// Token: 0x0600573C RID: 22332 RVA: 0x0010AD4C File Offset: 0x00108F4C
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<string>(descriptor, "imageUrl", base.ResolveClientUrl(this.ImageUrl), "");
			base.DescribeProperty<string>(descriptor, "text", this.Text, "");
			base.DescribeProperty<string>(descriptor, "tileType", this.TileType, null);
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x0600573D RID: 22333 RVA: 0x0010ADA7 File Offset: 0x00108FA7
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x17001CD9 RID: 7385
		// (get) Token: 0x0600573F RID: 22335 RVA: 0x0010ADB8 File Offset: 0x00108FB8
		// (set) Token: 0x06005740 RID: 22336 RVA: 0x0010ADD8 File Offset: 0x00108FD8
		[UrlProperty]
		[ClientControlProperty]
		[DefaultValue("")]
		[Description("Gets or sets the name of the command fired when the tool is clicked.")]
		[Category("Behavior")]
		public string ImageUrl
		{
			get
			{
				return ((string)this.ViewState["ImageUrl"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["ImageUrl"] = value;
			}
		}

		// Token: 0x17001CDA RID: 7386
		// (get) Token: 0x06005741 RID: 22337 RVA: 0x0010ADEB File Offset: 0x00108FEB
		// (set) Token: 0x06005742 RID: 22338 RVA: 0x0010AE10 File Offset: 0x00109010
		[DefaultValue(typeof(Unit), "")]
		[NotifyParentProperty(true)]
		public Unit ImageWidth
		{
			get
			{
				return (Unit)(this.ViewState["ImageWidth"] ?? Unit.Empty);
			}
			set
			{
				this.ViewState["ImageWidth"] = value;
			}
		}

		// Token: 0x17001CDB RID: 7387
		// (get) Token: 0x06005743 RID: 22339 RVA: 0x0010AE28 File Offset: 0x00109028
		// (set) Token: 0x06005744 RID: 22340 RVA: 0x0010AE4D File Offset: 0x0010904D
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(Unit), "")]
		public Unit ImageHeight
		{
			get
			{
				return (Unit)(this.ViewState["ImageHeight"] ?? Unit.Empty);
			}
			set
			{
				this.ViewState["ImageHeight"] = value;
			}
		}

		// Token: 0x17001CDC RID: 7388
		// (get) Token: 0x06005745 RID: 22341 RVA: 0x0010AE65 File Offset: 0x00109065
		// (set) Token: 0x06005746 RID: 22342 RVA: 0x0010AE85 File Offset: 0x00109085
		[Description("Gets or sets the Text which will be rendered in the tile.")]
		[Category("Behavior")]
		[ClientControlProperty]
		[DefaultValue("")]
		public string Text
		{
			get
			{
				return ((string)this.ViewState["Text"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["Text"] = value;
			}
		}

		// Token: 0x17001CDD RID: 7389
		// (get) Token: 0x06005747 RID: 22343 RVA: 0x0010AE98 File Offset: 0x00109098
		[ClientControlProperty]
		internal override string TileType
		{
			get
			{
				return "RadImageAndTextTile";
			}
		}

		// Token: 0x06005748 RID: 22344 RVA: 0x0010AEA0 File Offset: 0x001090A0
		protected override void RenderTileBody(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rtileTopContent");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rtileContentImage");
			writer.AddAttribute(HtmlTextWriterAttribute.Src, base.ResolveClientUrl(this.ImageUrl));
			writer.AddAttribute(HtmlTextWriterAttribute.Alt, "");
			if (this.ImageWidth != Unit.Empty)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Width, this.ImageWidth.ToString());
			}
			if (this.ImageHeight != Unit.Empty)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Height, this.ImageHeight.ToString());
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Img);
			writer.RenderEndTag();
			writer.RenderEndTag();
			writer.AddAttribute(HtmlTextWriterAttribute.Class, this.GetBottomContentCssClass());
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.Write(this.Text);
			writer.RenderEndTag();
		}

		// Token: 0x06005749 RID: 22345 RVA: 0x0010AF88 File Offset: 0x00109188
		private string GetBottomContentCssClass()
		{
			StringBuilder stringBuilder = new StringBuilder("rtileBottomContent");
			if (!string.IsNullOrEmpty(base.Title.Text) || !string.IsNullOrEmpty(base.ResolveClientUrl(base.Title.ImageUrl)))
			{
				stringBuilder.Append(" rtileTitleIndent");
			}
			if (base.Badge.Value != null || !string.IsNullOrEmpty(base.Badge.ImageUrl) || base.Badge.PredefinedType != TileBadgeType.None)
			{
				stringBuilder.Append(" rtileBadgeIndent");
			}
			return stringBuilder.ToString();
		}
	}
}
