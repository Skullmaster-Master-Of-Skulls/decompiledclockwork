using System;

namespace Oracle.SqlAndPlsqlParser.RuleProcessors
{
	// Token: 0x020002DD RID: 733
	internal class OracleMbEarleyParserMultiContext : OracleMbParserContextBase<Earley, OracleMbEarleyRuleMultiProcessorTable>
	{
		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06001AAF RID: 6831 RVA: 0x0010B378 File Offset: 0x00109578
		public ParserRuleTuple CurrentRule
		{
			get
			{
				return this.m_vParser.EarleyGrammar.m_vRules[this.m_vCurrentParseNode.m_vRulesUsed[this.m_vCurrentRuleIndex]];
			}
		}

		// Token: 0x06001AB0 RID: 6832 RVA: 0x0010B3A4 File Offset: 0x001095A4
		public OracleMbEarleyParserMultiContext(Earley parser, OracleMbRuleProcessorTableDictionary<OracleMbEarleyRuleMultiProcessorTable> ruleProcessorTableDictionary) : base(parser, ruleProcessorTableDictionary)
		{
		}

		// Token: 0x06001AB1 RID: 6833 RVA: 0x0010B3B0 File Offset: 0x001095B0
		public virtual object GetActiveObject(int type)
		{
			return null;
		}

		// Token: 0x06001AB2 RID: 6834 RVA: 0x0010B3B4 File Offset: 0x001095B4
		public virtual void SetActiveObject(int type, object ao)
		{
		}
	}
}
