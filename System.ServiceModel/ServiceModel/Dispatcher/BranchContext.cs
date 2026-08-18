using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000483 RID: 1155
	internal struct BranchContext
	{
		// Token: 0x06002CC1 RID: 11457 RVA: 0x000AEA5A File Offset: 0x000ACC5A
		internal BranchContext(ProcessingContext context)
		{
			this.sourceContext = context;
			this.branchContext = null;
		}

		// Token: 0x06002CC2 RID: 11458 RVA: 0x000AEA6A File Offset: 0x000ACC6A
		internal ProcessingContext Create()
		{
			if (this.branchContext == null)
			{
				this.branchContext = this.sourceContext.Clone();
			}
			else
			{
				this.branchContext.CopyFrom(this.sourceContext);
			}
			return this.branchContext;
		}

		// Token: 0x06002CC3 RID: 11459 RVA: 0x000AEA9E File Offset: 0x000ACC9E
		internal void Release()
		{
			if (this.branchContext != null)
			{
				this.branchContext.Release();
			}
		}

		// Token: 0x0400244E RID: 9294
		private ProcessingContext branchContext;

		// Token: 0x0400244F RID: 9295
		private ProcessingContext sourceContext;
	}
}
