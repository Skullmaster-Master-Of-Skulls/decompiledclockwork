using System;

namespace TechnoPro.Common.Public.Entities.AppointmentsTestBooking.TestBookingViews
{
	// Token: 0x0200051C RID: 1308
	public class ExamManagementViewGroupAttribute : Attribute
	{
		// Token: 0x06002890 RID: 10384 RVA: 0x0000EC26 File Offset: 0x0000CE26
		public ExamManagementViewGroupAttribute()
		{
		}

		// Token: 0x06002891 RID: 10385 RVA: 0x0002A241 File Offset: 0x00028441
		public ExamManagementViewGroupAttribute(string title)
		{
			this.Title = title;
		}

		// Token: 0x170010FA RID: 4346
		// (get) Token: 0x06002892 RID: 10386 RVA: 0x0002A253 File Offset: 0x00028453
		// (set) Token: 0x06002893 RID: 10387 RVA: 0x0002A25B File Offset: 0x0002845B
		public string Title { get; set; }
	}
}
