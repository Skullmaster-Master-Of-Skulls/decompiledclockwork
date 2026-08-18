using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AlternativeFormat;

namespace TechnoPro.Common.DAO.AlternativeFormat
{
	// Token: 0x020000CE RID: 206
	public interface IMediaVolunteerDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060005E7 RID: 1511
		MediaJobVolunteerInfo GetMediaVolunteerById(int jobVolunteerId);

		// Token: 0x060005E8 RID: 1512
		MediaJobVolunteerInfo GetMediaVolunteerByVolunteerAndJob(int volunteerId, int mediaJobId);

		// Token: 0x060005E9 RID: 1513
		IList<MediaJobVolunteerInfo> GetMediaVolunteersAssignedToMediaJob(int mediaJobId);

		// Token: 0x060005EA RID: 1514
		IList<MediaJobVolunteerInfo> GetMediaJobVolunteerInfoByVolunteer(int volunteerId);

		// Token: 0x060005EB RID: 1515
		int CreateMediaJobVolunteer(MediaJobVolunteerInfo mediaJobVolunteer);

		// Token: 0x060005EC RID: 1516
		void ChangeMediaJobVolunteerNotes(int volunteerId, int mediaJobId, string newNotes);

		// Token: 0x060005ED RID: 1517
		void ChangeMediaJobVolunteerActiveStatus(int volunteerId, int mediaJobId, bool isActive);

		// Token: 0x060005EE RID: 1518
		void ChangeMediaJobVolunteerActiveStatus(int jobVolunteerId, bool isActive);

		// Token: 0x060005EF RID: 1519
		void ChangeMediaJobVolunteerActiveStatus(IList<int> jobVolunteerId, bool isActive);

		// Token: 0x060005F0 RID: 1520
		IList<MediaJobVolunteerWorkingHoursInfo> GetMediaJobVolunteerWorkingHoursByVolunteerAndJob(int volunteerId, int mediaJobId);

		// Token: 0x060005F1 RID: 1521
		IList<MediaJobVolunteerWorkingHoursInfo> GetAllMediaJobVolunteerWorkingHoursByVolunteerId(int volunteerId);

		// Token: 0x060005F2 RID: 1522
		int AddMediaJobVolunteerWorkingHours(MediaJobVolunteerWorkingHoursInfo volunteerWorkingHours);

		// Token: 0x060005F3 RID: 1523
		void UpdateMediaJobVolunteerWorkingHours(MediaJobVolunteerWorkingHoursInfo volunteerWorkingHours);

		// Token: 0x060005F4 RID: 1524
		void DeleteMediaJobVolunteerWorkingHours(int jobVolunteerWorkingHoursIdI);
	}
}
