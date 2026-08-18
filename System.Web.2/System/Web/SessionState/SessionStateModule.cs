using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Util;
using Microsoft.Win32;

namespace System.Web.SessionState
{
	// Token: 0x02000134 RID: 308
	public sealed class SessionStateModule : ISessionStateModule, IHttpModule
	{
		// Token: 0x06001258 RID: 4696 RVA: 0x00032FD5 File Offset: 0x000311D5
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		public SessionStateModule()
		{
		}

		// Token: 0x06001259 RID: 4697 RVA: 0x00032FE8 File Offset: 0x000311E8
		private static bool CheckTrustLevel(SessionStateSection config)
		{
			SessionStateMode mode = config.Mode;
			return mode <= SessionStateMode.InProc || mode - SessionStateMode.StateServer > 1 || HttpRuntime.HasAspNetHostingPermission(AspNetHostingPermissionLevel.Medium);
		}

		// Token: 0x0600125A RID: 4698 RVA: 0x00033012 File Offset: 0x00031212
		[AspNetHostingPermission(SecurityAction.Assert, Level = AspNetHostingPermissionLevel.Low)]
		private SessionStateStoreProviderBase SecureInstantiateProvider(ProviderSettings settings)
		{
			return (SessionStateStoreProviderBase)ProvidersHelper.InstantiateProvider(settings, typeof(SessionStateStoreProviderBase));
		}

		// Token: 0x0600125B RID: 4699 RVA: 0x0003302C File Offset: 0x0003122C
		private SessionStateStoreProviderBase InitCustomStore(SessionStateSection config)
		{
			string customProvider = config.CustomProvider;
			if (string.IsNullOrEmpty(customProvider))
			{
				throw new ConfigurationErrorsException(SR.GetString("Invalid_session_custom_provider", new object[]
				{
					customProvider
				}), config.ElementInformation.Properties["customProvider"].Source, config.ElementInformation.Properties["customProvider"].LineNumber);
			}
			ProviderSettings providerSettings = config.Providers[customProvider];
			if (providerSettings == null)
			{
				throw new ConfigurationErrorsException(SR.GetString("Missing_session_custom_provider", new object[]
				{
					customProvider
				}), config.ElementInformation.Properties["customProvider"].Source, config.ElementInformation.Properties["customProvider"].LineNumber);
			}
			return this.SecureInstantiateProvider(providerSettings);
		}

		// Token: 0x0600125C RID: 4700 RVA: 0x000330FC File Offset: 0x000312FC
		private IPartitionResolver InitPartitionResolver(SessionStateSection config)
		{
			string partitionResolverType = config.PartitionResolverType;
			if (string.IsNullOrEmpty(partitionResolverType))
			{
				return null;
			}
			if (config.Mode != SessionStateMode.StateServer && config.Mode != SessionStateMode.SQLServer)
			{
				throw new ConfigurationErrorsException(SR.GetString("Cant_use_partition_resolve"), config.ElementInformation.Properties["partitionResolverType"].Source, config.ElementInformation.Properties["partitionResolverType"].LineNumber);
			}
			Type type = ConfigUtil.GetType(partitionResolverType, "partitionResolverType", config);
			ConfigUtil.CheckAssignableType(typeof(IPartitionResolver), type, config, "partitionResolverType");
			IPartitionResolver partitionResolver = (IPartitionResolver)HttpRuntime.CreatePublicInstance(type);
			partitionResolver.Initialize();
			return partitionResolver;
		}

		// Token: 0x0600125D RID: 4701 RVA: 0x000331A8 File Offset: 0x000313A8
		private ISessionIDManager InitSessionIDManager(SessionStateSection config)
		{
			string sessionIDManagerType = config.SessionIDManagerType;
			ISessionIDManager sessionIDManager;
			if (string.IsNullOrEmpty(sessionIDManagerType))
			{
				sessionIDManager = new SessionIDManager();
				this._usingAspnetSessionIdManager = true;
			}
			else
			{
				Type type = ConfigUtil.GetType(sessionIDManagerType, "sessionIDManagerType", config);
				ConfigUtil.CheckAssignableType(typeof(ISessionIDManager), type, config, "sessionIDManagerType");
				sessionIDManager = (ISessionIDManager)HttpRuntime.CreatePublicInstanceByWebObjectActivator(type);
			}
			sessionIDManager.Initialize();
			return sessionIDManager;
		}

