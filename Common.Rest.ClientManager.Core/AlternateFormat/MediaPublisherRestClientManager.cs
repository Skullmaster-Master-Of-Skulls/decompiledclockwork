using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.Common.ClientManager.ICore.AlternateFormat;
using TechnoPro.Common.Public;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.AlternateFormat
{
	// Token: 0x0200008B RID: 139
	public class MediaPublisherRestClientManager : BearerTokenRestProxy<IMediaPublisherClientManager>, IMediaPublisherClientManager, IWebService
	{
		// Token: 0x060005B8 RID: 1464 RVA: 0x00010116 File Offset: 0x0000E316
		public MediaPublisherRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x00010120 File Offset: 0x0000E320
		public MediaPublisherRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x0001012B File Offset: 0x0000E32B
		public int CreatePublisher(MediaPublisherDTO publisher)
		{
			return base.Post<MediaPublisherDTO, int>(publisher, "mediapublisher");
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x00010139 File Offset: 0x0000E339
		public bool UpdatePublisher(MediaPublisherDTO publisher)
		{
			return base.Post<MediaPublisherDTO, bool>(publisher, "mediapublisher/update");
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x00010147 File Offset: 0x0000E347
		public bool DeletePublisher(int publisherId)
		{
			return base.Post<int, bool>(publisherId, string.Format("mediapublisher/delete/id/{0}", publisherId));
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x00010160 File Offset: 0x0000E360
		public MediaPublisherDTO LoadPublisherById(int publisherId)
		{
			return base.Get<MediaPublisherDTO>(string.Format("mediapublisher/id/{0}", publisherId), true);
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x00010179 File Offset: 0x0000E379
		public MediaPublisherDTO LoadPublisherByName(string publisherName)
		{
			return base.Get<MediaPublisherDTO>(string.Format("mediapublisher/name/{0}", publisherName), true);
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x0001018D File Offset: 0x0000E38D
		public IList<MediaPublisherDTO> LoadAllPublishers()
		{
			return base.GetMany<MediaPublisherDTO>("mediapublisher", true);
		}
	}
}
