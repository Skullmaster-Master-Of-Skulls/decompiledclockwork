using System;
using System.Diagnostics;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000D1 RID: 209
	internal sealed class OuterApplyOp : ApplyBaseOp
	{
		// Token: 0x06000C4F RID: 3151 RVA: 0x0003C093 File Offset: 0x0003A293
		private OuterApplyOp() : base(OpType.OuterApply)
		{
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x0003C09D File Offset: 0x0003A29D
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x0003C0A7 File Offset: 0x0003A2A7
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x0400096F RID: 2415
		internal static readonly OuterApplyOp Instance = new OuterApplyOp();

		// Token: 0x04000970 RID: 2416
		internal static readonly OuterApplyOp Pattern = OuterApplyOp.Instance;
	}
}