		// Token: 0x0600125E RID: 4702 RVA: 0x0003320C File Offset: 0x0003140C
		private void InitModuleFromConfig(HttpApplication app, SessionStateSection config)
		{
			if (config.Mode == SessionStateMode.Off)
			{
				return;
			}
			app.AddOnAcquireRequestStateAsync(new BeginEventHandler(this.BeginAcquireState), new EndEventHandler(this.EndAcquireState));
			app.ReleaseRequestState += this.OnReleaseState;
			app.EndRequest += this.OnEndRequest;
			this._partitionResolver = this.InitPartitionResolver(config);
			switch (config.Mode)
			{
			case SessionStateMode.InProc:
				if (HttpRuntime.UseIntegratedPipeline)
				{
					SessionStateModule.s_canSkipEndRequestCall = true;
				}
				this._store = new InProcSessionStateStore();
				this._store.Initialize(null, null);
				break;
			case SessionStateMode.StateServer:
				if (HttpRuntime.UseIntegratedPipeline)
				{
					SessionStateModule.s_canSkipEndRequestCall = true;
				}
				this._store = new OutOfProcSessionStateStore();
				((OutOfProcSessionStateStore)this._store).Initialize(null, null, this._partitionResolver);
				break;
			case SessionStateMode.SQLServer:
				this._store = new SqlSessionStateStore();
				((SqlSessionStateStore)this._store).Initialize(null, null, this._partitionResolver);
				break;
			case SessionStateMode.Custom:
				this._store = this.InitCustomStore(config);
				break;
			}
			this._idManager = this.InitSessionIDManager(config);
			if ((config.Mode == SessionStateMode.InProc || config.Mode == SessionStateMode.StateServer) && this._usingAspnetSessionIdManager)
			{
				this._ignoreImpersonation = true;
			}
		}

		// Token: 0x0600125F RID: 4703 RVA: 0x00033350 File Offset: 0x00031550
		public void Init(HttpApplication app)
		{
			bool flag = false;
			SessionStateSection sessionState = RuntimeConfig.GetAppConfig().SessionState;
			if (!this.s_oneTimeInit)
			{
				SessionStateModule.s_lock.AcquireWriterLock();
				try
				{
					if (!this.s_oneTimeInit)
					{
						this.InitModuleFromConfig(app, sessionState);
						flag = true;
						if (!SessionStateModule.CheckTrustLevel(sessionState))
						{
							SessionStateModule.s_trustLevelInsufficient = true;
						}
						SessionStateModule.s_timeout = (int)sessionState.Timeout.TotalMinutes;
						SessionStateModule.s_useHostingIdentity = sessionState.UseHostingIdentity;
						if (sessionState.Mode == SessionStateMode.InProc && this._usingAspnetSessionIdManager)
						{
							SessionStateModule.s_allowInProcOptimization = true;
						}
						if (sessionState.Mode != SessionStateMode.Custom && sessionState.Mode != SessionStateMode.Off && !sessionState.RegenerateExpiredSessionId)
						{
							SessionStateModule.s_allowDelayedStateStoreItemCreation = true;
						}
						SessionStateModule.s_configExecutionTimeout = RuntimeConfig.GetConfig().HttpRuntime.ExecutionTimeout;
						SessionStateModule.s_configRegenerateExpiredSessionId = sessionState.RegenerateExpiredSessionId;
						SessionStateModule.s_configCookieless = sessionState.Cookieless;
						SessionStateModule.s_configMode = sessionState.Mode;
						this.s_oneTimeInit = true;
					}
				}
				finally
				{
					SessionStateModule.s_lock.ReleaseWriterLock();
				}
			}
			if (!flag)
			{
				this.InitModuleFromConfig(app, sessionState);
			}
			if (SessionStateModule.s_trustLevelInsufficient)
			{
				throw new HttpException(SR.GetString("Session_state_need_higher_trust"));
			}
		}

		// Token: 0x06001260 RID: 4704 RVA: 0x00033474 File Offset: 0x00031674
		public void Dispose()
		{
			if (this._timer != null)
			{
				((IDisposable)this._timer).Dispose();
			}
			if (this._store != null)
			{
				this._store.Dispose();
			}
		}

		// Token: 0x06001261 RID: 4705 RVA: 0x0003349C File Offset: 0x0003169C
		private void ResetPerRequestFields()
		{
			this._rqSessionState = null;
			this._rqId = null;
			this._rqSessionItems = null;
			this._rqStaticObjects = null;
			this._rqIsNewSession = false;
			this._rqSessionStateNotFound = true;
			this._rqReadonly = false;
			this._rqItem = null;
			this._rqContext = null;
			this._rqAr = null;
			this._rqLockId = null;
			this._rqInCallback = 0;
			this._rqLastPollCompleted = DateTime.MinValue;
			this._rqExecutionTimeout = TimeSpan.Zero;
			this._rqAddedCookie = false;
			this._rqIdNew = false;
			this._rqActionFlags = SessionStateActions.None;
			this._rqIctx = null;
			this._rqChangeImpersonationRefCount = 0;
			this._rqTimerThreadImpersonationIctx = null;
			this._rqSupportSessionIdReissue = false;
		}

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x06001262 RID: 4706 RVA: 0x00033544 File Offset: 0x00031744
		// (remove) Token: 0x06001263 RID: 4707 RVA: 0x0003355D File Offset: 0x0003175D
		public event EventHandler Start
		{
			add
			{
				this._sessionStartEventHandler = (EventHandler)Delegate.Combine(this._sessionStartEventHandler, value);
			}
			remove
			{
				this._sessionStartEventHandler = (EventHandler)Delegate.Remove(this._sessionStartEventHandler, value);
			}
		}

