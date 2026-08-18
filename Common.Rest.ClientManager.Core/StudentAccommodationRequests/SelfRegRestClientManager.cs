using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.Accommodations;
using TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests;
using TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests.SelfRegProcessing;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.StudentAccommodationRequests;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.StudentAccommodationRequests.SelfRegProcessing;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.StudentAccommodationRequests
{
	// Token: 0x02000011 RID: 17
	public class SelfRegRestClientManager : BearerTokenRestProxy<ISelfRegClientManager>, ISelfRegClientManager, IWebService
	{
		// Token: 0x0600008B RID: 139 RVA: 0x0000361D File Offset: 0x0000181D
		public SelfRegRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003627 File Offset: 0x00001827
		public SelfRegRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003634 File Offset: 0x00001834
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
			base.Post<ProcessSelfRegRequestReq>(processSelfRegRequestReq, "selfreg/processselfregrequest");
		}

		// Token: 0x0600008E RID: 142 RVA: 0x0000369D File Offset: 0x0000189D
		public AllowedStudentCourseRegistrationsForCustomEmailLogicDTO GetCoursesAllowedBySelfRegCustomLogicRulesToViewLoaFor(int studentPersonId)
		{
			return base.Get<AllowedStudentCourseRegistrationsForCustomEmailLogicDTO>(string.Format("selfreg/coursesallowedbycustomlogicrulestoviewloafor/studentpid/{0}", studentPersonId), true);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000036B6 File Offset: 0x000018B6
		public AllowedStudentCourseRegistrationsForCustomEmailLogicDTO GetCoursesAllowedBySelfRegCustomLogicRulesToViewLoaFor(string hash, string hashPlainText)
		{
			return base.Get<AllowedStudentCourseRegistrationsForCustomEmailLogicDTO>(string.Format("selfreg/coursesallowedbycustomlogicrulestoviewloafor/hash/{0}/plaintext/{1}", hash, hashPlainText), true);
		}
	}
}
