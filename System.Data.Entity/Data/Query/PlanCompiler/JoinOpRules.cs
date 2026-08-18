using System;
using System.Collections.Generic;
using System.Data.Query.InternalTrees;

namespace System.Data.Query.PlanCompiler
{
	// Token: 0x02000075 RID: 117
	internal static class JoinOpRules
	{
		// Token: 0x06000915 RID: 2325 RVA: 0x0003195C File Offset: 0x0002FB5C
		private static bool ProcessJoinOverProject(RuleProcessingContext context, Node joinNode, out Node newNode)
		{
			newNode = joinNode;
			TransformationRulesContext transformationRulesContext = (TransformationRulesContext)context;
			Command command = transformationRulesContext.Command;
			Node node = joinNode.HasChild2 ? joinNode.Child2 : null;
			Dictionary<Var, int> varRefMap = new Dictionary<Var, int>();
			if (node != null && !transformationRulesContext.IsScalarOpTree(node, varRefMap))
			{
				return false;
			}
			VarVec varVec = command.CreateVarVec();
			List<Node> list = new List<Node>();
			if (joinNode.Op.OpType != OpType.LeftOuterJoin && joinNode.Child0.Op.OpType == OpType.Project && joinNode.Child1.Op.OpType == OpType.Project)
			{
				ProjectOp projectOp = (ProjectOp)joinNode.Child0.Op;
				ProjectOp projectOp2 = (ProjectOp)joinNode.Child1.Op;
				Dictionary<Var, Node> varMap = transformationRulesContext.GetVarMap(joinNode.Child0.Child1, varRefMap);
				Dictionary<Var, Node> varMap2 = transformationRulesContext.GetVarMap(joinNode.Child1.Child1, varRefMap);
				if (varMap == null || varMap2 == null)
				{
					return false;
				}
				Node arg;
				if (node != null)
				{
					node = transformationRulesContext.ReMap(node, varMap);
					node = transformationRulesContext.ReMap(node, varMap2);
					arg = context.Command.CreateNode(joinNode.Op, joinNode.Child0.Child0, joinNode.Child1.Child0, node);
				}
				else
				{
					arg = context.Command.CreateNode(joinNode.Op, joinNode.Child0.Child0, joinNode.Child1.Child0);
				}
				varVec.InitFrom(projectOp.Outputs);
				foreach (Var v in projectOp2.Outputs)
				{
					varVec.Set(v);
				}
				ProjectOp op = command.CreateProjectOp(varVec);
				list.AddRange(joinNode.Child0.Child1.Children);
				list.AddRange(joinNode.Child1.Child1.Children);
				Node arg2 = command.CreateNode(command.CreateVarDefListOp(), list);
				Node node2 = command.CreateNode(op, arg, arg2);
				newNode = node2;
				return true;
			}
			else
			{
				int index;
				int index2;
				if (joinNode.Child0.Op.OpType == OpType.Project)
				{
					index = 0;
					index2 = 1;
				}
				else
				{
					PlanCompiler.Assert(joinNode.Op.OpType != OpType.LeftOuterJoin, "unexpected non-LeftOuterJoin");
					index = 1;
					index2 = 0;
				}
				Node node3 = joinNode.Children[index];
				ProjectOp projectOp3 = node3.Op as ProjectOp;
				Dictionary<Var, Node> varMap3 = transformationRulesContext.GetVarMap(node3.Child1, varRefMap);
				if (varMap3 == null)
				{
					return false;
				}
				ExtendedNodeInfo extendedNodeInfo = command.GetExtendedNodeInfo(joinNode.Children[index2]);
				VarVec varVec2 = command.CreateVarVec(projectOp3.Outputs);
				varVec2.Or(extendedNodeInfo.Definitions);
				projectOp3.Outputs.InitFrom(varVec2);
				if (node != null)
				{
					node = transformationRulesContext.ReMap(node, varMap3);
					joinNode.Child2 = node;
				}
				joinNode.Children[index] = node3.Child0;
				context.Command.RecomputeNodeInfo(joinNode);
				newNode = context.Command.CreateNode(projectOp3, joinNode, node3.Child1);
				return true;
			}
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x00031C68 File Offset: 0x0002FE68
		private static bool ProcessJoinOverFilter(RuleProcessingContext context, Node joinNode, out Node newNode)
		{
			newNode = joinNode;
			TransformationRulesContext transformationRulesContext = (TransformationRulesContext)context;
			Command command = transformationRulesContext.Command;
			Node node = null;
			Node child = joinNode.Child0;
			if (joinNode.Child0.Op.OpType == OpType.Filter)
			{
				node = joinNode.Child0.Child1;
				child = joinNode.Child0.Child0;
			}
			Node arg = joinNode.Child1;
			if (joinNode.Child1.Op.OpType == OpType.Filter && joinNode.Op.OpType != OpType.LeftOuterJoin)
			{
				if (node == null)
				{
					node = joinNode.Child1.Child1;
				}
				else
				{
					node = command.CreateNode(command.CreateConditionalOp(OpType.And), node, joinNode.Child1.Child1);
				}
				arg = joinNode.Child1.Child0;
			}
			if (node == null)
			{
				return false;
			}
			Node arg2;
			if (joinNode.Op.OpType == OpType.CrossJoin)
			{
				arg2 = command.CreateNode(joinNode.Op, child, arg);
			}
			else
			{
				arg2 = command.CreateNode(joinNode.Op, child, arg, joinNode.Child2);
			}
			FilterOp op = command.CreateFilterOp();
			newNode = command.CreateNode(op, arg2, node);
			transformationRulesContext.SuppressFilterPushdown(newNode);
			return true;
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x00031D7B File Offset: 0x0002FF7B
		private static bool ProcessJoinOverSingleRowTable(RuleProcessingContext context, Node joinNode, out Node newNode)
		{
			newNode = joinNode;
			if (joinNode.Child0.Op.OpType == OpType.SingleRowTable)
			{
				newNode = joinNode.Child1;
			}
			else
			{
				newNode = joinNode.Child0;
			}
			return true;
		}

		// Token: 0x0400084D RID: 2125
		internal static readonly PatternMatchRule Rule_CrossJoinOverProject1 = new PatternMatchRule(new Node(CrossJoinOp.Pattern, new Node[]
		{
			new Node(LeafOp.Pattern, new Node[0]),
			new Node(ProjectOp.Pattern, new Node[]
			{
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0])
			})
		}), new Rule.ProcessNodeDelegate(JoinOpRules.ProcessJoinOverProject));

