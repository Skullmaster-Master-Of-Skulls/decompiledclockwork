using System;
using System.Runtime;

namespace System.ServiceModel.Discovery.VersionCD1
{
	// Token: 0x0200005F RID: 95
	internal sealed class ByeOperationCD1AsyncResult : ByeOperationAsyncResult<ByeMessageCD1>
	{
		// Token: 0x060004EC RID: 1260 RVA: 0x0000F1DC File Offset: 0x0000D3DC
		public ByeOperationCD1AsyncResult(IAnnouncementServiceImplementation announcementServiceImpl, ByeMessageCD1 message, AsyncCallback callback, object state) : base(announcementServiceImpl, message, callback, state)
		{
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x0000F1E9 File Offset: 0x0000D3E9
		public static void End(IAsyncResult result)
		{
			AsyncResult.End<ByeOperationCD1AsyncResult>(result);
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x0000F1F2 File Offset: 0x0000D3F2
		protected override bool ValidateContent(ByeMessageCD1 message)
		{
			return message.Bye != null;
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x0000F1FD File Offset: 0x0000D3FD
		protected override DiscoveryMessageSequence GetMessageSequence(ByeMessageCD1 message)
		{
			return DiscoveryUtility.ToDiscoveryMessageSequenceOrNull(message.MessageSequence);
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x0000F20A File Offset: 0x0000D40A
		protected override EndpointDiscoveryMetadata GetEndpointDiscoveryMetadata(ByeMessageCD1 message)
		{
			return message.Bye.ToEndpointDiscoveryMetadata();
		}
	}
}
