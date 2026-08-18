using System;
using System.Collections.Generic;
using TechnoPro.Common.DAO.AppointmentsTestExamViews;
using TechnoPro.Common.DAO.Impl.AppointmentsTestExamViews;
using TechnoPro.Common.ICore.AppointmentsTestExamViews;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentsTestExamViews.TestBookings;

namespace TechnoPro.Common.Core.AppointmentsTestExamViews
{
	// Token: 0x02000139 RID: 313
	public class TestBookingsViewManager : ITestBookingsViewManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000D90 RID: 3472 RVA: 0x000622F1 File Offset: 0x000604F1
		public TestBookingsViewManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000D91 RID: 3473 RVA: 0x00062303 File Offset: 0x00060503
		// (set) Token: 0x06000D92 RID: 3474 RVA: 0x0006230B File Offset: 0x0006050B
		public OperationContext OpContext { get; set; }

		// Token: 0x06000D93 RID: 3475 RVA: 0x00062314 File Offset: 0x00060514
		public IList<TestBookingsViewLight> LoadTestBookingsViewLight(TestBookingsViewContext context)
		{
			ITestBookingsViewDAO testBookingsViewDAO = new TestBookingsViewDAO(this.OpContext);
			return testBookingsViewDAO.LoadTestBookingsViewLight(context);
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x0006233C File Offset: 0x0006053C
		public IList<TestBookingsViewFull> LoadTestBookingsViewFull(TestBookingsViewContext context)
		{
			ITestBookingsViewDAO testBookingsViewDAO = new TestBookingsViewDAO(this.OpContext);
			return testBookingsViewDAO.LoadTestBookingsViewFull(context);
		}
	}
}
