using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Query.InternalTrees;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000681 RID: 1665
	internal static class JoinOpRules
	{
		// Token: 0x06004151 RID: 16721 RVA: 0x0012EC84 File Offset: 0x0012CE84
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "non-LeftOuterJoin")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
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

		// Token: 0x06004152 RID: 16722 RVA: 0x0012EF90 File Offset: 0x0012D190
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

		// Token: 0x06004153 RID: 16723 RVA: 0x0012F0A3 File Offset: 0x0012D2A3
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

		// Token: 0x04001847 RID: 6215
		internal static readonly PatternMatchRule Rule_CrossJoinOverProject1 = new PatternMatchRule(new Node(CrossJoinOp.Pattern, new Node[]
		{
			new Node(LeafOp.Pattern, new Node[0]),
			new Node(ProjectOp.Pattern, new Node[]
			{
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0])
			})
		}), new Rule.ProcessNodeDelegate(JoinOpRules.ProcessJoinOverProject));

		// Token: 0x04001848 RID: 6216
		internal static readonly PatternMatchRule Rule_CrossJoinOverProject2 = new PatternMatchRule(new Node(CrossJoinOp.Pattern, new Node[]
		{
			new Node(ProjectOp.Pattern, new Node[]
			{
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0])
			}),
			new Node(LeafOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(JoinOpRules.ProcessJoinOverProject));

		// Token: 0x04001849 RID: 6217
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

		// Token: 0x0400184A RID: 6218
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

		// Token: 0x0400184B RID: 6219
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

		// Token: 0x0400184C RID: 6220
		internal static readonly PatternMatchRule Rule_CrossJoinOverFilter1 = new PatternMatchRule(new Node(CrossJoinOp.Pattern, new Node[]
		{
			new Node(LeafOp.Pattern, new Node[0]),
			new Node(FilterOp.Pattern, new Node[]
			{
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0])
			})
		}), new Rule.ProcessNodeDelegate(JoinOpRules.ProcessJoinOverFilter));

		// Token: 0x0400184D RID: 6221
		internal static readonly PatternMatchRule Rule_CrossJoinOverFilter2 = new PatternMatchRule(new Node(CrossJoinOp.Pattern, new Node[]
		{
			new Node(FilterOp.Pattern, new Node[]
			{
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0])
			}),
			new Node(LeafOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(JoinOpRules.ProcessJoinOverFilter));

		// Token: 0x0400184E RID: 6222
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

		// Token: 0x0400184F RID: 6223
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

		// Token: 0x04001850 RID: 6224
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

		// Token: 0x04001851 RID: 6225
		internal static readonly PatternMatchRule Rule_CrossJoinOverSingleRowTable1 = new PatternMatchRule(new Node(CrossJoinOp.Pattern, new Node[]
		{
			new Node(SingleRowTableOp.Pattern, new Node[0]),
			new Node(LeafOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(JoinOpRules.ProcessJoinOverSingleRowTable));

		// Token: 0x04001852 RID: 6226
		internal static readonly PatternMatchRule Rule_CrossJoinOverSingleRowTable2 = new PatternMatchRule(new Node(CrossJoinOp.Pattern, new Node[]
		{
			new Node(LeafOp.Pattern, new Node[0]),
			new Node(SingleRowTableOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(JoinOpRules.ProcessJoinOverSingleRowTable));

		// Token: 0x04001853 RID: 6227
		internal static readonly PatternMatchRule Rule_LeftOuterJoinOverSingleRowTable = new PatternMatchRule(new Node(LeftOuterJoinOp.Pattern, new Node[]
		{
			new Node(LeafOp.Pattern, new Node[0]),
			new Node(SingleRowTableOp.Pattern, new Node[0]),
			new Node(LeafOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(JoinOpRules.ProcessJoinOverSingleRowTable));

		// Token: 0x04001854 RID: 6228
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
