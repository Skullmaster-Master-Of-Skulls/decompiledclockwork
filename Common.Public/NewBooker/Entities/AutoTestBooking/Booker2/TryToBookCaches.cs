using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace NewBooker.Entities.AutoTestBooking.Booker2
{
	// Token: 0x020000A9 RID: 169
	public class TryToBookCaches
	{
		// Token: 0x060003D8 RID: 984 RVA: 0x0000CFB8 File Offset: 0x0000B1B8
		public TryToBookCaches()
		{
			this.Holidays = new Dictionary<DateTime, bool>();
			this.RoomScheduleCache = new List<TryToBookSchedule>();
			this.StudentScheduleCache = new Dictionary<DateTime, List<TryToBookAvailability>>();
			this.NumberOfOtherTestsExamsStudentHasByDate = new Dictionary<DateTime, int>();
			this.DateTimesAlreadyChecked = new List<long>();
			this.GeneralCache = new Dictionary<string, object>();
			this.NoticesCache = new Dictionary<DateTime, IList<string>>();
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x0000D020 File Offset: 0x0000B220
		// (set) Token: 0x060003DA RID: 986 RVA: 0x0000D028 File Offset: 0x0000B228
		public IList<TryToBookSchedule> RoomScheduleCache { get; set; }

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060003DB RID: 987 RVA: 0x0000D031 File Offset: 0x0000B231
		// (set) Token: 0x060003DC RID: 988 RVA: 0x0000D039 File Offset: 0x0000B239
		public IDictionary<DateTime, List<TryToBookAvailability>> StudentScheduleCache { get; set; }

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060003DD RID: 989 RVA: 0x0000D042 File Offset: 0x0000B242
		// (set) Token: 0x060003DE RID: 990 RVA: 0x0000D04A File Offset: 0x0000B24A
		public IDictionary<DateTime, bool> Holidays { get; set; }

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060003DF RID: 991 RVA: 0x0000D053 File Offset: 0x0000B253
		// (set) Token: 0x060003E0 RID: 992 RVA: 0x0000D05B File Offset: 0x0000B25B
		public IDictionary<DateTime, int> NumberOfOtherTestsExamsStudentHasByDate { get; set; }

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x0000D064 File Offset: 0x0000B264
		// (set) Token: 0x060003E2 RID: 994 RVA: 0x0000D06C File Offset: 0x0000B26C
		public IDictionary<string, object> GeneralCache { get; set; }

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060003E3 RID: 995 RVA: 0x0000D075 File Offset: 0x0000B275
		// (set) Token: 0x060003E4 RID: 996 RVA: 0x0000D07D File Offset: 0x0000B27D
		public IList<LookupCourse> StudentCourses { get; set; }

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x0000D086 File Offset: 0x0000B286
		// (set) Token: 0x060003E6 RID: 998 RVA: 0x0000D08E File Offset: 0x0000B28E
		public IDictionary<DateTime, IList<string>> NoticesCache { get; set; }

		// Token: 0x04000196 RID: 406
		public IList<long> DateTimesAlreadyChecked;
	}
}
