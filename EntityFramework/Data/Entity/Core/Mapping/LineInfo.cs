using System;
using System.Xml;
using System.Xml.XPath;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x020003C0 RID: 960
	internal sealed class LineInfo : IXmlLineInfo
	{
		// Token: 0x06002310 RID: 8976 RVA: 0x000A4258 File Offset: 0x000A2458
		internal LineInfo(XPathNavigator nav) : this((IXmlLineInfo)nav)
		{
		}

		// Token: 0x06002311 RID: 8977 RVA: 0x000A4266 File Offset: 0x000A2466
		internal LineInfo(IXmlLineInfo lineInfo)
		{
			this.m_hasLineInfo = lineInfo.HasLineInfo();
			this.m_lineNumber = lineInfo.LineNumber;
			this.m_linePosition = lineInfo.LinePosition;
		}

		// Token: 0x06002312 RID: 8978 RVA: 0x000A4292 File Offset: 0x000A2492
		private LineInfo()
		{
			this.m_hasLineInfo = false;
			this.m_lineNumber = 0;
			this.m_linePosition = 0;
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06002313 RID: 8979 RVA: 0x000A42AF File Offset: 0x000A24AF
		public int LineNumber
		{
			get
			{
				return this.m_lineNumber;
			}
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06002314 RID: 8980 RVA: 0x000A42B7 File Offset: 0x000A24B7
		public int LinePosition
		{
			get
			{
				return this.m_linePosition;
			}
		}

		// Token: 0x06002315 RID: 8981 RVA: 0x000A42BF File Offset: 0x000A24BF
		public bool HasLineInfo()
		{
			return this.m_hasLineInfo;
		}

		// Token: 0x04000C57 RID: 3159
		private readonly bool m_hasLineInfo;

		// Token: 0x04000C58 RID: 3160
		private readonly int m_lineNumber;

		// Token: 0x04000C59 RID: 3161
		private readonly int m_linePosition;

		// Token: 0x04000C5A RID: 3162
		internal static readonly LineInfo Empty = new LineInfo();
	}
}
