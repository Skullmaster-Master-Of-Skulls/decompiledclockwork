using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Accommodations;
using TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests;
using TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests.SelfRegProcessing;
using TechnoPro.Common.Core.Mappers.Accommodations;
using TechnoPro.Common.Core.Mappers.StudentAccommodationRequests;
using TechnoPro.Common.Core.Mappers.StudentAccommodationRequests.SelfRegProcessing;
using TechnoPro.Common.Core.StudentAccommodationRequests;
using TechnoPro.Common.ICore.StudentAccommodationRequests;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Accommodations;
using TechnoPro.Common.Public.Entities.StudentAccommodationRequests;
using TechnoPro.Common.Public.Entities.StudentAccommodationRequests.SelfRegProcessing;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x0200008D RID: 141
	public class SelfRegServiceManager : ISelfReg, IService
	{
		// Token: 0x06000512 RID: 1298 RVA: 0x00017B30 File Offset: 0x00015D30
		public ProcessSelfRegRequestResp ProcessSelfRegRequest(ProcessSelfRegRequestReq Request)
		{
			ISelfRegManager selfRegManager = new SelfRegManager(Request.GetOperationContext());
			ISelfRegManager selfRegManager2 = selfRegManager;
			int studentPersonId = Request.StudentPersonId;
			eSelfRegCoursesAccommodationsStatus studentIndicatedCoursesAccommodationsStatus = Request.StudentIndicatedCoursesAccommodationsStatus;
			IList<SelfRegCourseInfo> selectedLucids = (from g in Request.LuCourseIdsToApplyTo
			select g.ToDomainObject()).ToList<SelfRegCourseInfo>();
			IList<SelfRegCheckedAccommodationDTO> checkedAccommodations = Request.CheckedAccommodations;
			List<SelfRegCheckedAccommodation> checkedAccommodations2;
			if (checkedAccommodations == null)
			{
				checkedAccommodations2 = null;
			}
			else
			{
				checkedAccommodations2 = (from g in checkedAccommodations
				select g.ToDomainObject()).ToList<SelfRegCheckedAccommodation>();
			}
			IList<AccommodationDataDTO> hidingAccommodations = Request.HidingAccommodations;
			IList<AccommodationData> hidingAccommodations2;
			if (hidingAccommodations == null)
			{
				hidingAccommodations2 = null;
			}
			else
			{
				hidingAccommodations2 = (from g in hidingAccommodations
				select g.ToDomainObject()).ToList<AccommodationData>();
			}
			selfRegManager2.ProcessSelfRegRequest(studentPersonId, studentIndicatedCoursesAccommodationsStatus, selectedLucids, checkedAccommodations2, hidingAccommodations2, Request.NoteFromStudent, Request.BaseUrl, Request.StudentPersonIdEncodedForUrl, Request.IpAddressForLogging);
			return new ProcessSelfRegRequestResp();
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x00017C1C File Offset: 0x00015E1C
		public GetCoursesAllowedBySelfRegCustomLogicRulesToViewLoaForResp GetCoursesAllowedBySelfRegCustomLogicRulesToViewLoaFor(GetCoursesAllowedBySelfRegCustomLogicRulesToViewLoaForReq Request)
		{
			ISelfRegManager selfRegManager = new SelfRegManager(Request.GetOperationContext());
			AllowedStudentCourseRegistrationsForCustomEmailLogic allowedStudentCourseRegistrationsForCustomEmailLogic = (Request.StudentPersonId > 0) ? selfRegManager.GetCoursesAllowedBySelfRegCustomLogicRulesToViewLoaFor(Request.StudentPersonId) : selfRegManager.GetCoursesAllowedBySelfRegCustomLogicRulesToViewLoaFor(Request.StudentPersonIdHash, Request.StudentPersonIdHashPlainText);
			return new GetCoursesAllowedBySelfRegCustomLogicRulesToViewLoaForResp
			{
				AllowedCourseRegistrations = ((allowedStudentCourseRegistrationsForCustomEmailLogic != null) ? allowedStudentCourseRegistrationsForCustomEmailLogic.ToDTO() : null)
			};
		}
	}
}
