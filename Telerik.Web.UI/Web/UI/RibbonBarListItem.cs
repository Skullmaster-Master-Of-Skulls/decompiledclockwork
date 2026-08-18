using System;
using System.ComponentModel;
using System.Web.UI;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02000F38 RID: 3896
	[ToolboxItem(false)]
	[XmlRoot("ListItem")]
	public class RibbonBarListItem : RibbonBarCollectionItemBase, IXmlSerializable
	{
		// Token: 0x06009490 RID: 38032 RVA: 0x002144B0 File Offset: 0x002126B0
		public override void RenderControl(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbListItem");
			writer.RenderBeginTag(HtmlTextWriterTag.Li);
			writer.Write(this.Text);
			writer.RenderEndTag();
		}

		// Token: 0x17002F00 RID: 12032
		// (get) Token: 0x06009491 RID: 38033 RVA: 0x002144D9 File Offset: 0x002126D9
		// (set) Token: 0x06009492 RID: 38034 RVA: 0x002144F9 File Offset: 0x002126F9
		[DefaultValue("")]
		public string Text
		{
			get
			{
				return (string)(this.ViewState["Text"] ?? string.Empty);
			}
			set
			{
				this.ViewState["Text"] = value;
			}
		}

		// Token: 0x17002F01 RID: 12033
		// (get) Token: 0x06009493 RID: 38035 RVA: 0x0021450C File Offset: 0x0021270C
		// (set) Token: 0x06009494 RID: 38036 RVA: 0x00214514 File Offset: 0x00212714
		[Description("Whether the item is selected or not.")]
		[Category("Behavior")]
		[DefaultValue(false)]
		public bool Selected { get; set; }

		// Token: 0x06009495 RID: 38037 RVA: 0x0021451D File Offset: 0x0021271D
		XmlSchema IXmlSerializable.GetSchema()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		// Token: 0x06009496 RID: 38038 RVA: 0x00214529 File Offset: 0x00212729
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			this.ReadXml(reader);
		}

		// Token: 0x06009497 RID: 38039 RVA: 0x00214532 File Offset: 0x00212732
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			this.WriteXml(writer);
		}

		// Token: 0x06009498 RID: 38040 RVA: 0x0021453B File Offset: 0x0021273B
		public void ReadXml(XmlReader reader)
		{
			XmlPersister.Deserialize(this, base.Attributes, null, reader);
		}

		// Token: 0x06009499 RID: 38041 RVA: 0x0021454B File Offset: 0x0021274B
		protected void WriteXml(XmlWriter writer)
		{
			XmlPersister.SerializePropertiesAsAttributes(this, writer);
			XmlPersister.SerializeAttributeCollectionAsAttributes(base.Attributes, writer);
		}
	}
}
