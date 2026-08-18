using System;
using System.Data.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x02000108 RID: 264
	internal sealed class GetRefKeyOp : ScalarOp
	{
		// Token: 0x06000D81 RID: 3457 RVA: 0x0003CFCE File Offset: 0x0003B1CE
		internal GetRefKeyOp(TypeUsage type) : base(OpType.GetRefKey, type)
		{
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x0003CFD9 File Offset: 0x0003B1D9
		private GetRefKeyOp() : base(OpType.GetRefKey)
		{
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000D83 RID: 3459 RVA: 0x00017938 File Offset: 0x00015B38
		internal override int Arity
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06000D84 RID: 3460 RVA: 0x0003CFE3 File Offset: 0x0003B1E3
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06000D85 RID: 3461 RVA: 0x0003CFED File Offset: 0x0003B1ED
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x040009C7 RID: 2503
		internal static readonly GetRefKeyOp Pattern = new GetRefKeyOp();
	}
}
