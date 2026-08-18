using System;

namespace TechnoPro.Common.Public.Entities.AlternativeFormat.Adapters
{
	// Token: 0x0200059D RID: 1437
	public static class MediaRequestStatusAdapter
	{
		// Token: 0x06002EB7 RID: 11959 RVA: 0x00033648 File Offset: 0x00031848
		public static string ToFormatString(this MediaRequestStatus reqStatus)
		{
			return reqStatus.ToString().Replace('_', ' ');
		}

		// Token: 0x06002EB8 RID: 11960 RVA: 0x00033670 File Offset: 0x00031870
		public static bool IsCancellable(this MediaRequestStatus reqStatus)
		{
			return reqStatus == MediaRequestStatus.Created;
		}

		// Token: 0x06002EB9 RID: 11961 RVA: 0x00033688 File Offset: 0x00031888
		public static bool IsReadyToDownload(this MediaRequestStatus reqStatus)
		{
			return reqStatus == MediaRequestStatus.Ready_To_Download || reqStatus == MediaRequestStatus.Ready_To_Pick_Up_or_Download;
		}
	}
}
