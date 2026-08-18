using System;
using System.Diagnostics;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x02000097 RID: 151
	internal sealed class VarDefOp : AncillaryOp
	{
		// Token: 0x060009F3 RID: 2547 RVA: 0x00035D42 File Offset: 0x00033F42
		internal VarDefOp(Var v) : this()
		{
			this.m_var = v;
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x00035D51 File Offset: 0x00033F51
		private VarDefOp() : base(OpType.VarDef)
		{
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060009F5 RID: 2549 RVA: 0x00017938 File Offset: 0x00015B38
		internal override int Arity
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060009F6 RID: 2550 RVA: 0x00035D5B File Offset: 0x00033F5B
		internal Var Var
		{
			get
			{
				return this.m_var;
			}
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x00035D63 File Offset: 0x00033F63
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x00035D6D File Offset: 0x00033F6D
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x040008AC RID: 2220
		private Var m_var;

		// Token: 0x040008AD RID: 2221
		internal static readonly VarDefOp Pattern = new VarDefOp();
	}
}
