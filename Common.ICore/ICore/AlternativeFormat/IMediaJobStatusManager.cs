using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AlternativeFormat;

namespace TechnoPro.Common.ICore.AlternativeFormat
{
	// Token: 0x020000F1 RID: 241
	public interface IMediaJobStatusManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060007BF RID: 1983
		int CreateMediaJobStatus(MediaJobStatus jobStatus);

		// Token: 0x060007C0 RID: 1984
		MediaJobStatus GetMediaJobStatusByName(string jobStatusName);

		// Token: 0x060007C1 RID: 1985
		IList<MediaJobStatus> GetMediaJobStatusByGroup(MediaJobStatusGroup statusGroup);

		// Token: 0x060007C2 RID: 1986
		IList<MediaJobStatus> GetAllMediaJobStatus();
	}
}
