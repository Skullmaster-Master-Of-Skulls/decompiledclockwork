using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020005DF RID: 1503
	internal sealed class ConstantPredicateOp : ConstantBaseOp
	{
		// Token: 0x06003BE0 RID: 15328 RVA: 0x00118891 File Offset: 0x00116A91
		internal ConstantPredicateOp(TypeUsage type, bool value) : base(OpType.ConstantPredicate, type, value)
		{
		}

		// Token: 0x06003BE1 RID: 15329 RVA: 0x001188A1 File Offset: 0x00116AA1
		private ConstantPredicateOp() : base(OpType.ConstantPredicate)
		{
		}

		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x06003BE2 RID: 15330 RVA: 0x001188AA File Offset: 0x00116AAA
		internal new bool Value
		{
			get
			{
				return (bool)base.Value;
			}
		}

		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x06003BE3 RID: 15331 RVA: 0x001188B7 File Offset: 0x00116AB7
		internal bool IsTrue
		{
			get
			{
				return this.Value;
			}
		}

		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x06003BE4 RID: 15332 RVA: 0x001188BF File Offset: 0x00116ABF
		internal bool IsFalse
		{
			get
			{
				return !this.Value;
			}
		}

		// Token: 0x06003BE5 RID: 15333 RVA: 0x001188CA File Offset: 0x00116ACA
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06003BE6 RID: 15334 RVA: 0x001188D4 File Offset: 0x00116AD4
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04001677 RID: 5751
		internal static readonly ConstantPredicateOp Pattern = new ConstantPredicateOp();
	}
}
