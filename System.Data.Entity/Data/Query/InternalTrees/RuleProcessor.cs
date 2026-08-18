using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000E6 RID: 230
	internal class RuleProcessor
	{
		// Token: 0x06000CBA RID: 3258 RVA: 0x0003C582 File Offset: 0x0003A782
		internal RuleProcessor()
		{
			this.m_processedNodeMap = new Dictionary<SubTreeId, SubTreeId>();
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x0003C598 File Offset: 0x0003A798
		private static bool ApplyRulesToNode(RuleProcessingContext context, ReadOnlyCollection<ReadOnlyCollection<Rule>> rules, Node currentNode, out Node newNode)
		{
			newNode = currentNode;
			context.PreProcess(currentNode);
			foreach (Rule rule in rules[(int)currentNode.Op.OpType])
			{
				if (rule.Match(currentNode) && rule.Apply(context, currentNode, out newNode))
				{
					context.PostProcess(newNode, rule);
					return true;
				}
			}
			context.PostProcess(currentNode, null);
			return false;
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x0003C620 File Offset: 0x0003A820
		private Node ApplyRulesToSubtree(RuleProcessingContext context, ReadOnlyCollection<ReadOnlyCollection<Rule>> rules, Node subTreeRoot, Node parent, int childIndexInParent)
		{
			int num = 0;
			Dictionary<SubTreeId, SubTreeId> dictionary = new Dictionary<SubTreeId, SubTreeId>();
			SubTreeId subTreeId;
			for (;;)
			{
				num++;
				context.PreProcessSubTree(subTreeRoot);
				subTreeId = new SubTreeId(context, subTreeRoot, parent, childIndexInParent);
				if (this.m_processedNodeMap.ContainsKey(subTreeId))
				{
					goto IL_B9;
				}
				if (dictionary.ContainsKey(subTreeId))
				{
					break;
				}
				dictionary[subTreeId] = subTreeId;
				for (int i = 0; i < subTreeRoot.Children.Count; i++)
				{
					subTreeRoot.Children[i] = this.ApplyRulesToSubtree(context, rules, subTreeRoot.Children[i], subTreeRoot, i);
				}
				Node node;
				if (!RuleProcessor.ApplyRulesToNode(context, rules, subTreeRoot, out node))
				{
					goto Block_4;
				}
				context.PostProcessSubTree(subTreeRoot);
				subTreeRoot = node;
			}
			this.m_processedNodeMap[subTreeId] = subTreeId;
			goto IL_B9;
			Block_4:
			this.m_processedNodeMap[subTreeId] = subTreeId;
			IL_B9:
			context.PostProcessSubTree(subTreeRoot);
			return subTreeRoot;
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x0003C6EE File Offset: 0x0003A8EE
		internal Node ApplyRulesToSubtree(RuleProcessingContext context, ReadOnlyCollection<ReadOnlyCollection<Rule>> rules, Node subTreeRoot)
		{
			return this.ApplyRulesToSubtree(context, rules, subTreeRoot, null, 0);
		}

		// Token: 0x04000992 RID: 2450
		private Dictionary<SubTreeId, SubTreeId> m_processedNodeMap;
	}
}
