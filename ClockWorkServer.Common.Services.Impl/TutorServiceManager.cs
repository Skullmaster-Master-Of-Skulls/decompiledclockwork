using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Tutoring;
using TechnoPro.Common.Core.Mappers.AppointmentBookingStudent.BookingRequest;
using TechnoPro.Common.Core.Mappers.Tutoring;
using TechnoPro.Common.Core.Tutoring;
using TechnoPro.Common.ICore.Tutoring;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AppointmentBookingStudent.BookingRequest;
using TechnoPro.Common.Public.Entities.Tutoring;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x0200009A RID: 154
	public class TutorServiceManager : ITutor, IService
	{
		// Token: 0x06000595 RID: 1429 RVA: 0x0001A1FC File Offset: 0x000183FC
		public SearchForTutorsResp SearchForTutors(SearchForTutorsReq Request)
		{
			ITutorManager tutorManager = new TutorManager(Request.GetOperationContext());
			bool includingCourse;
			IList<Tutor> list = tutorManager.SearchForTutors(Request.LuCourseId, Request.SearchString, (Request.MaxReturnResults > 0) ? Request.MaxReturnResults : 100, out includingCourse);
			SearchForTutorsResp searchForTutorsResp = new SearchForTutorsResp();
			IList<TutorDTO> tutors;
			if (list == null)
			{
				tutors = null;
			}
			else
			{
				tutors = list.ToList<Tutor>().ConvertAll<TutorDTO>((Tutor g) => g.ToDTO());
			}
			searchForTutorsResp.Tutors = tutors;
			searchForTutorsResp.IncludingCourse = includingCourse;
			return searchForTutorsResp;
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x0001A288 File Offset: 0x00018488
		public LoadTutorByPersonIdResp LoadTutorByPersonId(LoadTutorByPersonIdReq Request)
		{
			ITutorManager tutorManager = new TutorManager(Request.GetOperationContext());
			Tutor tutor = tutorManager.LoadTutorByPersonId(Request.PersonId);
			return new LoadTutorByPersonIdResp
			{
				Tutor = ((tutor != null) ? tutor.ToDTO() : null)
			};
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x0001A2CC File Offset: 0x000184CC
		public TryToBookTutorAppointmentResp TryToBookTutorAppointment(TryToBookTutorAppointmentReq Request)
		{
			ITutorManager tutorManager = new TutorManager(Request.GetOperationContext());
			AppointmentBookingRes appointmentBookingRes = tutorManager.TryToBookTutorAppointment(Request.BookingRequest.ToDomainObject(), Request.BookAppointmentNow);
			return new TryToBookTutorAppointmentResp
			{
				BookingResult = ((appointmentBookingRes != null) ? appointmentBookingRes.ToDTO() : null)
			};
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x0001A31C File Offset: 0x0001851C
		public void RecordConfidentialityAgreementSignedByTutor(RecordConfidentialityAgreementSignedByTutorReq Request)
		{
			ITutorManager tutorManager = new TutorManager(Request.GetOperationContext());
			tutorManager.RecordConfidentialityAgreementSignedByTutor(Request.TutorPersonId);
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x0001A344 File Offset: 0x00018544
		public IsConfidentialityAgreementSigningRequiredForTutorResp IsConfidentialityAgreementSigningRequiredForTutor(IsConfidentialityAgreementSigningRequiredForTutorReq Request)
		{
			ITutorManager tutorManager = new TutorManager(Request.GetOperationContext());
			bool isConfidentialityAgreementSigningRequired = tutorManager.IsConfidentialityAgreementSigningRequiredForTutor(Request.TutorPersonId);
			return new IsConfidentialityAgreementSigningRequiredForTutorResp
			{
				IsConfidentialityAgreementSigningRequired = isConfidentialityAgreementSigningRequired
			};
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x0001A37C File Offset: 0x0001857C
		public CreateTutorResp CreateTutor(CreateTutorReq Request)
		{
			ITutorManager tutorManager = new TutorManager(Request.GetOperationContext());
			int personId = tutorManager.CreateTutor(Request.FirstName, Request.MiddleName, Request.LastName, Request.StudentNumber);
			return new CreateTutorResp
			{
				PersonId = personId
			};
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x0001A3C8 File Offset: 0x000185C8
		public void RegisterTutorByExistingPersonId(RegisterTutorByExistingPersonIdReq Request)
		{
			ITutorManager tutorManager = new TutorManager(Request.GetOperationContext());
			tutorManager.RegisterTutorByExistingPersonId(Request.TutorPersonId);
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x0001A3F0 File Offset: 0x000185F0
		public GetTutorStatusResp GetTutorStatus(GetTutorStatusReq Request)
		{
			ITutorManager tutorManager = new TutorManager(Request.GetOperationContext());
			eTutorStatus tutorStatus = tutorManager.GetTutorStatus(Request.TutorPersonId);
			return new GetTutorStatusResp
			{
				Status = tutorStatus
			};
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x0001A428 File Offset: 0x00018628
		public LoadAllTutorsResp LoadAllTutors(LoadAllTutorsReq Request)
		{
			ITutorManager tutorManager = new TutorManager(Request.GetOperationContext());
			IList<TutorWithActiveStatus> list = tutorManager.LoadAllTutors();
			LoadAllTutorsResp loadAllTutorsResp = new LoadAllTutorsResp();
			IList<TutorWithActiveStatusDTO> tutors;
			if (list == null)
			{
				tutors = null;
			}
			else
			{
				tutors = list.ToList<TutorWithActiveStatus>().ConvertAll<TutorWithActiveStatusDTO>((TutorWithActiveStatus g) => g.ToDTO());
			}
			loadAllTutorsResp.Tutors = tutors;
			return loadAllTutorsResp;
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x0001A48C File Offset: 0x0001868C
		public void ActivateTutor(ActivateTutorReq Request)
		{
			ITutorManager tutorManager = new TutorManager(Request.GetOperationContext());
			tutorManager.ActivateTutor(Request.TutorPersonId);
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x0001A4B4 File Offset: 0x000186B4
		public void DeActivateTutor(DeActivateTutorReq Request)
		{
			ITutorManager tutorManager = new TutorManager(Request.GetOperationContext());
			tutorManager.DeActivateTutor(Request.TutorPersonId);
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x0001A4DC File Offset: 0x000186DC
		public LoadTutorAppointmentResp LoadTutorAppointment(LoadTutorAppointmentReq Request)
		{
			ITutorManager tutorManager = new TutorManager(Request.GetOperationContext());
			TutorAppointment tutorAppointment = tutorManager.LoadTutorAppointment(Request.AppointmentId);
			return new LoadTutorAppointmentResp
			{
				TutorAppointment = ((tutorAppointment != null) ? tutorAppointment.ToDTO() : null)
			};
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x0001A520 File Offset: 0x00018720
		public LoadTutorWithActiveStatusByIdResp LoadTutorWithActiveStatusById(LoadTutorWithActiveStatusByIdReq Request)
		{
			ITutorManager tutorManager = new TutorManager(Request.GetOperationContext());
			TutorWithActiveStatus tutorWithActiveStatus = tutorManager.LoadTutorWithActiveStatusById(Request.TutorPersonId);
			return new LoadTutorWithActiveStatusByIdResp
			{
				TutorWithStatus = ((tutorWithActiveStatus != null) ? tutorWithActiveStatus.ToDTO() : null)
			};
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x0001A564 File Offset: 0x00018764
		public GetTutorStatusesResp GetTutorStatuses(GetTutorStatusesReq Request)
		{
			ITutorManager tutorManager = new TutorManager(Request.GetOperationContext());
			return new GetTutorStatusesResp
			{
				TutorsWithStatus = tutorManager.GetTutorStatuses(Request.TutorPersonIds)
			};
		}
	}
}
