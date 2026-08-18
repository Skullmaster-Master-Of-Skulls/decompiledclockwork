using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.AlternateFormat;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.AlternateFormat
{
	// Token: 0x020000A3 RID: 163
	public class MediaVolunteerClientManager : IMediaVolunteerClientManager, IWebService
	{
		// Token: 0x06000628 RID: 1576 RVA: 0x0001B258 File Offset: 0x00019458
		public IList<AlternateFormatVolunteerDTO> GetAllMediaJobVolunteers()
		{
			GetAllMediaJobVolunteersReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetAllMediaJobVolunteersReq>();
			return ClientServiceFactory.GetClientInstance<IMediaJobVolunteer>().GetAllMediaJobVolunteers(request).Volunteers;
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x0001B288 File Offset: 0x00019488
		public int AddMediaJobVolunteer(AlternateFormatVolunteerDTO volunteer)
		{
			AddMediaJobVolunteerReq addMediaJobVolunteerReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<AddMediaJobVolunteerReq>();
			addMediaJobVolunteerReq.Volunteer = volunteer;
			return ClientServiceFactory.GetClientInstance<IMediaJobVolunteer>().AddMediaJobVolunteer(addMediaJobVolunteerReq).VolunteerId;
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x0001B2C0 File Offset: 0x000194C0
		public void UpdateMediaJobVolunteer(AlternateFormatVolunteerDTO volunteer)
		{
			UpdateMediaJobVolunteerReq updateMediaJobVolunteerReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateMediaJobVolunteerReq>();
			updateMediaJobVolunteerReq.Volunteer = volunteer;
			ClientServiceFactory.GetClientInstance<IMediaJobVolunteer>().UpdateMediaJobVolunteer(updateMediaJobVolunteerReq);
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x0001B2F0 File Offset: 0x000194F0
		public void DeleteMediaJobVolunteer(int vPersonId)
		{
			DeleteMediaJobVolunteerReq deleteMediaJobVolunteerReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteMediaJobVolunteerReq>();
			deleteMediaJobVolunteerReq.VolunteerId = vPersonId;
			ClientServiceFactory.GetClientInstance<IMediaJobVolunteer>().DeleteMediaJobVolunteer(deleteMediaJobVolunteerReq);
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x0001B320 File Offset: 0x00019520
		public MediaJobVolunteerInfoDTO GetMediaVolunteerById(int jobVolunteerId)
		{
			GetMediaVolunteerByIdReq getMediaVolunteerByIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetMediaVolunteerByIdReq>();
			getMediaVolunteerByIdReq.JobVolunteerId = jobVolunteerId;
			return ClientServiceFactory.GetClientInstance<IMediaJobVolunteer>().GetMediaVolunteerById(getMediaVolunteerByIdReq).MediaJobVolunteer;
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x0001B358 File Offset: 0x00019558
		public AlternateFormatVolunteerDTO GetMediaVolunteerByPersonId(int personId)
		{
			GetMediaVolunteerByPersonIdReq getMediaVolunteerByPersonIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetMediaVolunteerByPersonIdReq>();
			getMediaVolunteerByPersonIdReq.PersonId = personId;
			return ClientServiceFactory.GetClientInstance<IMediaJobVolunteer>().GetMediaVolunteerByPersonId(getMediaVolunteerByPersonIdReq).MediaVolunteer;
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x0001B390 File Offset: 0x00019590
		public MediaJobVolunteerInfoDTO GetMediaVolunteerByVolunteerAndJob(int volunteerId, int mediaJobId)
		{
			GetMediaVolunteerByVolunteerAndJobReq getMediaVolunteerByVolunteerAndJobReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetMediaVolunteerByVolunteerAndJobReq>();
			getMediaVolunteerByVolunteerAndJobReq.VolunteerId = volunteerId;
			getMediaVolunteerByVolunteerAndJobReq.MediaJobId = mediaJobId;
			return ClientServiceFactory.GetClientInstance<IMediaJobVolunteer>().GetMediaVolunteerByVolunteerAndJob(getMediaVolunteerByVolunteerAndJobReq).MediaJobVolunteer;
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x0001B3D0 File Offset: 0x000195D0
		public IList<MediaJobVolunteerInfoDTO> GetMediaVolunteersAssignedToMediaJob(int mediaJobId)
		{
			GetMediaVolunteersAssignedToMediaJobReq getMediaVolunteersAssignedToMediaJobReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetMediaVolunteersAssignedToMediaJobReq>();
			getMediaVolunteersAssignedToMediaJobReq.MediaJobId = mediaJobId;
			return ClientServiceFactory.GetClientInstance<IMediaJobVolunteer>().GetMediaVolunteersAssignedToMediaJob(getMediaVolunteersAssignedToMediaJobReq).MediaJobVolunteerList;
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x0001B408 File Offset: 0x00019608
		public IList<MediaJobVolunteerInfoDTO> GetMediaJobVolunteerInfoByVolunteer(int volunteerId)
		{
			GetMediaJobVolunteerInfoByVolunteerReq getMediaJobVolunteerInfoByVolunteerReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetMediaJobVolunteerInfoByVolunteerReq>();
			getMediaJobVolunteerInfoByVolunteerReq.VolunteerId = volunteerId;
			return ClientServiceFactory.GetClientInstance<IMediaJobVolunteer>().GetMediaJobVolunteerInfoByVolunteer(getMediaJobVolunteerInfoByVolunteerReq).MediaJobVolunteerList;
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x0001B440 File Offset: 0x00019640
		public int CreateMediaJobVolunteer(MediaJobVolunteerInfoDTO mediaJobVolunteer)
		{
			CreateMediaJobVolunteerReq createMediaJobVolunteerReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateMediaJobVolunteerReq>();
			createMediaJobVolunteerReq.MediaJobVolunteer = mediaJobVolunteer;
			return ClientServiceFactory.GetClientInstance<IMediaJobVolunteer>().CreateMediaJobVolunteer(createMediaJobVolunteerReq).MediaJobId;
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x0001B478 File Offset: 0x00019678
		public void ChangeMediaJobVolunteerNotes(int volunteerId, int mediaJobId, string newNotes)
		{
			ChangeMediaJobVolunteerNotesReq changeMediaJobVolunteerNotesReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ChangeMediaJobVolunteerNotesReq>();
			changeMediaJobVolunteerNotesReq.VolunteerId = volunteerId;
			changeMediaJobVolunteerNotesReq.MediaJobId = mediaJobId;
			changeMediaJobVolunteerNotesReq.Notes = newNotes;
			ClientServiceFactory.GetClientInstance<IMediaJobVolunteer>().ChangeMediaJobVolunteerNotes(changeMediaJobVolunteerNotesReq);
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x0001B4B8 File Offset: 0x000196B8
		public void ChangeMediaJobVolunteerActiveStatus(int volunteerId, int mediaJobId, bool isActive)
		{
			ChangeMediaJobVolunteerActiveStatusReq changeMediaJobVolunteerActiveStatusReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ChangeMediaJobVolunteerActiveStatusReq>();
			changeMediaJobVolunteerActiveStatusReq.VolunteerId = volunteerId;
			changeMediaJobVolunteerActiveStatusReq.MediaJobId = mediaJobId;
			changeMediaJobVolunteerActiveStatusReq.IsActive = isActive;
			ClientServiceFactory.GetClientInstance<IMediaJobVolunteer>().ChangeMediaJobVolunteerActiveStatus(changeMediaJobVolunteerActiveStatusReq);
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x0001B4F8 File Offset: 0x000196F8
		public void ChangeMediaJobVolunteerActiveStatus(int jobVvolunteerId, bool isActive)
		{
			ChangeMediaJobVolunteerActiveStatusReq changeMediaJobVolunteerActiveStatusReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ChangeMediaJobVolunteerActiveStatusReq>();
			changeMediaJobVolunteerActiveStatusReq.JobVolunteerId = jobVvolunteerId;
			changeMediaJobVolunteerActiveStatusReq.IsActive = isActive;
			ClientServiceFactory.GetClientInstance<IMediaJobVolunteer>().ChangeMediaJobVolunteerActiveStatus(changeMediaJobVolunteerActiveStatusReq);
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x0001B530 File Offset: 0x00019730
		public void ChangeMediaJobVolunteerListActiveStatus(IList<int> jobVolunteerIdList, bool isActive)
		{
			ChangeMediaJobVolunteerListActiveStatusReq changeMediaJobVolunteerListActiveStatusReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ChangeMediaJobVolunteerListActiveStatusReq>();
			changeMediaJobVolunteerListActiveStatusReq.JobVolunteerIdList = jobVolunteerIdList;
			changeMediaJobVolunteerListActiveStatusReq.IsActive = isActive;
			ClientServiceFactory.GetClientInstance<IMediaJobVolunteer>().ChangeMediaJobVolunteerListActiveStatus(changeMediaJobVolunteerListActiveStatusReq);
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x0001B568 File Offset: 0x00019768
		public IList<MediaJobVolunteerWorkingHoursInfoDTO> GetMediaJobVolunteerWorkingHoursByVolunteerAndJob(int volunteerId, int mediaJobId)
		{
			GetMediaJobVolunteerWorkingHoursByVolunteerAndJobReq getMediaJobVolunteerWorkingHoursByVolunteerAndJobReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetMediaJobVolunteerWorkingHoursByVolunteerAndJobReq>();
			getMediaJobVolunteerWorkingHoursByVolunteerAndJobReq.VolunteerId = volunteerId;
			getMediaJobVolunteerWorkingHoursByVolunteerAndJobReq.MediaJobId = mediaJobId;
			return ClientServiceFactory.GetClientInstance<IMediaJobVolunteer>().GetMediaJobVolunteerWorkingHoursByVolunteerAndJob(getMediaJobVolunteerWorkingHoursByVolunteerAndJobReq).MediaJobVolunteerWorkingHoursList;
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x0001B5A8 File Offset: 0x000197A8
		public IList<MediaJobVolunteerWorkingHoursInfoDTO> GetAllMediaJobVolunteerWorkingHoursByVolunteerId(int volunteerId)
		{
			GetAllMediaJobVolunteerWorkingHoursByVolunteerIdReq getAllMediaJobVolunteerWorkingHoursByVolunteerIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetAllMediaJobVolunteerWorkingHoursByVolunteerIdReq>();
			getAllMediaJobVolunteerWorkingHoursByVolunteerIdReq.VolunteerId = volunteerId;
			return ClientServiceFactory.GetClientInstance<IMediaJobVolunteer>().GetAllMediaJobVolunteerWorkingHoursByVolunteerId(getAllMediaJobVolunteerWorkingHoursByVolunteerIdReq).MediaJobVolunteerWorkingHoursList;
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x0001B5E0 File Offset: 0x000197E0
		public int AddMediaJobVolunteerWorkingHours(MediaJobVolunteerWorkingHoursInfoDTO volunteerWorkingHours)
		{
			AddMediaJobVolunteerWorkingHoursReq addMediaJobVolunteerWorkingHoursReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<AddMediaJobVolunteerWorkingHoursReq>();
			addMediaJobVolunteerWorkingHoursReq.MediaJobVolunteerWorkingHours = volunteerWorkingHours;
			return ClientServiceFactory.GetClientInstance<IMediaJobVolunteer>().AddMediaJobVolunteerWorkingHours(addMediaJobVolunteerWorkingHoursReq).MediaJobVolunteerWorkingHoursInfoId;
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x0001B618 File Offset: 0x00019818
		public void UpdateMediaJobVolunteerWorkingHours(MediaJobVolunteerWorkingHoursInfoDTO volunteerWorkingHours)
		{
			UpdateMediaJobVolunteerWorkingHoursReq updateMediaJobVolunteerWorkingHoursReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateMediaJobVolunteerWorkingHoursReq>();
			updateMediaJobVolunteerWorkingHoursReq.MediaJobVolunteerWorkingHours = volunteerWorkingHours;
			ClientServiceFactory.GetClientInstance<IMediaJobVolunteer>().UpdateMediaJobVolunteerWorkingHours(updateMediaJobVolunteerWorkingHoursReq);
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x0001B648 File Offset: 0x00019848
		public void DeleteMediaJobVolunteerWorkingHours(int jobVolunteerWorkingHoursId)
		{
			DeleteMediaJobVolunteerWorkingHoursReq deleteMediaJobVolunteerWorkingHoursReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteMediaJobVolunteerWorkingHoursReq>();
			deleteMediaJobVolunteerWorkingHoursReq.JobVolunteerWorkingHoursInfoId = jobVolunteerWorkingHoursId;
			ClientServiceFactory.GetClientInstance<IMediaJobVolunteer>().DeleteMediaJobVolunteerWorkingHours(deleteMediaJobVolunteerWorkingHoursReq);
		}
	}
}
