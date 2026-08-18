using System;
using System.Data.Query.PlanCompiler;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x02000116 RID: 278
	internal abstract class BasicOpVisitorOfT<TResultType>
	{
		// Token: 0x06000E15 RID: 3605 RVA: 0x0003D900 File Offset: 0x0003BB00
		protected virtual void VisitChildren(Node n)
		{
			for (int i = 0; i < n.Children.Count; i++)
			{
				this.VisitNode(n.Children[i]);
			}
		}

		// Token: 0x06000E16 RID: 3606 RVA: 0x0003D938 File Offset: 0x0003BB38
		protected virtual void VisitChildrenReverse(Node n)
		{
			for (int i = n.Children.Count - 1; i >= 0; i--)
			{
				this.VisitNode(n.Children[i]);
			}
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x0003D970 File Offset: 0x0003BB70
		internal TResultType VisitNode(Node n)
		{
			return n.Op.Accept<TResultType>(this, n);
		}

		// Token: 0x06000E18 RID: 3608 RVA: 0x0003D980 File Offset: 0x0003BB80
		protected virtual TResultType VisitDefault(Node n)
		{
			this.VisitChildren(n);
			return default(TResultType);
		}

		// Token: 0x06000E19 RID: 3609 RVA: 0x0003D9A0 File Offset: 0x0003BBA0
		internal virtual TResultType Unimplemented(Node n)
		{
			PlanCompiler.Assert(false, "Not implemented op type");
			return default(TResultType);
		}

		// Token: 0x06000E1A RID: 3610 RVA: 0x0003D9C1 File Offset: 0x0003BBC1
		public virtual TResultType Visit(Op op, Node n)
		{
			return this.Unimplemented(n);
		}

		// Token: 0x06000E1B RID: 3611 RVA: 0x0003D9CA File Offset: 0x0003BBCA
		protected virtual TResultType VisitAncillaryOpDefault(AncillaryOp op, Node n)
		{
			return this.VisitDefault(n);
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x0003D9D3 File Offset: 0x0003BBD3
		public virtual TResultType Visit(VarDefOp op, Node n)
		{
			return this.VisitAncillaryOpDefault(op, n);
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x0003D9D3 File Offset: 0x0003BBD3
		public virtual TResultType Visit(VarDefListOp op, Node n)
		{
			return this.VisitAncillaryOpDefault(op, n);
		}

		// Token: 0x06000E1E RID: 3614 RVA: 0x0003D9CA File Offset: 0x0003BBCA
		protected virtual TResultType VisitPhysicalOpDefault(PhysicalOp op, Node n)
		{
			return this.VisitDefault(n);
		}

		// Token: 0x06000E1F RID: 3615 RVA: 0x0003D9DD File Offset: 0x0003BBDD
		public virtual TResultType Visit(PhysicalProjectOp op, Node n)
		{
			return this.VisitPhysicalOpDefault(op, n);
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x0003D9DD File Offset: 0x0003BBDD
		protected virtual TResultType VisitNestOp(NestBaseOp op, Node n)
		{
			return this.VisitPhysicalOpDefault(op, n);
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x0003D9E7 File Offset: 0x0003BBE7
		public virtual TResultType Visit(SingleStreamNestOp op, Node n)
		{
			return this.VisitNestOp(op, n);
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x0003D9E7 File Offset: 0x0003BBE7
		public virtual TResultType Visit(MultiStreamNestOp op, Node n)
		{
			return this.VisitNestOp(op, n);
		}

		// Token: 0x06000E23 RID: 3619 RVA: 0x0003D9CA File Offset: 0x0003BBCA
		protected virtual TResultType VisitRelOpDefault(RelOp op, Node n)
		{
			return this.VisitDefault(n);
		}

		// Token: 0x06000E24 RID: 3620 RVA: 0x0003D9F1 File Offset: 0x0003BBF1
		protected virtual TResultType VisitApplyOp(ApplyBaseOp op, Node n)
		{
			return this.VisitRelOpDefault(op, n);
		}

		// Token: 0x06000E25 RID: 3621 RVA: 0x0003D9FB File Offset: 0x0003BBFB
		public virtual TResultType Visit(CrossApplyOp op, Node n)
		{
			return this.VisitApplyOp(op, n);
		}

		// Token: 0x06000E26 RID: 3622 RVA: 0x0003D9FB File Offset: 0x0003BBFB
		public virtual TResultType Visit(OuterApplyOp op, Node n)
		{
			return this.VisitApplyOp(op, n);
		}

		// Token: 0x06000E27 RID: 3623 RVA: 0x0003D9F1 File Offset: 0x0003BBF1
		protected virtual TResultType VisitJoinOp(JoinBaseOp op, Node n)
		{
			return this.VisitRelOpDefault(op, n);
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x0003DA05 File Offset: 0x0003BC05
		public virtual TResultType Visit(CrossJoinOp op, Node n)
		{
			return this.VisitJoinOp(op, n);
		}

		// Token: 0x06000E29 RID: 3625 RVA: 0x0003DA05 File Offset: 0x0003BC05
		public virtual TResultType Visit(FullOuterJoinOp op, Node n)
		{
			return this.VisitJoinOp(op, n);
		}

		// Token: 0x06000E2A RID: 3626 RVA: 0x0003DA05 File Offset: 0x0003BC05
		public virtual TResultType Visit(LeftOuterJoinOp op, Node n)
		{
			return this.VisitJoinOp(op, n);
		}

		// Token: 0x06000E2B RID: 3627 RVA: 0x0003DA05 File Offset: 0x0003BC05
		public virtual TResultType Visit(InnerJoinOp op, Node n)
		{
			return this.VisitJoinOp(op, n);
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x0003D9F1 File Offset: 0x0003BBF1
		protected virtual TResultType VisitSetOp(SetOp op, Node n)
		{
			return this.VisitRelOpDefault(op, n);
		}

		// Token: 0x06000E2D RID: 3629 RVA: 0x0003DA0F File Offset: 0x0003BC0F
		public virtual TResultType Visit(ExceptOp op, Node n)
		{
			return this.VisitSetOp(op, n);
		}

		// Token: 0x06000E2E RID: 3630 RVA: 0x0003DA0F File Offset: 0x0003BC0F
		public virtual TResultType Visit(IntersectOp op, Node n)
		{
			return this.VisitSetOp(op, n);
		}

		// Token: 0x06000E2F RID: 3631 RVA: 0x0003DA0F File Offset: 0x0003BC0F
		public virtual TResultType Visit(UnionAllOp op, Node n)
		{
			return this.VisitSetOp(op, n);
		}

		// Token: 0x06000E30 RID: 3632 RVA: 0x0003D9F1 File Offset: 0x0003BBF1
		public virtual TResultType Visit(DistinctOp op, Node n)
		{
			return this.VisitRelOpDefault(op, n);
		}

		// Token: 0x06000E31 RID: 3633 RVA: 0x0003D9F1 File Offset: 0x0003BBF1
		public virtual TResultType Visit(FilterOp op, Node n)
		{
			return this.VisitRelOpDefault(op, n);
		}

		// Token: 0x06000E32 RID: 3634 RVA: 0x0003D9F1 File Offset: 0x0003BBF1
		protected virtual TResultType VisitGroupByOp(GroupByBaseOp op, Node n)
		{
			return this.VisitRelOpDefault(op, n);
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x0003DA19 File Offset: 0x0003BC19
		public virtual TResultType Visit(GroupByOp op, Node n)
		{
			return this.VisitGroupByOp(op, n);
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x0003DA19 File Offset: 0x0003BC19
		public virtual TResultType Visit(GroupByIntoOp op, Node n)
		{
			return this.VisitGroupByOp(op, n);
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x0003D9F1 File Offset: 0x0003BBF1
		public virtual TResultType Visit(ProjectOp op, Node n)
		{
			return this.VisitRelOpDefault(op, n);
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x0003D9F1 File Offset: 0x0003BBF1
		protected virtual TResultType VisitTableOp(ScanTableBaseOp op, Node n)
		{
			return this.VisitRelOpDefault(op, n);
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x0003DA23 File Offset: 0x0003BC23
		public virtual TResultType Visit(ScanTableOp op, Node n)
		{
			return this.VisitTableOp(op, n);
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x0003DA23 File Offset: 0x0003BC23
		public virtual TResultType Visit(ScanViewOp op, Node n)
		{
			return this.VisitTableOp(op, n);
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x0003D9F1 File Offset: 0x0003BBF1
		public virtual TResultType Visit(SingleRowOp op, Node n)
		{
			return this.VisitRelOpDefault(op, n);
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x0003D9F1 File Offset: 0x0003BBF1
		public virtual TResultType Visit(SingleRowTableOp op, Node n)
		{
			return this.VisitRelOpDefault(op, n);
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x0003D9F1 File Offset: 0x0003BBF1
		protected virtual TResultType VisitSortOp(SortBaseOp op, Node n)
		{
			return this.VisitRelOpDefault(op, n);
		}

		// Token: 0x06000E3C RID: 3644 RVA: 0x0003DA2D File Offset: 0x0003BC2D
		public virtual TResultType Visit(SortOp op, Node n)
		{
			return this.VisitSortOp(op, n);
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x0003DA2D File Offset: 0x0003BC2D
		public virtual TResultType Visit(ConstrainedSortOp op, Node n)
		{
			return this.VisitSortOp(op, n);
		}

		// Token: 0x06000E3E RID: 3646 RVA: 0x0003D9F1 File Offset: 0x0003BBF1
		public virtual TResultType Visit(UnnestOp op, Node n)
		{
			return this.VisitRelOpDefault(op, n);
		}

		// Token: 0x06000E3F RID: 3647 RVA: 0x0003D9CA File Offset: 0x0003BBCA
		protected virtual TResultType VisitScalarOpDefault(ScalarOp op, Node n)
		{
			return this.VisitDefault(n);
		}

		// Token: 0x06000E40 RID: 3648 RVA: 0x0003DA37 File Offset: 0x0003BC37
		protected virtual TResultType VisitConstantOp(ConstantBaseOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06000E41 RID: 3649 RVA: 0x0003DA37 File Offset: 0x0003BC37
		public virtual TResultType Visit(AggregateOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06000E42 RID: 3650 RVA: 0x0003DA37 File Offset: 0x0003BC37
		public virtual TResultType Visit(ArithmeticOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x0003DA37 File Offset: 0x0003BC37
		public virtual TResultType Visit(CaseOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06000E44 RID: 3652 RVA: 0x0003DA37 File Offset: 0x0003BC37
		public virtual TResultType Visit(CastOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06000E45 RID: 3653 RVA: 0x0003DA37 File Offset: 0x0003BC37
		public virtual TResultType Visit(SoftCastOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06000E46 RID: 3654 RVA: 0x0003DA37 File Offset: 0x0003BC37
		public virtual TResultType Visit(CollectOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06000E47 RID: 3655 RVA: 0x0003DA37 File Offset: 0x0003BC37
		public virtual TResultType Visit(ComparisonOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06000E48 RID: 3656 RVA: 0x0003DA37 File Offset: 0x0003BC37
		public virtual TResultType Visit(ConditionalOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06000E49 RID: 3657 RVA: 0x0003DA41 File Offset: 0x0003BC41
		public virtual TResultType Visit(ConstantOp op, Node n)
		{
			return this.VisitConstantOp(op, n);
		}

		// Token: 0x06000E4A RID: 3658 RVA: 0x0003DA41 File Offset: 0x0003BC41
		public virtual TResultType Visit(ConstantPredicateOp op, Node n)
		{
			return this.VisitConstantOp(op, n);
		}

		// Token: 0x06000E4B RID: 3659 RVA: 0x0003DA37 File Offset: 0x0003BC37
		public virtual TResultType Visit(ElementOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06000E4C RID: 3660 RVA: 0x0003DA37 File Offset: 0x0003BC37
		public virtual TResultType Visit(ExistsOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x0003DA37 File Offset: 0x0003BC37
		public virtual TResultType Visit(FunctionOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x0003DA37 File Offset: 0x0003BC37
		public virtual TResultType Visit(GetEntityRefOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x0003DA37 File Offset: 0x0003BC37
		public virtual TResultType Visit(GetRefKeyOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06000E50 RID: 3664 RVA: 0x0003DA41 File Offset: 0x0003BC41
		public virtual TResultType Visit(InternalConstantOp op, Node n)
		{
			return this.VisitConstantOp(op, n);
		}

		// Token: 0x06000E51 RID: 3665 RVA: 0x0003DA37 File Offset: 0x0003BC37
		public virtual TResultType Visit(IsOfOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06000E52 RID: 3666 RVA: 0x0003DA37 File Offset: 0x0003BC37
		public virtual TResultType Visit(LikeOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06000E53 RID: 3667 RVA: 0x0003DA37 File Offset: 0x0003BC37
		public virtual TResultType Visit(NewEntityOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06000E54 RID: 3668 RVA: 0x0003DA37 File Offset: 0x0003BC37
		public virtual TResultType Visit(NewInstanceOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06000E55 RID: 3669 RVA: 0x0003DA37 File Offset: 0x0003BC37
		public virtual TResultType Visit(DiscriminatedNewEntityOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06000E56 RID: 3670 RVA: 0x0003DA37 File Offset: 0x0003BC37
		public virtual TResultType Visit(NewMultisetOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06000E57 RID: 3671 RVA: 0x0003DA37 File Offset: 0x0003BC37
		public virtual TResultType Visit(NewRecordOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06000E58 RID: 3672 RVA: 0x0003DA41 File Offset: 0x0003BC41
		public virtual TResultType Visit(NullOp op, Node n)
		{
			return this.VisitConstantOp(op, n);
		}

		// Token: 0x06000E59 RID: 3673 RVA: 0x0003DA41 File Offset: 0x0003BC41
		public virtual TResultType Visit(NullSentinelOp op, Node n)
		{
			return this.VisitConstantOp(op, n);
		}

		// Token: 0x06000E5A RID: 3674 RVA: 0x0003DA37 File Offset: 0x0003BC37
		public virtual TResultType Visit(PropertyOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06000E5B RID: 3675 RVA: 0x0003DA37 File Offset: 0x0003BC37
		public virtual TResultType Visit(RelPropertyOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06000E5C RID: 3676 RVA: 0x0003DA37 File Offset: 0x0003BC37
		public virtual TResultType Visit(RefOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06000E5D RID: 3677 RVA: 0x0003DA37 File Offset: 0x0003BC37
		public virtual TResultType Visit(TreatOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06000E5E RID: 3678 RVA: 0x0003DA37 File Offset: 0x0003BC37
		public virtual TResultType Visit(VarRefOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06000E5F RID: 3679 RVA: 0x0003DA37 File Offset: 0x0003BC37
		public virtual TResultType Visit(DerefOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06000E60 RID: 3680 RVA: 0x0003DA37 File Offset: 0x0003BC37
		public virtual TResultType Visit(NavigateOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}
	}
}
