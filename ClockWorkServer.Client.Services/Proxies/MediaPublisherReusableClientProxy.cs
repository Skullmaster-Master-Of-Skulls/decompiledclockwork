using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000012 RID: 18
	public class MediaPublisherReusableClientProxy : WCFTokenBasedReusableClientProxy<IMediaPublisher>, IMediaPublisher, IService
	{
		// Token: 0x060000FA RID: 250 RVA: 0x0000499A File Offset: 0x00002B9A
		public MediaPublisherReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000049A5 File Offset: 0x00002BA5
		public MediaPublisherReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060000FC RID: 252 RVA: 0x000049B4 File Offset: 0x00002BB4
		public CreatePublisherResp CreatePublisher(CreatePublisherReq request)
		{
			return this.WrapServiceMethod<CreatePublisherResp>(() => this.Proxy.CreatePublisher(request));
		}

		// Token: 0x060000FD RID: 253 RVA: 0x000049EC File Offset: 0x00002BEC
		public UpdatePublisherResp UpdatePublisher(UpdatePublisherReq request)
		{
			return this.WrapServiceMethod<UpdatePublisherResp>(() => this.Proxy.UpdatePublisher(request));
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00004A24 File Offset: 0x00002C24
		public DeletePublisherResp DeletePublisher(DeletePublisherReq request)
		{
			return this.WrapServiceMethod<DeletePublisherResp>(() => this.Proxy.DeletePublisher(request));
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00004A5C File Offset: 0x00002C5C
		public LoadPublisherByIdResp LoadPublisherById(LoadPublisherByIdReq request)
		{
			return this.WrapServiceMethod<LoadPublisherByIdResp>(() => this.Proxy.LoadPublisherById(request));
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00004A94 File Offset: 0x00002C94
		public LoadPublisherByNameResp LoadPublisherByName(LoadPublisherByNameReq request)
		{
			return this.WrapServiceMethod<LoadPublisherByNameResp>(() => this.Proxy.LoadPublisherByName(request));
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00004ACC File Offset: 0x00002CCC
		public LoadAllPublishersResp LoadAllPublishers(LoadAllPublishersReq request)
		{
			return this.WrapServiceMethod<LoadAllPublishersResp>(() => this.Proxy.LoadAllPublishers(request));
		}
	}
}
