using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.Cache
{
	// Token: 0x02000055 RID: 85
	internal struct XPathNode
	{
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x0000BECC File Offset: 0x0000A0CC
		public XPathNodeType NodeType
		{
			get
			{
				return (XPathNodeType)(this.props & 15U);
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x0000BED7 File Offset: 0x0000A0D7
		public string Prefix
		{
			get
			{
				return this.info.Prefix;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x0000BEE4 File Offset: 0x0000A0E4
		public string LocalName
		{
			get
			{
				return this.info.LocalName;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x0000BEF1 File Offset: 0x0000A0F1
		public string Name
		{
			get
			{
				if (this.Prefix.Length == 0)
				{
					return this.LocalName;
				}
				return this.Prefix + ":" + this.LocalName;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060002EA RID: 746 RVA: 0x0000BF1D File Offset: 0x0000A11D
		public string NamespaceUri
		{
			get
			{
				return this.info.NamespaceUri;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060002EB RID: 747 RVA: 0x0000BF2A File Offset: 0x0000A12A
		public XPathDocument Document
		{
			get
			{
				return this.info.Document;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060002EC RID: 748 RVA: 0x0000BF37 File Offset: 0x0000A137
		public string BaseUri
		{
			get
			{
				return this.info.BaseUri;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060002ED RID: 749 RVA: 0x0000BF44 File Offset: 0x0000A144
		public int LineNumber
		{
			get
			{
				return this.info.LineNumberBase + (int)((this.props & 16776192U) >> 10);
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060002EE RID: 750 RVA: 0x0000BF61 File Offset: 0x0000A161
		public int LinePosition
		{
			get
			{
				return this.info.LinePositionBase + (int)this.posOffset;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060002EF RID: 751 RVA: 0x0000BF75 File Offset: 0x0000A175
		public int CollapsedLinePosition
		{
			get
			{
				return this.LinePosition + (int)(this.props >> 24);
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x0000BF87 File Offset: 0x0000A187
		public XPathNodePageInfo PageInfo
		{
			get
			{
				return this.info.PageInfo;
			}
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000BF94 File Offset: 0x0000A194
		public int GetRoot(out XPathNode[] pageNode)
		{
			return this.info.Document.GetRootNode(out pageNode);
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000BFA7 File Offset: 0x0000A1A7
		public int GetParent(out XPathNode[] pageNode)
		{
			pageNode = this.info.ParentPage;
			return (int)this.idxParent;
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000BFBC File Offset: 0x0000A1BC
		public int GetSibling(out XPathNode[] pageNode)
		{
			pageNode = this.info.SiblingPage;
			return (int)this.idxSibling;
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000BFD1 File Offset: 0x0000A1D1
		public int GetSimilarElement(out XPathNode[] pageNode)
		{
			pageNode = this.info.SimilarElementPage;
			return (int)this.idxSimilar;
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000BFE6 File Offset: 0x0000A1E6
		public bool NameMatch(string localName, string namespaceName)
		{
			return this.info.LocalName == localName && this.info.NamespaceUri == namespaceName;
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000C009 File Offset: 0x0000A209
		public bool ElementMatch(string localName, string namespaceName)
		{
			return this.NodeType == XPathNodeType.Element && this.info.LocalName == localName && this.info.NamespaceUri == namespaceName;
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x0000C038 File Offset: 0x0000A238
		public bool IsXmlNamespaceNode
		{
			get
			{
				string localName = this.info.LocalName;
				return this.NodeType == XPathNodeType.Namespace && localName.Length == 3 && localName == "xml";
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x0000C070 File Offset: 0x0000A270
		public bool HasSibling
		{
			get
			{
				return this.idxSibling > 0;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x0000C07B File Offset: 0x0000A27B
		public bool HasCollapsedText
		{
			get
			{
				return (this.props & 128U) > 0U;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060002FA RID: 762 RVA: 0x0000C08C File Offset: 0x0000A28C
		public bool HasAttribute
		{
			get
			{
				return (this.props & 16U) > 0U;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060002FB RID: 763 RVA: 0x0000C09A File Offset: 0x0000A29A
		public bool HasContentChild
		{
			get
			{
				return (this.props & 32U) > 0U;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060002FC RID: 764 RVA: 0x0000C0A8 File Offset: 0x0000A2A8
		public bool HasElementChild
		{
			get
			{
				return (this.props & 64U) > 0U;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060002FD RID: 765 RVA: 0x0000C0B8 File Offset: 0x0000A2B8
		public bool IsAttrNmsp
		{
			get
			{
				XPathNodeType nodeType = this.NodeType;
				return nodeType == XPathNodeType.Attribute || nodeType == XPathNodeType.Namespace;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060002FE RID: 766 RVA: 0x0000C0D6 File Offset: 0x0000A2D6
		public bool IsText
		{
			get
			{
				return XPathNavigator.IsText(this.NodeType);
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060002FF RID: 767 RVA: 0x0000C0E3 File Offset: 0x0000A2E3
		// (set) Token: 0x06000300 RID: 768 RVA: 0x0000C0F4 File Offset: 0x0000A2F4
		public bool HasNamespaceDecls
		{
			get
			{
				return (this.props & 512U) > 0U;
			}
			set
			{
				if (value)
				{
					this.props |= 512U;
					return;
				}
				this.props &= 255U;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000301 RID: 769 RVA: 0x0000C11E File Offset: 0x0000A31E
		public bool AllowShortcutTag
		{
			get
			{
				return (this.props & 256U) > 0U;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000302 RID: 770 RVA: 0x0000C12F File Offset: 0x0000A32F
		public int LocalNameHashCode
		{
			get
			{
				return this.info.LocalNameHashCode;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000303 RID: 771 RVA: 0x0000C13C File Offset: 0x0000A33C
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000C144 File Offset: 0x0000A344
		public void Create(XPathNodePageInfo pageInfo)
		{
			this.info = new XPathNodeInfoAtom(pageInfo);
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000C152 File Offset: 0x0000A352
		public void Create(XPathNodeInfoAtom info, XPathNodeType xptyp, int idxParent)
		{
			this.info = info;
			this.props = (uint)xptyp;
			this.idxParent = (ushort)idxParent;
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000C16A File Offset: 0x0000A36A
		public void SetLineInfoOffsets(int lineNumOffset, int linePosOffset)
		{
			this.props |= (uint)((uint)lineNumOffset << 10);
			this.posOffset = (ushort)linePosOffset;
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000C185 File Offset: 0x0000A385
		public void SetCollapsedLineInfoOffset(int posOffset)
		{
			this.props |= (uint)((uint)posOffset << 24);
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000C198 File Offset: 0x0000A398
		public void SetValue(string value)
		{
			this.value = value;
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000C1A1 File Offset: 0x0000A3A1
		public void SetEmptyValue(bool allowShortcutTag)
		{
			this.value = string.Empty;
			if (allowShortcutTag)
			{
				this.props |= 256U;
			}
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000C1C3 File Offset: 0x0000A3C3
		public void SetCollapsedValue(string value)
		{
			this.value = value;
			this.props |= 160U;
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000C1DE File Offset: 0x0000A3DE
		public void SetParentProperties(XPathNodeType xptyp)
		{
			if (xptyp == XPathNodeType.Attribute)
			{
				this.props |= 16U;
				return;
			}
			this.props |= 32U;
			if (xptyp == XPathNodeType.Element)
			{
				this.props |= 64U;
			}
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000C218 File Offset: 0x0000A418
		public void SetSibling(XPathNodeInfoTable infoTable, XPathNode[] pageSibling, int idxSibling)
		{
			this.idxSibling = (ushort)idxSibling;
			if (pageSibling != this.info.SiblingPage)
			{
				this.info = infoTable.Create(this.info.LocalName, this.info.NamespaceUri, this.info.Prefix, this.info.BaseUri, this.info.ParentPage, pageSibling, this.info.SimilarElementPage, this.info.Document, this.info.LineNumberBase, this.info.LinePositionBase);
			}
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000C2AC File Offset: 0x0000A4AC
		public void SetSimilarElement(XPathNodeInfoTable infoTable, XPathNode[] pageSimilar, int idxSimilar)
		{
			this.idxSimilar = (ushort)idxSimilar;
			if (pageSimilar != this.info.SimilarElementPage)
			{
				this.info = infoTable.Create(this.info.LocalName, this.info.NamespaceUri, this.info.Prefix, this.info.BaseUri, this.info.ParentPage, this.info.SiblingPage, pageSimilar, this.info.Document, this.info.LineNumberBase, this.info.LinePositionBase);
			}
		}

		// Token: 0x0400012B RID: 299
		private XPathNodeInfoAtom info;

		// Token: 0x0400012C RID: 300
		private ushort idxSibling;

		// Token: 0x0400012D RID: 301
		private ushort idxParent;

		// Token: 0x0400012E RID: 302
		private ushort idxSimilar;

		// Token: 0x0400012F RID: 303
		private ushort posOffset;

		// Token: 0x04000130 RID: 304
		private uint props;

		// Token: 0x04000131 RID: 305
		private string value;

		// Token: 0x04000132 RID: 306
		private const uint NodeTypeMask = 15U;

		// Token: 0x04000133 RID: 307
		private const uint HasAttributeBit = 16U;

		// Token: 0x04000134 RID: 308
		private const uint HasContentChildBit = 32U;

		// Token: 0x04000135 RID: 309
		private const uint HasElementChildBit = 64U;

		// Token: 0x04000136 RID: 310
		private const uint HasCollapsedTextBit = 128U;

		// Token: 0x04000137 RID: 311
		private const uint AllowShortcutTagBit = 256U;

		// Token: 0x04000138 RID: 312
		private const uint HasNmspDeclsBit = 512U;

		// Token: 0x04000139 RID: 313
		private const uint LineNumberMask = 16776192U;

		// Token: 0x0400013A RID: 314
		private const int LineNumberShift = 10;

		// Token: 0x0400013B RID: 315
		private const int CollapsedPositionShift = 24;

		// Token: 0x0400013C RID: 316
		public const int MaxLineNumberOffset = 16383;

		// Token: 0x0400013D RID: 317
		public const int MaxLinePositionOffset = 65535;

		// Token: 0x0400013E RID: 318
		public const int MaxCollapsedPositionOffset = 255;
	}
}
