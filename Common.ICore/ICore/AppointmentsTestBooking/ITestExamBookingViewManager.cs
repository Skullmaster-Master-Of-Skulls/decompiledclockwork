using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.TestBookingViews;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.TestBookingViews.ViewEntities;

namespace TechnoPro.Common.ICore.AppointmentsTestBooking
{
	// Token: 0x020000CA RID: 202
	public interface ITestExamBookingViewManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000620 RID: 1568
		IList<TestBookingFull> LoadTestsFull(BookingsManagementContext context, DateTime? StartDate, DateTime? EndDate, bool HideCancelled, out IList<string> extendedColumnNames);

		// Token: 0x06000621 RID: 1569
		IList<TestBookingSmall> LoadTestsSmall(BookingsManagementContext context, DateTime? StartDate, DateTime? EndDate, bool HideCancelled, out IList<string> extendedColumnNames);

		// Token: 0x06000622 RID: 1570
		TestBookingFull LoadTestFullByAppId(BookingsManagementContext context, int AppId);

		// Token: 0x06000623 RID: 1571
		TestBookingSmall LoadTestSmallByAppId(BookingsManagementContext context, int AppId);

		// Token: 0x06000624 RID: 1572
		IList<TestBookingFull> LoadTestsFullByExamId(BookingsManagementContext context, int ExamId);

		// Token: 0x06000625 RID: 1573
		IList<TestBookingSmall> LoadTestsSmallByExamId(BookingsManagementContext context, int ExamId);

		// Token: 0x06000626 RID: 1574
		IList<ClassTestDefinitionSmall> LoadClassTestDefinitionsSmall(ClassTestDefinitionsManagementContext context, DateTime? StartDate, DateTime? EndDate, out IList<string> extendedColumnNames);

		// Token: 0x06000627 RID: 1575
		IList<UnbookedStudentsSmall> LoadUnbookedStudentsSmall(UnBookedStudentMmanagementContext context);

		// Token: 0x06000628 RID: 1576
		ClassTestDefinitionSmall LoadClassTestDefinitionSmallByExamId(ClassTestDefinitionsManagementContext context, int examId);

		// Token: 0x06000629 RID: 1577
		IList<TestBookingFull> LoadTestsFullByAppointmentIds(BookingsManagementContext context, params int[] appIds);

		// Token: 0x0600062A RID: 1578
		IList<TestBookingSmall> LoadTestsSmallByAppointmentIds(BookingsManagementContext context, params int[] appIds);

		// Token: 0x0600062B RID: 1579
		void SaveTestExamBookingLayoutToCentralizedSetting(eTestExamBookingGridViewType view, string layoutCompressed);

		// Token: 0x0600062C RID: 1580
		void ClearTestExamBookingLayoutInCentralizedSetting(eTestExamBookingGridViewType view);

		// Token: 0x0600062D RID: 1581
		IList<UnbookedTestExamStudent> LoadUnbookedTestExamStudents();
	}
}
