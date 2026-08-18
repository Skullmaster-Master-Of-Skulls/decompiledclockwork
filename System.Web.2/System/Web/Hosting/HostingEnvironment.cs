using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Configuration.Provider;
using System.Globalization;
using System.Runtime.Caching;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Caching;
using System.Web.Compilation;
using System.Web.Configuration;
using System.Web.Util;
using System.Web.WebSockets;

namespace System.Web.Hosting
{
	// Token: 0x020007AD RID: 1965
	public sealed class HostingEnvironment : MarshalByRefObject
	{
		// Token: 0x17001B2B RID: 6955
		// (get) Token: 0x06005D31 RID: 23857 RVA: 0x0014309B File Offset: 0x0014129B
		internal static FcnMode FcnMode
		{
			get
			{
				if (HostingEnvironment._theHostingEnvironment != null && HostingEnvironment._theHostingEnvironment._hostingParameters != null)
				{
					return HostingEnvironment._theHostingEnvironment._hostingParameters.FcnMode;
				}
				return FcnMode.NotSet;
			}
		}

		// Token: 0x17001B2C RID: 6956
		// (get) Token: 0x06005D32 RID: 23858 RVA: 0x001430C1 File Offset: 0x001412C1
		internal static bool FcnSkipReadAndCacheDacls
		{
			get
			{
				return HostingEnvironment._theHostingEnvironment != null && HostingEnvironment._theHostingEnvironment._hostingParameters != null && HostingEnvironment._theHostingEnvironment._hostingParameters.FcnSkipReadAndCacheDacls;
			}
		}

		// Token: 0x06005D33 RID: 23859 RVA: 0x0000298D File Offset: 0x00000B8D
		public override object InitializeLifetimeService()
		{
			return null;
		}

		// Token: 0x06005D34 RID: 23860 RVA: 0x001430E8 File Offset: 0x001412E8
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		public HostingEnvironment()
		{
			if (HostingEnvironment._theHostingEnvironment != null)
			{
				throw new InvalidOperationException(SR.GetString("Only_1_HostEnv"));
			}
			HostingEnvironment._theHostingEnvironment = this;
			this._onAppDomainUnload = new EventHandler(this.OnAppDomainUnload);
			Thread.GetDomain().DomainUnload += this._onAppDomainUnload;
			Thread.GetDomain().UnhandledException += ApplicationManager.OnUnhandledException;
		}

		// Token: 0x06005D35 RID: 23861 RVA: 0x00143171 File Offset: 0x00141371
		internal static long TrimCache(int percent)
		{
			if (HostingEnvironment._theHostingEnvironment != null)
			{
				return HostingEnvironment._theHostingEnvironment.TrimCacheInternal(percent);
			}
			return 0L;
		}

		// Token: 0x06005D36 RID: 23862 RVA: 0x00143188 File Offset: 0x00141388
		private long TrimCacheInternal(int percent)
		{
			if (Interlocked.Exchange(ref this._inTrimCache, 1) != 0)
			{
				return 0L;
			}
			long result;
			try
			{
				long num = 0L;
				if (!this._shutdownInitiated)
				{
					CacheStoreProvider internalCache = HttpRuntime.Cache.GetInternalCache(false);
					CacheStoreProvider objectCache = HttpRuntime.Cache.GetObjectCache(false);
					if (objectCache != null)
					{
						num = objectCache.Trim(percent);
					}
					if (internalCache != null && !internalCache.Equals(objectCache))
					{
						num += internalCache.Trim(percent);
					}
					if (this._objectCacheHost != null && !this._shutdownInitiated)
					{
						num += this._objectCacheHost.TrimCache(percent);
					}
				}
				result = num;
			}
			finally
			{
				Interlocked.Exchange(ref this._inTrimCache, 0);
			}
			return result;
		}

		// Token: 0x06005D37 RID: 23863 RVA: 0x00143230 File Offset: 0x00141430
		private void OnAppDomainUnload(object unusedObject, EventArgs unusedEventArgs)
		{
			Thread.GetDomain().DomainUnload -= this._onAppDomainUnload;
			if (!this._removedFromAppManager)
			{
				this.RemoveThisAppDomainFromAppManagerTableOnce();
			}
			HttpRuntime.RecoverFromUnexceptedAppDomainUnload();
			this.StopRegisteredObjects(true);
			if (this._appManager != null)
			{
				IApplicationHost appHost = null;
				if (this._externalAppHost)
				{
					appHost = this._appHost;
					this._appHost = new SimpleApplicationHost(this._appVirtualPath, this._appPhysicalPath);
					this._externalAppHost = false;
				}
				IDisposable disposable = this._configMapPath2 as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
				this._appManager.HostingEnvironmentShutdownComplete(this._appId, appHost);
			}
			if (this._configToken != IntPtr.Zero)
			{
				UnsafeNativeMethods.CloseHandle(this._configToken);
				this._configToken = IntPtr.Zero;
			}
		}

		// Token: 0x06005D38 RID: 23864 RVA: 0x001432EE File Offset: 0x001414EE
		internal void Initialize(ApplicationManager appManager, IApplicationHost appHost, IConfigMapPathFactory configMapPathFactory, HostingEnvironmentParameters hostingParameters, PolicyLevel policyLevel)
		{
			this.Initialize(appManager, appHost, configMapPathFactory, hostingParameters, policyLevel, null);
		}

		// Token: 0x06005D39 RID: 23865 RVA: 0x00143300 File Offset: 0x00141500
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		internal void Initialize(ApplicationManager appManager, IApplicationHost appHost, IConfigMapPathFactory configMapPathFactory, HostingEnvironmentParameters hostingParameters, PolicyLevel policyLevel, Exception appDomainCreationException)
		{
			this._hostingParameters = hostingParameters;
			HostingEnvironmentFlags hostingEnvironmentFlags = HostingEnvironmentFlags.Default;
			if (this._hostingParameters != null)
			{
				hostingEnvironmentFlags = this._hostingParameters.HostingFlags;
				if (this._hostingParameters.IISExpressVersion != null)
				{
					ServerConfig.IISExpressVersion = this._hostingParameters.IISExpressVersion;
				}
			}
			if ((hostingEnvironmentFlags & HostingEnvironmentFlags.HideFromAppManager) == HostingEnvironmentFlags.Default)
			{
				this._appManager = appManager;
			}
			if ((hostingEnvironmentFlags & HostingEnvironmentFlags.ClientBuildManager) != HostingEnvironmentFlags.Default)
			{
				BuildManagerHost.InClientBuildManager = true;
			}
			if ((hostingEnvironmentFlags & HostingEnvironmentFlags.SupportsMultiTargeting) != HostingEnvironmentFlags.Default)
			{
				BuildManagerHost.SupportsMultiTargeting = true;
			}
			if (this._hostingParameters != null && this._hostingParameters.ClrQuirksSwitches != null && this._hostingParameters.ClrQuirksSwitches.Length != 0)
			{
				HostingEnvironment.SetClrQuirksSwitches(this._hostingParameters.ClrQuirksSwitches);
			}
			if (appHost is ISAPIApplicationHost && !ServerConfig.UseMetabase)
			{
				string text = ((ISAPIApplicationHost)appHost).ResolveRootWebConfigPath();
				if (!string.IsNullOrEmpty(text))
				{
					HttpConfigurationSystem.RootWebConfigurationFilePath = text;
				}
				IProcessHostSupportFunctions supportFunctions = ((ISAPIApplicationHost)appHost).SupportFunctions;
				if (supportFunctions != null)
				{
					HostingEnvironment._functions = Misc.CreateLocalSupportFunctions(supportFunctions);
				}
			}
			this._appId = HttpRuntime.AppDomainAppId;
			this._appVirtualPath = HttpRuntime.AppDomainAppVirtualPathObject;
			this._appPhysicalPath = HttpRuntime.AppDomainAppPathInternal;
			this._appHost = appHost;
			this._configMapPath = configMapPathFactory.Create(this._appVirtualPath.VirtualPathString, this._appPhysicalPath);
			HttpConfigurationSystem.EnsureInit(this._configMapPath, true, false);
			this._configMapPath2 = (this._configMapPath as IConfigMapPath2);
			this._initiateShutdownWorkItemCallback = new WaitCallback(this.InitiateShutdownWorkItemCallback);
			if (this._appManager != null)
			{
				this._appManager.HostingEnvironmentActivated();
			}
			if (this._appHost == null)
			{
				this._appHost = new SimpleApplicationHost(this._appVirtualPath, this._appPhysicalPath);
			}
			else
			{
				this._externalAppHost = true;
			}
			this._configToken = this._appHost.GetConfigToken();
			this._mapPathBasedVirtualPathProvider = new MapPathBasedVirtualPathProvider();
			this._virtualPathProvider = this._mapPathBasedVirtualPathProvider;
			HttpRuntime.InitializeHostingFeatures(hostingEnvironmentFlags, policyLevel, appDomainCreationException);
			if (!BuildManagerHost.InClientBuildManager)
			{
				this.StartMonitoringForIdleTimeout();
			}
			this.EnforceAppDomainLimit();
			this.GetApplicationIdentity();
			this._applicationMonitors = new ApplicationMonitors();
			if (!HttpRuntime.HostingInitFailed)
			{
				try
				{
					BuildManager.ExecutePreAppStart();
					if ((hostingEnvironmentFlags & HostingEnvironmentFlags.DontCallAppInitialize) == HostingEnvironmentFlags.Default)
					{
						BuildManager.CallAppInitializeMethod();
					}
				}
				catch (Exception initializationException)
				{
					HttpRuntime.InitializationException = initializationException;
					if ((hostingEnvironmentFlags & HostingEnvironmentFlags.ThrowHostingInitErrors) != HostingEnvironmentFlags.Default)
					{
						throw;
					}
				}
			}
		}

