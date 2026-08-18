using System;
using System.Threading.Tasks;

namespace Google.Apis.Http
{
	// Token: 0x02000030 RID: 48
	public interface IHttpExceptionHandler
	{
		// Token: 0x06000108 RID: 264
		Task<bool> HandleExceptionAsync(HandleExceptionArgs args);
	}
}
