using System;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.StudentAccommodationRequests;

namespace TechnoPro.Common.ICore.StudentAccommodationRequests
{
	// Token: 0x02000031 RID: 49
	public interface ISelfRegTrackingManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000153 RID: 339
		Task LogExternalStaffLoaAccessAsync(LoaExternalAccessLogItem logItem);
	}
}
