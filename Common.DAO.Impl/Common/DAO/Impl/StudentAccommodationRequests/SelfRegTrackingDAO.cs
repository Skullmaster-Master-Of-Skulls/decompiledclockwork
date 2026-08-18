using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.Common.DAO.StudentAccommodationRequests;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.StudentAccommodationRequests;

namespace TechnoPro.Common.DAO.Impl.StudentAccommodationRequests
{
	// Token: 0x02000044 RID: 68
	public class SelfRegTrackingDAO : ISelfRegTrackingDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x00010319 File Offset: 0x0000E519
		// (set) Token: 0x060001C9 RID: 457 RVA: 0x00010321 File Offset: 0x0000E521
		public OperationContext OpContext { get; set; }

		// Token: 0x060001CA RID: 458 RVA: 0x0001032C File Offset: 0x0000E52C
		[DebuggerStepThrough]
		public Task LogExternalStaffLoaAccessAsync(LoaExternalAccessLogItem logItem)
		{
			SelfRegTrackingDAO.<LogExternalStaffLoaAccessAsync>d__4 <LogExternalStaffLoaAccessAsync>d__ = new SelfRegTrackingDAO.<LogExternalStaffLoaAccessAsync>d__4();
			<LogExternalStaffLoaAccessAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<LogExternalStaffLoaAccessAsync>d__.<>4__this = this;
			<LogExternalStaffLoaAccessAsync>d__.logItem = logItem;
			<LogExternalStaffLoaAccessAsync>d__.<>1__state = -1;
			<LogExternalStaffLoaAccessAsync>d__.<>t__builder.Start<SelfRegTrackingDAO.<LogExternalStaffLoaAccessAsync>d__4>(ref <LogExternalStaffLoaAccessAsync>d__);
			return <LogExternalStaffLoaAccessAsync>d__.<>t__builder.Task;
		}
	}
}
