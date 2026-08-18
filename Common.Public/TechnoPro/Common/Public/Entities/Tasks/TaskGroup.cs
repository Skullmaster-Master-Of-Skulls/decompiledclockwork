using System;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.Tasks
{
	// Token: 0x02000179 RID: 377
	public class TaskGroup : BusinessBase<int>
	{
		// Token: 0x1700036A RID: 874
		// (get) Token: 0x0600093A RID: 2362 RVA: 0x00012A10 File Offset: 0x00010C10
		// (set) Token: 0x0600093B RID: 2363 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int TaskGroupId
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x0600093C RID: 2364 RVA: 0x00012A28 File Offset: 0x00010C28
		// (set) Token: 0x0600093D RID: 2365 RVA: 0x00012A30 File Offset: 0x00010C30
		public PersonBase Owner { get; set; }

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x0600093E RID: 2366 RVA: 0x00012A39 File Offset: 0x00010C39
		// (set) Token: 0x0600093F RID: 2367 RVA: 0x00012A41 File Offset: 0x00010C41
		public string Description { get; set; }

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000940 RID: 2368 RVA: 0x00012A4A File Offset: 0x00010C4A
		// (set) Token: 0x06000941 RID: 2369 RVA: 0x00012A52 File Offset: 0x00010C52
		public int OrderNum { get; set; }

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000942 RID: 2370 RVA: 0x00012A5B File Offset: 0x00010C5B
		// (set) Token: 0x06000943 RID: 2371 RVA: 0x00012A63 File Offset: 0x00010C63
		public bool IsActive { get; set; }

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000944 RID: 2372 RVA: 0x00012A6C File Offset: 0x00010C6C
		// (set) Token: 0x06000945 RID: 2373 RVA: 0x00012A74 File Offset: 0x00010C74
		public bool IsPrivate { get; set; }

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000946 RID: 2374 RVA: 0x00012A7D File Offset: 0x00010C7D
		// (set) Token: 0x06000947 RID: 2375 RVA: 0x00012A85 File Offset: 0x00010C85
		public int ParentTaskGroupId { get; set; }
	}
}
