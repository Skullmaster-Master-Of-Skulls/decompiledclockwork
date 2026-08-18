using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace System.Web.Http
{
	// Token: 0x02000027 RID: 39
	public interface IHttpActionResult
	{
		// Token: 0x060000FD RID: 253
		Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken);
	}
}
