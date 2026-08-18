using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Appointments;

namespace TechnoPro.Common.ICore.Appointments
{
	// Token: 0x020000E4 RID: 228
	public interface IAppointmentShowTimeAsManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600070D RID: 1805
		IList<AppShowTimeAsType> LoadAllShowTimeAsTypes();

		// Token: 0x0600070E RID: 1806
		AppShowTimeAsType LoadShowTimeAsTypeByAppCode(int AppCode);

		// Token: 0x0600070F RID: 1807
		AppShowTimeAsType LoadShowTimeAsTypeById(int AppointmentShowTimeAsId);

		// Token: 0x06000710 RID: 1808
		void DeleteShowTimeAsTypeByAppCode(int AppCode);

		// Token: 0x06000711 RID: 1809
		void DeleteShowTimeAsTypeById(int AppointmentShowTimeAsId);

		// Token: 0x06000712 RID: 1810
		void UpdateShowTimeAsType(AppShowTimeAsType ShowTimeAsType);

		// Token: 0x06000713 RID: 1811
		int CreateShowTimeAsType(AppShowTimeAsType ShowTimeAsType);
	}
}
