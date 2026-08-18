using System;
using System.Diagnostics;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000F0 RID: 240
	internal sealed class VarRefOp : ScalarOp
	{
		// Token: 0x06000CED RID: 3309 RVA: 0x0003C962 File Offset: 0x0003AB62
		internal VarRefOp(Var v) : base(OpType.VarRef, v.Type)
		{
			this.m_var = v;
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x0003C978 File Offset: 0x0003AB78
		private VarRefOp() : base(OpType.VarRef)
		{
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000CEF RID: 3311 RVA: 0x000173E2 File Offset: 0x000155E2
		internal override int Arity
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06000CF0 RID: 3312 RVA: 0x0003C984 File Offset: 0x0003AB84
		internal override bool IsEquivalent(Op other)
		{
			VarRefOp varRefOp = other as VarRefOp;
			return varRefOp != null && varRefOp.Var.Equals(this.Var);
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000CF1 RID: 3313 RVA: 0x0003C9AE File Offset: 0x0003ABAE
		internal Var Var
		{
			get
			{
				return this.m_var;
			}
		}

		// Token: 0x06000CF2 RID: 3314 RVA: 0x0003C9B6 File Offset: 0x0003ABB6
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06000CF3 RID: 3315 RVA: 0x0003C9C0 File Offset: 0x0003ABC0
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x0400099F RID: 2463
		private Var m_var;

		// Token: 0x040009A0 RID: 2464
		internal static readonly VarRefOp Pattern = new VarRefOp();
	}
}
