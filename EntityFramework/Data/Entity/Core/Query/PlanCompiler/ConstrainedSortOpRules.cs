using System;
using System.Data.Entity.Core.Query.InternalTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x0200065F RID: 1631
	internal static class ConstrainedSortOpRules
	{
		// Token: 0x06003FB4 RID: 16308 RVA: 0x00123AD0 File Offset: 0x00121CD0
		private static bool ProcessConstrainedSortOpOverEmptySet(RuleProcessingContext context, Node n, out Node newNode)
		{
			ExtendedNodeInfo extendedNodeInfo = context.Command.GetExtendedNodeInfo(n.Child0);
			if (extendedNodeInfo.MaxRows == RowCount.Zero)
			{
				newNode = n.Child0;
				return true;
			}
			newNode = n;
			return false;
		}

		// Token: 0x040017BE RID: 6078
		internal static readonly SimpleRule Rule_ConstrainedSortOpOverEmptySet = new SimpleRule(OpType.ConstrainedSort, new Rule.ProcessNodeDelegate(ConstrainedSortOpRules.ProcessConstrainedSortOpOverEmptySet));

		// Token: 0x040017BF RID: 6079
		internal static readonly Rule[] Rules = new Rule[]
		{
			ConstrainedSortOpRules.Rule_ConstrainedSortOpOverEmptySet
		};
	}
}
