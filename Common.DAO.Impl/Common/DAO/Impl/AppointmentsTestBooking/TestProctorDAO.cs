using System;
using TechnoPro.Common.DAO.AppointmentsTestBooking;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.DAO.Impl.AppointmentsTestBooking
{
	// Token: 0x0200014D RID: 333
	public class TestProctorDAO : ITestProctorDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060009D0 RID: 2512 RVA: 0x00066854 File Offset: 0x00064A54
		// (set) Token: 0x060009D1 RID: 2513 RVA: 0x0006685C File Offset: 0x00064A5C
		public OperationContext OpContext { get; set; }

		// Token: 0x060009D2 RID: 2514 RVA: 0x00066865 File Offset: 0x00064A65
		public TestProctorDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}
	}
}
