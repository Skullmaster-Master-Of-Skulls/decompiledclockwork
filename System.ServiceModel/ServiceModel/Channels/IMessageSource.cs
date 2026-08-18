using System;
using System.Threading;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000810 RID: 2064
	internal interface IMessageSource
	{
		// Token: 0x06004D30 RID: 19760
		AsyncReceiveResult BeginReceive(TimeSpan timeout, WaitCallback callback, object state);

		// Token: 0x06004D31 RID: 19761
		Message EndReceive();

		// Token: 0x06004D32 RID: 19762
		Message Receive(TimeSpan timeout);

		// Token: 0x06004D33 RID: 19763
		AsyncReceiveResult BeginWaitForMessage(TimeSpan timeout, WaitCallback callback, object state);

		// Token: 0x06004D34 RID: 19764
		bool EndWaitForMessage();

		// Token: 0x06004D35 RID: 19765
		bool WaitForMessage(TimeSpan timeout);
	}
}
