using System;

namespace WebGrease
{
	// Token: 0x02000110 RID: 272
	public class TimeMeasureItem
	{
		// Token: 0x06001113 RID: 4371 RVA: 0x0004BFAE File Offset: 0x0004A1AE
		public TimeMeasureItem(string id, DateTime value)
		{
			this.Id = id;
			this.Value = value;
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06001114 RID: 4372 RVA: 0x0004BFC4 File Offset: 0x0004A1C4
		// (set) Token: 0x06001115 RID: 4373 RVA: 0x0004BFCC File Offset: 0x0004A1CC
		public string Id { get; set; }

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06001116 RID: 4374 RVA: 0x0004BFD5 File Offset: 0x0004A1D5
		// (set) Token: 0x06001117 RID: 4375 RVA: 0x0004BFDD File Offset: 0x0004A1DD
		public DateTime Value { get; set; }
	}
}
