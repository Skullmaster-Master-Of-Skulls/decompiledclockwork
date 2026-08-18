using System;
using System.Collections.Generic;

namespace OracleInternal.ConnectionPool
{
	// Token: 0x02000040 RID: 64
	internal class ComparisonInfo : IComparer<ComparisonInfo>
	{
		// Token: 0x060002F6 RID: 758 RVA: 0x00013050 File Offset: 0x00011250
		public ComparisonInfo(ComparisonType compType, int oriStartPos, int newStartPos, int length)
		{
			this.m_compType = compType;
			this.m_oriStartPos = oriStartPos;
			this.m_newStartPos = newStartPos;
			this.m_length = length;
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x00013078 File Offset: 0x00011278
		public int Compare(ComparisonInfo a, ComparisonInfo b)
		{
			if (a.m_oriStartPos == b.m_oriStartPos)
			{
				return 0;
			}
			if (a.m_oriStartPos < b.m_oriStartPos)
			{
				return -1;
			}
			return 1;
		}

		// Token: 0x04000439 RID: 1081
		public ComparisonType m_compType;

		// Token: 0x0400043A RID: 1082
		public int m_oriStartPos;

		// Token: 0x0400043B RID: 1083
		public int m_newStartPos;

		// Token: 0x0400043C RID: 1084
		public int m_length;
	}
}
