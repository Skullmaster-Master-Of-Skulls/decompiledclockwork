using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.ServiceModel.Discovery.VersionApril2005
{
	// Token: 0x0200008A RID: 138
	[XmlSchemaProvider("GetSchema")]
	public class ResolveCriteriaApril2005 : IXmlSerializable
	{
		// Token: 0x06000629 RID: 1577 RVA: 0x00010EAE File Offset: 0x0000F0AE
		private ResolveCriteriaApril2005()
		{
			this.resolveCriteria = new ResolveCriteria();
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x00010EC1 File Offset: 0x0000F0C1
		private ResolveCriteriaApril2005(ResolveCriteria resolveCriteria)
		{
			this.resolveCriteria = resolveCriteria;
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x00010ED0 File Offset: 0x0000F0D0
		public static ResolveCriteriaApril2005 FromResolveCriteria(ResolveCriteria resolveCriteria)
		{
			if (resolveCriteria == null)
			{
				throw FxTrace.Exception.ArgumentNull("resolveCriteria");
			}
			return new ResolveCriteriaApril2005(resolveCriteria);
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x00010EEB File Offset: 0x0000F0EB
		public static XmlQualifiedName GetSchema(XmlSchemaSet schemaSet)
		{
			if (schemaSet == null)
			{
				throw FxTrace.Exception.ArgumentNull("schemaSet");
			}
			return SchemaUtility.EnsureResolveSchema(DiscoveryVersion.WSDiscoveryApril2005, schemaSet);
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x00010F0B File Offset: 0x0000F10B
		public ResolveCriteria ToResolveCriteria()
		{
			return this.resolveCriteria;
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x00006B84 File Offset: 0x00004D84
		public XmlSchema GetSchema()
		{
			return null;
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x00010F13 File Offset: 0x0000F113
		public void ReadXml(XmlReader reader)
		{
			this.resolveCriteria.ReadFrom(DiscoveryVersion.WSDiscoveryApril2005, reader);
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x00010F26 File Offset: 0x0000F126
		public void WriteXml(XmlWriter writer)
		{
			this.resolveCriteria.WriteTo(DiscoveryVersion.WSDiscoveryApril2005, writer);
		}

		// Token: 0x04000178 RID: 376
		private ResolveCriteria resolveCriteria;
	}
}
