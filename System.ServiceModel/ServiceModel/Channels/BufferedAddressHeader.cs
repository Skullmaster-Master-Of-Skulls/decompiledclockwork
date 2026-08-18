using System;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009B0 RID: 2480
	internal class BufferedAddressHeader : AddressHeader
	{
		// Token: 0x0600614F RID: 24911 RVA: 0x0016AD34 File Offset: 0x00168F34
		public BufferedAddressHeader(XmlDictionaryReader reader)
		{
			this.buffer = new XmlBuffer(int.MaxValue);
			XmlDictionaryWriter xmlDictionaryWriter = this.buffer.OpenSection(reader.Quotas);
			this.name = reader.LocalName;
			this.ns = reader.NamespaceURI;
			xmlDictionaryWriter.WriteNode(reader, false);
			this.buffer.CloseSection();
			this.buffer.Close();
			this.isReferenceProperty = false;
		}

		// Token: 0x06006150 RID: 24912 RVA: 0x0016ADA6 File Offset: 0x00168FA6
		public BufferedAddressHeader(XmlDictionaryReader reader, bool isReferenceProperty) : this(reader)
		{
			this.isReferenceProperty = isReferenceProperty;
		}

		// Token: 0x17001775 RID: 6005
		// (get) Token: 0x06006151 RID: 24913 RVA: 0x0016ADB6 File Offset: 0x00168FB6
		public bool IsReferencePropertyHeader
		{
			get
			{
				return this.isReferenceProperty;
			}
		}

		// Token: 0x17001776 RID: 6006
		// (get) Token: 0x06006152 RID: 24914 RVA: 0x0016ADBE File Offset: 0x00168FBE
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17001777 RID: 6007
		// (get) Token: 0x06006153 RID: 24915 RVA: 0x0016ADC6 File Offset: 0x00168FC6
		public override string Namespace
		{
			get
			{
				return this.ns;
			}
		}

		// Token: 0x06006154 RID: 24916 RVA: 0x0016ADCE File Offset: 0x00168FCE
		public override XmlDictionaryReader GetAddressHeaderReader()
		{
			return this.buffer.GetReader(0);
		}

		// Token: 0x06006155 RID: 24917 RVA: 0x0016ADDC File Offset: 0x00168FDC
		protected override void OnWriteStartAddressHeader(XmlDictionaryWriter writer)
		{
			XmlDictionaryReader addressHeaderReader = this.GetAddressHeaderReader();
			writer.WriteStartElement(addressHeaderReader.Prefix, addressHeaderReader.LocalName, addressHeaderReader.NamespaceURI);
			writer.WriteAttributes(addressHeaderReader, false);
			addressHeaderReader.Close();
		}

		// Token: 0x06006156 RID: 24918 RVA: 0x0016AE18 File Offset: 0x00169018
		protected override void OnWriteAddressHeaderContents(XmlDictionaryWriter writer)
		{
			XmlDictionaryReader addressHeaderReader = this.GetAddressHeaderReader();
			addressHeaderReader.ReadStartElement();
			while (addressHeaderReader.NodeType != XmlNodeType.EndElement)
			{
				writer.WriteNode(addressHeaderReader, false);
			}
			addressHeaderReader.ReadEndElement();
			addressHeaderReader.Close();
		}

		// Token: 0x040038CF RID: 14543
		private string name;

		// Token: 0x040038D0 RID: 14544
		private string ns;

		// Token: 0x040038D1 RID: 14545
		private XmlBuffer buffer;

		// Token: 0x040038D2 RID: 14546
		private bool isReferenceProperty;
	}
}
