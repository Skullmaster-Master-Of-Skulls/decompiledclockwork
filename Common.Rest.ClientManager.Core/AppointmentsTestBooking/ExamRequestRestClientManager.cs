using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.AppointmentsTestBooking;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.AppointmentsTestBooking
{
	// Token: 0x02000077 RID: 119
	public class ExamRequestRestClientManager : BearerTokenRestProxy<IExamRequestClientManager>, IExamRequestClientManager, IWebService
	{
		// Token: 0x06000495 RID: 1173 RVA: 0x0000D1ED File Offset: 0x0000B3ED
		public ExamRequestRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x0000D1F7 File Offset: 0x0000B3F7
		public ExamRequestRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x0000D202 File Offset: 0x0000B402
		public IList<ExamRequestDTO> LoadRequestsByDateRange(DateTime StartDate, DateTime EndDate)
		{
			return base.GetMany<ExamRequestDTO>(string.Format("examrequest/range/{0}/{1}", StartDate, EndDate), true);
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x0000D224 File Offset: 0x0000B424
		public int CreateExamRequest(int PersonId, int LuCourseId)
		{
			CreateExamRequestReq createExamRequestReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateExamRequestReq>();
			createExamRequestReq.PersonId = PersonId;
			createExamRequestReq.LuCourseId = LuCourseId;
			return base.Post<CreateExamRequestReq, int>(createExamRequestReq, "examrequest");
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x0000D256 File Offset: 0x0000B456
		public void DeleteExamRequest(int ExamRequestId)
		{
			base.Delete(string.Format("examrequest/id/{0}", ExamRequestId));
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x0000D26E File Offset: 0x0000B46E
		public IList<ExamRequestDTO> LoadRequestsByCourse(int LuCourseId)
		{
			return base.GetMany<ExamRequestDTO>(string.Format("examrequest/lucourseid/{0}", LuCourseId), true);
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x0000D288 File Offset: 0x0000B488
		public IList<PersonBaseDTO> LoadStudentsRegisteredInCourseWithStudentListWhoSubmittedExamRequests(int LuCourseId, out IList<int> PersonIdsWhoSubmittedExamRequest)
		{
			LoadStudentsRegisteredInCourseWithStudentListWhoSubmittedExamRequestsResp loadStudentsRegisteredInCourseWithStudentListWhoSubmittedExamRequestsResp = base.Get<LoadStudentsRegisteredInCourseWithStudentListWhoSubmittedExamRequestsResp>(string.Format("examrequest/studentsregisteredincoursewithstudentlistwhosubmittedexamrequests/lucourseid/{0}", LuCourseId), true);
			PersonIdsWhoSubmittedExamRequest = loadStudentsRegisteredInCourseWithStudentListWhoSubmittedExamRequestsResp.PersonIdsWhoHaveSubmittedExamRequest;
			return loadStudentsRegisteredInCourseWithStudentListWhoSubmittedExamRequestsResp.StudentsRegisteredInCourse;
		}
	}
}
