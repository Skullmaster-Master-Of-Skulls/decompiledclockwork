using System;
using System.Data.Query.InternalTrees;

namespace System.Data.Query.PlanCompiler
{
	// Token: 0x0200007A RID: 122
	internal static class ConstrainedSortOpRules
	{
		// Token: 0x06000925 RID: 2341 RVA: 0x00032F70 File Offset: 0x00031170
		private static bool ProcessConstrainedSortOpOverEmptySet(RuleProcessingContext context, Node n, out Node newNode)
		{
			ExtendedNodeInfo extendedNodeInfo = ((TransformationRulesContext)context).Command.GetExtendedNodeInfo(n.Child0);
			if (extendedNodeInfo.MaxRows == RowCount.Zero)
			{
				newNode = n.Child0;
				return true;
			}
			newNode = n;
			return false;
		}

		// Token: 0x04000869 RID: 2153
		internal static readonly SimpleRule Rule_ConstrainedSortOpOverEmptySet = new SimpleRule(OpType.ConstrainedSort, new Rule.ProcessNodeDelegate(ConstrainedSortOpRules.ProcessConstrainedSortOpOverEmptySet));

		// Token: 0x0400086A RID: 2154
		internal static readonly Rule[] Rules = new Rule[]
		{
			ConstrainedSortOpRules.Rule_ConstrainedSortOpOverEmptySet
		};
	}
}
