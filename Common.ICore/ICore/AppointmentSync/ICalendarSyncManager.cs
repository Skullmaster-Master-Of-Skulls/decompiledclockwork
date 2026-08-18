using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AppointmentSync;

namespace TechnoPro.Common.ICore.AppointmentSync
{
	// Token: 0x020000C0 RID: 192
	public interface ICalendarSyncManager : IBaseOperationContext<SyncOperationContext>
	{
		// Token: 0x060005C4 RID: 1476
		void DoFastSync();

		// Token: 0x060005C5 RID: 1477
		void DoSlowSync();

		// Token: 0x060005C6 RID: 1478
		void DoSlowSync(DateTime syncStart, DateTime syncEnd);

		// Token: 0x060005C7 RID: 1479
		IList<DuplicateAppointmentSyncMapping> FindDuplicateMappingsOneExternalMultipleClockWork(DateTime StartDate, DateTime EndDate);

		// Token: 0x060005C8 RID: 1480
		IList<DuplicateAppointmentSyncMappingAction> MergeDuplicateMappingsOneExternalMultipleClockWork(IList<DuplicateAppointmentSyncMapping> duplicateSets, bool doAction);

		// Token: 0x060005C9 RID: 1481
		IList<DuplicateAppointmentSyncMapping> FindDuplicateMappingsOneClockWorkMultipleExternal(DateTime StartDate, DateTime EndDate);

		// Token: 0x060005CA RID: 1482
		IList<DuplicateAppointmentSyncMappingAction> MergeDuplicateMappingsOneClockWorkMultipleExternal(IList<DuplicateAppointmentSyncMapping> duplicateSets, bool doAction);
	}
}
