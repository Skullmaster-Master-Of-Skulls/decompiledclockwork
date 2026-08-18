using System;
using System.Runtime;

namespace System.ServiceModel.Discovery.VersionApril2005
{
	// Token: 0x02000080 RID: 128
	internal sealed class HelloOperationApril2005AsyncResult : HelloOperationAsyncResult<HelloMessageApril2005>
	{
		// Token: 0x060005FE RID: 1534 RVA: 0x00010D23 File Offset: 0x0000EF23
		public HelloOperationApril2005AsyncResult(IAnnouncementServiceImplementation announcementServiceImpl, HelloMessageApril2005 message, AsyncCallback callback, object state) : base(announcementServiceImpl, message, callback, state)
		{
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x00010D30 File Offset: 0x0000EF30
		public static void End(IAsyncResult result)
		{
			AsyncResult.End<HelloOperationApril2005AsyncResult>(result);
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x00010D39 File Offset: 0x0000EF39
		protected override bool ValidateContent(HelloMessageApril2005 message)
		{
			return message.Hello != null;
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x00010D44 File Offset: 0x0000EF44
		protected override DiscoveryMessageSequence GetMessageSequence(HelloMessageApril2005 message)
		{
			return DiscoveryUtility.ToDiscoveryMessageSequenceOrNull(message.MessageSequence);
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x00010D51 File Offset: 0x0000EF51
		protected override EndpointDiscoveryMetadata GetEndpointDiscoveryMetadata(HelloMessageApril2005 message)
		{
			return message.Hello.ToEndpointDiscoveryMetadata();
		}
	}
}
