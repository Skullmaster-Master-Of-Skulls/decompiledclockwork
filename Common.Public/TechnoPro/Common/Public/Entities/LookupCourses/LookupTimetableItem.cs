using System;

namespace TechnoPro.Common.Public.Entities.LookupCourses
{
	// Token: 0x020002EF RID: 751
	[Serializable]
	public class LookupTimetableItem : BusinessBase<int>
	{
		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x060016B2 RID: 5810 RVA: 0x0001BFF4 File Offset: 0x0001A1F4
		// (set) Token: 0x060016B3 RID: 5811 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int TimetableId
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

		// Token: 0x060016B4 RID: 5812 RVA: 0x0001C00C File Offset: 0x0001A20C
		public LookupTimetableItem()
		{
			this.TimetableType = 'C';
			this.Room = "";
		}

		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x060016B5 RID: 5813 RVA: 0x0001C02B File Offset: 0x0001A22B
		// (set) Token: 0x060016B6 RID: 5814 RVA: 0x0001C033 File Offset: 0x0001A233
		public char TimetableType { get; set; }

		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x060016B7 RID: 5815 RVA: 0x0001C03C File Offset: 0x0001A23C
		// (set) Token: 0x060016B8 RID: 5816 RVA: 0x0001C044 File Offset: 0x0001A244
		public TimeSpan StartTime { get; set; }

		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x060016B9 RID: 5817 RVA: 0x0001C04D File Offset: 0x0001A24D
		// (set) Token: 0x060016BA RID: 5818 RVA: 0x0001C055 File Offset: 0x0001A255
		public TimeSpan EndTime { get; set; }

		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x060016BB RID: 5819 RVA: 0x0001C05E File Offset: 0x0001A25E
		// (set) Token: 0x060016BC RID: 5820 RVA: 0x0001C066 File Offset: 0x0001A266
		public DayOfWeek DayOfWeek { get; set; }

		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x060016BD RID: 5821 RVA: 0x0001C06F File Offset: 0x0001A26F
		// (set) Token: 0x060016BE RID: 5822 RVA: 0x0001C077 File Offset: 0x0001A277
		public string Room { get; set; }
	}
}
