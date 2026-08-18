using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.Common.ICore.StudentAccommodationRequests;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.StudentAccommodationRequests;

namespace TechnoPro.Common.Core.StudentAccommodationRequests
{
	// Token: 0x0200003E RID: 62
	public class SelfRegTrackingManager : ISelfRegTrackingManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600028D RID: 653 RVA: 0x0000FA4A File Offset: 0x0000DC4A
		// (set) Token: 0x0600028E RID: 654 RVA: 0x0000FA52 File Offset: 0x0000DC52
		public OperationContext OpContext { get; set; }

		// Token: 0x0600028F RID: 655 RVA: 0x0000FA5C File Offset: 0x0000DC5C
		[DebuggerStepThrough]
		public Task LogExternalStaffLoaAccessAsync(LoaExternalAccessLogItem logItem)
		{
			SelfRegTrackingManager.<LogExternalStaffLoaAccessAsync>d__4 <LogExternalStaffLoaAccessAsync>d__ = new SelfRegTrackingManager.<LogExternalStaffLoaAccessAsync>d__4();
			<LogExternalStaffLoaAccessAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<LogExternalStaffLoaAccessAsync>d__.<>4__this = this;
			<LogExternalStaffLoaAccessAsync>d__.logItem = logItem;
			<LogExternalStaffLoaAccessAsync>d__.<>1__state = -1;
			<LogExternalStaffLoaAccessAsync>d__.<>t__builder.Start<SelfRegTrackingManager.<LogExternalStaffLoaAccessAsync>d__4>(ref <LogExternalStaffLoaAccessAsync>d__);
			return <LogExternalStaffLoaAccessAsync>d__.<>t__builder.Task;
		}
	}
}
