using System;
using System.Collections.Generic;
using System.Data.Query.InternalTrees;

namespace System.Data.Query.PlanCompiler
{
	// Token: 0x02000078 RID: 120
	internal static class GroupByOpRules
	{
		// Token: 0x0600091E RID: 2334 RVA: 0x000326FC File Offset: 0x000308FC
		private static bool ProcessGroupByWithSimpleVarRedefinitions(RuleProcessingContext context, Node n, out Node newNode)
		{
			newNode = n;
			GroupByOp groupByOp = (GroupByOp)n.Op;
			if (n.Child1.Children.Count == 0)
			{
				return false;
			}
			TransformationRulesContext transformationRulesContext = (TransformationRulesContext)context;
			Command command = transformationRulesContext.Command;
			ExtendedNodeInfo extendedNodeInfo = command.GetExtendedNodeInfo(n);
			bool flag = false;
			foreach (Node node in n.Child1.Children)
			{
				Node child = node.Child0;
				if (child.Op.OpType == OpType.VarRef)
				{
					VarRefOp varRefOp = (VarRefOp)child.Op;
					if (!extendedNodeInfo.ExternalReferences.IsSet(varRefOp.Var))
					{
						flag = true;
					}
				}
			}
			if (!flag)
			{
				return false;
			}
			List<Node> list = new List<Node>();
			foreach (Node node2 in n.Child1.Children)
			{
				VarDefOp varDefOp = (VarDefOp)node2.Op;
				VarRefOp varRefOp2 = node2.Child0.Op as VarRefOp;
				if (varRefOp2 != null && !extendedNodeInfo.ExternalReferences.IsSet(varRefOp2.Var))
				{
					groupByOp.Outputs.Clear(varDefOp.Var);
					groupByOp.Outputs.Set(varRefOp2.Var);
					groupByOp.Keys.Clear(varDefOp.Var);
					groupByOp.Keys.Set(varRefOp2.Var);
					transformationRulesContext.AddVarMapping(varDefOp.Var, varRefOp2.Var);
				}
				else
				{
					list.Add(node2);
				}
			}
			Node child2 = command.CreateNode(command.CreateVarDefListOp(), list);
			n.Child1 = child2;
			return true;
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x000328D8 File Offset: 0x00030AD8
		private static bool ProcessGroupByOverProject(RuleProcessingContext context, Node n, out Node newNode)
		{
			newNode = n;
			GroupByOp groupByOp = (GroupByOp)n.Op;
			Command command = ((TransformationRulesContext)context).Command;
			Node child = n.Child0;
			Node child2 = child.Child1;
			Node child3 = n.Child1;
			Node child4 = n.Child2;
			if (child3.Children.Count > 0)
			{
				return false;
			}
			VarVec varVec = command.GetExtendedNodeInfo(child).LocalDefinitions;
			if (groupByOp.Outputs.Overlaps(varVec))
			{
				return false;
			}
			bool flag = false;
			for (int i = 0; i < child2.Children.Count; i++)
			{
				Node node = child2.Children[i];
				if (node.Child0.Op.OpType == OpType.Constant || node.Child0.Op.OpType == OpType.InternalConstant || node.Child0.Op.OpType == OpType.NullSentinel)
				{
					if (!flag)
					{
						varVec = command.CreateVarVec(varVec);
						flag = true;
					}
					varVec.Clear(((VarDefOp)node.Op).Var);
				}
			}
			if (GroupByOpRules.VarRefUsageFinder.AnyVarUsedMoreThanOnce(varVec, child4, command))
			{
				return false;
			}
			Dictionary<Var, Node> dictionary = new Dictionary<Var, Node>(child2.Children.Count);
			for (int j = 0; j < child2.Children.Count; j++)
			{
				Node node2 = child2.Children[j];
				Var var = ((VarDefOp)node2.Op).Var;
				dictionary.Add(var, node2.Child0);
			}
			newNode.Child2 = GroupByOpRules.VarRefReplacer.Replace(dictionary, child4, command);
			newNode.Child0 = child.Child0;
			return true;
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x00032A6C File Offset: 0x00030C6C
		private static bool ProcessGroupByOpWithNoAggregates(RuleProcessingContext context, Node n, out Node newNode)
		{
			Command command = context.Command;
			GroupByOp groupByOp = (GroupByOp)n.Op;
			ExtendedNodeInfo extendedNodeInfo = command.GetExtendedNodeInfo(n.Child0);
			ProjectOp op = command.CreateProjectOp(groupByOp.Keys);
			VarDefListOp op2 = command.CreateVarDefListOp();
			Node node = command.CreateNode(op2);
			newNode = command.CreateNode(op, n.Child0, n.Child1);
			if (extendedNodeInfo.Keys.NoKeys || !groupByOp.Keys.Subsumes(extendedNodeInfo.Keys.KeyVars))
			{
				newNode = command.CreateNode(command.CreateDistinctOp(command.CreateVarVec(groupByOp.Keys)), newNode);
			}
			return true;
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x00032B10 File Offset: 0x00030D10
		private static bool ProcessGroupByOpOnAllInputColumnsWithAggregateOperation(RuleProcessingContext context, Node n, out Node newNode)
		{
			newNode = n;
			PhysicalProjectOp physicalProjectOp = context.Command.Root.Op as PhysicalProjectOp;
			if (physicalProjectOp == null || physicalProjectOp.Outputs.Count > 1)
			{
				return false;
			}
			if (n.Child0.Op.OpType != OpType.ScanTable)
			{
				return false;
			}
			if (n.Child2 == null || n.Child2.Child0 == null || n.Child2.Child0.Child0 == null || n.Child2.Child0.Child0.Op.OpType != OpType.Aggregate)
			{
				return false;
			}
			GroupByOp groupByOp = (GroupByOp)n.Op;
			Table table = ((ScanTableOp)n.Child0.Op).Table;
			VarList columns = table.Columns;
			foreach (Var v in columns)
			{
				if (!groupByOp.Keys.IsSet(v))
				{
					return false;
				}
			}
			foreach (Var v2 in columns)
			{
				groupByOp.Outputs.Clear(v2);
				groupByOp.Keys.Clear(v2);
			}
			Command command = context.Command;
			ScanTableOp scanTableOp = command.CreateScanTableOp(table.TableMetadata);
			Node arg = command.CreateNode(scanTableOp);
			Node arg2 = command.CreateNode(command.CreateOuterApplyOp(), arg, n);
			Var v3;
			Node arg3 = command.CreateVarDefListNode(command.CreateNode(command.CreateVarRefOp(groupByOp.Outputs.First)), out v3);
			newNode = command.CreateNode(command.CreateProjectOp(v3), arg2, arg3);
			Node node = null;
			IEnumerator<Var> enumerator3 = scanTableOp.Table.Keys.GetEnumerator();
			IEnumerator<Var> enumerator4 = table.Keys.GetEnumerator();
			for (int i = 0; i < table.Keys.Count; i++)
			{
				enumerator3.MoveNext();
				enumerator4.MoveNext();
				Node node2 = command.CreateNode(command.CreateComparisonOp(OpType.EQ), command.CreateNode(command.CreateVarRefOp(enumerator3.Current)), command.CreateNode(command.CreateVarRefOp(enumerator4.Current)));
				if (node != null)
				{
					node = command.CreateNode(command.CreateConditionalOp(OpType.And), node, node2);
				}
				else
				{
					node = node2;
				}
			}
			Node child = command.CreateNode(command.CreateFilterOp(), n.Child0, node);
			n.Child0 = child;
			return true;
		}

		// Token: 0x04000862 RID: 2146
		internal static readonly SimpleRule Rule_GroupByOpWithSimpleVarRedefinitions = new SimpleRule(OpType.GroupBy, new Rule.ProcessNodeDelegate(GroupByOpRules.ProcessGroupByWithSimpleVarRedefinitions));

		// Token: 0x04000863 RID: 2147
		internal static readonly PatternMatchRule Rule_GroupByOverProject = new PatternMatchRule(new Node(GroupByOp.Pattern, new Node[]
		{
			new Node(ProjectOp.Pattern, new Node[]
			{
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0])
			}),
			new Node(LeafOp.Pattern, new Node[0]),
			new Node(LeafOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(GroupByOpRules.ProcessGroupByOverProject));

		// Token: 0x04000864 RID: 2148
		internal static readonly PatternMatchRule Rule_GroupByOpWithNoAggregates = new PatternMatchRule(new Node(GroupByOp.Pattern, new Node[]
		{
			new Node(LeafOp.Pattern, new Node[0]),
			new Node(LeafOp.Pattern, new Node[0]),
			new Node(VarDefListOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(GroupByOpRules.ProcessGroupByOpWithNoAggregates));

		// Token: 0x04000865 RID: 2149
		internal static readonly SimpleRule Rule_GroupByOpOnAllInputColumnsWithAggregateOperation = new SimpleRule(OpType.GroupBy, new Rule.ProcessNodeDelegate(GroupByOpRules.ProcessGroupByOpOnAllInputColumnsWithAggregateOperation));

		// Token: 0x04000866 RID: 2150
		internal static readonly Rule[] Rules = new Rule[]
		{
			GroupByOpRules.Rule_GroupByOpWithSimpleVarRedefinitions,
			GroupByOpRules.Rule_GroupByOverProject,
			GroupByOpRules.Rule_GroupByOpWithNoAggregates,
			GroupByOpRules.Rule_GroupByOpOnAllInputColumnsWithAggregateOperation
		};

		// Token: 0x02000483 RID: 1155
		internal class VarRefReplacer : BasicOpVisitorOfNode
		{
			// Token: 0x06003B86 RID: 15238 RVA: 0x000E08F3 File Offset: 0x000DEAF3
			private VarRefReplacer(Dictionary<Var, Node> varReplacementTable, Command command)
			{
				this.m_varReplacementTable = varReplacementTable;
				this.m_command = command;
			}

			// Token: 0x06003B87 RID: 15239 RVA: 0x000E090C File Offset: 0x000DEB0C
			internal static Node Replace(Dictionary<Var, Node> varReplacementTable, Node root, Command command)
			{
				GroupByOpRules.VarRefReplacer varRefReplacer = new GroupByOpRules.VarRefReplacer(varReplacementTable, command);
				return varRefReplacer.VisitNode(root);
			}

			// Token: 0x06003B88 RID: 15240 RVA: 0x000E0928 File Offset: 0x000DEB28
			public override Node Visit(VarRefOp op, Node n)
			{
				Node result;
				if (this.m_varReplacementTable.TryGetValue(op.Var, out result))
				{
					return result;
				}
				return n;
			}

			// Token: 0x06003B89 RID: 15241 RVA: 0x000E0950 File Offset: 0x000DEB50
			protected override Node VisitDefault(Node n)
			{
				Node node = base.VisitDefault(n);
				this.m_command.RecomputeNodeInfo(node);
				return node;
			}

			// Token: 0x040019BC RID: 6588
			private Dictionary<Var, Node> m_varReplacementTable;

			// Token: 0x040019BD RID: 6589
			private Command m_command;
		}

		// Token: 0x02000484 RID: 1156
		internal class VarRefUsageFinder : BasicOpVisitor
		{
			// Token: 0x06003B8A RID: 15242 RVA: 0x000E0972 File Offset: 0x000DEB72
			private VarRefUsageFinder(VarVec varVec, Command command)
			{
				this.m_varVec = varVec;
				this.m_usedVars = command.CreateVarVec();
			}

			// Token: 0x06003B8B RID: 15243 RVA: 0x000E0990 File Offset: 0x000DEB90
			internal static bool AnyVarUsedMoreThanOnce(VarVec varVec, Node root, Command command)
			{
				GroupByOpRules.VarRefUsageFinder varRefUsageFinder = new GroupByOpRules.VarRefUsageFinder(varVec, command);
				varRefUsageFinder.VisitNode(root);
				return varRefUsageFinder.m_anyUsedMoreThenOnce;
			}

			// Token: 0x06003B8C RID: 15244 RVA: 0x000E09B4 File Offset: 0x000DEBB4
			public override void Visit(VarRefOp op, Node n)
			{
				Var var = op.Var;
				if (this.m_varVec.IsSet(var))
				{
					if (this.m_usedVars.IsSet(var))
					{
						this.m_anyUsedMoreThenOnce = true;
						return;
					}
					this.m_usedVars.Set(var);
				}
			}

			// Token: 0x06003B8D RID: 15245 RVA: 0x000E09F8 File Offset: 0x000DEBF8
			protected override void VisitChildren(Node n)
			{
				if (this.m_anyUsedMoreThenOnce)
				{
					return;
				}
				base.VisitChildren(n);
			}

			// Token: 0x040019BE RID: 6590
			private bool m_anyUsedMoreThenOnce;

			// Token: 0x040019BF RID: 6591
			private VarVec m_varVec;

			// Token: 0x040019C0 RID: 6592
			private VarVec m_usedVars;
		}
	}
}
