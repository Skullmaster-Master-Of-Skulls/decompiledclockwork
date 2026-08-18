using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001A24 RID: 6692
	public interface ISchedulerInfo
	{
		// Token: 0x17004EA2 RID: 20130
		// (get) Token: 0x060103C5 RID: 66501
		// (set) Token: 0x060103C6 RID: 66502
		DateTime ViewStart { get; set; }

		// Token: 0x17004EA3 RID: 20131
		// (get) Token: 0x060103C7 RID: 66503
		// (set) Token: 0x060103C8 RID: 66504
		DateTime ViewEnd { get; set; }

		// Token: 0x17004EA4 RID: 20132
		// (get) Token: 0x060103C9 RID: 66505
		// (set) Token: 0x060103CA RID: 66506
		bool EnableDescriptionField { get; set; }

		// Token: 0x17004EA5 RID: 20133
		// (get) Token: 0x060103CB RID: 66507
		// (set) Token: 0x060103CC RID: 66508
		int MinutesPerRow { get; set; }

		// Token: 0x17004EA6 RID: 20134
		// (get) Token: 0x060103CD RID: 66509
		// (set) Token: 0x060103CE RID: 66510
		int TimeZoneOffset { get; set; }

		// Token: 0x17004EA7 RID: 20135
		// (get) Token: 0x060103CF RID: 66511
		// (set) Token: 0x060103D0 RID: 66512
		int VisibleAppointmentsPerDay { get; set; }

		// Token: 0x17004EA8 RID: 20136
		// (get) Token: 0x060103D1 RID: 66513
		// (set) Token: 0x060103D2 RID: 66514
		AppointmentUpdateMode UpdateMode { get; set; }
	}
}
