using System;
using System.Runtime;

namespace System.ServiceModel.Discovery.VersionCD1
{
	// Token: 0x02000067 RID: 103
	internal sealed class HelloOperationCD1AsyncResult : HelloOperationAsyncResult<HelloMessageCD1>
	{
		// Token: 0x0600053C RID: 1340 RVA: 0x0000FCF7 File Offset: 0x0000DEF7
		public HelloOperationCD1AsyncResult(IAnnouncementServiceImplementation announcementServiceImpl, HelloMessageCD1 message, AsyncCallback callback, object state) : base(announcementServiceImpl, message, callback, state)
		{
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x0000FD04 File Offset: 0x0000DF04
		public static void End(IAsyncResult result)
		{
			AsyncResult.End<HelloOperationCD1AsyncResult>(result);
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x0000FD0D File Offset: 0x0000DF0D
		protected override bool ValidateContent(HelloMessageCD1 message)
		{
			return message.Hello != null;
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x0000FD18 File Offset: 0x0000DF18
		protected override DiscoveryMessageSequence GetMessageSequence(HelloMessageCD1 message)
		{
			return DiscoveryUtility.ToDiscoveryMessageSequenceOrNull(message.MessageSequence);
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x0000FD25 File Offset: 0x0000DF25
		protected override EndpointDiscoveryMetadata GetEndpointDiscoveryMetadata(HelloMessageCD1 message)
		{
			return message.Hello.ToEndpointDiscoveryMetadata();
		}
	}
}
