using System;

namespace TechnoPro.Common.Public.Entities.AppointmentsTestBooking
{
	// Token: 0x02000507 RID: 1287
	public class ExamStatus : BusinessBase<int>
	{
		// Token: 0x17001054 RID: 4180
		// (get) Token: 0x06002730 RID: 10032 RVA: 0x0002960A File Offset: 0x0002780A
		// (set) Token: 0x06002731 RID: 10033 RVA: 0x00029612 File Offset: 0x00027812
		public int ExamStatusLookupId { get; set; }

		// Token: 0x17001055 RID: 4181
		// (get) Token: 0x06002732 RID: 10034 RVA: 0x0002961B File Offset: 0x0002781B
		// (set) Token: 0x06002733 RID: 10035 RVA: 0x00029623 File Offset: 0x00027823
		public string Title { get; set; }

		// Token: 0x17001056 RID: 4182
		// (get) Token: 0x06002734 RID: 10036 RVA: 0x0002962C File Offset: 0x0002782C
		// (set) Token: 0x06002735 RID: 10037 RVA: 0x00029634 File Offset: 0x00027834
		public int ColourArgB { get; set; }
	}
}
