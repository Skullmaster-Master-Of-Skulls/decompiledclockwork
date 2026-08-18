using System;
using System.Data.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000EF RID: 239
	internal sealed class ConstantPredicateOp : ConstantBaseOp
	{
		// Token: 0x06000CE5 RID: 3301 RVA: 0x0003C909 File Offset: 0x0003AB09
		internal ConstantPredicateOp(TypeUsage type, bool value) : base(OpType.ConstantPredicate, type, value)
		{
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x0003C919 File Offset: 0x0003AB19
		private ConstantPredicateOp() : base(OpType.ConstantPredicate)
		{
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000CE7 RID: 3303 RVA: 0x0003C922 File Offset: 0x0003AB22
		internal new bool Value
		{
			get
			{
				return (bool)base.Value;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000CE8 RID: 3304 RVA: 0x0003C92F File Offset: 0x0003AB2F
		internal bool IsTrue
		{
			get
			{
				return this.Value;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000CE9 RID: 3305 RVA: 0x0003C937 File Offset: 0x0003AB37
		internal bool IsFalse
		{
			get
			{
				return !this.Value;
			}
		}

		// Token: 0x06000CEA RID: 3306 RVA: 0x0003C942 File Offset: 0x0003AB42
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x0003C94C File Offset: 0x0003AB4C
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x0400099E RID: 2462
		internal static readonly ConstantPredicateOp Pattern = new ConstantPredicateOp();
	}
}
