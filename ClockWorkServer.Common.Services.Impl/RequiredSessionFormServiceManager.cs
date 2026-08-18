using System;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.RequiredSessionForm;
using TechnoPro.Common.Core.RequiredSessionForm;
using TechnoPro.Common.ICore.RequiredSessionForm;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x0200007D RID: 125
	public class RequiredSessionFormServiceManager : IRequiredSessionForm, IService
	{
		// Token: 0x060004B7 RID: 1207 RVA: 0x000167B8 File Offset: 0x000149B8
		public LoadInfoPmIdForCurrentSessionResp LoadInfoPmIdForCurrentSession(LoadInfoPmIdForCurrentSessionReq Request)
		{
			IRequiredSessionFormManager requiredSessionFormManager = new RequiredSessionFormManager(Request.GetOperationContext());
			int infoPmId = requiredSessionFormManager.LoadInfoPmIdForCurrentSession(Request.StudentPersonId, Request.ScreenNum);
			return new LoadInfoPmIdForCurrentSessionResp
			{
				InfoPmId = infoPmId
			};
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x000167F8 File Offset: 0x000149F8
		public LoadInfoPmIdForSessionResp LoadInfoPmIdForSession(LoadInfoPmIdForSessionReq Request)
		{
			IRequiredSessionFormManager requiredSessionFormManager = new RequiredSessionFormManager(Request.GetOperationContext());
			int infoPmId = requiredSessionFormManager.LoadInfoPmIdForSession(Request.StudentPersonId, Request.ScreenNum, Request.DateInSession);
			return new LoadInfoPmIdForSessionResp
			{
				InfoPmId = infoPmId
			};
		}
	}
}
