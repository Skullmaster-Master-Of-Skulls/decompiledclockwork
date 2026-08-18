using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.ServiceModel.Discovery.Version11
{
	// Token: 0x02000097 RID: 151
	[XmlSchemaProvider("GetSchema")]
	public class FindCriteria11 : IXmlSerializable
	{
		// Token: 0x060006AE RID: 1710 RVA: 0x00011EFF File Offset: 0x000100FF
		private FindCriteria11()
		{
			this.findCriteria = new FindCriteria();
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x00011F12 File Offset: 0x00010112
		private FindCriteria11(FindCriteria findCriteria)
		{
			this.findCriteria = findCriteria;
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x00011F21 File Offset: 0x00010121
		public static FindCriteria11 FromFindCriteria(FindCriteria findCriteria)
		{
			if (findCriteria == null)
			{
				throw FxTrace.Exception.ArgumentNull("findCriteria");
			}
			return new FindCriteria11(findCriteria);
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x00011F3C File Offset: 0x0001013C
		public static XmlQualifiedName GetSchema(XmlSchemaSet schemaSet)
		{
			if (schemaSet == null)
			{
				throw FxTrace.Exception.ArgumentNull("schemaSet");
			}
			return SchemaUtility.EnsureProbeSchema(DiscoveryVersion.WSDiscovery11, schemaSet);
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x00011F5C File Offset: 0x0001015C
		public FindCriteria ToFindCriteria()
		{
			return this.findCriteria;
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x00006B84 File Offset: 0x00004D84
		public XmlSchema GetSchema()
		{
			return null;
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x00011F64 File Offset: 0x00010164
		public void ReadXml(XmlReader reader)
		{
			this.findCriteria.ReadFrom(DiscoveryVersion.WSDiscovery11, reader);
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x00011F77 File Offset: 0x00010177
		public void WriteXml(XmlWriter writer)
		{
			this.findCriteria.WriteTo(DiscoveryVersion.WSDiscovery11, writer);
		}

		// Token: 0x04000199 RID: 409
		private FindCriteria findCriteria;
	}
}
