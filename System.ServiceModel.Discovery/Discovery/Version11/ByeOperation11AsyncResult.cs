using System;
using System.Runtime;

namespace System.ServiceModel.Discovery.Version11
{
	// Token: 0x02000091 RID: 145
	internal sealed class ByeOperation11AsyncResult : ByeOperationAsyncResult<ByeMessage11>
	{
		// Token: 0x0600066C RID: 1644 RVA: 0x000114B0 File Offset: 0x0000F6B0
		public ByeOperation11AsyncResult(IAnnouncementServiceImplementation announcementServiceImpl, ByeMessage11 message, AsyncCallback callback, object state) : base(announcementServiceImpl, message, callback, state)
		{
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x000114BD File Offset: 0x0000F6BD
		public static void End(IAsyncResult result)
		{
			AsyncResult.End<ByeOperation11AsyncResult>(result);
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x000114C6 File Offset: 0x0000F6C6
		protected override bool ValidateContent(ByeMessage11 message)
		{
			return message.Bye != null;
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x000114D1 File Offset: 0x0000F6D1
		protected override DiscoveryMessageSequence GetMessageSequence(ByeMessage11 message)
		{
			return DiscoveryUtility.ToDiscoveryMessageSequenceOrNull(message.MessageSequence);
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x000114DE File Offset: 0x0000F6DE
		protected override EndpointDiscoveryMetadata GetEndpointDiscoveryMetadata(ByeMessage11 message)
		{
			return message.Bye.ToEndpointDiscoveryMetadata();
		}
	}
}
