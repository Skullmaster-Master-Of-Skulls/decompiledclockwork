using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020005D3 RID: 1491
	internal sealed class CollectOp : ScalarOp
	{
		// Token: 0x06003BA8 RID: 15272 RVA: 0x001184D3 File Offset: 0x001166D3
		internal CollectOp(TypeUsage type) : base(OpType.Collect, type)
		{
		}

		// Token: 0x06003BA9 RID: 15273 RVA: 0x001184DE File Offset: 0x001166DE
		private CollectOp() : base(OpType.Collect)
		{
		}

		// Token: 0x17000919 RID: 2329
		// (get) Token: 0x06003BAA RID: 15274 RVA: 0x001184E8 File Offset: 0x001166E8
		internal override int Arity
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06003BAB RID: 15275 RVA: 0x001184EB File Offset: 0x001166EB
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06003BAC RID: 15276 RVA: 0x001184F5 File Offset: 0x001166F5
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04001663 RID: 5731
		internal static readonly CollectOp Pattern = new CollectOp();
	}
}
