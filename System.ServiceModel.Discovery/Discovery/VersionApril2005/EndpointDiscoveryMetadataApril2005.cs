using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.ServiceModel.Discovery.VersionApril2005
{
	// Token: 0x0200007D RID: 125
	[XmlSchemaProvider("GetSchema")]
	public class EndpointDiscoveryMetadataApril2005 : IXmlSerializable
	{
		// Token: 0x060005E8 RID: 1512 RVA: 0x00010BCC File Offset: 0x0000EDCC
		private EndpointDiscoveryMetadataApril2005()
		{
			this.endpointDiscoveryMetadata = new EndpointDiscoveryMetadata();
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x00010BDF File Offset: 0x0000EDDF
		private EndpointDiscoveryMetadataApril2005(EndpointDiscoveryMetadata endpointDiscoveryMetadata)
		{
			this.endpointDiscoveryMetadata = endpointDiscoveryMetadata;
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x00010BEE File Offset: 0x0000EDEE
		public static EndpointDiscoveryMetadataApril2005 FromEndpointDiscoveryMetadata(EndpointDiscoveryMetadata endpointDiscoveryMetadata)
		{
			if (endpointDiscoveryMetadata == null)
			{
				throw FxTrace.Exception.ArgumentNull("endpointDiscoveryMetadata");
			}
			return new EndpointDiscoveryMetadataApril2005(endpointDiscoveryMetadata);
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x00010C09 File Offset: 0x0000EE09
		public static XmlQualifiedName GetSchema(XmlSchemaSet schemaSet)
		{
			if (schemaSet == null)
			{
				throw FxTrace.Exception.ArgumentNull("schemaSet");
			}
			return SchemaUtility.EnsureProbeMatchSchema(DiscoveryVersion.WSDiscoveryApril2005, schemaSet);
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x00010C29 File Offset: 0x0000EE29
		public EndpointDiscoveryMetadata ToEndpointDiscoveryMetadata()
		{
			return this.endpointDiscoveryMetadata;
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x00006B84 File Offset: 0x00004D84
		public XmlSchema GetSchema()
		{
			return null;
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x00010C31 File Offset: 0x0000EE31
		public void ReadXml(XmlReader reader)
		{
			this.endpointDiscoveryMetadata.ReadFrom(DiscoveryVersion.WSDiscoveryApril2005, reader);
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x00010C44 File Offset: 0x0000EE44
		public void WriteXml(XmlWriter writer)
		{
			this.endpointDiscoveryMetadata.WriteTo(DiscoveryVersion.WSDiscoveryApril2005, writer);
		}

		// Token: 0x04000171 RID: 369
		private EndpointDiscoveryMetadata endpointDiscoveryMetadata;
	}
}
