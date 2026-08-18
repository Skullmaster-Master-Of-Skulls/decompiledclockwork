using System;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020005CD RID: 1485
	internal abstract class BasicOpVisitor
	{
		// Token: 0x06003B3E RID: 15166 RVA: 0x00117FF4 File Offset: 0x001161F4
		protected virtual void VisitChildren(Node n)
		{
			foreach (Node n2 in n.Children)
			{
				this.VisitNode(n2);
			}
		}

		// Token: 0x06003B3F RID: 15167 RVA: 0x00118048 File Offset: 0x00116248
		protected virtual void VisitChildrenReverse(Node n)
		{
			for (int i = n.Children.Count - 1; i >= 0; i--)
			{
				this.VisitNode(n.Children[i]);
			}
		}

		// Token: 0x06003B40 RID: 15168 RVA: 0x0011807F File Offset: 0x0011627F
		internal virtual void VisitNode(Node n)
		{
			n.Op.Accept(this, n);
		}

		// Token: 0x06003B41 RID: 15169 RVA: 0x0011808E File Offset: 0x0011628E
		protected virtual void VisitDefault(Node n)
		{
			this.VisitChildren(n);
		}

		// Token: 0x06003B42 RID: 15170 RVA: 0x00118097 File Offset: 0x00116297
		protected virtual void VisitConstantOp(ConstantBaseOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06003B43 RID: 15171 RVA: 0x001180A1 File Offset: 0x001162A1
		protected virtual void VisitTableOp(ScanTableBaseOp op, Node n)
		{
			this.VisitRelOpDefault(op, n);
		}

		// Token: 0x06003B44 RID: 15172 RVA: 0x001180AB File Offset: 0x001162AB
		protected virtual void VisitJoinOp(JoinBaseOp op, Node n)
		{
			this.VisitRelOpDefault(op, n);
		}

		// Token: 0x06003B45 RID: 15173 RVA: 0x001180B5 File Offset: 0x001162B5
		protected virtual void VisitApplyOp(ApplyBaseOp op, Node n)
		{
			this.VisitRelOpDefault(op, n);
		}

		// Token: 0x06003B46 RID: 15174 RVA: 0x001180BF File Offset: 0x001162BF
		protected virtual void VisitSetOp(SetOp op, Node n)
		{
			this.VisitRelOpDefault(op, n);
		}

		// Token: 0x06003B47 RID: 15175 RVA: 0x001180C9 File Offset: 0x001162C9
		protected virtual void VisitSortOp(SortBaseOp op, Node n)
		{
			this.VisitRelOpDefault(op, n);
		}

		// Token: 0x06003B48 RID: 15176 RVA: 0x001180D3 File Offset: 0x001162D3
		protected virtual void VisitGroupByOp(GroupByBaseOp op, Node n)
		{
			this.VisitRelOpDefault(op, n);
		}

		// Token: 0x06003B49 RID: 15177 RVA: 0x001180DD File Offset: 0x001162DD
		public virtual void Visit(Op op, Node n)
		{
			throw new NotSupportedException(Strings.Iqt_General_UnsupportedOp(op.GetType().FullName));
		}

		// Token: 0x06003B4A RID: 15178 RVA: 0x001180F4 File Offset: 0x001162F4
		protected virtual void VisitScalarOpDefault(ScalarOp op, Node n)
		{
			this.VisitDefault(n);
		}

		// Token: 0x06003B4B RID: 15179 RVA: 0x001180FD File Offset: 0x001162FD
		public virtual void Visit(ConstantOp op, Node n)
		{
			this.VisitConstantOp(op, n);
		}

		// Token: 0x06003B4C RID: 15180 RVA: 0x00118107 File Offset: 0x00116307
		public virtual void Visit(NullOp op, Node n)
		{
			this.VisitConstantOp(op, n);
		}

		// Token: 0x06003B4D RID: 15181 RVA: 0x00118111 File Offset: 0x00116311
		public virtual void Visit(NullSentinelOp op, Node n)
		{
			this.VisitConstantOp(op, n);
		}

		// Token: 0x06003B4E RID: 15182 RVA: 0x0011811B File Offset: 0x0011631B
		public virtual void Visit(InternalConstantOp op, Node n)
		{
			this.VisitConstantOp(op, n);
		}

		// Token: 0x06003B4F RID: 15183 RVA: 0x00118125 File Offset: 0x00116325
		public virtual void Visit(ConstantPredicateOp op, Node n)
		{
			this.VisitConstantOp(op, n);
		}

		// Token: 0x06003B50 RID: 15184 RVA: 0x0011812F File Offset: 0x0011632F
		public virtual void Visit(FunctionOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06003B51 RID: 15185 RVA: 0x00118139 File Offset: 0x00116339
		public virtual void Visit(PropertyOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06003B52 RID: 15186 RVA: 0x00118143 File Offset: 0x00116343
		public virtual void Visit(RelPropertyOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06003B53 RID: 15187 RVA: 0x0011814D File Offset: 0x0011634D
		public virtual void Visit(CaseOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06003B54 RID: 15188 RVA: 0x00118157 File Offset: 0x00116357
		public virtual void Visit(ComparisonOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06003B55 RID: 15189 RVA: 0x00118161 File Offset: 0x00116361
		public virtual void Visit(LikeOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06003B56 RID: 15190 RVA: 0x0011816B File Offset: 0x0011636B
		public virtual void Visit(AggregateOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06003B57 RID: 15191 RVA: 0x00118175 File Offset: 0x00116375
		public virtual void Visit(NewInstanceOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06003B58 RID: 15192 RVA: 0x0011817F File Offset: 0x0011637F
		public virtual void Visit(NewEntityOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06003B59 RID: 15193 RVA: 0x00118189 File Offset: 0x00116389
		public virtual void Visit(DiscriminatedNewEntityOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06003B5A RID: 15194 RVA: 0x00118193 File Offset: 0x00116393
		public virtual void Visit(NewMultisetOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06003B5B RID: 15195 RVA: 0x0011819D File Offset: 0x0011639D
		public virtual void Visit(NewRecordOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06003B5C RID: 15196 RVA: 0x001181A7 File Offset: 0x001163A7
		public virtual void Visit(RefOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06003B5D RID: 15197 RVA: 0x001181B1 File Offset: 0x001163B1
		public virtual void Visit(VarRefOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06003B5E RID: 15198 RVA: 0x001181BB File Offset: 0x001163BB
		public virtual void Visit(ConditionalOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06003B5F RID: 15199 RVA: 0x001181C5 File Offset: 0x001163C5
		public virtual void Visit(ArithmeticOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06003B60 RID: 15200 RVA: 0x001181CF File Offset: 0x001163CF
		public virtual void Visit(TreatOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06003B61 RID: 15201 RVA: 0x001181D9 File Offset: 0x001163D9
		public virtual void Visit(CastOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06003B62 RID: 15202 RVA: 0x001181E3 File Offset: 0x001163E3
		public virtual void Visit(SoftCastOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06003B63 RID: 15203 RVA: 0x001181ED File Offset: 0x001163ED
		public virtual void Visit(IsOfOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06003B64 RID: 15204 RVA: 0x001181F7 File Offset: 0x001163F7
		public virtual void Visit(ExistsOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06003B65 RID: 15205 RVA: 0x00118201 File Offset: 0x00116401
		public virtual void Visit(ElementOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06003B66 RID: 15206 RVA: 0x0011820B File Offset: 0x0011640B
		public virtual void Visit(GetEntityRefOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06003B67 RID: 15207 RVA: 0x00118215 File Offset: 0x00116415
		public virtual void Visit(GetRefKeyOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06003B68 RID: 15208 RVA: 0x0011821F File Offset: 0x0011641F
		public virtual void Visit(CollectOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06003B69 RID: 15209 RVA: 0x00118229 File Offset: 0x00116429
		public virtual void Visit(DerefOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06003B6A RID: 15210 RVA: 0x00118233 File Offset: 0x00116433
		public virtual void Visit(NavigateOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06003B6B RID: 15211 RVA: 0x0011823D File Offset: 0x0011643D
		protected virtual void VisitAncillaryOpDefault(AncillaryOp op, Node n)
		{
			this.VisitDefault(n);
		}

		// Token: 0x06003B6C RID: 15212 RVA: 0x00118246 File Offset: 0x00116446
		public virtual void Visit(VarDefOp op, Node n)
		{
			this.VisitAncillaryOpDefault(op, n);
		}

		// Token: 0x06003B6D RID: 15213 RVA: 0x00118250 File Offset: 0x00116450
		public virtual void Visit(VarDefListOp op, Node n)
		{
			this.VisitAncillaryOpDefault(op, n);
		}

		// Token: 0x06003B6E RID: 15214 RVA: 0x0011825A File Offset: 0x0011645A
		protected virtual void VisitRelOpDefault(RelOp op, Node n)
		{
			this.VisitDefault(n);
		}

		// Token: 0x06003B6F RID: 15215 RVA: 0x00118263 File Offset: 0x00116463
		public virtual void Visit(ScanTableOp op, Node n)
		{
			this.VisitTableOp(op, n);
		}

		// Token: 0x06003B70 RID: 15216 RVA: 0x0011826D File Offset: 0x0011646D
		public virtual void Visit(ScanViewOp op, Node n)
		{
			this.VisitTableOp(op, n);
		}

		// Token: 0x06003B71 RID: 15217 RVA: 0x00118277 File Offset: 0x00116477
		public virtual void Visit(UnnestOp op, Node n)
		{
			this.VisitRelOpDefault(op, n);
		}

		// Token: 0x06003B72 RID: 15218 RVA: 0x00118281 File Offset: 0x00116481
		public virtual void Visit(ProjectOp op, Node n)
		{
			this.VisitRelOpDefault(op, n);
		}

		// Token: 0x06003B73 RID: 15219 RVA: 0x0011828B File Offset: 0x0011648B
		public virtual void Visit(FilterOp op, Node n)
		{
			this.VisitRelOpDefault(op, n);
		}

		// Token: 0x06003B74 RID: 15220 RVA: 0x00118295 File Offset: 0x00116495
		public virtual void Visit(SortOp op, Node n)
		{
			this.VisitSortOp(op, n);
		}

		// Token: 0x06003B75 RID: 15221 RVA: 0x0011829F File Offset: 0x0011649F
		public virtual void Visit(ConstrainedSortOp op, Node n)
		{
			this.VisitSortOp(op, n);
		}

		// Token: 0x06003B76 RID: 15222 RVA: 0x001182A9 File Offset: 0x001164A9
		public virtual void Visit(GroupByOp op, Node n)
		{
			this.VisitGroupByOp(op, n);
		}

		// Token: 0x06003B77 RID: 15223 RVA: 0x001182B3 File Offset: 0x001164B3
		public virtual void Visit(GroupByIntoOp op, Node n)
		{
			this.VisitGroupByOp(op, n);
		}

		// Token: 0x06003B78 RID: 15224 RVA: 0x001182BD File Offset: 0x001164BD
		public virtual void Visit(CrossJoinOp op, Node n)
		{
			this.VisitJoinOp(op, n);
		}

		// Token: 0x06003B79 RID: 15225 RVA: 0x001182C7 File Offset: 0x001164C7
		public virtual void Visit(InnerJoinOp op, Node n)
		{
			this.VisitJoinOp(op, n);
		}

		// Token: 0x06003B7A RID: 15226 RVA: 0x001182D1 File Offset: 0x001164D1
		public virtual void Visit(LeftOuterJoinOp op, Node n)
		{
			this.VisitJoinOp(op, n);
		}

		// Token: 0x06003B7B RID: 15227 RVA: 0x001182DB File Offset: 0x001164DB
		public virtual void Visit(FullOuterJoinOp op, Node n)
		{
			this.VisitJoinOp(op, n);
		}

		// Token: 0x06003B7C RID: 15228 RVA: 0x001182E5 File Offset: 0x001164E5
		public virtual void Visit(CrossApplyOp op, Node n)
		{
			this.VisitApplyOp(op, n);
		}

		// Token: 0x06003B7D RID: 15229 RVA: 0x001182EF File Offset: 0x001164EF
		public virtual void Visit(OuterApplyOp op, Node n)
		{
			this.VisitApplyOp(op, n);
		}

		// Token: 0x06003B7E RID: 15230 RVA: 0x001182F9 File Offset: 0x001164F9
		public virtual void Visit(UnionAllOp op, Node n)
		{
			this.VisitSetOp(op, n);
		}

		// Token: 0x06003B7F RID: 15231 RVA: 0x00118303 File Offset: 0x00116503
		public virtual void Visit(IntersectOp op, Node n)
		{
			this.VisitSetOp(op, n);
		}

		// Token: 0x06003B80 RID: 15232 RVA: 0x0011830D File Offset: 0x0011650D
		public virtual void Visit(ExceptOp op, Node n)
		{
			this.VisitSetOp(op, n);
		}

		// Token: 0x06003B81 RID: 15233 RVA: 0x00118317 File Offset: 0x00116517
		public virtual void Visit(DistinctOp op, Node n)
		{
			this.VisitRelOpDefault(op, n);
		}

		// Token: 0x06003B82 RID: 15234 RVA: 0x00118321 File Offset: 0x00116521
		public virtual void Visit(SingleRowOp op, Node n)
		{
			this.VisitRelOpDefault(op, n);
		}

		// Token: 0x06003B83 RID: 15235 RVA: 0x0011832B File Offset: 0x0011652B
		public virtual void Visit(SingleRowTableOp op, Node n)
		{
			this.VisitRelOpDefault(op, n);
		}

		// Token: 0x06003B84 RID: 15236 RVA: 0x00118335 File Offset: 0x00116535
		protected virtual void VisitPhysicalOpDefault(PhysicalOp op, Node n)
		{
			this.VisitDefault(n);
		}

		// Token: 0x06003B85 RID: 15237 RVA: 0x0011833E File Offset: 0x0011653E
		public virtual void Visit(PhysicalProjectOp op, Node n)
		{
			this.VisitPhysicalOpDefault(op, n);
		}

		// Token: 0x06003B86 RID: 15238 RVA: 0x00118348 File Offset: 0x00116548
		protected virtual void VisitNestOp(NestBaseOp op, Node n)
		{
			this.VisitPhysicalOpDefault(op, n);
		}

		// Token: 0x06003B87 RID: 15239 RVA: 0x00118352 File Offset: 0x00116552
		public virtual void Visit(SingleStreamNestOp op, Node n)
		{
			this.VisitNestOp(op, n);
		}

		// Token: 0x06003B88 RID: 15240 RVA: 0x0011835C File Offset: 0x0011655C
		public virtual void Visit(MultiStreamNestOp op, Node n)
		{
			this.VisitNestOp(op, n);
		}
	}
}
