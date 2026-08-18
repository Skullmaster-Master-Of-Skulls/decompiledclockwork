using System;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x02000627 RID: 1575
	internal sealed class SingleRowOp : RelOp
	{
		// Token: 0x06003D68 RID: 15720 RVA: 0x0011B2AB File Offset: 0x001194AB
		private SingleRowOp() : base(OpType.SingleRow)
		{
		}

		// Token: 0x1700097D RID: 2429
		// (get) Token: 0x06003D69 RID: 15721 RVA: 0x0011B2B5 File Offset: 0x001194B5
		internal override int Arity
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06003D6A RID: 15722 RVA: 0x0011B2B8 File Offset: 0x001194B8
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06003D6B RID: 15723 RVA: 0x0011B2C2 File Offset: 0x001194C2
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04001731 RID: 5937
		internal static readonly SingleRowOp Instance = new SingleRowOp();

		// Token: 0x04001732 RID: 5938
		internal static readonly SingleRowOp Pattern = SingleRowOp.Instance;
	}
}
