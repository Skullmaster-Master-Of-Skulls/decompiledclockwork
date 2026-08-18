using System;
using System.Collections.Generic;
using System.Text;

namespace Oracle.SqlAndPlsqlParser.LocalParsing.Ansi
{
	// Token: 0x020001EA RID: 490
	internal class OracleLpAnsiCrossOuterApplyClauseCollectionExpression : OracleLpAnsiCrossOuterApplyClause
	{
		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06001208 RID: 4616 RVA: 0x000C48E8 File Offset: 0x000C2AE8
		// (set) Token: 0x06001209 RID: 4617 RVA: 0x000C48F0 File Offset: 0x000C2AF0
		public OracleLpCollectionExpression CollectionExpression
		{
			get
			{
				return this.m_vCollectionExpression;
			}
			set
			{
				this.m_vCollectionExpression = value;
				if (this.m_vCollectionExpression != null)
				{
					this.m_vCollectionExpression.Parent = this;
				}
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x0600120A RID: 4618 RVA: 0x000C4910 File Offset: 0x000C2B10
		internal override List<OracleLpTablePrimary> TablePrimaryList
		{
			get
			{
				if (this.m_vTablePrimaryList == null)
				{
					this.m_vTablePrimaryList = new List<OracleLpTablePrimary>();
				}
				return this.m_vTablePrimaryList;
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x0600120B RID: 4619 RVA: 0x000C492C File Offset: 0x000C2B2C
		public override List<OracleLpColumnDescriptor> ColumnDescriptors
		{
			get
			{
				if (this.m_vColumnDescriptors == null)
				{
					this.m_vColumnDescriptors = new List<OracleLpColumnDescriptor>();
				}
				return this.m_vColumnDescriptors;
			}
		}

		// Token: 0x0600120C RID: 4620 RVA: 0x000C4948 File Offset: 0x000C2B48
		public OracleLpAnsiCrossOuterApplyClauseCollectionExpression(OracleLpFromListTerm ft) : base(ft)
		{
		}

		// Token: 0x0600120D RID: 4621 RVA: 0x000C4954 File Offset: 0x000C2B54
		internal override void ToString(StringBuilder sb)
		{
			string depthIndent = base.DepthIndent;
			sb.Append(depthIndent);
			sb.Append("JoinType:  ");
			sb.Append(this.ClauseType);
			sb.Append("  CrossOuterApplyJoinType:  ");
			sb.Append(this.m_vType);
			sb.Append('\n');
			this.m_vCollectionExpression.ToString(sb);
		}

		// Token: 0x04001429 RID: 5161
		protected OracleLpCollectionExpression m_vCollectionExpression;

		// Token: 0x0400142A RID: 5162
		protected List<OracleLpColumnDescriptor> m_vColumnDescriptors;
	}
}
