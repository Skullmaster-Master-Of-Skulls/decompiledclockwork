using System;

namespace Oracle.SqlAndPlsqlParser
{
	// Token: 0x02000271 RID: 625
	internal class EarleyParseTreeBuildState
	{
		// Token: 0x060018C2 RID: 6338 RVA: 0x001048BC File Offset: 0x00102ABC
		public EarleyParseTreeBuildState(int start, int end, int ruleIdx, int dot, ParserRuleTuple rule, ParseNode parent)
		{
			this.m_vStart = start;
			this.m_vEnd = end;
			this.m_vRuleIdx = ruleIdx;
			this.m_vDot = dot;
			this.m_vRule = rule;
			this.m_vParentNode = parent;
		}

		// Token: 0x04001B3C RID: 6972
		public int m_vStart;

		// Token: 0x04001B3D RID: 6973
		public int m_vEnd;

		// Token: 0x04001B3E RID: 6974
		public int m_vRuleIdx;

		// Token: 0x04001B3F RID: 6975
		public int m_vDot;

		// Token: 0x04001B40 RID: 6976
		public ParserRuleTuple m_vRule;

		// Token: 0x04001B41 RID: 6977
		public ParseNode m_vParentNode;
	}
}
