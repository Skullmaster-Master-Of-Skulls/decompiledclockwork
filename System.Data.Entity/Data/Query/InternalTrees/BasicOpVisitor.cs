using System;
using System.Data.Entity;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x02000115 RID: 277
	internal abstract class BasicOpVisitor
	{
		// Token: 0x06000DC9 RID: 3529 RVA: 0x00002050 File Offset: 0x00000250
		internal BasicOpVisitor()
		{
		}

		// Token: 0x06000DCA RID: 3530 RVA: 0x0003D7E8 File Offset: 0x0003B9E8
		protected virtual void VisitChildren(Node n)
		{
			foreach (Node n2 in n.Children)
			{
				this.VisitNode(n2);
			}
		}

		// Token: 0x06000DCB RID: 3531 RVA: 0x0003D83C File Offset: 0x0003BA3C
		protected virtual void VisitChildrenReverse(Node n)
		{
			for (int i = n.Children.Count - 1; i >= 0; i--)
			{
				this.VisitNode(n.Children[i]);
			}
		}

		// Token: 0x06000DCC RID: 3532 RVA: 0x0002C254 File Offset: 0x0002A454
		internal virtual void VisitNode(Node n)
		{
			n.Op.Accept(this, n);
		}

		// Token: 0x06000DCD RID: 3533 RVA: 0x0003D873 File Offset: 0x0003BA73
		protected virtual void VisitDefault(Node n)
		{
			this.VisitChildren(n);
		}

		// Token: 0x06000DCE RID: 3534 RVA: 0x0003D87C File Offset: 0x0003BA7C
		protected virtual void VisitConstantOp(ConstantBaseOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06000DCF RID: 3535 RVA: 0x0003D886 File Offset: 0x0003BA86
		protected virtual void VisitTableOp(ScanTableBaseOp op, Node n)
		{
			this.VisitRelOpDefault(op, n);
		}

		// Token: 0x06000DD0 RID: 3536 RVA: 0x0003D886 File Offset: 0x0003BA86
		protected virtual void VisitJoinOp(JoinBaseOp op, Node n)
		{
			this.VisitRelOpDefault(op, n);
		}

		// Token: 0x06000DD1 RID: 3537 RVA: 0x0003D886 File Offset: 0x0003BA86
		protected virtual void VisitApplyOp(ApplyBaseOp op, Node n)
		{
			this.VisitRelOpDefault(op, n);
		}

		// Token: 0x06000DD2 RID: 3538 RVA: 0x0003D886 File Offset: 0x0003BA86
		protected virtual void VisitSetOp(SetOp op, Node n)
		{
			this.VisitRelOpDefault(op, n);
		}

		// Token: 0x06000DD3 RID: 3539 RVA: 0x0003D886 File Offset: 0x0003BA86
		protected virtual void VisitSortOp(SortBaseOp op, Node n)
		{
			this.VisitRelOpDefault(op, n);
		}

		// Token: 0x06000DD4 RID: 3540 RVA: 0x0003D886 File Offset: 0x0003BA86
		protected virtual void VisitGroupByOp(GroupByBaseOp op, Node n)
		{
			this.VisitRelOpDefault(op, n);
		}

		// Token: 0x06000DD5 RID: 3541 RVA: 0x0003B181 File Offset: 0x00039381
		public virtual void Visit(Op op, Node n)
		{
			throw new NotSupportedException(Strings.Iqt_General_UnsupportedOp(op.GetType().FullName));
		}

		// Token: 0x06000DD6 RID: 3542 RVA: 0x0003D890 File Offset: 0x0003BA90
		protected virtual void VisitScalarOpDefault(ScalarOp op, Node n)
		{
			this.VisitDefault(n);
		}

		// Token: 0x06000DD7 RID: 3543 RVA: 0x0003D899 File Offset: 0x0003BA99
		public virtual void Visit(ConstantOp op, Node n)
		{
			this.VisitConstantOp(op, n);
		}

		// Token: 0x06000DD8 RID: 3544 RVA: 0x0003D899 File Offset: 0x0003BA99
		public virtual void Visit(NullOp op, Node n)
		{
			this.VisitConstantOp(op, n);
		}

		// Token: 0x06000DD9 RID: 3545 RVA: 0x0003D899 File Offset: 0x0003BA99
		public virtual void Visit(NullSentinelOp op, Node n)
		{
			this.VisitConstantOp(op, n);
		}

		// Token: 0x06000DDA RID: 3546 RVA: 0x0003D899 File Offset: 0x0003BA99
		public virtual void Visit(InternalConstantOp op, Node n)
		{
			this.VisitConstantOp(op, n);
		}

		// Token: 0x06000DDB RID: 3547 RVA: 0x0003D899 File Offset: 0x0003BA99
		public virtual void Visit(ConstantPredicateOp op, Node n)
		{
			this.VisitConstantOp(op, n);
		}

		// Token: 0x06000DDC RID: 3548 RVA: 0x0003D87C File Offset: 0x0003BA7C
		public virtual void Visit(FunctionOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06000DDD RID: 3549 RVA: 0x0003D87C File Offset: 0x0003BA7C
		public virtual void Visit(PropertyOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06000DDE RID: 3550 RVA: 0x0003D87C File Offset: 0x0003BA7C
		public virtual void Visit(RelPropertyOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06000DDF RID: 3551 RVA: 0x0003D87C File Offset: 0x0003BA7C
		public virtual void Visit(CaseOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06000DE0 RID: 3552 RVA: 0x0003D87C File Offset: 0x0003BA7C
		public virtual void Visit(ComparisonOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06000DE1 RID: 3553 RVA: 0x0003D87C File Offset: 0x0003BA7C
		public virtual void Visit(LikeOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06000DE2 RID: 3554 RVA: 0x0003D87C File Offset: 0x0003BA7C
		public virtual void Visit(AggregateOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06000DE3 RID: 3555 RVA: 0x0003D87C File Offset: 0x0003BA7C
		public virtual void Visit(NewInstanceOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06000DE4 RID: 3556 RVA: 0x0003D87C File Offset: 0x0003BA7C
		public virtual void Visit(NewEntityOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06000DE5 RID: 3557 RVA: 0x0003D87C File Offset: 0x0003BA7C
		public virtual void Visit(DiscriminatedNewEntityOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06000DE6 RID: 3558 RVA: 0x0003D87C File Offset: 0x0003BA7C
		public virtual void Visit(NewMultisetOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06000DE7 RID: 3559 RVA: 0x0003D87C File Offset: 0x0003BA7C
		public virtual void Visit(NewRecordOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06000DE8 RID: 3560 RVA: 0x0003D87C File Offset: 0x0003BA7C
		public virtual void Visit(RefOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06000DE9 RID: 3561 RVA: 0x0003D87C File Offset: 0x0003BA7C
		public virtual void Visit(VarRefOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06000DEA RID: 3562 RVA: 0x0003D87C File Offset: 0x0003BA7C
		public virtual void Visit(ConditionalOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06000DEB RID: 3563 RVA: 0x0003D87C File Offset: 0x0003BA7C
		public virtual void Visit(ArithmeticOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06000DEC RID: 3564 RVA: 0x0003D87C File Offset: 0x0003BA7C
		public virtual void Visit(TreatOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06000DED RID: 3565 RVA: 0x0003D87C File Offset: 0x0003BA7C
		public virtual void Visit(CastOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06000DEE RID: 3566 RVA: 0x0003D87C File Offset: 0x0003BA7C
		public virtual void Visit(SoftCastOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06000DEF RID: 3567 RVA: 0x0003D87C File Offset: 0x0003BA7C
		public virtual void Visit(IsOfOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06000DF0 RID: 3568 RVA: 0x0003D87C File Offset: 0x0003BA7C
		public virtual void Visit(ExistsOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06000DF1 RID: 3569 RVA: 0x0003D87C File Offset: 0x0003BA7C
		public virtual void Visit(ElementOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06000DF2 RID: 3570 RVA: 0x0003D87C File Offset: 0x0003BA7C
		public virtual void Visit(GetEntityRefOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06000DF3 RID: 3571 RVA: 0x0003D87C File Offset: 0x0003BA7C
		public virtual void Visit(GetRefKeyOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06000DF4 RID: 3572 RVA: 0x0003D87C File Offset: 0x0003BA7C
		public virtual void Visit(CollectOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06000DF5 RID: 3573 RVA: 0x0003D87C File Offset: 0x0003BA7C
		public virtual void Visit(DerefOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06000DF6 RID: 3574 RVA: 0x0003D87C File Offset: 0x0003BA7C
		public virtual void Visit(NavigateOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06000DF7 RID: 3575 RVA: 0x0003D890 File Offset: 0x0003BA90
		protected virtual void VisitAncillaryOpDefault(AncillaryOp op, Node n)
		{
			this.VisitDefault(n);
		}

		// Token: 0x06000DF8 RID: 3576 RVA: 0x0003D8A3 File Offset: 0x0003BAA3
		public virtual void Visit(VarDefOp op, Node n)
		{
			this.VisitAncillaryOpDefault(op, n);
		}

		// Token: 0x06000DF9 RID: 3577 RVA: 0x0003D8A3 File Offset: 0x0003BAA3
		public virtual void Visit(VarDefListOp op, Node n)
		{
			this.VisitAncillaryOpDefault(op, n);
		}

		// Token: 0x06000DFA RID: 3578 RVA: 0x0003D890 File Offset: 0x0003BA90
		protected virtual void VisitRelOpDefault(RelOp op, Node n)
		{
			this.VisitDefault(n);
		}

		// Token: 0x06000DFB RID: 3579 RVA: 0x0003D8AD File Offset: 0x0003BAAD
		public virtual void Visit(ScanTableOp op, Node n)
		{
			this.VisitTableOp(op, n);
		}

		// Token: 0x06000DFC RID: 3580 RVA: 0x0003D8AD File Offset: 0x0003BAAD
		public virtual void Visit(ScanViewOp op, Node n)
		{
			this.VisitTableOp(op, n);
		}

		// Token: 0x06000DFD RID: 3581 RVA: 0x0003D886 File Offset: 0x0003BA86
		public virtual void Visit(UnnestOp op, Node n)
		{
			this.VisitRelOpDefault(op, n);
		}

		// Token: 0x06000DFE RID: 3582 RVA: 0x0003D886 File Offset: 0x0003BA86
		public virtual void Visit(ProjectOp op, Node n)
		{
			this.VisitRelOpDefault(op, n);
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x0003D886 File Offset: 0x0003BA86
		public virtual void Visit(FilterOp op, Node n)
		{
			this.VisitRelOpDefault(op, n);
		}

		// Token: 0x06000E00 RID: 3584 RVA: 0x0003D8B7 File Offset: 0x0003BAB7
		public virtual void Visit(SortOp op, Node n)
		{
			this.VisitSortOp(op, n);
		}

		// Token: 0x06000E01 RID: 3585 RVA: 0x0003D8B7 File Offset: 0x0003BAB7
		public virtual void Visit(ConstrainedSortOp op, Node n)
		{
			this.VisitSortOp(op, n);
		}

		// Token: 0x06000E02 RID: 3586 RVA: 0x0003D8C1 File Offset: 0x0003BAC1
		public virtual void Visit(GroupByOp op, Node n)
		{
			this.VisitGroupByOp(op, n);
		}

		// Token: 0x06000E03 RID: 3587 RVA: 0x0003D8C1 File Offset: 0x0003BAC1
		public virtual void Visit(GroupByIntoOp op, Node n)
		{
			this.VisitGroupByOp(op, n);
		}

		// Token: 0x06000E04 RID: 3588 RVA: 0x0003D8CB File Offset: 0x0003BACB
		public virtual void Visit(CrossJoinOp op, Node n)
		{
			this.VisitJoinOp(op, n);
		}

		// Token: 0x06000E05 RID: 3589 RVA: 0x0003D8CB File Offset: 0x0003BACB
		public virtual void Visit(InnerJoinOp op, Node n)
		{
			this.VisitJoinOp(op, n);
		}

		// Token: 0x06000E06 RID: 3590 RVA: 0x0003D8CB File Offset: 0x0003BACB
		public virtual void Visit(LeftOuterJoinOp op, Node n)
		{
			this.VisitJoinOp(op, n);
		}

		// Token: 0x06000E07 RID: 3591 RVA: 0x0003D8CB File Offset: 0x0003BACB
		public virtual void Visit(FullOuterJoinOp op, Node n)
		{
			this.VisitJoinOp(op, n);
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x0003D8D5 File Offset: 0x0003BAD5
		public virtual void Visit(CrossApplyOp op, Node n)
		{
			this.VisitApplyOp(op, n);
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x0003D8D5 File Offset: 0x0003BAD5
		public virtual void Visit(OuterApplyOp op, Node n)
		{
			this.VisitApplyOp(op, n);
		}

		// Token: 0x06000E0A RID: 3594 RVA: 0x0003D8DF File Offset: 0x0003BADF
		public virtual void Visit(UnionAllOp op, Node n)
		{
			this.VisitSetOp(op, n);
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x0003D8DF File Offset: 0x0003BADF
		public virtual void Visit(IntersectOp op, Node n)
		{
			this.VisitSetOp(op, n);
		}

		// Token: 0x06000E0C RID: 3596 RVA: 0x0003D8DF File Offset: 0x0003BADF
		public virtual void Visit(ExceptOp op, Node n)
		{
			this.VisitSetOp(op, n);
		}

		// Token: 0x06000E0D RID: 3597 RVA: 0x0003D886 File Offset: 0x0003BA86
		public virtual void Visit(DistinctOp op, Node n)
		{
			this.VisitRelOpDefault(op, n);
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x0003D886 File Offset: 0x0003BA86
		public virtual void Visit(SingleRowOp op, Node n)
		{
			this.VisitRelOpDefault(op, n);
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x0003D886 File Offset: 0x0003BA86
		public virtual void Visit(SingleRowTableOp op, Node n)
		{
			this.VisitRelOpDefault(op, n);
		}

		// Token: 0x06000E10 RID: 3600 RVA: 0x0003D890 File Offset: 0x0003BA90
		protected virtual void VisitPhysicalOpDefault(PhysicalOp op, Node n)
		{
			this.VisitDefault(n);
		}

		// Token: 0x06000E11 RID: 3601 RVA: 0x0003D8E9 File Offset: 0x0003BAE9
		public virtual void Visit(PhysicalProjectOp op, Node n)
		{
			this.VisitPhysicalOpDefault(op, n);
		}

		// Token: 0x06000E12 RID: 3602 RVA: 0x0003D8E9 File Offset: 0x0003BAE9
		protected virtual void VisitNestOp(NestBaseOp op, Node n)
		{
			this.VisitPhysicalOpDefault(op, n);
		}

		// Token: 0x06000E13 RID: 3603 RVA: 0x0003D8F3 File Offset: 0x0003BAF3
		public virtual void Visit(SingleStreamNestOp op, Node n)
		{
			this.VisitNestOp(op, n);
		}

		// Token: 0x06000E14 RID: 3604 RVA: 0x0003D8F3 File Offset: 0x0003BAF3
		public virtual void Visit(MultiStreamNestOp op, Node n)
		{
			this.VisitNestOp(op, n);
		}
	}
}
