using System;
using TechnoPro.Common.ICore.AppointmentSync;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AppointmentSync;

namespace TechnoPro.Common.Core.GoogleCalendar
{
	// Token: 0x02000004 RID: 4
	public class GoogleCalendarSyncFactory : IApplicationSyncFactory, IBaseOperationContext<SyncOperationContext>
	{
		// Token: 0x0600002D RID: 45 RVA: 0x00002B97 File Offset: 0x00000D97
		public GoogleCalendarSyncFactory(SyncOperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002BA9 File Offset: 0x00000DA9
		// (set) Token: 0x0600002F RID: 47 RVA: 0x00002BB1 File Offset: 0x00000DB1
		public SyncOperationContext OpContext { get; set; }

		// Token: 0x06000030 RID: 48 RVA: 0x00002BBC File Offset: 0x00000DBC
		public IExternalAppointmentManager CreateExternalAppointmentManager()
		{
			return new GoogleCalendarAppointmentManager(this.OpContext);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002BDC File Offset: 0x00000DDC
		public IApplicationSyncAdministrationManager CreateApplicationSyncAdministrationManager()
		{
			return new GoogleCalendarSyncAdministrationManager(this.OpContext);
		}
	}
}
