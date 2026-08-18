using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Runtime.Remoting;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Web.Compilation;
using System.Web.Configuration;
using System.Web.Util;

namespace System.Web.Hosting
{
	// Token: 0x02000286 RID: 646
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class ApplicationManager : MarshalByRefObject
	{
		// Token: 0x06002123 RID: 8483 RVA: 0x000914C4 File Offset: 0x000904C4
		internal ApplicationManager()
		{
			this._onRespondToPingWaitCallback = new WaitCallback(this.OnRespondToPingWaitCallback);
			AppDomain.CurrentDomain.UnhandledException += ApplicationManager.OnUnhandledException;
		}

		// Token: 0x06002124 RID: 8484 RVA: 0x0009151A File Offset: 0x0009051A
		internal static void RecordFatalException(Exception e)
		{
			ApplicationManager.RecordFatalException(AppDomain.CurrentDomain, e);
		}

		// Token: 0x06002125 RID: 8485 RVA: 0x00091528 File Offset: 0x00090528
		internal static void RecordFatalException(AppDomain appDomain, Exception e)
		{
			if (Interlocked.CompareExchange<Exception>(ref ApplicationManager._fatalException, e, null) == null)
			{
				Misc.WriteUnhandledExceptionToEventLog(appDomain, e);
			}
		}

		// Token: 0x06002126 RID: 8486 RVA: 0x0009154C File Offset: 0x0009054C
		private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs eventArgs)
		{
			if (!eventArgs.IsTerminating)
			{
				return;
			}
			Exception ex = eventArgs.ExceptionObject as Exception;
			if (ex == null)
			{
				return;
			}
			AppDomain appDomain = sender as AppDomain;
			if (appDomain == null)
			{
				return;
			}
			ApplicationManager.RecordFatalException(appDomain, ex);
		}

		// Token: 0x06002127 RID: 8487 RVA: 0x00091584 File Offset: 0x00090584
		public override object InitializeLifetimeService()
		{
			return null;
		}

		// Token: 0x06002128 RID: 8488 RVA: 0x00091588 File Offset: 0x00090588
		public static ApplicationManager GetApplicationManager()
		{
			if (ApplicationManager._theAppManager == null)
			{
				lock (ApplicationManager._applicationManagerStaticLock)
				{
					if (ApplicationManager._theAppManager == null)
					{
						if (HostingEnvironment.IsHosted)
						{
							ApplicationManager._theAppManager = HostingEnvironment.GetApplicationManager();
						}
						if (ApplicationManager._theAppManager == null)
						{
							ApplicationManager._theAppManager = new ApplicationManager();
						}
					}
				}
			}
			return ApplicationManager._theAppManager;
		}

		// Token: 0x06002129 RID: 8489 RVA: 0x000915F0 File Offset: 0x000905F0
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		public void Open()
		{
			Interlocked.Increment(ref this._openCount);
		}

		// Token: 0x0600212A RID: 8490 RVA: 0x000915FE File Offset: 0x000905FE
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		public void Close()
		{
			if (Interlocked.Decrement(ref this._openCount) > 0)
			{
				return;
			}
			this.ShutdownAll();
		}

		// Token: 0x0600212B RID: 8491 RVA: 0x00091618 File Offset: 0x00090618
		private string CreateSimpleAppID(VirtualPath virtualPath, string physicalPath, string siteName)
		{
			string text = virtualPath.VirtualPathString + physicalPath;
			if (!string.IsNullOrEmpty(siteName))
			{
				text += siteName;
			}
			return text.GetHashCode().ToString("x", CultureInfo.InvariantCulture);
		}

