using System;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x020002C0 RID: 704
	internal abstract class OracleLpCompoundExpression : OracleLpExpression
	{
		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06001A21 RID: 6689 RVA: 0x0010A244 File Offset: 0x00108444
		// (set) Token: 0x06001A22 RID: 6690 RVA: 0x0010A24C File Offset: 0x0010844C
		public OracleLpCompoundExpressionType CompoundType
		{
			get
			{
				return this.m_vCompoundType;
			}
			set
			{
				this.m_vCompoundType = value;
			}
		}

		// Token: 0x06001A23 RID: 6691 RVA: 0x0010A258 File Offset: 0x00108458
		public OracleLpCompoundExpression(OracleLpStatementElement parent) : base(parent)
		{
			this.m_vExpressionType = OracleLpExpressionType.COMPOUND_EXPRESSION;
		}

		// Token: 0x04001C6C RID: 7276
		protected OracleLpCompoundExpressionType m_vCompoundType;
	}
}
