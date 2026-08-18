using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x0200062E RID: 1582
	internal sealed class TreatOp : ScalarOp
	{
		// Token: 0x06003D90 RID: 15760 RVA: 0x0011B5C7 File Offset: 0x001197C7
		internal TreatOp(TypeUsage type, bool isFake) : base(OpType.Treat, type)
		{
			this.m_isFake = isFake;
		}

		// Token: 0x06003D91 RID: 15761 RVA: 0x0011B5D9 File Offset: 0x001197D9
		private TreatOp() : base(OpType.Treat)
		{
		}

		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x06003D92 RID: 15762 RVA: 0x0011B5E3 File Offset: 0x001197E3
		internal override int Arity
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x06003D93 RID: 15763 RVA: 0x0011B5E6 File Offset: 0x001197E6
		internal bool IsFakeTreat
		{
			get
			{
				return this.m_isFake;
			}
		}

		// Token: 0x06003D94 RID: 15764 RVA: 0x0011B5EE File Offset: 0x001197EE
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06003D95 RID: 15765 RVA: 0x0011B5F8 File Offset: 0x001197F8
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04001743 RID: 5955
		private readonly bool m_isFake;

		// Token: 0x04001744 RID: 5956
		internal static readonly TreatOp Pattern = new TreatOp();
	}
}
