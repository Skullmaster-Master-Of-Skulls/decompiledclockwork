using System;
using System.Collections.Generic;
using TechnoPro.Common.DAO.AlternativeFormat;
using TechnoPro.Common.DAO.Impl.AlternativeFormat;
using TechnoPro.Common.ICore.AlternativeFormat;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AlternativeFormat;

namespace TechnoPro.Common.Core.AlternativeFormat
{
	// Token: 0x0200015B RID: 347
	public class MediaPublisherManager : IMediaPublisherManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000F99 RID: 3993 RVA: 0x00073491 File Offset: 0x00071691
		// (set) Token: 0x06000F9A RID: 3994 RVA: 0x00073499 File Offset: 0x00071699
		private IMediaPublisherDAO MediaPublisherDAO { get; set; }

		// Token: 0x06000F9B RID: 3995 RVA: 0x000734A2 File Offset: 0x000716A2
		public MediaPublisherManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.MediaPublisherDAO = new MediaPublisherDAO(opContext);
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000F9C RID: 3996 RVA: 0x000734C1 File Offset: 0x000716C1
		// (set) Token: 0x06000F9D RID: 3997 RVA: 0x000734C9 File Offset: 0x000716C9
		public OperationContext OpContext { get; set; }

		// Token: 0x06000F9E RID: 3998 RVA: 0x000734D4 File Offset: 0x000716D4
		public int CreatePublisher(MediaPublisher publisher)
		{
			return this.MediaPublisherDAO.CreatePublisher(publisher);
		}

		// Token: 0x06000F9F RID: 3999 RVA: 0x000734F4 File Offset: 0x000716F4
		public bool UpdatePublisher(MediaPublisher publisher)
		{
			return this.MediaPublisherDAO.UpdatePublisher(publisher);
		}

		// Token: 0x06000FA0 RID: 4000 RVA: 0x00073514 File Offset: 0x00071714
		public bool DeletePublisher(int publisherId)
		{
			return this.MediaPublisherDAO.DeletePublisher(publisherId);
		}

		// Token: 0x06000FA1 RID: 4001 RVA: 0x00073534 File Offset: 0x00071734
		public MediaPublisher LoadPublisherById(int publisherId)
		{
			return this.MediaPublisherDAO.LoadPublisherById(publisherId);
		}

		// Token: 0x06000FA2 RID: 4002 RVA: 0x00073554 File Offset: 0x00071754
		public MediaPublisher LoadPublisherByName(string publisherName)
		{
			return this.MediaPublisherDAO.LoadPublisherByName(publisherName);
		}

		// Token: 0x06000FA3 RID: 4003 RVA: 0x00073574 File Offset: 0x00071774
		public IList<MediaPublisher> LoadAllPublishers()
		{
			return this.MediaPublisherDAO.LoadAllPublishers();
		}
	}
}
