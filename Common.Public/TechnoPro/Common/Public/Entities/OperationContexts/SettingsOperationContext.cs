using System;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.Public.Entities.OperationContexts
{
	// Token: 0x02000272 RID: 626
	public class SettingsOperationContext : OperationContext
	{
		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x060012C7 RID: 4807 RVA: 0x00018FD7 File Offset: 0x000171D7
		// (set) Token: 0x060012C8 RID: 4808 RVA: 0x00018FDF File Offset: 0x000171DF
		public string InstanceName { get; set; }

		// Token: 0x060012C9 RID: 4809 RVA: 0x00018FE8 File Offset: 0x000171E8
		public SettingsOperationContext()
		{
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			string text = (string)cacheStorageManager["instancename"];
			this.InstanceName = (string.IsNullOrEmpty(text) ? "ClockWork" : text);
		}

		// Token: 0x060012CA RID: 4810 RVA: 0x0001902C File Offset: 0x0001722C
		public SettingsOperationContext(OperationContext opContext)
		{
			base.AppContext = opContext.AppContext;
			this.WhoAmI = opContext.WhoAmI;
			base.TenantId = opContext.TenantId;
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			string text = (string)cacheStorageManager["instancename"];
			this.InstanceName = (string.IsNullOrEmpty(text) ? "ClockWork" : text);
		}

		// Token: 0x060012CB RID: 4811 RVA: 0x00019098 File Offset: 0x00017298
		public SettingsOperationContext(OperationContext opContext, string instancename)
		{
			base.AppContext = opContext.AppContext;
			this.WhoAmI = opContext.WhoAmI;
			base.TenantId = opContext.TenantId;
			this.InstanceName = (string.IsNullOrEmpty(instancename) ? "ClockWork" : instancename);
		}
	}
}
