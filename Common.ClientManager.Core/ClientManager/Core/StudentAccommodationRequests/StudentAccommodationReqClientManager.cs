using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.StudentAccommodationRequests;
using TechnoPro.Common.DataStructure;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.StudentAccommodationRequests
{
	// Token: 0x02000018 RID: 24
	public class StudentAccommodationReqClientManager : IStudentAccommodationReqClientManager, IWebService
	{
		// Token: 0x060000BA RID: 186 RVA: 0x00004D74 File Offset: 0x00002F74
		public IList<CourseRegistrationWithAccommodationRequestDTO> LoadCourseRegistrationsWithRequestByStudentAndDate(int StudentPersonId, DateTime StartDate, DateTime EndDate, bool LoadAccommodations)
		{
			LoadCourseRegistrationsWithRequestByStudentAndDateReq loadCourseRegistrationsWithRequestByStudentAndDateReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadCourseRegistrationsWithRequestByStudentAndDateReq>();
			loadCourseRegistrationsWithRequestByStudentAndDateReq.LoadAccommodations = LoadAccommodations;
			loadCourseRegistrationsWithRequestByStudentAndDateReq.StudentPersonId = StudentPersonId;
			loadCourseRegistrationsWithRequestByStudentAndDateReq.StartDate = StartDate;
			loadCourseRegistrationsWithRequestByStudentAndDateReq.EndDate = EndDate;
			return ClientServiceFactory.GetClientInstance<IStudentAccommodationReq>().LoadCourseRegistrationsWithRequestByStudentAndDate(loadCourseRegistrationsWithRequestByStudentAndDateReq).CoursesWithRequests;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00004DC4 File Offset: 0x00002FC4
		public int AddRequest(int StudentPersonId, StudentCourseAccommodationRequestDTO StudentCourseAccommodationRequest)
		{
			AddRequestReq addRequestReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<AddRequestReq>();
			addRequestReq.StudentCourseAccommodationRequest = StudentCourseAccommodationRequest;
			addRequestReq.StudentPersonId = StudentPersonId;
			return ClientServiceFactory.GetClientInstance<IStudentAccommodationReq>().AddRequest(addRequestReq).StudentCourseAccommodationRequestId;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00004E04 File Offset: 0x00003004
		public IList<StudentCourseAccommodationRequestDTO> LoadRequestsByStudentAndDate(int StudentPersonId, DateTime StartDate, DateTime EndDate)
		{
			LoadRequestsByStudentAndDateReq loadRequestsByStudentAndDateReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadRequestsByStudentAndDateReq>();
			loadRequestsByStudentAndDateReq.StudentPersonId = StudentPersonId;
			loadRequestsByStudentAndDateReq.StartDate = StartDate;
			loadRequestsByStudentAndDateReq.EndDate = EndDate;
			return ClientServiceFactory.GetClientInstance<IStudentAccommodationReq>().LoadRequestsByStudentAndDate(loadRequestsByStudentAndDateReq).CourseAccommodationRequests;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00004E4C File Offset: 0x0000304C
		public StudentCourseAccommodationRequestDTO LoadRequestById(int StudentCourseAccommodationRequestId)
		{
			LoadRequestByIdReq loadRequestByIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadRequestByIdReq>();
			loadRequestByIdReq.StudentCourseAccommodationRequestId = StudentCourseAccommodationRequestId;
			return ClientServiceFactory.GetClientInstance<IStudentAccommodationReq>().LoadRequestById(loadRequestByIdReq).StudentCourseAccommodationRequest;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00004E84 File Offset: 0x00003084
		public void DeleteRequest(int StudentCourseAccommodationRequestId)
		{
			DeleteRequestReq deleteRequestReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteRequestReq>();
			deleteRequestReq.StudentCourseAccommodationRequestId = StudentCourseAccommodationRequestId;
			ClientServiceFactory.GetClientInstance<IStudentAccommodationReq>().DeleteRequest(deleteRequestReq);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00004EB4 File Offset: 0x000030B4
		public void UpdateRequest(StudentCourseAccommodationRequestDTO StudentCourseAccommodationRequest)
		{
			UpdateRequestReq updateRequestReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateRequestReq>();
			updateRequestReq.StudentCourseAccommodationRequest = StudentCourseAccommodationRequest;
			ClientServiceFactory.GetClientInstance<IStudentAccommodationReq>().UpdateRequest(updateRequestReq);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00004EE4 File Offset: 0x000030E4
		public IList<StudentCourseAccommodationRequestDTO> LoadCourseRegistrationsWithRequestByStatus(eStudentCourseAccommodationRequestStatusDTO Statuses, Range<DateTime> restrictCourseDates)
		{
			LoadCourseRegistrationsWithRequestByStatusReq loadCourseRegistrationsWithRequestByStatusReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadCourseRegistrationsWithRequestByStatusReq>();
			loadCourseRegistrationsWithRequestByStatusReq.Statuses = Statuses;
			loadCourseRegistrationsWithRequestByStatusReq.RestrictCourseDates = restrictCourseDates;
			return ClientServiceFactory.GetClientInstance<IStudentAccommodationReq>().LoadCourseRegistrationsWithRequestByStatus(loadCourseRegistrationsWithRequestByStatusReq).CoursesWithRequests;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00004F24 File Offset: 0x00003124
		public void UpdateRequestStatus(eStudentCourseAccommodationRequestStatusDTO Statuses, int StudentAccommodationRequestId)
		{
			UpdateRequestStatusReq updateRequestStatusReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateRequestStatusReq>();
			updateRequestStatusReq.Statuses = Statuses;
			updateRequestStatusReq.StudentAccommodationRequestId = StudentAccommodationRequestId;
			ClientServiceFactory.GetClientInstance<IStudentAccommodationReq>().UpdateRequestStatus(updateRequestStatusReq);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00004F5C File Offset: 0x0000315C
		public StudentCourseAccommodationRequestHistoryDTO LoadStudentCourseAccommodationRequestHistory(int PersonId, int LuCourseId)
		{
			LoadStudentCourseAccommodationRequestHistoryReq loadStudentCourseAccommodationRequestHistoryReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadStudentCourseAccommodationRequestHistoryReq>();
			loadStudentCourseAccommodationRequestHistoryReq.PersonId = PersonId;
			loadStudentCourseAccommodationRequestHistoryReq.LuCourseId = LuCourseId;
			return ClientServiceFactory.GetClientInstance<IStudentAccommodationReq>().LoadStudentCourseAccommodationRequestHistory(loadStudentCourseAccommodationRequestHistoryReq).AccommodationRequestHistory;
		}
	}
}
