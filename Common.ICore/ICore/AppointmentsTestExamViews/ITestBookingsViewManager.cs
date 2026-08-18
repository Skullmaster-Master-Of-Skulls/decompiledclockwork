using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentsTestExamViews.TestBookings;

namespace TechnoPro.Common.ICore.AppointmentsTestExamViews
{
	// Token: 0x020000E8 RID: 232
	public interface ITestBookingsViewManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600074C RID: 1868
		IList<TestBookingsViewLight> LoadTestBookingsViewLight(TestBookingsViewContext context);

		// Token: 0x0600074D RID: 1869
		IList<TestBookingsViewFull> LoadTestBookingsViewFull(TestBookingsViewContext context);
	}
}
