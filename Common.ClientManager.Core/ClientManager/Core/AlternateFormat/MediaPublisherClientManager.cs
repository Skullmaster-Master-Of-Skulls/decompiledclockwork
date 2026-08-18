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
	// Token: 0x020000A1 RID: 161
	public class MediaPublisherClientManager : IMediaPublisherClientManager, IWebService
	{
		// Token: 0x0600061A RID: 1562 RVA: 0x0001AFD0 File Offset: 0x000191D0
		public int CreatePublisher(MediaPublisherDTO publisher)
		{
			CreatePublisherReq createPublisherReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreatePublisherReq>();
			createPublisherReq.MediaPublisher = publisher;
			return ClientServiceFactory.GetClientInstance<IMediaPublisher>().CreatePublisher(createPublisherReq).MediaPublisherId;
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x0001B008 File Offset: 0x00019208
		public bool UpdatePublisher(MediaPublisherDTO publisher)
		{
			UpdatePublisherReq updatePublisherReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdatePublisherReq>();
			updatePublisherReq.MediaPublisher = publisher;
			return ClientServiceFactory.GetClientInstance<IMediaPublisher>().UpdatePublisher(updatePublisherReq).WasUpdated;
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x0001B040 File Offset: 0x00019240
		public bool DeletePublisher(int publisherId)
		{
			DeletePublisherReq deletePublisherReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeletePublisherReq>();
			deletePublisherReq.MediaPublisherId = publisherId;
			return ClientServiceFactory.GetClientInstance<IMediaPublisher>().DeletePublisher(deletePublisherReq).WasDeleted;
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x0001B078 File Offset: 0x00019278
		public MediaPublisherDTO LoadPublisherById(int publisherId)
		{
			LoadPublisherByIdReq loadPublisherByIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadPublisherByIdReq>();
			loadPublisherByIdReq.MediaPublisherId = publisherId;
			return ClientServiceFactory.GetClientInstance<IMediaPublisher>().LoadPublisherById(loadPublisherByIdReq).MediaPublisher;
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x0001B0B0 File Offset: 0x000192B0
		public MediaPublisherDTO LoadPublisherByName(string publisherName)
		{
			LoadPublisherByNameReq loadPublisherByNameReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadPublisherByNameReq>();
			loadPublisherByNameReq.MediaPublisherName = publisherName;
			return ClientServiceFactory.GetClientInstance<IMediaPublisher>().LoadPublisherByName(loadPublisherByNameReq).MediaPublisher;
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x0001B0E8 File Offset: 0x000192E8
		public IList<MediaPublisherDTO> LoadAllPublishers()
		{
			LoadAllPublishersReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAllPublishersReq>();
			return ClientServiceFactory.GetClientInstance<IMediaPublisher>().LoadAllPublishers(request).MediaPublishers;
		}
	}
}
