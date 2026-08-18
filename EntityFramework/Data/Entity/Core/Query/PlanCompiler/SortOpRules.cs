using System;
using System.Data.Entity.Core.Query.InternalTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x0200069F RID: 1695
	internal static class SortOpRules
	{
		// Token: 0x06004326 RID: 17190 RVA: 0x0013E70C File Offset: 0x0013C90C
		private static bool ProcessSortOpOverAtMostOneRow(RuleProcessingContext context, Node n, out Node newNode)
		{
			ExtendedNodeInfo extendedNodeInfo = context.Command.GetExtendedNodeInfo(n.Child0);
			if (extendedNodeInfo.MaxRows == RowCount.Zero || extendedNodeInfo.MaxRows == RowCount.One)
			{
				newNode = n.Child0;
				return true;
			}
			newNode = n;
			return false;
		}

		// Token: 0x040018DB RID: 6363
		internal static readonly SimpleRule Rule_SortOpOverAtMostOneRow = new SimpleRule(OpType.Sort, new Rule.ProcessNodeDelegate(SortOpRules.ProcessSortOpOverAtMostOneRow));

		// Token: 0x040018DC RID: 6364
		internal static readonly Rule[] Rules = new Rule[]
		{
			SortOpRules.Rule_SortOpOverAtMostOneRow
		};
	}
}
