using System;
using TechnoPro.Common.Public.Entities.AlternativeFormat.Adapters;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat.Adapters
{
	// Token: 0x02000C74 RID: 3188
	public static class MediaContentRequestedInfoDTOAdapter
	{
		// Token: 0x06004276 RID: 17014 RVA: 0x00020710 File Offset: 0x0001E910
		public static bool IsReadyToDownload(this MediaContentRequestedInfoDTO mediaContent)
		{
			return mediaContent.IsCompleted && mediaContent.RequestStatus.IsReadyToDownload() && DateTime.Today >= mediaContent.AvailableStartTime && DateTime.Today <= mediaContent.AvailableEndTime;
		}
	}
}
