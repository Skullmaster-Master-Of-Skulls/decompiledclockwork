using System;
using TechnoPro.ClockWorkServer.Contracts.DTO;

namespace TechnoPro.Common.ClientManager.ICore
{
	// Token: 0x02000002 RID: 2
	public interface IRequestBuilderClientManager
	{
		// Token: 0x06000001 RID: 1
		T CreateRequest<T>() where T : BaseMessageReq;

		// Token: 0x06000002 RID: 2
		T UpdateRequest<T>(T request) where T : BaseMessageReq;

		// Token: 0x06000003 RID: 3
		T CreateMessageRequest<T>() where T : BaseMessageContractReq;

		// Token: 0x06000004 RID: 4
		T UpdateMessageRequest<T>(T request) where T : BaseMessageContractReq;
	}
}
