using System;
using System.Collections.Generic;
using System.Data.Query.InternalTrees;
using System.Linq;

namespace System.Data.Query.PlanCompiler
{
	// Token: 0x02000073 RID: 115
	internal static class ProjectOpRules
	{
		// Token: 0x06000907 RID: 2311 RVA: 0x000304B4 File Offset: 0x0002E6B4
		private static bool ProcessProjectOverProject(RuleProcessingContext context, Node projectNode, out Node newNode)
		{
			newNode = projectNode;
			ProjectOp projectOp = (ProjectOp)projectNode.Op;
			Node child = projectNode.Child1;
			Node child2 = projectNode.Child0;
			ProjectOp projectOp2 = (ProjectOp)child2.Op;
			TransformationRulesContext transformationRulesContext = (TransformationRulesContext)context;
			Dictionary<Var, int> varRefMap = new Dictionary<Var, int>();
			foreach (Node node in child.Children)
			{
				if (!transformationRulesContext.IsScalarOpTree(node.Child0, varRefMap))
				{
					return false;
				}
			}
			Dictionary<Var, Node> varMap = transformationRulesContext.GetVarMap(child2.Child1, varRefMap);
			if (varMap == null)
			{
				return false;
			}
			Node node2 = transformationRulesContext.Command.CreateNode(transformationRulesContext.Command.CreateVarDefListOp());
			foreach (Node node3 in child.Children)
			{
				node3.Child0 = transformationRulesContext.ReMap(node3.Child0, varMap);
				transformationRulesContext.Command.RecomputeNodeInfo(node3);
				node2.Children.Add(node3);
			}
			ExtendedNodeInfo extendedNodeInfo = transformationRulesContext.Command.GetExtendedNodeInfo(projectNode);
			foreach (Node node4 in child2.Child1.Children)
			{
				VarDefOp varDefOp = (VarDefOp)node4.Op;
				if (extendedNodeInfo.Definitions.IsSet(varDefOp.Var))
				{
					node2.Children.Add(node4);
				}
			}
			projectNode.Child0 = child2.Child0;
			projectNode.Child1 = node2;
			return true;
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x00030690 File Offset: 0x0002E890
		private static bool ProcessProjectWithNoLocalDefinitions(RuleProcessingContext context, Node n, out Node newNode)
		{
			newNode = n;
			NodeInfo nodeInfo = context.Command.GetNodeInfo(n);
			if (!nodeInfo.ExternalReferences.IsEmpty)
			{
				return false;
			}
			newNode = n.Child0;
			return true;
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x000306C8 File Offset: 0x0002E8C8
		private static bool ProcessProjectWithSimpleVarRedefinitions(RuleProcessingContext context, Node n, out Node newNode)
		{
			newNode = n;
			ProjectOp projectOp = (ProjectOp)n.Op;
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
						break;
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
					projectOp.Outputs.Clear(varDefOp.Var);
					projectOp.Outputs.Set(varRefOp2.Var);
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

		// Token: 0x0600090A RID: 2314 RVA: 0x00030880 File Offset: 0x0002EA80
		private static bool ProcessProjectOpWithNullSentinel(RuleProcessingContext context, Node n, out Node newNode)
		{
			newNode = n;
			ProjectOp projectOp = (ProjectOp)n.Op;
			Node child3 = n.Child1;
			if ((from c in child3.Children
			where c.Child0.Op.OpType == OpType.NullSentinel
			select c).Count<Node>() == 0)
			{
				return false;
			}
			TransformationRulesContext transformationRulesContext = (TransformationRulesContext)context;
			Command command = transformationRulesContext.Command;
			ExtendedNodeInfo extendedNodeInfo = command.GetExtendedNodeInfo(n.Child0);
			bool flag = false;
			bool canChangeNullSentinelValue = transformationRulesContext.CanChangeNullSentinelValue;
			Var var;
			if (!canChangeNullSentinelValue || !TransformationRulesContext.TryGetInt32Var(extendedNodeInfo.NonNullableDefinitions, out var))
			{
				flag = true;
				if (canChangeNullSentinelValue)
				{
					if (TransformationRulesContext.TryGetInt32Var(from child in n.Child1.Children
					where child.Child0.Op.OpType == OpType.Constant || child.Child0.Op.OpType == OpType.InternalConstant
					select ((VarDefOp)child.Op).Var, out var))
					{
						goto IL_14A;
					}
				}
				var = (from child in n.Child1.Children
				where child.Child0.Op.OpType == OpType.NullSentinel
				select ((VarDefOp)child.Op).Var).FirstOrDefault<Var>();
				if (var == null)
				{
					return false;
				}
			}
			IL_14A:
			bool flag2 = false;
			for (int i = n.Child1.Children.Count - 1; i >= 0; i--)
			{
				Node node = n.Child1.Children[i];
				Node child2 = node.Child0;
				if (child2.Op.OpType == OpType.NullSentinel)
				{
					if (!flag)
					{
						VarRefOp op = command.CreateVarRefOp(var);
						node.Child0 = command.CreateNode(op);
						command.RecomputeNodeInfo(node);
						flag2 = true;
					}
					else if (!var.Equals(((VarDefOp)node.Op).Var))
					{
						projectOp.Outputs.Clear(((VarDefOp)node.Op).Var);
						n.Child1.Children.RemoveAt(i);
						transformationRulesContext.AddVarMapping(((VarDefOp)node.Op).Var, var);
						flag2 = true;
					}
				}
			}
			if (flag2)
			{
				command.RecomputeNodeInfo(n.Child1);
			}
			return flag2;
		}

		// Token: 0x0400083C RID: 2108
		internal static readonly PatternMatchRule Rule_ProjectOverProject = new PatternMatchRule(new Node(ProjectOp.Pattern, new Node[]
		{
			new Node(ProjectOp.Pattern, new Node[]
			{
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0])
			}),
			new Node(LeafOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(ProjectOpRules.ProcessProjectOverProject));

		// Token: 0x0400083D RID: 2109
		internal static readonly PatternMatchRule Rule_ProjectWithNoLocalDefs = new PatternMatchRule(new Node(ProjectOp.Pattern, new Node[]
		{
			new Node(LeafOp.Pattern, new Node[0]),
			new Node(VarDefListOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(ProjectOpRules.ProcessProjectWithNoLocalDefinitions));

		// Token: 0x0400083E RID: 2110
		internal static readonly SimpleRule Rule_ProjectOpWithSimpleVarRedefinitions = new SimpleRule(OpType.Project, new Rule.ProcessNodeDelegate(ProjectOpRules.ProcessProjectWithSimpleVarRedefinitions));

		// Token: 0x0400083F RID: 2111
		internal static readonly SimpleRule Rule_ProjectOpWithNullSentinel = new SimpleRule(OpType.Project, new Rule.ProcessNodeDelegate(ProjectOpRules.ProcessProjectOpWithNullSentinel));

		// Token: 0x04000840 RID: 2112
		internal static readonly Rule[] Rules = new Rule[]
		{
			ProjectOpRules.Rule_ProjectOpWithNullSentinel,
			ProjectOpRules.Rule_ProjectOpWithSimpleVarRedefinitions,
			ProjectOpRules.Rule_ProjectOverProject,
			ProjectOpRules.Rule_ProjectWithNoLocalDefs
		};
	}
}
