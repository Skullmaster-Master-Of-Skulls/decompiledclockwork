using System;
using System.Data.Query.InternalTrees;

namespace System.Data.Query.PlanCompiler
{
	// Token: 0x02000076 RID: 118
	internal static class SingleRowOpRules
	{
		// Token: 0x06000919 RID: 2329 RVA: 0x0003240C File Offset: 0x0003060C
		private static bool ProcessSingleRowOpOverAnything(RuleProcessingContext context, Node singleRowNode, out Node newNode)
		{
			newNode = singleRowNode;
			TransformationRulesContext transformationRulesContext = (TransformationRulesContext)context;
			ExtendedNodeInfo extendedNodeInfo = context.Command.GetExtendedNodeInfo(singleRowNode.Child0);
			if (extendedNodeInfo.MaxRows <= RowCount.One)
			{
				newNode = singleRowNode.Child0;
				return true;
			}
			if (singleRowNode.Child0.Op.OpType == OpType.Filter)
			{
				Predicate predicate = new Predicate(context.Command, singleRowNode.Child0.Child1);
				if (predicate.SatisfiesKey(extendedNodeInfo.Keys.KeyVars, extendedNodeInfo.Definitions))
				{
					extendedNodeInfo.MaxRows = RowCount.One;
					newNode = singleRowNode.Child0;
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x000324A0 File Offset: 0x000306A0
		private static bool ProcessSingleRowOpOverProject(RuleProcessingContext context, Node singleRowNode, out Node newNode)
		{
			newNode = singleRowNode;
			Node child = singleRowNode.Child0;
			Node child2 = child.Child0;
			singleRowNode.Child0 = child2;
			context.Command.RecomputeNodeInfo(singleRowNode);
			child.Child0 = singleRowNode;
			newNode = child;
			return true;
		}

		// Token: 0x0400085B RID: 2139
		internal static readonly PatternMatchRule Rule_SingleRowOpOverAnything = new PatternMatchRule(new Node(SingleRowOp.Pattern, new Node[]
		{
			new Node(LeafOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(SingleRowOpRules.ProcessSingleRowOpOverAnything));

		// Token: 0x0400085C RID: 2140
		internal static readonly PatternMatchRule Rule_SingleRowOpOverProject = new PatternMatchRule(new Node(SingleRowOp.Pattern, new Node[]
		{
			new Node(ProjectOp.Pattern, new Node[]
			{
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0])
			})
		}), new Rule.ProcessNodeDelegate(SingleRowOpRules.ProcessSingleRowOpOverProject));

		// Token: 0x0400085D RID: 2141
		internal static readonly Rule[] Rules = new Rule[]
		{
			SingleRowOpRules.Rule_SingleRowOpOverAnything,
			SingleRowOpRules.Rule_SingleRowOpOverProject
		};
	}
}
