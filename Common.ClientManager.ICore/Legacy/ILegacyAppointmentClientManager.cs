using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.Appointment;

namespace TechnoPro.Common.ClientManager.ICore.Legacy
{
	// Token: 0x02000044 RID: 68
	public interface ILegacyAppointmentClientManager
	{
		// Token: 0x060001EA RID: 490
		IList<AppointmentModifiedHistoryItemDTO> LoadAsAppointmentModifiedHistory(int AppointmentId);
	}
}
