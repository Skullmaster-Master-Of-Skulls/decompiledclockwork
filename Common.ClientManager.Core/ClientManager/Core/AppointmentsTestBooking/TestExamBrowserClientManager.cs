using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.AppointmentsTestBooking;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.AppointmentsTestBooking
{
	// Token: 0x02000092 RID: 146
	public class TestExamBrowserClientManager : ITestExamBrowserClientManager, IWebService
	{
		// Token: 0x06000554 RID: 1364 RVA: 0x00017944 File Offset: 0x00015B44
		public IList<TestExamRowDTO> LoadTestExamRows(DateTime StartDate, DateTime EndDate, bool HideCancelled)
		{
			LoadTestExamRowsReq loadTestExamRowsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadTestExamRowsReq>();
			loadTestExamRowsReq.StartDate = StartDate;
			loadTestExamRowsReq.EndDate = EndDate;
			loadTestExamRowsReq.HideCancelled = HideCancelled;
			return ClientServiceFactory.GetClientInstance<ITestExamBrowser>().LoadTestExamRows(loadTestExamRowsReq).TestExamRows;
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x0001798C File Offset: 0x00015B8C
		public TestExamRowDTO LoadTestExamRow(int AppointmentId)
		{
			LoadTestExamRowReq loadTestExamRowReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadTestExamRowReq>();
			loadTestExamRowReq.AppointmentId = AppointmentId;
			return ClientServiceFactory.GetClientInstance<ITestExamBrowser>().LoadTestExamRow(loadTestExamRowReq).TestExamRow;
		}
	}
}
