using System;
using System.Collections.Generic;
using System.Text;
using OracleInternal.Common;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x020002F6 RID: 758
	internal class OracleLpQteNamedObject : OracleLpQueryTableExpression
	{
		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06001B27 RID: 6951 RVA: 0x0010CE4C File Offset: 0x0010B04C
		public object SubqueryFactoringTerm
		{
			get
			{
				return this.m_vSubqueryFactoringTerm;
			}
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06001B28 RID: 6952 RVA: 0x0010CE54 File Offset: 0x0010B054
		public override OracleLpQueryTableExpressionType QueryTableExpressionType
		{
			get
			{
				return OracleLpQueryTableExpressionType.NamedObject;
			}
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06001B29 RID: 6953 RVA: 0x0010CE58 File Offset: 0x0010B058
		// (set) Token: 0x06001B2A RID: 6954 RVA: 0x0010CE60 File Offset: 0x0010B060
		public virtual OracleLpName ObjectName
		{
			get
			{
				return this.m_vObjectName;
			}
			set
			{
				this.m_vObjectName = value;
			}
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06001B2B RID: 6955 RVA: 0x0010CE6C File Offset: 0x0010B06C
		// (set) Token: 0x06001B2C RID: 6956 RVA: 0x0010CE74 File Offset: 0x0010B074
		public OracleLpName SchemaName
		{
			get
			{
				return this.m_vSchemaName;
			}
			set
			{
				this.m_vSchemaName = value;
			}
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06001B2D RID: 6957 RVA: 0x0010CE80 File Offset: 0x0010B080
		// (set) Token: 0x06001B2E RID: 6958 RVA: 0x0010CE88 File Offset: 0x0010B088
		public OracleLpDbLink Dblink
		{
			get
			{
				return this.m_vDblink;
			}
			set
			{
				this.m_vDblink = value;
			}
		}

		// Token: 0x06001B2F RID: 6959 RVA: 0x0010CE94 File Offset: 0x0010B094
		public OracleLpQteNamedObject(OracleLpStatementElement tr) : base(tr)
		{
		}

		// Token: 0x06001B30 RID: 6960 RVA: 0x0010CEA0 File Offset: 0x0010B0A0
		public override void Resolve()
		{
			if (this.m_vSubqueryFactoringTerm != null)
			{
				this.m_vColumnDescriptors = this.m_vSubqueryFactoringTerm.ColumnDescriptors;
				return;
			}
			this.m_vColumnDescriptors = new List<OracleLpColumnDescriptor>();
			for (int i = 1; i <= 5; i++)
			{
				OracleLpColumnDescriptor oracleLpColumnDescriptor = new OracleLpColumnDescriptor();
				oracleLpColumnDescriptor.ColumnName = new OracleLpName(string.Format("{0}_col{1}", this.m_vObjectName.DbName, i));
				oracleLpColumnDescriptor.BaseTableName = this.m_vObjectName;
				oracleLpColumnDescriptor.BaseSchemaName = this.m_vSchemaName;
				oracleLpColumnDescriptor.BaseColumnName = oracleLpColumnDescriptor.ColumnName;
				this.m_vColumnDescriptors.Add(oracleLpColumnDescriptor);
			}
		}

		// Token: 0x06001B31 RID: 6961 RVA: 0x0010CF3C File Offset: 0x0010B13C
		public override void RetrieveNamedObjectReferences(OracleLpStatement statement)
		{
			if (this.m_vSchemaName == null && this.m_vDblink == null)
			{
				for (OracleLpQueryBlock parentQueryBlock = base.GetParentQueryBlock(); parentQueryBlock != null; parentQueryBlock = parentQueryBlock.GetParentQueryBlock())
				{
					if (parentQueryBlock.WithClause != null && parentQueryBlock.WithClause.SubqueryFactoringClause != null)
					{
						List<OracleLpSubqueryFactoringTerm> subqueryFactoringTerms = parentQueryBlock.WithClause.SubqueryFactoringClause.SubqueryFactoringTerms;
						if (subqueryFactoringTerms != null)
						{
							foreach (OracleLpSubqueryFactoringTerm oracleLpSubqueryFactoringTerm in subqueryFactoringTerms)
							{
								if (oracleLpSubqueryFactoringTerm.ColumnMappedQueryName.Name.DbName == this.m_vObjectName.DbName)
								{
									this.m_vSubqueryFactoringTerm = oracleLpSubqueryFactoringTerm;
									break;
								}
							}
						}
					}
					if (this.m_vSubqueryFactoringTerm != null)
					{
						break;
					}
				}
			}
			if (this.m_vSubqueryFactoringTerm != null)
			{
				this.m_vSubqueryFactoringTerm.RetrieveNamedObjectReferences(statement);
				return;
			}
			statement.NamedObjectsReferences.Add(this);
		}

		// Token: 0x06001B32 RID: 6962 RVA: 0x0010D030 File Offset: 0x0010B230
		public void RetrieveColumnsInformation(OracleLpTableColumns tabCols)
		{
			this.m_vColumnDescriptors = new List<OracleLpColumnDescriptor>();
			foreach (OracleLpColumn oracleLpColumn in tabCols.m_columns)
			{
				OracleLpColumnDescriptor oracleLpColumnDescriptor = new OracleLpColumnDescriptor();
				oracleLpColumnDescriptor.BaseSchemaName = new OracleLpName();
				oracleLpColumnDescriptor.BaseSchemaName.DbName = oracleLpColumn.m_schemaName;
				oracleLpColumnDescriptor.BaseTableName = new OracleLpName();
				oracleLpColumnDescriptor.BaseTableName.DbName = oracleLpColumn.m_tableName;
				oracleLpColumnDescriptor.ColumnName = (oracleLpColumnDescriptor.BaseColumnName = new OracleLpName
				{
					DbName = oracleLpColumn.m_columnName
				});
				oracleLpColumnDescriptor.ColumnType = OracleLpColumnType.Column;
				oracleLpColumnDescriptor.IsHidden = oracleLpColumn.m_isHidden;
				this.m_vColumnDescriptors.Add(oracleLpColumnDescriptor);
			}
		}

		// Token: 0x06001B33 RID: 6963 RVA: 0x0010D108 File Offset: 0x0010B308
		internal override void ToString(StringBuilder sb)
		{
			string depthIndent = base.DepthIndent;
			base.ToString(sb);
			sb.Append(depthIndent);
			sb.Append("  Name: ");
			sb.Append(this.m_vObjectName);
			sb.Append("  Schema: ");
			sb.Append((this.m_vSchemaName == null) ? "none" : (this.m_vSchemaName.DbName ?? "none"));
			sb.Append('\n');
		}

		// Token: 0x04001D3A RID: 7482
		protected OracleLpSubqueryFactoringTerm m_vSubqueryFactoringTerm;

		// Token: 0x04001D3B RID: 7483
		protected OracleLpName m_vObjectName;

		// Token: 0x04001D3C RID: 7484
		protected OracleLpName m_vSchemaName;

		// Token: 0x04001D3D RID: 7485
		protected OracleLpDbLink m_vDblink;
	}
}
