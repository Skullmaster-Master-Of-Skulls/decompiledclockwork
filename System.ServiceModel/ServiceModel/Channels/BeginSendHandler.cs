using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200094B RID: 2379
	// (Invoke) Token: 0x06005B88 RID: 23432
	internal delegate IAsyncResult BeginSendHandler(MessageAttemptInfo attemptInfo, TimeSpan timeout, bool maskUnhandledException, AsyncCallback asyncCallback, object state);
}
