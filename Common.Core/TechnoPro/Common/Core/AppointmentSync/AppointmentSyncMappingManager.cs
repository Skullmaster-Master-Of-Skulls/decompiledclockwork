using System;
using System.Collections.Generic;
using TechnoPro.Common.DAO.AppointmentSync;
using TechnoPro.Common.DAO.Impl.AppointmentSync;
using TechnoPro.Common.ICore.AppointmentSync;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AppointmentSync;

namespace TechnoPro.Common.Core.AppointmentSync
{
	// Token: 0x02000132 RID: 306
	public class AppointmentSyncMappingManager : IAppointmentSyncMappingManager, IBaseOperationContext<SyncOperationContext>
	{
		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000D14 RID: 3348 RVA: 0x0005AEEB File Offset: 0x000590EB
		// (set) Token: 0x06000D15 RID: 3349 RVA: 0x0005AEF3 File Offset: 0x000590F3
		public IAppointmentSyncMappingDAO AppointmentSyncMappingDAO { get; set; }

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000D16 RID: 3350 RVA: 0x0005AEFC File Offset: 0x000590FC
		// (set) Token: 0x06000D17 RID: 3351 RVA: 0x0005AF04 File Offset: 0x00059104
		public SyncOperationContext OpContext { get; set; }

		// Token: 0x06000D18 RID: 3352 RVA: 0x0005AF0D File Offset: 0x0005910D
		public AppointmentSyncMappingManager(SyncOperationContext opContext)
		{
			this.OpContext = opContext;
			this.AppointmentSyncMappingDAO = new AppointmentSyncMappingDAO(this.OpContext);
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x0005AF34 File Offset: 0x00059134
		public ClockWorkExternalAppMapping LoadMappingByExternalId(ExternalAppointment exApp)
		{
			ClockWorkExternalAppMapping clockWorkExternalAppMapping = null;
			bool flag = !string.IsNullOrEmpty(exApp.UniqueId2);
			if (flag)
			{
				clockWorkExternalAppMapping = this.AppointmentSyncMappingDAO.LoadMappingByExternalUniqueAppointmentId2(exApp.UniqueId2);
			}
			clockWorkExternalAppMapping = (clockWorkExternalAppMapping ?? (exApp.IsRecurring ? this.AppointmentSyncMappingDAO.LoadMappingByExternalUniqueAppointmentId(exApp.UniqueId) : this.AppointmentSyncMappingDAO.LoadMappingByExternalGlobalAppointmentId(exApp.LegacyGlobalAppointmentId)));
			bool flag2 = clockWorkExternalAppMapping == null || string.IsNullOrEmpty(exApp.UniqueId2) || string.IsNullOrEmpty(clockWorkExternalAppMapping.ExternalApplicationUniqueAppointmentId2) || clockWorkExternalAppMapping.ExternalApplicationUniqueAppointmentId2 == exApp.UniqueId2;
			ClockWorkExternalAppMapping result;
			if (flag2)
			{
				result = clockWorkExternalAppMapping;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x0005AFD8 File Offset: 0x000591D8
		public IList<ClockWorkExternalAppMapping> LoadMappingByExternalMasterRecurrenceAppointmentId(string masterRecurrenceAppointmentId)
		{
			return this.AppointmentSyncMappingDAO.LoadMappingByExternalMasterRecurrenceAppointmentId(masterRecurrenceAppointmentId);
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x0005AFF8 File Offset: 0x000591F8
		public ClockWorkExternalAppMapping LoadMappingByClockWorkAppointmentId(int clockWorkAppointmentId)
		{
			return this.AppointmentSyncMappingDAO.LoadMappingByClockWorkAppointmentId(clockWorkAppointmentId);
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x0005B016 File Offset: 0x00059216
		public void CreateMapping(ClockWorkExternalAppMapping mapping)
		{
			this.AppointmentSyncMappingDAO.CreateMapping(mapping);
		}

		// Token: 0x06000D1D RID: 3357 RVA: 0x0005B026 File Offset: 0x00059226
		public void DeleteMapping(ClockWorkExternalAppMapping mapping)
		{
			this.AppointmentSyncMappingDAO.DeleteMapping(mapping);
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x0005B036 File Offset: 0x00059236
		public void UpdateMappingClockWorkChange(int clockworkAppId, DateTime newLastDateModified)
		{
			this.AppointmentSyncMappingDAO.UpdateMappingClockWorkChange(clockworkAppId, newLastDateModified);
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x0005B047 File Offset: 0x00059247
		public void UpdateMappingExternalChange(ExternalAppointmentId exAppId, DateTime newLastDateModified)
		{
			this.AppointmentSyncMappingDAO.UpdateMappingExternalChange(exAppId, newLastDateModified);
		}

		// Token: 0x06000D20 RID: 3360 RVA: 0x0005B058 File Offset: 0x00059258
		public string LoadUniqueIdByGlobalAppointmentId(ExternalAppointment exApp)
		{
			return exApp.IsRecurring ? null : this.AppointmentSyncMappingDAO.LoadUniqueIdByGlobalAppointmentId(exApp.LegacyGlobalAppointmentId);
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x0005B086 File Offset: 0x00059286
		public void UpdateMappingsLookupTable(string oldUniqueId, string newUniqueId)
		{
			this.AppointmentSyncMappingDAO.UpdateMappingsLookupTable(oldUniqueId, newUniqueId);
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x0005B097 File Offset: 0x00059297
		public void UpdateMappingsTable(string oldUniqueId, string newUniqueId)
		{
			this.AppointmentSyncMappingDAO.UpdateMappingsTable(oldUniqueId, newUniqueId);
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x0005B0A8 File Offset: 0x000592A8
		public void UpdateMappingsTable(int cwappid, string uniqueid, string newUniqueId2)
		{
			this.AppointmentSyncMappingDAO.UpdateMappingsTable(cwappid, uniqueid, newUniqueId2);
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x0005B0BC File Offset: 0x000592BC
		public IList<ClockWorkExternalAppMapping> LoadAllMappingsWithNoUniqueId2()
		{
			return this.AppointmentSyncMappingDAO.LoadAllMappingsWithNoUniqueId2();
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x0005B0DC File Offset: 0x000592DC
		public IList<ClockWorkExternalAppMapping> FindDuplicateMappingsOneExternalMultipleClockWork(DateTime StartDate, DateTime EndDate)
		{
			return this.AppointmentSyncMappingDAO.FindDuplicateMappingsOneExternalMultipleClockWork(StartDate, EndDate);
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x0005B0FC File Offset: 0x000592FC
		public IList<ClockWorkExternalAppMapping> FindDuplicateMappingsOneClockWorkMultipleExternal(DateTime StartDate, DateTime EndDate)
		{
			return this.AppointmentSyncMappingDAO.FindDuplicateMappingsOneClockWorkMultipleExternal(StartDate, EndDate);
		}
	}
}
