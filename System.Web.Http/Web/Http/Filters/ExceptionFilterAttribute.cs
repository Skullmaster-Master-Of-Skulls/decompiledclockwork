using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Web.Http.Filters
{
	// Token: 0x020000F3 RID: 243
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
	public abstract class ExceptionFilterAttribute : FilterAttribute, IExceptionFilter, IFilter
	{
		// Token: 0x06000608 RID: 1544 RVA: 0x0001410A File Offset: 0x0001230A
		public virtual void OnException(HttpActionExecutedContext actionExecutedContext)
		{
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x0001410C File Offset: 0x0001230C
		public virtual Task OnExceptionAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
		{
			try
			{
				this.OnException(actionExecutedContext);
			}
			catch (Exception exception)
			{
				return TaskHelpers.FromError(exception);
			}
			return TaskHelpers.Completed();
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x00014144 File Offset: 0x00012344
		Task IExceptionFilter.ExecuteExceptionFilterAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
		{
			if (actionExecutedContext == null)
			{
				throw Error.ArgumentNull("actionExecutedContext");
			}
			return this.ExecuteExceptionFilterAsyncCore(actionExecutedContext, cancellationToken);
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x00014238 File Offset: 0x00012438
		private async Task ExecuteExceptionFilterAsyncCore(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
		{
			await this.OnExceptionAsync(actionExecutedContext, cancellationToken);
		}
	}
}
