using System;
using System.Linq;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Tutoring
{
	// Token: 0x02000193 RID: 403
	[DataContract(Namespace = "http://tpro.ca")]
	public class TutorAppointmentDTO : BaseExtendedAppointmentDTO
	{
		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000974 RID: 2420 RVA: 0x00004384 File Offset: 0x00002584
		public AttendeeDTO Tutor
		{
			get
			{
				bool flag = this.Attendees == null;
				AttendeeDTO result;
				if (flag)
				{
					result = null;
				}
				else
				{
					result = this.Attendees.FirstOrDefault((AttendeeDTO g) => g.Person.CoreGroup == eCoreGroupDTO.Tutors);
				}
				return result;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000975 RID: 2421 RVA: 0x000043D4 File Offset: 0x000025D4
		public AttendeeDTO Student
		{
			get
			{
				bool flag = this.Attendees == null;
				AttendeeDTO result;
				if (flag)
				{
					result = null;
				}
				else
				{
					result = this.Attendees.FirstOrDefault((AttendeeDTO g) => g.Person.CoreGroup == eCoreGroupDTO.Students);
				}
				return result;
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000976 RID: 2422 RVA: 0x00004424 File Offset: 0x00002624
		public string StudentNoteToTutor
		{
			get
			{
				return this.Memo ?? "";
			}
		}
	}
}
