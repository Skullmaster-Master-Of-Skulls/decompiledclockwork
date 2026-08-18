using System;

namespace TechnoPro.ClockWorkWeb.staff.schedule
{
	// Token: 0x02000100 RID: 256
	public class SaveAppointmentResult
	{
		// Token: 0x0600076F RID: 1903 RVA: 0x0000AF9E File Offset: 0x0000919E
		public SaveAppointmentResult()
		{
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x00038BEE File Offset: 0x00036DEE
		public SaveAppointmentResult(string errorMessage)
		{
			this.Worked = false;
			this.ErrorMessage = errorMessage;
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x00038C08 File Offset: 0x00036E08
		public SaveAppointmentResult(int appId)
		{
			this.Worked = true;
			this.AppointmentId = appId;
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000772 RID: 1906 RVA: 0x00038C22 File Offset: 0x00036E22
		// (set) Token: 0x06000773 RID: 1907 RVA: 0x00038C2A File Offset: 0x00036E2A
		public bool Worked { get; set; }

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000774 RID: 1908 RVA: 0x00038C33 File Offset: 0x00036E33
		// (set) Token: 0x06000775 RID: 1909 RVA: 0x00038C3B File Offset: 0x00036E3B
		public string ErrorMessage { get; set; }

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000776 RID: 1910 RVA: 0x00038C44 File Offset: 0x00036E44
		// (set) Token: 0x06000777 RID: 1911 RVA: 0x00038C4C File Offset: 0x00036E4C
		public int AppointmentId { get; set; }
	}
}
