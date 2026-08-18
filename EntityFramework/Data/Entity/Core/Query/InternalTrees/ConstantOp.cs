using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020005DE RID: 1502
	internal sealed class ConstantOp : ConstantBaseOp
	{
		// Token: 0x06003BDB RID: 15323 RVA: 0x0011885D File Offset: 0x00116A5D
		internal ConstantOp(TypeUsage type, object value) : base(OpType.Constant, type, value)
		{
		}

		// Token: 0x06003BDC RID: 15324 RVA: 0x00118868 File Offset: 0x00116A68
		private ConstantOp() : base(OpType.Constant)
		{
		}

		// Token: 0x06003BDD RID: 15325 RVA: 0x00118871 File Offset: 0x00116A71
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06003BDE RID: 15326 RVA: 0x0011887B File Offset: 0x00116A7B
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04001676 RID: 5750
		internal static readonly ConstantOp Pattern = new ConstantOp();
	}
}
