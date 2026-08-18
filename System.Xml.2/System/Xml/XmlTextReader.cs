using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Security.Permissions;
using System.Text;

namespace System.Xml
{
	// Token: 0x020000D9 RID: 217
	[EditorBrowsable(EditorBrowsableState.Never)]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class XmlTextReader : XmlReader, IXmlLineInfo, IXmlNamespaceResolver
	{
		// Token: 0x06000AA7 RID: 2727 RVA: 0x00025769 File Offset: 0x00023969
		protected XmlTextReader()
		{
			this.impl = new XmlTextReaderImpl();
			this.impl.OuterReader = this;
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x00025788 File Offset: 0x00023988
		protected XmlTextReader(XmlNameTable nt)
		{
			this.impl = new XmlTextReaderImpl(nt);
			this.impl.OuterReader = this;
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x000257A8 File Offset: 0x000239A8
		public XmlTextReader(Stream input)
		{
			this.impl = new XmlTextReaderImpl(input);
			this.impl.OuterReader = this;
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x000257C8 File Offset: 0x000239C8
		public XmlTextReader(string url, Stream input)
		{
			this.impl = new XmlTextReaderImpl(url, input);
			this.impl.OuterReader = this;
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x000257E9 File Offset: 0x000239E9
		public XmlTextReader(Stream input, XmlNameTable nt)
		{
			this.impl = new XmlTextReaderImpl(input, nt);
			this.impl.OuterReader = this;
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x0002580A File Offset: 0x00023A0A
		public XmlTextReader(string url, Stream input, XmlNameTable nt)
		{
			this.impl = new XmlTextReaderImpl(url, input, nt);
			this.impl.OuterReader = this;
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x0002582C File Offset: 0x00023A2C
		public XmlTextReader(TextReader input)
		{
			this.impl = new XmlTextReaderImpl(input);
			this.impl.OuterReader = this;
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x0002584C File Offset: 0x00023A4C
		public XmlTextReader(string url, TextReader input)
		{
			this.impl = new XmlTextReaderImpl(url, input);
			this.impl.OuterReader = this;
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x0002586D File Offset: 0x00023A6D
		public XmlTextReader(TextReader input, XmlNameTable nt)
		{
			this.impl = new XmlTextReaderImpl(input, nt);
			this.impl.OuterReader = this;
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x0002588E File Offset: 0x00023A8E
		public XmlTextReader(string url, TextReader input, XmlNameTable nt)
		{
			this.impl = new XmlTextReaderImpl(url, input, nt);
			this.impl.OuterReader = this;
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x000258B0 File Offset: 0x00023AB0
		public XmlTextReader(Stream xmlFragment, XmlNodeType fragType, XmlParserContext context)
		{
			this.impl = new XmlTextReaderImpl(xmlFragment, fragType, context);
			this.impl.OuterReader = this;
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x000258D2 File Offset: 0x00023AD2
		public XmlTextReader(string xmlFragment, XmlNodeType fragType, XmlParserContext context)
		{
			this.impl = new XmlTextReaderImpl(xmlFragment, fragType, context);
			this.impl.OuterReader = this;
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x000258F4 File Offset: 0x00023AF4
		public XmlTextReader(string url)
		{
			this.impl = new XmlTextReaderImpl(url, new NameTable());
			this.impl.OuterReader = this;
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x00025919 File Offset: 0x00023B19
		public XmlTextReader(string url, XmlNameTable nt)
		{
			this.impl = new XmlTextReaderImpl(url, nt);
			this.impl.OuterReader = this;
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000AB5 RID: 2741 RVA: 0x0002593A File Offset: 0x00023B3A
		public override XmlNodeType NodeType
		{
			get
			{
				return this.impl.NodeType;
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000AB6 RID: 2742 RVA: 0x00025947 File Offset: 0x00023B47
		public override string Name
		{
			get
			{
				return this.impl.Name;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000AB7 RID: 2743 RVA: 0x00025954 File Offset: 0x00023B54
		public override string LocalName
		{
			get
			{
				return this.impl.LocalName;
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000AB8 RID: 2744 RVA: 0x00025961 File Offset: 0x00023B61
		public override string NamespaceURI
		{
			get
			{
				return this.impl.NamespaceURI;
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000AB9 RID: 2745 RVA: 0x0002596E File Offset: 0x00023B6E
		public override string Prefix
		{
			get
			{
				return this.impl.Prefix;
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000ABA RID: 2746 RVA: 0x0002597B File Offset: 0x00023B7B
		public override bool HasValue
		{
			get
			{
				return this.impl.HasValue;
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000ABB RID: 2747 RVA: 0x00025988 File Offset: 0x00023B88
		public override string Value
		{
			get
			{
				return this.impl.Value;
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000ABC RID: 2748 RVA: 0x00025995 File Offset: 0x00023B95
		public override int Depth
		{
			get
			{
				return this.impl.Depth;
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000ABD RID: 2749 RVA: 0x000259A2 File Offset: 0x00023BA2
		public override string BaseURI
		{
			get
			{
				return this.impl.BaseURI;
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000ABE RID: 2750 RVA: 0x000259AF File Offset: 0x00023BAF
		public override bool IsEmptyElement
		{
			get
			{
				return this.impl.IsEmptyElement;
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000ABF RID: 2751 RVA: 0x000259BC File Offset: 0x00023BBC
		public override bool IsDefault
		{
			get
			{
				return this.impl.IsDefault;
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000AC0 RID: 2752 RVA: 0x000259C9 File Offset: 0x00023BC9
		public override char QuoteChar
		{
			get
			{
				return this.impl.QuoteChar;
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000AC1 RID: 2753 RVA: 0x000259D6 File Offset: 0x00023BD6
		public override XmlSpace XmlSpace
		{
			get
			{
				return this.impl.XmlSpace;
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000AC2 RID: 2754 RVA: 0x000259E3 File Offset: 0x00023BE3
		public override string XmlLang
		{
			get
			{
				return this.impl.XmlLang;
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000AC3 RID: 2755 RVA: 0x000259F0 File Offset: 0x00023BF0
		public override int AttributeCount
		{
			get
			{
				return this.impl.AttributeCount;
			}
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x000259FD File Offset: 0x00023BFD
		public override string GetAttribute(string name)
		{
			return this.impl.GetAttribute(name);
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x00025A0B File Offset: 0x00023C0B
		public override string GetAttribute(string localName, string namespaceURI)
		{
			return this.impl.GetAttribute(localName, namespaceURI);
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x00025A1A File Offset: 0x00023C1A
		public override string GetAttribute(int i)
		{
			return this.impl.GetAttribute(i);
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x00025A28 File Offset: 0x00023C28
		public override bool MoveToAttribute(string name)
		{
			return this.impl.MoveToAttribute(name);
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x00025A36 File Offset: 0x00023C36
		public override bool MoveToAttribute(string localName, string namespaceURI)
		{
			return this.impl.MoveToAttribute(localName, namespaceURI);
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x00025A45 File Offset: 0x00023C45
		public override void MoveToAttribute(int i)
		{
			this.impl.MoveToAttribute(i);
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x00025A53 File Offset: 0x00023C53
		public override bool MoveToFirstAttribute()
		{
			return this.impl.MoveToFirstAttribute();
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x00025A60 File Offset: 0x00023C60
		public override bool MoveToNextAttribute()
		{
			return this.impl.MoveToNextAttribute();
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x00025A6D File Offset: 0x00023C6D
		public override bool MoveToElement()
		{
			return this.impl.MoveToElement();
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x00025A7A File Offset: 0x00023C7A
		public override bool ReadAttributeValue()
		{
			return this.impl.ReadAttributeValue();
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x00025A87 File Offset: 0x00023C87
		public override bool Read()
		{
			return this.impl.Read();
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000ACF RID: 2767 RVA: 0x00025A94 File Offset: 0x00023C94
		public override bool EOF
		{
			get
			{
				return this.impl.EOF;
			}
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x00025AA1 File Offset: 0x00023CA1
		public override void Close()
		{
			this.impl.Close();
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000AD1 RID: 2769 RVA: 0x00025AAE File Offset: 0x00023CAE
		public override ReadState ReadState
		{
			get
			{
				return this.impl.ReadState;
			}
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x00025ABB File Offset: 0x00023CBB
		public override void Skip()
		{
			this.impl.Skip();
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000AD3 RID: 2771 RVA: 0x00025AC8 File Offset: 0x00023CC8
		public override XmlNameTable NameTable
		{
			get
			{
				return this.impl.NameTable;
			}
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x00025AD8 File Offset: 0x00023CD8
		public override string LookupNamespace(string prefix)
		{
			string text = this.impl.LookupNamespace(prefix);
			if (text != null && text.Length == 0)
			{
				text = null;
			}
			return text;
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000AD5 RID: 2773 RVA: 0x00025B00 File Offset: 0x00023D00
		public override bool CanResolveEntity
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x00025B03 File Offset: 0x00023D03
		public override void ResolveEntity()
		{
			this.impl.ResolveEntity();
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000AD7 RID: 2775 RVA: 0x00025B10 File Offset: 0x00023D10
		public override bool CanReadBinaryContent
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x00025B13 File Offset: 0x00023D13
		public override int ReadContentAsBase64(byte[] buffer, int index, int count)
		{
			return this.impl.ReadContentAsBase64(buffer, index, count);
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x00025B23 File Offset: 0x00023D23
		public override int ReadElementContentAsBase64(byte[] buffer, int index, int count)
		{
			return this.impl.ReadElementContentAsBase64(buffer, index, count);
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x00025B33 File Offset: 0x00023D33
		public override int ReadContentAsBinHex(byte[] buffer, int index, int count)
		{
			return this.impl.ReadContentAsBinHex(buffer, index, count);
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x00025B43 File Offset: 0x00023D43
		public override int ReadElementContentAsBinHex(byte[] buffer, int index, int count)
		{
			return this.impl.ReadElementContentAsBinHex(buffer, index, count);
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000ADC RID: 2780 RVA: 0x00025B53 File Offset: 0x00023D53
		public override bool CanReadValueChunk
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x00025B56 File Offset: 0x00023D56
		public override string ReadString()
		{
			this.impl.MoveOffEntityReference();
			return base.ReadString();
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x00025B69 File Offset: 0x00023D69
		public bool HasLineInfo()
		{
			return true;
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000ADF RID: 2783 RVA: 0x00025B6C File Offset: 0x00023D6C
		public int LineNumber
		{
			get
			{
				return this.impl.LineNumber;
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000AE0 RID: 2784 RVA: 0x00025B79 File Offset: 0x00023D79
		public int LinePosition
		{
			get
			{
				return this.impl.LinePosition;
			}
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x00025B86 File Offset: 0x00023D86
		IDictionary<string, string> IXmlNamespaceResolver.GetNamespacesInScope(XmlNamespaceScope scope)
		{
			return this.impl.GetNamespacesInScope(scope);
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x00025B94 File Offset: 0x00023D94
		string IXmlNamespaceResolver.LookupNamespace(string prefix)
		{
			return this.impl.LookupNamespace(prefix);
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x00025BA2 File Offset: 0x00023DA2
		string IXmlNamespaceResolver.LookupPrefix(string namespaceName)
		{
			return this.impl.LookupPrefix(namespaceName);
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x00025BB0 File Offset: 0x00023DB0
		public IDictionary<string, string> GetNamespacesInScope(XmlNamespaceScope scope)
		{
			return this.impl.GetNamespacesInScope(scope);
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000AE5 RID: 2789 RVA: 0x00025BBE File Offset: 0x00023DBE
		// (set) Token: 0x06000AE6 RID: 2790 RVA: 0x00025BCB File Offset: 0x00023DCB
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

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000AE7 RID: 2791 RVA: 0x00025BD9 File Offset: 0x00023DD9
		// (set) Token: 0x06000AE8 RID: 2792 RVA: 0x00025BE6 File Offset: 0x00023DE6
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

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000AE9 RID: 2793 RVA: 0x00025BF4 File Offset: 0x00023DF4
		public Encoding Encoding
		{
			get
			{
				return this.impl.Encoding;
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000AEA RID: 2794 RVA: 0x00025C01 File Offset: 0x00023E01
		// (set) Token: 0x06000AEB RID: 2795 RVA: 0x00025C0E File Offset: 0x00023E0E
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

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000AEC RID: 2796 RVA: 0x00025C1C File Offset: 0x00023E1C
		// (set) Token: 0x06000AED RID: 2797 RVA: 0x00025C2C File Offset: 0x00023E2C
		[Obsolete("Use DtdProcessing property instead.")]
		public bool ProhibitDtd
		{
			get
			{
				return this.impl.DtdProcessing == DtdProcessing.Prohibit;
			}
			set
			{
				this.impl.DtdProcessing = (value ? DtdProcessing.Prohibit : DtdProcessing.Parse);
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000AEE RID: 2798 RVA: 0x00025C40 File Offset: 0x00023E40
		// (set) Token: 0x06000AEF RID: 2799 RVA: 0x00025C4D File Offset: 0x00023E4D
		public DtdProcessing DtdProcessing
		{
			get
			{
				return this.impl.DtdProcessing;
			}
			set
			{
				this.impl.DtdProcessing = value;
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000AF0 RID: 2800 RVA: 0x00025C5B File Offset: 0x00023E5B
		// (set) Token: 0x06000AF1 RID: 2801 RVA: 0x00025C68 File Offset: 0x00023E68
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

		// Token: 0x1700020A RID: 522
		// (set) Token: 0x06000AF2 RID: 2802 RVA: 0x00025C76 File Offset: 0x00023E76
		public XmlResolver XmlResolver
		{
			set
			{
				this.impl.XmlResolver = value;
			}
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x00025C84 File Offset: 0x00023E84
		public void ResetState()
		{
			this.impl.ResetState();
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x00025C91 File Offset: 0x00023E91
		public TextReader GetRemainder()
		{
			return this.impl.GetRemainder();
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x00025C9E File Offset: 0x00023E9E
		public int ReadChars(char[] buffer, int index, int count)
		{
			return this.impl.ReadChars(buffer, index, count);
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x00025CAE File Offset: 0x00023EAE
		public int ReadBase64(byte[] array, int offset, int len)
		{
			return this.impl.ReadBase64(array, offset, len);
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x00025CBE File Offset: 0x00023EBE
		public int ReadBinHex(byte[] array, int offset, int len)
		{
			return this.impl.ReadBinHex(array, offset, len);
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000AF8 RID: 2808 RVA: 0x00025CCE File Offset: 0x00023ECE
		internal XmlTextReaderImpl Impl
		{
			get
			{
				return this.impl;
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000AF9 RID: 2809 RVA: 0x00025CD6 File Offset: 0x00023ED6
		internal override XmlNamespaceManager NamespaceManager
		{
			get
			{
				return this.impl.NamespaceManager;
			}
		}

		// Token: 0x1700020D RID: 525
		// (set) Token: 0x06000AFA RID: 2810 RVA: 0x00025CE3 File Offset: 0x00023EE3
		internal bool XmlValidatingReaderCompatibilityMode
		{
			set
			{
				this.impl.XmlValidatingReaderCompatibilityMode = value;
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000AFB RID: 2811 RVA: 0x00025CF1 File Offset: 0x00023EF1
		internal override IDtdInfo DtdInfo
		{
			get
			{
				return this.impl.DtdInfo;
			}
		}

		// Token: 0x0400036B RID: 875
		private XmlTextReaderImpl impl;
	}
}
