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
	// Token: 0x0200000A RID: 10
	public class MediaVendorServiceManager : IMediaVendor, IService
	{
		// Token: 0x06000075 RID: 117 RVA: 0x00003B2C File Offset: 0x00001D2C
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003B40 File Offset: 0x00001D40
		public CreateMediaVendorResp CreateMediaVendor(CreateMediaVendorReq request)
		{
			IMediaVendorManager mediaVendorManager = new MediaVendorManager(request.GetOperationContext());
			return new CreateMediaVendorResp
			{
				MediaVendorId = mediaVendorManager.CreateMediaVendor(request.MediaVendor.ToDomainObject())
			};
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003B7C File Offset: 0x00001D7C
		public UpdateMediaVendorResp UpdateMediaVendor(UpdateMediaVendorReq request)
		{
			IMediaVendorManager mediaVendorManager = new MediaVendorManager(request.GetOperationContext());
			return new UpdateMediaVendorResp
			{
				WasUpdated = mediaVendorManager.UpdateMediaVendor(request.MediaVendor.ToDomainObject())
			};
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003BB8 File Offset: 0x00001DB8
		public DeleteMediaVendorResp DeleteMediaVendor(DeleteMediaVendorReq request)
		{
			IMediaVendorManager mediaVendorManager = new MediaVendorManager(request.GetOperationContext());
			mediaVendorManager.DeleteMediaVendor(request.MediaVendorId);
			return new DeleteMediaVendorResp();
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public LoadMediaVendorByIdResp LoadMediaVendorById(LoadMediaVendorByIdReq request)
		{
			IMediaVendorManager mediaVendorManager = new MediaVendorManager(request.GetOperationContext());
			return new LoadMediaVendorByIdResp
			{
				MediaVendor = mediaVendorManager.LoadMediaVendorById(request.MediaVendorId).ToDTO()
			};
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003C24 File Offset: 0x00001E24
		public LoadMediaVendorByNameResp LoadMediaVendorByName(LoadMediaVendorByNameReq request)
		{
			IMediaVendorManager mediaVendorManager = new MediaVendorManager(request.GetOperationContext());
			return new LoadMediaVendorByNameResp
			{
				MediaVendor = mediaVendorManager.LoadMediaVendorByName(request.MediaVendorName).ToDTO()
			};
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003C60 File Offset: 0x00001E60
		public LoadAllMediaVendorsResp LoadAllMediaVendors(LoadAllMediaVendorsReq request)
		{
			IMediaVendorManager mediaVendorManager = new MediaVendorManager(request.GetOperationContext());
			return new LoadAllMediaVendorsResp
			{
				MediaVendors = mediaVendorManager.LoadAllMediaVendors().ToDTO()
			};
		}
	}
}
