using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.ServiceModel.Discovery.VersionApril2005
{
	// Token: 0x0200007E RID: 126
	[XmlSchemaProvider("GetSchema")]
	public class FindCriteriaApril2005 : IXmlSerializable
	{
		// Token: 0x060005F0 RID: 1520 RVA: 0x00010C57 File Offset: 0x0000EE57
		private FindCriteriaApril2005()
		{
			this.findCriteria = new FindCriteria();
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x00010C6A File Offset: 0x0000EE6A
		private FindCriteriaApril2005(FindCriteria findCriteria)
		{
			this.findCriteria = findCriteria;
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x00010C79 File Offset: 0x0000EE79
		public static FindCriteriaApril2005 FromFindCriteria(FindCriteria findCriteria)
		{
			if (findCriteria == null)
			{
				throw FxTrace.Exception.ArgumentNull("findCriteria");
			}
			return new FindCriteriaApril2005(findCriteria);
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x00010C94 File Offset: 0x0000EE94
		public static XmlQualifiedName GetSchema(XmlSchemaSet schemaSet)
		{
			if (schemaSet == null)
			{
				throw FxTrace.Exception.ArgumentNull("schemaSet");
			}
			return SchemaUtility.EnsureProbeSchema(DiscoveryVersion.WSDiscoveryApril2005, schemaSet);
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x00010CB4 File Offset: 0x0000EEB4
		public FindCriteria ToFindCriteria()
		{
			return this.findCriteria;
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x00006B84 File Offset: 0x00004D84
		public XmlSchema GetSchema()
		{
			return null;
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x00010CBC File Offset: 0x0000EEBC
		public void ReadXml(XmlReader reader)
		{
			this.findCriteria.ReadFrom(DiscoveryVersion.WSDiscoveryApril2005, reader);
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x00010CCF File Offset: 0x0000EECF
		public void WriteXml(XmlWriter writer)
		{
			this.findCriteria.WriteTo(DiscoveryVersion.WSDiscoveryApril2005, writer);
		}

		// Token: 0x04000172 RID: 370
		private FindCriteria findCriteria;
	}
}
