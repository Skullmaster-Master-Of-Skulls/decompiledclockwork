using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000010 RID: 16
	public class MediaJobVolunteerReusableClientProxy : WCFTokenBasedReusableClientProxy<IMediaJobVolunteer>, IMediaJobVolunteer, IService
	{
		// Token: 0x060000D2 RID: 210 RVA: 0x0000433A File Offset: 0x0000253A
		public MediaJobVolunteerReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00004345 File Offset: 0x00002545
		public MediaJobVolunteerReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004354 File Offset: 0x00002554
		public GetAllMediaJobVolunteersResp GetAllMediaJobVolunteers(GetAllMediaJobVolunteersReq request)
		{
			return this.WrapServiceMethod<GetAllMediaJobVolunteersResp>(() => this.Proxy.GetAllMediaJobVolunteers(request));
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x0000438C File Offset: 0x0000258C
		public AddMediaJobVolunteerResp AddMediaJobVolunteer(AddMediaJobVolunteerReq request)
		{
			return this.WrapServiceMethod<AddMediaJobVolunteerResp>(() => this.Proxy.AddMediaJobVolunteer(request));
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x000043C4 File Offset: 0x000025C4
		public UpdateMediaJobVolunteerResp UpdateMediaJobVolunteer(UpdateMediaJobVolunteerReq request)
		{
			return this.WrapServiceMethod<UpdateMediaJobVolunteerResp>(() => this.Proxy.UpdateMediaJobVolunteer(request));
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x000043FC File Offset: 0x000025FC
		public DeleteMediaJobVolunteerResp DeleteMediaJobVolunteer(DeleteMediaJobVolunteerReq request)
		{
			return this.WrapServiceMethod<DeleteMediaJobVolunteerResp>(() => this.Proxy.DeleteMediaJobVolunteer(request));
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00004434 File Offset: 0x00002634
		public GetMediaVolunteerByIdResp GetMediaVolunteerById(GetMediaVolunteerByIdReq request)
		{
			return this.WrapServiceMethod<GetMediaVolunteerByIdResp>(() => this.Proxy.GetMediaVolunteerById(request));
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x0000446C File Offset: 0x0000266C
		public GetMediaVolunteerByPersonIdResp GetMediaVolunteerByPersonId(GetMediaVolunteerByPersonIdReq request)
		{
			return this.WrapServiceMethod<GetMediaVolunteerByPersonIdResp>(() => this.Proxy.GetMediaVolunteerByPersonId(request));
		}

		// Token: 0x060000DA RID: 218 RVA: 0x000044A4 File Offset: 0x000026A4
		public GetMediaVolunteerByVolunteerAndJobResp GetMediaVolunteerByVolunteerAndJob(GetMediaVolunteerByVolunteerAndJobReq request)
		{
			return this.WrapServiceMethod<GetMediaVolunteerByVolunteerAndJobResp>(() => this.Proxy.GetMediaVolunteerByVolunteerAndJob(request));
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000044DC File Offset: 0x000026DC
		public GetMediaVolunteersAssignedToMediaJobResp GetMediaVolunteersAssignedToMediaJob(GetMediaVolunteersAssignedToMediaJobReq request)
		{
			return this.WrapServiceMethod<GetMediaVolunteersAssignedToMediaJobResp>(() => this.Proxy.GetMediaVolunteersAssignedToMediaJob(request));
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00004514 File Offset: 0x00002714
		public GetMediaJobVolunteerInfoByVolunteerResp GetMediaJobVolunteerInfoByVolunteer(GetMediaJobVolunteerInfoByVolunteerReq request)
		{
			return this.WrapServiceMethod<GetMediaJobVolunteerInfoByVolunteerResp>(() => this.Proxy.GetMediaJobVolunteerInfoByVolunteer(request));
		}

		// Token: 0x060000DD RID: 221 RVA: 0x0000454C File Offset: 0x0000274C
		public CreateMediaJobVolunteerResp CreateMediaJobVolunteer(CreateMediaJobVolunteerReq request)
		{
			return this.WrapServiceMethod<CreateMediaJobVolunteerResp>(() => this.Proxy.CreateMediaJobVolunteer(request));
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00004584 File Offset: 0x00002784
		public ChangeMediaJobVolunteerNotesResp ChangeMediaJobVolunteerNotes(ChangeMediaJobVolunteerNotesReq request)
		{
			return this.WrapServiceMethod<ChangeMediaJobVolunteerNotesResp>(() => this.Proxy.ChangeMediaJobVolunteerNotes(request));
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000045BC File Offset: 0x000027BC
		public ChangeMediaJobVolunteerActiveStatusResp ChangeMediaJobVolunteerActiveStatus(ChangeMediaJobVolunteerActiveStatusReq request)
		{
			return this.WrapServiceMethod<ChangeMediaJobVolunteerActiveStatusResp>(() => this.Proxy.ChangeMediaJobVolunteerActiveStatus(request));
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000045F4 File Offset: 0x000027F4
		public ChangeMediaJobVolunteerListActiveStatusResp ChangeMediaJobVolunteerListActiveStatus(ChangeMediaJobVolunteerListActiveStatusReq request)
		{
			return this.WrapServiceMethod<ChangeMediaJobVolunteerListActiveStatusResp>(() => this.Proxy.ChangeMediaJobVolunteerListActiveStatus(request));
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x0000462C File Offset: 0x0000282C
		public GetMediaJobVolunteerWorkingHoursByVolunteerAndJobResp GetMediaJobVolunteerWorkingHoursByVolunteerAndJob(GetMediaJobVolunteerWorkingHoursByVolunteerAndJobReq request)
		{
			return this.WrapServiceMethod<GetMediaJobVolunteerWorkingHoursByVolunteerAndJobResp>(() => this.Proxy.GetMediaJobVolunteerWorkingHoursByVolunteerAndJob(request));
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00004664 File Offset: 0x00002864
		public GetAllMediaJobVolunteerWorkingHoursByVolunteerIdResp GetAllMediaJobVolunteerWorkingHoursByVolunteerId(GetAllMediaJobVolunteerWorkingHoursByVolunteerIdReq request)
		{
			return this.WrapServiceMethod<GetAllMediaJobVolunteerWorkingHoursByVolunteerIdResp>(() => this.Proxy.GetAllMediaJobVolunteerWorkingHoursByVolunteerId(request));
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x0000469C File Offset: 0x0000289C
		public AddMediaJobVolunteerWorkingHoursResp AddMediaJobVolunteerWorkingHours(AddMediaJobVolunteerWorkingHoursReq request)
		{
			return this.WrapServiceMethod<AddMediaJobVolunteerWorkingHoursResp>(() => this.Proxy.AddMediaJobVolunteerWorkingHours(request));
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x000046D4 File Offset: 0x000028D4
		public UpdateMediaJobVolunteerWorkingHoursResp UpdateMediaJobVolunteerWorkingHours(UpdateMediaJobVolunteerWorkingHoursReq request)
		{
			return this.WrapServiceMethod<UpdateMediaJobVolunteerWorkingHoursResp>(() => this.Proxy.UpdateMediaJobVolunteerWorkingHours(request));
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x0000470C File Offset: 0x0000290C
		public DeleteMediaJobVolunteerWorkingHoursResp DeleteMediaJobVolunteerWorkingHours(DeleteMediaJobVolunteerWorkingHoursReq request)
		{
			return this.WrapServiceMethod<DeleteMediaJobVolunteerWorkingHoursResp>(() => this.Proxy.DeleteMediaJobVolunteerWorkingHours(request));
		}
	}
}
