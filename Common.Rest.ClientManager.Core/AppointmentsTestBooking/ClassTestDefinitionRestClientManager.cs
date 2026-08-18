using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.AppointmentsTestBooking;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.AppointmentsTestBooking
{
	// Token: 0x02000075 RID: 117
	public class ClassTestDefinitionRestClientManager : BearerTokenRestProxy<IClassTestDefinitionClientManager>, IClassTestDefinitionClientManager, IWebService
	{
		// Token: 0x0600047C RID: 1148 RVA: 0x0000CEC5 File Offset: 0x0000B0C5
		public ClassTestDefinitionRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x0000CECF File Offset: 0x0000B0CF
		public ClassTestDefinitionRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x0000CEDA File Offset: 0x0000B0DA
		public ClassTestBaseDTO LoadClassTestBaseById(int ExamId)
		{
			return base.Get<ClassTestBaseDTO>(string.Format("classtestdefinition/classtestbase/examid/{0}", ExamId), true);
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x0000CEF3 File Offset: 0x0000B0F3
		public int CreateClassTestDefinitionBase(ClassTestBaseDTO ClassTestBase)
		{
			return base.Post<ClassTestBaseDTO, int>(ClassTestBase, "classtestdefinition");
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x0000CF01 File Offset: 0x0000B101
		public void UpdateClassTestDefinitionBase(ClassTestBaseDTO ClassTestBase)
		{
			base.Put<ClassTestBaseDTO>(ClassTestBase, "classtestdefinition/classtestbase");
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x0000CF0F File Offset: 0x0000B10F
		public IList<ClassTestDTO> LoadClassTestDefinitionsByCourse(int LuCourseId)
		{
			return base.GetMany<ClassTestDTO>(string.Format("classtestdefinition/bycourse/{0}", LuCourseId), true);
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x0000CF28 File Offset: 0x0000B128
		public void MarkTestDelivered(int ExamId, string TestDeliveredMessage)
		{
			UpdateTestDeliveredReq updateTestDeliveredReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateTestDeliveredReq>();
			updateTestDeliveredReq.ExamId = ExamId;
			updateTestDeliveredReq.TestDeliveredMessage = TestDeliveredMessage;
			base.Put<UpdateTestDeliveredReq>(updateTestDeliveredReq, "classtestdefinition/marktestdelivered");
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x0000CF5A File Offset: 0x0000B15A
		public ClassTestDTO LoadClassTestById(int ExamId)
		{
			return base.Get<ClassTestDTO>(string.Format("classtestdefinition/examid/{0}", ExamId), true);
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x0000CF73 File Offset: 0x0000B173
		public ClassTestForEditDTO LoadClassTestForEditById(int ExamId)
		{
			return base.Get<ClassTestForEditDTO>(string.Format("classtestdefinition/forediting/examid/{0}", ExamId), true);
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x0000CF8C File Offset: 0x0000B18C
		public void UpdateClassTestDefinition(ClassTestDTO ClassTest)
		{
			base.Put<ClassTestDTO>(ClassTest, "classtestdefinition");
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x0000CF9C File Offset: 0x0000B19C
		public void UpdateInstructorSubmittedTestInfo(int ExamId, int InstructorId)
		{
			UpdateInstructorSubmittedTestInfoReq updateInstructorSubmittedTestInfoReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateInstructorSubmittedTestInfoReq>();
			updateInstructorSubmittedTestInfoReq.ExamId = ExamId;
			updateInstructorSubmittedTestInfoReq.InstructorId = InstructorId;
			base.Put<UpdateInstructorSubmittedTestInfoReq>(updateInstructorSubmittedTestInfoReq, "classtestdefinition/instructorsubmittedtestinfo");
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x0000CFD0 File Offset: 0x0000B1D0
		public void UpdateInstructorContactedInfo(int ExamId, DateTime? InstructorContactedDate, string Note)
		{
			UpdateInstructorContactedInfoReq updateInstructorContactedInfoReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateInstructorContactedInfoReq>();
			updateInstructorContactedInfoReq.ExamId = ExamId;
			updateInstructorContactedInfoReq.InstructorContactedDate = InstructorContactedDate;
			updateInstructorContactedInfoReq.InstructorContactedNote = Note;
			base.Put<UpdateInstructorContactedInfoReq>(updateInstructorContactedInfoReq, "classtestdefinition/instructorcontactedinfo");
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x0000D00C File Offset: 0x0000B20C
		public void UpdateTestPickedUp(int ExamId, DateTime? DatePickedUp, string Note)
		{
			UpdateTestPickedUpReq updateTestPickedUpReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateTestPickedUpReq>();
			updateTestPickedUpReq.ExamId = ExamId;
			updateTestPickedUpReq.TestPickedUpDate = DatePickedUp;
			updateTestPickedUpReq.TestPickedUpNote = Note;
			base.Put<UpdateTestPickedUpReq>(updateTestPickedUpReq, "classtestdefinition/testpickedup");
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x0000D045 File Offset: 0x0000B245
		public ClassTestDTO LoadClassTestDefinitionByIdAndConfirmInstructorOrAltContact(int ExamId, int InstructorId, int AlternateContactId)
		{
			return base.Get<ClassTestDTO>(string.Format("classtestdefinition/examid/{0}/instructorid/{1}/alternatecontactid/{2}", ExamId, InstructorId, AlternateContactId), true);
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x0000D06A File Offset: 0x0000B26A
		public ClassTestForExamRequestDTO LoadClassTestForExamRequestById(int ExamId)
		{
			return base.Get<ClassTestForExamRequestDTO>(string.Format("classtestdefinition/classtestforexamrequest/examid/{0}", ExamId), true);
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x0000D083 File Offset: 0x0000B283
		public IList<ClassTestForExamRequestDTO> LoadClassTestsForExamRequestByDateRange(int LuCourseId, DateTime StartDate, DateTime EndDate, eClassTestType TestType = eClassTestType.Unknown)
		{
			return base.GetMany<ClassTestForExamRequestDTO>(string.Format("classtestdefinition/classtestforexamrequest/lucourseid/{0}/testtype/{1}/range/{2}/{3}", new object[]
			{
				LuCourseId,
				TestType,
				StartDate,
				EndDate
			}), true);
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x0000D0C1 File Offset: 0x0000B2C1
		public IList<ClassTestForDisplayDTO> LoadClassTestsForDisplay(DateTime StartDate, DateTime EndDate)
		{
			return base.GetMany<ClassTestForDisplayDTO>(string.Format("classtestdefinition/classtestfordisplay/range/{0}/{1}", StartDate, EndDate), true);
		}
	}
}
