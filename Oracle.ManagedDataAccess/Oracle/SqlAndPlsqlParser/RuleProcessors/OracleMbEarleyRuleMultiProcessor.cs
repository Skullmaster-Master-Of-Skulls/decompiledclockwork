using System;
using System.Collections.Generic;

namespace Oracle.SqlAndPlsqlParser.RuleProcessors
{
	// Token: 0x0200031C RID: 796
	internal static class OracleMbEarleyRuleMultiProcessor
	{
		// Token: 0x1400000E RID: 14
		// (add) Token: 0x06001D28 RID: 7464 RVA: 0x0011E7E8 File Offset: 0x0011C9E8
		// (remove) Token: 0x06001D29 RID: 7465 RVA: 0x0011E81C File Offset: 0x0011CA1C
		public static event OracleMbEarleyRuleMultiProcessorPrePostProcessingDelegate Preprocess;

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x06001D2A RID: 7466 RVA: 0x0011E850 File Offset: 0x0011CA50
		// (remove) Token: 0x06001D2B RID: 7467 RVA: 0x0011E884 File Offset: 0x0011CA84
		public static event OracleMbEarleyRuleMultiProcessorPrePostProcessingDelegate Postprocess;

		// Token: 0x06001D2D RID: 7469 RVA: 0x0011E8BC File Offset: 0x0011CABC
		public static object ProcessNodeRules(ParseNode pn, int startRuleIndex, int endRuleIndex, OracleMbEarleyParserMultiContext ctx)
		{
			if (OracleMbEarleyRuleMultiProcessor.Preprocess != null)
			{
				OracleMbEarleyRuleMultiProcessor.Preprocess(pn, ctx);
			}
			if (pn == null || pn.m_vRulesUsed == null)
			{
				return null;
			}
			object result = null;
			Dictionary<int, List<OracleMbEarleyRuleMultiProcessorToken>> ruleProcessors = ctx.RuleProcessorTable.RuleProcessors;
			if (endRuleIndex == -1)
			{
				endRuleIndex = pn.m_vRulesUsed.Count;
			}
			bool flag = pn != ctx.CurrentParseNode;
			ParseNode currentParseNode = null;
			int currentRuleIndex = 0;
			if (flag)
			{
				currentParseNode = ctx.CurrentParseNode;
				currentRuleIndex = ctx.CurrentRuleIndex;
				ctx.CurrentParseNode = pn;
			}
			for (int i = startRuleIndex; i < endRuleIndex; i++)
			{
				ctx.CurrentRuleIndex = i;
				int key = pn.m_vRulesUsed[i];
				List<OracleMbEarleyRuleMultiProcessorToken> list;
				if (ruleProcessors.TryGetValue(key, out list))
				{
					foreach (OracleMbEarleyRuleMultiProcessorToken oracleMbEarleyRuleMultiProcessorToken in list)
					{
						result = oracleMbEarleyRuleMultiProcessorToken.m_vMultiProcessor(ctx, oracleMbEarleyRuleMultiProcessorToken.m_vRuleMatchPosition);
					}
				}
				i = ctx.CurrentRuleIndex;
			}
			if (flag)
			{
				ctx.CurrentRuleIndex = currentRuleIndex;
				ctx.CurrentParseNode = currentParseNode;
			}
			else
			{
				ctx.CurrentRuleIndex = endRuleIndex;
			}
			if (OracleMbEarleyRuleMultiProcessor.Postprocess != null)
			{
				OracleMbEarleyRuleMultiProcessor.Postprocess(pn, ctx);
			}
			return result;
		}

