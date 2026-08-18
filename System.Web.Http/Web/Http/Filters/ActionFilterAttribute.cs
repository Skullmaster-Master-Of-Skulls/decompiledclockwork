using System;
using System.Net.Http;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Properties;

namespace System.Web.Http.Filters
{
	// Token: 0x020000F1 RID: 241
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
	public abstract class ActionFilterAttribute : FilterAttribute, IActionFilter, IFilter
	{
		// Token: 0x060005FF RID: 1535 RVA: 0x00013B49 File Offset: 0x00011D49
		public virtual void OnActionExecuting(HttpActionContext actionContext)
		{
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x00013B4B File Offset: 0x00011D4B
		public virtual void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
		{
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x00013B50 File Offset: 0x00011D50
		public virtual Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
		{
			try
			{
				this.OnActionExecuting(actionContext);
			}
			catch (Exception exception)
			{
				return TaskHelpers.FromError(exception);
			}
			return TaskHelpers.Completed();
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x00013B88 File Offset: 0x00011D88
		public virtual Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
		{
			try
			{
				this.OnActionExecuted(actionExecutedContext);
			}
			catch (Exception exception)
			{
				return TaskHelpers.FromError(exception);
			}
			return TaskHelpers.Completed();
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x00013BC0 File Offset: 0x00011DC0
		Task<HttpResponseMessage> IActionFilter.ExecuteActionFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
		{
			if (actionContext == null)
			{
				throw Error.ArgumentNull("actionContext");
			}
			if (continuation == null)
			{
				throw Error.ArgumentNull("continuation");
			}
			return this.ExecuteActionFilterAsyncCore(actionContext, cancellationToken, continuation);
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x00013D7C File Offset: 0x00011F7C
		private async Task<HttpResponseMessage> ExecuteActionFilterAsyncCore(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
		{
			await this.OnActionExecutingAsync(actionContext, cancellationToken);
			HttpResponseMessage result;
			if (actionContext.Response != null)
			{
				result = actionContext.Response;
			}
			else
			{
				result = await this.CallOnActionExecutedAsync(actionContext, cancellationToken, continuation);
			}
			return result;
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x000140A4 File Offset: 0x000122A4
		private async Task<HttpResponseMessage> CallOnActionExecutedAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
		{
			cancellationToken.ThrowIfCancellationRequested();
			HttpResponseMessage response = null;
			ExceptionDispatchInfo exceptionInfo = null;
			try
			{
				response = await continuation();
			}
			catch (Exception source)
			{
				exceptionInfo = ExceptionDispatchInfo.Capture(source);
			}
			Exception exception;
			if (exceptionInfo == null)
			{
				exception = null;
			}
			else
			{
				exception = exceptionInfo.SourceException;
			}
			HttpActionExecutedContext executedContext = new HttpActionExecutedContext(actionContext, exception)
			{
				Response = response
			};
			try
			{
				await this.OnActionExecutedAsync(executedContext, cancellationToken);
			}
			catch
			{
				actionContext.Response = null;
				throw;
			}
			if (executedContext.Response != null)
			{
				return executedContext.Response;
			}
			Exception newException = executedContext.Exception;
			if (newException != null)
			{
				if (newException != exception)
				{
					throw newException;
				}
				exceptionInfo.Throw();
			}
			throw Error.InvalidOperation(SRResources.ActionFilterAttribute_MustSupplyResponseOrException, new object[]
			{
				base.GetType().Name
			});
		}
	}
}
