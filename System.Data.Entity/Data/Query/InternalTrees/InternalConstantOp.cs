using System;
using System.Data.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000ED RID: 237
	internal sealed class InternalConstantOp : ConstantBaseOp
	{
		// Token: 0x06000CDB RID: 3291 RVA: 0x0003C8A1 File Offset: 0x0003AAA1
		internal InternalConstantOp(TypeUsage type, object value) : base(OpType.InternalConstant, type, value)
		{
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x0003C8AC File Offset: 0x0003AAAC
		private InternalConstantOp() : base(OpType.InternalConstant)
		{
		}

		// Token: 0x06000CDD RID: 3293 RVA: 0x0003C8B5 File Offset: 0x0003AAB5
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06000CDE RID: 3294 RVA: 0x0003C8BF File Offset: 0x0003AABF
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x0400099C RID: 2460
		internal static readonly InternalConstantOp Pattern = new InternalConstantOp();
	}
}
