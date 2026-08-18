using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;

namespace TechnoPro.Common.Public.Entities.DynamicForms.AppointmentNotes
{
	// Token: 0x020003AD RID: 941
	public class NotesAppointmentExtendedInfo : BusinessBase<int>
	{
		// Token: 0x17000BCD RID: 3021
		// (get) Token: 0x06001CB2 RID: 7346 RVA: 0x00020CDC File Offset: 0x0001EEDC
		// (set) Token: 0x06001CB3 RID: 7347 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int AppointmentId
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
			}
		}

		// Token: 0x17000BCE RID: 3022
		// (get) Token: 0x06001CB4 RID: 7348 RVA: 0x00020CF4 File Offset: 0x0001EEF4
		// (set) Token: 0x06001CB5 RID: 7349 RVA: 0x00020CFC File Offset: 0x0001EEFC
		public IList<Attendee> Attendees { get; set; }

		// Token: 0x17000BCF RID: 3023
		// (get) Token: 0x06001CB6 RID: 7350 RVA: 0x00020D05 File Offset: 0x0001EF05
		// (set) Token: 0x06001CB7 RID: 7351 RVA: 0x00020D0D File Offset: 0x0001EF0D
		public StudentClassTest StudentClassTestInfo { get; set; }
	}
}
