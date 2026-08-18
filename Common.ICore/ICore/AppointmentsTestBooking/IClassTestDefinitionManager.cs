using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;

namespace TechnoPro.Common.ICore.AppointmentsTestBooking
{
	// Token: 0x020000C5 RID: 197
	public interface IClassTestDefinitionManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060005EE RID: 1518
		int CreateClassTestDefinition(ClassTest ClassTestDefinition);

		// Token: 0x060005EF RID: 1519
		void DeleteClassTestDefinition(int ExamId);

		// Token: 0x060005F0 RID: 1520
		void UpdateClassTestDefinition(ClassTest ClassTestDefinition);

		// Token: 0x060005F1 RID: 1521
		void MarkTestDelivered(int ExamId, string TestDeliveredMessage);

		// Token: 0x060005F2 RID: 1522
		IList<ClassTest> LoadClassTestDefinitionsByCourse(int LuCourseId);

		// Token: 0x060005F3 RID: 1523
		ClassTest LoadClassTestDefinitionById(int ExamId);

		// Token: 0x060005F4 RID: 1524
		ClassTest LoadClassTestDefinitionByAppointmentId(int Appointment);

		// Token: 0x060005F5 RID: 1525
		ClassTest LoadClassTestDefinitionByIdAndConfirmInstructorOrAltContact(int ExamId, int InstructorId, int AlternateContactId);

		// Token: 0x060005F6 RID: 1526
		int CreateClassTestDefinitionBase(ClassTestBase ClassTestBase);

		// Token: 0x060005F7 RID: 1527
		void UpdateClassTestDefinitionBase(ClassTestBase ClassTestBase);

		// Token: 0x060005F8 RID: 1528
		ClassTestBase LoadClassTestBaseById(int ExamId);

		// Token: 0x060005F9 RID: 1529
		ClassTestForEdit LoadClassTestForEditById(int ExamId);

		// Token: 0x060005FA RID: 1530
		void UpdateInstructorSubmittedTestInfo(int ExamId, int InstructorId);

		// Token: 0x060005FB RID: 1531
		void UpdateInstructorContactedInfo(int ExamId, DateTime? InstructorContactedDate, string Note);

		// Token: 0x060005FC RID: 1532
		void UpdateTestPickedUp(int ExamId, DateTime? DatePickedUp, string Note);

		// Token: 0x060005FD RID: 1533
		ClassTestForExamRequest LoadClassTestForExamRequestById(int ExamId);

		// Token: 0x060005FE RID: 1534
		IList<ClassTestForExamRequest> LoadClassTestsForExamRequestByDateRange(int LuCourseId, DateTime StartDate, DateTime EndDate, eClassTestType testType = eClassTestType.Unknown);

		// Token: 0x060005FF RID: 1535
		IList<ClassTestForDisplay> LoadClassTestsForDisplay(DateTime StartDate, DateTime EndDate);

		// Token: 0x06000600 RID: 1536
		void RemoveInstructorHasSubmittedInformationAboutThisTestMarker(int examId);
	}
}
