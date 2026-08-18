using System;
using System.Data.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000F7 RID: 247
	internal sealed class ComparisonOp : ScalarOp
	{
		// Token: 0x06000D1E RID: 3358 RVA: 0x0003CB93 File Offset: 0x0003AD93
		internal ComparisonOp(OpType opType, TypeUsage type) : base(opType, type)
		{
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x0003C7C9 File Offset: 0x0003A9C9
		private ComparisonOp(OpType opType) : base(opType)
		{
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000D20 RID: 3360 RVA: 0x00033532 File Offset: 0x00031732
		internal override int Arity
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x0003CB9D File Offset: 0x0003AD9D
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x0003CBA7 File Offset: 0x0003ADA7
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x040009AC RID: 2476
		internal static readonly ComparisonOp PatternEq = new ComparisonOp(OpType.EQ);
	}
}
