using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AppointmentSync;

namespace TechnoPro.Common.DAO.AppointmentSync
{
	// Token: 0x020000AE RID: 174
	public interface IAppointmentSyncMappingDAO : IBaseOperationContext<SyncOperationContext>
	{
		// Token: 0x060004A1 RID: 1185
		ClockWorkExternalAppMapping LoadMappingByClockWorkAppointmentId(int clockWorkAppointmentId);

		// Token: 0x060004A2 RID: 1186
		ClockWorkExternalAppMapping LoadMappingByExternalGlobalAppointmentId(string externalGlobalAppointmentId);

		// Token: 0x060004A3 RID: 1187
		ClockWorkExternalAppMapping LoadMappingByExternalUniqueAppointmentId(string externalUniqueAppointmentId);

		// Token: 0x060004A4 RID: 1188
		ClockWorkExternalAppMapping LoadMappingByExternalUniqueAppointmentId2(string externalUniqueAppointmentId2);

		// Token: 0x060004A5 RID: 1189
		IList<ClockWorkExternalAppMapping> LoadMappingByExternalMasterRecurrenceAppointmentId(string masterRecurrenceAppointmentId);

		// Token: 0x060004A6 RID: 1190
		void CreateMapping(ClockWorkExternalAppMapping mapping);

		// Token: 0x060004A7 RID: 1191
		void DeleteMapping(ClockWorkExternalAppMapping mapping);

		// Token: 0x060004A8 RID: 1192
		void UpdateMappingClockWorkChange(int clockworkAppId, DateTime newLastDateModified);

		// Token: 0x060004A9 RID: 1193
		void UpdateMappingExternalChange(ExternalAppointmentId exAppId, DateTime newLastDateModified);

		// Token: 0x060004AA RID: 1194
		string LoadUniqueIdByGlobalAppointmentId(string globalAppointmentId);

		// Token: 0x060004AB RID: 1195
		void UpdateMappingsLookupTable(string oldUniqueId, string newUniqueId);

		// Token: 0x060004AC RID: 1196
		void UpdateMappingsTable(string oldUniqueId, string newUniqueId);

		// Token: 0x060004AD RID: 1197
		void UpdateMappingsTable(int cwappid, string uniqueid, string newUniqueId2);

		// Token: 0x060004AE RID: 1198
		IList<ClockWorkExternalAppMapping> LoadAllMappingsWithNoUniqueId2();

		// Token: 0x060004AF RID: 1199
		IList<ClockWorkExternalAppMapping> FindDuplicateMappingsOneExternalMultipleClockWork(DateTime StartDate, DateTime EndDate);

		// Token: 0x060004B0 RID: 1200
		IList<ClockWorkExternalAppMapping> FindDuplicateMappingsOneClockWorkMultipleExternal(DateTime StartDate, DateTime EndDate);
	}
}
