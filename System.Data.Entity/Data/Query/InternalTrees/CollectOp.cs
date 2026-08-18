using System;
using System.Data.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000FC RID: 252
	internal sealed class CollectOp : ScalarOp
	{
		// Token: 0x06000D3C RID: 3388 RVA: 0x0003CCCC File Offset: 0x0003AECC
		internal CollectOp(TypeUsage type) : base(OpType.Collect, type)
		{
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x0003CCD7 File Offset: 0x0003AED7
		private CollectOp() : base(OpType.Collect)
		{
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000D3E RID: 3390 RVA: 0x00017938 File Offset: 0x00015B38
		internal override int Arity
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x0003CCE1 File Offset: 0x0003AEE1
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x0003CCEB File Offset: 0x0003AEEB
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x040009B6 RID: 2486
		internal static readonly CollectOp Pattern = new CollectOp();
	}
}
