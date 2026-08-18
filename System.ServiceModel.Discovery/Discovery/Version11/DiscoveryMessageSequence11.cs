using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.ServiceModel.Discovery.Version11
{
	// Token: 0x02000094 RID: 148
	[XmlSchemaProvider("GetSchema")]
	public class DiscoveryMessageSequence11 : IXmlSerializable
	{
		// Token: 0x06000691 RID: 1681 RVA: 0x00011A76 File Offset: 0x0000FC76
		private DiscoveryMessageSequence11()
		{
			this.discoveryMessageSequence = new DiscoveryMessageSequence();
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x00011A89 File Offset: 0x0000FC89
		private DiscoveryMessageSequence11(DiscoveryMessageSequence discoveryMessageSequence)
		{
			this.discoveryMessageSequence = discoveryMessageSequence;
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x00011A98 File Offset: 0x0000FC98
		public static DiscoveryMessageSequence11 FromDiscoveryMessageSequence(DiscoveryMessageSequence discoveryMessageSequence)
		{
			if (discoveryMessageSequence == null)
			{
				throw FxTrace.Exception.ArgumentNull("discoveryMessageSequence");
			}
			return new DiscoveryMessageSequence11(discoveryMessageSequence);
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x00011AB9 File Offset: 0x0000FCB9
		public static XmlQualifiedName GetSchema(XmlSchemaSet schemaSet)
		{
			if (schemaSet == null)
			{
				throw FxTrace.Exception.ArgumentNull("schemaSet");
			}
			return SchemaUtility.EnsureAppSequenceSchema(DiscoveryVersion.WSDiscovery11, schemaSet);
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x00011AD9 File Offset: 0x0000FCD9
		public DiscoveryMessageSequence ToDiscoveryMessageSequence()
		{
			return this.discoveryMessageSequence;
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x00006B84 File Offset: 0x00004D84
		public XmlSchema GetSchema()
		{
			return null;
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x00011AE1 File Offset: 0x0000FCE1
		public void ReadXml(XmlReader reader)
		{
			this.discoveryMessageSequence.ReadFrom(reader);
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x00011AEF File Offset: 0x0000FCEF
		public void WriteXml(XmlWriter writer)
		{
			this.discoveryMessageSequence.WriteTo(writer);
		}

		// Token: 0x0400018B RID: 395
		private DiscoveryMessageSequence discoveryMessageSequence;
	}
}
