using System;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000E5 RID: 229
	internal sealed class LeafOp : RulePatternOp
	{
		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000CB7 RID: 3255 RVA: 0x000173E2 File Offset: 0x000155E2
		internal override int Arity
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x0003C562 File Offset: 0x0003A762
		private LeafOp() : base(OpType.Leaf)
		{
		}

		// Token: 0x04000990 RID: 2448
		internal static readonly LeafOp Instance = new LeafOp();

		// Token: 0x04000991 RID: 2449
		internal static readonly LeafOp Pattern = LeafOp.Instance;
	}
}
