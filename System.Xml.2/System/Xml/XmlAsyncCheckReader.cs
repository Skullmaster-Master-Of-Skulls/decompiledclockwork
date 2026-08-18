using System;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace System.Xml
{
	// Token: 0x020000C4 RID: 196
	internal class XmlAsyncCheckReader : XmlReader
	{
		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060006D3 RID: 1747 RVA: 0x00017CD3 File Offset: 0x00015ED3
		internal XmlReader CoreReader
		{
			get
			{
				return this.coreReader;
			}
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x00017CDC File Offset: 0x00015EDC
		public static XmlAsyncCheckReader CreateAsyncCheckWrapper(XmlReader reader)
		{
			if (reader is IXmlLineInfo)
			{
				if (!(reader is IXmlNamespaceResolver))
				{
					return new XmlAsyncCheckReaderWithLineInfo(reader);
				}
				if (reader is IXmlSchemaInfo)
				{
					return new XmlAsyncCheckReaderWithLineInfoNSSchema(reader);
				}
				return new XmlAsyncCheckReaderWithLineInfoNS(reader);
			}
			else
			{
				if (reader is IXmlNamespaceResolver)
				{
					return new XmlAsyncCheckReaderWithNS(reader);
				}
				return new XmlAsyncCheckReader(reader);
			}
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x00017D2B File Offset: 0x00015F2B
		public XmlAsyncCheckReader(XmlReader reader)
		{
			this.coreReader = reader;
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x00017D45 File Offset: 0x00015F45
		private void CheckAsync()
		{
			if (!this.lastTask.IsCompleted)
			{
				throw new InvalidOperationException(Res.GetString("Xml_AsyncIsRunningException"));
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060006D7 RID: 1751 RVA: 0x00017D64 File Offset: 0x00015F64
		public override XmlReaderSettings Settings
		{
			get
			{
				XmlReaderSettings xmlReaderSettings = this.coreReader.Settings;
				if (xmlReaderSettings != null)
				{
					xmlReaderSettings = xmlReaderSettings.Clone();
				}
				else
				{
					xmlReaderSettings = new XmlReaderSettings();
				}
				xmlReaderSettings.Async = true;
				xmlReaderSettings.ReadOnly = true;
				return xmlReaderSettings;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060006D8 RID: 1752 RVA: 0x00017D9E File Offset: 0x00015F9E
		public override XmlNodeType NodeType
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.NodeType;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060006D9 RID: 1753 RVA: 0x00017DB1 File Offset: 0x00015FB1
		public override string Name
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.Name;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060006DA RID: 1754 RVA: 0x00017DC4 File Offset: 0x00015FC4
		public override string LocalName
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.LocalName;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060006DB RID: 1755 RVA: 0x00017DD7 File Offset: 0x00015FD7
		public override string NamespaceURI
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.NamespaceURI;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060006DC RID: 1756 RVA: 0x00017DEA File Offset: 0x00015FEA
		public override string Prefix
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.Prefix;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060006DD RID: 1757 RVA: 0x00017DFD File Offset: 0x00015FFD
		public override bool HasValue
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.HasValue;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060006DE RID: 1758 RVA: 0x00017E10 File Offset: 0x00016010
		public override string Value
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.Value;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060006DF RID: 1759 RVA: 0x00017E23 File Offset: 0x00016023
		public override int Depth
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.Depth;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060006E0 RID: 1760 RVA: 0x00017E36 File Offset: 0x00016036
		public override string BaseURI
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.BaseURI;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060006E1 RID: 1761 RVA: 0x00017E49 File Offset: 0x00016049
		public override bool IsEmptyElement
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.IsEmptyElement;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060006E2 RID: 1762 RVA: 0x00017E5C File Offset: 0x0001605C
		public override bool IsDefault
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.IsDefault;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060006E3 RID: 1763 RVA: 0x00017E6F File Offset: 0x0001606F
		public override char QuoteChar
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.QuoteChar;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060006E4 RID: 1764 RVA: 0x00017E82 File Offset: 0x00016082
		public override XmlSpace XmlSpace
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.XmlSpace;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060006E5 RID: 1765 RVA: 0x00017E95 File Offset: 0x00016095
		public override string XmlLang
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.XmlLang;
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060006E6 RID: 1766 RVA: 0x00017EA8 File Offset: 0x000160A8
		public override IXmlSchemaInfo SchemaInfo
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.SchemaInfo;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060006E7 RID: 1767 RVA: 0x00017EBB File Offset: 0x000160BB
		public override Type ValueType
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.ValueType;
			}
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x00017ECE File Offset: 0x000160CE
		public override object ReadContentAsObject()
		{
			this.CheckAsync();
			return this.coreReader.ReadContentAsObject();
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x00017EE1 File Offset: 0x000160E1
		public override bool ReadContentAsBoolean()
		{
			this.CheckAsync();
			return this.coreReader.ReadContentAsBoolean();
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x00017EF4 File Offset: 0x000160F4
		public override DateTime ReadContentAsDateTime()
		{
			this.CheckAsync();
			return this.coreReader.ReadContentAsDateTime();
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x00017F07 File Offset: 0x00016107
		public override double ReadContentAsDouble()
		{
			this.CheckAsync();
			return this.coreReader.ReadContentAsDouble();
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x00017F1A File Offset: 0x0001611A
		public override float ReadContentAsFloat()
		{
			this.CheckAsync();
			return this.coreReader.ReadContentAsFloat();
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x00017F2D File Offset: 0x0001612D
		public override decimal ReadContentAsDecimal()
		{
			this.CheckAsync();
			return this.coreReader.ReadContentAsDecimal();
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x00017F40 File Offset: 0x00016140
		public override int ReadContentAsInt()
		{
			this.CheckAsync();
			return this.coreReader.ReadContentAsInt();
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x00017F53 File Offset: 0x00016153
		public override long ReadContentAsLong()
		{
			this.CheckAsync();
			return this.coreReader.ReadContentAsLong();
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x00017F66 File Offset: 0x00016166
		public override string ReadContentAsString()
		{
			this.CheckAsync();
			return this.coreReader.ReadContentAsString();
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x00017F79 File Offset: 0x00016179
		public override object ReadContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver)
		{
			this.CheckAsync();
			return this.coreReader.ReadContentAs(returnType, namespaceResolver);
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x00017F8E File Offset: 0x0001618E
		public override object ReadElementContentAsObject()
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsObject();
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x00017FA1 File Offset: 0x000161A1
		public override object ReadElementContentAsObject(string localName, string namespaceURI)
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsObject(localName, namespaceURI);
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x00017FB6 File Offset: 0x000161B6
		public override bool ReadElementContentAsBoolean()
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsBoolean();
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x00017FC9 File Offset: 0x000161C9
		public override bool ReadElementContentAsBoolean(string localName, string namespaceURI)
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsBoolean(localName, namespaceURI);
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x00017FDE File Offset: 0x000161DE
		public override DateTime ReadElementContentAsDateTime()
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsDateTime();
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x00017FF1 File Offset: 0x000161F1
		public override DateTime ReadElementContentAsDateTime(string localName, string namespaceURI)
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsDateTime(localName, namespaceURI);
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x00018006 File Offset: 0x00016206
		public override DateTimeOffset ReadContentAsDateTimeOffset()
		{
			this.CheckAsync();
			return this.coreReader.ReadContentAsDateTimeOffset();
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x00018019 File Offset: 0x00016219
		public override double ReadElementContentAsDouble()
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsDouble();
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x0001802C File Offset: 0x0001622C
		public override double ReadElementContentAsDouble(string localName, string namespaceURI)
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsDouble(localName, namespaceURI);
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x00018041 File Offset: 0x00016241
		public override float ReadElementContentAsFloat()
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsFloat();
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x00018054 File Offset: 0x00016254
		public override float ReadElementContentAsFloat(string localName, string namespaceURI)
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsFloat(localName, namespaceURI);
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x00018069 File Offset: 0x00016269
		public override decimal ReadElementContentAsDecimal()
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsDecimal();
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x0001807C File Offset: 0x0001627C
		public override decimal ReadElementContentAsDecimal(string localName, string namespaceURI)
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsDecimal(localName, namespaceURI);
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x00018091 File Offset: 0x00016291
		public override int ReadElementContentAsInt()
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsInt();
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x000180A4 File Offset: 0x000162A4
		public override int ReadElementContentAsInt(string localName, string namespaceURI)
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsInt(localName, namespaceURI);
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x000180B9 File Offset: 0x000162B9
		public override long ReadElementContentAsLong()
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsLong();
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x000180CC File Offset: 0x000162CC
		public override long ReadElementContentAsLong(string localName, string namespaceURI)
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsLong(localName, namespaceURI);
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x000180E1 File Offset: 0x000162E1
		public override string ReadElementContentAsString()
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsString();
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x000180F4 File Offset: 0x000162F4
		public override string ReadElementContentAsString(string localName, string namespaceURI)
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsString(localName, namespaceURI);
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x00018109 File Offset: 0x00016309
		public override object ReadElementContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver)
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAs(returnType, namespaceResolver);
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x0001811E File Offset: 0x0001631E
		public override object ReadElementContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver, string localName, string namespaceURI)
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAs(returnType, namespaceResolver, localName, namespaceURI);
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000707 RID: 1799 RVA: 0x00018136 File Offset: 0x00016336
		public override int AttributeCount
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.AttributeCount;
			}
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x00018149 File Offset: 0x00016349
		public override string GetAttribute(string name)
		{
			this.CheckAsync();
			return this.coreReader.GetAttribute(name);
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x0001815D File Offset: 0x0001635D
		public override string GetAttribute(string name, string namespaceURI)
		{
			this.CheckAsync();
			return this.coreReader.GetAttribute(name, namespaceURI);
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x00018172 File Offset: 0x00016372
		public override string GetAttribute(int i)
		{
			this.CheckAsync();
			return this.coreReader.GetAttribute(i);
		}

		// Token: 0x1700016A RID: 362
		public override string this[int i]
		{
			get
			{
				this.CheckAsync();
				return this.coreReader[i];
			}
		}

		// Token: 0x1700016B RID: 363
		public override string this[string name]
		{
			get
			{
				this.CheckAsync();
				return this.coreReader[name];
			}
		}

		// Token: 0x1700016C RID: 364
		public override string this[string name, string namespaceURI]
		{
			get
			{
				this.CheckAsync();
				return this.coreReader[name, namespaceURI];
			}
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x000181C3 File Offset: 0x000163C3
		public override bool MoveToAttribute(string name)
		{
			this.CheckAsync();
			return this.coreReader.MoveToAttribute(name);
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x000181D7 File Offset: 0x000163D7
		public override bool MoveToAttribute(string name, string ns)
		{
			this.CheckAsync();
			return this.coreReader.MoveToAttribute(name, ns);
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x000181EC File Offset: 0x000163EC
		public override void MoveToAttribute(int i)
		{
			this.CheckAsync();
			this.coreReader.MoveToAttribute(i);
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x00018200 File Offset: 0x00016400
		public override bool MoveToFirstAttribute()
		{
			this.CheckAsync();
			return this.coreReader.MoveToFirstAttribute();
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x00018213 File Offset: 0x00016413
		public override bool MoveToNextAttribute()
		{
			this.CheckAsync();
			return this.coreReader.MoveToNextAttribute();
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x00018226 File Offset: 0x00016426
		public override bool MoveToElement()
		{
			this.CheckAsync();
			return this.coreReader.MoveToElement();
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x00018239 File Offset: 0x00016439
		public override bool ReadAttributeValue()
		{
			this.CheckAsync();
			return this.coreReader.ReadAttributeValue();
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x0001824C File Offset: 0x0001644C
		public override bool Read()
		{
			this.CheckAsync();
			return this.coreReader.Read();
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000716 RID: 1814 RVA: 0x0001825F File Offset: 0x0001645F
		public override bool EOF
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.EOF;
			}
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x00018272 File Offset: 0x00016472
		public override void Close()
		{
			this.CheckAsync();
			this.coreReader.Close();
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000718 RID: 1816 RVA: 0x00018285 File Offset: 0x00016485
		public override ReadState ReadState
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.ReadState;
			}
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x00018298 File Offset: 0x00016498
		public override void Skip()
		{
			this.CheckAsync();
			this.coreReader.Skip();
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x0600071A RID: 1818 RVA: 0x000182AB File Offset: 0x000164AB
		public override XmlNameTable NameTable
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.NameTable;
			}
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x000182BE File Offset: 0x000164BE
		public override string LookupNamespace(string prefix)
		{
			this.CheckAsync();
			return this.coreReader.LookupNamespace(prefix);
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x0600071C RID: 1820 RVA: 0x000182D2 File Offset: 0x000164D2
		public override bool CanResolveEntity
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.CanResolveEntity;
			}
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x000182E5 File Offset: 0x000164E5
		public override void ResolveEntity()
		{
			this.CheckAsync();
			this.coreReader.ResolveEntity();
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x0600071E RID: 1822 RVA: 0x000182F8 File Offset: 0x000164F8
		public override bool CanReadBinaryContent
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.CanReadBinaryContent;
			}
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x0001830B File Offset: 0x0001650B
		public override int ReadContentAsBase64(byte[] buffer, int index, int count)
		{
			this.CheckAsync();
			return this.coreReader.ReadContentAsBase64(buffer, index, count);
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x00018321 File Offset: 0x00016521
		public override int ReadElementContentAsBase64(byte[] buffer, int index, int count)
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsBase64(buffer, index, count);
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x00018337 File Offset: 0x00016537
		public override int ReadContentAsBinHex(byte[] buffer, int index, int count)
		{
			this.CheckAsync();
			return this.coreReader.ReadContentAsBinHex(buffer, index, count);
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x0001834D File Offset: 0x0001654D
		public override int ReadElementContentAsBinHex(byte[] buffer, int index, int count)
		{
			this.CheckAsync();
			return this.coreReader.ReadElementContentAsBinHex(buffer, index, count);
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000723 RID: 1827 RVA: 0x00018363 File Offset: 0x00016563
		public override bool CanReadValueChunk
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.CanReadValueChunk;
			}
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x00018376 File Offset: 0x00016576
		public override int ReadValueChunk(char[] buffer, int index, int count)
		{
			this.CheckAsync();
			return this.coreReader.ReadValueChunk(buffer, index, count);
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x0001838C File Offset: 0x0001658C
		public override string ReadString()
		{
			this.CheckAsync();
			return this.coreReader.ReadString();
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x0001839F File Offset: 0x0001659F
		public override XmlNodeType MoveToContent()
		{
			this.CheckAsync();
			return this.coreReader.MoveToContent();
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x000183B2 File Offset: 0x000165B2
		public override void ReadStartElement()
		{
			this.CheckAsync();
			this.coreReader.ReadStartElement();
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x000183C5 File Offset: 0x000165C5
		public override void ReadStartElement(string name)
		{
			this.CheckAsync();
			this.coreReader.ReadStartElement(name);
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x000183D9 File Offset: 0x000165D9
		public override void ReadStartElement(string localname, string ns)
		{
			this.CheckAsync();
			this.coreReader.ReadStartElement(localname, ns);
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x000183EE File Offset: 0x000165EE
		public override string ReadElementString()
		{
			this.CheckAsync();
			return this.coreReader.ReadElementString();
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x00018401 File Offset: 0x00016601
		public override string ReadElementString(string name)
		{
			this.CheckAsync();
			return this.coreReader.ReadElementString(name);
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x00018415 File Offset: 0x00016615
		public override string ReadElementString(string localname, string ns)
		{
			this.CheckAsync();
			return this.coreReader.ReadElementString(localname, ns);
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x0001842A File Offset: 0x0001662A
		public override void ReadEndElement()
		{
			this.CheckAsync();
			this.coreReader.ReadEndElement();
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x0001843D File Offset: 0x0001663D
		public override bool IsStartElement()
		{
			this.CheckAsync();
			return this.coreReader.IsStartElement();
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x00018450 File Offset: 0x00016650
		public override bool IsStartElement(string name)
		{
			this.CheckAsync();
			return this.coreReader.IsStartElement(name);
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x00018464 File Offset: 0x00016664
		public override bool IsStartElement(string localname, string ns)
		{
			this.CheckAsync();
			return this.coreReader.IsStartElement(localname, ns);
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x00018479 File Offset: 0x00016679
		public override bool ReadToFollowing(string name)
		{
			this.CheckAsync();
			return this.coreReader.ReadToFollowing(name);
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x0001848D File Offset: 0x0001668D
		public override bool ReadToFollowing(string localName, string namespaceURI)
		{
			this.CheckAsync();
			return this.coreReader.ReadToFollowing(localName, namespaceURI);
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x000184A2 File Offset: 0x000166A2
		public override bool ReadToDescendant(string name)
		{
			this.CheckAsync();
			return this.coreReader.ReadToDescendant(name);
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x000184B6 File Offset: 0x000166B6
		public override bool ReadToDescendant(string localName, string namespaceURI)
		{
			this.CheckAsync();
			return this.coreReader.ReadToDescendant(localName, namespaceURI);
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x000184CB File Offset: 0x000166CB
		public override bool ReadToNextSibling(string name)
		{
			this.CheckAsync();
			return this.coreReader.ReadToNextSibling(name);
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x000184DF File Offset: 0x000166DF
		public override bool ReadToNextSibling(string localName, string namespaceURI)
		{
			this.CheckAsync();
			return this.coreReader.ReadToNextSibling(localName, namespaceURI);
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x000184F4 File Offset: 0x000166F4
		public override string ReadInnerXml()
		{
			this.CheckAsync();
			return this.coreReader.ReadInnerXml();
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x00018507 File Offset: 0x00016707
		public override string ReadOuterXml()
		{
			this.CheckAsync();
			return this.coreReader.ReadOuterXml();
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x0001851C File Offset: 0x0001671C
		public override XmlReader ReadSubtree()
		{
			this.CheckAsync();
			XmlReader reader = this.coreReader.ReadSubtree();
			return XmlAsyncCheckReader.CreateAsyncCheckWrapper(reader);
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x0600073A RID: 1850 RVA: 0x00018541 File Offset: 0x00016741
		public override bool HasAttributes
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.HasAttributes;
			}
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x00018554 File Offset: 0x00016754
		protected override void Dispose(bool disposing)
		{
			this.CheckAsync();
			this.coreReader.Dispose();
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x0600073C RID: 1852 RVA: 0x00018567 File Offset: 0x00016767
		internal override XmlNamespaceManager NamespaceManager
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.NamespaceManager;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x0600073D RID: 1853 RVA: 0x0001857A File Offset: 0x0001677A
		internal override IDtdInfo DtdInfo
		{
			get
			{
				this.CheckAsync();
				return this.coreReader.DtdInfo;
			}
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x00018590 File Offset: 0x00016790
		public override Task<string> GetValueAsync()
		{
			this.CheckAsync();
			Task<string> valueAsync = this.coreReader.GetValueAsync();
			this.lastTask = valueAsync;
			return valueAsync;
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x000185B8 File Offset: 0x000167B8
		public override Task<object> ReadContentAsObjectAsync()
		{
			this.CheckAsync();
			Task<object> result = this.coreReader.ReadContentAsObjectAsync();
			this.lastTask = result;
			return result;
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x000185E0 File Offset: 0x000167E0
		public override Task<string> ReadContentAsStringAsync()
		{
			this.CheckAsync();
			Task<string> result = this.coreReader.ReadContentAsStringAsync();
			this.lastTask = result;
			return result;
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x00018608 File Offset: 0x00016808
		public override Task<object> ReadContentAsAsync(Type returnType, IXmlNamespaceResolver namespaceResolver)
		{
			this.CheckAsync();
			Task<object> result = this.coreReader.ReadContentAsAsync(returnType, namespaceResolver);
			this.lastTask = result;
			return result;
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x00018634 File Offset: 0x00016834
		public override Task<object> ReadElementContentAsObjectAsync()
		{
			this.CheckAsync();
			Task<object> result = this.coreReader.ReadElementContentAsObjectAsync();
			this.lastTask = result;
			return result;
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x0001865C File Offset: 0x0001685C
		public override Task<string> ReadElementContentAsStringAsync()
		{
			this.CheckAsync();
			Task<string> result = this.coreReader.ReadElementContentAsStringAsync();
			this.lastTask = result;
			return result;
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x00018684 File Offset: 0x00016884
		public override Task<object> ReadElementContentAsAsync(Type returnType, IXmlNamespaceResolver namespaceResolver)
		{
			this.CheckAsync();
			Task<object> result = this.coreReader.ReadElementContentAsAsync(returnType, namespaceResolver);
			this.lastTask = result;
			return result;
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x000186B0 File Offset: 0x000168B0
		public override Task<bool> ReadAsync()
		{
			this.CheckAsync();
			Task<bool> result = this.coreReader.ReadAsync();
			this.lastTask = result;
			return result;
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x000186D8 File Offset: 0x000168D8
		public override Task SkipAsync()
		{
			this.CheckAsync();
			Task result = this.coreReader.SkipAsync();
			this.lastTask = result;
			return result;
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x00018700 File Offset: 0x00016900
		public override Task<int> ReadContentAsBase64Async(byte[] buffer, int index, int count)
		{
			this.CheckAsync();
			Task<int> result = this.coreReader.ReadContentAsBase64Async(buffer, index, count);
			this.lastTask = result;
			return result;
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x0001872C File Offset: 0x0001692C
		public override Task<int> ReadElementContentAsBase64Async(byte[] buffer, int index, int count)
		{
			this.CheckAsync();
			Task<int> result = this.coreReader.ReadElementContentAsBase64Async(buffer, index, count);
			this.lastTask = result;
			return result;
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x00018758 File Offset: 0x00016958
		public override Task<int> ReadContentAsBinHexAsync(byte[] buffer, int index, int count)
		{
			this.CheckAsync();
			Task<int> result = this.coreReader.ReadContentAsBinHexAsync(buffer, index, count);
			this.lastTask = result;
			return result;
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x00018784 File Offset: 0x00016984
		public override Task<int> ReadElementContentAsBinHexAsync(byte[] buffer, int index, int count)
		{
			this.CheckAsync();
			Task<int> result = this.coreReader.ReadElementContentAsBinHexAsync(buffer, index, count);
			this.lastTask = result;
			return result;
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x000187B0 File Offset: 0x000169B0
		public override Task<int> ReadValueChunkAsync(char[] buffer, int index, int count)
		{
			this.CheckAsync();
			Task<int> result = this.coreReader.ReadValueChunkAsync(buffer, index, count);
			this.lastTask = result;
			return result;
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x000187DC File Offset: 0x000169DC
		public override Task<XmlNodeType> MoveToContentAsync()
		{
			this.CheckAsync();
			Task<XmlNodeType> result = this.coreReader.MoveToContentAsync();
			this.lastTask = result;
			return result;
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x00018804 File Offset: 0x00016A04
		public override Task<string> ReadInnerXmlAsync()
		{
			this.CheckAsync();
			Task<string> result = this.coreReader.ReadInnerXmlAsync();
			this.lastTask = result;
			return result;
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x0001882C File Offset: 0x00016A2C
		public override Task<string> ReadOuterXmlAsync()
		{
			this.CheckAsync();
			Task<string> result = this.coreReader.ReadOuterXmlAsync();
			this.lastTask = result;
			return result;
		}

		// Token: 0x040002DA RID: 730
		private readonly XmlReader coreReader;

		// Token: 0x040002DB RID: 731
		private Task lastTask = AsyncHelper.DoneTask;
	}
}
