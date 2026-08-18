using System;
using System.Collections.Generic;
using NewBooker.Entities.AutoTestBooking.Booker2;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.DAO.AppointmentsTestBooking
{
	// Token: 0x020000B7 RID: 183
	public interface IAutoBooker2DAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060004E0 RID: 1248
		bool DoesStudentHaveAnExistingTestWithClassDateMatching(int pid, int lucid, DateTime classDate);

		// Token: 0x060004E1 RID: 1249
		IList<TryToBookAvailability> LoadStudentAppointments(int pid, DateTime date, int AppIdToIgnoreWhenCheckingStudentsSchedule);

		// Token: 0x060004E2 RID: 1250
		int GetNumberOfTestsAndExamsStudentHasInADay(int pid, int lucid, DateTime date);
	}
}
