using System;
using System.Data.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x02000106 RID: 262
	internal sealed class ExistsOp : ScalarOp
	{
		// Token: 0x06000D75 RID: 3445 RVA: 0x0003CF64 File Offset: 0x0003B164
		internal ExistsOp(TypeUsage type) : base(OpType.Exists, type)
		{
		}

		// Token: 0x06000D76 RID: 3446 RVA: 0x0003CF6F File Offset: 0x0003B16F
		private ExistsOp() : base(OpType.Exists)
		{
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000D77 RID: 3447 RVA: 0x00017938 File Offset: 0x00015B38
		internal override int Arity
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x0003CF79 File Offset: 0x0003B179
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06000D79 RID: 3449 RVA: 0x0003CF83 File Offset: 0x0003B183
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x040009C5 RID: 2501
		internal static readonly ExistsOp Pattern = new ExistsOp();
	}
}
