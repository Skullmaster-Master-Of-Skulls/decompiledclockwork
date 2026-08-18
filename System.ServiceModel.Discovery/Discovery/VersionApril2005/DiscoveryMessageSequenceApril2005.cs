using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.ServiceModel.Discovery.VersionApril2005
{
	// Token: 0x0200007B RID: 123
	[XmlSchemaProvider("GetSchema")]
	public class DiscoveryMessageSequenceApril2005 : IXmlSerializable
	{
		// Token: 0x060005D3 RID: 1491 RVA: 0x000107DB File Offset: 0x0000E9DB
		private DiscoveryMessageSequenceApril2005()
		{
			this.discoveryMessageSequence = new DiscoveryMessageSequence();
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x000107EE File Offset: 0x0000E9EE
		private DiscoveryMessageSequenceApril2005(DiscoveryMessageSequence discoveryMessageSequence)
		{
			this.discoveryMessageSequence = discoveryMessageSequence;
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x000107FD File Offset: 0x0000E9FD
		public static DiscoveryMessageSequenceApril2005 FromDiscoveryMessageSequence(DiscoveryMessageSequence discoveryMessageSequence)
		{
			if (discoveryMessageSequence == null)
			{
				throw FxTrace.Exception.ArgumentNull("discoveryMessageSequence");
			}
			return new DiscoveryMessageSequenceApril2005(discoveryMessageSequence);
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x0001081E File Offset: 0x0000EA1E
		public static XmlQualifiedName GetSchema(XmlSchemaSet schemaSet)
		{
			if (schemaSet == null)
			{
				throw FxTrace.Exception.ArgumentNull("schemaSet");
			}
			return SchemaUtility.EnsureAppSequenceSchema(DiscoveryVersion.WSDiscoveryApril2005, schemaSet);
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x0001083E File Offset: 0x0000EA3E
		public DiscoveryMessageSequence ToDiscoveryMessageSequence()
		{
			return this.discoveryMessageSequence;
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x00006B84 File Offset: 0x00004D84
		public XmlSchema GetSchema()
		{
			return null;
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x00010846 File Offset: 0x0000EA46
		public void ReadXml(XmlReader reader)
		{
			this.discoveryMessageSequence.ReadFrom(reader);
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x00010854 File Offset: 0x0000EA54
		public void WriteXml(XmlWriter writer)
		{
			this.discoveryMessageSequence.WriteTo(writer);
		}

		// Token: 0x04000164 RID: 356
		private DiscoveryMessageSequence discoveryMessageSequence;
	}
}
