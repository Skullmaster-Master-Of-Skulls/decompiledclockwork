using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000487 RID: 1159
	internal struct QueryBranchResult
	{
		// Token: 0x06002CE2 RID: 11490 RVA: 0x000AF10B File Offset: 0x000AD30B
		internal QueryBranchResult(QueryBranch branch, int valIndex)
		{
			this.branch = branch;
			this.valIndex = valIndex;
		}

		// Token: 0x17000AC5 RID: 2757
		// (get) Token: 0x06002CE3 RID: 11491 RVA: 0x000AF11B File Offset: 0x000AD31B
		internal QueryBranch Branch
		{
			get
			{
				return this.branch;
			}
		}

		// Token: 0x17000AC6 RID: 2758
		// (get) Token: 0x06002CE4 RID: 11492 RVA: 0x000AF123 File Offset: 0x000AD323
		internal int ValIndex
		{
			get
			{
				return this.valIndex;
			}
		}

		// Token: 0x04002455 RID: 9301
		internal QueryBranch branch;

		// Token: 0x04002456 RID: 9302
		private int valIndex;
	}
}
