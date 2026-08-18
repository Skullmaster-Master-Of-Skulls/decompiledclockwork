using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Query.InternalTrees;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x0200069B RID: 1691
	internal static class ScalarOpRules
	{
		// Token: 0x06004307 RID: 17159 RVA: 0x0013D6A8 File Offset: 0x0013B8A8
		private static bool ProcessSimplifyCase(RuleProcessingContext context, Node caseOpNode, out Node newNode)
		{
			CaseOp caseOp = (CaseOp)caseOpNode.Op;
			newNode = caseOpNode;
			return ScalarOpRules.ProcessSimplifyCase_Collapse(caseOpNode, out newNode) || ScalarOpRules.ProcessSimplifyCase_EliminateWhenClauses(context, caseOp, caseOpNode, out newNode);
		}

		// Token: 0x06004308 RID: 17160 RVA: 0x0013D6E0 File Offset: 0x0013B8E0
		private static bool ProcessSimplifyCase_Collapse(Node caseOpNode, out Node newNode)
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

		// Token: 0x06004309 RID: 17161 RVA: 0x0013D750 File Offset: 0x0013B950
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
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

		// Token: 0x0600430A RID: 17162 RVA: 0x0013D8D8 File Offset: 0x0013BAD8
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

		// Token: 0x0600430B RID: 17163 RVA: 0x0013D93C File Offset: 0x0013BB3C
		private static bool ProcessIsNullOverCase(RuleProcessingContext context, Node isNullOpNode, out Node newNode)
		{
			Node child = isNullOpNode.Child0;
			if (child.Children.Count != 3)
			{
				newNode = isNullOpNode;
				return false;
			}
			Node child2 = child.Child0;
			Node child3 = child.Child1;
			Node child4 = child.Child2;
			switch (child3.Op.OpType)
			{
			case OpType.Constant:
			case OpType.InternalConstant:
			case OpType.NullSentinel:
				if (child4.Op.OpType == OpType.Null)
				{
					newNode = context.Command.CreateNode(context.Command.CreateConditionalOp(OpType.Not), child2);
					return true;
				}
				break;
			case OpType.Null:
				switch (child4.Op.OpType)
				{
				case OpType.Constant:
				case OpType.InternalConstant:
				case OpType.NullSentinel:
					newNode = child2;
					return true;
				}
				break;
			}
			newNode = isNullOpNode;
			return false;
		}

		// Token: 0x0600430C RID: 17164 RVA: 0x0013D9F4 File Offset: 0x0013BBF4
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
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

		// Token: 0x0600430D RID: 17165 RVA: 0x0013DA9C File Offset: 0x0013BC9C
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

		// Token: 0x0600430E RID: 17166 RVA: 0x0013DB18 File Offset: 0x0013BD18
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

		// Token: 0x0600430F RID: 17167 RVA: 0x0013DBAC File Offset: 0x0013BDAC
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "OpType")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "constantPredicateOp")]
		private static bool ProcessLogOpOverConstant(RuleProcessingContext context, Node node, Node constantPredicateNode, Node otherNode, out Node newNode)
		{
			PlanCompiler.Assert(constantPredicateNode != null, "null constantPredicateOp?");
			ConstantPredicateOp constantPredicateOp = (ConstantPredicateOp)constantPredicateNode.Op;
			switch (node.Op.OpType)
			{
			case OpType.And:
				newNode = (constantPredicateOp.IsTrue ? otherNode : constantPredicateNode);
				return true;
			case OpType.Or:
				newNode = (constantPredicateOp.IsTrue ? constantPredicateNode : otherNode);
				return true;
			case OpType.Not:
				PlanCompiler.Assert(otherNode == null, "Not Op with more than 1 child. Gasp!");
				newNode = context.Command.CreateNode(context.Command.CreateConstantPredicateOp(!constantPredicateOp.Value));
				return true;
			}
			PlanCompiler.Assert(false, "Unexpected OpType - " + node.Op.OpType);
			newNode = null;
			return true;
		}

		// Token: 0x06004310 RID: 17168 RVA: 0x0013DC76 File Offset: 0x0013BE76
		private static bool ProcessAndOverConstantPredicate1(RuleProcessingContext context, Node node, out Node newNode)
		{
			return ScalarOpRules.ProcessLogOpOverConstant(context, node, node.Child1, node.Child0, out newNode);
		}

		// Token: 0x06004311 RID: 17169 RVA: 0x0013DC8C File Offset: 0x0013BE8C
		private static bool ProcessAndOverConstantPredicate2(RuleProcessingContext context, Node node, out Node newNode)
		{
			return ScalarOpRules.ProcessLogOpOverConstant(context, node, node.Child0, node.Child1, out newNode);
		}

		// Token: 0x06004312 RID: 17170 RVA: 0x0013DCA2 File Offset: 0x0013BEA2
		private static bool ProcessOrOverConstantPredicate1(RuleProcessingContext context, Node node, out Node newNode)
		{
			return ScalarOpRules.ProcessLogOpOverConstant(context, node, node.Child1, node.Child0, out newNode);
		}

		// Token: 0x06004313 RID: 17171 RVA: 0x0013DCB8 File Offset: 0x0013BEB8
		private static bool ProcessOrOverConstantPredicate2(RuleProcessingContext context, Node node, out Node newNode)
		{
			return ScalarOpRules.ProcessLogOpOverConstant(context, node, node.Child0, node.Child1, out newNode);
		}

		// Token: 0x06004314 RID: 17172 RVA: 0x0013DCCE File Offset: 0x0013BECE
		private static bool ProcessNotOverConstantPredicate(RuleProcessingContext context, Node node, out Node newNode)
		{
			return ScalarOpRules.ProcessLogOpOverConstant(context, node, node.Child0, null, out newNode);
		}

		// Token: 0x06004315 RID: 17173 RVA: 0x0013DCDF File Offset: 0x0013BEDF
		private static bool ProcessIsNullOverConstant(RuleProcessingContext context, Node isNullNode, out Node newNode)
		{
			newNode = context.Command.CreateNode(context.Command.CreateFalseOp());
			return true;
		}

		// Token: 0x06004316 RID: 17174 RVA: 0x0013DCFA File Offset: 0x0013BEFA
		private static bool ProcessIsNullOverNull(RuleProcessingContext context, Node isNullNode, out Node newNode)
		{
			newNode = context.Command.CreateNode(context.Command.CreateTrueOp());
			return true;
		}

		// Token: 0x06004317 RID: 17175 RVA: 0x0013DD15 File Offset: 0x0013BF15
		private static bool ProcessNullCast(RuleProcessingContext context, Node castNullOp, out Node newNode)
		{
			newNode = context.Command.CreateNode(context.Command.CreateNullOp(castNullOp.Op.Type));
			return true;
		}

		// Token: 0x06004318 RID: 17176 RVA: 0x0013DD3C File Offset: 0x0013BF3C
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

		// Token: 0x06004319 RID: 17177 RVA: 0x0013DD90 File Offset: 0x0013BF90
		private static bool ProcessIsNullOverAnything(RuleProcessingContext context, Node isNullNode, out Node newNode)
		{
			Command command = context.Command;
			OpType opType = isNullNode.Child0.Op.OpType;
			if (opType != OpType.Cast)
			{
				if (opType != OpType.Function)
				{
					newNode = isNullNode;
				}
				else
				{
					EdmFunction function = ((FunctionOp)isNullNode.Child0.Op).Function;
					newNode = (ScalarOpRules.PreservesNulls(function) ? command.CreateNode(command.CreateConditionalOp(OpType.IsNull), isNullNode.Child0.Child0) : isNullNode);
				}
			}
			else
			{
				newNode = command.CreateNode(command.CreateConditionalOp(OpType.IsNull), isNullNode.Child0.Child0);
			}
			switch (isNullNode.Child0.Op.OpType)
			{
			case OpType.Constant:
			case OpType.InternalConstant:
			case OpType.NullSentinel:
				return ScalarOpRules.ProcessIsNullOverConstant(context, newNode, out newNode);
			case OpType.Null:
				return ScalarOpRules.ProcessIsNullOverNull(context, newNode, out newNode);
			case OpType.VarRef:
				return ScalarOpRules.ProcessIsNullOverVarRef(context, newNode, out newNode);
			}
			return !object.ReferenceEquals(isNullNode, newNode);
		}

		// Token: 0x0600431A RID: 17178 RVA: 0x0013DE79 File Offset: 0x0013C079
		private static bool PreservesNulls(EdmFunction function)
		{
			return function.FullName == "Edm.Length";
		}

		// Token: 0x040018C2 RID: 6338
		internal static readonly SimpleRule Rule_SimplifyCase = new SimpleRule(OpType.Case, new Rule.ProcessNodeDelegate(ScalarOpRules.ProcessSimplifyCase));

		// Token: 0x040018C3 RID: 6339
		internal static readonly SimpleRule Rule_FlattenCase = new SimpleRule(OpType.Case, new Rule.ProcessNodeDelegate(ScalarOpRules.ProcessFlattenCase));

		// Token: 0x040018C4 RID: 6340
		internal static readonly PatternMatchRule Rule_IsNullOverCase = new PatternMatchRule(new Node(ConditionalOp.PatternIsNull, new Node[]
		{
			new Node(CaseOp.Pattern, new Node[]
			{
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0])
			})
		}), new Rule.ProcessNodeDelegate(ScalarOpRules.ProcessIsNullOverCase));

		// Token: 0x040018C5 RID: 6341
		internal static readonly PatternMatchRule Rule_EqualsOverConstant = new PatternMatchRule(new Node(ComparisonOp.PatternEq, new Node[]
		{
			new Node(InternalConstantOp.Pattern, new Node[0]),
			new Node(InternalConstantOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(ScalarOpRules.ProcessComparisonsOverConstant));

		// Token: 0x040018C6 RID: 6342
		internal static readonly PatternMatchRule Rule_LikeOverConstants = new PatternMatchRule(new Node(LikeOp.Pattern, new Node[]
		{
			new Node(InternalConstantOp.Pattern, new Node[0]),
			new Node(InternalConstantOp.Pattern, new Node[0]),
			new Node(NullOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(ScalarOpRules.ProcessLikeOverConstant));

		// Token: 0x040018C7 RID: 6343
		internal static readonly PatternMatchRule Rule_AndOverConstantPred1 = new PatternMatchRule(new Node(ConditionalOp.PatternAnd, new Node[]
		{
			new Node(LeafOp.Pattern, new Node[0]),
			new Node(ConstantPredicateOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(ScalarOpRules.ProcessAndOverConstantPredicate1));

		// Token: 0x040018C8 RID: 6344
		internal static readonly PatternMatchRule Rule_AndOverConstantPred2 = new PatternMatchRule(new Node(ConditionalOp.PatternAnd, new Node[]
		{
			new Node(ConstantPredicateOp.Pattern, new Node[0]),
			new Node(LeafOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(ScalarOpRules.ProcessAndOverConstantPredicate2));

		// Token: 0x040018C9 RID: 6345
		internal static readonly PatternMatchRule Rule_OrOverConstantPred1 = new PatternMatchRule(new Node(ConditionalOp.PatternOr, new Node[]
		{
			new Node(LeafOp.Pattern, new Node[0]),
			new Node(ConstantPredicateOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(ScalarOpRules.ProcessOrOverConstantPredicate1));

		// Token: 0x040018CA RID: 6346
		internal static readonly PatternMatchRule Rule_OrOverConstantPred2 = new PatternMatchRule(new Node(ConditionalOp.PatternOr, new Node[]
		{
			new Node(ConstantPredicateOp.Pattern, new Node[0]),
			new Node(LeafOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(ScalarOpRules.ProcessOrOverConstantPredicate2));

		// Token: 0x040018CB RID: 6347
		internal static readonly PatternMatchRule Rule_NotOverConstantPred = new PatternMatchRule(new Node(ConditionalOp.PatternNot, new Node[]
		{
			new Node(ConstantPredicateOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(ScalarOpRules.ProcessNotOverConstantPredicate));

		// Token: 0x040018CC RID: 6348
		internal static readonly PatternMatchRule Rule_IsNullOverConstant = new PatternMatchRule(new Node(ConditionalOp.PatternIsNull, new Node[]
		{
			new Node(InternalConstantOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(ScalarOpRules.ProcessIsNullOverConstant));

		// Token: 0x040018CD RID: 6349
		internal static readonly PatternMatchRule Rule_IsNullOverNullSentinel = new PatternMatchRule(new Node(ConditionalOp.PatternIsNull, new Node[]
		{
			new Node(NullSentinelOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(ScalarOpRules.ProcessIsNullOverConstant));

		// Token: 0x040018CE RID: 6350
		internal static readonly PatternMatchRule Rule_IsNullOverNull = new PatternMatchRule(new Node(ConditionalOp.PatternIsNull, new Node[]
		{
			new Node(NullOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(ScalarOpRules.ProcessIsNullOverNull));

		// Token: 0x040018CF RID: 6351
		internal static readonly PatternMatchRule Rule_NullCast = new PatternMatchRule(new Node(CastOp.Pattern, new Node[]
		{
			new Node(NullOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(ScalarOpRules.ProcessNullCast));

		// Token: 0x040018D0 RID: 6352
		internal static readonly PatternMatchRule Rule_IsNullOverVarRef = new PatternMatchRule(new Node(ConditionalOp.PatternIsNull, new Node[]
		{
			new Node(VarRefOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(ScalarOpRules.ProcessIsNullOverVarRef));

		// Token: 0x040018D1 RID: 6353
		internal static readonly PatternMatchRule Rule_IsNullOverAnything = new PatternMatchRule(new Node(ConditionalOp.PatternIsNull, new Node[]
		{
			new Node(LeafOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(ScalarOpRules.ProcessIsNullOverAnything));

		// Token: 0x040018D2 RID: 6354
		internal static readonly Rule[] Rules = new Rule[]
		{
			ScalarOpRules.Rule_IsNullOverCase,
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
