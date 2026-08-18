using System;
using System.Diagnostics;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000DA RID: 218
	internal sealed class GroupByIntoOp : GroupByBaseOp
	{
		// Token: 0x06000C81 RID: 3201 RVA: 0x0003C27B File Offset: 0x0003A47B
		private GroupByIntoOp() : base(OpType.GroupByInto)
		{
		}

		// Token: 0x06000C82 RID: 3202 RVA: 0x0003C285 File Offset: 0x0003A485
		internal GroupByIntoOp(VarVec keys, VarVec inputs, VarVec outputs) : base(OpType.GroupByInto, keys, outputs)
		{
			this.m_inputs = inputs;
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000C83 RID: 3203 RVA: 0x0003C298 File Offset: 0x0003A498
		internal VarVec Inputs
		{
			get
			{
				return this.m_inputs;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000C84 RID: 3204 RVA: 0x0003C2A0 File Offset: 0x0003A4A0
		internal override int Arity
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x06000C85 RID: 3205 RVA: 0x0003C2A3 File Offset: 0x0003A4A3
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06000C86 RID: 3206 RVA: 0x0003C2AD File Offset: 0x0003A4AD
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x0400097F RID: 2431
		private readonly VarVec m_inputs;

		// Token: 0x04000980 RID: 2432
		internal static readonly GroupByIntoOp Pattern = new GroupByIntoOp();
	}
}
