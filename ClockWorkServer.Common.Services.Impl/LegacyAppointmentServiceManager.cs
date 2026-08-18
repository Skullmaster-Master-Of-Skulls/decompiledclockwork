using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.Appointment;
using TechnoPro.Common.Core.Legacy;
using TechnoPro.Common.Core.Mappers.Legacy.Appointment;
using TechnoPro.Common.ICore.Legacy;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Legacy.Appointment;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x0200005A RID: 90
	public class LegacyAppointmentServiceManager : ILegacyAppointment, IService
	{
		// Token: 0x06000359 RID: 857 RVA: 0x0000FC60 File Offset: 0x0000DE60
		public LoadAsAppointmentModifiedHistoryResp LoadAsAppointmentModifiedHistory(LoadAsAppointmentModifiedHistoryReq Request)
		{
			ILegacyAppointmentManager legacyAppointmentManager = new LegacyAppointmentManager(Request.GetOperationContext());
			IList<AppointmentModifiedHistoryItem> list = legacyAppointmentManager.LoadAppointmentModifiedHistory(Request.AppointmentId);
			LoadAsAppointmentModifiedHistoryResp loadAsAppointmentModifiedHistoryResp = new LoadAsAppointmentModifiedHistoryResp();
			IList<AppointmentModifiedHistoryItemDTO> appointmentHistoryItems;
			if (list == null)
			{
				appointmentHistoryItems = null;
			}
			else
			{
				appointmentHistoryItems = (from g in list
				select g.ToDTO()).ToList<AppointmentModifiedHistoryItemDTO>();
			}
			loadAsAppointmentModifiedHistoryResp.AppointmentHistoryItems = appointmentHistoryItems;
			return loadAsAppointmentModifiedHistoryResp;
		}
	}
}
