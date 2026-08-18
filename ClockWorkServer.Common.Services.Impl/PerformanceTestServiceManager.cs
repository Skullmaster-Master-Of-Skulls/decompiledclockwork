using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar;
using TechnoPro.ClockWorkServer.Contracts.DTO.PerformanceTesting;
using TechnoPro.Common.Core.Mappers.AppointmentsCalendar;
using TechnoPro.Common.Core.Mappers.PerformanceTesting;
using TechnoPro.Common.Core.PerformanceTesting;
using TechnoPro.Common.ICore.PerformanceTesting;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AppointmentsCalendar;
using TechnoPro.Common.Public.Entities.PerformanceTesting;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x0200007B RID: 123
	public class PerformanceTestServiceManager : IPerformanceTest, IService
	{
		// Token: 0x0600048E RID: 1166 RVA: 0x00015694 File Offset: 0x00013894
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x000156A8 File Offset: 0x000138A8
		public SearchForPersonPerformanceTestResp SearchForPersonPerformanceTest(SearchForPersonPerformanceTestReq Request)
		{
			DateTime now = DateTime.Now;
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			IPerformanceTestManager performanceTestManager = new PerformanceTestManager(Request.GetOperationContext());
			SearchForPersonPerformanceTestResult searchForPersonPerformanceTestResult = performanceTestManager.SearchForPersonPerformanceTest("ea");
			stopwatch.Stop();
			searchForPersonPerformanceTestResult.TestResult.ServiceManagerTimeTaken = new PerformanceTestTimeTaken
			{
				EntryPoint = now,
				TimeElapsed = stopwatch.Elapsed
			};
			return new SearchForPersonPerformanceTestResp
			{
				Result = searchForPersonPerformanceTestResult.ToDTO()
			};
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00015728 File Offset: 0x00013928
		public LoadAppointmentsPerformanceTestResp LoadAppointmentsPerformanceTest(LoadAppointmentsPerformanceTestReq Request)
		{
			DateTime now = DateTime.Now;
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			IPerformanceTestManager performanceTestManager = new PerformanceTestManager(Request.GetOperationContext());
			IList<Appointment> list;
			PerformanceTestResult performanceTestResult = performanceTestManager.LoadAppointmentsPerformanceTest(Request.StartDate, Request.EndDate, Request.PersonIds, out list);
			stopwatch.Stop();
			performanceTestResult.ServiceManagerTimeTaken = new PerformanceTestTimeTaken
			{
				EntryPoint = now,
				TimeElapsed = stopwatch.Elapsed
			};
			LoadAppointmentsPerformanceTestResp loadAppointmentsPerformanceTestResp = new LoadAppointmentsPerformanceTestResp();
			loadAppointmentsPerformanceTestResp.Result = ((performanceTestResult == null) ? null : performanceTestResult.ToDTO());
			IList<AppointmentDTO> appointments;
			if (list != null)
			{
				appointments = list.ToList<Appointment>().ConvertAll<AppointmentDTO>((Appointment g) => g.ToDTO());
			}
			else
			{
				appointments = null;
			}
			loadAppointmentsPerformanceTestResp.Appointments = appointments;
			return loadAppointmentsPerformanceTestResp;
		}
	}
}
