using System;
using System.Diagnostics;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000E1 RID: 225
	internal sealed class SingleRowTableOp : RelOp
	{
		// Token: 0x06000CA9 RID: 3241 RVA: 0x0003C42C File Offset: 0x0003A62C
		private SingleRowTableOp() : base(OpType.SingleRowTable)
		{
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000CAA RID: 3242 RVA: 0x000173E2 File Offset: 0x000155E2
		internal override int Arity
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x0003C436 File Offset: 0x0003A636
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x0003C440 File Offset: 0x0003A640
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x0400098B RID: 2443
		internal static readonly SingleRowTableOp Instance = new SingleRowTableOp();

		// Token: 0x0400098C RID: 2444
		internal static readonly SingleRowTableOp Pattern = SingleRowTableOp.Instance;
	}
}
