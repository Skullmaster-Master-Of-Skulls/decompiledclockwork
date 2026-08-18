using System;
using System.Data.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x0200010A RID: 266
	internal sealed class DerefOp : ScalarOp
	{
		// Token: 0x06000D8D RID: 3469 RVA: 0x0003D038 File Offset: 0x0003B238
		internal DerefOp(TypeUsage type) : base(OpType.Deref, type)
		{
		}

		// Token: 0x06000D8E RID: 3470 RVA: 0x0003D043 File Offset: 0x0003B243
		private DerefOp() : base(OpType.Deref)
		{
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000D8F RID: 3471 RVA: 0x00017938 File Offset: 0x00015B38
		internal override int Arity
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06000D90 RID: 3472 RVA: 0x0003D04D File Offset: 0x0003B24D
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x0003D057 File Offset: 0x0003B257
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x040009C9 RID: 2505
		internal static readonly DerefOp Pattern = new DerefOp();
	}
}
