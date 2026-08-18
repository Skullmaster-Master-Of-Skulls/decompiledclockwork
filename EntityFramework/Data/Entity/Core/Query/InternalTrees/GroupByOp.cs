using System;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020005FA RID: 1530
	internal sealed class GroupByOp : GroupByBaseOp
	{
		// Token: 0x06003C7B RID: 15483 RVA: 0x00119202 File Offset: 0x00117402
		private GroupByOp() : base(OpType.GroupBy)
		{
		}

		// Token: 0x06003C7C RID: 15484 RVA: 0x0011920C File Offset: 0x0011740C
		internal GroupByOp(VarVec keys, VarVec outputs) : base(OpType.GroupBy, keys, outputs)
		{
		}

		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x06003C7D RID: 15485 RVA: 0x00119218 File Offset: 0x00117418
		internal override int Arity
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x06003C7E RID: 15486 RVA: 0x0011921B File Offset: 0x0011741B
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06003C7F RID: 15487 RVA: 0x00119225 File Offset: 0x00117425
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x040016A7 RID: 5799
		internal static readonly GroupByOp Pattern = new GroupByOp();
	}
}