		// Token: 0x06001264 RID: 4708 RVA: 0x00033578 File Offset: 0x00031778
		private void RaiseOnStart(EventArgs e)
		{
			if (this._sessionStartEventHandler == null)
			{
				return;
			}
			if (HttpRuntime.ApartmentThreading || this._rqContext.InAspCompatMode)
			{
				AspCompatApplicationStep.RaiseAspCompatEvent(this._rqContext, this._rqContext.ApplicationInstance, null, this._sessionStartEventHandler, this, e);
				return;
			}
			if (HttpContext.Current == null)
			{
				DisposableHttpContextWrapper.SwitchContext(this._rqContext);
			}
			this._sessionStartEventHandler(this, e);
		}

		// Token: 0x06001265 RID: 4709 RVA: 0x000335E2 File Offset: 0x000317E2
		private void OnStart(EventArgs e)
		{
			this.RaiseOnStart(e);
		}

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x06001266 RID: 4710 RVA: 0x000335EC File Offset: 0x000317EC
		// (remove) Token: 0x06001267 RID: 4711 RVA: 0x00033674 File Offset: 0x00031874
		public event EventHandler End
		{
			add
			{
				SessionOnEndTarget onEndTarget = this._onEndTarget;
				lock (onEndTarget)
				{
					if (this._store != null && this._onEndTarget.SessionEndEventHandlerCount == 0)
					{
						this._supportSessionExpiry = this._store.SetItemExpireCallback(new SessionStateItemExpireCallback(this._onEndTarget.RaiseSessionOnEnd));
					}
					SessionOnEndTarget onEndTarget2 = this._onEndTarget;
					int sessionEndEventHandlerCount = onEndTarget2.SessionEndEventHandlerCount + 1;
					onEndTarget2.SessionEndEventHandlerCount = sessionEndEventHandlerCount;
				}
			}
			remove
			{
				SessionOnEndTarget onEndTarget = this._onEndTarget;
				lock (onEndTarget)
				{
					SessionOnEndTarget onEndTarget2 = this._onEndTarget;
					int sessionEndEventHandlerCount = onEndTarget2.SessionEndEventHandlerCount - 1;
					onEndTarget2.SessionEndEventHandlerCount = sessionEndEventHandlerCount;
					if (this._store != null && this._onEndTarget.SessionEndEventHandlerCount == 0)
					{
						this._store.SetItemExpireCallback(null);
						this._supportSessionExpiry = false;
					}
				}
			}
		}

		// Token: 0x06001268 RID: 4712 RVA: 0x000336EC File Offset: 0x000318EC
		private IAsyncResult BeginAcquireState(object source, EventArgs e, AsyncCallback cb, object extraData)
		{
			bool flag = true;
			bool flag2 = false;
			this._acquireCalled = true;
			this._releaseCalled = false;
			this.ResetPerRequestFields();
			this._rqContext = ((HttpApplication)source).Context;
			this._rqAr = new HttpAsyncResult(cb, extraData);
			this.ChangeImpersonation(this._rqContext, false);
			IAsyncResult rqAr;
			try
			{
				if (EtwTrace.IsTraceEnabled(4, 8))
				{
					EtwTrace.Trace(EtwTraceType.ETW_TYPE_SESSION_DATA_BEGIN, this._rqContext.WorkerRequest);
				}
				this._store.InitializeRequest(this._rqContext);
				bool requiresSessionState = this._rqContext.RequiresSessionState;
				if (this._idManager.InitializeRequest(this._rqContext, false, out this._rqSupportSessionIdReissue))
				{
					this._rqAr.Complete(true, null, null);
					if (EtwTrace.IsTraceEnabled(4, 8))
					{
						EtwTrace.Trace(EtwTraceType.ETW_TYPE_SESSION_DATA_END, this._rqContext.WorkerRequest);
					}
					rqAr = this._rqAr;
				}
				else
				{
					if (SessionStateModule.s_allowInProcOptimization && !SessionStateModule.s_sessionEverSet && (!requiresSessionState || !((SessionIDManager)this._idManager).UseCookieless(this._rqContext)))
					{
						flag2 = true;
					}
					else
					{
						this._rqId = this._idManager.GetSessionID(this._rqContext);
					}
					if (!requiresSessionState)
					{
						if (this._rqId != null)
						{
							this._store.ResetItemTimeout(this._rqContext, this._rqId);
						}
						this._rqAr.Complete(true, null, null);
						if (EtwTrace.IsTraceEnabled(4, 8))
						{
							EtwTrace.Trace(EtwTraceType.ETW_TYPE_SESSION_DATA_END, this._rqContext.WorkerRequest);
						}
						rqAr = this._rqAr;
					}
					else
					{
						this._rqExecutionTimeout = this._rqContext.Timeout;
						if (this._rqExecutionTimeout == SessionStateModule.DEFAULT_DBG_EXECUTION_TIMEOUT)
						{
							this._rqExecutionTimeout = SessionStateModule.s_configExecutionTimeout;
						}
						this._rqReadonly = this._rqContext.ReadOnlySessionState;
						if (this._rqId != null)
						{
							flag = this.GetSessionStateItem();
						}
						else if (!flag2)
						{
							bool flag3 = this.CreateSessionId();
							this._rqIdNew = true;
							if (flag3)
							{
								if (SessionStateModule.s_configRegenerateExpiredSessionId)
								{
									this.CreateUninitializedSessionState();
								}
								this._rqAr.Complete(true, null, null);
								if (EtwTrace.IsTraceEnabled(4, 8))
								{
									EtwTrace.Trace(EtwTraceType.ETW_TYPE_SESSION_DATA_END, this._rqContext.WorkerRequest);
								}
								return this._rqAr;
							}
						}
						if (flag)
						{
							this.CompleteAcquireState();
							this._rqAr.Complete(true, null, null);
						}
						rqAr = this._rqAr;
					}
				}
			}
			finally
			{
				this.RestoreImpersonation();
			}
			return rqAr;
		}

