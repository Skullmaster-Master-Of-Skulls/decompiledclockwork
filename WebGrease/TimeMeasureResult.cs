using System;
using System.Collections.Generic;

namespace WebGrease
{
	// Token: 0x02000112 RID: 274
	public class TimeMeasureResult
	{
		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06001118 RID: 4376 RVA: 0x0004BFE6 File Offset: 0x0004A1E6
		// (set) Token: 0x06001119 RID: 4377 RVA: 0x0004BFEE File Offset: 0x0004A1EE
		public int Count { get; set; }

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x0600111A RID: 4378 RVA: 0x0004BFF7 File Offset: 0x0004A1F7
		// (set) Token: 0x0600111B RID: 4379 RVA: 0x0004BFFF File Offset: 0x0004A1FF
		public double Duration { get; set; }

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x0600111C RID: 4380 RVA: 0x0004C008 File Offset: 0x0004A208
		// (set) Token: 0x0600111D RID: 4381 RVA: 0x0004C010 File Offset: 0x0004A210
		public IEnumerable<string> IdParts { get; set; }

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x0600111E RID: 4382 RVA: 0x0004C019 File Offset: 0x0004A219
		public string Name
		{
			get
			{
				return WebGreaseContext.ToStringId(this.IdParts);
			}
		}
	}
}
