using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Query.InternalTrees;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x0200066D RID: 1645
	internal static class FilterOpRules
	{
		// Token: 0x0600403C RID: 16444 RVA: 0x00125F20 File Offset: 0x00124120
		private static Node GetPushdownPredicate(Command command, Node filterNode, VarVec columns, out Node nonPushdownPredicateNode)
		{
			Node node = filterNode.Child1;
			nonPushdownPredicateNode = null;
			ExtendedNodeInfo extendedNodeInfo = command.GetExtendedNodeInfo(filterNode);
			if (columns == null && extendedNodeInfo.ExternalReferences.IsEmpty)
			{
				return node;
			}
			if (columns == null)
			{
				ExtendedNodeInfo extendedNodeInfo2 = command.GetExtendedNodeInfo(filterNode.Child0);
				columns = extendedNodeInfo2.Definitions;
			}
			Predicate predicate = new Predicate(command, node);
			Predicate predicate2;
			predicate = predicate.GetSingleTablePredicates(columns, out predicate2);
			node = predicate.BuildAndTree();
			nonPushdownPredicateNode = predicate2.BuildAndTree();
			return node;
		}

		// Token: 0x0600403D RID: 16445 RVA: 0x00125F8C File Offset: 0x0012418C
		private static bool ProcessFilterOverFilter(RuleProcessingContext context, Node filterNode, out Node newNode)
		{
			Node arg = context.Command.CreateNode(context.Command.CreateConditionalOp(OpType.And), filterNode.Child0.Child1, filterNode.Child1);
			newNode = context.Command.CreateNode(context.Command.CreateFilterOp(), filterNode.Child0.Child0, arg);
			return true;
		}

		// Token: 0x0600403E RID: 16446 RVA: 0x00125FE8 File Offset: 0x001241E8
		private static bool ProcessFilterOverProject(RuleProcessingContext context, Node filterNode, out Node newNode)
		{
			newNode = filterNode;
			Node child = filterNode.Child1;
			if (child.Op.OpType == OpType.ConstantPredicate)
			{
				return false;
			}
			TransformationRulesContext transformationRulesContext = (TransformationRulesContext)context;
			Dictionary<Var, int> varRefMap = new Dictionary<Var, int>();
			if (!transformationRulesContext.IsScalarOpTree(child, varRefMap))
			{
				return false;
			}
			Node child2 = filterNode.Child0;
			Dictionary<Var, Node> varMap = transformationRulesContext.GetVarMap(child2.Child1, varRefMap);
			if (varMap == null)
			{
				return false;
			}
			Node arg = transformationRulesContext.ReMap(child, varMap);
			Node arg2 = transformationRulesContext.Command.CreateNode(transformationRulesContext.Command.CreateFilterOp(), child2.Child0, arg);
			Node node = transformationRulesContext.Command.CreateNode(child2.Op, arg2, child2.Child1);
			newNode = node;
			return true;
		}

		// Token: 0x0600403F RID: 16447 RVA: 0x00126090 File Offset: 0x00124290
		private static bool ProcessFilterOverSetOp(RuleProcessingContext context, Node filterNode, out Node newNode)
		{
			newNode = filterNode;
			TransformationRulesContext transformationRulesContext = (TransformationRulesContext)context;
			Node node;
			Node pushdownPredicate = FilterOpRules.GetPushdownPredicate(transformationRulesContext.Command, filterNode, null, out node);
			if (pushdownPredicate == null)
			{
				return false;
			}
			if (!transformationRulesContext.IsScalarOpTree(pushdownPredicate))
			{
				return false;
			}
			Node child = filterNode.Child0;
			SetOp setOp = (SetOp)child.Op;
			List<Node> list = new List<Node>();
			int num = 0;
			foreach (VarMap varMap2 in setOp.VarMap)
			{
				if (setOp.OpType == OpType.Except && num == 1)
				{
					list.Add(child.Child1);
					break;
				}
				Dictionary<Var, Node> dictionary = new Dictionary<Var, Node>();
				foreach (KeyValuePair<Var, Var> keyValuePair in varMap2)
				{
					Node value = transformationRulesContext.Command.CreateNode(transformationRulesContext.Command.CreateVarRefOp(keyValuePair.Value));
					dictionary.Add(keyValuePair.Key, value);
				}
				Node node2 = pushdownPredicate;
				if (num == 0 && filterNode.Op.OpType != OpType.Except)
				{
					node2 = transformationRulesContext.Copy(node2);
				}
				Node node3 = transformationRulesContext.ReMap(node2, dictionary);
				transformationRulesContext.Command.RecomputeNodeInfo(node3);
				Node item = transformationRulesContext.Command.CreateNode(transformationRulesContext.Command.CreateFilterOp(), child.Children[num], node3);
				list.Add(item);
				num++;
			}
			Node node4 = transformationRulesContext.Command.CreateNode(child.Op, list);
			if (node != null)
			{
				newNode = transformationRulesContext.Command.CreateNode(transformationRulesContext.Command.CreateFilterOp(), node4, node);
			}
			else
			{
				newNode = node4;
			}
			return true;
		}

		// Token: 0x06004040 RID: 16448 RVA: 0x0012624C File Offset: 0x0012444C
		private static bool ProcessFilterOverDistinct(RuleProcessingContext context, Node filterNode, out Node newNode)
		{
			newNode = filterNode;
			Node node;
			Node pushdownPredicate = FilterOpRules.GetPushdownPredicate(context.Command, filterNode, null, out node);
			if (pushdownPredicate == null)
			{
				return false;
			}
			Node child = filterNode.Child0;
			Node arg = context.Command.CreateNode(context.Command.CreateFilterOp(), child.Child0, pushdownPredicate);
			Node node2 = context.Command.CreateNode(child.Op, arg);
			if (node != null)
			{
				newNode = context.Command.CreateNode(context.Command.CreateFilterOp(), node2, node);
			}
			else
			{
				newNode = node2;
			}
			return true;
		}

		// Token: 0x06004041 RID: 16449 RVA: 0x001262D0 File Offset: 0x001244D0
		private static bool ProcessFilterOverGroupBy(RuleProcessingContext context, Node filterNode, out Node newNode)
		{
			newNode = filterNode;
			Node child = filterNode.Child0;
			GroupByOp groupByOp = (GroupByOp)child.Op;
			TransformationRulesContext transformationRulesContext = (TransformationRulesContext)context;
			Dictionary<Var, int> varRefMap = new Dictionary<Var, int>();
			if (!transformationRulesContext.IsScalarOpTree(filterNode.Child1, varRefMap))
			{
				return false;
			}
			Node node;
			Node pushdownPredicate = FilterOpRules.GetPushdownPredicate(context.Command, filterNode, groupByOp.Keys, out node);
			if (pushdownPredicate == null)
			{
				return false;
			}
			Dictionary<Var, Node> varMap = transformationRulesContext.GetVarMap(child.Child1, varRefMap);
			if (varMap == null)
			{
				return false;
			}
			Node arg = transformationRulesContext.ReMap(pushdownPredicate, varMap);
			Node arg2 = transformationRulesContext.Command.CreateNode(transformationRulesContext.Command.CreateFilterOp(), child.Child0, arg);
			Node node2 = transformationRulesContext.Command.CreateNode(child.Op, arg2, child.Child1, child.Child2);
			if (node == null)
			{
				newNode = node2;
			}
			else
			{
				newNode = transformationRulesContext.Command.CreateNode(transformationRulesContext.Command.CreateFilterOp(), node2, node);
			}
			return true;
		}

		// Token: 0x06004042 RID: 16450 RVA: 0x001263B8 File Offset: 0x001245B8
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "non-InnerJoin")]
		private static bool ProcessFilterOverJoin(RuleProcessingContext context, Node filterNode, out Node newNode)
		{
			newNode = filterNode;
			TransformationRulesContext transformationRulesContext = (TransformationRulesContext)context;
			if (transformationRulesContext.IsFilterPushdownSuppressed(filterNode))
			{
				return false;
			}
			Node child = filterNode.Child0;
			Op op = child.Op;
			Node node = child.Child0;
			Node node2 = child.Child1;
			Command command = transformationRulesContext.Command;
			bool flag = false;
			ExtendedNodeInfo extendedNodeInfo = command.GetExtendedNodeInfo(node2);
			Predicate predicate = new Predicate(command, filterNode.Child1);
			if (op.OpType == OpType.LeftOuterJoin && !predicate.PreservesNulls(extendedNodeInfo.Definitions, true))
			{
				if (transformationRulesContext.PlanCompiler.IsAfterPhase(PlanCompilerPhase.NullSemantics) && transformationRulesContext.PlanCompiler.IsAfterPhase(PlanCompilerPhase.JoinElimination))
				{
					op = command.CreateInnerJoinOp();
					flag = true;
				}
				else
				{
					transformationRulesContext.PlanCompiler.TransformationsDeferred = true;
				}
			}
			ExtendedNodeInfo extendedNodeInfo2 = command.GetExtendedNodeInfo(node);
			Node node3 = null;
			if (node.Op.OpType != OpType.ScanTable)
			{
				Predicate singleTablePredicates = predicate.GetSingleTablePredicates(extendedNodeInfo2.Definitions, out predicate);
				node3 = singleTablePredicates.BuildAndTree();
			}
			Node node4 = null;
			if (node2.Op.OpType != OpType.ScanTable && op.OpType != OpType.LeftOuterJoin)
			{
				Predicate singleTablePredicates2 = predicate.GetSingleTablePredicates(extendedNodeInfo.Definitions, out predicate);
				node4 = singleTablePredicates2.BuildAndTree();
			}
			Node node5 = null;
			if (op.OpType == OpType.CrossJoin || op.OpType == OpType.InnerJoin)
			{
				Predicate joinPredicates = predicate.GetJoinPredicates(extendedNodeInfo2.Definitions, extendedNodeInfo.Definitions, out predicate);
				node5 = joinPredicates.BuildAndTree();
			}
			if (node3 != null)
			{
				node = command.CreateNode(command.CreateFilterOp(), node, node3);
				flag = true;
			}
			if (node4 != null)
			{
				node2 = command.CreateNode(command.CreateFilterOp(), node2, node4);
				flag = true;
			}
			if (node5 != null)
			{
				flag = true;
				if (op.OpType == OpType.CrossJoin)
				{
					op = command.CreateInnerJoinOp();
				}
				else
				{
					PlanCompiler.Assert(op.OpType == OpType.InnerJoin, "unexpected non-InnerJoin?");
					node5 = PlanCompilerUtil.CombinePredicates(child.Child2, node5, command);
				}
			}
			else
			{
				node5 = ((op.OpType == OpType.CrossJoin) ? null : child.Child2);
			}
			if (!flag)
			{
				return false;
			}
			Node node6;
			if (op.OpType == OpType.CrossJoin)
			{
				node6 = command.CreateNode(op, node, node2);
			}
			else
			{
				node6 = command.CreateNode(op, node, node2, node5);
			}
			Node node7 = predicate.BuildAndTree();
			if (node7 == null)
			{
				newNode = node6;
			}
			else
			{
				newNode = command.CreateNode(command.CreateFilterOp(), node6, node7);
			}
			return true;
		}

		// Token: 0x06004043 RID: 16451 RVA: 0x001265F8 File Offset: 0x001247F8
		private static bool ProcessFilterOverOuterApply(RuleProcessingContext context, Node filterNode, out Node newNode)
		{
			newNode = filterNode;
			Node child = filterNode.Child0;
			Node child2 = child.Child1;
			TransformationRulesContext transformationRulesContext = (TransformationRulesContext)context;
			Command command = transformationRulesContext.Command;
			ExtendedNodeInfo extendedNodeInfo = command.GetExtendedNodeInfo(child2);
			Predicate predicate = new Predicate(command, filterNode.Child1);
			if (!predicate.PreservesNulls(extendedNodeInfo.Definitions, true))
			{
				if (transformationRulesContext.PlanCompiler.IsAfterPhase(PlanCompilerPhase.NullSemantics) && transformationRulesContext.PlanCompiler.IsAfterPhase(PlanCompilerPhase.JoinElimination))
				{
					Node arg = command.CreateNode(command.CreateCrossApplyOp(), child.Child0, child2);
					Node node = command.CreateNode(command.CreateFilterOp(), arg, filterNode.Child1);
					newNode = node;
					return true;
				}
				transformationRulesContext.PlanCompiler.TransformationsDeferred = true;
			}
			return false;
		}

		// Token: 0x06004044 RID: 16452 RVA: 0x001266A8 File Offset: 0x001248A8
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		private static bool ProcessFilterWithConstantPredicate(RuleProcessingContext context, Node n, out Node newNode)
		{
			newNode = n;
			ConstantPredicateOp constantPredicateOp = (ConstantPredicateOp)n.Child1.Op;
			if (constantPredicateOp.IsTrue)
			{
				newNode = n.Child0;
				return true;
			}
			PlanCompiler.Assert(constantPredicateOp.IsFalse, "unexpected non-false predicate?");
			if (n.Child0.Op.OpType == OpType.SingleRowTable || (n.Child0.Op.OpType == OpType.Project && n.Child0.Child0.Op.OpType == OpType.SingleRowTable))
			{
				return false;
			}
			TransformationRulesContext transformationRulesContext = (TransformationRulesContext)context;
			ExtendedNodeInfo extendedNodeInfo = transformationRulesContext.Command.GetExtendedNodeInfo(n.Child0);
			List<Node> list = new List<Node>();
			VarVec varVec = transformationRulesContext.Command.CreateVarVec();
			foreach (Var var in extendedNodeInfo.Definitions)
			{
				NullOp op = transformationRulesContext.Command.CreateNullOp(var.Type);
				Node definingExpr = transformationRulesContext.Command.CreateNode(op);
				Var var2;
				Node item = transformationRulesContext.Command.CreateVarDefNode(definingExpr, out var2);
				transformationRulesContext.AddVarMapping(var, var2);
				varVec.Set(var2);
				list.Add(item);
			}
			if (varVec.IsEmpty)
			{
				NullOp op2 = transformationRulesContext.Command.CreateNullOp(transformationRulesContext.Command.BooleanType);
				Node definingExpr2 = transformationRulesContext.Command.CreateNode(op2);
				Var v;
				Node item2 = transformationRulesContext.Command.CreateVarDefNode(definingExpr2, out v);
				varVec.Set(v);
				list.Add(item2);
			}
			Node child = transformationRulesContext.Command.CreateNode(transformationRulesContext.Command.CreateSingleRowTableOp());
			n.Child0 = child;
			Node arg = transformationRulesContext.Command.CreateNode(transformationRulesContext.Command.CreateVarDefListOp(), list);
			ProjectOp op3 = transformationRulesContext.Command.CreateProjectOp(varVec);
			Node node = transformationRulesContext.Command.CreateNode(op3, n, arg);
			node.Child0 = n;
			newNode = node;
			return true;
		}

		// Token: 0x040017E7 RID: 6119
		internal static readonly PatternMatchRule Rule_FilterOverFilter = new PatternMatchRule(new Node(FilterOp.Pattern, new Node[]
		{
			new Node(FilterOp.Pattern, new Node[]
			{
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0])
			}),
			new Node(LeafOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(FilterOpRules.ProcessFilterOverFilter));

		// Token: 0x040017E8 RID: 6120
		internal static readonly PatternMatchRule Rule_FilterOverProject = new PatternMatchRule(new Node(FilterOp.Pattern, new Node[]
		{
			new Node(ProjectOp.Pattern, new Node[]
			{
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0])
			}),
			new Node(LeafOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(FilterOpRules.ProcessFilterOverProject));

		// Token: 0x040017E9 RID: 6121
		internal static readonly PatternMatchRule Rule_FilterOverUnionAll = new PatternMatchRule(new Node(FilterOp.Pattern, new Node[]
		{
			new Node(UnionAllOp.Pattern, new Node[]
			{
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0])
			}),
			new Node(LeafOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(FilterOpRules.ProcessFilterOverSetOp));

		// Token: 0x040017EA RID: 6122
		internal static readonly PatternMatchRule Rule_FilterOverIntersect = new PatternMatchRule(new Node(FilterOp.Pattern, new Node[]
		{
			new Node(IntersectOp.Pattern, new Node[]
			{
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0])
			}),
			new Node(LeafOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(FilterOpRules.ProcessFilterOverSetOp));

		// Token: 0x040017EB RID: 6123
		internal static readonly PatternMatchRule Rule_FilterOverExcept = new PatternMatchRule(new Node(FilterOp.Pattern, new Node[]
		{
			new Node(ExceptOp.Pattern, new Node[]
			{
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0])
			}),
			new Node(LeafOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(FilterOpRules.ProcessFilterOverSetOp));

		// Token: 0x040017EC RID: 6124
		internal static readonly PatternMatchRule Rule_FilterOverDistinct = new PatternMatchRule(new Node(FilterOp.Pattern, new Node[]
		{
			new Node(DistinctOp.Pattern, new Node[]
			{
				new Node(LeafOp.Pattern, new Node[0])
			}),
			new Node(LeafOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(FilterOpRules.ProcessFilterOverDistinct));

		// Token: 0x040017ED RID: 6125
		internal static readonly PatternMatchRule Rule_FilterOverGroupBy = new PatternMatchRule(new Node(FilterOp.Pattern, new Node[]
		{
			new Node(GroupByOp.Pattern, new Node[]
			{
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0])
			}),
			new Node(LeafOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(FilterOpRules.ProcessFilterOverGroupBy));

		// Token: 0x040017EE RID: 6126
		internal static readonly PatternMatchRule Rule_FilterOverCrossJoin = new PatternMatchRule(new Node(FilterOp.Pattern, new Node[]
		{
			new Node(CrossJoinOp.Pattern, new Node[]
			{
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0])
			}),
			new Node(LeafOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(FilterOpRules.ProcessFilterOverJoin));

		// Token: 0x040017EF RID: 6127
		internal static readonly PatternMatchRule Rule_FilterOverInnerJoin = new PatternMatchRule(new Node(FilterOp.Pattern, new Node[]
		{
			new Node(InnerJoinOp.Pattern, new Node[]
			{
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0])
			}),
			new Node(LeafOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(FilterOpRules.ProcessFilterOverJoin));

		// Token: 0x040017F0 RID: 6128
		internal static readonly PatternMatchRule Rule_FilterOverLeftOuterJoin = new PatternMatchRule(new Node(FilterOp.Pattern, new Node[]
		{
			new Node(LeftOuterJoinOp.Pattern, new Node[]
			{
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0])
			}),
			new Node(LeafOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(FilterOpRules.ProcessFilterOverJoin));

		// Token: 0x040017F1 RID: 6129
		internal static readonly PatternMatchRule Rule_FilterOverOuterApply = new PatternMatchRule(new Node(FilterOp.Pattern, new Node[]
		{
			new Node(OuterApplyOp.Pattern, new Node[]
			{
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0])
			}),
			new Node(LeafOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(FilterOpRules.ProcessFilterOverOuterApply));

		// Token: 0x040017F2 RID: 6130
		internal static readonly PatternMatchRule Rule_FilterWithConstantPredicate = new PatternMatchRule(new Node(FilterOp.Pattern, new Node[]
		{
			new Node(LeafOp.Pattern, new Node[0]),
			new Node(ConstantPredicateOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(FilterOpRules.ProcessFilterWithConstantPredicate));

		// Token: 0x040017F3 RID: 6131
		internal static readonly Rule[] Rules = new Rule[]
		{
			FilterOpRules.Rule_FilterWithConstantPredicate,
			FilterOpRules.Rule_FilterOverCrossJoin,
			FilterOpRules.Rule_FilterOverDistinct,
			FilterOpRules.Rule_FilterOverExcept,
			FilterOpRules.Rule_FilterOverFilter,
			FilterOpRules.Rule_FilterOverGroupBy,
			FilterOpRules.Rule_FilterOverInnerJoin,
			FilterOpRules.Rule_FilterOverIntersect,
			FilterOpRules.Rule_FilterOverLeftOuterJoin,
			FilterOpRules.Rule_FilterOverProject,
			FilterOpRules.Rule_FilterOverUnionAll,
			FilterOpRules.Rule_FilterOverOuterApply
		};
	}
}
