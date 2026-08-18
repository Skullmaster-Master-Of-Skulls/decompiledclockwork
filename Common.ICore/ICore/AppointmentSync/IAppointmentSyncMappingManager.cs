using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AppointmentSync;

namespace TechnoPro.Common.ICore.AppointmentSync
{
	// Token: 0x020000C1 RID: 193
	public interface IAppointmentSyncMappingManager : IBaseOperationContext<SyncOperationContext>
	{
		// Token: 0x060005CB RID: 1483
		ClockWorkExternalAppMapping LoadMappingByClockWorkAppointmentId(int clockWorkAppointmentId);

		// Token: 0x060005CC RID: 1484
		ClockWorkExternalAppMapping LoadMappingByExternalId(ExternalAppointment exApp);

		// Token: 0x060005CD RID: 1485
		IList<ClockWorkExternalAppMapping> LoadMappingByExternalMasterRecurrenceAppointmentId(string masterRecurrenceAppointmentId);

		// Token: 0x060005CE RID: 1486
		void CreateMapping(ClockWorkExternalAppMapping mapping);

		// Token: 0x060005CF RID: 1487
		void DeleteMapping(ClockWorkExternalAppMapping mapping);

		// Token: 0x060005D0 RID: 1488
		void UpdateMappingClockWorkChange(int clockworkAppId, DateTime newLastDateModified);

		// Token: 0x060005D1 RID: 1489
		void UpdateMappingExternalChange(ExternalAppointmentId exAppId, DateTime newLastDateModified);

		// Token: 0x060005D2 RID: 1490
		string LoadUniqueIdByGlobalAppointmentId(ExternalAppointment exApp);

		// Token: 0x060005D3 RID: 1491
		void UpdateMappingsLookupTable(string oldUniqueId, string newUniqueId);

		// Token: 0x060005D4 RID: 1492
		void UpdateMappingsTable(string oldUniqueId, string newUniqueId);

		// Token: 0x060005D5 RID: 1493
		void UpdateMappingsTable(int cwappid, string uniqueid, string newUniqueId2);

		// Token: 0x060005D6 RID: 1494
		IList<ClockWorkExternalAppMapping> LoadAllMappingsWithNoUniqueId2();

		// Token: 0x060005D7 RID: 1495
		IList<ClockWorkExternalAppMapping> FindDuplicateMappingsOneExternalMultipleClockWork(DateTime StartDate, DateTime EndDate);
	}
}
