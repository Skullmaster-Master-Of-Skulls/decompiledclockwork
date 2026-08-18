using System;

namespace System.Xml.Serialization
{
	// Token: 0x02000319 RID: 793
	internal class RecursionLimiter
	{
		// Token: 0x0600258C RID: 9612 RVA: 0x000B355D File Offset: 0x000B255D
		internal RecursionLimiter()
		{
			this.depth = 0;
			this.maxDepth = (DiagnosticsSwitches.NonRecursiveTypeLoading.Enabled ? 1 : int.MaxValue);
		}

		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x0600258D RID: 9613 RVA: 0x000B3586 File Offset: 0x000B2586
		internal bool IsExceededLimit
		{
			get
			{
				return this.depth > this.maxDepth;
			}
		}

		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x0600258E RID: 9614 RVA: 0x000B3596 File Offset: 0x000B2596
		// (set) Token: 0x0600258F RID: 9615 RVA: 0x000B359E File Offset: 0x000B259E
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

		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x06002590 RID: 9616 RVA: 0x000B35A7 File Offset: 0x000B25A7
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

		// Token: 0x040015A6 RID: 5542
		private int maxDepth;

		// Token: 0x040015A7 RID: 5543
		private int depth;

		// Token: 0x040015A8 RID: 5544
		private WorkItems deferredWorkItems;
	}
}
