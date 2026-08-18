using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;

namespace TechnoPro.Common.ClientManager.ICore.AppointmentsTestBooking
{
	// Token: 0x02000085 RID: 133
	public interface IClassTestDefinitionClientManager : IWebService
	{
		// Token: 0x060003E9 RID: 1001
		ClassTestBaseDTO LoadClassTestBaseById(int ExamId);

		// Token: 0x060003EA RID: 1002
		int CreateClassTestDefinitionBase(ClassTestBaseDTO ClassTestBase);

		// Token: 0x060003EB RID: 1003
		void UpdateClassTestDefinitionBase(ClassTestBaseDTO ClassTestBase);

		// Token: 0x060003EC RID: 1004
		IList<ClassTestDTO> LoadClassTestDefinitionsByCourse(int LuCourseId);

		// Token: 0x060003ED RID: 1005
		void MarkTestDelivered(int ExamId, string TestDeliveredMessage);

		// Token: 0x060003EE RID: 1006
		ClassTestDTO LoadClassTestById(int ExamId);

		// Token: 0x060003EF RID: 1007
		ClassTestForEditDTO LoadClassTestForEditById(int ExamId);

		// Token: 0x060003F0 RID: 1008
		void UpdateClassTestDefinition(ClassTestDTO ClassTest);

		// Token: 0x060003F1 RID: 1009
		void UpdateInstructorSubmittedTestInfo(int ExamId, int InstructorId);

		// Token: 0x060003F2 RID: 1010
		void UpdateInstructorContactedInfo(int ExamId, DateTime? InstructorContactedDate, string Note);

		// Token: 0x060003F3 RID: 1011
		void UpdateTestPickedUp(int ExamId, DateTime? DatePickedUp, string Note);

		// Token: 0x060003F4 RID: 1012
		ClassTestDTO LoadClassTestDefinitionByIdAndConfirmInstructorOrAltContact(int ExamId, int InstructorId, int AlternateContactId);

		// Token: 0x060003F5 RID: 1013
		ClassTestForExamRequestDTO LoadClassTestForExamRequestById(int ExamId);

		// Token: 0x060003F6 RID: 1014
		IList<ClassTestForExamRequestDTO> LoadClassTestsForExamRequestByDateRange(int LuCourseId, DateTime StartDate, DateTime EndDate, eClassTestType TestType = eClassTestType.Unknown);

		// Token: 0x060003F7 RID: 1015
		IList<ClassTestForDisplayDTO> LoadClassTestsForDisplay(DateTime StartDate, DateTime EndDate);

		// Token: 0x060003F8 RID: 1016
		void RemoveInstructorHasSubmittedInformationAboutThisTestMarker(int examId);
	}
}
