using System;

namespace TechnoPro.ClockWorkWeb.user.TutoringTutors
{
	// Token: 0x0200003D RID: 61
	public class AvailabilityScheduleTimeWrapper
	{
		// Token: 0x0600017F RID: 383 RVA: 0x0000AF9E File Offset: 0x0000919E
		public AvailabilityScheduleTimeWrapper()
		{
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000B0A4 File Offset: 0x000092A4
		public AvailabilityScheduleTimeWrapper(DateTime date, TimeSpan start, TimeSpan end)
		{
			this.StartMinutes = Convert.ToInt32(start.TotalMinutes);
			this.EndMinutes = Convert.ToInt32(end.TotalMinutes);
			this.StartDisplay = DateTime.Now.Date.Add(start).ToString("h:mm tt");
			this.EndDisplay = DateTime.Now.Date.Add(end).ToString("h:mm tt");
			this.Id = string.Concat(new string[]
			{
				date.ToString("yyyy-MM-dd"),
				".",
				this.StartMinutes.ToString(),
				".",
				this.EndMinutes.ToString()
			});
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000181 RID: 385 RVA: 0x0000B186 File Offset: 0x00009386
		// (set) Token: 0x06000182 RID: 386 RVA: 0x0000B18E File Offset: 0x0000938E
		public int StartMinutes { get; private set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000183 RID: 387 RVA: 0x0000B197 File Offset: 0x00009397
		// (set) Token: 0x06000184 RID: 388 RVA: 0x0000B19F File Offset: 0x0000939F
		public int EndMinutes { get; private set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000185 RID: 389 RVA: 0x0000B1A8 File Offset: 0x000093A8
		// (set) Token: 0x06000186 RID: 390 RVA: 0x0000B1B0 File Offset: 0x000093B0
		public string StartDisplay { get; private set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000187 RID: 391 RVA: 0x0000B1B9 File Offset: 0x000093B9
		// (set) Token: 0x06000188 RID: 392 RVA: 0x0000B1C1 File Offset: 0x000093C1
		public string EndDisplay { get; private set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000189 RID: 393 RVA: 0x0000B1CA File Offset: 0x000093CA
		// (set) Token: 0x0600018A RID: 394 RVA: 0x0000B1D2 File Offset: 0x000093D2
		public string Id { get; private set; }
	}
}
