using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Permissions;
using System.Text;
using System.Xml.Schema;

namespace System.Xml
{
	// Token: 0x020000DF RID: 223
	[Obsolete("Use XmlReader created by XmlReader.Create() method using appropriate XmlReaderSettings instead. http://go.microsoft.com/fwlink/?linkid=14202")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class XmlValidatingReader : XmlReader, IXmlLineInfo, IXmlNamespaceResolver
	{
		// Token: 0x06000D84 RID: 3460 RVA: 0x0003A9E7 File Offset: 0x00038BE7
		public XmlValidatingReader(XmlReader reader)
		{
			this.impl = new XmlValidatingReaderImpl(reader);
			this.impl.OuterReader = this;
		}

		// Token: 0x06000D85 RID: 3461 RVA: 0x0003AA07 File Offset: 0x00038C07
		public XmlValidatingReader(string xmlFragment, XmlNodeType fragType, XmlParserContext context)
		{
			if (xmlFragment == null)
			{
				throw new ArgumentNullException("xmlFragment");
			}
			this.impl = new XmlValidatingReaderImpl(xmlFragment, fragType, context);
			this.impl.OuterReader = this;
		}

		// Token: 0x06000D86 RID: 3462 RVA: 0x0003AA37 File Offset: 0x00038C37
		public XmlValidatingReader(Stream xmlFragment, XmlNodeType fragType, XmlParserContext context)
		{
			if (xmlFragment == null)
			{
				throw new ArgumentNullException("xmlFragment");
			}
			this.impl = new XmlValidatingReaderImpl(xmlFragment, fragType, context);
			this.impl.OuterReader = this;
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000D87 RID: 3463 RVA: 0x0003AA67 File Offset: 0x00038C67
		public override XmlNodeType NodeType
		{
			get
			{
				return this.impl.NodeType;
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000D88 RID: 3464 RVA: 0x0003AA74 File Offset: 0x00038C74
		public override string Name
		{
			get
			{
				return this.impl.Name;
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000D89 RID: 3465 RVA: 0x0003AA81 File Offset: 0x00038C81
		public override string LocalName
		{
			get
			{
				return this.impl.LocalName;
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000D8A RID: 3466 RVA: 0x0003AA8E File Offset: 0x00038C8E
		public override string NamespaceURI
		{
			get
			{
				return this.impl.NamespaceURI;
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000D8B RID: 3467 RVA: 0x0003AA9B File Offset: 0x00038C9B
		public override string Prefix
		{
			get
			{
				return this.impl.Prefix;
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000D8C RID: 3468 RVA: 0x0003AAA8 File Offset: 0x00038CA8
		public override bool HasValue
		{
			get
			{
				return this.impl.HasValue;
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000D8D RID: 3469 RVA: 0x0003AAB5 File Offset: 0x00038CB5
		public override string Value
		{
			get
			{
				return this.impl.Value;
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000D8E RID: 3470 RVA: 0x0003AAC2 File Offset: 0x00038CC2
		public override int Depth
		{
			get
			{
				return this.impl.Depth;
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000D8F RID: 3471 RVA: 0x0003AACF File Offset: 0x00038CCF
		public override string BaseURI
		{
			get
			{
				return this.impl.BaseURI;
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000D90 RID: 3472 RVA: 0x0003AADC File Offset: 0x00038CDC
		public override bool IsEmptyElement
		{
			get
			{
				return this.impl.IsEmptyElement;
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000D91 RID: 3473 RVA: 0x0003AAE9 File Offset: 0x00038CE9
		public override bool IsDefault
		{
			get
			{
				return this.impl.IsDefault;
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000D92 RID: 3474 RVA: 0x0003AAF6 File Offset: 0x00038CF6
		public override char QuoteChar
		{
			get
			{
				return this.impl.QuoteChar;
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000D93 RID: 3475 RVA: 0x0003AB03 File Offset: 0x00038D03
		public override XmlSpace XmlSpace
		{
			get
			{
				return this.impl.XmlSpace;
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000D94 RID: 3476 RVA: 0x0003AB10 File Offset: 0x00038D10
		public override string XmlLang
		{
			get
			{
				return this.impl.XmlLang;
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000D95 RID: 3477 RVA: 0x0003AB1D File Offset: 0x00038D1D
		public override int AttributeCount
		{
			get
			{
				return this.impl.AttributeCount;
			}
		}

		// Token: 0x06000D96 RID: 3478 RVA: 0x0003AB2A File Offset: 0x00038D2A
		public override string GetAttribute(string name)
		{
			return this.impl.GetAttribute(name);
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x0003AB38 File Offset: 0x00038D38
		public override string GetAttribute(string localName, string namespaceURI)
		{
			return this.impl.GetAttribute(localName, namespaceURI);
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x0003AB47 File Offset: 0x00038D47
		public override string GetAttribute(int i)
		{
			return this.impl.GetAttribute(i);
		}

		// Token: 0x06000D99 RID: 3481 RVA: 0x0003AB55 File Offset: 0x00038D55
		public override bool MoveToAttribute(string name)
		{
			return this.impl.MoveToAttribute(name);
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x0003AB63 File Offset: 0x00038D63
		public override bool MoveToAttribute(string localName, string namespaceURI)
		{
			return this.impl.MoveToAttribute(localName, namespaceURI);
		}

		// Token: 0x06000D9B RID: 3483 RVA: 0x0003AB72 File Offset: 0x00038D72
		public override void MoveToAttribute(int i)
		{
			this.impl.MoveToAttribute(i);
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x0003AB80 File Offset: 0x00038D80
		public override bool MoveToFirstAttribute()
		{
			return this.impl.MoveToFirstAttribute();
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x0003AB8D File Offset: 0x00038D8D
		public override bool MoveToNextAttribute()
		{
			return this.impl.MoveToNextAttribute();
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x0003AB9A File Offset: 0x00038D9A
		public override bool MoveToElement()
		{
			return this.impl.MoveToElement();
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x0003ABA7 File Offset: 0x00038DA7
		public override bool ReadAttributeValue()
		{
			return this.impl.ReadAttributeValue();
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x0003ABB4 File Offset: 0x00038DB4
		public override bool Read()
		{
			return this.impl.Read();
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000DA1 RID: 3489 RVA: 0x0003ABC1 File Offset: 0x00038DC1
		public override bool EOF
		{
			get
			{
				return this.impl.EOF;
			}
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x0003ABCE File Offset: 0x00038DCE
		public override void Close()
		{
			this.impl.Close();
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000DA3 RID: 3491 RVA: 0x0003ABDB File Offset: 0x00038DDB
		public override ReadState ReadState
		{
			get
			{
				return this.impl.ReadState;
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000DA4 RID: 3492 RVA: 0x0003ABE8 File Offset: 0x00038DE8
		public override XmlNameTable NameTable
		{
			get
			{
				return this.impl.NameTable;
			}
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x0003ABF8 File Offset: 0x00038DF8
		public override string LookupNamespace(string prefix)
		{
			string text = this.impl.LookupNamespace(prefix);
			if (text != null && text.Length == 0)
			{
				text = null;
			}
			return text;
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000DA6 RID: 3494 RVA: 0x0003AC20 File Offset: 0x00038E20
		public override bool CanResolveEntity
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000DA7 RID: 3495 RVA: 0x0003AC23 File Offset: 0x00038E23
		public override void ResolveEntity()
		{
			this.impl.ResolveEntity();
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000DA8 RID: 3496 RVA: 0x0003AC30 File Offset: 0x00038E30
		public override bool CanReadBinaryContent
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000DA9 RID: 3497 RVA: 0x0003AC33 File Offset: 0x00038E33
		public override int ReadContentAsBase64(byte[] buffer, int index, int count)
		{
			return this.impl.ReadContentAsBase64(buffer, index, count);
		}

		// Token: 0x06000DAA RID: 3498 RVA: 0x0003AC43 File Offset: 0x00038E43
		public override int ReadElementContentAsBase64(byte[] buffer, int index, int count)
		{
			return this.impl.ReadElementContentAsBase64(buffer, index, count);
		}

		// Token: 0x06000DAB RID: 3499 RVA: 0x0003AC53 File Offset: 0x00038E53
		public override int ReadContentAsBinHex(byte[] buffer, int index, int count)
		{
			return this.impl.ReadContentAsBinHex(buffer, index, count);
		}

		// Token: 0x06000DAC RID: 3500 RVA: 0x0003AC63 File Offset: 0x00038E63
		public override int ReadElementContentAsBinHex(byte[] buffer, int index, int count)
		{
			return this.impl.ReadElementContentAsBinHex(buffer, index, count);
		}

		// Token: 0x06000DAD RID: 3501 RVA: 0x0003AC73 File Offset: 0x00038E73
		public override string ReadString()
		{
			this.impl.MoveOffEntityReference();
			return base.ReadString();
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x0003AC86 File Offset: 0x00038E86
		public bool HasLineInfo()
		{
			return true;
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000DAF RID: 3503 RVA: 0x0003AC89 File Offset: 0x00038E89
		public int LineNumber
		{
			get
			{
				return this.impl.LineNumber;
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000DB0 RID: 3504 RVA: 0x0003AC96 File Offset: 0x00038E96
		public int LinePosition
		{
			get
			{
				return this.impl.LinePosition;
			}
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x0003ACA3 File Offset: 0x00038EA3
		IDictionary<string, string> IXmlNamespaceResolver.GetNamespacesInScope(XmlNamespaceScope scope)
		{
			return this.impl.GetNamespacesInScope(scope);
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x0003ACB1 File Offset: 0x00038EB1
		string IXmlNamespaceResolver.LookupNamespace(string prefix)
		{
			return this.impl.LookupNamespace(prefix);
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x0003ACBF File Offset: 0x00038EBF
		string IXmlNamespaceResolver.LookupPrefix(string namespaceName)
		{
			return this.impl.LookupPrefix(namespaceName);
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000DB4 RID: 3508 RVA: 0x0003ACCD File Offset: 0x00038ECD
		// (remove) Token: 0x06000DB5 RID: 3509 RVA: 0x0003ACDB File Offset: 0x00038EDB
		public event ValidationEventHandler ValidationEventHandler
		{
			add
			{
				this.impl.ValidationEventHandler += value;
			}
			remove
			{
				this.impl.ValidationEventHandler -= value;
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000DB6 RID: 3510 RVA: 0x0003ACE9 File Offset: 0x00038EE9
		public object SchemaType
		{
			get
			{
				return this.impl.SchemaType;
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000DB7 RID: 3511 RVA: 0x0003ACF6 File Offset: 0x00038EF6
		public XmlReader Reader
		{
			get
			{
				return this.impl.Reader;
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000DB8 RID: 3512 RVA: 0x0003AD03 File Offset: 0x00038F03
		// (set) Token: 0x06000DB9 RID: 3513 RVA: 0x0003AD10 File Offset: 0x00038F10
		public ValidationType ValidationType
		{
			get
			{
				return this.impl.ValidationType;
			}
			set
			{
				this.impl.ValidationType = value;
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000DBA RID: 3514 RVA: 0x0003AD1E File Offset: 0x00038F1E
		public XmlSchemaCollection Schemas
		{
			get
			{
				return this.impl.Schemas;
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000DBB RID: 3515 RVA: 0x0003AD2B File Offset: 0x00038F2B
		// (set) Token: 0x06000DBC RID: 3516 RVA: 0x0003AD38 File Offset: 0x00038F38
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

		// Token: 0x17000276 RID: 630
		// (set) Token: 0x06000DBD RID: 3517 RVA: 0x0003AD46 File Offset: 0x00038F46
		public XmlResolver XmlResolver
		{
			set
			{
				this.impl.XmlResolver = value;
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000DBE RID: 3518 RVA: 0x0003AD54 File Offset: 0x00038F54
		// (set) Token: 0x06000DBF RID: 3519 RVA: 0x0003AD61 File Offset: 0x00038F61
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

		// Token: 0x06000DC0 RID: 3520 RVA: 0x0003AD6F File Offset: 0x00038F6F
		public object ReadTypedValue()
		{
			return this.impl.ReadTypedValue();
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000DC1 RID: 3521 RVA: 0x0003AD7C File Offset: 0x00038F7C
		public Encoding Encoding
		{
			get
			{
				return this.impl.Encoding;
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000DC2 RID: 3522 RVA: 0x0003AD89 File Offset: 0x00038F89
		internal XmlValidatingReaderImpl Impl
		{
			get
			{
				return this.impl;
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000DC3 RID: 3523 RVA: 0x0003AD91 File Offset: 0x00038F91
		internal override IDtdInfo DtdInfo
		{
			get
			{
				return this.impl.DtdInfo;
			}
		}

		// Token: 0x0400040D RID: 1037
		private XmlValidatingReaderImpl impl;
	}
}
