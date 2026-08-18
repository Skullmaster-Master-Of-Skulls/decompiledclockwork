using System;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x02000630 RID: 1584
	internal sealed class UnnestOp : RelOp
	{
		// Token: 0x06003D9D RID: 15773 RVA: 0x0011B655 File Offset: 0x00119855
		internal UnnestOp(Var v, Table t) : this()
		{
			this.m_var = v;
			this.m_table = t;
		}

		// Token: 0x06003D9E RID: 15774 RVA: 0x0011B66B File Offset: 0x0011986B
		private UnnestOp() : base(OpType.Unnest)
		{
		}

		// Token: 0x1700098E RID: 2446
		// (get) Token: 0x06003D9F RID: 15775 RVA: 0x0011B675 File Offset: 0x00119875
		internal Var Var
		{
			get
			{
				return this.m_var;
			}
		}

		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x06003DA0 RID: 15776 RVA: 0x0011B67D File Offset: 0x0011987D
		internal Table Table
		{
			get
			{
				return this.m_table;
			}
		}

		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x06003DA1 RID: 15777 RVA: 0x0011B685 File Offset: 0x00119885
		internal override int Arity
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06003DA2 RID: 15778 RVA: 0x0011B688 File Offset: 0x00119888
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06003DA3 RID: 15779 RVA: 0x0011B692 File Offset: 0x00119892
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04001747 RID: 5959
		private readonly Table m_table;

		// Token: 0x04001748 RID: 5960
		private readonly Var m_var;

		// Token: 0x04001749 RID: 5961
		internal static readonly UnnestOp Pattern = new UnnestOp();
	}
}
