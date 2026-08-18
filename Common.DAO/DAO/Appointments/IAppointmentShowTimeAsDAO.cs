using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Appointments;

namespace TechnoPro.Common.DAO.Appointments
{
	// Token: 0x020000AA RID: 170
	public interface IAppointmentShowTimeAsDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600045E RID: 1118
		IList<AppShowTimeAsType> LoadAllShowTimeAsTypes();

		// Token: 0x0600045F RID: 1119
		AppShowTimeAsType LoadShowTimeAsTypeByAppCode(int AppCode);

		// Token: 0x06000460 RID: 1120
		AppShowTimeAsType LoadShowTimeAsTypeById(int AppointmentShowTimeAsId);

		// Token: 0x06000461 RID: 1121
		void DeleteShowTimeAsTypeByAppCode(int AppCode);

		// Token: 0x06000462 RID: 1122
		void DeleteShowTimeAsTypeById(int AppointmentShowTimeAsId);

		// Token: 0x06000463 RID: 1123
		void UpdateShowTimeAsType(AppShowTimeAsType ShowTimeAsType);

		// Token: 0x06000464 RID: 1124
		int CreateShowTimeAsType(AppShowTimeAsType ShowTimeAsType);
	}
}
