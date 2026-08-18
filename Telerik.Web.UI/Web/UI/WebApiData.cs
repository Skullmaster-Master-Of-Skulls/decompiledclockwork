using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000823 RID: 2083
	public class WebApiData
	{
		// Token: 0x17001928 RID: 6440
		// (get) Token: 0x06004D0F RID: 19727 RVA: 0x000F28D2 File Offset: 0x000F0AD2
		// (set) Token: 0x06004D10 RID: 19728 RVA: 0x000F28DA File Offset: 0x000F0ADA
		public SchedulerInfo SchedulerInfo { get; set; }

		// Token: 0x17001929 RID: 6441
		// (get) Token: 0x06004D11 RID: 19729 RVA: 0x000F28E3 File Offset: 0x000F0AE3
		// (set) Token: 0x06004D12 RID: 19730 RVA: 0x000F28EB File Offset: 0x000F0AEB
		public AppointmentData AppointmentData { get; set; }

		// Token: 0x1700192A RID: 6442
		// (get) Token: 0x06004D13 RID: 19731 RVA: 0x000F28F4 File Offset: 0x000F0AF4
		// (set) Token: 0x06004D14 RID: 19732 RVA: 0x000F28FC File Offset: 0x000F0AFC
		public AppointmentData RecurrenceExceptionData { get; set; }

		// Token: 0x1700192B RID: 6443
		// (get) Token: 0x06004D15 RID: 19733 RVA: 0x000F2905 File Offset: 0x000F0B05
		// (set) Token: 0x06004D16 RID: 19734 RVA: 0x000F290D File Offset: 0x000F0B0D
		public AppointmentData MasterAppointmentData { get; set; }

		// Token: 0x1700192C RID: 6444
		// (get) Token: 0x06004D17 RID: 19735 RVA: 0x000F2916 File Offset: 0x000F0B16
		// (set) Token: 0x06004D18 RID: 19736 RVA: 0x000F291E File Offset: 0x000F0B1E
		public bool DeleteSeries { get; set; }
	}
}
