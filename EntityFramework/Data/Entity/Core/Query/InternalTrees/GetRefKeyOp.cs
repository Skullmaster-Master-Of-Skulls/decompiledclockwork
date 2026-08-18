using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020005F7 RID: 1527
	internal sealed class GetRefKeyOp : ScalarOp
	{
		// Token: 0x06003C68 RID: 15464 RVA: 0x0011913E File Offset: 0x0011733E
		internal GetRefKeyOp(TypeUsage type) : base(OpType.GetRefKey, type)
		{
		}

		// Token: 0x06003C69 RID: 15465 RVA: 0x00119149 File Offset: 0x00117349
		private GetRefKeyOp() : base(OpType.GetRefKey)
		{
		}

		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x06003C6A RID: 15466 RVA: 0x00119153 File Offset: 0x00117353
		internal override int Arity
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06003C6B RID: 15467 RVA: 0x00119156 File Offset: 0x00117356
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06003C6C RID: 15468 RVA: 0x00119160 File Offset: 0x00117360
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x040016A2 RID: 5794
		internal static readonly GetRefKeyOp Pattern = new GetRefKeyOp();
	}
}
