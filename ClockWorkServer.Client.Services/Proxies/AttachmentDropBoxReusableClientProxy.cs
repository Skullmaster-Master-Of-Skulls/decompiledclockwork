using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200004C RID: 76
	public class AttachmentDropBoxReusableClientProxy : WCFTokenBasedReusableClientProxy<IAttachmentDropBox>, IAttachmentDropBox, IService
	{
		// Token: 0x060003CA RID: 970 RVA: 0x0000B34E File Offset: 0x0000954E
		public AttachmentDropBoxReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0000B359 File Offset: 0x00009559
		public AttachmentDropBoxReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0000B368 File Offset: 0x00009568
		public int CountAttachments()
		{
			return this.WrapServiceMethod<int>(() => base.Proxy.CountAttachments());
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0000B38C File Offset: 0x0000958C
		public AttachmentFile GetAttachment(AttachmentRequest request)
		{
			return this.WrapServiceMethod<AttachmentFile>(() => this.Proxy.GetAttachment(request));
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0000B3C4 File Offset: 0x000095C4
		public void DeleteAttachment(int attID)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.DeleteAttachment(attID);
			});
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0000B3FC File Offset: 0x000095FC
		public IList<AttachmentInfo> GetAttachmentsInfo()
		{
			return this.WrapServiceMethod<IList<AttachmentInfo>>(() => base.Proxy.GetAttachmentsInfo());
		}
	}
}
