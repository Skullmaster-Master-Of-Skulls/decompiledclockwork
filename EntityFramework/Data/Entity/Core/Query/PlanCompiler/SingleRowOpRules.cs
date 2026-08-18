using System;
using System.Data.Entity.Core.Query.InternalTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x0200069E RID: 1694
	internal static class SingleRowOpRules
	{
		// Token: 0x06004323 RID: 17187 RVA: 0x0013E574 File Offset: 0x0013C774
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

		// Token: 0x06004324 RID: 17188 RVA: 0x0013E608 File Offset: 0x0013C808
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

		// Token: 0x040018D8 RID: 6360
		internal static readonly PatternMatchRule Rule_SingleRowOpOverAnything = new PatternMatchRule(new Node(SingleRowOp.Pattern, new Node[]
		{
			new Node(LeafOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(SingleRowOpRules.ProcessSingleRowOpOverAnything));

		// Token: 0x040018D9 RID: 6361
		internal static readonly PatternMatchRule Rule_SingleRowOpOverProject = new PatternMatchRule(new Node(SingleRowOp.Pattern, new Node[]
		{
			new Node(ProjectOp.Pattern, new Node[]
			{
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0])
			})
		}), new Rule.ProcessNodeDelegate(SingleRowOpRules.ProcessSingleRowOpOverProject));

		// Token: 0x040018DA RID: 6362
		internal static readonly Rule[] Rules = new Rule[]
		{
			SingleRowOpRules.Rule_SingleRowOpOverAnything,
			SingleRowOpRules.Rule_SingleRowOpOverProject
		};
	}
}
