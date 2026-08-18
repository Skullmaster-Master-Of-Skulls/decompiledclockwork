using System;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x02000631 RID: 1585
	internal sealed class VarDefListOp : AncillaryOp
	{
		// Token: 0x06003DA5 RID: 15781 RVA: 0x0011B6A8 File Offset: 0x001198A8
		private VarDefListOp() : base(OpType.VarDefList)
		{
		}

		// Token: 0x06003DA6 RID: 15782 RVA: 0x0011B6B2 File Offset: 0x001198B2
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06003DA7 RID: 15783 RVA: 0x0011B6BC File Offset: 0x001198BC
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x0400174A RID: 5962
		internal static readonly VarDefListOp Instance = new VarDefListOp();

		// Token: 0x0400174B RID: 5963
		internal static readonly VarDefListOp Pattern = VarDefListOp.Instance;
	}
}
