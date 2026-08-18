using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001A3A RID: 6714
	public class CreateRecurrenceExceptionContext
	{
		// Token: 0x17004EF0 RID: 20208
		// (get) Token: 0x0601048C RID: 66700 RVA: 0x003A38C9 File Offset: 0x003A1AC9
		// (set) Token: 0x0601048D RID: 66701 RVA: 0x003A38D1 File Offset: 0x003A1AD1
		public DateTime RecurrenceExceptionDate
		{
			get
			{
				return this._recurrenceExceptionDate;
			}
			protected set
			{
				this._recurrenceExceptionDate = value;
			}
		}

		// Token: 0x17004EF1 RID: 20209
		// (get) Token: 0x0601048E RID: 66702 RVA: 0x003A38DA File Offset: 0x003A1ADA
		// (set) Token: 0x0601048F RID: 66703 RVA: 0x003A38E2 File Offset: 0x003A1AE2
		public Appointment ParentAppointment
		{
			get
			{
				return this._parentAppointment;
			}
			protected set
			{
				this._parentAppointment = value;
			}
		}

		// Token: 0x06010490 RID: 66704 RVA: 0x003A38EB File Offset: 0x003A1AEB
		public CreateRecurrenceExceptionContext()
		{
		}

		// Token: 0x06010491 RID: 66705 RVA: 0x003A38F3 File Offset: 0x003A1AF3
		public CreateRecurrenceExceptionContext(DateTime recurrenceExceptionDate, Appointment parentAppointment)
		{
			this._recurrenceExceptionDate = recurrenceExceptionDate;
			this._parentAppointment = parentAppointment;
		}

		// Token: 0x0400495A RID: 18778
		private DateTime _recurrenceExceptionDate;

		// Token: 0x0400495B RID: 18779
		private Appointment _parentAppointment;
	}
}
