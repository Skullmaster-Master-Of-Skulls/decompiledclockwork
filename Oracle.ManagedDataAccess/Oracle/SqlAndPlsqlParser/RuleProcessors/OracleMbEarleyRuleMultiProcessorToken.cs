using System;

namespace Oracle.SqlAndPlsqlParser.RuleProcessors
{
	// Token: 0x0200031B RID: 795
	internal class OracleMbEarleyRuleMultiProcessorToken
	{
		// Token: 0x06001D27 RID: 7463 RVA: 0x0011E7C4 File Offset: 0x0011C9C4
		public OracleMbEarleyRuleMultiProcessorToken(OracleMbEarleyRuleMultiProcessorDelegate multiProcessor, int ruleMatchPosition, int ruleMatchLength)
		{
			this.m_vMultiProcessor = multiProcessor;
			this.m_vRuleMatchPosition = ruleMatchPosition;
			this.m_vRuleMatchLength = ruleMatchLength;
		}

		// Token: 0x04001D6E RID: 7534
		public OracleMbEarleyRuleMultiProcessorDelegate m_vMultiProcessor;

		// Token: 0x04001D6F RID: 7535
		public int m_vRuleMatchPosition = -1;

		// Token: 0x04001D70 RID: 7536
		public int m_vRuleMatchLength;
	}
}
