using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x02001A07 RID: 6663
	public class SchedulerOperationResult<T> : ISchedulerOperationResult<T> where T : IAppointmentData
	{
		// Token: 0x17004DCE RID: 19918
		// (get) Token: 0x060101ED RID: 66029 RVA: 0x0039F47E File Offset: 0x0039D67E
		// (set) Token: 0x060101EE RID: 66030 RVA: 0x0039F486 File Offset: 0x0039D686
		public IEnumerable<T> Appointments
		{
			get
			{
				return this._appointments;
			}
			set
			{
				this._appointments = value;
			}
		}

		// Token: 0x04004904 RID: 18692
		private IEnumerable<T> _appointments;
	}
}
