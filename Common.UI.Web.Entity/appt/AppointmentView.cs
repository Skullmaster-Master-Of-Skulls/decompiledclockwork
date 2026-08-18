using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Contracts.DTO.Adapters;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar;

namespace TechnoPro.Common.UI.Web.Entity.appt
{
	// Token: 0x02000042 RID: 66
	[Serializable]
	public class AppointmentView
	{
		// Token: 0x06000194 RID: 404 RVA: 0x00002221 File Offset: 0x00000421
		public AppointmentView()
		{
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00003A38 File Offset: 0x00001C38
		public AppointmentView(AppointmentDTO app)
		{
			this.AppointmentId = app.AppointmentId;
			this.StartDateTime = app.StartDateTime;
			this.EndDateTime = app.EndDateTime;
			this.Title = app.GetTitleAndSubtitle();
			this.Attendees = app.Attendees.ConvertAll<AttendeeView>((AttendeeDTO g) => new AttendeeView(g)).ToList<AttendeeView>();
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00003AB8 File Offset: 0x00001CB8
		public AppointmentView(AvailabilityScheduleItemDTO availabilityItem, IList<AttendeeView> users)
		{
			this.IsAvailability = true;
			this.AppointmentId = availabilityItem.AvailabilityScheduleId;
			this.PrimaryAttendeeID = availabilityItem.Context.PersonId.ToString();
			this.StartDateTime = availabilityItem.StartDateTime;
			this.EndDateTime = availabilityItem.EndDateTime;
			List<AttendeeView> attendees;
			if (users != null)
			{
				(attendees = new List<AttendeeView>()).Add(users.FirstOrDefault((AttendeeView h) => h.PersonId == availabilityItem.Context.PersonId));
			}
			else
			{
				attendees = new List<AttendeeView>();
			}
			this.Attendees = attendees;
			this.Title = "Available";
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00003B74 File Offset: 0x00001D74
		// (set) Token: 0x06000198 RID: 408 RVA: 0x00003BAF File Offset: 0x00001DAF
		public string ID
		{
			get
			{
				bool flag = !string.IsNullOrEmpty(this.id);
				string result;
				if (flag)
				{
					result = this.id;
				}
				else
				{
					result = this.AppointmentId.ToString();
				}
				return result;
			}
			set
			{
				this.id = value;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00003BB9 File Offset: 0x00001DB9
		// (set) Token: 0x0600019A RID: 410 RVA: 0x00003BC1 File Offset: 0x00001DC1
		public int AppointmentId { get; set; }

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600019B RID: 411 RVA: 0x00003BCA File Offset: 0x00001DCA
		// (set) Token: 0x0600019C RID: 412 RVA: 0x00003BD2 File Offset: 0x00001DD2
		public string Title { get; set; }

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600019D RID: 413 RVA: 0x00003BDB File Offset: 0x00001DDB
		// (set) Token: 0x0600019E RID: 414 RVA: 0x00003BE3 File Offset: 0x00001DE3
		public DateTime StartDateTime { get; set; }

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600019F RID: 415 RVA: 0x00003BEC File Offset: 0x00001DEC
		// (set) Token: 0x060001A0 RID: 416 RVA: 0x00003BF4 File Offset: 0x00001DF4
		public DateTime EndDateTime { get; set; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x00003C00 File Offset: 0x00001E00
		public string DateFormatted
		{
			get
			{
				return this.StartDateTime.ToString("ddd MMM d, yyyy");
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00003C28 File Offset: 0x00001E28
		public string TimeFormatted
		{
			get
			{
				return this.StartDateTime.ToString("h:mm tt") + " to " + this.EndDateTime.ToString("h:mm tt");
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x00003C6C File Offset: 0x00001E6C
		public string FirstAttendeeName
		{
			get
			{
				bool flag = this.Attendees == null || this.Attendees.Count < 1;
				string result;
				if (flag)
				{
					result = "";
				}
				else
				{
					result = (this.Attendees[0].Name ?? "");
				}
				return result;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00003CC0 File Offset: 0x00001EC0
		public int FirstAttendeePersonId
		{
			get
			{
				bool flag = this.Attendees == null || this.Attendees.Count < 1;
				int result;
				if (flag)
				{
					result = 0;
				}
				else
				{
					result = this.Attendees[0].PersonId;
				}
				return result;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x00003D04 File Offset: 0x00001F04
		private AttendeeView WithWho
		{
			get
			{
				IList<AttendeeView> attendees = this.Attendees;
				bool flag = attendees == null || attendees.Count < 1;
				AttendeeView result;
				if (flag)
				{
					result = null;
				}
				else
				{
					result = attendees.FirstOrDefault((AttendeeView g) => !g.IsStudentRoomOrResource);
				}
				return result;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x00003D5C File Offset: 0x00001F5C
		public string WithWhoName
		{
			get
			{
				AttendeeView withWho = this.WithWho;
				return (withWho == null) ? "" : (withWho.Name ?? "");
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x00003D90 File Offset: 0x00001F90
		public int WithWhoPersonId
		{
			get
			{
				AttendeeView withWho = this.WithWho;
				return (withWho == null) ? 0 : withWho.PersonId;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x00003DB5 File Offset: 0x00001FB5
		// (set) Token: 0x060001A9 RID: 425 RVA: 0x00003DBD File Offset: 0x00001FBD
		public IList<AttendeeView> Attendees { get; set; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00003DC8 File Offset: 0x00001FC8
		public IList<string> AttendeeIDs
		{
			get
			{
				bool flag = this.Attendees == null;
				IList<string> result;
				if (flag)
				{
					result = new List<string>();
				}
				else
				{
					result = this.Attendees.ToList<AttendeeView>().ConvertAll<string>((AttendeeView g) => g.ID);
				}
				return result;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060001AB RID: 427 RVA: 0x00003E1E File Offset: 0x0000201E
		// (set) Token: 0x060001AC RID: 428 RVA: 0x00003E26 File Offset: 0x00002026
		public string PrimaryAttendeeID { get; set; }

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060001AD RID: 429 RVA: 0x00003E2F File Offset: 0x0000202F
		// (set) Token: 0x060001AE RID: 430 RVA: 0x00003E37 File Offset: 0x00002037
		public bool IsAvailability { get; set; }

		// Token: 0x0400014C RID: 332
		private string id;
	}
}
