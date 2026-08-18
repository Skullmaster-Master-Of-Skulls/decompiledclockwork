using System;
using System.Data.Query.InternalTrees;

namespace System.Data.Query.PlanCompiler
{
	// Token: 0x02000079 RID: 121
	internal static class SortOpRules
	{
		// Token: 0x06000923 RID: 2339 RVA: 0x00032F00 File Offset: 0x00031100
		private static bool ProcessSortOpOverAtMostOneRow(RuleProcessingContext context, Node n, out Node newNode)
		{
			ExtendedNodeInfo extendedNodeInfo = ((TransformationRulesContext)context).Command.GetExtendedNodeInfo(n.Child0);
			if (extendedNodeInfo.MaxRows == RowCount.Zero || extendedNodeInfo.MaxRows == RowCount.One)
			{
				newNode = n.Child0;
				return true;
			}
			newNode = n;
			return false;
		}

		// Token: 0x04000867 RID: 2151
		internal static readonly SimpleRule Rule_SortOpOverAtMostOneRow = new SimpleRule(OpType.Sort, new Rule.ProcessNodeDelegate(SortOpRules.ProcessSortOpOverAtMostOneRow));

		// Token: 0x04000868 RID: 2152
		internal static readonly Rule[] Rules = new Rule[]
		{
			SortOpRules.Rule_SortOpOverAtMostOneRow
		};
	}
}
