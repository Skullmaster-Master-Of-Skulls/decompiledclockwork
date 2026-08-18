using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.Accommodations;
using TechnoPro.ClockWorkServer.Contracts.DTO.CourseRegistrations;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.DynamicForms;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.DynamicForms
{
	// Token: 0x02000051 RID: 81
	public class AccommodationsRestClientManager : BearerTokenRestProxy<IAccommodationsClientManager>, IAccommodationsClientManager, IWebService
	{
		// Token: 0x0600030C RID: 780 RVA: 0x00009589 File Offset: 0x00007789
		public AccommodationsRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x0600030D RID: 781 RVA: 0x00009593 File Offset: 0x00007793
		public AccommodationsRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x0600030E RID: 782 RVA: 0x000095A0 File Offset: 0x000077A0
		public IList<AccommodationDataDTO> LoadAccommodationsByStudentAndCourseOrTemplate(int PersonId, int LuCourseId)
		{
			bool flag;
			return this.LoadAccommodationsByStudentAndCourseOrTemplate(PersonId, LuCourseId, out flag);
		}

		// Token: 0x0600030F RID: 783 RVA: 0x000095B8 File Offset: 0x000077B8
		public IList<AccommodationDataDTO> LoadAccommodationsByStudentAndCourseOrTemplate(int PersonId, int LuCourseId, out bool IsUsingTemplateAccommodations)
		{
			LoadAccommodationsByStudentAndCourseOrTemplateResp loadAccommodationsByStudentAndCourseOrTemplateResp = base.Get<LoadAccommodationsByStudentAndCourseOrTemplateResp>(string.Format("accommodations/pid/{0}/lucourseid/{1}", PersonId, LuCourseId), true);
			IsUsingTemplateAccommodations = (loadAccommodationsByStudentAndCourseOrTemplateResp == null || loadAccommodationsByStudentAndCourseOrTemplateResp.IsUsingTemplateAccommodations);
			if (loadAccommodationsByStudentAndCourseOrTemplateResp == null)
			{
				return null;
			}
			return loadAccommodationsByStudentAndCourseOrTemplateResp.Accommodations;
		}

		// Token: 0x06000310 RID: 784 RVA: 0x000095FC File Offset: 0x000077FC
		public void MergeOrReplaceAccommodations(bool ReplaceExistingAccommodations, int SourcePersonId, int SourceLuCourseId, int DestPersonId, int DestLuCourseId)
		{
			MergeOrReplaceAccommodationsReq mergeOrReplaceAccommodationsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<MergeOrReplaceAccommodationsReq>();
			mergeOrReplaceAccommodationsReq.ReplaceExistingAccommodations = ReplaceExistingAccommodations;
			mergeOrReplaceAccommodationsReq.SourceLuCourseId = SourceLuCourseId;
			mergeOrReplaceAccommodationsReq.SourcePersonId = SourcePersonId;
			mergeOrReplaceAccommodationsReq.DestLuCourseId = DestLuCourseId;
			mergeOrReplaceAccommodationsReq.DestPersonId = DestPersonId;
			base.Post("accommodations/mergeorreplaceaccommodations");
		}

		// Token: 0x06000311 RID: 785 RVA: 0x00009638 File Offset: 0x00007838
		public void ClearAccommodations(int PersonId, int CourseId, bool RequiresApproval)
		{
			ClearAccommodationsReq clearAccommodationsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ClearAccommodationsReq>();
			clearAccommodationsReq.PersonId = PersonId;
			clearAccommodationsReq.CourseId = CourseId;
			clearAccommodationsReq.RequiresApproval = RequiresApproval;
			base.Post<ClearAccommodationsReq>(clearAccommodationsReq, "accommodations/clearaccommodations");
		}

		// Token: 0x06000312 RID: 786 RVA: 0x00009674 File Offset: 0x00007874
		public IList<CourseRegistrationWithAccommodationsDTO> LoadStudentsRegisteredCoursesWithAccommodationsAndRequests(int PersonId, DateTime StartDate, DateTime EndDate, bool LoadAccommodations, bool IncludeOfflineAccommodations = false)
		{
			return base.GetMany<CourseRegistrationWithAccommodationsDTO>(string.Format("accommodations/studentsregisteredcourseswithaccommodationsandrequests/pid/{0}/range/{1}/{2}?loadaccommodations={3}&includeofflineaccommodations={4}", new object[]
			{
				PersonId,
				StartDate,
				EndDate,
				LoadAccommodations,
				IncludeOfflineAccommodations
			}), true);
		}

		// Token: 0x06000313 RID: 787 RVA: 0x000096C8 File Offset: 0x000078C8
		public void MarkAccommodationLetterIssued(int PersonId, params int[] LuCourseIds)
		{
			MarkAccommodationLetterIssuedReq markAccommodationLetterIssuedReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<MarkAccommodationLetterIssuedReq>();
			markAccommodationLetterIssuedReq.PersonId = PersonId;
			markAccommodationLetterIssuedReq.LuCourseIds = LuCourseIds;
			base.Post<MarkAccommodationLetterIssuedReq>(markAccommodationLetterIssuedReq, "accommodations/markaccommodationletterissued");
		}

		// Token: 0x06000314 RID: 788 RVA: 0x000096FA File Offset: 0x000078FA
		public DateTime? GetStudentAccommodationsExpiryDate(int PersonId)
		{
			return base.Get<DateTime?>(string.Format("accommodations/studentaccommodationsexpirydate/pid/{0}", PersonId), true);
		}
	}
}
