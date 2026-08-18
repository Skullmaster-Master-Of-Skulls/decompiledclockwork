using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters;
using TechnoPro.Common.Core.AppointmentsTestBooking;
using TechnoPro.Common.Core.Mappers.AppointmentsTestBooking;
using TechnoPro.Common.ICore.AppointmentsTestBooking;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.TestExamBrowser;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x0200001A RID: 26
	public class TestExamBrowserServiceManager : ITestExamBrowser, IService
	{
		// Token: 0x0600013F RID: 319 RVA: 0x00006E94 File Offset: 0x00005094
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00006EA8 File Offset: 0x000050A8
		public LoadTestExamRowsResp LoadTestExamRows(LoadTestExamRowsReq Request)
		{
			ITestExamBrowserManager testExamBrowserManager = new TestExamBrowserManager(Request.GetOperationContext());
			IList<TestExamRow> list = testExamBrowserManager.LoadTestExamRows(Request.StartDate, Request.EndDate, Request.HideCancelled, eTestExamColumnGroup.All);
			LoadTestExamRowsResp loadTestExamRowsResp = new LoadTestExamRowsResp();
			IList<TestExamRowDTO> testExamRows;
			if (list != null)
			{
				testExamRows = list.ToList<TestExamRow>().ConvertAll<TestExamRowDTO>((TestExamRow g) => g.ToDTO());
			}
			else
			{
				testExamRows = null;
			}
			loadTestExamRowsResp.TestExamRows = testExamRows;
			return loadTestExamRowsResp;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00006F20 File Offset: 0x00005120
		public LoadTestExamRowResp LoadTestExamRow(LoadTestExamRowReq Request)
		{
			ITestExamBrowserManager testExamBrowserManager = new TestExamBrowserManager(Request.GetOperationContext());
			TestExamRow testExamRow = testExamBrowserManager.LoadTestExamRow(Request.AppointmentId);
			return new LoadTestExamRowResp
			{
				TestExamRow = ((testExamRow == null) ? null : testExamRow.ToDTO())
			};
		}
	}
}
