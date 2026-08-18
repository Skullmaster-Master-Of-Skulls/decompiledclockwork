using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Accommodations;
using TechnoPro.ClockWorkServer.Contracts.DTO.CourseRegistrations;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.DynamicForms;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.DynamicForms
{
	// Token: 0x02000062 RID: 98
	public class AccommodationsClientManager : IAccommodationsClientManager, IWebService
	{
		// Token: 0x06000381 RID: 897 RVA: 0x0000FA60 File Offset: 0x0000DC60
		public IList<AccommodationDataDTO> LoadAccommodationsByStudentAndCourseOrTemplate(int PersonId, int LuCourseId)
		{
			bool flag;
			return this.LoadAccommodationsByStudentAndCourseOrTemplate(PersonId, LuCourseId, out flag);
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0000FA7C File Offset: 0x0000DC7C
		public IList<AccommodationDataDTO> LoadAccommodationsByStudentAndCourseOrTemplate(int PersonId, int LuCourseId, out bool IsUsingTemplateAccommodations)
		{
			LoadAccommodationsByStudentAndCourseOrTemplateReq loadAccommodationsByStudentAndCourseOrTemplateReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAccommodationsByStudentAndCourseOrTemplateReq>();
			loadAccommodationsByStudentAndCourseOrTemplateReq.PersonId = PersonId;
			loadAccommodationsByStudentAndCourseOrTemplateReq.CourseId = LuCourseId;
			LoadAccommodationsByStudentAndCourseOrTemplateResp loadAccommodationsByStudentAndCourseOrTemplateResp = ClientServiceFactory.GetClientInstance<IAccommodations>().LoadAccommodationsByStudentAndCourseOrTemplate(loadAccommodationsByStudentAndCourseOrTemplateReq);
			IsUsingTemplateAccommodations = (loadAccommodationsByStudentAndCourseOrTemplateResp == null || loadAccommodationsByStudentAndCourseOrTemplateResp.IsUsingTemplateAccommodations);
			return (loadAccommodationsByStudentAndCourseOrTemplateResp == null) ? null : loadAccommodationsByStudentAndCourseOrTemplateResp.Accommodations;
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0000FAD0 File Offset: 0x0000DCD0
		public void MergeOrReplaceAccommodations(bool ReplaceExistingAccommodations, int SourcePersonId, int SourceLuCourseId, int DestPersonId, int DestLuCourseId)
		{
			MergeOrReplaceAccommodationsReq mergeOrReplaceAccommodationsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<MergeOrReplaceAccommodationsReq>();
			mergeOrReplaceAccommodationsReq.ReplaceExistingAccommodations = ReplaceExistingAccommodations;
			mergeOrReplaceAccommodationsReq.SourceLuCourseId = SourceLuCourseId;
			mergeOrReplaceAccommodationsReq.SourcePersonId = SourcePersonId;
			mergeOrReplaceAccommodationsReq.DestLuCourseId = DestLuCourseId;
			mergeOrReplaceAccommodationsReq.DestPersonId = DestPersonId;
			ClientServiceFactory.GetClientInstance<IAccommodations>().MergeOrReplaceAccommodations(mergeOrReplaceAccommodationsReq);
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0000FB20 File Offset: 0x0000DD20
		public void ClearAccommodations(int PersonId, int CourseId, bool RequiresApproval)
		{
			ClearAccommodationsReq clearAccommodationsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ClearAccommodationsReq>();
			clearAccommodationsReq.PersonId = PersonId;
			clearAccommodationsReq.CourseId = CourseId;
			clearAccommodationsReq.RequiresApproval = RequiresApproval;
			ClientServiceFactory.GetClientInstance<IAccommodations>().ClearAccommodations(clearAccommodationsReq);
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0000FB60 File Offset: 0x0000DD60
		public IList<CourseRegistrationWithAccommodationsDTO> LoadStudentsRegisteredCoursesWithAccommodationsAndRequests(int PersonId, DateTime StartDate, DateTime EndDate, bool LoadAccommodations, bool IncludeOfflineAccommodations = false)
		{
			LoadStudentsRegisteredCoursesWithAccommodationsAndRequestsReq loadStudentsRegisteredCoursesWithAccommodationsAndRequestsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadStudentsRegisteredCoursesWithAccommodationsAndRequestsReq>();
			loadStudentsRegisteredCoursesWithAccommodationsAndRequestsReq.PersonId = PersonId;
			loadStudentsRegisteredCoursesWithAccommodationsAndRequestsReq.StartDate = StartDate;
			loadStudentsRegisteredCoursesWithAccommodationsAndRequestsReq.EndDate = EndDate;
			loadStudentsRegisteredCoursesWithAccommodationsAndRequestsReq.LoadAccommodations = LoadAccommodations;
			loadStudentsRegisteredCoursesWithAccommodationsAndRequestsReq.IncludeOfflineAccommodations = IncludeOfflineAccommodations;
			return ClientServiceFactory.GetClientInstance<IAccommodations>().LoadStudentsRegisteredCoursesWithAccommodationsAndRequests(loadStudentsRegisteredCoursesWithAccommodationsAndRequestsReq).CourseRegistrationsWithAccommodations;
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0000FBB8 File Offset: 0x0000DDB8
		public void MarkAccommodationLetterIssued(int PersonId, params int[] LuCourseIds)
		{
			MarkAccommodationLetterIssuedReq markAccommodationLetterIssuedReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<MarkAccommodationLetterIssuedReq>();
			markAccommodationLetterIssuedReq.PersonId = PersonId;
			markAccommodationLetterIssuedReq.LuCourseIds = LuCourseIds;
			ClientServiceFactory.GetClientInstance<IAccommodations>().MarkAccommodationLetterIssued(markAccommodationLetterIssuedReq);
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0000FBF0 File Offset: 0x0000DDF0
		public DateTime? GetStudentAccommodationsExpiryDate(int PersonId)
		{
			GetStudentAccommodationsExpiryDateReq getStudentAccommodationsExpiryDateReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetStudentAccommodationsExpiryDateReq>();
			getStudentAccommodationsExpiryDateReq.PersonId = PersonId;
			return ClientServiceFactory.GetClientInstance<IAccommodations>().GetStudentAccommodationsExpiryDate(getStudentAccommodationsExpiryDateReq).ExpiryDate;
		}
	}
}
