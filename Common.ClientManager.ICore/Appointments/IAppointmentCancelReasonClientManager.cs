using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.Common.DataStructure.Tree;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.Appointments
{
	// Token: 0x0200007D RID: 125
	public interface IAppointmentCancelReasonClientManager : IWebService
	{
		// Token: 0x0600039B RID: 923
		Forest<AppCancelReasonOrGroupDTO> LoadCancelReasons();

		// Token: 0x0600039C RID: 924
		IList<AppCancelReasonDTO> LoadAllCancelReasons();

		// Token: 0x0600039D RID: 925
		AppCancelReasonDTO LoadCancelReasonById(int CancelReasonId);

		// Token: 0x0600039E RID: 926
		void DeleteCancelReason(int CancelReasonId);

		// Token: 0x0600039F RID: 927
		void UpdateCancelReason(AppCancelReasonDTO CancelReason);

		// Token: 0x060003A0 RID: 928
		int CreateCancelReason(AppCancelReasonDTO CancelReason);
	}
}
