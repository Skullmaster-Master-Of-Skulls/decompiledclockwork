using System;
using System.Collections.Generic;
using System.Text;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x020002E0 RID: 736
	internal class OracleLpQueryBlock : OracleLpStatementDataContainer
	{
		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06001AC9 RID: 6857 RVA: 0x0010BF3C File Offset: 0x0010A13C
		internal override OracleLpStatementElementType ElementType
		{
			get
			{
				return OracleLpStatementElementType.QueryBlock;
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06001ACA RID: 6858 RVA: 0x0010BF40 File Offset: 0x0010A140
		// (set) Token: 0x06001ACB RID: 6859 RVA: 0x0010BF48 File Offset: 0x0010A148
		public OracleLpWithClause WithClause
		{
			get
			{
				return this.m_vWithClause;
			}
			set
			{
				this.m_vWithClause = value;
				if (this.m_vWithClause != null)
				{
					this.m_vWithClause.Parent = this;
				}
			}
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06001ACC RID: 6860 RVA: 0x0010BF68 File Offset: 0x0010A168
		public OracleLpSelectClause SelectClause
		{
			get
			{
				return this.m_vSelectClause;
			}
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06001ACD RID: 6861 RVA: 0x0010BF70 File Offset: 0x0010A170
		// (set) Token: 0x06001ACE RID: 6862 RVA: 0x0010BF78 File Offset: 0x0010A178
		public OracleLpFromClauseBase FromClause
		{
			get
			{
				return this.m_vFromClause;
			}
			set
			{
				this.m_vFromClause = value;
				if (this.m_vFromClause != null)
				{
					this.m_vFromClause.Parent = this;
				}
			}
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06001ACF RID: 6863 RVA: 0x0010BF98 File Offset: 0x0010A198
		public bool IsFromDual
		{
			get
			{
				return this.m_vIsFromDual;
			}
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06001AD0 RID: 6864 RVA: 0x0010BFA0 File Offset: 0x0010A1A0
		public override List<OracleLpColumnDescriptor> ColumnDescriptors
		{
			get
			{
				return this.m_vSelectClause.ColumnDescriptors;
			}
		}

		// Token: 0x06001AD1 RID: 6865 RVA: 0x0010BFB0 File Offset: 0x0010A1B0
		public OracleLpQueryBlock(OracleLpSubquery parentSubquery) : base(parentSubquery)
		{
			this.m_vSelectClause = new OracleLpSelectClause(this);
		}

		// Token: 0x06001AD2 RID: 6866 RVA: 0x0010BFC8 File Offset: 0x0010A1C8
		public override void RetrieveNamedObjectReferences(OracleLpStatement statement)
		{
			if (this.m_vWithClause != null)
			{
				this.m_vWithClause.RetrieveNamedObjectReferences(statement);
			}
			foreach (OracleLpSelectTerm oracleLpSelectTerm in this.m_vSelectClause.SelectList)
			{
				if (oracleLpSelectTerm.Type == OracleLpSelectTermType.SPECIFIC)
				{
					OracleLpExpression expression = ((OracleLpSelectTermSpecific)oracleLpSelectTerm).Expression;
					if (expression.ExpressionType == OracleLpExpressionType.SCALAR_SUBQUERY_EXPRESSION)
					{
						((OracleLpScalarSubqueryExpression)expression).RetrieveNamedObjectReferences(statement);
					}
				}
			}
			foreach (OracleLpStatementDataContainer oracleLpStatementDataContainer in this.m_vFromClause.Terms)
			{
				oracleLpStatementDataContainer.RetrieveNamedObjectReferences(statement);
			}
		}

		// Token: 0x06001AD3 RID: 6867 RVA: 0x0010C0A0 File Offset: 0x0010A2A0
		internal override void ToString(StringBuilder sb)
		{
			if (this.m_vWithClause != null)
			{
				this.m_vWithClause.ToString(sb);
			}
			this.m_vSelectClause.ToString(sb);
			this.m_vFromClause.ToString(sb);
		}

		// Token: 0x04001CD8 RID: 7384
		protected OracleLpWithClause m_vWithClause;

		// Token: 0x04001CD9 RID: 7385
		protected OracleLpSelectClause m_vSelectClause;

		// Token: 0x04001CDA RID: 7386
		protected OracleLpFromClauseBase m_vFromClause;

		// Token: 0x04001CDB RID: 7387
		protected bool m_vIsFromDual;
	}
}
