using System;
using System.Collections.Generic;
using System.Data.Common.Utils;
using System.Data.Mapping.ViewGeneration.CqlGeneration;
using System.Data.Mapping.ViewGeneration.QueryRewriting;
using System.Linq;
using System.Text;

namespace System.Data.Mapping.ViewGeneration.Structures
{
	// Token: 0x020002A6 RID: 678
	internal abstract class CellTreeNode : InternalBase
	{
		// Token: 0x06002865 RID: 10341 RVA: 0x0009CCA5 File Offset: 0x0009AEA5
		protected CellTreeNode(ViewgenContext context)
		{
			this.m_viewgenContext = context;
		}

		// Token: 0x06002866 RID: 10342 RVA: 0x0009CCB4 File Offset: 0x0009AEB4
		internal CellTreeNode MakeCopy()
		{
			CellTreeNode.DefaultCellTreeVisitor<bool> visitor = new CellTreeNode.DefaultCellTreeVisitor<bool>();
			return this.Accept<bool, CellTreeNode>(visitor, true);
		}

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x06002867 RID: 10343
		internal abstract CellTreeOpType OpType { get; }

		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x06002868 RID: 10344
		internal abstract MemberDomainMap RightDomainMap { get; }

		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x06002869 RID: 10345
		internal abstract FragmentQuery LeftFragmentQuery { get; }

		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x0600286A RID: 10346
		internal abstract FragmentQuery RightFragmentQuery { get; }

		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x0600286B RID: 10347 RVA: 0x0009CCD1 File Offset: 0x0009AED1
		internal bool IsEmptyRightFragmentQuery
		{
			get
			{
				return !this.m_viewgenContext.RightFragmentQP.IsSatisfiable(this.RightFragmentQuery);
			}
		}

		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x0600286C RID: 10348
		internal abstract Set<MemberPath> Attributes { get; }

		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x0600286D RID: 10349
		internal abstract List<CellTreeNode> Children { get; }

		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x0600286E RID: 10350
		internal abstract int NumProjectedSlots { get; }

		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x0600286F RID: 10351
		internal abstract int NumBoolSlots { get; }

		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x06002870 RID: 10352 RVA: 0x0009CCEC File Offset: 0x0009AEEC
		internal MemberProjectionIndex ProjectedSlotMap
		{
			get
			{
				return this.m_viewgenContext.MemberMaps.ProjectedSlotMap;
			}
		}

		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x06002871 RID: 10353 RVA: 0x0009CCFE File Offset: 0x0009AEFE
		internal ViewgenContext ViewgenContext
		{
			get
			{
				return this.m_viewgenContext;
			}
		}

		// Token: 0x06002872 RID: 10354
		internal abstract CqlBlock ToCqlBlock(bool[] requiredSlots, CqlIdentifiers identifiers, ref int blockAliasNum, ref List<WithRelationship> withRelationships);

		// Token: 0x06002873 RID: 10355
		internal abstract bool IsProjectedSlot(int slot);

		// Token: 0x06002874 RID: 10356
		internal abstract TOutput Accept<TInput, TOutput>(CellTreeNode.CellTreeVisitor<TInput, TOutput> visitor, TInput param);

		// Token: 0x06002875 RID: 10357
		internal abstract TOutput Accept<TInput, TOutput>(CellTreeNode.SimpleCellTreeVisitor<TInput, TOutput> visitor, TInput param);

		// Token: 0x06002876 RID: 10358 RVA: 0x0009CD06 File Offset: 0x0009AF06
		internal CellTreeNode Flatten()
		{
			return CellTreeNode.FlatteningVisitor.Flatten(this);
		}

		// Token: 0x06002877 RID: 10359 RVA: 0x0009CD0E File Offset: 0x0009AF0E
		internal List<LeftCellWrapper> GetLeaves()
		{
			return (from leafNode in this.GetLeafNodes()
			select leafNode.LeftCellWrapper).ToList<LeftCellWrapper>();
		}

		// Token: 0x06002878 RID: 10360 RVA: 0x0009CD3F File Offset: 0x0009AF3F
		internal IEnumerable<LeafCellTreeNode> GetLeafNodes()
		{
			return CellTreeNode.LeafVisitor.GetLeaves(this);
		}

