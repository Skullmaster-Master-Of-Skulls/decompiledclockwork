using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Security.Permissions;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Caching;
using System.Web.Compilation;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Instrumentation;
using System.Web.Management;
using System.Web.Profile;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.Util;
using System.Web.WebSockets;

namespace System.Web
{
	// Token: 0x0200008F RID: 143
	public sealed class HttpContext : IServiceProvider, IPrincipalContainer
	{
		// Token: 0x060008BE RID: 2238 RVA: 0x00013C04 File Offset: 0x00011E04
		private HttpContext.WebSocketInitStatus GetWebSocketInitStatus()
		{
			IIS7WorkerRequest iis7WorkerRequest = this._wr as IIS7WorkerRequest;
			if (iis7WorkerRequest == null)
			{
				return HttpContext.WebSocketInitStatus.RequiresIntegratedMode;
			}
			if (this.CurrentNotification <= RequestNotification.BeginRequest)
			{
				return HttpContext.WebSocketInitStatus.CannotCallFromBeginRequest;
			}
			if (!iis7WorkerRequest.IsWebSocketRequest())
			{
				if (iis7WorkerRequest.IsWebSocketModuleActive())
				{
					return HttpContext.WebSocketInitStatus.NotAWebSocketRequest;
				}
				return HttpContext.WebSocketInitStatus.NativeModuleNotEnabled;
			}
			else
			{
				if (iis7WorkerRequest.GetIsChildRequest())
				{
					return HttpContext.WebSocketInitStatus.CurrentRequestIsChildRequest;
				}
				return HttpContext.WebSocketInitStatus.Success;
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x060008BF RID: 2239 RVA: 0x00013C4C File Offset: 0x00011E4C
		public bool IsWebSocketRequest
		{
			get
			{
				if (this.IsWebSocketRequestUpgrading)
				{
					return true;
				}
				switch (this.GetWebSocketInitStatus())
				{
				case HttpContext.WebSocketInitStatus.Success:
					return true;
				case HttpContext.WebSocketInitStatus.RequiresIntegratedMode:
					throw new PlatformNotSupportedException(SR.GetString("Requires_Iis_Integrated_Mode"));
				case HttpContext.WebSocketInitStatus.CannotCallFromBeginRequest:
					throw new InvalidOperationException(SR.GetString("WebSockets_CannotBeCalledDuringBeginRequest"));
				default:
					return false;
				}
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x060008C0 RID: 2240 RVA: 0x00013CA1 File Offset: 0x00011EA1
		public bool IsWebSocketRequestUpgrading
		{
			get
			{
				return this.WebSocketTransitionState >= WebSocketTransitionState.AcceptWebSocketRequestCalled;
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x060008C1 RID: 2241 RVA: 0x00013CAF File Offset: 0x00011EAF
		internal bool HasWebSocketRequestTransitionStarted
		{
			get
			{
				return this.WebSocketTransitionState >= WebSocketTransitionState.TransitionStarted;
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x060008C2 RID: 2242 RVA: 0x00013CBD File Offset: 0x00011EBD
		internal bool HasWebSocketRequestTransitionCompleted
		{
			get
			{
				return this.WebSocketTransitionState >= WebSocketTransitionState.TransitionCompleted;
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x060008C3 RID: 2243 RVA: 0x00013CCB File Offset: 0x00011ECB
		// (set) Token: 0x060008C4 RID: 2244 RVA: 0x00013CD3 File Offset: 0x00011ED3
		internal WebSocketTransitionState WebSocketTransitionState
		{
			get
			{
				return this._webSocketTransitionState;
			}
			private set
			{
				this._webSocketTransitionState = value;
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x060008C5 RID: 2245 RVA: 0x00013CDC File Offset: 0x00011EDC
		public IList<string> WebSocketRequestedProtocols
		{
			get
			{
				if (this.IsWebSocketRequest)
				{
					if (this._webSocketRequestedProtocols == null)
					{
						string unknownRequestHeader = this._wr.GetUnknownRequestHeader("Sec-WebSocket-Protocol");
						IList<string> list = SubProtocolUtil.ParseHeader(unknownRequestHeader);
						this._webSocketRequestedProtocols = new ReadOnlyCollection<string>(list ?? new string[0]);
					}
					return this._webSocketRequestedProtocols;
				}
				return null;
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x060008C6 RID: 2246 RVA: 0x00013D2F File Offset: 0x00011F2F
		public string WebSocketNegotiatedProtocol
		{
			get
			{
				return this._webSocketNegotiatedProtocol;
			}
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x00013D37 File Offset: 0x00011F37
		public void AcceptWebSocketRequest(Func<AspNetWebSocketContext, Task> userFunc)
		{
			this.AcceptWebSocketRequest(userFunc, null);
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x00013D44 File Offset: 0x00011F44
		public void AcceptWebSocketRequest(Func<AspNetWebSocketContext, Task> userFunc, AspNetWebSocketOptions options)
		{
			if (userFunc == null)
			{
				throw new ArgumentNullException("userFunc");
			}
			if (this.IsWebSocketRequestUpgrading)
			{
				throw new InvalidOperationException(SR.GetString("WebSockets_AcceptWebSocketRequestCanOnlyBeCalledOnce"));
			}
			SynchronizationContextUtil.ValidateModeForWebSockets();
			switch (this.GetWebSocketInitStatus())
			{
			case HttpContext.WebSocketInitStatus.Success:
			{
				if (this.CurrentNotification > RequestNotification.ExecuteRequestHandler)
				{
					throw new InvalidOperationException(SR.GetString("WebSockets_CannotBeCalledAfterHandlerExecute"));
				}
				IIS7WorkerRequest iis7WorkerRequest = (IIS7WorkerRequest)this._wr;
				if (options != null && options.RequireSameOrigin && !WebSocketUtil.IsSameOriginRequest(iis7WorkerRequest))
				{
					throw new HttpException(403, SR.GetString("WebSockets_OriginCheckFailed"));
				}
				string text = null;
				if (options != null && !string.IsNullOrEmpty(options.SubProtocol))
				{
					text = options.SubProtocol;
				}
				if (text != null)
				{
					IList<string> webSocketRequestedProtocols = this.WebSocketRequestedProtocols;
					if (webSocketRequestedProtocols == null || !webSocketRequestedProtocols.Contains(text, StringComparer.Ordinal))
					{
						throw new ArgumentException(SR.GetString("WebSockets_SubProtocolCannotBeNegotiated", new object[]
						{
							text
						}), "options");
					}
				}
				iis7WorkerRequest.AcceptWebSocket();
				this.TransitionToWebSocketState(WebSocketTransitionState.AcceptWebSocketRequestCalled);
				this.Response.StatusCode = 101;
				if (text != null)
				{
					this.Response.AppendHeader("Sec-WebSocket-Protocol", text);
					this._webSocketNegotiatedProtocol = text;
				}
				this.RootedObjects.WebSocketPipeline = new WebSocketPipeline(this.RootedObjects, this, userFunc, text);
				return;
			}
			case HttpContext.WebSocketInitStatus.RequiresIntegratedMode:
				throw new PlatformNotSupportedException(SR.GetString("Requires_Iis_Integrated_Mode"));
			case HttpContext.WebSocketInitStatus.CannotCallFromBeginRequest:
				throw new InvalidOperationException(SR.GetString("WebSockets_CannotBeCalledDuringBeginRequest"));
			case HttpContext.WebSocketInitStatus.NativeModuleNotEnabled:
				throw new PlatformNotSupportedException(SR.GetString("WebSockets_WebSocketModuleNotEnabled"));
			case HttpContext.WebSocketInitStatus.NotAWebSocketRequest:
				throw new HttpException(400, SR.GetString("WebSockets_NotAWebSocketRequest"));
			case HttpContext.WebSocketInitStatus.CurrentRequestIsChildRequest:
				throw new InvalidOperationException(SR.GetString("WebSockets_CannotBeCalledDuringChildExecute"));
			default:
				throw new HttpException(SR.GetString("WebSockets_UnknownErrorWhileAccepting"));
			}
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x00013EFA File Offset: 0x000120FA
		internal void TransitionToWebSocketState(WebSocketTransitionState newState)
		{
			this.WebSocketTransitionState = newState;
			if (newState == WebSocketTransitionState.TransitionStarted)
			{
				this._threadWhichStartedWebSocketTransition = Thread.CurrentThread;
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x060008CA RID: 2250 RVA: 0x00013F12 File Offset: 0x00012112
		internal bool DidCurrentThreadStartWebSocketTransition
		{
			get
			{
				return this._threadWhichStartedWebSocketTransition == Thread.CurrentThread;
			}
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x00013F21 File Offset: 0x00012121
		internal void EnsureHasNotTransitionedToWebSocket()
		{
			if (this.HasWebSocketRequestTransitionCompleted)
			{
				throw new NotSupportedException(SR.GetString("WebSockets_MethodNotAvailableDuringWebSocketProcessing"));
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x060008CC RID: 2252 RVA: 0x00013F3B File Offset: 0x0001213B
		// (set) Token: 0x060008CD RID: 2253 RVA: 0x00013F43 File Offset: 0x00012143
		internal bool FirstRequest { get; set; }

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x060008CE RID: 2254 RVA: 0x00013F4C File Offset: 0x0001214C
		internal bool RequiresSessionState
		{
			get
			{
				switch (this.SessionStateBehavior)
				{
				case SessionStateBehavior.Required:
				case SessionStateBehavior.ReadOnly:
					return true;
				case SessionStateBehavior.Disabled:
					return false;
				}
				return this._requiresSessionStateFromHandler;
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x060008CF RID: 2255 RVA: 0x00013F84 File Offset: 0x00012184
		internal bool ReadOnlySessionState
		{
			get
			{
				switch (this.SessionStateBehavior)
				{
				case SessionStateBehavior.Required:
				case SessionStateBehavior.Disabled:
					return false;
				case SessionStateBehavior.ReadOnly:
					return true;
				}
				return this._readOnlySessionStateFromHandler;
			}
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x00013FBA File Offset: 0x000121BA
		public HttpContext(HttpRequest request, HttpResponse response)
		{
			this.Init(request, response);
			request.Context = this;
			response.Context = this;
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x00013FFC File Offset: 0x000121FC
		public HttpContext(HttpWorkerRequest wr)
		{
			this._wr = wr;
			this.Init(new HttpRequest(wr, this), new HttpResponse(wr, this));
			this._response.InitResponseWriter();
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x00014058 File Offset: 0x00012258
		internal HttpContext(HttpWorkerRequest wr, bool initResponseWriter)
		{
			this._wr = wr;
			this.Init(new HttpRequest(wr, this), new HttpResponse(wr, this));
			if (initResponseWriter)
			{
				this._response.InitResponseWriter();
			}
			PerfCounters.IncrementCounter(AppPerfCounter.REQUESTS_EXECUTING);
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x000140C0 File Offset: 0x000122C0
		private void Init(HttpRequest request, HttpResponse response)
		{
			this._request = request;
			this._response = response;
			this._utcTimestamp = DateTime.UtcNow;
			this._principalContainer = this;
			if (this._wr is IIS7WorkerRequest)
			{
				this._isIntegratedPipeline = true;
			}
			if (!(this._wr is StateHttpWorkerRequest))
			{
				this.CookielessHelper.RemoveCookielessValuesFromPath();
			}
			Profiler profile = HttpRuntime.Profile;
			if (profile != null && profile.IsEnabled)
			{
				this._topTraceContext = new TraceContext(this);
			}
			string eurl = this.GetEurl();
			if (!string.IsNullOrEmpty(eurl))
			{
				string path = request.Path;
				int num = path.Length - eurl.Length;
				bool flag = path[path.Length - 1] == '/';
				if (flag)
				{
					num--;
				}
				if (num >= 0 && StringUtil.Equals(path, num, eurl, 0, eurl.Length))
				{
					int num2 = num;
					if (flag)
					{
						num2++;
					}
					string virtualPath = path.Substring(0, num2);
					this.ConfigurationPath = null;
					this.Request.InternalRewritePath(VirtualPath.Create(virtualPath), null, true);
				}
			}
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x000141C0 File Offset: 0x000123C0
		private string GetEurl()
		{
			if (!(this._wr is ISAPIWorkerRequestInProcForIIS6) || this._wr is ISAPIWorkerRequestInProcForIIS7)
			{
				return null;
			}
			string text = HttpContext.s_eurl;
			if (text == null && !HttpContext.s_eurlSet)
			{
				try
				{
					IntPtr extensionlessUrlAppendage = UnsafeNativeMethods.GetExtensionlessUrlAppendage();
					if (extensionlessUrlAppendage != IntPtr.Zero)
					{
						text = StringUtil.StringFromWCharPtr(extensionlessUrlAppendage, UnsafeNativeMethods.lstrlenW(extensionlessUrlAppendage));
					}
				}
				catch
				{
				}
				HttpContext.s_eurl = text;
				HttpContext.s_eurlSet = true;
			}
			return text;
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x060008D5 RID: 2261 RVA: 0x00014240 File Offset: 0x00012440
		// (set) Token: 0x060008D6 RID: 2262 RVA: 0x0001424C File Offset: 0x0001244C
		public static HttpContext Current
		{
			get
			{
				return ContextBase.Current as HttpContext;
			}
			set
			{
				ContextBase.Current = value;
			}
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x00014254 File Offset: 0x00012454
		internal void Root()
		{
			this._rootedPtr = GCUtil.RootObject(this);
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x00014262 File Offset: 0x00012462
		internal void Unroot()
		{
			GCUtil.UnrootObject(this._rootedPtr);
			this._rootedPtr = IntPtr.Zero;
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x0001427B File Offset: 0x0001247B
		internal void FinishPipelineRequest()
		{
			if (!this._finishPipelineRequestCalled)
			{
				this._finishPipelineRequestCalled = true;
				HttpRuntime.FinishPipelineRequest(this);
			}
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x00014292 File Offset: 0x00012492
		public ISubscriptionToken AddOnRequestCompleted(Action<HttpContext> callback)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			return this._requestCompletedQueue.Enqueue(callback);
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x000142B0 File Offset: 0x000124B0
		internal void RaiseOnRequestCompleted()
		{
			try
			{
				this._requestCompletedQueue.FireAndComplete(delegate(Action<HttpContext> action)
				{
					action(this);
				});
			}
			catch (Exception e)
			{
				WebBaseEvent.RaiseRuntimeError(e, this);
			}
			finally
			{
				this.DisposeTimedOutToken();
			}
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x00014304 File Offset: 0x00012504
		public ISubscriptionToken DisposeOnPipelineCompleted(IDisposable target)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			if (this.RootedObjects != null)
			{
				return this.RootedObjects.DisposeOnPipelineCompleted(target);
			}
			return this._pipelineCompletedQueue.Enqueue(target);
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x00014338 File Offset: 0x00012538
		internal void RaiseOnPipelineCompleted()
		{
			try
			{
				this._pipelineCompletedQueue.FireAndComplete(delegate(IDisposable disposable)
				{
					disposable.Dispose();
				});
			}
			catch (Exception e)
			{
				WebBaseEvent.RaiseRuntimeError(e, null);
			}
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x0001438C File Offset: 0x0001258C
		internal void ValidatePath()
		{
			CachedPathData configurationPathData = this.GetConfigurationPathData();
			configurationPathData.ValidatePath(this._request.PhysicalPathInternal);
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x000143B4 File Offset: 0x000125B4
		object IServiceProvider.GetService(Type service)
		{
			object result;
			if (service == typeof(HttpWorkerRequest))
			{
				InternalSecurityPermissions.UnmanagedCode.Demand();
				result = this._wr;
			}
			else if (service == typeof(HttpRequest))
			{
				result = this.Request;
			}
			else if (service == typeof(HttpResponse))
			{
				result = this.Response;
			}
			else if (service == typeof(HttpApplication))
			{
				result = this.ApplicationInstance;
			}
			else if (service == typeof(HttpApplicationState))
			{
				result = this.Application;
			}
			else if (service == typeof(HttpSessionState))
			{
				result = this.Session;
			}
			else if (service == typeof(HttpServerUtility))
			{
				result = this.Server;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x060008E0 RID: 2272 RVA: 0x00014491 File Offset: 0x00012691
		// (set) Token: 0x060008E1 RID: 2273 RVA: 0x00014499 File Offset: 0x00012699
		internal IHttpAsyncHandler AsyncAppHandler
		{
			get
			{
				return this._asyncAppHandler;
			}
			set
			{
				this._asyncAppHandler = value;
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x060008E2 RID: 2274 RVA: 0x000144A2 File Offset: 0x000126A2
		// (set) Token: 0x060008E3 RID: 2275 RVA: 0x000144CF File Offset: 0x000126CF
		public AsyncPreloadModeFlags AsyncPreloadMode
		{
			get
			{
				if (!this._asyncPreloadModeFlagsSet)
				{
					this._asyncPreloadModeFlags = RuntimeConfig.GetConfig(this).HttpRuntime.AsyncPreloadMode;
					this._asyncPreloadModeFlagsSet = true;
				}
				return this._asyncPreloadModeFlags;
			}
			set
			{
				this._asyncPreloadModeFlags = value;
				this._asyncPreloadModeFlagsSet = true;
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x060008E4 RID: 2276 RVA: 0x000144DF File Offset: 0x000126DF
		// (set) Token: 0x060008E5 RID: 2277 RVA: 0x000144EC File Offset: 0x000126EC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool AllowAsyncDuringSyncStages
		{
			get
			{
				return this.SyncContext.AllowAsyncDuringSyncStages;
			}
			set
			{
				this.SyncContext.AllowAsyncDuringSyncStages = value;
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x060008E6 RID: 2278 RVA: 0x000144FA File Offset: 0x000126FA
		// (set) Token: 0x060008E7 RID: 2279 RVA: 0x00014504 File Offset: 0x00012704
		public HttpApplication ApplicationInstance
		{
			get
			{
				return this._appInstance;
			}
			set
			{
				if (this._isIntegratedPipeline && this._appInstance != null && value != null)
				{
					throw new InvalidOperationException(SR.GetString("Application_instance_cannot_be_changed"));
				}
				this._appInstance = value;
				if (this._isIntegratedPipeline)
				{
					IAllocatorProvider allocatorProvider = (this._appInstance != null) ? this._appInstance.AllocatorProvider : null;
					this._response.SetAllocatorProvider(allocatorProvider);
					((IIS7WorkerRequest)this._wr).AllocatorProvider = allocatorProvider;
				}
			}
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x060008E8 RID: 2280 RVA: 0x00014577 File Offset: 0x00012777
		public HttpApplicationState Application
		{
			get
			{
				return HttpApplicationFactory.ApplicationState;
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x060008E9 RID: 2281 RVA: 0x0001457E File Offset: 0x0001277E
		// (set) Token: 0x060008EA RID: 2282 RVA: 0x00014586 File Offset: 0x00012786
		internal bool DisableCustomHttpEncoder { get; set; }

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x060008EB RID: 2283 RVA: 0x0001458F File Offset: 0x0001278F
		// (set) Token: 0x060008EC RID: 2284 RVA: 0x00014598 File Offset: 0x00012798
		public IHttpHandler Handler
		{
			get
			{
				return this._handler;
			}
			set
			{
				this._handler = value;
				this._requiresSessionStateFromHandler = false;
				this._readOnlySessionStateFromHandler = false;
				this.InAspCompatMode = false;
				if (this._handler != null)
				{
					if (this._handler is IRequiresSessionState)
					{
						this._requiresSessionStateFromHandler = true;
					}
					if (this._handler is IReadOnlySessionState)
					{
						this._readOnlySessionStateFromHandler = true;
					}
					Page page = this._handler as Page;
					if (page != null && page.IsInAspCompatMode)
					{
						this.InAspCompatMode = true;
					}
				}
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x060008ED RID: 2285 RVA: 0x0001460F File Offset: 0x0001280F
		public IHttpHandler PreviousHandler
		{
			get
			{
				if (this._handlerStack == null || this._handlerStack.Count == 0)
				{
					return null;
				}
				return (IHttpHandler)this._handlerStack.Peek();
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x060008EE RID: 2286 RVA: 0x00014638 File Offset: 0x00012838
		public IHttpHandler CurrentHandler
		{
			get
			{
				if (this._currentHandler == null)
				{
					this._currentHandler = this._handler;
				}
				return this._currentHandler;
			}
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x00014654 File Offset: 0x00012854
		internal void RestoreCurrentHandler()
		{
			this._currentHandler = (IHttpHandler)this._handlerStack.Pop();
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x0001466C File Offset: 0x0001286C
		internal void SetCurrentHandler(IHttpHandler newtHandler)
		{
			if (this._handlerStack == null)
			{
				this._handlerStack = new Stack();
			}
			this._handlerStack.Push(this.CurrentHandler);
			this._currentHandler = newtHandler;
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x0001469C File Offset: 0x0001289C
		public void RemapHandler(IHttpHandler handler)
		{
			this.EnsureHasNotTransitionedToWebSocket();
			IIS7WorkerRequest iis7WorkerRequest = this._wr as IIS7WorkerRequest;
			if (iis7WorkerRequest != null)
			{
				if (this._notificationContext.CurrentNotification >= RequestNotification.MapRequestHandler)
				{
					throw new InvalidOperationException(SR.GetString("Invoke_before_pipeline_event", new object[]
					{
						"HttpContext.RemapHandler",
						"HttpApplication.MapRequestHandler"
					}));
				}
				string handlerType = null;
				string handlerName = null;
				if (handler != null)
				{
					Type type = handler.GetType();
					handlerType = type.AssemblyQualifiedName;
					handlerName = type.FullName;
				}
				iis7WorkerRequest.SetRemapHandler(handlerType, handlerName);
			}
			this._remapHandler = handler;
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x060008F2 RID: 2290 RVA: 0x00014720 File Offset: 0x00012920
		internal IHttpHandler RemapHandlerInstance
		{
			get
			{
				return this._remapHandler;
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x060008F3 RID: 2291 RVA: 0x00014728 File Offset: 0x00012928
		public HttpRequest Request
		{
			get
			{
				if (this.HideRequestResponse)
				{
					throw new HttpException(SR.GetString("Request_not_available"));
				}
				return this._request;
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x060008F4 RID: 2292 RVA: 0x00014748 File Offset: 0x00012948
		public HttpResponse Response
		{
			get
			{
				if (this.HideRequestResponse || this.HasWebSocketRequestTransitionCompleted)
				{
					throw new HttpException(SR.GetString("Response_not_available"));
				}
				return this._response;
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x060008F5 RID: 2293 RVA: 0x00014770 File Offset: 0x00012970
		internal IHttpHandler TopHandler
		{
			get
			{
				if (this._handlerStack == null)
				{
					return this._handler;
				}
				object[] array = this._handlerStack.ToArray();
				if (array == null || array.Length == 0)
				{
					return this._handler;
				}
				return (IHttpHandler)array[array.Length - 1];
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x060008F6 RID: 2294 RVA: 0x000147B2 File Offset: 0x000129B2
		public TraceContext Trace
		{
			get
			{
				if (this._topTraceContext == null)
				{
					this._topTraceContext = new TraceContext(this);
				}
				return this._topTraceContext;
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x060008F7 RID: 2295 RVA: 0x000147CE File Offset: 0x000129CE
		// (set) Token: 0x060008F8 RID: 2296 RVA: 0x000147E5 File Offset: 0x000129E5
		internal bool TraceIsEnabled
		{
			get
			{
				return this._topTraceContext != null && this._topTraceContext.IsEnabled;
			}
			set
			{
				if (value)
				{
					this._topTraceContext = new TraceContext(this);
				}
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x060008F9 RID: 2297 RVA: 0x000147F6 File Offset: 0x000129F6
		public IDictionary Items
		{
			get
			{
				if (this._items == null)
				{
					this._items = new Hashtable();
				}
				return this._items;
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x060008FA RID: 2298 RVA: 0x00014814 File Offset: 0x00012A14
		public HttpSessionState Session
		{
			get
			{
				if (this.HasWebSocketRequestTransitionCompleted)
				{
					return null;
				}
				if (this._delayedSessionState)
				{
					lock (this)
					{
						if (this._delayedSessionState)
						{
							this._sessionStateModule.InitStateStoreItem(true);
							this._delayedSessionState = false;
						}
					}
				}
				return (HttpSessionState)this.Items["AspSession"];
			}
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x00014894 File Offset: 0x00012A94
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal void EnsureSessionStateIfNecessary()
		{
			if (this._sessionStateModule == null)
			{
				return;
			}
			HttpSessionState httpSessionState = (HttpSessionState)this.Items["AspSession"];
			if (httpSessionState != null && httpSessionState.Count > 0 && !string.IsNullOrEmpty(httpSessionState.SessionID))
			{
				this._sessionStateModule.EnsureStateStoreItemLocked();
			}
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x000148E8 File Offset: 0x00012AE8
		internal void AddHttpSessionStateModule(SessionStateModule module, bool delayed)
		{
			if (this._sessionStateModule != null && this._sessionStateModule != module)
			{
				throw new HttpException(SR.GetString("Cant_have_multiple_session_module"));
			}
			this._sessionStateModule = module;
			this._delayedSessionState = delayed;
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x00014921 File Offset: 0x00012B21
		internal void RemoveHttpSessionStateModule()
		{
			this._delayedSessionState = false;
			this._sessionStateModule = null;
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x060008FE RID: 2302 RVA: 0x00014935 File Offset: 0x00012B35
		public HttpServerUtility Server
		{
			get
			{
				if (this._server == null)
				{
					this._server = new HttpServerUtility(this);
				}
				return this._server;
			}
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x00014954 File Offset: 0x00012B54
		internal void ReportRuntimeErrorIfExists(ref RequestNotificationStatus status)
		{
			Exception error = this.Error;
			if (error == null || this._runtimeErrorReported)
			{
				return;
			}
			if (this._notificationContext != null && this.CurrentModuleIndex == -1)
			{
				try
				{
					IIS7WorkerRequest iis7WorkerRequest = this._wr as IIS7WorkerRequest;
					if (this.Request.QueryString["aspxerrorpath"] != null && iis7WorkerRequest != null && string.IsNullOrEmpty(iis7WorkerRequest.GetManagedHandlerType()) && iis7WorkerRequest.GetCurrentModuleName() == "AspNetInitializationExceptionModule")
					{
						status = RequestNotificationStatus.Continue;
						return;
					}
				}
				catch
				{
				}
			}
			this._runtimeErrorReported = true;
			if (HttpRuntime.AppOfflineMessage != null)
			{
				try
				{
					this.Response.TrySkipIisCustomErrors = true;
					HttpRuntime.ReportAppOfflineErrorMessage(this.Response, HttpRuntime.AppOfflineMessage);
					goto IL_F7;
				}
				catch
				{
					goto IL_F7;
				}
			}
			using (new DisposableHttpContextWrapper(this))
			{
				this.DisableCustomHttpEncoder = true;
				using (new ApplicationImpersonationContext())
				{
					try
					{
						try
						{
							this.Response.ReportRuntimeError(error, true, false);
						}
						catch (Exception e)
						{
							this.Response.ReportRuntimeError(e, false, false);
						}
					}
					catch (Exception)
					{
					}
				}
			}
			IL_F7:
			status = RequestNotificationStatus.FinishRequest;
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000900 RID: 2304 RVA: 0x00014AA8 File Offset: 0x00012CA8
		public Exception Error
		{
			get
			{
				if (this._tempError != null)
				{
					return this._tempError;
				}
				if (this._errors == null || this._errors.Count == 0 || this._errorCleared)
				{
					return null;
				}
				return (Exception)this._errors[0];
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000901 RID: 2305 RVA: 0x00014AF4 File Offset: 0x00012CF4
		// (set) Token: 0x06000902 RID: 2306 RVA: 0x00014AFC File Offset: 0x00012CFC
		internal Exception TempError
		{
			get
			{
				return this._tempError;
			}
			set
			{
				this._tempError = value;
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000903 RID: 2307 RVA: 0x00014B08 File Offset: 0x00012D08
		public Exception[] AllErrors
		{
			get
			{
				int num = (this._errors != null) ? this._errors.Count : 0;
				if (num == 0)
				{
					return null;
				}
				Exception[] array = new Exception[num];
				this._errors.CopyTo(0, array, 0, num);
				return array;
			}
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x00014B48 File Offset: 0x00012D48
		public void AddError(Exception errorInfo)
		{
			if (this._errors == null)
			{
				this._errors = new ArrayList();
			}
			this._errors.Add(errorInfo);
			if (this._isIntegratedPipeline && this._notificationContext != null)
			{
				this._notificationContext.Error = errorInfo;
			}
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x00014B95 File Offset: 0x00012D95
		public void ClearError()
		{
			if (this._tempError != null)
			{
				this._tempError = null;
			}
			else
			{
				this._errorCleared = true;
			}
			if (this._isIntegratedPipeline && this._notificationContext != null)
			{
				this._notificationContext.Error = null;
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000906 RID: 2310 RVA: 0x00014BCF File Offset: 0x00012DCF
		// (set) Token: 0x06000907 RID: 2311 RVA: 0x00014BDC File Offset: 0x00012DDC
		public IPrincipal User
		{
			get
			{
				return this._principalContainer.Principal;
			}
			[SecurityPermission(SecurityAction.Demand, ControlPrincipal = true)]
			set
			{
				this.SetPrincipalNoDemand(value);
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000908 RID: 2312 RVA: 0x00014BE5 File Offset: 0x00012DE5
		// (set) Token: 0x06000909 RID: 2313 RVA: 0x00014BED File Offset: 0x00012DED
		IPrincipal IPrincipalContainer.Principal { get; set; }

		// Token: 0x0600090A RID: 2314 RVA: 0x00014BF8 File Offset: 0x00012DF8
		internal void SetPrincipalNoDemand(IPrincipal principal, bool needToSetNativePrincipal)
		{
			this._principalContainer.Principal = principal;
			if (needToSetNativePrincipal && this._isIntegratedPipeline && this._notificationContext.CurrentNotification == RequestNotification.AuthenticateRequest)
			{
				IntPtr zero = IntPtr.Zero;
				IIS7WorkerRequest iis7WorkerRequest = (IIS7WorkerRequest)this._wr;
				iis7WorkerRequest.SetPrincipal(principal);
			}
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x00014C45 File Offset: 0x00012E45
		internal void SetPrincipalNoDemand(IPrincipal principal)
		{
			this.SetPrincipalNoDemand(principal, true);
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x0600090C RID: 2316 RVA: 0x00014C50 File Offset: 0x00012E50
		public ProfileBase Profile
		{
			get
			{
				if (this._Profile == null && this._ProfileDelayLoad)
				{
					this._Profile = ProfileBase.Create(this.Request.IsAuthenticated ? this.User.Identity.Name : this.Request.AnonymousID, this.Request.IsAuthenticated);
				}
				return this._Profile;
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x0600090D RID: 2317 RVA: 0x00014CB3 File Offset: 0x00012EB3
		// (set) Token: 0x0600090E RID: 2318 RVA: 0x00014CBB File Offset: 0x00012EBB
		internal SessionStateBehavior SessionStateBehavior { get; set; }

		// Token: 0x0600090F RID: 2319 RVA: 0x00014CC4 File Offset: 0x00012EC4
		public void SetSessionStateBehavior(SessionStateBehavior sessionStateBehavior)
		{
			if (this._notificationContext != null && this._notificationContext.CurrentNotification >= RequestNotification.AcquireRequestState)
			{
				throw new InvalidOperationException(SR.GetString("Invoke_before_pipeline_event", new object[]
				{
					"HttpContext.SetSessionStateBehavior",
					"HttpApplication.AcquireRequestState"
				}));
			}
			this.SessionStateBehavior = sessionStateBehavior;
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06000910 RID: 2320 RVA: 0x00014D19 File Offset: 0x00012F19
		// (set) Token: 0x06000911 RID: 2321 RVA: 0x00014D21 File Offset: 0x00012F21
		public bool SkipAuthorization
		{
			get
			{
				return this._skipAuthorization;
			}
			[SecurityPermission(SecurityAction.Demand, ControlPrincipal = true)]
			set
			{
				this.SetSkipAuthorizationNoDemand(value, false);
			}
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x00014D2B File Offset: 0x00012F2B
		internal void SetSkipAuthorizationNoDemand(bool value, bool managedOnly)
		{
			if (HttpRuntime.UseIntegratedPipeline && !managedOnly && value != this._skipAuthorization)
			{
				this._request.SetSkipAuthorization(value);
			}
			this._skipAuthorization = value;
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06000913 RID: 2323 RVA: 0x00014D53 File Offset: 0x00012F53
		// (set) Token: 0x06000914 RID: 2324 RVA: 0x00014D5B File Offset: 0x00012F5B
		internal RootedObjects RootedObjects
		{
			get
			{
				return this._rootedObjects;
			}
			set
			{
				this.SwitchPrincipalContainer(value);
				this._rootedObjects = value;
			}
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x00014D6C File Offset: 0x00012F6C
		private void SwitchPrincipalContainer(IPrincipalContainer newPrincipalContainer)
		{
			if (newPrincipalContainer == null)
			{
				newPrincipalContainer = this;
			}
			IPrincipal principal = this._principalContainer.Principal;
			newPrincipalContainer.Principal = principal;
			this._principalContainer = newPrincipalContainer;
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000916 RID: 2326 RVA: 0x00014D9C File Offset: 0x00012F9C
		public bool IsDebuggingEnabled
		{
			get
			{
				bool result;
				try
				{
					result = CompilationUtil.IsDebuggingEnabled(this);
				}
				catch
				{
					result = false;
				}
				return result;
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000917 RID: 2327 RVA: 0x00014DC8 File Offset: 0x00012FC8
		public bool IsCustomErrorEnabled
		{
			get
			{
				return CustomErrorsSection.GetSettings(this).CustomErrorsEnabled(this._request);
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000918 RID: 2328 RVA: 0x00014DDB File Offset: 0x00012FDB
		// (set) Token: 0x06000919 RID: 2329 RVA: 0x00014DE3 File Offset: 0x00012FE3
		internal TemplateControl TemplateControl
		{
			get
			{
				return this._templateControl;
			}
			set
			{
				this._templateControl = value;
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x0600091A RID: 2330 RVA: 0x00014DEC File Offset: 0x00012FEC
		public DateTime Timestamp
		{
			get
			{
				return this._utcTimestamp.ToLocalTime();
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x0600091B RID: 2331 RVA: 0x00014DF9 File Offset: 0x00012FF9
		internal DateTime UtcTimestamp
		{
			get
			{
				return this._utcTimestamp;
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x0600091C RID: 2332 RVA: 0x00014E01 File Offset: 0x00013001
		internal HttpWorkerRequest WorkerRequest
		{
			get
			{
				return this._wr;
			}
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x0600091D RID: 2333 RVA: 0x00014E09 File Offset: 0x00013009
		public Cache Cache
		{
			get
			{
				return HttpRuntime.Cache;
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x0600091E RID: 2334 RVA: 0x00014E10 File Offset: 0x00013010
		public PageInstrumentationService PageInstrumentation
		{
			get
			{
				if (this._pageInstrumentationService == null)
				{
					this._pageInstrumentationService = new PageInstrumentationService();
				}
				return this._pageInstrumentationService;
			}
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x0600091F RID: 2335 RVA: 0x00014E2B File Offset: 0x0001302B
		// (set) Token: 0x06000920 RID: 2336 RVA: 0x00014E52 File Offset: 0x00013052
		internal VirtualPath ConfigurationPath
		{
			get
			{
				if (this._configurationPath == null)
				{
					this._configurationPath = this._request.FilePathObject;
				}
				return this._configurationPath;
			}
			set
			{
				this._configurationPath = value;
				this._configurationPathData = null;
				this._filePathData = null;
			}
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x00014E69 File Offset: 0x00013069
		internal CachedPathData GetFilePathData()
		{
			if (this._filePathData == null)
			{
				this._filePathData = CachedPathData.GetVirtualPathData(this._request.FilePathObject, false);
			}
			return this._filePathData;
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x00014E90 File Offset: 0x00013090
		internal CachedPathData GetConfigurationPathData()
		{
			if (this._configurationPath == null)
			{
				return this.GetFilePathData();
			}
			if (this._configurationPathData == null)
			{
				this._configurationPathData = CachedPathData.GetVirtualPathData(this._configurationPath, true);
			}
			return this._configurationPathData;
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x00014EC8 File Offset: 0x000130C8
		internal CachedPathData GetPathData(VirtualPath path)
		{
			if (path != null)
			{
				if (path.Equals(this._request.FilePathObject))
				{
					return this.GetFilePathData();
				}
				if (this._configurationPath != null && path.Equals(this._configurationPath))
				{
					return this.GetConfigurationPathData();
				}
			}
			return CachedPathData.GetVirtualPathData(path, false);
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x00014F22 File Offset: 0x00013122
		internal void FinishRequestForCachedPathData(int statusCode)
		{
			if (this._filePathData != null && !this._filePathData.CompletedFirstRequest)
			{
				if (400 <= statusCode && statusCode < 500)
				{
					CachedPathData.RemoveBadPathData(this._filePathData);
					return;
				}
				CachedPathData.MarkCompleted(this._filePathData);
			}
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x00014F60 File Offset: 0x00013160
		[Obsolete("The recommended alternative is System.Web.Configuration.WebConfigurationManager.GetWebApplicationSection in System.Web.dll. http://go.microsoft.com/fwlink/?linkid=14202")]
		public static object GetAppConfig(string name)
		{
			return WebConfigurationManager.GetWebApplicationSection(name);
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x00014F68 File Offset: 0x00013168
		[Obsolete("The recommended alternative is System.Web.HttpContext.GetSection in System.Web.dll. http://go.microsoft.com/fwlink/?linkid=14202")]
		public object GetConfig(string name)
		{
			return this.GetSection(name);
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x00014F71 File Offset: 0x00013171
		public object GetSection(string sectionName)
		{
			if (HttpConfigurationSystem.UseHttpConfigurationSystem)
			{
				return this.GetConfigurationPathData().ConfigRecord.GetSection(sectionName);
			}
			return ConfigurationManager.GetSection(sectionName);
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x00014F92 File Offset: 0x00013192
		internal RuntimeConfig GetRuntimeConfig()
		{
			return this.GetConfigurationPathData().RuntimeConfig;
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x00014F9F File Offset: 0x0001319F
		internal RuntimeConfig GetRuntimeConfig(VirtualPath path)
		{
			return this.GetPathData(path).RuntimeConfig;
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x00014FAD File Offset: 0x000131AD
		public void RewritePath(string path)
		{
			this.RewritePath(path, true);
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x00014FB8 File Offset: 0x000131B8
		public void RewritePath(string path, bool rebaseClientPath)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			string newQueryString = null;
			int num = path.IndexOf('?');
			if (num >= 0)
			{
				newQueryString = ((num < path.Length - 1) ? path.Substring(num + 1) : string.Empty);
				path = path.Substring(0, num);
			}
			VirtualPath virtualPath = VirtualPath.Create(path);
			virtualPath = this.Request.FilePathObject.Combine(virtualPath);
			virtualPath.FailIfNotWithinAppRoot();
			this.ConfigurationPath = null;
			this.Request.InternalRewritePath(virtualPath, newQueryString, rebaseClientPath);
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x0001503C File Offset: 0x0001323C
		public void RewritePath(string filePath, string pathInfo, string queryString)
		{
			this.RewritePath(VirtualPath.CreateAllowNull(filePath), VirtualPath.CreateAllowNull(pathInfo), queryString, false);
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x00015052 File Offset: 0x00013252
		public void RewritePath(string filePath, string pathInfo, string queryString, bool setClientFilePath)
		{
			this.RewritePath(VirtualPath.CreateAllowNull(filePath), VirtualPath.CreateAllowNull(pathInfo), queryString, setClientFilePath);
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x0001506C File Offset: 0x0001326C
		internal void RewritePath(VirtualPath filePath, VirtualPath pathInfo, string queryString, bool setClientFilePath)
		{
			this.EnsureHasNotTransitionedToWebSocket();
			if (filePath == null)
			{
				throw new ArgumentNullException("filePath");
			}
			filePath = this.Request.FilePathObject.Combine(filePath);
			filePath.FailIfNotWithinAppRoot();
			this.ConfigurationPath = null;
			this.Request.InternalRewritePath(filePath, pathInfo, queryString, setClientFilePath);
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x0600092F RID: 2351 RVA: 0x000150C3 File Offset: 0x000132C3
		// (set) Token: 0x06000930 RID: 2352 RVA: 0x000150CB File Offset: 0x000132CB
		internal CultureInfo DynamicCulture
		{
			get
			{
				return this._dynamicCulture;
			}
			set
			{
				this._dynamicCulture = value;
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000931 RID: 2353 RVA: 0x000150D4 File Offset: 0x000132D4
		// (set) Token: 0x06000932 RID: 2354 RVA: 0x000150DC File Offset: 0x000132DC
		internal CultureInfo DynamicUICulture
		{
			get
			{
				return this._dynamicUICulture;
			}
			set
			{
				this._dynamicUICulture = value;
			}
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x000150E5 File Offset: 0x000132E5
		public static object GetGlobalResourceObject(string classKey, string resourceKey)
		{
			return HttpContext.GetGlobalResourceObject(classKey, resourceKey, null);
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x000150EF File Offset: 0x000132EF
		public static object GetGlobalResourceObject(string classKey, string resourceKey, CultureInfo culture)
		{
			return ResourceExpressionBuilder.GetGlobalResourceObject(classKey, resourceKey, null, null, culture);
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x000150FB File Offset: 0x000132FB
		public static object GetLocalResourceObject(string virtualPath, string resourceKey)
		{
			return HttpContext.GetLocalResourceObject(virtualPath, resourceKey, null);
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x00015108 File Offset: 0x00013308
		public static object GetLocalResourceObject(string virtualPath, string resourceKey, CultureInfo culture)
		{
			IResourceProvider localResourceProvider = ResourceExpressionBuilder.GetLocalResourceProvider(VirtualPath.Create(virtualPath));
			return ResourceExpressionBuilder.GetResourceObject(localResourceProvider, resourceKey, culture);
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06000937 RID: 2359 RVA: 0x00015129 File Offset: 0x00013329
		// (set) Token: 0x06000938 RID: 2360 RVA: 0x00015131 File Offset: 0x00013331
		internal int ServerExecuteDepth
		{
			get
			{
				return this._serverExecuteDepth;
			}
			set
			{
				this._serverExecuteDepth = value;
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000939 RID: 2361 RVA: 0x0001513A File Offset: 0x0001333A
		// (set) Token: 0x0600093A RID: 2362 RVA: 0x00015142 File Offset: 0x00013342
		internal bool PreventPostback
		{
			get
			{
				return this._preventPostback;
			}
			set
			{
				this._preventPostback = value;
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x0600093B RID: 2363 RVA: 0x0001514B File Offset: 0x0001334B
		// (set) Token: 0x0600093C RID: 2364 RVA: 0x00015153 File Offset: 0x00013353
		internal Thread CurrentThread
		{
			get
			{
				return this._thread;
			}
			set
			{
				this._thread = value;
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x0600093D RID: 2365 RVA: 0x0001515C File Offset: 0x0001335C
		// (set) Token: 0x0600093E RID: 2366 RVA: 0x00015176 File Offset: 0x00013376
		internal TimeSpan Timeout
		{
			get
			{
				long value = this.EnsureTimeout();
				return TimeSpan.FromTicks(value);
			}
			set
			{
				Interlocked.Exchange(ref this._timeoutTicks, value.Ticks);
			}
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x0600093F RID: 2367 RVA: 0x0001518C File Offset: 0x0001338C
		internal CancellationToken TimedOutToken
		{
			get
			{
				CancellationTokenHelper cancellationTokenHelper = LazyInitializer.EnsureInitialized<CancellationTokenHelper>(ref this._timeoutCancellationTokenHelper, () => new CancellationTokenHelper(false));
				return cancellationTokenHelper.Token;
			}
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000940 RID: 2368 RVA: 0x000151CA File Offset: 0x000133CA
		// (set) Token: 0x06000941 RID: 2369 RVA: 0x000151D7 File Offset: 0x000133D7
		public bool ThreadAbortOnTimeout
		{
			get
			{
				return Volatile.Read(ref this._threadAbortOnTimeout);
			}
			set
			{
				Volatile.Write(ref this._threadAbortOnTimeout, value);
			}
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x000151E8 File Offset: 0x000133E8
		private void DisposeTimedOutToken()
		{
			CancellationTokenHelper cancellationTokenHelper = LazyInitializer.EnsureInitialized<CancellationTokenHelper>(ref this._timeoutCancellationTokenHelper, () => CancellationTokenHelper.StaticDisposed);
			cancellationTokenHelper.Dispose();
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x00015228 File Offset: 0x00013428
		internal long EnsureTimeout()
		{
			long num = Volatile.Read(ref this._timeoutTicks);
			if (num == -1L)
			{
				HttpRuntimeSection httpRuntime = RuntimeConfig.GetConfig(this).HttpRuntime;
				num = httpRuntime.ExecutionTimeout.Ticks;
				long num2 = Interlocked.CompareExchange(ref this._timeoutTicks, num, -1L);
				if (num2 != -1L)
				{
					num = num2;
				}
			}
			return num;
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000944 RID: 2372 RVA: 0x00015278 File Offset: 0x00013478
		// (set) Token: 0x06000945 RID: 2373 RVA: 0x00015280 File Offset: 0x00013480
		internal DoubleLink TimeoutLink
		{
			get
			{
				return this._timeoutLink;
			}
			set
			{
				this._timeoutLink = value;
			}
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x00015289 File Offset: 0x00013489
		internal void BeginCancellablePeriod()
		{
			if (Volatile.Read(ref this._timeoutStartTimeUtcTicks) == -1L)
			{
				this.SetStartTime();
			}
			Volatile.Write(ref this._timeoutState, 1);
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x000152AC File Offset: 0x000134AC
		internal void SetStartTime()
		{
			Interlocked.Exchange(ref this._timeoutStartTimeUtcTicks, DateTime.UtcNow.Ticks);
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x000152D2 File Offset: 0x000134D2
		internal void EndCancellablePeriod()
		{
			Interlocked.CompareExchange(ref this._timeoutState, 0, 1);
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x000152E2 File Offset: 0x000134E2
		internal void WaitForExceptionIfCancelled()
		{
			while (Volatile.Read(ref this._timeoutState) == -1)
			{
				Thread.Sleep(100);
			}
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x0600094A RID: 2378 RVA: 0x000152FB File Offset: 0x000134FB
		internal bool IsInCancellablePeriod
		{
			get
			{
				return Volatile.Read(ref this._timeoutState) == 1;
			}
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x0001530C File Offset: 0x0001350C
		internal Thread MustTimeout(DateTime utcNow)
		{
			if (this._utcTimestamp + this.Timeout < utcNow)
			{
				CancellationTokenHelper cancellationTokenHelper = LazyInitializer.EnsureInitialized<CancellationTokenHelper>(ref this._timeoutCancellationTokenHelper, () => new CancellationTokenHelper(true));
				cancellationTokenHelper.Cancel();
			}
			if (Volatile.Read(ref this._timeoutState) == 1 && this.ThreadAbortOnTimeout)
			{
				long num = Volatile.Read(ref this._timeoutStartTimeUtcTicks) + this.Timeout.Ticks;
				if (num < utcNow.Ticks)
				{
					try
					{
						if (CompilationUtil.IsDebuggingEnabled(this) || Debugger.IsAttached)
						{
							return null;
						}
					}
					catch
					{
						return null;
					}
					if (Interlocked.CompareExchange(ref this._timeoutState, -1, 1) == 1)
					{
						if (this._wr.IsInReadEntitySync)
						{
							this.AbortConnection();
						}
						return this._thread;
					}
				}
			}
			return null;
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x0600094C RID: 2380 RVA: 0x000153F8 File Offset: 0x000135F8
		internal bool HasTimeoutExpired
		{
			get
			{
				if (Volatile.Read(ref this._timeoutState) != 1 || !this.ThreadAbortOnTimeout)
				{
					return false;
				}
				long num = Volatile.Read(ref this._timeoutStartTimeUtcTicks) + this.Timeout.Ticks;
				if (num >= DateTime.UtcNow.Ticks)
				{
					return false;
				}
				try
				{
					if (CompilationUtil.IsDebuggingEnabled(this) || Debugger.IsAttached)
					{
						return false;
					}
				}
				catch
				{
					return false;
				}
				return true;
			}
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x00015478 File Offset: 0x00013678
		internal void InvokeCancellableCallback(WaitCallback callback, object state)
		{
			if (this.IsInCancellablePeriod)
			{
				callback(state);
				return;
			}
			try
			{
				this.BeginCancellablePeriod();
				try
				{
					callback(state);
				}
				finally
				{
					this.EndCancellablePeriod();
				}
				this.WaitForExceptionIfCancelled();
			}
			catch (ThreadAbortException ex)
			{
				if (ex.ExceptionState != null && ex.ExceptionState is HttpApplication.CancelModuleException && ((HttpApplication.CancelModuleException)ex.ExceptionState).Timeout)
				{
					Thread.ResetAbort();
					PerfCounters.IncrementCounter(AppPerfCounter.REQUESTS_TIMED_OUT);
					throw new HttpException(SR.GetString("Request_timed_out"), null, 3001);
				}
			}
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x0001551C File Offset: 0x0001371C
		internal void PushTraceContext()
		{
			if (this._traceContextStack == null)
			{
				this._traceContextStack = new Stack();
			}
			this._traceContextStack.Push(this._topTraceContext);
			if (this._topTraceContext != null)
			{
				TraceContext traceContext = new TraceContext(this);
				this._topTraceContext.CopySettingsTo(traceContext);
				this._topTraceContext = traceContext;
			}
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x0001556F File Offset: 0x0001376F
		internal void PopTraceContext()
		{
			this._topTraceContext = (TraceContext)this._traceContextStack.Pop();
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x00015587 File Offset: 0x00013787
		internal bool RequestRequiresAuthorization()
		{
			return this.User.Identity.IsAuthenticated && (FileAuthorizationModule.RequestRequiresAuthorization(this) || UrlAuthorizationModule.RequestRequiresAuthorization(this));
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x000155AD File Offset: 0x000137AD
		internal int CallISAPI(UnsafeNativeMethods.CallISAPIFunc iFunction, byte[] bufIn, byte[] bufOut)
		{
			if (this._wr == null || !(this._wr is ISAPIWorkerRequest))
			{
				throw new HttpException(SR.GetString("Cannot_call_ISAPI_functions"));
			}
			return ((ISAPIWorkerRequest)this._wr).CallISAPI(iFunction, bufIn, bufOut);
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x000155E7 File Offset: 0x000137E7
		internal void SendEmptyResponse()
		{
			if (this._wr != null && this._wr is ISAPIWorkerRequest)
			{
				((ISAPIWorkerRequest)this._wr).SendEmptyResponse();
			}
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06000953 RID: 2387 RVA: 0x0001560E File Offset: 0x0001380E
		internal CookielessHelperClass CookielessHelper
		{
			get
			{
				if (this._CookielessHelper == null)
				{
					this._CookielessHelper = new CookielessHelperClass(this);
				}
				return this._CookielessHelper;
			}
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x0001562A File Offset: 0x0001382A
		internal void ResetSqlDependencyCookie()
		{
			if (this._sqlDependencyCookie != null)
			{
				CallContext.LogicalSetData("MS.SqlDependencyCookie", this._sqlDependencyCookie);
			}
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x00015644 File Offset: 0x00013844
		internal void RemoveSqlDependencyCookie()
		{
			if (this._sqlDependencyCookie != null)
			{
				CallContext.LogicalSetData("MS.SqlDependencyCookie", null);
			}
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000956 RID: 2390 RVA: 0x00015659 File Offset: 0x00013859
		// (set) Token: 0x06000957 RID: 2391 RVA: 0x00015661 File Offset: 0x00013861
		internal string SqlDependencyCookie
		{
			get
			{
				return this._sqlDependencyCookie;
			}
			set
			{
				this._sqlDependencyCookie = value;
				CallContext.LogicalSetData("MS.SqlDependencyCookie", value);
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000958 RID: 2392 RVA: 0x00015675 File Offset: 0x00013875
		// (set) Token: 0x06000959 RID: 2393 RVA: 0x0001567F File Offset: 0x0001387F
		internal NotificationContext NotificationContext
		{
			get
			{
				return this._notificationContext;
			}
			set
			{
				this._notificationContext = value;
			}
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x0600095A RID: 2394 RVA: 0x0001568A File Offset: 0x0001388A
		// (set) Token: 0x0600095B RID: 2395 RVA: 0x000156B6 File Offset: 0x000138B6
		public RequestNotification CurrentNotification
		{
			get
			{
				this.EnsureHasNotTransitionedToWebSocket();
				if (!HttpRuntime.UseIntegratedPipeline)
				{
					throw new PlatformNotSupportedException(SR.GetString("Requires_Iis_Integrated_Mode"));
				}
				return this._notificationContext.CurrentNotification;
			}
			internal set
			{
				if (!HttpRuntime.UseIntegratedPipeline)
				{
					throw new PlatformNotSupportedException(SR.GetString("Requires_Iis_Integrated_Mode"));
				}
				this._notificationContext.CurrentNotification = value;
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x0600095C RID: 2396 RVA: 0x000156DD File Offset: 0x000138DD
		internal bool IsChangeInServerVars
		{
			get
			{
				return (this._notificationContext.CurrentNotificationFlags & 1) == 1;
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x0600095D RID: 2397 RVA: 0x000156F1 File Offset: 0x000138F1
		internal bool IsChangeInRequestHeaders
		{
			get
			{
				return (this._notificationContext.CurrentNotificationFlags & 2) == 2;
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x0600095E RID: 2398 RVA: 0x00015705 File Offset: 0x00013905
		internal bool IsChangeInResponseHeaders
		{
			get
			{
				return (this._notificationContext.CurrentNotificationFlags & 4) == 4;
			}
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x0600095F RID: 2399 RVA: 0x00015719 File Offset: 0x00013919
		internal bool IsChangeInResponseStatus
		{
			get
			{
				return (this._notificationContext.CurrentNotificationFlags & 128) == 128;
			}
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06000960 RID: 2400 RVA: 0x00015735 File Offset: 0x00013935
		internal bool IsChangeInUserPrincipal
		{
			get
			{
				return (this._notificationContext.CurrentNotificationFlags & 8) == 8;
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06000961 RID: 2401 RVA: 0x00015749 File Offset: 0x00013949
		internal bool IsRuntimeErrorReported
		{
			get
			{
				return this._runtimeErrorReported;
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06000962 RID: 2402 RVA: 0x00015751 File Offset: 0x00013951
		internal bool IsSendResponseHeaders
		{
			get
			{
				return (this._notificationContext.CurrentNotificationFlags & 16) == 16;
			}
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x00015768 File Offset: 0x00013968
		internal void SetImpersonationEnabled()
		{
			IdentitySection identity = RuntimeConfig.GetConfig(this).Identity;
			this._impersonationEnabled = (identity != null && identity.Impersonate);
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06000964 RID: 2404 RVA: 0x00015794 File Offset: 0x00013994
		internal bool UsesImpersonation
		{
			get
			{
				return (HttpRuntime.IsOnUNCShareInternal && HostingEnvironment.ApplicationIdentityToken != IntPtr.Zero) || (this._impersonationEnabled && (this.HasWebSocketRequestTransitionCompleted || (((this._notificationContext.CurrentNotification == RequestNotification.AuthenticateRequest && this._notificationContext.IsPostNotification) || this._notificationContext.CurrentNotification > RequestNotification.AuthenticateRequest) && this._notificationContext.CurrentNotification != RequestNotification.SendResponse)));
			}
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06000965 RID: 2405 RVA: 0x00015817 File Offset: 0x00013A17
		internal bool AreResponseHeadersSent
		{
			get
			{
				return (this._notificationContext.CurrentNotificationFlags & 32) == 32;
			}
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x00015830 File Offset: 0x00013A30
		internal bool NeedToInitializeApp()
		{
			bool flag = !this._isAppInitialized;
			if (flag)
			{
				this._isAppInitialized = true;
			}
			return flag;
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06000967 RID: 2407 RVA: 0x00015852 File Offset: 0x00013A52
		// (set) Token: 0x06000968 RID: 2408 RVA: 0x00015861 File Offset: 0x00013A61
		internal int CurrentNotificationFlags
		{
			get
			{
				return this._notificationContext.CurrentNotificationFlags;
			}
			set
			{
				this._notificationContext.CurrentNotificationFlags = value;
			}
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06000969 RID: 2409 RVA: 0x00015871 File Offset: 0x00013A71
		// (set) Token: 0x0600096A RID: 2410 RVA: 0x00015880 File Offset: 0x00013A80
		internal int CurrentModuleIndex
		{
			get
			{
				return this._notificationContext.CurrentModuleIndex;
			}
			set
			{
				this._notificationContext.CurrentModuleIndex = value;
			}
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x0600096B RID: 2411 RVA: 0x00015890 File Offset: 0x00013A90
		// (set) Token: 0x0600096C RID: 2412 RVA: 0x0001589F File Offset: 0x00013A9F
		internal int CurrentModuleEventIndex
		{
			get
			{
				return this._notificationContext.CurrentModuleEventIndex;
			}
			set
			{
				this._notificationContext.CurrentModuleEventIndex = value;
			}
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x000158B0 File Offset: 0x00013AB0
		internal void DisableNotifications(RequestNotification notifications, RequestNotification postNotifications)
		{
			IIS7WorkerRequest iis7WorkerRequest = this._wr as IIS7WorkerRequest;
			if (iis7WorkerRequest != null)
			{
				iis7WorkerRequest.DisableNotifications(notifications, postNotifications);
			}
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x0600096E RID: 2414 RVA: 0x000158D4 File Offset: 0x00013AD4
		// (set) Token: 0x0600096F RID: 2415 RVA: 0x00015900 File Offset: 0x00013B00
		public bool IsPostNotification
		{
			get
			{
				this.EnsureHasNotTransitionedToWebSocket();
				if (!HttpRuntime.UseIntegratedPipeline)
				{
					throw new PlatformNotSupportedException(SR.GetString("Requires_Iis_Integrated_Mode"));
				}
				return this._notificationContext.IsPostNotification;
			}
			internal set
			{
				if (!HttpRuntime.UseIntegratedPipeline)
				{
					throw new PlatformNotSupportedException(SR.GetString("Requires_Iis_Integrated_Mode"));
				}
				this._notificationContext.IsPostNotification = value;
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06000970 RID: 2416 RVA: 0x00015927 File Offset: 0x00013B27
		internal IntPtr ClientIdentityToken
		{
			get
			{
				if (this._wr != null)
				{
					return this._wr.GetUserToken();
				}
				return IntPtr.Zero;
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06000971 RID: 2417 RVA: 0x00015944 File Offset: 0x00013B44
		internal bool IsClientImpersonationConfigured
		{
			get
			{
				bool result;
				try
				{
					IdentitySection identity = RuntimeConfig.GetConfig(this).Identity;
					result = (identity != null && identity.Impersonate && identity.ImpersonateToken == IntPtr.Zero);
				}
				catch
				{
					result = false;
				}
				return result;
			}
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06000972 RID: 2418 RVA: 0x00015994 File Offset: 0x00013B94
		internal IntPtr ImpersonationToken
		{
			get
			{
				IntPtr result = HostingEnvironment.ApplicationIdentityToken;
				IdentitySection identity = RuntimeConfig.GetConfig(this).Identity;
				if (identity != null)
				{
					if (identity.Impersonate)
					{
						result = ((identity.ImpersonateToken != IntPtr.Zero) ? identity.ImpersonateToken : this.ClientIdentityToken);
					}
					else if (!HttpRuntime.IsOnUNCShareInternal)
					{
						result = IntPtr.Zero;
					}
				}
				return result;
			}
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06000973 RID: 2419 RVA: 0x000159EF File Offset: 0x00013BEF
		// (set) Token: 0x06000974 RID: 2420 RVA: 0x00015A0B File Offset: 0x00013C0B
		internal AspNetSynchronizationContextBase SyncContext
		{
			get
			{
				if (this._syncContext == null)
				{
					this._syncContext = this.CreateNewAspNetSynchronizationContext();
				}
				return this._syncContext;
			}
			set
			{
				this._syncContext = value;
			}
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x00015A14 File Offset: 0x00013C14
		internal AspNetSynchronizationContextBase InstallNewAspNetSynchronizationContext()
		{
			AspNetSynchronizationContextBase syncContext = this._syncContext;
			if (syncContext != null && syncContext == AsyncOperationManager.SynchronizationContext)
			{
				this._syncContext = this.CreateNewAspNetSynchronizationContext();
				AsyncOperationManager.SynchronizationContext = this._syncContext;
				return syncContext;
			}
			return null;
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x00015A4D File Offset: 0x00013C4D
		private AspNetSynchronizationContextBase CreateNewAspNetSynchronizationContext()
		{
			if (!AppSettings.UseTaskFriendlySynchronizationContext)
			{
				return new LegacyAspNetSynchronizationContext(this.ApplicationInstance);
			}
			return new AspNetSynchronizationContext(this.ApplicationInstance);
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x00015A6D File Offset: 0x00013C6D
		internal void RestoreSavedAspNetSynchronizationContext(AspNetSynchronizationContextBase syncContext)
		{
			AsyncOperationManager.SynchronizationContext = syncContext;
			this._syncContext = syncContext;
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x00015A7C File Offset: 0x00013C7C
		internal string[] UserLanguagesFromContext()
		{
			if (this.Request == null)
			{
				return null;
			}
			return this.Request.UserLanguages;
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x00015A94 File Offset: 0x00013C94
		internal void ClearReferences()
		{
			this._appInstance = null;
			this._handler = null;
			this._handlerStack = null;
			this._currentHandler = null;
			this._remapHandler = null;
			if (this._isIntegratedPipeline)
			{
				if (!this.HasWebSocketRequestTransitionStarted)
				{
					this._items = null;
				}
				this._syncContext = null;
			}
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x00015AE2 File Offset: 0x00013CE2
		internal void CompleteTransitionToWebSocket()
		{
			this.ClearReferencesForWebSocketProcessing();
			this.TransitionToWebSocketState(WebSocketTransitionState.TransitionCompleted);
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x00015AF4 File Offset: 0x00013CF4
		private void ClearReferencesForWebSocketProcessing()
		{
			HttpResponse response = this._response;
			ReflectionUtil.Reset<HttpContext>(this);
			this._request.ClearReferencesForWebSocketProcessing();
			if (response != null)
			{
				ReflectionUtil.Reset<HttpResponse>(response);
			}
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x00015B24 File Offset: 0x00013D24
		internal CultureInfo CultureFromConfig(string configString, bool requireSpecific)
		{
			if (StringUtil.EqualsIgnoreCase(configString, HttpApplication.AutoCulture))
			{
				string[] array = this.UserLanguagesFromContext();
				if (array != null)
				{
					try
					{
						return CultureUtil.CreateReadOnlyCulture(array, requireSpecific);
					}
					catch
					{
						return null;
					}
				}
				return null;
			}
			if (StringUtil.StringStartsWithIgnoreCase(configString, "auto:"))
			{
				string[] array2 = this.UserLanguagesFromContext();
				if (array2 != null)
				{
					try
					{
						return CultureUtil.CreateReadOnlyCulture(array2, requireSpecific);
					}
					catch
					{
						return CultureUtil.CreateReadOnlyCulture(configString.Substring(5), requireSpecific);
					}
				}
				return CultureUtil.CreateReadOnlyCulture(configString.Substring(5), requireSpecific);
			}
			return CultureUtil.CreateReadOnlyCulture(configString, requireSpecific);
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x00015BC0 File Offset: 0x00013DC0
		private void AbortConnection()
		{
			IIS7WorkerRequest iis7WorkerRequest = this._wr as IIS7WorkerRequest;
			if (iis7WorkerRequest != null)
			{
				iis7WorkerRequest.AbortConnection();
				return;
			}
			this._wr.CloseConnection();
		}

		// Token: 0x04000331 RID: 817
		internal static readonly Assembly SystemWebAssembly = typeof(HttpContext).Assembly;

		// Token: 0x04000332 RID: 818
		private static volatile bool s_eurlSet;

		// Token: 0x04000333 RID: 819
		private static string s_eurl;

		// Token: 0x04000334 RID: 820
		private IHttpAsyncHandler _asyncAppHandler;

		// Token: 0x04000335 RID: 821
		private AsyncPreloadModeFlags _asyncPreloadModeFlags;

		// Token: 0x04000336 RID: 822
		private bool _asyncPreloadModeFlagsSet;

		// Token: 0x04000337 RID: 823
		private HttpApplication _appInstance;

		// Token: 0x04000338 RID: 824
		private IHttpHandler _handler;

		// Token: 0x04000339 RID: 825
		[DoNotReset]
		private HttpRequest _request;

		// Token: 0x0400033A RID: 826
		private HttpResponse _response;

		// Token: 0x0400033B RID: 827
		private HttpServerUtility _server;

		// Token: 0x0400033C RID: 828
		private Stack _traceContextStack;

		// Token: 0x0400033D RID: 829
		private TraceContext _topTraceContext;

		// Token: 0x0400033E RID: 830
		[DoNotReset]
		private Hashtable _items;

		// Token: 0x0400033F RID: 831
		private ArrayList _errors;

		// Token: 0x04000340 RID: 832
		private Exception _tempError;

		// Token: 0x04000341 RID: 833
		private bool _errorCleared;

		// Token: 0x04000342 RID: 834
		[DoNotReset]
		private IPrincipalContainer _principalContainer;

		// Token: 0x04000343 RID: 835
		[DoNotReset]
		internal ProfileBase _Profile;

		// Token: 0x04000344 RID: 836
		[DoNotReset]
		private DateTime _utcTimestamp;

		// Token: 0x04000345 RID: 837
		[DoNotReset]
		private HttpWorkerRequest _wr;

		// Token: 0x04000346 RID: 838
		private VirtualPath _configurationPath;

		// Token: 0x04000347 RID: 839
		internal bool _skipAuthorization;

		// Token: 0x04000348 RID: 840
		[DoNotReset]
		private CultureInfo _dynamicCulture;

		// Token: 0x04000349 RID: 841
		[DoNotReset]
		private CultureInfo _dynamicUICulture;

		// Token: 0x0400034A RID: 842
		private int _serverExecuteDepth;

		// Token: 0x0400034B RID: 843
		private Stack _handlerStack;

		// Token: 0x0400034C RID: 844
		private bool _preventPostback;

		// Token: 0x0400034D RID: 845
		private bool _runtimeErrorReported;

		// Token: 0x0400034E RID: 846
		private PageInstrumentationService _pageInstrumentationService;

		// Token: 0x0400034F RID: 847
		private ReadOnlyCollection<string> _webSocketRequestedProtocols;

		// Token: 0x04000350 RID: 848
		[DoNotReset]
		private CancellationTokenHelper _timeoutCancellationTokenHelper;

		// Token: 0x04000351 RID: 849
		private long _timeoutStartTimeUtcTicks = -1L;

		// Token: 0x04000352 RID: 850
		private long _timeoutTicks = -1L;

		// Token: 0x04000353 RID: 851
		private int _timeoutState;

		// Token: 0x04000354 RID: 852
		private DoubleLink _timeoutLink;

		// Token: 0x04000355 RID: 853
		private bool _threadAbortOnTimeout = true;

		// Token: 0x04000356 RID: 854
		private Thread _thread;

		// Token: 0x04000357 RID: 855
		private CachedPathData _configurationPathData;

		// Token: 0x04000358 RID: 856
		private CachedPathData _filePathData;

		// Token: 0x04000359 RID: 857
		private string _sqlDependencyCookie;

		// Token: 0x0400035A RID: 858
		private volatile SessionStateModule _sessionStateModule;

		// Token: 0x0400035B RID: 859
		private volatile bool _delayedSessionState;

		// Token: 0x0400035C RID: 860
		private TemplateControl _templateControl;

		// Token: 0x0400035D RID: 861
		private SubscriptionQueue<Action<HttpContext>> _requestCompletedQueue;

		// Token: 0x0400035E RID: 862
		[DoNotReset]
		private SubscriptionQueue<IDisposable> _pipelineCompletedQueue;

		// Token: 0x0400035F RID: 863
		private const int FLAG_NONE = 0;

		// Token: 0x04000360 RID: 864
		private const int FLAG_CHANGE_IN_SERVER_VARIABLES = 1;

		// Token: 0x04000361 RID: 865
		private const int FLAG_CHANGE_IN_REQUEST_HEADERS = 2;

		// Token: 0x04000362 RID: 866
		private const int FLAG_CHANGE_IN_RESPONSE_HEADERS = 4;

		// Token: 0x04000363 RID: 867
		private const int FLAG_CHANGE_IN_USER_OBJECT = 8;

		// Token: 0x04000364 RID: 868
		private const int FLAG_SEND_RESPONSE_HEADERS = 16;

		// Token: 0x04000365 RID: 869
		private const int FLAG_RESPONSE_HEADERS_SENT = 32;

		// Token: 0x04000366 RID: 870
		internal const int FLAG_ETW_PROVIDER_ENABLED = 64;

		// Token: 0x04000367 RID: 871
		private const int FLAG_CHANGE_IN_RESPONSE_STATUS = 128;

		// Token: 0x04000368 RID: 872
		private volatile NotificationContext _notificationContext;

		// Token: 0x04000369 RID: 873
		private bool _isAppInitialized;

		// Token: 0x0400036A RID: 874
		[DoNotReset]
		private bool _isIntegratedPipeline;

		// Token: 0x0400036B RID: 875
		private bool _finishPipelineRequestCalled;

		// Token: 0x0400036C RID: 876
		[DoNotReset]
		private bool _impersonationEnabled;

		// Token: 0x0400036D RID: 877
		internal bool HideRequestResponse;

		// Token: 0x0400036E RID: 878
		internal volatile bool InIndicateCompletion;

		// Token: 0x0400036F RID: 879
		internal volatile ThreadContext IndicateCompletionContext;

		// Token: 0x04000370 RID: 880
		internal volatile Thread ThreadInsideIndicateCompletion;

		// Token: 0x04000371 RID: 881
		[DoNotReset]
		internal readonly object ThreadContextId = new object();

		// Token: 0x04000372 RID: 882
		private AspNetSynchronizationContextBase _syncContext;

		// Token: 0x04000373 RID: 883
		internal Thread _threadWhichStartedWebSocketTransition;

		// Token: 0x04000374 RID: 884
		[DoNotReset]
		private WebSocketTransitionState _webSocketTransitionState;

		// Token: 0x04000375 RID: 885
		[DoNotReset]
		private string _webSocketNegotiatedProtocol;

		// Token: 0x04000377 RID: 887
		private bool _requiresSessionStateFromHandler;

		// Token: 0x04000378 RID: 888
		private bool _readOnlySessionStateFromHandler;

		// Token: 0x04000379 RID: 889
		internal bool InAspCompatMode;

		// Token: 0x0400037A RID: 890
		private IHttpHandler _remapHandler;

		// Token: 0x0400037B RID: 891
		private IntPtr _rootedPtr;

		// Token: 0x0400037D RID: 893
		private IHttpHandler _currentHandler;

		// Token: 0x0400037F RID: 895
		[DoNotReset]
		internal bool _ProfileDelayLoad;

		// Token: 0x04000381 RID: 897
		[DoNotReset]
		private RootedObjects _rootedObjects;

		// Token: 0x04000382 RID: 898
		private CookielessHelperClass _CookielessHelper;

		// Token: 0x020008E0 RID: 2272
		private enum WebSocketInitStatus
		{
			// Token: 0x0400363E RID: 13886
			Success,
			// Token: 0x0400363F RID: 13887
			RequiresIntegratedMode,
			// Token: 0x04003640 RID: 13888
			CannotCallFromBeginRequest,
			// Token: 0x04003641 RID: 13889
			NativeModuleNotEnabled,
			// Token: 0x04003642 RID: 13890
			NotAWebSocketRequest,
			// Token: 0x04003643 RID: 13891
			CurrentRequestIsChildRequest
		}
	}
}
