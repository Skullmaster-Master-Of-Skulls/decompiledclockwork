using System;
using System.Data.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x02000109 RID: 265
	internal sealed class GetEntityRefOp : ScalarOp
	{
		// Token: 0x06000D87 RID: 3463 RVA: 0x0003D003 File Offset: 0x0003B203
		internal GetEntityRefOp(TypeUsage type) : base(OpType.GetEntityRef, type)
		{
		}

		// Token: 0x06000D88 RID: 3464 RVA: 0x0003D00E File Offset: 0x0003B20E
		private GetEntityRefOp() : base(OpType.GetEntityRef)
		{
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000D89 RID: 3465 RVA: 0x00017938 File Offset: 0x00015B38
		internal override int Arity
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x0003D018 File Offset: 0x0003B218
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06000D8B RID: 3467 RVA: 0x0003D022 File Offset: 0x0003B222
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x040009C8 RID: 2504
		internal static readonly GetEntityRefOp Pattern = new GetEntityRefOp();
	}
}
