using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.Common.Core.AppointmentsTestBooking;
using TechnoPro.Common.Core.Mappers.AppointmentsTestBooking;
using TechnoPro.Common.ICore.AppointmentsTestBooking;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000014 RID: 20
	public class ClassTestDefinitionServiceManager : IClassTestDefinition, IService
	{
		// Token: 0x060000FD RID: 253 RVA: 0x00005CE8 File Offset: 0x00003EE8
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00005CFC File Offset: 0x00003EFC
		public LoadClassTestBaseByIdResp LoadClassTestBaseById(LoadClassTestBaseByIdReq Request)
		{
			IClassTestDefinitionManager classTestDefinitionManager = new ClassTestDefinitionManager(Request.GetOperationContext());
			ClassTestBase classTestBase = classTestDefinitionManager.LoadClassTestBaseById(Request.ExamId);
			return new LoadClassTestBaseByIdResp
			{
				ClassTestBase = ((classTestBase == null) ? null : classTestBase.ToDTO())
			};
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00005D40 File Offset: 0x00003F40
		public void UpdateClassTestDefinitionBase(UpdateClassTestDefinitionBaseReq Request)
		{
			IClassTestDefinitionManager classTestDefinitionManager = new ClassTestDefinitionManager(Request.GetOperationContext());
			classTestDefinitionManager.UpdateClassTestDefinitionBase(Request.ClassTest.ToDomainObject());
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00005D6C File Offset: 0x00003F6C
		public CreateClassTestDefinitionBaseResp CreateClassTestDefinitionBase(CreateClassTestDefinitionBaseReq Request)
		{
			IClassTestDefinitionManager classTestDefinitionManager = new ClassTestDefinitionManager(Request.GetOperationContext());
			int examId = classTestDefinitionManager.CreateClassTestDefinitionBase(Request.ClassTestBase.ToDomainObject());
			return new CreateClassTestDefinitionBaseResp
			{
				ExamId = examId
			};
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00005DAC File Offset: 0x00003FAC
		public LoadClassTestDefinitionsResp LoadClassTestDefinitions(LoadClassTestDefinitionsReq Request)
		{
			IClassTestDefinitionManager classTestDefinitionManager = new ClassTestDefinitionManager(Request.GetOperationContext());
			IList<ClassTest> list = classTestDefinitionManager.LoadClassTestDefinitionsByCourse(Request.LuCourseId);
			List<ClassTestDTO> list2;
			if (list != null)
			{
				list2 = list.ToList<ClassTest>().ConvertAll<ClassTestDTO>((ClassTest g) => g.ToDTO());
			}
			else
			{
				list2 = null;
			}
			List<ClassTestDTO> classTests = list2;
			return new LoadClassTestDefinitionsResp
			{
				ClassTests = classTests
			};
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00005E18 File Offset: 0x00004018
		public void SaveClassTestDefinition(SaveClassTestDefinitionReq Request)
		{
			IClassTestDefinitionManager classTestDefinitionManager = new ClassTestDefinitionManager(Request.GetOperationContext());
			throw new NotImplementedException();
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00005E38 File Offset: 0x00004038
		public void UpdateTestDelivered(UpdateTestDeliveredReq Request)
		{
			IClassTestDefinitionManager classTestDefinitionManager = new ClassTestDefinitionManager(Request.GetOperationContext());
			classTestDefinitionManager.MarkTestDelivered(Request.ExamId, Request.TestDeliveredMessage);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00005E68 File Offset: 0x00004068
		public LoadClassTestByIdResp LoadClassTestById(LoadClassTestByIdReq Request)
		{
			IClassTestDefinitionManager classTestDefinitionManager = new ClassTestDefinitionManager(Request.GetOperationContext());
			ClassTest classTest = classTestDefinitionManager.LoadClassTestDefinitionById(Request.ExamId);
			return new LoadClassTestByIdResp
			{
				ClassTest = ((classTest == null) ? null : classTest.ToDTO())
			};
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00005EAC File Offset: 0x000040AC
		public LoadClassTestForEditByIdResp LoadClassTestForEditById(LoadClassTestForEditByIdReq Request)
		{
			IClassTestDefinitionManager classTestDefinitionManager = new ClassTestDefinitionManager(Request.GetOperationContext());
			ClassTestForEdit classTestForEdit = classTestDefinitionManager.LoadClassTestForEditById(Request.ExamId);
			return new LoadClassTestForEditByIdResp
			{
				ClassTestForEdit = ((classTestForEdit == null) ? null : classTestForEdit.ToDTO())
			};
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00005EF0 File Offset: 0x000040F0
		public void UpdateClassTestDefinition(UpdateClassTestDefinitionReq Request)
		{
			IClassTestDefinitionManager classTestDefinitionManager = new ClassTestDefinitionManager(Request.GetOperationContext());
			ClassTestDTO classTest = Request.ClassTest;
			ClassTest classTestDefinition = (classTest != null) ? classTest.ToDomainObject() : null;
			classTestDefinitionManager.UpdateClassTestDefinition(classTestDefinition);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00005F28 File Offset: 0x00004128
		public void UpdateInstructorSubmittedTestInfo(UpdateInstructorSubmittedTestInfoReq Request)
		{
			IClassTestDefinitionManager classTestDefinitionManager = new ClassTestDefinitionManager(Request.GetOperationContext());
			classTestDefinitionManager.UpdateInstructorSubmittedTestInfo(Request.ExamId, Request.InstructorId);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00005F58 File Offset: 0x00004158
		public void UpdateInstructorContactedInfo(UpdateInstructorContactedInfoReq Request)
		{
			IClassTestDefinitionManager classTestDefinitionManager = new ClassTestDefinitionManager(Request.GetOperationContext());
			classTestDefinitionManager.UpdateInstructorContactedInfo(Request.ExamId, Request.InstructorContactedDate, Request.InstructorContactedNote);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00005F8C File Offset: 0x0000418C
		public void UpdateTestPickedUp(UpdateTestPickedUpReq Request)
		{
			IClassTestDefinitionManager classTestDefinitionManager = new ClassTestDefinitionManager(Request.GetOperationContext());
			classTestDefinitionManager.UpdateTestPickedUp(Request.ExamId, Request.TestPickedUpDate, Request.TestPickedUpNote);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00005FC0 File Offset: 0x000041C0
		public LoadClassTestDefinitionByIdAndConfirmInstructorOrAltContactResp LoadClassTestDefinitionByIdAndConfirmInstructorOrAltContact(LoadClassTestDefinitionByIdAndConfirmInstructorOrAltContactReq Request)
		{
			IClassTestDefinitionManager classTestDefinitionManager = new ClassTestDefinitionManager(Request.GetOperationContext());
			ClassTest classTest = classTestDefinitionManager.LoadClassTestDefinitionByIdAndConfirmInstructorOrAltContact(Request.ExamId, Request.InstructorId, Request.AlternateContactId);
			return new LoadClassTestDefinitionByIdAndConfirmInstructorOrAltContactResp
			{
				Test = ((classTest != null) ? classTest.ToDTO() : null)
			};
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00006010 File Offset: 0x00004210
		public LoadClassTestForExamRequestByIdResp LoadClassTestForExamRequestById(LoadClassTestForExamRequestByIdReq Request)
		{
			IClassTestDefinitionManager classTestDefinitionManager = new ClassTestDefinitionManager(new OperationContext
			{
				WhoAmI = Request.WhoAmI
			});
			ClassTestForExamRequest classTestForExamRequest = classTestDefinitionManager.LoadClassTestForExamRequestById(Request.ExamId);
			return new LoadClassTestForExamRequestByIdResp
			{
				ClassTestForExamRequest = ((classTestForExamRequest != null) ? classTestForExamRequest.ToDTO() : null)
			};
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00006060 File Offset: 0x00004260
		public LoadClassTestsForExamRequestByDateRangeResp LoadClassTestsForExamRequestByDateRange(LoadClassTestsForExamRequestByDateRangeReq Request)
		{
			IClassTestDefinitionManager classTestDefinitionManager = new ClassTestDefinitionManager(new OperationContext
			{
				WhoAmI = Request.WhoAmI
			});
			IList<ClassTestForExamRequest> list = classTestDefinitionManager.LoadClassTestsForExamRequestByDateRange(Request.LuCourseId, Request.StartDate, Request.EndDate, Request.TestType);
			LoadClassTestsForExamRequestByDateRangeResp loadClassTestsForExamRequestByDateRangeResp = new LoadClassTestsForExamRequestByDateRangeResp();
			IList<ClassTestForExamRequestDTO> classTestsForExamRequest;
			if (list == null)
			{
				classTestsForExamRequest = null;
			}
			else
			{
				classTestsForExamRequest = (from g in list
				select g.ToDTO()).ToList<ClassTestForExamRequestDTO>();
			}
			loadClassTestsForExamRequestByDateRangeResp.ClassTestsForExamRequest = classTestsForExamRequest;
			return loadClassTestsForExamRequestByDateRangeResp;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x000060E8 File Offset: 0x000042E8
		public LoadClassTestsForDisplayResp LoadClassTestsForDisplay(LoadClassTestsForDisplayReq Request)
		{
			IClassTestDefinitionManager classTestDefinitionManager = new ClassTestDefinitionManager(new OperationContext
			{
				WhoAmI = Request.WhoAmI
			});
			IList<ClassTestForDisplay> list = classTestDefinitionManager.LoadClassTestsForDisplay(Request.StartDate, Request.EndDate);
			LoadClassTestsForDisplayResp loadClassTestsForDisplayResp = new LoadClassTestsForDisplayResp();
			IList<ClassTestForDisplayDTO> classTestsForDisplay;
			if (list == null)
			{
				classTestsForDisplay = null;
			}
			else
			{
				classTestsForDisplay = (from g in list
				select g.ToDTO()).ToList<ClassTestForDisplayDTO>();
			}
			loadClassTestsForDisplayResp.ClassTestsForDisplay = classTestsForDisplay;
			return loadClassTestsForDisplayResp;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00006164 File Offset: 0x00004364
		public RemoveInstructorHasSubmittedInformationAboutThisTestMarkerResp RemoveInstructorHasSubmittedInformationAboutThisTestMarker(RemoveInstructorHasSubmittedInformationAboutThisTestMarkerReq Request)
		{
			IClassTestDefinitionManager classTestDefinitionManager = new ClassTestDefinitionManager(new OperationContext
			{
				WhoAmI = Request.WhoAmI
			});
			classTestDefinitionManager.RemoveInstructorHasSubmittedInformationAboutThisTestMarker(Request.ExamId);
			return new RemoveInstructorHasSubmittedInformationAboutThisTestMarkerResp();
		}
	}
}
