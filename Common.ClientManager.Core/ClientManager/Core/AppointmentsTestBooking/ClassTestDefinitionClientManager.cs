using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.AppointmentsTestBooking;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.AppointmentsTestBooking
{
	// Token: 0x0200008B RID: 139
	public class ClassTestDefinitionClientManager : IClassTestDefinitionClientManager, IWebService
	{
		// Token: 0x060004FF RID: 1279 RVA: 0x00016754 File Offset: 0x00014954
		public ClassTestBaseDTO LoadClassTestBaseById(int ExamId)
		{
			LoadClassTestBaseByIdReq loadClassTestBaseByIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadClassTestBaseByIdReq>();
			loadClassTestBaseByIdReq.ExamId = ExamId;
			return ClientServiceFactory.GetClientInstance<IClassTestDefinition>().LoadClassTestBaseById(loadClassTestBaseByIdReq).ClassTestBase;
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x0001678C File Offset: 0x0001498C
		public int CreateClassTestDefinitionBase(ClassTestBaseDTO ClassTestBase)
		{
			CreateClassTestDefinitionBaseReq createClassTestDefinitionBaseReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateClassTestDefinitionBaseReq>();
			createClassTestDefinitionBaseReq.ClassTestBase = ClassTestBase;
			return ClientServiceFactory.GetClientInstance<IClassTestDefinition>().CreateClassTestDefinitionBase(createClassTestDefinitionBaseReq).ExamId;
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x000167C4 File Offset: 0x000149C4
		public void UpdateClassTestDefinitionBase(ClassTestBaseDTO ClassTestBase)
		{
			UpdateClassTestDefinitionBaseReq updateClassTestDefinitionBaseReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateClassTestDefinitionBaseReq>();
			updateClassTestDefinitionBaseReq.ClassTest = ClassTestBase;
			ClientServiceFactory.GetClientInstance<IClassTestDefinition>().UpdateClassTestDefinitionBase(updateClassTestDefinitionBaseReq);
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x000167F4 File Offset: 0x000149F4
		public void UpdateClassTestDefinition(ClassTestDTO ClassTest)
		{
			UpdateClassTestDefinitionBaseReq updateClassTestDefinitionBaseReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateClassTestDefinitionBaseReq>();
			updateClassTestDefinitionBaseReq.ClassTest = ClassTest;
			ClientServiceFactory.GetClientInstance<IClassTestDefinition>().UpdateClassTestDefinitionBase(updateClassTestDefinitionBaseReq);
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x00016824 File Offset: 0x00014A24
		public IList<ClassTestDTO> LoadClassTestDefinitionsByCourse(int LuCourseId)
		{
			LoadClassTestDefinitionsReq loadClassTestDefinitionsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadClassTestDefinitionsReq>();
			loadClassTestDefinitionsReq.LuCourseId = LuCourseId;
			return ClientServiceFactory.GetClientInstance<IClassTestDefinition>().LoadClassTestDefinitions(loadClassTestDefinitionsReq).ClassTests;
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0001685C File Offset: 0x00014A5C
		public void MarkTestDelivered(int ExamId, string TestDeliveredMessage)
		{
			UpdateTestDeliveredReq updateTestDeliveredReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateTestDeliveredReq>();
			updateTestDeliveredReq.ExamId = ExamId;
			updateTestDeliveredReq.TestDeliveredMessage = TestDeliveredMessage;
			ClientServiceFactory.GetClientInstance<IClassTestDefinition>().UpdateTestDelivered(updateTestDeliveredReq);
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x00016894 File Offset: 0x00014A94
		public ClassTestDTO LoadClassTestById(int ExamId)
		{
			LoadClassTestByIdReq loadClassTestByIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadClassTestByIdReq>();
			loadClassTestByIdReq.ExamId = ExamId;
			return ClientServiceFactory.GetClientInstance<IClassTestDefinition>().LoadClassTestById(loadClassTestByIdReq).ClassTest;
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x000168CC File Offset: 0x00014ACC
		public ClassTestForEditDTO LoadClassTestForEditById(int ExamId)
		{
			LoadClassTestForEditByIdReq loadClassTestForEditByIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadClassTestForEditByIdReq>();
			loadClassTestForEditByIdReq.ExamId = ExamId;
			return ClientServiceFactory.GetClientInstance<IClassTestDefinition>().LoadClassTestForEditById(loadClassTestForEditByIdReq).ClassTestForEdit;
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x00016904 File Offset: 0x00014B04
		public void UpdateInstructorSubmittedTestInfo(int ExamId, int InstructorId)
		{
			UpdateInstructorSubmittedTestInfoReq updateInstructorSubmittedTestInfoReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateInstructorSubmittedTestInfoReq>();
			updateInstructorSubmittedTestInfoReq.ExamId = ExamId;
			updateInstructorSubmittedTestInfoReq.InstructorId = InstructorId;
			ClientServiceFactory.GetClientInstance<IClassTestDefinition>().UpdateInstructorSubmittedTestInfo(updateInstructorSubmittedTestInfoReq);
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x0001693C File Offset: 0x00014B3C
		public void UpdateInstructorContactedInfo(int ExamId, DateTime? InstructorContactedDate, string Note)
		{
			UpdateInstructorContactedInfoReq updateInstructorContactedInfoReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateInstructorContactedInfoReq>();
			updateInstructorContactedInfoReq.ExamId = ExamId;
			updateInstructorContactedInfoReq.InstructorContactedDate = InstructorContactedDate;
			updateInstructorContactedInfoReq.InstructorContactedNote = Note;
			ClientServiceFactory.GetClientInstance<IClassTestDefinition>().UpdateInstructorContactedInfo(updateInstructorContactedInfoReq);
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x0001697C File Offset: 0x00014B7C
		public void UpdateTestPickedUp(int ExamId, DateTime? DatePickedUp, string Note)
		{
			UpdateTestPickedUpReq updateTestPickedUpReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateTestPickedUpReq>();
			updateTestPickedUpReq.ExamId = ExamId;
			updateTestPickedUpReq.TestPickedUpDate = DatePickedUp;
			updateTestPickedUpReq.TestPickedUpNote = Note;
			ClientServiceFactory.GetClientInstance<IClassTestDefinition>().UpdateTestPickedUp(updateTestPickedUpReq);
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x000169BC File Offset: 0x00014BBC
		public ClassTestDTO LoadClassTestDefinitionByIdAndConfirmInstructorOrAltContact(int ExamId, int InstructorId, int AlternateContactId)
		{
			LoadClassTestDefinitionByIdAndConfirmInstructorOrAltContactReq loadClassTestDefinitionByIdAndConfirmInstructorOrAltContactReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadClassTestDefinitionByIdAndConfirmInstructorOrAltContactReq>();
			loadClassTestDefinitionByIdAndConfirmInstructorOrAltContactReq.ExamId = ExamId;
			loadClassTestDefinitionByIdAndConfirmInstructorOrAltContactReq.InstructorId = InstructorId;
			loadClassTestDefinitionByIdAndConfirmInstructorOrAltContactReq.AlternateContactId = AlternateContactId;
			return ClientServiceFactory.GetClientInstance<IClassTestDefinition>().LoadClassTestDefinitionByIdAndConfirmInstructorOrAltContact(loadClassTestDefinitionByIdAndConfirmInstructorOrAltContactReq).Test;
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x00016A04 File Offset: 0x00014C04
		public ClassTestForExamRequestDTO LoadClassTestForExamRequestById(int ExamId)
		{
			LoadClassTestForExamRequestByIdReq loadClassTestForExamRequestByIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadClassTestForExamRequestByIdReq>();
			loadClassTestForExamRequestByIdReq.ExamId = ExamId;
			return ClientServiceFactory.GetClientInstance<IClassTestDefinition>().LoadClassTestForExamRequestById(loadClassTestForExamRequestByIdReq).ClassTestForExamRequest;
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x00016A3C File Offset: 0x00014C3C
		public IList<ClassTestForExamRequestDTO> LoadClassTestsForExamRequestByDateRange(int LuCourseId, DateTime StartDate, DateTime EndDate, eClassTestType TestType = eClassTestType.Unknown)
		{
			LoadClassTestsForExamRequestByDateRangeReq loadClassTestsForExamRequestByDateRangeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadClassTestsForExamRequestByDateRangeReq>();
			loadClassTestsForExamRequestByDateRangeReq.LuCourseId = LuCourseId;
			loadClassTestsForExamRequestByDateRangeReq.StartDate = StartDate;
			loadClassTestsForExamRequestByDateRangeReq.EndDate = EndDate;
			loadClassTestsForExamRequestByDateRangeReq.TestType = TestType;
			return ClientServiceFactory.GetClientInstance<IClassTestDefinition>().LoadClassTestsForExamRequestByDateRange(loadClassTestsForExamRequestByDateRangeReq).ClassTestsForExamRequest;
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x00016A8C File Offset: 0x00014C8C
		public IList<ClassTestForDisplayDTO> LoadClassTestsForDisplay(DateTime StartDate, DateTime EndDate)
		{
			LoadClassTestsForDisplayReq loadClassTestsForDisplayReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadClassTestsForDisplayReq>();
			loadClassTestsForDisplayReq.StartDate = StartDate;
			loadClassTestsForDisplayReq.EndDate = EndDate;
			return ClientServiceFactory.GetClientInstance<IClassTestDefinition>().LoadClassTestsForDisplay(loadClassTestsForDisplayReq).ClassTestsForDisplay;
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x00016ACC File Offset: 0x00014CCC
		public void RemoveInstructorHasSubmittedInformationAboutThisTestMarker(int examId)
		{
			RemoveInstructorHasSubmittedInformationAboutThisTestMarkerReq removeInstructorHasSubmittedInformationAboutThisTestMarkerReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<RemoveInstructorHasSubmittedInformationAboutThisTestMarkerReq>();
			removeInstructorHasSubmittedInformationAboutThisTestMarkerReq.ExamId = examId;
			ClientServiceFactory.GetClientInstance<IClassTestDefinition>().RemoveInstructorHasSubmittedInformationAboutThisTestMarker(removeInstructorHasSubmittedInformationAboutThisTestMarkerReq);
		}
	}
}
