using System;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x02000647 RID: 1607
	internal sealed class VarRefOp : ScalarOp
	{
		// Token: 0x06003EF4 RID: 16116 RVA: 0x00120486 File Offset: 0x0011E686
		internal VarRefOp(Var v) : base(OpType.VarRef, v.Type)
		{
			this.m_var = v;
		}

		// Token: 0x06003EF5 RID: 16117 RVA: 0x0012049C File Offset: 0x0011E69C
		private VarRefOp() : base(OpType.VarRef)
		{
		}

		// Token: 0x170009B9 RID: 2489
		// (get) Token: 0x06003EF6 RID: 16118 RVA: 0x001204A5 File Offset: 0x0011E6A5
		internal override int Arity
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06003EF7 RID: 16119 RVA: 0x001204A8 File Offset: 0x0011E6A8
		internal override bool IsEquivalent(Op other)
		{
			VarRefOp varRefOp = other as VarRefOp;
			return varRefOp != null && varRefOp.Var.Equals(this.Var);
		}

		// Token: 0x170009BA RID: 2490
		// (get) Token: 0x06003EF8 RID: 16120 RVA: 0x001204D2 File Offset: 0x0011E6D2
		internal Var Var
		{
			get
			{
				return this.m_var;
			}
		}

		// Token: 0x06003EF9 RID: 16121 RVA: 0x001204DA File Offset: 0x0011E6DA
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06003EFA RID: 16122 RVA: 0x001204E4 File Offset: 0x0011E6E4
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04001786 RID: 6022
		private readonly Var m_var;

		// Token: 0x04001787 RID: 6023
		internal static readonly VarRefOp Pattern = new VarRefOp();
	}
}
