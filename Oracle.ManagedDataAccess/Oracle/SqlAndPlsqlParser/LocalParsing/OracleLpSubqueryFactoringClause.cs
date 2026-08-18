using System;
using System.Collections.Generic;
using System.Text;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x02000300 RID: 768
	internal class OracleLpSubqueryFactoringClause : OracleLpStatementElement, IOracleLpNamedObjectContainer
	{
		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06001B68 RID: 7016 RVA: 0x0010D520 File Offset: 0x0010B720
		internal override OracleLpStatementElementType ElementType
		{
			get
			{
				return OracleLpStatementElementType.SubqueryFactoringClause;
			}
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06001B69 RID: 7017 RVA: 0x0010D524 File Offset: 0x0010B724
		public List<OracleLpSubqueryFactoringTerm> SubqueryFactoringTerms
		{
			get
			{
				return this.m_vSubqueryFactoringTerms;
			}
		}

		// Token: 0x06001B6A RID: 7018 RVA: 0x0010D52C File Offset: 0x0010B72C
		public OracleLpSubqueryFactoringClause(OracleLpStatementElement parent) : base(parent)
		{
		}

		// Token: 0x06001B6B RID: 7019 RVA: 0x0010D538 File Offset: 0x0010B738
		public void AddSubqueryFactoringTerm(OracleLpSubqueryFactoringTerm term)
		{
			if (this.m_vSubqueryFactoringTerms == null)
			{
				this.m_vSubqueryFactoringTerms = new List<OracleLpSubqueryFactoringTerm>(1);
			}
			this.m_vSubqueryFactoringTerms.Add(term);
			term.Parent = this;
		}

		// Token: 0x06001B6C RID: 7020 RVA: 0x0010D564 File Offset: 0x0010B764
		public void RetrieveNamedObjectReferences(OracleLpStatement statement)
		{
			if (this.m_vSubqueryFactoringTerms != null)
			{
				foreach (OracleLpSubqueryFactoringTerm oracleLpSubqueryFactoringTerm in this.m_vSubqueryFactoringTerms)
				{
					oracleLpSubqueryFactoringTerm.RetrieveNamedObjectReferences(statement);
				}
			}
		}

		// Token: 0x06001B6D RID: 7021 RVA: 0x0010D5C0 File Offset: 0x0010B7C0
		internal override void ToString(StringBuilder sb)
		{
			string depthIndent = base.DepthIndent;
			sb.Append(depthIndent);
			sb.Append("Subquery factoring clause:\n");
			if (this.m_vSubqueryFactoringTerms != null)
			{
				foreach (OracleLpSubqueryFactoringTerm oracleLpSubqueryFactoringTerm in this.m_vSubqueryFactoringTerms)
				{
					oracleLpSubqueryFactoringTerm.ToString(sb);
				}
			}
		}

		// Token: 0x04001D50 RID: 7504
		protected List<OracleLpSubqueryFactoringTerm> m_vSubqueryFactoringTerms;
	}
}
