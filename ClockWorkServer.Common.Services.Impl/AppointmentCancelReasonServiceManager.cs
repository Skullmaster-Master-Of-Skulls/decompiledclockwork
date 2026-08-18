using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters;
using TechnoPro.Common.Core.Appointments;
using TechnoPro.Common.Core.Mappers.Appointments;
using TechnoPro.Common.DataStructure.Tree;
using TechnoPro.Common.ICore.Appointments;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Appointments;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x0200001F RID: 31
	public class AppointmentCancelReasonServiceManager : IAppointmentCancelReason, IService
	{
		// Token: 0x06000163 RID: 355 RVA: 0x00007680 File Offset: 0x00005880
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00007694 File Offset: 0x00005894
		public LoadCancelReasonsResp LoadCancelReasons(LoadCancelReasonsReq Request)
		{
			IAppointmentCancelReasonManager appointmentCancelReasonManager = new AppointmentCancelReasonManager(Request.GetOperationContext());
			Forest<AppCancelReasonOrGroup> forest = appointmentCancelReasonManager.LoadCancelReasons();
			return new LoadCancelReasonsResp
			{
				Forest = ((forest == null) ? null : forest.ToDTO())
			};
		}

		// Token: 0x06000165 RID: 357 RVA: 0x000076D4 File Offset: 0x000058D4
		public LoadAllCancelReasonsResp LoadAllCancelReasons(LoadAllCancelReasonsReq Request)
		{
			IAppointmentCancelReasonManager appointmentCancelReasonManager = new AppointmentCancelReasonManager(Request.GetOperationContext());
			IList<AppCancelReason> list = appointmentCancelReasonManager.LoadAllCancelReasons();
			LoadAllCancelReasonsResp loadAllCancelReasonsResp = new LoadAllCancelReasonsResp();
			IList<AppCancelReasonDTO> appCancelReasons;
			if (list != null)
			{
				appCancelReasons = list.ToList<AppCancelReason>().ConvertAll<AppCancelReasonDTO>((AppCancelReason f) => f.ToDTO());
			}
			else
			{
				appCancelReasons = null;
			}
			loadAllCancelReasonsResp.AppCancelReasons = appCancelReasons;
			return loadAllCancelReasonsResp;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00007738 File Offset: 0x00005938
		public LoadCancelReasonByIdResp LoadCancelReasonById(LoadCancelReasonByIdReq Request)
		{
			IAppointmentCancelReasonManager appointmentCancelReasonManager = new AppointmentCancelReasonManager(Request.GetOperationContext());
			AppCancelReason appCancelReason = appointmentCancelReasonManager.LoadCancelReasonById(Request.CancelReasonId);
			return new LoadCancelReasonByIdResp
			{
				AppCancelReason = ((appCancelReason == null) ? null : appCancelReason.ToDTO())
			};
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000777C File Offset: 0x0000597C
		public void DeleteCancelReason(DeleteCancelReasonReq Request)
		{
			IAppointmentCancelReasonManager appointmentCancelReasonManager = new AppointmentCancelReasonManager(Request.GetOperationContext());
			appointmentCancelReasonManager.DeleteCancelReason(Request.CancelReasonId);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x000077A4 File Offset: 0x000059A4
		public void UpdateCancelReason(UpdateCancelReasonReq Request)
		{
			IAppointmentCancelReasonManager appointmentCancelReasonManager = new AppointmentCancelReasonManager(Request.GetOperationContext());
			appointmentCancelReasonManager.UpdateCancelReason(Request.CancelReason.ToDomainObject());
		}

		// Token: 0x06000169 RID: 361 RVA: 0x000077D0 File Offset: 0x000059D0
		public CreateCancelReasonResp CreateCancelReason(CreateCancelReasonReq Request)
		{
			IAppointmentCancelReasonManager appointmentCancelReasonManager = new AppointmentCancelReasonManager(Request.GetOperationContext());
			int cancelReasonId = appointmentCancelReasonManager.CreateCancelReason(Request.AppCancelReason.ToDomainObject());
			return new CreateCancelReasonResp
			{
				CancelReasonId = cancelReasonId
			};
		}
	}
}
