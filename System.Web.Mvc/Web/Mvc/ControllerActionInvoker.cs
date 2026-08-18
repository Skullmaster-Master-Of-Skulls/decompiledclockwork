using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web.Mvc.Filters;
using System.Web.Mvc.Properties;
using System.Web.Mvc.Routing;
using System.Web.Routing;
using Microsoft.Web.Infrastructure.DynamicValidationHelper;

namespace System.Web.Mvc
{
	// Token: 0x020000EA RID: 234
	public class ControllerActionInvoker : IActionInvoker
	{
		// Token: 0x060005FB RID: 1531 RVA: 0x00010430 File Offset: 0x0000E630
		public ControllerActionInvoker()
		{
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x000104A0 File Offset: 0x0000E6A0
		internal ControllerActionInvoker(params object[] filters) : this()
		{
			if (filters != null)
			{
				this._getFiltersThunk = ((ControllerContext cc, ActionDescriptor ad) => from f in filters
				select new Filter(f, FilterScope.Action, null));
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060005FD RID: 1533 RVA: 0x000104E1 File Offset: 0x0000E6E1
		// (set) Token: 0x060005FE RID: 1534 RVA: 0x000104FC File Offset: 0x0000E6FC
		protected internal ModelBinderDictionary Binders
		{
			get
			{
				if (this._binders == null)
				{
					this._binders = ModelBinders.Binders;
				}
				return this._binders;
			}
			set
			{
				this._binders = value;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060005FF RID: 1535 RVA: 0x00010505 File Offset: 0x0000E705
		// (set) Token: 0x06000600 RID: 1536 RVA: 0x00010520 File Offset: 0x0000E720
		internal ControllerDescriptorCache DescriptorCache
		{
			get
			{
				if (this._instanceDescriptorCache == null)
				{
					this._instanceDescriptorCache = ControllerActionInvoker._staticDescriptorCache;
				}
				return this._instanceDescriptorCache;
			}
			set
			{
				this._instanceDescriptorCache = value;
			}
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x0001052C File Offset: 0x0000E72C
		protected virtual ActionResult CreateActionResult(ControllerContext controllerContext, ActionDescriptor actionDescriptor, object actionReturnValue)
		{
			if (actionReturnValue == null)
			{
				return new EmptyResult();
			}
			ActionResult result;
			if ((result = (actionReturnValue as ActionResult)) == null)
			{
				result = new ContentResult
				{
					Content = Convert.ToString(actionReturnValue, CultureInfo.InvariantCulture)
				};
			}
			return result;
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x00010570 File Offset: 0x0000E770
		protected virtual ControllerDescriptor GetControllerDescriptor(ControllerContext controllerContext)
		{
			Type type = controllerContext.Controller.GetType();
			return this.DescriptorCache.GetDescriptor<Type>(type, (Type innerType) => new ReflectedControllerDescriptor(innerType), type);
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x000105C8 File Offset: 0x0000E7C8
		protected virtual ActionDescriptor FindAction(ControllerContext controllerContext, ControllerDescriptor controllerDescriptor, string actionName)
		{
			if (!controllerContext.RouteData.HasDirectRouteMatch())
			{
				return controllerDescriptor.FindAction(controllerContext, actionName);
			}
			List<DirectRouteCandidate> directRouteCandidates = ControllerActionInvoker.GetDirectRouteCandidates(controllerContext);
			DirectRouteCandidate directRouteCandidate = DirectRouteCandidate.SelectBestCandidate(directRouteCandidates, controllerContext);
			if (directRouteCandidate == null)
			{
				return null;
			}
			controllerContext.RouteData = directRouteCandidate.RouteData;
			controllerContext.RequestContext.RouteData = directRouteCandidate.RouteData;
			directRouteCandidate.RouteData.Values.RemoveFromDictionary((KeyValuePair<string, object> entry) => entry.Value == UrlParameter.Optional);
			return directRouteCandidate.ActionDescriptor;
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x00010654 File Offset: 0x0000E854
		private static List<DirectRouteCandidate> GetDirectRouteCandidates(ControllerContext controllerContext)
		{
			List<DirectRouteCandidate> list = new List<DirectRouteCandidate>();
			RouteData routeData = controllerContext.RouteData;
			foreach (RouteData routeData2 in routeData.GetDirectRouteMatches())
			{
				if (routeData2 != null)
				{
					if (routeData2.GetTargetControllerDescriptor() == null)
					{
						throw new InvalidOperationException(MvcResources.DirectRoute_MissingControllerDescriptor);
					}
					ActionDescriptor[] targetActionDescriptors = routeData2.GetTargetActionDescriptors();
					if (targetActionDescriptors == null || targetActionDescriptors.Length == 0)
					{
						throw new InvalidOperationException(MvcResources.DirectRoute_MissingActionDescriptors);
					}
					foreach (ActionDescriptor actionDescriptor in targetActionDescriptors)
					{
						if (actionDescriptor != null)
						{
							list.Add(new DirectRouteCandidate
							{
								ActionDescriptor = actionDescriptor,
								ActionNameSelectors = actionDescriptor.GetNameSelectors(),
								ActionSelectors = actionDescriptor.GetSelectors(),
								Order = routeData2.GetOrder(),
								Precedence = routeData2.GetPrecedence(),
								RouteData = routeData2
							});
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x00010764 File Offset: 0x0000E964
		protected virtual FilterInfo GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
		{
			return new FilterInfo(this._getFiltersThunk(controllerContext, actionDescriptor));
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x00010778 File Offset: 0x0000E978
		private IModelBinder GetModelBinder(ParameterDescriptor parameterDescriptor)
		{
			return parameterDescriptor.BindingInfo.Binder ?? this.Binders.GetBinder(parameterDescriptor.ParameterType);
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x0001079C File Offset: 0x0000E99C
		protected virtual object GetParameterValue(ControllerContext controllerContext, ParameterDescriptor parameterDescriptor)
		{
			Type parameterType = parameterDescriptor.ParameterType;
			IModelBinder modelBinder = this.GetModelBinder(parameterDescriptor);
			IValueProvider valueProvider = controllerContext.Controller.ValueProvider;
			string modelName = parameterDescriptor.BindingInfo.Prefix ?? parameterDescriptor.ParameterName;
			Predicate<string> propertyFilter = ControllerActionInvoker.GetPropertyFilter(parameterDescriptor);
			ModelBindingContext bindingContext = new ModelBindingContext
			{
				FallbackToEmptyPrefix = (parameterDescriptor.BindingInfo.Prefix == null),
				ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(null, parameterType),
				ModelName = modelName,
				ModelState = controllerContext.Controller.ViewData.ModelState,
				PropertyFilter = propertyFilter,
				ValueProvider = valueProvider
			};
			object obj = modelBinder.BindModel(controllerContext, bindingContext);
			return obj ?? parameterDescriptor.DefaultValue;
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x0001085C File Offset: 0x0000EA5C
		protected virtual IDictionary<string, object> GetParameterValues(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
			ParameterDescriptor[] parameters = actionDescriptor.GetParameters();
			foreach (ParameterDescriptor parameterDescriptor in parameters)
			{
				dictionary[parameterDescriptor.ParameterName] = this.GetParameterValue(controllerContext, parameterDescriptor);
			}
			return dictionary;
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x000108D0 File Offset: 0x0000EAD0
		private static Predicate<string> GetPropertyFilter(ParameterDescriptor parameterDescriptor)
		{
			ParameterBindingInfo bindingInfo = parameterDescriptor.BindingInfo;
			return (string propertyName) => BindAttribute.IsPropertyAllowed(propertyName, bindingInfo.Include, bindingInfo.Exclude);
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x000108FC File Offset: 0x0000EAFC
		public virtual bool InvokeAction(ControllerContext controllerContext, string actionName)
		{
			if (controllerContext == null)
			{
				throw new ArgumentNullException("controllerContext");
			}
			if (string.IsNullOrEmpty(actionName) && !controllerContext.RouteData.HasDirectRouteMatch())
			{
				throw new ArgumentException(MvcResources.Common_NullOrEmpty, "actionName");
			}
			ControllerDescriptor controllerDescriptor = this.GetControllerDescriptor(controllerContext);
			ActionDescriptor actionDescriptor = this.FindAction(controllerContext, controllerDescriptor, actionName);
			if (actionDescriptor != null)
			{
				FilterInfo filters = this.GetFilters(controllerContext, actionDescriptor);
				try
				{
					AuthenticationContext authenticationContext = this.InvokeAuthenticationFilters(controllerContext, filters.AuthenticationFilters, actionDescriptor);
					if (authenticationContext.Result != null)
					{
						AuthenticationChallengeContext authenticationChallengeContext = this.InvokeAuthenticationFiltersChallenge(controllerContext, filters.AuthenticationFilters, actionDescriptor, authenticationContext.Result);
						this.InvokeActionResult(controllerContext, authenticationChallengeContext.Result ?? authenticationContext.Result);
					}
					else
					{
						AuthorizationContext authorizationContext = this.InvokeAuthorizationFilters(controllerContext, filters.AuthorizationFilters, actionDescriptor);
						if (authorizationContext.Result != null)
						{
							AuthenticationChallengeContext authenticationChallengeContext2 = this.InvokeAuthenticationFiltersChallenge(controllerContext, filters.AuthenticationFilters, actionDescriptor, authorizationContext.Result);
							this.InvokeActionResult(controllerContext, authenticationChallengeContext2.Result ?? authorizationContext.Result);
						}
						else
						{
							if (controllerContext.Controller.ValidateRequest)
							{
								ControllerActionInvoker.ValidateRequest(controllerContext);
							}
							IDictionary<string, object> parameterValues = this.GetParameterValues(controllerContext, actionDescriptor);
							ActionExecutedContext actionExecutedContext = this.InvokeActionMethodWithFilters(controllerContext, filters.ActionFilters, actionDescriptor, parameterValues);
							AuthenticationChallengeContext authenticationChallengeContext3 = this.InvokeAuthenticationFiltersChallenge(controllerContext, filters.AuthenticationFilters, actionDescriptor, actionExecutedContext.Result);
							this.InvokeActionResultWithFilters(controllerContext, filters.ResultFilters, authenticationChallengeContext3.Result ?? actionExecutedContext.Result);
						}
					}
				}
				catch (ThreadAbortException)
				{
					throw;
				}
				catch (Exception exception)
				{
					ExceptionContext exceptionContext = this.InvokeExceptionFilters(controllerContext, filters.ExceptionFilters, exception);
					if (!exceptionContext.ExceptionHandled)
					{
						throw;
					}
					this.InvokeActionResult(controllerContext, exceptionContext.Result);
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x00010AAC File Offset: 0x0000ECAC
		protected virtual ActionResult InvokeActionMethod(ControllerContext controllerContext, ActionDescriptor actionDescriptor, IDictionary<string, object> parameters)
		{
			object actionReturnValue = actionDescriptor.Execute(controllerContext, parameters);
			return this.CreateActionResult(controllerContext, actionDescriptor, actionReturnValue);
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x00010AD0 File Offset: 0x0000ECD0
		internal static ActionExecutedContext InvokeActionMethodFilter(IActionFilter filter, ActionExecutingContext preContext, Func<ActionExecutedContext> continuation)
		{
			filter.OnActionExecuting(preContext);
			if (preContext.Result != null)
			{
				return new ActionExecutedContext(preContext, preContext.ActionDescriptor, true, null)
				{
					Result = preContext.Result
				};
			}
			bool flag = false;
			ActionExecutedContext actionExecutedContext = null;
			try
			{
				actionExecutedContext = continuation();
			}
			catch (ThreadAbortException)
			{
				actionExecutedContext = new ActionExecutedContext(preContext, preContext.ActionDescriptor, false, null);
				filter.OnActionExecuted(actionExecutedContext);
				throw;
			}
			catch (Exception exception)
			{
				flag = true;
				actionExecutedContext = new ActionExecutedContext(preContext, preContext.ActionDescriptor, false, exception);
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
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x00010C24 File Offset: 0x0000EE24
		protected virtual ActionExecutedContext InvokeActionMethodWithFilters(ControllerContext controllerContext, IList<IActionFilter> filters, ActionDescriptor actionDescriptor, IDictionary<string, object> parameters)
		{
			ActionExecutingContext preContext = new ActionExecutingContext(controllerContext, actionDescriptor, parameters);
			Func<ActionExecutedContext> seed = () => new ActionExecutedContext(controllerContext, actionDescriptor, false, null)
			{
				Result = this.InvokeActionMethod(controllerContext, actionDescriptor, parameters)
			};
			Func<ActionExecutedContext> func = filters.Reverse<IActionFilter>().Aggregate(seed, (Func<ActionExecutedContext> next, IActionFilter filter) => () => ControllerActionInvoker.InvokeActionMethodFilter(filter, preContext, next));
			return func();
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x00010C9D File Offset: 0x0000EE9D
		protected virtual void InvokeActionResult(ControllerContext controllerContext, ActionResult actionResult)
		{
			actionResult.ExecuteResult(controllerContext);
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x00010CA8 File Offset: 0x0000EEA8
		private ResultExecutedContext InvokeActionResultFilterRecursive(IList<IResultFilter> filters, int filterIndex, ResultExecutingContext preContext, ControllerContext controllerContext, ActionResult actionResult)
		{
			if (filterIndex > filters.Count - 1)
			{
				this.InvokeActionResult(controllerContext, actionResult);
				return new ResultExecutedContext(controllerContext, actionResult, false, null);
			}
			IResultFilter resultFilter = filters[filterIndex];
			resultFilter.OnResultExecuting(preContext);
			if (preContext.Cancel)
			{
				return new ResultExecutedContext(preContext, preContext.Result, true, null);
			}
			bool flag = false;
			ResultExecutedContext resultExecutedContext = null;
			try
			{
				int filterIndex2 = filterIndex + 1;
				resultExecutedContext = this.InvokeActionResultFilterRecursive(filters, filterIndex2, preContext, controllerContext, actionResult);
			}
			catch (ThreadAbortException)
			{
				resultExecutedContext = new ResultExecutedContext(preContext, preContext.Result, false, null);
				resultFilter.OnResultExecuted(resultExecutedContext);
				throw;
			}
			catch (Exception exception)
			{
				flag = true;
				resultExecutedContext = new ResultExecutedContext(preContext, preContext.Result, false, exception);
				resultFilter.OnResultExecuted(resultExecutedContext);
				if (!resultExecutedContext.ExceptionHandled)
				{
					throw;
				}
			}
			if (!flag)
			{
				resultFilter.OnResultExecuted(resultExecutedContext);
			}
			return resultExecutedContext;
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x00010D7C File Offset: 0x0000EF7C
		protected virtual ResultExecutedContext InvokeActionResultWithFilters(ControllerContext controllerContext, IList<IResultFilter> filters, ActionResult actionResult)
		{
			ResultExecutingContext preContext = new ResultExecutingContext(controllerContext, actionResult);
			int filterIndex = 0;
			return this.InvokeActionResultFilterRecursive(filters, filterIndex, preContext, controllerContext, actionResult);
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x00010DA0 File Offset: 0x0000EFA0
		protected virtual AuthenticationContext InvokeAuthenticationFilters(ControllerContext controllerContext, IList<IAuthenticationFilter> filters, ActionDescriptor actionDescriptor)
		{
			if (controllerContext == null)
			{
				throw new ArgumentNullException("controllerContext");
			}
			IPrincipal user = controllerContext.HttpContext.User;
			AuthenticationContext authenticationContext = new AuthenticationContext(controllerContext, actionDescriptor, user);
			foreach (IAuthenticationFilter authenticationFilter in filters)
			{
				authenticationFilter.OnAuthentication(authenticationContext);
				if (authenticationContext.Result != null)
				{
					break;
				}
			}
			IPrincipal principal = authenticationContext.Principal;
			if (principal != user)
			{
				authenticationContext.HttpContext.User = principal;
				Thread.CurrentPrincipal = principal;
			}
			return authenticationContext;
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x00010E38 File Offset: 0x0000F038
		protected virtual AuthenticationChallengeContext InvokeAuthenticationFiltersChallenge(ControllerContext controllerContext, IList<IAuthenticationFilter> filters, ActionDescriptor actionDescriptor, ActionResult result)
		{
			AuthenticationChallengeContext authenticationChallengeContext = new AuthenticationChallengeContext(controllerContext, actionDescriptor, result);
			foreach (IAuthenticationFilter authenticationFilter in filters)
			{
				authenticationFilter.OnAuthenticationChallenge(authenticationChallengeContext);
			}
			return authenticationChallengeContext;
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x00010E8C File Offset: 0x0000F08C
		protected virtual AuthorizationContext InvokeAuthorizationFilters(ControllerContext controllerContext, IList<IAuthorizationFilter> filters, ActionDescriptor actionDescriptor)
		{
			AuthorizationContext authorizationContext = new AuthorizationContext(controllerContext, actionDescriptor);
			foreach (IAuthorizationFilter authorizationFilter in filters)
			{
				authorizationFilter.OnAuthorization(authorizationContext);
				if (authorizationContext.Result != null)
				{
					break;
				}
			}
			return authorizationContext;
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x00010EE8 File Offset: 0x0000F0E8
		protected virtual ExceptionContext InvokeExceptionFilters(ControllerContext controllerContext, IList<IExceptionFilter> filters, Exception exception)
		{
			ExceptionContext exceptionContext = new ExceptionContext(controllerContext, exception);
			foreach (IExceptionFilter exceptionFilter in filters.Reverse<IExceptionFilter>())
			{
				exceptionFilter.OnException(exceptionContext);
			}
			return exceptionContext;
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x00010F40 File Offset: 0x0000F140
		internal static void ValidateRequest(ControllerContext controllerContext)
		{
			if (controllerContext.IsChildAction)
			{
				return;
			}
			HttpContext httpContext = HttpContext.Current;
			if (httpContext != null)
			{
				ValidationUtility.EnableDynamicValidation(httpContext);
			}
			controllerContext.HttpContext.Request.ValidateInput();
		}

		// Token: 0x040001A7 RID: 423
		private static readonly ControllerDescriptorCache _staticDescriptorCache = new ControllerDescriptorCache();

		// Token: 0x040001A8 RID: 424
		private ModelBinderDictionary _binders;

		// Token: 0x040001A9 RID: 425
		private Func<ControllerContext, ActionDescriptor, IEnumerable<Filter>> _getFiltersThunk = new Func<ControllerContext, ActionDescriptor, IEnumerable<Filter>>(FilterProviders.Providers.GetFilters);

		// Token: 0x040001AA RID: 426
		private ControllerDescriptorCache _instanceDescriptorCache;
	}
}
