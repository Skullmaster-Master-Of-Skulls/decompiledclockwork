using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.ServiceModel.Discovery.VersionCD1
{
	// Token: 0x02000064 RID: 100
	[XmlSchemaProvider("GetSchema")]
	public class EndpointDiscoveryMetadataCD1 : IXmlSerializable
	{
		// Token: 0x06000526 RID: 1318 RVA: 0x0000FBA0 File Offset: 0x0000DDA0
		private EndpointDiscoveryMetadataCD1()
		{
			this.endpointDiscoveryMetadata = new EndpointDiscoveryMetadata();
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x0000FBB3 File Offset: 0x0000DDB3
		private EndpointDiscoveryMetadataCD1(EndpointDiscoveryMetadata endpointDiscoveryMetadata)
		{
			this.endpointDiscoveryMetadata = endpointDiscoveryMetadata;
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x0000FBC2 File Offset: 0x0000DDC2
		public static EndpointDiscoveryMetadataCD1 FromEndpointDiscoveryMetadata(EndpointDiscoveryMetadata endpointDiscoveryMetadata)
		{
			if (endpointDiscoveryMetadata == null)
			{
				throw FxTrace.Exception.ArgumentNull("endpointDiscoveryMetadata");
			}
			return new EndpointDiscoveryMetadataCD1(endpointDiscoveryMetadata);
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x0000FBDD File Offset: 0x0000DDDD
		public static XmlQualifiedName GetSchema(XmlSchemaSet schemaSet)
		{
			if (schemaSet == null)
			{
				throw FxTrace.Exception.ArgumentNull("schemaSet");
			}
			return SchemaUtility.EnsureProbeMatchSchema(DiscoveryVersion.WSDiscoveryCD1, schemaSet);
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x0000FBFD File Offset: 0x0000DDFD
		public EndpointDiscoveryMetadata ToEndpointDiscoveryMetadata()
		{
			return this.endpointDiscoveryMetadata;
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x00006B84 File Offset: 0x00004D84
		public XmlSchema GetSchema()
		{
			return null;
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x0000FC05 File Offset: 0x0000DE05
		public void ReadXml(XmlReader reader)
		{
			this.endpointDiscoveryMetadata.ReadFrom(DiscoveryVersion.WSDiscoveryCD1, reader);
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x0000FC18 File Offset: 0x0000DE18
		public void WriteXml(XmlWriter writer)
		{
			this.endpointDiscoveryMetadata.WriteTo(DiscoveryVersion.WSDiscoveryCD1, writer);
		}

		// Token: 0x0400014B RID: 331
		private EndpointDiscoveryMetadata endpointDiscoveryMetadata;
	}
}
