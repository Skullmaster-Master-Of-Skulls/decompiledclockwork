using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020005F5 RID: 1525
	internal sealed class FunctionOp : ScalarOp
	{
		// Token: 0x06003C5B RID: 15451 RVA: 0x0011908B File Offset: 0x0011728B
		internal FunctionOp(EdmFunction function) : base(OpType.Function, function.ReturnParameter.TypeUsage)
		{
			this.m_function = function;
		}

		// Token: 0x06003C5C RID: 15452 RVA: 0x001190A7 File Offset: 0x001172A7
		private FunctionOp() : base(OpType.Function)
		{
		}

		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x06003C5D RID: 15453 RVA: 0x001190B1 File Offset: 0x001172B1
		internal EdmFunction Function
		{
			get
			{
				return this.m_function;
			}
		}

		// Token: 0x06003C5E RID: 15454 RVA: 0x001190BC File Offset: 0x001172BC
		internal override bool IsEquivalent(Op other)
		{
			FunctionOp functionOp = other as FunctionOp;
			return functionOp != null && functionOp.Function.EdmEquals(this.Function);
		}

		// Token: 0x06003C5F RID: 15455 RVA: 0x001190E6 File Offset: 0x001172E6
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06003C60 RID: 15456 RVA: 0x001190F0 File Offset: 0x001172F0
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x0400169F RID: 5791
		private readonly EdmFunction m_function;

		// Token: 0x040016A0 RID: 5792
		internal static readonly FunctionOp Pattern = new FunctionOp();
	}
}
