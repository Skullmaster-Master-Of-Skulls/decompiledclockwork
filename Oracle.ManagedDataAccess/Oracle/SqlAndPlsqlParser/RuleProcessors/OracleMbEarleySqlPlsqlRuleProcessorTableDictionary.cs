using System;
using Oracle.SqlAndPlsqlParser.RuleProcessors.ODPCommands;

namespace Oracle.SqlAndPlsqlParser.RuleProcessors
{
	// Token: 0x02000320 RID: 800
	internal class OracleMbEarleySqlPlsqlRuleProcessorTableDictionary : OracleMbRuleProcessorTableDictionary<OracleMbEarleyRuleMultiProcessorTable>
	{
		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x06001D32 RID: 7474 RVA: 0x0011ECCC File Offset: 0x0011CECC
		public OracleSqlEarleyParserGrammarDefinition Grammar
		{
			get
			{
				return this.m_vGrammar;
			}
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06001D33 RID: 7475 RVA: 0x0011ECD4 File Offset: 0x0011CED4
		public static OracleMbEarleySqlPlsqlRuleProcessorTableDictionary Instance
		{
			get
			{
				lock (OracleMbEarleySqlPlsqlRuleProcessorTableDictionary.m_vObjectLock)
				{
					if (OracleMbEarleySqlPlsqlRuleProcessorTableDictionary.s_vInstance == null)
					{
						OracleMbEarleySqlPlsqlRuleProcessorTableDictionary.s_vInstance = new OracleMbEarleySqlPlsqlRuleProcessorTableDictionary(OracleSqlEarleyParserGrammarDefinition.Instance);
					}
				}
				return OracleMbEarleySqlPlsqlRuleProcessorTableDictionary.s_vInstance;
			}
		}

		// Token: 0x06001D34 RID: 7476 RVA: 0x0011ED28 File Offset: 0x0011CF28
		protected OracleMbEarleySqlPlsqlRuleProcessorTableDictionary(OracleSqlEarleyParserGrammarDefinition grammar)
		{
			this.m_vGrammar = grammar;
			this.Initialize(grammar);
		}

		// Token: 0x06001D35 RID: 7477 RVA: 0x0011ED40 File Offset: 0x0011CF40
		protected virtual void Initialize(OracleSqlEarleyParserGrammarDefinition grammar)
		{
			this.m_vRuleProcessorTableDictionary.Add("empty", new OracleMbEarleyRuleMultiProcessorTable(grammar, OracleMbEarleyEmptyRuleMultiProcessors.s_vRuleProcessorItems));
			this.m_vRuleProcessorTableDictionary.Add("ODPCommands", new OracleMbEarleyRuleMultiProcessorTable(grammar, OracleMbODPCommandsRuleMultiProcessors.s_vRuleProcessorItems));
			this.m_vRuleProcessorTableDictionary.Add("unlabeled_nonblock_stmt", new OracleMbEarleyRuleMultiProcessorTable(grammar, OracleMbEarleyUnlabeledNonblockStmtRuleMultiProcessors.s_vRuleProcessorItems));
			this.m_vRuleProcessorTableDictionary.Add("labeled_block_stmt", new OracleMbEarleyRuleMultiProcessorTable(grammar, OracleMbEarleyLabeledBlockStmtRuleMultiProcessors.s_vRuleProcessorItems));
			this.m_vRuleProcessorTableDictionary.Add("block_stmt", new OracleMbEarleyRuleMultiProcessorTable(grammar, OracleMbEarleyBlockStmtRuleMultiProcessors.s_vRuleProcessorItems));
			this.m_vRuleProcessorTableDictionary.Add("sql_stmt", new OracleMbEarleyRuleMultiProcessorTable(grammar, OracleMbEarleySqlStmtRuleMultiProcessors.s_vRuleProcessorItems));
			this.m_vRuleProcessorTableDictionary.Add("sql_query_or_dml_stmt", new OracleMbEarleyRuleMultiProcessorTable(grammar, OracleMbEarleySqlQueryOrDmlStmtRuleMultiProcessors.s_vRuleProcessorItems));
			this.m_vRuleProcessorTableDictionary.Add("aliased_dml_table_expression_clause", new OracleMbEarleyRuleMultiProcessorTable(grammar, OracleMbEarleyAliasedDmlTableExpressionClauseRuleMultiProcessors.s_vRuleProcessorItems));
			this.m_vRuleProcessorTableDictionary.Add("where_clause", new OracleMbEarleyRuleMultiProcessorTable(grammar, OracleMbEarleyWhereClauseRuleMultiProcessors.s_vRuleProcessorItems));
			this.m_vRuleProcessorTableDictionary.Add("returning_clause", new OracleMbEarleyRuleMultiProcessorTable(grammar, OracleMbEarleyReturningClauseRuleMultiProcessors.s_vRuleProcessorItems));
			this.m_vRuleProcessorTableDictionary.Add("values_clause", new OracleMbEarleyRuleMultiProcessorTable(grammar, OracleMbEarleyValuesClauseRuleMultiProcessors.s_vRuleProcessorItems));
			this.m_vRuleProcessorTableDictionary.Add("condition", new OracleMbEarleyRuleMultiProcessorTable(grammar, OracleMbEarleyConditionRuleMultiProcessors.s_vRuleProcessorItems));
			this.m_vRuleProcessorTableDictionary.Add("expr", new OracleMbEarleyRuleMultiProcessorTable(grammar, OracleMbEarleyExprRuleMultiProcessors.s_vRuleProcessorItems));
			this.m_vRuleProcessorTableDictionary.Add("bind_var", new OracleMbEarleyRuleMultiProcessorTable(grammar, OracleMbEarleyBindVarRuleMultiProcessors.s_vRuleProcessorItems));
			this.m_vRuleProcessorTableDictionary.Add("simple_expression", new OracleMbEarleyRuleMultiProcessorTable(grammar, OracleMbEarleySimpleExpressionRuleMultiProcessors.s_vRuleProcessorItems));
			this.m_vRuleProcessorTableDictionary.Add("type_constructor_expression", new OracleMbEarleyRuleMultiProcessorTable(grammar, OracleMbEarleyTypeConstructorExpressionRuleMultiProcessors.s_vRuleProcessorItems));
			this.m_vRuleProcessorTableDictionary.Add("column", new OracleMbEarleyRuleMultiProcessorTable(grammar, OracleMbEarleyColumnRuleMultiProcessors.s_vRuleProcessorItems));
			this.m_vRuleProcessorTableDictionary.Add("literal", new OracleMbEarleyRuleMultiProcessorTable(grammar, OracleMbEarleyLiteralRuleMultiProcessors.s_vRuleProcessorItems));
			this.m_vRuleProcessorTableDictionary.Add("create", new OracleMbEarleyRuleMultiProcessorTable(grammar, OracleMbEarleyCreateRuleMultiProcessors.s_vRuleProcessorItems));
			this.m_vRuleProcessorTableDictionary.Add("select", new OracleMbEarleyRuleMultiProcessorTable(grammar, OracleMbEarleySelectRuleMultiProcessors.s_vRuleProcessorItems));
			this.m_vRuleProcessorTableDictionary.Add("call_statement", new OracleMbEarleyRuleMultiProcessorTable(grammar, OracleMbEarleyCallStatementRuleMultiProcessors.s_vRuleProcessorItems));
			this.m_vRuleProcessorTableDictionary.Add("sqlplus_command", new OracleMbEarleyRuleMultiProcessorTable(grammar, OracleMbEarleySqlPlusCommandRuleMultiProcessors.s_vRuleProcessorItems));
			this.m_vRuleProcessorTableDictionary.Add("dblink", new OracleMbEarleyRuleMultiProcessorTable(grammar, OracleMbEarleyDbLinkRuleMultiProcessors.s_vRuleProcessorItems));
			this.m_vRuleProcessorTableDictionary.Add("scalar_subquery_expression", new OracleMbEarleyRuleMultiProcessorTable(grammar, OracleMbEarleyScalarSubqueryExpressionRuleMultiProcessors.s_vRuleProcessorItems));
		}

		// Token: 0x04001D7F RID: 7551
		protected OracleSqlEarleyParserGrammarDefinition m_vGrammar;

		// Token: 0x04001D80 RID: 7552
		private static readonly object m_vObjectLock = new object();

		// Token: 0x04001D81 RID: 7553
		private static OracleMbEarleySqlPlsqlRuleProcessorTableDictionary s_vInstance = null;
	}
}
