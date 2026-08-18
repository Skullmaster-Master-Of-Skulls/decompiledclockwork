using System;
using TechnoPro.Common.ICore.AppointmentSync;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AppointmentSync;

namespace TechnoPro.Common.Core.Exchange
{
	// Token: 0x02000004 RID: 4
	public class ExchangeSyncFactory : IApplicationSyncFactory, IBaseOperationContext<SyncOperationContext>
	{
		// Token: 0x06000030 RID: 48 RVA: 0x00003544 File Offset: 0x00001744
		public ExchangeSyncFactory(SyncOperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00003556 File Offset: 0x00001756
		// (set) Token: 0x06000032 RID: 50 RVA: 0x0000355E File Offset: 0x0000175E
		public SyncOperationContext OpContext { get; set; }

		// Token: 0x06000033 RID: 51 RVA: 0x00003568 File Offset: 0x00001768
		public IExternalAppointmentManager CreateExternalAppointmentManager()
		{
			return new ExchangeAppointmentManager(this.OpContext);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00003588 File Offset: 0x00001788
		public IApplicationSyncAdministrationManager CreateApplicationSyncAdministrationManager()
		{
			return new ExchangeSyncAdministrationManager(this.OpContext);
		}
	}
}
