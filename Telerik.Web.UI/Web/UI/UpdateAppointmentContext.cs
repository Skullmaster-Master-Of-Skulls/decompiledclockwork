using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001A3B RID: 6715
	public class UpdateAppointmentContext
	{
		// Token: 0x17004EF2 RID: 20210
		// (get) Token: 0x06010492 RID: 66706 RVA: 0x003A3909 File Offset: 0x003A1B09
		// (set) Token: 0x06010493 RID: 66707 RVA: 0x003A3911 File Offset: 0x003A1B11
		public Appointment OriginalAppointment
		{
			get
			{
				return this._originalAppointment;
			}
			protected set
			{
				this._originalAppointment = value;
			}
		}

		// Token: 0x06010494 RID: 66708 RVA: 0x003A391A File Offset: 0x003A1B1A
		public UpdateAppointmentContext()
		{
		}

		// Token: 0x06010495 RID: 66709 RVA: 0x003A3922 File Offset: 0x003A1B22
		public UpdateAppointmentContext(Appointment originalAppointment)
		{
			this._originalAppointment = originalAppointment;
		}

		// Token: 0x0400495C RID: 18780
		private Appointment _originalAppointment;
	}
}
