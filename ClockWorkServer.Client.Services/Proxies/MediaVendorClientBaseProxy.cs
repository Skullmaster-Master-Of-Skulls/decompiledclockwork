using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000015 RID: 21
	internal class MediaVendorClientBaseProxy : ClientBase<IMediaVendor>, IMediaVendor, IService
	{
		// Token: 0x06000112 RID: 274 RVA: 0x00004D44 File Offset: 0x00002F44
		public MediaVendorClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00004D4F File Offset: 0x00002F4F
		public MediaVendorClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00004D5C File Offset: 0x00002F5C
		public CreateMediaVendorResp CreateMediaVendor(CreateMediaVendorReq request)
		{
			return base.Channel.CreateMediaVendor(request);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00004D7C File Offset: 0x00002F7C
		public UpdateMediaVendorResp UpdateMediaVendor(UpdateMediaVendorReq request)
		{
			return base.Channel.UpdateMediaVendor(request);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00004D9C File Offset: 0x00002F9C
		public DeleteMediaVendorResp DeleteMediaVendor(DeleteMediaVendorReq request)
		{
			return base.Channel.DeleteMediaVendor(request);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00004DBC File Offset: 0x00002FBC
		public LoadMediaVendorByIdResp LoadMediaVendorById(LoadMediaVendorByIdReq request)
		{
			return base.Channel.LoadMediaVendorById(request);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00004DDC File Offset: 0x00002FDC
		public LoadMediaVendorByNameResp LoadMediaVendorByName(LoadMediaVendorByNameReq request)
		{
			return base.Channel.LoadMediaVendorByName(request);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00004DFC File Offset: 0x00002FFC
		public LoadAllMediaVendorsResp LoadAllMediaVendors(LoadAllMediaVendorsReq request)
		{
			return base.Channel.LoadAllMediaVendors(request);
		}
	}
}
