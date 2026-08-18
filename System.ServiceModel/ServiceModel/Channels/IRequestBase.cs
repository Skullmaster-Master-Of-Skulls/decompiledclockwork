using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000767 RID: 1895
	internal interface IRequestBase
	{
		// Token: 0x06004868 RID: 18536
		void Abort(RequestChannel requestChannel);

		// Token: 0x06004869 RID: 18537
		void Fault(RequestChannel requestChannel);

		// Token: 0x0600486A RID: 18538
		void OnReleaseRequest();
	}
}
