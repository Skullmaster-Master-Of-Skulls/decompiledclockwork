using System;
using System.Data.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000F8 RID: 248
	internal sealed class LikeOp : ScalarOp
	{
		// Token: 0x06000D24 RID: 3364 RVA: 0x0003CBBF File Offset: 0x0003ADBF
		internal LikeOp(TypeUsage boolType) : base(OpType.Like, boolType)
		{
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x0003CBCA File Offset: 0x0003ADCA
		private LikeOp() : base(OpType.Like)
		{
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000D26 RID: 3366 RVA: 0x0003BF8C File Offset: 0x0003A18C
		internal override int Arity
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x0003CBD4 File Offset: 0x0003ADD4
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x0003CBDE File Offset: 0x0003ADDE
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x040009AD RID: 2477
		internal static readonly LikeOp Pattern = new LikeOp();
	}
}
