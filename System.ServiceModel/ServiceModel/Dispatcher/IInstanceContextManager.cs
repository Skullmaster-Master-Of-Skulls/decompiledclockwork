using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200059F RID: 1439
	internal interface IInstanceContextManager
	{
		// Token: 0x060037D8 RID: 14296
		void Abort();

		// Token: 0x060037D9 RID: 14297
		void Add(InstanceContext instanceContext);

		// Token: 0x060037DA RID: 14298
		IAsyncResult BeginClose(TimeSpan timeout, AsyncCallback callback, object state);

		// Token: 0x060037DB RID: 14299
		IAsyncResult BeginCloseInput(TimeSpan timeout, AsyncCallback callback, object state);

		// Token: 0x060037DC RID: 14300
		void Close(TimeSpan timeout);

		// Token: 0x060037DD RID: 14301
		void CloseInput(TimeSpan timeout);

		// Token: 0x060037DE RID: 14302
		void EndClose(IAsyncResult result);

		// Token: 0x060037DF RID: 14303
		void EndCloseInput(IAsyncResult result);

		// Token: 0x060037E0 RID: 14304
		bool Remove(InstanceContext instanceContext);

		// Token: 0x060037E1 RID: 14305
		InstanceContext[] ToArray();
	}
}
