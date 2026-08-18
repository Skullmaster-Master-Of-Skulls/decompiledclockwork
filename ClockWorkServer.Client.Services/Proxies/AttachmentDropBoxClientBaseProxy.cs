using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200004D RID: 77
	internal class AttachmentDropBoxClientBaseProxy : ClientBase<IAttachmentDropBox>, IAttachmentDropBox, IService
	{
		// Token: 0x060003D2 RID: 978 RVA: 0x0000B43A File Offset: 0x0000963A
		public AttachmentDropBoxClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0000B445 File Offset: 0x00009645
		public AttachmentDropBoxClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0000B454 File Offset: 0x00009654
		public int CountAttachments()
		{
			return base.Channel.CountAttachments();
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0000B474 File Offset: 0x00009674
		public AttachmentFile GetAttachment(AttachmentRequest request)
		{
			return base.Channel.GetAttachment(request);
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0000B492 File Offset: 0x00009692
		public void DeleteAttachment(int attID)
		{
			base.Channel.DeleteAttachment(attID);
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0000B4A4 File Offset: 0x000096A4
		public IList<AttachmentInfo> GetAttachmentsInfo()
		{
			return base.Channel.GetAttachmentsInfo();
		}
	}
}
