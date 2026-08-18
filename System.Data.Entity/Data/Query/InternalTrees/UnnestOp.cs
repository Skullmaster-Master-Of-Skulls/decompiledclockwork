using System;
using System.Diagnostics;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000C9 RID: 201
	internal sealed class UnnestOp : RelOp
	{
		// Token: 0x06000C2E RID: 3118 RVA: 0x0003BF3C File Offset: 0x0003A13C
		internal UnnestOp(Var v, Table t) : this()
		{
			this.m_var = v;
			this.m_table = t;
		}

		// Token: 0x06000C2F RID: 3119 RVA: 0x0003BF52 File Offset: 0x0003A152
		private UnnestOp() : base(OpType.Unnest)
		{
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000C30 RID: 3120 RVA: 0x0003BF5C File Offset: 0x0003A15C
		internal Var Var
		{
			get
			{
				return this.m_var;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000C31 RID: 3121 RVA: 0x0003BF64 File Offset: 0x0003A164
		internal Table Table
		{
			get
			{
				return this.m_table;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000C32 RID: 3122 RVA: 0x00017938 File Offset: 0x00015B38
		internal override int Arity
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x0003BF6C File Offset: 0x0003A16C
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06000C34 RID: 3124 RVA: 0x0003BF76 File Offset: 0x0003A176
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04000962 RID: 2402
		private Table m_table;

		// Token: 0x04000963 RID: 2403
		private Var m_var;

		// Token: 0x04000964 RID: 2404
		internal static readonly UnnestOp Pattern = new UnnestOp();
	}
}
