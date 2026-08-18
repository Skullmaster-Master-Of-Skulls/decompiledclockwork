using System;
using System.Threading;
using System.Threading.Tasks;

namespace Google.Apis.Requests
{
	// Token: 0x02000016 RID: 22
	public interface IClientServiceRequest<TResponse> : IClientServiceRequest
	{
		// Token: 0x060000D6 RID: 214
		Task<TResponse> ExecuteAsync();

		// Token: 0x060000D7 RID: 215
		Task<TResponse> ExecuteAsync(CancellationToken cancellationToken);

		// Token: 0x060000D8 RID: 216
		TResponse Execute();
	}
}