		// Token: 0x06002879 RID: 10361 RVA: 0x0009CD47 File Offset: 0x0009AF47
		internal CellTreeNode AssociativeFlatten()
		{
			return CellTreeNode.AssociativeOpFlatteningVisitor.Flatten(this);
		}

		// Token: 0x0600287A RID: 10362 RVA: 0x0009CD4F File Offset: 0x0009AF4F
		internal static bool IsAssociativeOp(CellTreeOpType opType)
		{
			return opType == CellTreeOpType.IJ || opType == CellTreeOpType.Union || opType == CellTreeOpType.FOJ;
		}

		// Token: 0x0600287B RID: 10363 RVA: 0x0009CD60 File Offset: 0x0009AF60
		internal bool[] GetProjectedSlots()
		{
			int num = this.ProjectedSlotMap.Count + this.NumBoolSlots;
			bool[] array = new bool[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = this.IsProjectedSlot(i);
			}
			return array;
		}

		// Token: 0x0600287C RID: 10364 RVA: 0x0009CD9E File Offset: 0x0009AF9E
		protected MemberPath GetMemberPath(int slotNum)
		{
			return this.ProjectedSlotMap.GetMemberPath(slotNum, this.NumBoolSlots);
		}

		// Token: 0x0600287D RID: 10365 RVA: 0x0009CDB2 File Offset: 0x0009AFB2
		protected int BoolIndexToSlot(int boolIndex)
		{
			return this.ProjectedSlotMap.BoolIndexToSlot(boolIndex, this.NumBoolSlots);
		}

		// Token: 0x0600287E RID: 10366 RVA: 0x0009CDC6 File Offset: 0x0009AFC6
		protected int SlotToBoolIndex(int slotNum)
		{
			return this.ProjectedSlotMap.SlotToBoolIndex(slotNum, this.NumBoolSlots);
		}

		// Token: 0x0600287F RID: 10367 RVA: 0x0009CDDA File Offset: 0x0009AFDA
		protected bool IsKeySlot(int slotNum)
		{
			return this.ProjectedSlotMap.IsKeySlot(slotNum, this.NumBoolSlots);
		}

		// Token: 0x06002880 RID: 10368 RVA: 0x0009CDEE File Offset: 0x0009AFEE
		protected bool IsBoolSlot(int slotNum)
		{
			return this.ProjectedSlotMap.IsBoolSlot(slotNum, this.NumBoolSlots);
		}

		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x06002881 RID: 10369 RVA: 0x0009CE04 File Offset: 0x0009B004
		protected IEnumerable<int> KeySlots
		{
			get
			{
				int numMembers = this.ProjectedSlotMap.Count;
				int num;
				for (int slotNum = 0; slotNum < numMembers; slotNum = num + 1)
				{
					if (this.IsKeySlot(slotNum))
					{
						yield return slotNum;
					}
					num = slotNum;
				}
				yield break;
			}
		}

		// Token: 0x06002882 RID: 10370 RVA: 0x0009CE24 File Offset: 0x0009B024
		internal override void ToFullString(StringBuilder builder)
		{
			int num = 0;
			bool[] projectedSlots = this.GetProjectedSlots();
			CqlIdentifiers identifiers = new CqlIdentifiers();
			List<WithRelationship> list = new List<WithRelationship>();
			CqlBlock cqlBlock = this.ToCqlBlock(projectedSlots, identifiers, ref num, ref list);
			cqlBlock.AsEsql(builder, false, 1);
		}

		// Token: 0x0400124F RID: 4687
		private ViewgenContext m_viewgenContext;

		// Token: 0x020005E4 RID: 1508
		internal abstract class CellTreeVisitor<TInput, TOutput>
		{
			// Token: 0x060041B4 RID: 16820
			internal abstract TOutput VisitLeaf(LeafCellTreeNode node, TInput param);

			// Token: 0x060041B5 RID: 16821
			internal abstract TOutput VisitUnion(OpCellTreeNode node, TInput param);

			// Token: 0x060041B6 RID: 16822
			internal abstract TOutput VisitInnerJoin(OpCellTreeNode node, TInput param);

			// Token: 0x060041B7 RID: 16823
			internal abstract TOutput VisitLeftOuterJoin(OpCellTreeNode node, TInput param);

			// Token: 0x060041B8 RID: 16824
			internal abstract TOutput VisitFullOuterJoin(OpCellTreeNode node, TInput param);

