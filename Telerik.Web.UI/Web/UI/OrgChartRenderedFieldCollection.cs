using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02000BDE RID: 3038
	[XmlRoot("Fields")]
	public class OrgChartRenderedFieldCollection : List<OrgChartRenderedField>, IXmlSerializable
	{
		// Token: 0x060073F6 RID: 29686 RVA: 0x001B1114 File Offset: 0x001AF314
		public XmlSchema GetSchema()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060073F7 RID: 29687 RVA: 0x001B111C File Offset: 0x001AF31C
		public void ReadXml(XmlReader reader)
		{
			while (reader.Read())
			{
				if (reader.NodeType != XmlNodeType.EndElement && reader.NodeType != XmlNodeType.Comment)
				{
					using (XmlReader xmlReader = reader.ReadSubtree())
					{
						XmlSerializer xmlSerializer = new XmlSerializer(typeof(OrgChartRenderedField));
						OrgChartRenderedField item = (OrgChartRenderedField)xmlSerializer.Deserialize(xmlReader);
						base.Add(item);
					}
					reader.MoveToContent();
				}
			}
		}

		// Token: 0x060073F8 RID: 29688 RVA: 0x001B1194 File Offset: 0x001AF394
		public void WriteXml(XmlWriter writer)
		{
			this.WriteXmlRenderedField(writer);
		}

		// Token: 0x060073F9 RID: 29689 RVA: 0x001B11A0 File Offset: 0x001AF3A0
		private void WriteXmlRenderedField(XmlWriter writer)
		{
			if (base.Count > 0)
			{
				foreach (OrgChartRenderedField o in this)
				{
					XmlSerializer xmlSerializer = new XmlSerializer(typeof(OrgChartRenderedField));
					xmlSerializer.Serialize(writer, o);
				}
			}
		}
	}
}
