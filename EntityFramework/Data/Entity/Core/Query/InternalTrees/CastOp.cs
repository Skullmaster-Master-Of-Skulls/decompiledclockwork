using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020005CF RID: 1487
	internal sealed class CastOp : ScalarOp
	{
		// Token: 0x06003B8F RID: 15247 RVA: 0x001183A3 File Offset: 0x001165A3
		internal CastOp(TypeUsage type) : base(OpType.Cast, type)
		{
		}

		// Token: 0x06003B90 RID: 15248 RVA: 0x001183AE File Offset: 0x001165AE
		private CastOp() : base(OpType.Cast)
		{
		}

		// Token: 0x1700090C RID: 2316
		// (get) Token: 0x06003B91 RID: 15249 RVA: 0x001183B8 File Offset: 0x001165B8
		internal override int Arity
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06003B92 RID: 15250 RVA: 0x001183BB File Offset: 0x001165BB
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06003B93 RID: 15251 RVA: 0x001183C5 File Offset: 0x001165C5
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04001656 RID: 5718
		internal static readonly CastOp Pattern = new CastOp();
	}
}
