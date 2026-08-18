using System;
using Oracle.SqlAndPlsqlParser.LocalParsing.Ansi;
using Oracle.SqlAndPlsqlParser.RuleProcessors.ODPCommands;

namespace Oracle.SqlAndPlsqlParser.RuleProcessors
{
	// Token: 0x02000321 RID: 801
	internal class OracleMbEarleySqlPlsqlAnsiRuleProcessorTableDictionary : OracleMbEarleySqlPlsqlRuleProcessorTableDictionary
	{
		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06001D37 RID: 7479 RVA: 0x0011EFEC File Offset: 0x0011D1EC
		public new static OracleMbEarleySqlPlsqlAnsiRuleProcessorTableDictionary Instance
		{
			get
			{
				lock (OracleMbEarleySqlPlsqlAnsiRuleProcessorTableDictionary.m_vObjectLock)
				{
					if (OracleMbEarleySqlPlsqlAnsiRuleProcessorTableDictionary.s_vInstance == null)
					{
						OracleMbEarleySqlPlsqlAnsiRuleProcessorTableDictionary.s_vInstance = new OracleMbEarleySqlPlsqlAnsiRuleProcessorTableDictionary(OracleSqlAnsiEarleyParserGrammarDefinition.Instance);
					}
				}
				return OracleMbEarleySqlPlsqlAnsiRuleProcessorTableDictionary.s_vInstance;
			}
		}

		// Token: 0x06001D38 RID: 7480 RVA: 0x0011F040 File Offset: 0x0011D240
		protected OracleMbEarleySqlPlsqlAnsiRuleProcessorTableDictionary(OracleSqlEarleyParserGrammarDefinition grammar) : base(grammar)
		{
		}

		// Token: 0x06001D39 RID: 7481 RVA: 0x0011F04C File Offset: 0x0011D24C
		protected override void Initialize(OracleSqlEarleyParserGrammarDefinition grammar)
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
			this.m_vRuleProcessorTableDictionary.Add("expr", new OracleMbEarleyRuleMultiProcessorTable(grammar, OracleMbEarleyExprNNRuleMultiProcessors.s_vRuleProcessorItems));
			this.m_vRuleProcessorTableDictionary.Add("bind_var", new OracleMbEarleyRuleMultiProcessorTable(grammar, OracleMbEarleyBindVarRuleMultiProcessors.s_vRuleProcessorItems));
			this.m_vRuleProcessorTableDictionary.Add("simple_expression", new OracleMbEarleyRuleMultiProcessorTable(grammar, OracleMbEarleySimpleExpressionRuleMultiProcessors.s_vRuleProcessorItems));
			this.m_vRuleProcessorTableDictionary.Add("type_constructor_expression", new OracleMbEarleyRuleMultiProcessorTable(grammar, OracleMbEarleyTypeConstructorExpressionRuleMultiProcessors.s_vRuleProcessorItems));
			this.m_vRuleProcessorTableDictionary.Add("column", new OracleMbEarleyRuleMultiProcessorTable(grammar, OracleMbEarleyColumnRuleMultiProcessors.s_vRuleProcessorItems));
			this.m_vRuleProcessorTableDictionary.Add("literal", new OracleMbEarleyRuleMultiProcessorTable(grammar, OracleMbEarleyLiteralRuleMultiProcessors.s_vRuleProcessorItems));
			this.m_vRuleProcessorTableDictionary.Add("create", new OracleMbEarleyRuleMultiProcessorTable(grammar, OracleMbEarleyCreateRuleMultiProcessors.s_vRuleProcessorItems));
			this.m_vRuleProcessorTableDictionary.Add("select", new OracleMbEarleyRuleMultiProcessorTable(grammar, OracleMbEarleySelectAnsiRuleMultiProcessors.s_vRuleProcessorItems));
			this.m_vRuleProcessorTableDictionary.Add("call_statement", new OracleMbEarleyRuleMultiProcessorTable(grammar, OracleMbEarleyCallStatementRuleMultiProcessors.s_vRuleProcessorItems));
			this.m_vRuleProcessorTableDictionary.Add("sqlplus_command", new OracleMbEarleyRuleMultiProcessorTable(grammar, OracleMbEarleySqlPlusCommandRuleMultiProcessors.s_vRuleProcessorItems));
			this.m_vRuleProcessorTableDictionary.Add("dblink", new OracleMbEarleyRuleMultiProcessorTable(grammar, OracleMbEarleyDbLinkRuleMultiProcessors.s_vRuleProcessorItems));
			this.m_vRuleProcessorTableDictionary.Add("scalar_subquery_expression", new OracleMbEarleyRuleMultiProcessorTable(grammar, OracleMbEarleyScalarSubqueryExpressionRuleMultiProcessors.s_vRuleProcessorItems));
		}

		// Token: 0x04001D82 RID: 7554
		private static readonly object m_vObjectLock = new object();

		// Token: 0x04001D83 RID: 7555
		private static OracleMbEarleySqlPlsqlAnsiRuleProcessorTableDictionary s_vInstance = null;
	}
}
