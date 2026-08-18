using System;
using System.Data.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000F3 RID: 243
	internal sealed class TreatOp : ScalarOp
	{
		// Token: 0x06000D03 RID: 3331 RVA: 0x0003CA92 File Offset: 0x0003AC92
		internal TreatOp(TypeUsage type, bool isFake) : base(OpType.Treat, type)
		{
			this.m_isFake = isFake;
		}

		// Token: 0x06000D04 RID: 3332 RVA: 0x0003CAA4 File Offset: 0x0003ACA4
		private TreatOp() : base(OpType.Treat)
		{
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000D05 RID: 3333 RVA: 0x00017938 File Offset: 0x00015B38
		internal override int Arity
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000D06 RID: 3334 RVA: 0x0003CAAE File Offset: 0x0003ACAE
		internal bool IsFakeTreat
		{
			get
			{
				return this.m_isFake;
			}
		}

		// Token: 0x06000D07 RID: 3335 RVA: 0x0003CAB6 File Offset: 0x0003ACB6
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x0003CAC0 File Offset: 0x0003ACC0
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x040009A5 RID: 2469
		private bool m_isFake;

		// Token: 0x040009A6 RID: 2470
		internal static readonly TreatOp Pattern = new TreatOp();
	}
}
