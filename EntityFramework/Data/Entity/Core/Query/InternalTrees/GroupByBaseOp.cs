using System;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020005F8 RID: 1528
	internal abstract class GroupByBaseOp : RelOp
	{
		// Token: 0x06003C6E RID: 15470 RVA: 0x00119176 File Offset: 0x00117376
		protected GroupByBaseOp(OpType opType) : base(opType)
		{
		}

		// Token: 0x06003C6F RID: 15471 RVA: 0x0011917F File Offset: 0x0011737F
		internal GroupByBaseOp(OpType opType, VarVec keys, VarVec outputs) : this(opType)
		{
			this.m_keys = keys;
			this.m_outputs = outputs;
		}

		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x06003C70 RID: 15472 RVA: 0x00119196 File Offset: 0x00117396
		internal VarVec Keys
		{
			get
			{
				return this.m_keys;
			}
		}

		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x06003C71 RID: 15473 RVA: 0x0011919E File Offset: 0x0011739E
		internal VarVec Outputs
		{
			get
			{
				return this.m_outputs;
			}
		}

		// Token: 0x06003C72 RID: 15474 RVA: 0x001191A6 File Offset: 0x001173A6
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06003C73 RID: 15475 RVA: 0x001191B0 File Offset: 0x001173B0
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x040016A3 RID: 5795
		private readonly VarVec m_keys;

		// Token: 0x040016A4 RID: 5796
		private readonly VarVec m_outputs;
	}
}
