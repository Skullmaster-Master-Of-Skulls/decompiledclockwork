using System;
using System.Collections.Generic;
using System.Data.Common.Utils;
using System.Data.Metadata.Edm;
using System.Data.Query.InternalTrees;

namespace System.Data.Query.PlanCompiler
{
	// Token: 0x02000091 RID: 145
	internal static class PlanCompilerUtil
	{
		// Token: 0x060009C9 RID: 2505 RVA: 0x00034C3C File Offset: 0x00032E3C
		internal static bool IsRowTypeCaseOpWithNullability(CaseOp op, Node n, out bool thenClauseIsNull)
		{
			thenClauseIsNull = false;
			if (!TypeSemantics.IsRowType(op.Type))
			{
				return false;
			}
			if (n.Children.Count != 3)
			{
				return false;
			}
			if (!n.Child1.Op.Type.EdmEquals(op.Type) || !n.Child2.Op.Type.EdmEquals(op.Type))
			{
				return false;
			}
			if (n.Child1.Op.OpType == OpType.Null)
			{
				thenClauseIsNull = true;
				return true;
			}
			return n.Child2.Op.OpType == OpType.Null;
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x00034CD5 File Offset: 0x00032ED5
		internal static bool IsCollectionAggregateFunction(FunctionOp op, Node n)
		{
			return n.Children.Count == 1 && TypeSemantics.IsCollectionType(n.Child0.Op.Type) && TypeSemantics.IsAggregateFunction(op.Function);
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x00034D09 File Offset: 0x00032F09
		internal static bool IsConstantBaseOp(OpType opType)
		{
			return opType == OpType.Constant || opType == OpType.InternalConstant || opType == OpType.Null || opType == OpType.NullSentinel;
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x00034D1C File Offset: 0x00032F1C
		internal static Node CombinePredicates(Node predicate1, Node predicate2, Command command)
		{
			IEnumerable<Node> enumerable = PlanCompilerUtil.BreakIntoAndParts(predicate1);
			IEnumerable<Node> enumerable2 = PlanCompilerUtil.BreakIntoAndParts(predicate2);
			Node node = predicate1;
			foreach (Node node2 in enumerable2)
			{
				bool flag = false;
				foreach (Node node3 in enumerable)
				{
					if (node3.IsEquivalent(node2))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					node = command.CreateNode(command.CreateConditionalOp(OpType.And), node, node2);
				}
			}
			return node;
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x00034DD0 File Offset: 0x00032FD0
		private static IEnumerable<Node> BreakIntoAndParts(Node predicate)
		{
			return Helpers.GetLeafNodes<Node>(predicate, (Node node) => node.Op.OpType != OpType.And, (Node node) => new Node[]
			{
				node.Child0,
				node.Child1
			});
		}
	}
}
