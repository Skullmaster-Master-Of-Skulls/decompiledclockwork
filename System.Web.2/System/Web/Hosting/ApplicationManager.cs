using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Configuration.Provider;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Runtime.Versioning;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Compilation;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.Util;

namespace System.Web.Hosting
{
	// Token: 0x020007A7 RID: 1959
	public sealed class ApplicationManager : MarshalByRefObject
	{
		// Token: 0x06005CD0 RID: 23760 RVA: 0x00140BFD File Offset: 0x0013EDFD
		internal ApplicationManager()
		{
			this._onRespondToPingWaitCallback = new WaitCallback(this.OnRespondToPingWaitCallback);
			AppDomain.CurrentDomain.UnhandledException += ApplicationManager.OnUnhandledException;
		}

		// Token: 0x17001B1A RID: 6938
		// (get) Token: 0x06005CD1 RID: 23761 RVA: 0x00140C3D File Offset: 0x0013EE3D
		internal bool ShutdownInProgress
		{
			get
			{
				return this._shutdownInProgress;
			}
		}

		// Token: 0x17001B1B RID: 6939
		// (get) Token: 0x06005CD2 RID: 23762 RVA: 0x00140C45 File Offset: 0x0013EE45
		// (set) Token: 0x06005CD3 RID: 23763 RVA: 0x00140C4D File Offset: 0x0013EE4D
		private bool FatalExceptionRecorded
		{
			get
			{
				return this._fatalExceptionRecorded;
			}
			set
			{
				this._fatalExceptionRecorded = value;
			}
		}

		// Token: 0x06005CD4 RID: 23764 RVA: 0x00140C56 File Offset: 0x0013EE56
		internal static void RecordFatalException(Exception e)
		{
			ApplicationManager.RecordFatalException(AppDomain.CurrentDomain, e);
		}

		// Token: 0x06005CD5 RID: 23765 RVA: 0x00140C64 File Offset: 0x0013EE64
		internal static void RecordFatalException(AppDomain appDomain, Exception e)
		{
			if (Interlocked.CompareExchange<Exception>(ref ApplicationManager._fatalException, e, null) == null)
			{
				Misc.WriteUnhandledExceptionToEventLog(appDomain, e);
			}
		}

		// Token: 0x06005CD6 RID: 23766 RVA: 0x00140C88 File Offset: 0x0013EE88
		internal static void OnUnhandledException(object sender, UnhandledExceptionEventArgs eventArgs)
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
			ApplicationManager applicationManager = ApplicationManager.GetApplicationManager();
			if (AppDomain.CurrentDomain.IsDefaultAppDomain() && applicationManager.FatalExceptionRecorded)
			{
				return;
			}
			applicationManager.FatalExceptionRecorded = true;
			ApplicationManager.RecordFatalException(appDomain, ex);
		}

		// Token: 0x06005CD7 RID: 23767 RVA: 0x0000298D File Offset: 0x00000B8D
		public override object InitializeLifetimeService()
		{
			return null;
		}

		// Token: 0x06005CD8 RID: 23768 RVA: 0x00140CE4 File Offset: 0x0013EEE4
		public static ApplicationManager GetApplicationManager()
		{
			if (ApplicationManager._theAppManager == null)
			{
				object applicationManagerStaticLock = ApplicationManager._applicationManagerStaticLock;
				lock (applicationManagerStaticLock)
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

		// Token: 0x06005CD9 RID: 23769 RVA: 0x00140D54 File Offset: 0x0013EF54
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		public void Open()
		{
			Interlocked.Increment(ref this._openCount);
		}

		// Token: 0x06005CDA RID: 23770 RVA: 0x00140D62 File Offset: 0x0013EF62
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		public void Close()
		{
			if (Interlocked.Decrement(ref this._openCount) > 0)
			{
				return;
			}
			this.ShutdownAll();
		}

		// Token: 0x06005CDB RID: 23771 RVA: 0x00140D7C File Offset: 0x0013EF7C
		private string CreateSimpleAppID(VirtualPath virtualPath, string physicalPath, string siteName)
		{
			string text = virtualPath.VirtualPathString + physicalPath;
			if (!string.IsNullOrEmpty(siteName))
			{
				text += siteName;
			}
			return text.GetHashCode().ToString("x", CultureInfo.InvariantCulture);
		}

		// Token: 0x06005CDC RID: 23772 RVA: 0x00140DC0 File Offset: 0x0013EFC0
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
			string appId = this.CreateSimpleAppID(appHost);
			return this.CreateObjectInternal(appId, type, appHost, false);
		}

		// Token: 0x06005CDD RID: 23773 RVA: 0x00140E01 File Offset: 0x0013F001
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public IRegisteredObject CreateObject(string appId, Type type, string virtualPath, string physicalPath, bool failIfExists)
		{
			return this.CreateObject(appId, type, virtualPath, physicalPath, failIfExists, false);
		}

		// Token: 0x06005CDE RID: 23774 RVA: 0x00140E14 File Offset: 0x0013F014
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

		// Token: 0x06005CDF RID: 23775 RVA: 0x00140E5C File Offset: 0x0013F05C
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

		// Token: 0x06005CE0 RID: 23776 RVA: 0x00140E9C File Offset: 0x0013F09C
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
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
			ObjectHandle objectHandle = appDomainWithHostingEnvironment.CreateWellKnownObjectInstance(type.AssemblyQualifiedName, failIfExists);
			if (objectHandle == null)
			{
				return null;
			}
			return objectHandle.Unwrap() as IRegisteredObject;
		}

		// Token: 0x06005CE1 RID: 23777 RVA: 0x00140F09 File Offset: 0x0013F109
		internal IRegisteredObject CreateObjectWithDefaultAppHostAndAppId(string physicalPath, string virtualPath, Type type, out string appId, out IApplicationHost appHost)
		{
			return this.CreateObjectWithDefaultAppHostAndAppId(physicalPath, VirtualPath.CreateNonRelative(virtualPath), type, out appId, out appHost);
		}

		// Token: 0x06005CE2 RID: 23778 RVA: 0x00140F20 File Offset: 0x0013F120
		internal IRegisteredObject CreateObjectWithDefaultAppHostAndAppId(string physicalPath, VirtualPath virtualPath, Type type, out string appId, out IApplicationHost appHost)
		{
			return this.CreateObjectWithDefaultAppHostAndAppId(physicalPath, virtualPath, type, false, new HostingEnvironmentParameters
			{
				HostingFlags = HostingEnvironmentFlags.DontCallAppInitialize
			}, out appId, out appHost);
		}

		// Token: 0x06005CE3 RID: 23779 RVA: 0x00140F4C File Offset: 0x0013F14C
		internal IRegisteredObject CreateObjectWithDefaultAppHostAndAppId(string physicalPath, VirtualPath virtualPath, Type type, bool failIfExists, HostingEnvironmentParameters hostingParameters, out string appId, out IApplicationHost appHost)
		{
			if (physicalPath == null)
			{
				HttpRuntime.ForceStaticInit();
				ISAPIApplicationHost isapiapplicationHost = new ISAPIApplicationHost(virtualPath.VirtualPathString, null, true, null, hostingParameters.IISExpressVersion);
				appHost = isapiapplicationHost;
				appId = isapiapplicationHost.AppId;
				virtualPath = VirtualPath.Create(appHost.GetVirtualPath());
				physicalPath = FileUtil.FixUpPhysicalDirectory(appHost.GetPhysicalPath());
			}
			else
			{
				appId = this.CreateSimpleAppID(virtualPath, physicalPath, null);
				appHost = new SimpleApplicationHost(virtualPath, physicalPath);
			}
			string precompilationTargetPhysicalDirectory = hostingParameters.PrecompilationTargetPhysicalDirectory;
			if (precompilationTargetPhysicalDirectory != null)
			{
				if (hostingParameters.ClientBuildManagerParameter != null && (hostingParameters.ClientBuildManagerParameter.PrecompilationFlags & PrecompilationFlags.Updatable) == PrecompilationFlags.Default)
				{
					appId += "_precompile";
				}
				else
				{
					appId += "_precompile_u";
				}
			}
			return this.CreateObjectInternal(appId, type, appHost, failIfExists, hostingParameters);
		}

