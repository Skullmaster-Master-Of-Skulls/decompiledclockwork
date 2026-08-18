using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Core.AppointmentsTestBooking;
using TechnoPro.Common.Core.Mappers;
using TechnoPro.Common.Core.Mappers.AppointmentsTestBooking;
using TechnoPro.Common.ICore.AppointmentsTestBooking;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000016 RID: 22
	public class ExamRequestServiceManager : IExamRequest, IService
	{
		// Token: 0x06000117 RID: 279 RVA: 0x000063B4 File Offset: 0x000045B4
		public LoadRequestsByDateRangeResp LoadRequestsByDateRange(LoadRequestsByDateRangeReq Request)
		{
			IExamRequestManager examRequestManager = new ExamRequestManager(Request.GetOperationContext());
			IList<ExamRequest> list = examRequestManager.LoadRequestsByDateRange(Request.StartDate, Request.EndDate);
			LoadRequestsByDateRangeResp loadRequestsByDateRangeResp = new LoadRequestsByDateRangeResp();
			IList<ExamRequestDTO> examRequests;
			if (list != null)
			{
				examRequests = list.ToList<ExamRequest>().ConvertAll<ExamRequestDTO>((ExamRequest g) => g.ToDTO());
			}
			else
			{
				examRequests = null;
			}
			loadRequestsByDateRangeResp.ExamRequests = examRequests;
			return loadRequestsByDateRangeResp;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00006424 File Offset: 0x00004624
		public CreateExamRequestResp CreateExamRequest(CreateExamRequestReq Request)
		{
			IExamRequestManager examRequestManager = new ExamRequestManager(Request.GetOperationContext());
			int examRequestId = examRequestManager.CreateExamRequest(Request.PersonId, Request.LuCourseId);
			return new CreateExamRequestResp
			{
				ExamRequestId = examRequestId
			};
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00006464 File Offset: 0x00004664
		public void DeleteExamRequest(DeleteExamRequestReq Request)
		{
			IExamRequestManager examRequestManager = new ExamRequestManager(Request.GetOperationContext());
			examRequestManager.DeleteExamRequest(Request.ExamRequestId);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x0000648C File Offset: 0x0000468C
		public LoadRequestsByCourseResp LoadRequestsByCourse(LoadRequestsByCourseReq Request)
		{
			IExamRequestManager examRequestManager = new ExamRequestManager(Request.GetOperationContext());
			IList<ExamRequest> list = examRequestManager.LoadRequestsByCourse(Request.LuCourseId);
			LoadRequestsByCourseResp loadRequestsByCourseResp = new LoadRequestsByCourseResp();
			IList<ExamRequestDTO> examRequests;
			if (list != null)
			{
				examRequests = list.ToList<ExamRequest>().ConvertAll<ExamRequestDTO>((ExamRequest g) => g.ToDTO());
			}
			else
			{
				examRequests = null;
			}
			loadRequestsByCourseResp.ExamRequests = examRequests;
			return loadRequestsByCourseResp;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x000064F4 File Offset: 0x000046F4
		public LoadStudentsRegisteredInCourseWithStudentListWhoSubmittedExamRequestsResp LoadStudentsRegisteredInCourseWithStudentListWhoSubmittedExamRequests(LoadStudentsRegisteredInCourseWithStudentListWhoSubmittedExamRequestsReq Request)
		{
			IExamRequestManager examRequestManager = new ExamRequestManager(Request.GetOperationContext());
			IList<int> source;
			IList<PersonBase> list = examRequestManager.LoadStudentsRegisteredInCourseWithStudentListWhoSubmittedExamRequests(Request.LuCourseId, out source);
			LoadStudentsRegisteredInCourseWithStudentListWhoSubmittedExamRequestsResp loadStudentsRegisteredInCourseWithStudentListWhoSubmittedExamRequestsResp = new LoadStudentsRegisteredInCourseWithStudentListWhoSubmittedExamRequestsResp();
			IList<PersonBaseDTO> studentsRegisteredInCourse;
			if (list != null)
			{
				studentsRegisteredInCourse = (from g in list
				select g.ToDTO()).ToList<PersonBaseDTO>();
			}
			else
			{
				studentsRegisteredInCourse = null;
			}
			loadStudentsRegisteredInCourseWithStudentListWhoSubmittedExamRequestsResp.StudentsRegisteredInCourse = studentsRegisteredInCourse;
			loadStudentsRegisteredInCourseWithStudentListWhoSubmittedExamRequestsResp.PersonIdsWhoHaveSubmittedExamRequest = source.ToList<int>();
			return loadStudentsRegisteredInCourseWithStudentListWhoSubmittedExamRequestsResp;
		}
	}
}
