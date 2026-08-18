using System;
using System.Collections;
using System.Globalization;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using System.Web.Caching;
using System.Web.Compilation;
using System.Web.Configuration;
using System.Web.Util;

namespace System.Web.Hosting
{
	// Token: 0x0200028B RID: 651
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class HostingEnvironment : MarshalByRefObject
	{
		// Token: 0x0600215B RID: 8539 RVA: 0x0009266A File Offset: 0x0009166A
		public override object InitializeLifetimeService()
		{
			return null;
		}

		// Token: 0x0600215C RID: 8540 RVA: 0x00092670 File Offset: 0x00091670
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
		}

		// Token: 0x0600215D RID: 8541 RVA: 0x000926D8 File Offset: 0x000916D8
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
				this._appManager.HostingEnvironmentShutdownComplete(this._appId, appHost);
			}
			if (this._configToken != IntPtr.Zero)
			{
				UnsafeNativeMethods.CloseHandle(this._configToken);
				this._configToken = IntPtr.Zero;
			}
		}

		// Token: 0x0600215E RID: 8542 RVA: 0x00092784 File Offset: 0x00091784
		internal void Initialize(ApplicationManager appManager, IApplicationHost appHost, IConfigMapPathFactory configMapPathFactory, HostingEnvironmentParameters hostingParameters)
		{
			this._hostingParameters = hostingParameters;
			HostingEnvironmentFlags hostingEnvironmentFlags = HostingEnvironmentFlags.Default;
			if (this._hostingParameters != null)
			{
				hostingEnvironmentFlags = this._hostingParameters.HostingFlags;
			}
			if ((hostingEnvironmentFlags & HostingEnvironmentFlags.HideFromAppManager) == HostingEnvironmentFlags.Default)
			{
				this._appManager = appManager;
			}
			if ((hostingEnvironmentFlags & HostingEnvironmentFlags.ClientBuildManager) != HostingEnvironmentFlags.Default)
			{
				BuildManagerHost.InClientBuildManager = true;
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
			this._appId = HttpRuntime.AppDomainAppIdInternal;
			this._appVirtualPath = HttpRuntime.AppDomainAppVirtualPathObject;
			this._appPhysicalPath = HttpRuntime.AppDomainAppPathInternal;
			this._appHost = appHost;
			this._configMapPath = configMapPathFactory.Create(this._appVirtualPath.VirtualPathString, this._appPhysicalPath);
			HttpConfigurationSystem.EnsureInit(this._configMapPath, true, false);
			this._configMapPath2 = (this._configMapPath as IConfigMapPath2);
			this._initiateShutdownWorkItemCallback = new WaitCallback(this.InitiateShutdownWorkItemCallback);
			if (this._appManager != null)
			{
				this._appManager.HostingEnvironmentActivated(this._appId);
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
			HttpRuntime.InitializeHostingFeatures(hostingEnvironmentFlags);
			if (!BuildManagerHost.InClientBuildManager)
			{
				this.StartMonitoringForIdleTimeout();
			}
			this.EnforceAppDomainLimit();
			this.GetApplicationIdentity();
			if ((hostingEnvironmentFlags & HostingEnvironmentFlags.DontCallAppInitialize) == HostingEnvironmentFlags.Default && !HttpRuntime.HostingInitFailed)
			{
				try
				{
					BuildManager.CallAppInitializeMethod();
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

		// Token: 0x0600215F RID: 8543 RVA: 0x0009293C File Offset: 0x0009193C
		private void StartMonitoringForIdleTimeout()
		{
			HostingEnvironmentSection hostingEnvironment = RuntimeConfig.GetAppLKGConfig().HostingEnvironment;
			TimeSpan timeout = (hostingEnvironment != null) ? hostingEnvironment.IdleTimeout : HostingEnvironmentSection.DefaultIdleTimeout;
			this._idleTimeoutMonitor = new IdleTimeoutMonitor(timeout);
		}

		// Token: 0x06002160 RID: 8544 RVA: 0x00092974 File Offset: 0x00091974
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

		// Token: 0x06002161 RID: 8545 RVA: 0x000929D4 File Offset: 0x000919D4
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

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x06002162 RID: 8546 RVA: 0x00092A44 File Offset: 0x00091A44
		public static Exception InitializationException
		{
			get
			{
				return HttpRuntime.InitializationException;
			}
		}

		// Token: 0x06002163 RID: 8547 RVA: 0x00092A4B File Offset: 0x00091A4B
		internal ApplicationInfo GetApplicationInfo()
		{
			return new ApplicationInfo(this._appId, this._appVirtualPath, this._appPhysicalPath);
		}

		// Token: 0x06002164 RID: 8548 RVA: 0x00092A64 File Offset: 0x00091A64
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

		// Token: 0x06002165 RID: 8549 RVA: 0x00092B74 File Offset: 0x00091B74
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

		// Token: 0x06002166 RID: 8550 RVA: 0x00092C34 File Offset: 0x00091C34
		internal void InitiateShutdownInternal()
		{
			bool flag = false;
			if (!this._shutdownInitated)
			{
				lock (this)
				{
					if (!this._shutdownInitated)
					{
						this._shutdownInProgress = true;
						flag = true;
						this._shutdownInitated = true;
					}
				}
			}
			if (!flag)
			{
				return;
			}
			HttpRuntime.SetShutdownReason(ApplicationShutdownReason.HostingEnvironment, "HostingEnvironment initiated shutdown");
			new EnvironmentPermission(PermissionState.Unrestricted).Assert();
			try
			{
				this._shutDownStack = Environment.StackTrace;
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			this.RemoveThisAppDomainFromAppManagerTableOnce();
			ThreadPool.QueueUserWorkItem(this._initiateShutdownWorkItemCallback);
		}

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x06002167 RID: 8551 RVA: 0x00092CD0 File Offset: 0x00091CD0
		// (set) Token: 0x06002168 RID: 8552 RVA: 0x00092CD7 File Offset: 0x00091CD7
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

		// Token: 0x06002169 RID: 8553 RVA: 0x00092CE0 File Offset: 0x00091CE0
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

		// Token: 0x0600216A RID: 8554 RVA: 0x00092D48 File Offset: 0x00091D48
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
			HttpRuntime.SetUserForcedShutdown();
			this._shutdownInProgress = false;
			HttpRuntime.ShutdownAppDomainWithStackTrace(ApplicationShutdownReason.HostingEnvironment, SR.GetString("Hosting_Env_Restart"), this._shutDownStack);
		}

		// Token: 0x0600216B RID: 8555 RVA: 0x00092DD4 File Offset: 0x00091DD4
		internal ObjectHandle CreateInstance(Type type)
		{
			return new ObjectHandle(Activator.CreateInstance(type));
		}

		// Token: 0x0600216C RID: 8556 RVA: 0x00092DE4 File Offset: 0x00091DE4
		internal ObjectHandle CreateWellKnownObjectInstance(Type type, bool failIfExists)
		{
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

		// Token: 0x0600216D RID: 8557 RVA: 0x00092E7C File Offset: 0x00091E7C
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

		// Token: 0x0600216E RID: 8558 RVA: 0x00092ECC File Offset: 0x00091ECC
		internal ObjectHandle FindWellKnownObject(Type type)
		{
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

		// Token: 0x0600216F RID: 8559 RVA: 0x00092F20 File Offset: 0x00091F20
		internal void StopWellKnownObject(Type type)
		{
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

		// Token: 0x06002170 RID: 8560 RVA: 0x00092F80 File Offset: 0x00091F80
		internal bool IsIdle()
		{
			bool isBusy = this._isBusy;
			this._isBusy = false;
			return !isBusy && this._busyCount == 0;
		}

		// Token: 0x06002171 RID: 8561 RVA: 0x00092FA9 File Offset: 0x00091FA9
		internal bool GetIdleValue()
		{
			return !this._isBusy && this._busyCount == 0;
		}

		// Token: 0x06002172 RID: 8562 RVA: 0x00092FBE File Offset: 0x00091FBE
		internal void IncrementBusyCountInternal()
		{
			this._isBusy = true;
			Interlocked.Increment(ref this._busyCount);
		}

		// Token: 0x06002173 RID: 8563 RVA: 0x00092FD4 File Offset: 0x00091FD4
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

		// Token: 0x06002174 RID: 8564 RVA: 0x00093009 File Offset: 0x00092009
		internal void IsUnloaded()
		{
		}

		// Token: 0x06002175 RID: 8565 RVA: 0x0009300C File Offset: 0x0009200C
		private void MessageReceivedInternal()
		{
			this._isBusy = true;
			IdleTimeoutMonitor idleTimeoutMonitor = this._idleTimeoutMonitor;
			if (idleTimeoutMonitor != null)
			{
				idleTimeoutMonitor.LastEvent = DateTime.UtcNow;
			}
		}

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x06002176 RID: 8566 RVA: 0x00093038 File Offset: 0x00092038
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

		// Token: 0x06002177 RID: 8567 RVA: 0x0009307B File Offset: 0x0009207B
		internal static ApplicationManager GetApplicationManager()
		{
			if (HostingEnvironment._theHostingEnvironment == null)
			{
				return null;
			}
			return HostingEnvironment._theHostingEnvironment._appManager;
		}

		// Token: 0x06002178 RID: 8568 RVA: 0x00093090 File Offset: 0x00092090
		private void RegisterRunningObjectInternal(IRegisteredObject obj)
		{
			lock (this)
			{
				this._registeredObjects[obj] = obj;
			}
		}

		// Token: 0x06002179 RID: 8569 RVA: 0x000930CC File Offset: 0x000920CC
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

		// Token: 0x0600217A RID: 8570 RVA: 0x00093148 File Offset: 0x00092148
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

		// Token: 0x0600217B RID: 8571 RVA: 0x000931D0 File Offset: 0x000921D0
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

		// Token: 0x0600217C RID: 8572 RVA: 0x00093264 File Offset: 0x00092264
		private string GetAppConfigPath()
		{
			if (this._appConfigPath == null)
			{
				this._appConfigPath = WebConfigurationHost.GetConfigPathFromSiteIDAndVPath(HostingEnvironment.SiteID, HostingEnvironment.ApplicationVirtualPathObject);
			}
			return this._appConfigPath;
		}

		// Token: 0x0600217D RID: 8573 RVA: 0x0009328C File Offset: 0x0009228C
		private static string GetFixedMappingSlotName(VirtualPath virtualPath)
		{
			return "MapPath_" + virtualPath.VirtualPathString.ToLowerInvariant().GetHashCode().ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x0600217E RID: 8574 RVA: 0x000932C0 File Offset: 0x000922C0
		private static string GetVirtualPathToFileMapping(VirtualPath virtualPath)
		{
			return CallContext.GetData(HostingEnvironment.GetFixedMappingSlotName(virtualPath)) as string;
		}

		// Token: 0x0600217F RID: 8575 RVA: 0x000932D4 File Offset: 0x000922D4
		internal static object AddVirtualPathToFileMapping(VirtualPath virtualPath, string physicalPath)
		{
			CallContext.SetData(HostingEnvironment.GetFixedMappingSlotName(virtualPath), physicalPath);
			HostingEnvironment.VirtualPathToFileMappingState virtualPathToFileMappingState = new HostingEnvironment.VirtualPathToFileMappingState();
			virtualPathToFileMappingState.VirtualPath = virtualPath;
			virtualPathToFileMappingState.VirtualPathProvider = HostingEnvironment._theHostingEnvironment._virtualPathProvider;
			HostingEnvironment._theHostingEnvironment._virtualPathProvider = HostingEnvironment._theHostingEnvironment._mapPathBasedVirtualPathProvider;
			return virtualPathToFileMappingState;
		}

		// Token: 0x06002180 RID: 8576 RVA: 0x00093320 File Offset: 0x00092320
		internal static void ClearVirtualPathToFileMapping(object state)
		{
			HostingEnvironment.VirtualPathToFileMappingState virtualPathToFileMappingState = (HostingEnvironment.VirtualPathToFileMappingState)state;
			CallContext.SetData(HostingEnvironment.GetFixedMappingSlotName(virtualPathToFileMappingState.VirtualPath), null);
			HostingEnvironment._theHostingEnvironment._virtualPathProvider = virtualPathToFileMappingState.VirtualPathProvider;
		}

		// Token: 0x06002181 RID: 8577 RVA: 0x00093358 File Offset: 0x00092358
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
						if (this._configMapPath2 != null)
						{
							text = this._configMapPath2.MapPath(this.GetSiteID(), virtualPath);
						}
						else
						{
							text = this._configMapPath.MapPath(this.GetSiteID(), virtualPath.VirtualPathString);
						}
					}
				}
			}
			if (string.IsNullOrEmpty(text))
			{
				if (!permitNull)
				{
					throw new InvalidOperationException(SR.GetString("Cannot_map_path", new object[]
					{
						virtualPath
					}));
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

		// Token: 0x06002182 RID: 8578 RVA: 0x00093488 File Offset: 0x00092488
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		public static void RegisterObject(IRegisteredObject obj)
		{
			if (HostingEnvironment._theHostingEnvironment != null)
			{
				HostingEnvironment._theHostingEnvironment.RegisterRunningObjectInternal(obj);
			}
		}

		// Token: 0x06002183 RID: 8579 RVA: 0x0009349C File Offset: 0x0009249C
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		public static void UnregisterObject(IRegisteredObject obj)
		{
			if (HostingEnvironment._theHostingEnvironment != null)
			{
				HostingEnvironment._theHostingEnvironment.UnregisterRunningObjectInternal(obj);
			}
		}

		// Token: 0x06002184 RID: 8580 RVA: 0x000934B0 File Offset: 0x000924B0
		public static void IncrementBusyCount()
		{
			if (HostingEnvironment._theHostingEnvironment != null)
			{
				HostingEnvironment._theHostingEnvironment.IncrementBusyCountInternal();
			}
		}

		// Token: 0x06002185 RID: 8581 RVA: 0x000934C3 File Offset: 0x000924C3
		public static void DecrementBusyCount()
		{
			if (HostingEnvironment._theHostingEnvironment != null)
			{
				HostingEnvironment._theHostingEnvironment.DecrementBusyCountInternal();
			}
		}

		// Token: 0x06002186 RID: 8582 RVA: 0x000934D6 File Offset: 0x000924D6
		public static void MessageReceived()
		{
			if (HostingEnvironment._theHostingEnvironment != null)
			{
				HostingEnvironment._theHostingEnvironment.MessageReceivedInternal();
			}
		}

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x06002187 RID: 8583 RVA: 0x000934E9 File Offset: 0x000924E9
		public static bool IsHosted
		{
			get
			{
				return HostingEnvironment._theHostingEnvironment != null;
			}
		}

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x06002188 RID: 8584 RVA: 0x000934F8 File Offset: 0x000924F8
		internal static bool IsUnderIISProcess
		{
			get
			{
				string exeName = VersionInfo.ExeName;
				return exeName == "aspnet_wp" || exeName == "w3wp" || exeName == "inetinfo";
			}
		}

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x06002189 RID: 8585 RVA: 0x00093532 File Offset: 0x00092532
		internal static bool IsUnderIIS6Process
		{
			get
			{
				return VersionInfo.ExeName == "w3wp";
			}
		}

		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x0600218A RID: 8586 RVA: 0x00093543 File Offset: 0x00092543
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

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x0600218B RID: 8587 RVA: 0x00093558 File Offset: 0x00092558
		internal IApplicationHost InternalApplicationHost
		{
			get
			{
				return this._appHost;
			}
		}

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x0600218C RID: 8588 RVA: 0x00093560 File Offset: 0x00092560
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

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x0600218D RID: 8589 RVA: 0x00093575 File Offset: 0x00092575
		internal static bool ShutdownInitiated
		{
			get
			{
				return HostingEnvironment._theHostingEnvironment != null && HostingEnvironment._theHostingEnvironment._shutdownInitated;
			}
		}

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x0600218E RID: 8590 RVA: 0x0009358A File Offset: 0x0009258A
		internal static bool ShutdownInProgress
		{
			get
			{
				return HostingEnvironment._theHostingEnvironment != null && HostingEnvironment._theHostingEnvironment._shutdownInProgress;
			}
		}

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x0600218F RID: 8591 RVA: 0x0009359F File Offset: 0x0009259F
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

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x06002190 RID: 8592 RVA: 0x000935BE File Offset: 0x000925BE
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

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x06002191 RID: 8593 RVA: 0x000935D3 File Offset: 0x000925D3
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

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x06002192 RID: 8594 RVA: 0x000935F2 File Offset: 0x000925F2
		public static string ApplicationVirtualPath
		{
			get
			{
				return VirtualPath.GetVirtualPathStringNoTrailingSlash(HostingEnvironment.ApplicationVirtualPathObject);
			}
		}

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x06002193 RID: 8595 RVA: 0x000935FE File Offset: 0x000925FE
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

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x06002194 RID: 8596 RVA: 0x00093613 File Offset: 0x00092613
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

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x06002195 RID: 8597 RVA: 0x00093632 File Offset: 0x00092632
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

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x06002196 RID: 8598 RVA: 0x00093647 File Offset: 0x00092647
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

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x06002197 RID: 8599 RVA: 0x0009365C File Offset: 0x0009265C
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

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x06002198 RID: 8600 RVA: 0x00093671 File Offset: 0x00092671
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

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x06002199 RID: 8601 RVA: 0x00093686 File Offset: 0x00092686
		public static Cache Cache
		{
			get
			{
				return HttpRuntime.Cache;
			}
		}

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x0600219A RID: 8602 RVA: 0x00093690 File Offset: 0x00092690
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

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x0600219B RID: 8603 RVA: 0x000936AE File Offset: 0x000926AE
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

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x0600219C RID: 8604 RVA: 0x000936C4 File Offset: 0x000926C4
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

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x0600219D RID: 8605 RVA: 0x000936F5 File Offset: 0x000926F5
		public static ApplicationShutdownReason ShutdownReason
		{
			get
			{
				return HttpRuntime.ShutdownReason;
			}
		}

		// Token: 0x0600219E RID: 8606 RVA: 0x000936FC File Offset: 0x000926FC
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		public static void InitiateShutdown()
		{
			if (HostingEnvironment._theHostingEnvironment != null)
			{
				HostingEnvironment._theHostingEnvironment.InitiateShutdownInternal();
			}
		}

		// Token: 0x0600219F RID: 8607 RVA: 0x0009370F File Offset: 0x0009270F
		public static string MapPath(string virtualPath)
		{
			return HostingEnvironment.MapPath(VirtualPath.Create(virtualPath));
		}

		// Token: 0x060021A0 RID: 8608 RVA: 0x0009371C File Offset: 0x0009271C
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

		// Token: 0x060021A1 RID: 8609 RVA: 0x00093748 File Offset: 0x00092748
		internal static string MapPathInternal(string virtualPath)
		{
			return HostingEnvironment.MapPathInternal(VirtualPath.Create(virtualPath));
		}

		// Token: 0x060021A2 RID: 8610 RVA: 0x00093755 File Offset: 0x00092755
		internal static string MapPathInternal(VirtualPath virtualPath)
		{
			if (HostingEnvironment._theHostingEnvironment == null)
			{
				return null;
			}
			return HostingEnvironment._theHostingEnvironment.MapPathActual(virtualPath, false);
		}

		// Token: 0x060021A3 RID: 8611 RVA: 0x0009376C File Offset: 0x0009276C
		internal static string MapPathInternal(string virtualPath, bool permitNull)
		{
			return HostingEnvironment.MapPathInternal(VirtualPath.Create(virtualPath), permitNull);
		}

		// Token: 0x060021A4 RID: 8612 RVA: 0x0009377A File Offset: 0x0009277A
		internal static string MapPathInternal(VirtualPath virtualPath, bool permitNull)
		{
			if (HostingEnvironment._theHostingEnvironment == null)
			{
				return null;
			}
			return HostingEnvironment._theHostingEnvironment.MapPathActual(virtualPath, permitNull);
		}

		// Token: 0x060021A5 RID: 8613 RVA: 0x00093791 File Offset: 0x00092791
		internal static string MapPathInternal(string virtualPath, string baseVirtualDir, bool allowCrossAppMapping)
		{
			return HostingEnvironment.MapPathInternal(VirtualPath.Create(virtualPath), VirtualPath.CreateNonRelative(baseVirtualDir), allowCrossAppMapping);
		}

		// Token: 0x060021A6 RID: 8614 RVA: 0x000937A8 File Offset: 0x000927A8
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

		// Token: 0x060021A7 RID: 8615 RVA: 0x000937EC File Offset: 0x000927EC
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

		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x060021A8 RID: 8616 RVA: 0x00093879 File Offset: 0x00092879
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

		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x060021A9 RID: 8617 RVA: 0x000938A9 File Offset: 0x000928A9
		internal static bool HasHostingIdentity
		{
			get
			{
				return HostingEnvironment.ApplicationIdentityToken != IntPtr.Zero;
			}
		}

		// Token: 0x060021AA RID: 8618 RVA: 0x000938BA File Offset: 0x000928BA
		[SecurityPermission(SecurityAction.Demand, ControlPrincipal = true)]
		public static IDisposable Impersonate()
		{
			return new ApplicationImpersonationContext();
		}

		// Token: 0x060021AB RID: 8619 RVA: 0x000938C1 File Offset: 0x000928C1
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		public static IDisposable Impersonate(IntPtr token)
		{
			if (token == IntPtr.Zero)
			{
				return new ProcessImpersonationContext();
			}
			return new ImpersonationContext(token);
		}

		// Token: 0x060021AC RID: 8620 RVA: 0x000938DC File Offset: 0x000928DC
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

		// Token: 0x060021AD RID: 8621 RVA: 0x0009393D File Offset: 0x0009293D
		public static IDisposable SetCultures()
		{
			return HostingEnvironment.SetCultures(RuntimeConfig.GetAppLKGConfig().Globalization);
		}

		// Token: 0x060021AE RID: 8622 RVA: 0x0009394E File Offset: 0x0009294E
		public static IDisposable SetCultures(string virtualPath)
		{
			virtualPath = UrlPath.MakeVirtualPathAppAbsoluteReduceAndCheck(virtualPath);
			return HostingEnvironment.SetCultures(RuntimeConfig.GetConfig(virtualPath).Globalization);
		}

		// Token: 0x060021AF RID: 8623 RVA: 0x00093968 File Offset: 0x00092968
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

		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x060021B0 RID: 8624 RVA: 0x000939F8 File Offset: 0x000929F8
		public static VirtualPathProvider VirtualPathProvider
		{
			get
			{
				if (HostingEnvironment._theHostingEnvironment == null)
				{
					return null;
				}
				return HostingEnvironment._theHostingEnvironment._virtualPathProvider;
			}
		}

		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x060021B1 RID: 8625 RVA: 0x00093A0D File Offset: 0x00092A0D
		internal static bool UsingMapPathBasedVirtualPathProvider
		{
			get
			{
				return HostingEnvironment._theHostingEnvironment == null || HostingEnvironment._theHostingEnvironment._virtualPathProvider == HostingEnvironment._theHostingEnvironment._mapPathBasedVirtualPathProvider;
			}
		}

		// Token: 0x060021B2 RID: 8626 RVA: 0x00093A2E File Offset: 0x00092A2E
		[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.High)]
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

		// Token: 0x060021B3 RID: 8627 RVA: 0x00093A4C File Offset: 0x00092A4C
		internal static void RegisterVirtualPathProviderInternal(VirtualPathProvider virtualPathProvider)
		{
			VirtualPathProvider virtualPathProvider2 = HostingEnvironment._theHostingEnvironment._virtualPathProvider;
			HostingEnvironment._theHostingEnvironment._virtualPathProvider = virtualPathProvider;
			virtualPathProvider.Initialize(virtualPathProvider2);
		}

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x060021B4 RID: 8628 RVA: 0x00093A76 File Offset: 0x00092A76
		// (set) Token: 0x060021B5 RID: 8629 RVA: 0x00093A7D File Offset: 0x00092A7D
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

		// Token: 0x04001B09 RID: 6921
		private static HostingEnvironment _theHostingEnvironment;

		// Token: 0x04001B0A RID: 6922
		private EventHandler _onAppDomainUnload;

		// Token: 0x04001B0B RID: 6923
		private ApplicationManager _appManager;

		// Token: 0x04001B0C RID: 6924
		private HostingEnvironmentParameters _hostingParameters;

		// Token: 0x04001B0D RID: 6925
		private IApplicationHost _appHost;

		// Token: 0x04001B0E RID: 6926
		private bool _externalAppHost;

		// Token: 0x04001B0F RID: 6927
		private IConfigMapPath _configMapPath;

		// Token: 0x04001B10 RID: 6928
		private IConfigMapPath2 _configMapPath2;

		// Token: 0x04001B11 RID: 6929
		private IntPtr _configToken;

		// Token: 0x04001B12 RID: 6930
		private IdentitySection _appIdentity;

		// Token: 0x04001B13 RID: 6931
		private IntPtr _appIdentityToken;

		// Token: 0x04001B14 RID: 6932
		private bool _appIdentityTokenSet;

		// Token: 0x04001B15 RID: 6933
		private string _appId;

		// Token: 0x04001B16 RID: 6934
		private VirtualPath _appVirtualPath;

		// Token: 0x04001B17 RID: 6935
		private string _appPhysicalPath;

		// Token: 0x04001B18 RID: 6936
		private string _siteName;

		// Token: 0x04001B19 RID: 6937
		private string _siteID;

		// Token: 0x04001B1A RID: 6938
		private string _appConfigPath;

		// Token: 0x04001B1B RID: 6939
		private bool _isBusy;

		// Token: 0x04001B1C RID: 6940
		private int _busyCount;

		// Token: 0x04001B1D RID: 6941
		private bool _removedFromAppManager;

		// Token: 0x04001B1E RID: 6942
		private bool _appDomainShutdownStarted;

		// Token: 0x04001B1F RID: 6943
		private bool _shutdownInitated;

		// Token: 0x04001B20 RID: 6944
		private bool _shutdownInProgress;

		// Token: 0x04001B21 RID: 6945
		private string _shutDownStack;

		// Token: 0x04001B22 RID: 6946
		private Hashtable _wellKnownObjects = new Hashtable();

		// Token: 0x04001B23 RID: 6947
		private Hashtable _registeredObjects = new Hashtable();

		// Token: 0x04001B24 RID: 6948
		private WaitCallback _initiateShutdownWorkItemCallback;

		// Token: 0x04001B25 RID: 6949
		private IdleTimeoutMonitor _idleTimeoutMonitor;

		// Token: 0x04001B26 RID: 6950
		private static IProcessHostSupportFunctions _functions;

		// Token: 0x04001B27 RID: 6951
		private static bool _hasBeenRemovedFromAppManangerTable;

		// Token: 0x04001B28 RID: 6952
		private static int s_appDomainUniqueInteger;

		// Token: 0x04001B29 RID: 6953
		private VirtualPathProvider _virtualPathProvider;

		// Token: 0x04001B2A RID: 6954
		private VirtualPathProvider _mapPathBasedVirtualPathProvider;

		// Token: 0x0200028C RID: 652
		private class CultureContext : IDisposable
		{
			// Token: 0x060021B6 RID: 8630 RVA: 0x00093A85 File Offset: 0x00092A85
			internal CultureContext()
			{
			}

			// Token: 0x060021B7 RID: 8631 RVA: 0x00093A8D File Offset: 0x00092A8D
			void IDisposable.Dispose()
			{
				this.RestoreCultures();
			}

			// Token: 0x060021B8 RID: 8632 RVA: 0x00093A98 File Offset: 0x00092A98
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

			// Token: 0x060021B9 RID: 8633 RVA: 0x00093AF0 File Offset: 0x00092AF0
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

			// Token: 0x04001B2B RID: 6955
			private CultureInfo _savedCulture;

			// Token: 0x04001B2C RID: 6956
			private CultureInfo _savedUICulture;
		}

		// Token: 0x0200028D RID: 653
		internal class VirtualPathToFileMappingState
		{
			// Token: 0x04001B2D RID: 6957
			internal VirtualPath VirtualPath;

			// Token: 0x04001B2E RID: 6958
			internal VirtualPathProvider VirtualPathProvider;
		}
	}
}
