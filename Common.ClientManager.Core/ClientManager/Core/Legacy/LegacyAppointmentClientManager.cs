using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.Appointment;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Legacy;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.Legacy
{
	// Token: 0x02000049 RID: 73
	public class LegacyAppointmentClientManager : ILegacyAppointmentClientManager
	{
		// Token: 0x060002A0 RID: 672 RVA: 0x0000BFA0 File Offset: 0x0000A1A0
		public IList<AppointmentModifiedHistoryItemDTO> LoadAsAppointmentModifiedHistory(int AppointmentId)
		{
			LoadAsAppointmentModifiedHistoryReq loadAsAppointmentModifiedHistoryReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAsAppointmentModifiedHistoryReq>();
			loadAsAppointmentModifiedHistoryReq.AppointmentId = AppointmentId;
			LoadAsAppointmentModifiedHistoryResp loadAsAppointmentModifiedHistoryResp = ClientServiceFactory.GetClientInstance<ILegacyAppointment>().LoadAsAppointmentModifiedHistory(loadAsAppointmentModifiedHistoryReq);
			return (loadAsAppointmentModifiedHistoryResp != null) ? loadAsAppointmentModifiedHistoryResp.AppointmentHistoryItems : null;
		}
	}
}
