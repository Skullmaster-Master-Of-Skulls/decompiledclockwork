using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200057F RID: 1407
	internal interface IResumeMessageRpc
	{
		// Token: 0x06003659 RID: 13913
		InstanceContext GetMessageInstanceContext();

		// Token: 0x0600365A RID: 13914
		void Resume();

		// Token: 0x0600365B RID: 13915
		void Resume(out bool alreadyResumedNoLock);

		// Token: 0x0600365C RID: 13916
		void Resume(IAsyncResult result);

		// Token: 0x0600365D RID: 13917
		void Resume(object instance);

		// Token: 0x0600365E RID: 13918
		void SignalConditionalResume(IAsyncResult result);
	}
}
