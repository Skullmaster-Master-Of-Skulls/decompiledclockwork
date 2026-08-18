using System;
using System.Text;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x020002F2 RID: 754
	internal class OracleLpCompoundSubquery : OracleLpSubquery
	{
		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06001B1B RID: 6939 RVA: 0x0010CD5C File Offset: 0x0010AF5C
		public override OracleLpSubqueryType SubqueryType
		{
			get
			{
				return OracleLpSubqueryType.Compound;
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06001B1C RID: 6940 RVA: 0x0010CD60 File Offset: 0x0010AF60
		// (set) Token: 0x06001B1D RID: 6941 RVA: 0x0010CD68 File Offset: 0x0010AF68
		public OracleLpSubquery Subquery
		{
			get
			{
				return this.m_vSubquery;
			}
			internal set
			{
				this.m_vSubquery = value;
				this.m_vSubquery.Parent = this;
			}
		}

		// Token: 0x06001B1E RID: 6942 RVA: 0x0010CD80 File Offset: 0x0010AF80
		public OracleLpCompoundSubquery(OracleLpStatementElement parent) : base(parent)
		{
		}

		// Token: 0x06001B1F RID: 6943 RVA: 0x0010CD8C File Offset: 0x0010AF8C
		public override void Resolve()
		{
			this.m_vColumnDescriptors = this.m_vSubquery.ColumnDescriptors;
		}

		// Token: 0x06001B20 RID: 6944 RVA: 0x0010CDA0 File Offset: 0x0010AFA0
		public override void RetrieveNamedObjectReferences(OracleLpStatement statement)
		{
			this.m_vSubquery.RetrieveNamedObjectReferences(statement);
		}

		// Token: 0x06001B21 RID: 6945 RVA: 0x0010CDB0 File Offset: 0x0010AFB0
		internal override void ToString(StringBuilder sb)
		{
			string depthIndent = base.DepthIndent;
			base.ToString(sb);
			sb.Append(depthIndent);
			sb.Append("Compound subquery:\n");
			this.m_vSubquery.ToString(sb);
			sb.Append('\n');
		}

		// Token: 0x04001D2E RID: 7470
		private OracleLpSubquery m_vSubquery;
	}
}
