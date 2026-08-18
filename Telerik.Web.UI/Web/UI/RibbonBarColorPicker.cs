using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Telerik.Web.UI.RibbonBar.Renderers;

namespace Telerik.Web.UI
{
	// Token: 0x02000E57 RID: 3671
	[ParseChildren(ChildrenAsProperties = true)]
	[XmlRoot("ColorPicker")]
	public class RibbonBarColorPicker : RibbonBarDropDownItem, IXmlSerializable
	{
		// Token: 0x17002BFA RID: 11258
		// (get) Token: 0x06008B28 RID: 35624 RVA: 0x001FAC34 File Offset: 0x001F8E34
		internal override string ItemCssClass
		{
			get
			{
				return "rrbColorPicker";
			}
		}

		// Token: 0x17002BFB RID: 11259
		// (get) Token: 0x06008B29 RID: 35625 RVA: 0x001FAC3B File Offset: 0x001F8E3B
		internal override string InnerCssClass
		{
			get
			{
				return "rrbCPInner";
			}
		}

		// Token: 0x17002BFC RID: 11260
		// (get) Token: 0x06008B2A RID: 35626 RVA: 0x001FAC42 File Offset: 0x001F8E42
		internal override string InputCssClass
		{
			get
			{
				return "rrbCPFakeInput";
			}
		}

		// Token: 0x17002BFD RID: 11261
		// (get) Token: 0x06008B2B RID: 35627 RVA: 0x001FAC49 File Offset: 0x001F8E49
		public override RibbonBarItemType ItemType
		{
			get
			{
				return RibbonBarItemType.ColorPicker;
			}
		}

		// Token: 0x06008B2C RID: 35628 RVA: 0x001FAC4D File Offset: 0x001F8E4D
		protected override IRenderer CreateControlRenderer()
		{
			if (base.RibbonBar.ResolvedRenderMode == RenderMode.Lightweight)
			{
				return new RibbonBarColorPickerLiteRenderer(this);
			}
			return new RibbonBarColorPickerClassicRenderer(this);
		}

		// Token: 0x06008B2D RID: 35629 RVA: 0x001FAC6A File Offset: 0x001F8E6A
		internal void SyncronizeItemsAndPreset()
		{
			if (this.Items.Count == 0)
			{
				this.Items.AddRange(RibbonBarColorPicker.GetStandardColors());
			}
		}

		// Token: 0x06008B2E RID: 35630 RVA: 0x001FAC8C File Offset: 0x001F8E8C
		private static RibbonBarColorPickerItemCollection GetStandardColors()
		{
			return new RibbonBarColorPickerItemCollection
			{
				new RibbonBarColorPickerItem(ColorTranslator.FromHtml("#FFC00000")),
				new RibbonBarColorPickerItem(ColorTranslator.FromHtml("#FFFF0000")),
				new RibbonBarColorPickerItem(ColorTranslator.FromHtml("#FFFFC000")),
				new RibbonBarColorPickerItem(ColorTranslator.FromHtml("#FFFFFF00")),
				new RibbonBarColorPickerItem(ColorTranslator.FromHtml("#FF92D050")),
				new RibbonBarColorPickerItem(ColorTranslator.FromHtml("#FF00B050")),
				new RibbonBarColorPickerItem(ColorTranslator.FromHtml("#FF00B0F0")),
				new RibbonBarColorPickerItem(ColorTranslator.FromHtml("#FF0070C0")),
				new RibbonBarColorPickerItem(ColorTranslator.FromHtml("#FF002060")),
				new RibbonBarColorPickerItem(ColorTranslator.FromHtml("#FF7030A0")),
				new RibbonBarColorPickerItem(Color.Black),
				new RibbonBarColorPickerItem(Color.White)
			};
		}

		// Token: 0x17002BFE RID: 11262
		// (get) Token: 0x06008B2F RID: 35631 RVA: 0x001FAD92 File Offset: 0x001F8F92
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public RibbonBarColorPickerItemCollection Items
		{
			get
			{
				if (this._items == null)
				{
					this._items = new RibbonBarColorPickerItemCollection();
				}
				return this._items;
			}
		}

