using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Appointments;

namespace TechnoPro.Common.ICore.Appointments
{
	// Token: 0x020000E2 RID: 226
	public interface IAppointmentIconManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000700 RID: 1792
		IList<AppointmentIcon> LoadAppointmentIconsByAppointment(int AppointmentId);

		// Token: 0x06000701 RID: 1793
		AppointmentIcon LoadAppointmentIcon(int AppointmentId, int IconNum);

		// Token: 0x06000702 RID: 1794
		AppointmentIcon LoadAppointmentIcon(int IconInfoId);

		// Token: 0x06000703 RID: 1795
		AppointmentIcon LoadAppointmentIconByIconNum(int IconNum);

		// Token: 0x06000704 RID: 1796
		void DeleteAppointmentIconsNotInList(bool runInTransaction, int AppointmentId, IList<int> IconNums);

		// Token: 0x06000705 RID: 1797
		int InsertOrUpdateAppointmentIcon(bool runInTransaction, int AppointmentId, AppointmentIcon icon);

		// Token: 0x06000706 RID: 1798
		void DeleteAppointmentIcon(bool runInTransaction, int AppointmentId, int IconNum);

		// Token: 0x06000707 RID: 1799
		IList<IconInfo> LoadAllIconInfos();
	}
}
