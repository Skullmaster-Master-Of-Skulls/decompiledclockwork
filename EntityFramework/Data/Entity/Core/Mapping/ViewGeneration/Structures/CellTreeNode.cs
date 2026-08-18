using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.CqlGeneration;
using System.Data.Entity.Core.Mapping.ViewGeneration.QueryRewriting;
using System.Linq;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Structures
{
	// Token: 0x02000443 RID: 1091
	internal abstract class CellTreeNode : InternalBase
	{
		// Token: 0x06002837 RID: 10295 RVA: 0x000C4974 File Offset: 0x000C2B74
		protected CellTreeNode(ViewgenContext context)
		{
			this.m_viewgenContext = context;
		}

		// Token: 0x06002838 RID: 10296 RVA: 0x000C4984 File Offset: 0x000C2B84
		internal CellTreeNode MakeCopy()
		{
			CellTreeNode.DefaultCellTreeVisitor<bool> visitor = new CellTreeNode.DefaultCellTreeVisitor<bool>();
			return this.Accept<bool, CellTreeNode>(visitor, true);
		}

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x06002839 RID: 10297
		internal abstract CellTreeOpType OpType { get; }

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x0600283A RID: 10298
		internal abstract MemberDomainMap RightDomainMap { get; }

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x0600283B RID: 10299
		internal abstract FragmentQuery LeftFragmentQuery { get; }

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x0600283C RID: 10300
		internal abstract FragmentQuery RightFragmentQuery { get; }

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x0600283D RID: 10301 RVA: 0x000C49A1 File Offset: 0x000C2BA1
		internal bool IsEmptyRightFragmentQuery
		{
			get
			{
				return !this.m_viewgenContext.RightFragmentQP.IsSatisfiable(this.RightFragmentQuery);
			}
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x0600283E RID: 10302
		internal abstract Set<MemberPath> Attributes { get; }

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x0600283F RID: 10303
		internal abstract List<CellTreeNode> Children { get; }

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x06002840 RID: 10304
		internal abstract int NumProjectedSlots { get; }

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x06002841 RID: 10305
		internal abstract int NumBoolSlots { get; }

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x06002842 RID: 10306 RVA: 0x000C49BC File Offset: 0x000C2BBC
		internal MemberProjectionIndex ProjectedSlotMap
		{
			get
			{
				return this.m_viewgenContext.MemberMaps.ProjectedSlotMap;
			}
		}

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x06002843 RID: 10307 RVA: 0x000C49CE File Offset: 0x000C2BCE
		internal ViewgenContext ViewgenContext
		{
			get
			{
				return this.m_viewgenContext;
			}
		}

		// Token: 0x06002844 RID: 10308
		internal abstract CqlBlock ToCqlBlock(bool[] requiredSlots, CqlIdentifiers identifiers, ref int blockAliasNum, ref List<WithRelationship> withRelationships);

		// Token: 0x06002845 RID: 10309
		internal abstract bool IsProjectedSlot(int slot);

		// Token: 0x06002846 RID: 10310
		internal abstract TOutput Accept<TInput, TOutput>(CellTreeNode.CellTreeVisitor<TInput, TOutput> visitor, TInput param);

		// Token: 0x06002847 RID: 10311
		internal abstract TOutput Accept<TInput, TOutput>(CellTreeNode.SimpleCellTreeVisitor<TInput, TOutput> visitor, TInput param);

		// Token: 0x06002848 RID: 10312 RVA: 0x000C49D6 File Offset: 0x000C2BD6
		internal CellTreeNode Flatten()
		{
			return CellTreeNode.FlatteningVisitor.Flatten(this);
		}

		// Token: 0x06002849 RID: 10313 RVA: 0x000C49E6 File Offset: 0x000C2BE6
		internal List<LeftCellWrapper> GetLeaves()
		{
			return (from leafNode in this.GetLeafNodes()
			select leafNode.LeftCellWrapper).ToList<LeftCellWrapper>();
		}

		// Token: 0x0600284A RID: 10314 RVA: 0x000C4A15 File Offset: 0x000C2C15
		internal IEnumerable<LeafCellTreeNode> GetLeafNodes()
		{
			return CellTreeNode.LeafVisitor.GetLeaves(this);
		}

		// Token: 0x0600284B RID: 10315 RVA: 0x000C4A1D File Offset: 0x000C2C1D
		internal CellTreeNode AssociativeFlatten()
		{
			return CellTreeNode.AssociativeOpFlatteningVisitor.Flatten(this);
		}

		// Token: 0x0600284C RID: 10316 RVA: 0x000C4A25 File Offset: 0x000C2C25
		internal static bool IsAssociativeOp(CellTreeOpType opType)
		{
			return opType == CellTreeOpType.IJ || opType == CellTreeOpType.Union || opType == CellTreeOpType.FOJ;
		}

		// Token: 0x0600284D RID: 10317 RVA: 0x000C4A38 File Offset: 0x000C2C38
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

		// Token: 0x0600284E RID: 10318 RVA: 0x000C4A76 File Offset: 0x000C2C76
		protected MemberPath GetMemberPath(int slotNum)
		{
			return this.ProjectedSlotMap.GetMemberPath(slotNum, this.NumBoolSlots);
		}

		// Token: 0x0600284F RID: 10319 RVA: 0x000C4A8A File Offset: 0x000C2C8A
		protected int BoolIndexToSlot(int boolIndex)
		{
			return this.ProjectedSlotMap.BoolIndexToSlot(boolIndex, this.NumBoolSlots);
		}

		// Token: 0x06002850 RID: 10320 RVA: 0x000C4A9E File Offset: 0x000C2C9E
		protected int SlotToBoolIndex(int slotNum)
		{
			return this.ProjectedSlotMap.SlotToBoolIndex(slotNum, this.NumBoolSlots);
		}

		// Token: 0x06002851 RID: 10321 RVA: 0x000C4AB2 File Offset: 0x000C2CB2
		protected bool IsKeySlot(int slotNum)
		{
			return this.ProjectedSlotMap.IsKeySlot(slotNum, this.NumBoolSlots);
		}

		// Token: 0x06002852 RID: 10322 RVA: 0x000C4AC6 File Offset: 0x000C2CC6
		protected bool IsBoolSlot(int slotNum)
		{
			return this.ProjectedSlotMap.IsBoolSlot(slotNum, this.NumBoolSlots);
		}

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06002853 RID: 10323 RVA: 0x000C4BF8 File Offset: 0x000C2DF8
		protected IEnumerable<int> KeySlots
		{
			get
			{
				int numMembers = this.ProjectedSlotMap.Count;
				for (int slotNum = 0; slotNum < numMembers; slotNum++)
				{
					if (this.IsKeySlot(slotNum))
					{
						yield return slotNum;
					}
				}
				yield break;
			}
		}

		// Token: 0x06002854 RID: 10324 RVA: 0x000C4C18 File Offset: 0x000C2E18
		internal override void ToFullString(StringBuilder builder)
		{
			int num = 0;
			bool[] projectedSlots = this.GetProjectedSlots();
			CqlIdentifiers identifiers = new CqlIdentifiers();
			List<WithRelationship> list = new List<WithRelationship>();
			CqlBlock cqlBlock = this.ToCqlBlock(projectedSlots, identifiers, ref num, ref list);
			cqlBlock.AsEsql(builder, false, 1);
		}

		// Token: 0x04000F2D RID: 3885
		private readonly ViewgenContext m_viewgenContext;

		// Token: 0x02000444 RID: 1092
		internal abstract class CellTreeVisitor<TInput, TOutput>
		{
			// Token: 0x06002856 RID: 10326
			internal abstract TOutput VisitLeaf(LeafCellTreeNode node, TInput param);

			// Token: 0x06002857 RID: 10327
			internal abstract TOutput VisitUnion(OpCellTreeNode node, TInput param);

			// Token: 0x06002858 RID: 10328
			internal abstract TOutput VisitInnerJoin(OpCellTreeNode node, TInput param);

			// Token: 0x06002859 RID: 10329
			internal abstract TOutput VisitLeftOuterJoin(OpCellTreeNode node, TInput param);

			// Token: 0x0600285A RID: 10330
			internal abstract TOutput VisitFullOuterJoin(OpCellTreeNode node, TInput param);

			// Token: 0x0600285B RID: 10331
			internal abstract TOutput VisitLeftAntiSemiJoin(OpCellTreeNode node, TInput param);
		}

		// Token: 0x02000445 RID: 1093
		internal abstract class SimpleCellTreeVisitor<TInput, TOutput>
		{
			// Token: 0x0600285D RID: 10333
			internal abstract TOutput VisitLeaf(LeafCellTreeNode node, TInput param);

			// Token: 0x0600285E RID: 10334
			internal abstract TOutput VisitOpNode(OpCellTreeNode node, TInput param);
		}

		// Token: 0x02000446 RID: 1094
		private class DefaultCellTreeVisitor<TInput> : CellTreeNode.CellTreeVisitor<TInput, CellTreeNode>
		{
			// Token: 0x06002860 RID: 10336 RVA: 0x000C4C63 File Offset: 0x000C2E63
			internal override CellTreeNode VisitLeaf(LeafCellTreeNode node, TInput param)
			{
				return node;
			}

			// Token: 0x06002861 RID: 10337 RVA: 0x000C4C66 File Offset: 0x000C2E66
			internal override CellTreeNode VisitUnion(OpCellTreeNode node, TInput param)
			{
				return this.AcceptChildren(node, param);
			}

			// Token: 0x06002862 RID: 10338 RVA: 0x000C4C70 File Offset: 0x000C2E70
			internal override CellTreeNode VisitInnerJoin(OpCellTreeNode node, TInput param)
			{
				return this.AcceptChildren(node, param);
			}

			// Token: 0x06002863 RID: 10339 RVA: 0x000C4C7A File Offset: 0x000C2E7A
			internal override CellTreeNode VisitLeftOuterJoin(OpCellTreeNode node, TInput param)
			{
				return this.AcceptChildren(node, param);
			}

			// Token: 0x06002864 RID: 10340 RVA: 0x000C4C84 File Offset: 0x000C2E84
			internal override CellTreeNode VisitFullOuterJoin(OpCellTreeNode node, TInput param)
			{
				return this.AcceptChildren(node, param);
			}

			// Token: 0x06002865 RID: 10341 RVA: 0x000C4C8E File Offset: 0x000C2E8E
			internal override CellTreeNode VisitLeftAntiSemiJoin(OpCellTreeNode node, TInput param)
			{
				return this.AcceptChildren(node, param);
			}

			// Token: 0x06002866 RID: 10342 RVA: 0x000C4C98 File Offset: 0x000C2E98
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

		// Token: 0x02000447 RID: 1095
		private class FlatteningVisitor : CellTreeNode.SimpleCellTreeVisitor<bool, CellTreeNode>
		{
			// Token: 0x06002868 RID: 10344 RVA: 0x000C4D14 File Offset: 0x000C2F14
			protected FlatteningVisitor()
			{
			}

			// Token: 0x06002869 RID: 10345 RVA: 0x000C4D1C File Offset: 0x000C2F1C
			internal static CellTreeNode Flatten(CellTreeNode node)
			{
				CellTreeNode.FlatteningVisitor visitor = new CellTreeNode.FlatteningVisitor();
				return node.Accept<bool, CellTreeNode>(visitor, true);
			}

			// Token: 0x0600286A RID: 10346 RVA: 0x000C4D37 File Offset: 0x000C2F37
			internal override CellTreeNode VisitLeaf(LeafCellTreeNode node, bool dummy)
			{
				return node;
			}

			// Token: 0x0600286B RID: 10347 RVA: 0x000C4D3C File Offset: 0x000C2F3C
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

		// Token: 0x02000448 RID: 1096
		private class AssociativeOpFlatteningVisitor : CellTreeNode.SimpleCellTreeVisitor<bool, CellTreeNode>
		{
			// Token: 0x0600286C RID: 10348 RVA: 0x000C4DC4 File Offset: 0x000C2FC4
			private AssociativeOpFlatteningVisitor()
			{
			}

			// Token: 0x0600286D RID: 10349 RVA: 0x000C4DCC File Offset: 0x000C2FCC
			internal static CellTreeNode Flatten(CellTreeNode node)
			{
				CellTreeNode cellTreeNode = CellTreeNode.FlatteningVisitor.Flatten(node);
				CellTreeNode.AssociativeOpFlatteningVisitor visitor = new CellTreeNode.AssociativeOpFlatteningVisitor();
				return cellTreeNode.Accept<bool, CellTreeNode>(visitor, true);
			}

			// Token: 0x0600286E RID: 10350 RVA: 0x000C4DEE File Offset: 0x000C2FEE
			internal override CellTreeNode VisitLeaf(LeafCellTreeNode node, bool dummy)
			{
				return node;
			}

			// Token: 0x0600286F RID: 10351 RVA: 0x000C4DF4 File Offset: 0x000C2FF4
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

		// Token: 0x02000449 RID: 1097
		private class LeafVisitor : CellTreeNode.SimpleCellTreeVisitor<bool, IEnumerable<LeafCellTreeNode>>
		{
			// Token: 0x06002870 RID: 10352 RVA: 0x000C4EE0 File Offset: 0x000C30E0
			private LeafVisitor()
			{
			}

			// Token: 0x06002871 RID: 10353 RVA: 0x000C4EE8 File Offset: 0x000C30E8
			internal static IEnumerable<LeafCellTreeNode> GetLeaves(CellTreeNode node)
			{
				CellTreeNode.LeafVisitor visitor = new CellTreeNode.LeafVisitor();
				return node.Accept<bool, IEnumerable<LeafCellTreeNode>>(visitor, true);
			}

			// Token: 0x06002872 RID: 10354 RVA: 0x000C4FD8 File Offset: 0x000C31D8
			internal override IEnumerable<LeafCellTreeNode> VisitLeaf(LeafCellTreeNode node, bool dummy)
			{
				yield return node;
				yield break;
			}

			// Token: 0x06002873 RID: 10355 RVA: 0x000C5244 File Offset: 0x000C3444
			internal override IEnumerable<LeafCellTreeNode> VisitOpNode(OpCellTreeNode node, bool dummy)
			{
				foreach (CellTreeNode child in node.Children)
				{
					IEnumerable<LeafCellTreeNode> children = child.Accept<bool, IEnumerable<LeafCellTreeNode>>(this, dummy);
					foreach (LeafCellTreeNode leafNode in children)
					{
						yield return leafNode;
					}
				}
				yield break;
			}
		}
	}
}
