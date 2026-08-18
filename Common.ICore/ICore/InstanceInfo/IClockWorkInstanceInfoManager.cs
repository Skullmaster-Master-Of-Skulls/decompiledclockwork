using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.InstanceInfo;

namespace TechnoPro.Common.ICore.InstanceInfo
{
	// Token: 0x0200008A RID: 138
	public interface IClockWorkInstanceInfoManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060003E1 RID: 993
		ClockWorkInstanceInfo GetDefaultClockWorkInstanceInfo();
	}
}