		// Token: 0x06005D3A RID: 23866 RVA: 0x00143520 File Offset: 0x00141720
		private void InitializeObjectCacheHostPrivate()
		{
			if (ObjectCache.Host == null)
			{
				ObjectCacheHost objectCacheHost = new ObjectCacheHost();
				ObjectCache.Host = objectCacheHost;
				this._objectCacheHost = objectCacheHost;
			}
		}

		// Token: 0x06005D3B RID: 23867 RVA: 0x00143547 File Offset: 0x00141747
		internal static void InitializeObjectCacheHost()
		{
			if (HostingEnvironment._theHostingEnvironment != null)
			{
				HostingEnvironment._theHostingEnvironment.InitializeObjectCacheHostPrivate();
			}
		}

		// Token: 0x06005D3C RID: 23868 RVA: 0x0014355C File Offset: 0x0014175C
		private void StartMonitoringForIdleTimeout()
		{
			HostingEnvironmentSection hostingEnvironment = RuntimeConfig.GetAppLKGConfig().HostingEnvironment;
			TimeSpan timeout = (hostingEnvironment != null) ? hostingEnvironment.IdleTimeout : HostingEnvironmentSection.DefaultIdleTimeout;
			this._idleTimeoutMonitor = new IdleTimeoutMonitor(timeout);
		}

		// Token: 0x06005D3D RID: 23869 RVA: 0x00143594 File Offset: 0x00141794
		private void EnforceAppDomainLimit()
		{
			if (this._appManager == null)
			{
				return;
			}
			int num = 0;
			try
			{
				ProcessModelSection processModel = RuntimeConfig.GetMachineConfig().ProcessModel;
				num = processModel.MaxAppDomains;
			}
			catch
			{
			}
			if (num > 0 && this._appManager.AppDomainsCount >= num)
			{
				this._appManager.ReduceAppDomainsCount(num);
			}
		}

		// Token: 0x06005D3E RID: 23870 RVA: 0x001435F4 File Offset: 0x001417F4
		private void GetApplicationIdentity()
		{
			try
			{
				IdentitySection identity = RuntimeConfig.GetAppConfig().Identity;
				if (identity.Impersonate && identity.ImpersonateToken != IntPtr.Zero)
				{
					this._appIdentity = identity;
					this._appIdentityToken = identity.ImpersonateToken;
				}
				else
				{
					this._appIdentityToken = this._configToken;
				}
				this._appIdentityTokenSet = true;
			}
			catch
			{
			}
		}