			// Token: 0x060041B9 RID: 16825
			internal abstract TOutput VisitLeftAntiSemiJoin(OpCellTreeNode node, TInput param);
		}

		// Token: 0x020005E5 RID: 1509
		internal abstract class SimpleCellTreeVisitor<TInput, TOutput>
		{
			// Token: 0x060041BB RID: 16827
			internal abstract TOutput VisitLeaf(LeafCellTreeNode node, TInput param);

			// Token: 0x060041BC RID: 16828
			internal abstract TOutput VisitOpNode(OpCellTreeNode node, TInput param);
		}

		// Token: 0x020005E6 RID: 1510
		private class DefaultCellTreeVisitor<TInput> : CellTreeNode.CellTreeVisitor<TInput, CellTreeNode>
		{
			// Token: 0x060041BE RID: 16830 RVA: 0x00002391 File Offset: 0x00000591
			internal override CellTreeNode VisitLeaf(LeafCellTreeNode node, TInput param)
			{
				return node;
			}

			// Token: 0x060041BF RID: 16831 RVA: 0x000EF677 File Offset: 0x000ED877
			internal override CellTreeNode VisitUnion(OpCellTreeNode node, TInput param)
			{
				return this.AcceptChildren(node, param);
			}

			// Token: 0x060041C0 RID: 16832 RVA: 0x000EF677 File Offset: 0x000ED877
			internal override CellTreeNode VisitInnerJoin(OpCellTreeNode node, TInput param)
			{
				return this.AcceptChildren(node, param);
			}

			// Token: 0x060041C1 RID: 16833 RVA: 0x000EF677 File Offset: 0x000ED877
			internal override CellTreeNode VisitLeftOuterJoin(OpCellTreeNode node, TInput param)
			{
				return this.AcceptChildren(node, param);
			}

			// Token: 0x060041C2 RID: 16834 RVA: 0x000EF677 File Offset: 0x000ED877
			internal override CellTreeNode VisitFullOuterJoin(OpCellTreeNode node, TInput param)
			{
				return this.AcceptChildren(node, param);
			}

			// Token: 0x060041C3 RID: 16835 RVA: 0x000EF677 File Offset: 0x000ED877
			internal override CellTreeNode VisitLeftAntiSemiJoin(OpCellTreeNode node, TInput param)
			{
				return this.AcceptChildren(node, param);
			}

			// Token: 0x060041C4 RID: 16836 RVA: 0x000EF684 File Offset: 0x000ED884
			private OpCellTreeNode AcceptChildren(OpCellTreeNode node, TInput param)
			{
				List<CellTreeNode> list = new List<CellTreeNode>();
				foreach (CellTreeNode cellTreeNode in node.Children)
				{
					list.Add(cellTreeNode.Accept<TInput, CellTreeNode>(this, param));
				}
				return new OpCellTreeNode(node.ViewgenContext, node.OpType, list);
			}
		}

		// Token: 0x020005E7 RID: 1511
		private class FlatteningVisitor : CellTreeNode.SimpleCellTreeVisitor<bool, CellTreeNode>
		{
			// Token: 0x060041C6 RID: 16838 RVA: 0x000EF700 File Offset: 0x000ED900
			protected FlatteningVisitor()
			{
			}

			// Token: 0x060041C7 RID: 16839 RVA: 0x000EF708 File Offset: 0x000ED908
			internal static CellTreeNode Flatten(CellTreeNode node)
			{
				CellTreeNode.FlatteningVisitor visitor = new CellTreeNode.FlatteningVisitor();
				return node.Accept<bool, CellTreeNode>(visitor, true);
			}

			// Token: 0x060041C8 RID: 16840 RVA: 0x00002391 File Offset: 0x00000591
			internal override CellTreeNode VisitLeaf(LeafCellTreeNode node, bool dummy)
			{
				return node;
			}

			// Token: 0x060041C9 RID: 16841 RVA: 0x000EF724 File Offset: 0x000ED924
			internal override CellTreeNode VisitOpNode(OpCellTreeNode node, bool dummy)
			{
				List<CellTreeNode> list = new List<CellTreeNode>();
				foreach (CellTreeNode cellTreeNode in node.Children)
				{
					CellTreeNode item = cellTreeNode.Accept<bool, CellTreeNode>(this, dummy);
					list.Add(item);
				}
				if (list.Count == 1)
				{
					return list[0];
				}
				return new OpCellTreeNode(node.ViewgenContext, node.OpType, list);
			}
		}

