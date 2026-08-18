using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x02000642 RID: 1602
	internal class RuleProcessor
	{
		// Token: 0x06003ED5 RID: 16085 RVA: 0x00120107 File Offset: 0x0011E307
		internal RuleProcessor()
		{
			this.m_processedNodeMap = new Dictionary<SubTreeId, SubTreeId>();
		}

		// Token: 0x06003ED6 RID: 16086 RVA: 0x0012011C File Offset: 0x0011E31C
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

		// Token: 0x06003ED7 RID: 16087 RVA: 0x001201A4 File Offset: 0x0011E3A4
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
					goto IL_C1;
				}
				if (dictionary.ContainsKey(subTreeId))
				{
					break;
				}
				dictionary[subTreeId] = subTreeId;
				for (int i = 0; i < subTreeRoot.Children.Count; i++)
				{
					Node node = subTreeRoot.Children[i];
					if (RuleProcessor.ShouldApplyRules(node, subTreeRoot))
					{
						subTreeRoot.Children[i] = this.ApplyRulesToSubtree(context, rules, node, subTreeRoot, i);
					}
				}
				Node node2;
				if (!RuleProcessor.ApplyRulesToNode(context, rules, subTreeRoot, out node2))
				{
					goto Block_5;
				}
				context.PostProcessSubTree(subTreeRoot);
				subTreeRoot = node2;
			}
			this.m_processedNodeMap[subTreeId] = subTreeId;
			goto IL_C1;
			Block_5:
			this.m_processedNodeMap[subTreeId] = subTreeId;
			IL_C1:
			context.PostProcessSubTree(subTreeRoot);
			return subTreeRoot;
		}

		// Token: 0x06003ED8 RID: 16088 RVA: 0x0012027A File Offset: 0x0011E47A
		private static bool ShouldApplyRules(Node node, Node parent)
		{
			return parent.Op.OpType != OpType.In || node.Op.OpType != OpType.Constant;
		}

		// Token: 0x06003ED9 RID: 16089 RVA: 0x0012029E File Offset: 0x0011E49E
		internal Node ApplyRulesToSubtree(RuleProcessingContext context, ReadOnlyCollection<ReadOnlyCollection<Rule>> rules, Node subTreeRoot)
		{
			return this.ApplyRulesToSubtree(context, rules, subTreeRoot, null, 0);
		}

		// Token: 0x04001780 RID: 6016
		private readonly Dictionary<SubTreeId, SubTreeId> m_processedNodeMap;
	}
}
