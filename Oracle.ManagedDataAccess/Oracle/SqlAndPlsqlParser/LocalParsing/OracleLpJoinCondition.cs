using System;
using System.Collections.Generic;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x020002D4 RID: 724
	internal class OracleLpJoinCondition
	{
		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06001A6E RID: 6766 RVA: 0x0010AD74 File Offset: 0x00108F74
		// (set) Token: 0x06001A6F RID: 6767 RVA: 0x0010AD7C File Offset: 0x00108F7C
		public OracleLpJoinConditionType Type
		{
			get
			{
				return this.m_vType;
			}
			set
			{
				switch (value)
				{
				case OracleLpJoinConditionType.On:
					this.m_vColumns = null;
					return;
				case OracleLpJoinConditionType.Using:
					this.m_vCondition = null;
					this.m_vColumns = new List<OracleLpColumn>();
					return;
				default:
					this.m_vCondition = null;
					this.m_vColumns = null;
					return;
				}
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06001A70 RID: 6768 RVA: 0x0010ADC8 File Offset: 0x00108FC8
		// (set) Token: 0x06001A71 RID: 6769 RVA: 0x0010ADD0 File Offset: 0x00108FD0
		public OracleLpCondition Condition
		{
			get
			{
				return this.m_vCondition;
			}
			set
			{
				this.m_vCondition = value;
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06001A72 RID: 6770 RVA: 0x0010ADDC File Offset: 0x00108FDC
		public List<OracleLpColumn> Columns
		{
			get
			{
				return this.m_vColumns;
			}
		}

		// Token: 0x04001C9B RID: 7323
		protected OracleLpJoinConditionType m_vType;

		// Token: 0x04001C9C RID: 7324
		protected OracleLpCondition m_vCondition;

		// Token: 0x04001C9D RID: 7325
		protected List<OracleLpColumn> m_vColumns;
	}
}