		// Token: 0x06001269 RID: 4713 RVA: 0x00033948 File Offset: 0x00031B48
		internal bool CreateSessionId()
		{
			this._rqId = this._idManager.CreateSessionID(this._rqContext);
			bool result;
			this._idManager.SaveSessionID(this._rqContext, this._rqId, out result, out this._rqAddedCookie);
			return result;
		}

		// Token: 0x0600126A RID: 4714 RVA: 0x0003398C File Offset: 0x00031B8C
		internal void EnsureStateStoreItemLocked()
		{
			if (!this._rqSessionStateNotFound)
			{
				return;
			}
			this.ChangeImpersonation(this._rqContext, false);
			try
			{
				this._store.SetAndReleaseItemExclusive(this._rqContext, this._rqId, this._rqItem, this._rqLockId, true);
				this.LockSessionStateItem();
			}
			catch
			{
				throw;
			}
			finally
			{
				this.RestoreImpersonation();
			}
			this._rqSessionStateNotFound = false;
			SessionStateModule.s_sessionEverSet = true;
		}

		// Token: 0x0600126B RID: 4715 RVA: 0x00033A10 File Offset: 0x00031C10
		private void CompleteAcquireState()
		{
			bool flag = false;
			try
			{
				if (this._rqItem != null)
				{
					this._rqSessionStateNotFound = false;
					if ((this._rqActionFlags & SessionStateActions.InitializeItem) != SessionStateActions.None)
					{
						this._rqIsNewSession = true;
					}
					else
					{
						this._rqIsNewSession = false;
					}
				}
				else
				{
					this._rqIsNewSession = true;
					this._rqSessionStateNotFound = true;
					if (SessionStateModule.s_allowDelayedStateStoreItemCreation)
					{
						flag = true;
					}
					if (!this._rqIdNew && SessionStateModule.s_configRegenerateExpiredSessionId && this._rqSupportSessionIdReissue)
					{
						bool flag2 = this.CreateSessionId();
						if (flag2)
						{
							this.CreateUninitializedSessionState();
							return;
						}
					}
				}
				if (flag)
				{
					this._rqSessionState = SessionStateModule.s_delayedSessionState;
				}
				else
				{
					this.InitStateStoreItem(true);
				}
				SessionStateUtility.AddHttpSessionStateModuleToContext(this._rqContext, this, flag);
				if (this._rqIsNewSession)
				{
					this.OnStart(EventArgs.Empty);
				}
			}
			finally
			{
				if (EtwTrace.IsTraceEnabled(4, 8))
				{
					EtwTrace.Trace(EtwTraceType.ETW_TYPE_SESSION_DATA_END, this._rqContext.WorkerRequest);
				}
			}
		}

		// Token: 0x0600126C RID: 4716 RVA: 0x00033AF0 File Offset: 0x00031CF0
		private void CreateUninitializedSessionState()
		{
			this._store.CreateUninitializedItem(this._rqContext, this._rqId, SessionStateModule.s_timeout);
		}

		// Token: 0x0600126D RID: 4717 RVA: 0x00033B10 File Offset: 0x00031D10
		internal void InitStateStoreItem(bool addToContext)
		{
			this.ChangeImpersonation(this._rqContext, false);
			try
			{
				if (this._rqItem == null)
				{
					this._rqItem = this._store.CreateNewStoreData(this._rqContext, SessionStateModule.s_timeout);
				}
				this._rqSessionItems = this._rqItem.Items;
				if (this._rqSessionItems == null)
				{
					throw new HttpException(SR.GetString("Null_value_for_SessionStateItemCollection"));
				}
				this._rqStaticObjects = this._rqItem.StaticObjects;
				this._rqSessionItems.Dirty = false;
				this._rqSessionState = new HttpSessionStateContainer(this, this._rqId, this._rqSessionItems, this._rqStaticObjects, this._rqItem.Timeout, this._rqIsNewSession, SessionStateModule.s_configCookieless, SessionStateModule.s_configMode, this._rqReadonly);
				if (addToContext)
				{
					SessionStateUtility.AddHttpSessionStateToContext(this._rqContext, this._rqSessionState);
				}
			}
			finally
			{
				this.RestoreImpersonation();
			}
		}

		// Token: 0x0600126E RID: 4718 RVA: 0x00033C00 File Offset: 0x00031E00
		internal string DelayedGetSessionId()
		{
			this.ChangeImpersonation(this._rqContext, false);
			try
			{
				this._rqId = this._idManager.GetSessionID(this._rqContext);
				if (this._rqId == null)
				{
					bool flag = this.CreateSessionId();
				}
			}
			finally
			{
				this.RestoreImpersonation();
			}
			return this._rqId;
		}

