using System;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x02000621 RID: 1569
	internal sealed class ScanViewOp : ScanTableBaseOp
	{
		// Token: 0x06003D53 RID: 15699 RVA: 0x0011B059 File Offset: 0x00119259
		internal ScanViewOp(Table table) : base(OpType.ScanView, table)
		{
		}

		// Token: 0x06003D54 RID: 15700 RVA: 0x0011B064 File Offset: 0x00119264
		private ScanViewOp() : base(OpType.ScanView)
		{
		}

		// Token: 0x17000979 RID: 2425
		// (get) Token: 0x06003D55 RID: 15701 RVA: 0x0011B06E File Offset: 0x0011926E
		internal override int Arity
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06003D56 RID: 15702 RVA: 0x0011B071 File Offset: 0x00119271
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06003D57 RID: 15703 RVA: 0x0011B07B File Offset: 0x0011927B
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x0400172D RID: 5933
		internal static readonly ScanViewOp Pattern = new ScanViewOp();
	}
}
