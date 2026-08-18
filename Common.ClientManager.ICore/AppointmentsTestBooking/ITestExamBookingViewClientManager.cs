using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.TestBookingViews;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.TestBookingViews.ViewEntities;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.TestBookingViews;

namespace TechnoPro.Common.ClientManager.ICore.AppointmentsTestBooking
{
	// Token: 0x0200008B RID: 139
	public interface ITestExamBookingViewClientManager : IWebService
	{
		// Token: 0x06000428 RID: 1064
		LoadTestsFullResp LoadTestsFull(BookingsManagementContextDTO context, DateTime? StartDate, DateTime? EndDate, bool HideCancelled);

		// Token: 0x06000429 RID: 1065
		LoadTestsSmallResp LoadTestsSmall(BookingsManagementContextDTO context, DateTime? StartDate, DateTime? EndDate, bool HideCancelled);

		// Token: 0x0600042A RID: 1066
		LoadClassTestDefinitionsSmallResp LoadClassTestDefinitionsSmall(ClassTestDefinitionsManagementContextDTO context, DateTime? StartDate, DateTime? EndDate);

		// Token: 0x0600042B RID: 1067
		LoadUnbookedStudentsSmallResp LoadUnbookedStudentsSmall(UnBookedStudentMmanagementContextDTO context);

		// Token: 0x0600042C RID: 1068
		LoadTestFullByAppIdResp LoadTestFullByAppId(BookingsManagementContextDTO context, int appId);

		// Token: 0x0600042D RID: 1069
		LoadTestSmallByAppIdResp LoadTestSmallByAppId(BookingsManagementContextDTO context, int appId);

		// Token: 0x0600042E RID: 1070
		LoadClassTestDefinitionSmallByExamIdResp LoadClassTestDefinitionSmallByExamId(ClassTestDefinitionsManagementContextDTO context, int examId);

		// Token: 0x0600042F RID: 1071
		IList<TestBookingFullDTO> LoadTestsFullByExamId(BookingsManagementContextDTO context, int ExamId);

		// Token: 0x06000430 RID: 1072
		IList<TestBookingSmallDTO> LoadTestsSmallByExamId(BookingsManagementContextDTO context, int ExamId);

		// Token: 0x06000431 RID: 1073
		IList<TestBookingFullDTO> LoadTestsFullByAppointmentIds(BookingsManagementContextDTO context, params int[] appIds);

		// Token: 0x06000432 RID: 1074
		IList<TestBookingSmallDTO> LoadTestsSmallByAppointmentIds(BookingsManagementContextDTO context, params int[] appIds);

		// Token: 0x06000433 RID: 1075
		void SaveTestExamBookingLayoutToCentralizedSetting(eTestExamBookingGridViewType view, string layoutCompressed);

		// Token: 0x06000434 RID: 1076
		void ClearTestExamBookingLayoutInCentralizedSetting(eTestExamBookingGridViewType view);

		// Token: 0x06000435 RID: 1077
		IList<UnbookedTestExamStudentDTO> LoadUnbookedTestExamStudents();
	}
}
