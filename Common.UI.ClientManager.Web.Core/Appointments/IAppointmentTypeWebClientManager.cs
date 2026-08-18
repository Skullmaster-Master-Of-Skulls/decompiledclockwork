using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;

namespace TechnoPro.Common.UI.ClientManager.Web.Core.Appointments
{
	// Token: 0x02000019 RID: 25
	public interface IAppointmentTypeWebClientManager
	{
		// Token: 0x06000074 RID: 116
		IList<AppTypeDTO> LoadAllowedAppTypes();
	}
}
