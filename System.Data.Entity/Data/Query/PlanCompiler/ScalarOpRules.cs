using System;
using System.Collections.Generic;
using System.Data.Query.InternalTrees;

namespace System.Data.Query.PlanCompiler
{
	// Token: 0x02000071 RID: 113
	internal static class ScalarOpRules
	{
		// Token: 0x060008EB RID: 2283 RVA: 0x0002EBA4 File Offset: 0x0002CDA4
		private static bool ProcessSimplifyCase(RuleProcessingContext context, Node caseOpNode, out Node newNode)
		{
			CaseOp caseOp = (CaseOp)caseOpNode.Op;
			newNode = caseOpNode;
			return ScalarOpRules.ProcessSimplifyCase_Collapse(caseOp, caseOpNode, out newNode) || ScalarOpRules.ProcessSimplifyCase_EliminateWhenClauses(context, caseOp, caseOpNode, out newNode);
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x0002EBDC File Offset: 0x0002CDDC
		private static bool ProcessSimplifyCase_Collapse(CaseOp caseOp, Node caseOpNode, out Node newNode)
		{
			newNode = caseOpNode;
			Node child = caseOpNode.Child1;
			Node other = caseOpNode.Children[caseOpNode.Children.Count - 1];
			if (!child.IsEquivalent(other))
			{
				return false;
			}
			for (int i = 3; i < caseOpNode.Children.Count - 1; i += 2)
			{
				if (!caseOpNode.Children[i].IsEquivalent(child))
				{
					return false;
				}
			}
			newNode = child;
			return true;
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x0002EC4C File Offset: 0x0002CE4C
		private static bool ProcessSimplifyCase_EliminateWhenClauses(RuleProcessingContext context, CaseOp caseOp, Node caseOpNode, out Node newNode)
		{
			List<Node> list = null;
			newNode = caseOpNode;
			int i = 0;
			while (i < caseOpNode.Children.Count)
			{
				if (i == caseOpNode.Children.Count - 1)
				{
					if (OpType.SoftCast == caseOpNode.Children[i].Op.OpType)
					{
						return false;
					}
					if (list != null)
					{
						list.Add(caseOpNode.Children[i]);
						break;
					}
					break;
				}
				else
				{
					if (OpType.SoftCast == caseOpNode.Children[i + 1].Op.OpType)
					{
						return false;
					}
					if (caseOpNode.Children[i].Op.OpType != OpType.ConstantPredicate)
					{
						if (list != null)
						{
							list.Add(caseOpNode.Children[i]);
							list.Add(caseOpNode.Children[i + 1]);
						}
						i += 2;
					}
					else
					{
						ConstantPredicateOp constantPredicateOp = (ConstantPredicateOp)caseOpNode.Children[i].Op;
						if (list == null)
						{
							list = new List<Node>();
							for (int j = 0; j < i; j++)
							{
								list.Add(caseOpNode.Children[j]);
							}
						}
						if (constantPredicateOp.IsTrue)
						{
							list.Add(caseOpNode.Children[i + 1]);
							break;
						}
						PlanCompiler.Assert(constantPredicateOp.IsFalse, "constant predicate must be either true or false");
						i += 2;
					}
				}
			}
			if (list == null)
			{
				return false;
			}
			PlanCompiler.Assert(list.Count > 0, "new args list must not be empty");
			if (list.Count == 1)
			{
				newNode = list[0];
			}
			else
			{
				newNode = context.Command.CreateNode(caseOp, list);
			}
			return true;
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x0002EDD4 File Offset: 0x0002CFD4
		private static bool ProcessFlattenCase(RuleProcessingContext context, Node caseOpNode, out Node newNode)
		{
			newNode = caseOpNode;
			Node node = caseOpNode.Children[caseOpNode.Children.Count - 1];
			if (node.Op.OpType != OpType.Case)
			{
				return false;
			}
			caseOpNode.Children.RemoveAt(caseOpNode.Children.Count - 1);
			caseOpNode.Children.AddRange(node.Children);
			return true;
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x0002EE38 File Offset: 0x0002D038
		private static bool ProcessComparisonsOverConstant(RuleProcessingContext context, Node node, out Node newNode)
		{
			newNode = node;
			PlanCompiler.Assert(node.Op.OpType == OpType.EQ || node.Op.OpType == OpType.NE, "unexpected comparison op type?");
			bool? flag = new bool?(node.Child0.Op.IsEquivalent(node.Child1.Op));
			if (flag == null)
			{
				return false;
			}
			bool value = (node.Op.OpType == OpType.EQ) ? flag.Value : (!flag.Value);
			ConstantPredicateOp op = context.Command.CreateConstantPredicateOp(value);
			newNode = context.Command.CreateNode(op);
			return true;
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x0002EEE0 File Offset: 0x0002D0E0
		private static bool? MatchesPattern(string str, string pattern)
		{
			int num = pattern.IndexOf('%');
			if (num == -1 || num != pattern.Length - 1 || pattern.Length > str.Length + 1)
			{
				return null;
			}
			bool value = true;
			int num2 = 0;
			while (num2 < str.Length && num2 < pattern.Length - 1)
			{
				if (pattern[num2] != str[num2])
				{
					value = false;
					break;
				}
				num2++;
			}
			return new bool?(value);
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x0002EF5C File Offset: 0x0002D15C
		private static bool ProcessLikeOverConstant(RuleProcessingContext context, Node n, out Node newNode)
		{
			newNode = n;
			InternalConstantOp internalConstantOp = (InternalConstantOp)n.Child1.Op;
			InternalConstantOp internalConstantOp2 = (InternalConstantOp)n.Child0.Op;
			string text = (string)internalConstantOp2.Value;
			string text2 = (string)internalConstantOp.Value;
			bool? flag = ScalarOpRules.MatchesPattern((string)internalConstantOp2.Value, (string)internalConstantOp.Value);
			if (flag == null)
			{
				return false;
			}
			ConstantPredicateOp op = context.Command.CreateConstantPredicateOp(flag.Value);
			newNode = context.Command.CreateNode(op);
			return true;
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x0002EFF4 File Offset: 0x0002D1F4
		private static bool ProcessLogOpOverConstant(RuleProcessingContext context, Node node, Node constantPredicateNode, Node otherNode, out Node newNode)
		{
			PlanCompiler.Assert(constantPredicateNode != null, "null constantPredicateOp?");
			ConstantPredicateOp constantPredicateOp = (ConstantPredicateOp)constantPredicateNode.Op;
			switch (node.Op.OpType)
			{
			case OpType.And:
				newNode = (constantPredicateOp.IsTrue ? otherNode : constantPredicateNode);
				break;
			case OpType.Or:
				newNode = (constantPredicateOp.IsTrue ? constantPredicateNode : otherNode);
				break;
			case OpType.Not:
				PlanCompiler.Assert(otherNode == null, "Not Op with more than 1 child. Gasp!");
				newNode = context.Command.CreateNode(context.Command.CreateConstantPredicateOp(!constantPredicateOp.Value));
				break;
			default:
				PlanCompiler.Assert(false, "Unexpected OpType - " + node.Op.OpType.ToString());
				newNode = null;
				break;
			}
			return true;
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x0002F0C0 File Offset: 0x0002D2C0
		private static bool ProcessAndOverConstantPredicate1(RuleProcessingContext context, Node node, out Node newNode)
		{
			return ScalarOpRules.ProcessLogOpOverConstant(context, node, node.Child1, node.Child0, out newNode);
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x0002F0D6 File Offset: 0x0002D2D6
		private static bool ProcessAndOverConstantPredicate2(RuleProcessingContext context, Node node, out Node newNode)
		{
			return ScalarOpRules.ProcessLogOpOverConstant(context, node, node.Child0, node.Child1, out newNode);
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x0002F0C0 File Offset: 0x0002D2C0
		private static bool ProcessOrOverConstantPredicate1(RuleProcessingContext context, Node node, out Node newNode)
		{
			return ScalarOpRules.ProcessLogOpOverConstant(context, node, node.Child1, node.Child0, out newNode);
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x0002F0D6 File Offset: 0x0002D2D6
		private static bool ProcessOrOverConstantPredicate2(RuleProcessingContext context, Node node, out Node newNode)
		{
			return ScalarOpRules.ProcessLogOpOverConstant(context, node, node.Child0, node.Child1, out newNode);
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x0002F0EC File Offset: 0x0002D2EC
		private static bool ProcessNotOverConstantPredicate(RuleProcessingContext context, Node node, out Node newNode)
		{
			return ScalarOpRules.ProcessLogOpOverConstant(context, node, node.Child0, null, out newNode);
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x0002F0FD File Offset: 0x0002D2FD
		private static bool ProcessIsNullOverConstant(RuleProcessingContext context, Node isNullNode, out Node newNode)
		{
			newNode = context.Command.CreateNode(context.Command.CreateFalseOp());
			return true;
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x0002F118 File Offset: 0x0002D318
		private static bool ProcessIsNullOverNull(RuleProcessingContext context, Node isNullNode, out Node newNode)
		{
			newNode = context.Command.CreateNode(context.Command.CreateTrueOp());
			return true;
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x0002F133 File Offset: 0x0002D333
		private static bool ProcessNullCast(RuleProcessingContext context, Node castNullOp, out Node newNode)
		{
			newNode = context.Command.CreateNode(context.Command.CreateNullOp(castNullOp.Op.Type));
			return true;
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x0002F15C File Offset: 0x0002D35C
		private static bool ProcessIsNullOverVarRef(RuleProcessingContext context, Node isNullNode, out Node newNode)
		{
			Command command = context.Command;
			TransformationRulesContext transformationRulesContext = (TransformationRulesContext)context;
			Var var = ((VarRefOp)isNullNode.Child0.Op).Var;
			if (transformationRulesContext.IsNonNullable(var))
			{
				newNode = command.CreateNode(context.Command.CreateFalseOp());
				return true;
			}
			newNode = isNullNode;
			return false;
		}

		// Token: 0x04000820 RID: 2080
		internal static readonly SimpleRule Rule_SimplifyCase = new SimpleRule(OpType.Case, new Rule.ProcessNodeDelegate(ScalarOpRules.ProcessSimplifyCase));

		// Token: 0x04000821 RID: 2081
		internal static readonly SimpleRule Rule_FlattenCase = new SimpleRule(OpType.Case, new Rule.ProcessNodeDelegate(ScalarOpRules.ProcessFlattenCase));

		// Token: 0x04000822 RID: 2082
		internal static readonly PatternMatchRule Rule_EqualsOverConstant = new PatternMatchRule(new Node(ComparisonOp.PatternEq, new Node[]
		{
			new Node(InternalConstantOp.Pattern, new Node[0]),
			new Node(InternalConstantOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(ScalarOpRules.ProcessComparisonsOverConstant));

		// Token: 0x04000823 RID: 2083
		internal static readonly PatternMatchRule Rule_LikeOverConstants = new PatternMatchRule(new Node(LikeOp.Pattern, new Node[]
		{
			new Node(InternalConstantOp.Pattern, new Node[0]),
			new Node(InternalConstantOp.Pattern, new Node[0]),
			new Node(NullOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(ScalarOpRules.ProcessLikeOverConstant));

		// Token: 0x04000824 RID: 2084
		internal static readonly PatternMatchRule Rule_AndOverConstantPred1 = new PatternMatchRule(new Node(ConditionalOp.PatternAnd, new Node[]
		{
			new Node(LeafOp.Pattern, new Node[0]),
			new Node(ConstantPredicateOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(ScalarOpRules.ProcessAndOverConstantPredicate1));

		// Token: 0x04000825 RID: 2085
		internal static readonly PatternMatchRule Rule_AndOverConstantPred2 = new PatternMatchRule(new Node(ConditionalOp.PatternAnd, new Node[]
		{
			new Node(ConstantPredicateOp.Pattern, new Node[0]),
			new Node(LeafOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(ScalarOpRules.ProcessAndOverConstantPredicate2));

		// Token: 0x04000826 RID: 2086
		internal static readonly PatternMatchRule Rule_OrOverConstantPred1 = new PatternMatchRule(new Node(ConditionalOp.PatternOr, new Node[]
		{
			new Node(LeafOp.Pattern, new Node[0]),
			new Node(ConstantPredicateOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(ScalarOpRules.ProcessOrOverConstantPredicate1));

		// Token: 0x04000827 RID: 2087
		internal static readonly PatternMatchRule Rule_OrOverConstantPred2 = new PatternMatchRule(new Node(ConditionalOp.PatternOr, new Node[]
		{
			new Node(ConstantPredicateOp.Pattern, new Node[0]),
			new Node(LeafOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(ScalarOpRules.ProcessOrOverConstantPredicate2));

		// Token: 0x04000828 RID: 2088
		internal static readonly PatternMatchRule Rule_NotOverConstantPred = new PatternMatchRule(new Node(ConditionalOp.PatternNot, new Node[]
		{
			new Node(ConstantPredicateOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(ScalarOpRules.ProcessNotOverConstantPredicate));

		// Token: 0x04000829 RID: 2089
		internal static readonly PatternMatchRule Rule_IsNullOverConstant = new PatternMatchRule(new Node(ConditionalOp.PatternIsNull, new Node[]
		{
			new Node(InternalConstantOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(ScalarOpRules.ProcessIsNullOverConstant));

		// Token: 0x0400082A RID: 2090
		internal static readonly PatternMatchRule Rule_IsNullOverNullSentinel = new PatternMatchRule(new Node(ConditionalOp.PatternIsNull, new Node[]
		{
			new Node(NullSentinelOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(ScalarOpRules.ProcessIsNullOverConstant));

		// Token: 0x0400082B RID: 2091
		internal static readonly PatternMatchRule Rule_IsNullOverNull = new PatternMatchRule(new Node(ConditionalOp.PatternIsNull, new Node[]
		{
			new Node(NullOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(ScalarOpRules.ProcessIsNullOverNull));

		// Token: 0x0400082C RID: 2092
		internal static readonly PatternMatchRule Rule_NullCast = new PatternMatchRule(new Node(CastOp.Pattern, new Node[]
		{
			new Node(NullOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(ScalarOpRules.ProcessNullCast));

		// Token: 0x0400082D RID: 2093
		internal static readonly PatternMatchRule Rule_IsNullOverVarRef = new PatternMatchRule(new Node(ConditionalOp.PatternIsNull, new Node[]
		{
			new Node(VarRefOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(ScalarOpRules.ProcessIsNullOverVarRef));

		// Token: 0x0400082E RID: 2094
		internal static readonly Rule[] Rules = new Rule[]
		{
			ScalarOpRules.Rule_SimplifyCase,
			ScalarOpRules.Rule_FlattenCase,
			ScalarOpRules.Rule_LikeOverConstants,
			ScalarOpRules.Rule_EqualsOverConstant,
			ScalarOpRules.Rule_AndOverConstantPred1,
			ScalarOpRules.Rule_AndOverConstantPred2,
			ScalarOpRules.Rule_OrOverConstantPred1,
			ScalarOpRules.Rule_OrOverConstantPred2,
			ScalarOpRules.Rule_NotOverConstantPred,
			ScalarOpRules.Rule_IsNullOverConstant,
			ScalarOpRules.Rule_IsNullOverNullSentinel,
			ScalarOpRules.Rule_IsNullOverNull,
			ScalarOpRules.Rule_NullCast,
			ScalarOpRules.Rule_IsNullOverVarRef
		};
	}
}
