using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02000E54 RID: 3668
	[ToolboxItem(false)]
	[XmlRoot("ColorPickerItem")]
	public class RibbonBarColorPickerItem : WebControl, IXmlSerializable
	{
		// Token: 0x06008B0F RID: 35599 RVA: 0x001FAAAD File Offset: 0x001F8CAD
		public RibbonBarColorPickerItem()
		{
		}

		// Token: 0x06008B10 RID: 35600 RVA: 0x001FAAB5 File Offset: 0x001F8CB5
		public RibbonBarColorPickerItem(Color value)
		{
			this.Value = value;
		}

		// Token: 0x06008B11 RID: 35601 RVA: 0x001FAAC4 File Offset: 0x001F8CC4
		public RibbonBarColorPickerItem(Color value, string title) : this(value)
		{
			this.Title = title;
		}

		// Token: 0x17002BF2 RID: 11250
		// (get) Token: 0x06008B12 RID: 35602 RVA: 0x001FAAD4 File Offset: 0x001F8CD4
		// (set) Token: 0x06008B13 RID: 35603 RVA: 0x001FAAFA File Offset: 0x001F8CFA
		[Description("Gets or sets the tooltip text of the ColorPickerItem.")]
		public string Title
		{
			get
			{
				return ((string)this.ViewState["Title"]) ?? ColorTranslator.ToHtml(this.Value);
			}
			set
			{
				this.ViewState["Title"] = value;
			}
		}

		// Token: 0x17002BF3 RID: 11251
		// (get) Token: 0x06008B14 RID: 35604 RVA: 0x001FAB0D File Offset: 0x001F8D0D
		// (set) Token: 0x06008B15 RID: 35605 RVA: 0x001FAB32 File Offset: 0x001F8D32
		[DefaultValue(typeof(Color))]
		[Description("Gets or sets the Color value of the RibbonBarColorPickerItem.")]
		public Color Value
		{
			get
			{
				return (Color)(this.ViewState["Value"] ?? Color.Empty);
			}
			set
			{
				this.ViewState["Value"] = value;
			}
		}

		// Token: 0x06008B16 RID: 35606 RVA: 0x001FAB4A File Offset: 0x001F8D4A
		XmlSchema IXmlSerializable.GetSchema()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		// Token: 0x06008B17 RID: 35607 RVA: 0x001FAB56 File Offset: 0x001F8D56
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			this.ReadXml(reader);
		}

		// Token: 0x06008B18 RID: 35608 RVA: 0x001FAB5F File Offset: 0x001F8D5F
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			this.WriteXml(writer);
		}

		// Token: 0x06008B19 RID: 35609 RVA: 0x001FAB68 File Offset: 0x001F8D68
		public void ReadXml(XmlReader reader)
		{
			XmlPersister.Deserialize(this, base.Attributes, null, reader);
		}

		// Token: 0x06008B1A RID: 35610 RVA: 0x001FAB78 File Offset: 0x001F8D78
		protected void WriteXml(XmlWriter writer)
		{
			XmlPersister.SerializePropertiesAsAttributes(this, writer);
			XmlPersister.SerializeAttributeCollectionAsAttributes(base.Attributes, writer);
		}
	}
}
