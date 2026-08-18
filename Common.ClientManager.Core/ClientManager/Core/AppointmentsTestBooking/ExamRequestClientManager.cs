using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.AppointmentsTestBooking;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.AppointmentsTestBooking
{
	// Token: 0x0200008D RID: 141
	public class ExamRequestClientManager : IExamRequestClientManager, IWebService
	{
		// Token: 0x06000517 RID: 1303 RVA: 0x00016C7C File Offset: 0x00014E7C
		public IList<ExamRequestDTO> LoadRequestsByDateRange(DateTime StartDate, DateTime EndDate)
		{
			LoadRequestsByDateRangeReq loadRequestsByDateRangeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadRequestsByDateRangeReq>();
			loadRequestsByDateRangeReq.StartDate = StartDate;
			loadRequestsByDateRangeReq.EndDate = EndDate;
			return ClientServiceFactory.GetClientInstance<IExamRequest>().LoadRequestsByDateRange(loadRequestsByDateRangeReq).ExamRequests;
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x00016CBC File Offset: 0x00014EBC
		public int CreateExamRequest(int PersonId, int LuCourseId)
		{
			CreateExamRequestReq createExamRequestReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateExamRequestReq>();
			createExamRequestReq.PersonId = PersonId;
			createExamRequestReq.LuCourseId = LuCourseId;
			return ClientServiceFactory.GetClientInstance<IExamRequest>().CreateExamRequest(createExamRequestReq).ExamRequestId;
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x00016CFC File Offset: 0x00014EFC
		public void DeleteExamRequest(int ExamRequestId)
		{
			DeleteExamRequestReq deleteExamRequestReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteExamRequestReq>();
			deleteExamRequestReq.ExamRequestId = ExamRequestId;
			ClientServiceFactory.GetClientInstance<IExamRequest>().DeleteExamRequest(deleteExamRequestReq);
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x00016D2C File Offset: 0x00014F2C
		public IList<ExamRequestDTO> LoadRequestsByCourse(int LuCourseId)
		{
			LoadRequestsByCourseReq loadRequestsByCourseReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadRequestsByCourseReq>();
			loadRequestsByCourseReq.LuCourseId = LuCourseId;
			return ClientServiceFactory.GetClientInstance<IExamRequest>().LoadRequestsByCourse(loadRequestsByCourseReq).ExamRequests;
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x00016D64 File Offset: 0x00014F64
		public IList<PersonBaseDTO> LoadStudentsRegisteredInCourseWithStudentListWhoSubmittedExamRequests(int LuCourseId, out IList<int> PersonIdsWhoSubmittedExamRequest)
		{
			LoadStudentsRegisteredInCourseWithStudentListWhoSubmittedExamRequestsReq loadStudentsRegisteredInCourseWithStudentListWhoSubmittedExamRequestsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadStudentsRegisteredInCourseWithStudentListWhoSubmittedExamRequestsReq>();
			loadStudentsRegisteredInCourseWithStudentListWhoSubmittedExamRequestsReq.LuCourseId = LuCourseId;
			LoadStudentsRegisteredInCourseWithStudentListWhoSubmittedExamRequestsResp loadStudentsRegisteredInCourseWithStudentListWhoSubmittedExamRequestsResp = ClientServiceFactory.GetClientInstance<IExamRequest>().LoadStudentsRegisteredInCourseWithStudentListWhoSubmittedExamRequests(loadStudentsRegisteredInCourseWithStudentListWhoSubmittedExamRequestsReq);
			PersonIdsWhoSubmittedExamRequest = loadStudentsRegisteredInCourseWithStudentListWhoSubmittedExamRequestsResp.PersonIdsWhoHaveSubmittedExamRequest;
			return loadStudentsRegisteredInCourseWithStudentListWhoSubmittedExamRequestsResp.StudentsRegisteredInCourse;
		}
	}
}
