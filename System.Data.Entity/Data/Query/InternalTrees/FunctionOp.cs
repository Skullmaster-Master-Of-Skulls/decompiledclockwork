using System;
using System.Data.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000F1 RID: 241
	internal sealed class FunctionOp : ScalarOp
	{
		// Token: 0x06000CF5 RID: 3317 RVA: 0x0003C9D6 File Offset: 0x0003ABD6
		internal FunctionOp(EdmFunction function) : base(OpType.Function, function.ReturnParameter.TypeUsage)
		{
			this.m_function = function;
		}

		// Token: 0x06000CF6 RID: 3318 RVA: 0x0003C9F2 File Offset: 0x0003ABF2
		private FunctionOp() : base(OpType.Function)
		{
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000CF7 RID: 3319 RVA: 0x0003C9FC File Offset: 0x0003ABFC
		internal EdmFunction Function
		{
			get
			{
				return this.m_function;
			}
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x0003CA04 File Offset: 0x0003AC04
		internal override bool IsEquivalent(Op other)
		{
			FunctionOp functionOp = other as FunctionOp;
			return functionOp != null && functionOp.Function.EdmEquals(this.Function);
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x0003CA2E File Offset: 0x0003AC2E
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x0003CA38 File Offset: 0x0003AC38
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x040009A1 RID: 2465
		private EdmFunction m_function;

		// Token: 0x040009A2 RID: 2466
		internal static readonly FunctionOp Pattern = new FunctionOp();
	}
}
