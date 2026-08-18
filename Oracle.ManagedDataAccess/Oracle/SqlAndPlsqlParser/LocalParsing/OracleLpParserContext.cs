using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Oracle.SqlAndPlsqlParser.LocalParsing.Ansi;
using Oracle.SqlAndPlsqlParser.RuleProcessors;
using OracleInternal.Common;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x020002DF RID: 735
	internal class OracleLpParserContext : OracleMbEarleyParserMultiContext
	{
		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06001AB7 RID: 6839 RVA: 0x0010BBA8 File Offset: 0x00109DA8
		public ReadOnlyCollection<OracleLpStatement> Statements
		{
			get
			{
				if (this.m_vStatements != null)
				{
					return this.m_vStatements.AsReadOnly();
				}
				return null;
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06001AB8 RID: 6840 RVA: 0x0010BBC0 File Offset: 0x00109DC0
		// (set) Token: 0x06001AB9 RID: 6841 RVA: 0x0010BBC8 File Offset: 0x00109DC8
		public OracleLpTextFragment CurrentStatementText
		{
			get
			{
				return this.m_vCurrentStatementText;
			}
			set
			{
				this.m_vCurrentStatementText = value;
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06001ABA RID: 6842 RVA: 0x0010BBD4 File Offset: 0x00109DD4
		// (set) Token: 0x06001ABB RID: 6843 RVA: 0x0010BBDC File Offset: 0x00109DDC
		public int CurrentStatementBindVarCount
		{
			get
			{
				return this.m_vCurrentStatementBindVarCount;
			}
			set
			{
				this.m_vCurrentStatementBindVarCount = value;
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06001ABC RID: 6844 RVA: 0x0010BBE8 File Offset: 0x00109DE8
		public Dictionary<ParseNode, OracleLpBindParameter> CurrentStatementBindVarParseNodes
		{
			get
			{
				return this.m_vCurrentStatementBindVarParseNodes;
			}
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06001ABD RID: 6845 RVA: 0x0010BBF0 File Offset: 0x00109DF0
		// (set) Token: 0x06001ABE RID: 6846 RVA: 0x0010BBF8 File Offset: 0x00109DF8
		public OracleLpStatementClauseType CurrentStatementClause
		{
			get
			{
				return this.m_vCurrentStatementClause;
			}
			set
			{
				this.m_vCurrentStatementClause = value;
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06001ABF RID: 6847 RVA: 0x0010BC04 File Offset: 0x00109E04
		// (set) Token: 0x06001AC0 RID: 6848 RVA: 0x0010BC0C File Offset: 0x00109E0C
		public OracleLpStatement CurrentStatement
		{
			get
			{
				return this.m_vCurrentStatement;
			}
			set
			{
				if (this.m_vCurrentStatement != value)
				{
					this.m_vCurrentStatement = value;
					if (value != null)
					{
						if (this.m_vStatements == null)
						{
							this.m_vStatements = new List<OracleLpStatement>();
						}
						this.m_vStatements.Add(value);
						this.m_vCurrentStatementBindVarCount = 0;
						this.m_vCurrentStatementBindVarParseNodes.Clear();
					}
				}
			}
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06001AC1 RID: 6849 RVA: 0x0010BC60 File Offset: 0x00109E60
		// (set) Token: 0x06001AC2 RID: 6850 RVA: 0x0010BC68 File Offset: 0x00109E68
		public bool HandleBindVariables
		{
			get
			{
				return this.m_vHandleBindVariables;
			}
			set
			{
				this.m_vHandleBindVariables = value;
			}
		}

		// Token: 0x06001AC3 RID: 6851 RVA: 0x0010BC74 File Offset: 0x00109E74
		static OracleLpParserContext()
		{
			OracleMbEarleyRuleMultiProcessor.Postprocess += OracleLpParserContext.ProcessBindVariables;
		}

		// Token: 0x06001AC4 RID: 6852 RVA: 0x0010BC88 File Offset: 0x00109E88
		public OracleLpParserContext(Earley parser, OracleMbRuleProcessorTableDictionary<OracleMbEarleyRuleMultiProcessorTable> ruleProcessorTableDictionary) : base(parser, ruleProcessorTableDictionary)
		{
		}

		// Token: 0x06001AC5 RID: 6853 RVA: 0x0010BCA0 File Offset: 0x00109EA0
		public override object GetActiveObject(int type)
		{
			switch (type)
			{
			case 0:
				return this.m_vActiveODPContext;
			case 3:
				return this.m_vCurrentStatement;
			case 4:
				return this.m_vActiveLpSubquery;
			case 5:
				if (this.m_vActiveLpSubquery.SubqueryType != OracleLpSubqueryType.QueryBlock)
				{
					return null;
				}
				return ((OracleLpQueryBlockSubquery)this.m_vActiveLpSubquery).QueryBlock;
			case 6:
			{
				if (this.m_vActiveLpSubquery.SubqueryType != OracleLpSubqueryType.QueryBlock)
				{
					return null;
				}
				OracleLpQueryBlock queryBlock = ((OracleLpQueryBlockSubquery)this.m_vActiveLpSubquery).QueryBlock;
				List<OracleLpStatementDataContainer> terms = queryBlock.FromClause.Terms;
				return terms[terms.Count - 1];
			}
			case 7:
				return this.m_vActiveLpTableReference;
			case 8:
				return this.m_vActiveLpTablePrimary;
			case 9:
				return this.m_vActiveLpCondition;
			case 10:
				return this.m_vActiveLpColumn;
			case 11:
				return this.m_vActiveLpExpression;
			case 12:
				return this.m_vActiveColumnMappedQueryName;
			case 13:
				return this.m_vActiveLpQTENamedObject;
			case 14:
				return this.m_vActiveAnsiJoinClause;
			}
			return null;
		}

		// Token: 0x06001AC6 RID: 6854 RVA: 0x0010BDA4 File Offset: 0x00109FA4
		public override void SetActiveObject(int type, object ao)
		{
			switch (type)
			{
			case 0:
				this.m_vActiveODPContext = (IOracleMetadata)ao;
				return;
			case 1:
			case 2:
			case 5:
			case 6:
				break;
			case 3:
				this.m_vCurrentStatement = (OracleLpStatement)ao;
				return;
			case 4:
				this.m_vActiveLpSubquery = (OracleLpSubquery)ao;
				return;
			case 7:
				this.m_vActiveLpTableReference = (OracleLpStatementDataContainer)ao;
				return;
			case 8:
				this.m_vActiveLpTablePrimary = (OracleLpTablePrimary)ao;
				return;
			case 9:
				this.m_vActiveLpCondition = (OracleLpCondition)ao;
				return;
			case 10:
				this.m_vActiveLpColumn = (OracleLpColumn)ao;
				return;
			case 11:
				this.m_vActiveLpExpression = (OracleLpExpression)ao;
				return;
			case 12:
				this.m_vActiveColumnMappedQueryName = (OracleLpColumnMappedQueryName)ao;
				return;
			case 13:
				this.m_vActiveLpQTENamedObject = (OracleLpQteNamedObject)ao;
				return;
			case 14:
				this.m_vActiveAnsiJoinClause = (OracleLpBaseAnsiJoinClause)ao;
				break;
			default:
				return;
			}
		}

		// Token: 0x06001AC7 RID: 6855 RVA: 0x0010BE88 File Offset: 0x0010A088
		public override void Clear()
		{
			base.Clear();
			this.m_vRuleProcessorTable = base.GetRuleProcessorTable("ODPCommands");
			this.m_vCurrentStatementText = null;
			this.m_vStatements = null;
			this.m_vCurrentStatement = null;
			this.m_vCurrentStatementBindVarCount = 0;
			this.m_vCurrentStatementBindVarParseNodes.Clear();
			this.m_vCurrentStatementClause = OracleLpStatementClauseType.Unknown;
			this.m_vHandleBindVariables = false;
		}

		// Token: 0x06001AC8 RID: 6856 RVA: 0x0010BEE4 File Offset: 0x0010A0E4
		public static void ProcessBindVariables(ParseNode pn, OracleMbEarleyParserMultiContext ctx)
		{
			OracleLpParserContext oracleLpParserContext = (OracleLpParserContext)ctx;
			if (oracleLpParserContext.m_vHandleBindVariables)
			{
				oracleLpParserContext.m_vHandleBindVariables = false;
				OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
				ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("bind_var");
				OracleMbEarleyRuleMultiProcessor.TraverseAndProcessNodeSubtreeRules(pn, ctx, ctx.RuleProcessorTable.RuleProcessors);
				ctx.RuleProcessorTable = ruleProcessorTable;
			}
		}

		// Token: 0x04001CC7 RID: 7367
		protected List<OracleLpStatement> m_vStatements;

		// Token: 0x04001CC8 RID: 7368
		protected OracleLpTextFragment m_vCurrentStatementText;

		// Token: 0x04001CC9 RID: 7369
		private int m_vCurrentStatementBindVarCount;

		// Token: 0x04001CCA RID: 7370
		private Dictionary<ParseNode, OracleLpBindParameter> m_vCurrentStatementBindVarParseNodes = new Dictionary<ParseNode, OracleLpBindParameter>();

		// Token: 0x04001CCB RID: 7371
		private OracleLpStatementClauseType m_vCurrentStatementClause;

		// Token: 0x04001CCC RID: 7372
		protected OracleLpStatement m_vCurrentStatement;

		// Token: 0x04001CCD RID: 7373
		protected bool m_vHandleBindVariables;

		// Token: 0x04001CCE RID: 7374
		protected IOracleMetadata m_vActiveODPContext;

		// Token: 0x04001CCF RID: 7375
		protected OracleLpSubquery m_vActiveLpSubquery;

		// Token: 0x04001CD0 RID: 7376
		protected OracleLpStatementDataContainer m_vActiveLpTableReference;

		// Token: 0x04001CD1 RID: 7377
		protected OracleLpTablePrimary m_vActiveLpTablePrimary;

		// Token: 0x04001CD2 RID: 7378
		protected OracleLpQteNamedObject m_vActiveLpQTENamedObject;

		// Token: 0x04001CD3 RID: 7379
		protected OracleLpCondition m_vActiveLpCondition;

		// Token: 0x04001CD4 RID: 7380
		protected OracleLpColumn m_vActiveLpColumn;

		// Token: 0x04001CD5 RID: 7381
		protected OracleLpExpression m_vActiveLpExpression;

		// Token: 0x04001CD6 RID: 7382
		protected OracleLpColumnMappedQueryName m_vActiveColumnMappedQueryName;

		// Token: 0x04001CD7 RID: 7383
		protected OracleLpBaseAnsiJoinClause m_vActiveAnsiJoinClause;
	}
}
