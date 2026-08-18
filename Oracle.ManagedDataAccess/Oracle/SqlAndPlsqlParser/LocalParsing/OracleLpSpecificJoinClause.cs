using System;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x020002D5 RID: 725
	internal abstract class OracleLpSpecificJoinClause : OracleLpStatementElement, IOracleLpNamedObjectContainer
	{
		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06001A74 RID: 6772 RVA: 0x0010ADEC File Offset: 0x00108FEC
		internal override OracleLpStatementElementType ElementType
		{
			get
			{
				return OracleLpStatementElementType.JoinClause;
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06001A75 RID: 6773 RVA: 0x0010ADF0 File Offset: 0x00108FF0
		// (set) Token: 0x06001A76 RID: 6774 RVA: 0x0010ADF8 File Offset: 0x00108FF8
		public OracleLpTableReference TableReference
		{
			get
			{
				return this.m_vTableReference;
			}
			set
			{
				this.m_vTableReference = value;
				if (this.m_vTableReference != null)
				{
					this.m_vTableReference.Parent = this;
				}
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06001A77 RID: 6775 RVA: 0x0010AE18 File Offset: 0x00109018
		// (set) Token: 0x06001A78 RID: 6776 RVA: 0x0010AE20 File Offset: 0x00109020
		public OracleLpJoinCondition Condition
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

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06001A79 RID: 6777 RVA: 0x0010AE2C File Offset: 0x0010902C
		public OracleLpJoinClauseType ClauseType
		{
			get
			{
				return this.m_vClauseType;
			}
		}

		// Token: 0x06001A7A RID: 6778 RVA: 0x0010AE34 File Offset: 0x00109034
		public OracleLpSpecificJoinClause(OracleLpFromListTerm ft) : base(ft)
		{
		}

		// Token: 0x06001A7B RID: 6779 RVA: 0x0010AE40 File Offset: 0x00109040
		public virtual void Resolve()
		{
		}

		// Token: 0x06001A7C RID: 6780 RVA: 0x0010AE44 File Offset: 0x00109044
		public void RetrieveNamedObjectReferences(OracleLpStatement statement)
		{
			this.m_vTableReference.RetrieveNamedObjectReferences(statement);
		}

		// Token: 0x04001C9E RID: 7326
		protected OracleLpTableReference m_vTableReference;

		// Token: 0x04001C9F RID: 7327
		protected OracleLpJoinCondition m_vCondition;

		// Token: 0x04001CA0 RID: 7328
		protected OracleLpJoinClauseType m_vClauseType;
	}
}