		// Token: 0x0600126F RID: 4719 RVA: 0x00033C60 File Offset: 0x00031E60
		private void LockSessionStateItem()
		{
			if (!this._rqReadonly)
			{
				bool flag;
				TimeSpan timeSpan;
				SessionStateStoreData itemExclusive = this._store.GetItemExclusive(this._rqContext, this._rqId, out flag, out timeSpan, out this._rqLockId, out this._rqActionFlags);
			}
		}

		// Token: 0x06001270 RID: 4720 RVA: 0x00033CA0 File Offset: 0x00031EA0
		private bool GetSessionStateItem()
		{
			bool result = true;
			bool flag;
			TimeSpan t;
			if (this._rqReadonly)
			{
				this._rqItem = this._store.GetItem(this._rqContext, this._rqId, out flag, out t, out this._rqLockId, out this._rqActionFlags);
			}
			else
			{
				this._rqItem = this._store.GetItemExclusive(this._rqContext, this._rqId, out flag, out t, out this._rqLockId, out this._rqActionFlags);
				if (this._rqItem == null && !flag && this._rqId != null && (SessionStateModule.s_configCookieless != HttpCookieMode.UseUri || !SessionStateModule.s_configRegenerateExpiredSessionId))
				{
					this.CreateUninitializedSessionState();
					this._rqItem = this._store.GetItemExclusive(this._rqContext, this._rqId, out flag, out t, out this._rqLockId, out this._rqActionFlags);
				}
			}
			if (this._rqItem == null && flag)
			{
				if (t >= this._rqExecutionTimeout)
				{
					this._store.ReleaseItemExclusive(this._rqContext, this._rqId, this._rqLockId);
				}
				result = false;
				this.PollLockedSession();
			}
			return result;
		}

		// Token: 0x06001271 RID: 4721 RVA: 0x00033DAC File Offset: 0x00031FAC
		private void PollLockedSession()
		{
			this.EnsureRequestTimeout();
			if (this._timerCallback == null)
			{
				this._timerCallback = new TimerCallback(this.PollLockedSessionCallback);
			}
			if (this._timer == null)
			{
				this._timerId++;
				this.QueueRef();
				if (!SessionStateModule.s_PollIntervalRegLookedUp)
				{
					SessionStateModule.LookUpRegForPollInterval();
				}
				this._timer = new Timer(this._timerCallback, this._timerId, SessionStateModule.LOCKED_ITEM_POLLING_INTERVAL, SessionStateModule.LOCKED_ITEM_POLLING_INTERVAL);
			}
		}

