using System;

namespace TechnoPro.Common.Public.Entities.LookupCourses
{
	// Token: 0x020002E1 RID: 737
	public class DayInYear
	{
		// Token: 0x06001627 RID: 5671 RVA: 0x0000D55A File Offset: 0x0000B75A
		public DayInYear()
		{
		}

		// Token: 0x06001628 RID: 5672 RVA: 0x0001B908 File Offset: 0x00019B08
		public DayInYear(int month, int day)
		{
			try
			{
				this.DayOfYear = new DateTime(2015, month, day).DayOfYear;
				this.IsValid = true;
			}
			catch
			{
				this.IsValid = false;
			}
		}

		// Token: 0x1700091B RID: 2331
		// (get) Token: 0x06001629 RID: 5673 RVA: 0x0001B964 File Offset: 0x00019B64
		// (set) Token: 0x0600162A RID: 5674 RVA: 0x0001B96C File Offset: 0x00019B6C
		public bool IsValid { get; set; }

		// Token: 0x1700091C RID: 2332
		// (get) Token: 0x0600162B RID: 5675 RVA: 0x0001B975 File Offset: 0x00019B75
		// (set) Token: 0x0600162C RID: 5676 RVA: 0x0001B97D File Offset: 0x00019B7D
		public int DayOfYear { get; set; }

		// Token: 0x0400132A RID: 4906
		private const int NonLeapYear = 2015;
	}
}
