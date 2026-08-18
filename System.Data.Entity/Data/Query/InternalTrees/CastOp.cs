using System;
using System.Data.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000F5 RID: 245
	internal sealed class CastOp : ScalarOp
	{
		// Token: 0x06000D12 RID: 3346 RVA: 0x0003CB29 File Offset: 0x0003AD29
		internal CastOp(TypeUsage type) : base(OpType.Cast, type)
		{
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x0003CB34 File Offset: 0x0003AD34
		private CastOp() : base(OpType.Cast)
		{
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000D14 RID: 3348 RVA: 0x00017938 File Offset: 0x00015B38
		internal override int Arity
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x0003CB3E File Offset: 0x0003AD3E
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x0003CB48 File Offset: 0x0003AD48
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x040009AA RID: 2474
		internal static readonly CastOp Pattern = new CastOp();
	}
}
