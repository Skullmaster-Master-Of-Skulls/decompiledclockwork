using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentsTestExamViews.TestBookings;

namespace TechnoPro.Common.DAO.AppointmentsTestExamViews
{
	// Token: 0x020000B6 RID: 182
	public interface ITestBookingsViewDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060004DE RID: 1246
		IList<TestBookingsViewLight> LoadTestBookingsViewLight(TestBookingsViewContext context);

		// Token: 0x060004DF RID: 1247
		IList<TestBookingsViewFull> LoadTestBookingsViewFull(TestBookingsViewContext context);
	}
}
