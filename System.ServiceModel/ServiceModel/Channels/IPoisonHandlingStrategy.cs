using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008D3 RID: 2259
	internal interface IPoisonHandlingStrategy : IDisposable
	{
		// Token: 0x06005602 RID: 22018
		bool CheckAndHandlePoisonMessage(MsmqMessageProperty messageProperty);

		// Token: 0x06005603 RID: 22019
		void FinalDisposition(MsmqMessageProperty messageProperty);

		// Token: 0x06005604 RID: 22020
		void Open();
	}
}
