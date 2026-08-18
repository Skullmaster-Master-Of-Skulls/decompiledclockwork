using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.OnlineForms;
using TechnoPro.Common.Core.Mappers.OnlineForms;
using TechnoPro.Common.Core.OnlineForms;
using TechnoPro.Common.ICore.OnlineForms;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.OnlineForms;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000071 RID: 113
	public class OnlineFormServiceManager : IOnlineForm, IService
	{
		// Token: 0x06000437 RID: 1079 RVA: 0x00014090 File Offset: 0x00012290
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x000140A4 File Offset: 0x000122A4
		public GetAllOnlineFormsResp GetAllOnlineForms(GetAllOnlineFormsReq request)
		{
			IOnlineFormManager onlineFormManager = new OnlineFormManager(request.GetOperationContext());
			List<OnlineForm> allOnlineForms = onlineFormManager.GetAllOnlineForms();
			List<OnlineFormDTO> onlineForms = allOnlineForms.ConvertAll<OnlineFormDTO>((OnlineForm s) => s.ToDTO());
			return new GetAllOnlineFormsResp
			{
				OnlineForms = onlineForms
			};
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x000140FC File Offset: 0x000122FC
		public GetOnlineFormResp GetOnlineForm(GetOnlineFormReq request)
		{
			IOnlineFormManager onlineFormManager = new OnlineFormManager(request.GetOperationContext());
			OnlineForm onlineForm = onlineFormManager.GetOnlineForm(request.OnlineFormId);
			return new GetOnlineFormResp
			{
				OnlineForm = onlineForm.ToDTO()
			};
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x0001413C File Offset: 0x0001233C
		public void DeleteOnlineForm(DeleteOnlineFormReq Request)
		{
			IOnlineFormManager onlineFormManager = new OnlineFormManager(Request.GetOperationContext());
			onlineFormManager.DeleteOnlineForm(Request.OnlineFormId);
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x00014164 File Offset: 0x00012364
		public void UpdateOnlineForm(UpdateOnlineFormReq request)
		{
			IOnlineFormManager onlineFormManager = new OnlineFormManager(request.GetOperationContext());
			onlineFormManager.UpdateOnlineForm(request.OnlineForm.ToDomainObject());
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x00014190 File Offset: 0x00012390
		public CreateNewOnlineFormResp CreateNewOnlineForm(CreateNewOnlineFormReq request)
		{
			IOnlineFormManager onlineFormManager = new OnlineFormManager(request.GetOperationContext());
			int onlineFormId = onlineFormManager.CreateOnlineForm(request.OnlineForm.ToDomainObject());
			return new CreateNewOnlineFormResp
			{
				OnlineFormId = onlineFormId
			};
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x000141D0 File Offset: 0x000123D0
		public void DisableOnlineForm(DisableOnlineFormReq Request)
		{
			IOnlineFormManager onlineFormManager = new OnlineFormManager(Request.GetOperationContext());
			onlineFormManager.DisableOnlineForm(Request.OnlineFormId);
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x000141F8 File Offset: 0x000123F8
		public void EnableOnlineForm(EnableOnlineFormReq Request)
		{
			IOnlineFormManager onlineFormManager = new OnlineFormManager(Request.GetOperationContext());
			onlineFormManager.EnableOnlineForm(Request.OnlineFormId);
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x00014220 File Offset: 0x00012420
		public GetActiveOnlineFormsResp GetActiveOnlineForms(GetActiveOnlineFormsReq request)
		{
			IOnlineFormManager onlineFormManager = new OnlineFormManager(request.GetOperationContext());
			GetActiveOnlineFormsResp getActiveOnlineFormsResp = new GetActiveOnlineFormsResp();
			List<OnlineForm> activeOnlineForms = onlineFormManager.GetActiveOnlineForms();
			List<OnlineFormDTO> onlineForms;
			if (activeOnlineForms == null)
			{
				onlineForms = null;
			}
			else
			{
				onlineForms = (from g in activeOnlineForms
				select g.ToDTO()).ToList<OnlineFormDTO>();
			}
			getActiveOnlineFormsResp.OnlineForms = onlineForms;
			return getActiveOnlineFormsResp;
		}
	}
}
