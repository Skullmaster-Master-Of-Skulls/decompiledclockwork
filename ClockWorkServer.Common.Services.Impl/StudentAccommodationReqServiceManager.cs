using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests;
using TechnoPro.Common.Core.Mappers.StudentAccommodationRequests;
using TechnoPro.Common.Core.StudentAccommodationRequests;
using TechnoPro.Common.ICore.StudentAccommodationRequests;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.StudentAccommodationRequests;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x0200008E RID: 142
	public class StudentAccommodationReqServiceManager : IStudentAccommodationReq, IService
	{
		// Token: 0x06000515 RID: 1301 RVA: 0x00017C7C File Offset: 0x00015E7C
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x00017C90 File Offset: 0x00015E90
		public AddRequestResp AddRequest(AddRequestReq Request)
		{
			IStudentAccommodationRequestManager studentAccommodationRequestManager = new StudentAccommodationRequestManager(Request.GetOperationContext());
			int studentCourseAccommodationRequestId = studentAccommodationRequestManager.AddRequest(Request.StudentPersonId, Request.StudentCourseAccommodationRequest.ToDomainObject());
			return new AddRequestResp
			{
				StudentCourseAccommodationRequestId = studentCourseAccommodationRequestId
			};
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x00017CD4 File Offset: 0x00015ED4
		public LoadRequestsByStudentAndDateResp LoadRequestsByStudentAndDate(LoadRequestsByStudentAndDateReq Request)
		{
			IStudentAccommodationRequestManager studentAccommodationRequestManager = new StudentAccommodationRequestManager(Request.GetOperationContext());
			IList<StudentCourseAccommodationRequest> list = studentAccommodationRequestManager.LoadRequestsByStudentAndDate(Request.StudentPersonId, Request.StartDate, Request.EndDate);
			LoadRequestsByStudentAndDateResp loadRequestsByStudentAndDateResp = new LoadRequestsByStudentAndDateResp();
			IList<StudentCourseAccommodationRequestDTO> courseAccommodationRequests;
			if (list != null)
			{
				courseAccommodationRequests = list.ToList<StudentCourseAccommodationRequest>().ConvertAll<StudentCourseAccommodationRequestDTO>((StudentCourseAccommodationRequest f) => f.ToDTO());
			}
			else
			{
				courseAccommodationRequests = null;
			}
			loadRequestsByStudentAndDateResp.CourseAccommodationRequests = courseAccommodationRequests;
			return loadRequestsByStudentAndDateResp;
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x00017D48 File Offset: 0x00015F48
		public LoadRequestByIdResp LoadRequestById(LoadRequestByIdReq Request)
		{
			IStudentAccommodationRequestManager studentAccommodationRequestManager = new StudentAccommodationRequestManager(Request.GetOperationContext());
			StudentCourseAccommodationRequest studentCourseAccommodationRequest = studentAccommodationRequestManager.LoadRequestById(Request.StudentCourseAccommodationRequestId);
			return new LoadRequestByIdResp
			{
				StudentCourseAccommodationRequest = ((studentCourseAccommodationRequest == null) ? null : studentCourseAccommodationRequest.ToDTO())
			};
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x00017D8C File Offset: 0x00015F8C
		public void DeleteRequest(DeleteRequestReq Request)
		{
			IStudentAccommodationRequestManager studentAccommodationRequestManager = new StudentAccommodationRequestManager(Request.GetOperationContext());
			studentAccommodationRequestManager.DeleteRequest(Request.StudentCourseAccommodationRequestId);
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x00017DB4 File Offset: 0x00015FB4
		public void UpdateRequest(UpdateRequestReq Request)
		{
			IStudentAccommodationRequestManager studentAccommodationRequestManager = new StudentAccommodationRequestManager(Request.GetOperationContext());
			studentAccommodationRequestManager.UpdateRequest(Request.StudentCourseAccommodationRequest.ToDomainObject());
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x00017DE0 File Offset: 0x00015FE0
		public LoadCourseRegistrationsWithRequestByStudentAndDateResp LoadCourseRegistrationsWithRequestByStudentAndDate(LoadCourseRegistrationsWithRequestByStudentAndDateReq Request)
		{
			IStudentAccommodationRequestManager studentAccommodationRequestManager = new StudentAccommodationRequestManager(Request.GetOperationContext());
			IList<CourseRegistrationWithAccommodationRequest> list = studentAccommodationRequestManager.LoadCourseRegistrationsWithRequestByStudentAndDate(Request.StudentPersonId, Request.StartDate, Request.EndDate, Request.LoadAccommodations);
			LoadCourseRegistrationsWithRequestByStudentAndDateResp loadCourseRegistrationsWithRequestByStudentAndDateResp = new LoadCourseRegistrationsWithRequestByStudentAndDateResp();
			IList<CourseRegistrationWithAccommodationRequestDTO> coursesWithRequests;
			if (list != null)
			{
				coursesWithRequests = list.ToList<CourseRegistrationWithAccommodationRequest>().ConvertAll<CourseRegistrationWithAccommodationRequestDTO>((CourseRegistrationWithAccommodationRequest f) => f.ToDTO());
			}
			else
			{
				coursesWithRequests = null;
			}
			loadCourseRegistrationsWithRequestByStudentAndDateResp.CoursesWithRequests = coursesWithRequests;
			return loadCourseRegistrationsWithRequestByStudentAndDateResp;
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x00017E5C File Offset: 0x0001605C
		public LoadCourseRegistrationsWithRequestByStatusResp LoadCourseRegistrationsWithRequestByStatus(LoadCourseRegistrationsWithRequestByStatusReq Request)
		{
			IStudentAccommodationRequestManager studentAccommodationRequestManager = new StudentAccommodationRequestManager(Request.GetOperationContext());
			IList<StudentCourseAccommodationRequest> list = studentAccommodationRequestManager.LoadCourseRegistrationsWithRequestByStatus((eStudentCourseAccommodationRequestStatus)Request.Statuses, Request.RestrictCourseDates);
			LoadCourseRegistrationsWithRequestByStatusResp loadCourseRegistrationsWithRequestByStatusResp = new LoadCourseRegistrationsWithRequestByStatusResp();
			IList<StudentCourseAccommodationRequestDTO> coursesWithRequests;
			if (list != null)
			{
				coursesWithRequests = list.ToList<StudentCourseAccommodationRequest>().ConvertAll<StudentCourseAccommodationRequestDTO>((StudentCourseAccommodationRequest f) => f.ToDTO());
			}
			else
			{
				coursesWithRequests = null;
			}
			loadCourseRegistrationsWithRequestByStatusResp.CoursesWithRequests = coursesWithRequests;
			return loadCourseRegistrationsWithRequestByStatusResp;
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x00017ECC File Offset: 0x000160CC
		public void UpdateRequestStatus(UpdateRequestStatusReq Request)
		{
			IStudentAccommodationRequestManager studentAccommodationRequestManager = new StudentAccommodationRequestManager(Request.GetOperationContext());
			studentAccommodationRequestManager.UpdateRequestStatus(Request.StudentAccommodationRequestId, (eStudentCourseAccommodationRequestStatus)Request.Statuses);
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x00017EFC File Offset: 0x000160FC
		public LoadStudentCourseAccommodationRequestHistoryResp LoadStudentCourseAccommodationRequestHistory(LoadStudentCourseAccommodationRequestHistoryReq Request)
		{
			IStudentAccommodationRequestManager studentAccommodationRequestManager = new StudentAccommodationRequestManager(Request.GetOperationContext());
			StudentCourseAccommodationRequestHistory studentCourseAccommodationRequestHistory = studentAccommodationRequestManager.LoadStudentCourseAccommodationRequestHistory(Request.PersonId, Request.LuCourseId);
			return new LoadStudentCourseAccommodationRequestHistoryResp
			{
				AccommodationRequestHistory = ((studentCourseAccommodationRequestHistory == null) ? null : studentCourseAccommodationRequestHistory.ToDTO())
			};
		}
	}
}
