using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x02000342 RID: 834
	public interface IPostbackCommandContext
	{
		// Token: 0x1700099C RID: 2460
		// (get) Token: 0x06001C62 RID: 7266
		// (set) Token: 0x06001C63 RID: 7267
		CommandType Command { get; set; }

		// Token: 0x1700099D RID: 2461
		// (get) Token: 0x06001C64 RID: 7268
		// (set) Token: 0x06001C65 RID: 7269
		List<ITask> InsertedTasks { get; set; }

		// Token: 0x1700099E RID: 2462
		// (get) Token: 0x06001C66 RID: 7270
		// (set) Token: 0x06001C67 RID: 7271
		List<ITask> UpdatedTasks { get; set; }

		// Token: 0x1700099F RID: 2463
		// (get) Token: 0x06001C68 RID: 7272
		// (set) Token: 0x06001C69 RID: 7273
		List<ITask> DeletedTasks { get; set; }

		// Token: 0x170009A0 RID: 2464
		// (get) Token: 0x06001C6A RID: 7274
		// (set) Token: 0x06001C6B RID: 7275
		List<IDependency> InsertedDependencies { get; set; }

		// Token: 0x170009A1 RID: 2465
		// (get) Token: 0x06001C6C RID: 7276
		// (set) Token: 0x06001C6D RID: 7277
		List<IDependency> DeletedDependencies { get; set; }

		// Token: 0x170009A2 RID: 2466
		// (get) Token: 0x06001C6E RID: 7278
		// (set) Token: 0x06001C6F RID: 7279
		List<IAssignment> InsertedAssignments { get; set; }

		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x06001C70 RID: 7280
		// (set) Token: 0x06001C71 RID: 7281
		List<IAssignment> UpdatedAssignments { get; set; }

		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x06001C72 RID: 7282
		// (set) Token: 0x06001C73 RID: 7283
		List<IAssignment> DeletedAssignments { get; set; }
	}
}
