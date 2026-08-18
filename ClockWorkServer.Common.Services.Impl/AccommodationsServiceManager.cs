using System;
using System.Collections.Generic;
using System.Linq;
using ClockWorkLogger;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Accommodations;
using TechnoPro.ClockWorkServer.Contracts.DTO.CourseRegistrations;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.Common.Core.DynamicForms;
using TechnoPro.Common.Core.Mappers.Accommodations;
using TechnoPro.Common.Core.Mappers.CourseRegistrations;
using TechnoPro.Common.Core.Mappers.DynamicForms;
using TechnoPro.Common.ICore.DynamicForms;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Accommodations;
using TechnoPro.Common.Public.Entities.CourseRegistrations;
using TechnoPro.Common.Public.Entities.DynamicForms;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x0200003A RID: 58
	public class AccommodationsServiceManager : IAccommodations, IService
	{
		// Token: 0x06000235 RID: 565 RVA: 0x0000B030 File Offset: 0x00009230
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000B044 File Offset: 0x00009244
		public LoadAccommodationChangesResp LoadAccommodationChanges(LoadAccommodationChangesReq Request)
		{
			IAccommodationsManager accommodationsManager = new AccommodationsManager(Request.GetOperationContext());
			List<DynamicDataChange> list = accommodationsManager.LoadAccommodationChanges(Request.PersonId, Request.LuCourseId, Request.SinceDate);
			LoadAccommodationChangesResp loadAccommodationChangesResp = new LoadAccommodationChangesResp();
			loadAccommodationChangesResp.Changes = list.ConvertAll<DynamicDataChangeDTO>((DynamicDataChange f) => f.ToDTO());
			return loadAccommodationChangesResp;
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000B0AC File Offset: 0x000092AC
		public LoadStudentsRegisteredCoursesWithAccommodationsResp LoadStudentsRegisteredCoursesWithAccommodations(LoadStudentsRegisteredCoursesWithAccommodationsReq Request)
		{
			IAccommodationsManager accommodationsManager = new AccommodationsManager(Request.GetOperationContext());
			IList<CourseRegistrationWithAccommodations> source = accommodationsManager.LoadStudentsRegisteredCoursesWithAccommodations(Request.PersonId, Request.StartDate, Request.EndDate, Request.LoadAccommodations, Request.IncludeOfflineAccommodations);
			LoadStudentsRegisteredCoursesWithAccommodationsResp loadStudentsRegisteredCoursesWithAccommodationsResp = new LoadStudentsRegisteredCoursesWithAccommodationsResp();
			loadStudentsRegisteredCoursesWithAccommodationsResp.CourseRegistrationsWithAccommodations = source.ToList<CourseRegistrationWithAccommodations>().ConvertAll<CourseRegistrationWithAccommodationsDTO>((CourseRegistrationWithAccommodations f) => f.ToDTO());
			return loadStudentsRegisteredCoursesWithAccommodationsResp;
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000B128 File Offset: 0x00009328
		public LoadAccommodationsByStudentAndCourseOrTemplateResp LoadAccommodationsByStudentAndCourseOrTemplate(LoadAccommodationsByStudentAndCourseOrTemplateReq Request)
		{
			IAccommodationsManager accommodationsManager = new AccommodationsManager(Request.GetOperationContext());
			bool isUsingTemplateAccommodations;
			IList<AccommodationData> source = accommodationsManager.LoadAccommodationsByStudentAndCourseOrTemplate(Request.PersonId, Request.CourseId, out isUsingTemplateAccommodations);
			LoadAccommodationsByStudentAndCourseOrTemplateResp loadAccommodationsByStudentAndCourseOrTemplateResp = new LoadAccommodationsByStudentAndCourseOrTemplateResp();
			loadAccommodationsByStudentAndCourseOrTemplateResp.Accommodations = source.ToList<AccommodationData>().ConvertAll<AccommodationDataDTO>((AccommodationData f) => f.ToDTO());
			loadAccommodationsByStudentAndCourseOrTemplateResp.IsUsingTemplateAccommodations = isUsingTemplateAccommodations;
			return loadAccommodationsByStudentAndCourseOrTemplateResp;
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000B19C File Offset: 0x0000939C
		public void ClearAccommodations(ClearAccommodationsReq Request)
		{
			IAccommodationsManager accommodationsManager = new AccommodationsManager(Request.GetOperationContext());
			accommodationsManager.ClearAccommodations(Request.PersonId, Request.CourseId, Request.RequiresApproval);
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000B1D0 File Offset: 0x000093D0
		public LoadStudentsRegisteredCoursesWithAccommodationsAndRequestsResp LoadStudentsRegisteredCoursesWithAccommodationsAndRequests(LoadStudentsRegisteredCoursesWithAccommodationsAndRequestsReq Request)
		{
			IAccommodationsManager accommodationsManager = new AccommodationsManager(Request.GetOperationContext());
			IList<CourseRegistrationWithAccommodations> list = accommodationsManager.LoadStudentsRegisteredCoursesWithAccommodationsAndRequests(Request.PersonId, Request.StartDate, Request.EndDate, Request.LoadAccommodations, Request.IncludeOfflineAccommodations);
			IList<CourseRegistrationWithAccommodationsDTO> courseRegistrationsWithAccommodations;
			try
			{
				IList<CourseRegistrationWithAccommodationsDTO> list2;
				if (list != null)
				{
					list2 = list.ToList<CourseRegistrationWithAccommodations>().ConvertAll<CourseRegistrationWithAccommodationsDTO>((CourseRegistrationWithAccommodations g) => g.ToDTO());
				}
				else
				{
					list2 = null;
				}
				courseRegistrationsWithAccommodations = list2;
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Trace("LoadStudentsRegisteredCoursesWithAccommodationsAndRequestsXYZ:ERROR={0}", ex.ToString());
				courseRegistrationsWithAccommodations = new List<CourseRegistrationWithAccommodationsDTO>();
			}
			return new LoadStudentsRegisteredCoursesWithAccommodationsAndRequestsResp
			{
				CourseRegistrationsWithAccommodations = courseRegistrationsWithAccommodations
			};
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000B288 File Offset: 0x00009488
		public void MarkAccommodationLetterIssued(MarkAccommodationLetterIssuedReq Request)
		{
			IAccommodationsManager accommodationsManager = new AccommodationsManager(Request.GetOperationContext());
			accommodationsManager.MarkAccommodationLetterIssued(Request.PersonId, Request.LuCourseIds.ToArray<int>());
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000B2BC File Offset: 0x000094BC
		public void MergeOrReplaceAccommodations(MergeOrReplaceAccommodationsReq Request)
		{
			IAccommodationsManager accommodationsManager = new AccommodationsManager(Request.GetOperationContext());
			accommodationsManager.MergeOrReplaceAccommodations(Request.ReplaceExistingAccommodations, Request.SourcePersonId, Request.SourceLuCourseId, Request.DestPersonId, Request.DestLuCourseId);
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000B2FC File Offset: 0x000094FC
		public GetStudentAccommodationsExpiryDateResp GetStudentAccommodationsExpiryDate(GetStudentAccommodationsExpiryDateReq Request)
		{
			IAccommodationsManager accommodationsManager = new AccommodationsManager(Request.GetOperationContext());
			return new GetStudentAccommodationsExpiryDateResp
			{
				ExpiryDate = accommodationsManager.GetStudentAccommodationsExpiryDate(Request.PersonId)
			};
		}
	}
}
