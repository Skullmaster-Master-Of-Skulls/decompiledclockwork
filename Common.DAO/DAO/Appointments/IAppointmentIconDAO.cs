using System;
using System.Collections.Generic;
using System.Data.Common;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Appointments;

namespace TechnoPro.Common.DAO.Appointments
{
	// Token: 0x020000A7 RID: 167
	public interface IAppointmentIconDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000450 RID: 1104
		IList<AppointmentIcon> LoadAppointmentIconsByAppointment(int AppointmentId);

		// Token: 0x06000451 RID: 1105
		AppointmentIcon LoadAppointmentIcon(int AppointmentId, int IconNum);

		// Token: 0x06000452 RID: 1106
		AppointmentIcon LoadAppointmentIcon(int IconInfoId);

		// Token: 0x06000453 RID: 1107
		AppointmentIcon LoadAppointmentIconByIconNum(int IconNum);

		// Token: 0x06000454 RID: 1108
		void DeleteAppointmentIconsNotInList(int AppointmentId, IList<int> IconNums, DbTransaction transaction = null);

		// Token: 0x06000455 RID: 1109
		int InsertOrUpdateAppointmentIcon(int AppointmentId, AppointmentIcon icon, DbTransaction transaction = null);

		// Token: 0x06000456 RID: 1110
		void DeleteAppointmentIcon(int AppointmentId, int IconNum, DbTransaction transaction = null);

		// Token: 0x06000457 RID: 1111
		IDictionary<int, IList<AppointmentIcon>> LoadAppointmentIconsByAppointments(int[] appointmentIds);
	}
}
