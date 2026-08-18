using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Web.Mvc.Async;
using System.Web.Mvc.Properties;
using System.Web.Routing;
using System.Web.SessionState;
using Microsoft.Web.Infrastructure.DynamicValidationHelper;

namespace System.Web.Mvc
{
	// Token: 0x0200012A RID: 298
	public class MvcHandler : IHttpAsyncHandler, IHttpHandler, IRequiresSessionState
	{
		// Token: 0x060007D7 RID: 2007 RVA: 0x00015360 File Offset: 0x00013560
		public MvcHandler(RequestContext requestContext)
		{
			if (requestContext == null)
			{
				throw new ArgumentNullException("requestContext");
			}
			this.RequestContext = requestContext;
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x060007D8 RID: 2008 RVA: 0x0001537D File Offset: 0x0001357D
		// (set) Token: 0x060007D9 RID: 2009 RVA: 0x00015398 File Offset: 0x00013598
		internal ControllerBuilder ControllerBuilder
		{
			get
			{
				if (this._controllerBuilder == null)
				{
					this._controllerBuilder = ControllerBuilder.Current;
				}
				return this._controllerBuilder;
			}
			set
			{
				this._controllerBuilder = value;
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x060007DA RID: 2010 RVA: 0x000153A1 File Offset: 0x000135A1
		// (set) Token: 0x060007DB RID: 2011 RVA: 0x000153A8 File Offset: 0x000135A8
		public static bool DisableMvcResponseHeader { get; set; }

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x060007DC RID: 2012 RVA: 0x000153B0 File Offset: 0x000135B0
		protected virtual bool IsReusable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x060007DD RID: 2013 RVA: 0x000153B3 File Offset: 0x000135B3
		// (set) Token: 0x060007DE RID: 2014 RVA: 0x000153BB File Offset: 0x000135BB
		public RequestContext RequestContext { get; private set; }

		// Token: 0x060007DF RID: 2015 RVA: 0x000153C4 File Offset: 0x000135C4
		protected internal virtual void AddVersionHeader(HttpContextBase httpContext)
		{
			if (!MvcHandler.DisableMvcResponseHeader)
			{
				httpContext.Response.AppendHeader(MvcHandler.MvcVersionHeaderName, MvcHandler.MvcVersion);
			}
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x000153E4 File Offset: 0x000135E4
		protected virtual IAsyncResult BeginProcessRequest(HttpContext httpContext, AsyncCallback callback, object state)
		{
			HttpContextBase httpContext2 = new HttpContextWrapper(httpContext);
			return this.BeginProcessRequest(httpContext2, callback, state);
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x000154C8 File Offset: 0x000136C8
		protected internal virtual IAsyncResult BeginProcessRequest(HttpContextBase httpContext, AsyncCallback callback, object state)
		{
			IController controller;
			IControllerFactory factory;
			this.ProcessRequestInit(httpContext, out controller, out factory);
			IAsyncController asyncController = controller as IAsyncController;
			if (asyncController != null)
			{
				BeginInvokeDelegate<MvcHandler.ProcessRequestState> beginDelegate = delegate(AsyncCallback asyncCallback, object asyncState, MvcHandler.ProcessRequestState innerState)
				{
					IAsyncResult result;
					try
					{
						result = innerState.AsyncController.BeginExecute(innerState.RequestContext, asyncCallback, asyncState);
					}
					catch
					{
						innerState.ReleaseController();
						throw;
					}
					return result;
				};
				EndInvokeVoidDelegate<MvcHandler.ProcessRequestState> endDelegate = delegate(IAsyncResult asyncResult, MvcHandler.ProcessRequestState innerState)
				{
					try
					{
						innerState.AsyncController.EndExecute(asyncResult);
					}
					finally
					{
						innerState.ReleaseController();
					}
				};
				MvcHandler.ProcessRequestState invokeState = new MvcHandler.ProcessRequestState
				{
					AsyncController = asyncController,
					Factory = factory,
					RequestContext = this.RequestContext
				};
				SynchronizationContext synchronizationContext = SynchronizationContextUtil.GetSynchronizationContext();
				return AsyncResultWrapper.Begin<MvcHandler.ProcessRequestState>(callback, state, beginDelegate, endDelegate, invokeState, MvcHandler._processRequestTag, -1, synchronizationContext);
			}
			Action action = delegate()
			{
				try
				{
					controller.Execute(this.RequestContext);
				}
				finally
				{
					factory.ReleaseController(controller);
				}
			};
			return AsyncResultWrapper.BeginSynchronous(callback, state, action, MvcHandler._processRequestTag);
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x000155B8 File Offset: 0x000137B8
		protected internal virtual void EndProcessRequest(IAsyncResult asyncResult)
		{
			AsyncResultWrapper.End(asyncResult, MvcHandler._processRequestTag);
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x000155C5 File Offset: 0x000137C5
		private static string GetMvcVersionString()
		{
			return new AssemblyName(typeof(MvcHandler).Assembly.FullName).Version.ToString(2);
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x000155EC File Offset: 0x000137EC
		protected virtual void ProcessRequest(HttpContext httpContext)
		{
			HttpContextBase httpContext2 = new HttpContextWrapper(httpContext);
			this.ProcessRequest(httpContext2);
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x00015608 File Offset: 0x00013808
		protected internal virtual void ProcessRequest(HttpContextBase httpContext)
		{
			IController controller;
			IControllerFactory controllerFactory;
			this.ProcessRequestInit(httpContext, out controller, out controllerFactory);
			try
			{
				controller.Execute(this.RequestContext);
			}
			finally
			{
				controllerFactory.ReleaseController(controller);
			}
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x00015648 File Offset: 0x00013848
		private void ProcessRequestInit(HttpContextBase httpContext, out IController controller, out IControllerFactory factory)
		{
			HttpContext httpContext2 = HttpContext.Current;
			if (httpContext2 != null && ValidationUtility.IsValidationEnabled(httpContext2) == true)
			{
				ValidationUtility.EnableDynamicValidation(httpContext2);
			}
			this.AddVersionHeader(httpContext);
			this.RemoveOptionalRoutingParameters();
			string requiredString = this.RequestContext.RouteData.GetRequiredString("controller");
			factory = this.ControllerBuilder.GetControllerFactory();
			controller = factory.CreateController(this.RequestContext, requiredString);
			if (controller == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, MvcResources.ControllerBuilder_FactoryReturnedNull, new object[]
				{
					factory.GetType(),
					requiredString
				}));
			}
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x00015708 File Offset: 0x00013908
		private void RemoveOptionalRoutingParameters()
		{
			RouteValueDictionary values = this.RequestContext.RouteData.Values;
			values.RemoveFromDictionary((KeyValuePair<string, object> entry) => entry.Value == UrlParameter.Optional);
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x060007E8 RID: 2024 RVA: 0x00015749 File Offset: 0x00013949
		bool IHttpHandler.IsReusable
		{
			get
			{
				return this.IsReusable;
			}
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x00015751 File Offset: 0x00013951
		void IHttpHandler.ProcessRequest(HttpContext httpContext)
		{
			this.ProcessRequest(httpContext);
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x0001575A File Offset: 0x0001395A
		IAsyncResult IHttpAsyncHandler.BeginProcessRequest(HttpContext context, AsyncCallback cb, object extraData)
		{
			return this.BeginProcessRequest(context, cb, extraData);
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x00015765 File Offset: 0x00013965
		void IHttpAsyncHandler.EndProcessRequest(IAsyncResult result)
		{
			this.EndProcessRequest(result);
		}

		// Token: 0x0400022E RID: 558
		private static readonly object _processRequestTag = new object();

		// Token: 0x0400022F RID: 559
		internal static readonly string MvcVersion = MvcHandler.GetMvcVersionString();

		// Token: 0x04000230 RID: 560
		public static readonly string MvcVersionHeaderName = "X-AspNetMvc-Version";

		// Token: 0x04000231 RID: 561
		private ControllerBuilder _controllerBuilder;

		// Token: 0x0200012B RID: 299
		private struct ProcessRequestState
		{
			// Token: 0x060007F0 RID: 2032 RVA: 0x0001578E File Offset: 0x0001398E
			internal void ReleaseController()
			{
				this.Factory.ReleaseController(this.AsyncController);
			}

			// Token: 0x04000237 RID: 567
			internal IAsyncController AsyncController;

			// Token: 0x04000238 RID: 568
			internal IControllerFactory Factory;

			// Token: 0x04000239 RID: 569
			internal RequestContext RequestContext;
		}
	}
}
