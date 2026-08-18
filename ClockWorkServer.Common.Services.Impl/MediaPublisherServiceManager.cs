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
	// Token: 0x02000009 RID: 9
	public class MediaPublisherServiceManager : IMediaPublisher, IService
	{
		// Token: 0x0600006D RID: 109 RVA: 0x000039B8 File Offset: 0x00001BB8
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000039CC File Offset: 0x00001BCC
		public CreatePublisherResp CreatePublisher(CreatePublisherReq request)
		{
			IMediaPublisherManager mediaPublisherManager = new MediaPublisherManager(request.GetOperationContext());
			return new CreatePublisherResp
			{
				MediaPublisherId = mediaPublisherManager.CreatePublisher(request.MediaPublisher.ToDomainObject())
			};
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003A08 File Offset: 0x00001C08
		public UpdatePublisherResp UpdatePublisher(UpdatePublisherReq request)
		{
			IMediaPublisherManager mediaPublisherManager = new MediaPublisherManager(request.GetOperationContext());
			return new UpdatePublisherResp
			{
				WasUpdated = mediaPublisherManager.UpdatePublisher(request.MediaPublisher.ToDomainObject())
			};
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003A44 File Offset: 0x00001C44
		public DeletePublisherResp DeletePublisher(DeletePublisherReq request)
		{
			IMediaPublisherManager mediaPublisherManager = new MediaPublisherManager(request.GetOperationContext());
			return new DeletePublisherResp
			{
				WasDeleted = mediaPublisherManager.DeletePublisher(request.MediaPublisherId)
			};
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003A7C File Offset: 0x00001C7C
		public LoadPublisherByIdResp LoadPublisherById(LoadPublisherByIdReq request)
		{
			IMediaPublisherManager mediaPublisherManager = new MediaPublisherManager(request.GetOperationContext());
			return new LoadPublisherByIdResp
			{
				MediaPublisher = mediaPublisherManager.LoadPublisherById(request.MediaPublisherId).ToDTO()
			};
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003AB8 File Offset: 0x00001CB8
		public LoadPublisherByNameResp LoadPublisherByName(LoadPublisherByNameReq request)
		{
			IMediaPublisherManager mediaPublisherManager = new MediaPublisherManager(request.GetOperationContext());
			return new LoadPublisherByNameResp
			{
				MediaPublisher = mediaPublisherManager.LoadPublisherByName(request.MediaPublisherName).ToDTO()
			};
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003AF4 File Offset: 0x00001CF4
		public LoadAllPublishersResp LoadAllPublishers(LoadAllPublishersReq request)
		{
			IMediaPublisherManager mediaPublisherManager = new MediaPublisherManager(request.GetOperationContext());
			return new LoadAllPublishersResp
			{
				MediaPublishers = mediaPublisherManager.LoadAllPublishers().ToDTO()
			};
		}
	}
}
