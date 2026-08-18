using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentBookingStudent.BookingRequest;
using TechnoPro.Common.Public.Entities.Tutoring;

namespace TechnoPro.Common.ICore.Tutoring
{
	// Token: 0x02000020 RID: 32
	public interface ITutorManager : IBaseOperationContext<OperationContext>
	{
		// Token: 0x060000CA RID: 202
		IList<Tutor> SearchForTutors(int LuCourseId, string SearchString, int MaxResultCount, out bool includeCourses);

		// Token: 0x060000CB RID: 203
		Tutor LoadTutorByPersonId(int PersonId);

		// Token: 0x060000CC RID: 204
		AppointmentBookingRes TryToBookTutorAppointment(AppointmentBookingReq BookingRequest, bool BookAppointmentNow = true);

		// Token: 0x060000CD RID: 205
		void RecordConfidentialityAgreementSignedByTutor(int TutorPersonId);

		// Token: 0x060000CE RID: 206
		bool IsConfidentialityAgreementSigningRequiredForTutor(int TutorPersonId);

		// Token: 0x060000CF RID: 207
		eTutorStatus GetTutorStatus(int TutorPersonId);

		// Token: 0x060000D0 RID: 208
		int CreateTutor(string FirstName, string MiddleName, string LastName, string StudentNumber);

		// Token: 0x060000D1 RID: 209
		void RegisterTutorByExistingPersonId(int PersonId);

		// Token: 0x060000D2 RID: 210
		IList<TutorWithActiveStatus> LoadAllTutors();

		// Token: 0x060000D3 RID: 211
		void ActivateTutor(int TutorPersonId);

		// Token: 0x060000D4 RID: 212
		void DeActivateTutor(int TutorPersonId);

		// Token: 0x060000D5 RID: 213
		TutorAppointment LoadTutorAppointment(int AppointmentId);

		// Token: 0x060000D6 RID: 214
		TutorWithActiveStatus LoadTutorWithActiveStatusById(int TutorPersonId);

		// Token: 0x060000D7 RID: 215
		IDictionary<int, eTutorStatus> GetTutorStatuses(int[] tutorPersonIds);
	}
}
