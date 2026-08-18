using System;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x02000620 RID: 1568
	internal sealed class ScanTableOp : ScanTableBaseOp
	{
		// Token: 0x06003D4D RID: 15693 RVA: 0x0011B021 File Offset: 0x00119221
		internal ScanTableOp(Table table) : base(OpType.ScanTable, table)
		{
		}

		// Token: 0x06003D4E RID: 15694 RVA: 0x0011B02C File Offset: 0x0011922C
		private ScanTableOp() : base(OpType.ScanTable)
		{
		}

		// Token: 0x17000978 RID: 2424
		// (get) Token: 0x06003D4F RID: 15695 RVA: 0x0011B036 File Offset: 0x00119236
		internal override int Arity
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06003D50 RID: 15696 RVA: 0x0011B039 File Offset: 0x00119239
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06003D51 RID: 15697 RVA: 0x0011B043 File Offset: 0x00119243
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x0400172C RID: 5932
		internal static readonly ScanTableOp Pattern = new ScanTableOp();
	}
}
