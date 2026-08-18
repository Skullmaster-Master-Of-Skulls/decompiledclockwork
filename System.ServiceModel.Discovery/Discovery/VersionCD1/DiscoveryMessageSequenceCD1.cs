using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.ServiceModel.Discovery.VersionCD1
{
	// Token: 0x02000062 RID: 98
	[XmlSchemaProvider("GetSchema")]
	public class DiscoveryMessageSequenceCD1 : IXmlSerializable
	{
		// Token: 0x06000511 RID: 1297 RVA: 0x0000F7A2 File Offset: 0x0000D9A2
		private DiscoveryMessageSequenceCD1()
		{
			this.discoveryMessageSequence = new DiscoveryMessageSequence();
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x0000F7B5 File Offset: 0x0000D9B5
		private DiscoveryMessageSequenceCD1(DiscoveryMessageSequence discoveryMessageSequence)
		{
			this.discoveryMessageSequence = discoveryMessageSequence;
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x0000F7C4 File Offset: 0x0000D9C4
		public static DiscoveryMessageSequenceCD1 FromDiscoveryMessageSequence(DiscoveryMessageSequence discoveryMessageSequence)
		{
			if (discoveryMessageSequence == null)
			{
				throw FxTrace.Exception.ArgumentNull("discoveryMessageSequence");
			}
			return new DiscoveryMessageSequenceCD1(discoveryMessageSequence);
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x0000F7E5 File Offset: 0x0000D9E5
		public static XmlQualifiedName GetSchema(XmlSchemaSet schemaSet)
		{
			if (schemaSet == null)
			{
				throw FxTrace.Exception.ArgumentNull("schemaSet");
			}
			return SchemaUtility.EnsureAppSequenceSchema(DiscoveryVersion.WSDiscoveryCD1, schemaSet);
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x0000F805 File Offset: 0x0000DA05
		public DiscoveryMessageSequence ToDiscoveryMessageSequence()
		{
			return this.discoveryMessageSequence;
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x00006B84 File Offset: 0x00004D84
		public XmlSchema GetSchema()
		{
			return null;
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x0000F80D File Offset: 0x0000DA0D
		public void ReadXml(XmlReader reader)
		{
			this.discoveryMessageSequence.ReadFrom(reader);
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x0000F81B File Offset: 0x0000DA1B
		public void WriteXml(XmlWriter writer)
		{
			this.discoveryMessageSequence.WriteTo(writer);
		}

		// Token: 0x0400013E RID: 318
		private DiscoveryMessageSequence discoveryMessageSequence;
	}
}
