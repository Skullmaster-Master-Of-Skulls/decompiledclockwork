using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02000BEC RID: 3052
	[XmlRoot("Field")]
	public class OrgChartRenderedField : IXmlSerializable
	{
		// Token: 0x170025F4 RID: 9716
		// (get) Token: 0x06007476 RID: 29814 RVA: 0x001B2BA8 File Offset: 0x001B0DA8
		// (set) Token: 0x06007477 RID: 29815 RVA: 0x001B2BB0 File Offset: 0x001B0DB0
		internal OrgChartRenderedField MasterField { get; set; }

		// Token: 0x06007478 RID: 29816 RVA: 0x001B2BB9 File Offset: 0x001B0DB9
		XmlSchema IXmlSerializable.GetSchema()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06007479 RID: 29817 RVA: 0x001B2BC0 File Offset: 0x001B0DC0
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			if (reader.HasAttributes)
			{
				this.Text = reader.GetAttribute("Text");
			}
		}

		// Token: 0x0600747A RID: 29818 RVA: 0x001B2BDB File Offset: 0x001B0DDB
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			this.WriteRenderedFiledsAttributes(writer);
		}

		// Token: 0x0600747B RID: 29819 RVA: 0x001B2BE4 File Offset: 0x001B0DE4
		private void WriteRenderedFiledsAttributes(XmlWriter writer)
		{
			if (!string.IsNullOrEmpty(this.TextToRender))
			{
				writer.WriteAttributeString("Text", this.TextToRender);
			}
		}

		// Token: 0x170025F5 RID: 9717
		// (get) Token: 0x0600747C RID: 29820 RVA: 0x001B2C04 File Offset: 0x001B0E04
		// (set) Token: 0x0600747D RID: 29821 RVA: 0x001B2C0C File Offset: 0x001B0E0C
		public string DataField { get; set; }

		// Token: 0x170025F6 RID: 9718
		// (get) Token: 0x0600747E RID: 29822 RVA: 0x001B2C15 File Offset: 0x001B0E15
		// (set) Token: 0x0600747F RID: 29823 RVA: 0x001B2C31 File Offset: 0x001B0E31
		public string Label
		{
			get
			{
				if (this.MasterField == null)
				{
					return this._label;
				}
				return this.MasterField.Label;
			}
			set
			{
				this._label = value;
			}
		}

		// Token: 0x170025F7 RID: 9719
		// (get) Token: 0x06007480 RID: 29824 RVA: 0x001B2C3A File Offset: 0x001B0E3A
		// (set) Token: 0x06007481 RID: 29825 RVA: 0x001B2C42 File Offset: 0x001B0E42
		public string Text { get; set; }

		// Token: 0x170025F8 RID: 9720
		// (get) Token: 0x06007482 RID: 29826 RVA: 0x001B2C4B File Offset: 0x001B0E4B
		public string TextToRender
		{
			get
			{
				if (string.IsNullOrEmpty(this.Label))
				{
					return this.Text;
				}
				return this.Label + ": " + this.Text;
			}
		}

		// Token: 0x04001FA3 RID: 8099
		private string _label;
	}
}