		// Token: 0x020005E8 RID: 1512
		private class AssociativeOpFlatteningVisitor : CellTreeNode.SimpleCellTreeVisitor<bool, CellTreeNode>
		{
			// Token: 0x060041CA RID: 16842 RVA: 0x000EF700 File Offset: 0x000ED900
			private AssociativeOpFlatteningVisitor()
			{
			}

			// Token: 0x060041CB RID: 16843 RVA: 0x000EF7B0 File Offset: 0x000ED9B0
			internal static CellTreeNode Flatten(CellTreeNode node)
			{
				CellTreeNode cellTreeNode = CellTreeNode.FlatteningVisitor.Flatten(node);
				CellTreeNode.AssociativeOpFlatteningVisitor visitor = new CellTreeNode.AssociativeOpFlatteningVisitor();
				return cellTreeNode.Accept<bool, CellTreeNode>(visitor, true);
			}

			// Token: 0x060041CC RID: 16844 RVA: 0x00002391 File Offset: 0x00000591
			internal override CellTreeNode VisitLeaf(LeafCellTreeNode node, bool dummy)
			{
				return node;
			}

			// Token: 0x060041CD RID: 16845 RVA: 0x000EF7D4 File Offset: 0x000ED9D4
			internal override CellTreeNode VisitOpNode(OpCellTreeNode node, bool dummy)
			{
				List<CellTreeNode> list = new List<CellTreeNode>();
				foreach (CellTreeNode cellTreeNode in node.Children)
				{
					CellTreeNode item = cellTreeNode.Accept<bool, CellTreeNode>(this, dummy);
					list.Add(item);
				}
				List<CellTreeNode> list2 = list;
				if (CellTreeNode.IsAssociativeOp(node.OpType))
				{
					list2 = new List<CellTreeNode>();
					foreach (CellTreeNode cellTreeNode2 in list)
					{
						if (cellTreeNode2.OpType == node.OpType)
						{
							list2.AddRange(cellTreeNode2.Children);
						}
						else
						{
							list2.Add(cellTreeNode2);
						}
					}
				}
				return new OpCellTreeNode(node.ViewgenContext, node.OpType, list2);
			}
		}

		// Token: 0x020005E9 RID: 1513
		private class LeafVisitor : CellTreeNode.SimpleCellTreeVisitor<bool, IEnumerable<LeafCellTreeNode>>
		{
			// Token: 0x060041CE RID: 16846 RVA: 0x000EF8C4 File Offset: 0x000EDAC4
			private LeafVisitor()
			{
			}

			// Token: 0x060041CF RID: 16847 RVA: 0x000EF8CC File Offset: 0x000EDACC
			internal static IEnumerable<LeafCellTreeNode> GetLeaves(CellTreeNode node)
			{
				CellTreeNode.LeafVisitor visitor = new CellTreeNode.LeafVisitor();
				return node.Accept<bool, IEnumerable<LeafCellTreeNode>>(visitor, true);
			}

			// Token: 0x060041D0 RID: 16848 RVA: 0x000EF8E7 File Offset: 0x000EDAE7
			internal override IEnumerable<LeafCellTreeNode> VisitLeaf(LeafCellTreeNode node, bool dummy)
			{
				yield return node;
				yield break;
			}

			// Token: 0x060041D1 RID: 16849 RVA: 0x000EF8F7 File Offset: 0x000EDAF7
			internal override IEnumerable<LeafCellTreeNode> VisitOpNode(OpCellTreeNode node, bool dummy)
			{
				foreach (CellTreeNode cellTreeNode in node.Children)
				{
					IEnumerable<LeafCellTreeNode> enumerable = cellTreeNode.Accept<bool, IEnumerable<LeafCellTreeNode>>(this, dummy);
					foreach (LeafCellTreeNode leafCellTreeNode in enumerable)
					{
						yield return leafCellTreeNode;
					}
					IEnumerator<LeafCellTreeNode> enumerator2 = null;
				}
				List<CellTreeNode>.Enumerator enumerator = default(List<CellTreeNode>.Enumerator);
				yield break;
				yield break;
			}
		}
	}
}
