using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Metadata.Edm;
using System.Data.Query.InternalTrees;
using System.Linq;

namespace System.Data.Query.PlanCompiler
{
	// Token: 0x02000057 RID: 87
	internal class NestPullup : BasicOpVisitorOfNode
	{
		// Token: 0x06000741 RID: 1857 RVA: 0x00022348 File Offset: 0x00020548
		private NestPullup(PlanCompiler compilerState)
		{
			this.m_compilerState = compilerState;
			this.m_varRemapper = new VarRemapper(compilerState.Command);
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x00022380 File Offset: 0x00020580
		internal static void Process(PlanCompiler compilerState)
		{
			NestPullup nestPullup = new NestPullup(compilerState);
			nestPullup.Process();
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x0002239C File Offset: 0x0002059C
		private void Process()
		{
			PlanCompiler.Assert(this.Command.Root.Op.OpType == OpType.PhysicalProject, "root node is not physicalProject?");
			this.Command.Root = base.VisitNode(this.Command.Root);
			if (this.m_foundSortUnderUnnest)
			{
				SortRemover.Process(this.Command);
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000744 RID: 1860 RVA: 0x000223FB File Offset: 0x000205FB
		private Command Command
		{
			get
			{
				return this.m_compilerState.Command;
			}
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x00022408 File Offset: 0x00020608
		private static bool IsNestOpNode(Node n)
		{
			PlanCompiler.Assert(n.Op.OpType != OpType.SingleStreamNest, "illegal singleStreamNest?");
			return n.Op.OpType == OpType.SingleStreamNest || n.Op.OpType == OpType.MultiStreamNest;
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x00022448 File Offset: 0x00020648
		private Node NestingNotSupported(Op op, Node n)
		{
			this.VisitChildren(n);
			this.m_varRemapper.RemapNode(n);
			foreach (Node node in n.Children)
			{
				if (NestPullup.IsNestOpNode(node))
				{
					throw EntityUtil.NestingNotSupported(op, node.Op);
				}
			}
			return n;
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x000224C0 File Offset: 0x000206C0
		private Var ResolveVarReference(Var refVar)
		{
			Var var = refVar;
			while (this.m_varRefMap.TryGetValue(var, out var))
			{
				refVar = var;
			}
			return refVar;
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x000224E8 File Offset: 0x000206E8
		private void UpdateReplacementVarMap(IEnumerable<Var> fromVars, IEnumerable<Var> toVars)
		{
			IEnumerator<Var> enumerator = toVars.GetEnumerator();
			foreach (Var oldVar in fromVars)
			{
				if (!enumerator.MoveNext())
				{
					throw EntityUtil.InternalError(EntityUtil.InternalErrorCode.ColumnCountMismatch, 2);
				}
				this.m_varRemapper.AddMapping(oldVar, enumerator.Current);
			}
			if (enumerator.MoveNext())
			{
				throw EntityUtil.InternalError(EntityUtil.InternalErrorCode.ColumnCountMismatch, 3);
			}
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x0002256C File Offset: 0x0002076C
		private static void RemapSortKeys(List<SortKey> sortKeys, Dictionary<Var, Var> varMap)
		{
			if (sortKeys != null)
			{
				foreach (SortKey sortKey in sortKeys)
				{
					Var var;
					if (varMap.TryGetValue(sortKey.Var, out var))
					{
						sortKey.Var = var;
					}
				}
			}
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x000225D0 File Offset: 0x000207D0
		private IEnumerable<Var> RemapVars(IEnumerable<Var> vars, Dictionary<Var, Var> varMap)
		{
			foreach (Var var in vars)
			{
				Var var2;
				if (varMap.TryGetValue(var, out var2))
				{
					yield return var2;
				}
				else
				{
					yield return var;
				}
			}
			IEnumerator<Var> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x000225E8 File Offset: 0x000207E8
		private VarList RemapVarList(VarList varList, Dictionary<Var, Var> varMap)
		{
			return Command.CreateVarList(this.RemapVars(varList, varMap));
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x00022604 File Offset: 0x00020804
		private VarVec RemapVarVec(VarVec varVec, Dictionary<Var, Var> varMap)
		{
			return this.Command.CreateVarVec(this.RemapVars(varVec, varMap));
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x00022628 File Offset: 0x00020828
		public override Node Visit(VarDefOp op, Node n)
		{
			this.VisitChildren(n);
			this.m_varRemapper.RemapNode(n);
			if (n.Child0.Op.OpType == OpType.VarRef)
			{
				this.m_varRefMap.Add(op.Var, ((VarRefOp)n.Child0.Op).Var);
			}
			return n;
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x00022682 File Offset: 0x00020882
		public override Node Visit(VarRefOp op, Node n)
		{
			this.VisitChildren(n);
			this.m_varRemapper.RemapNode(n);
			return n;
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x00022698 File Offset: 0x00020898
		public override Node Visit(CaseOp op, Node n)
		{
			foreach (Node node in n.Children)
			{
				if (node.Op.OpType == OpType.Collect)
				{
					throw EntityUtil.NestingNotSupported(op, node.Op);
				}
				if (node.Op.OpType == OpType.VarRef)
				{
					Var var = ((VarRefOp)node.Op).Var;
					if (this.m_definingNodeMap.ContainsKey(var))
					{
						throw EntityUtil.NestingNotSupported(op, node.Op);
					}
				}
			}
			return this.VisitDefault(n);
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x00022744 File Offset: 0x00020944
		public override Node Visit(ExistsOp op, Node n)
		{
			Var first = ((ProjectOp)n.Child0.Op).Outputs.First;
			this.VisitChildren(n);
			VarVec outputs = ((ProjectOp)n.Child0.Op).Outputs;
			if (outputs.Count > 1)
			{
				PlanCompiler.Assert(outputs.IsSet(first), "The constant var is not present after NestPull up over the input of ExistsOp.");
				outputs.Clear();
				outputs.Set(first);
			}
			return n;
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x000227B1 File Offset: 0x000209B1
		protected override Node VisitRelOpDefault(RelOp op, Node n)
		{
			return this.NestingNotSupported(op, n);
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x000227BC File Offset: 0x000209BC
		private Node ApplyOpJoinOp(Op op, Node n)
		{
			this.VisitChildren(n);
			int num = 0;
			foreach (Node node in n.Children)
			{
				NestBaseOp nestBaseOp = node.Op as NestBaseOp;
				if (nestBaseOp != null)
				{
					num++;
					if (OpType.SingleStreamNest == node.Op.OpType)
					{
						throw EntityUtil.InternalError(EntityUtil.InternalErrorCode.JoinOverSingleStreamNest);
					}
				}
			}
			if (num == 0)
			{
				return n;
			}
			foreach (Node node2 in n.Children)
			{
				if (op.OpType != OpType.MultiStreamNest && node2.Op.IsRelOp)
				{
					KeyVec keyVec = this.Command.PullupKeys(node2);
					if (keyVec == null || keyVec.NoKeys)
					{
						throw EntityUtil.KeysRequiredForJoinOverNest(op);
					}
				}
			}
			List<Node> list = new List<Node>();
			List<Node> list2 = new List<Node>();
			List<CollectionInfo> list3 = new List<CollectionInfo>();
			foreach (Node node3 in n.Children)
			{
				if (node3.Op.OpType == OpType.MultiStreamNest)
				{
					list3.AddRange(((MultiStreamNestOp)node3.Op).CollectionInfo);
					if (op.OpType == OpType.FullOuterJoin || ((op.OpType == OpType.LeftOuterJoin || op.OpType == OpType.OuterApply) && n.Child1.Op.OpType == OpType.MultiStreamNest))
					{
						Var sentinelVar = null;
						list2.Add(this.AugmentNodeWithConstant(node3.Child0, () => this.Command.CreateNullSentinelOp(), out sentinelVar));
						foreach (CollectionInfo collectionInfo in ((MultiStreamNestOp)node3.Op).CollectionInfo)
						{
							this.m_definingNodeMap[collectionInfo.CollectionVar].Child0 = this.ApplyIsNotNullFilter(this.m_definingNodeMap[collectionInfo.CollectionVar].Child0, sentinelVar);
						}
						for (int i = 1; i < node3.Children.Count; i++)
						{
							Node item = this.ApplyIsNotNullFilter(node3.Children[i], sentinelVar);
							list.Add(item);
						}
					}
					else
					{
						list2.Add(node3.Child0);
						for (int j = 1; j < node3.Children.Count; j++)
						{
							list.Add(node3.Children[j]);
						}
					}
				}
				else
				{
					list2.Add(node3);
				}
			}
			Node node4 = this.Command.CreateNode(op, list2);
			list.Insert(0, node4);
			ExtendedNodeInfo extendedNodeInfo = node4.GetExtendedNodeInfo(this.Command);
			VarVec varVec = this.Command.CreateVarVec(extendedNodeInfo.Definitions);
			foreach (CollectionInfo collectionInfo2 in list3)
			{
				varVec.Set(collectionInfo2.CollectionVar);
			}
			NestBaseOp op2 = this.Command.CreateMultiStreamNestOp(new List<SortKey>(), varVec, list3);
			return this.Command.CreateNode(op2, list);
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x00022B84 File Offset: 0x00020D84
		private Node ApplyIsNotNullFilter(Node node, Var sentinelVar)
		{
			Node node2 = node;
			Node node3 = null;
			while (node2.Op.OpType == OpType.MultiStreamNest)
			{
				node3 = node2;
				node2 = node2.Child0;
			}
			Node node4 = this.CapWithIsNotNullFilter(node2, sentinelVar);
			Node result;
			if (node3 != null)
			{
				node3.Child0 = node4;
				result = node;
			}
			else
			{
				result = node4;
			}
			return result;
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x00022BCC File Offset: 0x00020DCC
		private Node CapWithIsNotNullFilter(Node input, Var var)
		{
			Node arg = this.Command.CreateNode(this.Command.CreateVarRefOp(var));
			Node arg2 = this.Command.CreateNode(this.Command.CreateConditionalOp(OpType.Not), this.Command.CreateNode(this.Command.CreateConditionalOp(OpType.IsNull), arg));
			return this.Command.CreateNode(this.Command.CreateFilterOp(), input, arg2);
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x00022C3D File Offset: 0x00020E3D
		protected override Node VisitApplyOp(ApplyBaseOp op, Node n)
		{
			return this.ApplyOpJoinOp(op, n);
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x000227B1 File Offset: 0x000209B1
		public override Node Visit(DistinctOp op, Node n)
		{
			return this.NestingNotSupported(op, n);
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x00022C48 File Offset: 0x00020E48
		public override Node Visit(FilterOp op, Node n)
		{
			this.VisitChildren(n);
			NestBaseOp nestBaseOp = n.Child0.Op as NestBaseOp;
			if (nestBaseOp != null)
			{
				Node child = n.Child0;
				Node child2 = child.Child0;
				n.Child0 = child2;
				child.Child0 = n;
				this.Command.RecomputeNodeInfo(n);
				this.Command.RecomputeNodeInfo(child);
				return child;
			}
			return n;
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x000227B1 File Offset: 0x000209B1
		public override Node Visit(GroupByOp op, Node n)
		{
			return this.NestingNotSupported(op, n);
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x00022CA8 File Offset: 0x00020EA8
		public override Node Visit(GroupByIntoOp op, Node n)
		{
			PlanCompiler.Assert(n.HasChild3 && n.Child3.Children.Count > 0, "GroupByIntoOp with no group aggregates?");
			Node child = n.Child3;
			VarVec vars = this.Command.CreateVarVec(op.Outputs);
			VarVec outputs = op.Outputs;
			foreach (Node node in child.Children)
			{
				VarDefOp varDefOp = node.Op as VarDefOp;
				outputs.Clear(varDefOp.Var);
			}
			Node arg = this.Command.CreateNode(this.Command.CreateGroupByOp(op.Keys, outputs), n.Child0, n.Child1, n.Child2);
			Node n2 = this.Command.CreateNode(this.Command.CreateProjectOp(vars), arg, child);
			return base.VisitNode(n2);
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x00022C3D File Offset: 0x00020E3D
		protected override Node VisitJoinOp(JoinBaseOp op, Node n)
		{
			return this.ApplyOpJoinOp(op, n);
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x00022DAC File Offset: 0x00020FAC
		public override Node Visit(ProjectOp op, Node n)
		{
			this.VisitChildren(n);
			this.m_varRemapper.RemapNode(n);
			Node result;
			if (n.Child0.Op.OpType == OpType.Sort)
			{
				Node child = n.Child0;
				foreach (SortKey sortKey in ((SortOp)child.Op).Keys)
				{
					if (!this.Command.GetExtendedNodeInfo(child).ExternalReferences.IsSet(sortKey.Var))
					{
						op.Outputs.Set(sortKey.Var);
					}
				}
				n.Child0 = child.Child0;
				this.Command.RecomputeNodeInfo(n);
				child.Child0 = this.HandleProjectNode(n);
				this.Command.RecomputeNodeInfo(child);
				result = child;
			}
			else
			{
				result = this.HandleProjectNode(n);
			}
			return result;
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x00022EA0 File Offset: 0x000210A0
		private Node HandleProjectNode(Node n)
		{
			Node node = this.ProjectOpCase1(n);
			if (node.Op.OpType == OpType.Project && NestPullup.IsNestOpNode(node.Child0))
			{
				node = this.ProjectOpCase2(node);
			}
			return this.MergeNestedNestOps(node);
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x00022EE4 File Offset: 0x000210E4
		private Node MergeNestedNestOps(Node nestNode)
		{
			if (!NestPullup.IsNestOpNode(nestNode) || !NestPullup.IsNestOpNode(nestNode.Child0))
			{
				return nestNode;
			}
			NestBaseOp nestBaseOp = (NestBaseOp)nestNode.Op;
			Node child = nestNode.Child0;
			NestBaseOp nestBaseOp2 = (NestBaseOp)child.Op;
			VarVec varVec = this.Command.CreateVarVec();
			foreach (CollectionInfo collectionInfo in nestBaseOp.CollectionInfo)
			{
				varVec.Set(collectionInfo.CollectionVar);
			}
			List<Node> list = new List<Node>();
			List<CollectionInfo> list2 = new List<CollectionInfo>();
			VarVec varVec2 = this.Command.CreateVarVec(nestBaseOp.Outputs);
			list.Add(child.Child0);
			for (int i = 1; i < child.Children.Count; i++)
			{
				CollectionInfo collectionInfo2 = nestBaseOp2.CollectionInfo[i - 1];
				if (varVec.IsSet(collectionInfo2.CollectionVar) || varVec2.IsSet(collectionInfo2.CollectionVar))
				{
					list2.Add(collectionInfo2);
					list.Add(child.Children[i]);
					PlanCompiler.Assert(varVec2.IsSet(collectionInfo2.CollectionVar), "collectionVar not in output Vars?");
				}
			}
			for (int j = 1; j < nestNode.Children.Count; j++)
			{
				CollectionInfo collectionInfo3 = nestBaseOp.CollectionInfo[j - 1];
				list2.Add(collectionInfo3);
				list.Add(nestNode.Children[j]);
				PlanCompiler.Assert(varVec2.IsSet(collectionInfo3.CollectionVar), "collectionVar not in output Vars?");
			}
			List<SortKey> list3 = this.ConsolidateSortKeys(nestBaseOp.PrefixSortKeys, nestBaseOp2.PrefixSortKeys);
			foreach (SortKey sortKey in list3)
			{
				varVec2.Set(sortKey.Var);
			}
			MultiStreamNestOp op = this.Command.CreateMultiStreamNestOp(list3, varVec2, list2);
			Node node = this.Command.CreateNode(op, list);
			this.Command.RecomputeNodeInfo(node);
			return node;
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x0002311C File Offset: 0x0002131C
		private Node ProjectOpCase1(Node projectNode)
		{
			ProjectOp projectOp = (ProjectOp)projectNode.Op;
			List<CollectionInfo> collectionInfoList = new List<CollectionInfo>();
			List<Node> list = new List<Node>();
			List<Node> list2 = new List<Node>();
			VarVec varVec = this.Command.CreateVarVec();
			VarVec varVec2 = this.Command.CreateVarVec();
			List<Node> list3 = new List<Node>();
			List<Node> list4 = new List<Node>();
			foreach (Node node in projectNode.Child1.Children)
			{
				VarDefOp varDefOp = (VarDefOp)node.Op;
				Node child = node.Child0;
				if (OpType.Collect == child.Op.OpType)
				{
					PlanCompiler.Assert(child.HasChild0, "collect without input?");
					PlanCompiler.Assert(OpType.PhysicalProject == child.Child0.Op.OpType, "collect without physicalProject?");
					Node child2 = child.Child0;
					this.m_definingNodeMap.Add(varDefOp.Var, child2);
					this.ConvertToNestOpInput(child2, varDefOp.Var, collectionInfoList, list2, varVec, varVec2);
				}
				else if (OpType.VarRef == child.Op.OpType)
				{
					Var var = ((VarRefOp)child.Op).Var;
					Node node2;
					if (this.m_definingNodeMap.TryGetValue(var, out node2))
					{
						node2 = this.CopyCollectionVarDefinition(node2);
						this.m_definingNodeMap.Add(varDefOp.Var, node2);
						this.ConvertToNestOpInput(node2, varDefOp.Var, collectionInfoList, list2, varVec, varVec2);
					}
					else
					{
						list4.Add(node);
						list.Add(node);
					}
				}
				else
				{
					list3.Add(node);
					list.Add(node);
				}
			}
			if (list2.Count == 0)
			{
				return projectNode;
			}
			VarVec varVec3 = this.Command.CreateVarVec(projectOp.Outputs);
			VarVec varVec4 = this.Command.CreateVarVec(projectOp.Outputs);
			varVec4.Minus(varVec2);
			varVec4.Or(varVec);
			if (!varVec4.IsEmpty)
			{
				if (NestPullup.IsNestOpNode(projectNode.Child0))
				{
					if (list3.Count == 0 && list4.Count == 0)
					{
						projectNode = projectNode.Child0;
						this.EnsureReferencedVarsAreRemoved(list4, varVec3);
					}
					else
					{
						NestBaseOp nestBaseOp = (NestBaseOp)projectNode.Child0.Op;
						List<Node> list5 = new List<Node>();
						list5.Add(projectNode.Child0.Child0);
						list4.AddRange(list3);
						list5.Add(this.Command.CreateNode(this.Command.CreateVarDefListOp(), list4));
						VarVec varVec5 = this.Command.CreateVarVec(nestBaseOp.Outputs);
						foreach (CollectionInfo collectionInfo in nestBaseOp.CollectionInfo)
						{
							varVec5.Clear(collectionInfo.CollectionVar);
						}
						foreach (Node node3 in list4)
						{
							varVec5.Set(((VarDefOp)node3.Op).Var);
						}
						Node item = this.Command.CreateNode(this.Command.CreateProjectOp(varVec5), list5);
						VarVec varVec6 = this.Command.CreateVarVec(varVec5);
						varVec6.Or(nestBaseOp.Outputs);
						MultiStreamNestOp op = this.Command.CreateMultiStreamNestOp(nestBaseOp.PrefixSortKeys, varVec6, nestBaseOp.CollectionInfo);
						List<Node> list6 = new List<Node>();
						list6.Add(item);
						for (int i = 1; i < projectNode.Child0.Children.Count; i++)
						{
							list6.Add(projectNode.Child0.Children[i]);
						}
						projectNode = this.Command.CreateNode(op, list6);
					}
				}
				else
				{
					ProjectOp op2 = this.Command.CreateProjectOp(varVec4);
					projectNode.Child1 = this.Command.CreateNode(projectNode.Child1.Op, list);
					projectNode.Op = op2;
					this.EnsureReferencedVarsAreRemapped(list4);
				}
			}
			else
			{
				projectNode = projectNode.Child0;
				this.EnsureReferencedVarsAreRemoved(list4, varVec3);
			}
			varVec.And(projectNode.GetExtendedNodeInfo(this.Command).Definitions);
			varVec3.Or(varVec);
			MultiStreamNestOp op3 = this.Command.CreateMultiStreamNestOp(new List<SortKey>(), varVec3, collectionInfoList);
			list2.Insert(0, projectNode);
			Node node4 = this.Command.CreateNode(op3, list2);
			this.Command.RecomputeNodeInfo(projectNode);
			this.Command.RecomputeNodeInfo(node4);
			return node4;
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x000235F0 File Offset: 0x000217F0
		private void EnsureReferencedVarsAreRemoved(List<Node> referencedVars, VarVec outputVars)
		{
			foreach (Node node in referencedVars)
			{
				VarDefOp varDefOp = (VarDefOp)node.Op;
				Var var = varDefOp.Var;
				Var var2 = this.ResolveVarReference(var);
				this.m_varRemapper.AddMapping(var, var2);
				outputVars.Clear(var);
				outputVars.Set(var2);
			}
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x00023670 File Offset: 0x00021870
		private void EnsureReferencedVarsAreRemapped(List<Node> referencedVars)
		{
			foreach (Node node in referencedVars)
			{
				VarDefOp varDefOp = (VarDefOp)node.Op;
				Var var = varDefOp.Var;
				Var oldVar = this.ResolveVarReference(var);
				this.m_varRemapper.AddMapping(oldVar, var);
			}
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x000236E4 File Offset: 0x000218E4
		private void ConvertToNestOpInput(Node physicalProjectNode, Var collectionVar, List<CollectionInfo> collectionInfoList, List<Node> collectionNodes, VarVec externalReferences, VarVec collectionReferences)
		{
			externalReferences.Or(this.Command.GetNodeInfo(physicalProjectNode).ExternalReferences);
			Node child = physicalProjectNode.Child0;
			PhysicalProjectOp physicalProjectOp = (PhysicalProjectOp)physicalProjectNode.Op;
			VarList varList = Command.CreateVarList(physicalProjectOp.Outputs);
			VarVec varVec = this.Command.CreateVarVec(varList);
			List<SortKey> list = null;
			if (OpType.Sort == child.Op.OpType)
			{
				SortOp sortOp = (SortOp)child.Op;
				list = OpCopier.Copy(this.Command, sortOp.Keys);
				using (List<SortKey>.Enumerator enumerator = list.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						SortKey sortKey = enumerator.Current;
						if (!varVec.IsSet(sortKey.Var))
						{
							varList.Add(sortKey.Var);
							varVec.Set(sortKey.Var);
						}
					}
					goto IL_D4;
				}
			}
			list = new List<SortKey>();
			IL_D4:
			VarVec keyVars = this.Command.GetExtendedNodeInfo(child).Keys.KeyVars;
			VarVec varVec2 = keyVars.Clone();
			varVec2.Minus(varVec);
			VarVec keys = varVec2.IsEmpty ? keyVars.Clone() : this.Command.CreateVarVec();
			CollectionInfo item = Command.CreateCollectionInfo(collectionVar, physicalProjectOp.ColumnMap.Element, varList, keys, list, null);
			collectionInfoList.Add(item);
			collectionNodes.Add(child);
			collectionReferences.Set(collectionVar);
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x00023850 File Offset: 0x00021A50
		private Node ProjectOpCase2(Node projectNode)
		{
			ProjectOp projectOp = (ProjectOp)projectNode.Op;
			Node node = projectNode.Child0;
			NestBaseOp nestBaseOp = node.Op as NestBaseOp;
			VarVec varVec = this.Command.CreateVarVec();
			foreach (CollectionInfo collectionInfo in nestBaseOp.CollectionInfo)
			{
				varVec.Set(collectionInfo.CollectionVar);
			}
			VarVec varVec2 = this.Command.CreateVarVec(nestBaseOp.Outputs);
			varVec2.Minus(varVec);
			VarVec varVec3 = this.Command.CreateVarVec(projectOp.Outputs);
			varVec3.Minus(varVec);
			VarVec varVec4 = this.Command.CreateVarVec(projectOp.Outputs);
			varVec4.Minus(varVec3);
			VarVec varVec5 = this.Command.CreateVarVec(varVec);
			varVec5.Minus(varVec4);
			List<CollectionInfo> list;
			List<Node> list2;
			if (varVec5.IsEmpty)
			{
				list = nestBaseOp.CollectionInfo;
				list2 = new List<Node>(node.Children);
			}
			else
			{
				list = new List<CollectionInfo>();
				list2 = new List<Node>();
				list2.Add(node.Child0);
				int num = 1;
				foreach (CollectionInfo collectionInfo2 in nestBaseOp.CollectionInfo)
				{
					if (!varVec5.IsSet(collectionInfo2.CollectionVar))
					{
						list.Add(collectionInfo2);
						list2.Add(node.Children[num]);
					}
					num++;
				}
			}
			VarVec varVec6 = this.Command.CreateVarVec();
			for (int i = 1; i < node.Children.Count; i++)
			{
				varVec6.Or(node.Children[i].GetExtendedNodeInfo(this.Command).ExternalReferences);
			}
			varVec6.And(node.Child0.GetExtendedNodeInfo(this.Command).Definitions);
			VarVec varVec7 = this.Command.CreateVarVec(varVec3);
			varVec7.Or(varVec2);
			varVec7.Or(varVec6);
			List<Node> list3 = new List<Node>(projectNode.Child1.Children.Count);
			foreach (Node node2 in projectNode.Child1.Children)
			{
				VarDefOp varDefOp = (VarDefOp)node2.Op;
				if (varVec7.IsSet(varDefOp.Var))
				{
					list3.Add(node2);
				}
			}
			if (list.Count != 0 && varVec7.IsEmpty)
			{
				PlanCompiler.Assert(list3.Count == 0, "outputs is empty with non-zero count of children?");
				NullOp op = this.Command.CreateNullOp(this.Command.StringType);
				Node definingExpr = this.Command.CreateNode(op);
				Var v;
				Node item = this.Command.CreateVarDefNode(definingExpr, out v);
				list3.Add(item);
				varVec7.Set(v);
			}
			projectNode.Op = this.Command.CreateProjectOp(this.Command.CreateVarVec(varVec7));
			projectNode.Child1 = this.Command.CreateNode(projectNode.Child1.Op, list3);
			if (list.Count == 0)
			{
				projectNode.Child0 = node.Child0;
				node = projectNode;
			}
			else
			{
				VarVec varVec8 = this.Command.CreateVarVec(projectOp.Outputs);
				for (int j = 1; j < list2.Count; j++)
				{
					varVec8.Or(list2[j].GetNodeInfo(this.Command).ExternalReferences);
				}
				foreach (SortKey sortKey in nestBaseOp.PrefixSortKeys)
				{
					varVec8.Set(sortKey.Var);
				}
				node.Op = this.Command.CreateMultiStreamNestOp(nestBaseOp.PrefixSortKeys, varVec8, list);
				node = this.Command.CreateNode(node.Op, list2);
				projectNode.Child0 = node.Child0;
				node.Child0 = projectNode;
				this.Command.RecomputeNodeInfo(projectNode);
			}
			this.Command.RecomputeNodeInfo(node);
			return node;
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x000227B1 File Offset: 0x000209B1
		protected override Node VisitSetOp(SetOp op, Node n)
		{
			return this.NestingNotSupported(op, n);
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x00023CB0 File Offset: 0x00021EB0
		public override Node Visit(SingleRowOp op, Node n)
		{
			this.VisitChildren(n);
			if (NestPullup.IsNestOpNode(n.Child0))
			{
				n = n.Child0;
				Node child = this.Command.CreateNode(op, n.Child0);
				n.Child0 = child;
				this.Command.RecomputeNodeInfo(n);
			}
			return n;
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x00023D00 File Offset: 0x00021F00
		public override Node Visit(SortOp op, Node n)
		{
			this.VisitChildren(n);
			this.m_varRemapper.RemapNode(n);
			NestBaseOp nestBaseOp = n.Child0.Op as NestBaseOp;
			if (nestBaseOp != null)
			{
				n.Child0.Op = this.GetNestOpWithConsolidatedSortKeys(nestBaseOp, op.Keys);
				return n.Child0;
			}
			return n;
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x00023D54 File Offset: 0x00021F54
		public override Node Visit(ConstrainedSortOp op, Node n)
		{
			this.VisitChildren(n);
			NestBaseOp nestBaseOp = n.Child0.Op as NestBaseOp;
			if (nestBaseOp != null)
			{
				Node child = n.Child0;
				n.Child0 = child.Child0;
				child.Child0 = n;
				child.Op = this.GetNestOpWithConsolidatedSortKeys(nestBaseOp, op.Keys);
				n = child;
			}
			return n;
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x00023DB0 File Offset: 0x00021FB0
		private NestBaseOp GetNestOpWithConsolidatedSortKeys(NestBaseOp inputNestOp, List<SortKey> sortKeys)
		{
			NestBaseOp result;
			if (inputNestOp.PrefixSortKeys.Count == 0)
			{
				foreach (SortKey sortKey in sortKeys)
				{
					inputNestOp.PrefixSortKeys.Add(Command.CreateSortKey(sortKey.Var, sortKey.AscendingSort, sortKey.Collation));
				}
				result = inputNestOp;
			}
			else
			{
				VarVec varVec = this.Command.CreateVarVec();
				List<SortKey> prefixSortKeys = this.ConsolidateSortKeys(sortKeys, inputNestOp.PrefixSortKeys);
				PlanCompiler.Assert(inputNestOp is MultiStreamNestOp, "Unexpected SingleStreamNestOp?");
				result = this.Command.CreateMultiStreamNestOp(prefixSortKeys, inputNestOp.Outputs, inputNestOp.CollectionInfo);
			}
			return result;
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x00023E74 File Offset: 0x00022074
		private List<SortKey> ConsolidateSortKeys(List<SortKey> sortKeyList1, List<SortKey> sortKeyList2)
		{
			VarVec varVec = this.Command.CreateVarVec();
			List<SortKey> list = new List<SortKey>();
			foreach (SortKey sortKey in sortKeyList1)
			{
				if (!varVec.IsSet(sortKey.Var))
				{
					varVec.Set(sortKey.Var);
					list.Add(Command.CreateSortKey(sortKey.Var, sortKey.AscendingSort, sortKey.Collation));
				}
			}
			foreach (SortKey sortKey2 in sortKeyList2)
			{
				if (!varVec.IsSet(sortKey2.Var))
				{
					varVec.Set(sortKey2.Var);
					list.Add(Command.CreateSortKey(sortKey2.Var, sortKey2.AscendingSort, sortKey2.Collation));
				}
			}
			return list;
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x00023F7C File Offset: 0x0002217C
		public override Node Visit(UnnestOp op, Node n)
		{
			this.VisitChildren(n);
			CollectionType edmType = TypeHelpers.GetEdmType<CollectionType>(op.Var.Type);
			if (TypeUtils.IsUdt(edmType.TypeUsage))
			{
				return n;
			}
			PlanCompiler.Assert(n.Child0.Op.OpType == OpType.VarDef, "Unnest without VarDef input?");
			PlanCompiler.Assert(((VarDefOp)n.Child0.Op).Var == op.Var, "Unnest var not found?");
			PlanCompiler.Assert(n.Child0.HasChild0, "VarDef without input?");
			Node node = n.Child0.Child0;
			if (OpType.Function == node.Op.OpType)
			{
				return n;
			}
			if (OpType.Collect == node.Op.OpType)
			{
				PlanCompiler.Assert(node.HasChild0, "collect without input?");
				node = node.Child0;
				PlanCompiler.Assert(node.Op.OpType == OpType.PhysicalProject, "collect without physicalProject?");
				this.m_definingNodeMap.Add(op.Var, node);
			}
			else
			{
				if (OpType.VarRef != node.Op.OpType)
				{
					throw EntityUtil.InternalError(EntityUtil.InternalErrorCode.InvalidInternalTree, 2, node.Op.OpType);
				}
				Var var = ((VarRefOp)node.Op).Var;
				Node refVarDefiningNode;
				bool condition = this.m_definingNodeMap.TryGetValue(var, out refVarDefiningNode);
				PlanCompiler.Assert(condition, "Could not find a definition for a referenced collection var");
				node = this.CopyCollectionVarDefinition(refVarDefiningNode);
				PlanCompiler.Assert(node.Op.OpType == OpType.PhysicalProject, "driving node is not physicalProject?");
			}
			IEnumerable<Var> outputs = ((PhysicalProjectOp)node.Op).Outputs;
			PlanCompiler.Assert(node.HasChild0, "physicalProject without input?");
			node = node.Child0;
			if (node.Op.OpType == OpType.Sort)
			{
				this.m_foundSortUnderUnnest = true;
			}
			this.UpdateReplacementVarMap(op.Table.Columns, outputs);
			return node;
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x00024148 File Offset: 0x00022348
		private Node CopyCollectionVarDefinition(Node refVarDefiningNode)
		{
			VarMap varMap;
			Dictionary<Var, Node> dictionary;
			Node result = OpCopierTrackingCollectionVars.Copy(this.Command, refVarDefiningNode, out varMap, out dictionary);
			if (dictionary.Count != 0)
			{
				VarMap reverseMap = varMap.GetReverseMap();
				foreach (KeyValuePair<Var, Node> keyValuePair in dictionary)
				{
					Var key = reverseMap[keyValuePair.Key];
					Node node;
					if (this.m_definingNodeMap.TryGetValue(key, out node))
					{
						PhysicalProjectOp physicalProjectOp = (PhysicalProjectOp)node.Op;
						VarList outputVars = VarRemapper.RemapVarList(this.Command, varMap, physicalProjectOp.Outputs);
						SimpleCollectionColumnMap columnMap = (SimpleCollectionColumnMap)ColumnMapCopier.Copy(physicalProjectOp.ColumnMap, varMap);
						PhysicalProjectOp op = this.Command.CreatePhysicalProjectOp(outputVars, columnMap);
						Node value = this.Command.CreateNode(op, keyValuePair.Value);
						this.m_definingNodeMap.Add(keyValuePair.Key, value);
					}
				}
			}
			return result;
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x00024250 File Offset: 0x00022450
		protected override Node VisitNestOp(NestBaseOp op, Node n)
		{
			this.VisitChildren(n);
			foreach (Node n2 in n.Children)
			{
				if (NestPullup.IsNestOpNode(n2))
				{
					throw EntityUtil.InternalError(EntityUtil.InternalErrorCode.NestOverNest);
				}
			}
			return n;
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x000242B8 File Offset: 0x000224B8
		public override Node Visit(PhysicalProjectOp op, Node n)
		{
			PlanCompiler.Assert(n.Children.Count == 1, "multiple inputs to physicalProject?");
			this.VisitChildren(n);
			this.m_varRemapper.RemapNode(n);
			if (n != this.Command.Root || !NestPullup.IsNestOpNode(n.Child0))
			{
				return n;
			}
			Node node = n.Child0;
			Dictionary<Var, ColumnMap> dictionary = new Dictionary<Var, ColumnMap>();
			VarList varList = Command.CreateVarList(from v in op.Outputs
			where v.VarType == VarType.Parameter
			select v);
			SimpleColumnMap[] keys;
			node = this.ConvertToSingleStreamNest(node, dictionary, varList, out keys);
			SingleStreamNestOp ssnOp = (SingleStreamNestOp)node.Op;
			Node child = this.BuildSortForNestElimination(ssnOp, node);
			SimpleCollectionColumnMap simpleCollectionColumnMap = (SimpleCollectionColumnMap)ColumnMapTranslator.Translate(((PhysicalProjectOp)n.Op).ColumnMap, dictionary);
			simpleCollectionColumnMap = new SimpleCollectionColumnMap(simpleCollectionColumnMap.Type, simpleCollectionColumnMap.Name, simpleCollectionColumnMap.Element, keys, null);
			n.Op = this.Command.CreatePhysicalProjectOp(varList, simpleCollectionColumnMap);
			n.Child0 = child;
			return n;
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x000243C8 File Offset: 0x000225C8
		private Node BuildSortForNestElimination(SingleStreamNestOp ssnOp, Node nestNode)
		{
			List<SortKey> list = this.BuildSortKeyList(ssnOp);
			Node result;
			if (list.Count > 0)
			{
				SortOp op = this.Command.CreateSortOp(list);
				result = this.Command.CreateNode(op, nestNode.Child0);
			}
			else
			{
				result = nestNode.Child0;
			}
			return result;
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x00024410 File Offset: 0x00022610
		private List<SortKey> BuildSortKeyList(SingleStreamNestOp ssnOp)
		{
			VarVec varVec = this.Command.CreateVarVec();
			List<SortKey> list = new List<SortKey>();
			foreach (SortKey sortKey in ssnOp.PrefixSortKeys)
			{
				if (!varVec.IsSet(sortKey.Var))
				{
					varVec.Set(sortKey.Var);
					list.Add(sortKey);
				}
			}
			foreach (Var v in ssnOp.Keys)
			{
				if (!varVec.IsSet(v))
				{
					varVec.Set(v);
					SortKey item = Command.CreateSortKey(v);
					list.Add(item);
				}
			}
			PlanCompiler.Assert(!varVec.IsSet(ssnOp.Discriminator), "prefix sort on discriminator?");
			list.Add(Command.CreateSortKey(ssnOp.Discriminator));
			foreach (SortKey sortKey2 in ssnOp.PostfixSortKeys)
			{
				if (!varVec.IsSet(sortKey2.Var))
				{
					varVec.Set(sortKey2.Var);
					list.Add(sortKey2);
				}
			}
			return list;
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x0002457C File Offset: 0x0002277C
		private Node ConvertToSingleStreamNest(Node nestNode, Dictionary<Var, ColumnMap> varRefReplacementMap, VarList flattenedOutputVarList, out SimpleColumnMap[] parentKeyColumnMaps)
		{
			MultiStreamNestOp multiStreamNestOp = (MultiStreamNestOp)nestNode.Op;
			for (int i = 1; i < nestNode.Children.Count; i++)
			{
				Node node = nestNode.Children[i];
				if (node.Op.OpType == OpType.MultiStreamNest)
				{
					CollectionInfo collectionInfo = multiStreamNestOp.CollectionInfo[i - 1];
					VarList varList = Command.CreateVarList();
					SimpleColumnMap[] array;
					nestNode.Children[i] = this.ConvertToSingleStreamNest(node, varRefReplacementMap, varList, out array);
					ColumnMap columnMap = ColumnMapTranslator.Translate(collectionInfo.ColumnMap, varRefReplacementMap);
					VarVec keys = this.Command.CreateVarVec(((SingleStreamNestOp)nestNode.Children[i].Op).Keys);
					multiStreamNestOp.CollectionInfo[i - 1] = Command.CreateCollectionInfo(collectionInfo.CollectionVar, columnMap, varList, keys, collectionInfo.SortKeys, null);
				}
			}
			Node child = nestNode.Child0;
			KeyVec keyVec = this.Command.PullupKeys(child);
			if (keyVec.NoKeys)
			{
				throw EntityUtil.KeysRequiredForNesting();
			}
			ExtendedNodeInfo extendedNodeInfo = this.Command.GetExtendedNodeInfo(child);
			VarVec definitions = extendedNodeInfo.Definitions;
			VarList varList2 = Command.CreateVarList(definitions);
			VarList discriminatorVarList;
			List<List<SortKey>> list;
			this.NormalizeNestOpInputs(multiStreamNestOp, nestNode, out discriminatorVarList, out list);
			Var var;
			List<Dictionary<Var, Var>> list2;
			Node arg = this.BuildUnionAllSubqueryForNestOp(multiStreamNestOp, nestNode, varList2, discriminatorVarList, out var, out list2);
			Dictionary<Var, Var> dictionary = list2[0];
			flattenedOutputVarList.AddRange(this.RemapVars(varList2, dictionary));
			VarVec varVec = this.Command.CreateVarVec(flattenedOutputVarList);
			VarVec varVec2 = this.Command.CreateVarVec(varVec);
			foreach (KeyValuePair<Var, Var> keyValuePair in dictionary)
			{
				if (keyValuePair.Key != keyValuePair.Value)
				{
					varRefReplacementMap[keyValuePair.Key] = new VarRefColumnMap(keyValuePair.Value);
				}
			}
			NestPullup.RemapSortKeys(multiStreamNestOp.PrefixSortKeys, dictionary);
			List<SortKey> list3 = new List<SortKey>();
			List<CollectionInfo> list4 = new List<CollectionInfo>();
			VarRefColumnMap discriminator = new VarRefColumnMap(var);
			varVec2.Set(var);
			if (!varVec.IsSet(var))
			{
				flattenedOutputVarList.Add(var);
				varVec.Set(var);
			}
			VarVec varVec3 = this.RemapVarVec(keyVec.KeyVars, dictionary);
			parentKeyColumnMaps = new SimpleColumnMap[varVec3.Count];
			int num = 0;
			foreach (Var var2 in varVec3)
			{
				parentKeyColumnMaps[num] = new VarRefColumnMap(var2);
				num++;
				if (!varVec.IsSet(var2))
				{
					flattenedOutputVarList.Add(var2);
					varVec.Set(var2);
				}
			}
			for (int j = 1; j < nestNode.Children.Count; j++)
			{
				CollectionInfo collectionInfo2 = multiStreamNestOp.CollectionInfo[j - 1];
				List<SortKey> list5 = list[j];
				NestPullup.RemapSortKeys(list5, list2[j]);
				list3.AddRange(list5);
				ColumnMap columnMap2 = ColumnMapTranslator.Translate(collectionInfo2.ColumnMap, list2[j]);
				VarList varList3 = this.RemapVarList(collectionInfo2.FlattenedElementVars, list2[j]);
				VarVec keys2 = this.RemapVarVec(collectionInfo2.Keys, list2[j]);
				NestPullup.RemapSortKeys(collectionInfo2.SortKeys, list2[j]);
				CollectionInfo collectionInfo3 = Command.CreateCollectionInfo(collectionInfo2.CollectionVar, columnMap2, varList3, keys2, collectionInfo2.SortKeys, j);
				list4.Add(collectionInfo3);
				foreach (Var var3 in varList3)
				{
					if (!varVec.IsSet(var3))
					{
						flattenedOutputVarList.Add(var3);
						varVec.Set(var3);
					}
				}
				varVec2.Set(collectionInfo2.CollectionVar);
				int num2 = 0;
				SimpleColumnMap[] array2 = new SimpleColumnMap[collectionInfo3.Keys.Count];
				foreach (Var v in collectionInfo3.Keys)
				{
					array2[num2] = new VarRefColumnMap(v);
					num2++;
				}
				DiscriminatedCollectionColumnMap value = new DiscriminatedCollectionColumnMap(TypeUtils.CreateCollectionType(collectionInfo3.ColumnMap.Type), collectionInfo3.ColumnMap.Name, collectionInfo3.ColumnMap, array2, parentKeyColumnMaps, discriminator, collectionInfo3.DiscriminatorValue);
				varRefReplacementMap[collectionInfo2.CollectionVar] = value;
			}
			SingleStreamNestOp op = this.Command.CreateSingleStreamNestOp(varVec3, multiStreamNestOp.PrefixSortKeys, list3, varVec2, list4, var);
			return this.Command.CreateNode(op, arg);
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x00024A54 File Offset: 0x00022C54
		private void NormalizeNestOpInputs(NestBaseOp nestOp, Node nestNode, out VarList discriminatorVarList, out List<List<SortKey>> sortKeys)
		{
			discriminatorVarList = Command.CreateVarList();
			discriminatorVarList.Add(null);
			sortKeys = new List<List<SortKey>>();
			sortKeys.Add(nestOp.PrefixSortKeys);
			for (int i = 1; i < nestNode.Children.Count; i++)
			{
				Node node = nestNode.Children[i];
				SingleStreamNestOp singleStreamNestOp = node.Op as SingleStreamNestOp;
				if (singleStreamNestOp != null)
				{
					List<SortKey> item = this.BuildSortKeyList(singleStreamNestOp);
					sortKeys.Add(item);
					node = node.Child0;
				}
				else
				{
					SortOp sortOp = node.Op as SortOp;
					if (sortOp != null)
					{
						node = node.Child0;
						sortKeys.Add(sortOp.Keys);
					}
					else
					{
						sortKeys.Add(new List<SortKey>());
					}
				}
				VarList flattenedElementVars = nestOp.CollectionInfo[i - 1].FlattenedElementVars;
				foreach (SortKey sortKey in sortKeys[i])
				{
					if (!flattenedElementVars.Contains(sortKey.Var))
					{
						flattenedElementVars.Add(sortKey.Var);
					}
				}
				Var item2;
				Node value = this.AugmentNodeWithInternalIntegerConstant(node, i, out item2);
				nestNode.Children[i] = value;
				discriminatorVarList.Add(item2);
			}
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x00024BA8 File Offset: 0x00022DA8
		private Node AugmentNodeWithInternalIntegerConstant(Node input, int value, out Var internalConstantVar)
		{
			return this.AugmentNodeWithConstant(input, () => this.Command.CreateInternalConstantOp(this.Command.IntegerType, value), out internalConstantVar);
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x00024BE0 File Offset: 0x00022DE0
		private Node AugmentNodeWithConstant(Node input, Func<ConstantBaseOp> createOp, out Var constantVar)
		{
			ConstantBaseOp op = createOp();
			Node definingExpr = this.Command.CreateNode(op);
			Node arg = this.Command.CreateVarDefListNode(definingExpr, out constantVar);
			ExtendedNodeInfo extendedNodeInfo = this.Command.GetExtendedNodeInfo(input);
			VarVec varVec = this.Command.CreateVarVec(extendedNodeInfo.Definitions);
			varVec.Set(constantVar);
			ProjectOp op2 = this.Command.CreateProjectOp(varVec);
			return this.Command.CreateNode(op2, input, arg);
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x00024C5C File Offset: 0x00022E5C
		private Node BuildUnionAllSubqueryForNestOp(NestBaseOp nestOp, Node nestNode, VarList drivingNodeVars, VarList discriminatorVarList, out Var discriminatorVar, out List<Dictionary<Var, Var>> varMapList)
		{
			Node child = nestNode.Child0;
			Node node = null;
			VarList varList = null;
			for (int i = 1; i < nestNode.Children.Count; i++)
			{
				VarList varList2;
				Node arg;
				VarList collection;
				Op op;
				if (i > 1)
				{
					arg = OpCopier.Copy(this.Command, child, drivingNodeVars, out varList2);
					VarRemapper varRemapper = new VarRemapper(this.Command);
					for (int j = 0; j < drivingNodeVars.Count; j++)
					{
						varRemapper.AddMapping(drivingNodeVars[j], varList2[j]);
					}
					varRemapper.RemapSubtree(nestNode.Children[i]);
					collection = varRemapper.RemapVarList(nestOp.CollectionInfo[i - 1].FlattenedElementVars);
					op = this.Command.CreateCrossApplyOp();
				}
				else
				{
					arg = child;
					varList2 = drivingNodeVars;
					collection = nestOp.CollectionInfo[i - 1].FlattenedElementVars;
					op = this.Command.CreateOuterApplyOp();
				}
				Node arg2 = this.Command.CreateNode(op, arg, nestNode.Children[i]);
				List<Node> list = new List<Node>();
				VarList varList3 = Command.CreateVarList();
				varList3.Add(discriminatorVarList[i]);
				varList3.AddRange(varList2);
				for (int k = 1; k < nestNode.Children.Count; k++)
				{
					CollectionInfo collectionInfo = nestOp.CollectionInfo[k - 1];
					if (i == k)
					{
						varList3.AddRange(collection);
					}
					else
					{
						foreach (Var var in collectionInfo.FlattenedElementVars)
						{
							NullOp op2 = this.Command.CreateNullOp(var.Type);
							Node definingExpr = this.Command.CreateNode(op2);
							Var item2;
							Node item = this.Command.CreateVarDefNode(definingExpr, out item2);
							list.Add(item);
							varList3.Add(item2);
						}
					}
				}
				Node arg3 = this.Command.CreateNode(this.Command.CreateVarDefListOp(), list);
				VarVec vars = this.Command.CreateVarVec(varList3);
				ProjectOp op3 = this.Command.CreateProjectOp(vars);
				Node node2 = this.Command.CreateNode(op3, arg2, arg3);
				if (node == null)
				{
					node = node2;
					varList = varList3;
				}
				else
				{
					VarMap varMap = new VarMap();
					VarMap varMap2 = new VarMap();
					for (int l = 0; l < varList.Count; l++)
					{
						Var key = this.Command.CreateSetOpVar(varList[l].Type);
						varMap.Add(key, varList[l]);
						varMap2.Add(key, varList3[l]);
					}
					UnionAllOp unionAllOp = this.Command.CreateUnionAllOp(varMap, varMap2);
					node = this.Command.CreateNode(unionAllOp, node, node2);
					varList = NestPullup.GetUnionOutputs(unionAllOp, varList);
				}
			}
			varMapList = new List<Dictionary<Var, Var>>();
			IEnumerator<Var> enumerator2 = varList.GetEnumerator();
			if (!enumerator2.MoveNext())
			{
				throw EntityUtil.InternalError(EntityUtil.InternalErrorCode.ColumnCountMismatch, 4);
			}
			discriminatorVar = enumerator2.Current;
			for (int m = 0; m < nestNode.Children.Count; m++)
			{
				Dictionary<Var, Var> dictionary = new Dictionary<Var, Var>();
				VarList varList4 = (m == 0) ? drivingNodeVars : nestOp.CollectionInfo[m - 1].FlattenedElementVars;
				foreach (Var key2 in varList4)
				{
					if (!enumerator2.MoveNext())
					{
						throw EntityUtil.InternalError(EntityUtil.InternalErrorCode.ColumnCountMismatch, 5);
					}
					dictionary[key2] = enumerator2.Current;
				}
				varMapList.Add(dictionary);
			}
			if (enumerator2.MoveNext())
			{
				throw EntityUtil.InternalError(EntityUtil.InternalErrorCode.ColumnCountMismatch, 6);
			}
			return node;
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x00025034 File Offset: 0x00023234
		private static VarList GetUnionOutputs(UnionAllOp unionOp, VarList leftVars)
		{
			VarMap varMap = unionOp.VarMap[0];
			Dictionary<Var, Var> reverseMap = varMap.GetReverseMap();
			VarList varList = Command.CreateVarList();
			foreach (Var key in leftVars)
			{
				Var item = reverseMap[key];
				varList.Add(item);
			}
			return varList;
		}

		// Token: 0x040007BC RID: 1980
		private PlanCompiler m_compilerState;

		// Token: 0x040007BD RID: 1981
		private Dictionary<Var, Node> m_definingNodeMap = new Dictionary<Var, Node>();

		// Token: 0x040007BE RID: 1982
		private VarRemapper m_varRemapper;

		// Token: 0x040007BF RID: 1983
		private Dictionary<Var, Var> m_varRefMap = new Dictionary<Var, Var>();

		// Token: 0x040007C0 RID: 1984
		private bool m_foundSortUnderUnnest;
	}
}
