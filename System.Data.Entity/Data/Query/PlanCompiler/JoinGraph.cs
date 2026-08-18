using System;
using System.Collections.Generic;
using System.Data.Metadata.Edm;
using System.Data.Query.InternalTrees;
using System.Linq;

namespace System.Data.Query.PlanCompiler
{
	// Token: 0x02000055 RID: 85
	internal class JoinGraph
	{
		// Token: 0x060006FE RID: 1790 RVA: 0x0001F578 File Offset: 0x0001D778
		internal JoinGraph(Command command, ConstraintManager constraintManager, VarRefManager varRefManager, Node joinNode, bool isSqlCe)
		{
			this.m_command = command;
			this.m_constraintManager = constraintManager;
			this.m_varRefManager = varRefManager;
			this.m_isSqlCe = isSqlCe;
			this.m_vertexes = new List<AugmentedNode>();
			this.m_tableVertexes = new List<AugmentedTableNode>();
			this.m_tableVertexMap = new Dictionary<Table, AugmentedTableNode>();
			this.m_varMap = new VarMap();
			this.m_reverseVarMap = new Dictionary<Var, VarVec>();
			this.m_varToDefiningNodeMap = new Dictionary<Var, AugmentedTableNode>();
			this.m_processedNodes = new Dictionary<Node, Node>();
			this.m_root = (this.BuildAugmentedNodeTree(joinNode) as AugmentedJoinNode);
			PlanCompiler.Assert(this.m_root != null, "The root isn't a join?");
			this.BuildJoinEdges(this.m_root, this.m_root.Id);
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x0001F634 File Offset: 0x0001D834
		internal Node DoJoinElimination(out VarMap varMap, out Dictionary<Node, Node> processedNodes)
		{
			this.TryTurnLeftOuterJoinsIntoInnerJoins();
			this.GenerateTransitiveEdges();
			this.EliminateSelfJoins();
			this.EliminateParentChildJoins();
			Node result = this.BuildNodeTree();
			varMap = this.m_varMap;
			processedNodes = this.m_processedNodes;
			return result;
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x0001F674 File Offset: 0x0001D874
		private VarVec GetColumnVars(VarVec varVec)
		{
			VarVec varVec2 = this.m_command.CreateVarVec();
			foreach (Var var in varVec)
			{
				if (var.VarType == VarType.Column)
				{
					varVec2.Set(var);
				}
			}
			return varVec2;
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x0001F6D4 File Offset: 0x0001D8D4
		private static void GetColumnVars(List<ColumnVar> columnVars, IEnumerable<Var> vec)
		{
			foreach (Var var in vec)
			{
				PlanCompiler.Assert(var.VarType == VarType.Column, "Expected a columnVar. Found " + var.VarType.ToString());
				columnVars.Add((ColumnVar)var);
			}
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x0001F750 File Offset: 0x0001D950
		private void SplitPredicate(Node joinNode, out List<ColumnVar> leftVars, out List<ColumnVar> rightVars, out Node otherPredicateNode)
		{
			leftVars = new List<ColumnVar>();
			rightVars = new List<ColumnVar>();
			otherPredicateNode = joinNode.Child2;
			if (joinNode.Op.OpType == OpType.FullOuterJoin)
			{
				return;
			}
			Predicate predicate = new Predicate(this.m_command, joinNode.Child2);
			ExtendedNodeInfo extendedNodeInfo = this.m_command.GetExtendedNodeInfo(joinNode.Child0);
			ExtendedNodeInfo extendedNodeInfo2 = this.m_command.GetExtendedNodeInfo(joinNode.Child1);
			VarVec columnVars = this.GetColumnVars(extendedNodeInfo.Definitions);
			VarVec columnVars2 = this.GetColumnVars(extendedNodeInfo2.Definitions);
			List<Var> vec;
			List<Var> vec2;
			Predicate predicate2;
			predicate.GetEquiJoinPredicates(columnVars, columnVars2, out vec, out vec2, out predicate2);
			otherPredicateNode = predicate2.BuildAndTree();
			JoinGraph.GetColumnVars(leftVars, vec);
			JoinGraph.GetColumnVars(rightVars, vec2);
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x0001F800 File Offset: 0x0001DA00
		private AugmentedNode BuildAugmentedNodeTree(Node node)
		{
			AugmentedNode augmentedNode;
			switch (node.Op.OpType)
			{
			case OpType.ScanTable:
			{
				this.m_processedNodes[node] = node;
				ScanTableOp scanTableOp = (ScanTableOp)node.Op;
				augmentedNode = new AugmentedTableNode(this.m_vertexes.Count, node);
				this.m_tableVertexMap[scanTableOp.Table] = (AugmentedTableNode)augmentedNode;
				goto IL_16A;
			}
			case OpType.InnerJoin:
			case OpType.LeftOuterJoin:
			case OpType.FullOuterJoin:
			{
				this.m_processedNodes[node] = node;
				AugmentedNode leftChild = this.BuildAugmentedNodeTree(node.Child0);
				AugmentedNode rightChild = this.BuildAugmentedNodeTree(node.Child1);
				List<ColumnVar> leftVars;
				List<ColumnVar> rightVars;
				Node otherPredicate;
				this.SplitPredicate(node, out leftVars, out rightVars, out otherPredicate);
				this.m_varRefManager.AddChildren(node);
				augmentedNode = new AugmentedJoinNode(this.m_vertexes.Count, node, leftChild, rightChild, leftVars, rightVars, otherPredicate);
				goto IL_16A;
			}
			case OpType.CrossJoin:
			{
				this.m_processedNodes[node] = node;
				List<AugmentedNode> list = new List<AugmentedNode>();
				foreach (Node node2 in node.Children)
				{
					list.Add(this.BuildAugmentedNodeTree(node2));
				}
				augmentedNode = new AugmentedJoinNode(this.m_vertexes.Count, node, list);
				this.m_varRefManager.AddChildren(node);
				goto IL_16A;
			}
			}
			augmentedNode = new AugmentedNode(this.m_vertexes.Count, node);
			IL_16A:
			this.m_vertexes.Add(augmentedNode);
			return augmentedNode;
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x0001F994 File Offset: 0x0001DB94
		private bool AddJoinEdge(AugmentedJoinNode joinNode, ColumnVar leftVar, ColumnVar rightVar)
		{
			AugmentedTableNode augmentedTableNode;
			if (!this.m_tableVertexMap.TryGetValue(leftVar.Table, out augmentedTableNode))
			{
				return false;
			}
			AugmentedTableNode augmentedTableNode2;
			if (!this.m_tableVertexMap.TryGetValue(rightVar.Table, out augmentedTableNode2))
			{
				return false;
			}
			if (augmentedTableNode.LastVisibleId < joinNode.Id || augmentedTableNode2.LastVisibleId < joinNode.Id)
			{
				return false;
			}
			foreach (JoinEdge joinEdge in augmentedTableNode.JoinEdges)
			{
				if (joinEdge.Right.Table.Equals(rightVar.Table))
				{
					return joinEdge.AddCondition(joinNode, leftVar, rightVar);
				}
			}
			JoinEdge item = JoinEdge.CreateJoinEdge(augmentedTableNode, augmentedTableNode2, joinNode, leftVar, rightVar);
			augmentedTableNode.JoinEdges.Add(item);
			joinNode.JoinEdges.Add(item);
			return true;
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x0001FA7C File Offset: 0x0001DC7C
		private static bool SingleTableVars(IEnumerable<ColumnVar> varList)
		{
			Table table = null;
			foreach (ColumnVar columnVar in varList)
			{
				if (table == null)
				{
					table = columnVar.Table;
				}
				else if (columnVar.Table != table)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x0001FADC File Offset: 0x0001DCDC
		private void BuildJoinEdges(AugmentedJoinNode joinNode, int maxVisibility)
		{
			OpType opType = joinNode.Node.Op.OpType;
			if (opType == OpType.CrossJoin)
			{
				foreach (AugmentedNode node in joinNode.Children)
				{
					this.BuildJoinEdges(node, maxVisibility);
				}
				return;
			}
			int maxVisibility2;
			int maxVisibility3;
			if (opType == OpType.FullOuterJoin)
			{
				maxVisibility2 = joinNode.Id;
				maxVisibility3 = joinNode.Id;
			}
			else if (opType == OpType.LeftOuterJoin)
			{
				maxVisibility2 = maxVisibility;
				maxVisibility3 = joinNode.Id;
			}
			else
			{
				maxVisibility2 = maxVisibility;
				maxVisibility3 = maxVisibility;
			}
			this.BuildJoinEdges(joinNode.Children[0], maxVisibility2);
			this.BuildJoinEdges(joinNode.Children[1], maxVisibility3);
			if (joinNode.Node.Op.OpType == OpType.FullOuterJoin || joinNode.OtherPredicate != null || joinNode.LeftVars.Count == 0)
			{
				return;
			}
			if (opType == OpType.LeftOuterJoin && (!JoinGraph.SingleTableVars(joinNode.RightVars) || !JoinGraph.SingleTableVars(joinNode.LeftVars)))
			{
				return;
			}
			JoinKind joinKind = (opType == OpType.LeftOuterJoin) ? JoinKind.LeftOuter : JoinKind.Inner;
			for (int i = 0; i < joinNode.LeftVars.Count; i++)
			{
				if (this.AddJoinEdge(joinNode, joinNode.LeftVars[i], joinNode.RightVars[i]) && joinKind == JoinKind.Inner)
				{
					this.AddJoinEdge(joinNode, joinNode.RightVars[i], joinNode.LeftVars[i]);
				}
			}
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x0001FC50 File Offset: 0x0001DE50
		private void BuildJoinEdges(AugmentedNode node, int maxVisibility)
		{
			OpType opType = node.Node.Op.OpType;
			if (opType != OpType.ScanTable)
			{
				if (opType - OpType.InnerJoin <= 3)
				{
					this.BuildJoinEdges(node as AugmentedJoinNode, maxVisibility);
					return;
				}
			}
			else
			{
				AugmentedTableNode augmentedTableNode = (AugmentedTableNode)node;
				augmentedTableNode.LastVisibleId = maxVisibility;
			}
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x0001FC98 File Offset: 0x0001DE98
		private bool GenerateTransitiveEdge(JoinEdge edge1, JoinEdge edge2)
		{
			PlanCompiler.Assert(edge1.Right == edge2.Left, "need a common table for transitive predicate generation");
			if (edge2.Right == edge1.Left)
			{
				return false;
			}
			if (edge1.JoinKind != edge2.JoinKind)
			{
				return false;
			}
			if (edge1.JoinKind == JoinKind.LeftOuter && (edge1.Left != edge1.Right || edge2.Left != edge2.Right))
			{
				return false;
			}
			if (edge1.JoinKind == JoinKind.LeftOuter && edge1.RightVars.Count != edge2.LeftVars.Count)
			{
				return false;
			}
			foreach (JoinEdge joinEdge in edge1.Left.JoinEdges)
			{
				if (joinEdge.Right == edge2.Right)
				{
					return false;
				}
			}
			IEnumerable<KeyValuePair<ColumnVar, ColumnVar>> enumerable = JoinGraph.CreateOrderedKeyValueList(edge1.RightVars, edge1.LeftVars);
			IEnumerable<KeyValuePair<ColumnVar, ColumnVar>> enumerable2 = JoinGraph.CreateOrderedKeyValueList(edge2.LeftVars, edge2.RightVars);
			IEnumerator<KeyValuePair<ColumnVar, ColumnVar>> enumerator2 = enumerable.GetEnumerator();
			IEnumerator<KeyValuePair<ColumnVar, ColumnVar>> enumerator3 = enumerable2.GetEnumerator();
			List<ColumnVar> list = new List<ColumnVar>();
			List<ColumnVar> list2 = new List<ColumnVar>();
			bool flag = enumerator2.MoveNext() && enumerator3.MoveNext();
			while (flag)
			{
				KeyValuePair<ColumnVar, ColumnVar> keyValuePair = enumerator2.Current;
				ColumnVar key = keyValuePair.Key;
				keyValuePair = enumerator3.Current;
				if (key == keyValuePair.Key)
				{
					List<ColumnVar> list3 = list;
					keyValuePair = enumerator2.Current;
					list3.Add(keyValuePair.Value);
					List<ColumnVar> list4 = list2;
					keyValuePair = enumerator3.Current;
					list4.Add(keyValuePair.Value);
					flag = (enumerator2.MoveNext() && enumerator3.MoveNext());
				}
				else
				{
					if (edge1.JoinKind == JoinKind.LeftOuter)
					{
						return false;
					}
					keyValuePair = enumerator2.Current;
					int id = keyValuePair.Key.Id;
					keyValuePair = enumerator3.Current;
					if (id > keyValuePair.Key.Id)
					{
						flag = enumerator3.MoveNext();
					}
					else
					{
						flag = enumerator2.MoveNext();
					}
				}
			}
			JoinEdge item = JoinEdge.CreateTransitiveJoinEdge(edge1.Left, edge2.Right, edge1.JoinKind, list, list2);
			edge1.Left.JoinEdges.Add(item);
			if (edge1.JoinKind == JoinKind.Inner)
			{
				JoinEdge item2 = JoinEdge.CreateTransitiveJoinEdge(edge2.Right, edge1.Left, edge1.JoinKind, list2, list);
				edge2.Right.JoinEdges.Add(item2);
			}
			return true;
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x0001FEF8 File Offset: 0x0001E0F8
		private static IEnumerable<KeyValuePair<ColumnVar, ColumnVar>> CreateOrderedKeyValueList(List<ColumnVar> keyVars, List<ColumnVar> valueVars)
		{
			List<KeyValuePair<ColumnVar, ColumnVar>> list = new List<KeyValuePair<ColumnVar, ColumnVar>>(keyVars.Count);
			for (int i = 0; i < keyVars.Count; i++)
			{
				list.Add(new KeyValuePair<ColumnVar, ColumnVar>(keyVars[i], valueVars[i]));
			}
			return from kv in list
			orderby kv.Key.Id
			select kv;
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x0001FF60 File Offset: 0x0001E160
		private void TryTurnLeftOuterJoinsIntoInnerJoins()
		{
			foreach (AugmentedJoinNode augmentedJoinNode in from j in this.m_vertexes.OfType<AugmentedJoinNode>()
			where j.Node.Op.OpType == OpType.LeftOuterJoin && j.JoinEdges.Count > 0
			select j)
			{
				bool flag;
				if (!this.m_isSqlCe && !EntityUtil.UseFx40CompatMode)
				{
					flag = this.CanAllJoinEdgesBeTurnedIntoInnerJoins(augmentedJoinNode.Children[1], augmentedJoinNode.JoinEdges);
				}
				else
				{
					flag = (augmentedJoinNode.Children.All((AugmentedNode c) => c is AugmentedTableNode) && augmentedJoinNode.JoinEdges.All((JoinEdge joinEdge) => this.IsConstraintPresentForTurningIntoInnerJoin(joinEdge)));
				}
				if (flag)
				{
					augmentedJoinNode.Node.Op = this.m_command.CreateInnerJoinOp();
					this.m_modifiedGraph = true;
					List<JoinEdge> list = new List<JoinEdge>(augmentedJoinNode.JoinEdges.Count);
					foreach (JoinEdge joinEdge3 in augmentedJoinNode.JoinEdges)
					{
						joinEdge3.JoinKind = JoinKind.Inner;
						if (!this.ContainsJoinEdgeForTable(joinEdge3.Right.JoinEdges, joinEdge3.Left.Table))
						{
							JoinEdge joinEdge2 = JoinEdge.CreateJoinEdge(joinEdge3.Right, joinEdge3.Left, augmentedJoinNode, joinEdge3.RightVars[0], joinEdge3.LeftVars[0]);
							joinEdge3.Right.JoinEdges.Add(joinEdge2);
							list.Add(joinEdge2);
							for (int i = 1; i < joinEdge3.LeftVars.Count; i++)
							{
								joinEdge2.AddCondition(augmentedJoinNode, joinEdge3.RightVars[i], joinEdge3.LeftVars[i]);
							}
						}
					}
					augmentedJoinNode.JoinEdges.AddRange(list);
				}
			}
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x0002019C File Offset: 0x0001E39C
		private bool AreAllTableRowsPreserved(AugmentedNode root, AugmentedTableNode table)
		{
			if (root is AugmentedTableNode)
			{
				return true;
			}
			AugmentedNode augmentedNode = table;
			for (;;)
			{
				AugmentedJoinNode augmentedJoinNode = (AugmentedJoinNode)augmentedNode.Parent;
				if (augmentedJoinNode.Node.Op.OpType != OpType.LeftOuterJoin || augmentedJoinNode.Children[0] != augmentedNode)
				{
					break;
				}
				augmentedNode = augmentedJoinNode;
				if (augmentedNode == root)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x000201F0 File Offset: 0x0001E3F0
		private bool ContainsJoinEdgeForTable(IEnumerable<JoinEdge> joinEdges, Table table)
		{
			foreach (JoinEdge joinEdge in joinEdges)
			{
				if (joinEdge.Right.Table.Equals(table))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x0002024C File Offset: 0x0001E44C
		private bool CanAllJoinEdgesBeTurnedIntoInnerJoins(AugmentedNode rightNode, IEnumerable<JoinEdge> joinEdges)
		{
			foreach (JoinEdge joinEdge in joinEdges)
			{
				if (!this.CanJoinEdgeBeTurnedIntoInnerJoin(rightNode, joinEdge))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x000202A0 File Offset: 0x0001E4A0
		private bool CanJoinEdgeBeTurnedIntoInnerJoin(AugmentedNode rightNode, JoinEdge joinEdge)
		{
			return this.AreAllTableRowsPreserved(rightNode, joinEdge.Right) && this.IsConstraintPresentForTurningIntoInnerJoin(joinEdge);
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x000202BC File Offset: 0x0001E4BC
		private bool IsConstraintPresentForTurningIntoInnerJoin(JoinEdge joinEdge)
		{
			List<ForeignKeyConstraint> list;
			if (this.m_constraintManager.IsParentChildRelationship(joinEdge.Right.Table.TableMetadata.Extent, joinEdge.Left.Table.TableMetadata.Extent, out list))
			{
				PlanCompiler.Assert(list != null && list.Count > 0, "invalid fk constraints?");
				foreach (ForeignKeyConstraint foreignKeyConstraint in list)
				{
					IList<ColumnVar> source;
					if (JoinGraph.IsJoinOnFkConstraint(foreignKeyConstraint, joinEdge.RightVars, joinEdge.LeftVars, out source) && foreignKeyConstraint.ParentKeys.Count == joinEdge.RightVars.Count)
					{
						if ((from v in source
						where v.ColumnMetadata.IsNullable
						select v).Count<ColumnVar>() == 0)
						{
							return true;
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x000203BC File Offset: 0x0001E5BC
		private void GenerateTransitiveEdges()
		{
			foreach (AugmentedNode augmentedNode in this.m_vertexes)
			{
				AugmentedTableNode augmentedTableNode = augmentedNode as AugmentedTableNode;
				if (augmentedTableNode != null)
				{
					for (int i = 0; i < augmentedTableNode.JoinEdges.Count; i++)
					{
						JoinEdge joinEdge = augmentedTableNode.JoinEdges[i];
						int j = 0;
						AugmentedTableNode right = joinEdge.Right;
						while (j < right.JoinEdges.Count)
						{
							JoinEdge edge = right.JoinEdges[j];
							this.GenerateTransitiveEdge(joinEdge, edge);
							j++;
						}
					}
				}
			}
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x00020478 File Offset: 0x0001E678
		private static bool CanBeEliminatedBasedOnLojParticipation(AugmentedTableNode table, AugmentedTableNode replacingTable)
		{
			if (replacingTable.Id < table.NewLocationId)
			{
				return JoinGraph.CanBeMovedBasedOnLojParticipation(table, replacingTable);
			}
			return JoinGraph.CanBeMovedBasedOnLojParticipation(replacingTable, table);
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x00020498 File Offset: 0x0001E698
		private static bool CanBeEliminatedViaStarJoinBasedOnOtherJoinParticipation(JoinEdge tableJoinEdge, JoinEdge replacingTableJoinEdge)
		{
			if (tableJoinEdge.JoinNode == null || replacingTableJoinEdge.JoinNode == null)
			{
				return false;
			}
			AugmentedNode leastCommonAncestor = JoinGraph.GetLeastCommonAncestor(tableJoinEdge.Right, replacingTableJoinEdge.Right);
			return !JoinGraph.CanGetFileredByJoins(tableJoinEdge, leastCommonAncestor, true) && !JoinGraph.CanGetFileredByJoins(replacingTableJoinEdge, leastCommonAncestor, false);
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x000204E0 File Offset: 0x0001E6E0
		private static bool CanGetFileredByJoins(JoinEdge joinEdge, AugmentedNode leastCommonAncestor, bool disallowAnyJoin)
		{
			AugmentedNode augmentedNode = joinEdge.Right;
			AugmentedNode parent = augmentedNode.Parent;
			while (parent != null && augmentedNode != leastCommonAncestor)
			{
				if (parent.Node != joinEdge.JoinNode.Node && (disallowAnyJoin || parent.Node.Op.OpType != OpType.LeftOuterJoin || parent.Children[0] != augmentedNode))
				{
					return true;
				}
				augmentedNode = augmentedNode.Parent;
				parent = augmentedNode.Parent;
			}
			return false;
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x00020550 File Offset: 0x0001E750
		private static bool CanBeMovedBasedOnLojParticipation(AugmentedTableNode table, AugmentedTableNode replacingTable)
		{
			AugmentedNode leastCommonAncestor = JoinGraph.GetLeastCommonAncestor(table, replacingTable);
			AugmentedNode augmentedNode = table;
			while (augmentedNode.Parent != null && augmentedNode != leastCommonAncestor)
			{
				if (augmentedNode.Parent.Node.Op.OpType == OpType.LeftOuterJoin && augmentedNode.Parent.Children[0] == augmentedNode)
				{
					return false;
				}
				augmentedNode = augmentedNode.Parent;
			}
			return true;
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x000205AC File Offset: 0x0001E7AC
		private static AugmentedNode GetLeastCommonAncestor(AugmentedNode node1, AugmentedNode node2)
		{
			if (node1.Id == node2.Id)
			{
				return node1;
			}
			AugmentedNode augmentedNode;
			AugmentedNode augmentedNode2;
			if (node1.Id < node2.Id)
			{
				augmentedNode = node1;
				augmentedNode2 = node2;
			}
			else
			{
				augmentedNode = node2;
				augmentedNode2 = node1;
			}
			while (augmentedNode.Id < augmentedNode2.Id)
			{
				augmentedNode = augmentedNode.Parent;
			}
			return augmentedNode;
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x000205FC File Offset: 0x0001E7FC
		private void MarkTableAsEliminated<T>(AugmentedTableNode tableNode, AugmentedTableNode replacementNode, List<T> tableVars, List<T> replacementVars) where T : Var
		{
			PlanCompiler.Assert(tableVars != null && replacementVars != null, "null vars");
			PlanCompiler.Assert(tableVars.Count == replacementVars.Count, "var count mismatch");
			PlanCompiler.Assert(tableVars.Count > 0, "no vars in the table ?");
			this.m_modifiedGraph = true;
			if (tableNode.Id < replacementNode.NewLocationId)
			{
				tableNode.ReplacementTable = replacementNode;
				replacementNode.NewLocationId = tableNode.Id;
			}
			else
			{
				tableNode.ReplacementTable = null;
			}
			for (int i = 0; i < tableVars.Count; i++)
			{
				if (tableNode.Table.ReferencedColumns.IsSet(tableVars[i]))
				{
					this.m_varMap[tableVars[i]] = replacementVars[i];
					this.AddReverseMapping(replacementVars[i], tableVars[i]);
					replacementNode.Table.ReferencedColumns.Set(replacementVars[i]);
				}
			}
			foreach (Var key in replacementNode.Table.ReferencedColumns)
			{
				this.m_varToDefiningNodeMap[key] = replacementNode;
			}
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x0002075C File Offset: 0x0001E95C
		private void AddReverseMapping(Var replacingVar, Var replacedVar)
		{
			VarVec varVec;
			if (this.m_reverseVarMap.TryGetValue(replacedVar, out varVec))
			{
				this.m_reverseVarMap.Remove(replacedVar);
			}
			VarVec varVec2;
			if (!this.m_reverseVarMap.TryGetValue(replacingVar, out varVec2))
			{
				if (varVec != null)
				{
					varVec2 = varVec;
				}
				else
				{
					varVec2 = this.m_command.CreateVarVec();
				}
				this.m_reverseVarMap[replacingVar] = varVec2;
			}
			else if (varVec != null)
			{
				varVec2.Or(varVec);
			}
			varVec2.Set(replacedVar);
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x000207C9 File Offset: 0x0001E9C9
		private void EliminateSelfJoinedTable(AugmentedTableNode tableNode, AugmentedTableNode replacementNode)
		{
			this.MarkTableAsEliminated<Var>(tableNode, replacementNode, tableNode.Table.Columns, replacementNode.Table.Columns);
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x000207EC File Offset: 0x0001E9EC
		private void EliminateStarSelfJoin(List<JoinEdge> joinEdges)
		{
			List<List<JoinEdge>> list = new List<List<JoinEdge>>();
			foreach (JoinEdge joinEdge in joinEdges)
			{
				bool flag = false;
				foreach (List<JoinEdge> list2 in list)
				{
					if (this.AreMatchingForStarSelfJoinElimination(list2[0], joinEdge))
					{
						list2.Add(joinEdge);
						flag = true;
						break;
					}
				}
				if (!flag && this.QualifiesForStarSelfJoinGroup(joinEdge))
				{
					list.Add(new List<JoinEdge>
					{
						joinEdge
					});
				}
			}
			foreach (List<JoinEdge> list3 in from l in list
			where l.Count > 1
			select l)
			{
				JoinEdge joinEdge2 = list3[0];
				foreach (JoinEdge joinEdge3 in list3)
				{
					if (joinEdge2.Right.Id > joinEdge3.Right.Id)
					{
						joinEdge2 = joinEdge3;
					}
				}
				foreach (JoinEdge joinEdge4 in list3)
				{
					if (joinEdge4 != joinEdge2 && JoinGraph.CanBeEliminatedViaStarJoinBasedOnOtherJoinParticipation(joinEdge4, joinEdge2))
					{
						this.EliminateSelfJoinedTable(joinEdge4.Right, joinEdge2.Right);
					}
				}
			}
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x000209D4 File Offset: 0x0001EBD4
		private bool AreMatchingForStarSelfJoinElimination(JoinEdge edge1, JoinEdge edge2)
		{
			if (edge2.LeftVars.Count != edge1.LeftVars.Count || edge2.JoinKind != edge1.JoinKind)
			{
				return false;
			}
			for (int i = 0; i < edge2.LeftVars.Count; i++)
			{
				if (!edge2.LeftVars[i].Equals(edge1.LeftVars[i]) || !edge2.RightVars[i].ColumnMetadata.Name.Equals(edge1.RightVars[i].ColumnMetadata.Name))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x00020A74 File Offset: 0x0001EC74
		private bool QualifiesForStarSelfJoinGroup(JoinEdge joinEdge)
		{
			VarVec varVec = this.m_command.CreateVarVec(joinEdge.Right.Table.Keys);
			foreach (Var v in joinEdge.RightVars)
			{
				if (joinEdge.JoinKind == JoinKind.LeftOuter && !varVec.IsSet(v))
				{
					return false;
				}
				varVec.Clear(v);
			}
			return varVec.IsEmpty;
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x00020B08 File Offset: 0x0001ED08
		private void EliminateStarSelfJoins(AugmentedTableNode tableNode)
		{
			Dictionary<EntitySetBase, List<JoinEdge>> dictionary = new Dictionary<EntitySetBase, List<JoinEdge>>();
			foreach (JoinEdge joinEdge in tableNode.JoinEdges)
			{
				if (!joinEdge.IsEliminated)
				{
					List<JoinEdge> list;
					if (!dictionary.TryGetValue(joinEdge.Right.Table.TableMetadata.Extent, out list))
					{
						list = new List<JoinEdge>();
						dictionary[joinEdge.Right.Table.TableMetadata.Extent] = list;
					}
					list.Add(joinEdge);
				}
			}
			foreach (KeyValuePair<EntitySetBase, List<JoinEdge>> keyValuePair in dictionary)
			{
				if (keyValuePair.Value.Count > 1)
				{
					this.EliminateStarSelfJoin(keyValuePair.Value);
				}
			}
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x00020C00 File Offset: 0x0001EE00
		private bool EliminateSelfJoin(JoinEdge joinEdge)
		{
			if (joinEdge.IsEliminated)
			{
				return false;
			}
			if (!joinEdge.Left.Table.TableMetadata.Extent.Equals(joinEdge.Right.Table.TableMetadata.Extent))
			{
				return false;
			}
			for (int i = 0; i < joinEdge.LeftVars.Count; i++)
			{
				if (!joinEdge.LeftVars[i].ColumnMetadata.Name.Equals(joinEdge.RightVars[i].ColumnMetadata.Name))
				{
					return false;
				}
			}
			VarVec varVec = this.m_command.CreateVarVec(joinEdge.Left.Table.Keys);
			foreach (Var v in joinEdge.LeftVars)
			{
				if (joinEdge.JoinKind == JoinKind.LeftOuter && !varVec.IsSet(v))
				{
					return false;
				}
				varVec.Clear(v);
			}
			if (!varVec.IsEmpty)
			{
				return false;
			}
			if (!JoinGraph.CanBeEliminatedBasedOnLojParticipation(joinEdge.Right, joinEdge.Left))
			{
				return false;
			}
			this.EliminateSelfJoinedTable(joinEdge.Right, joinEdge.Left);
			return true;
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x00020D44 File Offset: 0x0001EF44
		private void EliminateSelfJoins(AugmentedTableNode tableNode)
		{
			if (tableNode.IsEliminated)
			{
				return;
			}
			foreach (JoinEdge joinEdge in tableNode.JoinEdges)
			{
				this.EliminateSelfJoin(joinEdge);
			}
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x00020DA4 File Offset: 0x0001EFA4
		private void EliminateSelfJoins()
		{
			foreach (AugmentedNode augmentedNode in this.m_vertexes)
			{
				AugmentedTableNode augmentedTableNode = augmentedNode as AugmentedTableNode;
				if (augmentedTableNode != null)
				{
					this.EliminateSelfJoins(augmentedTableNode);
					this.EliminateStarSelfJoins(augmentedTableNode);
				}
			}
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x00020E08 File Offset: 0x0001F008
		private void EliminateLeftTable(JoinEdge joinEdge)
		{
			PlanCompiler.Assert(joinEdge.JoinKind == JoinKind.Inner, "Expected inner join");
			this.MarkTableAsEliminated<ColumnVar>(joinEdge.Left, joinEdge.Right, joinEdge.LeftVars, joinEdge.RightVars);
			if (joinEdge.Right.NullableColumns == null)
			{
				joinEdge.Right.NullableColumns = this.m_command.CreateVarVec();
			}
			foreach (ColumnVar columnVar in joinEdge.RightVars)
			{
				if (columnVar.ColumnMetadata.IsNullable)
				{
					joinEdge.Right.NullableColumns.Set(columnVar);
				}
			}
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x00020EC8 File Offset: 0x0001F0C8
		private void EliminateRightTable(JoinEdge joinEdge)
		{
			PlanCompiler.Assert(joinEdge.JoinKind == JoinKind.LeftOuter, "Expected left-outer-join");
			PlanCompiler.Assert(joinEdge.Left.Id < joinEdge.Right.Id, string.Concat(new string[]
			{
				"(left-id, right-id) = (",
				joinEdge.Left.Id.ToString(),
				",",
				joinEdge.Right.Id.ToString(),
				")"
			}));
			this.MarkTableAsEliminated<ColumnVar>(joinEdge.Right, joinEdge.Left, joinEdge.RightVars, joinEdge.LeftVars);
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x00020F72 File Offset: 0x0001F172
		private static bool HasNonKeyReferences(Table table)
		{
			return !table.Keys.Subsumes(table.ReferencedColumns);
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x00020F88 File Offset: 0x0001F188
		private bool RightTableHasKeyReferences(JoinEdge joinEdge)
		{
			if (joinEdge.JoinNode == null)
			{
				return true;
			}
			VarVec varVec = null;
			foreach (Var key in joinEdge.Right.Table.Keys)
			{
				VarVec other;
				if (this.m_reverseVarMap.TryGetValue(key, out other))
				{
					if (varVec == null)
					{
						varVec = joinEdge.Right.Table.Keys.Clone();
					}
					varVec.Or(other);
				}
			}
			if (varVec == null)
			{
				varVec = joinEdge.Right.Table.Keys;
			}
			return this.m_varRefManager.HasKeyReferences(varVec, joinEdge.Right.Node, joinEdge.JoinNode.Node);
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x0002104C File Offset: 0x0001F24C
		private bool TryEliminateParentChildJoin(JoinEdge joinEdge, ForeignKeyConstraint fkConstraint)
		{
			if (joinEdge.JoinKind == JoinKind.LeftOuter && fkConstraint.ChildMultiplicity == RelationshipMultiplicity.Many)
			{
				return false;
			}
			IList<ColumnVar> list;
			if (!JoinGraph.IsJoinOnFkConstraint(fkConstraint, joinEdge.LeftVars, joinEdge.RightVars, out list))
			{
				return false;
			}
			if (joinEdge.JoinKind != JoinKind.Inner)
			{
				return this.TryEliminateRightTable(joinEdge, fkConstraint.ChildKeys.Count, fkConstraint.ChildMultiplicity == RelationshipMultiplicity.One);
			}
			if (JoinGraph.HasNonKeyReferences(joinEdge.Left.Table))
			{
				return false;
			}
			if (!JoinGraph.CanBeEliminatedBasedOnLojParticipation(joinEdge.Right, joinEdge.Left))
			{
				return false;
			}
			this.EliminateLeftTable(joinEdge);
			return true;
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x000210DC File Offset: 0x0001F2DC
		private static bool IsJoinOnFkConstraint(ForeignKeyConstraint fkConstraint, IList<ColumnVar> parentVars, IList<ColumnVar> childVars, out IList<ColumnVar> childForeignKeyVars)
		{
			childForeignKeyVars = new List<ColumnVar>(fkConstraint.ChildKeys.Count);
			foreach (string value in fkConstraint.ParentKeys)
			{
				bool flag = false;
				foreach (ColumnVar columnVar in parentVars)
				{
					if (columnVar.ColumnMetadata.Name.Equals(value))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			foreach (string value2 in fkConstraint.ChildKeys)
			{
				bool flag2 = false;
				int i = 0;
				while (i < parentVars.Count)
				{
					ColumnVar columnVar2 = childVars[i];
					if (columnVar2.ColumnMetadata.Name.Equals(value2))
					{
						childForeignKeyVars.Add(columnVar2);
						flag2 = true;
						ColumnVar columnVar3 = parentVars[i];
						string text;
						if (!fkConstraint.GetParentProperty(columnVar2.ColumnMetadata.Name, out text) || !text.Equals(columnVar3.ColumnMetadata.Name))
						{
							return false;
						}
						break;
					}
					else
					{
						i++;
					}
				}
				if (!flag2)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x00021260 File Offset: 0x0001F460
		private bool TryEliminateChildParentJoin(JoinEdge joinEdge, ForeignKeyConstraint fkConstraint)
		{
			IList<ColumnVar> list;
			if (!JoinGraph.IsJoinOnFkConstraint(fkConstraint, joinEdge.RightVars, joinEdge.LeftVars, out list))
			{
				return false;
			}
			if (list.Count > 1)
			{
				if ((from v in list
				where v.ColumnMetadata.IsNullable
				select v).Count<ColumnVar>() > 0)
				{
					return false;
				}
			}
			return this.TryEliminateRightTable(joinEdge, fkConstraint.ParentKeys.Count, true);
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x000212D0 File Offset: 0x0001F4D0
		private bool TryEliminateRightTable(JoinEdge joinEdge, int fkConstraintKeyCount, bool allowRefsForJoinedOnFkOnly)
		{
			if (JoinGraph.HasNonKeyReferences(joinEdge.Right.Table))
			{
				return false;
			}
			if ((!allowRefsForJoinedOnFkOnly || joinEdge.RightVars.Count != fkConstraintKeyCount) && this.RightTableHasKeyReferences(joinEdge))
			{
				return false;
			}
			if (!JoinGraph.CanBeEliminatedBasedOnLojParticipation(joinEdge.Right, joinEdge.Left))
			{
				return false;
			}
			this.EliminateRightTable(joinEdge);
			return true;
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x0002132C File Offset: 0x0001F52C
		private void EliminateParentChildJoin(JoinEdge joinEdge)
		{
			List<ForeignKeyConstraint> list;
			if (this.m_constraintManager.IsParentChildRelationship(joinEdge.Left.Table.TableMetadata.Extent, joinEdge.Right.Table.TableMetadata.Extent, out list))
			{
				PlanCompiler.Assert(list != null && list.Count > 0, "invalid fk constraints?");
				foreach (ForeignKeyConstraint fkConstraint in list)
				{
					if (this.TryEliminateParentChildJoin(joinEdge, fkConstraint))
					{
						return;
					}
				}
			}
			if (joinEdge.JoinKind == JoinKind.LeftOuter && this.m_constraintManager.IsParentChildRelationship(joinEdge.Right.Table.TableMetadata.Extent, joinEdge.Left.Table.TableMetadata.Extent, out list))
			{
				PlanCompiler.Assert(list != null && list.Count > 0, "invalid fk constraints?");
				foreach (ForeignKeyConstraint fkConstraint2 in list)
				{
					if (this.TryEliminateChildParentJoin(joinEdge, fkConstraint2))
					{
						break;
					}
				}
			}
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x00021478 File Offset: 0x0001F678
		private void EliminateParentChildJoins(AugmentedTableNode tableNode)
		{
			foreach (JoinEdge joinEdge in tableNode.JoinEdges)
			{
				this.EliminateParentChildJoin(joinEdge);
				if (tableNode.IsEliminated)
				{
					break;
				}
			}
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x000214D8 File Offset: 0x0001F6D8
		private void EliminateParentChildJoins()
		{
			foreach (AugmentedNode augmentedNode in this.m_vertexes)
			{
				AugmentedTableNode augmentedTableNode = augmentedNode as AugmentedTableNode;
				if (augmentedTableNode != null && !augmentedTableNode.IsEliminated)
				{
					this.EliminateParentChildJoins(augmentedTableNode);
				}
			}
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x00021540 File Offset: 0x0001F740
		private Node BuildNodeTree()
		{
			if (!this.m_modifiedGraph)
			{
				return this.m_root.Node;
			}
			VarMap varMap = new VarMap();
			foreach (KeyValuePair<Var, Var> keyValuePair in this.m_varMap)
			{
				Var var = keyValuePair.Value;
				Var var2;
				while (this.m_varMap.TryGetValue(var, out var2))
				{
					PlanCompiler.Assert(var2 != null, "null var mapping?");
					var = var2;
				}
				varMap[keyValuePair.Key] = var;
			}
			this.m_varMap = varMap;
			Dictionary<Node, int> dictionary;
			Node node = this.RebuildNodeTree(this.m_root, out dictionary);
			PlanCompiler.Assert(node != null, "Resulting node tree is null");
			PlanCompiler.Assert(dictionary == null || dictionary.Count == 0, "Leaking predicates?");
			return node;
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x00021624 File Offset: 0x0001F824
		private Node BuildFilterForNullableColumns(Node inputNode, VarVec nonNullableColumns)
		{
			if (nonNullableColumns == null)
			{
				return inputNode;
			}
			VarVec varVec = nonNullableColumns.Remap(this.m_varMap);
			if (varVec.IsEmpty)
			{
				return inputNode;
			}
			Node node = null;
			foreach (Var v in varVec)
			{
				Node arg = this.m_command.CreateNode(this.m_command.CreateVarRefOp(v));
				Node node2 = this.m_command.CreateNode(this.m_command.CreateConditionalOp(OpType.IsNull), arg);
				node2 = this.m_command.CreateNode(this.m_command.CreateConditionalOp(OpType.Not), node2);
				if (node == null)
				{
					node = node2;
				}
				else
				{
					node = this.m_command.CreateNode(this.m_command.CreateConditionalOp(OpType.And), node, node2);
				}
			}
			PlanCompiler.Assert(node != null, "Null predicate?");
			return this.m_command.CreateNode(this.m_command.CreateFilterOp(), inputNode, node);
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x00021728 File Offset: 0x0001F928
		private Node BuildFilterNode(Node inputNode, Node predicateNode)
		{
			if (predicateNode == null)
			{
				return inputNode;
			}
			return this.m_command.CreateNode(this.m_command.CreateFilterOp(), inputNode, predicateNode);
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x00021748 File Offset: 0x0001F948
		private Node RebuildPredicate(AugmentedJoinNode joinNode, out int minLocationId)
		{
			minLocationId = joinNode.Id;
			if (joinNode.OtherPredicate != null)
			{
				foreach (Var var in joinNode.OtherPredicate.GetNodeInfo(this.m_command).ExternalReferences)
				{
					Var var2;
					if (!this.m_varMap.TryGetValue(var, out var2))
					{
						var2 = var;
					}
					minLocationId = this.GetLeastCommonAncestor(minLocationId, this.GetLocationId(var2, minLocationId));
				}
			}
			Node node = joinNode.OtherPredicate;
			for (int i = 0; i < joinNode.LeftVars.Count; i++)
			{
				Var var3;
				if (!this.m_varMap.TryGetValue(joinNode.LeftVars[i], out var3))
				{
					var3 = joinNode.LeftVars[i];
				}
				Var var4;
				if (!this.m_varMap.TryGetValue(joinNode.RightVars[i], out var4))
				{
					var4 = joinNode.RightVars[i];
				}
				if (!var3.Equals(var4))
				{
					minLocationId = this.GetLeastCommonAncestor(minLocationId, this.GetLocationId(var3, minLocationId));
					minLocationId = this.GetLeastCommonAncestor(minLocationId, this.GetLocationId(var4, minLocationId));
					Node arg = this.m_command.CreateNode(this.m_command.CreateVarRefOp(var3));
					Node arg2 = this.m_command.CreateNode(this.m_command.CreateVarRefOp(var4));
					Node node2 = this.m_command.CreateNode(this.m_command.CreateComparisonOp(OpType.EQ), arg, arg2);
					if (node != null)
					{
						node = PlanCompilerUtil.CombinePredicates(node2, node, this.m_command);
					}
					else
					{
						node = node2;
					}
				}
			}
			return node;
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x000218F0 File Offset: 0x0001FAF0
		private Node RebuildNodeTreeForCrossJoins(AugmentedJoinNode joinNode)
		{
			List<Node> list = new List<Node>();
			foreach (AugmentedNode augmentedNode in joinNode.Children)
			{
				Dictionary<Node, int> dictionary;
				list.Add(this.RebuildNodeTree(augmentedNode, out dictionary));
				PlanCompiler.Assert(dictionary == null || dictionary.Count == 0, "Leaking predicates");
			}
			if (list.Count == 0)
			{
				return null;
			}
			if (list.Count == 1)
			{
				return list[0];
			}
			Node node = this.m_command.CreateNode(this.m_command.CreateCrossJoinOp(), list);
			this.m_processedNodes[node] = node;
			return node;
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x000219B0 File Offset: 0x0001FBB0
		private Node RebuildNodeTree(AugmentedJoinNode joinNode, out Dictionary<Node, int> predicates)
		{
			if (joinNode.Node.Op.OpType == OpType.CrossJoin)
			{
				predicates = null;
				return this.RebuildNodeTreeForCrossJoins(joinNode);
			}
			Dictionary<Node, int> leftPredicates;
			Node node = this.RebuildNodeTree(joinNode.Children[0], out leftPredicates);
			Dictionary<Node, int> rightPredicates;
			Node node2 = this.RebuildNodeTree(joinNode.Children[1], out rightPredicates);
			int id;
			Node node3;
			if (node != null && node2 == null && joinNode.Node.Op.OpType == OpType.LeftOuterJoin)
			{
				id = joinNode.Id;
				node3 = null;
			}
			else
			{
				node3 = this.RebuildPredicate(joinNode, out id);
			}
			node3 = this.CombinePredicateNodes(joinNode.Id, node3, id, leftPredicates, rightPredicates, out predicates);
			if (node == null && node2 == null)
			{
				if (node3 == null)
				{
					return null;
				}
				Node inputNode = this.m_command.CreateNode(this.m_command.CreateSingleRowTableOp());
				return this.BuildFilterNode(inputNode, node3);
			}
			else
			{
				if (node == null)
				{
					return this.BuildFilterNode(node2, node3);
				}
				if (node2 == null)
				{
					return this.BuildFilterNode(node, node3);
				}
				if (node3 == null)
				{
					node3 = this.m_command.CreateNode(this.m_command.CreateTrueOp());
				}
				Node node4 = this.m_command.CreateNode(joinNode.Node.Op, node, node2, node3);
				this.m_processedNodes[node4] = node4;
				return node4;
			}
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x00021AE0 File Offset: 0x0001FCE0
		private Node RebuildNodeTree(AugmentedTableNode tableNode)
		{
			AugmentedTableNode augmentedTableNode = tableNode;
			if (tableNode.IsMoved)
			{
				return null;
			}
			while (augmentedTableNode.IsEliminated)
			{
				augmentedTableNode = augmentedTableNode.ReplacementTable;
				if (augmentedTableNode == null)
				{
					return null;
				}
			}
			if (augmentedTableNode.NewLocationId < tableNode.Id)
			{
				return null;
			}
			return this.BuildFilterForNullableColumns(augmentedTableNode.Node, augmentedTableNode.NullableColumns);
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x00021B34 File Offset: 0x0001FD34
		private Node RebuildNodeTree(AugmentedNode augmentedNode, out Dictionary<Node, int> predicates)
		{
			OpType opType = augmentedNode.Node.Op.OpType;
			if (opType == OpType.ScanTable)
			{
				predicates = null;
				return this.RebuildNodeTree((AugmentedTableNode)augmentedNode);
			}
			if (opType - OpType.InnerJoin > 3)
			{
				predicates = null;
				return augmentedNode.Node;
			}
			return this.RebuildNodeTree((AugmentedJoinNode)augmentedNode, out predicates);
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x00021B88 File Offset: 0x0001FD88
		private Node CombinePredicateNodes(int targetNodeId, Node localPredicateNode, int localPredicateMinLocationId, Dictionary<Node, int> leftPredicates, Dictionary<Node, int> rightPredicates, out Dictionary<Node, int> outPredicates)
		{
			Node result = null;
			outPredicates = new Dictionary<Node, int>();
			if (localPredicateNode != null)
			{
				result = this.ClassifyPredicate(targetNodeId, localPredicateNode, localPredicateMinLocationId, result, outPredicates);
			}
			if (leftPredicates != null)
			{
				foreach (KeyValuePair<Node, int> keyValuePair in leftPredicates)
				{
					result = this.ClassifyPredicate(targetNodeId, keyValuePair.Key, keyValuePair.Value, result, outPredicates);
				}
			}
			if (rightPredicates != null)
			{
				foreach (KeyValuePair<Node, int> keyValuePair2 in rightPredicates)
				{
					result = this.ClassifyPredicate(targetNodeId, keyValuePair2.Key, keyValuePair2.Value, result, outPredicates);
				}
			}
			return result;
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x00021C60 File Offset: 0x0001FE60
		private Node ClassifyPredicate(int targetNodeId, Node predicateNode, int predicateMinLocationId, Node result, Dictionary<Node, int> outPredicates)
		{
			if (targetNodeId >= predicateMinLocationId)
			{
				result = this.CombinePredicates(result, predicateNode);
			}
			else
			{
				outPredicates.Add(predicateNode, predicateMinLocationId);
			}
			return result;
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x00021C7E File Offset: 0x0001FE7E
		private Node CombinePredicates(Node node1, Node node2)
		{
			if (node1 == null)
			{
				return node2;
			}
			if (node2 == null)
			{
				return node1;
			}
			return PlanCompilerUtil.CombinePredicates(node1, node2, this.m_command);
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x00021C98 File Offset: 0x0001FE98
		private int GetLocationId(Var var, int defaultLocationId)
		{
			AugmentedTableNode augmentedTableNode;
			if (!this.m_varToDefiningNodeMap.TryGetValue(var, out augmentedTableNode))
			{
				return defaultLocationId;
			}
			if (augmentedTableNode.IsMoved)
			{
				return augmentedTableNode.NewLocationId;
			}
			return augmentedTableNode.Id;
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x00021CCC File Offset: 0x0001FECC
		private int GetLeastCommonAncestor(int nodeId1, int nodeId2)
		{
			if (nodeId1 == nodeId2)
			{
				return nodeId1;
			}
			AugmentedNode augmentedNode = this.m_root;
			AugmentedNode augmentedNode2 = augmentedNode;
			AugmentedNode augmentedNode3 = augmentedNode;
			while (augmentedNode2 == augmentedNode3)
			{
				augmentedNode = augmentedNode2;
				if (augmentedNode.Id == nodeId1 || augmentedNode.Id == nodeId2)
				{
					return augmentedNode.Id;
				}
				augmentedNode2 = JoinGraph.PickSubtree(nodeId1, augmentedNode);
				augmentedNode3 = JoinGraph.PickSubtree(nodeId2, augmentedNode);
			}
			return augmentedNode.Id;
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x00021D24 File Offset: 0x0001FF24
		private static AugmentedNode PickSubtree(int nodeId, AugmentedNode root)
		{
			AugmentedNode augmentedNode = root.Children[0];
			int num = 1;
			while (augmentedNode.Id < nodeId && num < root.Children.Count)
			{
				augmentedNode = root.Children[num];
				num++;
			}
			return augmentedNode;
		}

		// Token: 0x040007AE RID: 1966
		private Command m_command;

		// Token: 0x040007AF RID: 1967
		private AugmentedJoinNode m_root;

		// Token: 0x040007B0 RID: 1968
		private List<AugmentedNode> m_vertexes;

		// Token: 0x040007B1 RID: 1969
		private List<AugmentedTableNode> m_tableVertexes;

		// Token: 0x040007B2 RID: 1970
		private Dictionary<Table, AugmentedTableNode> m_tableVertexMap;

		// Token: 0x040007B3 RID: 1971
		private VarMap m_varMap;

		// Token: 0x040007B4 RID: 1972
		private Dictionary<Var, VarVec> m_reverseVarMap;

		// Token: 0x040007B5 RID: 1973
		private Dictionary<Var, AugmentedTableNode> m_varToDefiningNodeMap;

		// Token: 0x040007B6 RID: 1974
		private Dictionary<Node, Node> m_processedNodes;

		// Token: 0x040007B7 RID: 1975
		private bool m_modifiedGraph;

		// Token: 0x040007B8 RID: 1976
		private ConstraintManager m_constraintManager;

		// Token: 0x040007B9 RID: 1977
		private VarRefManager m_varRefManager;

		// Token: 0x040007BA RID: 1978
		private bool m_isSqlCe;
	}
}
