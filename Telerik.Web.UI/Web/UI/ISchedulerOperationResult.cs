using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x02001A06 RID: 6662
	public interface ISchedulerOperationResult<T> where T : IAppointmentData
	{
		// Token: 0x17004DCD RID: 19917
		// (get) Token: 0x060101EB RID: 66027
		// (set) Token: 0x060101EC RID: 66028
		IEnumerable<T> Appointments { get; set; }
	}
}
