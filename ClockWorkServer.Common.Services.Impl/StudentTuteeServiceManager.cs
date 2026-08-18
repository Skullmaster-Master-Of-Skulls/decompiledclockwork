using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Tutoring;
using TechnoPro.Common.Core.Mappers.Tutoring;
using TechnoPro.Common.Core.Tutoring;
using TechnoPro.Common.ICore.Tutoring;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Tutoring;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000099 RID: 153
	public class StudentTuteeServiceManager : IStudentTutee, IService
	{
		// Token: 0x0600058E RID: 1422 RVA: 0x0001A078 File Offset: 0x00018278
		public GetStudentMyTutorsResp GetStudentMyTutors(GetStudentMyTutorsReq Request)
		{
			IStudentTuteeManager studentTuteeManager = new StudentTuteeManager(Request.GetOperationContext());
			IList<MyTutor> studentMyTutors = studentTuteeManager.GetStudentMyTutors(Request.StudentPersonId, Request.StartDateTime, Request.EndDate);
			GetStudentMyTutorsResp getStudentMyTutorsResp = new GetStudentMyTutorsResp();
			IList<MyTutorDTO> myTutors;
			if (studentMyTutors == null)
			{
				myTutors = null;
			}
			else
			{
				myTutors = studentMyTutors.ToList<MyTutor>().ConvertAll<MyTutorDTO>((MyTutor g) => g.ToDTO());
			}
			getStudentMyTutorsResp.MyTutors = myTutors;
			return getStudentMyTutorsResp;
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x0001A0EC File Offset: 0x000182EC
		public void MarkStudentCantFindTutor(MarkStudentCantFindTutorReq Request)
		{
			IStudentTuteeManager studentTuteeManager = new StudentTuteeManager(Request.GetOperationContext());
			studentTuteeManager.MarkStudentCantFindTutor(Request.StudentPersonId, Request.SearchLucid, Request.SearchLuc, Request.SearchString);
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x0001A128 File Offset: 0x00018328
		public void MarkStudentCantFindAvailability(MarkStudentCantFindAvailabilityReq Request)
		{
			IStudentTuteeManager studentTuteeManager = new StudentTuteeManager(Request.GetOperationContext());
			IStudentTuteeManager studentTuteeManager2 = studentTuteeManager;
			int studentPersonId = Request.StudentPersonId;
			IList<int> tutorPids = Request.TutorPids;
			studentTuteeManager2.MarkStudentCantFindAvailability(studentPersonId, (tutorPids != null) ? tutorPids.ToArray<int>() : null);
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x0001A164 File Offset: 0x00018364
		public GetTuteeStatusResp GetTuteeStatus(GetTuteeStatusReq Request)
		{
			IStudentTuteeManager studentTuteeManager = new StudentTuteeManager(Request.GetOperationContext());
			return new GetTuteeStatusResp
			{
				Status = studentTuteeManager.GetTuteeStatus(Request.StudentPersonId)
			};
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x0001A19C File Offset: 0x0001839C
		public IsConfidentialityAgreementSigningRequiredForStudentResp IsConfidentialityAgreementSigningRequiredForStudent(IsConfidentialityAgreementSigningRequiredForStudentReq Request)
		{
			IStudentTuteeManager studentTuteeManager = new StudentTuteeManager(Request.GetOperationContext());
			return new IsConfidentialityAgreementSigningRequiredForStudentResp
			{
				IsConfidentialityRequired = studentTuteeManager.IsConfidentialityAgreementSigningRequiredForStudent(Request.StudentPersonId)
			};
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x0001A1D4 File Offset: 0x000183D4
		public void RecordConfidentialityAgreementSignedByStudent(RecordConfidentialityAgreementSignedByStudentReq Request)
		{
			IStudentTuteeManager studentTuteeManager = new StudentTuteeManager(Request.GetOperationContext());
			studentTuteeManager.RecordConfidentialityAgreementSignedByStudent(Request.StudentPersonId);
		}
	}
}