		// Token: 0x17002BFF RID: 11263
		// (get) Token: 0x06008B30 RID: 35632 RVA: 0x001FADAD File Offset: 0x001F8FAD
		// (set) Token: 0x06008B31 RID: 35633 RVA: 0x001FADE1 File Offset: 0x001F8FE1
		[Description("Get/Set the selected color of the ColorPicker.")]
		[DefaultValue(typeof(Color))]
		[Category("Behavior")]
		public Color SelectedColor
		{
			get
			{
				if (this.ViewState["SelectedColor"] == null)
				{
					return Color.Empty;
				}
				return ColorTranslator.FromHtml((string)this.ViewState["SelectedColor"]);
			}
			set
			{
				this.ViewState["SelectedColor"] = ColorTranslator.ToHtml(value);
			}
		}

		// Token: 0x17002C00 RID: 11264
		// (get) Token: 0x06008B32 RID: 35634 RVA: 0x001FADF9 File Offset: 0x001F8FF9
		// (set) Token: 0x06008B33 RID: 35635 RVA: 0x001FAE19 File Offset: 0x001F9019
		[UrlProperty]
		[Description("The URL of the image displayed for the color picker.")]
		[ClientPersistedProperty]
		[Category("Appearance")]
		[DefaultValue("")]
		public string ImageUrl
		{
			get
			{
				return (string)(this.ViewState["ImageUrl"] ?? string.Empty);
			}
			set
			{
				this.ViewState["ImageUrl"] = value;
			}
		}

		// Token: 0x06008B34 RID: 35636 RVA: 0x001FAE2C File Offset: 0x001F902C
		XmlSchema IXmlSerializable.GetSchema()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		// Token: 0x06008B35 RID: 35637 RVA: 0x001FAE38 File Offset: 0x001F9038
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			this.ReadXml(reader);
		}

		// Token: 0x06008B36 RID: 35638 RVA: 0x001FAE41 File Offset: 0x001F9041
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			this.WriteXml(writer);
		}

		// Token: 0x06008B37 RID: 35639 RVA: 0x001FAE4A File Offset: 0x001F904A
		public void ReadXml(XmlReader reader)
		{
			XmlPersister.Deserialize(this, base.Attributes, null, reader);
			this.ReadXmlForItems(reader);
		}

		// Token: 0x06008B38 RID: 35640 RVA: 0x001FAE64 File Offset: 0x001F9064
		protected void ReadXmlForItems(XmlReader reader)
		{
			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "ColorPicker")
				{
					return;
				}
				if (reader.NodeType == XmlNodeType.Element && reader.Name != "ColorPicker" && reader.Name != "ColorPickerItem")
				{
					return;
				}
				if (reader.NodeType != XmlNodeType.EndElement && reader.NodeType != XmlNodeType.Comment)
				{
					XmlSerializer xmlSerializer = new XmlSerializer(typeof(RibbonBarColorPickerItem));
					RibbonBarColorPickerItem item = (RibbonBarColorPickerItem)xmlSerializer.Deserialize(reader);
					this.Items.Add(item);
					reader.MoveToContent();
				}
			}
		}

		// Token: 0x06008B39 RID: 35641 RVA: 0x001FAF0F File Offset: 0x001F910F
		protected void WriteXml(XmlWriter writer)
		{
			XmlPersister.SerializePropertiesAsAttributes(this, writer);
			XmlPersister.SerializeAttributeCollectionAsAttributes(base.Attributes, writer);
			this.WriteXmlForItems(writer);
		}

		// Token: 0x06008B3A RID: 35642 RVA: 0x001FAF2C File Offset: 0x001F912C
		protected void WriteXmlForItems(XmlWriter writer)
		{
			foreach (RibbonBarColorPickerItem ribbonBarColorPickerItem in this.Items)
			{
				XmlSerializer xmlSerializer = new XmlSerializer(ribbonBarColorPickerItem.GetType());
				xmlSerializer.Serialize(writer, ribbonBarColorPickerItem);
			}
		}

		// Token: 0x0400270E RID: 9998
		private RibbonBarColorPickerItemCollection _items;
	}
}
