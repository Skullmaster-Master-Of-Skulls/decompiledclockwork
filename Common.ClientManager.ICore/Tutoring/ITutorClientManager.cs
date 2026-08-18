using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentBookingStudent.BookingRequest;
using TechnoPro.ClockWorkServer.Contracts.DTO.Tutoring;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Tutoring;

namespace TechnoPro.Common.ClientManager.ICore.Tutoring
{
	// Token: 0x0200000B RID: 11
	public interface ITutorClientManager : IWebService
	{
		// Token: 0x06000032 RID: 50
		IList<TutorWithActiveStatusDTO> LoadAllTutors();

		// Token: 0x06000033 RID: 51
		int CreateTutor(string FirstName, string MiddleName, string LastName, string StudentNumber);

		// Token: 0x06000034 RID: 52
		void ActivateTutor(int TutorPersonId);

		// Token: 0x06000035 RID: 53
		void DeActivateTutor(int TutorPersonId);

		// Token: 0x06000036 RID: 54
		TutorWithActiveStatusDTO LoadTutorWithActiveStatusById(int TutorPersonId);

		// Token: 0x06000037 RID: 55
		SearchForTutorsResp SearchForTutors(int LuCourseId, string SearchString, int MaxResultCount = 100);

		// Token: 0x06000038 RID: 56
		AppointmentBookingResDTO TryToBookTutorAppointment(AppointmentBookingReqDTO BookingRequest, bool BookAppointmentNow = true);

		// Token: 0x06000039 RID: 57
		TutorDTO LoadTutorById(int PersonId);

		// Token: 0x0600003A RID: 58
		void RecordConfidentialityAgreementSignedByTutor(int TutorPersonId);

		// Token: 0x0600003B RID: 59
		eTutorStatus GetTutorStatus(int TutorPersonId);

		// Token: 0x0600003C RID: 60
		void RegisterTutorByExistingPersonId(int PersonId);

		// Token: 0x0600003D RID: 61
		TutorAppointmentDTO LoadTutorAppointment(int AppointmentId);

		// Token: 0x0600003E RID: 62
		IDictionary<int, eTutorStatus> GetTutorStatuses(int[] tutorPersonIds);
	}
}
