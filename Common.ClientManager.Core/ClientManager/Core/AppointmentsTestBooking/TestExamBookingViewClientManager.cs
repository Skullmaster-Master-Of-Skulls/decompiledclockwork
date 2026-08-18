using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.TestBookingViews;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.TestBookingViews.ViewEntities;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.AppointmentsTestBooking;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.TestBookingViews;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.AppointmentsTestBooking
{
	// Token: 0x02000091 RID: 145
	public class TestExamBookingViewClientManager : ITestExamBookingViewClientManager, IWebService
	{
		// Token: 0x06000545 RID: 1349 RVA: 0x000175E0 File Offset: 0x000157E0
		public LoadTestsFullResp LoadTestsFull(BookingsManagementContextDTO Context, DateTime? StartDate, DateTime? EndDate, bool HideCancelled)
		{
			LoadTestsFullReq loadTestsFullReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadTestsFullReq>();
			loadTestsFullReq.Context = Context;
			loadTestsFullReq.StartDate = StartDate;
			loadTestsFullReq.EndDate = EndDate;
			loadTestsFullReq.HideCancelled = HideCancelled;
			return ClientServiceFactory.GetClientInstance<ITestExamBookingView>().LoadTestsFull(loadTestsFullReq);
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x0001762C File Offset: 0x0001582C
		public LoadTestsSmallResp LoadTestsSmall(BookingsManagementContextDTO Context, DateTime? StartDate, DateTime? EndDate, bool HideCancelled)
		{
			LoadTestsSmallReq loadTestsSmallReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadTestsSmallReq>();
			loadTestsSmallReq.Context = Context;
			loadTestsSmallReq.StartDate = StartDate;
			loadTestsSmallReq.EndDate = EndDate;
			loadTestsSmallReq.HideCancelled = HideCancelled;
			return ClientServiceFactory.GetClientInstance<ITestExamBookingView>().LoadTestsSmall(loadTestsSmallReq);
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x00017678 File Offset: 0x00015878
		public LoadClassTestDefinitionsSmallResp LoadClassTestDefinitionsSmall(ClassTestDefinitionsManagementContextDTO context, DateTime? StartDate, DateTime? EndDate)
		{
			LoadClassTestDefinitionsSmallReq loadClassTestDefinitionsSmallReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadClassTestDefinitionsSmallReq>();
			loadClassTestDefinitionsSmallReq.Context = context;
			loadClassTestDefinitionsSmallReq.StartDate = StartDate;
			loadClassTestDefinitionsSmallReq.EndDate = EndDate;
			return ClientServiceFactory.GetClientInstance<ITestExamBookingView>().LoadClassTestDefinitionsSmall(loadClassTestDefinitionsSmallReq);
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x000176B8 File Offset: 0x000158B8
		public LoadUnbookedStudentsSmallResp LoadUnbookedStudentsSmall(UnBookedStudentMmanagementContextDTO context)
		{
			LoadUnbookedStudentsSmallReq loadUnbookedStudentsSmallReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadUnbookedStudentsSmallReq>();
			loadUnbookedStudentsSmallReq.Context = context;
			return ClientServiceFactory.GetClientInstance<ITestExamBookingView>().LoadUnbookedStudentsSmall(loadUnbookedStudentsSmallReq);
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x000176E8 File Offset: 0x000158E8
		public LoadTestFullByAppIdResp LoadTestFullByAppId(BookingsManagementContextDTO Context, int appId)
		{
			LoadTestFullByAppIdReq loadTestFullByAppIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadTestFullByAppIdReq>();
			loadTestFullByAppIdReq.Context = Context;
			loadTestFullByAppIdReq.AppId = appId;
			return ClientServiceFactory.GetClientInstance<ITestExamBookingView>().LoadTestFullByAppId(loadTestFullByAppIdReq);
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x00017720 File Offset: 0x00015920
		public LoadTestSmallByAppIdResp LoadTestSmallByAppId(BookingsManagementContextDTO Context, int appId)
		{
			LoadTestSmallByAppIdReq loadTestSmallByAppIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadTestSmallByAppIdReq>();
			loadTestSmallByAppIdReq.Context = Context;
			loadTestSmallByAppIdReq.AppId = appId;
			return ClientServiceFactory.GetClientInstance<ITestExamBookingView>().LoadTestSmallByAppId(loadTestSmallByAppIdReq);
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x00017758 File Offset: 0x00015958
		public LoadClassTestDefinitionSmallByExamIdResp LoadClassTestDefinitionSmallByExamId(ClassTestDefinitionsManagementContextDTO context, int examId)
		{
			LoadClassTestDefinitionSmallByExamIdReq loadClassTestDefinitionSmallByExamIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadClassTestDefinitionSmallByExamIdReq>();
			loadClassTestDefinitionSmallByExamIdReq.Context = context;
			loadClassTestDefinitionSmallByExamIdReq.ExamId = examId;
			return ClientServiceFactory.GetClientInstance<ITestExamBookingView>().LoadClassTestDefinitionSmallByExamId(loadClassTestDefinitionSmallByExamIdReq);
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x00017790 File Offset: 0x00015990
		public IList<TestBookingFullDTO> LoadTestsFullByExamId(BookingsManagementContextDTO context, int ExamId)
		{
			LoadTestsFullByExamIdReq loadTestsFullByExamIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadTestsFullByExamIdReq>();
			loadTestsFullByExamIdReq.Context = context;
			loadTestsFullByExamIdReq.ExamId = ExamId;
			return ClientServiceFactory.GetClientInstance<ITestExamBookingView>().LoadTestsFullByExamId(loadTestsFullByExamIdReq).BookingsLarge;
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x000177D0 File Offset: 0x000159D0
		public IList<TestBookingSmallDTO> LoadTestsSmallByExamId(BookingsManagementContextDTO context, int ExamId)
		{
			LoadTestsSmallByExamIdReq loadTestsSmallByExamIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadTestsSmallByExamIdReq>();
			loadTestsSmallByExamIdReq.Context = context;
			loadTestsSmallByExamIdReq.ExamId = ExamId;
			loadTestsSmallByExamIdReq.BinPath = ((loadTestsSmallByExamIdReq.ApplicationContext != null) ? loadTestsSmallByExamIdReq.ApplicationContext.ExecutingPath : null);
			return ClientServiceFactory.GetClientInstance<ITestExamBookingView>().LoadTestsSmallByExamId(loadTestsSmallByExamIdReq).BookingsSmall;
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x0001782C File Offset: 0x00015A2C
		public IList<TestBookingFullDTO> LoadTestsFullByAppointmentIds(BookingsManagementContextDTO context, params int[] appIds)
		{
			LoadTestsFullByAppointmentIdsReq loadTestsFullByAppointmentIdsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadTestsFullByAppointmentIdsReq>();
			loadTestsFullByAppointmentIdsReq.Context = context;
			loadTestsFullByAppointmentIdsReq.AppointmentIds = appIds;
			return ClientServiceFactory.GetClientInstance<ITestExamBookingView>().LoadTestsFullByAppointmentIds(loadTestsFullByAppointmentIdsReq).BookingsFull;
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x0001786C File Offset: 0x00015A6C
		public IList<TestBookingSmallDTO> LoadTestsSmallByAppointmentIds(BookingsManagementContextDTO context, params int[] appIds)
		{
			LoadTestsSmallByAppointmentIdsReq loadTestsSmallByAppointmentIdsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadTestsSmallByAppointmentIdsReq>();
			loadTestsSmallByAppointmentIdsReq.Context = context;
			loadTestsSmallByAppointmentIdsReq.AppointmentIds = appIds;
			return ClientServiceFactory.GetClientInstance<ITestExamBookingView>().LoadTestsSmallByAppointmentIds(loadTestsSmallByAppointmentIdsReq).BookingsSmall;
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x000178AC File Offset: 0x00015AAC
		public void SaveTestExamBookingLayoutToCentralizedSetting(eTestExamBookingGridViewType view, string layoutCompressed)
		{
			SaveTestExamBookingLayoutToCentralizedSettingReq saveTestExamBookingLayoutToCentralizedSettingReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SaveTestExamBookingLayoutToCentralizedSettingReq>();
			saveTestExamBookingLayoutToCentralizedSettingReq.View = view;
			saveTestExamBookingLayoutToCentralizedSettingReq.LayoutCompressed = layoutCompressed;
			ClientServiceFactory.GetClientInstance<ITestExamBookingView>().SaveTestExamBookingLayoutToCentralizedSetting(saveTestExamBookingLayoutToCentralizedSettingReq);
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x000178E4 File Offset: 0x00015AE4
		public void ClearTestExamBookingLayoutInCentralizedSetting(eTestExamBookingGridViewType view)
		{
			ClearTestExamBookingLayoutInCentralizedSettingReq clearTestExamBookingLayoutInCentralizedSettingReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ClearTestExamBookingLayoutInCentralizedSettingReq>();
			clearTestExamBookingLayoutInCentralizedSettingReq.View = view;
			ClientServiceFactory.GetClientInstance<ITestExamBookingView>().ClearTestExamBookingLayoutInCentralizedSetting(clearTestExamBookingLayoutInCentralizedSettingReq);
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x00017914 File Offset: 0x00015B14
		public IList<UnbookedTestExamStudentDTO> LoadUnbookedTestExamStudents()
		{
			LoadUnbookedTestExamStudentsReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadUnbookedTestExamStudentsReq>();
			return ClientServiceFactory.GetClientInstance<ITestExamBookingView>().LoadUnbookedTestExamStudents(request).UnbookedStudents;
		}
	}
}
