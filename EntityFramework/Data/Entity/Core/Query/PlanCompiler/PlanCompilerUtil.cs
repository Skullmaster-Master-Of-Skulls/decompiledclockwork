using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Query.InternalTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x0200068D RID: 1677
	internal static class PlanCompilerUtil
	{
		// Token: 0x06004217 RID: 16919 RVA: 0x001379F4 File Offset: 0x00135BF4
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

		// Token: 0x06004218 RID: 16920 RVA: 0x00137A8D File Offset: 0x00135C8D
		internal static bool IsCollectionAggregateFunction(FunctionOp op, Node n)
		{
			return n.Children.Count == 1 && TypeSemantics.IsCollectionType(n.Child0.Op.Type) && TypeSemantics.IsAggregateFunction(op.Function);
		}

		// Token: 0x06004219 RID: 16921 RVA: 0x00137AC1 File Offset: 0x00135CC1
		internal static bool IsConstantBaseOp(OpType opType)
		{
			return opType == OpType.Constant || opType == OpType.InternalConstant || opType == OpType.Null || opType == OpType.NullSentinel;
		}

		// Token: 0x0600421A RID: 16922 RVA: 0x00137AD4 File Offset: 0x00135CD4
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

		// Token: 0x0600421B RID: 16923 RVA: 0x00137BC4 File Offset: 0x00135DC4
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
