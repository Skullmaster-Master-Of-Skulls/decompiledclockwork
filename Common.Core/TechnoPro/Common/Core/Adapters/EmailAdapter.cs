using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.AppointmentsPointOfContact;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Entities.TPMailMan;

namespace TechnoPro.Common.Core.Adapters
{
	// Token: 0x0200016F RID: 367
	public static class EmailAdapter
	{
		// Token: 0x0600103A RID: 4154 RVA: 0x0007703C File Offset: 0x0007523C
		public static PointOfContact ConvertToPointOfContact(this TPMailMessage Email, int WhoBookedPersonId, int StudentPersonId, int StaffPersonId, Dictionary<string, int> attachmentFileIds = null)
		{
			PointOfContact pointOfContact = new PointOfContact();
			object staff;
			if (StaffPersonId <= 0)
			{
				staff = null;
			}
			else
			{
				(staff = new Attendee()).Person = new PersonBase
				{
					PersonId = StaffPersonId
				};
			}
			pointOfContact.Staff = staff;
			pointOfContact.Student = new Attendee
			{
				IsNoShow = false,
				Person = new PersonBase
				{
					PersonId = StudentPersonId
				}
			};
			pointOfContact.StartDateTime = DateTime.Now.Date;
			object whoBooked;
			if (WhoBookedPersonId <= 0)
			{
				whoBooked = null;
			}
			else
			{
				(whoBooked = new PersonBase()).PersonId = WhoBookedPersonId;
			}
			pointOfContact.WhoBooked = whoBooked;
			pointOfContact.SessionNotesData = new List<DynamicData>();
			PointOfContact pointOfContact2 = pointOfContact;
			string memo = Email.ConvertEmailToRichText(attachmentFileIds);
			pointOfContact2.Memo = memo;
			return pointOfContact2;
		}
	}
}