		// Token: 0x06001D2E RID: 7470 RVA: 0x0011E9F0 File Offset: 0x0011CBF0
		public static object TraverseAndProcessNodeSubtreeRules(ParseNode pn, OracleMbEarleyParserMultiContext ctx, Dictionary<int, List<OracleMbEarleyRuleMultiProcessorToken>> rpd)
		{
			if (pn == null)
			{
				return null;
			}
			bool flag = false;
			object result = null;
			if (pn.m_vRulesUsed != null)
			{
				foreach (int key in pn.m_vRulesUsed)
				{
					if (rpd.ContainsKey(key))
					{
						flag = true;
						break;
					}
				}
			}
			if (flag)
			{
				ParseNode currentParseNode = ctx.CurrentParseNode;
				int currentRuleIndex = ctx.CurrentRuleIndex;
				ctx.CurrentParseNode = pn;
				ctx.CurrentRuleIndex = 0;
				result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(pn, 0, -1, ctx);
				ctx.CurrentRuleIndex = currentRuleIndex;
				ctx.CurrentParseNode = currentParseNode;
			}
			else
			{
				List<ParseNode> list = pn.Children();
				if (list == null)
				{
					return null;
				}
				foreach (ParseNode pn2 in list)
				{
					result = OracleMbEarleyRuleMultiProcessor.TraverseAndProcessNodeSubtreeRules(pn2, ctx, rpd);
				}
			}
			return result;
		}

		// Token: 0x06001D2F RID: 7471 RVA: 0x0011EAE8 File Offset: 0x0011CCE8
		public static object ProcessSubtreeRules(ParseNode pn, int startRuleIndex, int endRuleIndex, OracleMbEarleyParserMultiContext ctx)
		{
			if (pn == null || pn.m_vRulesUsed == null)
			{
				return null;
			}
			object result = null;
			Dictionary<int, List<OracleMbEarleyRuleMultiProcessorToken>> ruleProcessors = ctx.RuleProcessorTable.RuleProcessors;
			if (endRuleIndex == -1)
			{
				endRuleIndex = pn.m_vRulesUsed.Count;
			}
			bool flag = pn != ctx.CurrentParseNode;
			ParseNode currentParseNode = null;
			int currentRuleIndex = 0;
			if (flag)
			{
				currentParseNode = ctx.CurrentParseNode;
				currentRuleIndex = ctx.CurrentRuleIndex;
				ctx.CurrentParseNode = pn;
			}
			Queue<ParseNode> queue = new Queue<ParseNode>();
			queue.Enqueue(pn);
			int currentRuleIndex2 = startRuleIndex;
			int num = endRuleIndex;
			while (queue.Count > 0)
			{
				ParseNode parseNode = queue.Dequeue();
				ctx.CurrentParseNode = parseNode;
				if (num == -1)
				{
					num = parseNode.m_vRulesUsed.Count;
				}
				bool flag2 = false;
				ctx.CurrentRuleIndex = currentRuleIndex2;
				while (ctx.CurrentRuleIndex < num)
				{
					int key = parseNode.m_vRulesUsed[ctx.CurrentRuleIndex];
					List<OracleMbEarleyRuleMultiProcessorToken> list;
					if (ruleProcessors.TryGetValue(key, out list))
					{
						flag2 = true;
						foreach (OracleMbEarleyRuleMultiProcessorToken oracleMbEarleyRuleMultiProcessorToken in list)
						{
							result = oracleMbEarleyRuleMultiProcessorToken.m_vMultiProcessor(ctx, oracleMbEarleyRuleMultiProcessorToken.m_vRuleMatchPosition);
						}
					}
					ctx.CurrentRuleIndex++;
				}
				if (!flag2)
				{
					foreach (ParseNode parseNode2 in parseNode.Children())
					{
						if (parseNode2.m_vRulesUsed != null)
						{
							queue.Enqueue(parseNode2);
						}
					}
				}
				currentRuleIndex2 = 0;
				num = -1;
			}
			if (flag)
			{
				ctx.CurrentRuleIndex = currentRuleIndex;
				ctx.CurrentParseNode = currentParseNode;
			}
			else
			{
				ctx.CurrentRuleIndex = endRuleIndex;
			}
			return result;
		}
	}
}
