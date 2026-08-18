using System;

namespace System.Data.Mapping.ViewGeneration.Structures
{
	// Token: 0x020002A4 RID: 676
	internal class CellLabel
	{
		// Token: 0x06002834 RID: 10292 RVA: 0x0009BC66 File Offset: 0x00099E66
		internal CellLabel(CellLabel source)
		{
			this.m_startLineNumber = source.m_startLineNumber;
			this.m_startLinePosition = source.m_startLinePosition;
			this.m_sourceLocation = source.m_sourceLocation;
		}

		// Token: 0x06002835 RID: 10293 RVA: 0x0009BC92 File Offset: 0x00099E92
		internal CellLabel(StorageMappingFragment fragmentInfo) : this(fragmentInfo.StartLineNumber, fragmentInfo.StartLinePosition, fragmentInfo.SourceLocation)
		{
		}

		// Token: 0x06002836 RID: 10294 RVA: 0x0009BCAC File Offset: 0x00099EAC
		internal CellLabel(int startLineNumber, int startLinePosition, string sourceLocation)
		{
			this.m_startLineNumber = startLineNumber;
			this.m_startLinePosition = startLinePosition;
			this.m_sourceLocation = sourceLocation;
		}

		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x06002837 RID: 10295 RVA: 0x0009BCC9 File Offset: 0x00099EC9
		internal int StartLineNumber
		{
			get
			{
				return this.m_startLineNumber;
			}
		}

		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x06002838 RID: 10296 RVA: 0x0009BCD1 File Offset: 0x00099ED1
		internal int StartLinePosition
		{
			get
			{
				return this.m_startLinePosition;
			}
		}

		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x06002839 RID: 10297 RVA: 0x0009BCD9 File Offset: 0x00099ED9
		internal string SourceLocation
		{
			get
			{
				return this.m_sourceLocation;
			}
		}

		// Token: 0x04001245 RID: 4677
		private int m_startLineNumber;

		// Token: 0x04001246 RID: 4678
		private int m_startLinePosition;

		// Token: 0x04001247 RID: 4679
		private string m_sourceLocation;
	}
}
