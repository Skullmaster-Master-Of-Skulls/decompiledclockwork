using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008EB RID: 2283
	internal interface IMsmqMessagePool : IDisposable
	{
		// Token: 0x06005712 RID: 22290
		MsmqInputMessage TakeMessage();

		// Token: 0x06005713 RID: 22291
		void ReturnMessage(MsmqInputMessage message);
	}
}
