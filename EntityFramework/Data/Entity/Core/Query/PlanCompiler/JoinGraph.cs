using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Query.InternalTrees;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x0200067F RID: 1663
	internal class JoinGraph
	{
		// Token: 0x0600410E RID: 16654 RVA: 0x0012C250 File Offset: 0x0012A450
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		internal JoinGraph(Command command, ConstraintManager constraintManager, VarRefManager varRefManager, Node joinNode)
		{
			this.m_command = command;
			this.m_constraintManager = constraintManager;
			this.m_varRefManager = varRefManager;
			this.m_vertexes = new List<AugmentedNode>();
			this.m_tableVertexMap = new Dictionary<Table, AugmentedTableNode>();
			this.m_varMap = new VarMap();
			this.m_reverseVarMap = new Dictionary<Var, VarVec>();
			this.m_varToDefiningNodeMap = new Dictionary<Var, AugmentedTableNode>();
			this.m_processedNodes = new Dictionary<Node, Node>();
			this.m_root = (this.BuildAugmentedNodeTree(joinNode) as AugmentedJoinNode);
			PlanCompiler.Assert(this.m_root != null, "The root isn't a join?");
			this.BuildJoinEdges(this.m_root, this.m_root.Id);
		}

		// Token: 0x0600410F RID: 16655 RVA: 0x0012C2FC File Offset: 0x0012A4FC
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

		// Token: 0x06004110 RID: 16656 RVA: 0x0012C33C File Offset: 0x0012A53C
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

		// Token: 0x06004111 RID: 16657 RVA: 0x0012C39C File Offset: 0x0012A59C
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "columnVar")]
		private static void GetColumnVars(List<ColumnVar> columnVars, IEnumerable<Var> vec)
		{
			foreach (Var var in vec)
			{
				PlanCompiler.Assert(var.VarType == VarType.Column, "Expected a columnVar. Found " + var.VarType);
				columnVars.Add((ColumnVar)var);
			}
		}

		// Token: 0x06004112 RID: 16658 RVA: 0x0012C40C File Offset: 0x0012A60C
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

		// Token: 0x06004113 RID: 16659 RVA: 0x0012C4BC File Offset: 0x0012A6BC
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

		// Token: 0x06004114 RID: 16660 RVA: 0x0012C650 File Offset: 0x0012A850
		private bool AddJoinEdge(AugmentedJoinNode joinNode, ColumnVar leftVar, ColumnVar rightVar)
		{
			AugmentedTableNode augmentedTableNode;
			if (!this.m_tableVertexMap.TryGetValue(leftVar.Table, out augmentedTableNode))
			{
				return false;
			}
			AugmentedTableNode right;
			if (!this.m_tableVertexMap.TryGetValue(rightVar.Table, out right))
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
			JoinEdge item = JoinEdge.CreateJoinEdge(augmentedTableNode, right, joinNode, leftVar, rightVar);
			augmentedTableNode.JoinEdges.Add(item);
			joinNode.JoinEdges.Add(item);
			return true;
		}

		// Token: 0x06004115 RID: 16661 RVA: 0x0012C718 File Offset: 0x0012A918
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

		// Token: 0x06004116 RID: 16662 RVA: 0x0012C778 File Offset: 0x0012A978
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
			if (joinNode.Node.Op.OpType == OpType.FullOuterJoin || joinNode.LeftVars.Count == 0)
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

		// Token: 0x06004117 RID: 16663 RVA: 0x0012C8E4 File Offset: 0x0012AAE4
		private void BuildJoinEdges(AugmentedNode node, int maxVisibility)
		{
			switch (node.Node.Op.OpType)
			{
			case OpType.ScanTable:
			{
				AugmentedTableNode augmentedTableNode = (AugmentedTableNode)node;
				augmentedTableNode.LastVisibleId = maxVisibility;
				break;
			}
			case OpType.ScanView:
			case OpType.Filter:
			case OpType.Project:
				break;
			case OpType.InnerJoin:
			case OpType.LeftOuterJoin:
			case OpType.FullOuterJoin:
			case OpType.CrossJoin:
				this.BuildJoinEdges(node as AugmentedJoinNode, maxVisibility);
				return;
			default:
				return;
			}
		}

		// Token: 0x06004118 RID: 16664 RVA: 0x0012C948 File Offset: 0x0012AB48
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		private static bool GenerateTransitiveEdge(JoinEdge edge1, JoinEdge edge2)
		{
			PlanCompiler.Assert(edge1.Right == edge2.Left, "need a common table for transitive predicate generation");
			if (edge1.RestrictedElimination || edge2.RestrictedElimination)
			{
				return false;
			}
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
				KeyValuePair<ColumnVar, ColumnVar> keyValuePair2 = enumerator3.Current;
				if (key == keyValuePair2.Key)
				{
					List<ColumnVar> list3 = list;
					KeyValuePair<ColumnVar, ColumnVar> keyValuePair3 = enumerator2.Current;
					list3.Add(keyValuePair3.Value);
					List<ColumnVar> list4 = list2;
					KeyValuePair<ColumnVar, ColumnVar> keyValuePair4 = enumerator3.Current;
					list4.Add(keyValuePair4.Value);
					flag = (enumerator2.MoveNext() && enumerator3.MoveNext());
				}
				else
				{
					if (edge1.JoinKind == JoinKind.LeftOuter)
					{
						return false;
					}
					KeyValuePair<ColumnVar, ColumnVar> keyValuePair5 = enumerator2.Current;
					int id = keyValuePair5.Key.Id;
					KeyValuePair<ColumnVar, ColumnVar> keyValuePair6 = enumerator3.Current;
					if (id > keyValuePair6.Key.Id)
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

		// Token: 0x06004119 RID: 16665 RVA: 0x0012CBCC File Offset: 0x0012ADCC
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

		// Token: 0x0600411A RID: 16666 RVA: 0x0012CC58 File Offset: 0x0012AE58
		private void TryTurnLeftOuterJoinsIntoInnerJoins()
		{
			foreach (AugmentedJoinNode augmentedJoinNode in from j in this.m_vertexes.OfType<AugmentedJoinNode>()
			where j.Node.Op.OpType == OpType.LeftOuterJoin && j.JoinEdges.Count > 0
			select j)
			{
				if (this.CanAllJoinEdgesBeTurnedIntoInnerJoins(augmentedJoinNode.Children[1], augmentedJoinNode.JoinEdges))
				{
					augmentedJoinNode.Node.Op = this.m_command.CreateInnerJoinOp();
					this.m_modifiedGraph = true;
					List<JoinEdge> list = new List<JoinEdge>(augmentedJoinNode.JoinEdges.Count);
					foreach (JoinEdge joinEdge in augmentedJoinNode.JoinEdges)
					{
						joinEdge.JoinKind = JoinKind.Inner;
						if (!JoinGraph.ContainsJoinEdgeForTable(joinEdge.Right.JoinEdges, joinEdge.Left.Table))
						{
							JoinEdge joinEdge2 = JoinEdge.CreateJoinEdge(joinEdge.Right, joinEdge.Left, augmentedJoinNode, joinEdge.RightVars[0], joinEdge.LeftVars[0]);
							joinEdge.Right.JoinEdges.Add(joinEdge2);
							list.Add(joinEdge2);
							for (int i = 1; i < joinEdge.LeftVars.Count; i++)
							{
								joinEdge2.AddCondition(augmentedJoinNode, joinEdge.RightVars[i], joinEdge.LeftVars[i]);
							}
						}
					}
					augmentedJoinNode.JoinEdges.AddRange(list);
				}
			}
		}

		// Token: 0x0600411B RID: 16667 RVA: 0x0012CE24 File Offset: 0x0012B024
		private static bool AreAllTableRowsPreserved(AugmentedNode root, AugmentedTableNode table)
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

		// Token: 0x0600411C RID: 16668 RVA: 0x0012CE78 File Offset: 0x0012B078
		private static bool ContainsJoinEdgeForTable(IEnumerable<JoinEdge> joinEdges, Table table)
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

		// Token: 0x0600411D RID: 16669 RVA: 0x0012CED4 File Offset: 0x0012B0D4
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

		// Token: 0x0600411E RID: 16670 RVA: 0x0012CF28 File Offset: 0x0012B128
		private bool CanJoinEdgeBeTurnedIntoInnerJoin(AugmentedNode rightNode, JoinEdge joinEdge)
		{
			return !joinEdge.RestrictedElimination && JoinGraph.AreAllTableRowsPreserved(rightNode, joinEdge.Right) && this.IsConstraintPresentForTurningIntoInnerJoin(joinEdge);
		}

		// Token: 0x0600411F RID: 16671 RVA: 0x0012CF58 File Offset: 0x0012B158
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		private bool IsConstraintPresentForTurningIntoInnerJoin(JoinEdge joinEdge)
		{
			List<ForeignKeyConstraint> list;
			if (this.m_constraintManager.IsParentChildRelationship(joinEdge.Right.Table.TableMetadata.Extent, joinEdge.Left.Table.TableMetadata.Extent, out list))
			{
				PlanCompiler.Assert(list != null && list.Count > 0, "Invalid foreign key constraints");
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

		// Token: 0x06004120 RID: 16672 RVA: 0x0012D054 File Offset: 0x0012B254
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
							JoinGraph.GenerateTransitiveEdge(joinEdge, edge);
							j++;
						}
					}
				}
			}
		}

		// Token: 0x06004121 RID: 16673 RVA: 0x0012D10C File Offset: 0x0012B30C
		private static bool CanBeEliminatedBasedOnLojParticipation(AugmentedTableNode table, AugmentedTableNode replacingTable)
		{
			if (replacingTable.Id < table.NewLocationId)
			{
				return JoinGraph.CanBeMovedBasedOnLojParticipation(table, replacingTable);
			}
			return JoinGraph.CanBeMovedBasedOnLojParticipation(replacingTable, table);
		}

		// Token: 0x06004122 RID: 16674 RVA: 0x0012D12C File Offset: 0x0012B32C
		private static bool CanBeEliminatedViaStarJoinBasedOnOtherJoinParticipation(JoinEdge tableJoinEdge, JoinEdge replacingTableJoinEdge)
		{
			if (tableJoinEdge.JoinNode == null || replacingTableJoinEdge.JoinNode == null)
			{
				return false;
			}
			AugmentedNode leastCommonAncestor = JoinGraph.GetLeastCommonAncestor(tableJoinEdge.Right, replacingTableJoinEdge.Right);
			return !JoinGraph.CanGetFileredByJoins(tableJoinEdge, leastCommonAncestor, true) && !JoinGraph.CanGetFileredByJoins(replacingTableJoinEdge, leastCommonAncestor, false);
		}

		// Token: 0x06004123 RID: 16675 RVA: 0x0012D174 File Offset: 0x0012B374
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

		// Token: 0x06004124 RID: 16676 RVA: 0x0012D1E4 File Offset: 0x0012B3E4
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

		// Token: 0x06004125 RID: 16677 RVA: 0x0012D240 File Offset: 0x0012B440
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

		// Token: 0x06004126 RID: 16678 RVA: 0x0012D290 File Offset: 0x0012B490
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "vars")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
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

		// Token: 0x06004127 RID: 16679 RVA: 0x0012D3F4 File Offset: 0x0012B5F4
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

		// Token: 0x06004128 RID: 16680 RVA: 0x0012D461 File Offset: 0x0012B661
		private void EliminateSelfJoinedTable(AugmentedTableNode tableNode, AugmentedTableNode replacementNode)
		{
			this.MarkTableAsEliminated<Var>(tableNode, replacementNode, tableNode.Table.Columns, replacementNode.Table.Columns);
		}

		// Token: 0x06004129 RID: 16681 RVA: 0x0012D48C File Offset: 0x0012B68C
		private void EliminateStarSelfJoin(List<JoinEdge> joinEdges)
		{
			List<List<JoinEdge>> list = new List<List<JoinEdge>>();
			foreach (JoinEdge joinEdge in joinEdges)
			{
				bool flag = false;
				foreach (List<JoinEdge> list2 in list)
				{
					if (JoinGraph.AreMatchingForStarSelfJoinElimination(list2[0], joinEdge))
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

		// Token: 0x0600412A RID: 16682 RVA: 0x0012D670 File Offset: 0x0012B870
		private static bool AreMatchingForStarSelfJoinElimination(JoinEdge edge1, JoinEdge edge2)
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
			return JoinGraph.MatchOtherPredicates(edge1, edge2);
		}

		// Token: 0x0600412B RID: 16683 RVA: 0x0012D718 File Offset: 0x0012B918
		private static bool MatchOtherPredicates(JoinEdge edge1, JoinEdge edge2)
		{
			if (edge1.JoinNode == null)
			{
				return edge2.JoinNode == null;
			}
			if (edge2.JoinNode == null)
			{
				return false;
			}
			if (edge1.JoinNode.OtherPredicate == null)
			{
				return edge2.JoinNode.OtherPredicate == null;
			}
			return edge2.JoinNode.OtherPredicate != null && JoinGraph.MatchOtherPredicates(edge1.JoinNode.OtherPredicate, edge2.JoinNode.OtherPredicate);
		}

		// Token: 0x0600412C RID: 16684 RVA: 0x0012D7AC File Offset: 0x0012B9AC
		private static bool MatchOtherPredicates(Node x, Node y)
		{
			if (x.Children.Count != y.Children.Count)
			{
				return false;
			}
			if (x.Op.IsEquivalent(y.Op))
			{
				return !x.Children.Where((Node t, int i) => !JoinGraph.MatchOtherPredicates(t, y.Children[i])).Any<Node>();
			}
			VarRefOp varRefOp = x.Op as VarRefOp;
			if (varRefOp == null)
			{
				return false;
			}
			VarRefOp varRefOp2 = y.Op as VarRefOp;
			if (varRefOp2 == null)
			{
				return false;
			}
			ColumnVar columnVar = varRefOp.Var as ColumnVar;
			if (columnVar == null)
			{
				return false;
			}
			ColumnVar columnVar2 = varRefOp2.Var as ColumnVar;
			return columnVar2 != null && columnVar.ColumnMetadata.Name.Equals(columnVar2.ColumnMetadata.Name);
		}

		// Token: 0x0600412D RID: 16685 RVA: 0x0012D894 File Offset: 0x0012BA94
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
			return varVec.IsEmpty && (joinEdge.JoinNode == null || joinEdge.JoinNode.OtherPredicate == null || JoinGraph.QualifiesForStarSelfJoinGroup(joinEdge.JoinNode.OtherPredicate, this.m_command.GetExtendedNodeInfo(joinEdge.Right.Node).Definitions));
		}

		// Token: 0x0600412E RID: 16686 RVA: 0x0012D980 File Offset: 0x0012BB80
		private static bool QualifiesForStarSelfJoinGroup(Node otherPredicateNode, VarVec rightTableColumnVars)
		{
			VarRefOp varRefOp = otherPredicateNode.Op as VarRefOp;
			if (varRefOp == null)
			{
				return true;
			}
			ColumnVar columnVar = varRefOp.Var as ColumnVar;
			return columnVar == null || (rightTableColumnVars.IsSet(columnVar) && otherPredicateNode.Children.All((Node node) => JoinGraph.QualifiesForStarSelfJoinGroup(node, rightTableColumnVars)));
		}

		// Token: 0x0600412F RID: 16687 RVA: 0x0012D9E4 File Offset: 0x0012BBE4
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

		// Token: 0x06004130 RID: 16688 RVA: 0x0012DADC File Offset: 0x0012BCDC
		private bool EliminateSelfJoin(JoinEdge joinEdge)
		{
			if (joinEdge.RestrictedElimination)
			{
				return false;
			}
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

		// Token: 0x06004131 RID: 16689 RVA: 0x0012DC28 File Offset: 0x0012BE28
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

		// Token: 0x06004132 RID: 16690 RVA: 0x0012DC88 File Offset: 0x0012BE88
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

		// Token: 0x06004133 RID: 16691 RVA: 0x0012DCEC File Offset: 0x0012BEEC
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
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

		// Token: 0x06004134 RID: 16692 RVA: 0x0012DDAC File Offset: 0x0012BFAC
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		private void EliminateRightTable(JoinEdge joinEdge)
		{
			PlanCompiler.Assert(joinEdge.JoinKind == JoinKind.LeftOuter, "Expected left-outer-join");
			PlanCompiler.Assert(joinEdge.Left.Id < joinEdge.Right.Id, string.Concat(new object[]
			{
				"(left-id, right-id) = (",
				joinEdge.Left.Id,
				",",
				joinEdge.Right.Id,
				")"
			}));
			this.MarkTableAsEliminated<ColumnVar>(joinEdge.Right, joinEdge.Left, joinEdge.RightVars, joinEdge.LeftVars);
		}

		// Token: 0x06004135 RID: 16693 RVA: 0x0012DE52 File Offset: 0x0012C052
		private static bool HasNonKeyReferences(Table table)
		{
			return !table.Keys.Subsumes(table.ReferencedColumns);
		}

		// Token: 0x06004136 RID: 16694 RVA: 0x0012DE68 File Offset: 0x0012C068
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

		// Token: 0x06004137 RID: 16695 RVA: 0x0012DF2C File Offset: 0x0012C12C
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

		// Token: 0x06004138 RID: 16696 RVA: 0x0012DFBC File Offset: 0x0012C1BC
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

		// Token: 0x06004139 RID: 16697 RVA: 0x0012E150 File Offset: 0x0012C350
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

		// Token: 0x0600413A RID: 16698 RVA: 0x0012E1C0 File Offset: 0x0012C3C0
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

		// Token: 0x0600413B RID: 16699 RVA: 0x0012E21C File Offset: 0x0012C41C
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		private void EliminateParentChildJoin(JoinEdge joinEdge)
		{
			if (joinEdge.RestrictedElimination)
			{
				return;
			}
			List<ForeignKeyConstraint> list;
			if (this.m_constraintManager.IsParentChildRelationship(joinEdge.Left.Table.TableMetadata.Extent, joinEdge.Right.Table.TableMetadata.Extent, out list))
			{
				PlanCompiler.Assert(list != null && list.Count > 0, "Invalid foreign key constraints");
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
				PlanCompiler.Assert(list != null && list.Count > 0, "Invalid foreign key constraints");
				foreach (ForeignKeyConstraint fkConstraint2 in list)
				{
					if (this.TryEliminateChildParentJoin(joinEdge, fkConstraint2))
					{
						break;
					}
				}
			}
		}

		// Token: 0x0600413C RID: 16700 RVA: 0x0012E370 File Offset: 0x0012C570
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

		// Token: 0x0600413D RID: 16701 RVA: 0x0012E3D0 File Offset: 0x0012C5D0
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

		// Token: 0x0600413E RID: 16702 RVA: 0x0012E438 File Offset: 0x0012C638
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
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

		// Token: 0x0600413F RID: 16703 RVA: 0x0012E520 File Offset: 0x0012C720
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
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

		// Token: 0x06004140 RID: 16704 RVA: 0x0012E628 File Offset: 0x0012C828
		private Node BuildFilterNode(Node inputNode, Node predicateNode)
		{
			if (predicateNode == null)
			{
				return inputNode;
			}
			return this.m_command.CreateNode(this.m_command.CreateFilterOp(), inputNode, predicateNode);
		}

		// Token: 0x06004141 RID: 16705 RVA: 0x0012E648 File Offset: 0x0012C848
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
					Node node2 = this.m_command.CreateNode(this.m_command.CreateComparisonOp(OpType.EQ, false), arg, arg2);
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

		// Token: 0x06004142 RID: 16706 RVA: 0x0012E7F0 File Offset: 0x0012C9F0
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
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

		// Token: 0x06004143 RID: 16707 RVA: 0x0012E8AC File Offset: 0x0012CAAC
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

		// Token: 0x06004144 RID: 16708 RVA: 0x0012E9DC File Offset: 0x0012CBDC
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

		// Token: 0x06004145 RID: 16709 RVA: 0x0012EA30 File Offset: 0x0012CC30
		private Node RebuildNodeTree(AugmentedNode augmentedNode, out Dictionary<Node, int> predicates)
		{
			switch (augmentedNode.Node.Op.OpType)
			{
			case OpType.ScanTable:
				predicates = null;
				return this.RebuildNodeTree((AugmentedTableNode)augmentedNode);
			case OpType.InnerJoin:
			case OpType.LeftOuterJoin:
			case OpType.FullOuterJoin:
			case OpType.CrossJoin:
				return this.RebuildNodeTree((AugmentedJoinNode)augmentedNode, out predicates);
			}
			predicates = null;
			return augmentedNode.Node;
		}

		// Token: 0x06004146 RID: 16710 RVA: 0x0012EAA0 File Offset: 0x0012CCA0
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

		// Token: 0x06004147 RID: 16711 RVA: 0x0012EB78 File Offset: 0x0012CD78
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

		// Token: 0x06004148 RID: 16712 RVA: 0x0012EB96 File Offset: 0x0012CD96
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

		// Token: 0x06004149 RID: 16713 RVA: 0x0012EBB0 File Offset: 0x0012CDB0
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

		// Token: 0x0600414A RID: 16714 RVA: 0x0012EBE4 File Offset: 0x0012CDE4
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

		// Token: 0x0600414B RID: 16715 RVA: 0x0012EC3C File Offset: 0x0012CE3C
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

		// Token: 0x04001834 RID: 6196
		private readonly Command m_command;

		// Token: 0x04001835 RID: 6197
		private readonly AugmentedJoinNode m_root;

		// Token: 0x04001836 RID: 6198
		private readonly List<AugmentedNode> m_vertexes;

		// Token: 0x04001837 RID: 6199
		private readonly Dictionary<Table, AugmentedTableNode> m_tableVertexMap;

		// Token: 0x04001838 RID: 6200
		private VarMap m_varMap;

		// Token: 0x04001839 RID: 6201
		private readonly Dictionary<Var, VarVec> m_reverseVarMap;

		// Token: 0x0400183A RID: 6202
		private readonly Dictionary<Var, AugmentedTableNode> m_varToDefiningNodeMap;

		// Token: 0x0400183B RID: 6203
		private readonly Dictionary<Node, Node> m_processedNodes;

		// Token: 0x0400183C RID: 6204
		private bool m_modifiedGraph;

		// Token: 0x0400183D RID: 6205
		private readonly ConstraintManager m_constraintManager;

		// Token: 0x0400183E RID: 6206
		private readonly VarRefManager m_varRefManager;
	}
}
