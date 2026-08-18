using System;

namespace TechnoPro.Common.Public.Entities.Tasks
{
	// Token: 0x0200017B RID: 379
	public class TaskOrGroup
	{
		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000956 RID: 2390 RVA: 0x00012AFD File Offset: 0x00010CFD
		// (set) Token: 0x06000957 RID: 2391 RVA: 0x00012B05 File Offset: 0x00010D05
		public Task Task { get; set; }

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000958 RID: 2392 RVA: 0x00012B0E File Offset: 0x00010D0E
		// (set) Token: 0x06000959 RID: 2393 RVA: 0x00012B16 File Offset: 0x00010D16
		public TaskGroup Group { get; set; }
	}
}
