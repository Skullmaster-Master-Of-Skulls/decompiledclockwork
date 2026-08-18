using System;
using System.Text;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x020002F7 RID: 759
	internal class OracleLpQteSubquery : OracleLpQueryTableExpression
	{
		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06001B34 RID: 6964 RVA: 0x0010D184 File Offset: 0x0010B384
		public override OracleLpQueryTableExpressionType QueryTableExpressionType
		{
			get
			{
				return OracleLpQueryTableExpressionType.Subquery;
			}
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06001B35 RID: 6965 RVA: 0x0010D188 File Offset: 0x0010B388
		// (set) Token: 0x06001B36 RID: 6966 RVA: 0x0010D190 File Offset: 0x0010B390
		public OracleLpSubquery Subquery
		{
			get
			{
				return this.m_vSubquery;
			}
			set
			{
				this.m_vSubquery = value;
				if (this.m_vSubquery != null)
				{
					this.m_vSubquery.Parent = this;
				}
			}
		}

		// Token: 0x06001B37 RID: 6967 RVA: 0x0010D1B0 File Offset: 0x0010B3B0
		public OracleLpQteSubquery(OracleLpStatementElement tr) : base(tr)
		{
		}

		// Token: 0x06001B38 RID: 6968 RVA: 0x0010D1BC File Offset: 0x0010B3BC
		public override void Resolve()
		{
			this.m_vColumnDescriptors = this.m_vSubquery.ColumnDescriptors;
		}

		// Token: 0x06001B39 RID: 6969 RVA: 0x0010D1D0 File Offset: 0x0010B3D0
		public override void RetrieveNamedObjectReferences(OracleLpStatement statement)
		{
			this.m_vSubquery.RetrieveNamedObjectReferences(statement);
		}

		// Token: 0x06001B3A RID: 6970 RVA: 0x0010D1E0 File Offset: 0x0010B3E0
		internal override void ToString(StringBuilder sb)
		{
			base.ToString(sb);
			this.m_vSubquery.ToString(sb);
		}

		// Token: 0x04001D3E RID: 7486
		protected OracleLpSubquery m_vSubquery;
	}
}
