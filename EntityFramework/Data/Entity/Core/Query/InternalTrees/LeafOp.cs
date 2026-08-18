using System;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x02000641 RID: 1601
	internal sealed class LeafOp : RulePatternOp
	{
		// Token: 0x170009B0 RID: 2480
		// (get) Token: 0x06003ED2 RID: 16082 RVA: 0x001200E4 File Offset: 0x0011E2E4
		internal override int Arity
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06003ED3 RID: 16083 RVA: 0x001200E7 File Offset: 0x0011E2E7
		private LeafOp() : base(OpType.Leaf)
		{
		}

		// Token: 0x0400177E RID: 6014
		internal static readonly LeafOp Instance = new LeafOp();

		// Token: 0x0400177F RID: 6015
		internal static readonly LeafOp Pattern = LeafOp.Instance;
	}
}
