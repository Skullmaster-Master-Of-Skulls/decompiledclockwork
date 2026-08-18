using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.AlternateFormat
{
	// Token: 0x0200009F RID: 159
	public interface IMediaVolunteerClientManager : IWebService
	{
		// Token: 0x06000509 RID: 1289
		IList<AlternateFormatVolunteerDTO> GetAllMediaJobVolunteers();

		// Token: 0x0600050A RID: 1290
		int AddMediaJobVolunteer(AlternateFormatVolunteerDTO volunteer);

		// Token: 0x0600050B RID: 1291
		void UpdateMediaJobVolunteer(AlternateFormatVolunteerDTO volunteer);

		// Token: 0x0600050C RID: 1292
		void DeleteMediaJobVolunteer(int vPersonId);

		// Token: 0x0600050D RID: 1293
		AlternateFormatVolunteerDTO GetMediaVolunteerByPersonId(int personId);

		// Token: 0x0600050E RID: 1294
		MediaJobVolunteerInfoDTO GetMediaVolunteerById(int jobVolunteerId);

		// Token: 0x0600050F RID: 1295
		MediaJobVolunteerInfoDTO GetMediaVolunteerByVolunteerAndJob(int volunteerId, int mediaJobId);

		// Token: 0x06000510 RID: 1296
		IList<MediaJobVolunteerInfoDTO> GetMediaVolunteersAssignedToMediaJob(int mediaJobId);

		// Token: 0x06000511 RID: 1297
		IList<MediaJobVolunteerInfoDTO> GetMediaJobVolunteerInfoByVolunteer(int volunteerId);

		// Token: 0x06000512 RID: 1298
		int CreateMediaJobVolunteer(MediaJobVolunteerInfoDTO mediaJobVolunteer);

		// Token: 0x06000513 RID: 1299
		void ChangeMediaJobVolunteerNotes(int volunteerId, int mediaJobId, string newNotes);

		// Token: 0x06000514 RID: 1300
		void ChangeMediaJobVolunteerActiveStatus(int volunteerId, int mediaJobId, bool isActive);

		// Token: 0x06000515 RID: 1301
		void ChangeMediaJobVolunteerActiveStatus(int jobVolunteerId, bool isActive);

		// Token: 0x06000516 RID: 1302
		void ChangeMediaJobVolunteerListActiveStatus(IList<int> jobVolunteerIdList, bool isActive);

		// Token: 0x06000517 RID: 1303
		IList<MediaJobVolunteerWorkingHoursInfoDTO> GetMediaJobVolunteerWorkingHoursByVolunteerAndJob(int volunteerId, int mediaJobId);

		// Token: 0x06000518 RID: 1304
		IList<MediaJobVolunteerWorkingHoursInfoDTO> GetAllMediaJobVolunteerWorkingHoursByVolunteerId(int volunteerId);

		// Token: 0x06000519 RID: 1305
		int AddMediaJobVolunteerWorkingHours(MediaJobVolunteerWorkingHoursInfoDTO volunteerWorkingHours);

		// Token: 0x0600051A RID: 1306
		void UpdateMediaJobVolunteerWorkingHours(MediaJobVolunteerWorkingHoursInfoDTO volunteerWorkingHours);

		// Token: 0x0600051B RID: 1307
		void DeleteMediaJobVolunteerWorkingHours(int jobVolunteerWorkingHoursId);
	}
}
