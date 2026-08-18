using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.Structures;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration
{
	// Token: 0x02000420 RID: 1056
	internal class CellTreeSimplifier : InternalBase
	{
		// Token: 0x060026E2 RID: 9954 RVA: 0x000BCF32 File Offset: 0x000BB132
		private CellTreeSimplifier(ViewgenContext context)
		{
			this.m_viewgenContext = context;
		}

		// Token: 0x060026E3 RID: 9955 RVA: 0x000BCF44 File Offset: 0x000BB144
		internal static CellTreeNode MergeNodes(CellTreeNode rootNode)
		{
			CellTreeSimplifier cellTreeSimplifier = new CellTreeSimplifier(rootNode.ViewgenContext);
			return cellTreeSimplifier.SimplifyTreeByMergingNodes(rootNode);
		}

		// Token: 0x060026E4 RID: 9956 RVA: 0x000BCF64 File Offset: 0x000BB164
		private CellTreeNode SimplifyTreeByMergingNodes(CellTreeNode rootNode)
		{
			if (rootNode is LeafCellTreeNode)
			{
				return rootNode;
			}
			rootNode = this.RestructureTreeForMerges(rootNode);
			List<CellTreeNode> list = rootNode.Children;
			for (int i = 0; i < list.Count; i++)
			{
				list[i] = this.SimplifyTreeByMergingNodes(list[i]);
			}
			bool flag = CellTreeNode.IsAssociativeOp(rootNode.OpType);
			if (flag)
			{
				list = CellTreeSimplifier.GroupLeafChildrenByExtent(list);
			}
			else
			{
				list = CellTreeSimplifier.GroupNonAssociativeLeafChildren(list);
			}
			OpCellTreeNode opCellTreeNode = new OpCellTreeNode(this.m_viewgenContext, rootNode.OpType);
			CellTreeNode cellTreeNode = null;
			bool flag2 = false;
			foreach (CellTreeNode cellTreeNode2 in list)
			{
				if (cellTreeNode == null)
				{
					cellTreeNode = cellTreeNode2;
				}
				else
				{
					bool flag3 = false;
					if (!flag2 && cellTreeNode.OpType == CellTreeOpType.Leaf && cellTreeNode2.OpType == CellTreeOpType.Leaf)
					{
						flag3 = this.TryMergeCellQueries(rootNode.OpType, ref cellTreeNode, cellTreeNode2);
					}
					if (!flag3)
					{
						opCellTreeNode.Add(cellTreeNode);
						cellTreeNode = cellTreeNode2;
						if (!flag)
						{
							flag2 = true;
						}
					}
				}
			}
			opCellTreeNode.Add(cellTreeNode);
			return opCellTreeNode.AssociativeFlatten();
		}

		// Token: 0x060026E5 RID: 9957 RVA: 0x000BD080 File Offset: 0x000BB280
		private CellTreeNode RestructureTreeForMerges(CellTreeNode rootNode)
		{
			List<CellTreeNode> children = rootNode.Children;
			if (!CellTreeNode.IsAssociativeOp(rootNode.OpType) || children.Count <= 1)
			{
				return rootNode;
			}
			Set<LeafCellTreeNode> commonGrandChildren = CellTreeSimplifier.GetCommonGrandChildren(children);
			if (commonGrandChildren == null)
			{
				return rootNode;
			}
			CellTreeOpType opType = children[0].OpType;
			List<OpCellTreeNode> list = new List<OpCellTreeNode>(children.Count);
			foreach (CellTreeNode cellTreeNode in children)
			{
				OpCellTreeNode opCellTreeNode = (OpCellTreeNode)cellTreeNode;
				List<LeafCellTreeNode> list2 = new List<LeafCellTreeNode>(opCellTreeNode.Children.Count);
				foreach (CellTreeNode cellTreeNode2 in opCellTreeNode.Children)
				{
					LeafCellTreeNode leafCellTreeNode = (LeafCellTreeNode)cellTreeNode2;
					if (!commonGrandChildren.Contains(leafCellTreeNode))
					{
						list2.Add(leafCellTreeNode);
					}
				}
				OpCellTreeNode item = new OpCellTreeNode(this.m_viewgenContext, opCellTreeNode.OpType, Helpers.AsSuperTypeList<LeafCellTreeNode, CellTreeNode>(list2));
				list.Add(item);
			}
			CellTreeNode cellTreeNode3 = new OpCellTreeNode(this.m_viewgenContext, rootNode.OpType, Helpers.AsSuperTypeList<OpCellTreeNode, CellTreeNode>(list));
			CellTreeNode cellTreeNode4 = new OpCellTreeNode(this.m_viewgenContext, opType, Helpers.AsSuperTypeList<LeafCellTreeNode, CellTreeNode>(commonGrandChildren));
			CellTreeNode cellTreeNode5 = new OpCellTreeNode(this.m_viewgenContext, opType, new CellTreeNode[]
			{
				cellTreeNode4,
				cellTreeNode3
			});
			return cellTreeNode5.AssociativeFlatten();
		}

		// Token: 0x060026E6 RID: 9958 RVA: 0x000BD200 File Offset: 0x000BB400
		private static Set<LeafCellTreeNode> GetCommonGrandChildren(List<CellTreeNode> nodes)
		{
			Set<LeafCellTreeNode> set = null;
			CellTreeOpType cellTreeOpType = CellTreeOpType.Leaf;
			foreach (CellTreeNode cellTreeNode in nodes)
			{
				OpCellTreeNode opCellTreeNode = cellTreeNode as OpCellTreeNode;
				if (opCellTreeNode == null)
				{
					return null;
				}
				if (cellTreeOpType == CellTreeOpType.Leaf)
				{
					cellTreeOpType = opCellTreeNode.OpType;
				}
				else if (!CellTreeNode.IsAssociativeOp(opCellTreeNode.OpType) || cellTreeOpType != opCellTreeNode.OpType)
				{
					return null;
				}
				Set<LeafCellTreeNode> set2 = new Set<LeafCellTreeNode>(LeafCellTreeNode.EqualityComparer);
				foreach (CellTreeNode cellTreeNode2 in opCellTreeNode.Children)
				{
					LeafCellTreeNode leafCellTreeNode = cellTreeNode2 as LeafCellTreeNode;
					if (leafCellTreeNode == null)
					{
						return null;
					}
					set2.Add(leafCellTreeNode);
				}
				if (set == null)
				{
					set = set2;
				}
				else
				{
					set.Intersect(set2);
				}
			}
			if (set.Count == 0)
			{
				return null;
			}
			return set;
		}

		// Token: 0x060026E7 RID: 9959 RVA: 0x000BD310 File Offset: 0x000BB510
		private static List<CellTreeNode> GroupLeafChildrenByExtent(List<CellTreeNode> nodes)
		{
			KeyToListMap<EntitySetBase, CellTreeNode> keyToListMap = new KeyToListMap<EntitySetBase, CellTreeNode>(EqualityComparer<EntitySetBase>.Default);
			List<CellTreeNode> list = new List<CellTreeNode>();
			foreach (CellTreeNode cellTreeNode in nodes)
			{
				LeafCellTreeNode leafCellTreeNode = cellTreeNode as LeafCellTreeNode;
				if (leafCellTreeNode != null)
				{
					keyToListMap.Add(leafCellTreeNode.LeftCellWrapper.RightCellQuery.Extent, leafCellTreeNode);
				}
				else
				{
					list.Add(cellTreeNode);
				}
			}
			list.AddRange(keyToListMap.AllValues);
			return list;
		}

		// Token: 0x060026E8 RID: 9960 RVA: 0x000BD3A0 File Offset: 0x000BB5A0
		private static List<CellTreeNode> GroupNonAssociativeLeafChildren(List<CellTreeNode> nodes)
		{
			KeyToListMap<EntitySetBase, CellTreeNode> keyToListMap = new KeyToListMap<EntitySetBase, CellTreeNode>(EqualityComparer<EntitySetBase>.Default);
			List<CellTreeNode> list = new List<CellTreeNode>();
			List<CellTreeNode> list2 = new List<CellTreeNode>();
			list.Add(nodes[0]);
			for (int i = 1; i < nodes.Count; i++)
			{
				CellTreeNode cellTreeNode = nodes[i];
				LeafCellTreeNode leafCellTreeNode = cellTreeNode as LeafCellTreeNode;
				if (leafCellTreeNode != null)
				{
					keyToListMap.Add(leafCellTreeNode.LeftCellWrapper.RightCellQuery.Extent, leafCellTreeNode);
				}
				else
				{
					list2.Add(cellTreeNode);
				}
			}
			LeafCellTreeNode leafCellTreeNode2 = nodes[0] as LeafCellTreeNode;
			if (leafCellTreeNode2 != null)
			{
				EntitySetBase extent = leafCellTreeNode2.LeftCellWrapper.RightCellQuery.Extent;
				if (keyToListMap.ContainsKey(extent))
				{
					list.AddRange(keyToListMap.ListForKey(extent));
					keyToListMap.RemoveKey(extent);
				}
			}
			list.AddRange(keyToListMap.AllValues);
			list.AddRange(list2);
			return list;
		}

		// Token: 0x060026E9 RID: 9961 RVA: 0x000BD478 File Offset: 0x000BB678
		private bool TryMergeCellQueries(CellTreeOpType opType, ref CellTreeNode node1, CellTreeNode node2)
		{
			LeafCellTreeNode leafCellTreeNode = node1 as LeafCellTreeNode;
			LeafCellTreeNode leafCellTreeNode2 = node2 as LeafCellTreeNode;
			CellQuery rightCellQuery;
			if (!CellTreeSimplifier.TryMergeTwoCellQueries(leafCellTreeNode.LeftCellWrapper.RightCellQuery, leafCellTreeNode2.LeftCellWrapper.RightCellQuery, opType, out rightCellQuery))
			{
				return false;
			}
			CellQuery leftCellQuery;
			if (!CellTreeSimplifier.TryMergeTwoCellQueries(leafCellTreeNode.LeftCellWrapper.LeftCellQuery, leafCellTreeNode2.LeftCellWrapper.LeftCellQuery, opType, out leftCellQuery))
			{
				return false;
			}
			OpCellTreeNode opCellTreeNode = new OpCellTreeNode(this.m_viewgenContext, opType);
			opCellTreeNode.Add(node1);
			opCellTreeNode.Add(node2);
			if (opType != CellTreeOpType.FOJ)
			{
			}
			LeftCellWrapper cellWrapper = new LeftCellWrapper(this.m_viewgenContext.ViewTarget, opCellTreeNode.Attributes, opCellTreeNode.LeftFragmentQuery, leftCellQuery, rightCellQuery, this.m_viewgenContext.MemberMaps, leafCellTreeNode.LeftCellWrapper.Cells.Concat(leafCellTreeNode2.LeftCellWrapper.Cells));
			node1 = new LeafCellTreeNode(this.m_viewgenContext, cellWrapper, opCellTreeNode.RightFragmentQuery);
			return true;
		}

		// Token: 0x060026EA RID: 9962 RVA: 0x000BD560 File Offset: 0x000BB760
		internal static bool TryMergeTwoCellQueries(CellQuery query1, CellQuery query2, CellTreeOpType opType, out CellQuery mergedQuery)
		{
			mergedQuery = null;
			BoolExpression boolExpression = null;
			BoolExpression boolExpression2 = null;
			switch (opType)
			{
			case CellTreeOpType.Union:
			case CellTreeOpType.FOJ:
				boolExpression = BoolExpression.True;
				boolExpression2 = BoolExpression.True;
				break;
			case CellTreeOpType.LOJ:
			case CellTreeOpType.LASJ:
				boolExpression2 = BoolExpression.True;
				break;
			}
			Dictionary<MemberPath, MemberPath> remap = new Dictionary<MemberPath, MemberPath>(MemberPath.EqualityComparer);
			if (!query1.Extent.Equals(query2.Extent))
			{
				return false;
			}
			MemberPath sourceExtentMemberPath = query1.SourceExtentMemberPath;
			BoolExpression conjunct = BoolExpression.True;
			BoolExpression boolExpression3 = BoolExpression.True;
			BoolExpression boolExpression4 = null;
			switch (opType)
			{
			case CellTreeOpType.Union:
			case CellTreeOpType.FOJ:
				conjunct = BoolExpression.CreateAnd(new BoolExpression[]
				{
					query1.WhereClause,
					boolExpression
				});
				boolExpression3 = BoolExpression.CreateAnd(new BoolExpression[]
				{
					query2.WhereClause,
					boolExpression2
				});
				boolExpression4 = BoolExpression.CreateOr(new BoolExpression[]
				{
					BoolExpression.CreateAnd(new BoolExpression[]
					{
						query1.WhereClause,
						boolExpression
					}),
					BoolExpression.CreateAnd(new BoolExpression[]
					{
						query2.WhereClause,
						boolExpression2
					})
				});
				break;
			case CellTreeOpType.LOJ:
				boolExpression3 = BoolExpression.CreateAnd(new BoolExpression[]
				{
					query2.WhereClause,
					boolExpression2
				});
				boolExpression4 = query1.WhereClause;
				break;
			case CellTreeOpType.IJ:
				boolExpression4 = BoolExpression.CreateAnd(new BoolExpression[]
				{
					query1.WhereClause,
					query2.WhereClause
				});
				break;
			case CellTreeOpType.LASJ:
				boolExpression3 = BoolExpression.CreateAnd(new BoolExpression[]
				{
					query2.WhereClause,
					boolExpression2
				});
				boolExpression4 = BoolExpression.CreateAnd(new BoolExpression[]
				{
					query1.WhereClause,
					BoolExpression.CreateNot(boolExpression3)
				});
				break;
			}
			List<BoolExpression> boolExprs = CellTreeSimplifier.MergeBoolExpressions(query1, query2, conjunct, boolExpression3, opType);
			ProjectedSlot[] projectedSlots;
			if (!ProjectedSlot.TryMergeRemapSlots(query1.ProjectedSlots, query2.ProjectedSlots, out projectedSlots))
			{
				return false;
			}
			boolExpression4 = boolExpression4.RemapBool(remap);
			CellQuery.SelectDistinct elimDupl = CellTreeSimplifier.MergeDupl(query1.SelectDistinctFlag, query2.SelectDistinctFlag);
			boolExpression4.ExpensiveSimplify();
			mergedQuery = new CellQuery(projectedSlots, boolExpression4, boolExprs, elimDupl, sourceExtentMemberPath);
			return true;
		}

		// Token: 0x060026EB RID: 9963 RVA: 0x000BD795 File Offset: 0x000BB995
		private static CellQuery.SelectDistinct MergeDupl(CellQuery.SelectDistinct d1, CellQuery.SelectDistinct d2)
		{
			if (d1 == CellQuery.SelectDistinct.Yes || d2 == CellQuery.SelectDistinct.Yes)
			{
				return CellQuery.SelectDistinct.Yes;
			}
			return CellQuery.SelectDistinct.No;
		}

		// Token: 0x060026EC RID: 9964 RVA: 0x000BD7A0 File Offset: 0x000BB9A0
		private static List<BoolExpression> MergeBoolExpressions(CellQuery query1, CellQuery query2, BoolExpression conjunct1, BoolExpression conjunct2, CellTreeOpType opType)
		{
			List<BoolExpression> list = query1.BoolVars;
			List<BoolExpression> list2 = query2.BoolVars;
			if (!conjunct1.IsTrue)
			{
				list = BoolExpression.AddConjunctionToBools(list, conjunct1);
			}
			if (!conjunct2.IsTrue)
			{
				list2 = BoolExpression.AddConjunctionToBools(list2, conjunct2);
			}
			List<BoolExpression> list3 = new List<BoolExpression>();
			for (int i = 0; i < list.Count; i++)
			{
				BoolExpression boolExpression = null;
				if (list[i] == null)
				{
					boolExpression = list2[i];
				}
				else if (list2[i] == null)
				{
					boolExpression = list[i];
				}
				else if (opType == CellTreeOpType.IJ)
				{
					boolExpression = BoolExpression.CreateAnd(new BoolExpression[]
					{
						list[i],
						list2[i]
					});
				}
				else if (opType == CellTreeOpType.Union)
				{
					boolExpression = BoolExpression.CreateOr(new BoolExpression[]
					{
						list[i],
						list2[i]
					});
				}
				else if (opType == CellTreeOpType.LASJ)
				{
					boolExpression = BoolExpression.CreateAnd(new BoolExpression[]
					{
						list[i],
						BoolExpression.CreateNot(list2[i])
					});
				}
				if (boolExpression != null)
				{
					boolExpression.ExpensiveSimplify();
				}
				list3.Add(boolExpression);
			}
			return list3;
		}

		// Token: 0x060026ED RID: 9965 RVA: 0x000BD8CA File Offset: 0x000BBACA
		internal override void ToCompactString(StringBuilder builder)
		{
			this.m_viewgenContext.MemberMaps.ProjectedSlotMap.ToCompactString(builder);
		}

		// Token: 0x04000EA1 RID: 3745
		private readonly ViewgenContext m_viewgenContext;
	}
}