		// Token: 0x06005D3F RID: 23871 RVA: 0x00143664 File Offset: 0x00141864
		private static void SetClrQuirksSwitches(KeyValuePair<string, bool>[] switches)
		{
			Type type = Type.GetType("System.AppContext, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
			if (type == null)
			{
				return;
			}
			Action<string, bool> action = (Action<string, bool>)Delegate.CreateDelegate(typeof(Action<string, bool>), type, "SetSwitch", false, false);
			if (action == null)
			{
				return;
			}
			foreach (KeyValuePair<string, bool> keyValuePair in switches)
			{
				action(keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x17001B2D RID: 6957
		// (get) Token: 0x06005D40 RID: 23872 RVA: 0x001436D4 File Offset: 0x001418D4
		public static Exception InitializationException
		{
			get
			{
				return HttpRuntime.InitializationException;
			}
		}

		// Token: 0x06005D41 RID: 23873 RVA: 0x001436DB File Offset: 0x001418DB
		internal ApplicationInfo GetApplicationInfo()
		{
			return new ApplicationInfo(this._appId, this._appVirtualPath, this._appPhysicalPath);
		}

		// Token: 0x06005D42 RID: 23874 RVA: 0x001436F4 File Offset: 0x001418F4
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		private void StopRegisteredObjects(bool immediate)
		{
			if (this._registeredObjects.Count > 0)
			{
				ArrayList arrayList = new ArrayList();
				lock (this)
				{
					foreach (object obj in this._registeredObjects)
					{
						object key = ((DictionaryEntry)obj).Key;
						if (this.IsWellKnownObject(key))
						{
							arrayList.Insert(0, key);
						}
						else
						{
							arrayList.Add(key);
						}
					}
				}
				foreach (object obj2 in arrayList)
				{
					IRegisteredObject registeredObject = (IRegisteredObject)obj2;
					try
					{
						registeredObject.Stop(immediate);
					}
					catch
					{
					}
				}
			}
		}

		// Token: 0x06005D43 RID: 23875 RVA: 0x00143808 File Offset: 0x00141A08
		private void InitiateShutdownWorkItemCallback(object state)
		{
			if (this._registeredObjects.Count == 0)
			{
				this.ShutdownThisAppDomainOnce();
				return;
			}
			this.StopRegisteredObjects(false);
			if (this._registeredObjects.Count == 0)
			{
				this.ShutdownThisAppDomainOnce();
				return;
			}
			int num = 30;
			HostingEnvironmentSection hostingEnvironment = RuntimeConfig.GetAppLKGConfig().HostingEnvironment;
			if (hostingEnvironment != null)
			{
				num = (int)hostingEnvironment.ShutdownTimeout.TotalSeconds;
			}
			DateTime t = DateTime.UtcNow.AddSeconds((double)num);
			while (this._registeredObjects.Count > 0 && DateTime.UtcNow < t)
			{
				Thread.Sleep(100);
			}
			this.StopRegisteredObjects(true);
			if (this._registeredObjects.Count == 0)
			{
				this.ShutdownThisAppDomainOnce();
				return;
			}
			this._registeredObjects = new Hashtable();
			this.ShutdownThisAppDomainOnce();
		}

		// Token: 0x06005D44 RID: 23876 RVA: 0x001438C8 File Offset: 0x00141AC8
		internal void InitiateShutdownInternal()
		{
			bool flag = false;
			if (!this._shutdownInitiated)
			{
				lock (this)
				{
					if (!this._shutdownInitiated)
					{
						this._shutdownInProgress = true;
						flag = true;
						this._shutdownInitiated = true;
					}
				}
			}
			if (!flag)
			{
				return;
			}
			HttpRuntime.SetShutdownReason(ApplicationShutdownReason.HostingEnvironment, "HostingEnvironment initiated shutdown");
			if (!BuildManagerHost.InClientBuildManager)
			{
				new EnvironmentPermission(PermissionState.Unrestricted).Assert();
				try
				{
					this._shutDownStack = Environment.StackTrace;
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
			}
			if (!BuildManagerHost.InClientBuildManager)
			{
				HttpRuntime.CoalesceNotifications();
			}
			this.RemoveThisAppDomainFromAppManagerTableOnce();
			ThreadPool.QueueUserWorkItem(this._initiateShutdownWorkItemCallback);
		}

		// Token: 0x17001B2E RID: 6958
		// (get) Token: 0x06005D45 RID: 23877 RVA: 0x00143980 File Offset: 0x00141B80
		// (set) Token: 0x06005D46 RID: 23878 RVA: 0x00143987 File Offset: 0x00141B87
		internal bool HasBeenRemovedFromAppManagerTable
		{
			get
			{
				return HostingEnvironment._hasBeenRemovedFromAppManangerTable;
			}
			set
			{
				HostingEnvironment._hasBeenRemovedFromAppManangerTable = value;
			}
		}

		// Token: 0x06005D47 RID: 23879 RVA: 0x00143990 File Offset: 0x00141B90
		private void RemoveThisAppDomainFromAppManagerTableOnce()
		{
			bool flag = false;
			if (!this._removedFromAppManager)
			{
				lock (this)
				{
					if (!this._removedFromAppManager)
					{
						flag = true;
						this._removedFromAppManager = true;
					}
				}
			}
			if (!flag)
			{
				return;
			}
			if (this._appManager != null)
			{
				this._appManager.HostingEnvironmentShutdownInitiated(this._appId, this);
			}
		}

		// Token: 0x06005D48 RID: 23880 RVA: 0x00143A00 File Offset: 0x00141C00
		private void ShutdownThisAppDomainOnce()
		{
			bool flag = false;
			if (!this._appDomainShutdownStarted)
			{
				lock (this)
				{
					if (!this._appDomainShutdownStarted)
					{
						flag = true;
						this._appDomainShutdownStarted = true;
					}
				}
			}
			if (!flag)
			{
				return;
			}
			if (this._idleTimeoutMonitor != null)
			{
				this._idleTimeoutMonitor.Stop();
				this._idleTimeoutMonitor = null;
			}
			while (this._inTrimCache == 1)
			{
				Thread.Sleep(100);
			}
			AspNetWebSocketManager.Current.AbortAllAndWait();
			HttpRuntime.SetUserForcedShutdown();
			this._shutdownInProgress = false;
			HttpRuntime.ShutdownAppDomainWithStackTrace(ApplicationShutdownReason.HostingEnvironment, SR.GetString("Hosting_Env_Restart"), this._shutDownStack);
		}

		// Token: 0x06005D49 RID: 23881 RVA: 0x00143AB0 File Offset: 0x00141CB0
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		internal ObjectHandle CreateInstance(string assemblyQualifiedName)
		{
			Type type = Type.GetType(assemblyQualifiedName, true);
			return new ObjectHandle(Activator.CreateInstance(type));
		}

		// Token: 0x06005D4A RID: 23882 RVA: 0x00143AD0 File Offset: 0x00141CD0
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		internal ObjectHandle CreateWellKnownObjectInstance(string assemblyQualifiedName, bool failIfExists)
		{
			Type type = Type.GetType(assemblyQualifiedName, true);
			IRegisteredObject registeredObject = null;
			string fullName = type.FullName;
			bool flag = false;
			lock (this)
			{
				registeredObject = (this._wellKnownObjects[fullName] as IRegisteredObject);
				if (registeredObject == null)
				{
					registeredObject = (IRegisteredObject)Activator.CreateInstance(type);
					this._wellKnownObjects[fullName] = registeredObject;
				}
				else
				{
					flag = true;
				}
			}
			if (flag && failIfExists)
			{
				throw new InvalidOperationException(SR.GetString("Wellknown_object_already_exists", new object[]
				{
					fullName
				}));
			}
			return new ObjectHandle(registeredObject);
		}

		// Token: 0x06005D4B RID: 23883 RVA: 0x00143B74 File Offset: 0x00141D74
		private bool IsWellKnownObject(object obj)
		{
			bool result = false;
			string fullName = obj.GetType().FullName;
			lock (this)
			{
				if (this._wellKnownObjects[fullName] == obj)
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06005D4C RID: 23884 RVA: 0x00143BCC File Offset: 0x00141DCC
		internal ObjectHandle FindWellKnownObject(string assemblyQualifiedName)
		{
			Type type = Type.GetType(assemblyQualifiedName, true);
			IRegisteredObject registeredObject = null;
			string fullName = type.FullName;
			lock (this)
			{
				registeredObject = (this._wellKnownObjects[fullName] as IRegisteredObject);
			}
			if (registeredObject == null)
			{
				return null;
			}
			return new ObjectHandle(registeredObject);
		}

		// Token: 0x06005D4D RID: 23885 RVA: 0x00143C34 File Offset: 0x00141E34
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		internal void StopWellKnownObject(string assemblyQualifiedName)
		{
			Type type = Type.GetType(assemblyQualifiedName, true);
			string fullName = type.FullName;
			lock (this)
			{
				IRegisteredObject registeredObject = this._wellKnownObjects[fullName] as IRegisteredObject;
				if (registeredObject != null)
				{
					this._wellKnownObjects.Remove(fullName);
					registeredObject.Stop(false);
				}
			}
		}

		// Token: 0x06005D4E RID: 23886 RVA: 0x00143CA4 File Offset: 0x00141EA4
		internal bool IsIdle()
		{
			bool isBusy = this._isBusy;
			this._isBusy = false;
			return !isBusy && this._busyCount == 0;
		}

		// Token: 0x06005D4F RID: 23887 RVA: 0x00143CCD File Offset: 0x00141ECD
		internal bool GetIdleValue()
		{
			return !this._isBusy && this._busyCount == 0;
		}

		// Token: 0x06005D50 RID: 23888 RVA: 0x00143CE2 File Offset: 0x00141EE2
		internal void IncrementBusyCountInternal()
		{
			this._isBusy = true;
			Interlocked.Increment(ref this._busyCount);
		}

		// Token: 0x06005D51 RID: 23889 RVA: 0x00143CF8 File Offset: 0x00141EF8
		internal void DecrementBusyCountInternal()
		{
			this._isBusy = true;
			Interlocked.Decrement(ref this._busyCount);
			IdleTimeoutMonitor idleTimeoutMonitor = this._idleTimeoutMonitor;
			if (idleTimeoutMonitor != null)
			{
				idleTimeoutMonitor.LastEvent = DateTime.UtcNow;
			}
		}

		// Token: 0x06005D52 RID: 23890 RVA: 0x00006164 File Offset: 0x00004364
		internal void IsUnloaded()
		{
		}

		// Token: 0x06005D53 RID: 23891 RVA: 0x00143D30 File Offset: 0x00141F30
		private void MessageReceivedInternal()
		{
			this._isBusy = true;
			IdleTimeoutMonitor idleTimeoutMonitor = this._idleTimeoutMonitor;
			if (idleTimeoutMonitor != null)
			{
				idleTimeoutMonitor.LastEvent = DateTime.UtcNow;
			}
		}

		// Token: 0x17001B2F RID: 6959
		// (get) Token: 0x06005D54 RID: 23892 RVA: 0x00143D5C File Offset: 0x00141F5C
		internal int LruScore
		{
			get
			{
				if (this._busyCount > 0)
				{
					return this._busyCount;
				}
				IdleTimeoutMonitor idleTimeoutMonitor = this._idleTimeoutMonitor;
				if (idleTimeoutMonitor == null)
				{
					return 0;
				}
				return -(int)(DateTime.UtcNow - idleTimeoutMonitor.LastEvent).TotalSeconds;
			}
		}

		// Token: 0x06005D55 RID: 23893 RVA: 0x00143D9F File Offset: 0x00141F9F
		internal static ApplicationManager GetApplicationManager()
		{
			if (HostingEnvironment._theHostingEnvironment == null)
			{
				return null;
			}
			return HostingEnvironment._theHostingEnvironment._appManager;
		}

		// Token: 0x06005D56 RID: 23894 RVA: 0x00143DB4 File Offset: 0x00141FB4
		private void RegisterRunningObjectInternal(IRegisteredObject obj)
		{
			lock (this)
			{
				this._registeredObjects[obj] = obj;
				ISuspendibleRegisteredObject suspendibleRegisteredObject = obj as ISuspendibleRegisteredObject;
				if (suspendibleRegisteredObject != null)
				{
					this._suspendManager.RegisterObject(suspendibleRegisteredObject);
				}
			}
		}

		// Token: 0x06005D57 RID: 23895 RVA: 0x00143E0C File Offset: 0x0014200C
		private void UnregisterRunningObjectInternal(IRegisteredObject obj)
		{
			bool flag = false;
			lock (this)
			{
				string fullName = obj.GetType().FullName;
				if (this._wellKnownObjects[fullName] == obj)
				{
					this._wellKnownObjects.Remove(fullName);
				}
				this._registeredObjects.Remove(obj);
				ISuspendibleRegisteredObject suspendibleRegisteredObject = obj as ISuspendibleRegisteredObject;
				if (suspendibleRegisteredObject != null)
				{
					this._suspendManager.UnregisterObject(suspendibleRegisteredObject);
				}
				if (this._registeredObjects.Count == 0)
				{
					flag = true;
				}
			}
			if (!flag)
			{
				return;
			}
			this.InitiateShutdownInternal();
		}

		// Token: 0x06005D58 RID: 23896 RVA: 0x00143EA8 File Offset: 0x001420A8
		private string GetSiteName()
		{
			if (this._siteName == null)
			{
				lock (this)
				{
					if (this._siteName == null)
					{
						string text = null;
						if (this._appHost != null)
						{
							InternalSecurityPermissions.Unrestricted.Assert();
							try
							{
								text = this._appHost.GetSiteName();
							}
							finally
							{
								CodeAccessPermission.RevertAssert();
							}
						}
						if (text == null)
						{
							text = WebConfigurationHost.DefaultSiteName;
						}
						this._siteName = text;
					}
				}
			}
			return this._siteName;
		}

		// Token: 0x06005D59 RID: 23897 RVA: 0x00143F38 File Offset: 0x00142138
		private string GetSiteID()
		{
			if (this._siteID == null)
			{
				lock (this)
				{
					if (this._siteID == null)
					{
						string text = null;
						if (this._appHost != null)
						{
							InternalSecurityPermissions.Unrestricted.Assert();
							try
							{
								text = this._appHost.GetSiteID();
							}
							finally
							{
								CodeAccessPermission.RevertAssert();
							}
						}
						if (text == null)
						{
							text = "1";
						}
						this._siteID = text.ToLower(CultureInfo.InvariantCulture);
					}
				}
			}
			return this._siteID;
		}

		// Token: 0x06005D5A RID: 23898 RVA: 0x00143FD4 File Offset: 0x001421D4
		private string GetAppConfigPath()
		{
			if (this._appConfigPath == null)
			{
				this._appConfigPath = WebConfigurationHost.GetConfigPathFromSiteIDAndVPath(HostingEnvironment.SiteID, HostingEnvironment.ApplicationVirtualPathObject);
			}
			return this._appConfigPath;
		}

		// Token: 0x06005D5B RID: 23899 RVA: 0x00143FFC File Offset: 0x001421FC
		private static string GetFixedMappingSlotName(VirtualPath virtualPath)
		{
			return "MapPath_" + virtualPath.VirtualPathString.ToLowerInvariant().GetHashCode().ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06005D5C RID: 23900 RVA: 0x00144030 File Offset: 0x00142230
		private static string GetVirtualPathToFileMapping(VirtualPath virtualPath)
		{
			return CallContext.GetData(HostingEnvironment.GetFixedMappingSlotName(virtualPath)) as string;
		}

		// Token: 0x06005D5D RID: 23901 RVA: 0x00144044 File Offset: 0x00142244
		internal static object AddVirtualPathToFileMapping(VirtualPath virtualPath, string physicalPath)
		{
			CallContext.SetData(HostingEnvironment.GetFixedMappingSlotName(virtualPath), physicalPath);
			HostingEnvironment.VirtualPathToFileMappingState virtualPathToFileMappingState = new HostingEnvironment.VirtualPathToFileMappingState();
			virtualPathToFileMappingState.VirtualPath = virtualPath;
			virtualPathToFileMappingState.VirtualPathProvider = HostingEnvironment._theHostingEnvironment._virtualPathProvider;
			CallContext.SetData("__TemporaryVirtualPathProvider__", HostingEnvironment._theHostingEnvironment._mapPathBasedVirtualPathProvider);
			return virtualPathToFileMappingState;
		}

		// Token: 0x06005D5E RID: 23902 RVA: 0x00144090 File Offset: 0x00142290
		internal static void ClearVirtualPathToFileMapping(object state)
		{
			HostingEnvironment.VirtualPathToFileMappingState virtualPathToFileMappingState = (HostingEnvironment.VirtualPathToFileMappingState)state;
			CallContext.SetData(HostingEnvironment.GetFixedMappingSlotName(virtualPathToFileMappingState.VirtualPath), null);
			CallContext.SetData("__TemporaryVirtualPathProvider__", null);
		}

		// Token: 0x06005D5F RID: 23903 RVA: 0x001440C0 File Offset: 0x001422C0
		private string MapPathActual(VirtualPath virtualPath, bool permitNull)
		{
			string text = null;
			virtualPath.FailIfRelativePath();
			if (string.CompareOrdinal(virtualPath.VirtualPathString, this._appVirtualPath.VirtualPathString) == 0)
			{
				text = this._appPhysicalPath;
			}
			else
			{
				using (new ProcessImpersonationContext())
				{
					text = HostingEnvironment.GetVirtualPathToFileMapping(virtualPath);
					if (text == null)
					{
						if (this._configMapPath == null)
						{
							throw new InvalidOperationException(SR.GetString("Cannot_map_path", new object[]
							{
								virtualPath
							}));
						}
						try
						{
							if (this._configMapPath2 != null)
							{
								text = this._configMapPath2.MapPath(this.GetSiteID(), virtualPath);
							}
							else
							{
								text = this._configMapPath.MapPath(this.GetSiteID(), virtualPath.VirtualPathString);
							}
							if (HttpRuntime.IsMapPathRelaxed)
							{
								text = HttpRuntime.GetRelaxedMapPathResult(text);
							}
						}
						catch
						{
							if (!HttpRuntime.IsMapPathRelaxed)
							{
								throw;
							}
							text = HttpRuntime.GetRelaxedMapPathResult(null);
						}
					}
				}
			}
			if (string.IsNullOrEmpty(text))
			{
				if (!permitNull)
				{
					if (!HttpRuntime.IsMapPathRelaxed)
					{
						throw new InvalidOperationException(SR.GetString("Cannot_map_path", new object[]
						{
							virtualPath
						}));
					}
					text = HttpRuntime.GetRelaxedMapPathResult(null);
				}
			}
			else if (virtualPath.HasTrailingSlash)
			{
				if (!UrlPath.PathEndsWithExtraSlash(text) && !UrlPath.PathIsDriveRoot(text))
				{
					text += "\\";
				}
			}
			else if (UrlPath.PathEndsWithExtraSlash(text) && !UrlPath.PathIsDriveRoot(text))
			{
				text = text.Substring(0, text.Length - 1);
			}
			return text;
		}

		// Token: 0x06005D60 RID: 23904 RVA: 0x0014422C File Offset: 0x0014242C
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		public static void RegisterObject(IRegisteredObject obj)
		{
			if (HostingEnvironment._theHostingEnvironment != null)
			{
				HostingEnvironment._theHostingEnvironment.RegisterRunningObjectInternal(obj);
			}
		}

		// Token: 0x06005D61 RID: 23905 RVA: 0x00144240 File Offset: 0x00142440
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		public static void UnregisterObject(IRegisteredObject obj)
		{
			if (HostingEnvironment._theHostingEnvironment != null)
			{
				HostingEnvironment._theHostingEnvironment.UnregisterRunningObjectInternal(obj);
			}
		}

		// Token: 0x06005D62 RID: 23906 RVA: 0x00144254 File Offset: 0x00142454
		[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		public static void QueueBackgroundWorkItem(Action<CancellationToken> workItem)
		{
			if (workItem == null)
			{
				throw new ArgumentNullException("workItem");
			}
			HostingEnvironment.QueueBackgroundWorkItem(delegate(CancellationToken ct)
			{
				workItem(ct);
				return HostingEnvironment._completedTask;
			});
		}

		// Token: 0x06005D63 RID: 23907 RVA: 0x00144292 File Offset: 0x00142492
		[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		public static void QueueBackgroundWorkItem(Func<CancellationToken, Task> workItem)
		{
			if (workItem == null)
			{
				throw new ArgumentNullException("workItem");
			}
			if (HostingEnvironment._theHostingEnvironment == null)
			{
				throw new InvalidOperationException();
			}
			HostingEnvironment._theHostingEnvironment.QueueBackgroundWorkItemInternal(workItem);
		}

		// Token: 0x06005D64 RID: 23908 RVA: 0x001442BC File Offset: 0x001424BC
		private void QueueBackgroundWorkItemInternal(Func<CancellationToken, Task> workItem)
		{
			BackgroundWorkScheduler backgroundWorkScheduler = Volatile.Read<BackgroundWorkScheduler>(ref this._backgroundWorkScheduler);
			if (backgroundWorkScheduler == null)
			{
				BackgroundWorkScheduler backgroundWorkScheduler2 = new BackgroundWorkScheduler(new Action<BackgroundWorkScheduler>(HostingEnvironment.UnregisterObject), new Action<AppDomain, Exception>(Misc.WriteUnhandledExceptionToEventLog), null);
				backgroundWorkScheduler = (Interlocked.CompareExchange<BackgroundWorkScheduler>(ref this._backgroundWorkScheduler, backgroundWorkScheduler2, null) ?? backgroundWorkScheduler2);
				if (backgroundWorkScheduler == backgroundWorkScheduler2)
				{
					HostingEnvironment.RegisterObject(backgroundWorkScheduler);
				}
			}
			backgroundWorkScheduler.ScheduleWorkItem(workItem);
		}

		// Token: 0x1400012F RID: 303
		// (add) Token: 0x06005D65 RID: 23909 RVA: 0x0014431C File Offset: 0x0014251C
		// (remove) Token: 0x06005D66 RID: 23910 RVA: 0x00144350 File Offset: 0x00142550
		public static event EventHandler StopListening;

		// Token: 0x06005D67 RID: 23911 RVA: 0x00144383 File Offset: 0x00142583
		public static void IncrementBusyCount()
		{
			if (HostingEnvironment._theHostingEnvironment != null)
			{
				HostingEnvironment._theHostingEnvironment.IncrementBusyCountInternal();
			}
		}

		// Token: 0x06005D68 RID: 23912 RVA: 0x00144396 File Offset: 0x00142596
		public static void DecrementBusyCount()
		{
			if (HostingEnvironment._theHostingEnvironment != null)
			{
				HostingEnvironment._theHostingEnvironment.DecrementBusyCountInternal();
			}
		}

		// Token: 0x06005D69 RID: 23913 RVA: 0x001443A9 File Offset: 0x001425A9
		public static void MessageReceived()
		{
			if (HostingEnvironment._theHostingEnvironment != null)
			{
				HostingEnvironment._theHostingEnvironment.MessageReceivedInternal();
			}
		}

		// Token: 0x17001B30 RID: 6960
		// (get) Token: 0x06005D6A RID: 23914 RVA: 0x001443BC File Offset: 0x001425BC
		public static bool InClientBuildManager
		{
			get
			{
				return BuildManagerHost.InClientBuildManager;
			}
		}

		// Token: 0x17001B31 RID: 6961
		// (get) Token: 0x06005D6B RID: 23915 RVA: 0x001443C3 File Offset: 0x001425C3
		public static bool IsHosted
		{
			get
			{
				return HostingEnvironment._theHostingEnvironment != null;
			}
		}

		// Token: 0x17001B32 RID: 6962
		// (get) Token: 0x06005D6C RID: 23916 RVA: 0x001443D0 File Offset: 0x001425D0
		internal static bool IsUnderIISProcess
		{
			get
			{
				string exeName = VersionInfo.ExeName;
				return exeName == "aspnet_wp" || exeName == "w3wp" || exeName == "inetinfo";
			}
		}

		// Token: 0x17001B33 RID: 6963
		// (get) Token: 0x06005D6D RID: 23917 RVA: 0x0014440A File Offset: 0x0014260A
		internal static bool IsUnderIIS6Process
		{
			get
			{
				return VersionInfo.ExeName == "w3wp";
			}
		}

		// Token: 0x17001B34 RID: 6964
		// (get) Token: 0x06005D6E RID: 23918 RVA: 0x0014441B File Offset: 0x0014261B
		internal static bool IsUnderIISExpressProcess
		{
			get
			{
				return VersionInfo.ExeName == "iisexpress";
			}
		}

		// Token: 0x17001B35 RID: 6965
		// (get) Token: 0x06005D6F RID: 23919 RVA: 0x0014442C File Offset: 0x0014262C
		public static IApplicationHost ApplicationHost
		{
			[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
			get
			{
				if (HostingEnvironment._theHostingEnvironment == null)
				{
					return null;
				}
				return HostingEnvironment._theHostingEnvironment._appHost;
			}
		}

		// Token: 0x17001B36 RID: 6966
		// (get) Token: 0x06005D70 RID: 23920 RVA: 0x0014442C File Offset: 0x0014262C
		internal static IApplicationHost ApplicationHostInternal
		{
			get
			{
				if (HostingEnvironment._theHostingEnvironment == null)
				{
					return null;
				}
				return HostingEnvironment._theHostingEnvironment._appHost;
			}
		}

		// Token: 0x17001B37 RID: 6967
		// (get) Token: 0x06005D71 RID: 23921 RVA: 0x00144441 File Offset: 0x00142641
		internal IApplicationHost InternalApplicationHost
		{
			get
			{
				return this._appHost;
			}
		}

		// Token: 0x17001B38 RID: 6968
		// (get) Token: 0x06005D72 RID: 23922 RVA: 0x00144449 File Offset: 0x00142649
		public static ApplicationMonitors ApplicationMonitors
		{
			get
			{
				if (HostingEnvironment._theHostingEnvironment == null)
				{
					return null;
				}
				return HostingEnvironment._theHostingEnvironment._applicationMonitors;
			}
		}

		// Token: 0x17001B39 RID: 6969
		// (get) Token: 0x06005D73 RID: 23923 RVA: 0x0014445E File Offset: 0x0014265E
		internal static int BusyCount
		{
			get
			{
				if (HostingEnvironment._theHostingEnvironment == null)
				{
					return 0;
				}
				return HostingEnvironment._theHostingEnvironment._busyCount;
			}
		}

		// Token: 0x17001B3A RID: 6970
		// (get) Token: 0x06005D74 RID: 23924 RVA: 0x00144473 File Offset: 0x00142673
		internal static bool ShutdownInitiated
		{
			get
			{
				return HostingEnvironment._theHostingEnvironment != null && HostingEnvironment._theHostingEnvironment._shutdownInitiated;
			}
		}

		// Token: 0x17001B3B RID: 6971
		// (get) Token: 0x06005D75 RID: 23925 RVA: 0x00144488 File Offset: 0x00142688
		internal static bool ShutdownInProgress
		{
			get
			{
				return HostingEnvironment._theHostingEnvironment != null && HostingEnvironment._theHostingEnvironment._shutdownInProgress;
			}
		}

		// Token: 0x17001B3C RID: 6972
		// (get) Token: 0x06005D76 RID: 23926 RVA: 0x0014449D File Offset: 0x0014269D
		public static string ApplicationID
		{
			get
			{
				if (HostingEnvironment._theHostingEnvironment == null)
				{
					return null;
				}
				InternalSecurityPermissions.AspNetHostingPermissionLevelHigh.Demand();
				return HostingEnvironment._theHostingEnvironment._appId;
			}
		}

		// Token: 0x17001B3D RID: 6973
		// (get) Token: 0x06005D77 RID: 23927 RVA: 0x001444BC File Offset: 0x001426BC
		internal static string ApplicationIDNoDemand
		{
			get
			{
				if (HostingEnvironment._theHostingEnvironment == null)
				{
					return null;
				}
				return HostingEnvironment._theHostingEnvironment._appId;
			}
		}

		// Token: 0x17001B3E RID: 6974
		// (get) Token: 0x06005D78 RID: 23928 RVA: 0x001444D1 File Offset: 0x001426D1
		public static string ApplicationPhysicalPath
		{
			get
			{
				if (HostingEnvironment._theHostingEnvironment == null)
				{
					return null;
				}
				InternalSecurityPermissions.AppPathDiscovery.Demand();
				return HostingEnvironment._theHostingEnvironment._appPhysicalPath;
			}
		}

		// Token: 0x17001B3F RID: 6975
		// (get) Token: 0x06005D79 RID: 23929 RVA: 0x001444F0 File Offset: 0x001426F0
		public static string ApplicationVirtualPath
		{
			get
			{
				return VirtualPath.GetVirtualPathStringNoTrailingSlash(HostingEnvironment.ApplicationVirtualPathObject);
			}
		}

		// Token: 0x17001B40 RID: 6976
		// (get) Token: 0x06005D7A RID: 23930 RVA: 0x001444FC File Offset: 0x001426FC
		internal static VirtualPath ApplicationVirtualPathObject
		{
			get
			{
				if (HostingEnvironment._theHostingEnvironment == null)
				{
					return null;
				}
				return HostingEnvironment._theHostingEnvironment._appVirtualPath;
			}
		}

		// Token: 0x17001B41 RID: 6977
		// (get) Token: 0x06005D7B RID: 23931 RVA: 0x00144511 File Offset: 0x00142711
		public static string SiteName
		{
			get
			{
				if (HostingEnvironment._theHostingEnvironment == null)
				{
					return null;
				}
				InternalSecurityPermissions.AspNetHostingPermissionLevelMedium.Demand();
				return HostingEnvironment._theHostingEnvironment.GetSiteName();
			}
		}

		// Token: 0x17001B42 RID: 6978
		// (get) Token: 0x06005D7C RID: 23932 RVA: 0x00144530 File Offset: 0x00142730
		internal static string SiteNameNoDemand
		{
			get
			{
				if (HostingEnvironment._theHostingEnvironment == null)
				{
					return null;
				}
				return HostingEnvironment._theHostingEnvironment.GetSiteName();
			}
		}

		// Token: 0x17001B43 RID: 6979
		// (get) Token: 0x06005D7D RID: 23933 RVA: 0x00144545 File Offset: 0x00142745
		internal static string SiteID
		{
			get
			{
				if (HostingEnvironment._theHostingEnvironment == null)
				{
					return null;
				}
				return HostingEnvironment._theHostingEnvironment.GetSiteID();
			}
		}

		// Token: 0x17001B44 RID: 6980
		// (get) Token: 0x06005D7E RID: 23934 RVA: 0x0014455A File Offset: 0x0014275A
		internal static IConfigMapPath ConfigMapPath
		{
			get
			{
				if (HostingEnvironment._theHostingEnvironment == null)
				{
					return null;
				}
				return HostingEnvironment._theHostingEnvironment._configMapPath;
			}
		}

		// Token: 0x17001B45 RID: 6981
		// (get) Token: 0x06005D7F RID: 23935 RVA: 0x0014456F File Offset: 0x0014276F
		internal static string AppConfigPath
		{
			get
			{
				if (HostingEnvironment._theHostingEnvironment == null)
				{
					return null;
				}
				return HostingEnvironment._theHostingEnvironment.GetAppConfigPath();
			}
		}

		// Token: 0x17001B46 RID: 6982
		// (get) Token: 0x06005D80 RID: 23936 RVA: 0x00144584 File Offset: 0x00142784
		public static bool IsDevelopmentEnvironment
		{
			get
			{
				bool? flag = AppDomain.CurrentDomain.GetData(".devEnvironment") as bool?;
				bool flag2 = true;
				return flag.GetValueOrDefault() == flag2 & flag != null;
			}
		}

		// Token: 0x17001B47 RID: 6983
		// (get) Token: 0x06005D81 RID: 23937 RVA: 0x00014E09 File Offset: 0x00013009
		public static Cache Cache
		{
			get
			{
				return HttpRuntime.Cache;
			}
		}

		// Token: 0x17001B48 RID: 6984
		// (get) Token: 0x06005D82 RID: 23938 RVA: 0x001445C0 File Offset: 0x001427C0
		internal static NameValueCollection CacheStoreProviderSettings
		{
			get
			{
				if (HostingEnvironment._cacheProviderSettings == null)
				{
					if (AppDomain.CurrentDomain.IsDefaultAppDomain())
					{
						Configuration configuration = WebConfigurationManager.OpenWebConfiguration(null);
						CacheSection cacheSection = (CacheSection)configuration.GetSection("system.web/caching/cache");
						if (cacheSection != null && cacheSection.DefaultProvider != null && !string.IsNullOrWhiteSpace(cacheSection.DefaultProvider))
						{
							ProviderSettingsCollection providers = cacheSection.Providers;
							if (providers == null || providers.Count < 1)
							{
								throw new ProviderException(SR.GetString("Def_provider_not_found"));
							}
							ProviderSettings providerSettings = providers[cacheSection.DefaultProvider];
							if (providerSettings == null)
							{
								throw new ProviderException(SR.GetString("Def_provider_not_found"));
							}
							NameValueCollection parameters = providerSettings.Parameters;
							parameters["name"] = providerSettings.Name;
							parameters["type"] = providerSettings.Type;
							HostingEnvironment._cacheProviderSettings = parameters;
						}
					}
					else
					{
						HostingEnvironment._cacheProviderSettings = (AppDomain.CurrentDomain.GetData(".defaultObjectCacheProvider") as NameValueCollection);
					}
				}
				if (HostingEnvironment._cacheProviderSettings != null)
				{
					return new NameValueCollection(HostingEnvironment._cacheProviderSettings);
				}
				return null;
			}
		}

		// Token: 0x17001B49 RID: 6985
		// (get) Token: 0x06005D83 RID: 23939 RVA: 0x001446C4 File Offset: 0x001428C4
		internal static int AppDomainsCount
		{
			get
			{
				ApplicationManager applicationManager = HostingEnvironment.GetApplicationManager();
				if (applicationManager == null)
				{
					return 0;
				}
				return applicationManager.AppDomainsCount;
			}
		}

		// Token: 0x17001B4A RID: 6986
		// (get) Token: 0x06005D84 RID: 23940 RVA: 0x001446E2 File Offset: 0x001428E2
		internal static HostingEnvironmentParameters HostingParameters
		{
			get
			{
				if (HostingEnvironment._theHostingEnvironment == null)
				{
					return null;
				}
				return HostingEnvironment._theHostingEnvironment._hostingParameters;
			}
		}

		// Token: 0x17001B4B RID: 6987
		// (get) Token: 0x06005D85 RID: 23941 RVA: 0x001446F8 File Offset: 0x001428F8
		internal static int AppDomainUniqueInteger
		{
			get
			{
				if (HostingEnvironment.s_appDomainUniqueInteger == 0)
				{
					HostingEnvironment.s_appDomainUniqueInteger = Guid.NewGuid().GetHashCode();
				}
				return HostingEnvironment.s_appDomainUniqueInteger;
			}
		}

		// Token: 0x17001B4C RID: 6988
		// (get) Token: 0x06005D86 RID: 23942 RVA: 0x00144729 File Offset: 0x00142929
		public static ApplicationShutdownReason ShutdownReason
		{
			get
			{
				return HttpRuntime.ShutdownReason;
			}
		}

		// Token: 0x17001B4D RID: 6989
		// (get) Token: 0x06005D87 RID: 23943 RVA: 0x00144730 File Offset: 0x00142930
		internal static bool StopListeningWasCalled
		{
			get
			{
				return HostingEnvironment._stopListeningWasCalled;
			}
		}

		// Token: 0x06005D88 RID: 23944 RVA: 0x0014473C File Offset: 0x0014293C
		internal static void SetupStopListeningHandler()
		{
			StopListeningWaitHandle waitObject = new StopListeningWaitHandle();
			RegisteredWaitHandle registeredWaitHandle = null;
			registeredWaitHandle = ThreadPool.UnsafeRegisterWaitForSingleObject(waitObject, delegate(object _, bool __)
			{
				GC.KeepAlive(registeredWaitHandle);
				HostingEnvironment.OnGlobalStopListening();
			}, null, -1, true);
		}

		// Token: 0x06005D89 RID: 23945 RVA: 0x00144778 File Offset: 0x00142978
		private static void OnGlobalStopListening()
		{
			HostingEnvironment._stopListeningWasCalled = true;
			EventHandler stopListening = HostingEnvironment.StopListening;
			if (stopListening != null)
			{
				stopListening(null, EventArgs.Empty);
			}
			if (HostingEnvironment._theHostingEnvironment != null)
			{
				HostingEnvironment._theHostingEnvironment.FireStopListeningHandlers();
			}
		}

		// Token: 0x06005D8A RID: 23946 RVA: 0x001447B4 File Offset: 0x001429B4
		private void FireStopListeningHandlers()
		{
			List<IStopListeningRegisteredObject> list = new List<IStopListeningRegisteredObject>();
			lock (this)
			{
				foreach (object obj in this._registeredObjects)
				{
					IStopListeningRegisteredObject stopListeningRegisteredObject = ((DictionaryEntry)obj).Key as IStopListeningRegisteredObject;
					if (stopListeningRegisteredObject != null)
					{
						list.Add(stopListeningRegisteredObject);
					}
				}
			}
			foreach (IStopListeningRegisteredObject stopListeningRegisteredObject2 in list)
			{
				stopListeningRegisteredObject2.StopListening();
			}
		}

		// Token: 0x06005D8B RID: 23947 RVA: 0x0014488C File Offset: 0x00142A8C
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		public static void InitiateShutdown()
		{
			if (HostingEnvironment._theHostingEnvironment != null)
			{
				HostingEnvironment._theHostingEnvironment.InitiateShutdownInternal();
			}
		}

		// Token: 0x06005D8C RID: 23948 RVA: 0x0014488C File Offset: 0x00142A8C
		internal static void InitiateShutdownWithoutDemand()
		{
			if (HostingEnvironment._theHostingEnvironment != null)
			{
				HostingEnvironment._theHostingEnvironment.InitiateShutdownInternal();
			}
		}

		// Token: 0x06005D8D RID: 23949 RVA: 0x001448A0 File Offset: 0x00142AA0
		internal IntPtr SuspendApplication()
		{
			object obj = this._suspendManager.Suspend();
			return GCUtil.RootObject(obj);
		}

		// Token: 0x06005D8E RID: 23950 RVA: 0x001448C0 File Offset: 0x00142AC0
		internal void ResumeApplication(IntPtr state)
		{
			object state2 = GCUtil.UnrootObject(state);
			this._suspendManager.Resume(state2);
		}

		// Token: 0x06005D8F RID: 23951 RVA: 0x001448E0 File Offset: 0x00142AE0
		public static string MapPath(string virtualPath)
		{
			return HostingEnvironment.MapPath(VirtualPath.Create(virtualPath));
		}

		// Token: 0x06005D90 RID: 23952 RVA: 0x001448F0 File Offset: 0x00142AF0
		internal static string MapPath(VirtualPath virtualPath)
		{
			if (HostingEnvironment._theHostingEnvironment == null)
			{
				return null;
			}
			string text = HostingEnvironment.MapPathInternal(virtualPath);
			if (text != null)
			{
				InternalSecurityPermissions.PathDiscovery(text).Demand();
			}
			return text;
		}

		// Token: 0x06005D91 RID: 23953 RVA: 0x0014491C File Offset: 0x00142B1C
		internal static string MapPathInternal(string virtualPath)
		{
			return HostingEnvironment.MapPathInternal(VirtualPath.Create(virtualPath));
		}

		// Token: 0x06005D92 RID: 23954 RVA: 0x00144929 File Offset: 0x00142B29
		internal static string MapPathInternal(VirtualPath virtualPath)
		{
			if (HostingEnvironment._theHostingEnvironment == null)
			{
				return null;
			}
			return HostingEnvironment._theHostingEnvironment.MapPathActual(virtualPath, false);
		}

		// Token: 0x06005D93 RID: 23955 RVA: 0x00144940 File Offset: 0x00142B40
		internal static string MapPathInternal(string virtualPath, bool permitNull)
		{
			return HostingEnvironment.MapPathInternal(VirtualPath.Create(virtualPath), permitNull);
		}

		// Token: 0x06005D94 RID: 23956 RVA: 0x0014494E File Offset: 0x00142B4E
		internal static string MapPathInternal(VirtualPath virtualPath, bool permitNull)
		{
			if (HostingEnvironment._theHostingEnvironment == null)
			{
				return null;
			}
			return HostingEnvironment._theHostingEnvironment.MapPathActual(virtualPath, permitNull);
		}

		// Token: 0x06005D95 RID: 23957 RVA: 0x00144965 File Offset: 0x00142B65
		internal static string MapPathInternal(string virtualPath, string baseVirtualDir, bool allowCrossAppMapping)
		{
			return HostingEnvironment.MapPathInternal(VirtualPath.Create(virtualPath), VirtualPath.CreateNonRelative(baseVirtualDir), allowCrossAppMapping);
		}

		// Token: 0x06005D96 RID: 23958 RVA: 0x00144979 File Offset: 0x00142B79
		internal static string MapPathInternal(VirtualPath virtualPath, VirtualPath baseVirtualDir, bool allowCrossAppMapping)
		{
			virtualPath = baseVirtualDir.Combine(virtualPath);
			if (!allowCrossAppMapping && !virtualPath.IsWithinAppRoot)
			{
				throw new ArgumentException(SR.GetString("Cross_app_not_allowed", new object[]
				{
					virtualPath
				}));
			}
			return HostingEnvironment.MapPathInternal(virtualPath);
		}

		// Token: 0x06005D97 RID: 23959 RVA: 0x001449B0 File Offset: 0x00142BB0
		internal static WebApplicationLevel GetPathLevel(string path)
		{
			WebApplicationLevel result = WebApplicationLevel.AboveApplication;
			if (HostingEnvironment._theHostingEnvironment != null && !string.IsNullOrEmpty(path))
			{
				string applicationVirtualPath = HostingEnvironment.ApplicationVirtualPath;
				if (applicationVirtualPath == "/")
				{
					if (path == "/")
					{
						result = WebApplicationLevel.AtApplication;
					}
					else if (path[0] == '/')
					{
						result = WebApplicationLevel.BelowApplication;
					}
				}
				else if (StringUtil.EqualsIgnoreCase(applicationVirtualPath, path))
				{
					result = WebApplicationLevel.AtApplication;
				}
				else if (path.Length > applicationVirtualPath.Length && path[applicationVirtualPath.Length] == '/' && StringUtil.StringStartsWithIgnoreCase(path, applicationVirtualPath))
				{
					result = WebApplicationLevel.BelowApplication;
				}
			}
			return result;
		}

		// Token: 0x17001B4E RID: 6990
		// (get) Token: 0x06005D98 RID: 23960 RVA: 0x00144A3D File Offset: 0x00142C3D
		internal static IntPtr ApplicationIdentityToken
		{
			get
			{
				if (HostingEnvironment._theHostingEnvironment == null)
				{
					return IntPtr.Zero;
				}
				if (HostingEnvironment._theHostingEnvironment._appIdentityTokenSet)
				{
					return HostingEnvironment._theHostingEnvironment._appIdentityToken;
				}
				return HostingEnvironment._theHostingEnvironment._configToken;
			}
		}

		// Token: 0x17001B4F RID: 6991
		// (get) Token: 0x06005D99 RID: 23961 RVA: 0x00144A6D File Offset: 0x00142C6D
		internal static bool HasHostingIdentity
		{
			get
			{
				return HostingEnvironment.ApplicationIdentityToken != IntPtr.Zero;
			}
		}

		// Token: 0x06005D9A RID: 23962 RVA: 0x0013E929 File Offset: 0x0013CB29
		[SecurityPermission(SecurityAction.Demand, ControlPrincipal = true)]
		public static IDisposable Impersonate()
		{
			return new ApplicationImpersonationContext();
		}

		// Token: 0x06005D9B RID: 23963 RVA: 0x00144A7E File Offset: 0x00142C7E
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		public static IDisposable Impersonate(IntPtr token)
		{
			if (token == IntPtr.Zero)
			{
				return new ProcessImpersonationContext();
			}
			return new ImpersonationContext(token);
		}

		// Token: 0x06005D9C RID: 23964 RVA: 0x00144A9C File Offset: 0x00142C9C
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		public static IDisposable Impersonate(IntPtr userToken, string virtualPath)
		{
			virtualPath = UrlPath.MakeVirtualPathAppAbsoluteReduceAndCheck(virtualPath);
			if (HostingEnvironment._theHostingEnvironment == null)
			{
				return HostingEnvironment.Impersonate(userToken);
			}
			IdentitySection identity = RuntimeConfig.GetConfig(virtualPath).Identity;
			if (!identity.Impersonate)
			{
				return new ApplicationImpersonationContext();
			}
			if (identity.ImpersonateToken != IntPtr.Zero)
			{
				return new ImpersonationContext(identity.ImpersonateToken);
			}
			return new ImpersonationContext(userToken);
		}

		// Token: 0x06005D9D RID: 23965 RVA: 0x00144AFD File Offset: 0x00142CFD
		public static IDisposable SetCultures()
		{
			return HostingEnvironment.SetCultures(RuntimeConfig.GetAppLKGConfig().Globalization);
		}

		// Token: 0x06005D9E RID: 23966 RVA: 0x00144B0E File Offset: 0x00142D0E
		public static IDisposable SetCultures(string virtualPath)
		{
			virtualPath = UrlPath.MakeVirtualPathAppAbsoluteReduceAndCheck(virtualPath);
			return HostingEnvironment.SetCultures(RuntimeConfig.GetConfig(virtualPath).Globalization);
		}

		// Token: 0x06005D9F RID: 23967 RVA: 0x00144B28 File Offset: 0x00142D28
		private static IDisposable SetCultures(GlobalizationSection gs)
		{
			HostingEnvironment.CultureContext cultureContext = new HostingEnvironment.CultureContext();
			if (gs != null)
			{
				CultureInfo culture = null;
				CultureInfo uiCulture = null;
				if (gs.Culture != null && gs.Culture.Length > 0)
				{
					try
					{
						culture = HttpServerUtility.CreateReadOnlyCultureInfo(gs.Culture);
					}
					catch
					{
					}
				}
				if (gs.UICulture != null && gs.UICulture.Length > 0)
				{
					try
					{
						uiCulture = HttpServerUtility.CreateReadOnlyCultureInfo(gs.UICulture);
					}
					catch
					{
					}
				}
				cultureContext.SetCultures(culture, uiCulture);
			}
			return cultureContext;
		}

		// Token: 0x17001B50 RID: 6992
		// (get) Token: 0x06005DA0 RID: 23968 RVA: 0x00144BB8 File Offset: 0x00142DB8
		public static VirtualPathProvider VirtualPathProvider
		{
			get
			{
				if (HostingEnvironment._theHostingEnvironment == null)
				{
					return null;
				}
				object data = CallContext.GetData("__TemporaryVirtualPathProvider__");
				if (data != null)
				{
					return data as VirtualPathProvider;
				}
				return HostingEnvironment._theHostingEnvironment._virtualPathProvider;
			}
		}

		// Token: 0x17001B51 RID: 6993
		// (get) Token: 0x06005DA1 RID: 23969 RVA: 0x00144BED File Offset: 0x00142DED
		internal static bool UsingMapPathBasedVirtualPathProvider
		{
			get
			{
				return HostingEnvironment._theHostingEnvironment == null || HostingEnvironment._theHostingEnvironment._virtualPathProvider == HostingEnvironment._theHostingEnvironment._mapPathBasedVirtualPathProvider;
			}
		}

		// Token: 0x06005DA2 RID: 23970 RVA: 0x00144C0E File Offset: 0x00142E0E
		public static void RegisterVirtualPathProvider(VirtualPathProvider virtualPathProvider)
		{
			if (HostingEnvironment._theHostingEnvironment == null)
			{
				throw new InvalidOperationException();
			}
			if (BuildManager.IsPrecompiledApp)
			{
				return;
			}
			HostingEnvironment.RegisterVirtualPathProviderInternal(virtualPathProvider);
		}

		// Token: 0x06005DA3 RID: 23971 RVA: 0x00144C2C File Offset: 0x00142E2C
		internal static void RegisterVirtualPathProviderInternal(VirtualPathProvider virtualPathProvider)
		{
			VirtualPathProvider virtualPathProvider2 = HostingEnvironment._theHostingEnvironment._virtualPathProvider;
			HostingEnvironment._theHostingEnvironment._virtualPathProvider = virtualPathProvider;
			virtualPathProvider.Initialize(virtualPathProvider2);
		}

		// Token: 0x17001B52 RID: 6994
		// (get) Token: 0x06005DA4 RID: 23972 RVA: 0x00144C56 File Offset: 0x00142E56
		// (set) Token: 0x06005DA5 RID: 23973 RVA: 0x00144C5D File Offset: 0x00142E5D
		internal static IProcessHostSupportFunctions SupportFunctions
		{
			get
			{
				return HostingEnvironment._functions;
			}
			set
			{
				HostingEnvironment._functions = value;
			}
		}

		// Token: 0x17001B53 RID: 6995
		// (get) Token: 0x06005DA6 RID: 23974 RVA: 0x00144C65 File Offset: 0x00142E65
		// (set) Token: 0x06005DA7 RID: 23975 RVA: 0x00144C84 File Offset: 0x00142E84
		public static int MaxConcurrentRequestsPerCPU
		{
			get
			{
				if (!HttpRuntime.UseIntegratedPipeline)
				{
					throw new PlatformNotSupportedException(SR.GetString("Requires_Iis_Integrated_Mode"));
				}
				return UnsafeIISMethods.MgdGetMaxConcurrentRequestsPerCPU();
			}
			[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
			set
			{
				if (!HttpRuntime.UseIntegratedPipeline)
				{
					throw new PlatformNotSupportedException(SR.GetString("Requires_Iis_Integrated_Mode"));
				}
				int num = UnsafeIISMethods.MgdSetMaxConcurrentRequestsPerCPU(value);
				if (num == -2147024809)
				{
					throw new ArgumentException(SR.GetString("Invalid_queue_limit"));
				}
				if (num == 1)
				{
					throw new InvalidOperationException(SR.GetString("Queue_limit_is_zero", new object[]
					{
						"maxConcurrentRequestsPerCPU"
					}));
				}
			}
		}

		// Token: 0x17001B54 RID: 6996
		// (get) Token: 0x06005DA8 RID: 23976 RVA: 0x00144CE9 File Offset: 0x00142EE9
		// (set) Token: 0x06005DA9 RID: 23977 RVA: 0x00144D08 File Offset: 0x00142F08
		public static int MaxConcurrentThreadsPerCPU
		{
			get
			{
				if (!HttpRuntime.UseIntegratedPipeline)
				{
					throw new PlatformNotSupportedException(SR.GetString("Requires_Iis_Integrated_Mode"));
				}
				return UnsafeIISMethods.MgdGetMaxConcurrentThreadsPerCPU();
			}
			[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
			set
			{
				if (!HttpRuntime.UseIntegratedPipeline)
				{
					throw new PlatformNotSupportedException(SR.GetString("Requires_Iis_Integrated_Mode"));
				}
				int num = UnsafeIISMethods.MgdSetMaxConcurrentThreadsPerCPU(value);
				if (num == -2147024809)
				{
					throw new ArgumentException(SR.GetString("Invalid_queue_limit"));
				}
				if (num == 1)
				{
					throw new InvalidOperationException(SR.GetString("Queue_limit_is_zero", new object[]
					{
						"maxConcurrentThreadsPerCPU"
					}));
				}
			}
		}

		// Token: 0x17001B55 RID: 6997
		// (get) Token: 0x06005DAA RID: 23978 RVA: 0x00141136 File Offset: 0x0013F336
		internal AppDomain HostedAppDomain
		{
			get
			{
				return AppDomain.CurrentDomain;
			}
		}

		// Token: 0x04003107 RID: 12551
		private static HostingEnvironment _theHostingEnvironment;

		// Token: 0x04003108 RID: 12552
		private EventHandler _onAppDomainUnload;

		// Token: 0x04003109 RID: 12553
		private ApplicationManager _appManager;

		// Token: 0x0400310A RID: 12554
		private HostingEnvironmentParameters _hostingParameters;

		// Token: 0x0400310B RID: 12555
		private IApplicationHost _appHost;

		// Token: 0x0400310C RID: 12556
		private bool _externalAppHost;

		// Token: 0x0400310D RID: 12557
		private IConfigMapPath _configMapPath;

		// Token: 0x0400310E RID: 12558
		private IConfigMapPath2 _configMapPath2;

		// Token: 0x0400310F RID: 12559
		private IntPtr _configToken;

		// Token: 0x04003110 RID: 12560
		private IdentitySection _appIdentity;

		// Token: 0x04003111 RID: 12561
		private IntPtr _appIdentityToken;

		// Token: 0x04003112 RID: 12562
		private bool _appIdentityTokenSet;

		// Token: 0x04003113 RID: 12563
		private string _appId;

		// Token: 0x04003114 RID: 12564
		private VirtualPath _appVirtualPath;

		// Token: 0x04003115 RID: 12565
		private string _appPhysicalPath;

		// Token: 0x04003116 RID: 12566
		private string _siteName;

		// Token: 0x04003117 RID: 12567
		private string _siteID;

		// Token: 0x04003118 RID: 12568
		private string _appConfigPath;

		// Token: 0x04003119 RID: 12569
		private bool _isBusy;

		// Token: 0x0400311A RID: 12570
		private int _busyCount;

		// Token: 0x0400311B RID: 12571
		private static volatile bool _stopListeningWasCalled;

		// Token: 0x0400311C RID: 12572
		private bool _removedFromAppManager;

		// Token: 0x0400311D RID: 12573
		private bool _appDomainShutdownStarted;

		// Token: 0x0400311E RID: 12574
		private bool _shutdownInitiated;

		// Token: 0x0400311F RID: 12575
		private bool _shutdownInProgress;

		// Token: 0x04003120 RID: 12576
		private string _shutDownStack;

		// Token: 0x04003121 RID: 12577
		private static NameValueCollection _cacheProviderSettings;

		// Token: 0x04003122 RID: 12578
		private int _inTrimCache;

		// Token: 0x04003123 RID: 12579
		private ObjectCacheHost _objectCacheHost;

		// Token: 0x04003124 RID: 12580
		private Hashtable _wellKnownObjects = new Hashtable();

		// Token: 0x04003125 RID: 12581
		private Hashtable _registeredObjects = new Hashtable();

		// Token: 0x04003126 RID: 12582
		private SuspendManager _suspendManager = new SuspendManager();

		// Token: 0x04003127 RID: 12583
		private ApplicationMonitors _applicationMonitors;

		// Token: 0x04003128 RID: 12584
		private BackgroundWorkScheduler _backgroundWorkScheduler;

		// Token: 0x04003129 RID: 12585
		private static readonly Task<object> _completedTask = Task.FromResult<object>(null);

		// Token: 0x0400312A RID: 12586
		private WaitCallback _initiateShutdownWorkItemCallback;

		// Token: 0x0400312B RID: 12587
		private IdleTimeoutMonitor _idleTimeoutMonitor;

		// Token: 0x0400312C RID: 12588
		private static IProcessHostSupportFunctions _functions;

		// Token: 0x0400312D RID: 12589
		private static bool _hasBeenRemovedFromAppManangerTable;

		// Token: 0x0400312E RID: 12590
		private const string TemporaryVirtualPathProviderKey = "__TemporaryVirtualPathProvider__";

		// Token: 0x04003130 RID: 12592
		private static int s_appDomainUniqueInteger;

		// Token: 0x04003131 RID: 12593
		private VirtualPathProvider _virtualPathProvider;

		// Token: 0x04003132 RID: 12594
		private VirtualPathProvider _mapPathBasedVirtualPathProvider;

		// Token: 0x02000A5E RID: 2654
		private class CultureContext : IDisposable
		{
			// Token: 0x06006EF0 RID: 28400 RVA: 0x000030B5 File Offset: 0x000012B5
			internal CultureContext()
			{
			}

			// Token: 0x06006EF1 RID: 28401 RVA: 0x0018B3DB File Offset: 0x001895DB
			void IDisposable.Dispose()
			{
				this.RestoreCultures();
			}

			// Token: 0x06006EF2 RID: 28402 RVA: 0x0018B3E4 File Offset: 0x001895E4
			internal void SetCultures(CultureInfo culture, CultureInfo uiCulture)
			{
				CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
				CultureInfo currentUICulture = Thread.CurrentThread.CurrentUICulture;
				if (culture != null && culture != currentCulture)
				{
					Thread.CurrentThread.CurrentCulture = culture;
					this._savedCulture = currentCulture;
				}
				if (uiCulture != null && uiCulture != currentCulture)
				{
					Thread.CurrentThread.CurrentUICulture = uiCulture;
					this._savedUICulture = currentUICulture;
				}
			}

			// Token: 0x06006EF3 RID: 28403 RVA: 0x0018B43C File Offset: 0x0018963C
			internal void RestoreCultures()
			{
				if (this._savedCulture != null && this._savedCulture != Thread.CurrentThread.CurrentCulture)
				{
					Thread.CurrentThread.CurrentCulture = this._savedCulture;
					this._savedCulture = null;
				}
				if (this._savedUICulture != null && this._savedUICulture != Thread.CurrentThread.CurrentUICulture)
				{
					Thread.CurrentThread.CurrentUICulture = this._savedUICulture;
					this._savedUICulture = null;
				}
			}

			// Token: 0x04003B84 RID: 15236
			private CultureInfo _savedCulture;

			// Token: 0x04003B85 RID: 15237
			private CultureInfo _savedUICulture;
		}

		// Token: 0x02000A5F RID: 2655
		internal class VirtualPathToFileMappingState
		{
			// Token: 0x04003B86 RID: 15238
			internal VirtualPath VirtualPath;

			// Token: 0x04003B87 RID: 15239
			internal VirtualPathProvider VirtualPathProvider;
		}
	}
}
