using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x02000629 RID: 1577
	internal sealed class SoftCastOp : ScalarOp
	{
		// Token: 0x06003D74 RID: 15732 RVA: 0x0011B336 File Offset: 0x00119536
		internal SoftCastOp(TypeUsage type) : base(OpType.SoftCast, type)
		{
		}

		// Token: 0x06003D75 RID: 15733 RVA: 0x0011B341 File Offset: 0x00119541
		private SoftCastOp() : base(OpType.SoftCast)
		{
		}

		// Token: 0x17000982 RID: 2434
		// (get) Token: 0x06003D76 RID: 15734 RVA: 0x0011B34B File Offset: 0x0011954B
		internal override int Arity
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06003D77 RID: 15735 RVA: 0x0011B34E File Offset: 0x0011954E
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06003D78 RID: 15736 RVA: 0x0011B358 File Offset: 0x00119558
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04001736 RID: 5942
		internal static readonly SoftCastOp Pattern = new SoftCastOp();
	}
}
