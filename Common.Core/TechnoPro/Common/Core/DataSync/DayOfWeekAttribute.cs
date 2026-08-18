using System;

namespace TechnoPro.Common.Core.DataSync
{
	// Token: 0x0200010C RID: 268
	internal class DayOfWeekAttribute : Attribute
	{
		// Token: 0x06000AFE RID: 2814 RVA: 0x000487C2 File Offset: 0x000469C2
		public DayOfWeekAttribute()
		{
			this.TitlesLowerCase = new string[0];
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x000487D9 File Offset: 0x000469D9
		public DayOfWeekAttribute(DayOfWeek dow, params string[] lowerCaseTitles)
		{
			this.DayOfWeek = dow;
			this.TitlesLowerCase = (lowerCaseTitles ?? new string[0]);
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000B00 RID: 2816 RVA: 0x000487FD File Offset: 0x000469FD
		// (set) Token: 0x06000B01 RID: 2817 RVA: 0x00048805 File Offset: 0x00046A05
		public string[] TitlesLowerCase { get; set; }

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000B02 RID: 2818 RVA: 0x0004880E File Offset: 0x00046A0E
		// (set) Token: 0x06000B03 RID: 2819 RVA: 0x00048816 File Offset: 0x00046A16
		public DayOfWeek DayOfWeek { get; set; }
	}
}
