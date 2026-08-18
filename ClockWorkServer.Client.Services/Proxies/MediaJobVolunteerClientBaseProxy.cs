using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000011 RID: 17
	internal class MediaJobVolunteerClientBaseProxy : ClientBase<IMediaJobVolunteer>, IMediaJobVolunteer, IService
	{
		// Token: 0x060000E6 RID: 230 RVA: 0x00004744 File Offset: 0x00002944
		public MediaJobVolunteerClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x0000474F File Offset: 0x0000294F
		public MediaJobVolunteerClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x0000475C File Offset: 0x0000295C
		public GetAllMediaJobVolunteersResp GetAllMediaJobVolunteers(GetAllMediaJobVolunteersReq request)
		{
			return base.Channel.GetAllMediaJobVolunteers(request);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x0000477C File Offset: 0x0000297C
		public AddMediaJobVolunteerResp AddMediaJobVolunteer(AddMediaJobVolunteerReq request)
		{
			return base.Channel.AddMediaJobVolunteer(request);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x0000479C File Offset: 0x0000299C
		public UpdateMediaJobVolunteerResp UpdateMediaJobVolunteer(UpdateMediaJobVolunteerReq request)
		{
			return base.Channel.UpdateMediaJobVolunteer(request);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x000047BC File Offset: 0x000029BC
		public DeleteMediaJobVolunteerResp DeleteMediaJobVolunteer(DeleteMediaJobVolunteerReq request)
		{
			return base.Channel.DeleteMediaJobVolunteer(request);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000047DC File Offset: 0x000029DC
		public GetMediaVolunteerByIdResp GetMediaVolunteerById(GetMediaVolunteerByIdReq request)
		{
			return base.Channel.GetMediaVolunteerById(request);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000047FC File Offset: 0x000029FC
		public GetMediaVolunteerByPersonIdResp GetMediaVolunteerByPersonId(GetMediaVolunteerByPersonIdReq request)
		{
			return base.Channel.GetMediaVolunteerByPersonId(request);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x0000481C File Offset: 0x00002A1C
		public GetMediaVolunteerByVolunteerAndJobResp GetMediaVolunteerByVolunteerAndJob(GetMediaVolunteerByVolunteerAndJobReq request)
		{
			return base.Channel.GetMediaVolunteerByVolunteerAndJob(request);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x0000483C File Offset: 0x00002A3C
		public GetMediaVolunteersAssignedToMediaJobResp GetMediaVolunteersAssignedToMediaJob(GetMediaVolunteersAssignedToMediaJobReq request)
		{
			return base.Channel.GetMediaVolunteersAssignedToMediaJob(request);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x0000485C File Offset: 0x00002A5C
		public GetMediaJobVolunteerInfoByVolunteerResp GetMediaJobVolunteerInfoByVolunteer(GetMediaJobVolunteerInfoByVolunteerReq request)
		{
			return base.Channel.GetMediaJobVolunteerInfoByVolunteer(request);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x0000487C File Offset: 0x00002A7C
		public CreateMediaJobVolunteerResp CreateMediaJobVolunteer(CreateMediaJobVolunteerReq request)
		{
			return base.Channel.CreateMediaJobVolunteer(request);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x0000489C File Offset: 0x00002A9C
		public ChangeMediaJobVolunteerNotesResp ChangeMediaJobVolunteerNotes(ChangeMediaJobVolunteerNotesReq request)
		{
			return base.Channel.ChangeMediaJobVolunteerNotes(request);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000048BC File Offset: 0x00002ABC
		public ChangeMediaJobVolunteerActiveStatusResp ChangeMediaJobVolunteerActiveStatus(ChangeMediaJobVolunteerActiveStatusReq request)
		{
			return base.Channel.ChangeMediaJobVolunteerActiveStatus(request);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x000048DC File Offset: 0x00002ADC
		public ChangeMediaJobVolunteerListActiveStatusResp ChangeMediaJobVolunteerListActiveStatus(ChangeMediaJobVolunteerListActiveStatusReq request)
		{
			return base.Channel.ChangeMediaJobVolunteerListActiveStatus(request);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000048FC File Offset: 0x00002AFC
		public GetMediaJobVolunteerWorkingHoursByVolunteerAndJobResp GetMediaJobVolunteerWorkingHoursByVolunteerAndJob(GetMediaJobVolunteerWorkingHoursByVolunteerAndJobReq request)
		{
			return base.Channel.GetMediaJobVolunteerWorkingHoursByVolunteerAndJob(request);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x0000491C File Offset: 0x00002B1C
		public GetAllMediaJobVolunteerWorkingHoursByVolunteerIdResp GetAllMediaJobVolunteerWorkingHoursByVolunteerId(GetAllMediaJobVolunteerWorkingHoursByVolunteerIdReq request)
		{
			return base.Channel.GetAllMediaJobVolunteerWorkingHoursByVolunteerId(request);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x0000493C File Offset: 0x00002B3C
		public AddMediaJobVolunteerWorkingHoursResp AddMediaJobVolunteerWorkingHours(AddMediaJobVolunteerWorkingHoursReq request)
		{
			return base.Channel.AddMediaJobVolunteerWorkingHours(request);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x0000495C File Offset: 0x00002B5C
		public UpdateMediaJobVolunteerWorkingHoursResp UpdateMediaJobVolunteerWorkingHours(UpdateMediaJobVolunteerWorkingHoursReq request)
		{
			return base.Channel.UpdateMediaJobVolunteerWorkingHours(request);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x0000497C File Offset: 0x00002B7C
		public DeleteMediaJobVolunteerWorkingHoursResp DeleteMediaJobVolunteerWorkingHours(DeleteMediaJobVolunteerWorkingHoursReq request)
		{
			return base.Channel.DeleteMediaJobVolunteerWorkingHours(request);
		}
	}
}
