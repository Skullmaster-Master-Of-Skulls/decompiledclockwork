using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Caching;
using System.Web.Configuration;
using System.Web.Configuration.Common;
using System.Web.Hosting;
using System.Web.Management;
using System.Web.SessionState;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x0200007A RID: 122
	[ToolboxItem(false)]
	public class HttpApplication : IComponent, IDisposable, IHttpAsyncHandler, IHttpHandler, IRequestCompletedNotifier, ISyncContext
	{
		// Token: 0x170002FF RID: 767
		// (get) Token: 0x060006DD RID: 1757 RVA: 0x0000CB3C File Offset: 0x0000AD3C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public HttpContext Context
		{
			get
			{
				if (this._context == null)
				{
					return this._initContext;
				}
				return this._context;
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x060006DE RID: 1758 RVA: 0x0000CB53 File Offset: 0x0000AD53
		private bool IsContainerInitalizationAllowed
		{
			get
			{
				return HttpRuntime.UseIntegratedPipeline && HttpApplication._initSpecialCompleted && !this._initInternalCompleted;
			}
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x0000CB6E File Offset: 0x0000AD6E
		private void ThrowIfEventBindingDisallowed()
		{
			if (HttpRuntime.UseIntegratedPipeline && HttpApplication._initSpecialCompleted && this._initInternalCompleted)
			{
				throw new InvalidOperationException(SR.GetString("Event_Binding_Disallowed"));
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x060006E0 RID: 1760 RVA: 0x0000CB98 File Offset: 0x0000AD98
		private PipelineModuleStepContainer[] ModuleContainers
		{
			get
			{
				if (this._moduleContainers == null)
				{
					this._moduleContainers = new PipelineModuleStepContainer[HttpApplication._moduleIndexMap.Count];
					for (int i = 0; i < this._moduleContainers.Length; i++)
					{
						this._moduleContainers[i] = new PipelineModuleStepContainer();
					}
				}
				return this._moduleContainers;
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060006E1 RID: 1761 RVA: 0x0000CBE8 File Offset: 0x0000ADE8
		// (remove) Token: 0x060006E2 RID: 1762 RVA: 0x0000CBFB File Offset: 0x0000ADFB
		public event EventHandler Disposed
		{
			add
			{
				this.Events.AddHandler(HttpApplication.EventDisposed, value);
			}
			remove
			{
				this.Events.RemoveHandler(HttpApplication.EventDisposed, value);
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x060006E3 RID: 1763 RVA: 0x0000CC0E File Offset: 0x0000AE0E
		protected EventHandlerList Events
		{
			get
			{
				if (this._events == null)
				{
					this._events = new EventHandlerList();
				}
				return this._events;
			}
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x0000CC2C File Offset: 0x0000AE2C
		internal HttpApplication.IExecutionStep CreateImplicitAsyncPreloadExecutionStep()
		{
			ImplicitAsyncPreloadModule implicitAsyncPreloadModule = new ImplicitAsyncPreloadModule();
			BeginEventHandler beginHandler = null;
			EndEventHandler endHandler = null;
			implicitAsyncPreloadModule.GetEventHandlers(out beginHandler, out endHandler);
			return new HttpApplication.AsyncEventExecutionStep(this, beginHandler, endHandler, null);
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x060006E5 RID: 1765 RVA: 0x0000CC56 File Offset: 0x0000AE56
		private HttpApplication.AsyncAppEventHandlersTable AsyncEvents
		{
			get
			{
				if (this._asyncEvents == null)
				{
					this._asyncEvents = new HttpApplication.AsyncAppEventHandlersTable();
				}
				return this._asyncEvents;
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x060006E6 RID: 1766 RVA: 0x0000CC71 File Offset: 0x0000AE71
		internal Exception LastError
		{
			get
			{
				if (this._context == null)
				{
					return this._lastError;
				}
				return this._context.Error;
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x060006E7 RID: 1767 RVA: 0x0000CC8D File Offset: 0x0000AE8D
		internal byte[] EntityBuffer
		{
			get
			{
				if (this._entityBuffer == null)
				{
					this._entityBuffer = new byte[8192];
				}
				return this._entityBuffer;
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x060006E8 RID: 1768 RVA: 0x0000CCB0 File Offset: 0x0000AEB0
		internal IAllocatorProvider AllocatorProvider
		{
			get
			{
				if (this._allocator == null)
				{
					Interlocked.CompareExchange<IAllocatorProvider>(ref this._allocator, new AllocatorProvider
					{
						CharBufferAllocator = new SimpleBufferAllocator<char>(1024),
						IntBufferAllocator = new SimpleBufferAllocator<int>(128),
						IntPtrBufferAllocator = new SimpleBufferAllocator<IntPtr>(128)
					}, null);
				}
				return this._allocator;
			}
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x0000CD0F File Offset: 0x0000AF0F
		internal void ClearError()
		{
			this._lastError = null;
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x060006EA RID: 1770 RVA: 0x0000CD18 File Offset: 0x0000AF18
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public HttpRequest Request
		{
			get
			{
				HttpRequest httpRequest = null;
				if (this._context != null && !this._hideRequestResponse)
				{
					httpRequest = this._context.Request;
				}
				if (httpRequest == null)
				{
					throw new HttpException(SR.GetString("Request_not_available"));
				}
				return httpRequest;
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x060006EB RID: 1771 RVA: 0x0000CD58 File Offset: 0x0000AF58
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public HttpResponse Response
		{
			get
			{
				HttpResponse httpResponse = null;
				if (this._context != null && !this._hideRequestResponse)
				{
					httpResponse = this._context.Response;
				}
				if (httpResponse == null)
				{
					throw new HttpException(SR.GetString("Response_not_available"));
				}
				return httpResponse;
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x060006EC RID: 1772 RVA: 0x0000CD98 File Offset: 0x0000AF98
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public HttpSessionState Session
		{
			get
			{
				HttpSessionState httpSessionState = null;
				if (this._session != null)
				{
					httpSessionState = this._session;
				}
				else if (this._context != null)
				{
					httpSessionState = this._context.Session;
				}
				if (httpSessionState == null)
				{
					throw new HttpException(SR.GetString("Session_not_available"));
				}
				return httpSessionState;
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x060006ED RID: 1773 RVA: 0x0000CDE0 File Offset: 0x0000AFE0
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public HttpApplicationState Application
		{
			get
			{
				return this._state;
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x060006EE RID: 1774 RVA: 0x0000CDE8 File Offset: 0x0000AFE8
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public HttpServerUtility Server
		{
			get
			{
				if (this._context != null)
				{
					return this._context.Server;
				}
				return new HttpServerUtility(this);
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x060006EF RID: 1775 RVA: 0x0000CE04 File Offset: 0x0000B004
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IPrincipal User
		{
			get
			{
				if (this._context == null)
				{
					throw new HttpException(SR.GetString("User_not_available"));
				}
				return this._context.User;
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x060006F0 RID: 1776 RVA: 0x0000CE29 File Offset: 0x0000B029
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public HttpModuleCollection Modules
		{
			[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.High)]
			get
			{
				if (this._moduleCollection == null)
				{
					this._moduleCollection = new HttpModuleCollection();
				}
				return this._moduleCollection;
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x060006F1 RID: 1777 RVA: 0x0000CE44 File Offset: 0x0000B044
		// (set) Token: 0x060006F2 RID: 1778 RVA: 0x0000CE5F File Offset: 0x0000B05F
		internal EventArgs AppEvent
		{
			get
			{
				if (this._appEvent == null)
				{
					this._appEvent = EventArgs.Empty;
				}
				return this._appEvent;
			}
			set
			{
				this._appEvent = null;
			}
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x0000CE68 File Offset: 0x0000B068
		private ISessionStateModule FindISessionStateModule()
		{
			if (!HttpRuntime.UseIntegratedPipeline)
			{
				return null;
			}
			if (this._moduleCollection != null)
			{
				for (int i = 0; i < this._moduleCollection.Count; i++)
				{
					ISessionStateModule sessionStateModule = this._moduleCollection.Get(i) as ISessionStateModule;
					if (sessionStateModule != null)
					{
						return sessionStateModule;
					}
				}
			}
			return null;
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x0000CEB4 File Offset: 0x0000B0B4
		internal void EnsureReleaseState()
		{
			ISessionStateModule sessionStateModule = this.FindISessionStateModule();
			if (sessionStateModule != null)
			{
				sessionStateModule.ReleaseSessionState(this.Context);
			}
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x0000CED8 File Offset: 0x0000B0D8
		internal Task EnsureReleaseStateAsync()
		{
			ISessionStateModule sessionStateModule = this.FindISessionStateModule();
			if (sessionStateModule != null)
			{
				return sessionStateModule.ReleaseSessionStateAsync(this.Context);
			}
			return TaskAsyncHelper.CompletedTask;
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x0000CF01 File Offset: 0x0000B101
		public void CompleteRequest()
		{
			this._stepManager.CompleteRequest();
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x060006F7 RID: 1783 RVA: 0x0000CF0E File Offset: 0x0000B10E
		internal bool IsRequestCompleted
		{
			get
			{
				return this._stepManager != null && this._stepManager.IsCompleted;
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x060006F8 RID: 1784 RVA: 0x0000CF25 File Offset: 0x0000B125
		bool IRequestCompletedNotifier.IsRequestCompleted
		{
			get
			{
				return this.IsRequestCompleted;
			}
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x0000CF2D File Offset: 0x0000B12D
		internal void AcquireNotifcationContextLock(ref bool locked)
		{
			Monitor.Enter(this._stepManager, ref locked);
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x0000CF3B File Offset: 0x0000B13B
		internal void ReleaseNotifcationContextLock()
		{
			Monitor.Exit(this._stepManager);
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x0000CF48 File Offset: 0x0000B148
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void GetNotifcationContextPropertiesUnderLock(ref bool isReentry, ref int eventCount)
		{
			bool flag = false;
			try
			{
				this.AcquireNotifcationContextLock(ref flag);
				isReentry = this.Context.NotificationContext.IsReEntry;
				eventCount = this.CurrentModuleContainer.GetEventCount(this.Context.CurrentNotification, this.Context.IsPostNotification) - 1;
			}
			finally
			{
				if (flag)
				{
					this.ReleaseNotifcationContextLock();
				}
			}
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x0000CFB4 File Offset: 0x0000B1B4
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void GetNotifcationContextProperties(ref bool isReentry, ref int eventCount)
		{
			NotificationContext notificationContext = this.Context.NotificationContext;
			isReentry = notificationContext.IsReEntry;
			if (!isReentry)
			{
				eventCount = this.ModuleContainers[notificationContext.CurrentModuleIndex].GetEventCount(notificationContext.CurrentNotification, notificationContext.IsPostNotification) - 1;
				if (notificationContext == this.Context.NotificationContext)
				{
					return;
				}
			}
			this.GetNotifcationContextPropertiesUnderLock(ref isReentry, ref eventCount);
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x0000D014 File Offset: 0x0000B214
		private void RaiseOnError()
		{
			EventHandler eventHandler = (EventHandler)this.Events[HttpApplication.EventErrorRecorded];
			if (eventHandler != null)
			{
				try
				{
					eventHandler(this, this.AppEvent);
				}
				catch (Exception errorInfo)
				{
					if (this._context != null)
					{
						this._context.AddError(errorInfo);
					}
				}
			}
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x0000D070 File Offset: 0x0000B270
		private void RaiseOnRequestCompleted()
		{
			EventHandler eventHandler = (EventHandler)this.Events[HttpApplication.EventRequestCompleted];
			if (eventHandler != null)
			{
				try
				{
					eventHandler(this, this.AppEvent);
				}
				catch (Exception e)
				{
					WebBaseEvent.RaiseRuntimeError(e, this);
				}
			}
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x0000D0C0 File Offset: 0x0000B2C0
		internal void RaiseOnPreSendRequestHeaders()
		{
			EventHandler eventHandler = (EventHandler)this.Events[HttpApplication.EventPreSendRequestHeaders];
			if (eventHandler != null)
			{
				try
				{
					eventHandler(this, this.AppEvent);
				}
				catch (Exception error)
				{
					this.RecordError(error);
				}
			}
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x0000D110 File Offset: 0x0000B310
		internal void RaiseOnPreSendRequestContent()
		{
			EventHandler eventHandler = (EventHandler)this.Events[HttpApplication.EventPreSendRequestContent];
			if (eventHandler != null)
			{
				try
				{
					eventHandler(this, this.AppEvent);
				}
				catch (Exception error)
				{
					this.RecordError(error);
				}
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000701 RID: 1793 RVA: 0x0000D160 File Offset: 0x0000B360
		// (set) Token: 0x06000702 RID: 1794 RVA: 0x0000D18F File Offset: 0x0000B38F
		internal HttpAsyncResult AsyncResult
		{
			get
			{
				if (!HttpRuntime.UseIntegratedPipeline)
				{
					return this._ar;
				}
				if (this._context.NotificationContext == null)
				{
					return null;
				}
				return this._context.NotificationContext.AsyncResult;
			}
			set
			{
				if (HttpRuntime.UseIntegratedPipeline)
				{
					this._context.NotificationContext.AsyncResult = value;
					return;
				}
				this._ar = value;
			}
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x0000D1B1 File Offset: 0x0000B3B1
		internal void AddSyncEventHookup(object key, Delegate handler, RequestNotification notification)
		{
			this.AddSyncEventHookup(key, handler, notification, false);
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000704 RID: 1796 RVA: 0x0000D1BD File Offset: 0x0000B3BD
		private PipelineModuleStepContainer CurrentModuleContainer
		{
			get
			{
				return this.ModuleContainers[this._context.CurrentModuleIndex];
			}
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x0000D1D4 File Offset: 0x0000B3D4
		private PipelineModuleStepContainer GetModuleContainer(string moduleName)
		{
			object obj = HttpApplication._moduleIndexMap[moduleName];
			if (obj == null)
			{
				return null;
			}
			int num = (int)obj;
			return this.ModuleContainers[num];
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x0000D204 File Offset: 0x0000B404
		private void AddSyncEventHookup(object key, Delegate handler, RequestNotification notification, bool isPostNotification)
		{
			this.ThrowIfEventBindingDisallowed();
			this.Events.AddHandler(key, handler);
			if (this.IsContainerInitalizationAllowed)
			{
				PipelineModuleStepContainer moduleContainer = this.GetModuleContainer(this.CurrentModuleCollectionKey);
				if (moduleContainer != null)
				{
					HttpApplication.SyncEventExecutionStep step = new HttpApplication.SyncEventExecutionStep(this, (EventHandler)handler);
					moduleContainer.AddEvent(notification, isPostNotification, step);
				}
			}
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x0000D253 File Offset: 0x0000B453
		internal void RemoveSyncEventHookup(object key, Delegate handler, RequestNotification notification)
		{
			this.RemoveSyncEventHookup(key, handler, notification, false);
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x0000D260 File Offset: 0x0000B460
		internal void RemoveSyncEventHookup(object key, Delegate handler, RequestNotification notification, bool isPostNotification)
		{
			this.ThrowIfEventBindingDisallowed();
			this.Events.RemoveHandler(key, handler);
			if (this.IsContainerInitalizationAllowed)
			{
				PipelineModuleStepContainer moduleContainer = this.GetModuleContainer(this.CurrentModuleCollectionKey);
				if (moduleContainer != null)
				{
					moduleContainer.RemoveEvent(notification, isPostNotification, handler);
				}
			}
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x0000D2A4 File Offset: 0x0000B4A4
		private void AddSendResponseEventHookup(object key, Delegate handler)
		{
			this.ThrowIfEventBindingDisallowed();
			this.Events.AddHandler(key, handler);
			if (this.IsContainerInitalizationAllowed)
			{
				PipelineModuleStepContainer moduleContainer = this.GetModuleContainer(this.CurrentModuleCollectionKey);
				if (moduleContainer != null)
				{
					bool isHeaders = key == HttpApplication.EventPreSendRequestHeaders;
					HttpApplication.SendResponseExecutionStep step = new HttpApplication.SendResponseExecutionStep(this, (EventHandler)handler, isHeaders);
					moduleContainer.AddEvent(RequestNotification.SendResponse, false, step);
				}
			}
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x0000D300 File Offset: 0x0000B500
		private void RemoveSendResponseEventHookup(object key, Delegate handler)
		{
			this.ThrowIfEventBindingDisallowed();
			this.Events.RemoveHandler(key, handler);
			if (this.IsContainerInitalizationAllowed)
			{
				PipelineModuleStepContainer moduleContainer = this.GetModuleContainer(this.CurrentModuleCollectionKey);
				if (moduleContainer != null)
				{
					moduleContainer.RemoveEvent(RequestNotification.SendResponse, false, handler);
				}
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600070B RID: 1803 RVA: 0x0000D345 File Offset: 0x0000B545
		// (remove) Token: 0x0600070C RID: 1804 RVA: 0x0000D354 File Offset: 0x0000B554
		public event EventHandler BeginRequest
		{
			add
			{
				this.AddSyncEventHookup(HttpApplication.EventBeginRequest, value, RequestNotification.BeginRequest);
			}
			remove
			{
				this.RemoveSyncEventHookup(HttpApplication.EventBeginRequest, value, RequestNotification.BeginRequest);
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600070D RID: 1805 RVA: 0x0000D363 File Offset: 0x0000B563
		// (remove) Token: 0x0600070E RID: 1806 RVA: 0x0000D372 File Offset: 0x0000B572
		public event EventHandler AuthenticateRequest
		{
			add
			{
				this.AddSyncEventHookup(HttpApplication.EventAuthenticateRequest, value, RequestNotification.AuthenticateRequest);
			}
			remove
			{
				this.RemoveSyncEventHookup(HttpApplication.EventAuthenticateRequest, value, RequestNotification.AuthenticateRequest);
			}
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600070F RID: 1807 RVA: 0x0000D381 File Offset: 0x0000B581
		// (remove) Token: 0x06000710 RID: 1808 RVA: 0x0000D390 File Offset: 0x0000B590
		internal event EventHandler DefaultAuthentication
		{
			add
			{
				this.AddSyncEventHookup(HttpApplication.EventDefaultAuthentication, value, RequestNotification.AuthenticateRequest);
			}
			remove
			{
				this.RemoveSyncEventHookup(HttpApplication.EventDefaultAuthentication, value, RequestNotification.AuthenticateRequest);
			}
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000711 RID: 1809 RVA: 0x0000D39F File Offset: 0x0000B59F
		// (remove) Token: 0x06000712 RID: 1810 RVA: 0x0000D3AF File Offset: 0x0000B5AF
		public event EventHandler PostAuthenticateRequest
		{
			add
			{
				this.AddSyncEventHookup(HttpApplication.EventPostAuthenticateRequest, value, RequestNotification.AuthenticateRequest, true);
			}
			remove
			{
				this.RemoveSyncEventHookup(HttpApplication.EventPostAuthenticateRequest, value, RequestNotification.AuthenticateRequest, true);
			}
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000713 RID: 1811 RVA: 0x0000D3BF File Offset: 0x0000B5BF
		// (remove) Token: 0x06000714 RID: 1812 RVA: 0x0000D3CE File Offset: 0x0000B5CE
		public event EventHandler AuthorizeRequest
		{
			add
			{
				this.AddSyncEventHookup(HttpApplication.EventAuthorizeRequest, value, RequestNotification.AuthorizeRequest);
			}
			remove
			{
				this.RemoveSyncEventHookup(HttpApplication.EventAuthorizeRequest, value, RequestNotification.AuthorizeRequest);
			}
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000715 RID: 1813 RVA: 0x0000D3DD File Offset: 0x0000B5DD
		// (remove) Token: 0x06000716 RID: 1814 RVA: 0x0000D3ED File Offset: 0x0000B5ED
		public event EventHandler PostAuthorizeRequest
		{
			add
			{
				this.AddSyncEventHookup(HttpApplication.EventPostAuthorizeRequest, value, RequestNotification.AuthorizeRequest, true);
			}
			remove
			{
				this.RemoveSyncEventHookup(HttpApplication.EventPostAuthorizeRequest, value, RequestNotification.AuthorizeRequest, true);
			}
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000717 RID: 1815 RVA: 0x0000D3FD File Offset: 0x0000B5FD
		// (remove) Token: 0x06000718 RID: 1816 RVA: 0x0000D40C File Offset: 0x0000B60C
		public event EventHandler ResolveRequestCache
		{
			add
			{
				this.AddSyncEventHookup(HttpApplication.EventResolveRequestCache, value, RequestNotification.ResolveRequestCache);
			}
			remove
			{
				this.RemoveSyncEventHookup(HttpApplication.EventResolveRequestCache, value, RequestNotification.ResolveRequestCache);
			}
		}

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000719 RID: 1817 RVA: 0x0000D41B File Offset: 0x0000B61B
		// (remove) Token: 0x0600071A RID: 1818 RVA: 0x0000D42B File Offset: 0x0000B62B
		public event EventHandler PostResolveRequestCache
		{
			add
			{
				this.AddSyncEventHookup(HttpApplication.EventPostResolveRequestCache, value, RequestNotification.ResolveRequestCache, true);
			}
			remove
			{
				this.RemoveSyncEventHookup(HttpApplication.EventPostResolveRequestCache, value, RequestNotification.ResolveRequestCache, true);
			}
		}

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x0600071B RID: 1819 RVA: 0x0000D43B File Offset: 0x0000B63B
		// (remove) Token: 0x0600071C RID: 1820 RVA: 0x0000D462 File Offset: 0x0000B662
		public event EventHandler MapRequestHandler
		{
			add
			{
				if (!HttpRuntime.UseIntegratedPipeline)
				{
					throw new PlatformNotSupportedException(SR.GetString("Requires_Iis_Integrated_Mode"));
				}
				this.AddSyncEventHookup(HttpApplication.EventMapRequestHandler, value, RequestNotification.MapRequestHandler);
			}
			remove
			{
				if (!HttpRuntime.UseIntegratedPipeline)
				{
					throw new PlatformNotSupportedException(SR.GetString("Requires_Iis_Integrated_Mode"));
				}
				this.RemoveSyncEventHookup(HttpApplication.EventMapRequestHandler, value, RequestNotification.MapRequestHandler);
			}
		}

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x0600071D RID: 1821 RVA: 0x0000D489 File Offset: 0x0000B689
		// (remove) Token: 0x0600071E RID: 1822 RVA: 0x0000D49A File Offset: 0x0000B69A
		public event EventHandler PostMapRequestHandler
		{
			add
			{
				this.AddSyncEventHookup(HttpApplication.EventPostMapRequestHandler, value, RequestNotification.MapRequestHandler, true);
			}
			remove
			{
				this.RemoveSyncEventHookup(HttpApplication.EventPostMapRequestHandler, value, RequestNotification.MapRequestHandler);
			}
		}

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x0600071F RID: 1823 RVA: 0x0000D4AA File Offset: 0x0000B6AA
		// (remove) Token: 0x06000720 RID: 1824 RVA: 0x0000D4BA File Offset: 0x0000B6BA
		public event EventHandler AcquireRequestState
		{
			add
			{
				this.AddSyncEventHookup(HttpApplication.EventAcquireRequestState, value, RequestNotification.AcquireRequestState);
			}
			remove
			{
				this.RemoveSyncEventHookup(HttpApplication.EventAcquireRequestState, value, RequestNotification.AcquireRequestState);
			}
		}

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06000721 RID: 1825 RVA: 0x0000D4CA File Offset: 0x0000B6CA
		// (remove) Token: 0x06000722 RID: 1826 RVA: 0x0000D4DB File Offset: 0x0000B6DB
		public event EventHandler PostAcquireRequestState
		{
			add
			{
				this.AddSyncEventHookup(HttpApplication.EventPostAcquireRequestState, value, RequestNotification.AcquireRequestState, true);
			}
			remove
			{
				this.RemoveSyncEventHookup(HttpApplication.EventPostAcquireRequestState, value, RequestNotification.AcquireRequestState, true);
			}
		}

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x06000723 RID: 1827 RVA: 0x0000D4EC File Offset: 0x0000B6EC
		// (remove) Token: 0x06000724 RID: 1828 RVA: 0x0000D4FC File Offset: 0x0000B6FC
		public event EventHandler PreRequestHandlerExecute
		{
			add
			{
				this.AddSyncEventHookup(HttpApplication.EventPreRequestHandlerExecute, value, RequestNotification.PreExecuteRequestHandler);
			}
			remove
			{
				this.RemoveSyncEventHookup(HttpApplication.EventPreRequestHandlerExecute, value, RequestNotification.PreExecuteRequestHandler);
			}
		}

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x06000725 RID: 1829 RVA: 0x0000D50C File Offset: 0x0000B70C
		// (remove) Token: 0x06000726 RID: 1830 RVA: 0x0000D520 File Offset: 0x0000B720
		public event EventHandler PostRequestHandlerExecute
		{
			add
			{
				this.AddSyncEventHookup(HttpApplication.EventPostRequestHandlerExecute, value, RequestNotification.ExecuteRequestHandler, true);
			}
			remove
			{
				this.RemoveSyncEventHookup(HttpApplication.EventPostRequestHandlerExecute, value, RequestNotification.ExecuteRequestHandler, true);
			}
		}

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x06000727 RID: 1831 RVA: 0x0000D534 File Offset: 0x0000B734
		// (remove) Token: 0x06000728 RID: 1832 RVA: 0x0000D547 File Offset: 0x0000B747
		public event EventHandler ReleaseRequestState
		{
			add
			{
				this.AddSyncEventHookup(HttpApplication.EventReleaseRequestState, value, RequestNotification.ReleaseRequestState);
			}
			remove
			{
				this.RemoveSyncEventHookup(HttpApplication.EventReleaseRequestState, value, RequestNotification.ReleaseRequestState);
			}
		}

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x06000729 RID: 1833 RVA: 0x0000D55A File Offset: 0x0000B75A
		// (remove) Token: 0x0600072A RID: 1834 RVA: 0x0000D56E File Offset: 0x0000B76E
		public event EventHandler PostReleaseRequestState
		{
			add
			{
				this.AddSyncEventHookup(HttpApplication.EventPostReleaseRequestState, value, RequestNotification.ReleaseRequestState, true);
			}
			remove
			{
				this.RemoveSyncEventHookup(HttpApplication.EventPostReleaseRequestState, value, RequestNotification.ReleaseRequestState, true);
			}
		}

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x0600072B RID: 1835 RVA: 0x0000D582 File Offset: 0x0000B782
		// (remove) Token: 0x0600072C RID: 1836 RVA: 0x0000D595 File Offset: 0x0000B795
		public event EventHandler UpdateRequestCache
		{
			add
			{
				this.AddSyncEventHookup(HttpApplication.EventUpdateRequestCache, value, RequestNotification.UpdateRequestCache);
			}
			remove
			{
				this.RemoveSyncEventHookup(HttpApplication.EventUpdateRequestCache, value, RequestNotification.UpdateRequestCache);
			}
		}

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x0600072D RID: 1837 RVA: 0x0000D5A8 File Offset: 0x0000B7A8
		// (remove) Token: 0x0600072E RID: 1838 RVA: 0x0000D5BC File Offset: 0x0000B7BC
		public event EventHandler PostUpdateRequestCache
		{
			add
			{
				this.AddSyncEventHookup(HttpApplication.EventPostUpdateRequestCache, value, RequestNotification.UpdateRequestCache, true);
			}
			remove
			{
				this.RemoveSyncEventHookup(HttpApplication.EventPostUpdateRequestCache, value, RequestNotification.UpdateRequestCache, true);
			}
		}

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x0600072F RID: 1839 RVA: 0x0000D5D0 File Offset: 0x0000B7D0
		// (remove) Token: 0x06000730 RID: 1840 RVA: 0x0000D5FA File Offset: 0x0000B7FA
		public event EventHandler LogRequest
		{
			add
			{
				if (!HttpRuntime.UseIntegratedPipeline)
				{
					throw new PlatformNotSupportedException(SR.GetString("Requires_Iis_Integrated_Mode"));
				}
				this.AddSyncEventHookup(HttpApplication.EventLogRequest, value, RequestNotification.LogRequest);
			}
			remove
			{
				if (!HttpRuntime.UseIntegratedPipeline)
				{
					throw new PlatformNotSupportedException(SR.GetString("Requires_Iis_Integrated_Mode"));
				}
				this.RemoveSyncEventHookup(HttpApplication.EventLogRequest, value, RequestNotification.LogRequest);
			}
		}

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x06000731 RID: 1841 RVA: 0x0000D624 File Offset: 0x0000B824
		// (remove) Token: 0x06000732 RID: 1842 RVA: 0x0000D64F File Offset: 0x0000B84F
		public event EventHandler PostLogRequest
		{
			add
			{
				if (!HttpRuntime.UseIntegratedPipeline)
				{
					throw new PlatformNotSupportedException(SR.GetString("Requires_Iis_Integrated_Mode"));
				}
				this.AddSyncEventHookup(HttpApplication.EventPostLogRequest, value, RequestNotification.LogRequest, true);
			}
			remove
			{
				if (!HttpRuntime.UseIntegratedPipeline)
				{
					throw new PlatformNotSupportedException(SR.GetString("Requires_Iis_Integrated_Mode"));
				}
				this.RemoveSyncEventHookup(HttpApplication.EventPostLogRequest, value, RequestNotification.LogRequest, true);
			}
		}

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x06000733 RID: 1843 RVA: 0x0000D67A File Offset: 0x0000B87A
		// (remove) Token: 0x06000734 RID: 1844 RVA: 0x0000D68D File Offset: 0x0000B88D
		public event EventHandler EndRequest
		{
			add
			{
				this.AddSyncEventHookup(HttpApplication.EventEndRequest, value, RequestNotification.EndRequest);
			}
			remove
			{
				this.RemoveSyncEventHookup(HttpApplication.EventEndRequest, value, RequestNotification.EndRequest);
			}
		}

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x06000735 RID: 1845 RVA: 0x0000D6A0 File Offset: 0x0000B8A0
		// (remove) Token: 0x06000736 RID: 1846 RVA: 0x0000D6B3 File Offset: 0x0000B8B3
		public event EventHandler Error
		{
			add
			{
				this.Events.AddHandler(HttpApplication.EventErrorRecorded, value);
			}
			remove
			{
				this.Events.RemoveHandler(HttpApplication.EventErrorRecorded, value);
			}
		}

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x06000737 RID: 1847 RVA: 0x0000D6C6 File Offset: 0x0000B8C6
		// (remove) Token: 0x06000738 RID: 1848 RVA: 0x0000D6D9 File Offset: 0x0000B8D9
		public event EventHandler RequestCompleted
		{
			add
			{
				this.Events.AddHandler(HttpApplication.EventRequestCompleted, value);
			}
			remove
			{
				this.Events.RemoveHandler(HttpApplication.EventRequestCompleted, value);
			}
		}

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x06000739 RID: 1849 RVA: 0x0000D6EC File Offset: 0x0000B8EC
		// (remove) Token: 0x0600073A RID: 1850 RVA: 0x0000D6FA File Offset: 0x0000B8FA
		public event EventHandler PreSendRequestHeaders
		{
			add
			{
				this.AddSendResponseEventHookup(HttpApplication.EventPreSendRequestHeaders, value);
			}
			remove
			{
				this.RemoveSendResponseEventHookup(HttpApplication.EventPreSendRequestHeaders, value);
			}
		}

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x0600073B RID: 1851 RVA: 0x0000D708 File Offset: 0x0000B908
		// (remove) Token: 0x0600073C RID: 1852 RVA: 0x0000D716 File Offset: 0x0000B916
		public event EventHandler PreSendRequestContent
		{
			add
			{
				this.AddSendResponseEventHookup(HttpApplication.EventPreSendRequestContent, value);
			}
			remove
			{
				this.RemoveSendResponseEventHookup(HttpApplication.EventPreSendRequestContent, value);
			}
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x0000D724 File Offset: 0x0000B924
		public void AddOnBeginRequestAsync(BeginEventHandler bh, EndEventHandler eh)
		{
			this.AddOnBeginRequestAsync(bh, eh, null);
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x0000D72F File Offset: 0x0000B92F
		public void AddOnBeginRequestAsync(BeginEventHandler beginHandler, EndEventHandler endHandler, object state)
		{
			this.AsyncEvents.AddHandler(HttpApplication.EventBeginRequest, beginHandler, endHandler, state, RequestNotification.BeginRequest, false, this);
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x0000D747 File Offset: 0x0000B947
		public void AddOnAuthenticateRequestAsync(BeginEventHandler bh, EndEventHandler eh)
		{
			this.AddOnAuthenticateRequestAsync(bh, eh, null);
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x0000D752 File Offset: 0x0000B952
		public void AddOnAuthenticateRequestAsync(BeginEventHandler beginHandler, EndEventHandler endHandler, object state)
		{
			this.AsyncEvents.AddHandler(HttpApplication.EventAuthenticateRequest, beginHandler, endHandler, state, RequestNotification.AuthenticateRequest, false, this);
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x0000D76A File Offset: 0x0000B96A
		public void AddOnPostAuthenticateRequestAsync(BeginEventHandler bh, EndEventHandler eh)
		{
			this.AddOnPostAuthenticateRequestAsync(bh, eh, null);
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x0000D775 File Offset: 0x0000B975
		public void AddOnPostAuthenticateRequestAsync(BeginEventHandler beginHandler, EndEventHandler endHandler, object state)
		{
			this.AsyncEvents.AddHandler(HttpApplication.EventPostAuthenticateRequest, beginHandler, endHandler, state, RequestNotification.AuthenticateRequest, true, this);
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x0000D78D File Offset: 0x0000B98D
		public void AddOnAuthorizeRequestAsync(BeginEventHandler bh, EndEventHandler eh)
		{
			this.AddOnAuthorizeRequestAsync(bh, eh, null);
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x0000D798 File Offset: 0x0000B998
		public void AddOnAuthorizeRequestAsync(BeginEventHandler beginHandler, EndEventHandler endHandler, object state)
		{
			this.AsyncEvents.AddHandler(HttpApplication.EventAuthorizeRequest, beginHandler, endHandler, state, RequestNotification.AuthorizeRequest, false, this);
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x0000D7B0 File Offset: 0x0000B9B0
		public void AddOnPostAuthorizeRequestAsync(BeginEventHandler bh, EndEventHandler eh)
		{
			this.AddOnPostAuthorizeRequestAsync(bh, eh, null);
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x0000D7BB File Offset: 0x0000B9BB
		public void AddOnPostAuthorizeRequestAsync(BeginEventHandler beginHandler, EndEventHandler endHandler, object state)
		{
			this.AsyncEvents.AddHandler(HttpApplication.EventPostAuthorizeRequest, beginHandler, endHandler, state, RequestNotification.AuthorizeRequest, true, this);
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x0000D7D3 File Offset: 0x0000B9D3
		public void AddOnResolveRequestCacheAsync(BeginEventHandler bh, EndEventHandler eh)
		{
			this.AddOnResolveRequestCacheAsync(bh, eh, null);
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x0000D7DE File Offset: 0x0000B9DE
		public void AddOnResolveRequestCacheAsync(BeginEventHandler beginHandler, EndEventHandler endHandler, object state)
		{
			this.AsyncEvents.AddHandler(HttpApplication.EventResolveRequestCache, beginHandler, endHandler, state, RequestNotification.ResolveRequestCache, false, this);
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x0000D7F6 File Offset: 0x0000B9F6
		public void AddOnPostResolveRequestCacheAsync(BeginEventHandler bh, EndEventHandler eh)
		{
			this.AddOnPostResolveRequestCacheAsync(bh, eh, null);
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x0000D801 File Offset: 0x0000BA01
		public void AddOnPostResolveRequestCacheAsync(BeginEventHandler beginHandler, EndEventHandler endHandler, object state)
		{
			this.AsyncEvents.AddHandler(HttpApplication.EventPostResolveRequestCache, beginHandler, endHandler, state, RequestNotification.ResolveRequestCache, true, this);
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x0000D819 File Offset: 0x0000BA19
		public void AddOnMapRequestHandlerAsync(BeginEventHandler bh, EndEventHandler eh)
		{
			if (!HttpRuntime.UseIntegratedPipeline)
			{
				throw new PlatformNotSupportedException(SR.GetString("Requires_Iis_Integrated_Mode"));
			}
			this.AddOnMapRequestHandlerAsync(bh, eh, null);
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x0000D83B File Offset: 0x0000BA3B
		public void AddOnMapRequestHandlerAsync(BeginEventHandler beginHandler, EndEventHandler endHandler, object state)
		{
			if (!HttpRuntime.UseIntegratedPipeline)
			{
				throw new PlatformNotSupportedException(SR.GetString("Requires_Iis_Integrated_Mode"));
			}
			this.AsyncEvents.AddHandler(HttpApplication.EventMapRequestHandler, beginHandler, endHandler, state, RequestNotification.MapRequestHandler, false, this);
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x0000D86B File Offset: 0x0000BA6B
		public void AddOnPostMapRequestHandlerAsync(BeginEventHandler bh, EndEventHandler eh)
		{
			this.AddOnPostMapRequestHandlerAsync(bh, eh, null);
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x0000D876 File Offset: 0x0000BA76
		public void AddOnPostMapRequestHandlerAsync(BeginEventHandler beginHandler, EndEventHandler endHandler, object state)
		{
			this.AsyncEvents.AddHandler(HttpApplication.EventPostMapRequestHandler, beginHandler, endHandler, state, RequestNotification.MapRequestHandler, true, this);
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x0000D88F File Offset: 0x0000BA8F
		public void AddOnAcquireRequestStateAsync(BeginEventHandler bh, EndEventHandler eh)
		{
			this.AddOnAcquireRequestStateAsync(bh, eh, null);
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x0000D89A File Offset: 0x0000BA9A
		public void AddOnAcquireRequestStateAsync(BeginEventHandler beginHandler, EndEventHandler endHandler, object state)
		{
			this.AsyncEvents.AddHandler(HttpApplication.EventAcquireRequestState, beginHandler, endHandler, state, RequestNotification.AcquireRequestState, false, this);
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x0000D8B3 File Offset: 0x0000BAB3
		public void AddOnPostAcquireRequestStateAsync(BeginEventHandler bh, EndEventHandler eh)
		{
			this.AddOnPostAcquireRequestStateAsync(bh, eh, null);
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x0000D8BE File Offset: 0x0000BABE
		public void AddOnPostAcquireRequestStateAsync(BeginEventHandler beginHandler, EndEventHandler endHandler, object state)
		{
			this.AsyncEvents.AddHandler(HttpApplication.EventPostAcquireRequestState, beginHandler, endHandler, state, RequestNotification.AcquireRequestState, true, this);
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x0000D8D7 File Offset: 0x0000BAD7
		public void AddOnPreRequestHandlerExecuteAsync(BeginEventHandler bh, EndEventHandler eh)
		{
			this.AddOnPreRequestHandlerExecuteAsync(bh, eh, null);
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x0000D8E2 File Offset: 0x0000BAE2
		public void AddOnPreRequestHandlerExecuteAsync(BeginEventHandler beginHandler, EndEventHandler endHandler, object state)
		{
			this.AsyncEvents.AddHandler(HttpApplication.EventPreRequestHandlerExecute, beginHandler, endHandler, state, RequestNotification.PreExecuteRequestHandler, false, this);
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x0000D8FB File Offset: 0x0000BAFB
		public void AddOnPostRequestHandlerExecuteAsync(BeginEventHandler bh, EndEventHandler eh)
		{
			this.AddOnPostRequestHandlerExecuteAsync(bh, eh, null);
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x0000D906 File Offset: 0x0000BB06
		public void AddOnPostRequestHandlerExecuteAsync(BeginEventHandler beginHandler, EndEventHandler endHandler, object state)
		{
			this.AsyncEvents.AddHandler(HttpApplication.EventPostRequestHandlerExecute, beginHandler, endHandler, state, RequestNotification.ExecuteRequestHandler, true, this);
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x0000D922 File Offset: 0x0000BB22
		public void AddOnReleaseRequestStateAsync(BeginEventHandler bh, EndEventHandler eh)
		{
			this.AddOnReleaseRequestStateAsync(bh, eh, null);
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x0000D92D File Offset: 0x0000BB2D
		public void AddOnReleaseRequestStateAsync(BeginEventHandler beginHandler, EndEventHandler endHandler, object state)
		{
			this.AsyncEvents.AddHandler(HttpApplication.EventReleaseRequestState, beginHandler, endHandler, state, RequestNotification.ReleaseRequestState, false, this);
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x0000D949 File Offset: 0x0000BB49
		public void AddOnPostReleaseRequestStateAsync(BeginEventHandler bh, EndEventHandler eh)
		{
			this.AddOnPostReleaseRequestStateAsync(bh, eh, null);
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x0000D954 File Offset: 0x0000BB54
		public void AddOnPostReleaseRequestStateAsync(BeginEventHandler beginHandler, EndEventHandler endHandler, object state)
		{
			this.AsyncEvents.AddHandler(HttpApplication.EventPostReleaseRequestState, beginHandler, endHandler, state, RequestNotification.ReleaseRequestState, true, this);
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x0000D970 File Offset: 0x0000BB70
		public void AddOnUpdateRequestCacheAsync(BeginEventHandler bh, EndEventHandler eh)
		{
			this.AddOnUpdateRequestCacheAsync(bh, eh, null);
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x0000D97B File Offset: 0x0000BB7B
		public void AddOnUpdateRequestCacheAsync(BeginEventHandler beginHandler, EndEventHandler endHandler, object state)
		{
			this.AsyncEvents.AddHandler(HttpApplication.EventUpdateRequestCache, beginHandler, endHandler, state, RequestNotification.UpdateRequestCache, false, this);
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x0000D997 File Offset: 0x0000BB97
		public void AddOnPostUpdateRequestCacheAsync(BeginEventHandler bh, EndEventHandler eh)
		{
			this.AddOnPostUpdateRequestCacheAsync(bh, eh, null);
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x0000D9A2 File Offset: 0x0000BBA2
		public void AddOnPostUpdateRequestCacheAsync(BeginEventHandler beginHandler, EndEventHandler endHandler, object state)
		{
			this.AsyncEvents.AddHandler(HttpApplication.EventPostUpdateRequestCache, beginHandler, endHandler, state, RequestNotification.UpdateRequestCache, true, this);
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x0000D9BE File Offset: 0x0000BBBE
		public void AddOnLogRequestAsync(BeginEventHandler bh, EndEventHandler eh)
		{
			if (!HttpRuntime.UseIntegratedPipeline)
			{
				throw new PlatformNotSupportedException(SR.GetString("Requires_Iis_Integrated_Mode"));
			}
			this.AddOnLogRequestAsync(bh, eh, null);
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x0000D9E0 File Offset: 0x0000BBE0
		public void AddOnLogRequestAsync(BeginEventHandler beginHandler, EndEventHandler endHandler, object state)
		{
			if (!HttpRuntime.UseIntegratedPipeline)
			{
				throw new PlatformNotSupportedException(SR.GetString("Requires_Iis_Integrated_Mode"));
			}
			this.AsyncEvents.AddHandler(HttpApplication.EventLogRequest, beginHandler, endHandler, state, RequestNotification.LogRequest, false, this);
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x0000DA13 File Offset: 0x0000BC13
		public void AddOnPostLogRequestAsync(BeginEventHandler bh, EndEventHandler eh)
		{
			if (!HttpRuntime.UseIntegratedPipeline)
			{
				throw new PlatformNotSupportedException(SR.GetString("Requires_Iis_Integrated_Mode"));
			}
			this.AddOnPostLogRequestAsync(bh, eh, null);
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x0000DA35 File Offset: 0x0000BC35
		public void AddOnPostLogRequestAsync(BeginEventHandler beginHandler, EndEventHandler endHandler, object state)
		{
			if (!HttpRuntime.UseIntegratedPipeline)
			{
				throw new PlatformNotSupportedException(SR.GetString("Requires_Iis_Integrated_Mode"));
			}
			this.AsyncEvents.AddHandler(HttpApplication.EventPostLogRequest, beginHandler, endHandler, state, RequestNotification.LogRequest, true, this);
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x0000DA68 File Offset: 0x0000BC68
		public void AddOnEndRequestAsync(BeginEventHandler bh, EndEventHandler eh)
		{
			this.AddOnEndRequestAsync(bh, eh, null);
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x0000DA73 File Offset: 0x0000BC73
		public void AddOnEndRequestAsync(BeginEventHandler beginHandler, EndEventHandler endHandler, object state)
		{
			this.AsyncEvents.AddHandler(HttpApplication.EventEndRequest, beginHandler, endHandler, state, RequestNotification.EndRequest, false, this);
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x00006164 File Offset: 0x00004364
		public virtual void Init()
		{
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x0000DA90 File Offset: 0x0000BC90
		public virtual void Dispose()
		{
			this._site = null;
			if (this._events != null)
			{
				try
				{
					EventHandler eventHandler = (EventHandler)this._events[HttpApplication.EventDisposed];
					if (eventHandler != null)
					{
						eventHandler(this, EventArgs.Empty);
					}
				}
				finally
				{
					this._events.Dispose();
				}
			}
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x0000DAF0 File Offset: 0x0000BCF0
		[SecurityPermission(SecurityAction.Assert, ControlPrincipal = true)]
		internal static void SetCurrentPrincipalWithAssert(IPrincipal user)
		{
			Thread.CurrentPrincipal = user;
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x0000DAF8 File Offset: 0x0000BCF8
		[SecurityPermission(SecurityAction.Assert, ControlPrincipal = true)]
		internal static WindowsIdentity GetCurrentWindowsIdentityWithAssert()
		{
			return WindowsIdentity.GetCurrent();
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x0000DB00 File Offset: 0x0000BD00
		private HttpHandlerAction GetHandlerMapping(HttpContext context, string requestType, VirtualPath path, bool useAppConfig)
		{
			CachedPathData cachedPathData = null;
			HandlerMappingMemo handlerMappingMemo = null;
			if (!useAppConfig)
			{
				cachedPathData = context.GetPathData(path);
				handlerMappingMemo = cachedPathData.CachedHandler;
				if (handlerMappingMemo != null && !handlerMappingMemo.IsMatch(requestType, path))
				{
					handlerMappingMemo = null;
				}
			}
			HttpHandlerAction httpHandlerAction;
			if (handlerMappingMemo == null)
			{
				HttpHandlersSection httpHandlersSection = useAppConfig ? RuntimeConfig.GetAppConfig().HttpHandlers : RuntimeConfig.GetConfig(context).HttpHandlers;
				httpHandlerAction = httpHandlersSection.FindMapping(requestType, path);
				if (!useAppConfig)
				{
					handlerMappingMemo = new HandlerMappingMemo(httpHandlerAction, requestType, path);
					cachedPathData.CachedHandler = handlerMappingMemo;
				}
			}
			else
			{
				httpHandlerAction = handlerMappingMemo.Mapping;
			}
			return httpHandlerAction;
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x0000DB7C File Offset: 0x0000BD7C
		internal IHttpHandler MapIntegratedHttpHandler(HttpContext context, string requestType, VirtualPath path, string pathTranslated, bool useAppConfig, bool convertNativeStaticFileModule)
		{
			IHttpHandler httpHandler = null;
			using (new ApplicationImpersonationContext())
			{
				string text = path.VirtualPathString;
				if (useAppConfig)
				{
					int num = text.LastIndexOf('/');
					num++;
					if (num != 0 && num < text.Length)
					{
						text = UrlPath.SimpleCombine(HttpRuntime.AppDomainAppVirtualPathString, text.Substring(num));
					}
					else
					{
						text = HttpRuntime.AppDomainAppVirtualPathString;
					}
				}
				IIS7WorkerRequest iis7WorkerRequest = context.WorkerRequest as IIS7WorkerRequest;
				string text2 = iis7WorkerRequest.MapHandlerAndGetHandlerTypeString(requestType, text, convertNativeStaticFileModule, false);
				if (text2 == null)
				{
					PerfCounters.IncrementCounter(AppPerfCounter.REQUESTS_NOT_FOUND);
					PerfCounters.IncrementCounter(AppPerfCounter.REQUESTS_FAILED);
					throw new HttpException(SR.GetString("Http_handler_not_found_for_request_type", new object[]
					{
						requestType
					}));
				}
				if (string.IsNullOrEmpty(text2))
				{
					return httpHandler;
				}
				IHttpHandlerFactory factory = this.GetFactory(text2);
				try
				{
					httpHandler = factory.GetHandler(context, requestType, path.VirtualPathString, pathTranslated);
				}
				catch (FileNotFoundException innerException)
				{
					if (HttpRuntime.HasPathDiscoveryPermission(pathTranslated))
					{
						throw new HttpException(404, null, innerException);
					}
					throw new HttpException(404, null);
				}
				catch (DirectoryNotFoundException innerException2)
				{
					if (HttpRuntime.HasPathDiscoveryPermission(pathTranslated))
					{
						throw new HttpException(404, null, innerException2);
					}
					throw new HttpException(404, null);
				}
				catch (PathTooLongException innerException3)
				{
					if (HttpRuntime.HasPathDiscoveryPermission(pathTranslated))
					{
						throw new HttpException(414, null, innerException3);
					}
					throw new HttpException(414, null);
				}
				if (this._handlerRecycleList == null)
				{
					this._handlerRecycleList = new ArrayList();
				}
				this._handlerRecycleList.Add(new HandlerWithFactory(httpHandler, factory));
			}
			return httpHandler;
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x0000DD54 File Offset: 0x0000BF54
		internal IHttpHandler MapHttpHandler(HttpContext context, string requestType, VirtualPath path, string pathTranslated, bool useAppConfig)
		{
			IHttpHandler httpHandler = (context.ServerExecuteDepth == 0) ? context.RemapHandlerInstance : null;
			using (new ApplicationImpersonationContext())
			{
				if (httpHandler != null)
				{
					return httpHandler;
				}
				HttpHandlerAction handlerMapping = this.GetHandlerMapping(context, requestType, path, useAppConfig);
				if (handlerMapping == null)
				{
					PerfCounters.IncrementCounter(AppPerfCounter.REQUESTS_NOT_FOUND);
					PerfCounters.IncrementCounter(AppPerfCounter.REQUESTS_FAILED);
					throw new HttpException(SR.GetString("Http_handler_not_found_for_request_type", new object[]
					{
						requestType
					}));
				}
				IHttpHandlerFactory factory = this.GetFactory(handlerMapping);
				try
				{
					IHttpHandlerFactory2 httpHandlerFactory = factory as IHttpHandlerFactory2;
					if (httpHandlerFactory != null)
					{
						httpHandler = httpHandlerFactory.GetHandler(context, requestType, path, pathTranslated);
					}
					else
					{
						httpHandler = factory.GetHandler(context, requestType, path.VirtualPathString, pathTranslated);
					}
				}
				catch (FileNotFoundException innerException)
				{
					if (HttpRuntime.HasPathDiscoveryPermission(pathTranslated))
					{
						throw new HttpException(404, null, innerException);
					}
					throw new HttpException(404, null);
				}
				catch (DirectoryNotFoundException innerException2)
				{
					if (HttpRuntime.HasPathDiscoveryPermission(pathTranslated))
					{
						throw new HttpException(404, null, innerException2);
					}
					throw new HttpException(404, null);
				}
				catch (PathTooLongException innerException3)
				{
					if (HttpRuntime.HasPathDiscoveryPermission(pathTranslated))
					{
						throw new HttpException(414, null, innerException3);
					}
					throw new HttpException(414, null);
				}
				if (this._handlerRecycleList == null)
				{
					this._handlerRecycleList = new ArrayList();
				}
				this._handlerRecycleList.Add(new HandlerWithFactory(httpHandler, factory));
			}
			return httpHandler;
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x0000DEFC File Offset: 0x0000C0FC
		public virtual string GetVaryByCustomString(HttpContext context, string custom)
		{
			if (StringUtil.EqualsIgnoreCase(custom, "browser"))
			{
				return context.Request.Browser.Type;
			}
			return null;
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x0000DF1D File Offset: 0x0000C11D
		public virtual string GetOutputCacheProviderName(HttpContext context)
		{
			return OutputCache.DefaultProviderName;
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x0600076E RID: 1902 RVA: 0x0000DF24 File Offset: 0x0000C124
		// (set) Token: 0x0600076F RID: 1903 RVA: 0x0000DF2C File Offset: 0x0000C12C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ISite Site
		{
			get
			{
				return this._site;
			}
			set
			{
				this._site = value;
			}
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x0000DF38 File Offset: 0x0000C138
		IAsyncResult IHttpAsyncHandler.BeginProcessRequest(HttpContext context, AsyncCallback cb, object extraData)
		{
			this._context = context;
			this._context.ApplicationInstance = this;
			this._stepManager.InitRequest();
			this._context.Root();
			HttpAsyncResult httpAsyncResult = new HttpAsyncResult(cb, extraData);
			this.AsyncResult = httpAsyncResult;
			if (this._context.TraceIsEnabled)
			{
				HttpRuntime.Profile.StartRequest(this._context);
			}
			this.ResumeSteps(null);
			return httpAsyncResult;
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x0000DFA4 File Offset: 0x0000C1A4
		void IHttpAsyncHandler.EndProcessRequest(IAsyncResult result)
		{
			HttpAsyncResult httpAsyncResult = (HttpAsyncResult)result;
			if (httpAsyncResult.Error != null)
			{
				throw httpAsyncResult.Error;
			}
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x0000DFC7 File Offset: 0x0000C1C7
		void IHttpHandler.ProcessRequest(HttpContext context)
		{
			throw new HttpException(SR.GetString("Sync_not_supported"));
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000773 RID: 1907 RVA: 0x000097B7 File Offset: 0x000079B7
		bool IHttpHandler.IsReusable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x0000DFD8 File Offset: 0x0000C1D8
		[ReflectionPermission(SecurityAction.Assert, Flags = ReflectionPermissionFlag.RestrictedMemberAccess)]
		private void InvokeMethodWithAssert(MethodInfo method, int paramCount, object eventSource, EventArgs eventArgs)
		{
			if (paramCount == 0)
			{
				method.Invoke(this, new object[0]);
				return;
			}
			method.Invoke(this, new object[]
			{
				eventSource,
				eventArgs
			});
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x0000E004 File Offset: 0x0000C204
		internal void ProcessSpecialRequest(HttpContext context, MethodInfo method, int paramCount, object eventSource, EventArgs eventArgs, HttpSessionState session)
		{
			this._context = context;
			if (HttpRuntime.UseIntegratedPipeline && this._context != null)
			{
				this._context.HideRequestResponse = true;
			}
			this._hideRequestResponse = true;
			this._session = session;
			this._lastError = null;
			using (new DisposableHttpContextWrapper(context))
			{
				using (new ApplicationImpersonationContext())
				{
					try
					{
						this.SetAppLevelCulture();
						this.InvokeMethodWithAssert(method, paramCount, eventSource, eventArgs);
					}
					catch (Exception ex)
					{
						Exception ex2;
						if (ex is TargetInvocationException)
						{
							ex2 = ex.InnerException;
						}
						else
						{
							ex2 = ex;
						}
						this.RecordError(ex2);
						if (context == null)
						{
							try
							{
								WebBaseEvent.RaiseRuntimeError(ex2, this);
							}
							catch
							{
							}
						}
					}
					finally
					{
						if (this._state != null)
						{
							this._state.EnsureUnLock();
						}
						this.RestoreAppLevelCulture();
						if (HttpRuntime.UseIntegratedPipeline && this._context != null)
						{
							this._context.HideRequestResponse = false;
						}
						this._hideRequestResponse = false;
						this._context = null;
						this._session = null;
						this._lastError = null;
						this._appEvent = null;
					}
				}
			}
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x0000E148 File Offset: 0x0000C348
		internal void RaiseErrorWithoutContext(Exception error)
		{
			try
			{
				try
				{
					this.SetAppLevelCulture();
					this._lastError = error;
					this.RaiseOnError();
				}
				finally
				{
					if (this._state != null)
					{
						this._state.EnsureUnLock();
					}
					this.RestoreAppLevelCulture();
					this._lastError = null;
					this._appEvent = null;
				}
			}
			catch
			{
				throw;
			}
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x0000E1B4 File Offset: 0x0000C3B4
		internal void InitInternal(HttpContext context, HttpApplicationState state, MethodInfo[] handlers)
		{
			this._state = state;
			PerfCounters.IncrementCounter(AppPerfCounter.PIPELINES);
			try
			{
				try
				{
					this._initContext = context;
					this._initContext.ApplicationInstance = this;
					context.ConfigurationPath = context.Request.ApplicationPathObject;
					using (new DisposableHttpContextWrapper(context))
					{
						if (HttpRuntime.UseIntegratedPipeline)
						{
							try
							{
								context.HideRequestResponse = true;
								this._hideRequestResponse = true;
								this.InitIntegratedModules();
								goto IL_6B;
							}
							finally
							{
								context.HideRequestResponse = false;
								this._hideRequestResponse = false;
							}
						}
						this.InitModules();
						IL_6B:
						if (handlers != null)
						{
							this.HookupEventHandlersForApplicationAndModules(handlers);
						}
						this._context = context;
						if (HttpRuntime.UseIntegratedPipeline && this._context != null)
						{
							this._context.HideRequestResponse = true;
						}
						this._hideRequestResponse = true;
						try
						{
							this.Init();
						}
						catch (Exception error)
						{
							this.RecordError(error);
						}
					}
					if (HttpRuntime.UseIntegratedPipeline && this._context != null)
					{
						this._context.HideRequestResponse = false;
					}
					this._hideRequestResponse = false;
					this._context = null;
					this._resumeStepsWaitCallback = new WaitCallback(this.ResumeStepsWaitCallback);
					if (HttpRuntime.UseIntegratedPipeline)
					{
						this._stepManager = new HttpApplication.PipelineStepManager(this);
					}
					else
					{
						this._stepManager = new HttpApplication.ApplicationStepManager(this);
					}
					this._stepManager.BuildSteps(this._resumeStepsWaitCallback);
				}
				finally
				{
					this._initInternalCompleted = true;
					context.ConfigurationPath = null;
					this._initContext.ApplicationInstance = null;
					this._initContext = null;
				}
			}
			catch
			{
				throw;
			}
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x0000E390 File Offset: 0x0000C590
		private void CreateEventExecutionSteps(object eventIndex, ArrayList steps)
		{
			HttpApplication.AsyncAppEventHandler asyncAppEventHandler = this.AsyncEvents[eventIndex];
			if (asyncAppEventHandler != null)
			{
				asyncAppEventHandler.CreateExecutionSteps(this, steps);
			}
			EventHandler eventHandler = (EventHandler)this.Events[eventIndex];
			if (eventHandler != null)
			{
				Delegate[] invocationList = eventHandler.GetInvocationList();
				for (int i = 0; i < invocationList.Length; i++)
				{
					steps.Add(new HttpApplication.SyncEventExecutionStep(this, (EventHandler)invocationList[i]));
				}
			}
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x0000E3F4 File Offset: 0x0000C5F4
		internal void InitSpecial(HttpApplicationState state, MethodInfo[] handlers, IntPtr appContext, HttpContext context)
		{
			this._state = state;
			try
			{
				if (context != null)
				{
					this._initContext = context;
					this._initContext.ApplicationInstance = this;
				}
				if (appContext != IntPtr.Zero)
				{
					using (new ApplicationImpersonationContext())
					{
						HttpRuntime.CheckApplicationEnabled();
					}
					this.InitAppLevelCulture();
					this.RegisterEventSubscriptionsWithIIS(appContext, context, handlers);
				}
				else
				{
					this.InitAppLevelCulture();
					if (handlers != null)
					{
						this.HookupEventHandlersForApplicationAndModules(handlers);
					}
				}
				if (appContext != IntPtr.Zero && (this._appPostNotifications != (RequestNotification)0 || this._appRequestNotifications != (RequestNotification)0))
				{
					this.RegisterIntegratedEvent(appContext, "global.asax", this._appRequestNotifications, this._appPostNotifications, base.GetType().FullName, "managedHandler", false);
				}
			}
			finally
			{
				HttpApplication._initSpecialCompleted = true;
				if (this._initContext != null)
				{
					this._initContext.ApplicationInstance = null;
					this._initContext = null;
				}
			}
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x0000E4EC File Offset: 0x0000C6EC
		internal void DisposeInternal()
		{
			PerfCounters.DecrementCounter(AppPerfCounter.PIPELINES);
			try
			{
				this.Dispose();
			}
			catch (Exception error)
			{
				this.RecordError(error);
			}
			if (this._moduleCollection != null)
			{
				int count = this._moduleCollection.Count;
				for (int i = 0; i < count; i++)
				{
					try
					{
						if (HttpRuntime.UseIntegratedPipeline)
						{
							this._currentModuleCollectionKey = this._moduleCollection.GetKey(i);
						}
						this._moduleCollection[i].Dispose();
					}
					catch
					{
					}
				}
				this._moduleCollection = null;
			}
			if (this._allocator != null)
			{
				this._allocator.TrimMemory();
			}
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x0000E598 File Offset: 0x0000C798
		private void BuildEventMaskDictionary(Dictionary<string, RequestNotification> eventMask)
		{
			eventMask["BeginRequest"] = RequestNotification.BeginRequest;
			eventMask["AuthenticateRequest"] = RequestNotification.AuthenticateRequest;
			eventMask["PostAuthenticateRequest"] = RequestNotification.AuthenticateRequest;
			eventMask["AuthorizeRequest"] = RequestNotification.AuthorizeRequest;
			eventMask["PostAuthorizeRequest"] = RequestNotification.AuthorizeRequest;
			eventMask["ResolveRequestCache"] = RequestNotification.ResolveRequestCache;
			eventMask["PostResolveRequestCache"] = RequestNotification.ResolveRequestCache;
			eventMask["MapRequestHandler"] = RequestNotification.MapRequestHandler;
			eventMask["PostMapRequestHandler"] = RequestNotification.MapRequestHandler;
			eventMask["AcquireRequestState"] = RequestNotification.AcquireRequestState;
			eventMask["PostAcquireRequestState"] = RequestNotification.AcquireRequestState;
			eventMask["PreRequestHandlerExecute"] = RequestNotification.PreExecuteRequestHandler;
			eventMask["PostRequestHandlerExecute"] = RequestNotification.ExecuteRequestHandler;
			eventMask["ReleaseRequestState"] = RequestNotification.ReleaseRequestState;
			eventMask["PostReleaseRequestState"] = RequestNotification.ReleaseRequestState;
			eventMask["UpdateRequestCache"] = RequestNotification.UpdateRequestCache;
			eventMask["PostUpdateRequestCache"] = RequestNotification.UpdateRequestCache;
			eventMask["LogRequest"] = RequestNotification.LogRequest;
			eventMask["PostLogRequest"] = RequestNotification.LogRequest;
			eventMask["EndRequest"] = RequestNotification.EndRequest;
			eventMask["PreSendRequestHeaders"] = RequestNotification.SendResponse;
			eventMask["PreSendRequestContent"] = RequestNotification.SendResponse;
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x0000E6DC File Offset: 0x0000C8DC
		private void HookupEventHandlersForApplicationAndModules(MethodInfo[] handlers)
		{
			this._currentModuleCollectionKey = "global.asax";
			if (this._pipelineEventMasks == null)
			{
				Dictionary<string, RequestNotification> dictionary = new Dictionary<string, RequestNotification>();
				this.BuildEventMaskDictionary(dictionary);
				if (this._pipelineEventMasks == null)
				{
					this._pipelineEventMasks = dictionary;
				}
			}
			foreach (MethodInfo methodInfo in handlers)
			{
				string name = methodInfo.Name;
				int num = name.IndexOf('_');
				string text = name.Substring(0, num);
				object obj = null;
				if (StringUtil.EqualsIgnoreCase(text, "Application"))
				{
					obj = this;
				}
				else if (this._moduleCollection != null)
				{
					obj = this._moduleCollection[text];
				}
				if (obj != null)
				{
					Type type = obj.GetType();
					EventDescriptorCollection events = TypeDescriptor.GetEvents(type);
					string text2 = name.Substring(num + 1);
					EventDescriptor eventDescriptor = events.Find(text2, true);
					if (eventDescriptor == null && StringUtil.EqualsIgnoreCase(text2.Substring(0, 2), "on"))
					{
						text2 = text2.Substring(2);
						eventDescriptor = events.Find(text2, true);
					}
					MethodInfo methodInfo2 = null;
					if (eventDescriptor != null)
					{
						EventInfo @event = type.GetEvent(eventDescriptor.Name);
						if (@event != null)
						{
							methodInfo2 = @event.GetAddMethod();
						}
					}
					if (!(methodInfo2 == null))
					{
						ParameterInfo[] parameters = methodInfo2.GetParameters();
						if (parameters.Length == 1)
						{
							Delegate @delegate = null;
							ParameterInfo[] parameters2 = methodInfo.GetParameters();
							if (parameters2.Length == 0)
							{
								if (parameters[0].ParameterType != typeof(EventHandler))
								{
									goto IL_201;
								}
								ArglessEventHandlerProxy arglessEventHandlerProxy = new ArglessEventHandlerProxy(this, methodInfo);
								@delegate = arglessEventHandlerProxy.Handler;
							}
							else
							{
								try
								{
									@delegate = Delegate.CreateDelegate(parameters[0].ParameterType, this, name);
								}
								catch
								{
									goto IL_201;
								}
							}
							try
							{
								methodInfo2.Invoke(obj, new object[]
								{
									@delegate
								});
							}
							catch
							{
								if (HttpRuntime.UseIntegratedPipeline)
								{
									throw;
								}
							}
							if (text2 != null && this._pipelineEventMasks.ContainsKey(text2))
							{
								if (!StringUtil.StringStartsWith(text2, "Post"))
								{
									this._appRequestNotifications |= this._pipelineEventMasks[text2];
								}
								else
								{
									this._appPostNotifications |= this._pipelineEventMasks[text2];
								}
							}
						}
					}
				}
				IL_201:;
			}
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x0000E914 File Offset: 0x0000CB14
		private void RegisterIntegratedEvent(IntPtr appContext, string moduleName, RequestNotification requestNotifications, RequestNotification postRequestNotifications, string moduleType, string modulePrecondition, bool useHighPriority)
		{
			int num;
			if (HttpApplication._moduleIndexMap.ContainsKey(moduleName))
			{
				num = (int)HttpApplication._moduleIndexMap[moduleName];
			}
			else
			{
				num = HttpApplication._moduleIndexMap.Count;
				HttpApplication._moduleIndexMap[moduleName] = num;
			}
			int num2 = UnsafeIISMethods.MgdRegisterEventSubscription(appContext, moduleName, requestNotifications, postRequestNotifications, moduleType, modulePrecondition, new IntPtr(num), useHighPriority);
			if (num2 < 0)
			{
				throw new HttpException(SR.GetString("Failed_Pipeline_Subscription", new object[]
				{
					moduleName
				}));
			}
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x0000E994 File Offset: 0x0000CB94
		private void SetAppLevelCulture()
		{
			CultureInfo cultureInfo = null;
			if ((this._appLevelAutoCulture || this._appLevelAutoUICulture) && this._context != null && !this._context.HideRequestResponse)
			{
				string[] array = this._context.UserLanguagesFromContext();
				if (array != null)
				{
					try
					{
						cultureInfo = CultureUtil.CreateReadOnlyCulture(array, true);
					}
					catch
					{
					}
				}
			}
			CultureInfo cultureInfo2 = this._appLevelCulture;
			CultureInfo cultureInfo3 = this._appLevelUICulture;
			if (cultureInfo != null)
			{
				if (this._appLevelAutoCulture)
				{
					cultureInfo2 = cultureInfo;
				}
				if (this._appLevelAutoUICulture)
				{
					cultureInfo3 = cultureInfo;
				}
			}
			this._savedAppLevelCulture = Thread.CurrentThread.CurrentCulture;
			this._savedAppLevelUICulture = Thread.CurrentThread.CurrentUICulture;
			if (cultureInfo2 != null && cultureInfo2 != Thread.CurrentThread.CurrentCulture)
			{
				HttpRuntime.SetCurrentThreadCultureWithAssert(cultureInfo2);
			}
			if (cultureInfo3 != null && cultureInfo3 != Thread.CurrentThread.CurrentUICulture)
			{
				Thread.CurrentThread.CurrentUICulture = cultureInfo3;
			}
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x0000EA70 File Offset: 0x0000CC70
		private void RestoreAppLevelCulture()
		{
			CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
			CultureInfo currentUICulture = Thread.CurrentThread.CurrentUICulture;
			if (this._savedAppLevelCulture != null)
			{
				if (currentCulture != this._savedAppLevelCulture)
				{
					HttpRuntime.SetCurrentThreadCultureWithAssert(this._savedAppLevelCulture);
				}
				this._savedAppLevelCulture = null;
			}
			if (this._savedAppLevelUICulture != null)
			{
				if (currentUICulture != this._savedAppLevelUICulture)
				{
					Thread.CurrentThread.CurrentUICulture = this._savedAppLevelUICulture;
				}
				this._savedAppLevelUICulture = null;
			}
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x0000EAE0 File Offset: 0x0000CCE0
		private ThreadContext OnThreadEnterPrivate(bool setImpersonationContext)
		{
			ThreadContext threadContext = new ThreadContext(this._context);
			threadContext.AssociateWithCurrentThread(setImpersonationContext);
			if (!this._timeoutManagerInitialized)
			{
				this._context.EnsureTimeout();
				HttpRuntime.RequestTimeoutManager.Add(this._context);
				this._timeoutManagerInitialized = true;
			}
			return threadContext;
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000781 RID: 1921 RVA: 0x0000EB2C File Offset: 0x0000CD2C
		HttpContext ISyncContext.HttpContext
		{
			get
			{
				return this._context;
			}
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x0000EB34 File Offset: 0x0000CD34
		ISyncContextLock ISyncContext.Enter()
		{
			return this.OnThreadEnter();
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x0000EB3C File Offset: 0x0000CD3C
		internal ThreadContext OnThreadEnter()
		{
			return this.OnThreadEnterPrivate(true);
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x0000EB45 File Offset: 0x0000CD45
		internal ThreadContext OnThreadEnter(bool setImpersonationContext)
		{
			return this.OnThreadEnterPrivate(setImpersonationContext);
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x0000EB50 File Offset: 0x0000CD50
		public void OnExecuteRequestStep(Action<HttpContextBase, Action> callback)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			if (!HttpRuntime.UseIntegratedPipeline)
			{
				throw new PlatformNotSupportedException(SR.GetString("Requires_Iis_Integrated_Mode"));
			}
			if (HttpApplication._initSpecialCompleted && this._initInternalCompleted)
			{
				throw new InvalidOperationException(SR.GetString("OnExecuteRequestStep_Cannot_Be_Called"));
			}
			if (this._stepInvoker == null)
			{
				this._stepInvoker = new HttpApplication.StepInvoker();
			}
			HttpApplication.StepInvoker stepInvoker = this._stepInvoker;
			this._stepInvoker = new HttpApplication.StepInvoker(delegate(Action nextStepAction)
			{
				callback(new HttpContextWrapper(this._context), nextStepAction);
			}, stepInvoker);
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x0000EBEC File Offset: 0x0000CDEC
		private void ExecuteStepImpl(HttpApplication.IExecutionStep step)
		{
			if (this._stepInvoker != null)
			{
				bool stepCalled = false;
				this._stepInvoker.Invoke(delegate
				{
					if (!stepCalled)
					{
						stepCalled = true;
						step.Execute();
					}
				});
				if (!stepCalled)
				{
					step.Execute();
					return;
				}
			}
			else
			{
				step.Execute();
			}
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x0000EC4C File Offset: 0x0000CE4C
		internal Exception ExecuteStep(HttpApplication.IExecutionStep step, ref bool completedSynchronously)
		{
			Exception result = null;
			try
			{
				try
				{
					if (step.IsCancellable)
					{
						this._context.BeginCancellablePeriod();
						try
						{
							this.ExecuteStepImpl(step);
						}
						finally
						{
							this._context.EndCancellablePeriod();
						}
						this._context.WaitForExceptionIfCancelled();
					}
					else
					{
						this.ExecuteStepImpl(step);
					}
					if (!step.CompletedSynchronously)
					{
						completedSynchronously = false;
						return null;
					}
				}
				catch (Exception ex)
				{
					result = ex;
					if (ImpersonationContext.CurrentThreadTokenExists)
					{
						ex.Data["ASPIMPERSONATING"] = string.Empty;
					}
					if (ex is ThreadAbortException && (Thread.CurrentThread.ThreadState & System.Threading.ThreadState.AbortRequested) == System.Threading.ThreadState.Running)
					{
						result = null;
						this._stepManager.CompleteRequest();
					}
				}
				catch
				{
				}
			}
			catch (ThreadAbortException ex2)
			{
				if (ex2.ExceptionState != null && ex2.ExceptionState is HttpApplication.CancelModuleException)
				{
					HttpApplication.CancelModuleException ex3 = (HttpApplication.CancelModuleException)ex2.ExceptionState;
					if (ex3.Timeout)
					{
						result = new HttpException(SR.GetString("Request_timed_out"), null, 3001);
						PerfCounters.IncrementCounter(AppPerfCounter.REQUESTS_TIMED_OUT);
					}
					else
					{
						result = null;
						this._stepManager.CompleteRequest();
					}
					Thread.ResetAbort();
				}
			}
			completedSynchronously = true;
			return result;
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x0000ED94 File Offset: 0x0000CF94
		private void ResumeStepsFromThreadPoolThread(Exception error)
		{
			if (Thread.CurrentThread.IsThreadPoolThread)
			{
				this.ResumeSteps(error);
				return;
			}
			ThreadPool.QueueUserWorkItem(this._resumeStepsWaitCallback, error);
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x0000EDB7 File Offset: 0x0000CFB7
		private void ResumeStepsWaitCallback(object error)
		{
			this.ResumeSteps(error as Exception);
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x0000EDC5 File Offset: 0x0000CFC5
		private void ResumeSteps(Exception error)
		{
			this._stepManager.ResumeSteps(error);
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x0000EDD4 File Offset: 0x0000CFD4
		private void RecordError(Exception error)
		{
			bool flag = true;
			if (this._context != null)
			{
				if (this._context.Error != null)
				{
					flag = false;
				}
				this._context.AddError(error);
			}
			else
			{
				if (this._lastError != null)
				{
					flag = false;
				}
				this._lastError = error;
			}
			if (flag)
			{
				this.RaiseOnError();
			}
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x0000EE24 File Offset: 0x0000D024
		private void InitModulesCommon()
		{
			int count = this._moduleCollection.Count;
			for (int i = 0; i < count; i++)
			{
				this._currentModuleCollectionKey = this._moduleCollection.GetKey(i);
				this._moduleCollection[i].Init(this);
			}
			this._currentModuleCollectionKey = null;
			this.InitAppLevelCulture();
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x0000EE7A File Offset: 0x0000D07A
		private void InitIntegratedModules()
		{
			this._moduleCollection = this.BuildIntegratedModuleCollection(HttpApplication._moduleConfigInfo);
			this.InitModulesCommon();
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x0000EE94 File Offset: 0x0000D094
		private void InitModules()
		{
			HttpModulesSection httpModules = RuntimeConfig.GetAppConfig().HttpModules;
			HttpModuleCollection httpModuleCollection = httpModules.CreateModules();
			HttpModuleCollection other = this.CreateDynamicModules();
			httpModuleCollection.AppendCollection(other);
			this._moduleCollection = httpModuleCollection;
			this.InitModulesCommon();
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x0000EED0 File Offset: 0x0000D0D0
		private HttpModuleCollection CreateDynamicModules()
		{
			HttpModuleCollection httpModuleCollection = new HttpModuleCollection();
			foreach (DynamicModuleRegistryEntry dynamicModuleRegistryEntry in HttpApplication._dynamicModuleRegistry.LockAndFetchList())
			{
				HttpModuleAction httpModuleAction = new HttpModuleAction(dynamicModuleRegistryEntry.Name, dynamicModuleRegistryEntry.Type);
				httpModuleCollection.AddModule(httpModuleAction.Entry.ModuleName, httpModuleAction.Entry.Create());
			}
			return httpModuleCollection;
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000790 RID: 1936 RVA: 0x0000EF50 File Offset: 0x0000D150
		internal string CurrentModuleCollectionKey
		{
			get
			{
				if (this._currentModuleCollectionKey != null)
				{
					return this._currentModuleCollectionKey;
				}
				return "UnknownModule";
			}
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x0000EF66 File Offset: 0x0000D166
		internal static void RegisterModuleInternal(Type moduleType)
		{
			HttpApplication._dynamicModuleRegistry.Add(moduleType);
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x0000EF74 File Offset: 0x0000D174
		public static void RegisterModule(Type moduleType)
		{
			RuntimeConfig appConfig = RuntimeConfig.GetAppConfig();
			HttpRuntimeSection httpRuntime = appConfig.HttpRuntime;
			if (httpRuntime.AllowDynamicModuleRegistration)
			{
				HttpApplication.RegisterModuleInternal(moduleType);
				return;
			}
			throw new InvalidOperationException(SR.GetString("DynamicModuleRegistrationOff"));
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x0000EFAC File Offset: 0x0000D1AC
		private void RegisterEventSubscriptionsWithIIS(IntPtr appContext, HttpContext context, MethodInfo[] handlers)
		{
			this.RegisterIntegratedEvent(appContext, "AspNetFilterModule", RequestNotification.UpdateRequestCache | RequestNotification.LogRequest, (RequestNotification)0, string.Empty, string.Empty, true);
			this._moduleCollection = this.GetModuleCollection(appContext);
			if (handlers != null)
			{
				this.HookupEventHandlersForApplicationAndModules(handlers);
			}
			HttpApplicationFactory.EnsureAppStartCalledForIntegratedMode(context, this);
			this._currentModuleCollectionKey = "global.asax";
			try
			{
				this._hideRequestResponse = true;
				context.HideRequestResponse = true;
				this._context = context;
				this.Init();
			}
			catch (Exception error)
			{
				this.RecordError(error);
				Exception error2 = context.Error;
				if (error2 != null)
				{
					throw error2;
				}
			}
			finally
			{
				this._context = null;
				context.HideRequestResponse = false;
				this._hideRequestResponse = false;
			}
			RequestNotification requestNotification;
			RequestNotification requestNotification2;
			this.ProcessEventSubscriptions(out requestNotification, out requestNotification2);
			this._appRequestNotifications |= requestNotification;
			this._appPostNotifications |= requestNotification2;
			for (int i = 0; i < this._moduleCollection.Count; i++)
			{
				this._currentModuleCollectionKey = this._moduleCollection.GetKey(i);
				IHttpModule httpModule = this._moduleCollection.Get(i);
				ModuleConfigurationInfo moduleConfigurationInfo = HttpApplication._moduleConfigInfo[i];
				httpModule.Init(this);
				this.ProcessEventSubscriptions(out requestNotification, out requestNotification2);
				if (requestNotification != (RequestNotification)0 || requestNotification2 != (RequestNotification)0)
				{
					this.RegisterIntegratedEvent(appContext, moduleConfigurationInfo.Name, requestNotification, requestNotification2, moduleConfigurationInfo.Type, moduleConfigurationInfo.Precondition, false);
				}
			}
			this.RegisterIntegratedEvent(appContext, "ManagedPipelineHandler", RequestNotification.MapRequestHandler | RequestNotification.ExecuteRequestHandler, RequestNotification.EndRequest, string.Empty, string.Empty, false);
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x0000F12C File Offset: 0x0000D32C
		private void ProcessEventSubscriptions(out RequestNotification requestNotifications, out RequestNotification postRequestNotifications)
		{
			requestNotifications = (RequestNotification)0;
			postRequestNotifications = (RequestNotification)0;
			if (this.HasEventSubscription(HttpApplication.EventBeginRequest))
			{
				requestNotifications |= RequestNotification.BeginRequest;
			}
			if (this.HasEventSubscription(HttpApplication.EventAuthenticateRequest))
			{
				requestNotifications |= RequestNotification.AuthenticateRequest;
			}
			if (this.HasEventSubscription(HttpApplication.EventPostAuthenticateRequest))
			{
				postRequestNotifications |= RequestNotification.AuthenticateRequest;
			}
			if (this.HasEventSubscription(HttpApplication.EventAuthorizeRequest))
			{
				requestNotifications |= RequestNotification.AuthorizeRequest;
			}
			if (this.HasEventSubscription(HttpApplication.EventPostAuthorizeRequest))
			{
				postRequestNotifications |= RequestNotification.AuthorizeRequest;
			}
			if (this.HasEventSubscription(HttpApplication.EventResolveRequestCache))
			{
				requestNotifications |= RequestNotification.ResolveRequestCache;
			}
			if (this.HasEventSubscription(HttpApplication.EventPostResolveRequestCache))
			{
				postRequestNotifications |= RequestNotification.ResolveRequestCache;
			}
			if (this.HasEventSubscription(HttpApplication.EventMapRequestHandler))
			{
				requestNotifications |= RequestNotification.MapRequestHandler;
			}
			if (this.HasEventSubscription(HttpApplication.EventPostMapRequestHandler))
			{
				postRequestNotifications |= RequestNotification.MapRequestHandler;
			}
			if (this.HasEventSubscription(HttpApplication.EventAcquireRequestState))
			{
				requestNotifications |= RequestNotification.AcquireRequestState;
			}
			if (this.HasEventSubscription(HttpApplication.EventPostAcquireRequestState))
			{
				postRequestNotifications |= RequestNotification.AcquireRequestState;
			}
			if (this.HasEventSubscription(HttpApplication.EventPreRequestHandlerExecute))
			{
				requestNotifications |= RequestNotification.PreExecuteRequestHandler;
			}
			if (this.HasEventSubscription(HttpApplication.EventPostRequestHandlerExecute))
			{
				postRequestNotifications |= RequestNotification.ExecuteRequestHandler;
			}
			if (this.HasEventSubscription(HttpApplication.EventReleaseRequestState))
			{
				requestNotifications |= RequestNotification.ReleaseRequestState;
			}
			if (this.HasEventSubscription(HttpApplication.EventPostReleaseRequestState))
			{
				postRequestNotifications |= RequestNotification.ReleaseRequestState;
			}
			if (this.HasEventSubscription(HttpApplication.EventUpdateRequestCache))
			{
				requestNotifications |= RequestNotification.UpdateRequestCache;
			}
			if (this.HasEventSubscription(HttpApplication.EventPostUpdateRequestCache))
			{
				postRequestNotifications |= RequestNotification.UpdateRequestCache;
			}
			if (this.HasEventSubscription(HttpApplication.EventLogRequest))
			{
				requestNotifications |= RequestNotification.LogRequest;
			}
			if (this.HasEventSubscription(HttpApplication.EventPostLogRequest))
			{
				postRequestNotifications |= RequestNotification.LogRequest;
			}
			if (this.HasEventSubscription(HttpApplication.EventEndRequest))
			{
				requestNotifications |= RequestNotification.EndRequest;
			}
			if (this.HasEventSubscription(HttpApplication.EventPreSendRequestHeaders))
			{
				requestNotifications |= RequestNotification.SendResponse;
			}
			if (this.HasEventSubscription(HttpApplication.EventPreSendRequestContent))
			{
				requestNotifications |= RequestNotification.SendResponse;
			}
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x0000F310 File Offset: 0x0000D510
		private bool HasEventSubscription(object eventIndex)
		{
			bool result = false;
			HttpApplication.AsyncAppEventHandler asyncAppEventHandler = this.AsyncEvents[eventIndex];
			if (asyncAppEventHandler != null && asyncAppEventHandler.Count > 0)
			{
				asyncAppEventHandler.Reset();
				result = true;
			}
			EventHandler eventHandler = (EventHandler)this.Events[eventIndex];
			if (eventHandler != null)
			{
				Delegate[] invocationList = eventHandler.GetInvocationList();
				if (invocationList.Length != 0)
				{
					result = true;
				}
				foreach (Delegate value in invocationList)
				{
					this.Events.RemoveHandler(eventIndex, value);
				}
			}
			return result;
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x0000F390 File Offset: 0x0000D590
		private void InitAppLevelCulture()
		{
			GlobalizationSection globalization = RuntimeConfig.GetAppConfig().Globalization;
			string culture = globalization.Culture;
			string uiculture = globalization.UICulture;
			if (!string.IsNullOrEmpty(culture))
			{
				if (StringUtil.StringStartsWithIgnoreCase(culture, HttpApplication.AutoCulture))
				{
					this._appLevelAutoCulture = true;
					string fallbackCulture = HttpApplication.GetFallbackCulture(culture);
					if (fallbackCulture != null)
					{
						this._appLevelCulture = HttpServerUtility.CreateReadOnlyCultureInfo(culture.Substring(5));
					}
				}
				else
				{
					this._appLevelAutoCulture = false;
					this._appLevelCulture = HttpServerUtility.CreateReadOnlyCultureInfo(globalization.Culture);
				}
			}
			if (!string.IsNullOrEmpty(uiculture))
			{
				if (StringUtil.StringStartsWithIgnoreCase(uiculture, HttpApplication.AutoCulture))
				{
					this._appLevelAutoUICulture = true;
					string fallbackCulture2 = HttpApplication.GetFallbackCulture(uiculture);
					if (fallbackCulture2 != null)
					{
						this._appLevelUICulture = HttpServerUtility.CreateReadOnlyCultureInfo(uiculture.Substring(5));
						return;
					}
				}
				else
				{
					this._appLevelAutoUICulture = false;
					this._appLevelUICulture = HttpServerUtility.CreateReadOnlyCultureInfo(globalization.UICulture);
				}
			}
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x0000F45B File Offset: 0x0000D65B
		internal static string GetFallbackCulture(string culture)
		{
			if (culture.Length > 5 && culture.IndexOf(':') == 4)
			{
				return culture.Substring(5);
			}
			return null;
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x0000F47C File Offset: 0x0000D67C
		private IHttpHandlerFactory GetFactory(HttpHandlerAction mapping)
		{
			HandlerFactoryCache handlerFactoryCache = (HandlerFactoryCache)this._handlerFactories[mapping.Type];
			if (handlerFactoryCache == null)
			{
				handlerFactoryCache = new HandlerFactoryCache(mapping);
				this._handlerFactories[mapping.Type] = handlerFactoryCache;
			}
			return handlerFactoryCache.Factory;
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x0000F4C4 File Offset: 0x0000D6C4
		private IHttpHandlerFactory GetFactory(string type)
		{
			HandlerFactoryCache handlerFactoryCache = (HandlerFactoryCache)this._handlerFactories[type];
			if (handlerFactoryCache == null)
			{
				handlerFactoryCache = new HandlerFactoryCache(type);
				this._handlerFactories[type] = handlerFactoryCache;
			}
			return handlerFactoryCache.Factory;
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x0000F500 File Offset: 0x0000D700
		private void RecycleHandlers()
		{
			if (this._handlerRecycleList != null)
			{
				int count = this._handlerRecycleList.Count;
				for (int i = 0; i < count; i++)
				{
					((HandlerWithFactory)this._handlerRecycleList[i]).Recycle();
				}
				this._handlerRecycleList = null;
			}
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x0000F54C File Offset: 0x0000D74C
		internal void AssignContext(HttpContext context)
		{
			if (this._context == null)
			{
				this._stepManager.InitRequest();
				this._context = context;
				this._context.ApplicationInstance = this;
				if (this._context.TraceIsEnabled)
				{
					HttpRuntime.Profile.StartRequest(this._context);
				}
				this._context.SetImpersonationEnabled();
			}
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x0000F5A8 File Offset: 0x0000D7A8
		internal IAsyncResult BeginProcessRequestNotification(HttpContext context, AsyncCallback cb)
		{
			if (this._context == null)
			{
				this.AssignContext(context);
			}
			context.CurrentModuleEventIndex = -1;
			HttpAsyncResult httpAsyncResult = new HttpAsyncResult(cb, context);
			context.NotificationContext.AsyncResult = httpAsyncResult;
			this.ResumeSteps(null);
			return httpAsyncResult;
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x0000F5E8 File Offset: 0x0000D7E8
		internal RequestNotificationStatus EndProcessRequestNotification(IAsyncResult result)
		{
			HttpAsyncResult httpAsyncResult = (HttpAsyncResult)result;
			if (httpAsyncResult.Error != null)
			{
				throw httpAsyncResult.Error;
			}
			return httpAsyncResult.Status;
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x0000F614 File Offset: 0x0000D814
		internal void ReleaseAppInstance()
		{
			if (this._context != null)
			{
				if (this._context.TraceIsEnabled)
				{
					HttpRuntime.Profile.EndRequest(this._context);
				}
				this._context.ClearReferences();
				if (this._timeoutManagerInitialized)
				{
					HttpRuntime.RequestTimeoutManager.Remove(this._context);
					this._timeoutManagerInitialized = false;
				}
				if (HttpRuntime.EnablePrefetchOptimization && HttpRuntime.InitializationException == null && this._context.FirstRequest && this._context.Error == null)
				{
					UnsafeNativeMethods.EndPrefetchActivity((uint)StringUtil.GetNonRandomizedHashCode(HttpRuntime.AppDomainAppId, false));
				}
			}
			this.RecycleHandlers();
			if (this.AsyncResult != null)
			{
				this.AsyncResult = null;
			}
			this._context = null;
			this.RaiseOnRequestCompleted();
			this.AppEvent = null;
			if (this.ApplicationInstanceConsumersCounter != null)
			{
				this.ApplicationInstanceConsumersCounter.MarkOperationCompleted();
				return;
			}
			HttpApplicationFactory.RecycleApplicationInstance(this);
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x0000F6F0 File Offset: 0x0000D8F0
		private void AddEventMapping(string moduleName, RequestNotification requestNotification, bool isPostNotification, HttpApplication.IExecutionStep step)
		{
			this.ThrowIfEventBindingDisallowed();
			if (!this.IsContainerInitalizationAllowed)
			{
				return;
			}
			PipelineModuleStepContainer moduleContainer = this.GetModuleContainer(moduleName);
			if (moduleContainer != null)
			{
				moduleContainer.AddEvent(requestNotification, isPostNotification, step);
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x060007A0 RID: 1952 RVA: 0x0000F721 File Offset: 0x0000D921
		internal static List<ModuleConfigurationInfo> IntegratedModuleList
		{
			get
			{
				return HttpApplication._moduleConfigInfo;
			}
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x0000F728 File Offset: 0x0000D928
		private HttpModuleCollection GetModuleCollection(IntPtr appContext)
		{
			if (HttpApplication._moduleConfigInfo != null)
			{
				return this.BuildIntegratedModuleCollection(HttpApplication._moduleConfigInfo);
			}
			List<ModuleConfigurationInfo> list = null;
			IntPtr zero = IntPtr.Zero;
			IntPtr zero2 = IntPtr.Zero;
			int num = 0;
			IntPtr zero3 = IntPtr.Zero;
			int num2 = 0;
			IntPtr zero4 = IntPtr.Zero;
			int num3 = 0;
			try
			{
				int num4 = 0;
				int num5 = UnsafeIISMethods.MgdGetModuleCollection(IntPtr.Zero, appContext, out zero, out num4);
				if (num5 < 0)
				{
					throw new HttpException(SR.GetString("Cant_Read_Native_Modules", new object[]
					{
						num5.ToString("X8", CultureInfo.InvariantCulture)
					}));
				}
				list = new List<ModuleConfigurationInfo>(num4);
				uint num6 = 0U;
				while ((ulong)num6 < (ulong)((long)num4))
				{
					num5 = UnsafeIISMethods.MgdGetNextModule(zero, ref num6, out zero2, out num, out zero3, out num2, out zero4, out num3);
					if (num5 < 0)
					{
						throw new HttpException(SR.GetString("Cant_Read_Native_Modules", new object[]
						{
							num5.ToString("X8", CultureInfo.InvariantCulture)
						}));
					}
					string text = (num > 0) ? StringUtil.StringFromWCharPtr(zero2, num) : null;
					string text2 = (num2 > 0) ? StringUtil.StringFromWCharPtr(zero3, num2) : null;
					string condition = (num3 > 0) ? StringUtil.StringFromWCharPtr(zero4, num3) : string.Empty;
					Marshal.FreeBSTR(zero2);
					zero2 = IntPtr.Zero;
					num = 0;
					Marshal.FreeBSTR(zero3);
					zero3 = IntPtr.Zero;
					num2 = 0;
					Marshal.FreeBSTR(zero4);
					zero4 = IntPtr.Zero;
					num3 = 0;
					if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2))
					{
						list.Add(new ModuleConfigurationInfo(text, text2, condition));
					}
					num6 += 1U;
				}
			}
			finally
			{
				if (zero != IntPtr.Zero)
				{
					Marshal.Release(zero);
					zero = IntPtr.Zero;
				}
				if (zero2 != IntPtr.Zero)
				{
					Marshal.FreeBSTR(zero2);
					zero2 = IntPtr.Zero;
				}
				if (zero3 != IntPtr.Zero)
				{
					Marshal.FreeBSTR(zero3);
					zero3 = IntPtr.Zero;
				}
				if (zero4 != IntPtr.Zero)
				{
					Marshal.FreeBSTR(zero4);
					zero4 = IntPtr.Zero;
				}
			}
			list.AddRange(this.GetConfigInfoForDynamicModules());
			HttpApplication._moduleConfigInfo = list;
			return this.BuildIntegratedModuleCollection(HttpApplication._moduleConfigInfo);
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x0000F94C File Offset: 0x0000DB4C
		private IEnumerable<ModuleConfigurationInfo> GetConfigInfoForDynamicModules()
		{
			return from entry in HttpApplication._dynamicModuleRegistry.LockAndFetchList()
			select new ModuleConfigurationInfo(entry.Name, entry.Type, "managedHandler");
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x0000F97C File Offset: 0x0000DB7C
		private HttpModuleCollection BuildIntegratedModuleCollection(List<ModuleConfigurationInfo> moduleList)
		{
			HttpModuleCollection httpModuleCollection = new HttpModuleCollection();
			foreach (ModuleConfigurationInfo moduleConfigurationInfo in moduleList)
			{
				ModulesEntry modulesEntry = new ModulesEntry(moduleConfigurationInfo.Name, moduleConfigurationInfo.Type, "type", null);
				httpModuleCollection.AddModule(modulesEntry.ModuleName, modulesEntry.Create());
			}
			return httpModuleCollection;
		}

		// Token: 0x04000233 RID: 563
		private HttpApplicationState _state;

		// Token: 0x04000234 RID: 564
		private HttpContext _initContext;

		// Token: 0x04000235 RID: 565
		private HttpAsyncResult _ar;

		// Token: 0x04000236 RID: 566
		private static readonly DynamicModuleRegistry _dynamicModuleRegistry = new DynamicModuleRegistry();

		// Token: 0x04000237 RID: 567
		private HttpModuleCollection _moduleCollection;

		// Token: 0x04000238 RID: 568
		private static readonly object EventDisposed = new object();

		// Token: 0x04000239 RID: 569
		private static readonly object EventErrorRecorded = new object();

		// Token: 0x0400023A RID: 570
		private static readonly object EventRequestCompleted = new object();

		// Token: 0x0400023B RID: 571
		private static readonly object EventPreSendRequestHeaders = new object();

		// Token: 0x0400023C RID: 572
		private static readonly object EventPreSendRequestContent = new object();

		// Token: 0x0400023D RID: 573
		private static readonly object EventBeginRequest = new object();

		// Token: 0x0400023E RID: 574
		private static readonly object EventAuthenticateRequest = new object();

		// Token: 0x0400023F RID: 575
		private static readonly object EventDefaultAuthentication = new object();

		// Token: 0x04000240 RID: 576
		private static readonly object EventPostAuthenticateRequest = new object();

		// Token: 0x04000241 RID: 577
		private static readonly object EventAuthorizeRequest = new object();

		// Token: 0x04000242 RID: 578
		private static readonly object EventPostAuthorizeRequest = new object();

		// Token: 0x04000243 RID: 579
		private static readonly object EventResolveRequestCache = new object();

		// Token: 0x04000244 RID: 580
		private static readonly object EventPostResolveRequestCache = new object();

		// Token: 0x04000245 RID: 581
		private static readonly object EventMapRequestHandler = new object();

		// Token: 0x04000246 RID: 582
		private static readonly object EventPostMapRequestHandler = new object();

		// Token: 0x04000247 RID: 583
		private static readonly object EventAcquireRequestState = new object();

		// Token: 0x04000248 RID: 584
		private static readonly object EventPostAcquireRequestState = new object();

		// Token: 0x04000249 RID: 585
		private static readonly object EventPreRequestHandlerExecute = new object();

		// Token: 0x0400024A RID: 586
		private static readonly object EventPostRequestHandlerExecute = new object();

		// Token: 0x0400024B RID: 587
		private static readonly object EventReleaseRequestState = new object();

		// Token: 0x0400024C RID: 588
		private static readonly object EventPostReleaseRequestState = new object();

		// Token: 0x0400024D RID: 589
		private static readonly object EventUpdateRequestCache = new object();

		// Token: 0x0400024E RID: 590
		private static readonly object EventPostUpdateRequestCache = new object();

		// Token: 0x0400024F RID: 591
		private static readonly object EventLogRequest = new object();

		// Token: 0x04000250 RID: 592
		private static readonly object EventPostLogRequest = new object();

		// Token: 0x04000251 RID: 593
		private static readonly object EventEndRequest = new object();

		// Token: 0x04000252 RID: 594
		internal static readonly string AutoCulture = "auto";

		// Token: 0x04000253 RID: 595
		private EventHandlerList _events;

		// Token: 0x04000254 RID: 596
		private HttpApplication.AsyncAppEventHandlersTable _asyncEvents;

		// Token: 0x04000255 RID: 597
		private HttpApplication.StepManager _stepManager;

		// Token: 0x04000256 RID: 598
		private WaitCallback _resumeStepsWaitCallback;

		// Token: 0x04000257 RID: 599
		private EventArgs _appEvent;

		// Token: 0x04000258 RID: 600
		private Hashtable _handlerFactories = new Hashtable();

		// Token: 0x04000259 RID: 601
		private ArrayList _handlerRecycleList;

		// Token: 0x0400025A RID: 602
		private bool _hideRequestResponse;

		// Token: 0x0400025B RID: 603
		private HttpContext _context;

		// Token: 0x0400025C RID: 604
		private Exception _lastError;

		// Token: 0x0400025D RID: 605
		private bool _timeoutManagerInitialized;

		// Token: 0x0400025E RID: 606
		private HttpSessionState _session;

		// Token: 0x0400025F RID: 607
		private CultureInfo _appLevelCulture;

		// Token: 0x04000260 RID: 608
		private CultureInfo _appLevelUICulture;

		// Token: 0x04000261 RID: 609
		private CultureInfo _savedAppLevelCulture;

		// Token: 0x04000262 RID: 610
		private CultureInfo _savedAppLevelUICulture;

		// Token: 0x04000263 RID: 611
		private bool _appLevelAutoCulture;

		// Token: 0x04000264 RID: 612
		private bool _appLevelAutoUICulture;

		// Token: 0x04000265 RID: 613
		private Dictionary<string, RequestNotification> _pipelineEventMasks;

		// Token: 0x04000266 RID: 614
		private ISite _site;

		// Token: 0x04000267 RID: 615
		internal const string MANAGED_PRECONDITION = "managedHandler";

		// Token: 0x04000268 RID: 616
		internal const string IMPLICIT_FILTER_MODULE = "AspNetFilterModule";

		// Token: 0x04000269 RID: 617
		internal const string IMPLICIT_HANDLER = "ManagedPipelineHandler";

		// Token: 0x0400026A RID: 618
		private static Hashtable _moduleIndexMap = new Hashtable();

		// Token: 0x0400026B RID: 619
		private static bool _initSpecialCompleted;

		// Token: 0x0400026C RID: 620
		private bool _initInternalCompleted;

		// Token: 0x0400026D RID: 621
		private RequestNotification _appRequestNotifications;

		// Token: 0x0400026E RID: 622
		private RequestNotification _appPostNotifications;

		// Token: 0x0400026F RID: 623
		private string _currentModuleCollectionKey = "global.asax";

		// Token: 0x04000270 RID: 624
		private static List<ModuleConfigurationInfo> _moduleConfigInfo;

		// Token: 0x04000271 RID: 625
		private PipelineModuleStepContainer[] _moduleContainers;

		// Token: 0x04000272 RID: 626
		private byte[] _entityBuffer;

		// Token: 0x04000273 RID: 627
		internal CountdownTask ApplicationInstanceConsumersCounter;

		// Token: 0x04000274 RID: 628
		private IAllocatorProvider _allocator;

		// Token: 0x04000275 RID: 629
		private HttpApplication.StepInvoker _stepInvoker;

		// Token: 0x020008C6 RID: 2246
		internal class CancelModuleException
		{
			// Token: 0x060067D4 RID: 26580 RVA: 0x00170B71 File Offset: 0x0016ED71
			internal CancelModuleException(bool timeout)
			{
				this._timeout = timeout;
			}

			// Token: 0x17001CDB RID: 7387
			// (get) Token: 0x060067D5 RID: 26581 RVA: 0x00170B80 File Offset: 0x0016ED80
			internal bool Timeout
			{
				get
				{
					return this._timeout;
				}
			}

			// Token: 0x040035FF RID: 13823
			private bool _timeout;
		}

		// Token: 0x020008C7 RID: 2247
		internal class AsyncAppEventHandler
		{
			// Token: 0x060067D6 RID: 26582 RVA: 0x00170B88 File Offset: 0x0016ED88
			internal AsyncAppEventHandler()
			{
				this._count = 0;
				this._beginHandlers = new ArrayList();
				this._endHandlers = new ArrayList();
				this._stateObjects = new ArrayList();
			}

			// Token: 0x060067D7 RID: 26583 RVA: 0x00170BB8 File Offset: 0x0016EDB8
			internal void Reset()
			{
				this._count = 0;
				this._beginHandlers.Clear();
				this._endHandlers.Clear();
				this._stateObjects.Clear();
			}

			// Token: 0x17001CDC RID: 7388
			// (get) Token: 0x060067D8 RID: 26584 RVA: 0x00170BE2 File Offset: 0x0016EDE2
			internal int Count
			{
				get
				{
					return this._count;
				}
			}

			// Token: 0x060067D9 RID: 26585 RVA: 0x00170BEA File Offset: 0x0016EDEA
			internal void Add(BeginEventHandler beginHandler, EndEventHandler endHandler, object state)
			{
				this._beginHandlers.Add(beginHandler);
				this._endHandlers.Add(endHandler);
				this._stateObjects.Add(state);
				this._count++;
			}

			// Token: 0x060067DA RID: 26586 RVA: 0x00170C24 File Offset: 0x0016EE24
			internal void CreateExecutionSteps(HttpApplication app, ArrayList steps)
			{
				for (int i = 0; i < this._count; i++)
				{
					steps.Add(new HttpApplication.AsyncEventExecutionStep(app, (BeginEventHandler)this._beginHandlers[i], (EndEventHandler)this._endHandlers[i], this._stateObjects[i]));
				}
			}

			// Token: 0x04003600 RID: 13824
			private int _count;

			// Token: 0x04003601 RID: 13825
			private ArrayList _beginHandlers;

			// Token: 0x04003602 RID: 13826
			private ArrayList _endHandlers;

			// Token: 0x04003603 RID: 13827
			private ArrayList _stateObjects;
		}

		// Token: 0x020008C8 RID: 2248
		internal class AsyncAppEventHandlersTable
		{
			// Token: 0x060067DB RID: 26587 RVA: 0x00170C80 File Offset: 0x0016EE80
			internal void AddHandler(object eventId, BeginEventHandler beginHandler, EndEventHandler endHandler, object state, RequestNotification requestNotification, bool isPost, HttpApplication app)
			{
				if (this._table == null)
				{
					this._table = new Hashtable();
				}
				HttpApplication.AsyncAppEventHandler asyncAppEventHandler = (HttpApplication.AsyncAppEventHandler)this._table[eventId];
				if (asyncAppEventHandler == null)
				{
					asyncAppEventHandler = new HttpApplication.AsyncAppEventHandler();
					this._table[eventId] = asyncAppEventHandler;
				}
				asyncAppEventHandler.Add(beginHandler, endHandler, state);
				if (HttpRuntime.UseIntegratedPipeline)
				{
					HttpApplication.AsyncEventExecutionStep step = new HttpApplication.AsyncEventExecutionStep(app, beginHandler, endHandler, state);
					app.AddEventMapping(app.CurrentModuleCollectionKey, requestNotification, isPost, step);
				}
			}

			// Token: 0x17001CDD RID: 7389
			internal HttpApplication.AsyncAppEventHandler this[object eventId]
			{
				get
				{
					if (this._table == null)
					{
						return null;
					}
					return (HttpApplication.AsyncAppEventHandler)this._table[eventId];
				}
			}

			// Token: 0x04003604 RID: 13828
			private Hashtable _table;
		}

		// Token: 0x020008C9 RID: 2249
		internal interface IExecutionStep
		{
			// Token: 0x060067DE RID: 26590
			void Execute();

			// Token: 0x17001CDE RID: 7390
			// (get) Token: 0x060067DF RID: 26591
			bool CompletedSynchronously { get; }

			// Token: 0x17001CDF RID: 7391
			// (get) Token: 0x060067E0 RID: 26592
			bool IsCancellable { get; }
		}

		// Token: 0x020008CA RID: 2250
		internal class NoopExecutionStep : HttpApplication.IExecutionStep
		{
			// Token: 0x060067E1 RID: 26593 RVA: 0x000030B5 File Offset: 0x000012B5
			internal NoopExecutionStep()
			{
			}

			// Token: 0x060067E2 RID: 26594 RVA: 0x00006164 File Offset: 0x00004364
			void HttpApplication.IExecutionStep.Execute()
			{
			}

			// Token: 0x17001CE0 RID: 7392
			// (get) Token: 0x060067E3 RID: 26595 RVA: 0x000097B7 File Offset: 0x000079B7
			bool HttpApplication.IExecutionStep.CompletedSynchronously
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001CE1 RID: 7393
			// (get) Token: 0x060067E4 RID: 26596 RVA: 0x00007722 File Offset: 0x00005922
			bool HttpApplication.IExecutionStep.IsCancellable
			{
				get
				{
					return false;
				}
			}
		}

		// Token: 0x020008CB RID: 2251
		internal class SyncEventExecutionStep : HttpApplication.IExecutionStep
		{
			// Token: 0x060067E5 RID: 26597 RVA: 0x00170D15 File Offset: 0x0016EF15
			internal SyncEventExecutionStep(HttpApplication app, EventHandler handler)
			{
				this._application = app;
				this._handler = handler;
			}

			// Token: 0x17001CE2 RID: 7394
			// (get) Token: 0x060067E6 RID: 26598 RVA: 0x00170D2B File Offset: 0x0016EF2B
			internal EventHandler Handler
			{
				get
				{
					return this._handler;
				}
			}

			// Token: 0x060067E7 RID: 26599 RVA: 0x00170D34 File Offset: 0x0016EF34
			void HttpApplication.IExecutionStep.Execute()
			{
				string data = null;
				if (this._handler != null)
				{
					if (EtwTrace.IsTraceEnabled(5, 2))
					{
						data = this._handler.Method.ReflectedType.ToString();
						EtwTrace.Trace(EtwTraceType.ETW_TYPE_PIPELINE_ENTER, this._application.Context.WorkerRequest, data);
					}
					this._handler(this._application, this._application.AppEvent);
					if (EtwTrace.IsTraceEnabled(5, 2))
					{
						EtwTrace.Trace(EtwTraceType.ETW_TYPE_PIPELINE_LEAVE, this._application.Context.WorkerRequest, data);
					}
				}
			}

			// Token: 0x17001CE3 RID: 7395
			// (get) Token: 0x060067E8 RID: 26600 RVA: 0x000097B7 File Offset: 0x000079B7
			bool HttpApplication.IExecutionStep.CompletedSynchronously
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001CE4 RID: 7396
			// (get) Token: 0x060067E9 RID: 26601 RVA: 0x000097B7 File Offset: 0x000079B7
			bool HttpApplication.IExecutionStep.IsCancellable
			{
				get
				{
					return true;
				}
			}

			// Token: 0x04003605 RID: 13829
			private HttpApplication _application;

			// Token: 0x04003606 RID: 13830
			private EventHandler _handler;
		}

		// Token: 0x020008CC RID: 2252
		internal class AsyncEventExecutionStep : HttpApplication.IExecutionStep
		{
			// Token: 0x060067EA RID: 26602 RVA: 0x00170DBF File Offset: 0x0016EFBF
			internal AsyncEventExecutionStep(HttpApplication app, BeginEventHandler beginHandler, EndEventHandler endHandler, object state) : this(app, beginHandler, endHandler, state, HttpRuntime.UseIntegratedPipeline)
			{
			}

			// Token: 0x060067EB RID: 26603 RVA: 0x00170DD4 File Offset: 0x0016EFD4
			internal AsyncEventExecutionStep(HttpApplication app, BeginEventHandler beginHandler, EndEventHandler endHandler, object state, bool useIntegratedPipeline)
			{
				this._application = app;
				this._beginHandler = AppVerifier.WrapBeginMethod(this._application, beginHandler);
				this._endHandler = endHandler;
				this._state = state;
				this._completionCallback = new AsyncCallback(this.OnAsyncEventCompletion);
			}

			// Token: 0x060067EC RID: 26604 RVA: 0x00170E24 File Offset: 0x0016F024
			private void OnAsyncEventCompletion(IAsyncResult ar)
			{
				if (ar.CompletedSynchronously)
				{
					return;
				}
				HttpContext context = this._application.Context;
				Exception error = null;
				context.SyncContext.ProhibitVoidAsyncOperations();
				try
				{
					this.InvokeEndHandler(ar);
				}
				catch (Exception ex)
				{
					error = ex;
				}
				if (!this._asyncStepCompletionInfo.RegisterAsyncCompletion(error))
				{
					return;
				}
				if (EtwTrace.IsTraceEnabled(5, 2))
				{
					EtwTrace.Trace(EtwTraceType.ETW_TYPE_PIPELINE_LEAVE, context.WorkerRequest, this._targetTypeStr);
				}
				context.SetStartTime();
				if (HttpRuntime.IsLegacyCas)
				{
					this.ResumeStepsWithAssert(error);
					return;
				}
				this.ResumeSteps(error);
			}

			// Token: 0x060067ED RID: 26605 RVA: 0x00170EBC File Offset: 0x0016F0BC
			private void InvokeEndHandler(IAsyncResult ar)
			{
				if (this._application._stepInvoker != null)
				{
					bool stepCalled = false;
					this._application._stepInvoker.Invoke(delegate
					{
						if (!stepCalled)
						{
							stepCalled = true;
							this._endHandler(ar);
						}
					});
					if (!stepCalled)
					{
						this._endHandler(ar);
						return;
					}
				}
				else
				{
					this._endHandler(ar);
				}
			}

			// Token: 0x060067EE RID: 26606 RVA: 0x00170F38 File Offset: 0x0016F138
			[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
			private void ResumeStepsWithAssert(Exception error)
			{
				this.ResumeSteps(error);
			}

			// Token: 0x060067EF RID: 26607 RVA: 0x00170F41 File Offset: 0x0016F141
			private void ResumeSteps(Exception error)
			{
				this._application.ResumeStepsFromThreadPoolThread(error);
			}

			// Token: 0x060067F0 RID: 26608 RVA: 0x00170F50 File Offset: 0x0016F150
			void HttpApplication.IExecutionStep.Execute()
			{
				this._sync = false;
				if (EtwTrace.IsTraceEnabled(5, 2))
				{
					this._targetTypeStr = this._beginHandler.Method.ReflectedType.ToString();
					EtwTrace.Trace(EtwTraceType.ETW_TYPE_PIPELINE_ENTER, this._application.Context.WorkerRequest, this._targetTypeStr);
				}
				HttpContext context = this._application.Context;
				this._asyncStepCompletionInfo.Reset();
				context.SyncContext.AllowVoidAsyncOperations();
				IAsyncResult asyncResult;
				try
				{
					asyncResult = this._beginHandler(this._application, this._application.AppEvent, this._completionCallback, this._state);
				}
				catch
				{
					context.SyncContext.ProhibitVoidAsyncOperations();
					throw;
				}
				bool flag;
				bool flag2;
				this._asyncStepCompletionInfo.RegisterBeginUnwound(asyncResult, out flag, out flag2);
				if (flag)
				{
					this._sync = true;
					if (flag2)
					{
						context.SyncContext.ProhibitVoidAsyncOperations();
						this._endHandler(asyncResult);
					}
					this._asyncStepCompletionInfo.ReportError();
					if (EtwTrace.IsTraceEnabled(5, 2))
					{
						EtwTrace.Trace(EtwTraceType.ETW_TYPE_PIPELINE_LEAVE, this._application.Context.WorkerRequest, this._targetTypeStr);
					}
				}
			}

			// Token: 0x17001CE5 RID: 7397
			// (get) Token: 0x060067F1 RID: 26609 RVA: 0x00171078 File Offset: 0x0016F278
			bool HttpApplication.IExecutionStep.CompletedSynchronously
			{
				get
				{
					return this._sync;
				}
			}

			// Token: 0x17001CE6 RID: 7398
			// (get) Token: 0x060067F2 RID: 26610 RVA: 0x00007722 File Offset: 0x00005922
			bool HttpApplication.IExecutionStep.IsCancellable
			{
				get
				{
					return false;
				}
			}

			// Token: 0x04003607 RID: 13831
			private HttpApplication _application;

			// Token: 0x04003608 RID: 13832
			private BeginEventHandler _beginHandler;

			// Token: 0x04003609 RID: 13833
			private EndEventHandler _endHandler;

			// Token: 0x0400360A RID: 13834
			private object _state;

			// Token: 0x0400360B RID: 13835
			private AsyncCallback _completionCallback;

			// Token: 0x0400360C RID: 13836
			private HttpApplication.AsyncStepCompletionInfo _asyncStepCompletionInfo;

			// Token: 0x0400360D RID: 13837
			private bool _sync;

			// Token: 0x0400360E RID: 13838
			private string _targetTypeStr;
		}

		// Token: 0x020008CD RID: 2253
		internal class ValidatePathExecutionStep : HttpApplication.IExecutionStep
		{
			// Token: 0x060067F3 RID: 26611 RVA: 0x00171080 File Offset: 0x0016F280
			internal ValidatePathExecutionStep(HttpApplication app)
			{
				this._application = app;
			}

			// Token: 0x060067F4 RID: 26612 RVA: 0x0017108F File Offset: 0x0016F28F
			void HttpApplication.IExecutionStep.Execute()
			{
				this._application.Context.ValidatePath();
			}

			// Token: 0x17001CE7 RID: 7399
			// (get) Token: 0x060067F5 RID: 26613 RVA: 0x000097B7 File Offset: 0x000079B7
			bool HttpApplication.IExecutionStep.CompletedSynchronously
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001CE8 RID: 7400
			// (get) Token: 0x060067F6 RID: 26614 RVA: 0x00007722 File Offset: 0x00005922
			bool HttpApplication.IExecutionStep.IsCancellable
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0400360F RID: 13839
			private HttpApplication _application;
		}

		// Token: 0x020008CE RID: 2254
		internal class ValidateRequestExecutionStep : HttpApplication.IExecutionStep
		{
			// Token: 0x060067F7 RID: 26615 RVA: 0x001710A1 File Offset: 0x0016F2A1
			internal ValidateRequestExecutionStep(HttpApplication app)
			{
				this._application = app;
			}

			// Token: 0x060067F8 RID: 26616 RVA: 0x001710B0 File Offset: 0x0016F2B0
			void HttpApplication.IExecutionStep.Execute()
			{
				this._application.Context.Request.ValidateInputIfRequiredByConfig();
			}

			// Token: 0x17001CE9 RID: 7401
			// (get) Token: 0x060067F9 RID: 26617 RVA: 0x000097B7 File Offset: 0x000079B7
			bool HttpApplication.IExecutionStep.CompletedSynchronously
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001CEA RID: 7402
			// (get) Token: 0x060067FA RID: 26618 RVA: 0x00007722 File Offset: 0x00005922
			bool HttpApplication.IExecutionStep.IsCancellable
			{
				get
				{
					return false;
				}
			}

			// Token: 0x04003610 RID: 13840
			private HttpApplication _application;
		}

		// Token: 0x020008CF RID: 2255
		internal class MaterializeHandlerExecutionStep : HttpApplication.IExecutionStep
		{
			// Token: 0x060067FB RID: 26619 RVA: 0x001710C7 File Offset: 0x0016F2C7
			internal MaterializeHandlerExecutionStep(HttpApplication app)
			{
				this._application = app;
			}

			// Token: 0x060067FC RID: 26620 RVA: 0x001710D8 File Offset: 0x0016F2D8
			void HttpApplication.IExecutionStep.Execute()
			{
				HttpContext context = this._application.Context;
				HttpRequest request = context.Request;
				IHttpHandler handler = null;
				string text = null;
				if (EtwTrace.IsTraceEnabled(5, 1))
				{
					EtwTrace.Trace(EtwTraceType.ETW_TYPE_MAPHANDLER_ENTER, context.WorkerRequest);
				}
				IIS7WorkerRequest iis7WorkerRequest = context.WorkerRequest as IIS7WorkerRequest;
				if (context.RemapHandlerInstance != null)
				{
					iis7WorkerRequest.SetScriptMapForRemapHandler();
					context.Handler = context.RemapHandlerInstance;
				}
				else if (request.RewrittenUrl != null)
				{
					bool flag;
					text = iis7WorkerRequest.ReMapHandlerAndGetHandlerTypeString(context, request.Path, out flag);
					if (!flag)
					{
						throw new HttpException(404, SR.GetString("Http_handler_not_found_for_request_type", new object[]
						{
							request.RequestType
						}));
					}
				}
				else
				{
					text = iis7WorkerRequest.GetManagedHandlerType();
				}
				if (!string.IsNullOrEmpty(text))
				{
					IHttpHandlerFactory factory = this._application.GetFactory(text);
					string physicalPathInternal = request.PhysicalPathInternal;
					try
					{
						handler = factory.GetHandler(context, request.RequestType, request.FilePath, physicalPathInternal);
					}
					catch (FileNotFoundException innerException)
					{
						if (HttpRuntime.HasPathDiscoveryPermission(physicalPathInternal))
						{
							throw new HttpException(404, null, innerException);
						}
						throw new HttpException(404, null);
					}
					catch (DirectoryNotFoundException innerException2)
					{
						if (HttpRuntime.HasPathDiscoveryPermission(physicalPathInternal))
						{
							throw new HttpException(404, null, innerException2);
						}
						throw new HttpException(404, null);
					}
					catch (PathTooLongException innerException3)
					{
						if (HttpRuntime.HasPathDiscoveryPermission(physicalPathInternal))
						{
							throw new HttpException(414, null, innerException3);
						}
						throw new HttpException(414, null);
					}
					context.Handler = handler;
					if (this._application._handlerRecycleList == null)
					{
						this._application._handlerRecycleList = new ArrayList();
					}
					this._application._handlerRecycleList.Add(new HandlerWithFactory(handler, factory));
				}
				if (EtwTrace.IsTraceEnabled(5, 1))
				{
					EtwTrace.Trace(EtwTraceType.ETW_TYPE_MAPHANDLER_LEAVE, context.WorkerRequest);
				}
			}

			// Token: 0x17001CEB RID: 7403
			// (get) Token: 0x060067FD RID: 26621 RVA: 0x000097B7 File Offset: 0x000079B7
			bool HttpApplication.IExecutionStep.CompletedSynchronously
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001CEC RID: 7404
			// (get) Token: 0x060067FE RID: 26622 RVA: 0x00007722 File Offset: 0x00005922
			bool HttpApplication.IExecutionStep.IsCancellable
			{
				get
				{
					return false;
				}
			}

			// Token: 0x04003611 RID: 13841
			private HttpApplication _application;
		}

		// Token: 0x020008D0 RID: 2256
		internal class MapHandlerExecutionStep : HttpApplication.IExecutionStep
		{
			// Token: 0x060067FF RID: 26623 RVA: 0x001712AC File Offset: 0x0016F4AC
			internal MapHandlerExecutionStep(HttpApplication app)
			{
				this._application = app;
			}

			// Token: 0x06006800 RID: 26624 RVA: 0x001712BC File Offset: 0x0016F4BC
			void HttpApplication.IExecutionStep.Execute()
			{
				HttpContext context = this._application.Context;
				HttpRequest request = context.Request;
				if (EtwTrace.IsTraceEnabled(5, 1))
				{
					EtwTrace.Trace(EtwTraceType.ETW_TYPE_MAPHANDLER_ENTER, context.WorkerRequest);
				}
				context.Handler = this._application.MapHttpHandler(context, request.RequestType, request.FilePathObject, request.PhysicalPathInternal, false);
				if (EtwTrace.IsTraceEnabled(5, 1))
				{
					EtwTrace.Trace(EtwTraceType.ETW_TYPE_MAPHANDLER_LEAVE, context.WorkerRequest);
				}
			}

			// Token: 0x17001CED RID: 7405
			// (get) Token: 0x06006801 RID: 26625 RVA: 0x000097B7 File Offset: 0x000079B7
			bool HttpApplication.IExecutionStep.CompletedSynchronously
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001CEE RID: 7406
			// (get) Token: 0x06006802 RID: 26626 RVA: 0x00007722 File Offset: 0x00005922
			bool HttpApplication.IExecutionStep.IsCancellable
			{
				get
				{
					return false;
				}
			}

			// Token: 0x04003612 RID: 13842
			private HttpApplication _application;
		}

		// Token: 0x020008D1 RID: 2257
		internal class CallHandlerExecutionStep : HttpApplication.IExecutionStep
		{
			// Token: 0x06006803 RID: 26627 RVA: 0x0017132D File Offset: 0x0016F52D
			internal CallHandlerExecutionStep(HttpApplication app)
			{
				this._application = app;
				this._completionCallback = new AsyncCallback(this.OnAsyncHandlerCompletion);
			}

			// Token: 0x06006804 RID: 26628 RVA: 0x00171350 File Offset: 0x0016F550
			private void OnAsyncHandlerCompletion(IAsyncResult ar)
			{
				if (ar.CompletedSynchronously)
				{
					return;
				}
				HttpContext context = this._application.Context;
				Exception error = null;
				context.SyncContext.ProhibitVoidAsyncOperations();
				try
				{
					try
					{
						this.InvokeEndHandler(ar);
					}
					finally
					{
						HttpApplication.CallHandlerExecutionStep.SuppressPostEndRequestIfNecessary(context);
						context.Response.GenerateResponseHeadersForHandler();
					}
				}
				catch (Exception ex)
				{
					if (ex is ThreadAbortException || (ex.InnerException != null && ex.InnerException is ThreadAbortException))
					{
						this._application.CompleteRequest();
					}
					else
					{
						error = ex;
					}
				}
				if (!this._asyncStepCompletionInfo.RegisterAsyncCompletion(error))
				{
					return;
				}
				if (EtwTrace.IsTraceEnabled(4, 4))
				{
					EtwTrace.Trace(EtwTraceType.ETW_TYPE_HTTPHANDLER_LEAVE, context.WorkerRequest);
				}
				this._handler = null;
				context.SetStartTime();
				if (HttpRuntime.IsLegacyCas)
				{
					this.ResumeStepsWithAssert(error);
					return;
				}
				this.ResumeSteps(error);
			}

			// Token: 0x06006805 RID: 26629 RVA: 0x00171434 File Offset: 0x0016F634
			private void InvokeEndHandler(IAsyncResult ar)
			{
				if (this._application._stepInvoker != null)
				{
					bool stepCalled = false;
					this._application._stepInvoker.Invoke(delegate
					{
						if (!stepCalled)
						{
							stepCalled = true;
							this._handler.EndProcessRequest(ar);
						}
					});
					if (!stepCalled)
					{
						this._handler.EndProcessRequest(ar);
						return;
					}
				}
				else
				{
					this._handler.EndProcessRequest(ar);
				}
			}

			// Token: 0x06006806 RID: 26630 RVA: 0x001714B0 File Offset: 0x0016F6B0
			[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
			private void ResumeStepsWithAssert(Exception error)
			{
				this.ResumeSteps(error);
			}

			// Token: 0x06006807 RID: 26631 RVA: 0x001714B9 File Offset: 0x0016F6B9
			private void ResumeSteps(Exception error)
			{
				this._application.ResumeStepsFromThreadPoolThread(error);
			}

			// Token: 0x06006808 RID: 26632 RVA: 0x001714C8 File Offset: 0x0016F6C8
			private static void SuppressPostEndRequestIfNecessary(HttpContext context)
			{
				if (!context.IsWebSocketRequestUpgrading)
				{
					IIS7WorkerRequest iis7WorkerRequest = context.WorkerRequest as IIS7WorkerRequest;
					if (iis7WorkerRequest != null)
					{
						iis7WorkerRequest.DisableNotifications((RequestNotification)0, RequestNotification.EndRequest);
					}
				}
			}

			// Token: 0x06006809 RID: 26633 RVA: 0x001714F8 File Offset: 0x0016F6F8
			void HttpApplication.IExecutionStep.Execute()
			{
				HttpContext context = this._application.Context;
				IHttpHandler handler = context.Handler;
				if (EtwTrace.IsTraceEnabled(4, 4))
				{
					EtwTrace.Trace(EtwTraceType.ETW_TYPE_HTTPHANDLER_ENTER, context.WorkerRequest);
				}
				if (handler != null && HttpRuntime.UseIntegratedPipeline)
				{
					IIS7WorkerRequest iis7WorkerRequest = context.WorkerRequest as IIS7WorkerRequest;
					if (iis7WorkerRequest != null && iis7WorkerRequest.IsHandlerExecutionDenied())
					{
						this._sync = true;
						HttpException ex = new HttpException(403, SR.GetString("Handler_access_denied"));
						ex.SetFormatter(new PageForbiddenErrorFormatter(context.Request.Path, SR.GetString("Handler_access_denied")));
						throw ex;
					}
				}
				if (handler == null)
				{
					this._sync = true;
					return;
				}
				if (handler is IHttpAsyncHandler)
				{
					IHttpAsyncHandler httpAsyncHandler = (IHttpAsyncHandler)handler;
					this._sync = false;
					this._handler = httpAsyncHandler;
					Func<HttpContext, AsyncCallback, object, IAsyncResult> func = AppVerifier.WrapBeginMethod<HttpContext>(this._application, new Func<HttpContext, AsyncCallback, object, IAsyncResult>(httpAsyncHandler.BeginProcessRequest));
					this._asyncStepCompletionInfo.Reset();
					context.SyncContext.AllowVoidAsyncOperations();
					IAsyncResult asyncResult;
					try
					{
						asyncResult = func(context, this._completionCallback, null);
					}
					catch
					{
						context.SyncContext.ProhibitVoidAsyncOperations();
						throw;
					}
					bool flag;
					bool flag2;
					this._asyncStepCompletionInfo.RegisterBeginUnwound(asyncResult, out flag, out flag2);
					if (flag)
					{
						this._sync = true;
						this._handler = null;
						context.SyncContext.ProhibitVoidAsyncOperations();
						try
						{
							if (flag2)
							{
								httpAsyncHandler.EndProcessRequest(asyncResult);
							}
							this._asyncStepCompletionInfo.ReportError();
						}
						finally
						{
							HttpApplication.CallHandlerExecutionStep.SuppressPostEndRequestIfNecessary(context);
							context.Response.GenerateResponseHeadersForHandler();
						}
						if (EtwTrace.IsTraceEnabled(4, 4))
						{
							EtwTrace.Trace(EtwTraceType.ETW_TYPE_HTTPHANDLER_LEAVE, context.WorkerRequest);
							return;
						}
					}
				}
				else
				{
					this._sync = true;
					context.SyncContext.SetSyncCaller();
					try
					{
						handler.ProcessRequest(context);
					}
					finally
					{
						context.SyncContext.ResetSyncCaller();
						if (EtwTrace.IsTraceEnabled(4, 4))
						{
							EtwTrace.Trace(EtwTraceType.ETW_TYPE_HTTPHANDLER_LEAVE, context.WorkerRequest);
						}
						HttpApplication.CallHandlerExecutionStep.SuppressPostEndRequestIfNecessary(context);
						context.Response.GenerateResponseHeadersForHandler();
					}
				}
			}

			// Token: 0x17001CEF RID: 7407
			// (get) Token: 0x0600680A RID: 26634 RVA: 0x001716F8 File Offset: 0x0016F8F8
			bool HttpApplication.IExecutionStep.CompletedSynchronously
			{
				get
				{
					return this._sync;
				}
			}

			// Token: 0x17001CF0 RID: 7408
			// (get) Token: 0x0600680B RID: 26635 RVA: 0x00171700 File Offset: 0x0016F900
			bool HttpApplication.IExecutionStep.IsCancellable
			{
				get
				{
					return !(this._application.Context.Handler is IHttpAsyncHandler);
				}
			}

			// Token: 0x04003613 RID: 13843
			private HttpApplication _application;

			// Token: 0x04003614 RID: 13844
			private AsyncCallback _completionCallback;

			// Token: 0x04003615 RID: 13845
			private IHttpAsyncHandler _handler;

			// Token: 0x04003616 RID: 13846
			private HttpApplication.AsyncStepCompletionInfo _asyncStepCompletionInfo;

			// Token: 0x04003617 RID: 13847
			private bool _sync;
		}

		// Token: 0x020008D2 RID: 2258
		internal class TransitionToWebSocketsExecutionStep : HttpApplication.IExecutionStep
		{
			// Token: 0x0600680C RID: 26636 RVA: 0x0017171C File Offset: 0x0016F91C
			internal TransitionToWebSocketsExecutionStep(HttpApplication app)
			{
				this._application = app;
			}

			// Token: 0x0600680D RID: 26637 RVA: 0x0017172C File Offset: 0x0016F92C
			void HttpApplication.IExecutionStep.Execute()
			{
				HttpContext context = this._application.Context;
				if (context.RootedObjects == null || context.RootedObjects.WebSocketPipeline == null || context.Response.StatusCode != 101)
				{
					this.CompletedSynchronously = true;
					return;
				}
				context.Request.StoreReferenceToResponseCookies(context.Response.GetCookiesNoCreate());
				context.TransitionToWebSocketState(WebSocketTransitionState.TransitionStarted);
				this.CompletedSynchronously = false;
			}

			// Token: 0x17001CF1 RID: 7409
			// (get) Token: 0x0600680E RID: 26638 RVA: 0x00171795 File Offset: 0x0016F995
			// (set) Token: 0x0600680F RID: 26639 RVA: 0x0017179D File Offset: 0x0016F99D
			public bool CompletedSynchronously { get; private set; }

			// Token: 0x17001CF2 RID: 7410
			// (get) Token: 0x06006810 RID: 26640 RVA: 0x00007722 File Offset: 0x00005922
			public bool IsCancellable
			{
				get
				{
					return false;
				}
			}

			// Token: 0x04003618 RID: 13848
			private readonly HttpApplication _application;
		}

		// Token: 0x020008D3 RID: 2259
		internal class CallFilterExecutionStep : HttpApplication.IExecutionStep
		{
			// Token: 0x06006811 RID: 26641 RVA: 0x001717A6 File Offset: 0x0016F9A6
			internal CallFilterExecutionStep(HttpApplication app)
			{
				this._application = app;
			}

			// Token: 0x06006812 RID: 26642 RVA: 0x001717B8 File Offset: 0x0016F9B8
			void HttpApplication.IExecutionStep.Execute()
			{
				try
				{
					this._application.Context.Response.FilterOutput();
				}
				finally
				{
					if (HttpRuntime.UseIntegratedPipeline && this._application.Context.CurrentNotification == RequestNotification.UpdateRequestCache)
					{
						this._application.Context.DisableNotifications(RequestNotification.LogRequest, (RequestNotification)0);
					}
				}
			}

			// Token: 0x17001CF3 RID: 7411
			// (get) Token: 0x06006813 RID: 26643 RVA: 0x000097B7 File Offset: 0x000079B7
			bool HttpApplication.IExecutionStep.CompletedSynchronously
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001CF4 RID: 7412
			// (get) Token: 0x06006814 RID: 26644 RVA: 0x000097B7 File Offset: 0x000079B7
			bool HttpApplication.IExecutionStep.IsCancellable
			{
				get
				{
					return true;
				}
			}

			// Token: 0x0400361A RID: 13850
			private HttpApplication _application;
		}

		// Token: 0x020008D4 RID: 2260
		internal class SendResponseExecutionStep : HttpApplication.IExecutionStep
		{
			// Token: 0x06006815 RID: 26645 RVA: 0x00171824 File Offset: 0x0016FA24
			internal SendResponseExecutionStep(HttpApplication app, EventHandler handler, bool isHeaders)
			{
				this._application = app;
				this._handler = handler;
				this._isHeaders = isHeaders;
			}

			// Token: 0x06006816 RID: 26646 RVA: 0x00171844 File Offset: 0x0016FA44
			void HttpApplication.IExecutionStep.Execute()
			{
				if ((this._application.Context.IsSendResponseHeaders && this._isHeaders) || !this._isHeaders)
				{
					string data = null;
					if (this._handler != null)
					{
						if (EtwTrace.IsTraceEnabled(5, 2))
						{
							data = this._handler.Method.ReflectedType.ToString();
							EtwTrace.Trace(EtwTraceType.ETW_TYPE_PIPELINE_ENTER, this._application.Context.WorkerRequest, data);
						}
						this._handler(this._application, this._application.AppEvent);
						if (EtwTrace.IsTraceEnabled(5, 2))
						{
							EtwTrace.Trace(EtwTraceType.ETW_TYPE_PIPELINE_LEAVE, this._application.Context.WorkerRequest, data);
						}
					}
				}
			}

			// Token: 0x17001CF5 RID: 7413
			// (get) Token: 0x06006817 RID: 26647 RVA: 0x000097B7 File Offset: 0x000079B7
			bool HttpApplication.IExecutionStep.CompletedSynchronously
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001CF6 RID: 7414
			// (get) Token: 0x06006818 RID: 26648 RVA: 0x000097B7 File Offset: 0x000079B7
			bool HttpApplication.IExecutionStep.IsCancellable
			{
				get
				{
					return true;
				}
			}

			// Token: 0x0400361B RID: 13851
			private HttpApplication _application;

			// Token: 0x0400361C RID: 13852
			private EventHandler _handler;

			// Token: 0x0400361D RID: 13853
			private bool _isHeaders;
		}

		// Token: 0x020008D5 RID: 2261
		internal class UrlMappingsExecutionStep : HttpApplication.IExecutionStep
		{
			// Token: 0x06006819 RID: 26649 RVA: 0x001718F1 File Offset: 0x0016FAF1
			internal UrlMappingsExecutionStep(HttpApplication app)
			{
				this._application = app;
			}

			// Token: 0x0600681A RID: 26650 RVA: 0x00171900 File Offset: 0x0016FB00
			void HttpApplication.IExecutionStep.Execute()
			{
				HttpContext context = this._application.Context;
				UrlMappingsModule.UrlMappingRewritePath(context);
			}

			// Token: 0x17001CF7 RID: 7415
			// (get) Token: 0x0600681B RID: 26651 RVA: 0x000097B7 File Offset: 0x000079B7
			bool HttpApplication.IExecutionStep.CompletedSynchronously
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001CF8 RID: 7416
			// (get) Token: 0x0600681C RID: 26652 RVA: 0x00007722 File Offset: 0x00005922
			bool HttpApplication.IExecutionStep.IsCancellable
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0400361E RID: 13854
			private HttpApplication _application;
		}

		// Token: 0x020008D6 RID: 2262
		internal abstract class StepManager
		{
			// Token: 0x0600681D RID: 26653 RVA: 0x0017191F File Offset: 0x0016FB1F
			internal StepManager(HttpApplication application)
			{
				this._application = application;
			}

			// Token: 0x17001CF9 RID: 7417
			// (get) Token: 0x0600681E RID: 26654 RVA: 0x0017192E File Offset: 0x0016FB2E
			internal bool IsCompleted
			{
				get
				{
					return this._requestCompleted;
				}
			}

			// Token: 0x0600681F RID: 26655
			internal abstract void BuildSteps(WaitCallback stepCallback);

			// Token: 0x06006820 RID: 26656 RVA: 0x00171938 File Offset: 0x0016FB38
			internal void CompleteRequest()
			{
				this._requestCompleted = true;
				if (HttpRuntime.UseIntegratedPipeline)
				{
					HttpContext context = this._application.Context;
					if (context != null && context.NotificationContext != null)
					{
						context.NotificationContext.RequestCompleted = true;
					}
				}
			}

			// Token: 0x06006821 RID: 26657
			internal abstract void InitRequest();

			// Token: 0x06006822 RID: 26658
			internal abstract void ResumeSteps(Exception error);

			// Token: 0x0400361F RID: 13855
			protected HttpApplication _application;

			// Token: 0x04003620 RID: 13856
			protected bool _requestCompleted;
		}

		// Token: 0x020008D7 RID: 2263
		internal class ApplicationStepManager : HttpApplication.StepManager
		{
			// Token: 0x06006823 RID: 26659 RVA: 0x00171976 File Offset: 0x0016FB76
			internal ApplicationStepManager(HttpApplication app) : base(app)
			{
			}

			// Token: 0x06006824 RID: 26660 RVA: 0x00171980 File Offset: 0x0016FB80
			internal override void BuildSteps(WaitCallback stepCallback)
			{
				ArrayList arrayList = new ArrayList();
				HttpApplication application = this._application;
				UrlMappingsSection urlMappings = RuntimeConfig.GetConfig().UrlMappings;
				bool flag = urlMappings.IsEnabled && urlMappings.UrlMappings.Count > 0;
				arrayList.Add(new HttpApplication.ValidateRequestExecutionStep(application));
				arrayList.Add(new HttpApplication.ValidatePathExecutionStep(application));
				if (flag)
				{
					arrayList.Add(new HttpApplication.UrlMappingsExecutionStep(application));
				}
				application.CreateEventExecutionSteps(HttpApplication.EventBeginRequest, arrayList);
				application.CreateEventExecutionSteps(HttpApplication.EventAuthenticateRequest, arrayList);
				application.CreateEventExecutionSteps(HttpApplication.EventDefaultAuthentication, arrayList);
				application.CreateEventExecutionSteps(HttpApplication.EventPostAuthenticateRequest, arrayList);
				application.CreateEventExecutionSteps(HttpApplication.EventAuthorizeRequest, arrayList);
				application.CreateEventExecutionSteps(HttpApplication.EventPostAuthorizeRequest, arrayList);
				application.CreateEventExecutionSteps(HttpApplication.EventResolveRequestCache, arrayList);
				application.CreateEventExecutionSteps(HttpApplication.EventPostResolveRequestCache, arrayList);
				arrayList.Add(new HttpApplication.MapHandlerExecutionStep(application));
				application.CreateEventExecutionSteps(HttpApplication.EventPostMapRequestHandler, arrayList);
				application.CreateEventExecutionSteps(HttpApplication.EventAcquireRequestState, arrayList);
				application.CreateEventExecutionSteps(HttpApplication.EventPostAcquireRequestState, arrayList);
				application.CreateEventExecutionSteps(HttpApplication.EventPreRequestHandlerExecute, arrayList);
				arrayList.Add(application.CreateImplicitAsyncPreloadExecutionStep());
				arrayList.Add(new HttpApplication.CallHandlerExecutionStep(application));
				application.CreateEventExecutionSteps(HttpApplication.EventPostRequestHandlerExecute, arrayList);
				application.CreateEventExecutionSteps(HttpApplication.EventReleaseRequestState, arrayList);
				application.CreateEventExecutionSteps(HttpApplication.EventPostReleaseRequestState, arrayList);
				arrayList.Add(new HttpApplication.CallFilterExecutionStep(application));
				application.CreateEventExecutionSteps(HttpApplication.EventUpdateRequestCache, arrayList);
				application.CreateEventExecutionSteps(HttpApplication.EventPostUpdateRequestCache, arrayList);
				this._endRequestStepIndex = arrayList.Count;
				application.CreateEventExecutionSteps(HttpApplication.EventEndRequest, arrayList);
				arrayList.Add(new HttpApplication.NoopExecutionStep());
				this._execSteps = new HttpApplication.IExecutionStep[arrayList.Count];
				arrayList.CopyTo(this._execSteps);
				this._resumeStepsWaitCallback = stepCallback;
			}

			// Token: 0x06006825 RID: 26661 RVA: 0x00171B33 File Offset: 0x0016FD33
			internal override void InitRequest()
			{
				this._currentStepIndex = -1;
				this._numStepCalls = 0;
				this._numSyncStepCalls = 0;
				this._requestCompleted = false;
			}

			// Token: 0x06006826 RID: 26662 RVA: 0x00171B54 File Offset: 0x0016FD54
			[DebuggerStepperBoundary]
			internal override void ResumeSteps(Exception error)
			{
				bool flag = false;
				bool flag2 = true;
				HttpApplication application = this._application;
				CountdownTask applicationInstanceConsumersCounter = application.ApplicationInstanceConsumersCounter;
				HttpContext context = application.Context;
				ThreadContext threadContext = null;
				AspNetSynchronizationContextBase syncContext = context.SyncContext;
				try
				{
					if (applicationInstanceConsumersCounter != null)
					{
						applicationInstanceConsumersCounter.MarkOperationPending();
					}
					using (syncContext.AcquireThreadLock())
					{
						try
						{
							threadContext = application.OnThreadEnter();
						}
						catch (Exception ex)
						{
							if (error == null)
							{
								error = ex;
							}
						}
						try
						{
							try
							{
								for (;;)
								{
									if (syncContext.Error != null)
									{
										error = syncContext.Error;
										syncContext.ClearError();
									}
									if (error != null)
									{
										application.RecordError(error);
										error = null;
									}
									if (syncContext.PendingCompletion(this._resumeStepsWaitCallback))
									{
										goto IL_123;
									}
									if (this._currentStepIndex < this._endRequestStepIndex && (context.Error != null || this._requestCompleted))
									{
										context.Response.FilterOutput();
										this._currentStepIndex = this._endRequestStepIndex;
									}
									else
									{
										this._currentStepIndex++;
									}
									if (this._currentStepIndex >= this._execSteps.Length)
									{
										break;
									}
									this._numStepCalls++;
									syncContext.Enable();
									error = application.ExecuteStep(this._execSteps[this._currentStepIndex], ref flag2);
									if (!flag2)
									{
										goto IL_123;
									}
									this._numSyncStepCalls++;
								}
								flag = true;
								IL_123:;
							}
							finally
							{
								if (flag)
								{
									context.RaiseOnRequestCompleted();
								}
								if (threadContext != null)
								{
									try
									{
										threadContext.DisassociateFromCurrentThread();
									}
									catch
									{
									}
								}
							}
						}
						catch
						{
							throw;
						}
					}
					if (flag)
					{
						context.RaiseOnPipelineCompleted();
						context.Unroot();
						application.AsyncResult.Complete(this._numStepCalls == this._numSyncStepCalls, null, null);
						application.ReleaseAppInstance();
					}
				}
				finally
				{
					if (applicationInstanceConsumersCounter != null)
					{
						applicationInstanceConsumersCounter.MarkOperationCompleted();
					}
				}
			}

			// Token: 0x04003621 RID: 13857
			private HttpApplication.IExecutionStep[] _execSteps;

			// Token: 0x04003622 RID: 13858
			private WaitCallback _resumeStepsWaitCallback;

			// Token: 0x04003623 RID: 13859
			private int _currentStepIndex;

			// Token: 0x04003624 RID: 13860
			private int _numStepCalls;

			// Token: 0x04003625 RID: 13861
			private int _numSyncStepCalls;

			// Token: 0x04003626 RID: 13862
			private int _endRequestStepIndex;
		}

		// Token: 0x020008D8 RID: 2264
		internal class PipelineStepManager : HttpApplication.StepManager
		{
			// Token: 0x06006827 RID: 26663 RVA: 0x00171976 File Offset: 0x0016FB76
			internal PipelineStepManager(HttpApplication app) : base(app)
			{
			}

			// Token: 0x06006828 RID: 26664 RVA: 0x00171D84 File Offset: 0x0016FF84
			internal override void BuildSteps(WaitCallback stepCallback)
			{
				HttpApplication application = this._application;
				HttpApplication.IExecutionStep step = new HttpApplication.MaterializeHandlerExecutionStep(application);
				application.AddEventMapping("ManagedPipelineHandler", RequestNotification.MapRequestHandler, false, step);
				application.AddEventMapping("ManagedPipelineHandler", RequestNotification.ExecuteRequestHandler, false, application.CreateImplicitAsyncPreloadExecutionStep());
				HttpApplication.IExecutionStep step2 = new HttpApplication.CallHandlerExecutionStep(application);
				application.AddEventMapping("ManagedPipelineHandler", RequestNotification.ExecuteRequestHandler, false, step2);
				HttpApplication.IExecutionStep step3 = new HttpApplication.TransitionToWebSocketsExecutionStep(application);
				application.AddEventMapping("ManagedPipelineHandler", RequestNotification.EndRequest, true, step3);
				HttpApplication.IExecutionStep step4 = new HttpApplication.CallFilterExecutionStep(application);
				application.AddEventMapping("AspNetFilterModule", RequestNotification.UpdateRequestCache, false, step4);
				application.AddEventMapping("AspNetFilterModule", RequestNotification.LogRequest, false, step4);
				this._resumeStepsWaitCallback = stepCallback;
			}

			// Token: 0x06006829 RID: 26665 RVA: 0x00171E2C File Offset: 0x0017002C
			internal override void InitRequest()
			{
				this._requestCompleted = false;
				this._validatePathCalled = false;
				this._validateInputCalled = false;
			}

			// Token: 0x0600682A RID: 26666 RVA: 0x00171E44 File Offset: 0x00170044
			[DebuggerStepperBoundary]
			internal override void ResumeSteps(Exception error)
			{
				HttpContext context = this._application.Context;
				IIS7WorkerRequest iis7WorkerRequest = context.WorkerRequest as IIS7WorkerRequest;
				AspNetSynchronizationContextBase syncContext = context.SyncContext;
				RequestNotificationStatus status = RequestNotificationStatus.Continue;
				ThreadContext threadContext = null;
				bool flag = false;
				bool flag2 = false;
				bool flag3 = false;
				bool flag4 = false;
				bool flag5 = false;
				int num = -1;
				this._application.GetNotifcationContextProperties(ref flag5, ref num);
				CountdownTask applicationInstanceConsumersCounter = this._application.ApplicationInstanceConsumersCounter;
				using (context.RootedObjects.WithinTraceBlock())
				{
					if (!flag5)
					{
						syncContext.AssociateWithCurrentThread();
					}
					try
					{
						if (applicationInstanceConsumersCounter != null)
						{
							applicationInstanceConsumersCounter.MarkOperationPending();
						}
						bool flag6 = false;
						try
						{
							if (!flag5)
							{
								if (context.InIndicateCompletion && context.ThreadInsideIndicateCompletion == Thread.CurrentThread)
								{
									threadContext = context.IndicateCompletionContext;
									if (context.UsesImpersonation)
									{
										threadContext.SetImpersonationContext();
									}
								}
								else
								{
									threadContext = this._application.OnThreadEnter(context.UsesImpersonation);
									flag = true;
								}
							}
							for (;;)
							{
								if (syncContext.Error != null)
								{
									error = syncContext.Error;
									syncContext.ClearError();
								}
								if (error != null)
								{
									this._application.RecordError(error);
									error = null;
								}
								if (!this._validateInputCalled || !this._validatePathCalled)
								{
									error = this.ValidateHelper(context);
									if (error != null)
									{
										continue;
									}
								}
								if (!flag5 && syncContext.PendingCompletion(this._resumeStepsWaitCallback))
								{
									break;
								}
								bool flag7 = (context.NotificationContext.Error != null || context.NotificationContext.RequestCompleted) && context.CurrentNotification != RequestNotification.LogRequest && context.CurrentNotification != RequestNotification.EndRequest;
								if (flag7 || context.CurrentModuleEventIndex == num)
								{
									status = (flag7 ? RequestNotificationStatus.FinishRequest : RequestNotificationStatus.Continue);
									if (context.NotificationContext.PendingAsyncCompletion)
									{
										goto Block_23;
									}
									if (flag7 || UnsafeIISMethods.MgdGetNextNotification(iis7WorkerRequest.RequestContext, RequestNotificationStatus.Continue) != 1)
									{
										goto IL_1D2;
									}
									int currentModuleIndex = 0;
									bool isPostNotification = false;
									int currentNotification = 0;
									UnsafeIISMethods.MgdGetCurrentNotificationInfo(iis7WorkerRequest.RequestContext, out currentModuleIndex, out isPostNotification, out currentNotification);
									context.CurrentModuleIndex = currentModuleIndex;
									context.IsPostNotification = isPostNotification;
									context.CurrentNotification = (RequestNotification)currentNotification;
									context.CurrentModuleEventIndex = -1;
									num = this._application.CurrentModuleContainer.GetEventCount(context.CurrentNotification, context.IsPostNotification) - 1;
								}
								HttpContext httpContext = context;
								int currentModuleEventIndex = httpContext.CurrentModuleEventIndex;
								httpContext.CurrentModuleEventIndex = currentModuleEventIndex + 1;
								HttpApplication.IExecutionStep nextEvent = this._application.CurrentModuleContainer.GetNextEvent(context.CurrentNotification, context.IsPostNotification, context.CurrentModuleEventIndex);
								context.SyncContext.Enable();
								flag4 = false;
								error = this._application.ExecuteStep(nextEvent, ref flag4);
								if (!flag4)
								{
									goto Block_25;
								}
								context.Response.SyncStatusIntegrated();
							}
							this._application.AcquireNotifcationContextLock(ref flag6);
							context.NotificationContext.PendingAsyncCompletion = true;
							goto IL_37A;
							Block_23:
							context.Response.SyncStatusIntegrated();
							context.NotificationContext.PendingAsyncCompletion = false;
							flag2 = false;
							flag3 = true;
							goto IL_37A;
							IL_1D2:
							flag2 = true;
							flag3 = true;
							goto IL_37A;
							Block_25:
							this._application.AcquireNotifcationContextLock(ref flag6);
							context.NotificationContext.PendingAsyncCompletion = true;
						}
						finally
						{
							if (flag6)
							{
								this._application.ReleaseNotifcationContextLock();
							}
							if (threadContext != null)
							{
								if (context.InIndicateCompletion)
								{
									if (flag2)
									{
										threadContext.Synchronize();
										threadContext.UndoImpersonationContext();
										goto IL_36E;
									}
									if (threadContext.HasBeenDisassociatedFromThread)
									{
										goto IL_36E;
									}
									ThreadContext obj = threadContext;
									lock (obj)
									{
										if (!threadContext.HasBeenDisassociatedFromThread)
										{
											threadContext.DisassociateFromCurrentThread();
											flag = false;
											if (context.ThreadInsideIndicateCompletion == Thread.CurrentThread)
											{
												context.IndicateCompletionContext = null;
											}
										}
										goto IL_36E;
									}
								}
								if (flag2)
								{
									threadContext.Synchronize();
									context.IndicateCompletionContext = threadContext;
									flag = false;
									threadContext.UndoImpersonationContext();
								}
								else
								{
									threadContext.DisassociateFromCurrentThread();
									flag = false;
								}
								IL_36E:
								if (flag)
								{
									threadContext.DisassociateFromCurrentThread();
								}
							}
						}
						IL_37A:
						if (flag3)
						{
							this._application.AsyncResult.Complete(flag2, null, null, status);
						}
					}
					finally
					{
						if (!flag5)
						{
							syncContext.DisassociateFromCurrentThread();
						}
						if (applicationInstanceConsumersCounter != null)
						{
							applicationInstanceConsumersCounter.MarkOperationCompleted();
						}
					}
				}
			}

			// Token: 0x0600682B RID: 26667 RVA: 0x00172270 File Offset: 0x00170470
			private Exception ValidateHelper(HttpContext context)
			{
				if (!this._validateInputCalled)
				{
					this._validateInputCalled = true;
					try
					{
						context.Request.ValidateInputIfRequiredByConfig();
					}
					catch (Exception result)
					{
						return result;
					}
				}
				if (!this._validatePathCalled)
				{
					this._validatePathCalled = true;
					try
					{
						context.ValidatePath();
					}
					catch (Exception result2)
					{
						return result2;
					}
				}
				return null;
			}

			// Token: 0x04003627 RID: 13863
			private WaitCallback _resumeStepsWaitCallback;

			// Token: 0x04003628 RID: 13864
			private bool _validatePathCalled;

			// Token: 0x04003629 RID: 13865
			private bool _validateInputCalled;
		}

		// Token: 0x020008D9 RID: 2265
		private struct AsyncStepCompletionInfo
		{
			// Token: 0x0600682C RID: 26668 RVA: 0x001722DC File Offset: 0x001704DC
			public bool RegisterAsyncCompletion(Exception error)
			{
				this._error = ((error != null) ? ExceptionDispatchInfo.Capture(error) : null);
				if (Interlocked.Exchange(ref this._asyncState, 2) == 0)
				{
					return false;
				}
				this._error = null;
				return true;
			}

			// Token: 0x0600682D RID: 26669 RVA: 0x00172318 File Offset: 0x00170518
			public void RegisterBeginUnwound(IAsyncResult asyncResult, out bool operationCompleted, out bool mustCallEndHandler)
			{
				operationCompleted = false;
				mustCallEndHandler = false;
				if (Interlocked.Exchange(ref this._asyncState, 1) == 0)
				{
					if (asyncResult.CompletedSynchronously)
					{
						operationCompleted = true;
						mustCallEndHandler = true;
						return;
					}
				}
				else
				{
					operationCompleted = true;
				}
			}

			// Token: 0x0600682E RID: 26670 RVA: 0x00172350 File Offset: 0x00170550
			public void ReportError()
			{
				ExceptionDispatchInfo error = this._error;
				if (error != null)
				{
					this._error = null;
					error.Throw();
				}
			}

			// Token: 0x0600682F RID: 26671 RVA: 0x00172374 File Offset: 0x00170574
			public void Reset()
			{
				this._error = null;
				this._asyncState = 0;
			}

			// Token: 0x0400362A RID: 13866
			private const int ASYNC_STATE_NONE = 0;

			// Token: 0x0400362B RID: 13867
			private const int ASYNC_STATE_BEGIN_UNWOUND = 1;

			// Token: 0x0400362C RID: 13868
			private const int ASYNC_STATE_CALLBACK_COMPLETED = 2;

			// Token: 0x0400362D RID: 13869
			private volatile int _asyncState;

			// Token: 0x0400362E RID: 13870
			private ExceptionDispatchInfo _error;
		}

		// Token: 0x020008DA RID: 2266
		private class StepInvoker
		{
			// Token: 0x06006830 RID: 26672 RVA: 0x000030B5 File Offset: 0x000012B5
			public StepInvoker()
			{
			}

			// Token: 0x06006831 RID: 26673 RVA: 0x00172386 File Offset: 0x00170586
			public StepInvoker(Action<Action> action, HttpApplication.StepInvoker step)
			{
				this._action = action;
				this._nextStep = step;
			}

			// Token: 0x06006832 RID: 26674 RVA: 0x0017239C File Offset: 0x0017059C
			public void Invoke(Action executionStep)
			{
				if (this._action != null)
				{
					this._action(delegate
					{
						this._nextStep.Invoke(executionStep);
					});
					return;
				}
				executionStep();
			}

			// Token: 0x0400362F RID: 13871
			private Action<Action> _action;

			// Token: 0x04003630 RID: 13872
			private HttpApplication.StepInvoker _nextStep;
		}
	}
}
