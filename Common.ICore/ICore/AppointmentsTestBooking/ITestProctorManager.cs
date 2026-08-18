using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;

namespace TechnoPro.Common.ICore.AppointmentsTestBooking
{
	// Token: 0x020000CD RID: 205
	public interface ITestProctorManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000633 RID: 1587
		IList<Proctor> LoadAllProctors();

		// Token: 0x06000634 RID: 1588
		IList<Proctor> LoadAllReaders();

		// Token: 0x06000635 RID: 1589
		IList<Proctor> LoadAllScribes();

		// Token: 0x06000636 RID: 1590
		IList<Proctor> LoadProctorsByAppointmentId(int AppointmentId);

		// Token: 0x06000637 RID: 1591
		Proctor LoadProctorById(int ProctorPersonId);

		// Token: 0x06000638 RID: 1592
		int CreateProctor(Proctor Proctor);

		// Token: 0x06000639 RID: 1593
		int CreateReader(Proctor Proctor);

		// Token: 0x0600063A RID: 1594
		int CreateScribe(Proctor Proctor);

		// Token: 0x0600063B RID: 1595
		void DeleteProctor(int ProctorPersonId);

		// Token: 0x0600063C RID: 1596
		void UpdateProctor(Proctor Proctor);
	}
}
