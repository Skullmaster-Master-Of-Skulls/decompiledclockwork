using System;
using TechnoPro.Common.Core.ClockWorkServerConnection;
using TechnoPro.Common.ICore.ClockWorkServerConnection;
using TechnoPro.Common.ICore.Startup;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.ClockWorkServerConnection;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.Core.Startup
{
	// Token: 0x02000040 RID: 64
	public class ClientStartupManager : IClientStartupManager, IBaseOperationContext<ClockWorkServerOperationContext>
	{
		// Token: 0x060002A5 RID: 677 RVA: 0x0000FEB7 File Offset: 0x0000E0B7
		public ClientStartupManager(ClockWorkServerOperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000FECC File Offset: 0x0000E0CC
		public CertificateInfo GetClockWorkServerCertificate()
		{
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			object obj = cacheStorageManager["ServerCertificate"];
			bool flag = obj == null;
			CertificateInfo result;
			if (flag)
			{
				IClockWorkServerConnectionInfoManager clockWorkServerConnectionInfoManager = new ClockWorkServerConnectionInfoManager(this.OpContext);
				ClockWorkServerConnectionInfo clockWorkServerConnectionInfo = clockWorkServerConnectionInfoManager.GetClockWorkServerConnectionInfo();
				cacheStorageManager.Insert("ServerCertificate", clockWorkServerConnectionInfo.Certificate);
				result = clockWorkServerConnectionInfo.Certificate;
			}
			else
			{
				result = (CertificateInfo)obj;
			}
			return result;
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x0000FF33 File Offset: 0x0000E133
		// (set) Token: 0x060002A8 RID: 680 RVA: 0x0000FF3B File Offset: 0x0000E13B
		public ClockWorkServerOperationContext OpContext { get; set; }
	}
}
