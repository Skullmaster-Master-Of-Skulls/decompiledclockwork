using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Query.InternalTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000674 RID: 1652
	internal static class GroupByOpRules
	{
		// Token: 0x0600406F RID: 16495 RVA: 0x00127A38 File Offset: 0x00125C38
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

		// Token: 0x06004070 RID: 16496 RVA: 0x00127C14 File Offset: 0x00125E14
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
				Node node2 = command.CreateNode(command.CreateComparisonOp(OpType.EQ, false), command.CreateNode(command.CreateVarRefOp(enumerator3.Current)), command.CreateNode(command.CreateVarRefOp(enumerator4.Current)));
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

		// Token: 0x06004071 RID: 16497 RVA: 0x00127EB8 File Offset: 0x001260B8
		private static bool ProcessGroupByOverProject(RuleProcessingContext context, Node n, out Node newNode)
		{
			newNode = n;
			GroupByOp groupByOp = (GroupByOp)n.Op;
			Command command = context.Command;
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

		// Token: 0x06004072 RID: 16498 RVA: 0x00128048 File Offset: 0x00126248
		private static bool ProcessGroupByOpWithNoAggregates(RuleProcessingContext context, Node n, out Node newNode)
		{
			Command command = context.Command;
			GroupByOp groupByOp = (GroupByOp)n.Op;
			ExtendedNodeInfo extendedNodeInfo = command.GetExtendedNodeInfo(n.Child0);
			ProjectOp op = command.CreateProjectOp(groupByOp.Keys);
			VarDefListOp op2 = command.CreateVarDefListOp();
			command.CreateNode(op2);
			newNode = command.CreateNode(op, n.Child0, n.Child1);
			if (extendedNodeInfo.Keys.NoKeys || !groupByOp.Keys.Subsumes(extendedNodeInfo.Keys.KeyVars))
			{
				newNode = command.CreateNode(command.CreateDistinctOp(command.CreateVarVec(groupByOp.Keys)), newNode);
			}
			return true;
		}

		// Token: 0x04001809 RID: 6153
		internal static readonly SimpleRule Rule_GroupByOpWithSimpleVarRedefinitions = new SimpleRule(OpType.GroupBy, new Rule.ProcessNodeDelegate(GroupByOpRules.ProcessGroupByWithSimpleVarRedefinitions));

		// Token: 0x0400180A RID: 6154
		internal static readonly SimpleRule Rule_GroupByOpOnAllInputColumnsWithAggregateOperation = new SimpleRule(OpType.GroupBy, new Rule.ProcessNodeDelegate(GroupByOpRules.ProcessGroupByOpOnAllInputColumnsWithAggregateOperation));

		// Token: 0x0400180B RID: 6155
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

		// Token: 0x0400180C RID: 6156
		internal static readonly PatternMatchRule Rule_GroupByOpWithNoAggregates = new PatternMatchRule(new Node(GroupByOp.Pattern, new Node[]
		{
			new Node(LeafOp.Pattern, new Node[0]),
			new Node(LeafOp.Pattern, new Node[0]),
			new Node(VarDefListOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(GroupByOpRules.ProcessGroupByOpWithNoAggregates));

		// Token: 0x0400180D RID: 6157
		internal static readonly Rule[] Rules = new Rule[]
		{
			GroupByOpRules.Rule_GroupByOpWithSimpleVarRedefinitions,
			GroupByOpRules.Rule_GroupByOverProject,
			GroupByOpRules.Rule_GroupByOpWithNoAggregates,
			GroupByOpRules.Rule_GroupByOpOnAllInputColumnsWithAggregateOperation
		};

		// Token: 0x02000675 RID: 1653
		internal class VarRefReplacer : BasicOpVisitorOfNode
		{
			// Token: 0x06004074 RID: 16500 RVA: 0x00128240 File Offset: 0x00126440
			private VarRefReplacer(Dictionary<Var, Node> varReplacementTable, Command command)
			{
				this.m_varReplacementTable = varReplacementTable;
				this.m_command = command;
			}

			// Token: 0x06004075 RID: 16501 RVA: 0x00128258 File Offset: 0x00126458
			internal static Node Replace(Dictionary<Var, Node> varReplacementTable, Node root, Command command)
			{
				GroupByOpRules.VarRefReplacer varRefReplacer = new GroupByOpRules.VarRefReplacer(varReplacementTable, command);
				return varRefReplacer.VisitNode(root);
			}

			// Token: 0x06004076 RID: 16502 RVA: 0x00128274 File Offset: 0x00126474
			public override Node Visit(VarRefOp op, Node n)
			{
				Node result;
				if (this.m_varReplacementTable.TryGetValue(op.Var, out result))
				{
					return result;
				}
				return n;
			}

			// Token: 0x06004077 RID: 16503 RVA: 0x0012829C File Offset: 0x0012649C
			protected override Node VisitDefault(Node n)
			{
				Node node = base.VisitDefault(n);
				this.m_command.RecomputeNodeInfo(node);
				return node;
			}

			// Token: 0x0400180E RID: 6158
			private readonly Dictionary<Var, Node> m_varReplacementTable;

			// Token: 0x0400180F RID: 6159
			private readonly Command m_command;
		}

		// Token: 0x02000676 RID: 1654
		internal class VarRefUsageFinder : BasicOpVisitor
		{
			// Token: 0x06004078 RID: 16504 RVA: 0x001282BE File Offset: 0x001264BE
			private VarRefUsageFinder(VarVec varVec, Command command)
			{
				this.m_varVec = varVec;
				this.m_usedVars = command.CreateVarVec();
			}

			// Token: 0x06004079 RID: 16505 RVA: 0x001282DC File Offset: 0x001264DC
			internal static bool AnyVarUsedMoreThanOnce(VarVec varVec, Node root, Command command)
			{
				GroupByOpRules.VarRefUsageFinder varRefUsageFinder = new GroupByOpRules.VarRefUsageFinder(varVec, command);
				varRefUsageFinder.VisitNode(root);
				return varRefUsageFinder.m_anyUsedMoreThenOnce;
			}

			// Token: 0x0600407A RID: 16506 RVA: 0x00128300 File Offset: 0x00126500
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

			// Token: 0x0600407B RID: 16507 RVA: 0x00128344 File Offset: 0x00126544
			protected override void VisitChildren(Node n)
			{
				if (this.m_anyUsedMoreThenOnce)
				{
					return;
				}
				base.VisitChildren(n);
			}

			// Token: 0x04001810 RID: 6160
			private bool m_anyUsedMoreThenOnce;

			// Token: 0x04001811 RID: 6161
			private readonly VarVec m_varVec;

			// Token: 0x04001812 RID: 6162
			private readonly VarVec m_usedVars;
		}
	}
}
