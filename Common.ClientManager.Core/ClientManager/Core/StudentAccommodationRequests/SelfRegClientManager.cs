using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Accommodations;
using TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests;
using TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests.SelfRegProcessing;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.StudentAccommodationRequests;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.StudentAccommodationRequests.SelfRegProcessing;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.StudentAccommodationRequests
{
	// Token: 0x02000017 RID: 23
	public class SelfRegClientManager : ISelfRegClientManager, IWebService
	{
		// Token: 0x060000B6 RID: 182 RVA: 0x00004C80 File Offset: 0x00002E80
		public void ProcessSelfRegRequest(int studentPersonId, eSelfRegCoursesAccommodationsStatus studentIndicatedCoursesAccommodationsStatus, IList<SelfRegCourseInfoDTO> luCourseIdsToApplyTo, List<SelfRegCheckedAccommodationDTO> checkedAccommodations, IList<AccommodationDataDTO> hidingAccommodations, string noteFromStudent, string baseUrl, string studentPersonIdEncodedForUrl, string ipAddressForLogging)
		{
			ProcessSelfRegRequestReq processSelfRegRequestReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ProcessSelfRegRequestReq>();
			processSelfRegRequestReq.StudentPersonId = studentPersonId;
			processSelfRegRequestReq.BaseUrl = baseUrl;
			processSelfRegRequestReq.CheckedAccommodations = checkedAccommodations;
			processSelfRegRequestReq.HidingAccommodations = hidingAccommodations;
			processSelfRegRequestReq.IpAddressForLogging = ipAddressForLogging;
			processSelfRegRequestReq.StudentPersonIdEncodedForUrl = studentPersonIdEncodedForUrl;
			processSelfRegRequestReq.LuCourseIdsToApplyTo = luCourseIdsToApplyTo;
			processSelfRegRequestReq.NoteFromStudent = noteFromStudent;
			processSelfRegRequestReq.StudentIndicatedCoursesAccommodationsStatus = studentIndicatedCoursesAccommodationsStatus;
			ClientServiceFactory.GetClientInstance<ISelfReg>().ProcessSelfRegRequest(processSelfRegRequestReq);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00004CF4 File Offset: 0x00002EF4
		public AllowedStudentCourseRegistrationsForCustomEmailLogicDTO GetCoursesAllowedBySelfRegCustomLogicRulesToViewLoaFor(string hash, string hashPlainText)
		{
			GetCoursesAllowedBySelfRegCustomLogicRulesToViewLoaForReq getCoursesAllowedBySelfRegCustomLogicRulesToViewLoaForReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetCoursesAllowedBySelfRegCustomLogicRulesToViewLoaForReq>();
			getCoursesAllowedBySelfRegCustomLogicRulesToViewLoaForReq.StudentPersonIdHash = hash;
			getCoursesAllowedBySelfRegCustomLogicRulesToViewLoaForReq.StudentPersonIdHashPlainText = hashPlainText;
			GetCoursesAllowedBySelfRegCustomLogicRulesToViewLoaForResp coursesAllowedBySelfRegCustomLogicRulesToViewLoaFor = ClientServiceFactory.GetClientInstance<ISelfReg>().GetCoursesAllowedBySelfRegCustomLogicRulesToViewLoaFor(getCoursesAllowedBySelfRegCustomLogicRulesToViewLoaForReq);
			return (coursesAllowedBySelfRegCustomLogicRulesToViewLoaFor != null) ? coursesAllowedBySelfRegCustomLogicRulesToViewLoaFor.AllowedCourseRegistrations : null;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00004D38 File Offset: 0x00002F38
		public AllowedStudentCourseRegistrationsForCustomEmailLogicDTO GetCoursesAllowedBySelfRegCustomLogicRulesToViewLoaFor(int studentPersonId)
		{
			GetCoursesAllowedBySelfRegCustomLogicRulesToViewLoaForReq getCoursesAllowedBySelfRegCustomLogicRulesToViewLoaForReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetCoursesAllowedBySelfRegCustomLogicRulesToViewLoaForReq>();
			getCoursesAllowedBySelfRegCustomLogicRulesToViewLoaForReq.StudentPersonId = studentPersonId;
			GetCoursesAllowedBySelfRegCustomLogicRulesToViewLoaForResp coursesAllowedBySelfRegCustomLogicRulesToViewLoaFor = ClientServiceFactory.GetClientInstance<ISelfReg>().GetCoursesAllowedBySelfRegCustomLogicRulesToViewLoaFor(getCoursesAllowedBySelfRegCustomLogicRulesToViewLoaForReq);
			return (coursesAllowedBySelfRegCustomLogicRulesToViewLoaFor != null) ? coursesAllowedBySelfRegCustomLogicRulesToViewLoaFor.AllowedCourseRegistrations : null;
		}
	}
}
