using System;
using System.Diagnostics;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000C8 RID: 200
	internal sealed class ScanViewOp : ScanTableBaseOp
	{
		// Token: 0x06000C28 RID: 3112 RVA: 0x0003BF07 File Offset: 0x0003A107
		internal ScanViewOp(Table table) : base(OpType.ScanView, table)
		{
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x0003BF12 File Offset: 0x0003A112
		private ScanViewOp() : base(OpType.ScanView)
		{
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000C2A RID: 3114 RVA: 0x00017938 File Offset: 0x00015B38
		internal override int Arity
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06000C2B RID: 3115 RVA: 0x0003BF1C File Offset: 0x0003A11C
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06000C2C RID: 3116 RVA: 0x0003BF26 File Offset: 0x0003A126
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04000961 RID: 2401
		internal static readonly ScanViewOp Pattern = new ScanViewOp();
	}
}
