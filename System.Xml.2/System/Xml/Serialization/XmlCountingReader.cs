using System;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	// Token: 0x02000190 RID: 400
	internal class XmlCountingReader : XmlReader, IXmlTextParser, IXmlLineInfo
	{
		// Token: 0x06001A37 RID: 6711 RVA: 0x00075B33 File Offset: 0x00073D33
		internal XmlCountingReader(XmlReader xmlReader)
		{
			if (xmlReader == null)
			{
				throw new ArgumentNullException("xmlReader");
			}
			this.innerReader = xmlReader;
			this.advanceCount = 0;
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x06001A38 RID: 6712 RVA: 0x00075B57 File Offset: 0x00073D57
		internal int AdvanceCount
		{
			get
			{
				return this.advanceCount;
			}
		}

		// Token: 0x06001A39 RID: 6713 RVA: 0x00075B5F File Offset: 0x00073D5F
		private void IncrementCount()
		{
			if (this.advanceCount == 2147483647)
			{
				this.advanceCount = 0;
				return;
			}
			this.advanceCount++;
		}

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x06001A3A RID: 6714 RVA: 0x00075B84 File Offset: 0x00073D84
		public override XmlReaderSettings Settings
		{
			get
			{
				return this.innerReader.Settings;
			}
		}

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x06001A3B RID: 6715 RVA: 0x00075B91 File Offset: 0x00073D91
		public override XmlNodeType NodeType
		{
			get
			{
				return this.innerReader.NodeType;
			}
		}

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x06001A3C RID: 6716 RVA: 0x00075B9E File Offset: 0x00073D9E
		public override string Name
		{
			get
			{
				return this.innerReader.Name;
			}
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06001A3D RID: 6717 RVA: 0x00075BAB File Offset: 0x00073DAB
		public override string LocalName
		{
			get
			{
				return this.innerReader.LocalName;
			}
		}

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x06001A3E RID: 6718 RVA: 0x00075BB8 File Offset: 0x00073DB8
		public override string NamespaceURI
		{
			get
			{
				return this.innerReader.NamespaceURI;
			}
		}

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x06001A3F RID: 6719 RVA: 0x00075BC5 File Offset: 0x00073DC5
		public override string Prefix
		{
			get
			{
				return this.innerReader.Prefix;
			}
		}

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x06001A40 RID: 6720 RVA: 0x00075BD2 File Offset: 0x00073DD2
		public override bool HasValue
		{
			get
			{
				return this.innerReader.HasValue;
			}
		}

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x06001A41 RID: 6721 RVA: 0x00075BDF File Offset: 0x00073DDF
		public override string Value
		{
			get
			{
				return this.innerReader.Value;
			}
		}

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x06001A42 RID: 6722 RVA: 0x00075BEC File Offset: 0x00073DEC
		public override int Depth
		{
			get
			{
				return this.innerReader.Depth;
			}
		}

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x06001A43 RID: 6723 RVA: 0x00075BF9 File Offset: 0x00073DF9
		public override string BaseURI
		{
			get
			{
				return this.innerReader.BaseURI;
			}
		}

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x06001A44 RID: 6724 RVA: 0x00075C06 File Offset: 0x00073E06
		public override bool IsEmptyElement
		{
			get
			{
				return this.innerReader.IsEmptyElement;
			}
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x06001A45 RID: 6725 RVA: 0x00075C13 File Offset: 0x00073E13
		public override bool IsDefault
		{
			get
			{
				return this.innerReader.IsDefault;
			}
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x06001A46 RID: 6726 RVA: 0x00075C20 File Offset: 0x00073E20
		public override char QuoteChar
		{
			get
			{
				return this.innerReader.QuoteChar;
			}
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x06001A47 RID: 6727 RVA: 0x00075C2D File Offset: 0x00073E2D
		public override XmlSpace XmlSpace
		{
			get
			{
				return this.innerReader.XmlSpace;
			}
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x06001A48 RID: 6728 RVA: 0x00075C3A File Offset: 0x00073E3A
		public override string XmlLang
		{
			get
			{
				return this.innerReader.XmlLang;
			}
		}

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x06001A49 RID: 6729 RVA: 0x00075C47 File Offset: 0x00073E47
		public override IXmlSchemaInfo SchemaInfo
		{
			get
			{
				return this.innerReader.SchemaInfo;
			}
		}

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x06001A4A RID: 6730 RVA: 0x00075C54 File Offset: 0x00073E54
		public override Type ValueType
		{
			get
			{
				return this.innerReader.ValueType;
			}
		}

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x06001A4B RID: 6731 RVA: 0x00075C61 File Offset: 0x00073E61
		public override int AttributeCount
		{
			get
			{
				return this.innerReader.AttributeCount;
			}
		}

		// Token: 0x170005C7 RID: 1479
		public override string this[int i]
		{
			get
			{
				return this.innerReader[i];
			}
		}

		// Token: 0x170005C8 RID: 1480
		public override string this[string name]
		{
			get
			{
				return this.innerReader[name];
			}
		}

		// Token: 0x170005C9 RID: 1481
		public override string this[string name, string namespaceURI]
		{
			get
			{
				return this.innerReader[name, namespaceURI];
			}
		}

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x06001A4F RID: 6735 RVA: 0x00075C99 File Offset: 0x00073E99
		public override bool EOF
		{
			get
			{
				return this.innerReader.EOF;
			}
		}

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x06001A50 RID: 6736 RVA: 0x00075CA6 File Offset: 0x00073EA6
		public override ReadState ReadState
		{
			get
			{
				return this.innerReader.ReadState;
			}
		}

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x06001A51 RID: 6737 RVA: 0x00075CB3 File Offset: 0x00073EB3
		public override XmlNameTable NameTable
		{
			get
			{
				return this.innerReader.NameTable;
			}
		}

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x06001A52 RID: 6738 RVA: 0x00075CC0 File Offset: 0x00073EC0
		public override bool CanResolveEntity
		{
			get
			{
				return this.innerReader.CanResolveEntity;
			}
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x06001A53 RID: 6739 RVA: 0x00075CCD File Offset: 0x00073ECD
		public override bool CanReadBinaryContent
		{
			get
			{
				return this.innerReader.CanReadBinaryContent;
			}
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x06001A54 RID: 6740 RVA: 0x00075CDA File Offset: 0x00073EDA
		public override bool CanReadValueChunk
		{
			get
			{
				return this.innerReader.CanReadValueChunk;
			}
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x06001A55 RID: 6741 RVA: 0x00075CE7 File Offset: 0x00073EE7
		public override bool HasAttributes
		{
			get
			{
				return this.innerReader.HasAttributes;
			}
		}

		// Token: 0x06001A56 RID: 6742 RVA: 0x00075CF4 File Offset: 0x00073EF4
		public override void Close()
		{
			this.innerReader.Close();
		}

		// Token: 0x06001A57 RID: 6743 RVA: 0x00075D01 File Offset: 0x00073F01
		public override string GetAttribute(string name)
		{
			return this.innerReader.GetAttribute(name);
		}

		// Token: 0x06001A58 RID: 6744 RVA: 0x00075D0F File Offset: 0x00073F0F
		public override string GetAttribute(string name, string namespaceURI)
		{
			return this.innerReader.GetAttribute(name, namespaceURI);
		}

		// Token: 0x06001A59 RID: 6745 RVA: 0x00075D1E File Offset: 0x00073F1E
		public override string GetAttribute(int i)
		{
			return this.innerReader.GetAttribute(i);
		}

		// Token: 0x06001A5A RID: 6746 RVA: 0x00075D2C File Offset: 0x00073F2C
		public override bool MoveToAttribute(string name)
		{
			return this.innerReader.MoveToAttribute(name);
		}

		// Token: 0x06001A5B RID: 6747 RVA: 0x00075D3A File Offset: 0x00073F3A
		public override bool MoveToAttribute(string name, string ns)
		{
			return this.innerReader.MoveToAttribute(name, ns);
		}

		// Token: 0x06001A5C RID: 6748 RVA: 0x00075D49 File Offset: 0x00073F49
		public override void MoveToAttribute(int i)
		{
			this.innerReader.MoveToAttribute(i);
		}

		// Token: 0x06001A5D RID: 6749 RVA: 0x00075D57 File Offset: 0x00073F57
		public override bool MoveToFirstAttribute()
		{
			return this.innerReader.MoveToFirstAttribute();
		}

		// Token: 0x06001A5E RID: 6750 RVA: 0x00075D64 File Offset: 0x00073F64
		public override bool MoveToNextAttribute()
		{
			return this.innerReader.MoveToNextAttribute();
		}

		// Token: 0x06001A5F RID: 6751 RVA: 0x00075D71 File Offset: 0x00073F71
		public override bool MoveToElement()
		{
			return this.innerReader.MoveToElement();
		}

		// Token: 0x06001A60 RID: 6752 RVA: 0x00075D7E File Offset: 0x00073F7E
		public override string LookupNamespace(string prefix)
		{
			return this.innerReader.LookupNamespace(prefix);
		}

		// Token: 0x06001A61 RID: 6753 RVA: 0x00075D8C File Offset: 0x00073F8C
		public override bool ReadAttributeValue()
		{
			return this.innerReader.ReadAttributeValue();
		}

		// Token: 0x06001A62 RID: 6754 RVA: 0x00075D99 File Offset: 0x00073F99
		public override void ResolveEntity()
		{
			this.innerReader.ResolveEntity();
		}

		// Token: 0x06001A63 RID: 6755 RVA: 0x00075DA6 File Offset: 0x00073FA6
		public override bool IsStartElement()
		{
			return this.innerReader.IsStartElement();
		}

		// Token: 0x06001A64 RID: 6756 RVA: 0x00075DB3 File Offset: 0x00073FB3
		public override bool IsStartElement(string name)
		{
			return this.innerReader.IsStartElement(name);
		}

		// Token: 0x06001A65 RID: 6757 RVA: 0x00075DC1 File Offset: 0x00073FC1
		public override bool IsStartElement(string localname, string ns)
		{
			return this.innerReader.IsStartElement(localname, ns);
		}

		// Token: 0x06001A66 RID: 6758 RVA: 0x00075DD0 File Offset: 0x00073FD0
		public override XmlReader ReadSubtree()
		{
			return this.innerReader.ReadSubtree();
		}

		// Token: 0x06001A67 RID: 6759 RVA: 0x00075DDD File Offset: 0x00073FDD
		public override XmlNodeType MoveToContent()
		{
			return this.innerReader.MoveToContent();
		}

		// Token: 0x06001A68 RID: 6760 RVA: 0x00075DEA File Offset: 0x00073FEA
		public override bool Read()
		{
			this.IncrementCount();
			return this.innerReader.Read();
		}

		// Token: 0x06001A69 RID: 6761 RVA: 0x00075DFD File Offset: 0x00073FFD
		public override void Skip()
		{
			this.IncrementCount();
			this.innerReader.Skip();
		}

		// Token: 0x06001A6A RID: 6762 RVA: 0x00075E10 File Offset: 0x00074010
		public override string ReadInnerXml()
		{
			if (this.innerReader.NodeType != XmlNodeType.Attribute)
			{
				this.IncrementCount();
			}
			return this.innerReader.ReadInnerXml();
		}

		// Token: 0x06001A6B RID: 6763 RVA: 0x00075E31 File Offset: 0x00074031
		public override string ReadOuterXml()
		{
			if (this.innerReader.NodeType != XmlNodeType.Attribute)
			{
				this.IncrementCount();
			}
			return this.innerReader.ReadOuterXml();
		}

		// Token: 0x06001A6C RID: 6764 RVA: 0x00075E52 File Offset: 0x00074052
		public override object ReadContentAsObject()
		{
			this.IncrementCount();
			return this.innerReader.ReadContentAsObject();
		}

		// Token: 0x06001A6D RID: 6765 RVA: 0x00075E65 File Offset: 0x00074065
		public override bool ReadContentAsBoolean()
		{
			this.IncrementCount();
			return this.innerReader.ReadContentAsBoolean();
		}

		// Token: 0x06001A6E RID: 6766 RVA: 0x00075E78 File Offset: 0x00074078
		public override DateTime ReadContentAsDateTime()
		{
			this.IncrementCount();
			return this.innerReader.ReadContentAsDateTime();
		}

		// Token: 0x06001A6F RID: 6767 RVA: 0x00075E8B File Offset: 0x0007408B
		public override double ReadContentAsDouble()
		{
			this.IncrementCount();
			return this.innerReader.ReadContentAsDouble();
		}

		// Token: 0x06001A70 RID: 6768 RVA: 0x00075E9E File Offset: 0x0007409E
		public override int ReadContentAsInt()
		{
			this.IncrementCount();
			return this.innerReader.ReadContentAsInt();
		}

		// Token: 0x06001A71 RID: 6769 RVA: 0x00075EB1 File Offset: 0x000740B1
		public override long ReadContentAsLong()
		{
			this.IncrementCount();
			return this.innerReader.ReadContentAsLong();
		}

		// Token: 0x06001A72 RID: 6770 RVA: 0x00075EC4 File Offset: 0x000740C4
		public override string ReadContentAsString()
		{
			this.IncrementCount();
			return this.innerReader.ReadContentAsString();
		}

		// Token: 0x06001A73 RID: 6771 RVA: 0x00075ED7 File Offset: 0x000740D7
		public override object ReadContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver)
		{
			this.IncrementCount();
			return this.innerReader.ReadContentAs(returnType, namespaceResolver);
		}

		// Token: 0x06001A74 RID: 6772 RVA: 0x00075EEC File Offset: 0x000740EC
		public override object ReadElementContentAsObject()
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsObject();
		}

		// Token: 0x06001A75 RID: 6773 RVA: 0x00075EFF File Offset: 0x000740FF
		public override object ReadElementContentAsObject(string localName, string namespaceURI)
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsObject(localName, namespaceURI);
		}

		// Token: 0x06001A76 RID: 6774 RVA: 0x00075F14 File Offset: 0x00074114
		public override bool ReadElementContentAsBoolean()
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsBoolean();
		}

		// Token: 0x06001A77 RID: 6775 RVA: 0x00075F27 File Offset: 0x00074127
		public override bool ReadElementContentAsBoolean(string localName, string namespaceURI)
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsBoolean(localName, namespaceURI);
		}

		// Token: 0x06001A78 RID: 6776 RVA: 0x00075F3C File Offset: 0x0007413C
		public override DateTime ReadElementContentAsDateTime()
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsDateTime();
		}

		// Token: 0x06001A79 RID: 6777 RVA: 0x00075F4F File Offset: 0x0007414F
		public override DateTime ReadElementContentAsDateTime(string localName, string namespaceURI)
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsDateTime(localName, namespaceURI);
		}

		// Token: 0x06001A7A RID: 6778 RVA: 0x00075F64 File Offset: 0x00074164
		public override double ReadElementContentAsDouble()
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsDouble();
		}

		// Token: 0x06001A7B RID: 6779 RVA: 0x00075F77 File Offset: 0x00074177
		public override double ReadElementContentAsDouble(string localName, string namespaceURI)
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsDouble(localName, namespaceURI);
		}

		// Token: 0x06001A7C RID: 6780 RVA: 0x00075F8C File Offset: 0x0007418C
		public override int ReadElementContentAsInt()
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsInt();
		}

		// Token: 0x06001A7D RID: 6781 RVA: 0x00075F9F File Offset: 0x0007419F
		public override int ReadElementContentAsInt(string localName, string namespaceURI)
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsInt(localName, namespaceURI);
		}

		// Token: 0x06001A7E RID: 6782 RVA: 0x00075FB4 File Offset: 0x000741B4
		public override long ReadElementContentAsLong()
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsLong();
		}

		// Token: 0x06001A7F RID: 6783 RVA: 0x00075FC7 File Offset: 0x000741C7
		public override long ReadElementContentAsLong(string localName, string namespaceURI)
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsLong(localName, namespaceURI);
		}

		// Token: 0x06001A80 RID: 6784 RVA: 0x00075FDC File Offset: 0x000741DC
		public override string ReadElementContentAsString()
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsString();
		}

		// Token: 0x06001A81 RID: 6785 RVA: 0x00075FEF File Offset: 0x000741EF
		public override string ReadElementContentAsString(string localName, string namespaceURI)
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsString(localName, namespaceURI);
		}

		// Token: 0x06001A82 RID: 6786 RVA: 0x00076004 File Offset: 0x00074204
		public override object ReadElementContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver)
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAs(returnType, namespaceResolver);
		}

		// Token: 0x06001A83 RID: 6787 RVA: 0x00076019 File Offset: 0x00074219
		public override object ReadElementContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver, string localName, string namespaceURI)
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAs(returnType, namespaceResolver, localName, namespaceURI);
		}

		// Token: 0x06001A84 RID: 6788 RVA: 0x00076031 File Offset: 0x00074231
		public override int ReadContentAsBase64(byte[] buffer, int index, int count)
		{
			this.IncrementCount();
			return this.innerReader.ReadContentAsBase64(buffer, index, count);
		}

		// Token: 0x06001A85 RID: 6789 RVA: 0x00076047 File Offset: 0x00074247
		public override int ReadElementContentAsBase64(byte[] buffer, int index, int count)
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsBase64(buffer, index, count);
		}

		// Token: 0x06001A86 RID: 6790 RVA: 0x0007605D File Offset: 0x0007425D
		public override int ReadContentAsBinHex(byte[] buffer, int index, int count)
		{
			this.IncrementCount();
			return this.innerReader.ReadContentAsBinHex(buffer, index, count);
		}

		// Token: 0x06001A87 RID: 6791 RVA: 0x00076073 File Offset: 0x00074273
		public override int ReadElementContentAsBinHex(byte[] buffer, int index, int count)
		{
			this.IncrementCount();
			return this.innerReader.ReadElementContentAsBinHex(buffer, index, count);
		}

		// Token: 0x06001A88 RID: 6792 RVA: 0x00076089 File Offset: 0x00074289
		public override int ReadValueChunk(char[] buffer, int index, int count)
		{
			this.IncrementCount();
			return this.innerReader.ReadValueChunk(buffer, index, count);
		}

		// Token: 0x06001A89 RID: 6793 RVA: 0x0007609F File Offset: 0x0007429F
		public override string ReadString()
		{
			this.IncrementCount();
			return this.innerReader.ReadString();
		}

		// Token: 0x06001A8A RID: 6794 RVA: 0x000760B2 File Offset: 0x000742B2
		public override void ReadStartElement()
		{
			this.IncrementCount();
			this.innerReader.ReadStartElement();
		}

		// Token: 0x06001A8B RID: 6795 RVA: 0x000760C5 File Offset: 0x000742C5
		public override void ReadStartElement(string name)
		{
			this.IncrementCount();
			this.innerReader.ReadStartElement(name);
		}

		// Token: 0x06001A8C RID: 6796 RVA: 0x000760D9 File Offset: 0x000742D9
		public override void ReadStartElement(string localname, string ns)
		{
			this.IncrementCount();
			this.innerReader.ReadStartElement(localname, ns);
		}

		// Token: 0x06001A8D RID: 6797 RVA: 0x000760EE File Offset: 0x000742EE
		public override string ReadElementString()
		{
			this.IncrementCount();
			return this.innerReader.ReadElementString();
		}

		// Token: 0x06001A8E RID: 6798 RVA: 0x00076101 File Offset: 0x00074301
		public override string ReadElementString(string name)
		{
			this.IncrementCount();
			return this.innerReader.ReadElementString(name);
		}

		// Token: 0x06001A8F RID: 6799 RVA: 0x00076115 File Offset: 0x00074315
		public override string ReadElementString(string localname, string ns)
		{
			this.IncrementCount();
			return this.innerReader.ReadElementString(localname, ns);
		}

		// Token: 0x06001A90 RID: 6800 RVA: 0x0007612A File Offset: 0x0007432A
		public override void ReadEndElement()
		{
			this.IncrementCount();
			this.innerReader.ReadEndElement();
		}

		// Token: 0x06001A91 RID: 6801 RVA: 0x0007613D File Offset: 0x0007433D
		public override bool ReadToFollowing(string name)
		{
			this.IncrementCount();
			return this.ReadToFollowing(name);
		}

		// Token: 0x06001A92 RID: 6802 RVA: 0x0007614C File Offset: 0x0007434C
		public override bool ReadToFollowing(string localName, string namespaceURI)
		{
			this.IncrementCount();
			return this.innerReader.ReadToFollowing(localName, namespaceURI);
		}

		// Token: 0x06001A93 RID: 6803 RVA: 0x00076161 File Offset: 0x00074361
		public override bool ReadToDescendant(string name)
		{
			this.IncrementCount();
			return this.innerReader.ReadToDescendant(name);
		}

		// Token: 0x06001A94 RID: 6804 RVA: 0x00076175 File Offset: 0x00074375
		public override bool ReadToDescendant(string localName, string namespaceURI)
		{
			this.IncrementCount();
			return this.innerReader.ReadToDescendant(localName, namespaceURI);
		}

		// Token: 0x06001A95 RID: 6805 RVA: 0x0007618A File Offset: 0x0007438A
		public override bool ReadToNextSibling(string name)
		{
			this.IncrementCount();
			return this.innerReader.ReadToNextSibling(name);
		}

		// Token: 0x06001A96 RID: 6806 RVA: 0x0007619E File Offset: 0x0007439E
		public override bool ReadToNextSibling(string localName, string namespaceURI)
		{
			this.IncrementCount();
			return this.innerReader.ReadToNextSibling(localName, namespaceURI);
		}

		// Token: 0x06001A97 RID: 6807 RVA: 0x000761B4 File Offset: 0x000743B4
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					IDisposable disposable = this.innerReader;
					if (disposable != null)
					{
						disposable.Dispose();
					}
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x06001A98 RID: 6808 RVA: 0x000761F0 File Offset: 0x000743F0
		// (set) Token: 0x06001A99 RID: 6809 RVA: 0x0007622C File Offset: 0x0007442C
		bool IXmlTextParser.Normalized
		{
			get
			{
				XmlTextReader xmlTextReader = this.innerReader as XmlTextReader;
				if (xmlTextReader == null)
				{
					IXmlTextParser xmlTextParser = this.innerReader as IXmlTextParser;
					return xmlTextParser != null && xmlTextParser.Normalized;
				}
				return xmlTextReader.Normalization;
			}
			set
			{
				XmlTextReader xmlTextReader = this.innerReader as XmlTextReader;
				if (xmlTextReader == null)
				{
					IXmlTextParser xmlTextParser = this.innerReader as IXmlTextParser;
					if (xmlTextParser != null)
					{
						xmlTextParser.Normalized = value;
						return;
					}
				}
				else
				{
					xmlTextReader.Normalization = value;
				}
			}
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x06001A9A RID: 6810 RVA: 0x00076268 File Offset: 0x00074468
		// (set) Token: 0x06001A9B RID: 6811 RVA: 0x000762A4 File Offset: 0x000744A4
		WhitespaceHandling IXmlTextParser.WhitespaceHandling
		{
			get
			{
				XmlTextReader xmlTextReader = this.innerReader as XmlTextReader;
				if (xmlTextReader != null)
				{
					return xmlTextReader.WhitespaceHandling;
				}
				IXmlTextParser xmlTextParser = this.innerReader as IXmlTextParser;
				if (xmlTextParser != null)
				{
					return xmlTextParser.WhitespaceHandling;
				}
				return WhitespaceHandling.None;
			}
			set
			{
				XmlTextReader xmlTextReader = this.innerReader as XmlTextReader;
				if (xmlTextReader == null)
				{
					IXmlTextParser xmlTextParser = this.innerReader as IXmlTextParser;
					if (xmlTextParser != null)
					{
						xmlTextParser.WhitespaceHandling = value;
						return;
					}
				}
				else
				{
					xmlTextReader.WhitespaceHandling = value;
				}
			}
		}

		// Token: 0x06001A9C RID: 6812 RVA: 0x000762E0 File Offset: 0x000744E0
		bool IXmlLineInfo.HasLineInfo()
		{
			IXmlLineInfo xmlLineInfo = this.innerReader as IXmlLineInfo;
			return xmlLineInfo != null && xmlLineInfo.HasLineInfo();
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x06001A9D RID: 6813 RVA: 0x00076304 File Offset: 0x00074504
		int IXmlLineInfo.LineNumber
		{
			get
			{
				IXmlLineInfo xmlLineInfo = this.innerReader as IXmlLineInfo;
				if (xmlLineInfo != null)
				{
					return xmlLineInfo.LineNumber;
				}
				return 0;
			}
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x06001A9E RID: 6814 RVA: 0x00076328 File Offset: 0x00074528
		int IXmlLineInfo.LinePosition
		{
			get
			{
				IXmlLineInfo xmlLineInfo = this.innerReader as IXmlLineInfo;
				if (xmlLineInfo != null)
				{
					return xmlLineInfo.LinePosition;
				}
				return 0;
			}
		}

		// Token: 0x04000BE6 RID: 3046
		private XmlReader innerReader;

		// Token: 0x04000BE7 RID: 3047
		private int advanceCount;
	}
}
