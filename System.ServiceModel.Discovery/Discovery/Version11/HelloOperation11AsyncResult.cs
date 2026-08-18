using System;
using System.Runtime;

namespace System.ServiceModel.Discovery.Version11
{
	// Token: 0x02000099 RID: 153
	internal sealed class HelloOperation11AsyncResult : HelloOperationAsyncResult<HelloMessage11>
	{
		// Token: 0x060006BC RID: 1724 RVA: 0x00011FCB File Offset: 0x000101CB
		public HelloOperation11AsyncResult(IAnnouncementServiceImplementation announcementServiceImpl, HelloMessage11 message, AsyncCallback callback, object state) : base(announcementServiceImpl, message, callback, state)
		{
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x00011FD8 File Offset: 0x000101D8
		public static void End(IAsyncResult result)
		{
			AsyncResult.End<HelloOperation11AsyncResult>(result);
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x00011FE1 File Offset: 0x000101E1
		protected override bool ValidateContent(HelloMessage11 message)
		{
			return message.Hello != null;
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x00011FEC File Offset: 0x000101EC
		protected override DiscoveryMessageSequence GetMessageSequence(HelloMessage11 message)
		{
			return DiscoveryUtility.ToDiscoveryMessageSequenceOrNull(message.MessageSequence);
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x00011FF9 File Offset: 0x000101F9
		protected override EndpointDiscoveryMetadata GetEndpointDiscoveryMetadata(HelloMessage11 message)
		{
			return message.Hello.ToEndpointDiscoveryMetadata();
		}
	}
}
