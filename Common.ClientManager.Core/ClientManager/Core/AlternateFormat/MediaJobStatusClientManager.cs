using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.AlternateFormat;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AlternativeFormat;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.AlternateFormat
{
	// Token: 0x020000A0 RID: 160
	public class MediaJobStatusClientManager : IMediaJobStatusClientManager, IWebService
	{
		// Token: 0x06000615 RID: 1557 RVA: 0x0001AEF8 File Offset: 0x000190F8
		public int CreateMediaJobStatus(MediaJobStatusDTO jobStatus)
		{
			CreateMediaJobStatusReq createMediaJobStatusReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateMediaJobStatusReq>();
			createMediaJobStatusReq.MediaJobStatus = jobStatus;
			return ClientServiceFactory.GetClientInstance<IMediaJobStatus>().CreateMediaJobStatus(createMediaJobStatusReq).MediaJobStatusId;
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x0001AF30 File Offset: 0x00019130
		public MediaJobStatusDTO GetMediaJobStatusByName(string jobStatusName)
		{
			GetMediaJobStatusByNameReq getMediaJobStatusByNameReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetMediaJobStatusByNameReq>();
			getMediaJobStatusByNameReq.MediaJobStatusName = jobStatusName;
			return ClientServiceFactory.GetClientInstance<IMediaJobStatus>().GetMediaJobStatusByName(getMediaJobStatusByNameReq).MediaJobStatus;
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x0001AF68 File Offset: 0x00019168
		public IList<MediaJobStatusDTO> GetMediaJobStatusByGroup(MediaJobStatusGroup statusGroup)
		{
			GetMediaJobStatusByGroupReq getMediaJobStatusByGroupReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetMediaJobStatusByGroupReq>();
			getMediaJobStatusByGroupReq.MediaJobStatusGroup = statusGroup;
			return ClientServiceFactory.GetClientInstance<IMediaJobStatus>().GetMediaJobStatusByGroup(getMediaJobStatusByGroupReq).MediaJobStatusList;
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x0001AFA0 File Offset: 0x000191A0
		public IList<MediaJobStatusDTO> GetAllMediaJobStatus()
		{
			GetAllMediaJobStatusReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetAllMediaJobStatusReq>();
			return ClientServiceFactory.GetClientInstance<IMediaJobStatus>().GetAllMediaJobStatus(request).MediaJobStatusList;
		}
	}
}
