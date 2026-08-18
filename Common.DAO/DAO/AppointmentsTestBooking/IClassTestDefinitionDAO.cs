using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;

namespace TechnoPro.Common.DAO.AppointmentsTestBooking
{
	// Token: 0x020000B8 RID: 184
	public interface IClassTestDefinitionDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060004E3 RID: 1251
		int CreateClassTestDefinition(ClassTest ClassTestDefinition);

		// Token: 0x060004E4 RID: 1252
		int CreateClassTestDefinitionBase(ClassTestBase ClassTestBase);

		// Token: 0x060004E5 RID: 1253
		void DeleteClassTestDefinition(int ExamId);

		// Token: 0x060004E6 RID: 1254
		void UpdateClassTestDefinition(ClassTest ClassTestDefinition);

		// Token: 0x060004E7 RID: 1255
		void UpdateClassTestDefinitionBase(ClassTestBase ClassTestBase);

		// Token: 0x060004E8 RID: 1256
		void MarkTestDelivered(int ExamId, string TestDeliveredMessage);

		// Token: 0x060004E9 RID: 1257
		IList<ClassTest> LoadClassTestDefinitionsByCourse(int LuCourseId, eClassTestType testType = eClassTestType.Unknown);

		// Token: 0x060004EA RID: 1258
		ClassTest LoadClassTestDefinitionById(int ExamId);

		// Token: 0x060004EB RID: 1259
		ClassTest LoadClassTestDefinitionByAppointmentId(int AppointmentId);

		// Token: 0x060004EC RID: 1260
		ClassTest LoadClassTestDefinitionByIdAndConfirmInstructorOrAltContact(int ExamId, int InstructorId, int AlternateContactId);

		// Token: 0x060004ED RID: 1261
		ClassTestBase LoadClassTestBaseById(int ExamId);

		// Token: 0x060004EE RID: 1262
		bool LoadClassTestWasUpdatedByInstructor(int ExamId);

		// Token: 0x060004EF RID: 1263
		void SetInstructorLastModified(int ExamId, int InstructorId);

		// Token: 0x060004F0 RID: 1264
		void ClearInstructorLastModified(int ExamId);

		// Token: 0x060004F1 RID: 1265
		void UpdateInstructorContactedInfo(int ExamId, DateTime? InstructorContactedDate, string Note);

		// Token: 0x060004F2 RID: 1266
		void UpdateTestPickedUp(int ExamId, DateTime? DatePickedUp, string Note);

		// Token: 0x060004F3 RID: 1267
		ClassTestForExamRequest LoadClassTestForExamRequestById(int ExamId);

		// Token: 0x060004F4 RID: 1268
		IList<ClassTestForExamRequest> LoadClassTestsForExamRequestByDateRange(int LuCourseId, DateTime StartDate, DateTime EndDate, eClassTestType testType = eClassTestType.Unknown);

		// Token: 0x060004F5 RID: 1269
		IList<ClassTestForDisplay> LoadClassTestsForDisplayWithoutInstructorFormData(DateTime StartDate, DateTime EndDate);

		// Token: 0x060004F6 RID: 1270
		void RemoveInstructorHasSubmittedInformationAboutThisTestMarker(int examId);
	}
}
