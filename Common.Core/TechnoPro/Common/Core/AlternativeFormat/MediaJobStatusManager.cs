using System;
using System.Collections.Generic;
using TechnoPro.Common.DAO.AlternativeFormat;
using TechnoPro.Common.DAO.Impl.AlternativeFormat;
using TechnoPro.Common.ICore.AlternativeFormat;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AlternativeFormat;

namespace TechnoPro.Common.Core.AlternativeFormat
{
	// Token: 0x0200015A RID: 346
	public class MediaJobStatusManager : IMediaJobStatusManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000F90 RID: 3984 RVA: 0x000733CE File Offset: 0x000715CE
		// (set) Token: 0x06000F91 RID: 3985 RVA: 0x000733D6 File Offset: 0x000715D6
		private IMediaJobStatusDAO MediaJobStatusDAO { get; set; }

		// Token: 0x06000F92 RID: 3986 RVA: 0x000733DF File Offset: 0x000715DF
		public MediaJobStatusManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.MediaJobStatusDAO = new MediaJobStatusDAO(this.OpContext);
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000F93 RID: 3987 RVA: 0x00073403 File Offset: 0x00071603
		// (set) Token: 0x06000F94 RID: 3988 RVA: 0x0007340B File Offset: 0x0007160B
		public OperationContext OpContext { get; set; }

		// Token: 0x06000F95 RID: 3989 RVA: 0x00073414 File Offset: 0x00071614
		public int CreateMediaJobStatus(MediaJobStatus jobStatus)
		{
			return this.MediaJobStatusDAO.CreateMediaJobStatus(jobStatus);
		}

		// Token: 0x06000F96 RID: 3990 RVA: 0x00073434 File Offset: 0x00071634
		public MediaJobStatus GetMediaJobStatusByName(string jobStatusName)
		{
			return this.MediaJobStatusDAO.GetMediaJobStatusByName(jobStatusName);
		}

		// Token: 0x06000F97 RID: 3991 RVA: 0x00073454 File Offset: 0x00071654
		public IList<MediaJobStatus> GetMediaJobStatusByGroup(MediaJobStatusGroup statusGroup)
		{
			return this.MediaJobStatusDAO.GetMediaJobStatusByGroup(statusGroup);
		}

		// Token: 0x06000F98 RID: 3992 RVA: 0x00073474 File Offset: 0x00071674
		public IList<MediaJobStatus> GetAllMediaJobStatus()
		{
			return this.MediaJobStatusDAO.GetAllMediaJobStatus();
		}
	}
}
