using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.TestBookingViews;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.TestBookingViews.ViewEntities;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.AppointmentsTestBooking;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.TestBookingViews;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.AppointmentsTestBooking
{
	// Token: 0x0200007B RID: 123
	public class TestExamBookingViewRestClientManager : BearerTokenRestProxy<ITestExamBookingViewClientManager>, ITestExamBookingViewClientManager, IWebService
	{
		// Token: 0x060004C5 RID: 1221 RVA: 0x0000D81D File Offset: 0x0000BA1D
		public TestExamBookingViewRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x0000D827 File Offset: 0x0000BA27
		public TestExamBookingViewRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x0000D834 File Offset: 0x0000BA34
		public LoadTestsFullResp LoadTestsFull(BookingsManagementContextDTO context, DateTime? StartDate, DateTime? EndDate, bool HideCancelled)
		{
			return base.Get<LoadTestsFullResp>(string.Format("testexambookingview/testsfull/range/{0}/{1}?hidecancelled={2}&loadextendedinfo={3}&reportid={4}", new object[]
			{
				StartDate,
				EndDate,
				HideCancelled,
				context.LoadExtendedInfo,
				context.ReportId
			}), true);
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x0000D890 File Offset: 0x0000BA90
		public LoadTestsSmallResp LoadTestsSmall(BookingsManagementContextDTO context, DateTime? StartDate, DateTime? EndDate, bool HideCancelled)
		{
			return base.Get<LoadTestsSmallResp>(string.Format("testexambookingview/testssmall/range/{0}/{1}?hidecancelled={2}&loadextendedinfo={3}&reportid={4}", new object[]
			{
				StartDate,
				EndDate,
				HideCancelled,
				context.LoadExtendedInfo,
				context.ReportId
			}), true);
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x0000D8EC File Offset: 0x0000BAEC
		public LoadClassTestDefinitionsSmallResp LoadClassTestDefinitionsSmall(ClassTestDefinitionsManagementContextDTO context, DateTime? StartDate, DateTime? EndDate)
		{
			return base.Get<LoadClassTestDefinitionsSmallResp>(string.Format("testexambookingview/classtestdefinitionssmall/range/{0}/{1}?reportid={2}", StartDate, EndDate, context.ReportId), true);
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x0000D916 File Offset: 0x0000BB16
		public LoadUnbookedStudentsSmallResp LoadUnbookedStudentsSmall(UnBookedStudentMmanagementContextDTO context)
		{
			return base.Get<LoadUnbookedStudentsSmallResp>(string.Format("testexambookingview/unbookedstudentssmall?reportid={0}", context.ReportId), true);
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x0000D934 File Offset: 0x0000BB34
		public LoadTestFullByAppIdResp LoadTestFullByAppId(BookingsManagementContextDTO context, int appId)
		{
			return new LoadTestFullByAppIdResp
			{
				BookingsLarge = base.Get<TestBookingFullDTO>(string.Format("testexambookingview/testfull/appid/{0}?loadextendedinfo={1}&reportid={2}", appId, context.LoadExtendedInfo, context.ReportId), true)
			};
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x0000D96E File Offset: 0x0000BB6E
		public LoadTestSmallByAppIdResp LoadTestSmallByAppId(BookingsManagementContextDTO context, int appId)
		{
			return new LoadTestSmallByAppIdResp
			{
				BookingsSmall = base.Get<TestBookingSmallDTO>(string.Format("testexambookingview/testsmall/appid/{0}?loadextendedinfo={1}&reportid={2}", appId, context.LoadExtendedInfo, context.ReportId), true)
			};
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x0000D9A8 File Offset: 0x0000BBA8
		public LoadClassTestDefinitionSmallByExamIdResp LoadClassTestDefinitionSmallByExamId(ClassTestDefinitionsManagementContextDTO context, int examId)
		{
			return new LoadClassTestDefinitionSmallByExamIdResp
			{
				ClassTestDefinitionsSmall = base.Get<ClassTestDefinitionSmallDTO>(string.Format("testexambookingview/classtestdefinitionsmall/examid/{0}?reportid={1}", examId, context.ReportId), true)
			};
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x0000D9D7 File Offset: 0x0000BBD7
		public IList<TestBookingFullDTO> LoadTestsFullByExamId(BookingsManagementContextDTO context, int ExamId)
		{
			return base.GetMany<TestBookingFullDTO>(string.Format("testexambookingview/testsfull/examid/{0}?loadextendedinfo={1}&reportid={2}", ExamId, context.LoadExtendedInfo, context.ReportId), true);
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x0000DA06 File Offset: 0x0000BC06
		public IList<TestBookingSmallDTO> LoadTestsSmallByExamId(BookingsManagementContextDTO context, int ExamId)
		{
			return base.GetMany<TestBookingSmallDTO>(string.Format("testexambookingview/testssmall/examid/{0}?loadextendedinfo={1}&reportid={2}", ExamId, context.LoadExtendedInfo, context.ReportId), true);
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x0000DA35 File Offset: 0x0000BC35
		public IList<TestBookingFullDTO> LoadTestsFullByAppointmentIds(BookingsManagementContextDTO context, params int[] appIds)
		{
			return base.GetMany<TestBookingFullDTO>(string.Format("testexambookingview/testsfull/appids/{0}?loadextendedinfo={1}&reportid={2}", appIds.CommaSeparatedValuesWithoutSpace<int>(), context.LoadExtendedInfo, context.ReportId), true);
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x0000DA64 File Offset: 0x0000BC64
		public IList<TestBookingSmallDTO> LoadTestsSmallByAppointmentIds(BookingsManagementContextDTO context, params int[] appIds)
		{
			return base.GetMany<TestBookingSmallDTO>(string.Format("testexambookingview/testssmall/appids/{0}?loadextendedinfo={1}&reportid={2}", appIds.CommaSeparatedValuesWithoutSpace<int>(), context.LoadExtendedInfo, context.ReportId), true);
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x0000DA94 File Offset: 0x0000BC94
		public void SaveTestExamBookingLayoutToCentralizedSetting(eTestExamBookingGridViewType view, string layoutCompressed)
		{
			SaveTestExamBookingLayoutToCentralizedSettingReq saveTestExamBookingLayoutToCentralizedSettingReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SaveTestExamBookingLayoutToCentralizedSettingReq>();
			saveTestExamBookingLayoutToCentralizedSettingReq.View = view;
			saveTestExamBookingLayoutToCentralizedSettingReq.LayoutCompressed = layoutCompressed;
			base.Post<SaveTestExamBookingLayoutToCentralizedSettingReq>(saveTestExamBookingLayoutToCentralizedSettingReq, "testexambookingview/savetestexambookinglayout");
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x0000DAC8 File Offset: 0x0000BCC8
		public void ClearTestExamBookingLayoutInCentralizedSetting(eTestExamBookingGridViewType view)
		{
			ClearTestExamBookingLayoutInCentralizedSettingReq clearTestExamBookingLayoutInCentralizedSettingReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ClearTestExamBookingLayoutInCentralizedSettingReq>();
			clearTestExamBookingLayoutInCentralizedSettingReq.View = view;
			base.Post<ClearTestExamBookingLayoutInCentralizedSettingReq>(clearTestExamBookingLayoutInCentralizedSettingReq, "testexambookingview/cleartestexambookinglayout");
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x0000DAF3 File Offset: 0x0000BCF3
		public IList<UnbookedTestExamStudentDTO> LoadUnbookedTestExamStudents()
		{
			return base.GetMany<UnbookedTestExamStudentDTO>("testexambookingview/unbookedtestexamstudents", true);
		}
	}
}
