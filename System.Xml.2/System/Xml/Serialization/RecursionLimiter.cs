using System;

namespace System.Xml.Serialization
{
	// Token: 0x0200019F RID: 415
	internal class RecursionLimiter
	{
		// Token: 0x06001B5A RID: 7002 RVA: 0x0007C201 File Offset: 0x0007A401
		internal RecursionLimiter()
		{
			this.depth = 0;
			this.maxDepth = (DiagnosticsSwitches.NonRecursiveTypeLoading.Enabled ? 1 : int.MaxValue);
		}

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x06001B5B RID: 7003 RVA: 0x0007C22A File Offset: 0x0007A42A
		internal bool IsExceededLimit
		{
			get
			{
				return this.depth > this.maxDepth;
			}
		}

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x06001B5C RID: 7004 RVA: 0x0007C23A File Offset: 0x0007A43A
		// (set) Token: 0x06001B5D RID: 7005 RVA: 0x0007C242 File Offset: 0x0007A442
		internal int Depth
		{
			get
			{
				return this.depth;
			}
			set
			{
				this.depth = value;
			}
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x06001B5E RID: 7006 RVA: 0x0007C24B File Offset: 0x0007A44B
		internal WorkItems DeferredWorkItems
		{
			get
			{
				if (this.deferredWorkItems == null)
				{
					this.deferredWorkItems = new WorkItems();
				}
				return this.deferredWorkItems;
			}
		}

		// Token: 0x04000C17 RID: 3095
		private int maxDepth;

		// Token: 0x04000C18 RID: 3096
		private int depth;

		// Token: 0x04000C19 RID: 3097
		private WorkItems deferredWorkItems;
	}
}
