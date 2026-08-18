using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.ServiceModel.Discovery.Version11
{
	// Token: 0x02000096 RID: 150
	[XmlSchemaProvider("GetSchema")]
	public class EndpointDiscoveryMetadata11 : IXmlSerializable
	{
		// Token: 0x060006A6 RID: 1702 RVA: 0x00011E74 File Offset: 0x00010074
		private EndpointDiscoveryMetadata11()
		{
			this.endpointDiscoveryMetadata = new EndpointDiscoveryMetadata();
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x00011E87 File Offset: 0x00010087
		private EndpointDiscoveryMetadata11(EndpointDiscoveryMetadata endpointDiscoveryMetadata)
		{
			this.endpointDiscoveryMetadata = endpointDiscoveryMetadata;
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x00011E96 File Offset: 0x00010096
		public static EndpointDiscoveryMetadata11 FromEndpointDiscoveryMetadata(EndpointDiscoveryMetadata endpointDiscoveryMetadata)
		{
			if (endpointDiscoveryMetadata == null)
			{
				throw FxTrace.Exception.ArgumentNull("endpointDiscoveryMetadata");
			}
			return new EndpointDiscoveryMetadata11(endpointDiscoveryMetadata);
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x00011EB1 File Offset: 0x000100B1
		public static XmlQualifiedName GetSchema(XmlSchemaSet schemaSet)
		{
			if (schemaSet == null)
			{
				throw FxTrace.Exception.ArgumentNull("schemaSet");
			}
			return SchemaUtility.EnsureProbeMatchSchema(DiscoveryVersion.WSDiscovery11, schemaSet);
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x00011ED1 File Offset: 0x000100D1
		public EndpointDiscoveryMetadata ToEndpointDiscoveryMetadata()
		{
			return this.endpointDiscoveryMetadata;
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x00006B84 File Offset: 0x00004D84
		public XmlSchema GetSchema()
		{
			return null;
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x00011ED9 File Offset: 0x000100D9
		public void ReadXml(XmlReader reader)
		{
			this.endpointDiscoveryMetadata.ReadFrom(DiscoveryVersion.WSDiscovery11, reader);
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x00011EEC File Offset: 0x000100EC
		public void WriteXml(XmlWriter writer)
		{
			this.endpointDiscoveryMetadata.WriteTo(DiscoveryVersion.WSDiscovery11, writer);
		}

		// Token: 0x04000198 RID: 408
		private EndpointDiscoveryMetadata endpointDiscoveryMetadata;
	}
}
