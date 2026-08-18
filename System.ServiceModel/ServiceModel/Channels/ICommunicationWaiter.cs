using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000756 RID: 1878
	internal interface ICommunicationWaiter : IDisposable
	{
		// Token: 0x060047C7 RID: 18375
		void Signal();

		// Token: 0x060047C8 RID: 18376
		CommunicationWaitResult Wait(TimeSpan timeout, bool aborting);
	}
}
