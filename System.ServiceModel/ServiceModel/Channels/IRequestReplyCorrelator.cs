using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200074A RID: 1866
	internal interface IRequestReplyCorrelator
	{
		// Token: 0x0600475A RID: 18266
		void Add<T>(Message request, T state);

		// Token: 0x0600475B RID: 18267
		T Find<T>(Message reply, bool remove);
	}
}