		// Token: 0x06001272 RID: 4722 RVA: 0x00033E2D File Offset: 0x0003202D
		private void EnsureRequestTimeout()
		{
			if (this._rqContext.HasTimeoutExpired)
			{
				throw new HttpException(SR.GetString("Request_timed_out"));
			}
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x06001273 RID: 4723 RVA: 0x00033E4C File Offset: 0x0003204C
		private static bool IsRequestQueueEnabled
		{
			get
			{
				return AppSettings.RequestQueueLimitPerSession != int.MaxValue;
			}
		}

		// Token: 0x06001274 RID: 4724 RVA: 0x00033E60 File Offset: 0x00032060
		private void QueueRef()
		{
			if (!SessionStateModule.IsRequestQueueEnabled || this._rqId == null)
			{
				return;
			}
			int num = 0;
			SessionStateModule.s_queuedRequestsNumPerSession.TryGetValue(this._rqId, out num);
			if (num >= AppSettings.RequestQueueLimitPerSession)
			{
				throw new HttpException(SR.GetString("Request_Queue_Limit_Per_Session_Exceeded"));
			}
			SessionStateModule.s_queuedRequestsNumPerSession.AddOrUpdate(this._rqId, 1, (string key, int value) => value + 1);
		}

		// Token: 0x06001275 RID: 4725 RVA: 0x00033EDC File Offset: 0x000320DC
		private void DequeRef()
		{
			if (!SessionStateModule.IsRequestQueueEnabled || this._rqId == null)
			{
				return;
			}
			if (SessionStateModule.s_queuedRequestsNumPerSession.AddOrUpdate(this._rqId, 0, (string key, int value) => value - 1) == 0)
			{
				((ICollection<KeyValuePair<string, int>>)SessionStateModule.s_queuedRequestsNumPerSession).Remove(new KeyValuePair<string, int>(this._rqId, 0));
			}
		}

		// Token: 0x06001276 RID: 4726 RVA: 0x00033F44 File Offset: 0x00032144
		[RegistryPermission(SecurityAction.Assert, Unrestricted = true)]
		private static void LookUpRegForPollInterval()
		{
			object obj = SessionStateModule.s_PollIntervalRegLock;
			lock (obj)
			{
				if (!SessionStateModule.s_PollIntervalRegLookedUp)
				{
					try
					{
						object value = Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ASP.NET", "SessionStateLockedItemPollInterval", 0);
						if (value != null && (value is int || value is uint) && (int)value > 0)
						{
							SessionStateModule.LOCKED_ITEM_POLLING_INTERVAL = (long)((int)value);
						}
						SessionStateModule.s_PollIntervalRegLookedUp = true;
					}
					catch
					{
					}
				}
			}
		}

		// Token: 0x06001277 RID: 4727 RVA: 0x00033FDC File Offset: 0x000321DC
		private void ResetPollTimer()
		{
			this._timerId++;
			if (this._timer != null)
			{
				((IDisposable)this._timer).Dispose();
				this._timer = null;
			}
		}

		// Token: 0x06001278 RID: 4728 RVA: 0x0003400C File Offset: 0x0003220C
		private void ChangeImpersonation(HttpContext context, bool timerThread)
		{
			this._rqChangeImpersonationRefCount++;
			if (this._ignoreImpersonation)
			{
				return;
			}
			if (SessionStateModule.s_configMode == SessionStateMode.SQLServer && ((SqlSessionStateStore)this._store).KnowForSureNotUsingIntegratedSecurity && this._usingAspnetSessionIdManager)
			{
				return;
			}
			if (SessionStateModule.s_useHostingIdentity)
			{
				if (this._rqIctx == null)
				{
					this._rqIctx = new ApplicationImpersonationContext();
					return;
				}
				return;
			}
			else
			{
				if (timerThread)
				{
					this._rqTimerThreadImpersonationIctx = new ClientImpersonationContext(context, false);
					return;
				}
				return;
			}
		}

		// Token: 0x06001279 RID: 4729 RVA: 0x00034080 File Offset: 0x00032280
		private void RestoreImpersonation()
		{
			this._rqChangeImpersonationRefCount--;
			if (this._rqChangeImpersonationRefCount == 0)
			{
				if (this._rqIctx != null)
				{
					this._rqIctx.Undo();
					this._rqIctx = null;
				}
				if (this._rqTimerThreadImpersonationIctx != null)
				{
					this._rqTimerThreadImpersonationIctx.Undo();
					this._rqTimerThreadImpersonationIctx = null;
				}
			}
		}

		// Token: 0x0600127A RID: 4730 RVA: 0x000340D8 File Offset: 0x000322D8
		private void PollLockedSessionCallback(object state)
		{
			bool flag = false;
			Exception ex = null;
			if (Interlocked.CompareExchange(ref this._rqInCallback, 1, 0) != 0)
			{
				return;
			}
			try
			{
				int num = (int)state;
				if (num == this._timerId && DateTime.UtcNow - this._rqLastPollCompleted >= SessionStateModule.LOCKED_ITEM_POLLING_DELTA)
				{
					this.ChangeImpersonation(this._rqContext, true);
					try
					{
						flag = this.GetSessionStateItem();
						this._rqLastPollCompleted = DateTime.UtcNow;
						if (flag)
						{
							this.ResetPollTimer();
							this.CompleteAcquireState();
						}
					}
					finally
					{
						this.RestoreImpersonation();
					}
				}
			}
			catch (Exception ex2)
			{
				this.ResetPollTimer();
				ex = ex2;
			}
			finally
			{
				Interlocked.Exchange(ref this._rqInCallback, 0);
			}
			if (flag || ex != null)
			{
				this.DequeRef();
				this._rqAr.Complete(false, null, ex);
			}
		}

		// Token: 0x0600127B RID: 4731 RVA: 0x000341BC File Offset: 0x000323BC
		private void EndAcquireState(IAsyncResult ar)
		{
			((HttpAsyncResult)ar).End();
		}

		// Token: 0x0600127C RID: 4732 RVA: 0x000341CA File Offset: 0x000323CA
		private string ReleaseStateGetSessionID()
		{
			if (this._rqId == null)
			{
				this.DelayedGetSessionId();
			}
			return this._rqId;
		}

		// Token: 0x0600127D RID: 4733 RVA: 0x000341E4 File Offset: 0x000323E4
		private void OnReleaseState(object source, EventArgs eventArgs)
		{
			bool flag = false;
			this._releaseCalled = true;
			HttpApplication httpApplication = (HttpApplication)source;
			HttpContext context = httpApplication.Context;
			this.ChangeImpersonation(context, false);
			try
			{
				if (this._rqSessionState != null)
				{
					bool flag2 = this._rqSessionState == SessionStateModule.s_delayedSessionState;
					SessionStateUtility.RemoveHttpSessionStateFromContext(this._rqContext, flag2);
					if (!this._rqSessionStateNotFound || this._sessionStartEventHandler != null || (!flag2 && this._rqSessionItems.Dirty) || (!flag2 && this._rqStaticObjects != null && !this._rqStaticObjects.NeverAccessed))
					{
						if (this._rqSessionState.IsAbandoned)
						{
							if (this._rqSessionStateNotFound)
							{
								if (this._supportSessionExpiry)
								{
									if (flag2)
									{
										this.InitStateStoreItem(false);
									}
									this._onEndTarget.RaiseSessionOnEnd(this.ReleaseStateGetSessionID(), this._rqItem);
								}
							}
							else
							{
								this._store.RemoveItem(this._rqContext, this.ReleaseStateGetSessionID(), this._rqLockId, this._rqItem);
							}
						}
						else if (!this._rqReadonly || (this._rqReadonly && this._rqIsNewSession && this._sessionStartEventHandler != null && !this.SessionIDManagerUseCookieless))
						{
							if (context.Error == null && (this._rqSessionStateNotFound || this._rqSessionItems.Dirty || (this._rqStaticObjects != null && !this._rqStaticObjects.NeverAccessed) || this._rqItem.Timeout != this._rqSessionState.Timeout))
							{
								if (flag2)
								{
									this.InitStateStoreItem(false);
								}
								if (this._rqItem.Timeout != this._rqSessionState.Timeout)
								{
									this._rqItem.Timeout = this._rqSessionState.Timeout;
								}
								SessionStateModule.s_sessionEverSet = true;
								flag = true;
								this._store.SetAndReleaseItemExclusive(this._rqContext, this.ReleaseStateGetSessionID(), this._rqItem, this._rqLockId, this._rqSessionStateNotFound);
							}
							else if (!this._rqSessionStateNotFound)
							{
								this._store.ReleaseItemExclusive(this._rqContext, this.ReleaseStateGetSessionID(), this._rqLockId);
							}
						}
					}
				}
				if (this._rqAddedCookie && !flag && context.Response.IsBuffered())
				{
					this._idManager.RemoveSessionID(this._rqContext);
				}
			}
			finally
			{
				this.RestoreImpersonation();
			}
			bool requiresSessionState = context.RequiresSessionState;
			if (HttpRuntime.UseIntegratedPipeline && context.NotificationContext.CurrentNotification == RequestNotification.ReleaseRequestState && (SessionStateModule.s_canSkipEndRequestCall || !requiresSessionState))
			{
				context.DisableNotifications(RequestNotification.EndRequest, (RequestNotification)0);
				this._acquireCalled = false;
				this._releaseCalled = false;
				this.ResetPerRequestFields();
			}
		}

		// Token: 0x0600127E RID: 4734 RVA: 0x00034494 File Offset: 0x00032694
		private void OnEndRequest(object source, EventArgs eventArgs)
		{
			HttpApplication httpApplication = (HttpApplication)source;
			HttpContext context = httpApplication.Context;
			if (!context.RequiresSessionState)
			{
				this.ResetPerRequestFields();
				return;
			}
			this.ChangeImpersonation(context, false);
			try
			{
				if (!this._releaseCalled)
				{
					if (this._acquireCalled)
					{
						this.OnReleaseState(source, eventArgs);
					}
					else
					{
						if (this._rqContext == null)
						{
							this._rqContext = context;
						}
						this._store.InitializeRequest(this._rqContext);
						bool flag;
						this._idManager.InitializeRequest(this._rqContext, true, out flag);
						string sessionID = this._idManager.GetSessionID(context);
						if (sessionID != null)
						{
							this._store.ResetItemTimeout(context, sessionID);
						}
					}
				}
				this._store.EndRequest(this._rqContext);
			}
			finally
			{
				this._acquireCalled = false;
				this._releaseCalled = false;
				this.RestoreImpersonation();
				this.ResetPerRequestFields();
			}
		}

		// Token: 0x0600127F RID: 4735 RVA: 0x00034570 File Offset: 0x00032770
		internal static void ReadConnectionString(SessionStateSection config, ref string cntString, string propName)
		{
			ConfigsHelper.GetRegistryStringAttribute(ref cntString, config, propName);
			HandlerBase.CheckAndReadConnectionString(ref cntString, true);
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x06001280 RID: 4736 RVA: 0x00034582 File Offset: 0x00032782
		internal bool SessionIDManagerUseCookieless
		{
			get
			{
				if (!this._usingAspnetSessionIdManager)
				{
					return SessionStateModule.s_configCookieless == HttpCookieMode.UseUri;
				}
				return ((SessionIDManager)this._idManager).UseCookieless(this._rqContext);
			}
		}

		// Token: 0x06001281 RID: 4737 RVA: 0x000345AC File Offset: 0x000327AC
		public void ReleaseSessionState(HttpContext context)
		{
			if (HttpRuntime.UseIntegratedPipeline && this._acquireCalled && !this._releaseCalled)
			{
				try
				{
					this.OnReleaseState(context.ApplicationInstance, null);
				}
				catch
				{
				}
			}
		}

		// Token: 0x06001282 RID: 4738 RVA: 0x000345F4 File Offset: 0x000327F4
		public Task ReleaseSessionStateAsync(HttpContext context)
		{
			this.ReleaseSessionState(context);
			return TaskAsyncHelper.CompletedTask;
		}

		// Token: 0x04001437 RID: 5175
		internal const string SQL_CONNECTION_STRING_DEFAULT = "data source=localhost;Integrated Security=SSPI";

		// Token: 0x04001438 RID: 5176
		internal const string STATE_CONNECTION_STRING_DEFAULT = "tcpip=loopback:42424";

		// Token: 0x04001439 RID: 5177
		internal const int TIMEOUT_DEFAULT = 20;

		// Token: 0x0400143A RID: 5178
		internal const SessionStateMode MODE_DEFAULT = SessionStateMode.InProc;

		// Token: 0x0400143B RID: 5179
		private static long LOCKED_ITEM_POLLING_INTERVAL = 500L;

		// Token: 0x0400143C RID: 5180
		private static readonly TimeSpan LOCKED_ITEM_POLLING_DELTA = new TimeSpan(2500000L);

		// Token: 0x0400143D RID: 5181
		private static readonly TimeSpan DEFAULT_DBG_EXECUTION_TIMEOUT = new TimeSpan(0, 0, 30000000);

		// Token: 0x0400143E RID: 5182
		internal const int MAX_CACHE_BASED_TIMEOUT_MINUTES = 525600;

		// Token: 0x0400143F RID: 5183
		private bool s_oneTimeInit;

		// Token: 0x04001440 RID: 5184
		private static int s_timeout;

		// Token: 0x04001441 RID: 5185
		private static ReadWriteSpinLock s_lock;

		// Token: 0x04001442 RID: 5186
		private static bool s_trustLevelInsufficient;

		// Token: 0x04001443 RID: 5187
		private static TimeSpan s_configExecutionTimeout;

		// Token: 0x04001444 RID: 5188
		private static bool s_configRegenerateExpiredSessionId;

		// Token: 0x04001445 RID: 5189
		private static bool s_useHostingIdentity;

		// Token: 0x04001446 RID: 5190
		internal static HttpCookieMode s_configCookieless;

		// Token: 0x04001447 RID: 5191
		internal static SessionStateMode s_configMode;

		// Token: 0x04001448 RID: 5192
		private static bool s_canSkipEndRequestCall;

		// Token: 0x04001449 RID: 5193
		private static bool s_PollIntervalRegLookedUp = false;

		// Token: 0x0400144A RID: 5194
		private static object s_PollIntervalRegLock = new object();

		// Token: 0x0400144B RID: 5195
		private static ConcurrentDictionary<string, int> s_queuedRequestsNumPerSession = new ConcurrentDictionary<string, int>();

		// Token: 0x0400144C RID: 5196
		private static bool s_allowInProcOptimization;

		// Token: 0x0400144D RID: 5197
		private static bool s_sessionEverSet;

		// Token: 0x0400144E RID: 5198
		private static bool s_allowDelayedStateStoreItemCreation;

		// Token: 0x0400144F RID: 5199
		private static HttpSessionStateContainer s_delayedSessionState = new HttpSessionStateContainer();

		// Token: 0x04001450 RID: 5200
		private EventHandler _sessionStartEventHandler;

		// Token: 0x04001451 RID: 5201
		private Timer _timer;

		// Token: 0x04001452 RID: 5202
		private TimerCallback _timerCallback;

		// Token: 0x04001453 RID: 5203
		private volatile int _timerId;

		// Token: 0x04001454 RID: 5204
		private ISessionIDManager _idManager;

		// Token: 0x04001455 RID: 5205
		private bool _usingAspnetSessionIdManager;

		// Token: 0x04001456 RID: 5206
		private SessionStateStoreProviderBase _store;

		// Token: 0x04001457 RID: 5207
		private bool _supportSessionExpiry;

		// Token: 0x04001458 RID: 5208
		private IPartitionResolver _partitionResolver;

		// Token: 0x04001459 RID: 5209
		private bool _ignoreImpersonation;

		// Token: 0x0400145A RID: 5210
		private readonly SessionOnEndTarget _onEndTarget = new SessionOnEndTarget();

		// Token: 0x0400145B RID: 5211
		private bool _acquireCalled;

		// Token: 0x0400145C RID: 5212
		private bool _releaseCalled;

		// Token: 0x0400145D RID: 5213
		private HttpSessionStateContainer _rqSessionState;

		// Token: 0x0400145E RID: 5214
		private string _rqId;

		// Token: 0x0400145F RID: 5215
		private bool _rqIdNew;

		// Token: 0x04001460 RID: 5216
		private ISessionStateItemCollection _rqSessionItems;

		// Token: 0x04001461 RID: 5217
		private HttpStaticObjectsCollection _rqStaticObjects;

		// Token: 0x04001462 RID: 5218
		private bool _rqIsNewSession;

		// Token: 0x04001463 RID: 5219
		private bool _rqSessionStateNotFound;

		// Token: 0x04001464 RID: 5220
		private bool _rqReadonly;

		// Token: 0x04001465 RID: 5221
		private HttpContext _rqContext;

		// Token: 0x04001466 RID: 5222
		private HttpAsyncResult _rqAr;

		// Token: 0x04001467 RID: 5223
		private SessionStateStoreData _rqItem;

		// Token: 0x04001468 RID: 5224
		private object _rqLockId;

		// Token: 0x04001469 RID: 5225
		private int _rqInCallback;

		// Token: 0x0400146A RID: 5226
		private DateTime _rqLastPollCompleted;

		// Token: 0x0400146B RID: 5227
		private TimeSpan _rqExecutionTimeout;

		// Token: 0x0400146C RID: 5228
		private bool _rqAddedCookie;

		// Token: 0x0400146D RID: 5229
		private SessionStateActions _rqActionFlags;

		// Token: 0x0400146E RID: 5230
		private ImpersonationContext _rqIctx;

		// Token: 0x0400146F RID: 5231
		internal int _rqChangeImpersonationRefCount;

		// Token: 0x04001470 RID: 5232
		private ImpersonationContext _rqTimerThreadImpersonationIctx;

		// Token: 0x04001471 RID: 5233
		private bool _rqSupportSessionIdReissue;
	}
}
