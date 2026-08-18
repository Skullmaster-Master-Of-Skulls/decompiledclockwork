using System;
using System.Collections.Generic;
using System.Data.Query.InternalTrees;

namespace System.Data.Query.PlanCompiler
{
	// Token: 0x02000077 RID: 119
	internal static class SetOpRules
	{
		// Token: 0x0600091C RID: 2332 RVA: 0x0003259C File Offset: 0x0003079C
		private static bool ProcessSetOpOverEmptySet(RuleProcessingContext context, Node setOpNode, out Node newNode)
		{
			bool flag = context.Command.GetExtendedNodeInfo(setOpNode.Child0).MaxRows == RowCount.Zero;
			bool flag2 = context.Command.GetExtendedNodeInfo(setOpNode.Child1).MaxRows == RowCount.Zero;
			if (!flag && !flag2)
			{
				newNode = setOpNode;
				return false;
			}
			SetOp setOp = (SetOp)setOpNode.Op;
			int num;
			if ((!flag2 && setOp.OpType == OpType.UnionAll) || (!flag && setOp.OpType == OpType.Intersect))
			{
				num = 1;
			}
			else
			{
				num = 0;
			}
			newNode = setOpNode.Children[num];
			TransformationRulesContext transformationRulesContext = (TransformationRulesContext)context;
			foreach (KeyValuePair<Var, Var> keyValuePair in setOp.VarMap[num])
			{
				transformationRulesContext.AddVarMapping(keyValuePair.Key, keyValuePair.Value);
			}
			return true;
		}

		// Token: 0x0400085E RID: 2142
		internal static readonly SimpleRule Rule_UnionAllOverEmptySet = new SimpleRule(OpType.UnionAll, new Rule.ProcessNodeDelegate(SetOpRules.ProcessSetOpOverEmptySet));

		// Token: 0x0400085F RID: 2143
		internal static readonly SimpleRule Rule_IntersectOverEmptySet = new SimpleRule(OpType.Intersect, new Rule.ProcessNodeDelegate(SetOpRules.ProcessSetOpOverEmptySet));

		// Token: 0x04000860 RID: 2144
		internal static readonly SimpleRule Rule_ExceptOverEmptySet = new SimpleRule(OpType.Except, new Rule.ProcessNodeDelegate(SetOpRules.ProcessSetOpOverEmptySet));

		// Token: 0x04000861 RID: 2145
		internal static readonly Rule[] Rules = new Rule[]
		{
			SetOpRules.Rule_UnionAllOverEmptySet,
			SetOpRules.Rule_IntersectOverEmptySet,
			SetOpRules.Rule_ExceptOverEmptySet
		};
	}
}
