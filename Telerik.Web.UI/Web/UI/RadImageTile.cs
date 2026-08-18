using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Licensing;

namespace Telerik.Web.UI
{
	// Token: 0x02000901 RID: 2305
	[TelerikToolboxCategory("Navigation")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[ClientScriptResource("Telerik.Web.UI.RadImageTile", "Telerik.Web.UI.Tile.RadTileScripts.js")]
	[ToolboxBitmap(typeof(RadImageTile), "Telerik.Web.UI.TileList.png")]
	[Designer("Telerik.Web.Design.RadImageTileDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	public class RadImageTile : RadBaseTile
	{
		// Token: 0x06005731 RID: 22321 RVA: 0x0010AB97 File Offset: 0x00108D97
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<string>(descriptor, "imageUrl", base.ResolveClientUrl(this.ImageUrl), "");
			base.DescribeProperty<string>(descriptor, "tileType", this.TileType, null);
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x06005732 RID: 22322 RVA: 0x0010ABD0 File Offset: 0x00108DD0
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x17001CD5 RID: 7381
		// (get) Token: 0x06005734 RID: 22324 RVA: 0x0010ABE1 File Offset: 0x00108DE1
		// (set) Token: 0x06005735 RID: 22325 RVA: 0x0010AC01 File Offset: 0x00108E01
		[DefaultValue("")]
		[Category("Behavior")]
		[ClientControlProperty]
		[Description("Gets or sets the name of the command fired when the tool is clicked.")]
		[UrlProperty]
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

		// Token: 0x17001CD6 RID: 7382
		// (get) Token: 0x06005736 RID: 22326 RVA: 0x0010AC14 File Offset: 0x00108E14
		// (set) Token: 0x06005737 RID: 22327 RVA: 0x0010AC39 File Offset: 0x00108E39
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(Unit), "")]
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

		// Token: 0x17001CD7 RID: 7383
		// (get) Token: 0x06005738 RID: 22328 RVA: 0x0010AC51 File Offset: 0x00108E51
		// (set) Token: 0x06005739 RID: 22329 RVA: 0x0010AC76 File Offset: 0x00108E76
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

		// Token: 0x17001CD8 RID: 7384
		// (get) Token: 0x0600573A RID: 22330 RVA: 0x0010AC8E File Offset: 0x00108E8E
		[ClientControlProperty]
		internal override string TileType
		{
			get
			{
				return "RadImageTile";
			}
		}

		// Token: 0x0600573B RID: 22331 RVA: 0x0010AC98 File Offset: 0x00108E98
		protected override void RenderTileBody(HtmlTextWriter writer)
		{
			if (!string.IsNullOrEmpty(this.ImageUrl))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rtileIconImage");
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
			}
		}
	}
}
