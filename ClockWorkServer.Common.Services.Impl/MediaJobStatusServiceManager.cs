using System;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.Common.Core.AlternativeFormat;
using TechnoPro.Common.Core.Mappers.AlternativeFormat;
using TechnoPro.Common.ICore.AlternativeFormat;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000007 RID: 7
	public class MediaJobStatusServiceManager : IMediaJobStatus, IService
	{
		// Token: 0x06000053 RID: 83 RVA: 0x00003460 File Offset: 0x00001660
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003474 File Offset: 0x00001674
		public CreateMediaJobStatusResp CreateMediaJobStatus(CreateMediaJobStatusReq request)
		{
			IMediaJobStatusManager mediaJobStatusManager = new MediaJobStatusManager(request.GetOperationContext());
			return new CreateMediaJobStatusResp
			{
				MediaJobStatusId = mediaJobStatusManager.CreateMediaJobStatus(request.MediaJobStatus.ToDomainObject())
			};
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000034B0 File Offset: 0x000016B0
		public GetMediaJobStatusByNameResp GetMediaJobStatusByName(GetMediaJobStatusByNameReq request)
		{
			IMediaJobStatusManager mediaJobStatusManager = new MediaJobStatusManager(request.GetOperationContext());
			return new GetMediaJobStatusByNameResp
			{
				MediaJobStatus = mediaJobStatusManager.GetMediaJobStatusByName(request.MediaJobStatusName).ToDTO()
			};
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000034EC File Offset: 0x000016EC
		public GetMediaJobStatusByGroupResp GetMediaJobStatusByGroup(GetMediaJobStatusByGroupReq request)
		{
			IMediaJobStatusManager mediaJobStatusManager = new MediaJobStatusManager(request.GetOperationContext());
			return new GetMediaJobStatusByGroupResp
			{
				MediaJobStatusList = mediaJobStatusManager.GetMediaJobStatusByGroup(request.MediaJobStatusGroup).ToDTO()
			};
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003528 File Offset: 0x00001728
		public GetAllMediaJobStatusResp GetAllMediaJobStatus(GetAllMediaJobStatusReq request)
		{
			IMediaJobStatusManager mediaJobStatusManager = new MediaJobStatusManager(request.GetOperationContext());
			return new GetAllMediaJobStatusResp
			{
				MediaJobStatusList = mediaJobStatusManager.GetAllMediaJobStatus().ToDTO()
			};
		}
	}
}
