using System;
using System.Collections.Generic;
using System.Linq;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000695 RID: 1685
	internal class RequiredAggregateDescriptionsGenerator
	{
		// Token: 0x06003D1D RID: 15645 RVA: 0x000C4C24 File Offset: 0x000C2E24
		public RequiredAggregateDescriptionsGenerator(IAggregateDescriptionsGenerator generator)
		{
			this.aggregateDescriptionGenerator = generator;
		}

		// Token: 0x06003D1E RID: 15646 RVA: 0x000C4C54 File Offset: 0x000C2E54
		public AggregateDescriptionInfo[] AddRequiredAggregateDescriptions(List<AggregateDescriptionBase> aggregateDescriptions)
		{
			Dictionary<RequiredField, FieldSolveOrderNode> dictionary = new Dictionary<RequiredField, FieldSolveOrderNode>();
			HashSet<RequiredField> hashSet = new HashSet<RequiredField>();
			HashSet<RequiredField> hashSet2 = new HashSet<RequiredField>();
			HashSet<RequiredField> hashSet3 = new HashSet<RequiredField>();
			for (int i = 0; i < aggregateDescriptions.Count; i++)
			{
				AggregateDescriptionBase aggregateDescriptionBase = aggregateDescriptions[i];
				RequiredField requiredField = aggregateDescriptionBase.GetRequiredField();
				hashSet2.Add(requiredField);
				hashSet3.Add(requiredField);
				FieldSolveOrderNode fieldSolveOrderNode;
				if (!dictionary.TryGetValue(requiredField, out fieldSolveOrderNode))
				{
					fieldSolveOrderNode = new FieldSolveOrderNode();
					dictionary.Add(requiredField, fieldSolveOrderNode);
				}
				ICalculatedAggregateDescription calculatedAggregateDescription = aggregateDescriptions[i] as ICalculatedAggregateDescription;
				if (calculatedAggregateDescription != null)
				{
					foreach (RequiredField requiredField2 in calculatedAggregateDescription.CalculatedField.RequiredFields())
					{
						hashSet.Add(requiredField2);
						FieldSolveOrderNode fieldSolveOrderNode2;
						if (!dictionary.TryGetValue(requiredField2, out fieldSolveOrderNode2))
						{
							fieldSolveOrderNode2 = new FieldSolveOrderNode();
							dictionary.Add(requiredField2, fieldSolveOrderNode2);
						}
						fieldSolveOrderNode2.Parents.Add(fieldSolveOrderNode);
						fieldSolveOrderNode.Children.Add(fieldSolveOrderNode2);
					}
				}
			}
			hashSet.ExceptWith(hashSet2);
			foreach (RequiredField calculatedFieldSettings in hashSet.ToList<RequiredField>())
			{
				this.GenerateDescriptions(calculatedFieldSettings, dictionary, hashSet, aggregateDescriptions, hashSet3);
			}
			List<FieldSolveOrderNode> graph = (from n in dictionary
			select n.Value).ToList<FieldSolveOrderNode>();
			RequiredAggregateDescriptionsGenerator.ResolveLoopErrorsInGraph(graph);
			IList<FieldSolveOrderNode> list = RequiredAggregateDescriptionsGenerator.SolveCalculationOrder(graph);
			List<FieldSolveOrderNode> list2 = new List<FieldSolveOrderNode>(list.Count);
			for (int j = 0; j < list.Count; j++)
			{
				list2.Add(list[list.Count - j - 1]);
			}
			List<AggregateDescriptionInfo> list3 = new List<AggregateDescriptionInfo>(aggregateDescriptions.Count);
			for (int k = 0; k < aggregateDescriptions.Count; k++)
			{
				RequiredField requiredField3 = aggregateDescriptions[k].GetRequiredField();
				FieldSolveOrderNode fieldSolveOrderNode3 = dictionary[requiredField3];
				AggregateDescriptionInfo item = new AggregateDescriptionInfo
				{
					OriginalIndex = k,
					IsCalculated = requiredField3.IsCalculated,
					SolveOrder = list2.IndexOf(fieldSolveOrderNode3),
					IsError = fieldSolveOrderNode3.IsError,
					LocalCalculatedFieldSettings = requiredField3
				};
				list3.Add(item);
			}
			list3.Sort((AggregateDescriptionInfo left, AggregateDescriptionInfo right) => left.SolveOrder.CompareTo(right.SolveOrder));
			return list3.ToArray();
		}

		// Token: 0x06003D1F RID: 15647 RVA: 0x000C4EF8 File Offset: 0x000C30F8
		private void GenerateDescriptions(RequiredField calculatedFieldSettings, Dictionary<RequiredField, FieldSolveOrderNode> fieldToNodeDictionary, HashSet<RequiredField> calculatedMeasures, List<AggregateDescriptionBase> aggregateDescriptions, HashSet<RequiredField> visitedMeasures)
		{
			if (visitedMeasures.Contains(calculatedFieldSettings))
			{
				return;
			}
			AggregateDescriptionBase aggregateDescriptionBase = this.aggregateDescriptionGenerator.GenerateAggregateDescription(calculatedFieldSettings);
			bool initialized = ((IInitializeDescription)aggregateDescriptionBase).Initialized;
			aggregateDescriptions.Add(aggregateDescriptionBase);
			FieldSolveOrderNode fieldSolveOrderNode = null;
			if (fieldToNodeDictionary.TryGetValue(calculatedFieldSettings, out fieldSolveOrderNode))
			{
				fieldSolveOrderNode.IsError = (fieldSolveOrderNode.IsError || !initialized);
			}
			visitedMeasures.Add(calculatedFieldSettings);
			if (calculatedFieldSettings.IsCalculated)
			{
				FieldSolveOrderNode fieldSolveOrderNode2 = null;
				if (!fieldToNodeDictionary.TryGetValue(calculatedFieldSettings, out fieldSolveOrderNode2))
				{
					fieldSolveOrderNode2 = new FieldSolveOrderNode();
					fieldToNodeDictionary.Add(calculatedFieldSettings, fieldSolveOrderNode2);
				}
				fieldSolveOrderNode2.IsError = (fieldSolveOrderNode2.IsError || !initialized);
				foreach (RequiredField requiredField in ((ICalculatedAggregateDescription)aggregateDescriptionBase).CalculatedField.RequiredFields())
				{
					calculatedMeasures.Add(requiredField);
					FieldSolveOrderNode fieldSolveOrderNode3 = null;
					if (!fieldToNodeDictionary.TryGetValue(requiredField, out fieldSolveOrderNode3))
					{
						fieldSolveOrderNode3 = new FieldSolveOrderNode();
						fieldToNodeDictionary.Add(requiredField, fieldSolveOrderNode3);
					}
					fieldSolveOrderNode3.Parents.Add(fieldSolveOrderNode2);
					fieldSolveOrderNode2.Children.Add(fieldSolveOrderNode3);
					this.GenerateDescriptions(requiredField, fieldToNodeDictionary, calculatedMeasures, aggregateDescriptions, visitedMeasures);
				}
			}
		}

		// Token: 0x06003D20 RID: 15648 RVA: 0x000C5034 File Offset: 0x000C3234
		private static void ResolveLoopErrorsInGraph(IList<FieldSolveOrderNode> graph)
		{
			List<HashSet<FieldSolveOrderNode>> list = RequiredAggregateDescriptionsGenerator.Tarjan(graph);
			foreach (HashSet<FieldSolveOrderNode> hashSet in list)
			{
				if (hashSet.Count == 1)
				{
					FieldSolveOrderNode fieldSolveOrderNode = hashSet.First<FieldSolveOrderNode>();
					if (fieldSolveOrderNode.Children.Contains(fieldSolveOrderNode))
					{
						RequiredAggregateDescriptionsGenerator.PropagateErrors(fieldSolveOrderNode);
					}
				}
				else
				{
					foreach (FieldSolveOrderNode node in hashSet)
					{
						RequiredAggregateDescriptionsGenerator.PropagateErrors(node);
					}
				}
			}
		}

		// Token: 0x06003D21 RID: 15649 RVA: 0x000C50E8 File Offset: 0x000C32E8
		private static void PropagateErrors(FieldSolveOrderNode node)
		{
			if (!node.IsError)
			{
				node.IsError = true;
				foreach (FieldSolveOrderNode node2 in node.Parents)
				{
					RequiredAggregateDescriptionsGenerator.PropagateErrors(node2);
				}
			}
		}

		// Token: 0x06003D22 RID: 15650 RVA: 0x000C5144 File Offset: 0x000C3344
		private static List<HashSet<FieldSolveOrderNode>> Tarjan(IList<FieldSolveOrderNode> graph)
		{
			int num = 0;
			Stack<FieldSolveOrderNode> stack = new Stack<FieldSolveOrderNode>();
			List<HashSet<FieldSolveOrderNode>> result = new List<HashSet<FieldSolveOrderNode>>();
			foreach (FieldSolveOrderNode fieldSolveOrderNode in graph)
			{
				if (fieldSolveOrderNode.Index == null)
				{
					RequiredAggregateDescriptionsGenerator.StrongConnect(fieldSolveOrderNode, ref num, ref stack, ref result);
				}
			}
			return result;
		}

		// Token: 0x06003D23 RID: 15651 RVA: 0x000C51B8 File Offset: 0x000C33B8
		private static void StrongConnect(FieldSolveOrderNode v, ref int index, ref Stack<FieldSolveOrderNode> stack, ref List<HashSet<FieldSolveOrderNode>> stronglyConnectedComponents)
		{
			v.Index = new int?(index);
			v.LowLink = index;
			index++;
			stack.Push(v);
			foreach (FieldSolveOrderNode fieldSolveOrderNode in v.Children)
			{
				if (fieldSolveOrderNode.Index == null)
				{
					RequiredAggregateDescriptionsGenerator.StrongConnect(fieldSolveOrderNode, ref index, ref stack, ref stronglyConnectedComponents);
					v.LowLink = Math.Min(v.LowLink, fieldSolveOrderNode.LowLink);
				}
				else if (stack.Contains(fieldSolveOrderNode))
				{
					v.LowLink = Math.Min(v.LowLink, fieldSolveOrderNode.Index.Value);
				}
			}
			if (v.LowLink == v.Index)
			{
				HashSet<FieldSolveOrderNode> hashSet = new HashSet<FieldSolveOrderNode>();
				FieldSolveOrderNode fieldSolveOrderNode2;
				do
				{
					fieldSolveOrderNode2 = stack.Pop();
					hashSet.Add(fieldSolveOrderNode2);
				}
				while (fieldSolveOrderNode2 != v);
				stronglyConnectedComponents.Add(hashSet);
			}
		}

		// Token: 0x06003D24 RID: 15652 RVA: 0x000C52E4 File Offset: 0x000C34E4
		private static IList<FieldSolveOrderNode> SolveCalculationOrder(List<FieldSolveOrderNode> graph)
		{
			List<FieldSolveOrderNode> list = new List<FieldSolveOrderNode>();
			foreach (FieldSolveOrderNode fieldSolveOrderNode in graph)
			{
				if (fieldSolveOrderNode.IsError)
				{
					foreach (FieldSolveOrderNode fieldSolveOrderNode2 in fieldSolveOrderNode.Children.ToList<FieldSolveOrderNode>())
					{
						fieldSolveOrderNode2.Parents.Remove(fieldSolveOrderNode);
						fieldSolveOrderNode.Children.Remove(fieldSolveOrderNode2);
					}
				}
			}
			List<FieldSolveOrderNode> list2 = (from n in graph
			where !n.IsError && !n.Parents.Any<FieldSolveOrderNode>()
			select n).ToList<FieldSolveOrderNode>();
			while (list2.Any<FieldSolveOrderNode>())
			{
				FieldSolveOrderNode fieldSolveOrderNode3 = list2.First<FieldSolveOrderNode>();
				list2.Remove(fieldSolveOrderNode3);
				list.Add(fieldSolveOrderNode3);
				foreach (FieldSolveOrderNode fieldSolveOrderNode4 in fieldSolveOrderNode3.Children.ToList<FieldSolveOrderNode>())
				{
					fieldSolveOrderNode3.Children.Remove(fieldSolveOrderNode4);
					fieldSolveOrderNode4.Parents.Remove(fieldSolveOrderNode3);
					if (!fieldSolveOrderNode4.Parents.Any<FieldSolveOrderNode>())
					{
						list2.Add(fieldSolveOrderNode4);
					}
				}
			}
			return list;
		}

		// Token: 0x04001065 RID: 4197
		private IAggregateDescriptionsGenerator aggregateDescriptionGenerator;
	}
}
