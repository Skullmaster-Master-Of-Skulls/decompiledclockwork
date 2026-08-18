using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AlternativeFormat;

namespace TechnoPro.Common.ClientManager.ICore.AlternateFormat
{
	// Token: 0x0200009C RID: 156
	public interface IMediaJobStatusClientManager : IWebService
	{
		// Token: 0x060004F9 RID: 1273
		int CreateMediaJobStatus(MediaJobStatusDTO jobStatus);

		// Token: 0x060004FA RID: 1274
		MediaJobStatusDTO GetMediaJobStatusByName(string jobStatusName);

		// Token: 0x060004FB RID: 1275
		IList<MediaJobStatusDTO> GetMediaJobStatusByGroup(MediaJobStatusGroup statusGroup);

		// Token: 0x060004FC RID: 1276
		IList<MediaJobStatusDTO> GetAllMediaJobStatus();
	}
}
