using System;

namespace TechnoPro.ClockWorkWeb.user.TutoringTutors
{
	// Token: 0x02000039 RID: 57
	public class SpecialDayItem
	{
		// Token: 0x06000165 RID: 357 RVA: 0x0000AF9E File Offset: 0x0000919E
		public SpecialDayItem()
		{
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0000AFA8 File Offset: 0x000091A8
		public SpecialDayItem(DateTime dt)
		{
			this.SetDate(dt);
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000AFBA File Offset: 0x000091BA
		private void SetDate(DateTime dt)
		{
			this.Year = dt.Year;
			this.Month = dt.Month;
			this.Day = dt.Day;
			this.id = dt.ToString("yyyy-MM-dd");
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000168 RID: 360 RVA: 0x0000AFFA File Offset: 0x000091FA
		// (set) Token: 0x06000169 RID: 361 RVA: 0x0000B002 File Offset: 0x00009202
		public string id { get; private set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600016A RID: 362 RVA: 0x0000B00B File Offset: 0x0000920B
		// (set) Token: 0x0600016B RID: 363 RVA: 0x0000B013 File Offset: 0x00009213
		public int Year { get; private set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600016C RID: 364 RVA: 0x0000B01C File Offset: 0x0000921C
		// (set) Token: 0x0600016D RID: 365 RVA: 0x0000B024 File Offset: 0x00009224
		public int Month { get; private set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600016E RID: 366 RVA: 0x0000B02D File Offset: 0x0000922D
		// (set) Token: 0x0600016F RID: 367 RVA: 0x0000B035 File Offset: 0x00009235
		public int Day { get; private set; }
	}
}
