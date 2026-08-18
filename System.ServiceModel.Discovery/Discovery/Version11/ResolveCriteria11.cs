using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.ServiceModel.Discovery.Version11
{
	// Token: 0x020000A3 RID: 163
	[XmlSchemaProvider("GetSchema")]
	public class ResolveCriteria11 : IXmlSerializable
	{
		// Token: 0x060006F3 RID: 1779 RVA: 0x0001219B File Offset: 0x0001039B
		private ResolveCriteria11()
		{
			this.resolveCriteria = new ResolveCriteria();
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x000121AE File Offset: 0x000103AE
		private ResolveCriteria11(ResolveCriteria resolveCriteria)
		{
			this.resolveCriteria = resolveCriteria;
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x000121BD File Offset: 0x000103BD
		public static ResolveCriteria11 FromResolveCriteria(ResolveCriteria resolveCriteria)
		{
			if (resolveCriteria == null)
			{
				throw FxTrace.Exception.ArgumentNull("resolveCriteria");
			}
			return new ResolveCriteria11(resolveCriteria);
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x000121D8 File Offset: 0x000103D8
		public static XmlQualifiedName GetSchema(XmlSchemaSet schemaSet)
		{
			if (schemaSet == null)
			{
				throw FxTrace.Exception.ArgumentNull("schemaSet");
			}
			return SchemaUtility.EnsureResolveSchema(DiscoveryVersion.WSDiscovery11, schemaSet);
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x000121F8 File Offset: 0x000103F8
		public ResolveCriteria ToResolveCriteria()
		{
			return this.resolveCriteria;
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x00006B84 File Offset: 0x00004D84
		public XmlSchema GetSchema()
		{
			return null;
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x00012200 File Offset: 0x00010400
		public void ReadXml(XmlReader reader)
		{
			this.resolveCriteria.ReadFrom(DiscoveryVersion.WSDiscovery11, reader);
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x00012213 File Offset: 0x00010413
		public void WriteXml(XmlWriter writer)
		{
			this.resolveCriteria.WriteTo(DiscoveryVersion.WSDiscovery11, writer);
		}

		// Token: 0x0400019F RID: 415
		private ResolveCriteria resolveCriteria;
	}
}
