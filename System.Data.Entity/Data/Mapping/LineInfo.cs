using System;
using System.Xml;
using System.Xml.XPath;

namespace System.Data.Mapping
{
	// Token: 0x0200022A RID: 554
	internal sealed class LineInfo : IXmlLineInfo
	{
		// Token: 0x060023C4 RID: 9156 RVA: 0x0008157B File Offset: 0x0007F77B
		internal LineInfo(XPathNavigator nav) : this((IXmlLineInfo)nav)
		{
		}

		// Token: 0x060023C5 RID: 9157 RVA: 0x00081589 File Offset: 0x0007F789
		internal LineInfo(IXmlLineInfo lineInfo)
		{
			this.m_hasLineInfo = lineInfo.HasLineInfo();
			this.m_lineNumber = lineInfo.LineNumber;
			this.m_linePosition = lineInfo.LinePosition;
		}

		// Token: 0x060023C6 RID: 9158 RVA: 0x000815B5 File Offset: 0x0007F7B5
		private LineInfo()
		{
			this.m_hasLineInfo = false;
			this.m_lineNumber = 0;
			this.m_linePosition = 0;
		}

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x060023C7 RID: 9159 RVA: 0x000815D2 File Offset: 0x0007F7D2
		public int LineNumber
		{
			get
			{
				return this.m_lineNumber;
			}
		}

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x060023C8 RID: 9160 RVA: 0x000815DA File Offset: 0x0007F7DA
		public int LinePosition
		{
			get
			{
				return this.m_linePosition;
			}
		}

		// Token: 0x060023C9 RID: 9161 RVA: 0x000815E2 File Offset: 0x0007F7E2
		public bool HasLineInfo()
		{
			return this.m_hasLineInfo;
		}

		// Token: 0x04000FDF RID: 4063
		private readonly bool m_hasLineInfo;

		// Token: 0x04000FE0 RID: 4064
		private readonly int m_lineNumber;

		// Token: 0x04000FE1 RID: 4065
		private readonly int m_linePosition;

		// Token: 0x04000FE2 RID: 4066
		internal static readonly LineInfo Empty = new LineInfo();
	}
}
