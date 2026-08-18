using System;
using System.Threading.Tasks;

namespace Google.Apis.Http
{
	// Token: 0x02000033 RID: 51
	public interface IHttpUnsuccessfulResponseHandler
	{
		// Token: 0x06000116 RID: 278
		Task<bool> HandleResponseAsync(HandleUnsuccessfulResponseArgs args);
	}
}
