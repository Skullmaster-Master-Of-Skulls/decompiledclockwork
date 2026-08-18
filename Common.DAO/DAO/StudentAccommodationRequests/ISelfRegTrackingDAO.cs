using System;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.StudentAccommodationRequests;

namespace TechnoPro.Common.DAO.StudentAccommodationRequests
{
	// Token: 0x0200002A RID: 42
	public interface ISelfRegTrackingDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060000B0 RID: 176
		Task LogExternalStaffLoaAccessAsync(LoaExternalAccessLogItem logItem);
	}
}
