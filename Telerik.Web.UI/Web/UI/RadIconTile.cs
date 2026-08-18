using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Licensing;

namespace Telerik.Web.UI
{
	// Token: 0x02000903 RID: 2307
	[ClientScriptResource("Telerik.Web.UI.RadIconTile", "Telerik.Web.UI.Tile.RadTileScripts.js")]
	[ToolboxBitmap(typeof(RadIconTile), "Telerik.Web.UI.TileList.png")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[TelerikToolboxCategory("Navigation")]
	[Designer("Telerik.Web.Design.RadIconTileDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	public class RadIconTile : RadBaseTile
	{
		// Token: 0x0600574A RID: 22346 RVA: 0x0010B01C File Offset: 0x0010921C
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<string>(descriptor, "imageUrl", base.ResolveClientUrl(this.ImageUrl), "");
			base.DescribeProperty<string>(descriptor, "tileType", this.TileType, null);
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x0600574B RID: 22347 RVA: 0x0010B055 File Offset: 0x00109255
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x17001CDE RID: 7390
		// (get) Token: 0x0600574D RID: 22349 RVA: 0x0010B066 File Offset: 0x00109266
		// (set) Token: 0x0600574E RID: 22350 RVA: 0x0010B086 File Offset: 0x00109286
		[ClientControlProperty]
		[Description("Gets or sets the name of the command fired when the tool is clicked.")]
		[UrlProperty]
		[DefaultValue("")]
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

		// Token: 0x17001CDF RID: 7391
		// (get) Token: 0x0600574F RID: 22351 RVA: 0x0010B099 File Offset: 0x00109299
		// (set) Token: 0x06005750 RID: 22352 RVA: 0x0010B0BE File Offset: 0x001092BE
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

		// Token: 0x17001CE0 RID: 7392
		// (get) Token: 0x06005751 RID: 22353 RVA: 0x0010B0D6 File Offset: 0x001092D6
		// (set) Token: 0x06005752 RID: 22354 RVA: 0x0010B0FB File Offset: 0x001092FB
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

		// Token: 0x17001CE1 RID: 7393
		// (get) Token: 0x06005753 RID: 22355 RVA: 0x0010B113 File Offset: 0x00109313
		[ClientControlProperty]
		internal override string TileType
		{
			get
			{
				return "RadIconTile";
			}
		}

		// Token: 0x06005754 RID: 22356 RVA: 0x0010B11C File Offset: 0x0010931C
		protected override void RenderTileBody(HtmlTextWriter writer)
		{
			if (!string.IsNullOrEmpty(this.ImageUrl))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rtileTopContent");
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
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
				writer.RenderEndTag();
			}
		}
	}
}
