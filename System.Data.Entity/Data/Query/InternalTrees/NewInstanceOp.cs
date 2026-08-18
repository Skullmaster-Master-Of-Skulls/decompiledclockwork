using System;
using System.Data.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x02000100 RID: 256
	internal sealed class NewInstanceOp : ScalarOp
	{
		// Token: 0x06000D53 RID: 3411 RVA: 0x0003CDB7 File Offset: 0x0003AFB7
		internal NewInstanceOp(TypeUsage type) : base(OpType.NewInstance, type)
		{
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x0003CDC2 File Offset: 0x0003AFC2
		private NewInstanceOp() : base(OpType.NewInstance)
		{
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x0003CDCC File Offset: 0x0003AFCC
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x0003CDD6 File Offset: 0x0003AFD6
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x040009BD RID: 2493
		internal static readonly NewInstanceOp Pattern = new NewInstanceOp();
	}
}
