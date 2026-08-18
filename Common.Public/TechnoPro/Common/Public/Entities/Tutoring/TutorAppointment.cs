using System;
using System.Linq;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.Tutoring
{
	// Token: 0x0200015C RID: 348
	public class TutorAppointment : BaseExtendedAppointment
	{
		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000849 RID: 2121 RVA: 0x000119B4 File Offset: 0x0000FBB4
		public Attendee Tutor
		{
			get
			{
				bool flag = this.Attendees == null;
				Attendee result;
				if (flag)
				{
					result = null;
				}
				else
				{
					result = this.Attendees.FirstOrDefault((Attendee g) => g.Person.CoreGroup == eCoreGroup.Tutors);
				}
				return result;
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x0600084A RID: 2122 RVA: 0x00011A04 File Offset: 0x0000FC04
		public Attendee Student
		{
			get
			{
				bool flag = this.Attendees == null;
				Attendee result;
				if (flag)
				{
					result = null;
				}
				else
				{
					result = this.Attendees.FirstOrDefault((Attendee g) => g.Person.CoreGroup == eCoreGroup.Students);
				}
				return result;
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x0600084B RID: 2123 RVA: 0x00011A54 File Offset: 0x0000FC54
		public string StudentNoteToTutor
		{
			get
			{
				return this.Memo ?? "";
			}
		}
	}
}
