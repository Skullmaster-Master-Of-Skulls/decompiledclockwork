using System;
using System.Collections.Generic;
using System.Threading;
using System.Web.Mvc.Filters;
using System.Web.Mvc.Routing;

namespace System.Web.Mvc.Async
{
	// Token: 0x020000EC RID: 236
	public class AsyncControllerActionInvoker : ControllerActionInvoker, IAsyncActionInvoker, IActionInvoker
	{
		// Token: 0x0600061B RID: 1563 RVA: 0x000114C0 File Offset: 0x0000F6C0
		public virtual IAsyncResult BeginInvokeAction(ControllerContext controllerContext, string actionName, AsyncCallback callback, object state)
		{
			if (controllerContext == null)
			{
				throw new ArgumentNullException("controllerContext");
			}
			if (string.IsNullOrEmpty(actionName) && !controllerContext.RouteData.HasDirectRouteMatch())
			{
				throw Error.ParameterCannotBeNullOrEmpty("actionName");
			}
			ControllerDescriptor controllerDescriptor = this.GetControllerDescriptor(controllerContext);
			ActionDescriptor actionDescriptor = this.FindAction(controllerContext, controllerDescriptor, actionName);
			if (actionDescriptor != null)
			{
				FilterInfo filterInfo = this.GetFilters(controllerContext, actionDescriptor);
				Action continuation = null;
				BeginInvokeDelegate beginDelegate = delegate(AsyncCallback asyncCallback, object asyncState)
				{
					try
					{
						AuthenticationContext authenticationContext = this.InvokeAuthenticationFilters(controllerContext, filterInfo.AuthenticationFilters, actionDescriptor);
						if (authenticationContext.Result != null)
						{
							AuthenticationChallengeContext challengeContext = this.InvokeAuthenticationFiltersChallenge(controllerContext, filterInfo.AuthenticationFilters, actionDescriptor, authenticationContext.Result);
							continuation = delegate()
							{
								this.InvokeActionResult(controllerContext, challengeContext.Result ?? authenticationContext.Result);
							};
						}
						else
						{
							AsyncControllerActionInvoker.<>c__DisplayClass21.<>c__DisplayClass27 <>c__DisplayClass3 = new AsyncControllerActionInvoker.<>c__DisplayClass21.<>c__DisplayClass27();
							<>c__DisplayClass3.authorizationContext = this.InvokeAuthorizationFilters(controllerContext, filterInfo.AuthorizationFilters, actionDescriptor);
							if (<>c__DisplayClass3.authorizationContext.Result == null)
							{
								if (controllerContext.Controller.ValidateRequest)
								{
									ControllerActionInvoker.ValidateRequest(controllerContext);
								}
								IDictionary<string, object> parameterValues = this.GetParameterValues(controllerContext, actionDescriptor);
								IAsyncResult asyncResult = this.BeginInvokeActionMethodWithFilters(controllerContext, filterInfo.ActionFilters, actionDescriptor, parameterValues, asyncCallback, asyncState);
								continuation = delegate()
								{
									ActionExecutedContext actionExecutedContext = this.EndInvokeActionMethodWithFilters(asyncResult);
									AuthenticationChallengeContext authenticationChallengeContext = this.InvokeAuthenticationFiltersChallenge(controllerContext, filterInfo.AuthenticationFilters, actionDescriptor, actionExecutedContext.Result);
									this.InvokeActionResultWithFilters(controllerContext, filterInfo.ResultFilters, authenticationChallengeContext.Result ?? actionExecutedContext.Result);
								};
								return asyncResult;
							}
							AuthenticationChallengeContext challengeContext = this.InvokeAuthenticationFiltersChallenge(controllerContext, filterInfo.AuthenticationFilters, actionDescriptor, <>c__DisplayClass3.authorizationContext.Result);
							continuation = delegate()
							{
								this.InvokeActionResult(controllerContext, challengeContext.Result ?? <>c__DisplayClass3.authorizationContext.Result);
							};
						}
					}
					catch (ThreadAbortException)
					{
						throw;
					}
					catch (Exception exception)
					{
						ExceptionContext exceptionContext = this.InvokeExceptionFilters(controllerContext, filterInfo.ExceptionFilters, exception);
						if (!exceptionContext.ExceptionHandled)
						{
							throw;
						}
						continuation = delegate()
						{
							this.InvokeActionResult(controllerContext, exceptionContext.Result);
						};
					}
					return AsyncControllerActionInvoker.BeginInvokeAction_MakeSynchronousAsyncResult(asyncCallback, asyncState);
				};
				EndInvokeDelegate<bool> endDelegate = delegate(IAsyncResult asyncResult)
				{
					try
					{
						continuation();
					}
					catch (ThreadAbortException)
					{
						throw;
					}
					catch (Exception exception)
					{
						ExceptionContext exceptionContext = this.InvokeExceptionFilters(controllerContext, filterInfo.ExceptionFilters, exception);
						if (!exceptionContext.ExceptionHandled)
						{
							throw;
						}
						this.InvokeActionResult(controllerContext, exceptionContext.Result);
					}
					return true;
				};
				return AsyncResultWrapper.Begin<bool>(callback, state, beginDelegate, endDelegate, AsyncControllerActionInvoker._invokeActionTag, -1);
			}
			return AsyncControllerActionInvoker.BeginInvokeAction_ActionNotFound(callback, state);
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x000115B0 File Offset: 0x0000F7B0
		private static IAsyncResult BeginInvokeAction_ActionNotFound(AsyncCallback callback, object state)
		{
			BeginInvokeDelegate beginDelegate = new BeginInvokeDelegate(AsyncControllerActionInvoker.BeginInvokeAction_MakeSynchronousAsyncResult);
			EndInvokeDelegate<bool> endDelegate = (IAsyncResult asyncResult) => false;
			return AsyncResultWrapper.Begin<bool>(callback, state, beginDelegate, endDelegate, AsyncControllerActionInvoker._invokeActionTag, -1);
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x000115F8 File Offset: 0x0000F7F8
		private static IAsyncResult BeginInvokeAction_MakeSynchronousAsyncResult(AsyncCallback callback, object state)
		{
			SimpleAsyncResult simpleAsyncResult = new SimpleAsyncResult(state);
			simpleAsyncResult.MarkCompleted(true, callback);
			return simpleAsyncResult;
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x00011618 File Offset: 0x0000F818
		protected internal virtual IAsyncResult BeginInvokeActionMethod(ControllerContext controllerContext, ActionDescriptor actionDescriptor, IDictionary<string, object> parameters, AsyncCallback callback, object state)
		{
			AsyncActionDescriptor asyncActionDescriptor = actionDescriptor as AsyncActionDescriptor;
			if (asyncActionDescriptor != null)
			{
				return this.BeginInvokeAsynchronousActionMethod(controllerContext, asyncActionDescriptor, parameters, callback, state);
			}
			return this.BeginInvokeSynchronousActionMethod(controllerContext, actionDescriptor, parameters, callback, state);
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x000116C0 File Offset: 0x0000F8C0
		protected internal virtual IAsyncResult BeginInvokeActionMethodWithFilters(ControllerContext controllerContext, IList<IActionFilter> filters, ActionDescriptor actionDescriptor, IDictionary<string, object> parameters, AsyncCallback callback, object state)
		{
			Func<ActionExecutedContext> endContinuation = null;
			BeginInvokeDelegate beginDelegate = delegate(AsyncCallback asyncCallback, object asyncState)
			{
				AsyncControllerActionInvoker.AsyncInvocationWithFilters asyncInvocationWithFilters = new AsyncControllerActionInvoker.AsyncInvocationWithFilters(this, controllerContext, actionDescriptor, filters, parameters, asyncCallback, asyncState);
				endContinuation = asyncInvocationWithFilters.InvokeActionMethodFilterAsynchronouslyRecursive(0);
				if (asyncInvocationWithFilters.InnerAsyncResult != null)
				{
					return asyncInvocationWithFilters.InnerAsyncResult;
				}
				SimpleAsyncResult simpleAsyncResult = new SimpleAsyncResult(asyncState);
				simpleAsyncResult.MarkCompleted(true, asyncCallback);
				return simpleAsyncResult;
			};
			EndInvokeDelegate<ActionExecutedContext> endDelegate = (IAsyncResult asyncResult) => endContinuation();
			return AsyncResultWrapper.Begin<ActionExecutedContext>(callback, state, beginDelegate, endDelegate, AsyncControllerActionInvoker._invokeActionMethodWithFiltersTag, -1);
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x00011780 File Offset: 0x0000F980
		private IAsyncResult BeginInvokeAsynchronousActionMethod(ControllerContext controllerContext, AsyncActionDescriptor actionDescriptor, IDictionary<string, object> parameters, AsyncCallback callback, object state)
		{
			BeginInvokeDelegate beginDelegate = (AsyncCallback asyncCallback, object asyncState) => actionDescriptor.BeginExecute(controllerContext, parameters, asyncCallback, asyncState);
			EndInvokeDelegate<ActionResult> endDelegate = delegate(IAsyncResult asyncResult)
			{
				object actionReturnValue = actionDescriptor.EndExecute(asyncResult);
				return this.CreateActionResult(controllerContext, actionDescriptor, actionReturnValue);
			};
			return AsyncResultWrapper.Begin<ActionResult>(callback, state, beginDelegate, endDelegate, AsyncControllerActionInvoker._invokeActionMethodTag, -1);
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x000117E4 File Offset: 0x0000F9E4
		private IAsyncResult BeginInvokeSynchronousActionMethod(ControllerContext controllerContext, ActionDescriptor actionDescriptor, IDictionary<string, object> parameters, AsyncCallback callback, object state)
		{
			EndInvokeDelegate<AsyncControllerActionInvoker.ActionInvocation, ActionResult> func = (IAsyncResult asyncResult, AsyncControllerActionInvoker.ActionInvocation innerInvokeState) => innerInvokeState.InvokeSynchronousActionMethod();
			AsyncControllerActionInvoker.ActionInvocation funcState = new AsyncControllerActionInvoker.ActionInvocation(this, controllerContext, actionDescriptor, parameters);
			return AsyncResultWrapper.BeginSynchronous<ActionResult, AsyncControllerActionInvoker.ActionInvocation>(callback, state, func, funcState, AsyncControllerActionInvoker._invokeActionMethodTag);
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x0001182A File Offset: 0x0000FA2A
		public virtual bool EndInvokeAction(IAsyncResult asyncResult)
		{
			return AsyncResultWrapper.End<bool>(asyncResult, AsyncControllerActionInvoker._invokeActionTag);
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x00011837 File Offset: 0x0000FA37
		protected internal virtual ActionResult EndInvokeActionMethod(IAsyncResult asyncResult)
		{
			return AsyncResultWrapper.End<ActionResult>(asyncResult, AsyncControllerActionInvoker._invokeActionMethodTag);
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x00011844 File Offset: 0x0000FA44
		protected internal virtual ActionExecutedContext EndInvokeActionMethodWithFilters(IAsyncResult asyncResult)
		{
			return AsyncResultWrapper.End<ActionExecutedContext>(asyncResult, AsyncControllerActionInvoker._invokeActionMethodWithFiltersTag);
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x00011854 File Offset: 0x0000FA54
		protected override ControllerDescriptor GetControllerDescriptor(ControllerContext controllerContext)
		{
			Type type = controllerContext.Controller.GetType();
			return base.DescriptorCache.GetDescriptor<Type>(type, ReflectedAsyncControllerDescriptor.DefaultDescriptorFactory, type);
		}

		// Token: 0x040001AD RID: 429
		private static readonly object _invokeActionTag = new object();

		// Token: 0x040001AE RID: 430
		private static readonly object _invokeActionMethodTag = new object();

		// Token: 0x040001AF RID: 431
		private static readonly object _invokeActionMethodWithFiltersTag = new object();

		// Token: 0x020000ED RID: 237
		private struct ActionInvocation
		{
			// Token: 0x0600062A RID: 1578 RVA: 0x000118A9 File Offset: 0x0000FAA9
			internal ActionInvocation(AsyncControllerActionInvoker invoker, ControllerContext controllerContext, ActionDescriptor actionDescriptor, IDictionary<string, object> parameters)
			{
				this._invoker = invoker;
				this._controllerContext = controllerContext;
				this._actionDescriptor = actionDescriptor;
				this._parameters = parameters;
			}

			// Token: 0x0600062B RID: 1579 RVA: 0x000118C8 File Offset: 0x0000FAC8
			internal ActionResult InvokeSynchronousActionMethod()
			{
				return this._invoker.InvokeActionMethod(this._controllerContext, this._actionDescriptor, this._parameters);
			}

			// Token: 0x040001B2 RID: 434
			private readonly AsyncControllerActionInvoker _invoker;

			// Token: 0x040001B3 RID: 435
			private readonly ControllerContext _controllerContext;

			// Token: 0x040001B4 RID: 436
			private readonly ActionDescriptor _actionDescriptor;

			// Token: 0x040001B5 RID: 437
			private readonly IDictionary<string, object> _parameters;
		}

		// Token: 0x020000EE RID: 238
		private class AsyncInvocationWithFilters
		{
			// Token: 0x0600062C RID: 1580 RVA: 0x000118E8 File Offset: 0x0000FAE8
			internal AsyncInvocationWithFilters(AsyncControllerActionInvoker invoker, ControllerContext controllerContext, ActionDescriptor actionDescriptor, IList<IActionFilter> filters, IDictionary<string, object> parameters, AsyncCallback asyncCallback, object asyncState)
			{
				this._invoker = invoker;
				this._controllerContext = controllerContext;
				this._actionDescriptor = actionDescriptor;
				this._filters = filters;
				this._parameters = parameters;
				this._asyncCallback = asyncCallback;
				this._asyncState = asyncState;
				this._preContext = new ActionExecutingContext(controllerContext, actionDescriptor, parameters);
				this._filterCount = this._filters.Count;
			}

			// Token: 0x0600062D RID: 1581 RVA: 0x00011A84 File Offset: 0x0000FC84
			internal Func<ActionExecutedContext> InvokeActionMethodFilterAsynchronouslyRecursive(int filterIndex)
			{
				if (filterIndex > this._filterCount - 1)
				{
					this.InnerAsyncResult = this._invoker.BeginInvokeActionMethod(this._controllerContext, this._actionDescriptor, this._parameters, this._asyncCallback, this._asyncState);
					return () => new ActionExecutedContext(this._controllerContext, this._actionDescriptor, false, null)
					{
						Result = this._invoker.EndInvokeActionMethod(this.InnerAsyncResult)
					};
				}
				IActionFilter filter = this._filters[filterIndex];
				ActionExecutingContext preContext = this._preContext;
				filter.OnActionExecuting(preContext);
				if (preContext.Result != null)
				{
					ActionExecutedContext shortCircuitedPostContext = new ActionExecutedContext(preContext, preContext.ActionDescriptor, true, null)
					{
						Result = preContext.Result
					};
					return () => shortCircuitedPostContext;
				}
				Func<ActionExecutedContext> result;
				try
				{
					int filterIndex2 = filterIndex + 1;
					Func<ActionExecutedContext> continuation = this.InvokeActionMethodFilterAsynchronouslyRecursive(filterIndex2);
					result = delegate()
					{
						bool flag = true;
						ActionExecutedContext actionExecutedContext;
						try
						{
							actionExecutedContext = continuation();
							flag = false;
						}
						catch (ThreadAbortException)
						{
							actionExecutedContext = new ActionExecutedContext(preContext, preContext.ActionDescriptor, false, null);
							filter.OnActionExecuted(actionExecutedContext);
							throw;
						}
						catch (Exception exception2)
						{
							actionExecutedContext = new ActionExecutedContext(preContext, preContext.ActionDescriptor, false, exception2);
							filter.OnActionExecuted(actionExecutedContext);
							if (!actionExecutedContext.ExceptionHandled)
							{
								throw;
							}
						}
						if (!flag)
						{
							filter.OnActionExecuted(actionExecutedContext);
						}
						return actionExecutedContext;
					};
				}
				catch (ThreadAbortException)
				{
					ActionExecutedContext filterContext = new ActionExecutedContext(preContext, preContext.ActionDescriptor, false, null);
					filter.OnActionExecuted(filterContext);
					throw;
				}
				catch (Exception exception)
				{
					ActionExecutedContext postContext = new ActionExecutedContext(preContext, preContext.ActionDescriptor, false, exception);
					filter.OnActionExecuted(postContext);
					if (!postContext.ExceptionHandled)
					{
						throw;
					}
					result = (() => postContext);
				}
				return result;
			}

			// Token: 0x040001B6 RID: 438
			private readonly AsyncControllerActionInvoker _invoker;

			// Token: 0x040001B7 RID: 439
			private readonly ControllerContext _controllerContext;

			// Token: 0x040001B8 RID: 440
			private readonly ActionDescriptor _actionDescriptor;

			// Token: 0x040001B9 RID: 441
			private readonly IList<IActionFilter> _filters;

			// Token: 0x040001BA RID: 442
			private readonly IDictionary<string, object> _parameters;

			// Token: 0x040001BB RID: 443
			private readonly AsyncCallback _asyncCallback;

			// Token: 0x040001BC RID: 444
			private readonly object _asyncState;

			// Token: 0x040001BD RID: 445
			private readonly int _filterCount;

			// Token: 0x040001BE RID: 446
			private readonly ActionExecutingContext _preContext;

			// Token: 0x040001BF RID: 447
			internal IAsyncResult InnerAsyncResult;
		}
	}
}
