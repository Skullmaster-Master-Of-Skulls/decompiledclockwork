using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.ServiceModel.Discovery.VersionCD1
{
	// Token: 0x02000065 RID: 101
	[XmlSchemaProvider("GetSchema")]
	public class FindCriteriaCD1 : IXmlSerializable
	{
		// Token: 0x0600052E RID: 1326 RVA: 0x0000FC2B File Offset: 0x0000DE2B
		private FindCriteriaCD1()
		{
			this.findCriteria = new FindCriteria();
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x0000FC3E File Offset: 0x0000DE3E
		private FindCriteriaCD1(FindCriteria findCriteria)
		{
			this.findCriteria = findCriteria;
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x0000FC4D File Offset: 0x0000DE4D
		public static FindCriteriaCD1 FromFindCriteria(FindCriteria findCriteria)
		{
			if (findCriteria == null)
			{
				throw FxTrace.Exception.ArgumentNull("findCriteria");
			}
			return new FindCriteriaCD1(findCriteria);
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x0000FC68 File Offset: 0x0000DE68
		public static XmlQualifiedName GetSchema(XmlSchemaSet schemaSet)
		{
			if (schemaSet == null)
			{
				throw FxTrace.Exception.ArgumentNull("schemaSet");
			}
			return SchemaUtility.EnsureProbeSchema(DiscoveryVersion.WSDiscoveryCD1, schemaSet);
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x0000FC88 File Offset: 0x0000DE88
		public FindCriteria ToFindCriteria()
		{
			return this.findCriteria;
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x00006B84 File Offset: 0x00004D84
		public XmlSchema GetSchema()
		{
			return null;
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x0000FC90 File Offset: 0x0000DE90
		public void ReadXml(XmlReader reader)
		{
			this.findCriteria.ReadFrom(DiscoveryVersion.WSDiscoveryCD1, reader);
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x0000FCA3 File Offset: 0x0000DEA3
		public void WriteXml(XmlWriter writer)
		{
			this.findCriteria.WriteTo(DiscoveryVersion.WSDiscoveryCD1, writer);
		}

		// Token: 0x0400014C RID: 332
		private FindCriteria findCriteria;
	}
}
