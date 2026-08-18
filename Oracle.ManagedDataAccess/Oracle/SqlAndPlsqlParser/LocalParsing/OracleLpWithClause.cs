using System;
using System.Text;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x02000303 RID: 771
	internal class OracleLpWithClause : OracleLpStatementElement, IOracleLpNamedObjectContainer
	{
		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06001B7D RID: 7037 RVA: 0x0010D834 File Offset: 0x0010BA34
		internal override OracleLpStatementElementType ElementType
		{
			get
			{
				return OracleLpStatementElementType.WithClause;
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06001B7E RID: 7038 RVA: 0x0010D838 File Offset: 0x0010BA38
		// (set) Token: 0x06001B7F RID: 7039 RVA: 0x0010D840 File Offset: 0x0010BA40
		public OracleLpPlsqlDeclarations PlsqlDeclarations
		{
			get
			{
				return this.m_vPlsqlDeclarations;
			}
			set
			{
				this.m_vPlsqlDeclarations = value;
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06001B80 RID: 7040 RVA: 0x0010D84C File Offset: 0x0010BA4C
		// (set) Token: 0x06001B81 RID: 7041 RVA: 0x0010D854 File Offset: 0x0010BA54
		public OracleLpSubqueryFactoringClause SubqueryFactoringClause
		{
			get
			{
				return this.m_vSubqueryFactoringClause;
			}
			set
			{
				this.m_vSubqueryFactoringClause = value;
				if (this.m_vSubqueryFactoringClause != null)
				{
					this.m_vSubqueryFactoringClause.Parent = this;
				}
			}
		}

		// Token: 0x06001B82 RID: 7042 RVA: 0x0010D874 File Offset: 0x0010BA74
		public OracleLpWithClause(OracleLpStatementElement parent) : base(parent)
		{
		}

		// Token: 0x06001B83 RID: 7043 RVA: 0x0010D880 File Offset: 0x0010BA80
		public void RetrieveNamedObjectReferences(OracleLpStatement statement)
		{
			if (this.m_vSubqueryFactoringClause != null)
			{
				this.m_vSubqueryFactoringClause.RetrieveNamedObjectReferences(statement);
			}
		}

		// Token: 0x06001B84 RID: 7044 RVA: 0x0010D898 File Offset: 0x0010BA98
		internal override void ToString(StringBuilder sb)
		{
			string depthIndent = base.DepthIndent;
			sb.Append(depthIndent);
			sb.Append("With clause:\n");
			if (this.m_vSubqueryFactoringClause != null)
			{
				this.m_vSubqueryFactoringClause.ToString(sb);
			}
		}

		// Token: 0x04001D57 RID: 7511
		protected OracleLpPlsqlDeclarations m_vPlsqlDeclarations;

		// Token: 0x04001D58 RID: 7512
		protected OracleLpSubqueryFactoringClause m_vSubqueryFactoringClause;
	}
}