		// Token: 0x0400084E RID: 2126
		internal static readonly PatternMatchRule Rule_CrossJoinOverProject2 = new PatternMatchRule(new Node(CrossJoinOp.Pattern, new Node[]
		{
			new Node(ProjectOp.Pattern, new Node[]
			{
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0])
			}),
			new Node(LeafOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(JoinOpRules.ProcessJoinOverProject));

		// Token: 0x0400084F RID: 2127
		internal static readonly PatternMatchRule Rule_InnerJoinOverProject1 = new PatternMatchRule(new Node(InnerJoinOp.Pattern, new Node[]
		{
			new Node(LeafOp.Pattern, new Node[0]),
			new Node(ProjectOp.Pattern, new Node[]
			{
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0])
			}),
			new Node(LeafOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(JoinOpRules.ProcessJoinOverProject));

		// Token: 0x04000850 RID: 2128
		internal static readonly PatternMatchRule Rule_InnerJoinOverProject2 = new PatternMatchRule(new Node(InnerJoinOp.Pattern, new Node[]
		{
			new Node(ProjectOp.Pattern, new Node[]
			{
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0])
			}),
			new Node(LeafOp.Pattern, new Node[0]),
			new Node(LeafOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(JoinOpRules.ProcessJoinOverProject));

		// Token: 0x04000851 RID: 2129
		internal static readonly PatternMatchRule Rule_OuterJoinOverProject2 = new PatternMatchRule(new Node(LeftOuterJoinOp.Pattern, new Node[]
		{
			new Node(ProjectOp.Pattern, new Node[]
			{
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0])
			}),
			new Node(LeafOp.Pattern, new Node[0]),
			new Node(LeafOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(JoinOpRules.ProcessJoinOverProject));

		// Token: 0x04000852 RID: 2130
		internal static readonly PatternMatchRule Rule_CrossJoinOverFilter1 = new PatternMatchRule(new Node(CrossJoinOp.Pattern, new Node[]
		{
			new Node(LeafOp.Pattern, new Node[0]),
			new Node(FilterOp.Pattern, new Node[]
			{
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0])
			})
		}), new Rule.ProcessNodeDelegate(JoinOpRules.ProcessJoinOverFilter));

		// Token: 0x04000853 RID: 2131
		internal static readonly PatternMatchRule Rule_CrossJoinOverFilter2 = new PatternMatchRule(new Node(CrossJoinOp.Pattern, new Node[]
		{
			new Node(FilterOp.Pattern, new Node[]
			{
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0])
			}),
			new Node(LeafOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(JoinOpRules.ProcessJoinOverFilter));

		// Token: 0x04000854 RID: 2132
		internal static readonly PatternMatchRule Rule_InnerJoinOverFilter1 = new PatternMatchRule(new Node(InnerJoinOp.Pattern, new Node[]
		{
			new Node(LeafOp.Pattern, new Node[0]),
			new Node(FilterOp.Pattern, new Node[]
			{
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0])
			}),
			new Node(LeafOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(JoinOpRules.ProcessJoinOverFilter));

		// Token: 0x04000855 RID: 2133
		internal static readonly PatternMatchRule Rule_InnerJoinOverFilter2 = new PatternMatchRule(new Node(InnerJoinOp.Pattern, new Node[]
		{
			new Node(FilterOp.Pattern, new Node[]
			{
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0])
			}),
			new Node(LeafOp.Pattern, new Node[0]),
			new Node(LeafOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(JoinOpRules.ProcessJoinOverFilter));

		// Token: 0x04000856 RID: 2134
		internal static readonly PatternMatchRule Rule_OuterJoinOverFilter2 = new PatternMatchRule(new Node(LeftOuterJoinOp.Pattern, new Node[]
		{
			new Node(FilterOp.Pattern, new Node[]
			{
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0])
			}),
			new Node(LeafOp.Pattern, new Node[0]),
			new Node(LeafOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(JoinOpRules.ProcessJoinOverFilter));

		// Token: 0x04000857 RID: 2135
		internal static readonly PatternMatchRule Rule_CrossJoinOverSingleRowTable1 = new PatternMatchRule(new Node(CrossJoinOp.Pattern, new Node[]
		{
			new Node(SingleRowTableOp.Pattern, new Node[0]),
			new Node(LeafOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(JoinOpRules.ProcessJoinOverSingleRowTable));

		// Token: 0x04000858 RID: 2136
		internal static readonly PatternMatchRule Rule_CrossJoinOverSingleRowTable2 = new PatternMatchRule(new Node(CrossJoinOp.Pattern, new Node[]
		{
			new Node(LeafOp.Pattern, new Node[0]),
			new Node(SingleRowTableOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(JoinOpRules.ProcessJoinOverSingleRowTable));

		// Token: 0x04000859 RID: 2137
		internal static readonly PatternMatchRule Rule_LeftOuterJoinOverSingleRowTable = new PatternMatchRule(new Node(LeftOuterJoinOp.Pattern, new Node[]
		{
			new Node(LeafOp.Pattern, new Node[0]),
			new Node(SingleRowTableOp.Pattern, new Node[0]),
			new Node(LeafOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(JoinOpRules.ProcessJoinOverSingleRowTable));

		// Token: 0x0400085A RID: 2138
		internal static readonly Rule[] Rules = new Rule[]
		{
			JoinOpRules.Rule_CrossJoinOverProject1,
			JoinOpRules.Rule_CrossJoinOverProject2,
			JoinOpRules.Rule_InnerJoinOverProject1,
			JoinOpRules.Rule_InnerJoinOverProject2,
			JoinOpRules.Rule_OuterJoinOverProject2,
			JoinOpRules.Rule_CrossJoinOverFilter1,
			JoinOpRules.Rule_CrossJoinOverFilter2,
			JoinOpRules.Rule_InnerJoinOverFilter1,
			JoinOpRules.Rule_InnerJoinOverFilter2,
			JoinOpRules.Rule_OuterJoinOverFilter2,
			JoinOpRules.Rule_CrossJoinOverSingleRowTable1,
			JoinOpRules.Rule_CrossJoinOverSingleRowTable2,
			JoinOpRules.Rule_LeftOuterJoinOverSingleRowTable
		};
	}
}
