using System;
using System.Xml;

namespace System.IdentityModel
{
	// Token: 0x02000045 RID: 69
	internal class IdentityModelWrappedXmlDictionaryReader : XmlDictionaryReader, IXmlLineInfo
	{
		// Token: 0x0600027E RID: 638 RVA: 0x0000B347 File Offset: 0x00009547
		public IdentityModelWrappedXmlDictionaryReader(XmlReader reader, XmlDictionaryReaderQuotas xmlDictionaryReaderQuotas)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			if (xmlDictionaryReaderQuotas == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("xmlDictionaryReaderQuotas");
			}
			this._reader = reader;
			this._xmlDictionaryReaderQuotas = xmlDictionaryReaderQuotas;
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600027F RID: 639 RVA: 0x0000B383 File Offset: 0x00009583
		public override int AttributeCount
		{
			get
			{
				return this._reader.AttributeCount;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000280 RID: 640 RVA: 0x0000B390 File Offset: 0x00009590
		public override string BaseURI
		{
			get
			{
				return this._reader.BaseURI;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000281 RID: 641 RVA: 0x0000B39D File Offset: 0x0000959D
		public override bool CanReadBinaryContent
		{
			get
			{
				return this._reader.CanReadBinaryContent;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000282 RID: 642 RVA: 0x0000B3AA File Offset: 0x000095AA
		public override bool CanReadValueChunk
		{
			get
			{
				return this._reader.CanReadValueChunk;
			}
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000B3B7 File Offset: 0x000095B7
		public override void Close()
		{
			this._reader.Close();
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000284 RID: 644 RVA: 0x0000B3C4 File Offset: 0x000095C4
		public override int Depth
		{
			get
			{
				return this._reader.Depth;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000285 RID: 645 RVA: 0x0000B3D1 File Offset: 0x000095D1
		public override bool EOF
		{
			get
			{
				return this._reader.EOF;
			}
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000B3DE File Offset: 0x000095DE
		public override string GetAttribute(int index)
		{
			return this._reader.GetAttribute(index);
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000B3EC File Offset: 0x000095EC
		public override string GetAttribute(string name)
		{
			return this._reader.GetAttribute(name);
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000B3FA File Offset: 0x000095FA
		public override string GetAttribute(string name, string namespaceUri)
		{
			return this._reader.GetAttribute(name, namespaceUri);
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000289 RID: 649 RVA: 0x0000B409 File Offset: 0x00009609
		public override bool HasValue
		{
			get
			{
				return this._reader.HasValue;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600028A RID: 650 RVA: 0x0000B416 File Offset: 0x00009616
		public override bool IsDefault
		{
			get
			{
				return this._reader.IsDefault;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600028B RID: 651 RVA: 0x0000B423 File Offset: 0x00009623
		public override bool IsEmptyElement
		{
			get
			{
				return this._reader.IsEmptyElement;
			}
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000B430 File Offset: 0x00009630
		public override bool IsStartElement(string name)
		{
			return this._reader.IsStartElement(name);
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000B43E File Offset: 0x0000963E
		public override bool IsStartElement(string localName, string namespaceUri)
		{
			return this._reader.IsStartElement(localName, namespaceUri);
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600028E RID: 654 RVA: 0x0000B44D File Offset: 0x0000964D
		public override string LocalName
		{
			get
			{
				return this._reader.LocalName;
			}
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000B45A File Offset: 0x0000965A
		public override string LookupNamespace(string namespaceUri)
		{
			return this._reader.LookupNamespace(namespaceUri);
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000B468 File Offset: 0x00009668
		public override void MoveToAttribute(int index)
		{
			this._reader.MoveToAttribute(index);
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000B476 File Offset: 0x00009676
		public override bool MoveToAttribute(string name)
		{
			return this._reader.MoveToAttribute(name);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000B484 File Offset: 0x00009684
		public override bool MoveToAttribute(string name, string namespaceUri)
		{
			return this._reader.MoveToAttribute(name, namespaceUri);
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000B493 File Offset: 0x00009693
		public override bool MoveToElement()
		{
			return this._reader.MoveToElement();
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000B4A0 File Offset: 0x000096A0
		public override bool MoveToFirstAttribute()
		{
			return this._reader.MoveToFirstAttribute();
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000B4AD File Offset: 0x000096AD
		public override bool MoveToNextAttribute()
		{
			return this._reader.MoveToNextAttribute();
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000296 RID: 662 RVA: 0x0000B4BA File Offset: 0x000096BA
		public override string Name
		{
			get
			{
				return this._reader.Name;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000297 RID: 663 RVA: 0x0000B4C7 File Offset: 0x000096C7
		public override string NamespaceURI
		{
			get
			{
				return this._reader.NamespaceURI;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000298 RID: 664 RVA: 0x0000B4D4 File Offset: 0x000096D4
		public override XmlNameTable NameTable
		{
			get
			{
				return this._reader.NameTable;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000299 RID: 665 RVA: 0x0000B4E1 File Offset: 0x000096E1
		public override XmlNodeType NodeType
		{
			get
			{
				return this._reader.NodeType;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600029A RID: 666 RVA: 0x0000B4EE File Offset: 0x000096EE
		public override string Prefix
		{
			get
			{
				return this._reader.Prefix;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600029B RID: 667 RVA: 0x0000B4FB File Offset: 0x000096FB
		public override char QuoteChar
		{
			get
			{
				return this._reader.QuoteChar;
			}
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000B508 File Offset: 0x00009708
		public override bool Read()
		{
			return this._reader.Read();
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000B515 File Offset: 0x00009715
		public override bool ReadAttributeValue()
		{
			return this._reader.ReadAttributeValue();
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000B522 File Offset: 0x00009722
		public override string ReadElementString(string name)
		{
			return this._reader.ReadElementString(name);
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000B530 File Offset: 0x00009730
		public override string ReadElementString(string localName, string namespaceUri)
		{
			return this._reader.ReadElementString(localName, namespaceUri);
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000B53F File Offset: 0x0000973F
		public override string ReadInnerXml()
		{
			return this._reader.ReadInnerXml();
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000B54C File Offset: 0x0000974C
		public override string ReadOuterXml()
		{
			return this._reader.ReadOuterXml();
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000B559 File Offset: 0x00009759
		public override void ReadStartElement(string name)
		{
			this._reader.ReadStartElement(name);
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000B567 File Offset: 0x00009767
		public override void ReadStartElement(string localName, string namespaceUri)
		{
			this._reader.ReadStartElement(localName, namespaceUri);
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000B576 File Offset: 0x00009776
		public override void ReadEndElement()
		{
			this._reader.ReadEndElement();
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000B583 File Offset: 0x00009783
		public override string ReadString()
		{
			return this._reader.ReadString();
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x0000B590 File Offset: 0x00009790
		public override ReadState ReadState
		{
			get
			{
				return this._reader.ReadState;
			}
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000B59D File Offset: 0x0000979D
		public override void ResolveEntity()
		{
			this._reader.ResolveEntity();
		}

		// Token: 0x170000AF RID: 175
		public override string this[int index]
		{
			get
			{
				return this._reader[index];
			}
		}

		// Token: 0x170000B0 RID: 176
		public override string this[string name]
		{
			get
			{
				return this._reader[name];
			}
		}

		// Token: 0x170000B1 RID: 177
		public override string this[string name, string namespaceUri]
		{
			get
			{
				return this._reader[name, namespaceUri];
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060002AB RID: 683 RVA: 0x0000B5D5 File Offset: 0x000097D5
		public override string Value
		{
			get
			{
				return this._reader.Value;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060002AC RID: 684 RVA: 0x0000B5E2 File Offset: 0x000097E2
		public override string XmlLang
		{
			get
			{
				return this._reader.XmlLang;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060002AD RID: 685 RVA: 0x0000B5EF File Offset: 0x000097EF
		public override XmlSpace XmlSpace
		{
			get
			{
				return this._reader.XmlSpace;
			}
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000B5FC File Offset: 0x000097FC
		public override int ReadElementContentAsBase64(byte[] buffer, int offset, int count)
		{
			return this._reader.ReadElementContentAsBase64(buffer, offset, count);
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000B60C File Offset: 0x0000980C
		public override int ReadContentAsBase64(byte[] buffer, int offset, int count)
		{
			return this._reader.ReadContentAsBase64(buffer, offset, count);
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000B61C File Offset: 0x0000981C
		public override int ReadElementContentAsBinHex(byte[] buffer, int offset, int count)
		{
			return this._reader.ReadElementContentAsBinHex(buffer, offset, count);
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000B62C File Offset: 0x0000982C
		public override int ReadContentAsBinHex(byte[] buffer, int offset, int count)
		{
			return this._reader.ReadContentAsBinHex(buffer, offset, count);
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000B63C File Offset: 0x0000983C
		public override int ReadValueChunk(char[] chars, int offset, int count)
		{
			return this._reader.ReadValueChunk(chars, offset, count);
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x0000B64C File Offset: 0x0000984C
		public override Type ValueType
		{
			get
			{
				return this._reader.ValueType;
			}
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000B659 File Offset: 0x00009859
		public override bool ReadContentAsBoolean()
		{
			return this._reader.ReadContentAsBoolean();
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000B666 File Offset: 0x00009866
		public override DateTime ReadContentAsDateTime()
		{
			return this._reader.ReadContentAsDateTime();
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000B673 File Offset: 0x00009873
		public override decimal ReadContentAsDecimal()
		{
			return (decimal)this._reader.ReadContentAs(typeof(decimal), null);
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000B690 File Offset: 0x00009890
		public override double ReadContentAsDouble()
		{
			return this._reader.ReadContentAsDouble();
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000B69D File Offset: 0x0000989D
		public override int ReadContentAsInt()
		{
			return this._reader.ReadContentAsInt();
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000B6AA File Offset: 0x000098AA
		public override long ReadContentAsLong()
		{
			return this._reader.ReadContentAsLong();
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000B6B7 File Offset: 0x000098B7
		public override float ReadContentAsFloat()
		{
			return this._reader.ReadContentAsFloat();
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000B6C4 File Offset: 0x000098C4
		public override string ReadContentAsString()
		{
			return this._reader.ReadContentAsString();
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000B6D1 File Offset: 0x000098D1
		public override object ReadContentAs(Type valueType, IXmlNamespaceResolver namespaceResolver)
		{
			return this._reader.ReadContentAs(valueType, namespaceResolver);
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000B6E0 File Offset: 0x000098E0
		public bool HasLineInfo()
		{
			IXmlLineInfo xmlLineInfo = this._reader as IXmlLineInfo;
			return xmlLineInfo != null && xmlLineInfo.HasLineInfo();
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060002BE RID: 702 RVA: 0x0000B704 File Offset: 0x00009904
		public int LineNumber
		{
			get
			{
				IXmlLineInfo xmlLineInfo = this._reader as IXmlLineInfo;
				if (xmlLineInfo == null)
				{
					return 1;
				}
				return xmlLineInfo.LineNumber;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060002BF RID: 703 RVA: 0x0000B728 File Offset: 0x00009928
		public int LinePosition
		{
			get
			{
				IXmlLineInfo xmlLineInfo = this._reader as IXmlLineInfo;
				if (xmlLineInfo == null)
				{
					return 1;
				}
				return xmlLineInfo.LinePosition;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x0000B74C File Offset: 0x0000994C
		public override XmlDictionaryReaderQuotas Quotas
		{
			get
			{
				return this._xmlDictionaryReaderQuotas;
			}
		}

		// Token: 0x04000293 RID: 659
		private XmlReader _reader;

		// Token: 0x04000294 RID: 660
		private XmlDictionaryReaderQuotas _xmlDictionaryReaderQuotas;
	}
}
