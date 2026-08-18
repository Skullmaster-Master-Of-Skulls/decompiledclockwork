using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Permissions;
using System.Text;

namespace System.Xml
{
	// Token: 0x02000084 RID: 132
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class XmlTextReader : XmlReader, IXmlLineInfo, IXmlNamespaceResolver
	{
		// Token: 0x06000624 RID: 1572 RVA: 0x0001931D File Offset: 0x0001831D
		protected XmlTextReader()
		{
			this.impl = new XmlTextReaderImpl();
			this.impl.OuterReader = this;
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x0001933C File Offset: 0x0001833C
		protected XmlTextReader(XmlNameTable nt)
		{
			this.impl = new XmlTextReaderImpl(nt);
			this.impl.OuterReader = this;
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x0001935C File Offset: 0x0001835C
		public XmlTextReader(Stream input)
		{
			this.impl = new XmlTextReaderImpl(input);
			this.impl.OuterReader = this;
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x0001937C File Offset: 0x0001837C
		public XmlTextReader(string url, Stream input)
		{
			this.impl = new XmlTextReaderImpl(url, input);
			this.impl.OuterReader = this;
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x0001939D File Offset: 0x0001839D
		public XmlTextReader(Stream input, XmlNameTable nt)
		{
			this.impl = new XmlTextReaderImpl(input, nt);
			this.impl.OuterReader = this;
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x000193BE File Offset: 0x000183BE
		public XmlTextReader(string url, Stream input, XmlNameTable nt)
		{
			this.impl = new XmlTextReaderImpl(url, input, nt);
			this.impl.OuterReader = this;
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x000193E0 File Offset: 0x000183E0
		public XmlTextReader(TextReader input)
		{
			this.impl = new XmlTextReaderImpl(input);
			this.impl.OuterReader = this;
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x00019400 File Offset: 0x00018400
		public XmlTextReader(string url, TextReader input)
		{
			this.impl = new XmlTextReaderImpl(url, input);
			this.impl.OuterReader = this;
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x00019421 File Offset: 0x00018421
		public XmlTextReader(TextReader input, XmlNameTable nt)
		{
			this.impl = new XmlTextReaderImpl(input, nt);
			this.impl.OuterReader = this;
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x00019442 File Offset: 0x00018442
		public XmlTextReader(string url, TextReader input, XmlNameTable nt)
		{
			this.impl = new XmlTextReaderImpl(url, input, nt);
			this.impl.OuterReader = this;
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x00019464 File Offset: 0x00018464
		public XmlTextReader(Stream xmlFragment, XmlNodeType fragType, XmlParserContext context)
		{
			this.impl = new XmlTextReaderImpl(xmlFragment, fragType, context);
			this.impl.OuterReader = this;
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x00019486 File Offset: 0x00018486
		public XmlTextReader(string xmlFragment, XmlNodeType fragType, XmlParserContext context)
		{
			this.impl = new XmlTextReaderImpl(xmlFragment, fragType, context);
			this.impl.OuterReader = this;
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x000194A8 File Offset: 0x000184A8
		public XmlTextReader(string url)
		{
			this.impl = new XmlTextReaderImpl(url, new NameTable());
			this.impl.OuterReader = this;
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x000194CD File Offset: 0x000184CD
		public XmlTextReader(string url, XmlNameTable nt)
		{
			this.impl = new XmlTextReaderImpl(url, nt);
			this.impl.OuterReader = this;
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000632 RID: 1586 RVA: 0x000194EE File Offset: 0x000184EE
		public override XmlReaderSettings Settings
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000633 RID: 1587 RVA: 0x000194F1 File Offset: 0x000184F1
		public override XmlNodeType NodeType
		{
			get
			{
				return this.impl.NodeType;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000634 RID: 1588 RVA: 0x000194FE File Offset: 0x000184FE
		public override string Name
		{
			get
			{
				return this.impl.Name;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000635 RID: 1589 RVA: 0x0001950B File Offset: 0x0001850B
		public override string LocalName
		{
			get
			{
				return this.impl.LocalName;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000636 RID: 1590 RVA: 0x00019518 File Offset: 0x00018518
		public override string NamespaceURI
		{
			get
			{
				return this.impl.NamespaceURI;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000637 RID: 1591 RVA: 0x00019525 File Offset: 0x00018525
		public override string Prefix
		{
			get
			{
				return this.impl.Prefix;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000638 RID: 1592 RVA: 0x00019532 File Offset: 0x00018532
		public override bool HasValue
		{
			get
			{
				return this.impl.HasValue;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000639 RID: 1593 RVA: 0x0001953F File Offset: 0x0001853F
		public override string Value
		{
			get
			{
				return this.impl.Value;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600063A RID: 1594 RVA: 0x0001954C File Offset: 0x0001854C
		public override int Depth
		{
			get
			{
				return this.impl.Depth;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600063B RID: 1595 RVA: 0x00019559 File Offset: 0x00018559
		public override string BaseURI
		{
			get
			{
				return this.impl.BaseURI;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600063C RID: 1596 RVA: 0x00019566 File Offset: 0x00018566
		public override bool IsEmptyElement
		{
			get
			{
				return this.impl.IsEmptyElement;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600063D RID: 1597 RVA: 0x00019573 File Offset: 0x00018573
		public override bool IsDefault
		{
			get
			{
				return this.impl.IsDefault;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x0600063E RID: 1598 RVA: 0x00019580 File Offset: 0x00018580
		public override char QuoteChar
		{
			get
			{
				return this.impl.QuoteChar;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x0600063F RID: 1599 RVA: 0x0001958D File Offset: 0x0001858D
		public override XmlSpace XmlSpace
		{
			get
			{
				return this.impl.XmlSpace;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000640 RID: 1600 RVA: 0x0001959A File Offset: 0x0001859A
		public override string XmlLang
		{
			get
			{
				return this.impl.XmlLang;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000641 RID: 1601 RVA: 0x000195A7 File Offset: 0x000185A7
		public override int AttributeCount
		{
			get
			{
				return this.impl.AttributeCount;
			}
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x000195B4 File Offset: 0x000185B4
		public override string GetAttribute(string name)
		{
			return this.impl.GetAttribute(name);
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x000195C2 File Offset: 0x000185C2
		public override string GetAttribute(string localName, string namespaceURI)
		{
			return this.impl.GetAttribute(localName, namespaceURI);
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x000195D1 File Offset: 0x000185D1
		public override string GetAttribute(int i)
		{
			return this.impl.GetAttribute(i);
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x000195DF File Offset: 0x000185DF
		public override bool MoveToAttribute(string name)
		{
			return this.impl.MoveToAttribute(name);
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x000195ED File Offset: 0x000185ED
		public override bool MoveToAttribute(string localName, string namespaceURI)
		{
			return this.impl.MoveToAttribute(localName, namespaceURI);
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x000195FC File Offset: 0x000185FC
		public override void MoveToAttribute(int i)
		{
			this.impl.MoveToAttribute(i);
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x0001960A File Offset: 0x0001860A
		public override bool MoveToFirstAttribute()
		{
			return this.impl.MoveToFirstAttribute();
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x00019617 File Offset: 0x00018617
		public override bool MoveToNextAttribute()
		{
			return this.impl.MoveToNextAttribute();
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x00019624 File Offset: 0x00018624
		public override bool MoveToElement()
		{
			return this.impl.MoveToElement();
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x00019631 File Offset: 0x00018631
		public override bool ReadAttributeValue()
		{
			return this.impl.ReadAttributeValue();
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x0001963E File Offset: 0x0001863E
		public override bool Read()
		{
			return this.impl.Read();
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600064D RID: 1613 RVA: 0x0001964B File Offset: 0x0001864B
		public override bool EOF
		{
			get
			{
				return this.impl.EOF;
			}
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x00019658 File Offset: 0x00018658
		public override void Close()
		{
			this.impl.Close();
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600064F RID: 1615 RVA: 0x00019665 File Offset: 0x00018665
		public override ReadState ReadState
		{
			get
			{
				return this.impl.ReadState;
			}
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x00019672 File Offset: 0x00018672
		public override void Skip()
		{
			this.impl.Skip();
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000651 RID: 1617 RVA: 0x0001967F File Offset: 0x0001867F
		public override XmlNameTable NameTable
		{
			get
			{
				return this.impl.NameTable;
			}
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x0001968C File Offset: 0x0001868C
		public override string LookupNamespace(string prefix)
		{
			string text = this.impl.LookupNamespace(prefix);
			if (text != null && text.Length == 0)
			{
				text = null;
			}
			return text;
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000653 RID: 1619 RVA: 0x000196B4 File Offset: 0x000186B4
		public override bool CanResolveEntity
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x000196B7 File Offset: 0x000186B7
		public override void ResolveEntity()
		{
			this.impl.ResolveEntity();
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000655 RID: 1621 RVA: 0x000196C4 File Offset: 0x000186C4
		public override bool CanReadBinaryContent
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x000196C7 File Offset: 0x000186C7
		public override int ReadContentAsBase64(byte[] buffer, int index, int count)
		{
			return this.impl.ReadContentAsBase64(buffer, index, count);
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x000196D7 File Offset: 0x000186D7
		public override int ReadElementContentAsBase64(byte[] buffer, int index, int count)
		{
			return this.impl.ReadElementContentAsBase64(buffer, index, count);
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x000196E7 File Offset: 0x000186E7
		public override int ReadContentAsBinHex(byte[] buffer, int index, int count)
		{
			return this.impl.ReadContentAsBinHex(buffer, index, count);
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x000196F7 File Offset: 0x000186F7
		public override int ReadElementContentAsBinHex(byte[] buffer, int index, int count)
		{
			return this.impl.ReadElementContentAsBinHex(buffer, index, count);
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600065A RID: 1626 RVA: 0x00019707 File Offset: 0x00018707
		public override bool CanReadValueChunk
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x0001970A File Offset: 0x0001870A
		public override string ReadString()
		{
			this.impl.MoveOffEntityReference();
			return base.ReadString();
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x0001971D File Offset: 0x0001871D
		public bool HasLineInfo()
		{
			return true;
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600065D RID: 1629 RVA: 0x00019720 File Offset: 0x00018720
		public int LineNumber
		{
			get
			{
				return this.impl.LineNumber;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600065E RID: 1630 RVA: 0x0001972D File Offset: 0x0001872D
		public int LinePosition
		{
			get
			{
				return this.impl.LinePosition;
			}
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x0001973A File Offset: 0x0001873A
		IDictionary<string, string> IXmlNamespaceResolver.GetNamespacesInScope(XmlNamespaceScope scope)
		{
			return this.impl.GetNamespacesInScope(scope);
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x00019748 File Offset: 0x00018748
		string IXmlNamespaceResolver.LookupNamespace(string prefix)
		{
			return this.impl.LookupNamespace(prefix);
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x00019756 File Offset: 0x00018756
		string IXmlNamespaceResolver.LookupPrefix(string namespaceName)
		{
			return this.impl.LookupPrefix(namespaceName);
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x00019764 File Offset: 0x00018764
		public IDictionary<string, string> GetNamespacesInScope(XmlNamespaceScope scope)
		{
			return this.impl.GetNamespacesInScope(scope);
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000663 RID: 1635 RVA: 0x00019772 File Offset: 0x00018772
		// (set) Token: 0x06000664 RID: 1636 RVA: 0x0001977F File Offset: 0x0001877F
		public bool Namespaces
		{
			get
			{
				return this.impl.Namespaces;
			}
			set
			{
				this.impl.Namespaces = value;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000665 RID: 1637 RVA: 0x0001978D File Offset: 0x0001878D
		// (set) Token: 0x06000666 RID: 1638 RVA: 0x0001979A File Offset: 0x0001879A
		public bool Normalization
		{
			get
			{
				return this.impl.Normalization;
			}
			set
			{
				this.impl.Normalization = value;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000667 RID: 1639 RVA: 0x000197A8 File Offset: 0x000187A8
		public Encoding Encoding
		{
			get
			{
				return this.impl.Encoding;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000668 RID: 1640 RVA: 0x000197B5 File Offset: 0x000187B5
		// (set) Token: 0x06000669 RID: 1641 RVA: 0x000197C2 File Offset: 0x000187C2
		public WhitespaceHandling WhitespaceHandling
		{
			get
			{
				return this.impl.WhitespaceHandling;
			}
			set
			{
				this.impl.WhitespaceHandling = value;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600066A RID: 1642 RVA: 0x000197D0 File Offset: 0x000187D0
		// (set) Token: 0x0600066B RID: 1643 RVA: 0x000197DD File Offset: 0x000187DD
		public bool ProhibitDtd
		{
			get
			{
				return this.impl.ProhibitDtd;
			}
			set
			{
				this.impl.ProhibitDtd = value;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600066C RID: 1644 RVA: 0x000197EB File Offset: 0x000187EB
		// (set) Token: 0x0600066D RID: 1645 RVA: 0x000197F8 File Offset: 0x000187F8
		public EntityHandling EntityHandling
		{
			get
			{
				return this.impl.EntityHandling;
			}
			set
			{
				this.impl.EntityHandling = value;
			}
		}

		// Token: 0x17000116 RID: 278
		// (set) Token: 0x0600066E RID: 1646 RVA: 0x00019806 File Offset: 0x00018806
		public XmlResolver XmlResolver
		{
			set
			{
				this.impl.XmlResolver = value;
			}
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x00019814 File Offset: 0x00018814
		public void ResetState()
		{
			this.impl.ResetState();
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x00019821 File Offset: 0x00018821
		public TextReader GetRemainder()
		{
			return this.impl.GetRemainder();
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x0001982E File Offset: 0x0001882E
		public int ReadChars(char[] buffer, int index, int count)
		{
			return this.impl.ReadChars(buffer, index, count);
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x0001983E File Offset: 0x0001883E
		public int ReadBase64(byte[] array, int offset, int len)
		{
			return this.impl.ReadBase64(array, offset, len);
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x0001984E File Offset: 0x0001884E
		public int ReadBinHex(byte[] array, int offset, int len)
		{
			return this.impl.ReadBinHex(array, offset, len);
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000674 RID: 1652 RVA: 0x0001985E File Offset: 0x0001885E
		internal XmlTextReaderImpl Impl
		{
			get
			{
				return this.impl;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000675 RID: 1653 RVA: 0x00019866 File Offset: 0x00018866
		internal override XmlNamespaceManager NamespaceManager
		{
			get
			{
				return this.impl.NamespaceManager;
			}
		}

		// Token: 0x17000119 RID: 281
		// (set) Token: 0x06000676 RID: 1654 RVA: 0x00019873 File Offset: 0x00018873
		internal bool XmlValidatingReaderCompatibilityMode
		{
			set
			{
				this.impl.XmlValidatingReaderCompatibilityMode = value;
			}
		}

		// Token: 0x04000680 RID: 1664
		private XmlTextReaderImpl impl;
	}
}
