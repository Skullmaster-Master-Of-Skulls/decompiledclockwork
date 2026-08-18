using System;
using System.Diagnostics;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000C7 RID: 199
	internal sealed class ScanTableOp : ScanTableBaseOp
	{
		// Token: 0x06000C22 RID: 3106 RVA: 0x0003BED2 File Offset: 0x0003A0D2
		internal ScanTableOp(Table table) : base(OpType.ScanTable, table)
		{
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x0003BEDD File Offset: 0x0003A0DD
		private ScanTableOp() : base(OpType.ScanTable)
		{
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000C24 RID: 3108 RVA: 0x000173E2 File Offset: 0x000155E2
		internal override int Arity
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x0003BEE7 File Offset: 0x0003A0E7
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x0003BEF1 File Offset: 0x0003A0F1
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04000960 RID: 2400
		internal static readonly ScanTableOp Pattern = new ScanTableOp();
	}
}
