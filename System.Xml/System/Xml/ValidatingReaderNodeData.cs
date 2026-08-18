using System;

namespace System.Xml
{
	// Token: 0x02000069 RID: 105
	internal class ValidatingReaderNodeData
	{
		// Token: 0x060003B1 RID: 945 RVA: 0x00011DFC File Offset: 0x00010DFC
		public ValidatingReaderNodeData()
		{
			this.Clear(XmlNodeType.None);
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x00011E0B File Offset: 0x00010E0B
		public ValidatingReaderNodeData(XmlNodeType nodeType)
		{
			this.Clear(nodeType);
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x00011E1A File Offset: 0x00010E1A
		// (set) Token: 0x060003B4 RID: 948 RVA: 0x00011E22 File Offset: 0x00010E22
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

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x00011E2B File Offset: 0x00010E2B
		// (set) Token: 0x060003B6 RID: 950 RVA: 0x00011E33 File Offset: 0x00010E33
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

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060003B7 RID: 951 RVA: 0x00011E3C File Offset: 0x00010E3C
		// (set) Token: 0x060003B8 RID: 952 RVA: 0x00011E44 File Offset: 0x00010E44
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

		// Token: 0x060003B9 RID: 953 RVA: 0x00011E50 File Offset: 0x00010E50
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

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060003BA RID: 954 RVA: 0x00011EA8 File Offset: 0x00010EA8
		// (set) Token: 0x060003BB RID: 955 RVA: 0x00011EB0 File Offset: 0x00010EB0
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

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060003BC RID: 956 RVA: 0x00011EB9 File Offset: 0x00010EB9
		// (set) Token: 0x060003BD RID: 957 RVA: 0x00011EC1 File Offset: 0x00010EC1
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

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060003BE RID: 958 RVA: 0x00011ECA File Offset: 0x00010ECA
		// (set) Token: 0x060003BF RID: 959 RVA: 0x00011ED2 File Offset: 0x00010ED2
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

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x00011EDB File Offset: 0x00010EDB
		// (set) Token: 0x060003C1 RID: 961 RVA: 0x00011EE3 File Offset: 0x00010EE3
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

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x00011EEC File Offset: 0x00010EEC
		// (set) Token: 0x060003C3 RID: 963 RVA: 0x00011EF4 File Offset: 0x00010EF4
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

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060003C4 RID: 964 RVA: 0x00011EFD File Offset: 0x00010EFD
		public int LineNumber
		{
			get
			{
				return this.lineNo;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060003C5 RID: 965 RVA: 0x00011F05 File Offset: 0x00010F05
		public int LinePosition
		{
			get
			{
				return this.linePos;
			}
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x00011F10 File Offset: 0x00010F10
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

		// Token: 0x060003C7 RID: 967 RVA: 0x00011F78 File Offset: 0x00010F78
		internal void ClearName()
		{
			this.localName = string.Empty;
			this.prefix = string.Empty;
			this.namespaceUri = string.Empty;
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x00011F9B File Offset: 0x00010F9B
		internal void SetLineInfo(int lineNo, int linePos)
		{
			this.lineNo = lineNo;
			this.linePos = linePos;
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x00011FAB File Offset: 0x00010FAB
		internal void SetLineInfo(IXmlLineInfo lineInfo)
		{
			if (lineInfo != null)
			{
				this.lineNo = lineInfo.LineNumber;
				this.linePos = lineInfo.LinePosition;
			}
		}

		// Token: 0x060003CA RID: 970 RVA: 0x00011FC8 File Offset: 0x00010FC8
		internal void SetItemData(string localName, string prefix, string ns, string value)
		{
			this.localName = localName;
			this.prefix = prefix;
			this.namespaceUri = ns;
			this.rawValue = value;
		}

		// Token: 0x060003CB RID: 971 RVA: 0x00011FE7 File Offset: 0x00010FE7
		internal void SetItemData(string localName, string prefix, string ns, int depth)
		{
			this.localName = localName;
			this.prefix = prefix;
			this.namespaceUri = ns;
			this.depth = depth;
			this.rawValue = string.Empty;
		}

		// Token: 0x060003CC RID: 972 RVA: 0x00012011 File Offset: 0x00011011
		internal void SetItemData(string value)
		{
			this.SetItemData(value, value);
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0001201B File Offset: 0x0001101B
		internal void SetItemData(string value, string originalStringValue)
		{
			this.rawValue = value;
			this.originalStringValue = originalStringValue;
		}

		// Token: 0x040005C4 RID: 1476
		private string localName;

		// Token: 0x040005C5 RID: 1477
		private string namespaceUri;

		// Token: 0x040005C6 RID: 1478
		private string prefix;

		// Token: 0x040005C7 RID: 1479
		private string nameWPrefix;

		// Token: 0x040005C8 RID: 1480
		private string rawValue;

		// Token: 0x040005C9 RID: 1481
		private string originalStringValue;

		// Token: 0x040005CA RID: 1482
		private int depth;

		// Token: 0x040005CB RID: 1483
		private AttributePSVIInfo attributePSVIInfo;

		// Token: 0x040005CC RID: 1484
		private XmlNodeType nodeType;

		// Token: 0x040005CD RID: 1485
		private int lineNo;

		// Token: 0x040005CE RID: 1486
		private int linePos;
	}
}
