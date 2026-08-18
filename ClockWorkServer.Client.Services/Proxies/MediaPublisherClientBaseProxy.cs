using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000013 RID: 19
	internal class MediaPublisherClientBaseProxy : ClientBase<IMediaPublisher>, IMediaPublisher, IService
	{
		// Token: 0x06000102 RID: 258 RVA: 0x00004B04 File Offset: 0x00002D04
		public MediaPublisherClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00004B0F File Offset: 0x00002D0F
		public MediaPublisherClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00004B1C File Offset: 0x00002D1C
		public CreatePublisherResp CreatePublisher(CreatePublisherReq request)
		{
			return base.Channel.CreatePublisher(request);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00004B3C File Offset: 0x00002D3C
		public UpdatePublisherResp UpdatePublisher(UpdatePublisherReq request)
		{
			return base.Channel.UpdatePublisher(request);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00004B5C File Offset: 0x00002D5C
		public DeletePublisherResp DeletePublisher(DeletePublisherReq request)
		{
			return base.Channel.DeletePublisher(request);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00004B7C File Offset: 0x00002D7C
		public LoadPublisherByIdResp LoadPublisherById(LoadPublisherByIdReq request)
		{
			return base.Channel.LoadPublisherById(request);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00004B9C File Offset: 0x00002D9C
		public LoadPublisherByNameResp LoadPublisherByName(LoadPublisherByNameReq request)
		{
			return base.Channel.LoadPublisherByName(request);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00004BBC File Offset: 0x00002DBC
		public LoadAllPublishersResp LoadAllPublishers(LoadAllPublishersReq request)
		{
			return base.Channel.LoadAllPublishers(request);
		}
	}
}
