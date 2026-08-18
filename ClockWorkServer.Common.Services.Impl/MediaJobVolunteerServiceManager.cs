using System;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.Common.Core.AlternativeFormat;
using TechnoPro.Common.Core.Mappers.AlternativeFormat;
using TechnoPro.Common.ICore.AlternativeFormat;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000008 RID: 8
	public class MediaJobVolunteerServiceManager : IMediaJobVolunteer, IService
	{
		// Token: 0x06000059 RID: 89 RVA: 0x00003560 File Offset: 0x00001760
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003574 File Offset: 0x00001774
		public GetAllMediaJobVolunteersResp GetAllMediaJobVolunteers(GetAllMediaJobVolunteersReq request)
		{
			IMediaVolunteerManager mediaVolunteerManager = new MediaVolunteerManager(request.GetOperationContext());
			return new GetAllMediaJobVolunteersResp
			{
				Volunteers = mediaVolunteerManager.GetAllMediaJobVolunteers().ToDTO()
			};
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000035AC File Offset: 0x000017AC
		public AddMediaJobVolunteerResp AddMediaJobVolunteer(AddMediaJobVolunteerReq request)
		{
			IMediaVolunteerManager mediaVolunteerManager = new MediaVolunteerManager(request.GetOperationContext());
			return new AddMediaJobVolunteerResp
			{
				VolunteerId = mediaVolunteerManager.AddMediaJobVolunteer(request.Volunteer.ToDomainObject())
			};
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000035E8 File Offset: 0x000017E8
		public UpdateMediaJobVolunteerResp UpdateMediaJobVolunteer(UpdateMediaJobVolunteerReq request)
		{
			IMediaVolunteerManager mediaVolunteerManager = new MediaVolunteerManager(request.GetOperationContext());
			mediaVolunteerManager.UpdateMediaJobVolunteer(request.Volunteer.ToDomainObject());
			return new UpdateMediaJobVolunteerResp();
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003620 File Offset: 0x00001820
		public DeleteMediaJobVolunteerResp DeleteMediaJobVolunteer(DeleteMediaJobVolunteerReq request)
		{
			IMediaVolunteerManager mediaVolunteerManager = new MediaVolunteerManager(request.GetOperationContext());
			mediaVolunteerManager.DeleteMediaJobVolunteer(request.VolunteerId);
			return new DeleteMediaJobVolunteerResp();
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003650 File Offset: 0x00001850
		public GetMediaVolunteerByIdResp GetMediaVolunteerById(GetMediaVolunteerByIdReq request)
		{
			IMediaVolunteerManager mediaVolunteerManager = new MediaVolunteerManager(request.GetOperationContext());
			return new GetMediaVolunteerByIdResp
			{
				MediaJobVolunteer = mediaVolunteerManager.GetMediaVolunteerById(request.JobVolunteerId).ToDTO()
			};
		}

		// Token: 0x0600005F RID: 95 RVA: 0x0000368C File Offset: 0x0000188C
		public GetMediaVolunteerByPersonIdResp GetMediaVolunteerByPersonId(GetMediaVolunteerByPersonIdReq request)
		{
			IMediaVolunteerManager mediaVolunteerManager = new MediaVolunteerManager(request.GetOperationContext());
			return new GetMediaVolunteerByPersonIdResp
			{
				MediaVolunteer = mediaVolunteerManager.GetMediaVolunteerByPersonId(request.PersonId).ToDTO()
			};
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000036C8 File Offset: 0x000018C8
		public GetMediaVolunteerByVolunteerAndJobResp GetMediaVolunteerByVolunteerAndJob(GetMediaVolunteerByVolunteerAndJobReq request)
		{
			IMediaVolunteerManager mediaVolunteerManager = new MediaVolunteerManager(request.GetOperationContext());
			return new GetMediaVolunteerByVolunteerAndJobResp
			{
				MediaJobVolunteer = mediaVolunteerManager.GetMediaVolunteerByVolunteerAndJob(request.VolunteerId, request.MediaJobId).ToDTO()
			};
		}

		// Token: 0x06000061 RID: 97 RVA: 0x0000370C File Offset: 0x0000190C
		public GetMediaVolunteersAssignedToMediaJobResp GetMediaVolunteersAssignedToMediaJob(GetMediaVolunteersAssignedToMediaJobReq request)
		{
			IMediaVolunteerManager mediaVolunteerManager = new MediaVolunteerManager(request.GetOperationContext());
			return new GetMediaVolunteersAssignedToMediaJobResp
			{
				MediaJobVolunteerList = mediaVolunteerManager.GetMediaVolunteersAssignedToMediaJob(request.MediaJobId).ToDTO()
			};
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003748 File Offset: 0x00001948
		public GetMediaJobVolunteerInfoByVolunteerResp GetMediaJobVolunteerInfoByVolunteer(GetMediaJobVolunteerInfoByVolunteerReq request)
		{
			IMediaVolunteerManager mediaVolunteerManager = new MediaVolunteerManager(request.GetOperationContext());
			return new GetMediaJobVolunteerInfoByVolunteerResp
			{
				MediaJobVolunteerList = mediaVolunteerManager.GetMediaJobVolunteerInfoByVolunteer(request.VolunteerId).ToDTO()
			};
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003784 File Offset: 0x00001984
		public CreateMediaJobVolunteerResp CreateMediaJobVolunteer(CreateMediaJobVolunteerReq request)
		{
			IMediaVolunteerManager mediaVolunteerManager = new MediaVolunteerManager(request.GetOperationContext());
			return new CreateMediaJobVolunteerResp
			{
				MediaJobId = mediaVolunteerManager.CreateMediaJobVolunteer(request.MediaJobVolunteer.ToDomainObject())
			};
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000037C0 File Offset: 0x000019C0
		public ChangeMediaJobVolunteerNotesResp ChangeMediaJobVolunteerNotes(ChangeMediaJobVolunteerNotesReq request)
		{
			IMediaVolunteerManager mediaVolunteerManager = new MediaVolunteerManager(request.GetOperationContext());
			mediaVolunteerManager.ChangeMediaJobVolunteerNotes(request.VolunteerId, request.MediaJobId, request.Notes);
			return new ChangeMediaJobVolunteerNotesResp();
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000037FC File Offset: 0x000019FC
		public ChangeMediaJobVolunteerActiveStatusResp ChangeMediaJobVolunteerActiveStatus(ChangeMediaJobVolunteerActiveStatusReq request)
		{
			IMediaVolunteerManager mediaVolunteerManager = new MediaVolunteerManager(request.GetOperationContext());
			bool flag = request.JobVolunteerId > 0;
			if (flag)
			{
				mediaVolunteerManager.ChangeMediaJobVolunteerActiveStatus(request.JobVolunteerId, request.IsActive);
			}
			else
			{
				mediaVolunteerManager.ChangeMediaJobVolunteerActiveStatus(request.VolunteerId, request.MediaJobId, request.IsActive);
			}
			return new ChangeMediaJobVolunteerActiveStatusResp();
		}

		// Token: 0x06000066 RID: 102 RVA: 0x0000385C File Offset: 0x00001A5C
		public ChangeMediaJobVolunteerListActiveStatusResp ChangeMediaJobVolunteerListActiveStatus(ChangeMediaJobVolunteerListActiveStatusReq request)
		{
			IMediaVolunteerManager mediaVolunteerManager = new MediaVolunteerManager(request.GetOperationContext());
			mediaVolunteerManager.ChangeMediaJobVolunteerActiveStatus(request.JobVolunteerIdList, request.IsActive);
			return new ChangeMediaJobVolunteerListActiveStatusResp();
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003894 File Offset: 0x00001A94
		public GetMediaJobVolunteerWorkingHoursByVolunteerAndJobResp GetMediaJobVolunteerWorkingHoursByVolunteerAndJob(GetMediaJobVolunteerWorkingHoursByVolunteerAndJobReq request)
		{
			IMediaVolunteerManager mediaVolunteerManager = new MediaVolunteerManager(request.GetOperationContext());
			return new GetMediaJobVolunteerWorkingHoursByVolunteerAndJobResp
			{
				MediaJobVolunteerWorkingHoursList = mediaVolunteerManager.GetMediaJobVolunteerWorkingHoursByVolunteerAndJob(request.VolunteerId, request.MediaJobId).ToDTO()
			};
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000038D8 File Offset: 0x00001AD8
		public GetAllMediaJobVolunteerWorkingHoursByVolunteerIdResp GetAllMediaJobVolunteerWorkingHoursByVolunteerId(GetAllMediaJobVolunteerWorkingHoursByVolunteerIdReq request)
		{
			IMediaVolunteerManager mediaVolunteerManager = new MediaVolunteerManager(request.GetOperationContext());
			return new GetAllMediaJobVolunteerWorkingHoursByVolunteerIdResp
			{
				MediaJobVolunteerWorkingHoursList = mediaVolunteerManager.GetAllMediaJobVolunteerWorkingHoursByVolunteerId(request.VolunteerId).ToDTO()
			};
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003914 File Offset: 0x00001B14
		public AddMediaJobVolunteerWorkingHoursResp AddMediaJobVolunteerWorkingHours(AddMediaJobVolunteerWorkingHoursReq request)
		{
			IMediaVolunteerManager mediaVolunteerManager = new MediaVolunteerManager(request.GetOperationContext());
			return new AddMediaJobVolunteerWorkingHoursResp
			{
				MediaJobVolunteerWorkingHoursInfoId = mediaVolunteerManager.AddMediaJobVolunteerWorkingHours(request.MediaJobVolunteerWorkingHours.ToDomainObject())
			};
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003950 File Offset: 0x00001B50
		public UpdateMediaJobVolunteerWorkingHoursResp UpdateMediaJobVolunteerWorkingHours(UpdateMediaJobVolunteerWorkingHoursReq request)
		{
			IMediaVolunteerManager mediaVolunteerManager = new MediaVolunteerManager(request.GetOperationContext());
			mediaVolunteerManager.UpdateMediaJobVolunteerWorkingHours(request.MediaJobVolunteerWorkingHours.ToDomainObject());
			return new UpdateMediaJobVolunteerWorkingHoursResp();
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003988 File Offset: 0x00001B88
		public DeleteMediaJobVolunteerWorkingHoursResp DeleteMediaJobVolunteerWorkingHours(DeleteMediaJobVolunteerWorkingHoursReq request)
		{
			IMediaVolunteerManager mediaVolunteerManager = new MediaVolunteerManager(request.GetOperationContext());
			mediaVolunteerManager.DeleteMediaJobVolunteerWorkingHours(request.JobVolunteerWorkingHoursInfoId);
			return new DeleteMediaJobVolunteerWorkingHoursResp();
		}
	}
}
