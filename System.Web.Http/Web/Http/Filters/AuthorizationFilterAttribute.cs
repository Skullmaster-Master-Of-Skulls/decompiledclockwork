using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;

namespace System.Web.Http.Filters
{
	// Token: 0x020000E5 RID: 229
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
	public abstract class AuthorizationFilterAttribute : FilterAttribute, IAuthorizationFilter, IFilter
	{
		// Token: 0x0600058D RID: 1421 RVA: 0x00012038 File Offset: 0x00010238
		public virtual void OnAuthorization(HttpActionContext actionContext)
		{
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x0001203C File Offset: 0x0001023C
		public virtual Task OnAuthorizationAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
		{
			try
			{
				this.OnAuthorization(actionContext);
			}
			catch (Exception exception)
			{
				return TaskHelpers.FromError(exception);
			}
			return TaskHelpers.Completed();
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x00012074 File Offset: 0x00010274
		Task<HttpResponseMessage> IAuthorizationFilter.ExecuteAuthorizationFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
		{
			if (actionContext == null)
			{
				throw Error.ArgumentNull("actionContext");
			}
			if (continuation == null)
			{
				throw Error.ArgumentNull("continuation");
			}
			return this.ExecuteAuthorizationFilterAsyncCore(actionContext, cancellationToken, continuation);
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x0001221C File Offset: 0x0001041C
		private async Task<HttpResponseMessage> ExecuteAuthorizationFilterAsyncCore(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
		{
			await this.OnAuthorizationAsync(actionContext, cancellationToken);
			HttpResponseMessage result;
			if (actionContext.Response != null)
			{
				result = actionContext.Response;
			}
			else
			{
				result = await continuation();
			}
			return result;
		}
	}
}
