using System;
using System.Collections.Generic;
using TechnoPro.Common.DAO.Impl.Legacy;
using TechnoPro.Common.DAO.Legacy;
using TechnoPro.Common.ICore.Legacy;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Legacy.Appointment;

namespace TechnoPro.Common.Core.Legacy
{
	// Token: 0x020000DB RID: 219
	public class LegacyAppointmentManager : ILegacyAppointmentManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000862 RID: 2146 RVA: 0x0003891D File Offset: 0x00036B1D
		public LegacyAppointmentManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000863 RID: 2147 RVA: 0x0003892F File Offset: 0x00036B2F
		// (set) Token: 0x06000864 RID: 2148 RVA: 0x00038937 File Offset: 0x00036B37
		public OperationContext OpContext { get; set; }

		// Token: 0x06000865 RID: 2149 RVA: 0x00038940 File Offset: 0x00036B40
		public IList<AppointmentModifiedHistoryItem> LoadAppointmentModifiedHistory(int AppointmentId)
		{
			ILegacyAppointmentDAO legacyAppointmentDAO = new LegacyAppointmentDAO(this.OpContext);
			return legacyAppointmentDAO.LoadAsAppointmentModifiedHistory(AppointmentId);
		}
	}
}
