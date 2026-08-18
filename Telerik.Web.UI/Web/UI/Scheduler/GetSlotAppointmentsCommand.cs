using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Scheduler
{
	// Token: 0x020007DE RID: 2014
	internal class GetSlotAppointmentsCommand : ReminderCommand
	{
		// Token: 0x170016A4 RID: 5796
		// (get) Token: 0x06004625 RID: 17957 RVA: 0x000DC4BE File Offset: 0x000DA6BE
		// (set) Token: 0x06004626 RID: 17958 RVA: 0x000DC4C6 File Offset: 0x000DA6C6
		public DateTime Start { get; set; }

		// Token: 0x170016A5 RID: 5797
		// (get) Token: 0x06004627 RID: 17959 RVA: 0x000DC4CF File Offset: 0x000DA6CF
		// (set) Token: 0x06004628 RID: 17960 RVA: 0x000DC4D7 File Offset: 0x000DA6D7
		public DateTime End { get; set; }

		// Token: 0x06004629 RID: 17961 RVA: 0x000DC4E0 File Offset: 0x000DA6E0
		public override void Execute(ICallbackCommandContext context)
		{
			IList<Appointment> appointmentsInRange = context.Appointments.GetAppointmentsInRange(this.Start, this.End);
			context.SlotAppointments(appointmentsInRange);
		}
	}
}
