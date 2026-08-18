using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Database;
using TechnoPro.Common.Public.Entities.InstanceInfo;

namespace TechnoPro.Common.ICore.InstanceInfo
{
	// Token: 0x0200008B RID: 139
	public interface IWebInstanceInfoManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060003E2 RID: 994
		IList<WebInstanceInfo> GetWebInstancesInfo(DbConnectionInfo dbConnectionInfo);

		// Token: 0x060003E3 RID: 995
		IList<WebInstanceInfo> GetWebInstancesInfo();

		// Token: 0x060003E4 RID: 996
		WebInstanceInfo GetWebInstanceInfo(string webAppName);
	}
}
