using System;
using System.Runtime;

namespace System.ServiceModel.Discovery.VersionApril2005
{
	// Token: 0x02000079 RID: 121
	internal sealed class ByeOperationApril2005AsyncResult : ByeOperationAsyncResult<ByeMessageApril2005>
	{
		// Token: 0x060005BB RID: 1467 RVA: 0x00010522 File Offset: 0x0000E722
		public ByeOperationApril2005AsyncResult(IAnnouncementServiceImplementation announcementServiceImpl, ByeMessageApril2005 message, AsyncCallback callback, object state) : base(announcementServiceImpl, message, callback, state)
		{
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x0001052F File Offset: 0x0000E72F
		public static void End(IAsyncResult result)
		{
			AsyncResult.End<ByeOperationApril2005AsyncResult>(result);
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x00010538 File Offset: 0x0000E738
		protected override bool ValidateContent(ByeMessageApril2005 message)
		{
			return message.Bye != null;
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x00010543 File Offset: 0x0000E743
		protected override DiscoveryMessageSequence GetMessageSequence(ByeMessageApril2005 message)
		{
			return DiscoveryUtility.ToDiscoveryMessageSequenceOrNull(message.MessageSequence);
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x00010550 File Offset: 0x0000E750
		protected override EndpointDiscoveryMetadata GetEndpointDiscoveryMetadata(ByeMessageApril2005 message)
		{
			return message.Bye.ToEndpointDiscoveryMetadata();
		}
	}
}
