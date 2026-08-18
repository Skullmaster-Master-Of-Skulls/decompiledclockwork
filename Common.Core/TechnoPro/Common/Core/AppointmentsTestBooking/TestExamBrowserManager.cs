using System;
using System.Collections.Generic;
using TechnoPro.Common.DAO.AppointmentsTestBooking;
using TechnoPro.Common.DAO.Impl.AppointmentsTestBooking;
using TechnoPro.Common.ICore.AppointmentsTestBooking;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.TestExamBrowser;

namespace TechnoPro.Common.Core.AppointmentsTestBooking
{
	// Token: 0x02000142 RID: 322
	public class TestExamBrowserManager : ITestExamBrowserManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000E49 RID: 3657 RVA: 0x0006B5FE File Offset: 0x000697FE
		public TestExamBrowserManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new TestExamBrowserDAO(this.OpContext);
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000E4A RID: 3658 RVA: 0x0006B622 File Offset: 0x00069822
		// (set) Token: 0x06000E4B RID: 3659 RVA: 0x0006B62A File Offset: 0x0006982A
		private ITestExamBrowserDAO dao { get; set; }

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000E4C RID: 3660 RVA: 0x0006B633 File Offset: 0x00069833
		// (set) Token: 0x06000E4D RID: 3661 RVA: 0x0006B63B File Offset: 0x0006983B
		public OperationContext OpContext { get; set; }

		// Token: 0x06000E4E RID: 3662 RVA: 0x0006B644 File Offset: 0x00069844
		public IList<TestExamRow> LoadTestExamRows(DateTime StartDate, DateTime EndDate, bool HideCancelled, eTestExamColumnGroup ColumnsToLoad)
		{
			return this.dao.LoadTestExamRows(StartDate, EndDate, HideCancelled, ColumnsToLoad);
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x0006B668 File Offset: 0x00069868
		public TestExamRow LoadTestExamRow(int AppointmentId)
		{
			return this.dao.LoadTestExamRow(AppointmentId);
		}
	}
}
