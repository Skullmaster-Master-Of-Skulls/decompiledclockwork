using System;
using System.Collections.Generic;
using System.Text;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x020002F1 RID: 753
	internal class OracleLpSetExpressionSubquery : OracleLpSubquery
	{
		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06001B10 RID: 6928 RVA: 0x0010CC18 File Offset: 0x0010AE18
		public override OracleLpSubqueryType SubqueryType
		{
			get
			{
				return OracleLpSubqueryType.SetExpression;
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06001B11 RID: 6929 RVA: 0x0010CC1C File Offset: 0x0010AE1C
		// (set) Token: 0x06001B12 RID: 6930 RVA: 0x0010CC24 File Offset: 0x0010AE24
		public OracleLpSubquery LeftSubquery
		{
			get
			{
				return this.m_vLeftSubquery;
			}
			internal set
			{
				this.m_vLeftSubquery = value;
				this.m_vLeftSubquery.Parent = this;
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06001B13 RID: 6931 RVA: 0x0010CC3C File Offset: 0x0010AE3C
		// (set) Token: 0x06001B14 RID: 6932 RVA: 0x0010CC44 File Offset: 0x0010AE44
		public OracleLpSubquery RightSubquery
		{
			get
			{
				return this.m_vRightSubquery;
			}
			internal set
			{
				this.m_vRightSubquery = value;
				this.m_vRightSubquery.Parent = this;
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06001B15 RID: 6933 RVA: 0x0010CC5C File Offset: 0x0010AE5C
		// (set) Token: 0x06001B16 RID: 6934 RVA: 0x0010CC64 File Offset: 0x0010AE64
		public OracleLpSetOperator SetOperator
		{
			get
			{
				return this.m_vSetOperator;
			}
			internal set
			{
				this.m_vSetOperator = value;
			}
		}

		// Token: 0x06001B17 RID: 6935 RVA: 0x0010CC70 File Offset: 0x0010AE70
		public OracleLpSetExpressionSubquery(OracleLpStatementElement parent) : base(parent)
		{
		}

		// Token: 0x06001B18 RID: 6936 RVA: 0x0010CC7C File Offset: 0x0010AE7C
		public override void Resolve()
		{
			List<OracleLpColumnDescriptor> columnDescriptors = this.m_vLeftSubquery.ColumnDescriptors;
			List<OracleLpColumnDescriptor> columnDescriptors2 = this.m_vRightSubquery.ColumnDescriptors;
			this.m_vColumnDescriptors = columnDescriptors;
		}

		// Token: 0x06001B19 RID: 6937 RVA: 0x0010CCA8 File Offset: 0x0010AEA8
		public override void RetrieveNamedObjectReferences(OracleLpStatement statement)
		{
			this.m_vLeftSubquery.RetrieveNamedObjectReferences(statement);
			this.m_vRightSubquery.RetrieveNamedObjectReferences(statement);
		}

		// Token: 0x06001B1A RID: 6938 RVA: 0x0010CCC4 File Offset: 0x0010AEC4
		internal override void ToString(StringBuilder sb)
		{
			string depthIndent = base.DepthIndent;
			base.ToString(sb);
			sb.Append(depthIndent);
			sb.Append("Left subquery:\n");
			this.m_vLeftSubquery.ToString(sb);
			sb.Append(depthIndent);
			sb.Append("Set operator: ");
			sb.Append(this.SetOperator.ToString());
			sb.Append('\n');
			sb.Append(depthIndent);
			sb.Append("Right subquery:\n");
			this.m_vRightSubquery.ToString(sb);
			sb.Append('\n');
		}

		// Token: 0x04001D2B RID: 7467
		private OracleLpSubquery m_vLeftSubquery;

		// Token: 0x04001D2C RID: 7468
		private OracleLpSubquery m_vRightSubquery;

		// Token: 0x04001D2D RID: 7469
		private OracleLpSetOperator m_vSetOperator;
	}
}
