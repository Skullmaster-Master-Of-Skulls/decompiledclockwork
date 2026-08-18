using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.TestBookingViews;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.TestBookingViews.ViewEntities;
using TechnoPro.Common.Core.AppointmentsTestBooking;
using TechnoPro.Common.Core.Mappers.AppointmentsTestBooking;
using TechnoPro.Common.Core.Mappers.AppointmentsTestBooking.TestBookingViews;
using TechnoPro.Common.Core.Mappers.AppointmentsTestBooking.TestBookingViews.ViewEntities;
using TechnoPro.Common.ICore.AppointmentsTestBooking;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.TestBookingViews.ViewEntities;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000019 RID: 25
	public class TestExamBookingViewServiceManager : ITestExamBookingView, IService
	{
		// Token: 0x06000130 RID: 304 RVA: 0x000068D8 File Offset: 0x00004AD8
		public LoadTestsFullResp LoadTestsFull(LoadTestsFullReq Request)
		{
			ITestExamBookingViewManager testExamBookingViewManager = new TestExamBookingViewManager(Request.GetOperationContext());
			ITestExamBookingViewManager testExamBookingViewManager2 = testExamBookingViewManager;
			BookingsManagementContextDTO context = Request.Context;
			IList<string> extendedColumnNames;
			IList<TestBookingFull> list = testExamBookingViewManager2.LoadTestsFull((context != null) ? context.ToDomainObject() : null, Request.StartDate, Request.EndDate, Request.HideCancelled, out extendedColumnNames);
			LoadTestsFullResp loadTestsFullResp = new LoadTestsFullResp();
			IList<TestBookingFullDTO> bookingsLarge;
			if (list == null)
			{
				bookingsLarge = null;
			}
			else
			{
				bookingsLarge = (from g in list
				select g.ToDTO()).ToList<TestBookingFullDTO>();
			}
			loadTestsFullResp.BookingsLarge = bookingsLarge;
			loadTestsFullResp.ExtendedColumnNames = extendedColumnNames;
			return loadTestsFullResp;
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00006968 File Offset: 0x00004B68
		public LoadTestsSmallResp LoadTestsSmall(LoadTestsSmallReq Request)
		{
			ITestExamBookingViewManager testExamBookingViewManager = new TestExamBookingViewManager(Request.GetOperationContext());
			ITestExamBookingViewManager testExamBookingViewManager2 = testExamBookingViewManager;
			BookingsManagementContextDTO context = Request.Context;
			IList<string> extendedColumnNames;
			IList<TestBookingSmall> list = testExamBookingViewManager2.LoadTestsSmall((context != null) ? context.ToDomainObject() : null, Request.StartDate, Request.EndDate, Request.HideCancelled, out extendedColumnNames);
			LoadTestsSmallResp loadTestsSmallResp = new LoadTestsSmallResp();
			IList<TestBookingSmallDTO> bookingsSmall;
			if (list == null)
			{
				bookingsSmall = null;
			}
			else
			{
				bookingsSmall = (from g in list
				select g.ToDTO()).ToList<TestBookingSmallDTO>();
			}
			loadTestsSmallResp.BookingsSmall = bookingsSmall;
			loadTestsSmallResp.ExtendedColumnNames = extendedColumnNames;
			return loadTestsSmallResp;
		}

		// Token: 0x06000132 RID: 306 RVA: 0x000069F8 File Offset: 0x00004BF8
		public LoadClassTestDefinitionsSmallResp LoadClassTestDefinitionsSmall(LoadClassTestDefinitionsSmallReq Request)
		{
			ITestExamBookingViewManager testExamBookingViewManager = new TestExamBookingViewManager(Request.GetOperationContext());
			ITestExamBookingViewManager testExamBookingViewManager2 = testExamBookingViewManager;
			ClassTestDefinitionsManagementContextDTO context = Request.Context;
			IList<string> extendedColumnNames;
			IList<ClassTestDefinitionSmall> list = testExamBookingViewManager2.LoadClassTestDefinitionsSmall((context != null) ? context.ToDomainObject() : null, Request.StartDate, Request.EndDate, out extendedColumnNames);
			LoadClassTestDefinitionsSmallResp loadClassTestDefinitionsSmallResp = new LoadClassTestDefinitionsSmallResp();
			IList<ClassTestDefinitionSmallDTO> classTestDefinitionsSmall;
			if (list == null)
			{
				classTestDefinitionsSmall = null;
			}
			else
			{
				classTestDefinitionsSmall = (from g in list
				select g.ToDTO()).ToList<ClassTestDefinitionSmallDTO>();
			}
			loadClassTestDefinitionsSmallResp.ClassTestDefinitionsSmall = classTestDefinitionsSmall;
			loadClassTestDefinitionsSmallResp.ExtendedColumnNames = extendedColumnNames;
			return loadClassTestDefinitionsSmallResp;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00006A84 File Offset: 0x00004C84
		public LoadUnbookedStudentsSmallResp LoadUnbookedStudentsSmall(LoadUnbookedStudentsSmallReq Request)
		{
			ITestExamBookingViewManager testExamBookingViewManager = new TestExamBookingViewManager(Request.GetOperationContext());
			IList<UnbookedStudentsSmall> list = testExamBookingViewManager.LoadUnbookedStudentsSmall(Request.Context.ToDomainObject());
			LoadUnbookedStudentsSmallResp loadUnbookedStudentsSmallResp = new LoadUnbookedStudentsSmallResp();
			IList<UnbookedStudentsSmallDTO> unbookedStudentsSmall;
			if (list == null)
			{
				unbookedStudentsSmall = null;
			}
			else
			{
				unbookedStudentsSmall = (from g in list
				select g.ToDTO()).ToList<UnbookedStudentsSmallDTO>();
			}
			loadUnbookedStudentsSmallResp.UnbookedStudentsSmall = unbookedStudentsSmall;
			return loadUnbookedStudentsSmallResp;
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00006AF0 File Offset: 0x00004CF0
		public LoadTestFullByAppIdResp LoadTestFullByAppId(LoadTestFullByAppIdReq Request)
		{
			ITestExamBookingViewManager testExamBookingViewManager = new TestExamBookingViewManager(Request.GetOperationContext());
			ITestExamBookingViewManager testExamBookingViewManager2 = testExamBookingViewManager;
			BookingsManagementContextDTO context = Request.Context;
			TestBookingFull item = testExamBookingViewManager2.LoadTestFullByAppId((context != null) ? context.ToDomainObject() : null, Request.AppId);
			return new LoadTestFullByAppIdResp
			{
				BookingsLarge = item.ToDTO()
			};
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00006B40 File Offset: 0x00004D40
		public LoadTestSmallByAppIdResp LoadTestSmallByAppId(LoadTestSmallByAppIdReq Request)
		{
			ITestExamBookingViewManager testExamBookingViewManager = new TestExamBookingViewManager(Request.GetOperationContext());
			ITestExamBookingViewManager testExamBookingViewManager2 = testExamBookingViewManager;
			BookingsManagementContextDTO context = Request.Context;
			TestBookingSmall item = testExamBookingViewManager2.LoadTestSmallByAppId((context != null) ? context.ToDomainObject() : null, Request.AppId);
			return new LoadTestSmallByAppIdResp
			{
				BookingsSmall = item.ToDTO()
			};
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00006B90 File Offset: 0x00004D90
		public LoadClassTestDefinitionSmallByExamIdResp LoadClassTestDefinitionSmallByExamId(LoadClassTestDefinitionSmallByExamIdReq Request)
		{
			ITestExamBookingViewManager testExamBookingViewManager = new TestExamBookingViewManager(Request.GetOperationContext());
			ITestExamBookingViewManager testExamBookingViewManager2 = testExamBookingViewManager;
			ClassTestDefinitionsManagementContextDTO context = Request.Context;
			ClassTestDefinitionSmall item = testExamBookingViewManager2.LoadClassTestDefinitionSmallByExamId((context != null) ? context.ToDomainObject() : null, Request.ExamId);
			return new LoadClassTestDefinitionSmallByExamIdResp
			{
				ClassTestDefinitionsSmall = item.ToDTO()
			};
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00006BE0 File Offset: 0x00004DE0
		public LoadTestsFullByExamIdResp LoadTestsFullByExamId(LoadTestsFullByExamIdReq Request)
		{
			ITestExamBookingViewManager testExamBookingViewManager = new TestExamBookingViewManager(Request.GetOperationContext());
			ITestExamBookingViewManager testExamBookingViewManager2 = testExamBookingViewManager;
			BookingsManagementContextDTO context = Request.Context;
			IList<TestBookingFull> list = testExamBookingViewManager2.LoadTestsFullByExamId((context != null) ? context.ToDomainObject() : null, Request.ExamId);
			LoadTestsFullByExamIdResp loadTestsFullByExamIdResp = new LoadTestsFullByExamIdResp();
			IList<TestBookingFullDTO> bookingsLarge;
			if (list == null)
			{
				bookingsLarge = null;
			}
			else
			{
				bookingsLarge = (from g in list
				select g.ToDTO()).ToList<TestBookingFullDTO>();
			}
			loadTestsFullByExamIdResp.BookingsLarge = bookingsLarge;
			return loadTestsFullByExamIdResp;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00006C5C File Offset: 0x00004E5C
		public LoadTestsSmallByExamIdResp LoadTestsSmallByExamId(LoadTestsSmallByExamIdReq Request)
		{
			ITestExamBookingViewManager testExamBookingViewManager = new TestExamBookingViewManager(Request.GetOperationContext());
			ITestExamBookingViewManager testExamBookingViewManager2 = testExamBookingViewManager;
			BookingsManagementContextDTO context = Request.Context;
			IList<TestBookingSmall> list = testExamBookingViewManager2.LoadTestsSmallByExamId((context != null) ? context.ToDomainObject() : null, Request.ExamId);
			LoadTestsSmallByExamIdResp loadTestsSmallByExamIdResp = new LoadTestsSmallByExamIdResp();
			IList<TestBookingSmallDTO> bookingsSmall;
			if (list == null)
			{
				bookingsSmall = null;
			}
			else
			{
				bookingsSmall = (from g in list
				select g.ToDTO()).ToList<TestBookingSmallDTO>();
			}
			loadTestsSmallByExamIdResp.BookingsSmall = bookingsSmall;
			return loadTestsSmallByExamIdResp;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00006CD8 File Offset: 0x00004ED8
		public LoadTestsFullByAppointmentIdsResp LoadTestsFullByAppointmentIds(LoadTestsFullByAppointmentIdsReq Request)
		{
			ITestExamBookingViewManager testExamBookingViewManager = new TestExamBookingViewManager(Request.GetOperationContext());
			ITestExamBookingViewManager testExamBookingViewManager2 = testExamBookingViewManager;
			BookingsManagementContextDTO context = Request.Context;
			IList<TestBookingFull> list = testExamBookingViewManager2.LoadTestsFullByAppointmentIds((context != null) ? context.ToDomainObject() : null, Request.AppointmentIds.ToArray<int>());
			LoadTestsFullByAppointmentIdsResp loadTestsFullByAppointmentIdsResp = new LoadTestsFullByAppointmentIdsResp();
			IList<TestBookingFullDTO> bookingsFull;
			if (list == null)
			{
				bookingsFull = null;
			}
			else
			{
				bookingsFull = (from g in list
				select g.ToDTO()).ToList<TestBookingFullDTO>();
			}
			loadTestsFullByAppointmentIdsResp.BookingsFull = bookingsFull;
			return loadTestsFullByAppointmentIdsResp;
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00006D58 File Offset: 0x00004F58
		public LoadTestsSmallByAppointmentIdsResp LoadTestsSmallByAppointmentIds(LoadTestsSmallByAppointmentIdsReq Request)
		{
			ITestExamBookingViewManager testExamBookingViewManager = new TestExamBookingViewManager(Request.GetOperationContext());
			ITestExamBookingViewManager testExamBookingViewManager2 = testExamBookingViewManager;
			BookingsManagementContextDTO context = Request.Context;
			IList<TestBookingSmall> list = testExamBookingViewManager2.LoadTestsSmallByAppointmentIds((context != null) ? context.ToDomainObject() : null, Request.AppointmentIds.ToArray<int>());
			LoadTestsSmallByAppointmentIdsResp loadTestsSmallByAppointmentIdsResp = new LoadTestsSmallByAppointmentIdsResp();
			IList<TestBookingSmallDTO> bookingsSmall;
			if (list == null)
			{
				bookingsSmall = null;
			}
			else
			{
				bookingsSmall = (from g in list
				select g.ToDTO()).ToList<TestBookingSmallDTO>();
			}
			loadTestsSmallByAppointmentIdsResp.BookingsSmall = bookingsSmall;
			return loadTestsSmallByAppointmentIdsResp;
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00006DD8 File Offset: 0x00004FD8
		public void SaveTestExamBookingLayoutToCentralizedSetting(SaveTestExamBookingLayoutToCentralizedSettingReq Request)
		{
			ITestExamBookingViewManager testExamBookingViewManager = new TestExamBookingViewManager(Request.GetOperationContext());
			testExamBookingViewManager.SaveTestExamBookingLayoutToCentralizedSetting(Request.View, Request.LayoutCompressed);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00006E08 File Offset: 0x00005008
		public void ClearTestExamBookingLayoutInCentralizedSetting(ClearTestExamBookingLayoutInCentralizedSettingReq Request)
		{
			ITestExamBookingViewManager testExamBookingViewManager = new TestExamBookingViewManager(Request.GetOperationContext());
			testExamBookingViewManager.ClearTestExamBookingLayoutInCentralizedSetting(Request.View);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00006E30 File Offset: 0x00005030
		public LoadUnbookedTestExamStudentsResp LoadUnbookedTestExamStudents(LoadUnbookedTestExamStudentsReq Request)
		{
			ITestExamBookingViewManager testExamBookingViewManager = new TestExamBookingViewManager(Request.GetOperationContext());
			IList<UnbookedTestExamStudent> list = testExamBookingViewManager.LoadUnbookedTestExamStudents();
			LoadUnbookedTestExamStudentsResp loadUnbookedTestExamStudentsResp = new LoadUnbookedTestExamStudentsResp();
			IList<UnbookedTestExamStudentDTO> unbookedStudents;
			if (list == null)
			{
				unbookedStudents = null;
			}
			else
			{
				unbookedStudents = (from g in list
				select g.ToDTO()).ToList<UnbookedTestExamStudentDTO>();
			}
			loadUnbookedTestExamStudentsResp.UnbookedStudents = unbookedStudents;
			return loadUnbookedTestExamStudentsResp;
		}
	}
}
