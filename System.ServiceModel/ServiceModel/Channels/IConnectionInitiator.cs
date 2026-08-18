using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007CD RID: 1997
	internal interface IConnectionInitiator
	{
		// Token: 0x06004B42 RID: 19266
		IConnection Connect(Uri uri, TimeSpan timeout);

		// Token: 0x06004B43 RID: 19267
		IAsyncResult BeginConnect(Uri uri, TimeSpan timeout, AsyncCallback callback, object state);

		// Token: 0x06004B44 RID: 19268
		IConnection EndConnect(IAsyncResult result);
	}
}
