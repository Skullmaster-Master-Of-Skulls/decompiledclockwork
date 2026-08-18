using System;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020005F9 RID: 1529
	internal sealed class GroupByIntoOp : GroupByBaseOp
	{
		// Token: 0x06003C74 RID: 15476 RVA: 0x001191BA File Offset: 0x001173BA
		private GroupByIntoOp() : base(OpType.GroupByInto)
		{
		}

		// Token: 0x06003C75 RID: 15477 RVA: 0x001191C4 File Offset: 0x001173C4
		internal GroupByIntoOp(VarVec keys, VarVec inputs, VarVec outputs) : base(OpType.GroupByInto, keys, outputs)
		{
			this.m_inputs = inputs;
		}

		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x06003C76 RID: 15478 RVA: 0x001191D7 File Offset: 0x001173D7
		internal VarVec Inputs
		{
			get
			{
				return this.m_inputs;
			}
		}

		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x06003C77 RID: 15479 RVA: 0x001191DF File Offset: 0x001173DF
		internal override int Arity
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x06003C78 RID: 15480 RVA: 0x001191E2 File Offset: 0x001173E2
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06003C79 RID: 15481 RVA: 0x001191EC File Offset: 0x001173EC
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x040016A5 RID: 5797
		private readonly VarVec m_inputs;

		// Token: 0x040016A6 RID: 5798
		internal static readonly GroupByIntoOp Pattern = new GroupByIntoOp();
	}
}
