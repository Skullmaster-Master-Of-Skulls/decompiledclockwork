using System;
using System.Globalization;
using System.IO;
using System.Security.Principal;
using System.Text;
using System.Web.Mvc.Async;
using System.Web.Mvc.Filters;
using System.Web.Mvc.Properties;
using System.Web.Mvc.Routing;
using System.Web.Profile;
using System.Web.Routing;
using System.Web.WebPages;

namespace System.Web.Mvc
{
	// Token: 0x02000103 RID: 259
	public abstract class Controller : ControllerBase, IActionFilter, IAuthenticationFilter, IAuthorizationFilter, IDisposable, IExceptionFilter, IResultFilter, IAsyncController, IController, IAsyncManagerContainer
	{
		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000691 RID: 1681 RVA: 0x00012559 File Offset: 0x00010759
		// (set) Token: 0x06000692 RID: 1682 RVA: 0x0001256A File Offset: 0x0001076A
		public IDependencyResolver Resolver
		{
			get
			{
				return this._resolver ?? DependencyResolver.CurrentCache;
			}
			set
			{
				this._resolver = value;
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000693 RID: 1683 RVA: 0x00012573 File Offset: 0x00010773
		public AsyncManager AsyncManager
		{
			get
			{
				return this._asyncManager;
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000694 RID: 1684 RVA: 0x0001257B File Offset: 0x0001077B
		protected virtual bool DisableAsyncSupport
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000695 RID: 1685 RVA: 0x0001257E File Offset: 0x0001077E
		// (set) Token: 0x06000696 RID: 1686 RVA: 0x0001259A File Offset: 0x0001079A
		public IActionInvoker ActionInvoker
		{
			get
			{
				if (this._actionInvoker == null)
				{
					this._actionInvoker = this.CreateActionInvoker();
				}
				return this._actionInvoker;
			}
			set
			{
				this._actionInvoker = value;
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000697 RID: 1687 RVA: 0x000125A3 File Offset: 0x000107A3
		// (set) Token: 0x06000698 RID: 1688 RVA: 0x000125BE File Offset: 0x000107BE
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

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000699 RID: 1689 RVA: 0x000125C7 File Offset: 0x000107C7
		public HttpContextBase HttpContext
		{
			get
			{
				if (base.ControllerContext != null)
				{
					return base.ControllerContext.HttpContext;
				}
				return null;
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x0600069A RID: 1690 RVA: 0x000125DE File Offset: 0x000107DE
		public ModelStateDictionary ModelState
		{
			get
			{
				return base.ViewData.ModelState;
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x0600069B RID: 1691 RVA: 0x000125EB File Offset: 0x000107EB
		public ProfileBase Profile
		{
			get
			{
				if (this.HttpContext != null)
				{
					return this.HttpContext.Profile;
				}
				return null;
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x0600069C RID: 1692 RVA: 0x00012602 File Offset: 0x00010802
		public HttpRequestBase Request
		{
			get
			{
				if (this.HttpContext != null)
				{
					return this.HttpContext.Request;
				}
				return null;
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x0600069D RID: 1693 RVA: 0x00012619 File Offset: 0x00010819
		public HttpResponseBase Response
		{
			get
			{
				if (this.HttpContext != null)
				{
					return this.HttpContext.Response;
				}
				return null;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x0600069E RID: 1694 RVA: 0x00012630 File Offset: 0x00010830
		// (set) Token: 0x0600069F RID: 1695 RVA: 0x0001264B File Offset: 0x0001084B
		internal RouteCollection RouteCollection
		{
			get
			{
				if (this._routeCollection == null)
				{
					this._routeCollection = RouteTable.Routes;
				}
				return this._routeCollection;
			}
			set
			{
				this._routeCollection = value;
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x060006A0 RID: 1696 RVA: 0x00012654 File Offset: 0x00010854
		public RouteData RouteData
		{
			get
			{
				if (base.ControllerContext != null)
				{
					return base.ControllerContext.RouteData;
				}
				return null;
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x060006A1 RID: 1697 RVA: 0x0001266B File Offset: 0x0001086B
		public HttpServerUtilityBase Server
		{
			get
			{
				if (this.HttpContext != null)
				{
					return this.HttpContext.Server;
				}
				return null;
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x060006A2 RID: 1698 RVA: 0x00012682 File Offset: 0x00010882
		public HttpSessionStateBase Session
		{
			get
			{
				if (this.HttpContext != null)
				{
					return this.HttpContext.Session;
				}
				return null;
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x060006A3 RID: 1699 RVA: 0x00012699 File Offset: 0x00010899
		// (set) Token: 0x060006A4 RID: 1700 RVA: 0x000126B5 File Offset: 0x000108B5
		public ITempDataProvider TempDataProvider
		{
			get
			{
				if (this._tempDataProvider == null)
				{
					this._tempDataProvider = this.CreateTempDataProvider();
				}
				return this._tempDataProvider;
			}
			set
			{
				this._tempDataProvider = value;
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x060006A5 RID: 1701 RVA: 0x000126BE File Offset: 0x000108BE
		// (set) Token: 0x060006A6 RID: 1702 RVA: 0x000126C6 File Offset: 0x000108C6
		public UrlHelper Url { get; set; }

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x060006A7 RID: 1703 RVA: 0x000126CF File Offset: 0x000108CF
		public IPrincipal User
		{
			get
			{
				if (this.HttpContext != null)
				{
					return this.HttpContext.User;
				}
				return null;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x060006A8 RID: 1704 RVA: 0x000126E6 File Offset: 0x000108E6
		// (set) Token: 0x060006A9 RID: 1705 RVA: 0x000126F7 File Offset: 0x000108F7
		public ViewEngineCollection ViewEngineCollection
		{
			get
			{
				return this._viewEngineCollection ?? ViewEngines.Engines;
			}
			set
			{
				this._viewEngineCollection = value;
			}
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x00012700 File Offset: 0x00010900
		protected internal ContentResult Content(string content)
		{
			return this.Content(content, null);
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x0001270A File Offset: 0x0001090A
		protected internal ContentResult Content(string content, string contentType)
		{
			return this.Content(content, contentType, null);
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x00012718 File Offset: 0x00010918
		protected internal virtual ContentResult Content(string content, string contentType, Encoding contentEncoding)
		{
			return new ContentResult
			{
				Content = content,
				ContentType = contentType,
				ContentEncoding = contentEncoding
			};
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x00012744 File Offset: 0x00010944
		protected virtual IActionInvoker CreateActionInvoker()
		{
			IAsyncActionInvokerFactory service = this.Resolver.GetService<IAsyncActionInvokerFactory>();
			if (service != null)
			{
				return service.CreateInstance();
			}
			IActionInvokerFactory service2 = this.Resolver.GetService<IActionInvokerFactory>();
			if (service2 != null)
			{
				return service2.CreateInstance();
			}
			IAsyncActionInvoker result;
			if ((result = this.Resolver.GetService<IAsyncActionInvoker>()) == null)
			{
				result = (this.Resolver.GetService<IActionInvoker>() ?? new AsyncControllerActionInvoker());
			}
			return result;
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x000127A0 File Offset: 0x000109A0
		protected virtual ITempDataProvider CreateTempDataProvider()
		{
			ITempDataProviderFactory service = this.Resolver.GetService<ITempDataProviderFactory>();
			if (service != null)
			{
				return service.CreateInstance();
			}
			return this.Resolver.GetService<ITempDataProvider>() ?? new SessionStateTempDataProvider();
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x000127D7 File Offset: 0x000109D7
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x000127E6 File Offset: 0x000109E6
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x000127E8 File Offset: 0x000109E8
		protected override void ExecuteCore()
		{
			this.PossiblyLoadTempData();
			try
			{
				string actionName = Controller.GetActionName(this.RouteData);
				if (!this.ActionInvoker.InvokeAction(base.ControllerContext, actionName))
				{
					this.HandleUnknownAction(actionName);
				}
			}
			finally
			{
				this.PossiblySaveTempData();
			}
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x0001283C File Offset: 0x00010A3C
		protected internal FileContentResult File(byte[] fileContents, string contentType)
		{
			return this.File(fileContents, contentType, null);
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x00012848 File Offset: 0x00010A48
		protected internal virtual FileContentResult File(byte[] fileContents, string contentType, string fileDownloadName)
		{
			return new FileContentResult(fileContents, contentType)
			{
				FileDownloadName = fileDownloadName
			};
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x00012865 File Offset: 0x00010A65
		protected internal FileStreamResult File(Stream fileStream, string contentType)
		{
			return this.File(fileStream, contentType, null);
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x00012870 File Offset: 0x00010A70
		protected internal virtual FileStreamResult File(Stream fileStream, string contentType, string fileDownloadName)
		{
			return new FileStreamResult(fileStream, contentType)
			{
				FileDownloadName = fileDownloadName
			};
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x0001288D File Offset: 0x00010A8D
		protected internal FilePathResult File(string fileName, string contentType)
		{
			return this.File(fileName, contentType, null);
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x00012898 File Offset: 0x00010A98
		protected internal virtual FilePathResult File(string fileName, string contentType, string fileDownloadName)
		{
			return new FilePathResult(fileName, contentType)
			{
				FileDownloadName = fileDownloadName
			};
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x000128B5 File Offset: 0x00010AB5
		private static string GetActionName(RouteData routeData)
		{
			if (routeData.HasDirectRouteMatch())
			{
				return null;
			}
			return routeData.GetRequiredString("action");
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x000128CC File Offset: 0x00010ACC
		protected virtual void HandleUnknownAction(string actionName)
		{
			if (string.IsNullOrEmpty(actionName))
			{
				throw new HttpException(404, string.Format(CultureInfo.CurrentCulture, MvcResources.Controller_UnknownAction_NoActionName, new object[]
				{
					base.GetType().FullName
				}));
			}
			throw new HttpException(404, string.Format(CultureInfo.CurrentCulture, MvcResources.Controller_UnknownAction, new object[]
			{
				actionName,
				base.GetType().FullName
			}));
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x00012944 File Offset: 0x00010B44
		protected internal HttpNotFoundResult HttpNotFound()
		{
			return this.HttpNotFound(null);
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x0001294D File Offset: 0x00010B4D
		protected internal virtual HttpNotFoundResult HttpNotFound(string statusDescription)
		{
			return new HttpNotFoundResult(statusDescription);
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x00012958 File Offset: 0x00010B58
		protected internal virtual JavaScriptResult JavaScript(string script)
		{
			return new JavaScriptResult
			{
				Script = script
			};
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x00012973 File Offset: 0x00010B73
		protected internal JsonResult Json(object data)
		{
			return this.Json(data, null, null, JsonRequestBehavior.DenyGet);
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x0001297F File Offset: 0x00010B7F
		protected internal JsonResult Json(object data, string contentType)
		{
			return this.Json(data, contentType, null, JsonRequestBehavior.DenyGet);
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x0001298B File Offset: 0x00010B8B
		protected internal virtual JsonResult Json(object data, string contentType, Encoding contentEncoding)
		{
			return this.Json(data, contentType, contentEncoding, JsonRequestBehavior.DenyGet);
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x00012997 File Offset: 0x00010B97
		protected internal JsonResult Json(object data, JsonRequestBehavior behavior)
		{
			return this.Json(data, null, null, behavior);
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x000129A3 File Offset: 0x00010BA3
		protected internal JsonResult Json(object data, string contentType, JsonRequestBehavior behavior)
		{
			return this.Json(data, contentType, null, behavior);
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x000129B0 File Offset: 0x00010BB0
		protected internal virtual JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
		{
			return new JsonResult
			{
				Data = data,
				ContentType = contentType,
				ContentEncoding = contentEncoding,
				JsonRequestBehavior = behavior
			};
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x000129E1 File Offset: 0x00010BE1
		protected override void Initialize(RequestContext requestContext)
		{
			base.Initialize(requestContext);
			this.Url = new UrlHelper(requestContext);
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x000129F6 File Offset: 0x00010BF6
		protected virtual void OnActionExecuting(ActionExecutingContext filterContext)
		{
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x000129F8 File Offset: 0x00010BF8
		protected virtual void OnActionExecuted(ActionExecutedContext filterContext)
		{
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x000129FA File Offset: 0x00010BFA
		protected virtual void OnAuthentication(AuthenticationContext filterContext)
		{
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x000129FC File Offset: 0x00010BFC
		protected virtual void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
		{
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x000129FE File Offset: 0x00010BFE
		protected virtual void OnAuthorization(AuthorizationContext filterContext)
		{
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x00012A00 File Offset: 0x00010C00
		protected virtual void OnException(ExceptionContext filterContext)
		{
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x00012A02 File Offset: 0x00010C02
		protected virtual void OnResultExecuted(ResultExecutedContext filterContext)
		{
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x00012A04 File Offset: 0x00010C04
		protected virtual void OnResultExecuting(ResultExecutingContext filterContext)
		{
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x00012A06 File Offset: 0x00010C06
		protected internal PartialViewResult PartialView()
		{
			return this.PartialView(null, null);
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x00012A10 File Offset: 0x00010C10
		protected internal PartialViewResult PartialView(object model)
		{
			return this.PartialView(null, model);
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x00012A1A File Offset: 0x00010C1A
		protected internal PartialViewResult PartialView(string viewName)
		{
			return this.PartialView(viewName, null);
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x00012A24 File Offset: 0x00010C24
		protected internal virtual PartialViewResult PartialView(string viewName, object model)
		{
			if (model != null)
			{
				base.ViewData.Model = model;
			}
			return new PartialViewResult
			{
				ViewName = viewName,
				ViewData = base.ViewData,
				TempData = base.TempData,
				ViewEngineCollection = this.ViewEngineCollection
			};
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x00012A72 File Offset: 0x00010C72
		internal void PossiblyLoadTempData()
		{
			if (!base.ControllerContext.IsChildAction)
			{
				base.TempData.Load(base.ControllerContext, this.TempDataProvider);
			}
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x00012A98 File Offset: 0x00010C98
		internal void PossiblySaveTempData()
		{
			if (!base.ControllerContext.IsChildAction)
			{
				base.TempData.Save(base.ControllerContext, this.TempDataProvider);
			}
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x00012ABE File Offset: 0x00010CBE
		protected internal virtual RedirectResult Redirect(string url)
		{
			if (string.IsNullOrEmpty(url))
			{
				throw new ArgumentException(MvcResources.Common_NullOrEmpty, "url");
			}
			return new RedirectResult(url);
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x00012ADE File Offset: 0x00010CDE
		protected internal virtual RedirectResult RedirectPermanent(string url)
		{
			if (string.IsNullOrEmpty(url))
			{
				throw new ArgumentException(MvcResources.Common_NullOrEmpty, "url");
			}
			return new RedirectResult(url, true);
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x00012AFF File Offset: 0x00010CFF
		protected internal RedirectToRouteResult RedirectToAction(string actionName)
		{
			return this.RedirectToAction(actionName, null);
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x00012B09 File Offset: 0x00010D09
		protected internal RedirectToRouteResult RedirectToAction(string actionName, object routeValues)
		{
			return this.RedirectToAction(actionName, TypeHelper.ObjectToDictionary(routeValues));
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x00012B18 File Offset: 0x00010D18
		protected internal RedirectToRouteResult RedirectToAction(string actionName, RouteValueDictionary routeValues)
		{
			return this.RedirectToAction(actionName, null, routeValues);
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x00012B23 File Offset: 0x00010D23
		protected internal RedirectToRouteResult RedirectToAction(string actionName, string controllerName)
		{
			return this.RedirectToAction(actionName, controllerName, null);
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x00012B2E File Offset: 0x00010D2E
		protected internal RedirectToRouteResult RedirectToAction(string actionName, string controllerName, object routeValues)
		{
			return this.RedirectToAction(actionName, controllerName, TypeHelper.ObjectToDictionary(routeValues));
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x00012B40 File Offset: 0x00010D40
		protected internal virtual RedirectToRouteResult RedirectToAction(string actionName, string controllerName, RouteValueDictionary routeValues)
		{
			RouteValueDictionary routeValues2;
			if (this.RouteData == null)
			{
				routeValues2 = RouteValuesHelpers.MergeRouteValues(actionName, controllerName, null, routeValues, true);
			}
			else
			{
				routeValues2 = RouteValuesHelpers.MergeRouteValues(actionName, controllerName, this.RouteData.Values, routeValues, true);
			}
			return new RedirectToRouteResult(routeValues2);
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x00012B7D File Offset: 0x00010D7D
		protected internal RedirectToRouteResult RedirectToActionPermanent(string actionName)
		{
			return this.RedirectToActionPermanent(actionName, null);
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x00012B87 File Offset: 0x00010D87
		protected internal RedirectToRouteResult RedirectToActionPermanent(string actionName, object routeValues)
		{
			return this.RedirectToActionPermanent(actionName, TypeHelper.ObjectToDictionary(routeValues));
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x00012B96 File Offset: 0x00010D96
		protected internal RedirectToRouteResult RedirectToActionPermanent(string actionName, RouteValueDictionary routeValues)
		{
			return this.RedirectToActionPermanent(actionName, null, routeValues);
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x00012BA1 File Offset: 0x00010DA1
		protected internal RedirectToRouteResult RedirectToActionPermanent(string actionName, string controllerName)
		{
			return this.RedirectToActionPermanent(actionName, controllerName, null);
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x00012BAC File Offset: 0x00010DAC
		protected internal RedirectToRouteResult RedirectToActionPermanent(string actionName, string controllerName, object routeValues)
		{
			return this.RedirectToActionPermanent(actionName, controllerName, TypeHelper.ObjectToDictionary(routeValues));
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x00012BBC File Offset: 0x00010DBC
		protected internal virtual RedirectToRouteResult RedirectToActionPermanent(string actionName, string controllerName, RouteValueDictionary routeValues)
		{
			RouteValueDictionary implicitRouteValues = (this.RouteData != null) ? this.RouteData.Values : null;
			RouteValueDictionary routeValues2 = RouteValuesHelpers.MergeRouteValues(actionName, controllerName, implicitRouteValues, routeValues, true);
			return new RedirectToRouteResult(null, routeValues2, true);
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x00012BF3 File Offset: 0x00010DF3
		protected internal RedirectToRouteResult RedirectToRoute(object routeValues)
		{
			return this.RedirectToRoute(TypeHelper.ObjectToDictionary(routeValues));
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x00012C01 File Offset: 0x00010E01
		protected internal RedirectToRouteResult RedirectToRoute(RouteValueDictionary routeValues)
		{
			return this.RedirectToRoute(null, routeValues);
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x00012C0B File Offset: 0x00010E0B
		protected internal RedirectToRouteResult RedirectToRoute(string routeName)
		{
			return this.RedirectToRoute(routeName, null);
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x00012C15 File Offset: 0x00010E15
		protected internal RedirectToRouteResult RedirectToRoute(string routeName, object routeValues)
		{
			return this.RedirectToRoute(routeName, TypeHelper.ObjectToDictionary(routeValues));
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x00012C24 File Offset: 0x00010E24
		protected internal virtual RedirectToRouteResult RedirectToRoute(string routeName, RouteValueDictionary routeValues)
		{
			return new RedirectToRouteResult(routeName, RouteValuesHelpers.GetRouteValues(routeValues));
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x00012C32 File Offset: 0x00010E32
		protected internal RedirectToRouteResult RedirectToRoutePermanent(object routeValues)
		{
			return this.RedirectToRoutePermanent(TypeHelper.ObjectToDictionary(routeValues));
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x00012C40 File Offset: 0x00010E40
		protected internal RedirectToRouteResult RedirectToRoutePermanent(RouteValueDictionary routeValues)
		{
			return this.RedirectToRoutePermanent(null, routeValues);
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x00012C4A File Offset: 0x00010E4A
		protected internal RedirectToRouteResult RedirectToRoutePermanent(string routeName)
		{
			return this.RedirectToRoutePermanent(routeName, null);
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x00012C54 File Offset: 0x00010E54
		protected internal RedirectToRouteResult RedirectToRoutePermanent(string routeName, object routeValues)
		{
			return this.RedirectToRoutePermanent(routeName, TypeHelper.ObjectToDictionary(routeValues));
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x00012C63 File Offset: 0x00010E63
		protected internal virtual RedirectToRouteResult RedirectToRoutePermanent(string routeName, RouteValueDictionary routeValues)
		{
			return new RedirectToRouteResult(routeName, RouteValuesHelpers.GetRouteValues(routeValues), true);
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x00012C72 File Offset: 0x00010E72
		protected internal bool TryUpdateModel<TModel>(TModel model) where TModel : class
		{
			return this.TryUpdateModel<TModel>(model, null, null, null, base.ValueProvider);
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x00012C84 File Offset: 0x00010E84
		protected internal bool TryUpdateModel<TModel>(TModel model, string prefix) where TModel : class
		{
			return this.TryUpdateModel<TModel>(model, prefix, null, null, base.ValueProvider);
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x00012C96 File Offset: 0x00010E96
		protected internal bool TryUpdateModel<TModel>(TModel model, string[] includeProperties) where TModel : class
		{
			return this.TryUpdateModel<TModel>(model, null, includeProperties, null, base.ValueProvider);
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x00012CA8 File Offset: 0x00010EA8
		protected internal bool TryUpdateModel<TModel>(TModel model, string prefix, string[] includeProperties) where TModel : class
		{
			return this.TryUpdateModel<TModel>(model, prefix, includeProperties, null, base.ValueProvider);
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x00012CBA File Offset: 0x00010EBA
		protected internal bool TryUpdateModel<TModel>(TModel model, string prefix, string[] includeProperties, string[] excludeProperties) where TModel : class
		{
			return this.TryUpdateModel<TModel>(model, prefix, includeProperties, excludeProperties, base.ValueProvider);
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x00012CCD File Offset: 0x00010ECD
		protected internal bool TryUpdateModel<TModel>(TModel model, IValueProvider valueProvider) where TModel : class
		{
			return this.TryUpdateModel<TModel>(model, null, null, null, valueProvider);
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x00012CDA File Offset: 0x00010EDA
		protected internal bool TryUpdateModel<TModel>(TModel model, string prefix, IValueProvider valueProvider) where TModel : class
		{
			return this.TryUpdateModel<TModel>(model, prefix, null, null, valueProvider);
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x00012CE7 File Offset: 0x00010EE7
		protected internal bool TryUpdateModel<TModel>(TModel model, string[] includeProperties, IValueProvider valueProvider) where TModel : class
		{
			return this.TryUpdateModel<TModel>(model, null, includeProperties, null, valueProvider);
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x00012CF4 File Offset: 0x00010EF4
		protected internal bool TryUpdateModel<TModel>(TModel model, string prefix, string[] includeProperties, IValueProvider valueProvider) where TModel : class
		{
			return this.TryUpdateModel<TModel>(model, prefix, includeProperties, null, valueProvider);
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x00012D2C File Offset: 0x00010F2C
		protected internal bool TryUpdateModel<TModel>(TModel model, string prefix, string[] includeProperties, string[] excludeProperties, IValueProvider valueProvider) where TModel : class
		{
			if (model == null)
			{
				throw new ArgumentNullException("model");
			}
			if (valueProvider == null)
			{
				throw new ArgumentNullException("valueProvider");
			}
			Predicate<string> propertyFilter = (string propertyName) => BindAttribute.IsPropertyAllowed(propertyName, includeProperties, excludeProperties);
			IModelBinder binder = this.Binders.GetBinder(typeof(TModel));
			ModelBindingContext bindingContext = new ModelBindingContext
			{
				ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(() => model, typeof(TModel)),
				ModelName = prefix,
				ModelState = this.ModelState,
				PropertyFilter = propertyFilter,
				ValueProvider = valueProvider
			};
			binder.BindModel(base.ControllerContext, bindingContext);
			return this.ModelState.IsValid;
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x00012E0F File Offset: 0x0001100F
		protected internal bool TryValidateModel(object model)
		{
			return this.TryValidateModel(model, null);
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x00012E2C File Offset: 0x0001102C
		protected internal bool TryValidateModel(object model, string prefix)
		{
			if (model == null)
			{
				throw new ArgumentNullException("model");
			}
			ModelMetadata metadataForType = ModelMetadataProviders.Current.GetMetadataForType(() => model, model.GetType());
			foreach (ModelValidationResult modelValidationResult in ModelValidator.GetModelValidator(metadataForType, base.ControllerContext).Validate(null))
			{
				this.ModelState.AddModelError(DefaultModelBinder.CreateSubPropertyName(prefix, modelValidationResult.MemberName), modelValidationResult.Message);
			}
			return this.ModelState.IsValid;
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x00012EE8 File Offset: 0x000110E8
		protected internal void UpdateModel<TModel>(TModel model) where TModel : class
		{
			this.UpdateModel<TModel>(model, null, null, null, base.ValueProvider);
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x00012EFA File Offset: 0x000110FA
		protected internal void UpdateModel<TModel>(TModel model, string prefix) where TModel : class
		{
			this.UpdateModel<TModel>(model, prefix, null, null, base.ValueProvider);
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x00012F0C File Offset: 0x0001110C
		protected internal void UpdateModel<TModel>(TModel model, string[] includeProperties) where TModel : class
		{
			this.UpdateModel<TModel>(model, null, includeProperties, null, base.ValueProvider);
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x00012F1E File Offset: 0x0001111E
		protected internal void UpdateModel<TModel>(TModel model, string prefix, string[] includeProperties) where TModel : class
		{
			this.UpdateModel<TModel>(model, prefix, includeProperties, null, base.ValueProvider);
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x00012F30 File Offset: 0x00011130
		protected internal void UpdateModel<TModel>(TModel model, string prefix, string[] includeProperties, string[] excludeProperties) where TModel : class
		{
			this.UpdateModel<TModel>(model, prefix, includeProperties, excludeProperties, base.ValueProvider);
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x00012F43 File Offset: 0x00011143
		protected internal void UpdateModel<TModel>(TModel model, IValueProvider valueProvider) where TModel : class
		{
			this.UpdateModel<TModel>(model, null, null, null, valueProvider);
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x00012F50 File Offset: 0x00011150
		protected internal void UpdateModel<TModel>(TModel model, string prefix, IValueProvider valueProvider) where TModel : class
		{
			this.UpdateModel<TModel>(model, prefix, null, null, valueProvider);
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x00012F5D File Offset: 0x0001115D
		protected internal void UpdateModel<TModel>(TModel model, string[] includeProperties, IValueProvider valueProvider) where TModel : class
		{
			this.UpdateModel<TModel>(model, null, includeProperties, null, valueProvider);
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x00012F6A File Offset: 0x0001116A
		protected internal void UpdateModel<TModel>(TModel model, string prefix, string[] includeProperties, IValueProvider valueProvider) where TModel : class
		{
			this.UpdateModel<TModel>(model, prefix, includeProperties, null, valueProvider);
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x00012F78 File Offset: 0x00011178
		protected internal void UpdateModel<TModel>(TModel model, string prefix, string[] includeProperties, string[] excludeProperties, IValueProvider valueProvider) where TModel : class
		{
			if (!this.TryUpdateModel<TModel>(model, prefix, includeProperties, excludeProperties, valueProvider))
			{
				string message = string.Format(CultureInfo.CurrentCulture, MvcResources.Controller_UpdateModel_UpdateUnsuccessful, new object[]
				{
					typeof(TModel).FullName
				});
				throw new InvalidOperationException(message);
			}
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x00012FC7 File Offset: 0x000111C7
		protected internal void ValidateModel(object model)
		{
			this.ValidateModel(model, null);
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x00012FD4 File Offset: 0x000111D4
		protected internal void ValidateModel(object model, string prefix)
		{
			if (!this.TryValidateModel(model, prefix))
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, MvcResources.Controller_Validate_ValidationFailed, new object[]
				{
					model.GetType().FullName
				}));
			}
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x00013016 File Offset: 0x00011216
		protected internal ViewResult View()
		{
			return this.View(null, null, null);
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x00013021 File Offset: 0x00011221
		protected internal ViewResult View(object model)
		{
			return this.View(null, null, model);
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x0001302C File Offset: 0x0001122C
		protected internal ViewResult View(string viewName)
		{
			return this.View(viewName, null, null);
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x00013037 File Offset: 0x00011237
		protected internal ViewResult View(string viewName, string masterName)
		{
			return this.View(viewName, masterName, null);
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x00013042 File Offset: 0x00011242
		protected internal ViewResult View(string viewName, object model)
		{
			return this.View(viewName, null, model);
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x00013050 File Offset: 0x00011250
		protected internal virtual ViewResult View(string viewName, string masterName, object model)
		{
			if (model != null)
			{
				base.ViewData.Model = model;
			}
			return new ViewResult
			{
				ViewName = viewName,
				MasterName = masterName,
				ViewData = base.ViewData,
				TempData = base.TempData,
				ViewEngineCollection = this.ViewEngineCollection
			};
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x000130A5 File Offset: 0x000112A5
		protected internal ViewResult View(IView view)
		{
			return this.View(view, null);
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x000130B0 File Offset: 0x000112B0
		protected internal virtual ViewResult View(IView view, object model)
		{
			if (model != null)
			{
				base.ViewData.Model = model;
			}
			return new ViewResult
			{
				View = view,
				ViewData = base.ViewData,
				TempData = base.TempData
			};
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x000130F2 File Offset: 0x000112F2
		IAsyncResult IAsyncController.BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
		{
			return this.BeginExecute(requestContext, callback, state);
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x000130FD File Offset: 0x000112FD
		void IAsyncController.EndExecute(IAsyncResult asyncResult)
		{
			this.EndExecute(asyncResult);
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x00013134 File Offset: 0x00011334
		protected virtual IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
		{
			if (this.DisableAsyncSupport)
			{
				Action action = delegate()
				{
					this.Execute(requestContext);
				};
				return AsyncResultWrapper.BeginSynchronous(callback, state, action, Controller._executeTag);
			}
			if (requestContext == null)
			{
				throw new ArgumentNullException("requestContext");
			}
			base.VerifyExecuteCalledOnce();
			this.Initialize(requestContext);
			BeginInvokeDelegate<Controller> beginDelegate = (AsyncCallback asyncCallback, object callbackState, Controller controller) => controller.BeginExecuteCore(asyncCallback, callbackState);
			EndInvokeVoidDelegate<Controller> endDelegate = delegate(IAsyncResult asyncResult, Controller controller)
			{
				controller.EndExecuteCore(asyncResult);
			};
			return AsyncResultWrapper.Begin<Controller>(callback, state, beginDelegate, endDelegate, this, Controller._executeTag, -1, null);
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x00013278 File Offset: 0x00011478
		protected virtual IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
		{
			this.PossiblyLoadTempData();
			IAsyncResult result;
			try
			{
				string actionName = Controller.GetActionName(this.RouteData);
				IActionInvoker invoker = this.ActionInvoker;
				IAsyncActionInvoker asyncActionInvoker = invoker as IAsyncActionInvoker;
				if (asyncActionInvoker != null)
				{
					BeginInvokeDelegate<Controller.ExecuteCoreState> beginDelegate = (AsyncCallback asyncCallback, object asyncState, Controller.ExecuteCoreState innerState) => innerState.AsyncInvoker.BeginInvokeAction(innerState.Controller.ControllerContext, innerState.ActionName, asyncCallback, asyncState);
					EndInvokeVoidDelegate<Controller.ExecuteCoreState> endDelegate = delegate(IAsyncResult asyncResult, Controller.ExecuteCoreState innerState)
					{
						if (!innerState.AsyncInvoker.EndInvokeAction(asyncResult))
						{
							innerState.Controller.HandleUnknownAction(innerState.ActionName);
						}
					};
					Controller.ExecuteCoreState invokeState = new Controller.ExecuteCoreState
					{
						Controller = this,
						AsyncInvoker = asyncActionInvoker,
						ActionName = actionName
					};
					result = AsyncResultWrapper.Begin<Controller.ExecuteCoreState>(callback, state, beginDelegate, endDelegate, invokeState, Controller._executeCoreTag, -1, null);
				}
				else
				{
					Action action = delegate()
					{
						if (!invoker.InvokeAction(this.ControllerContext, actionName))
						{
							this.HandleUnknownAction(actionName);
						}
					};
					result = AsyncResultWrapper.BeginSynchronous(callback, state, action, Controller._executeCoreTag);
				}
			}
			catch
			{
				this.PossiblySaveTempData();
				throw;
			}
			return result;
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x0001338C File Offset: 0x0001158C
		protected virtual void EndExecute(IAsyncResult asyncResult)
		{
			AsyncResultWrapper.End(asyncResult, Controller._executeTag);
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x0001339C File Offset: 0x0001159C
		protected virtual void EndExecuteCore(IAsyncResult asyncResult)
		{
			try
			{
				AsyncResultWrapper.End(asyncResult, Controller._executeCoreTag);
			}
			finally
			{
				this.PossiblySaveTempData();
			}
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x000133D0 File Offset: 0x000115D0
		void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
		{
			this.OnActionExecuting(filterContext);
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x000133D9 File Offset: 0x000115D9
		void IActionFilter.OnActionExecuted(ActionExecutedContext filterContext)
		{
			this.OnActionExecuted(filterContext);
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x000133E2 File Offset: 0x000115E2
		void IAuthenticationFilter.OnAuthentication(AuthenticationContext filterContext)
		{
			this.OnAuthentication(filterContext);
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x000133EB File Offset: 0x000115EB
		void IAuthenticationFilter.OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
		{
			this.OnAuthenticationChallenge(filterContext);
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x000133F4 File Offset: 0x000115F4
		void IAuthorizationFilter.OnAuthorization(AuthorizationContext filterContext)
		{
			this.OnAuthorization(filterContext);
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x000133FD File Offset: 0x000115FD
		void IExceptionFilter.OnException(ExceptionContext filterContext)
		{
			this.OnException(filterContext);
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x00013406 File Offset: 0x00011606
		void IResultFilter.OnResultExecuting(ResultExecutingContext filterContext)
		{
			this.OnResultExecuting(filterContext);
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x0001340F File Offset: 0x0001160F
		void IResultFilter.OnResultExecuted(ResultExecutedContext filterContext)
		{
			this.OnResultExecuted(filterContext);
		}

		// Token: 0x040001E7 RID: 487
		private static readonly object _executeTag = new object();

		// Token: 0x040001E8 RID: 488
		private static readonly object _executeCoreTag = new object();

		// Token: 0x040001E9 RID: 489
		private readonly AsyncManager _asyncManager = new AsyncManager();

		// Token: 0x040001EA RID: 490
		private IActionInvoker _actionInvoker;

		// Token: 0x040001EB RID: 491
		private ModelBinderDictionary _binders;

		// Token: 0x040001EC RID: 492
		private RouteCollection _routeCollection;

		// Token: 0x040001ED RID: 493
		private ITempDataProvider _tempDataProvider;

		// Token: 0x040001EE RID: 494
		private ViewEngineCollection _viewEngineCollection;

		// Token: 0x040001EF RID: 495
		private IDependencyResolver _resolver;

		// Token: 0x02000104 RID: 260
		private struct ExecuteCoreState
		{
			// Token: 0x040001F5 RID: 501
			internal IAsyncActionInvoker AsyncInvoker;

			// Token: 0x040001F6 RID: 502
			internal Controller Controller;

			// Token: 0x040001F7 RID: 503
			internal string ActionName;
		}
	}
}