		// Token: 0x0600212C RID: 8492 RVA: 0x0009165C File Offset: 0x0009065C
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		public IRegisteredObject CreateObject(IApplicationHost appHost, Type type)
		{
			if (appHost == null)
			{
				throw new ArgumentNullException("appHost");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			string appId = this.CreateSimpleAppID(VirtualPath.Create(appHost.GetVirtualPath()), appHost.GetPhysicalPath(), appHost.GetSiteName());
			return this.CreateObjectInternal(appId, type, appHost, false);
		}

		// Token: 0x0600212D RID: 8493 RVA: 0x000916AD File Offset: 0x000906AD
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public IRegisteredObject CreateObject(string appId, Type type, string virtualPath, string physicalPath, bool failIfExists)
		{
			return this.CreateObject(appId, type, virtualPath, physicalPath, failIfExists, false);
		}

		// Token: 0x0600212E RID: 8494 RVA: 0x000916C0 File Offset: 0x000906C0
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		public IRegisteredObject CreateObject(string appId, Type type, string virtualPath, string physicalPath, bool failIfExists, bool throwOnError)
		{
			if (appId == null)
			{
				throw new ArgumentNullException("appId");
			}
			SimpleApplicationHost appHost = new SimpleApplicationHost(VirtualPath.CreateAbsolute(virtualPath), physicalPath);
			HostingEnvironmentParameters hostingEnvironmentParameters = null;
			if (throwOnError)
			{
				hostingEnvironmentParameters = new HostingEnvironmentParameters();
				hostingEnvironmentParameters.HostingFlags = HostingEnvironmentFlags.ThrowHostingInitErrors;
			}
			return this.CreateObjectInternal(appId, type, appHost, failIfExists, hostingEnvironmentParameters);
		}

		// Token: 0x0600212F RID: 8495 RVA: 0x00091708 File Offset: 0x00090708
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		internal IRegisteredObject CreateObjectInternal(string appId, Type type, IApplicationHost appHost, bool failIfExists)
		{
			if (appId == null)
			{
				throw new ArgumentNullException("appId");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (appHost == null)
			{
				throw new ArgumentNullException("appHost");
			}
			return this.CreateObjectInternal(appId, type, appHost, failIfExists, null);
		}

		// Token: 0x06002130 RID: 8496 RVA: 0x00091740 File Offset: 0x00090740
		internal IRegisteredObject CreateObjectInternal(string appId, Type type, IApplicationHost appHost, bool failIfExists, HostingEnvironmentParameters hostingParameters)
		{
			if (!typeof(IRegisteredObject).IsAssignableFrom(type))
			{
				throw new ArgumentException(SR.GetString("Not_IRegisteredObject", new object[]
				{
					type.FullName
				}), "type");
			}
			HostingEnvironment appDomainWithHostingEnvironment = this.GetAppDomainWithHostingEnvironment(appId, appHost, hostingParameters);
			ObjectHandle objectHandle = appDomainWithHostingEnvironment.CreateWellKnownObjectInstance(type, failIfExists);
			if (objectHandle == null)
			{
				return null;
			}
			return objectHandle.Unwrap() as IRegisteredObject;
		}

		// Token: 0x06002131 RID: 8497 RVA: 0x000917AA File Offset: 0x000907AA
		internal IRegisteredObject CreateObjectWithDefaultAppHostAndAppId(string physicalPath, string virtualPath, Type type, out string appId)
		{
			return this.CreateObjectWithDefaultAppHostAndAppId(physicalPath, VirtualPath.CreateNonRelative(virtualPath), type, out appId);
		}

		// Token: 0x06002132 RID: 8498 RVA: 0x000917BC File Offset: 0x000907BC
		internal IRegisteredObject CreateObjectWithDefaultAppHostAndAppId(string physicalPath, VirtualPath virtualPath, Type type, out string appId)
		{
			return this.CreateObjectWithDefaultAppHostAndAppId(physicalPath, virtualPath, type, false, new HostingEnvironmentParameters
			{
				HostingFlags = HostingEnvironmentFlags.DontCallAppInitialize
			}, out appId);
		}

		// Token: 0x06002133 RID: 8499 RVA: 0x000917E4 File Offset: 0x000907E4
		internal IRegisteredObject CreateObjectWithDefaultAppHostAndAppId(string physicalPath, VirtualPath virtualPath, Type type, bool failIfExists, HostingEnvironmentParameters hostingParameters, out string appId)
		{
			IApplicationHost applicationHost;
			if (physicalPath == null)
			{
				HttpRuntime.ForceStaticInit();
				ISAPIApplicationHost isapiapplicationHost = new ISAPIApplicationHost(virtualPath.VirtualPathString, null, true);
				applicationHost = isapiapplicationHost;
				appId = isapiapplicationHost.AppId;
				virtualPath = VirtualPath.Create(applicationHost.GetVirtualPath());
				physicalPath = FileUtil.FixUpPhysicalDirectory(applicationHost.GetPhysicalPath());
			}
			else
			{
				appId = this.CreateSimpleAppID(virtualPath, physicalPath, null);
				applicationHost = new SimpleApplicationHost(virtualPath, physicalPath);
			}
			string precompilationTargetPhysicalDirectory = hostingParameters.PrecompilationTargetPhysicalDirectory;
			if (precompilationTargetPhysicalDirectory != null)
			{
				BuildManager.VerifyUnrelatedSourceAndDest(physicalPath, precompilationTargetPhysicalDirectory);
				if (hostingParameters.ClientBuildManagerParameter != null && (hostingParameters.ClientBuildManagerParameter.PrecompilationFlags & PrecompilationFlags.Updatable) == PrecompilationFlags.Default)
				{
					appId += "_precompile";
				}
				else
				{
					appId += "_precompile_u";
				}
			}
			return this.CreateObjectInternal(appId, type, applicationHost, failIfExists, hostingParameters);
		}

		// Token: 0x06002134 RID: 8500 RVA: 0x000918A0 File Offset: 0x000908A0
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		public IRegisteredObject GetObject(string appId, Type type)
		{
			if (appId == null)
			{
				throw new ArgumentNullException("appId");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			HostingEnvironment hostingEnvironment = this.FindAppDomainWithHostingEnvironment(appId);
			if (hostingEnvironment == null)
			{
				return null;
			}
			ObjectHandle objectHandle = hostingEnvironment.FindWellKnownObject(type);
			if (objectHandle == null)
			{
				return null;
			}
			return objectHandle.Unwrap() as IRegisteredObject;
		}

		// Token: 0x06002135 RID: 8501 RVA: 0x000918F0 File Offset: 0x000908F0
		internal void RemoveFromTableIfRuntimeExists(string appId, Type runtimeType)
		{
			if (appId == null)
			{
				throw new ArgumentNullException("appId");
			}
			if (runtimeType == null)
			{
				throw new ArgumentNullException("runtimeType");
			}
			HostingEnvironment hostingEnvironment = this.FindAppDomainWithHostingEnvironment(appId);
			if (hostingEnvironment == null)
			{
				return;
			}
			ObjectHandle objectHandle = hostingEnvironment.FindWellKnownObject(runtimeType);
			if (objectHandle != null)
			{
				this.HostingEnvironmentShutdownInitiated(appId, hostingEnvironment);
			}
		}

		// Token: 0x06002136 RID: 8502 RVA: 0x00091938 File Offset: 0x00090938
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		public void StopObject(string appId, Type type)
		{
			if (appId == null)
			{
				throw new ArgumentNullException("appId");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			HostingEnvironment hostingEnvironment = this.FindAppDomainWithHostingEnvironment(appId);
			if (hostingEnvironment != null)
			{
				hostingEnvironment.StopWellKnownObject(type);
			}
		}

		// Token: 0x06002137 RID: 8503 RVA: 0x00091974 File Offset: 0x00090974
		public bool IsIdle()
		{
			lock (this)
			{
				foreach (object obj in this._appDomains)
				{
					if (!((HostingEnvironment)((DictionaryEntry)obj).Value).IsIdle())
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06002138 RID: 8504 RVA: 0x00091A08 File Offset: 0x00090A08
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		public void ShutdownApplication(string appId)
		{
			if (appId == null)
			{
				throw new ArgumentNullException("appId");
			}
			HostingEnvironment hostingEnvironment = this.FindAppDomainWithHostingEnvironment(appId);
			if (hostingEnvironment != null)
			{
				hostingEnvironment.InitiateShutdownInternal();
			}
		}

		// Token: 0x06002139 RID: 8505 RVA: 0x00091A34 File Offset: 0x00090A34
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		public void ShutdownAll()
		{
			this._shutdownInProgress = true;
			lock (this)
			{
				foreach (object obj in this._appDomains)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					this._appDomainsShutdowdIds.Append(string.Concat(new object[]
					{
						"SA:",
						dictionaryEntry.Key,
						":",
						DateTime.UtcNow.ToShortTimeString(),
						";"
					}));
					((HostingEnvironment)dictionaryEntry.Value).InitiateShutdownInternal();
				}
				this._appDomains = new Hashtable();
			}
			int num = 0;
			while (this._activeHostingEnvCount > 0 && num < 3000)
			{
				Thread.Sleep(100);
				num++;
			}
		}

		// Token: 0x0600213A RID: 8506 RVA: 0x00091B3C File Offset: 0x00090B3C
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		public ApplicationInfo[] GetRunningApplications()
		{
			ArrayList arrayList = new ArrayList();
			lock (this)
			{
				foreach (object obj in this._appDomains)
				{
					arrayList.Add(((HostingEnvironment)((DictionaryEntry)obj).Value).GetApplicationInfo());
				}
			}
			int count = arrayList.Count;
			ApplicationInfo[] array = new ApplicationInfo[count];
			if (count > 0)
			{
				arrayList.CopyTo(array);
			}
			return array;
		}

		// Token: 0x0600213B RID: 8507 RVA: 0x00091BEC File Offset: 0x00090BEC
		internal AppDomainInfo[] GetAppDomainInfos()
		{
			ArrayList arrayList = new ArrayList();
			lock (this)
			{
				foreach (object obj in this._appDomains)
				{
					HostingEnvironment hostingEnvironment = (HostingEnvironment)((DictionaryEntry)obj).Value;
					IApplicationHost internalApplicationHost = hostingEnvironment.InternalApplicationHost;
					ApplicationInfo applicationInfo = hostingEnvironment.GetApplicationInfo();
					int siteId;
					if (internalApplicationHost != null)
					{
						try
						{
							siteId = int.Parse(internalApplicationHost.GetSiteID(), CultureInfo.InvariantCulture);
							goto IL_6A;
						}
						catch
						{
							siteId = 0;
							goto IL_6A;
						}
						goto IL_67;
					}
					goto IL_67;
					IL_6A:
					AppDomainInfo value = new AppDomainInfo(applicationInfo.ID, applicationInfo.VirtualPath, applicationInfo.PhysicalPath, siteId, hostingEnvironment.GetIdleValue());
					arrayList.Add(value);
					continue;
					IL_67:
					siteId = 0;
					goto IL_6A;
				}
			}
			return (AppDomainInfo[])arrayList.ToArray(typeof(AppDomainInfo));
		}

		// Token: 0x0600213C RID: 8508 RVA: 0x00091CF4 File Offset: 0x00090CF4
		internal void Ping(IProcessPingCallback callback)
		{
			if (callback == null || this._pendingPingCallback != null)
			{
				return;
			}
			if (Interlocked.CompareExchange(ref this._pendingPingCallback, callback, null) == null)
			{
				ThreadPool.QueueUserWorkItem(this._onRespondToPingWaitCallback);
			}
		}

		// Token: 0x0600213D RID: 8509 RVA: 0x00091D1D File Offset: 0x00090D1D
		internal void OnRespondToPingWaitCallback(object state)
		{
			this.RespondToPingIfNeeded();
		}

		// Token: 0x0600213E RID: 8510 RVA: 0x00091D28 File Offset: 0x00090D28
		internal void RespondToPingIfNeeded()
		{
			IProcessPingCallback processPingCallback = this._pendingPingCallback as IProcessPingCallback;
			if (processPingCallback != null && Interlocked.CompareExchange(ref this._pendingPingCallback, null, processPingCallback) == processPingCallback)
			{
				processPingCallback.Respond();
			}
		}

		// Token: 0x0600213F RID: 8511 RVA: 0x00091D5A File Offset: 0x00090D5A
		internal void HostingEnvironmentActivated(string appId)
		{
			Interlocked.Increment(ref this._activeHostingEnvCount);
		}

		// Token: 0x06002140 RID: 8512 RVA: 0x00091D68 File Offset: 0x00090D68
		internal void HostingEnvironmentShutdownComplete(string appId, IApplicationHost appHost)
		{
			try
			{
				if (appHost != null)
				{
					MarshalByRefObject marshalByRefObject = appHost as MarshalByRefObject;
					if (marshalByRefObject != null)
					{
						RemotingServices.Disconnect(marshalByRefObject);
					}
				}
			}
			finally
			{
				Interlocked.Decrement(ref this._activeHostingEnvCount);
			}
		}

		// Token: 0x06002141 RID: 8513 RVA: 0x00091DA8 File Offset: 0x00090DA8
		internal void HostingEnvironmentShutdownInitiated(string appId, HostingEnvironment env)
		{
			if (!this._shutdownInProgress)
			{
				lock (this)
				{
					if (!env.HasBeenRemovedFromAppManagerTable)
					{
						env.HasBeenRemovedFromAppManagerTable = true;
						this._appDomainsShutdowdIds.Append(string.Concat(new string[]
						{
							"SI:",
							appId,
							":",
							DateTime.UtcNow.ToShortTimeString(),
							";"
						}));
						this._appDomains.Remove(appId);
					}
				}
			}
		}

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x06002142 RID: 8514 RVA: 0x00091E40 File Offset: 0x00090E40
		internal int AppDomainsCount
		{
			get
			{
				int result = 0;
				lock (this)
				{
					result = this._appDomains.Count;
				}
				return result;
			}
		}

		// Token: 0x06002143 RID: 8515 RVA: 0x00091E80 File Offset: 0x00090E80
		internal void ReduceAppDomainsCount(int limit)
		{
			while (this._appDomains.Count >= limit && !this._shutdownInProgress)
			{
				HostingEnvironment hostingEnvironment = null;
				lock (this)
				{
					foreach (object obj in this._appDomains)
					{
						HostingEnvironment hostingEnvironment2 = (HostingEnvironment)((DictionaryEntry)obj).Value;
						if (hostingEnvironment == null || hostingEnvironment2.LruScore < hostingEnvironment.LruScore)
						{
							hostingEnvironment = hostingEnvironment2;
						}
					}
				}
				if (hostingEnvironment == null)
				{
					return;
				}
				hostingEnvironment.InitiateShutdownInternal();
			}
		}

		// Token: 0x06002144 RID: 8516 RVA: 0x00091F40 File Offset: 0x00090F40
		internal ObjectHandle CreateInstanceInNewWorkerAppDomain(Type type, string appId, VirtualPath virtualPath, string physicalPath)
		{
			IApplicationHost appHost = new SimpleApplicationHost(virtualPath, physicalPath);
			HostingEnvironment hostingEnvironment = this.CreateAppDomainWithHostingEnvironmentAndReportErrors(appId, appHost, new HostingEnvironmentParameters
			{
				HostingFlags = HostingEnvironmentFlags.HideFromAppManager
			});
			return hostingEnvironment.CreateInstance(type);
		}

		// Token: 0x06002145 RID: 8517 RVA: 0x00091F74 File Offset: 0x00090F74
		private HostingEnvironment FindAppDomainWithHostingEnvironment(string appId)
		{
			HostingEnvironment result = null;
			lock (this)
			{
				result = (this._appDomains[appId] as HostingEnvironment);
			}
			return result;
		}

		// Token: 0x06002146 RID: 8518 RVA: 0x00091FB8 File Offset: 0x00090FB8
		private HostingEnvironment GetAppDomainWithHostingEnvironment(string appId, IApplicationHost appHost, HostingEnvironmentParameters hostingParameters)
		{
			HostingEnvironment hostingEnvironment = null;
			lock (this)
			{
				hostingEnvironment = (this._appDomains[appId] as HostingEnvironment);
				if (hostingEnvironment != null)
				{
					try
					{
						hostingEnvironment.IsUnloaded();
					}
					catch (AppDomainUnloadedException)
					{
						hostingEnvironment = null;
						this._appDomainsShutdowdIds.Append(string.Concat(new string[]
						{
							"Un:",
							appId,
							":",
							DateTime.UtcNow.ToShortTimeString(),
							";"
						}));
					}
				}
				if (hostingEnvironment == null)
				{
					hostingEnvironment = this.CreateAppDomainWithHostingEnvironmentAndReportErrors(appId, appHost, hostingParameters);
					this._appDomains[appId] = hostingEnvironment;
				}
			}
			return hostingEnvironment;
		}

		// Token: 0x06002147 RID: 8519 RVA: 0x00092078 File Offset: 0x00091078
		private HostingEnvironment CreateAppDomainWithHostingEnvironmentAndReportErrors(string appId, IApplicationHost appHost, HostingEnvironmentParameters hostingParameters)
		{
			HostingEnvironment result;
			try
			{
				result = this.CreateAppDomainWithHostingEnvironment(appId, appHost, hostingParameters);
			}
			catch (Exception e)
			{
				Misc.ReportUnhandledException(e, new string[]
				{
					SR.GetString("Failed_to_initialize_AppDomain"),
					appId
				});
				throw;
			}
			return result;
		}

		// Token: 0x06002148 RID: 8520 RVA: 0x000920C4 File Offset: 0x000910C4
		private HostingEnvironment CreateAppDomainWithHostingEnvironment(string appId, IApplicationHost appHost, HostingEnvironmentParameters hostingParameters)
		{
			string text = appHost.GetPhysicalPath();
			if (!StringUtil.StringEndsWith(text, Path.DirectorySeparatorChar))
			{
				text += Path.DirectorySeparatorChar;
			}
			string text2 = ApplicationManager.ConstructAppDomainId(appId);
			string appName = StringUtil.GetStringHashCode(appId.ToLower(CultureInfo.InvariantCulture) + text.ToLower(CultureInfo.InvariantCulture)).ToString("x", CultureInfo.InvariantCulture);
			VirtualPath appVPath = VirtualPath.Create(appHost.GetVirtualPath());
			IDictionary dictionary = new Hashtable(20);
			AppDomainSetup appDomainSetup = new AppDomainSetup();
			ApplicationManager.PopulateDomainBindings(text2, appId, appName, text, appVPath, appDomainSetup, dictionary);
			AppDomain appDomain = null;
			Exception innerException = null;
			try
			{
				appDomain = AppDomain.CreateDomain(text2, ApplicationManager.GetDefaultDomainIdentity(), appDomainSetup);
				foreach (object obj in dictionary)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					appDomain.SetData((string)dictionaryEntry.Key, (string)dictionaryEntry.Value);
				}
			}
			catch (Exception ex)
			{
				innerException = ex;
			}
			if (appDomain == null)
			{
				throw new SystemException(SR.GetString("Cannot_create_AppDomain"), innerException);
			}
			Type typeFromHandle = typeof(HostingEnvironment);
			string fullName = typeFromHandle.Module.Assembly.FullName;
			string fullName2 = typeFromHandle.FullName;
			ObjectHandle objectHandle = null;
			ImpersonationContext impersonationContext = null;
			IntPtr intPtr = IntPtr.Zero;
			int num = 10;
			int i = 0;
			while (i < num)
			{
				try
				{
					intPtr = appHost.GetConfigToken();
					break;
				}
				catch (InvalidOperationException)
				{
					i++;
					Thread.Sleep(250);
				}
			}
			if (intPtr != IntPtr.Zero)
			{
				try
				{
					impersonationContext = new ImpersonationContext(intPtr);
				}
				catch
				{
				}
				finally
				{
					UnsafeNativeMethods.CloseHandle(intPtr);
				}
			}
			try
			{
				objectHandle = appDomain.CreateInstance(fullName, fullName2);
			}
			finally
			{
				if (impersonationContext != null)
				{
					impersonationContext.Undo();
				}
				if (objectHandle == null)
				{
					AppDomain.Unload(appDomain);
				}
			}
			HostingEnvironment hostingEnvironment = (objectHandle != null) ? (objectHandle.Unwrap() as HostingEnvironment) : null;
			if (hostingEnvironment == null)
			{
				throw new SystemException(SR.GetString("Cannot_create_HostEnv"));
			}
			bool wasLaunchedFromDevelopmentEnvironment = ApplicationManager.EnvironmentInfo.WasLaunchedFromDevelopmentEnvironment;
			IConfigMapPathFactory configMapPathFactory = appHost.GetConfigMapPathFactory();
			hostingEnvironment.Initialize(this, appHost, configMapPathFactory, hostingParameters);
			if (wasLaunchedFromDevelopmentEnvironment)
			{
				appDomain.DoCallBack(new CrossAppDomainDelegate(ApplicationManager.SetAppDomainAdditionalData));
			}
			return hostingEnvironment;
		}

		// Token: 0x06002149 RID: 8521 RVA: 0x00092340 File Offset: 0x00091340
		private static void SetAppDomainAdditionalData()
		{
			try
			{
				CachedPathData machinePathData = CachedPathData.GetMachinePathData();
				if (machinePathData != null && machinePathData.ConfigRecord != null)
				{
					DeploymentSection deploymentSection = machinePathData.ConfigRecord.GetSection("system.web/deployment") as DeploymentSection;
					if (deploymentSection != null && !deploymentSection.Retail)
					{
						AppDomain.CurrentDomain.SetData(".devEnvironment", true);
						AppDomain.CurrentDomain.SetData("ALLOW_LOCALDB_IN_PARTIAL_TRUST", true);
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x0600214A RID: 8522 RVA: 0x000923C0 File Offset: 0x000913C0
		private static void PopulateDomainBindings(string domainId, string appId, string appName, string appPath, VirtualPath appVPath, AppDomainSetup setup, IDictionary dict)
		{
			setup.PrivateBinPathProbe = "*";
			setup.ShadowCopyFiles = "true";
			setup.ApplicationBase = appPath;
			setup.ApplicationName = appName;
			setup.ConfigurationFile = "web.config";
			setup.DisallowCodeDownload = true;
			dict.Add(".appDomain", "*");
			dict.Add(".appId", appId);
			dict.Add(".appPath", appPath);
			dict.Add(".appVPath", appVPath.VirtualPathString);
			dict.Add(".domainId", domainId);
		}

		// Token: 0x0600214B RID: 8523 RVA: 0x00092454 File Offset: 0x00091454
		private static Evidence GetDefaultDomainIdentity()
		{
			Evidence evidence = new Evidence();
			bool flag = false;
			bool flag2 = false;
			IEnumerator enumerator = AppDomain.CurrentDomain.Evidence.GetHostEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Current is Zone)
				{
					flag = true;
				}
				if (enumerator.Current is Url)
				{
					flag2 = true;
				}
				evidence.AddHost(enumerator.Current);
			}
			enumerator = AppDomain.CurrentDomain.Evidence.GetAssemblyEnumerator();
			while (enumerator.MoveNext())
			{
				object id = enumerator.Current;
				evidence.AddAssembly(id);
			}
			if (!flag)
			{
				evidence.AddHost(new Zone(SecurityZone.MyComputer));
			}
			if (!flag2)
			{
				evidence.AddHost(new Url("ms-internal-microsoft-asp-net-webhost-20"));
			}
			return evidence;
		}

		// Token: 0x0600214C RID: 8524 RVA: 0x000924F8 File Offset: 0x000914F8
		private static string ConstructAppDomainId(string id)
		{
			int num = 0;
			lock (ApplicationManager.s_domainCountLock)
			{
				num = ++ApplicationManager.s_domainCount;
			}
			return string.Concat(new string[]
			{
				id,
				"-",
				num.ToString(NumberFormatInfo.InvariantInfo),
				"-",
				DateTime.UtcNow.ToFileTime().ToString()
			});
		}

		// Token: 0x04001AF3 RID: 6899
		private static object _applicationManagerStaticLock = new object();

		// Token: 0x04001AF4 RID: 6900
		private int _openCount;

		// Token: 0x04001AF5 RID: 6901
		private bool _shutdownInProgress;

		// Token: 0x04001AF6 RID: 6902
		private Hashtable _appDomains = new Hashtable(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04001AF7 RID: 6903
		private int _activeHostingEnvCount;

		// Token: 0x04001AF8 RID: 6904
		private object _pendingPingCallback;

		// Token: 0x04001AF9 RID: 6905
		private WaitCallback _onRespondToPingWaitCallback;

		// Token: 0x04001AFA RID: 6906
		private static ApplicationManager _theAppManager;

		// Token: 0x04001AFB RID: 6907
		private StringBuilder _appDomainsShutdowdIds = new StringBuilder();

		// Token: 0x04001AFC RID: 6908
		private static Exception _fatalException = null;

		// Token: 0x04001AFD RID: 6909
		private static int s_domainCount = 0;

		// Token: 0x04001AFE RID: 6910
		private static object s_domainCountLock = new object();

		// Token: 0x02000287 RID: 647
		private static class EnvironmentInfo
		{
			// Token: 0x0600214E RID: 8526 RVA: 0x000925A8 File Offset: 0x000915A8
			private static bool GetWasLaunchedFromDevelopmentEnvironmentValue()
			{
				bool result;
				try
				{
					string environmentVariable = Environment.GetEnvironmentVariable("DEV_ENVIRONMENT", EnvironmentVariableTarget.Process);
					result = string.Equals(environmentVariable, "1", StringComparison.Ordinal);
				}
				catch
				{
					result = false;
				}
				return result;
			}

			// Token: 0x04001AFF RID: 6911
			public static readonly bool WasLaunchedFromDevelopmentEnvironment = ApplicationManager.EnvironmentInfo.GetWasLaunchedFromDevelopmentEnvironmentValue();
		}
	}
}
