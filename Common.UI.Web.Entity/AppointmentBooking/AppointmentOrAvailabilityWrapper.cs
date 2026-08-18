using System;
using System.Drawing;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar;
using TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule;

namespace TechnoPro.Common.UI.Web.Entity.AppointmentBooking
{
	// Token: 0x0200004C RID: 76
	public class AppointmentOrAvailabilityWrapper
	{
		// Token: 0x060001EA RID: 490 RVA: 0x000041C8 File Offset: 0x000023C8
		public AppointmentOrAvailabilityWrapper(AvailabilityScheduleItemInfoDTO availabilityItem)
		{
			bool flag = availabilityItem == null;
			if (flag)
			{
				this._id = Guid.NewGuid().ToString();
			}
			else
			{
				this._id = this.GetAvailabilityScheduleItemId(availabilityItem);
				this._subject = "available";
				this._start = availabilityItem.DayAndTime.Date.Date.Add(availabilityItem.DayAndTime.Time.StartTime);
				this._end = availabilityItem.DayAndTime.Date.Date.Add(availabilityItem.DayAndTime.Time.EndTime);
				this._userID = new int?(0);
				this._colour = Color.DodgerBlue.ToArgb();
			}
		}

		// Token: 0x060001EB RID: 491 RVA: 0x000042A0 File Offset: 0x000024A0
		private string GetAvailabilityScheduleItemId(AvailabilityScheduleItemInfoDTO availabilityItem)
		{
			AppointmentOrAvailabilityWrapper.ctr++;
			bool flag = AppointmentOrAvailabilityWrapper.ctr >= int.MaxValue;
			if (flag)
			{
				AppointmentOrAvailabilityWrapper.ctr = 1;
			}
			return (-AppointmentOrAvailabilityWrapper.ctr).ToString();
		}

		// Token: 0x060001EC RID: 492 RVA: 0x000042E8 File Offset: 0x000024E8
		public AppointmentOrAvailabilityWrapper(AppointmentDTO app)
		{
			bool flag = app == null;
			if (flag)
			{
				this._id = Guid.NewGuid().ToString();
			}
			else
			{
				this._id = app.AppointmentId.ToString();
				this._subject = "booked";
				this._start = app.StartDateTime;
				this._end = app.EndDateTime;
				this._userID = new int?(0);
				this._colour = Color.Yellow.ToArgb();
			}
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00004378 File Offset: 0x00002578
		public AppointmentOrAvailabilityWrapper()
		{
			this._id = Guid.NewGuid().ToString();
		}

		// Token: 0x060001EE RID: 494 RVA: 0x000043A6 File Offset: 0x000025A6
		public AppointmentOrAvailabilityWrapper(string subject, DateTime start, DateTime end, string recurrenceRule, string recurrenceParentID, string reminder, int? userID) : this()
		{
			this._subject = subject;
			this._start = start;
			this._end = end;
			this._recurrenceRule = recurrenceRule;
			this._recurrenceParentId = recurrenceParentID;
			this._reminder = reminder;
			this._userID = userID;
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060001EF RID: 495 RVA: 0x000043E8 File Offset: 0x000025E8
		public int Colour
		{
			get
			{
				return this._colour;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x00004400 File Offset: 0x00002600
		public string ID
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x00004418 File Offset: 0x00002618
		// (set) Token: 0x060001F2 RID: 498 RVA: 0x00004430 File Offset: 0x00002630
		public string Subject
		{
			get
			{
				return this._subject;
			}
			set
			{
				this._subject = value;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x0000443C File Offset: 0x0000263C
		// (set) Token: 0x060001F4 RID: 500 RVA: 0x00004454 File Offset: 0x00002654
		public DateTime Start
		{
			get
			{
				return this._start;
			}
			set
			{
				this._start = value;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x00004460 File Offset: 0x00002660
		// (set) Token: 0x060001F6 RID: 502 RVA: 0x00004478 File Offset: 0x00002678
		public DateTime End
		{
			get
			{
				return this._end;
			}
			set
			{
				this._end = value;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x00004484 File Offset: 0x00002684
		// (set) Token: 0x060001F8 RID: 504 RVA: 0x0000449C File Offset: 0x0000269C
		public string RecurrenceRule
		{
			get
			{
				return this._recurrenceRule;
			}
			set
			{
				this._recurrenceRule = value;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x000044A8 File Offset: 0x000026A8
		// (set) Token: 0x060001FA RID: 506 RVA: 0x000044C0 File Offset: 0x000026C0
		public string RecurrenceParentID
		{
			get
			{
				return this._recurrenceParentId;
			}
			set
			{
				this._recurrenceParentId = value;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060001FB RID: 507 RVA: 0x000044CC File Offset: 0x000026CC
		// (set) Token: 0x060001FC RID: 508 RVA: 0x000044E4 File Offset: 0x000026E4
		public int? UserID
		{
			get
			{
				return this._userID;
			}
			set
			{
				this._userID = value;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060001FD RID: 509 RVA: 0x000044F0 File Offset: 0x000026F0
		// (set) Token: 0x060001FE RID: 510 RVA: 0x00004508 File Offset: 0x00002708
		public string Reminder
		{
			get
			{
				return this._reminder;
			}
			set
			{
				this._reminder = value;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060001FF RID: 511 RVA: 0x00004514 File Offset: 0x00002714
		public string AppointmentOrAvailability
		{
			get
			{
				bool flag = string.IsNullOrEmpty(this.ID);
				string result;
				if (flag)
				{
					result = "";
				}
				else
				{
					result = (this.ID.StartsWith("-") ? "Availability" : "Appointment");
				}
				return result;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000200 RID: 512 RVA: 0x0000455C File Offset: 0x0000275C
		public bool IsAppointment
		{
			get
			{
				return this.ID != null && !this.ID.StartsWith("-");
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000201 RID: 513 RVA: 0x0000458C File Offset: 0x0000278C
		public string FormattedDate
		{
			get
			{
				return this.Start.ToString("ddd MMM d, yyyy");
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000202 RID: 514 RVA: 0x000045B4 File Offset: 0x000027B4
		public string FormattedTime
		{
			get
			{
				return this.Start.ToString("h:mm tt") + " to " + this.End.ToString("h:mm tt");
			}
		}

		// Token: 0x04000175 RID: 373
		private readonly string _id;

		// Token: 0x04000176 RID: 374
		private string _subject;

		// Token: 0x04000177 RID: 375
		private DateTime _start;

		// Token: 0x04000178 RID: 376
		private DateTime _end;

		// Token: 0x04000179 RID: 377
		private string _recurrenceRule;

		// Token: 0x0400017A RID: 378
		private string _recurrenceParentId;

		// Token: 0x0400017B RID: 379
		private string _reminder;

		// Token: 0x0400017C RID: 380
		private int? _userID;

		// Token: 0x0400017D RID: 381
		private int _colour;

		// Token: 0x0400017E RID: 382
		private static int ctr;
	}
}
