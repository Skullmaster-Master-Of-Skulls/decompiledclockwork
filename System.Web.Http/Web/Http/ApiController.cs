using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Filters;
using System.Web.Http.Metadata;
using System.Web.Http.ModelBinding;
using System.Web.Http.Properties;
using System.Web.Http.Results;
using System.Web.Http.Routing;
using System.Web.Http.Validation;
using Newtonsoft.Json;

namespace System.Web.Http
{
	// Token: 0x020001A8 RID: 424
	public abstract class ApiController : IHttpController, IDisposable
	{
		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000AC4 RID: 2756 RVA: 0x0002405B File Offset: 0x0002225B
		// (set) Token: 0x06000AC5 RID: 2757 RVA: 0x00024068 File Offset: 0x00022268
		public HttpConfiguration Configuration
		{
			get
			{
				return this.ControllerContext.Configuration;
			}
			set
			{
				this.ControllerContext.Configuration = value;
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000AC6 RID: 2758 RVA: 0x00024078 File Offset: 0x00022278
		// (set) Token: 0x06000AC7 RID: 2759 RVA: 0x000240BA File Offset: 0x000222BA
		public HttpControllerContext ControllerContext
		{
			get
			{
				if (this.ActionContext.ControllerContext == null)
				{
					this.ActionContext.ControllerContext = new HttpControllerContext
					{
						RequestContext = new RequestBackedHttpRequestContext()
					};
				}
				return this.ActionContext.ControllerContext;
			}
			set
			{
				if (value == null)
				{
					throw Error.PropertyNull();
				}
				this.ActionContext.ControllerContext = value;
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000AC8 RID: 2760 RVA: 0x000240D1 File Offset: 0x000222D1
		// (set) Token: 0x06000AC9 RID: 2761 RVA: 0x000240D9 File Offset: 0x000222D9
		public HttpActionContext ActionContext
		{
			get
			{
				return this._actionContext;
			}
			set
			{
				if (value == null)
				{
					throw Error.PropertyNull();
				}
				this._actionContext = value;
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000ACA RID: 2762 RVA: 0x000240EB File Offset: 0x000222EB
		public ModelStateDictionary ModelState
		{
			get
			{
				return this.ActionContext.ModelState;
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000ACB RID: 2763 RVA: 0x000240F8 File Offset: 0x000222F8
		// (set) Token: 0x06000ACC RID: 2764 RVA: 0x00024108 File Offset: 0x00022308
		public HttpRequestMessage Request
		{
			get
			{
				return this.ControllerContext.Request;
			}
			set
			{
				if (value == null)
				{
					throw Error.PropertyNull();
				}
				HttpRequestContext requestContext = value.GetRequestContext();
				HttpRequestContext requestContext2 = this.RequestContext;
				if (requestContext != null && requestContext != requestContext2)
				{
					throw new InvalidOperationException(SRResources.RequestContextConflict);
				}
				this.ControllerContext.Request = value;
				value.SetRequestContext(requestContext2);
				RequestBackedHttpRequestContext requestBackedHttpRequestContext = requestContext2 as RequestBackedHttpRequestContext;
				if (requestBackedHttpRequestContext != null)
				{
					requestBackedHttpRequestContext.Request = value;
				}
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000ACD RID: 2765 RVA: 0x00024162 File Offset: 0x00022362
		// (set) Token: 0x06000ACE RID: 2766 RVA: 0x00024170 File Offset: 0x00022370
		public HttpRequestContext RequestContext
		{
			get
			{
				return this.ControllerContext.RequestContext;
			}
			set
			{
				if (value == null)
				{
					throw Error.PropertyNull();
				}
				HttpRequestContext requestContext = this.ControllerContext.RequestContext;
				HttpRequestMessage request = this.Request;
				if (request != null)
				{
					HttpRequestContext requestContext2 = request.GetRequestContext();
					if (requestContext2 != null && requestContext2 != requestContext && requestContext2 != value)
					{
						throw new InvalidOperationException(SRResources.RequestContextConflict);
					}
					request.SetRequestContext(value);
				}
				this.ControllerContext.RequestContext = value;
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000ACF RID: 2767 RVA: 0x000241CC File Offset: 0x000223CC
		// (set) Token: 0x06000AD0 RID: 2768 RVA: 0x000241D9 File Offset: 0x000223D9
		public UrlHelper Url
		{
			get
			{
				return this.RequestContext.Url;
			}
			set
			{
				this.RequestContext.Url = value;
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000AD1 RID: 2769 RVA: 0x000241E7 File Offset: 0x000223E7
		// (set) Token: 0x06000AD2 RID: 2770 RVA: 0x000241F4 File Offset: 0x000223F4
		public IPrincipal User
		{
			get
			{
				return this.RequestContext.Principal;
			}
			set
			{
				this.RequestContext.Principal = value;
			}
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x00024204 File Offset: 0x00022404
		public virtual Task<HttpResponseMessage> ExecuteAsync(HttpControllerContext controllerContext, CancellationToken cancellationToken)
		{
			if (this._initialized)
			{
				throw Error.InvalidOperation(SRResources.CannotSupportSingletonInstance, new object[]
				{
					typeof(ApiController).Name,
					typeof(IHttpControllerActivator).Name
				});
			}
			this.Initialize(controllerContext);
			if (this.Request != null)
			{
				this.Request.RegisterForDispose(this);
			}
			HttpControllerDescriptor controllerDescriptor = controllerContext.ControllerDescriptor;
			ServicesContainer services = controllerDescriptor.Configuration.Services;
			HttpActionDescriptor httpActionDescriptor = services.GetActionSelector().SelectAction(controllerContext);
			this.ActionContext.ActionDescriptor = httpActionDescriptor;
			if (this.Request != null)
			{
				this.Request.SetActionDescriptor(httpActionDescriptor);
			}
			FilterGrouping filterGrouping = httpActionDescriptor.GetFilterGrouping();
			IActionFilter[] actionFilters = filterGrouping.ActionFilters;
			IAuthenticationFilter[] authenticationFilters = filterGrouping.AuthenticationFilters;
			IAuthorizationFilter[] authorizationFilters = filterGrouping.AuthorizationFilters;
			IExceptionFilter[] exceptionFilters = filterGrouping.ExceptionFilters;
			IHttpActionResult httpActionResult = new ActionFilterResult(httpActionDescriptor.ActionBinding, this.ActionContext, services, actionFilters);
			if (authorizationFilters.Length > 0)
			{
				httpActionResult = new AuthorizationFilterResult(this.ActionContext, authorizationFilters, httpActionResult);
			}
			if (authenticationFilters.Length > 0)
			{
				httpActionResult = new AuthenticationFilterResult(this.ActionContext, this, authenticationFilters, httpActionResult);
			}
			if (exceptionFilters.Length > 0)
			{
				IExceptionLogger logger = ExceptionServices.GetLogger(services);
				IExceptionHandler handler = ExceptionServices.GetHandler(services);
				httpActionResult = new ExceptionFilterResult(this.ActionContext, exceptionFilters, logger, handler, httpActionResult);
			}
			return httpActionResult.ExecuteAsync(cancellationToken);
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x00024351 File Offset: 0x00022551
		public void Validate<TEntity>(TEntity entity)
		{
			this.Validate<TEntity>(entity, string.Empty);
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x00024360 File Offset: 0x00022560
		public void Validate<TEntity>(TEntity entity, string keyPrefix)
		{
			if (this.Configuration == null)
			{
				throw Error.InvalidOperation(SRResources.TypePropertyMustNotBeNull, new object[]
				{
					typeof(ApiController).Name,
					"Configuration"
				});
			}
			IBodyModelValidator bodyModelValidator = this.Configuration.Services.GetBodyModelValidator();
			if (bodyModelValidator != null)
			{
				ModelMetadataProvider modelMetadataProvider = this.Configuration.Services.GetModelMetadataProvider();
				bodyModelValidator.Validate(entity, typeof(TEntity), modelMetadataProvider, this.ActionContext, keyPrefix);
			}
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x000243E6 File Offset: 0x000225E6
		protected internal virtual BadRequestResult BadRequest()
		{
			return new BadRequestResult(this);
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x000243EE File Offset: 0x000225EE
		protected internal virtual BadRequestErrorMessageResult BadRequest(string message)
		{
			return new BadRequestErrorMessageResult(message, this);
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x000243F7 File Offset: 0x000225F7
		protected internal virtual InvalidModelStateResult BadRequest(ModelStateDictionary modelState)
		{
			return new InvalidModelStateResult(modelState, this);
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x00024400 File Offset: 0x00022600
		protected internal virtual ConflictResult Conflict()
		{
			return new ConflictResult(this);
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x00024408 File Offset: 0x00022608
		protected internal virtual NegotiatedContentResult<T> Content<T>(HttpStatusCode statusCode, T value)
		{
			return new NegotiatedContentResult<T>(statusCode, value, this);
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x00024412 File Offset: 0x00022612
		protected internal FormattedContentResult<T> Content<T>(HttpStatusCode statusCode, T value, MediaTypeFormatter formatter)
		{
			return this.Content<T>(statusCode, value, formatter, null);
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x0002441E File Offset: 0x0002261E
		protected internal FormattedContentResult<T> Content<T>(HttpStatusCode statusCode, T value, MediaTypeFormatter formatter, string mediaType)
		{
			return this.Content<T>(statusCode, value, formatter, new MediaTypeHeaderValue(mediaType));
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x00024430 File Offset: 0x00022630
		protected internal virtual FormattedContentResult<T> Content<T>(HttpStatusCode statusCode, T value, MediaTypeFormatter formatter, MediaTypeHeaderValue mediaType)
		{
			return new FormattedContentResult<T>(statusCode, value, formatter, mediaType, this);
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x0002443D File Offset: 0x0002263D
		protected internal CreatedNegotiatedContentResult<T> Created<T>(string location, T content)
		{
			if (location == null)
			{
				throw new ArgumentNullException("location");
			}
			return this.Created<T>(new Uri(location, UriKind.RelativeOrAbsolute), content);
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x0002445B File Offset: 0x0002265B
		protected internal virtual CreatedNegotiatedContentResult<T> Created<T>(Uri location, T content)
		{
			return new CreatedNegotiatedContentResult<T>(location, content, this);
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x00024465 File Offset: 0x00022665
		protected internal CreatedAtRouteNegotiatedContentResult<T> CreatedAtRoute<T>(string routeName, object routeValues, T content)
		{
			return this.CreatedAtRoute<T>(routeName, new HttpRouteValueDictionary(routeValues), content);
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x00024475 File Offset: 0x00022675
		protected internal virtual CreatedAtRouteNegotiatedContentResult<T> CreatedAtRoute<T>(string routeName, IDictionary<string, object> routeValues, T content)
		{
			return new CreatedAtRouteNegotiatedContentResult<T>(routeName, routeValues, content, this);
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x00024480 File Offset: 0x00022680
		protected internal virtual InternalServerErrorResult InternalServerError()
		{
			return new InternalServerErrorResult(this);
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x00024488 File Offset: 0x00022688
		protected internal virtual ExceptionResult InternalServerError(Exception exception)
		{
			return new ExceptionResult(exception, this);
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x00024491 File Offset: 0x00022691
		protected internal JsonResult<T> Json<T>(T content)
		{
			return this.Json<T>(content, new JsonSerializerSettings());
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x0002449F File Offset: 0x0002269F
		protected internal JsonResult<T> Json<T>(T content, JsonSerializerSettings serializerSettings)
		{
			return this.Json<T>(content, serializerSettings, new UTF8Encoding(false, true));
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x000244B0 File Offset: 0x000226B0
		protected internal virtual JsonResult<T> Json<T>(T content, JsonSerializerSettings serializerSettings, Encoding encoding)
		{
			return new JsonResult<T>(content, serializerSettings, encoding, this);
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x000244BB File Offset: 0x000226BB
		protected internal virtual NotFoundResult NotFound()
		{
			return new NotFoundResult(this);
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x000244C3 File Offset: 0x000226C3
		protected internal virtual OkResult Ok()
		{
			return new OkResult(this);
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x000244CB File Offset: 0x000226CB
		protected internal virtual OkNegotiatedContentResult<T> Ok<T>(T content)
		{
			return new OkNegotiatedContentResult<T>(content, this);
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x000244D4 File Offset: 0x000226D4
		protected internal virtual RedirectResult Redirect(string location)
		{
			if (location == null)
			{
				throw new ArgumentNullException("location");
			}
			return this.Redirect(new Uri(location));
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x000244F0 File Offset: 0x000226F0
		protected internal virtual RedirectResult Redirect(Uri location)
		{
			return new RedirectResult(location, this);
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x000244F9 File Offset: 0x000226F9
		protected internal RedirectToRouteResult RedirectToRoute(string routeName, object routeValues)
		{
			return this.RedirectToRoute(routeName, new HttpRouteValueDictionary(routeValues));
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x00024508 File Offset: 0x00022708
		protected internal virtual RedirectToRouteResult RedirectToRoute(string routeName, IDictionary<string, object> routeValues)
		{
			return new RedirectToRouteResult(routeName, routeValues, this);
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x00024512 File Offset: 0x00022712
		protected internal virtual ResponseMessageResult ResponseMessage(HttpResponseMessage response)
		{
			return new ResponseMessageResult(response);
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x0002451A File Offset: 0x0002271A
		protected internal virtual StatusCodeResult StatusCode(HttpStatusCode status)
		{
			return new StatusCodeResult(status, this);
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x00024523 File Offset: 0x00022723
		protected internal UnauthorizedResult Unauthorized(params AuthenticationHeaderValue[] challenges)
		{
			return this.Unauthorized((IEnumerable<AuthenticationHeaderValue>)challenges);
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x00024531 File Offset: 0x00022731
		protected internal virtual UnauthorizedResult Unauthorized(IEnumerable<AuthenticationHeaderValue> challenges)
		{
			return new UnauthorizedResult(challenges, this);
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x0002453A File Offset: 0x0002273A
		protected virtual void Initialize(HttpControllerContext controllerContext)
		{
			if (controllerContext == null)
			{
				throw Error.ArgumentNull("controllerContext");
			}
			this._initialized = true;
			this.ControllerContext = controllerContext;
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x00024558 File Offset: 0x00022758
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x00024567 File Offset: 0x00022767
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x0400032B RID: 811
		private HttpActionContext _actionContext = new HttpActionContext();

		// Token: 0x0400032C RID: 812
		private bool _initialized;
	}
}
