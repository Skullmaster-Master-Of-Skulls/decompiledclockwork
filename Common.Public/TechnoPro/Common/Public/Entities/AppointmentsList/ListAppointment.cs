using System;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.AppointmentsList
{
	// Token: 0x02000558 RID: 1368
	public class ListAppointment : BaseExtendedAppointment
	{
		// Token: 0x17001274 RID: 4724
		// (get) Token: 0x06002C07 RID: 11271 RVA: 0x00031123 File Offset: 0x0002F323
		// (set) Token: 0x06002C08 RID: 11272 RVA: 0x0003112B File Offset: 0x0002F32B
		public bool IsStudentsFirstApp { get; set; }

		// Token: 0x17001275 RID: 4725
		// (get) Token: 0x06002C09 RID: 11273 RVA: 0x00031134 File Offset: 0x0002F334
		public int WhoBookedPersonId
		{
			get
			{
				return (this.WhoBooked == null) ? 0 : this.WhoBooked.PersonId;
			}
		}

		// Token: 0x17001276 RID: 4726
		// (get) Token: 0x06002C0A RID: 11274 RVA: 0x0003115C File Offset: 0x0002F35C
		// (set) Token: 0x06002C0B RID: 11275 RVA: 0x00031188 File Offset: 0x0002F388
		public bool IsIn
		{
			get
			{
				Attendee attendee = this.FindAttendeeByCoreGroup(eCoreGroup.Students);
				return attendee != null && attendee.MiscCode == 2;
			}
			set
			{
				Attendee attendee = this.FindAttendeeByCoreGroup(eCoreGroup.Students);
				bool flag = attendee != null;
				if (flag)
				{
					attendee.MiscCode = (value ? 2 : 0);
				}
			}
		}

		// Token: 0x17001277 RID: 4727
		// (get) Token: 0x06002C0C RID: 11276 RVA: 0x000311B4 File Offset: 0x0002F3B4
		// (set) Token: 0x06002C0D RID: 11277 RVA: 0x000311E0 File Offset: 0x0002F3E0
		public bool IsConfirmed
		{
			get
			{
				Attendee attendee = this.FindAttendeeByCoreGroup(eCoreGroup.Students);
				return attendee != null && attendee.MiscCode == 4;
			}
			set
			{
				Attendee attendee = this.FindAttendeeByCoreGroup(eCoreGroup.Students);
				bool flag = attendee != null;
				if (flag)
				{
					bool flag2 = attendee.MiscCode < 1 || (!value && attendee.MiscCode == 4);
					if (flag2)
					{
						attendee.MiscCode = (value ? 4 : 0);
					}
				}
			}
		}

		// Token: 0x17001278 RID: 4728
		// (get) Token: 0x06002C0E RID: 11278 RVA: 0x0003122C File Offset: 0x0002F42C
		// (set) Token: 0x06002C0F RID: 11279 RVA: 0x00031258 File Offset: 0x0002F458
		public bool IsNoShow
		{
			get
			{
				Attendee attendee = this.FindAttendeeByCoreGroup(eCoreGroup.Students);
				bool flag = attendee == null;
				return !flag && attendee.IsNoShow;
			}
			set
			{
				Attendee attendee = this.FindAttendeeByCoreGroup(eCoreGroup.Students);
				bool flag = attendee != null;
				if (flag)
				{
					attendee.IsNoShow = value;
				}
			}
		}

		// Token: 0x06002C10 RID: 11280 RVA: 0x00031280 File Offset: 0x0002F480
		private Attendee FindAttendeeByCoreGroup(eCoreGroup coreGroup)
		{
			bool flag = this.Attendees != null;
			if (flag)
			{
				Attendee attendee = this.Attendees.Find((Attendee f) => f.Person.CoreGroup == coreGroup);
				bool flag2 = attendee != null;
				if (flag2)
				{
					return attendee;
				}
			}
			return null;
		}

		// Token: 0x17001279 RID: 4729
		// (get) Token: 0x06002C11 RID: 11281 RVA: 0x000312D8 File Offset: 0x0002F4D8
		// (set) Token: 0x06002C12 RID: 11282 RVA: 0x00031300 File Offset: 0x0002F500
		public PersonBase Student
		{
			get
			{
				Attendee attendee = this.FindAttendeeByCoreGroup(eCoreGroup.Students);
				return (attendee == null) ? null : attendee.Person;
			}
			set
			{
				Attendee attendee = this.FindAttendeeByCoreGroup(eCoreGroup.Students);
				bool flag = attendee != null;
				Attendee attendee2;
				if (flag)
				{
					attendee2 = attendee;
				}
				else
				{
					attendee2 = new Attendee();
					this.Attendees.Add(attendee2);
				}
				attendee2.Person = value;
			}
		}

		// Token: 0x1700127A RID: 4730
		// (get) Token: 0x06002C13 RID: 11283 RVA: 0x00031340 File Offset: 0x0002F540
		// (set) Token: 0x06002C14 RID: 11284 RVA: 0x00031368 File Offset: 0x0002F568
		public PersonBase Staff
		{
			get
			{
				Attendee attendee = this.FindAttendeeByCoreGroup(eCoreGroup.Staff);
				return (attendee == null) ? null : attendee.Person;
			}
			set
			{
				Attendee attendee = this.FindAttendeeByCoreGroup(eCoreGroup.Staff);
				bool flag = attendee != null;
				Attendee attendee2;
				if (flag)
				{
					attendee2 = attendee;
				}
				else
				{
					attendee2 = new Attendee();
					this.Attendees.Add(attendee2);
				}
				attendee2.Person = value;
			}
		}
	}
}
