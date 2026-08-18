using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AlternativeFormat;

namespace TechnoPro.Common.DAO.AlternativeFormat
{
	// Token: 0x020000CB RID: 203
	public interface IMediaJobStatusDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060005D7 RID: 1495
		int CreateMediaJobStatus(MediaJobStatus jobStatus);

		// Token: 0x060005D8 RID: 1496
		MediaJobStatus GetMediaJobStatusByName(string jobStatusName);

		// Token: 0x060005D9 RID: 1497
		IList<MediaJobStatus> GetMediaJobStatusByGroup(MediaJobStatusGroup statusGroup);

		// Token: 0x060005DA RID: 1498
		IList<MediaJobStatus> GetAllMediaJobStatus();
	}
}
