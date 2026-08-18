using System;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x02000644 RID: 1604
	internal sealed class VarDefOp : AncillaryOp
	{
		// Token: 0x06003EE4 RID: 16100 RVA: 0x00120319 File Offset: 0x0011E519
		internal VarDefOp(Var v) : this()
		{
			this.m_var = v;
		}

		// Token: 0x06003EE5 RID: 16101 RVA: 0x00120328 File Offset: 0x0011E528
		private VarDefOp() : base(OpType.VarDef)
		{
		}

		// Token: 0x170009B6 RID: 2486
		// (get) Token: 0x06003EE6 RID: 16102 RVA: 0x00120332 File Offset: 0x0011E532
		internal override int Arity
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170009B7 RID: 2487
		// (get) Token: 0x06003EE7 RID: 16103 RVA: 0x00120335 File Offset: 0x0011E535
		internal Var Var
		{
			get
			{
				return this.m_var;
			}
		}

		// Token: 0x06003EE8 RID: 16104 RVA: 0x0012033D File Offset: 0x0011E53D
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06003EE9 RID: 16105 RVA: 0x00120347 File Offset: 0x0011E547
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04001783 RID: 6019
		private readonly Var m_var;

		// Token: 0x04001784 RID: 6020
		internal static readonly VarDefOp Pattern = new VarDefOp();
	}
}
