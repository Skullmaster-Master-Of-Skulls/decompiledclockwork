using System;

namespace System.Xml
{
	// Token: 0x020000C1 RID: 193
	internal class ValidatingReaderNodeData
	{
		// Token: 0x060006B6 RID: 1718 RVA: 0x00017AA7 File Offset: 0x00015CA7
		public ValidatingReaderNodeData()
		{
			this.Clear(XmlNodeType.None);
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x00017AB6 File Offset: 0x00015CB6
		public ValidatingReaderNodeData(XmlNodeType nodeType)
		{
			this.Clear(nodeType);
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060006B8 RID: 1720 RVA: 0x00017AC5 File Offset: 0x00015CC5
		// (set) Token: 0x060006B9 RID: 1721 RVA: 0x00017ACD File Offset: 0x00015CCD
		public string LocalName
		{
			get
			{
				return this.localName;
			}
			set
			{
				this.localName = value;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060006BA RID: 1722 RVA: 0x00017AD6 File Offset: 0x00015CD6
		// (set) Token: 0x060006BB RID: 1723 RVA: 0x00017ADE File Offset: 0x00015CDE
		public string Namespace
		{
			get
			{
				return this.namespaceUri;
			}
			set
			{
				this.namespaceUri = value;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060006BC RID: 1724 RVA: 0x00017AE7 File Offset: 0x00015CE7
		// (set) Token: 0x060006BD RID: 1725 RVA: 0x00017AEF File Offset: 0x00015CEF
		public string Prefix
		{
			get
			{
				return this.prefix;
			}
			set
			{
				this.prefix = value;
			}
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x00017AF8 File Offset: 0x00015CF8
		public string GetAtomizedNameWPrefix(XmlNameTable nameTable)
		{
			if (this.nameWPrefix == null)
			{
				if (this.prefix.Length == 0)
				{
					this.nameWPrefix = this.localName;
				}
				else
				{
					this.nameWPrefix = nameTable.Add(this.prefix + ":" + this.localName);
				}
			}
			return this.nameWPrefix;
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060006BF RID: 1727 RVA: 0x00017B50 File Offset: 0x00015D50
		// (set) Token: 0x060006C0 RID: 1728 RVA: 0x00017B58 File Offset: 0x00015D58
		public int Depth
		{
			get
			{
				return this.depth;
			}
			set
			{
				this.depth = value;
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060006C1 RID: 1729 RVA: 0x00017B61 File Offset: 0x00015D61
		// (set) Token: 0x060006C2 RID: 1730 RVA: 0x00017B69 File Offset: 0x00015D69
		public string RawValue
		{
			get
			{
				return this.rawValue;
			}
			set
			{
				this.rawValue = value;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060006C3 RID: 1731 RVA: 0x00017B72 File Offset: 0x00015D72
		// (set) Token: 0x060006C4 RID: 1732 RVA: 0x00017B7A File Offset: 0x00015D7A
		public string OriginalStringValue
		{
			get
			{
				return this.originalStringValue;
			}
			set
			{
				this.originalStringValue = value;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060006C5 RID: 1733 RVA: 0x00017B83 File Offset: 0x00015D83
		// (set) Token: 0x060006C6 RID: 1734 RVA: 0x00017B8B File Offset: 0x00015D8B
		public XmlNodeType NodeType
		{
			get
			{
				return this.nodeType;
			}
			set
			{
				this.nodeType = value;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060006C7 RID: 1735 RVA: 0x00017B94 File Offset: 0x00015D94
		// (set) Token: 0x060006C8 RID: 1736 RVA: 0x00017B9C File Offset: 0x00015D9C
		public AttributePSVIInfo AttInfo
		{
			get
			{
				return this.attributePSVIInfo;
			}
			set
			{
				this.attributePSVIInfo = value;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060006C9 RID: 1737 RVA: 0x00017BA5 File Offset: 0x00015DA5
		public int LineNumber
		{
			get
			{
				return this.lineNo;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060006CA RID: 1738 RVA: 0x00017BAD File Offset: 0x00015DAD
		public int LinePosition
		{
			get
			{
				return this.linePos;
			}
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x00017BB8 File Offset: 0x00015DB8
		internal void Clear(XmlNodeType nodeType)
		{
			this.nodeType = nodeType;
			this.localName = string.Empty;
			this.prefix = string.Empty;
			this.namespaceUri = string.Empty;
			this.rawValue = string.Empty;
			if (this.attributePSVIInfo != null)
			{
				this.attributePSVIInfo.Reset();
			}
			this.nameWPrefix = null;
			this.lineNo = 0;
			this.linePos = 0;
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x00017C20 File Offset: 0x00015E20
		internal void ClearName()
		{
			this.localName = string.Empty;
			this.prefix = string.Empty;
			this.namespaceUri = string.Empty;
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x00017C43 File Offset: 0x00015E43
		internal void SetLineInfo(int lineNo, int linePos)
		{
			this.lineNo = lineNo;
			this.linePos = linePos;
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x00017C53 File Offset: 0x00015E53
		internal void SetLineInfo(IXmlLineInfo lineInfo)
		{
			if (lineInfo != null)
			{
				this.lineNo = lineInfo.LineNumber;
				this.linePos = lineInfo.LinePosition;
			}
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x00017C70 File Offset: 0x00015E70
		internal void SetItemData(string localName, string prefix, string ns, string value)
		{
			this.localName = localName;
			this.prefix = prefix;
			this.namespaceUri = ns;
			this.rawValue = value;
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x00017C8F File Offset: 0x00015E8F
		internal void SetItemData(string localName, string prefix, string ns, int depth)
		{
			this.localName = localName;
			this.prefix = prefix;
			this.namespaceUri = ns;
			this.depth = depth;
			this.rawValue = string.Empty;
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x00017CB9 File Offset: 0x00015EB9
		internal void SetItemData(string value)
		{
			this.SetItemData(value, value);
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x00017CC3 File Offset: 0x00015EC3
		internal void SetItemData(string value, string originalStringValue)
		{
			this.rawValue = value;
			this.originalStringValue = originalStringValue;
		}

		// Token: 0x040002C5 RID: 709
		private string localName;

		// Token: 0x040002C6 RID: 710
		private string namespaceUri;

		// Token: 0x040002C7 RID: 711
		private string prefix;

		// Token: 0x040002C8 RID: 712
		private string nameWPrefix;

		// Token: 0x040002C9 RID: 713
		private string rawValue;

		// Token: 0x040002CA RID: 714
		private string originalStringValue;

		// Token: 0x040002CB RID: 715
		private int depth;

		// Token: 0x040002CC RID: 716
		private AttributePSVIInfo attributePSVIInfo;

		// Token: 0x040002CD RID: 717
		private XmlNodeType nodeType;

		// Token: 0x040002CE RID: 718
		private int lineNo;

		// Token: 0x040002CF RID: 719
		private int linePos;
	}
}
