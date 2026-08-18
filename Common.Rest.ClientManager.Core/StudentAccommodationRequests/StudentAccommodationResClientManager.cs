using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.StudentAccommodationRequests;
using TechnoPro.Common.DataStructure;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.StudentAccommodationRequests
{
	// Token: 0x02000012 RID: 18
	public class StudentAccommodationResClientManager : BearerTokenRestProxy<IStudentAccommodationReqClientManager>, IStudentAccommodationReqClientManager, IWebService
	{
		// Token: 0x06000090 RID: 144 RVA: 0x000036CB File Offset: 0x000018CB
		public StudentAccommodationResClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000036D5 File Offset: 0x000018D5
		public StudentAccommodationResClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000036E0 File Offset: 0x000018E0
		public IList<CourseRegistrationWithAccommodationRequestDTO> LoadCourseRegistrationsWithRequestByStudentAndDate(int StudentPersonId, DateTime StartDate, DateTime EndDate, bool LoadAccommodations)
		{
			return base.GetMany<CourseRegistrationWithAccommodationRequestDTO>(string.Format("studentaccommodationreq/courseregistrationswithrequest/studentpersonid/{0}/range/{1}/{2}?loadaccommodations={3}", new object[]
			{
				StudentPersonId,
				StartDate,
				EndDate,
				LoadAccommodations
			}), true);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003720 File Offset: 0x00001920
		public int AddRequest(int StudentPersonId, StudentCourseAccommodationRequestDTO StudentCourseAccommodationRequest)
		{
			AddRequestReq addRequestReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<AddRequestReq>();
			addRequestReq.StudentCourseAccommodationRequest = StudentCourseAccommodationRequest;
			addRequestReq.StudentPersonId = StudentPersonId;
			return base.Post<AddRequestReq, int>(addRequestReq, "studentaccommodationreq");
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003752 File Offset: 0x00001952
		public IList<StudentCourseAccommodationRequestDTO> LoadRequestsByStudentAndDate(int StudentPersonId, DateTime StartDate, DateTime EndDate)
		{
			return base.GetMany<StudentCourseAccommodationRequestDTO>(string.Format("studentaccommodationreq/studentpersonid/{0}/range/{1}/{2}", StudentPersonId, StartDate, EndDate), true);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003777 File Offset: 0x00001977
		public StudentCourseAccommodationRequestDTO LoadRequestById(int StudentCourseAccommodationRequestId)
		{
			return base.Get<StudentCourseAccommodationRequestDTO>(string.Format("studentaccommodationreq/requestid/{0}", StudentCourseAccommodationRequestId), true);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003790 File Offset: 0x00001990
		public void DeleteRequest(int StudentCourseAccommodationRequestId)
		{
			base.Delete(string.Format("studentaccommodationreq/requestid/{0}", StudentCourseAccommodationRequestId));
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000037A8 File Offset: 0x000019A8
		public void UpdateRequest(StudentCourseAccommodationRequestDTO StudentCourseAccommodationRequest)
		{
			base.Put<StudentCourseAccommodationRequestDTO>(StudentCourseAccommodationRequest, "studentaccommodationreq");
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000037B6 File Offset: 0x000019B6
		public IList<StudentCourseAccommodationRequestDTO> LoadCourseRegistrationsWithRequestByStatus(eStudentCourseAccommodationRequestStatusDTO Statuses, Range<DateTime> restrictCourseDates)
		{
			return base.GetMany<StudentCourseAccommodationRequestDTO>(string.Format("studentaccommodationreq/courseregistrationswithrequest/statuses/{0}/range/{1}/{2}", Statuses, restrictCourseDates.Start, restrictCourseDates.End), true);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000037E8 File Offset: 0x000019E8
		public void UpdateRequestStatus(eStudentCourseAccommodationRequestStatusDTO Statuses, int StudentAccommodationRequestId)
		{
			UpdateRequestStatusReq updateRequestStatusReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateRequestStatusReq>();
			updateRequestStatusReq.Statuses = Statuses;
			updateRequestStatusReq.StudentAccommodationRequestId = StudentAccommodationRequestId;
			base.Put<UpdateRequestStatusReq>(updateRequestStatusReq, "studentaccommodationreq/status");
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0000381A File Offset: 0x00001A1A
		public StudentCourseAccommodationRequestHistoryDTO LoadStudentCourseAccommodationRequestHistory(int PersonId, int LuCourseId)
		{
			return base.Get<StudentCourseAccommodationRequestHistoryDTO>(string.Format("studentaccommodationreq/history/personid/{0}/lucourseid/{1}", PersonId, LuCourseId), true);
		}
	}
}
