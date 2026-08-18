using System;
using System.Collections.ObjectModel;
using System.ServiceModel.Channels;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.ServiceModel.Description
{
	// Token: 0x020003F1 RID: 1009
	[XmlRoot(ElementName = "MetadataReference", Namespace = "http://schemas.xmlsoap.org/ws/2004/09/mex")]
	public class MetadataReference : IXmlSerializable
	{
		// Token: 0x060025F9 RID: 9721 RVA: 0x00089621 File Offset: 0x00087821
		public MetadataReference()
		{
		}

		// Token: 0x060025FA RID: 9722 RVA: 0x00089634 File Offset: 0x00087834
		public MetadataReference(EndpointAddress address, AddressingVersion addressVersion)
		{
			this.address = address;
			this.addressVersion = addressVersion;
		}

		// Token: 0x1700099B RID: 2459
		// (get) Token: 0x060025FB RID: 9723 RVA: 0x00089655 File Offset: 0x00087855
		// (set) Token: 0x060025FC RID: 9724 RVA: 0x0008965D File Offset: 0x0008785D
		public EndpointAddress Address
		{
			get
			{
				return this.address;
			}
			set
			{
				this.address = value;
			}
		}

		// Token: 0x1700099C RID: 2460
		// (get) Token: 0x060025FD RID: 9725 RVA: 0x00089666 File Offset: 0x00087866
		// (set) Token: 0x060025FE RID: 9726 RVA: 0x0008966E File Offset: 0x0008786E
		public AddressingVersion AddressVersion
		{
			get
			{
				return this.addressVersion;
			}
			set
			{
				this.addressVersion = value;
			}
		}

		// Token: 0x060025FF RID: 9727 RVA: 0x00089677 File Offset: 0x00087877
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x06002600 RID: 9728 RVA: 0x0008967A File Offset: 0x0008787A
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			this.address = EndpointAddress.ReadFrom(XmlDictionaryReader.CreateDictionaryReader(reader), out this.addressVersion);
		}

		// Token: 0x06002601 RID: 9729 RVA: 0x00089693 File Offset: 0x00087893
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.address != null)
			{
				this.address.WriteContentsTo(this.addressVersion, writer);
			}
		}

		// Token: 0x04002171 RID: 8561
		private EndpointAddress address;

		// Token: 0x04002172 RID: 8562
		private AddressingVersion addressVersion;

		// Token: 0x04002173 RID: 8563
		private Collection<XmlAttribute> attributes = new Collection<XmlAttribute>();

		// Token: 0x04002174 RID: 8564
		private static XmlDocument Document = new XmlDocument();
	}
}
