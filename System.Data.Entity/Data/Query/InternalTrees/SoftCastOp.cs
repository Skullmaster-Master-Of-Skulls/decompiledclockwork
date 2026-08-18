using System;
using System.Data.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000F6 RID: 246
	internal sealed class SoftCastOp : ScalarOp
	{
		// Token: 0x06000D18 RID: 3352 RVA: 0x0003CB5E File Offset: 0x0003AD5E
		internal SoftCastOp(TypeUsage type) : base(OpType.SoftCast, type)
		{
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x0003CB69 File Offset: 0x0003AD69
		private SoftCastOp() : base(OpType.SoftCast)
		{
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000D1A RID: 3354 RVA: 0x00017938 File Offset: 0x00015B38
		internal override int Arity
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x0003CB73 File Offset: 0x0003AD73
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x0003CB7D File Offset: 0x0003AD7D
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x040009AB RID: 2475
		internal static readonly SoftCastOp Pattern = new SoftCastOp();
	}
}
