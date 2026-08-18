using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.Appointments
{
	// Token: 0x0200007F RID: 127
	public interface IAppointmentShowTimeAsTypeClientManager : IWebService
	{
		// Token: 0x060003A9 RID: 937
		IList<AppShowTimeAsTypeDTO> LoadAllShowTimeAsTypes();

		// Token: 0x060003AA RID: 938
		AppShowTimeAsTypeDTO LoadShowTimeAsTypeByAppCode(int AppCode);

		// Token: 0x060003AB RID: 939
		AppShowTimeAsTypeDTO LoadShowTimeAsTypeById(int AppCode);

		// Token: 0x060003AC RID: 940
		void DeleteShowTimeAsTypeByAppCode(int AppCode);

		// Token: 0x060003AD RID: 941
		void DeleteShowTimeAsTypeById(int AppointmentShowTimeAsId);

		// Token: 0x060003AE RID: 942
		void UpdateShowTimeAsType(AppShowTimeAsTypeDTO ShowTimeAsType);

		// Token: 0x060003AF RID: 943
		int CreateShowTimeAsType(AppShowTimeAsTypeDTO ShowTimeAsType);
	}
}
