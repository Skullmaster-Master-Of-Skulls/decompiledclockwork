using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;
using Telerik.Licensing;

namespace Telerik.Web.UI
{
	// Token: 0x02000907 RID: 2311
	[TelerikToolboxCategory("Navigation")]
	[Designer("Telerik.Web.Design.RadTextTileDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[ClientScriptResource("Telerik.Web.UI.RadTextTile", "Telerik.Web.UI.Tile.RadTileScripts.js")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[ToolboxBitmap(typeof(RadTextTile), "Telerik.Web.UI.TileList.png")]
	public class RadTextTile : RadBaseTile
	{
		// Token: 0x06005782 RID: 22402 RVA: 0x0010B734 File Offset: 0x00109934
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<string>(descriptor, "text", this.Text, "");
			base.DescribeProperty<string>(descriptor, "tileType", this.TileType, null);
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x06005783 RID: 22403 RVA: 0x0010B767 File Offset: 0x00109967
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x17001CF4 RID: 7412
		// (get) Token: 0x06005785 RID: 22405 RVA: 0x0010B778 File Offset: 0x00109978
		// (set) Token: 0x06005786 RID: 22406 RVA: 0x0010B798 File Offset: 0x00109998
		[ClientControlProperty]
		[DefaultValue("")]
		[Description("Gets or sets the Text which will be rendered in the tile.")]
		[Category("Behavior")]
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

		// Token: 0x17001CF5 RID: 7413
		// (get) Token: 0x06005787 RID: 22407 RVA: 0x0010B7AB File Offset: 0x001099AB
		[ClientControlProperty]
		internal override string TileType
		{
			get
			{
				return "RadTextTile";
			}
		}

		// Token: 0x06005788 RID: 22408 RVA: 0x0010B7B2 File Offset: 0x001099B2
		protected override void RenderTileBody(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rtileTopContent");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.WriteEncodedText(this.Text);
			writer.RenderEndTag();
		}
	}
}
