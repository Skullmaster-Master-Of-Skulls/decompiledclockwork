using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.ServiceModel.Discovery.VersionCD1
{
	// Token: 0x02000071 RID: 113
	[XmlSchemaProvider("GetSchema")]
	public class ResolveCriteriaCD1 : IXmlSerializable
	{
		// Token: 0x06000573 RID: 1395 RVA: 0x0000FEC7 File Offset: 0x0000E0C7
		private ResolveCriteriaCD1()
		{
			this.resolveCriteria = new ResolveCriteria();
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x0000FEDA File Offset: 0x0000E0DA
		private ResolveCriteriaCD1(ResolveCriteria resolveCriteria)
		{
			this.resolveCriteria = resolveCriteria;
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x0000FEE9 File Offset: 0x0000E0E9
		public static ResolveCriteriaCD1 FromResolveCriteria(ResolveCriteria resolveCriteria)
		{
			if (resolveCriteria == null)
			{
				throw FxTrace.Exception.ArgumentNull("resolveCriteria");
			}
			return new ResolveCriteriaCD1(resolveCriteria);
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x0000FF04 File Offset: 0x0000E104
		public static XmlQualifiedName GetSchema(XmlSchemaSet schemaSet)
		{
			if (schemaSet == null)
			{
				throw FxTrace.Exception.ArgumentNull("schemaSet");
			}
			return SchemaUtility.EnsureResolveSchema(DiscoveryVersion.WSDiscoveryCD1, schemaSet);
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x0000FF24 File Offset: 0x0000E124
		public ResolveCriteria ToResolveCriteria()
		{
			return this.resolveCriteria;
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x00006B84 File Offset: 0x00004D84
		public XmlSchema GetSchema()
		{
			return null;
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x0000FF2C File Offset: 0x0000E12C
		public void ReadXml(XmlReader reader)
		{
			this.resolveCriteria.ReadFrom(DiscoveryVersion.WSDiscoveryCD1, reader);
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x0000FF3F File Offset: 0x0000E13F
		public void WriteXml(XmlWriter writer)
		{
			this.resolveCriteria.WriteTo(DiscoveryVersion.WSDiscoveryCD1, writer);
		}

		// Token: 0x04000152 RID: 338
		private ResolveCriteria resolveCriteria;
	}
}
