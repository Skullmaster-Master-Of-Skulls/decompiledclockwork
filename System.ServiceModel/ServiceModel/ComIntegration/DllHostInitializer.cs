using System;
using System.EnterpriseServices;
using System.Runtime.InteropServices;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x0200021D RID: 541
	[ComVisible(true)]
	[Guid("59856830-3ECB-4D29-9CFE-DDD0F74B96A2")]
	public class DllHostInitializer : IProcessInitializer
	{
		// Token: 0x06001069 RID: 4201 RVA: 0x0003CD1C File Offset: 0x0003AF1C
		public void Startup(object punkProcessControl)
		{
			IProcessInitControl control = punkProcessControl as IProcessInitControl;
			this.worker.Startup(control);
		}

		// Token: 0x0600106A RID: 4202 RVA: 0x0003CD3C File Offset: 0x0003AF3C
		public void Shutdown()
		{
			this.worker.Shutdown();
		}

		// Token: 0x0400187A RID: 6266
		private DllHostInitializeWorker worker = new DllHostInitializeWorker();
	}
}