		// Token: 0x06005CE4 RID: 23780 RVA: 0x00141014 File Offset: 0x0013F214
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
			LockableAppDomainContext lockableAppDomainContext = this.GetLockableAppDomainContext(appId);
			LockableAppDomainContext obj = lockableAppDomainContext;
			IRegisteredObject result;
			lock (obj)
			{
				HostingEnvironment hostEnv = lockableAppDomainContext.HostEnv;
				if (hostEnv == null)
				{
					result = null;
				}
				else
				{
					ObjectHandle objectHandle = hostEnv.FindWellKnownObject(type.AssemblyQualifiedName);
					result = ((objectHandle != null) ? (objectHandle.Unwrap() as IRegisteredObject) : null);
				}
			}
			return result;
		}

		// Token: 0x06005CE5 RID: 23781 RVA: 0x001410A8 File Offset: 0x0013F2A8
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		public AppDomain GetAppDomain(string appId)
		{
			if (appId == null)
			{
				throw new ArgumentNullException("appId");
			}
			LockableAppDomainContext lockableAppDomainContext = this.GetLockableAppDomainContext(appId);
			LockableAppDomainContext obj = lockableAppDomainContext;
			AppDomain result;
			lock (obj)
			{
				HostingEnvironment hostEnv = lockableAppDomainContext.HostEnv;
				if (hostEnv == null)
				{
					result = null;
				}
				else
				{
					result = hostEnv.HostedAppDomain;
				}
			}
			return result;
		}

		// Token: 0x06005CE6 RID: 23782 RVA: 0x0014110C File Offset: 0x0013F30C
		public AppDomain GetAppDomain(IApplicationHost appHost)
		{
			if (appHost == null)
			{
				throw new ArgumentNullException("appHost");
			}
			string appId = this.CreateSimpleAppID(appHost);
			return this.GetAppDomain(appId);
		}

		// Token: 0x06005CE7 RID: 23783 RVA: 0x00141136 File Offset: 0x0013F336
		internal AppDomain GetDefaultAppDomain()
		{
			return AppDomain.CurrentDomain;
		}

		// Token: 0x06005CE8 RID: 23784 RVA: 0x0014113D File Offset: 0x0013F33D
		private string CreateSimpleAppID(IApplicationHost appHost)
		{
			if (appHost == null)
			{
				throw new ArgumentNullException("appHost");
			}
			return this.CreateSimpleAppID(VirtualPath.Create(appHost.GetVirtualPath()), appHost.GetPhysicalPath(), appHost.GetSiteName());
		}

		// Token: 0x06005CE9 RID: 23785 RVA: 0x0014116C File Offset: 0x0013F36C
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
			LockableAppDomainContext lockableAppDomainContext = this.GetLockableAppDomainContext(appId);
			LockableAppDomainContext obj = lockableAppDomainContext;
			lock (obj)
			{
				HostingEnvironment hostEnv = lockableAppDomainContext.HostEnv;
				if (hostEnv != null)
				{
					ObjectHandle objectHandle = hostEnv.FindWellKnownObject(runtimeType.AssemblyQualifiedName);
					if (objectHandle != null)
					{
						this.HostingEnvironmentShutdownInitiated(appId, hostEnv);
					}
				}
			}
		}

		// Token: 0x06005CEA RID: 23786 RVA: 0x001411F4 File Offset: 0x0013F3F4
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
			LockableAppDomainContext lockableAppDomainContext = this.GetLockableAppDomainContext(appId);
			LockableAppDomainContext obj = lockableAppDomainContext;
			lock (obj)
			{
				HostingEnvironment hostEnv = lockableAppDomainContext.HostEnv;
				if (hostEnv != null)
				{
					hostEnv.StopWellKnownObject(type.AssemblyQualifiedName);
				}
			}
		}

		// Token: 0x06005CEB RID: 23787 RVA: 0x0014126C File Offset: 0x0013F46C
		public bool IsIdle()
		{
			Dictionary<string, LockableAppDomainContext> dictionary = this.CloneAppDomainsCollection();
			foreach (LockableAppDomainContext lockableAppDomainContext in dictionary.Values)
			{
				LockableAppDomainContext obj = lockableAppDomainContext;
				lock (obj)
				{
					HostingEnvironment hostEnv = lockableAppDomainContext.HostEnv;
					if (hostEnv != null && !hostEnv.IsIdle())
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06005CEC RID: 23788 RVA: 0x0014130C File Offset: 0x0013F50C
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		public void ShutdownApplication(string appId)
		{
			if (appId == null)
			{
				throw new ArgumentNullException("appId");
			}
			LockableAppDomainContext lockableAppDomainContext = this.GetLockableAppDomainContext(appId);
			LockableAppDomainContext obj = lockableAppDomainContext;
			lock (obj)
			{
				if (lockableAppDomainContext.HostEnv != null)
				{
					lockableAppDomainContext.HostEnv.InitiateShutdownInternal();
				}
			}
		}

		// Token: 0x06005CED RID: 23789 RVA: 0x0014136C File Offset: 0x0013F56C
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		public void ShutdownAll()
		{
			this._shutdownInProgress = true;
			Dictionary<string, LockableAppDomainContext> dictionary = null;
			lock (this)
			{
				dictionary = this._appDomains;
				this._appDomains = new Dictionary<string, LockableAppDomainContext>(StringComparer.OrdinalIgnoreCase);
			}
			foreach (KeyValuePair<string, LockableAppDomainContext> keyValuePair in dictionary)
			{
				LockableAppDomainContext value = keyValuePair.Value;
				LockableAppDomainContext obj = value;
				lock (obj)
				{
					HostingEnvironment hostEnv = value.HostEnv;
					if (hostEnv != null)
					{
						hostEnv.InitiateShutdownInternal();
					}
				}
			}
			int num = 0;
			while (this._activeHostingEnvCount > 0 && num < 3000)
			{
				Thread.Sleep(100);
				num++;
			}
		}

		// Token: 0x06005CEE RID: 23790 RVA: 0x00141464 File Offset: 0x0013F664
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		public ApplicationInfo[] GetRunningApplications()
		{
			ArrayList arrayList = new ArrayList();
			Dictionary<string, LockableAppDomainContext> dictionary = this.CloneAppDomainsCollection();
			foreach (LockableAppDomainContext lockableAppDomainContext in dictionary.Values)
			{
				LockableAppDomainContext obj = lockableAppDomainContext;
				lock (obj)
				{
					HostingEnvironment hostEnv = lockableAppDomainContext.HostEnv;
					if (hostEnv != null)
					{
						arrayList.Add(hostEnv.GetApplicationInfo());
					}
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

		// Token: 0x06005CEF RID: 23791 RVA: 0x00141520 File Offset: 0x0013F720
		internal AppDomainInfo[] GetAppDomainInfos()
		{
			ArrayList arrayList = new ArrayList();
			Dictionary<string, LockableAppDomainContext> dictionary = this.CloneAppDomainsCollection();
			foreach (LockableAppDomainContext lockableAppDomainContext in dictionary.Values)
			{
				LockableAppDomainContext obj = lockableAppDomainContext;
				lock (obj)
				{
					HostingEnvironment hostEnv = lockableAppDomainContext.HostEnv;
					if (hostEnv != null)
					{
						IApplicationHost internalApplicationHost = hostEnv.InternalApplicationHost;
						ApplicationInfo applicationInfo = hostEnv.GetApplicationInfo();
						int siteId = 0;
						if (internalApplicationHost != null)
						{
							try
							{
								siteId = int.Parse(internalApplicationHost.GetSiteID(), CultureInfo.InvariantCulture);
							}
							catch
							{
							}
						}
						AppDomainInfo value = new AppDomainInfo(applicationInfo.ID, applicationInfo.VirtualPath, applicationInfo.PhysicalPath, siteId, hostEnv.GetIdleValue());
						arrayList.Add(value);
					}
				}
			}
			return (AppDomainInfo[])arrayList.ToArray(typeof(AppDomainInfo));
		}

		// Token: 0x06005CF0 RID: 23792 RVA: 0x00141638 File Offset: 0x0013F838
		internal object SuspendAllApplications()
		{
			LockableAppDomainContext[] source;
			lock (this)
			{
				source = this._appDomains.Values.ToArray<LockableAppDomainContext>();
			}
			return Task.WhenAll<ApplicationManager.ApplicationResumeStateContainer>(source.Select(new Func<LockableAppDomainContext, Task<ApplicationManager.ApplicationResumeStateContainer>>(ApplicationManager.CreateSuspendTask))).Result;
		}

		// Token: 0x06005CF1 RID: 23793 RVA: 0x0014169C File Offset: 0x0013F89C
		private static Task<ApplicationManager.ApplicationResumeStateContainer> CreateSuspendTask(LockableAppDomainContext appDomainContext)
		{
			if (appDomainContext == null)
			{
				return ApplicationManager._dummyCompletedSuspendTask;
			}
			HostingEnvironment hostEnv;
			lock (appDomainContext)
			{
				hostEnv = appDomainContext.HostEnv;
			}
			if (hostEnv == null)
			{
				return ApplicationManager._dummyCompletedSuspendTask;
			}
			TaskCompletionSource<ApplicationManager.ApplicationResumeStateContainer> tcs = new TaskCompletionSource<ApplicationManager.ApplicationResumeStateContainer>();
			ThreadPool.UnsafeQueueUserWorkItem(delegate(object _)
			{
				IntPtr resumeState;
				try
				{
					resumeState = hostEnv.SuspendApplication();
				}
				catch (AppDomainUnloadedException)
				{
					tcs.TrySetResult(null);
					return;
				}
				tcs.TrySetResult(new ApplicationManager.ApplicationResumeStateContainer(hostEnv, resumeState));
			}, null);
			return tcs.Task;
		}

		// Token: 0x06005CF2 RID: 23794 RVA: 0x00141724 File Offset: 0x0013F924
		internal void ResumeAllApplications(object state)
		{
			foreach (ApplicationManager.ApplicationResumeStateContainer applicationResumeStateContainer in (ApplicationManager.ApplicationResumeStateContainer[])state)
			{
				if (applicationResumeStateContainer != null)
				{
					applicationResumeStateContainer.Resume();
				}
			}
		}

		// Token: 0x06005CF3 RID: 23795 RVA: 0x00141753 File Offset: 0x0013F953
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

		// Token: 0x06005CF4 RID: 23796 RVA: 0x0014177C File Offset: 0x0013F97C
		internal void OnRespondToPingWaitCallback(object state)
		{
			this.RespondToPingIfNeeded();
		}

		// Token: 0x06005CF5 RID: 23797 RVA: 0x00141784 File Offset: 0x0013F984
		internal void RespondToPingIfNeeded()
		{
			IProcessPingCallback processPingCallback = this._pendingPingCallback as IProcessPingCallback;
			if (processPingCallback != null && Interlocked.CompareExchange(ref this._pendingPingCallback, null, processPingCallback) == processPingCallback)
			{
				processPingCallback.Respond();
			}
		}

		// Token: 0x06005CF6 RID: 23798 RVA: 0x001417B8 File Offset: 0x0013F9B8
		internal int GetNonRandomizedStringComparerHashCode(string s, bool ignoreCase)
		{
			StringComparer stringComparer = ignoreCase ? StringComparer.InvariantCultureIgnoreCase : StringComparer.InvariantCulture;
			return stringComparer.GetHashCode(s);
		}

		// Token: 0x06005CF7 RID: 23799 RVA: 0x001417DC File Offset: 0x0013F9DC
		internal void HostingEnvironmentActivated()
		{
			int num = Interlocked.Increment(ref this._activeHostingEnvCount);
		}

		// Token: 0x06005CF8 RID: 23800 RVA: 0x001417F8 File Offset: 0x0013F9F8
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

		// Token: 0x06005CF9 RID: 23801 RVA: 0x00141838 File Offset: 0x0013FA38
		internal void HostingEnvironmentShutdownInitiated(string appId, HostingEnvironment env)
		{
			if (!this._shutdownInProgress)
			{
				LockableAppDomainContext lockableAppDomainContext = this.GetLockableAppDomainContext(appId);
				LockableAppDomainContext obj = lockableAppDomainContext;
				lock (obj)
				{
					if (!env.HasBeenRemovedFromAppManagerTable)
					{
						env.HasBeenRemovedFromAppManagerTable = true;
						lockableAppDomainContext.HostEnv = null;
						Interlocked.Decrement(ref this._accessibleHostingEnvCount);
						if (lockableAppDomainContext.PreloadContext != null && !lockableAppDomainContext.RetryingPreload)
						{
							ProcessHost.PreloadApplicationIfNotShuttingdown(appId, lockableAppDomainContext);
						}
					}
				}
			}
		}

		// Token: 0x17001B1C RID: 6940
		// (get) Token: 0x06005CFA RID: 23802 RVA: 0x001418B8 File Offset: 0x0013FAB8
		internal int AppDomainsCount
		{
			get
			{
				return this._accessibleHostingEnvCount;
			}
		}

		// Token: 0x06005CFB RID: 23803 RVA: 0x001418C0 File Offset: 0x0013FAC0
		internal void ReduceAppDomainsCount(int limit)
		{
			Dictionary<string, LockableAppDomainContext> dictionary = this.CloneAppDomainsCollection();
			while (this._accessibleHostingEnvCount >= limit && !this._shutdownInProgress)
			{
				LockableAppDomainContext lockableAppDomainContext = null;
				int num = 0;
				foreach (LockableAppDomainContext lockableAppDomainContext2 in dictionary.Values)
				{
					HostingEnvironment hostEnv = lockableAppDomainContext2.HostEnv;
					if (hostEnv != null)
					{
						LockableAppDomainContext obj = lockableAppDomainContext2;
						lock (obj)
						{
							hostEnv = lockableAppDomainContext2.HostEnv;
							if (hostEnv != null)
							{
								int lruScore = hostEnv.LruScore;
								if (lockableAppDomainContext == null || lockableAppDomainContext.HostEnv == null || lruScore < num)
								{
									num = lruScore;
									lockableAppDomainContext = lockableAppDomainContext2;
								}
							}
						}
					}
				}
				if (lockableAppDomainContext == null)
				{
					break;
				}
				LockableAppDomainContext obj2 = lockableAppDomainContext;
				lock (obj2)
				{
					if (lockableAppDomainContext.HostEnv != null)
					{
						lockableAppDomainContext.HostEnv.InitiateShutdownInternal();
					}
				}
			}
		}

		// Token: 0x06005CFC RID: 23804 RVA: 0x001419D8 File Offset: 0x0013FBD8
		internal ObjectHandle CreateInstanceInNewWorkerAppDomain(Type type, string appId, VirtualPath virtualPath, string physicalPath)
		{
			IApplicationHost appHost = new SimpleApplicationHost(virtualPath, physicalPath);
			HostingEnvironment hostingEnvironment = this.CreateAppDomainWithHostingEnvironmentAndReportErrors(appId, appHost, new HostingEnvironmentParameters
			{
				HostingFlags = HostingEnvironmentFlags.HideFromAppManager
			});
			return hostingEnvironment.CreateInstance(type.AssemblyQualifiedName);
		}

		// Token: 0x06005CFD RID: 23805 RVA: 0x00141A14 File Offset: 0x0013FC14
		private HostingEnvironment GetAppDomainWithHostingEnvironment(string appId, IApplicationHost appHost, HostingEnvironmentParameters hostingParameters)
		{
			LockableAppDomainContext lockableAppDomainContext = this.GetLockableAppDomainContext(appId);
			LockableAppDomainContext obj = lockableAppDomainContext;
			HostingEnvironment result;
			lock (obj)
			{
				HostingEnvironment hostingEnvironment = lockableAppDomainContext.HostEnv;
				if (hostingEnvironment != null)
				{
					try
					{
						hostingEnvironment.IsUnloaded();
					}
					catch (AppDomainUnloadedException)
					{
						hostingEnvironment = null;
					}
				}
				if (hostingEnvironment == null)
				{
					hostingEnvironment = this.CreateAppDomainWithHostingEnvironmentAndReportErrors(appId, appHost, hostingParameters);
					lockableAppDomainContext.HostEnv = hostingEnvironment;
					Interlocked.Increment(ref this._accessibleHostingEnvCount);
				}
				result = hostingEnvironment;
			}
			return result;
		}

		// Token: 0x06005CFE RID: 23806 RVA: 0x00141A9C File Offset: 0x0013FC9C
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

		// Token: 0x06005CFF RID: 23807 RVA: 0x00141AE8 File Offset: 0x0013FCE8
		private HostingEnvironment CreateAppDomainWithHostingEnvironment(string appId, IApplicationHost appHost, HostingEnvironmentParameters hostingParameters)
		{
			string text = appHost.GetPhysicalPath();
			if (!StringUtil.StringEndsWith(text, Path.DirectorySeparatorChar))
			{
				text += Path.DirectorySeparatorChar.ToString();
			}
			string text2 = ApplicationManager.ConstructAppDomainId(appId);
			string appName = StringUtil.GetStringHashCode(appId.ToLower(CultureInfo.InvariantCulture) + text.ToLower(CultureInfo.InvariantCulture)).ToString("x", CultureInfo.InvariantCulture);
			VirtualPath virtualPath = VirtualPath.Create(appHost.GetVirtualPath());
			IDictionary dictionary = new Hashtable(20);
			AppDomainSetup appDomainSetup = new AppDomainSetup();
			ApplicationManager.AppDomainSwitches appDomainSwitches = new ApplicationManager.AppDomainSwitches();
			ApplicationManager.PopulateDomainBindings(text2, appId, appName, text, virtualPath, appDomainSetup, dictionary);
			AppDomain appDomain = null;
			Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
			Exception innerException = null;
			string siteID = appHost.GetSiteID();
			string virtualPathStringNoTrailingSlash = virtualPath.VirtualPathStringNoTrailingSlash;
			bool flag = false;
			Configuration configuration = null;
			PolicyLevel policyLevel = null;
			PermissionSet permissionSet = null;
			List<StrongName> list = new List<StrongName>();
			string[] array = new string[]
			{
				"System.Web, PublicKey=002400000480000094000000060200000024000052534131000400000100010007d1fa57c4aed9f0a32e84aa0faefd0de9e8fd6aec8f87fb03766c834c99921eb23be79ad9d5dcc1dd9ad236132102900b723cf980957fc4e177108fc607774f29e8320e92ea05ece4e821c0a5efe8f1645c4c0c93c1ab99285d622caa652c1dfad63d745d6f2de5f17e5eaf0fc4963d261c8a12436518206dc093344d5ad293",
				"System.Web.Extensions, PublicKey=0024000004800000940000000602000000240000525341310004000001000100b5fc90e7027f67871e773a8fde8938c81dd402ba65b9201d60593e96c492651e889cc13f1415ebb53fac1131ae0bd333c5ee6021672d9718ea31a8aebd0da0072f25d87dba6fc90ffd598ed4da35e44c398c454307e8e33b8426143daec9f596836f97c8f74750e5975c64e2189f45def46b2a2b1247adc3652bf5c308055da9",
				"System.Web.Abstractions, PublicKey=0024000004800000940000000602000000240000525341310004000001000100b5fc90e7027f67871e773a8fde8938c81dd402ba65b9201d60593e96c492651e889cc13f1415ebb53fac1131ae0bd333c5ee6021672d9718ea31a8aebd0da0072f25d87dba6fc90ffd598ed4da35e44c398c454307e8e33b8426143daec9f596836f97c8f74750e5975c64e2189f45def46b2a2b1247adc3652bf5c308055da9",
				"System.Web.Routing, PublicKey=0024000004800000940000000602000000240000525341310004000001000100b5fc90e7027f67871e773a8fde8938c81dd402ba65b9201d60593e96c492651e889cc13f1415ebb53fac1131ae0bd333c5ee6021672d9718ea31a8aebd0da0072f25d87dba6fc90ffd598ed4da35e44c398c454307e8e33b8426143daec9f596836f97c8f74750e5975c64e2189f45def46b2a2b1247adc3652bf5c308055da9",
				"System.Web.DynamicData, PublicKey=0024000004800000940000000602000000240000525341310004000001000100b5fc90e7027f67871e773a8fde8938c81dd402ba65b9201d60593e96c492651e889cc13f1415ebb53fac1131ae0bd333c5ee6021672d9718ea31a8aebd0da0072f25d87dba6fc90ffd598ed4da35e44c398c454307e8e33b8426143daec9f596836f97c8f74750e5975c64e2189f45def46b2a2b1247adc3652bf5c308055da9",
				"System.Web.DataVisualization, PublicKey=0024000004800000940000000602000000240000525341310004000001000100b5fc90e7027f67871e773a8fde8938c81dd402ba65b9201d60593e96c492651e889cc13f1415ebb53fac1131ae0bd333c5ee6021672d9718ea31a8aebd0da0072f25d87dba6fc90ffd598ed4da35e44c398c454307e8e33b8426143daec9f596836f97c8f74750e5975c64e2189f45def46b2a2b1247adc3652bf5c308055da9",
				"System.Web.ApplicationServices, PublicKey=0024000004800000940000000602000000240000525341310004000001000100b5fc90e7027f67871e773a8fde8938c81dd402ba65b9201d60593e96c492651e889cc13f1415ebb53fac1131ae0bd333c5ee6021672d9718ea31a8aebd0da0072f25d87dba6fc90ffd598ed4da35e44c398c454307e8e33b8426143daec9f596836f97c8f74750e5975c64e2189f45def46b2a2b1247adc3652bf5c308055da9"
			};
			Exception ex = null;
			ImpersonationContext impersonationContext = null;
			IntPtr intPtr = IntPtr.Zero;
			if (hostingParameters != null)
			{
				HostingEnvironmentFlags hostingFlags = hostingParameters.HostingFlags;
				if ((hostingFlags & HostingEnvironmentFlags.ClientBuildManager) != HostingEnvironmentFlags.Default)
				{
					flag = true;
					appDomainSetup.LoaderOptimization = LoaderOptimization.MultiDomainHost;
				}
			}
			try
			{
				bool overrideHostExecutionContextManager = false;
				bool overrideHostSecurityManager = false;
				AppDomain.CurrentDomain.SetData("ConfigurationBuilders.IgnoreLoadFailure", true);
				intPtr = appHost.GetConfigToken();
				if (intPtr != IntPtr.Zero)
				{
					impersonationContext = new ImpersonationContext(intPtr);
				}
				try
				{
					ExceptionDispatchInfo existingCustomLoaderFailureAndClear = ProcessHost.GetExistingCustomLoaderFailureAndClear(appId);
					if (existingCustomLoaderFailureAndClear != null)
					{
						existingCustomLoaderFailureAndClear.Throw();
					}
					if (ApplicationManager.EnvironmentInfo.IsStringHashCodeRandomizationDetected)
					{
						throw new ConfigurationErrorsException(SR.GetString("Require_stable_string_hash_codes"));
					}
					bool flag2 = false;
					if (flag && hostingParameters.IISExpressVersion != null)
					{
						permissionSet = new PermissionSet(PermissionState.Unrestricted);
						appDomainSetup.PartialTrustVisibleAssemblies = array;
						configuration = ApplicationManager.GetAppConfigIISExpress(siteID, virtualPathStringNoTrailingSlash, hostingParameters.IISExpressVersion);
						flag2 = true;
					}
					else if (appHost is ISAPIApplicationHost)
					{
						string key = "f" + siteID + virtualPath.VirtualPathString;
						MapPathCacheInfo mapPathCacheInfo = (MapPathCacheInfo)HttpRuntime.Cache.InternalCache.Remove(key);
						configuration = WebConfigurationManager.OpenWebConfiguration(virtualPathStringNoTrailingSlash, siteID);
					}
					else
					{
						configuration = ApplicationManager.GetAppConfigGeneric(appHost, siteID, virtualPathStringNoTrailingSlash, virtualPath, text);
					}
					HttpRuntimeSection httpRuntimeSection = (HttpRuntimeSection)configuration.GetSection("system.web/httpRuntime");
					if (httpRuntimeSection == null)
					{
						throw new ConfigurationErrorsException(SR.GetString("Config_section_not_present", new object[]
						{
							"httpRuntime"
						}));
					}
					FrameworkName targetFrameworkName = httpRuntimeSection.GetTargetFrameworkName();
					if (targetFrameworkName != null)
					{
						dictionary2["ASPNET_TARGETFRAMEWORK"] = targetFrameworkName;
					}
					if (!flag2)
					{
						if (httpRuntimeSection.DefaultRegexMatchTimeout != TimeSpan.Zero)
						{
							dictionary2["REGEX_DEFAULT_MATCH_TIMEOUT"] = httpRuntimeSection.DefaultRegexMatchTimeout;
						}
						if (targetFrameworkName != null)
						{
							appDomainSetup.TargetFrameworkName = targetFrameworkName.ToString();
						}
						AppSettingsSection appSettings = configuration.AppSettings;
						KeyValueConfigurationElement keyValueConfigurationElement = appSettings.Settings["aspnet:UseTaskFriendlySynchronizationContext"];
						if (keyValueConfigurationElement == null || !bool.TryParse(keyValueConfigurationElement.Value, out overrideHostExecutionContextManager))
						{
							overrideHostExecutionContextManager = new BinaryCompatibility(targetFrameworkName).TargetsAtLeastFramework45;
						}
						KeyValueConfigurationElement keyValueConfigurationElement2 = appSettings.Settings["aspnet:UseRandomizedStringHashAlgorithm"];
						bool useRandomizedStringHashAlgorithm = false;
						if (keyValueConfigurationElement2 != null && bool.TryParse(keyValueConfigurationElement2.Value, out useRandomizedStringHashAlgorithm))
						{
							appDomainSwitches.UseRandomizedStringHashAlgorithm = useRandomizedStringHashAlgorithm;
						}
						Dictionary<string, bool> dictionary3 = null;
						foreach (object obj in appSettings.Settings)
						{
							KeyValueConfigurationElement keyValueConfigurationElement3 = (KeyValueConfigurationElement)obj;
							bool value;
							if (keyValueConfigurationElement3.Key != null && keyValueConfigurationElement3.Key.Length > "AppContext.SetSwitch:".Length && keyValueConfigurationElement3.Key.StartsWith("AppContext.SetSwitch:", StringComparison.OrdinalIgnoreCase) && bool.TryParse(keyValueConfigurationElement3.Value, out value))
							{
								if (dictionary3 == null)
								{
									dictionary3 = new Dictionary<string, bool>();
								}
								dictionary3[keyValueConfigurationElement3.Key.Substring("AppContext.SetSwitch:".Length)] = value;
							}
						}
						if (dictionary3 != null && dictionary3.Count > 0)
						{
							if (hostingParameters == null)
							{
								hostingParameters = new HostingEnvironmentParameters();
							}
							hostingParameters.ClrQuirksSwitches = dictionary3.ToArray<KeyValuePair<string, bool>>();
						}
						if (httpRuntimeSection.FcnMode != FcnMode.NotSet)
						{
							if (hostingParameters == null)
							{
								hostingParameters = new HostingEnvironmentParameters();
							}
							hostingParameters.FcnMode = httpRuntimeSection.FcnMode;
						}
						KeyValueConfigurationElement keyValueConfigurationElement4 = appSettings.Settings["aspnet:DisableFcnDaclRead"];
						if (keyValueConfigurationElement4 != null)
						{
							bool flag3;
							bool.TryParse(keyValueConfigurationElement4.Value, out flag3);
							if (flag3)
							{
								if (hostingParameters == null)
								{
									hostingParameters = new HostingEnvironmentParameters();
								}
								hostingParameters.FcnSkipReadAndCacheDacls = true;
							}
						}
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
							dictionary2[".defaultObjectCacheProvider"] = parameters;
						}
						DeploymentSection deploymentSection = (DeploymentSection)configuration.GetSection("system.web/deployment");
						bool isDevEnvironment = false;
						if (deploymentSection != null && !deploymentSection.Retail && ApplicationManager.EnvironmentInfo.WasLaunchedFromDevelopmentEnvironment)
						{
							dictionary2[".devEnvironment"] = true;
							isDevEnvironment = true;
							dictionary2["ALLOW_LOCALDB_IN_PARTIAL_TRUST"] = true;
						}
						TrustSection trustSection = (TrustSection)configuration.GetSection("system.web/trust");
						if (trustSection == null || string.IsNullOrEmpty(trustSection.Level))
						{
							throw new ConfigurationErrorsException(SR.GetString("Config_section_not_present", new object[]
							{
								"trust"
							}));
						}
						appDomainSwitches.UseLegacyCas = trustSection.LegacyCasModel;
						if (flag)
						{
							permissionSet = new PermissionSet(PermissionState.Unrestricted);
							appDomainSetup.PartialTrustVisibleAssemblies = array;
						}
						else
						{
							if (!appDomainSwitches.UseLegacyCas)
							{
								if (trustSection.Level == "Full")
								{
									permissionSet = new PermissionSet(PermissionState.Unrestricted);
									appDomainSetup.PartialTrustVisibleAssemblies = array;
								}
								else
								{
									SecurityPolicySection securityPolicySection = (SecurityPolicySection)configuration.GetSection("system.web/securityPolicy");
									CompilationSection compilationSection = (CompilationSection)configuration.GetSection("system.web/compilation");
									FullTrustAssembliesSection fullTrustAssembliesSection = (FullTrustAssembliesSection)configuration.GetSection("system.web/fullTrustAssemblies");
									policyLevel = ApplicationManager.GetPartialTrustPolicyLevel(trustSection, securityPolicySection, compilationSection, text, virtualPath, isDevEnvironment);
									permissionSet = policyLevel.GetNamedPermissionSet(trustSection.PermissionSetName);
									if (permissionSet == null)
									{
										throw new ConfigurationErrorsException(SR.GetString("Permission_set_not_found", new object[]
										{
											trustSection.PermissionSetName
										}));
									}
									if (fullTrustAssembliesSection != null)
									{
										FullTrustAssemblyCollection fullTrustAssemblies = fullTrustAssembliesSection.FullTrustAssemblies;
										if (fullTrustAssemblies != null)
										{
											list.AddRange(from FullTrustAssembly fta in fullTrustAssemblies
											select ApplicationManager.CreateStrongName(fta.AssemblyName, fta.Version, fta.PublicKey));
										}
									}
									if (list.Contains(ApplicationManager._mwiV1StrongName))
									{
										list.AddRange(ApplicationManager.CreateFutureMicrosoftWebInfrastructureStrongNames());
									}
									overrideHostSecurityManager = true;
								}
							}
							if (trustSection.Level != "Full")
							{
								PartialTrustVisibleAssembliesSection partialTrustVisibleAssembliesSection = (PartialTrustVisibleAssembliesSection)configuration.GetSection("system.web/partialTrustVisibleAssemblies");
								string[] array2 = null;
								if (partialTrustVisibleAssembliesSection != null)
								{
									PartialTrustVisibleAssemblyCollection partialTrustVisibleAssemblies = partialTrustVisibleAssembliesSection.PartialTrustVisibleAssemblies;
									if (partialTrustVisibleAssemblies != null && partialTrustVisibleAssemblies.Count != 0)
									{
										array2 = new string[partialTrustVisibleAssemblies.Count + array.Length];
										for (int i = 0; i < partialTrustVisibleAssemblies.Count; i++)
										{
											array2[i] = partialTrustVisibleAssemblies[i].AssemblyName + ", PublicKey=" + ApplicationManager.NormalizePublicKeyBlob(partialTrustVisibleAssemblies[i].PublicKey);
										}
										array.CopyTo(array2, partialTrustVisibleAssemblies.Count);
									}
								}
								if (array2 == null)
								{
									array2 = array;
								}
								appDomainSetup.PartialTrustVisibleAssemblies = array2;
							}
						}
					}
				}
				catch (Exception ex2)
				{
					ex = ex2;
					permissionSet = new PermissionSet(PermissionState.Unrestricted);
				}
				Type aspNetAppDomainManagerType = ApplicationManager.AspNetAppDomainManager.GetAspNetAppDomainManagerType(overrideHostExecutionContextManager, overrideHostSecurityManager);
				if (aspNetAppDomainManagerType != null)
				{
					appDomainSetup.AppDomainManagerType = aspNetAppDomainManagerType.FullName;
					appDomainSetup.AppDomainManagerAssembly = aspNetAppDomainManagerType.Assembly.FullName;
				}
				appDomainSwitches.Apply(appDomainSetup);
				try
				{
					if (appDomainSwitches.UseLegacyCas)
					{
						appDomain = AppDomain.CreateDomain(text2, ApplicationManager.GetDefaultDomainIdentity(), appDomainSetup);
					}
					else
					{
						appDomain = AppDomain.CreateDomain(text2, ApplicationManager.GetDefaultDomainIdentity(), appDomainSetup, permissionSet, list.ToArray());
					}
					foreach (object obj2 in dictionary)
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)obj2;
						appDomain.SetData((string)dictionaryEntry.Key, (string)dictionaryEntry.Value);
					}
					foreach (KeyValuePair<string, object> keyValuePair in dictionary2)
					{
						appDomain.SetData(keyValuePair.Key, keyValuePair.Value);
					}
				}
				catch (Exception ex3)
				{
					innerException = ex3;
				}
			}
			finally
			{
				if (impersonationContext != null)
				{
					impersonationContext.Undo();
					impersonationContext = null;
				}
				if (intPtr != IntPtr.Zero)
				{
					UnsafeNativeMethods.CloseHandle(intPtr);
					intPtr = IntPtr.Zero;
				}
			}
			if (appDomain == null)
			{
				throw new SystemException(SR.GetString("Cannot_create_AppDomain"), innerException);
			}
			Type typeFromHandle = typeof(HostingEnvironment);
			string fullName = typeFromHandle.Module.Assembly.FullName;
			string fullName2 = typeFromHandle.FullName;
			ObjectHandle objectHandle = null;
			ImpersonationContext impersonationContext2 = null;
			IntPtr intPtr2 = IntPtr.Zero;
			int num = 10;
			int j = 0;
			while (j < num)
			{
				try
				{
					intPtr2 = appHost.GetConfigToken();
					break;
				}
				catch (InvalidOperationException)
				{
					j++;
					Thread.Sleep(250);
				}
			}
			if (intPtr2 != IntPtr.Zero)
			{
				try
				{
					impersonationContext2 = new ImpersonationContext(intPtr2);
				}
				catch
				{
				}
				finally
				{
					UnsafeNativeMethods.CloseHandle(intPtr2);
				}
			}
			try
			{
				objectHandle = Activator.CreateInstance(appDomain, fullName, fullName2);
			}
			finally
			{
				if (impersonationContext2 != null)
				{
					impersonationContext2.Undo();
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
			IConfigMapPathFactory configMapPathFactory = appHost.GetConfigMapPathFactory();
			if (ex == null)
			{
				hostingEnvironment.Initialize(this, appHost, configMapPathFactory, hostingParameters, policyLevel);
			}
			else
			{
				hostingEnvironment.Initialize(this, appHost, configMapPathFactory, hostingParameters, policyLevel, ex);
			}
			return hostingEnvironment;
		}

		// Token: 0x06005D00 RID: 23808 RVA: 0x0014260C File Offset: 0x0014080C
		private static string NormalizePublicKeyBlob(string publicKey)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < publicKey.Length; i++)
			{
				if (!char.IsWhiteSpace(publicKey[i]))
				{
					stringBuilder.Append(publicKey[i]);
				}
			}
			publicKey = stringBuilder.ToString();
			return publicKey;
		}

		// Token: 0x06005D01 RID: 23809 RVA: 0x00142658 File Offset: 0x00140858
		private static StrongName CreateStrongName(string assemblyName, string version, string publicKeyString)
		{
			publicKeyString = ApplicationManager.NormalizePublicKeyBlob(publicKeyString);
			int num = publicKeyString.Length / 2;
			byte[] array = new byte[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = byte.Parse(publicKeyString.Substring(i * 2, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
			}
			StrongNamePublicKeyBlob blob = new StrongNamePublicKeyBlob(array);
			return new StrongName(blob, assemblyName, new Version(version));
		}

		// Token: 0x06005D02 RID: 23810 RVA: 0x001426C5 File Offset: 0x001408C5
		private static StrongName GetMicrosoftWebInfrastructureV1StrongName()
		{
			return ApplicationManager.CreateStrongName("Microsoft.Web.Infrastructure", "1.0.0.0", "0024000004800000940000000602000000240000525341310004000001000100B5FC90E7027F67871E773A8FDE8938C81DD402BA65B9201D60593E96C492651E889CC13F1415EBB53FAC1131AE0BD333C5EE6021672D9718EA31A8AEBD0DA0072F25D87DBA6FC90FFD598ED4DA35E44C398C454307E8E33B8426143DAEC9F596836F97C8F74750E5975C64E2189F45DEF46B2A2B1247ADC3652BF5C308055DA9");
		}

		// Token: 0x06005D03 RID: 23811 RVA: 0x001426DB File Offset: 0x001408DB
		private static IEnumerable<StrongName> CreateFutureMicrosoftWebInfrastructureStrongNames()
		{
			string asmName = ApplicationManager._mwiV1StrongName.Name;
			StrongNamePublicKeyBlob publicKey = ApplicationManager._mwiV1StrongName.PublicKey;
			int num;
			for (int i = 2; i <= 10; i = num + 1)
			{
				yield return new StrongName(publicKey, asmName, new Version(i, 0, 0, 0));
				num = i;
			}
			yield break;
		}

		// Token: 0x06005D04 RID: 23812 RVA: 0x001426E4 File Offset: 0x001408E4
		private static PolicyLevel GetPartialTrustPolicyLevel(TrustSection trustSection, SecurityPolicySection securityPolicySection, CompilationSection compilationSection, string physicalPath, VirtualPath virtualPath, bool isDevEnvironment)
		{
			if (securityPolicySection == null || securityPolicySection.TrustLevels[trustSection.Level] == null)
			{
				throw new ConfigurationErrorsException(SR.GetString("Unable_to_get_policy_file", new object[]
				{
					trustSection.Level
				}), string.Empty, 0);
			}
			string policyFileExpanded = securityPolicySection.TrustLevels[trustSection.Level].PolicyFileExpanded;
			if (policyFileExpanded == null || !FileUtil.FileExists(policyFileExpanded))
			{
				throw new HttpException(SR.GetString("Unable_to_get_policy_file", new object[]
				{
					trustSection.Level
				}));
			}
			PolicyLevel policyLevel = null;
			string text = FileUtil.RemoveTrailingDirectoryBackSlash(physicalPath);
			string newValue = HttpRuntime.MakeFileUrl(text);
			string text2 = null;
			string text3 = null;
			string filename = null;
			int line = 0;
			if (compilationSection != null && !string.IsNullOrEmpty(compilationSection.TempDirectory))
			{
				text2 = compilationSection.TempDirectory;
				compilationSection.GetTempDirectoryErrorInfo(out text3, out filename, out line);
			}
			if (text2 != null)
			{
				text2 = text2.Trim();
				if (!Path.IsPathRooted(text2))
				{
					text2 = null;
				}
				else
				{
					try
					{
						text2 = new DirectoryInfo(text2).FullName;
					}
					catch
					{
						text2 = null;
					}
				}
				if (text2 == null)
				{
					throw new ConfigurationErrorsException(SR.GetString("Invalid_temp_directory", new object[]
					{
						text3
					}), filename, line);
				}
				try
				{
					Directory.CreateDirectory(text2);
					goto IL_150;
				}
				catch (Exception inner)
				{
					throw new ConfigurationErrorsException(SR.GetString("Invalid_temp_directory", new object[]
					{
						text3
					}), inner, filename, line);
				}
			}
			text2 = Path.Combine(RuntimeEnvironment.GetRuntimeDirectory(), "Temporary ASP.NET Files");
			IL_150:
			if (!Util.HasWriteAccessToDirectory(text2))
			{
				if (!Environment.UserInteractive)
				{
					throw new HttpException(SR.GetString("No_codegen_access", new object[]
					{
						Util.GetCurrentAccountName(),
						text2
					}));
				}
				text2 = Path.GetTempPath();
				text2 = Path.Combine(text2, "Temporary ASP.NET Files");
			}
			string path = AppManagerAppDomainFactory.ConstructSimpleAppName(VirtualPath.GetVirtualPathStringNoTrailingSlash(virtualPath), isDevEnvironment);
			string path2 = Path.Combine(text2, path);
			path2 = FileUtil.RemoveTrailingDirectoryBackSlash(path2);
			string newValue2 = HttpRuntime.MakeFileUrl(path2);
			string text4 = trustSection.OriginUrl;
			FileStream stream = new FileStream(policyFileExpanded, FileMode.Open, FileAccess.Read);
			StreamReader streamReader = new StreamReader(stream, Encoding.UTF8);
			string text5 = streamReader.ReadToEnd();
			streamReader.Close();
			text5 = text5.Replace("$AppDir$", text);
			text5 = text5.Replace("$AppDirUrl$", newValue);
			text5 = text5.Replace("$CodeGen$", newValue2);
			if (text4 == null)
			{
				text4 = string.Empty;
			}
			text5 = text5.Replace("$OriginHost$", text4);
			string text6 = null;
			if (text5.IndexOf("$Gac$", StringComparison.Ordinal) != -1)
			{
				text6 = HttpRuntime.GetGacLocation();
				if (text6 != null)
				{
					text6 = HttpRuntime.MakeFileUrl(text6);
				}
				if (text6 == null)
				{
					text6 = string.Empty;
				}
				text5 = text5.Replace("$Gac$", text6);
			}
			policyLevel = SecurityManager.LoadPolicyLevelFromString(text5, PolicyLevelType.AppDomain);
			if (policyLevel == null)
			{
				throw new ConfigurationErrorsException(SR.GetString("Unable_to_get_policy_file", new object[]
				{
					trustSection.Level
				}));
			}
			if (text6 != null)
			{
				CodeGroup rootCodeGroup = policyLevel.RootCodeGroup;
				bool flag = false;
				foreach (object obj in rootCodeGroup.Children)
				{
					CodeGroup codeGroup = (CodeGroup)obj;
					if (codeGroup.MembershipCondition is GacMembershipCondition)
					{
						flag = true;
						break;
					}
				}
				if (!flag && rootCodeGroup is FirstMatchCodeGroup)
				{
					FirstMatchCodeGroup firstMatchCodeGroup = (FirstMatchCodeGroup)rootCodeGroup;
					if (firstMatchCodeGroup.MembershipCondition is AllMembershipCondition && firstMatchCodeGroup.PermissionSetName == "Nothing")
					{
						PermissionSet permSet = new PermissionSet(PermissionState.Unrestricted);
						CodeGroup codeGroup2 = new UnionCodeGroup(new GacMembershipCondition(), new PolicyStatement(permSet));
						CodeGroup codeGroup3 = new FirstMatchCodeGroup(rootCodeGroup.MembershipCondition, rootCodeGroup.PolicyStatement);
						foreach (object obj2 in rootCodeGroup.Children)
						{
							CodeGroup codeGroup4 = (CodeGroup)obj2;
							if (codeGroup4 is UnionCodeGroup && codeGroup4.MembershipCondition is UrlMembershipCondition && codeGroup4.PolicyStatement.PermissionSet.IsUnrestricted() && codeGroup2 != null)
							{
								codeGroup3.AddChild(codeGroup2);
								codeGroup2 = null;
							}
							codeGroup3.AddChild(codeGroup4);
						}
						policyLevel.RootCodeGroup = codeGroup3;
					}
				}
			}
			return policyLevel;
		}

		// Token: 0x06005D05 RID: 23813 RVA: 0x00142B30 File Offset: 0x00140D30
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

		// Token: 0x06005D06 RID: 23814 RVA: 0x00142BC4 File Offset: 0x00140DC4
		private static Evidence GetDefaultDomainIdentity()
		{
			Evidence evidence = AppDomain.CurrentDomain.Evidence;
			bool flag = evidence.GetHostEvidence<Zone>() != null;
			bool flag2 = evidence.GetHostEvidence<Url>() != null;
			if (!flag)
			{
				evidence.AddHostEvidence<Zone>(new Zone(SecurityZone.MyComputer));
			}
			if (!flag2)
			{
				evidence.AddHostEvidence<Url>(new Url("ms-internal-microsoft-asp-net-webhost-20"));
			}
			return evidence;
		}

		// Token: 0x06005D07 RID: 23815 RVA: 0x00142C14 File Offset: 0x00140E14
		private static string ConstructAppDomainId(string id)
		{
			int num = 0;
			object obj = ApplicationManager.s_domainCountLock;
			lock (obj)
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

		// Token: 0x06005D08 RID: 23816 RVA: 0x00142CA4 File Offset: 0x00140EA4
		internal LockableAppDomainContext GetLockableAppDomainContext(string appId)
		{
			LockableAppDomainContext result;
			lock (this)
			{
				LockableAppDomainContext lockableAppDomainContext;
				if (!this._appDomains.TryGetValue(appId, out lockableAppDomainContext))
				{
					lockableAppDomainContext = new LockableAppDomainContext();
					this._appDomains.Add(appId, lockableAppDomainContext);
				}
				result = lockableAppDomainContext;
			}
			return result;
		}

		// Token: 0x06005D09 RID: 23817 RVA: 0x00142D00 File Offset: 0x00140F00
		private Dictionary<string, LockableAppDomainContext> CloneAppDomainsCollection()
		{
			Dictionary<string, LockableAppDomainContext> result;
			lock (this)
			{
				result = new Dictionary<string, LockableAppDomainContext>(this._appDomains, StringComparer.OrdinalIgnoreCase);
			}
			return result;
		}

		// Token: 0x06005D0A RID: 23818 RVA: 0x00142D48 File Offset: 0x00140F48
		private static Configuration GetAppConfigCommon(IConfigMapPath configMapPath, string siteID, string appSegment)
		{
			WebConfigurationFileMap webConfigurationFileMap = new WebConfigurationFileMap();
			string text = null;
			string text2 = null;
			string text3 = "/";
			configMapPath.GetPathConfigFilename(siteID, text3, out text, out text2);
			if (text != null)
			{
				webConfigurationFileMap.VirtualDirectories.Add(text3, new VirtualDirectoryMapping(Path.GetFullPath(text), true));
			}
			string[] array = appSegment.Split(new char[]
			{
				'/'
			}, StringSplitOptions.RemoveEmptyEntries);
			foreach (string str in array)
			{
				text3 += str;
				configMapPath.GetPathConfigFilename(siteID, text3, out text, out text2);
				if (text != null)
				{
					webConfigurationFileMap.VirtualDirectories.Add(text3, new VirtualDirectoryMapping(Path.GetFullPath(text), true));
				}
				text3 += "/";
			}
			return WebConfigurationManager.OpenMappedWebConfiguration(webConfigurationFileMap, appSegment, siteID);
		}

		// Token: 0x06005D0B RID: 23819 RVA: 0x00142E04 File Offset: 0x00141004
		private static Configuration GetAppConfigGeneric(IApplicationHost appHost, string siteID, string appSegment, VirtualPath virtualPath, string physicalPath)
		{
			WebConfigurationFileMap webConfigurationFileMap = new WebConfigurationFileMap();
			IConfigMapPathFactory configMapPathFactory = appHost.GetConfigMapPathFactory();
			IConfigMapPath configMapPath = configMapPathFactory.Create(virtualPath.VirtualPathString, physicalPath);
			return ApplicationManager.GetAppConfigCommon(configMapPath, siteID, appSegment);
		}

		// Token: 0x06005D0C RID: 23820 RVA: 0x00142E38 File Offset: 0x00141038
		private static Configuration GetAppConfigIISExpress(string siteID, string appSegment, string iisExpressVersion)
		{
			ExpressServerConfig configMapPath = (ExpressServerConfig)ServerConfig.GetDefaultDomainInstance(iisExpressVersion);
			return ApplicationManager.GetAppConfigCommon(configMapPath, siteID, appSegment);
		}

		// Token: 0x040030DF RID: 12511
		private const string _clrQuirkAppSettingsAppContextPrefix = "AppContext.SetSwitch:";

		// Token: 0x040030E0 RID: 12512
		private const string _regexMatchTimeoutKey = "REGEX_DEFAULT_MATCH_TIMEOUT";

		// Token: 0x040030E1 RID: 12513
		private const string _configBuildersIgnoreLoadFailuresSwitch = "ConfigurationBuilders.IgnoreLoadFailure";

		// Token: 0x040030E2 RID: 12514
		private static readonly StrongName _mwiV1StrongName = ApplicationManager.GetMicrosoftWebInfrastructureV1StrongName();

		// Token: 0x040030E3 RID: 12515
		private static object _applicationManagerStaticLock = new object();

		// Token: 0x040030E4 RID: 12516
		private int _openCount;

		// Token: 0x040030E5 RID: 12517
		private bool _shutdownInProgress;

		// Token: 0x040030E6 RID: 12518
		private Dictionary<string, LockableAppDomainContext> _appDomains = new Dictionary<string, LockableAppDomainContext>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x040030E7 RID: 12519
		private int _accessibleHostingEnvCount;

		// Token: 0x040030E8 RID: 12520
		private int _activeHostingEnvCount;

		// Token: 0x040030E9 RID: 12521
		private object _pendingPingCallback;

		// Token: 0x040030EA RID: 12522
		private WaitCallback _onRespondToPingWaitCallback;

		// Token: 0x040030EB RID: 12523
		private bool _fatalExceptionRecorded;

		// Token: 0x040030EC RID: 12524
		private static ApplicationManager _theAppManager;

		// Token: 0x040030ED RID: 12525
		private static Exception _fatalException = null;

		// Token: 0x040030EE RID: 12526
		private static Task<ApplicationManager.ApplicationResumeStateContainer> _dummyCompletedSuspendTask = Task.FromResult<ApplicationManager.ApplicationResumeStateContainer>(null);

		// Token: 0x040030EF RID: 12527
		private static int s_domainCount = 0;

		// Token: 0x040030F0 RID: 12528
		private static object s_domainCountLock = new object();

		// Token: 0x02000A57 RID: 2647
		private sealed class ApplicationResumeStateContainer
		{
			// Token: 0x06006ED9 RID: 28377 RVA: 0x0018B0C5 File Offset: 0x001892C5
			internal ApplicationResumeStateContainer(HostingEnvironment hostEnv, IntPtr resumeState)
			{
				this._hostEnv = hostEnv;
				this._resumeState = resumeState;
			}

			// Token: 0x06006EDA RID: 28378 RVA: 0x0018B0DB File Offset: 0x001892DB
			internal void Resume()
			{
				ThreadPool.UnsafeQueueUserWorkItem(ApplicationManager.ApplicationResumeStateContainer._tpCallback, this);
			}

			// Token: 0x06006EDB RID: 28379 RVA: 0x0018B0EC File Offset: 0x001892EC
			private static void ResumeCallback(object state)
			{
				ApplicationManager.ApplicationResumeStateContainer applicationResumeStateContainer = (ApplicationManager.ApplicationResumeStateContainer)state;
				try
				{
					applicationResumeStateContainer._hostEnv.ResumeApplication(applicationResumeStateContainer._resumeState);
				}
				catch (AppDomainUnloadedException)
				{
				}
			}

			// Token: 0x04003B73 RID: 15219
			private static readonly WaitCallback _tpCallback = new WaitCallback(ApplicationManager.ApplicationResumeStateContainer.ResumeCallback);

			// Token: 0x04003B74 RID: 15220
			private readonly HostingEnvironment _hostEnv;

			// Token: 0x04003B75 RID: 15221
			private readonly IntPtr _resumeState;
		}

		// Token: 0x02000A58 RID: 2648
		[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		private static class AspNetAppDomainManager
		{
			// Token: 0x06006EDD RID: 28381 RVA: 0x0018B13C File Offset: 0x0018933C
			internal static Type GetAspNetAppDomainManagerType(bool overrideHostExecutionContextManager, bool overrideHostSecurityManager)
			{
				if (!overrideHostExecutionContextManager && !overrideHostSecurityManager)
				{
					return null;
				}
				Type typeFromHandle = typeof(ApplicationManager.AspNetAppDomainManager.AspNetAppDomainManagerImpl<, >);
				return typeFromHandle.MakeGenericType(new Type[]
				{
					overrideHostExecutionContextManager ? typeof(AspNetHostExecutionContextManager) : typeof(object),
					overrideHostSecurityManager ? typeof(ApplicationManager.AspNetAppDomainManager.AspNetHostSecurityManager) : typeof(object)
				});
			}

			// Token: 0x02000A9A RID: 2714
			private sealed class AspNetAppDomainManagerImpl<THostExecutionContextManager, THostSecurityManager> : AppDomainManager where THostExecutionContextManager : class, new() where THostSecurityManager : class, new()
			{
				// Token: 0x17001E55 RID: 7765
				// (get) Token: 0x06006F7E RID: 28542 RVA: 0x0018CF47 File Offset: 0x0018B147
				public override HostExecutionContextManager HostExecutionContextManager
				{
					get
					{
						return this._hostExecutionContextManager ?? base.HostExecutionContextManager;
					}
				}

				// Token: 0x17001E56 RID: 7766
				// (get) Token: 0x06006F7F RID: 28543 RVA: 0x0018CF59 File Offset: 0x0018B159
				public override HostSecurityManager HostSecurityManager
				{
					get
					{
						return this._hostSecurityManager ?? base.HostSecurityManager;
					}
				}

				// Token: 0x06006F80 RID: 28544 RVA: 0x0018CF6C File Offset: 0x0018B16C
				private static HostExecutionContextManager CreateHostExecutionContextManager()
				{
					object obj = Activator.CreateInstance<THostExecutionContextManager>();
					return obj as HostExecutionContextManager;
				}

				// Token: 0x06006F81 RID: 28545 RVA: 0x0018CF8C File Offset: 0x0018B18C
				private static HostSecurityManager CreateHostSecurityManager()
				{
					object obj = Activator.CreateInstance<THostSecurityManager>();
					return obj as HostSecurityManager;
				}

				// Token: 0x04003C13 RID: 15379
				private readonly HostExecutionContextManager _hostExecutionContextManager = ApplicationManager.AspNetAppDomainManager.AspNetAppDomainManagerImpl<THostExecutionContextManager, THostSecurityManager>.CreateHostExecutionContextManager();

				// Token: 0x04003C14 RID: 15380
				private readonly HostSecurityManager _hostSecurityManager = ApplicationManager.AspNetAppDomainManager.AspNetAppDomainManagerImpl<THostExecutionContextManager, THostSecurityManager>.CreateHostSecurityManager();
			}

			// Token: 0x02000A9B RID: 2715
			private sealed class AspNetHostSecurityManager : HostSecurityManager
			{
				// Token: 0x17001E57 RID: 7767
				// (get) Token: 0x06006F83 RID: 28547 RVA: 0x0018CFC8 File Offset: 0x0018B1C8
				public override HostSecurityManagerOptions Flags
				{
					get
					{
						return HostSecurityManagerOptions.HostResolvePolicy;
					}
				}

				// Token: 0x06006F84 RID: 28548 RVA: 0x0018CFCC File Offset: 0x0018B1CC
				[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.SerializationFormatter)]
				public override PermissionSet ResolvePolicy(Evidence evidence)
				{
					if (base.ResolvePolicy(evidence).IsUnrestricted())
					{
						return this.FullTrust;
					}
					if (!string.IsNullOrEmpty(HttpRuntime.HostSecurityPolicyResolverType) && this.hostSecurityPolicyResolver == null)
					{
						this.hostSecurityPolicyResolver = (Activator.CreateInstance(Type.GetType(HttpRuntime.HostSecurityPolicyResolverType)) as HostSecurityPolicyResolver);
					}
					if (this.hostSecurityPolicyResolver != null)
					{
						switch (this.hostSecurityPolicyResolver.ResolvePolicy(evidence))
						{
						case HostSecurityPolicyResults.FullTrust:
							return this.FullTrust;
						case HostSecurityPolicyResults.AppDomainTrust:
							return HttpRuntime.NamedPermissionSet;
						case HostSecurityPolicyResults.Nothing:
							return this.Nothing;
						}
					}
					if (HttpRuntime.PolicyLevel == null || HttpRuntime.PolicyLevel.Resolve(evidence).PermissionSet.IsUnrestricted())
					{
						return this.FullTrust;
					}
					if (HttpRuntime.PolicyLevel.Resolve(evidence).PermissionSet.Equals(this.Nothing))
					{
						return this.Nothing;
					}
					return HttpRuntime.NamedPermissionSet;
				}

				// Token: 0x04003C15 RID: 15381
				private PermissionSet Nothing = new PermissionSet(PermissionState.None);

				// Token: 0x04003C16 RID: 15382
				private PermissionSet FullTrust = new PermissionSet(PermissionState.Unrestricted);

				// Token: 0x04003C17 RID: 15383
				private HostSecurityPolicyResolver hostSecurityPolicyResolver;
			}
		}

		// Token: 0x02000A59 RID: 2649
		private sealed class AppDomainSwitches
		{
			// Token: 0x06006EDE RID: 28382 RVA: 0x0018B1A4 File Offset: 0x001893A4
			public void Apply(AppDomainSetup setup)
			{
				List<string> list = new List<string>();
				if (this.UseLegacyCas)
				{
					list.Add("NetFx40_LegacySecurityPolicy");
				}
				if (this.UseRandomizedStringHashAlgorithm)
				{
					list.Add("UseRandomizedStringHashAlgorithm");
				}
				if (list.Count > 0)
				{
					setup.SetCompatibilitySwitches(list);
				}
			}

			// Token: 0x04003B76 RID: 15222
			public bool UseLegacyCas;

			// Token: 0x04003B77 RID: 15223
			public bool UseRandomizedStringHashAlgorithm;
		}

		// Token: 0x02000A5A RID: 2650
		private static class EnvironmentInfo
		{
			// Token: 0x06006EE0 RID: 28384 RVA: 0x0018B1ED File Offset: 0x001893ED
			private static bool GetIsStringHashCodeRandomizationDetected()
			{
				return StringComparer.InvariantCultureIgnoreCase.GetHashCode("The quick brown fox jumps over the lazy dog.") != 1883137582;
			}

			// Token: 0x06006EE1 RID: 28385 RVA: 0x0018B208 File Offset: 0x00189408
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

			// Token: 0x04003B78 RID: 15224
			public static readonly bool IsStringHashCodeRandomizationDetected = ApplicationManager.EnvironmentInfo.GetIsStringHashCodeRandomizationDetected();

			// Token: 0x04003B79 RID: 15225
			public static readonly bool WasLaunchedFromDevelopmentEnvironment = ApplicationManager.EnvironmentInfo.GetWasLaunchedFromDevelopmentEnvironmentValue();
		}
	}
}
