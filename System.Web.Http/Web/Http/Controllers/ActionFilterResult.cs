using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace System.Web.Http.Controllers
{
	// Token: 0x02000028 RID: 40
	internal class ActionFilterResult : IHttpActionResult
	{
		// Token: 0x060000FE RID: 254 RVA: 0x00005CD2 File Offset: 0x00003ED2
		public ActionFilterResult(HttpActionBinding binding, HttpActionContext context, ServicesContainer services, IActionFilter[] filters)
		{
			this._binding = binding;
			this._context = context;
			this._services = services;
			this._filters = filters;
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00005F68 File Offset: 0x00004168
		public async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
		{
			await this._binding.ExecuteBindingAsync(this._context, cancellationToken);
			ActionFilterResult.ActionInvoker actionInvoker = new ActionFilterResult.ActionInvoker(this._context, cancellationToken, this._services);
			HttpResponseMessage result;
			if (this._filters.Length == 0)
			{
				result = await actionInvoker.InvokeActionAsync();
			}
			else
			{
				Func<ActionFilterResult.ActionInvoker, Task<HttpResponseMessage>> invokeCallback = (ActionFilterResult.ActionInvoker innerInvoker) => innerInvoker.InvokeActionAsync();
				result = await ActionFilterResult.InvokeActionWithActionFilters<ActionFilterResult.ActionInvoker>(this._context, cancellationToken, this._filters, invokeCallback, actionInvoker)();
			}
			return result;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00006024 File Offset: 0x00004224
		public static Func<Task<HttpResponseMessage>> InvokeActionWithActionFilters(HttpActionContext actionContext, CancellationToken cancellationToken, IActionFilter[] filters, Func<Task<HttpResponseMessage>> innerAction)
		{
			Func<Task<HttpResponseMessage>> func = innerAction;
			for (int i = filters.Length - 1; i >= 0; i--)
			{
				IActionFilter arg = filters[i];
				Func<Func<Task<HttpResponseMessage>>, IActionFilter, Func<Task<HttpResponseMessage>>> func2 = (Func<Task<HttpResponseMessage>> continuation, IActionFilter innerFilter) => () => innerFilter.ExecuteActionFilterAsync(actionContext, cancellationToken, continuation);
				func = func2(func, arg);
			}
			return func;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x0000609C File Offset: 0x0000429C
		private static Func<Task<HttpResponseMessage>> InvokeActionWithActionFilters<T>(HttpActionContext actionContext, CancellationToken cancellationToken, IActionFilter[] filters, Func<T, Task<HttpResponseMessage>> innerAction, T state)
		{
			return ActionFilterResult.InvokeActionWithActionFilters(actionContext, cancellationToken, filters, () => innerAction(state));
		}

		// Token: 0x0400004D RID: 77
		private readonly HttpActionBinding _binding;

		// Token: 0x0400004E RID: 78
		private readonly HttpActionContext _context;

		// Token: 0x0400004F RID: 79
		private readonly ServicesContainer _services;

		// Token: 0x04000050 RID: 80
		private readonly IActionFilter[] _filters;

		// Token: 0x02000029 RID: 41
		private struct ActionInvoker
		{
			// Token: 0x06000103 RID: 259 RVA: 0x000060D2 File Offset: 0x000042D2
			public ActionInvoker(HttpActionContext context, CancellationToken cancellationToken, ServicesContainer controllerServices)
			{
				this._context = context;
				this._cancellationToken = cancellationToken;
				this._controllerServices = controllerServices;
			}

			// Token: 0x06000104 RID: 260 RVA: 0x000060E9 File Offset: 0x000042E9
			public Task<HttpResponseMessage> InvokeActionAsync()
			{
				return this._controllerServices.GetActionInvoker().InvokeActionAsync(this._context, this._cancellationToken);
			}

			// Token: 0x04000052 RID: 82
			private readonly HttpActionContext _context;

			// Token: 0x04000053 RID: 83
			private readonly CancellationToken _cancellationToken;

			// Token: 0x04000054 RID: 84
			private readonly ServicesContainer _controllerServices;
		}
	}
}
