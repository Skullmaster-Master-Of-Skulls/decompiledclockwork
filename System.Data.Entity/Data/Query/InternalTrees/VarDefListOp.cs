using System;
using System.Diagnostics;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x02000098 RID: 152
	internal sealed class VarDefListOp : AncillaryOp
	{
		// Token: 0x060009FA RID: 2554 RVA: 0x00035D83 File Offset: 0x00033F83
		private VarDefListOp() : base(OpType.VarDefList)
		{
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x00035D8D File Offset: 0x00033F8D
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x00035D97 File Offset: 0x00033F97
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x040008AE RID: 2222
		internal static readonly VarDefListOp Instance = new VarDefListOp();

		// Token: 0x040008AF RID: 2223
		internal static readonly VarDefListOp Pattern = VarDefListOp.Instance;
	}
}
