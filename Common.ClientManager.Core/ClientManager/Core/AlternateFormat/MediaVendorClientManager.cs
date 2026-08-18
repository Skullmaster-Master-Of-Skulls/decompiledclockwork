using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.AlternateFormat;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.AlternateFormat
{
	// Token: 0x020000A2 RID: 162
	public class MediaVendorClientManager : IMediaVendorClientManager, IWebService
	{
		// Token: 0x06000621 RID: 1569 RVA: 0x0001B118 File Offset: 0x00019318
		public int CreateMediaVendor(MediaVendorDTO vendor)
		{
			CreateMediaVendorReq createMediaVendorReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateMediaVendorReq>();
			createMediaVendorReq.MediaVendor = vendor;
			return ClientServiceFactory.GetClientInstance<IMediaVendor>().CreateMediaVendor(createMediaVendorReq).MediaVendorId;
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x0001B150 File Offset: 0x00019350
		public bool UpdateMediaVendor(MediaVendorDTO vendor)
		{
			UpdateMediaVendorReq updateMediaVendorReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateMediaVendorReq>();
			updateMediaVendorReq.MediaVendor = vendor;
			return ClientServiceFactory.GetClientInstance<IMediaVendor>().UpdateMediaVendor(updateMediaVendorReq).WasUpdated;
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x0001B188 File Offset: 0x00019388
		public void DeleteMediaVendor(int vendorId)
		{
			DeleteMediaVendorReq deleteMediaVendorReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteMediaVendorReq>();
			deleteMediaVendorReq.MediaVendorId = vendorId;
			ClientServiceFactory.GetClientInstance<IMediaVendor>().DeleteMediaVendor(deleteMediaVendorReq);
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x0001B1B8 File Offset: 0x000193B8
		public MediaVendorDTO LoadMediaVendorById(int id)
		{
			LoadMediaVendorByIdReq loadMediaVendorByIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadMediaVendorByIdReq>();
			loadMediaVendorByIdReq.MediaVendorId = id;
			return ClientServiceFactory.GetClientInstance<IMediaVendor>().LoadMediaVendorById(loadMediaVendorByIdReq).MediaVendor;
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x0001B1F0 File Offset: 0x000193F0
		public MediaVendorDTO LoadMediaVendorByName(string name)
		{
			LoadMediaVendorByNameReq loadMediaVendorByNameReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadMediaVendorByNameReq>();
			loadMediaVendorByNameReq.MediaVendorName = name;
			return ClientServiceFactory.GetClientInstance<IMediaVendor>().LoadMediaVendorByName(loadMediaVendorByNameReq).MediaVendor;
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x0001B228 File Offset: 0x00019428
		public IList<MediaVendorDTO> LoadAllMediaVendors()
		{
			LoadAllMediaVendorsReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAllMediaVendorsReq>();
			return ClientServiceFactory.GetClientInstance<IMediaVendor>().LoadAllMediaVendors(request).MediaVendors;
		}
	}
}
