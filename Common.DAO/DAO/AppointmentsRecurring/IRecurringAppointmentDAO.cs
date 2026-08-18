using System;
using System.Collections.Generic;
using System.Data.Common;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentsRecurring;

namespace TechnoPro.Common.DAO.AppointmentsRecurring
{
	// Token: 0x020000A1 RID: 161
	public interface IRecurringAppointmentDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600042E RID: 1070
		AppointmentRecurringInfo LoadCurrentRecurringAppointmentsSet(int MasterGroupCode);

		// Token: 0x0600042F RID: 1071
		void UpdateRecurringGroupCode(int AppointmentId, int GroupCode, DbTransaction transaction = null);

		// Token: 0x06000430 RID: 1072
		IDictionary<int, bool> LoadAppointmentsInARecurringSetWithPermissionsToEditForASpecificUser(int AppointmentId, int PersonId);
	}
}
