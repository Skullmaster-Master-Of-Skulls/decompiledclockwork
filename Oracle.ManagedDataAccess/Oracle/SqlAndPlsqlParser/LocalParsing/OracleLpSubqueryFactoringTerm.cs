using System;
using System.Collections.Generic;
using System.Text;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x02000302 RID: 770
	internal class OracleLpSubqueryFactoringTerm : OracleLpStatementDataContainer
	{
		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06001B73 RID: 7027 RVA: 0x0010D684 File Offset: 0x0010B884
		internal override OracleLpStatementElementType ElementType
		{
			get
			{
				return OracleLpStatementElementType.SubqueryFactoringTerm;
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06001B74 RID: 7028 RVA: 0x0010D688 File Offset: 0x0010B888
		// (set) Token: 0x06001B75 RID: 7029 RVA: 0x0010D690 File Offset: 0x0010B890
		public OracleLpColumnMappedQueryName ColumnMappedQueryName
		{
			get
			{
				return this.m_vColumnMappedQueryName;
			}
			set
			{
				this.m_vColumnMappedQueryName = value;
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06001B76 RID: 7030 RVA: 0x0010D69C File Offset: 0x0010B89C
		// (set) Token: 0x06001B77 RID: 7031 RVA: 0x0010D6A4 File Offset: 0x0010B8A4
		internal OracleLpSubquery Subquery
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

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06001B78 RID: 7032 RVA: 0x0010D6C4 File Offset: 0x0010B8C4
		public override List<OracleLpColumnDescriptor> ColumnDescriptors
		{
			get
			{
				if (this.m_vColumnDescriptors == null)
				{
					this.Resolve();
				}
				return this.m_vColumnDescriptors;
			}
		}

		// Token: 0x06001B79 RID: 7033 RVA: 0x0010D6DC File Offset: 0x0010B8DC
		public OracleLpSubqueryFactoringTerm(OracleLpStatementElement parent) : base(parent)
		{
		}

		// Token: 0x06001B7A RID: 7034 RVA: 0x0010D6E8 File Offset: 0x0010B8E8
		public override void Resolve()
		{
			this.m_vColumnDescriptors = this.m_vSubquery.ColumnDescriptors;
			if (this.m_vColumnMappedQueryName != null && this.m_vColumnMappedQueryName.ColumnAliases != null)
			{
				for (int i = 0; i < this.m_vColumnDescriptors.Count; i++)
				{
					OracleLpColumnDescriptor oracleLpColumnDescriptor = this.m_vColumnDescriptors[i];
					oracleLpColumnDescriptor.ColumnName = this.m_vColumnMappedQueryName.ColumnAliases[i];
					oracleLpColumnDescriptor.IsAliased = true;
				}
			}
		}

		// Token: 0x06001B7B RID: 7035 RVA: 0x0010D75C File Offset: 0x0010B95C
		public override void RetrieveNamedObjectReferences(OracleLpStatement statement)
		{
			if (!this.m_vNamedObjectReferencesRetrieved)
			{
				this.m_vNamedObjectReferencesRetrieved = true;
				this.m_vSubquery.RetrieveNamedObjectReferences(statement);
			}
		}

		// Token: 0x06001B7C RID: 7036 RVA: 0x0010D77C File Offset: 0x0010B97C
		internal override void ToString(StringBuilder sb)
		{
			string depthIndent = base.DepthIndent;
			sb.Append(depthIndent);
			sb.Append("Term: ");
			sb.Append(this.m_vColumnMappedQueryName.Name.DbName);
			if (this.m_vColumnMappedQueryName.ColumnAliases != null)
			{
				foreach (OracleLpName oracleLpName in this.m_vColumnMappedQueryName.ColumnAliases)
				{
					sb.Append(" ,col: ");
					sb.Append(oracleLpName.DbName);
				}
			}
			sb.Append("\n");
		}

		// Token: 0x04001D53 RID: 7507
		protected bool m_vNamedObjectReferencesRetrieved;

		// Token: 0x04001D54 RID: 7508
		protected OracleLpColumnMappedQueryName m_vColumnMappedQueryName;

		// Token: 0x04001D55 RID: 7509
		private OracleLpSubquery m_vSubquery;

		// Token: 0x04001D56 RID: 7510
		protected List<OracleLpColumnDescriptor> m_vColumnDescriptors;
	}
}
