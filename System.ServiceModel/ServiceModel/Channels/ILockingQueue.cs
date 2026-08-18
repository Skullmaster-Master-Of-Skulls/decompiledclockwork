using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200070C RID: 1804
	internal interface ILockingQueue
	{
		// Token: 0x060044C7 RID: 17607
		void DeleteMessage(long lookupId, TimeSpan timeout);

		// Token: 0x060044C8 RID: 17608
		void UnlockMessage(long lookupId, TimeSpan timeout);
	}
}
