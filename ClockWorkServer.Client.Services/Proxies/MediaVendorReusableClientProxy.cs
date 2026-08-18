using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000014 RID: 20
	public class MediaVendorReusableClientProxy : WCFTokenBasedReusableClientProxy<IMediaVendor>, IMediaVendor, IService
	{
		// Token: 0x0600010A RID: 266 RVA: 0x00004BDA File Offset: 0x00002DDA
		public MediaVendorReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00004BE5 File Offset: 0x00002DE5
		public MediaVendorReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00004BF4 File Offset: 0x00002DF4
		public CreateMediaVendorResp CreateMediaVendor(CreateMediaVendorReq request)
		{
			return this.WrapServiceMethod<CreateMediaVendorResp>(() => this.Proxy.CreateMediaVendor(request));
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00004C2C File Offset: 0x00002E2C
		public UpdateMediaVendorResp UpdateMediaVendor(UpdateMediaVendorReq request)
		{
			return this.WrapServiceMethod<UpdateMediaVendorResp>(() => this.Proxy.UpdateMediaVendor(request));
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00004C64 File Offset: 0x00002E64
		public DeleteMediaVendorResp DeleteMediaVendor(DeleteMediaVendorReq request)
		{
			return this.WrapServiceMethod<DeleteMediaVendorResp>(() => this.Proxy.DeleteMediaVendor(request));
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00004C9C File Offset: 0x00002E9C
		public LoadMediaVendorByIdResp LoadMediaVendorById(LoadMediaVendorByIdReq request)
		{
			return this.WrapServiceMethod<LoadMediaVendorByIdResp>(() => this.Proxy.LoadMediaVendorById(request));
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00004CD4 File Offset: 0x00002ED4
		public LoadMediaVendorByNameResp LoadMediaVendorByName(LoadMediaVendorByNameReq request)
		{
			return this.WrapServiceMethod<LoadMediaVendorByNameResp>(() => this.Proxy.LoadMediaVendorByName(request));
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00004D0C File Offset: 0x00002F0C
		public LoadAllMediaVendorsResp LoadAllMediaVendors(LoadAllMediaVendorsReq request)
		{
			return this.WrapServiceMethod<LoadAllMediaVendorsResp>(() => this.Proxy.LoadAllMediaVendors(request));
		}
	}
}
