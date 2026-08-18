using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.Appointments
{
	// Token: 0x0200007E RID: 126
	public interface IAppointmentIconClientManager : IWebService
	{
		// Token: 0x060003A1 RID: 929
		IList<AppointmentIconDTO> LoadAppointmentIconsByAppointment(int AppointmentId);

		// Token: 0x060003A2 RID: 930
		AppointmentIconDTO LoadAppointmentIcon(int AppointmentId, int IconNum);

		// Token: 0x060003A3 RID: 931
		AppointmentIconDTO LoadAppointmentIcon(int IconInfoId);

		// Token: 0x060003A4 RID: 932
		AppointmentIconDTO LoadAppointmentIconByIconNum(int IconNum);

		// Token: 0x060003A5 RID: 933
		void DeleteAppointmentIconsNotInList(int AppointmentId, IList<int> IconNums);

		// Token: 0x060003A6 RID: 934
		int InsertOrUpdateAppointmentIcon(int AppointmentId, AppointmentIconDTO icon);

		// Token: 0x060003A7 RID: 935
		void DeleteAppointmentIcon(int AppointmentId, int IconNum);

		// Token: 0x060003A8 RID: 936
		IList<IconInfoDTO> LoadAllIconInfos();
	}
}
