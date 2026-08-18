using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AlternativeFormat;

namespace TechnoPro.Common.ICore.AlternativeFormat
{
	// Token: 0x020000F4 RID: 244
	public interface IMediaVolunteerManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060007CF RID: 1999
		IList<AlternateFormatVolunteer> GetAllMediaJobVolunteers();

		// Token: 0x060007D0 RID: 2000
		int AddMediaJobVolunteer(AlternateFormatVolunteer volunteer);

		// Token: 0x060007D1 RID: 2001
		void UpdateMediaJobVolunteer(AlternateFormatVolunteer volunteer);

		// Token: 0x060007D2 RID: 2002
		void DeleteMediaJobVolunteer(int vPersonId);

		// Token: 0x060007D3 RID: 2003
		AlternateFormatVolunteer GetMediaVolunteerByPersonId(int personId);

		// Token: 0x060007D4 RID: 2004
		MediaJobVolunteerInfo GetMediaVolunteerById(int jobVolunteerId);

		// Token: 0x060007D5 RID: 2005
		MediaJobVolunteerInfo GetMediaVolunteerByVolunteerAndJob(int volunteerId, int mediaJobId);

		// Token: 0x060007D6 RID: 2006
		IList<MediaJobVolunteerInfo> GetMediaVolunteersAssignedToMediaJob(int mediaJobId);

		// Token: 0x060007D7 RID: 2007
		IList<MediaJobVolunteerInfo> GetMediaJobVolunteerInfoByVolunteer(int volunteerId);

		// Token: 0x060007D8 RID: 2008
		int CreateMediaJobVolunteer(MediaJobVolunteerInfo mediaJobVolunteer);

		// Token: 0x060007D9 RID: 2009
		void ChangeMediaJobVolunteerNotes(int volunteerId, int mediaJobId, string newNotes);

		// Token: 0x060007DA RID: 2010
		void ChangeMediaJobVolunteerActiveStatus(int volunteerId, int mediaJobId, bool isActive);

		// Token: 0x060007DB RID: 2011
		void ChangeMediaJobVolunteerActiveStatus(int jobVolunteerId, bool isActive);

		// Token: 0x060007DC RID: 2012
		void ChangeMediaJobVolunteerActiveStatus(IList<int> jobVolunteerIdList, bool isActive);

		// Token: 0x060007DD RID: 2013
		IList<MediaJobVolunteerWorkingHoursInfo> GetMediaJobVolunteerWorkingHoursByVolunteerAndJob(int volunteerId, int mediaJobId);

		// Token: 0x060007DE RID: 2014
		IList<MediaJobVolunteerWorkingHoursInfo> GetAllMediaJobVolunteerWorkingHoursByVolunteerId(int volunteerId);

		// Token: 0x060007DF RID: 2015
		int AddMediaJobVolunteerWorkingHours(MediaJobVolunteerWorkingHoursInfo volunteerWorkingHours);

		// Token: 0x060007E0 RID: 2016
		void UpdateMediaJobVolunteerWorkingHours(MediaJobVolunteerWorkingHoursInfo volunteerWorkingHours);

		// Token: 0x060007E1 RID: 2017
		void DeleteMediaJobVolunteerWorkingHours(int jobVolunteerWorkingHoursId);
	}
}
