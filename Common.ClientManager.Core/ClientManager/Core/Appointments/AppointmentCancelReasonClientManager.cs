using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Appointments;
using TechnoPro.Common.DataStructure.Tree;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.Appointments
{
	// Token: 0x02000083 RID: 131
	public class AppointmentCancelReasonClientManager : IAppointmentCancelReasonClientManager, IWebService
	{
		// Token: 0x060004A9 RID: 1193 RVA: 0x0001548C File Offset: 0x0001368C
		public Forest<AppCancelReasonOrGroupDTO> LoadCancelReasons()
		{
			LoadCancelReasonsReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadCancelReasonsReq>();
			return ClientServiceFactory.GetClientInstance<IAppointmentCancelReason>().LoadCancelReasons(request).Forest;
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x000154BC File Offset: 0x000136BC
		public IList<AppCancelReasonDTO> LoadAllCancelReasons()
		{
			LoadAllCancelReasonsReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAllCancelReasonsReq>();
			return ClientServiceFactory.GetClientInstance<IAppointmentCancelReason>().LoadAllCancelReasons(request).AppCancelReasons;
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x000154EC File Offset: 0x000136EC
		public AppCancelReasonDTO LoadCancelReasonById(int CancelReasonId)
		{
			LoadCancelReasonByIdReq loadCancelReasonByIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadCancelReasonByIdReq>();
			loadCancelReasonByIdReq.CancelReasonId = CancelReasonId;
			return ClientServiceFactory.GetClientInstance<IAppointmentCancelReason>().LoadCancelReasonById(loadCancelReasonByIdReq).AppCancelReason;
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x00015524 File Offset: 0x00013724
		public void DeleteCancelReason(int CancelReasonId)
		{
			DeleteCancelReasonReq deleteCancelReasonReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteCancelReasonReq>();
			deleteCancelReasonReq.CancelReasonId = CancelReasonId;
			ClientServiceFactory.GetClientInstance<IAppointmentCancelReason>().DeleteCancelReason(deleteCancelReasonReq);
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x00015554 File Offset: 0x00013754
		public void UpdateCancelReason(AppCancelReasonDTO CancelReason)
		{
			UpdateCancelReasonReq updateCancelReasonReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateCancelReasonReq>();
			updateCancelReasonReq.CancelReason = CancelReason;
			ClientServiceFactory.GetClientInstance<IAppointmentCancelReason>().UpdateCancelReason(updateCancelReasonReq);
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x00015584 File Offset: 0x00013784
		public int CreateCancelReason(AppCancelReasonDTO CancelReason)
		{
			CreateCancelReasonReq createCancelReasonReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateCancelReasonReq>();
			createCancelReasonReq.AppCancelReason = CancelReason;
			return ClientServiceFactory.GetClientInstance<IAppointmentCancelReason>().CreateCancelReason(createCancelReasonReq).CancelReasonId;
		}
	}
}
