using System;
using System.Diagnostics;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000D8 RID: 216
	internal abstract class GroupByBaseOp : RelOp
	{
		// Token: 0x06000C75 RID: 3189 RVA: 0x0003BEC1 File Offset: 0x0003A0C1
		protected GroupByBaseOp(OpType opType) : base(opType)
		{
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x0003C214 File Offset: 0x0003A414
		internal GroupByBaseOp(OpType opType, VarVec keys, VarVec outputs) : this(opType)
		{
			this.m_keys = keys;
			this.m_outputs = outputs;
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000C77 RID: 3191 RVA: 0x0003C22B File Offset: 0x0003A42B
		internal VarVec Keys
		{
			get
			{
				return this.m_keys;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000C78 RID: 3192 RVA: 0x0003C233 File Offset: 0x0003A433
		internal VarVec Outputs
		{
			get
			{
				return this.m_outputs;
			}
		}

		// Token: 0x06000C79 RID: 3193 RVA: 0x0003BCF2 File Offset: 0x00039EF2
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06000C7A RID: 3194 RVA: 0x0003C23B File Offset: 0x0003A43B
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x0400097C RID: 2428
		private VarVec m_keys;

		// Token: 0x0400097D RID: 2429
		private VarVec m_outputs;
	}
}
