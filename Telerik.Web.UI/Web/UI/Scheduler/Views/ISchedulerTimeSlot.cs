using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.Scheduler.Views
{
	// Token: 0x02000838 RID: 2104
	public interface ISchedulerTimeSlot
	{
		// Token: 0x17001974 RID: 6516
		// (get) Token: 0x06004DEC RID: 19948
		IList<Appointment> Appointments { get; }

		// Token: 0x17001975 RID: 6517
		// (get) Token: 0x06004DED RID: 19949
		// (set) Token: 0x06004DEE RID: 19950
		WebControl Control { get; set; }

		// Token: 0x17001976 RID: 6518
		// (get) Token: 0x06004DEF RID: 19951
		DateTime Start { get; }

		// Token: 0x17001977 RID: 6519
		// (get) Token: 0x06004DF0 RID: 19952
		DateTime End { get; }

		// Token: 0x17001978 RID: 6520
		// (get) Token: 0x06004DF1 RID: 19953
		TimeSpan Duration { get; }

		// Token: 0x17001979 RID: 6521
		// (get) Token: 0x06004DF2 RID: 19954
		string Index { get; }

		// Token: 0x1700197A RID: 6522
		// (get) Token: 0x06004DF3 RID: 19955
		// (set) Token: 0x06004DF4 RID: 19956
		SchedulerFormContainer FormContainer { get; set; }

		// Token: 0x1700197B RID: 6523
		// (get) Token: 0x06004DF5 RID: 19957
		// (set) Token: 0x06004DF6 RID: 19958
		string CssClass { get; set; }

		// Token: 0x1700197C RID: 6524
		// (get) Token: 0x06004DF7 RID: 19959
		// (set) Token: 0x06004DF8 RID: 19960
		Resource Resource { get; set; }
	}
}
