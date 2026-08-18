using System;
using System.Collections;
using System.Configuration;
using System.Data.Common;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Security.Policy;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web.Caching;
using System.Web.Compilation;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Management;
using System.Web.UI;
using System.Web.Util;
using System.Xml;

namespace System.Web
{
	// Token: 0x020000B5 RID: 181
	public sealed class HttpRuntime
	{
		// Token: 0x06000BF9 RID: 3065 RVA: 0x0001F7BC File Offset: 0x0001D9BC
		static HttpRuntime()
		{
			HttpRuntime.AddAppDomainTraceMessage("*HttpRuntime::cctor");
			HttpRuntime.StaticInit();
			HttpRuntime._theRuntime = new HttpRuntime();
			HttpRuntime._theRuntime.Init();
			HttpRuntime.AddAppDomainTraceMessage("HttpRuntime::cctor*");
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x0001F851 File Offset: 0x0001DA51
		[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		public HttpRuntime()
		{
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x00006164 File Offset: 0x00004364
		internal static void ForceStaticInit()
		{
		}

		// Token: 0x06000BFC RID: 3068 RVA: 0x0001F860 File Offset: 0x0001DA60
		private static void StaticInit()
		{
			if (HttpRuntime.s_initialized)
			{
				return;
			}
			bool flag = false;
			bool flag2 = false;
			string runtimeDirectory = RuntimeEnvironment.GetRuntimeDirectory();
			if (UnsafeNativeMethods.GetModuleHandle("webengine4.dll") != IntPtr.Zero)
			{
				flag = true;
			}
			if (!flag)
			{
				string libFilename = runtimeDirectory + Path.DirectorySeparatorChar.ToString() + "webengine4.dll";
				if (UnsafeNativeMethods.LoadLibrary(libFilename) != IntPtr.Zero)
				{
					flag = true;
					flag2 = true;
				}
			}
			if (flag)
			{
				UnsafeNativeMethods.InitializeLibrary(false);
				if (flag2)
				{
					UnsafeNativeMethods.PerfCounterInitialize();
				}
			}
			HttpRuntime.s_installDirectory = runtimeDirectory;
			HttpRuntime.s_isEngineLoaded = flag;
			HttpRuntime.s_initialized = true;
			HttpRuntime.PopulateIISVersionInformation();
			HttpRuntime.AddAppDomainTraceMessage("Initialize");
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x0001F900 File Offset: 0x0001DB00
		private void Init()
		{
			try
			{
				if (Environment.OSVersion.Platform != PlatformID.Win32NT)
				{
					throw new PlatformNotSupportedException(SR.GetString("RequiresNT"));
				}
				this._profiler = new Profiler();
				this._timeoutManager = new RequestTimeoutManager();
				this._wpUserId = HttpRuntime.GetCurrentUserName();
				this._requestNotificationCompletionCallback = new AsyncCallback(this.OnRequestNotificationCompletion);
				this._handlerCompletionCallback = new AsyncCallback(this.OnHandlerCompletion);
				this._asyncEndOfSendCallback = new HttpWorkerRequest.EndOfSendNotification(this.EndOfSendCallback);
				this._appDomainUnloadallback = new WaitCallback(this.ReleaseResourcesAndUnloadAppDomain);
				if (HttpRuntime.GetAppDomainString(".appDomain") != null)
				{
					this._appDomainAppId = HttpRuntime.GetAppDomainString(".appId");
					this._appDomainAppPath = HttpRuntime.GetAppDomainString(".appPath");
					this._appDomainAppVPath = VirtualPath.CreateNonRelativeTrailingSlash(HttpRuntime.GetAppDomainString(".appVPath"));
					this._appDomainId = HttpRuntime.GetAppDomainString(".domainId");
					this._isOnUNCShare = StringUtil.StringStartsWith(this._appDomainAppPath, "\\\\");
					PerfCounters.Open(this._appDomainAppId);
				}
				this._fcm = new FileChangesMonitor(HostingEnvironment.FcnMode);
			}
			catch (Exception initializationException)
			{
				HttpRuntime.InitializationException = initializationException;
			}
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x0001FA3C File Offset: 0x0001DC3C
		private void SetUpDataDirectory()
		{
			string text = Path.Combine(this._appDomainAppPath, "App_Data");
			AppDomain.CurrentDomain.SetData("DataDirectory", text, new FileIOPermission(FileIOPermissionAccess.PathDiscovery, text));
		}

		// Token: 0x06000BFF RID: 3071 RVA: 0x0001FA74 File Offset: 0x0001DC74
		private void DisposeAppDomainShutdownTimer()
		{
			Timer appDomainShutdownTimer = this._appDomainShutdownTimer;
			if (appDomainShutdownTimer != null && Interlocked.CompareExchange<Timer>(ref this._appDomainShutdownTimer, null, appDomainShutdownTimer) == appDomainShutdownTimer)
			{
				appDomainShutdownTimer.Dispose();
			}
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x0001FAA4 File Offset: 0x0001DCA4
		private void AppDomainShutdownTimerCallback(object state)
		{
			try
			{
				this.DisposeAppDomainShutdownTimer();
				HttpRuntime.ShutdownAppDomain(ApplicationShutdownReason.InitializationError, "Initialization Error");
			}
			catch
			{
			}
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x0001FADC File Offset: 0x0001DCDC
		private void StartAppDomainShutdownTimer()
		{
			if (this._appDomainShutdownTimer == null && !this._shutdownInProgress)
			{
				lock (this)
				{
					if (this._appDomainShutdownTimer == null && !this._shutdownInProgress)
					{
						this._appDomainShutdownTimer = new Timer(new TimerCallback(this.AppDomainShutdownTimerCallback), null, 10000, 0);
					}
				}
			}
		}

		// Token: 0x06000C02 RID: 3074 RVA: 0x0001FB50 File Offset: 0x0001DD50
		private void HostingInit(HostingEnvironmentFlags hostingFlags, PolicyLevel policyLevel, Exception appDomainCreationException)
		{
			using (new ApplicationImpersonationContext())
			{
				try
				{
					this._firstRequestStartTime = DateTime.UtcNow;
					this.SetUpDataDirectory();
					this.EnsureAccessToApplicationDirectory();
					this.StartMonitoringDirectoryRenamesAndBinDirectory();
					if (HttpRuntime.InitializationException == null)
					{
						HostingEnvironment.InitializeObjectCacheHost();
					}
					CacheSection cacheSection;
					TrustSection trustSection;
					SecurityPolicySection securityPolicySection;
					CompilationSection compilationSection;
					HostingEnvironmentSection hostingEnvironmentSection;
					Exception ex;
					this.GetInitConfigSections(out cacheSection, out trustSection, out securityPolicySection, out compilationSection, out hostingEnvironmentSection, out ex);
					this.SetUpCodegenDirectory(compilationSection);
					if (compilationSection != null)
					{
						HttpRuntime._enablePrefetchOptimization = compilationSection.EnablePrefetchOptimization;
						if (HttpRuntime._enablePrefetchOptimization)
						{
							UnsafeNativeMethods.StartPrefetchActivity((uint)StringUtil.GetStringHashCode(this._appDomainAppId));
						}
					}
					if (appDomainCreationException != null)
					{
						throw appDomainCreationException;
					}
					if (trustSection == null || string.IsNullOrEmpty(trustSection.Level))
					{
						throw new ConfigurationErrorsException(SR.GetString("Config_section_not_present", new object[]
						{
							"trust"
						}));
					}
					if (trustSection.LegacyCasModel)
					{
						try
						{
							this._disableProcessRequestInApplicationTrust = false;
							this._isLegacyCas = true;
							this.SetTrustLevel(trustSection, securityPolicySection);
							goto IL_F0;
						}
						catch
						{
							if (ex != null)
							{
								throw ex;
							}
							throw;
						}
					}
					if ((hostingFlags & HostingEnvironmentFlags.ClientBuildManager) != HostingEnvironmentFlags.Default)
					{
						this._trustLevel = "Full";
					}
					else
					{
						this._disableProcessRequestInApplicationTrust = true;
						this.SetTrustParameters(trustSection, securityPolicySection, policyLevel);
					}
					IL_F0:
					this.InitFusion(hostingEnvironmentSection);
					CachedPathData.InitializeUrlMetadataSlidingExpiration(hostingEnvironmentSection);
					HttpConfigurationSystem.CompleteInit();
					if (ex != null)
					{
						throw ex;
					}
					this.SetThreadPoolLimits();
					HttpRuntime.SetAutogenKeys();
					BuildManager.InitializeBuildManager();
					if (compilationSection != null && compilationSection.ProfileGuidedOptimizations == ProfileGuidedOptimizationsFlags.All)
					{
						ProfileOptimization.SetProfileRoot(this._codegenDir);
						ProfileOptimization.StartProfile("profileoptimization.prof");
					}
					this.InitApartmentThreading();
					this.InitDebuggingSupport();
					this._processRequestInApplicationTrust = trustSection.ProcessRequestInApplicationTrust;
					AppDomainResourcePerfCounters.Init();
					this.RelaxMapPathIfRequired();
				}
				catch (Exception initializationException)
				{
					this._hostingInitFailed = true;
					HttpRuntime.InitializationException = initializationException;
					if ((hostingFlags & HostingEnvironmentFlags.ThrowHostingInitErrors) != HostingEnvironmentFlags.Default)
					{
						throw;
					}
				}
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06000C03 RID: 3075 RVA: 0x0001FD30 File Offset: 0x0001DF30
		// (set) Token: 0x06000C04 RID: 3076 RVA: 0x0001FD3C File Offset: 0x0001DF3C
		internal static Exception InitializationException
		{
			get
			{
				return HttpRuntime._theRuntime._initializationError;
			}
			set
			{
				HttpRuntime._theRuntime._initializationError = value;
				if (!HttpRuntime.HostingInitFailed)
				{
					HttpRuntime._theRuntime.StartAppDomainShutdownTimer();
				}
			}
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06000C05 RID: 3077 RVA: 0x0001FD5A File Offset: 0x0001DF5A
		internal static bool HostingInitFailed
		{
			get
			{
				return HttpRuntime._theRuntime._hostingInitFailed;
			}
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x0001FD66 File Offset: 0x0001DF66
		internal static void InitializeHostingFeatures(HostingEnvironmentFlags hostingFlags, PolicyLevel policyLevel, Exception appDomainCreationException)
		{
			HttpRuntime._theRuntime.HostingInit(hostingFlags, policyLevel, appDomainCreationException);
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06000C07 RID: 3079 RVA: 0x0001FD75 File Offset: 0x0001DF75
		internal static bool EnableHeaderChecking
		{
			get
			{
				return HttpRuntime._theRuntime._enableHeaderChecking;
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06000C08 RID: 3080 RVA: 0x0001FD81 File Offset: 0x0001DF81
		internal static bool ProcessRequestInApplicationTrust
		{
			get
			{
				return HttpRuntime._theRuntime._processRequestInApplicationTrust;
			}
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06000C09 RID: 3081 RVA: 0x0001FD8D File Offset: 0x0001DF8D
		internal static bool DisableProcessRequestInApplicationTrust
		{
			get
			{
				return HttpRuntime._theRuntime._disableProcessRequestInApplicationTrust;
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06000C0A RID: 3082 RVA: 0x0001FD99 File Offset: 0x0001DF99
		internal static bool IsLegacyCas
		{
			get
			{
				return HttpRuntime._theRuntime._isLegacyCas;
			}
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06000C0B RID: 3083 RVA: 0x0001FDA5 File Offset: 0x0001DFA5
		internal static byte[] AppOfflineMessage
		{
			get
			{
				return HttpRuntime._theRuntime._appOfflineMessage;
			}
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x0001FDB4 File Offset: 0x0001DFB4
		private void FirstRequestInit(HttpContext context)
		{
			Exception ex = null;
			if (HttpRuntime.InitializationException == null && this._appDomainId != null)
			{
				try
				{
					using (new ApplicationImpersonationContext())
					{
						CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
						CultureInfo currentUICulture = Thread.CurrentThread.CurrentUICulture;
						try
						{
							HttpRuntime.InitHttpConfiguration();
							HttpRuntime.CheckApplicationEnabled();
							this.CheckAccessToTempDirectory();
							this.InitializeHealthMonitoring();
							this.InitRequestQueue();
							this.InitTrace(context);
							HealthMonitoringManager.StartHealthMonitoringHeartbeat();
							HttpRuntime.RestrictIISFolders(context);
							this.PreloadAssembliesFromBin();
							this.InitHeaderEncoding();
							HttpEncoder.InitializeOnFirstRequest();
							RequestValidator.InitializeOnFirstRequest();
							if (context.WorkerRequest is ISAPIWorkerRequestOutOfProc)
							{
								ProcessModelSection processModel = RuntimeConfig.GetMachineConfig().ProcessModel;
							}
						}
						finally
						{
							Thread.CurrentThread.CurrentUICulture = currentUICulture;
							HttpRuntime.SetCurrentThreadCultureWithAssert(currentCulture);
						}
					}
				}
				catch (ConfigurationException ex2)
				{
					ex = ex2;
				}
				catch (Exception ex3)
				{
					ex = new HttpException(SR.GetString("XSP_init_error", new object[]
					{
						ex3.Message
					}), ex3);
				}
			}
			if (HttpRuntime.InitializationException != null)
			{
				throw new HttpException(HttpRuntime.InitializationException.Message, HttpRuntime.InitializationException);
			}
			if (ex != null)
			{
				HttpRuntime.InitializationException = ex;
				throw ex;
			}
			HttpRuntime.AddAppDomainTraceMessage("FirstRequestInit");
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x0001FF08 File Offset: 0x0001E108
		[SecurityPermission(SecurityAction.Assert, ControlThread = true)]
		internal static void SetCurrentThreadCultureWithAssert(CultureInfo cultureInfo)
		{
			Thread.CurrentThread.CurrentCulture = cultureInfo;
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x0001FF18 File Offset: 0x0001E118
		private void EnsureFirstRequestInit(HttpContext context)
		{
			if (this._beforeFirstRequest)
			{
				lock (this)
				{
					if (this._beforeFirstRequest)
					{
						this._firstRequestStartTime = DateTime.UtcNow;
						this.FirstRequestInit(context);
						this._beforeFirstRequest = false;
						context.FirstRequest = true;
					}
				}
			}
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x0001FF80 File Offset: 0x0001E180
		private void EnsureAccessToApplicationDirectory()
		{
			if (FileUtil.DirectoryAccessible(this._appDomainAppPath))
			{
				return;
			}
			if (this._appDomainAppPath.IndexOf('?') >= 0)
			{
				throw new HttpException(SR.GetString("Access_denied_to_unicode_app_dir", new object[]
				{
					this._appDomainAppPath
				}));
			}
			throw new HttpException(SR.GetString("Access_denied_to_app_dir", new object[]
			{
				this._appDomainAppPath
			}));
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x0001FFE8 File Offset: 0x0001E1E8
		private void StartMonitoringDirectoryRenamesAndBinDirectory()
		{
			this._fcm.StartMonitoringDirectoryRenamesAndBinDirectory(HttpRuntime.AppDomainAppPathInternal, new FileChangeEventHandler(this.OnCriticalDirectoryChange));
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x00020006 File Offset: 0x0001E206
		internal static void StartListeningToLocalResourcesDirectory(VirtualPath virtualDir)
		{
			HttpRuntime._theRuntime._fcm.StartListeningToLocalResourcesDirectory(virtualDir);
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x00020018 File Offset: 0x0001E218
		private void GetInitConfigSections(out CacheSection cacheSection, out TrustSection trustSection, out SecurityPolicySection securityPolicySection, out CompilationSection compilationSection, out HostingEnvironmentSection hostingEnvironmentSection, out Exception initException)
		{
			cacheSection = null;
			trustSection = null;
			securityPolicySection = null;
			compilationSection = null;
			hostingEnvironmentSection = null;
			initException = null;
			RuntimeConfig appLKGConfig = RuntimeConfig.GetAppLKGConfig();
			RuntimeConfig runtimeConfig = null;
			try
			{
				runtimeConfig = RuntimeConfig.GetAppConfig();
			}
			catch (Exception ex)
			{
				initException = ex;
			}
			if (runtimeConfig != null)
			{
				try
				{
					cacheSection = runtimeConfig.Cache;
				}
				catch (Exception ex2)
				{
					if (initException == null)
					{
						initException = ex2;
					}
				}
			}
			if (cacheSection == null)
			{
				cacheSection = appLKGConfig.Cache;
			}
			if (runtimeConfig != null)
			{
				try
				{
					trustSection = runtimeConfig.Trust;
				}
				catch (Exception ex3)
				{
					if (initException == null)
					{
						initException = ex3;
					}
				}
			}
			if (trustSection == null)
			{
				trustSection = appLKGConfig.Trust;
			}
			if (runtimeConfig != null)
			{
				try
				{
					securityPolicySection = runtimeConfig.SecurityPolicy;
				}
				catch (Exception ex4)
				{
					if (initException == null)
					{
						initException = ex4;
					}
				}
			}
			if (securityPolicySection == null)
			{
				securityPolicySection = appLKGConfig.SecurityPolicy;
			}
			if (runtimeConfig != null)
			{
				try
				{
					compilationSection = runtimeConfig.Compilation;
				}
				catch (Exception ex5)
				{
					if (initException == null)
					{
						initException = ex5;
					}
				}
			}
			if (compilationSection == null)
			{
				compilationSection = appLKGConfig.Compilation;
			}
			if (runtimeConfig != null)
			{
				try
				{
					hostingEnvironmentSection = runtimeConfig.HostingEnvironment;
				}
				catch (Exception ex6)
				{
					if (initException == null)
					{
						initException = ex6;
					}
				}
			}
			if (hostingEnvironmentSection == null)
			{
				hostingEnvironmentSection = appLKGConfig.HostingEnvironment;
			}
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x00020164 File Offset: 0x0001E364
		private void SetUpCodegenDirectory(CompilationSection compilationSection)
		{
			AppDomain domain = Thread.GetDomain();
			string path = AppManagerAppDomainFactory.ConstructSimpleAppName(HttpRuntime.AppDomainAppVirtualPath, HostingEnvironment.IsDevelopmentEnvironment);
			string text = null;
			string text2 = null;
			string filename = null;
			int line = 0;
			if (compilationSection != null && !string.IsNullOrEmpty(compilationSection.TempDirectory))
			{
				text = compilationSection.TempDirectory;
				compilationSection.GetTempDirectoryErrorInfo(out text2, out filename, out line);
			}
			if (text != null)
			{
				text = text.Trim();
				if (!Path.IsPathRooted(text))
				{
					text = null;
				}
				else
				{
					try
					{
						text = new DirectoryInfo(text).FullName;
					}
					catch
					{
						text = null;
					}
				}
				if (text == null)
				{
					throw new ConfigurationErrorsException(SR.GetString("Invalid_temp_directory", new object[]
					{
						text2
					}), filename, line);
				}
				try
				{
					Directory.CreateDirectory(text);
					goto IL_CD;
				}
				catch (Exception inner)
				{
					throw new ConfigurationErrorsException(SR.GetString("Invalid_temp_directory", new object[]
					{
						text2
					}), inner, filename, line);
				}
			}
			text = Path.Combine(HttpRuntime.s_installDirectory, "Temporary ASP.NET Files");
			IL_CD:
			if (!Util.HasWriteAccessToDirectory(text))
			{
				if (!BuildManagerHost.InClientBuildManager && !Environment.UserInteractive)
				{
					throw new HttpException(SR.GetString("No_codegen_access", new object[]
					{
						Util.GetCurrentAccountName(),
						text
					}));
				}
				text = Path.GetTempPath();
				text = Path.Combine(text, "Temporary ASP.NET Files");
			}
			this._tempDir = text;
			string dynamicBase = Path.Combine(text, path);
			domain.SetDynamicBase(dynamicBase);
			this._codegenDir = Thread.GetDomain().DynamicDirectory;
			Directory.CreateDirectory(this._codegenDir);
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x000202D8 File Offset: 0x0001E4D8
		private void InitFusion(HostingEnvironmentSection hostingEnvironmentSection)
		{
			AppDomain domain = Thread.GetDomain();
			string text = this._appDomainAppPath;
			if (text.IndexOf(HttpRuntime.DoubleDirectorySeparatorString, 1, StringComparison.Ordinal) >= 1)
			{
				text = text[0].ToString() + text.Substring(1).Replace(HttpRuntime.DoubleDirectorySeparatorString, HttpRuntime.DirectorySeparatorString);
			}
			domain.AppendPrivatePath(text + "bin");
			if (hostingEnvironmentSection != null && !hostingEnvironmentSection.ShadowCopyBinAssemblies)
			{
				domain.ClearShadowCopyPath();
			}
			else
			{
				domain.SetShadowCopyPath(text + "bin");
			}
			string fullName = Directory.GetParent(this._codegenDir).FullName;
			domain.SetCachePath(fullName);
			this._fusionInited = true;
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x00020384 File Offset: 0x0001E584
		private void InitRequestQueue()
		{
			RuntimeConfig appConfig = RuntimeConfig.GetAppConfig();
			HttpRuntimeSection httpRuntime = appConfig.HttpRuntime;
			ProcessModelSection processModel = appConfig.ProcessModel;
			if (processModel.AutoConfig)
			{
				this._requestQueue = new RequestQueue(88 * processModel.CpuCount, 76 * processModel.CpuCount, httpRuntime.AppRequestQueueLimit, processModel.ClientConnectedCheck);
				return;
			}
			int num = (processModel.MaxWorkerThreadsTimesCpuCount < processModel.MaxIoThreadsTimesCpuCount) ? processModel.MaxWorkerThreadsTimesCpuCount : processModel.MaxIoThreadsTimesCpuCount;
			if (httpRuntime.MinFreeThreads >= num)
			{
				if (httpRuntime.ElementInformation.Properties["minFreeThreads"].LineNumber != 0)
				{
					throw new ConfigurationErrorsException(SR.GetString("Min_free_threads_must_be_under_thread_pool_limits", new object[]
					{
						num.ToString(CultureInfo.InvariantCulture)
					}), httpRuntime.ElementInformation.Properties["minFreeThreads"].Source, httpRuntime.ElementInformation.Properties["minFreeThreads"].LineNumber);
				}
				if (processModel.ElementInformation.Properties["maxWorkerThreads"].LineNumber != 0)
				{
					throw new ConfigurationErrorsException(SR.GetString("Thread_pool_limit_must_be_greater_than_minFreeThreads", new object[]
					{
						httpRuntime.MinFreeThreads.ToString(CultureInfo.InvariantCulture)
					}), processModel.ElementInformation.Properties["maxWorkerThreads"].Source, processModel.ElementInformation.Properties["maxWorkerThreads"].LineNumber);
				}
				throw new ConfigurationErrorsException(SR.GetString("Thread_pool_limit_must_be_greater_than_minFreeThreads", new object[]
				{
					httpRuntime.MinFreeThreads.ToString(CultureInfo.InvariantCulture)
				}), processModel.ElementInformation.Properties["maxIoThreads"].Source, processModel.ElementInformation.Properties["maxIoThreads"].LineNumber);
			}
			else
			{
				if (httpRuntime.MinLocalRequestFreeThreads <= httpRuntime.MinFreeThreads)
				{
					this._requestQueue = new RequestQueue(httpRuntime.MinFreeThreads, httpRuntime.MinLocalRequestFreeThreads, httpRuntime.AppRequestQueueLimit, processModel.ClientConnectedCheck);
					return;
				}
				if (httpRuntime.ElementInformation.Properties["minLocalRequestFreeThreads"].LineNumber == 0)
				{
					throw new ConfigurationErrorsException(SR.GetString("Local_free_threads_cannot_exceed_free_threads"), processModel.ElementInformation.Properties["minFreeThreads"].Source, processModel.ElementInformation.Properties["minFreeThreads"].LineNumber);
				}
				throw new ConfigurationErrorsException(SR.GetString("Local_free_threads_cannot_exceed_free_threads"), httpRuntime.ElementInformation.Properties["minLocalRequestFreeThreads"].Source, httpRuntime.ElementInformation.Properties["minLocalRequestFreeThreads"].LineNumber);
			}
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x00020630 File Offset: 0x0001E830
		private void InitApartmentThreading()
		{
			HttpRuntimeSection httpRuntime = RuntimeConfig.GetAppConfig().HttpRuntime;
			if (httpRuntime != null)
			{
				this._apartmentThreading = httpRuntime.ApartmentThreading;
				return;
			}
			this._apartmentThreading = false;
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x00020660 File Offset: 0x0001E860
		private void InitTrace(HttpContext context)
		{
			TraceSection trace = RuntimeConfig.GetAppConfig().Trace;
			HttpRuntime.Profile.RequestsToProfile = trace.RequestLimit;
			HttpRuntime.Profile.PageOutput = trace.PageOutput;
			HttpRuntime.Profile.OutputMode = TraceMode.SortByTime;
			if (trace.TraceMode == TraceDisplayMode.SortByCategory)
			{
				HttpRuntime.Profile.OutputMode = TraceMode.SortByCategory;
			}
			HttpRuntime.Profile.LocalOnly = trace.LocalOnly;
			HttpRuntime.Profile.IsEnabled = trace.Enabled;
			HttpRuntime.Profile.MostRecent = trace.MostRecent;
			HttpRuntime.Profile.Reset();
			context.TraceIsEnabled = trace.Enabled;
			TraceContext.SetWriteToDiagnosticsTrace(trace.WriteToDiagnosticsTrace);
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x00020708 File Offset: 0x0001E908
		private void InitDebuggingSupport()
		{
			CompilationSection compilation = RuntimeConfig.GetAppConfig().Compilation;
			this._debuggingEnabled = compilation.Debug;
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x0002072C File Offset: 0x0001E92C
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		private void PreloadAssembliesFromBin()
		{
			bool flag = false;
			if (!this._isOnUNCShare)
			{
				IdentitySection identity = RuntimeConfig.GetAppConfig().Identity;
				if (identity.Impersonate && identity.ImpersonateToken == IntPtr.Zero)
				{
					flag = true;
				}
			}
			if (!flag)
			{
				return;
			}
			string binDirectoryInternal = HttpRuntime.BinDirectoryInternal;
			DirectoryInfo directoryInfo = new DirectoryInfo(binDirectoryInternal);
			if (!directoryInfo.Exists)
			{
				return;
			}
			this.PreloadAssembliesFromBinRecursive(directoryInfo);
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x0002078C File Offset: 0x0001E98C
		private void PreloadAssembliesFromBinRecursive(DirectoryInfo dirInfo)
		{
			FileInfo[] files = dirInfo.GetFiles("*.dll");
			foreach (FileInfo fileInfo in files)
			{
				try
				{
					Assembly.Load(Util.GetAssemblyNameFromFileName(fileInfo.Name));
				}
				catch (FileNotFoundException)
				{
					try
					{
						Assembly.LoadFrom(fileInfo.FullName);
					}
					catch
					{
					}
				}
				catch
				{
				}
			}
			DirectoryInfo[] directories = dirInfo.GetDirectories();
			foreach (DirectoryInfo dirInfo2 in directories)
			{
				this.PreloadAssembliesFromBinRecursive(dirInfo2);
			}
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x0002083C File Offset: 0x0001EA3C
		private void SetAutoConfigLimits(ProcessModelSection pmConfig)
		{
			int num;
			int num2;
			ThreadPool.GetMaxThreads(out num, out num2);
			if (pmConfig.DefaultMaxWorkerThreadsForAutoConfig != num || pmConfig.DefaultMaxIoThreadsForAutoConfig != num2)
			{
				UnsafeNativeMethods.SetClrThreadPoolLimits(pmConfig.DefaultMaxWorkerThreadsForAutoConfig, pmConfig.DefaultMaxIoThreadsForAutoConfig, true);
			}
			ServicePointManager.DefaultConnectionLimit = int.MaxValue;
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x00020880 File Offset: 0x0001EA80
		private void SetThreadPoolLimits()
		{
			try
			{
				ProcessModelSection processModel = RuntimeConfig.GetMachineConfig().ProcessModel;
				if (processModel.AutoConfig)
				{
					this.SetAutoConfigLimits(processModel);
				}
				else if (processModel.MaxWorkerThreadsTimesCpuCount > 0 && processModel.MaxIoThreadsTimesCpuCount > 0)
				{
					int num;
					int num2;
					ThreadPool.GetMaxThreads(out num, out num2);
					if (processModel.MaxWorkerThreadsTimesCpuCount != num || processModel.MaxIoThreadsTimesCpuCount != num2)
					{
						UnsafeNativeMethods.SetClrThreadPoolLimits(processModel.MaxWorkerThreadsTimesCpuCount, processModel.MaxIoThreadsTimesCpuCount, false);
					}
				}
				if (processModel.MinWorkerThreadsTimesCpuCount > 0 || processModel.MinIoThreadsTimesCpuCount > 0)
				{
					int num3;
					int num4;
					ThreadPool.GetMinThreads(out num3, out num4);
					int num5 = (processModel.MinWorkerThreadsTimesCpuCount > 0) ? processModel.MinWorkerThreadsTimesCpuCount : num3;
					int num6 = (processModel.MinIoThreadsTimesCpuCount > 0) ? processModel.MinIoThreadsTimesCpuCount : num4;
					if (num5 > 0 && num6 > 0 && (num5 != num3 || num6 != num4))
					{
						ThreadPool.SetMinThreads(num5, num6);
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x00020960 File Offset: 0x0001EB60
		internal static void CheckApplicationEnabled()
		{
			string text = Path.Combine(HttpRuntime._theRuntime._appDomainAppPath, "App_Offline.htm");
			bool flag = false;
			HttpRuntime._theRuntime._fcm.StartMonitoringFile(text, new FileChangeEventHandler(HttpRuntime._theRuntime.OnAppOfflineFileChange));
			try
			{
				if (File.Exists(text))
				{
					using (FileStream fileStream = new FileStream(text, FileMode.Open, FileAccess.Read, FileShare.Read))
					{
						if (fileStream.Length <= 1048576L)
						{
							int num = (int)fileStream.Length;
							if (num > 0)
							{
								byte[] array = new byte[num];
								if (fileStream.Read(array, 0, num) == num)
								{
									HttpRuntime._theRuntime._appOfflineMessage = array;
									flag = true;
								}
							}
							else
							{
								flag = true;
								HttpRuntime._theRuntime._appOfflineMessage = new byte[0];
							}
						}
					}
				}
			}
			catch
			{
			}
			if (flag)
			{
				throw new HttpException(503, string.Empty);
			}
			HttpRuntimeSection httpRuntime = RuntimeConfig.GetAppConfig().HttpRuntime;
			if (!httpRuntime.Enable)
			{
				throw new HttpException(404, string.Empty);
			}
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x00020A70 File Offset: 0x0001EC70
		[FileIOPermission(SecurityAction.Assert, Unrestricted = true)]
		private void CheckAccessToTempDirectory()
		{
			if (HostingEnvironment.HasHostingIdentity)
			{
				using (new ApplicationImpersonationContext())
				{
					if (!Util.HasWriteAccessToDirectory(this._tempDir))
					{
						throw new HttpException(SR.GetString("No_codegen_access", new object[]
						{
							Util.GetCurrentAccountName(),
							this._tempDir
						}));
					}
				}
			}
		}

		// Token: 0x06000C1F RID: 3103 RVA: 0x00020ADC File Offset: 0x0001ECDC
		private void InitializeHealthMonitoring()
		{
			ProcessModelSection processModel = RuntimeConfig.GetMachineConfig().ProcessModel;
			int deadlockIntervalSeconds = (int)processModel.ResponseDeadlockInterval.TotalSeconds;
			int requestQueueLimit = processModel.RequestQueueLimit;
			UnsafeNativeMethods.InitializeHealthMonitor(deadlockIntervalSeconds, requestQueueLimit);
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x00020B14 File Offset: 0x0001ED14
		private static void InitHttpConfiguration()
		{
			if (!HttpRuntime._theRuntime._configInited)
			{
				HttpRuntime._theRuntime._configInited = true;
				HttpConfigurationSystem.EnsureInit(null, true, true);
				GlobalizationSection globalization = RuntimeConfig.GetAppLKGConfig().Globalization;
				if (globalization != null)
				{
					if (!string.IsNullOrEmpty(globalization.Culture) && !StringUtil.StringStartsWithIgnoreCase(globalization.Culture, "auto"))
					{
						HttpRuntime.SetCurrentThreadCultureWithAssert(HttpServerUtility.CreateReadOnlyCultureInfo(globalization.Culture));
					}
					if (!string.IsNullOrEmpty(globalization.UICulture) && !StringUtil.StringStartsWithIgnoreCase(globalization.UICulture, "auto"))
					{
						Thread.CurrentThread.CurrentUICulture = HttpServerUtility.CreateReadOnlyCultureInfo(globalization.UICulture);
					}
				}
				RuntimeConfig appConfig = RuntimeConfig.GetAppConfig();
				object obj = appConfig.ProcessModel;
				obj = appConfig.HostingEnvironment;
			}
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x00020BC8 File Offset: 0x0001EDC8
		private void InitHeaderEncoding()
		{
			HttpRuntimeSection httpRuntime = RuntimeConfig.GetAppConfig().HttpRuntime;
			this._enableHeaderChecking = httpRuntime.EnableHeaderChecking;
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x00020BEC File Offset: 0x0001EDEC
		private static void SetAutogenKeys()
		{
			byte[] array = new byte[HttpRuntime.s_autogenKeys.Length];
			byte[] array2 = new byte[HttpRuntime.s_autogenKeys.Length];
			bool flag = false;
			RNGCryptoServiceProvider rngcryptoServiceProvider = new RNGCryptoServiceProvider();
			rngcryptoServiceProvider.GetBytes(array);
			if (!flag)
			{
				flag = (UnsafeNativeMethods.EcbCallISAPI(IntPtr.Zero, UnsafeNativeMethods.CallISAPIFunc.GetAutogenKeys, array, array.Length, array2, array2.Length) == 1);
			}
			if (flag)
			{
				Buffer.BlockCopy(array2, 0, HttpRuntime.s_autogenKeys, 0, HttpRuntime.s_autogenKeys.Length);
				return;
			}
			Buffer.BlockCopy(array, 0, HttpRuntime.s_autogenKeys, 0, HttpRuntime.s_autogenKeys.Length);
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x00020C68 File Offset: 0x0001EE68
		internal static void IncrementActivePipelineCount()
		{
			Interlocked.Increment(ref HttpRuntime._theRuntime._activeRequestCount);
			HostingEnvironment.IncrementBusyCount();
		}

		// Token: 0x06000C24 RID: 3108 RVA: 0x00020C7F File Offset: 0x0001EE7F
		internal static void DecrementActivePipelineCount()
		{
			HostingEnvironment.DecrementBusyCount();
			Interlocked.Decrement(ref HttpRuntime._theRuntime._activeRequestCount);
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x00020C98 File Offset: 0x0001EE98
		internal static void PopulateIISVersionInformation()
		{
			if (HttpRuntime.IsEngineLoaded)
			{
				uint num;
				bool useIntegratedPipeline;
				UnsafeIISMethods.MgdGetIISVersionInformation(out num, out useIntegratedPipeline);
				if (num != 0U)
				{
					HttpRuntime._iisVersion = new Version((int)(num >> 16), (int)(num & 65535U));
					HttpRuntime._useIntegratedPipeline = useIntegratedPipeline;
				}
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06000C26 RID: 3110 RVA: 0x00020CD3 File Offset: 0x0001EED3
		public static Version IISVersion
		{
			get
			{
				return HttpRuntime._iisVersion;
			}
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06000C27 RID: 3111 RVA: 0x00020CDA File Offset: 0x0001EEDA
		public static bool UsingIntegratedPipeline
		{
			get
			{
				return HttpRuntime.UseIntegratedPipeline;
			}
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06000C28 RID: 3112 RVA: 0x00020CE1 File Offset: 0x0001EEE1
		internal static bool UseIntegratedPipeline
		{
			get
			{
				return HttpRuntime._useIntegratedPipeline;
			}
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06000C29 RID: 3113 RVA: 0x00020CE8 File Offset: 0x0001EEE8
		internal static bool EnablePrefetchOptimization
		{
			get
			{
				return HttpRuntime._enablePrefetchOptimization;
			}
		}

		// Token: 0x06000C2A RID: 3114 RVA: 0x00020CEF File Offset: 0x0001EEEF
		internal static RequestNotificationStatus ProcessRequestNotification(IIS7WorkerRequest wr, HttpContext context)
		{
			return HttpRuntime._theRuntime.ProcessRequestNotificationPrivate(wr, context);
		}

		// Token: 0x06000C2B RID: 3115 RVA: 0x00020D00 File Offset: 0x0001EF00
		private RequestNotificationStatus ProcessRequestNotificationPrivate(IIS7WorkerRequest wr, HttpContext context)
		{
			RequestNotificationStatus requestNotificationStatus = RequestNotificationStatus.Pending;
			try
			{
				int currentModuleIndex;
				bool isPostNotification;
				int currentNotification;
				UnsafeIISMethods.MgdGetCurrentNotificationInfo(wr.RequestContext, out currentModuleIndex, out isPostNotification, out currentNotification);
				context.CurrentModuleIndex = currentModuleIndex;
				context.IsPostNotification = isPostNotification;
				context.CurrentNotification = (RequestNotification)currentNotification;
				IHttpHandler httpHandler = null;
				if (context.NeedToInitializeApp())
				{
					try
					{
						this.EnsureFirstRequestInit(context);
					}
					catch
					{
						if (!context.Request.IsDebuggingRequest)
						{
							throw;
						}
					}
					context.Response.InitResponseWriter();
					httpHandler = HttpApplicationFactory.GetApplicationInstance(context);
					if (httpHandler == null)
					{
						throw new HttpException(SR.GetString("Unable_create_app_object"));
					}
					if (EtwTrace.IsTraceEnabled(5, 1))
					{
						EtwTrace.Trace(EtwTraceType.ETW_TYPE_START_HANDLER, context.WorkerRequest, httpHandler.GetType().FullName, "Start");
					}
					HttpApplication httpApplication = httpHandler as HttpApplication;
					if (httpApplication != null)
					{
						httpApplication.AssignContext(context);
					}
				}
				wr.SynchronizeVariables(context);
				if (context.ApplicationInstance != null)
				{
					IAsyncResult asyncResult = context.ApplicationInstance.BeginProcessRequestNotification(context, this._requestNotificationCompletionCallback);
					if (asyncResult.CompletedSynchronously)
					{
						requestNotificationStatus = RequestNotificationStatus.Continue;
					}
				}
				else if (httpHandler != null)
				{
					httpHandler.ProcessRequest(context);
					requestNotificationStatus = RequestNotificationStatus.FinishRequest;
				}
				else
				{
					requestNotificationStatus = RequestNotificationStatus.Continue;
				}
			}
			catch (Exception errorInfo)
			{
				requestNotificationStatus = RequestNotificationStatus.FinishRequest;
				context.Response.InitResponseWriter();
				context.AddError(errorInfo);
			}
			if (requestNotificationStatus != RequestNotificationStatus.Pending)
			{
				this.FinishRequestNotification(wr, context, ref requestNotificationStatus);
			}
			return requestNotificationStatus;
		}

		// Token: 0x06000C2C RID: 3116 RVA: 0x00020E48 File Offset: 0x0001F048
		private void FinishRequestNotification(IIS7WorkerRequest wr, HttpContext context, ref RequestNotificationStatus status)
		{
			HttpApplication applicationInstance = context.ApplicationInstance;
			if (context.NotificationContext.RequestCompleted)
			{
				status = RequestNotificationStatus.FinishRequest;
			}
			context.ReportRuntimeErrorIfExists(ref status);
			if (status == RequestNotificationStatus.FinishRequest && (context.CurrentNotification == RequestNotification.LogRequest || context.CurrentNotification == RequestNotification.EndRequest))
			{
				status = RequestNotificationStatus.Continue;
			}
			IntPtr requestContext = wr.RequestContext;
			bool flag = UnsafeIISMethods.MgdIsLastNotification(requestContext, status);
			try
			{
				context.Response.UpdateNativeResponse(flag);
			}
			catch (Exception errorInfo)
			{
				wr.UnlockCachedResponseBytes();
				context.AddError(errorInfo);
				context.ReportRuntimeErrorIfExists(ref status);
				try
				{
					context.Response.UpdateNativeResponse(flag);
				}
				catch
				{
				}
			}
			if (flag)
			{
				context.FinishPipelineRequest();
			}
			if (status != RequestNotificationStatus.Pending)
			{
				PipelineRuntime.DisposeHandler(context, requestContext, status);
			}
		}

		// Token: 0x06000C2D RID: 3117 RVA: 0x00020F10 File Offset: 0x0001F110
		internal static void FinishPipelineRequest(HttpContext context)
		{
			HttpRuntime._theRuntime._firstRequestCompleted = true;
			context.RaiseOnRequestCompleted();
			context.Request.Dispose();
			context.Response.Dispose();
			HttpApplication applicationInstance = context.ApplicationInstance;
			if (applicationInstance != null)
			{
				ThreadContext indicateCompletionContext = context.IndicateCompletionContext;
				if (indicateCompletionContext != null && !indicateCompletionContext.HasBeenDisassociatedFromThread)
				{
					ThreadContext obj = indicateCompletionContext;
					lock (obj)
					{
						if (!indicateCompletionContext.HasBeenDisassociatedFromThread)
						{
							indicateCompletionContext.DisassociateFromCurrentThread();
							context.IndicateCompletionContext = null;
							context.InIndicateCompletion = false;
						}
					}
				}
				applicationInstance.ReleaseAppInstance();
			}
			HttpRuntime.SetExecutionTimePerformanceCounter(context);
			HttpRuntime.UpdatePerfCounters(context.Response.StatusCode);
			if (EtwTrace.IsTraceEnabled(5, 1))
			{
				EtwTrace.Trace(EtwTraceType.ETW_TYPE_END_HANDLER, context.WorkerRequest);
			}
			if (HttpRuntime.HostingInitFailed)
			{
				HttpRuntime.ShutdownAppDomain(ApplicationShutdownReason.HostingEnvironment, "HostingInit error");
			}
		}

		// Token: 0x06000C2E RID: 3118 RVA: 0x00020FF0 File Offset: 0x0001F1F0
		private void ProcessRequestInternal(HttpWorkerRequest wr)
		{
			Interlocked.Increment(ref this._activeRequestCount);
			if (this._disposingHttpRuntime)
			{
				try
				{
					wr.SendStatus(503, "Server Too Busy");
					wr.SendKnownResponseHeader(12, "text/html; charset=utf-8");
					byte[] bytes = Encoding.ASCII.GetBytes("<html><body>Server Too Busy</body></html>");
					wr.SendResponseFromMemory(bytes, bytes.Length);
					wr.FlushResponse(true);
					wr.EndOfRequest();
				}
				finally
				{
					Interlocked.Decrement(ref this._activeRequestCount);
				}
				return;
			}
			HttpContext httpContext;
			try
			{
				httpContext = new HttpContext(wr, false);
			}
			catch
			{
				try
				{
					wr.SendStatus(400, "Bad Request");
					wr.SendKnownResponseHeader(12, "text/html; charset=utf-8");
					byte[] bytes2 = Encoding.ASCII.GetBytes("<html><body>Bad Request</body></html>");
					wr.SendResponseFromMemory(bytes2, bytes2.Length);
					wr.FlushResponse(true);
					wr.EndOfRequest();
					return;
				}
				finally
				{
					Interlocked.Decrement(ref this._activeRequestCount);
				}
			}
			wr.SetEndOfSendNotification(this._asyncEndOfSendCallback, httpContext);
			HostingEnvironment.IncrementBusyCount();
			try
			{
				try
				{
					this.EnsureFirstRequestInit(httpContext);
				}
				catch
				{
					if (!httpContext.Request.IsDebuggingRequest)
					{
						throw;
					}
				}
				httpContext.Response.InitResponseWriter();
				IHttpHandler applicationInstance = HttpApplicationFactory.GetApplicationInstance(httpContext);
				if (applicationInstance == null)
				{
					throw new HttpException(SR.GetString("Unable_create_app_object"));
				}
				if (EtwTrace.IsTraceEnabled(5, 1))
				{
					EtwTrace.Trace(EtwTraceType.ETW_TYPE_START_HANDLER, httpContext.WorkerRequest, applicationInstance.GetType().FullName, "Start");
				}
				if (applicationInstance is IHttpAsyncHandler)
				{
					IHttpAsyncHandler httpAsyncHandler = (IHttpAsyncHandler)applicationInstance;
					httpContext.AsyncAppHandler = httpAsyncHandler;
					httpAsyncHandler.BeginProcessRequest(httpContext, this._handlerCompletionCallback, httpContext);
				}
				else
				{
					applicationInstance.ProcessRequest(httpContext);
					this.FinishRequest(httpContext.WorkerRequest, httpContext, null);
				}
			}
			catch (Exception e)
			{
				httpContext.Response.InitResponseWriter();
				this.FinishRequest(wr, httpContext, e);
			}
		}

		// Token: 0x06000C2F RID: 3119 RVA: 0x000211DC File Offset: 0x0001F3DC
		private void RejectRequestInternal(HttpWorkerRequest wr, bool silent)
		{
			HttpContext httpContext = new HttpContext(wr, false);
			wr.SetEndOfSendNotification(this._asyncEndOfSendCallback, httpContext);
			Interlocked.Increment(ref this._activeRequestCount);
			HostingEnvironment.IncrementBusyCount();
			if (silent)
			{
				httpContext.Response.InitResponseWriter();
				this.FinishRequest(wr, httpContext, null);
				return;
			}
			PerfCounters.IncrementGlobalCounter(GlobalPerfCounter.REQUESTS_REJECTED);
			PerfCounters.IncrementCounter(AppPerfCounter.APP_REQUESTS_REJECTED);
			try
			{
				throw new HttpException(503, SR.GetString("Server_too_busy"));
			}
			catch (Exception e)
			{
				httpContext.Response.InitResponseWriter();
				this.FinishRequest(wr, httpContext, e);
			}
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x00021270 File Offset: 0x0001F470
		internal static void ReportAppOfflineErrorMessage(HttpResponse response, byte[] appOfflineMessage)
		{
			response.StatusCode = 503;
			response.ContentType = "text/html";
			response.AddHeader("Retry-After", "3600");
			response.OutputStream.Write(appOfflineMessage, 0, appOfflineMessage.Length);
		}

		// Token: 0x06000C31 RID: 3121 RVA: 0x000212A8 File Offset: 0x0001F4A8
		private void FinishRequest(HttpWorkerRequest wr, HttpContext context, Exception e)
		{
			HttpResponse response = context.Response;
			if (EtwTrace.IsTraceEnabled(5, 1))
			{
				EtwTrace.Trace(EtwTraceType.ETW_TYPE_END_HANDLER, context.WorkerRequest);
			}
			HttpRuntime.SetExecutionTimePerformanceCounter(context);
			if (e == null)
			{
				using (new ClientImpersonationContext(context, false))
				{
					try
					{
						response.FinalFlushAtTheEndOfRequestProcessing();
					}
					catch (Exception ex)
					{
						e = ex;
					}
				}
			}
			if (e != null)
			{
				using (new DisposableHttpContextWrapper(context))
				{
					context.DisableCustomHttpEncoder = true;
					if (this._appOfflineMessage != null)
					{
						try
						{
							HttpRuntime.ReportAppOfflineErrorMessage(response, this._appOfflineMessage);
							response.FinalFlushAtTheEndOfRequestProcessing();
							goto IL_BA;
						}
						catch
						{
							goto IL_BA;
						}
					}
					using (new ApplicationImpersonationContext())
					{
						try
						{
							try
							{
								response.ReportRuntimeError(e, true, false);
							}
							catch (Exception e2)
							{
								response.ReportRuntimeError(e2, false, false);
							}
							response.FinalFlushAtTheEndOfRequestProcessing();
						}
						catch
						{
						}
					}
				}
			}
			IL_BA:
			this._firstRequestCompleted = true;
			if (this._hostingInitFailed)
			{
				HttpRuntime.ShutdownAppDomain(ApplicationShutdownReason.HostingEnvironment, "HostingInit error");
			}
			int statusCode = response.StatusCode;
			HttpRuntime.UpdatePerfCounters(statusCode);
			context.FinishRequestForCachedPathData(statusCode);
			try
			{
				wr.EndOfRequest();
			}
			catch (Exception e3)
			{
				WebBaseEvent.RaiseRuntimeError(e3, this);
			}
			HostingEnvironment.DecrementBusyCount();
			Interlocked.Decrement(ref this._activeRequestCount);
			if (this._requestQueue != null)
			{
				this._requestQueue.ScheduleMoreWorkIfNeeded();
			}
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x0002143C File Offset: 0x0001F63C
		private bool InitiateShutdownOnce()
		{
			if (this._shutdownInProgress)
			{
				return false;
			}
			lock (this)
			{
				if (this._shutdownInProgress)
				{
					return false;
				}
				this._shutdownInProgress = true;
			}
			return true;
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x00021494 File Offset: 0x0001F694
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		private void ReleaseResourcesAndUnloadAppDomain(object state)
		{
			try
			{
				PerfCounters.IncrementGlobalCounter(GlobalPerfCounter.APPLICATION_RESTARTS);
			}
			catch
			{
			}
			try
			{
				this.Dispose();
			}
			catch
			{
			}
			Thread.Sleep(250);
			HttpRuntime.AddAppDomainTraceMessage("before Unload");
			for (;;)
			{
				try
				{
					AppDomain.Unload(Thread.GetDomain());
				}
				catch (CannotUnloadAppDomainException)
				{
				}
				catch (Exception ex)
				{
					if (!BuildManagerHost.InClientBuildManager)
					{
						string str = "Unload Exception: ";
						Exception ex2 = ex;
						HttpRuntime.AddAppDomainTraceMessage(str + ((ex2 != null) ? ex2.ToString() : null));
					}
					throw;
				}
			}
		}

		// Token: 0x06000C34 RID: 3124 RVA: 0x00021538 File Offset: 0x0001F738
		private static void SetExecutionTimePerformanceCounter(HttpContext context)
		{
			long num = DateTime.UtcNow.Subtract(context.WorkerRequest.GetStartTime()).Ticks / 10000L;
			if (num > 2147483647L)
			{
				num = 2147483647L;
			}
			PerfCounters.SetGlobalCounter(GlobalPerfCounter.REQUEST_EXECUTION_TIME, (int)num);
			PerfCounters.SetCounter(AppPerfCounter.APP_REQUEST_EXEC_TIME, (int)num);
		}

		// Token: 0x06000C35 RID: 3125 RVA: 0x00021590 File Offset: 0x0001F790
		private static void UpdatePerfCounters(int statusCode)
		{
			if (400 > statusCode)
			{
				PerfCounters.IncrementCounter(AppPerfCounter.REQUESTS_SUCCEDED);
				return;
			}
			PerfCounters.IncrementCounter(AppPerfCounter.REQUESTS_FAILED);
			if (statusCode == 401)
			{
				PerfCounters.IncrementCounter(AppPerfCounter.REQUESTS_NOT_AUTHORIZED);
				return;
			}
			if (statusCode != 404 && statusCode != 414)
			{
				return;
			}
			PerfCounters.IncrementCounter(AppPerfCounter.REQUESTS_NOT_FOUND);
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x000215DC File Offset: 0x0001F7DC
		private void WaitForRequestsToFinish(int waitTimeoutMs)
		{
			DateTime t = DateTime.UtcNow.AddMilliseconds((double)waitTimeoutMs);
			while (this._activeRequestCount != 0 || (this._requestQueue != null && !this._requestQueue.IsEmpty))
			{
				Thread.Sleep(250);
				if (!Debugger.IsAttached && DateTime.UtcNow > t)
				{
					break;
				}
			}
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x00021634 File Offset: 0x0001F834
		private void Dispose()
		{
			int num = 90;
			try
			{
				HttpRuntimeSection httpRuntime = RuntimeConfig.GetAppLKGConfig().HttpRuntime;
				if (httpRuntime != null)
				{
					num = (int)httpRuntime.ShutdownTimeout.TotalSeconds;
				}
				this.WaitForRequestsToFinish(num * 1000);
				if (this._requestQueue != null)
				{
					this._requestQueue.Drain();
				}
			}
			finally
			{
				this._disposingHttpRuntime = true;
			}
			this.WaitForRequestsToFinish(num * 1000 / 6);
			ISAPIWorkerRequestInProcForIIS6.WaitForPendingAsyncIo();
			if (HttpRuntime.UseIntegratedPipeline)
			{
				PipelineRuntime.WaitForRequestsToDrain();
			}
			else
			{
				while (this._activeRequestCount != 0)
				{
					Thread.Sleep(250);
				}
			}
			this.DisposeAppDomainShutdownTimer();
			this._timeoutManager.Stop();
			AppDomainResourcePerfCounters.Stop();
			ISAPIWorkerRequestInProcForIIS6.WaitForPendingAsyncIo();
			SqlCacheDependencyManager.Dispose(num * 1000 / 2);
			HealthMonitoringManager.IsCacheDisposed = true;
			if (this._cachePublic != null)
			{
				CacheStoreProvider objectCache = HttpRuntime.Cache.GetObjectCache(false);
				CacheStoreProvider internalCache = HttpRuntime.Cache.GetInternalCache(false);
				if (objectCache != null)
				{
					objectCache.Dispose();
				}
				if (internalCache != null)
				{
					internalCache.Dispose();
				}
			}
			HttpApplicationFactory.EndApplication();
			this._fcm.Stop();
			HealthMonitoringManager.Shutdown();
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x0002174C File Offset: 0x0001F94C
		private void OnRequestNotificationCompletion(IAsyncResult ar)
		{
			try
			{
				this.OnRequestNotificationCompletionHelper(ar);
			}
			catch (Exception e)
			{
				ApplicationManager.RecordFatalException(e);
				throw;
			}
		}

		// Token: 0x06000C39 RID: 3129 RVA: 0x0002177C File Offset: 0x0001F97C
		private void OnRequestNotificationCompletionHelper(IAsyncResult ar)
		{
			if (ar.CompletedSynchronously)
			{
				return;
			}
			RequestNotificationStatus notificationStatus = RequestNotificationStatus.Continue;
			HttpContext httpContext = (HttpContext)ar.AsyncState;
			IIS7WorkerRequest iis7WorkerRequest = httpContext.WorkerRequest as IIS7WorkerRequest;
			try
			{
				httpContext.ApplicationInstance.EndProcessRequestNotification(ar);
			}
			catch (Exception errorInfo)
			{
				notificationStatus = RequestNotificationStatus.FinishRequest;
				httpContext.AddError(errorInfo);
			}
			IntPtr requestContext = iis7WorkerRequest.RequestContext;
			this.FinishRequestNotification(iis7WorkerRequest, httpContext, ref notificationStatus);
			httpContext.NotificationContext = null;
			int hresult = UnsafeIISMethods.MgdPostCompletion(requestContext, notificationStatus);
			Misc.ThrowIfFailedHr(hresult);
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x00021804 File Offset: 0x0001FA04
		private void OnHandlerCompletion(IAsyncResult ar)
		{
			HttpContext httpContext = (HttpContext)ar.AsyncState;
			try
			{
				httpContext.AsyncAppHandler.EndProcessRequest(ar);
			}
			catch (Exception errorInfo)
			{
				httpContext.AddError(errorInfo);
			}
			finally
			{
				httpContext.AsyncAppHandler = null;
			}
			this.FinishRequest(httpContext.WorkerRequest, httpContext, httpContext.Error);
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x0002186C File Offset: 0x0001FA6C
		private void EndOfSendCallback(HttpWorkerRequest wr, object arg)
		{
			HttpContext httpContext = (HttpContext)arg;
			httpContext.Request.Dispose();
			httpContext.Response.Dispose();
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x00021898 File Offset: 0x0001FA98
		private void OnCriticalDirectoryChange(object sender, FileChangeEvent e)
		{
			ApplicationShutdownReason reason = ApplicationShutdownReason.None;
			string name = new DirectoryInfo(e.FileName).Name;
			string text = FileChangesMonitor.GenerateErrorMessage(e.Action, null);
			text = ((text != null) ? (text + name) : (name + " dir change or directory rename"));
			if (StringUtil.EqualsIgnoreCase(name, "App_Code"))
			{
				reason = ApplicationShutdownReason.CodeDirChangeOrDirectoryRename;
			}
			else if (StringUtil.EqualsIgnoreCase(name, "App_GlobalResources"))
			{
				reason = ApplicationShutdownReason.ResourcesDirChangeOrDirectoryRename;
			}
			else if (StringUtil.EqualsIgnoreCase(name, "App_Browsers"))
			{
				reason = ApplicationShutdownReason.BrowsersDirChangeOrDirectoryRename;
			}
			else if (StringUtil.EqualsIgnoreCase(name, "bin"))
			{
				reason = ApplicationShutdownReason.BinDirChangeOrDirectoryRename;
			}
			if (e.Action == FileAction.Added)
			{
				HttpRuntime.SetUserForcedShutdown();
			}
			HttpRuntime.ShutdownAppDomain(reason, text);
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x00021938 File Offset: 0x0001FB38
		internal static void CoalesceNotifications()
		{
			int num = 0;
			int num2 = 0;
			try
			{
				HttpRuntimeSection httpRuntime = RuntimeConfig.GetAppLKGConfig().HttpRuntime;
				if (httpRuntime != null)
				{
					num = httpRuntime.WaitChangeNotification;
					num2 = httpRuntime.MaxWaitChangeNotification;
				}
			}
			catch
			{
			}
			if (num == 0 || num2 == 0)
			{
				return;
			}
			DateTime t = DateTime.UtcNow.AddSeconds((double)num2);
			try
			{
				while (DateTime.UtcNow < t && !(DateTime.UtcNow > HttpRuntime._theRuntime.LastShutdownAttemptTime.AddSeconds((double)num)))
				{
					Thread.Sleep(250);
				}
			}
			catch
			{
			}
		}

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x06000C3E RID: 3134 RVA: 0x000219E0 File Offset: 0x0001FBE0
		// (remove) Token: 0x06000C3F RID: 3135 RVA: 0x00021A14 File Offset: 0x0001FC14
		internal static event BuildManagerHostUnloadEventHandler AppDomainShutdown;

		// Token: 0x06000C40 RID: 3136 RVA: 0x00021A47 File Offset: 0x0001FC47
		internal static void OnAppDomainShutdown(BuildManagerHostUnloadEventArgs e)
		{
			if (HttpRuntime.AppDomainShutdown != null)
			{
				HttpRuntime.AppDomainShutdown(HttpRuntime._theRuntime, e);
			}
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x00021A60 File Offset: 0x0001FC60
		internal static void SetUserForcedShutdown()
		{
			HttpRuntime._theRuntime._userForcedShutdown = true;
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x00021A6D File Offset: 0x0001FC6D
		internal static bool ShutdownAppDomain(ApplicationShutdownReason reason, string message)
		{
			return HttpRuntime.ShutdownAppDomainWithStackTrace(reason, message, null);
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x00021A77 File Offset: 0x0001FC77
		internal static bool ShutdownAppDomainWithStackTrace(ApplicationShutdownReason reason, string message, string stackTrace)
		{
			HttpRuntime.SetShutdownReason(reason, message);
			return HttpRuntime.ShutdownAppDomain(stackTrace);
		}

		// Token: 0x06000C44 RID: 3140 RVA: 0x00021A88 File Offset: 0x0001FC88
		private static bool ShutdownAppDomain(string stackTrace)
		{
			if (HttpRuntime._theRuntime.LastShutdownAttemptTime == DateTime.MinValue && !HttpRuntime._theRuntime._firstRequestCompleted && !HttpRuntime._theRuntime._userForcedShutdown)
			{
				try
				{
					RuntimeConfig appLKGConfig = RuntimeConfig.GetAppLKGConfig();
					if (appLKGConfig != null)
					{
						HttpRuntimeSection httpRuntime = appLKGConfig.HttpRuntime;
						if (httpRuntime != null)
						{
							int num = (int)httpRuntime.DelayNotificationTimeout.TotalSeconds;
							if (DateTime.UtcNow < HttpRuntime._theRuntime._firstRequestStartTime.AddSeconds((double)num))
							{
								return false;
							}
						}
					}
				}
				catch
				{
				}
			}
			try
			{
				HttpRuntime._theRuntime.RaiseShutdownWebEventOnce();
			}
			catch
			{
			}
			HttpRuntime._theRuntime.LastShutdownAttemptTime = DateTime.UtcNow;
			if (!HostingEnvironment.ShutdownInitiated)
			{
				HostingEnvironment.InitiateShutdownWithoutDemand();
				return true;
			}
			if (HostingEnvironment.ShutdownInProgress)
			{
				return false;
			}
			if (!HttpRuntime._theRuntime.InitiateShutdownOnce())
			{
				return false;
			}
			if (string.IsNullOrEmpty(stackTrace) && !BuildManagerHost.InClientBuildManager)
			{
				new EnvironmentPermission(PermissionState.Unrestricted).Assert();
				try
				{
					HttpRuntime._theRuntime._shutDownStack = Environment.StackTrace;
					goto IL_FD;
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
			}
			HttpRuntime._theRuntime._shutDownStack = stackTrace;
			IL_FD:
			HttpRuntime.OnAppDomainShutdown(new BuildManagerHostUnloadEventArgs(HttpRuntime._theRuntime._shutdownReason));
			ThreadPool.QueueUserWorkItem(HttpRuntime._theRuntime._appDomainUnloadallback);
			return true;
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x00021BE4 File Offset: 0x0001FDE4
		internal static void RecoverFromUnexceptedAppDomainUnload()
		{
			if (HttpRuntime._theRuntime._shutdownInProgress)
			{
				return;
			}
			HttpRuntime._theRuntime._shutdownInProgress = true;
			try
			{
				ISAPIRuntime.RemoveThisAppDomainFromUnmanagedTable();
				PipelineRuntime.RemoveThisAppDomainFromUnmanagedTable();
				HttpRuntime.AddAppDomainTraceMessage("AppDomainRestart");
			}
			finally
			{
				HttpRuntime._theRuntime.Dispose();
			}
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x00021C3C File Offset: 0x0001FE3C
		internal static void OnConfigChange(string message)
		{
			HttpRuntime.ShutdownAppDomain(ApplicationShutdownReason.ConfigurationChange, (message != null) ? message : "CONFIG change");
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x00021C50 File Offset: 0x0001FE50
		internal static void SetShutdownReason(ApplicationShutdownReason reason, string message)
		{
			if (HttpRuntime._theRuntime._shutdownReason == ApplicationShutdownReason.None)
			{
				HttpRuntime._theRuntime._shutdownReason = reason;
			}
			HttpRuntime.SetShutdownMessage(message);
		}

		// Token: 0x06000C48 RID: 3144 RVA: 0x00021C6F File Offset: 0x0001FE6F
		internal static void SetShutdownMessage(string message)
		{
			if (message != null)
			{
				if (HttpRuntime._theRuntime._shutDownMessage == null)
				{
					HttpRuntime._theRuntime._shutDownMessage = message;
					return;
				}
				HttpRuntime theRuntime = HttpRuntime._theRuntime;
				theRuntime._shutDownMessage = theRuntime._shutDownMessage + "\r\n" + message;
			}
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06000C49 RID: 3145 RVA: 0x00021CA7 File Offset: 0x0001FEA7
		internal static ApplicationShutdownReason ShutdownReason
		{
			get
			{
				return HttpRuntime._theRuntime._shutdownReason;
			}
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x00021CB3 File Offset: 0x0001FEB3
		[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Medium)]
		public static void ProcessRequest(HttpWorkerRequest wr)
		{
			if (wr == null)
			{
				throw new ArgumentNullException("wr");
			}
			if (HttpRuntime.UseIntegratedPipeline)
			{
				throw new PlatformNotSupportedException(SR.GetString("Method_Not_Supported_By_Iis_Integrated_Mode", new object[]
				{
					"HttpRuntime.ProcessRequest"
				}));
			}
			HttpRuntime.ProcessRequestNoDemand(wr);
		}

		// Token: 0x06000C4B RID: 3147 RVA: 0x00021CF0 File Offset: 0x0001FEF0
		internal static void ProcessRequestNoDemand(HttpWorkerRequest wr)
		{
			RequestQueue requestQueue = HttpRuntime._theRuntime._requestQueue;
			wr.UpdateInitialCounters();
			if (requestQueue != null)
			{
				wr = requestQueue.GetRequestToExecute(wr);
			}
			if (wr != null)
			{
				HttpRuntime.CalculateWaitTimeAndUpdatePerfCounter(wr);
				wr.ResetStartTime();
				HttpRuntime.ProcessRequestNow(wr);
			}
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x00021D30 File Offset: 0x0001FF30
		private static void CalculateWaitTimeAndUpdatePerfCounter(HttpWorkerRequest wr)
		{
			DateTime startTime = wr.GetStartTime();
			long num = DateTime.UtcNow.Subtract(startTime).Ticks / 10000L;
			if (num > 2147483647L)
			{
				num = 2147483647L;
			}
			PerfCounters.SetGlobalCounter(GlobalPerfCounter.REQUEST_WAIT_TIME, (int)num);
			PerfCounters.SetCounter(AppPerfCounter.APP_REQUEST_WAIT_TIME, (int)num);
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x00021D83 File Offset: 0x0001FF83
		internal static void ProcessRequestNow(HttpWorkerRequest wr)
		{
			HttpRuntime._theRuntime.ProcessRequestInternal(wr);
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x00021D90 File Offset: 0x0001FF90
		internal static void RejectRequestNow(HttpWorkerRequest wr, bool silent)
		{
			HttpRuntime._theRuntime.RejectRequestInternal(wr, silent);
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x00021D9E File Offset: 0x0001FF9E
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		public static void Close()
		{
			if (HttpRuntime._theRuntime.InitiateShutdownOnce())
			{
				HttpRuntime.SetShutdownReason(ApplicationShutdownReason.HttpRuntimeClose, "HttpRuntime.Close is called");
				if (HostingEnvironment.IsHosted)
				{
					HostingEnvironment.InitiateShutdownWithoutDemand();
					return;
				}
				HttpRuntime._theRuntime.Dispose();
			}
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x00021DCF File Offset: 0x0001FFCF
		public static void UnloadAppDomain()
		{
			HttpRuntime._theRuntime._userForcedShutdown = true;
			HttpRuntime.ShutdownAppDomain(ApplicationShutdownReason.UnloadAppDomainCalled, "User code called UnloadAppDomain");
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06000C51 RID: 3153 RVA: 0x00021DE8 File Offset: 0x0001FFE8
		// (set) Token: 0x06000C52 RID: 3154 RVA: 0x00021E28 File Offset: 0x00020028
		private DateTime LastShutdownAttemptTime
		{
			get
			{
				DateTime lastShutdownAttemptTime;
				lock (this)
				{
					lastShutdownAttemptTime = this._lastShutdownAttemptTime;
				}
				return lastShutdownAttemptTime;
			}
			set
			{
				lock (this)
				{
					this._lastShutdownAttemptTime = value;
				}
			}
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06000C53 RID: 3155 RVA: 0x00021E64 File Offset: 0x00020064
		internal static Profiler Profile
		{
			get
			{
				return HttpRuntime._theRuntime._profiler;
			}
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06000C54 RID: 3156 RVA: 0x00021E70 File Offset: 0x00020070
		internal static bool IsTrustLevelInitialized
		{
			get
			{
				return !HostingEnvironment.IsHosted || HttpRuntime.TrustLevel != null;
			}
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06000C55 RID: 3157 RVA: 0x00021E83 File Offset: 0x00020083
		internal static NamedPermissionSet NamedPermissionSet
		{
			get
			{
				return HttpRuntime._theRuntime._namedPermissionSet;
			}
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06000C56 RID: 3158 RVA: 0x00021E8F File Offset: 0x0002008F
		internal static PolicyLevel PolicyLevel
		{
			get
			{
				return HttpRuntime._theRuntime._policyLevel;
			}
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06000C57 RID: 3159 RVA: 0x00021E9B File Offset: 0x0002009B
		internal static string HostSecurityPolicyResolverType
		{
			get
			{
				return HttpRuntime._theRuntime._hostSecurityPolicyResolverType;
			}
		}

		// Token: 0x06000C58 RID: 3160 RVA: 0x00021EA8 File Offset: 0x000200A8
		[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Unrestricted)]
		public static NamedPermissionSet GetNamedPermissionSet()
		{
			NamedPermissionSet namedPermissionSet = HttpRuntime._theRuntime._namedPermissionSet;
			if (namedPermissionSet == null)
			{
				return null;
			}
			return new NamedPermissionSet(namedPermissionSet);
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06000C59 RID: 3161 RVA: 0x00021ECB File Offset: 0x000200CB
		internal static bool IsFullTrust
		{
			get
			{
				return HttpRuntime._theRuntime._namedPermissionSet == null;
			}
		}

		// Token: 0x06000C5A RID: 3162 RVA: 0x00021EDC File Offset: 0x000200DC
		internal static void CheckVirtualFilePermission(string virtualPath)
		{
			string path = HostingEnvironment.MapPath(virtualPath);
			HttpRuntime.CheckFilePermission(path);
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x00021EF6 File Offset: 0x000200F6
		internal static void CheckFilePermission(string path)
		{
			HttpRuntime.CheckFilePermission(path, false);
		}

		// Token: 0x06000C5C RID: 3164 RVA: 0x00021EFF File Offset: 0x000200FF
		internal static void CheckFilePermission(string path, bool writePermissions)
		{
			if (!HttpRuntime.HasFilePermission(path, writePermissions))
			{
				throw new HttpException(SR.GetString("Access_denied_to_path", new object[]
				{
					HttpRuntime.GetSafePath(path)
				}));
			}
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x00021F29 File Offset: 0x00020129
		internal static bool HasFilePermission(string path)
		{
			return HttpRuntime.HasFilePermission(path, false);
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x00021F34 File Offset: 0x00020134
		internal static bool HasFilePermission(string path, bool writePermissions)
		{
			if (HttpRuntime.TrustLevel == null && HttpRuntime.InitializationException != null)
			{
				return true;
			}
			if (HttpRuntime.NamedPermissionSet == null)
			{
				return true;
			}
			bool result = false;
			IPermission permission = HttpRuntime.NamedPermissionSet.GetPermission(typeof(FileIOPermission));
			if (permission != null)
			{
				IPermission permission2 = null;
				try
				{
					if (!writePermissions)
					{
						permission2 = new FileIOPermission(FileIOPermissionAccess.Read, path);
					}
					else
					{
						permission2 = new FileIOPermission(FileIOPermissionAccess.AllAccess, path);
					}
				}
				catch
				{
					return false;
				}
				return permission2.IsSubsetOf(permission);
			}
			return result;
		}

		// Token: 0x06000C5F RID: 3167 RVA: 0x00021FB0 File Offset: 0x000201B0
		internal static bool HasWebPermission(Uri uri)
		{
			if (HttpRuntime.NamedPermissionSet == null)
			{
				return true;
			}
			bool result = false;
			IPermission permission = HttpRuntime.NamedPermissionSet.GetPermission(typeof(WebPermission));
			if (permission != null)
			{
				IPermission permission2 = null;
				try
				{
					permission2 = new WebPermission(NetworkAccess.Connect, uri.ToString());
				}
				catch
				{
					return false;
				}
				return permission2.IsSubsetOf(permission);
			}
			return result;
		}

		// Token: 0x06000C60 RID: 3168 RVA: 0x00022014 File Offset: 0x00020214
		internal static bool HasDbPermission(DbProviderFactory factory)
		{
			if (HttpRuntime.NamedPermissionSet == null)
			{
				return true;
			}
			bool result = false;
			CodeAccessPermission codeAccessPermission = factory.CreatePermission(PermissionState.Unrestricted);
			if (codeAccessPermission != null)
			{
				IPermission permission = HttpRuntime.NamedPermissionSet.GetPermission(codeAccessPermission.GetType());
				if (permission != null)
				{
					result = codeAccessPermission.IsSubsetOf(permission);
				}
			}
			return result;
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x00022054 File Offset: 0x00020254
		internal static bool HasPathDiscoveryPermission(string path)
		{
			if (HttpRuntime.TrustLevel == null && HttpRuntime.InitializationException != null)
			{
				return true;
			}
			if (HttpRuntime.NamedPermissionSet == null)
			{
				return true;
			}
			bool result = false;
			IPermission permission = HttpRuntime.NamedPermissionSet.GetPermission(typeof(FileIOPermission));
			if (permission != null)
			{
				IPermission permission2 = new FileIOPermission(FileIOPermissionAccess.PathDiscovery, path);
				result = permission2.IsSubsetOf(permission);
			}
			return result;
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x000220A5 File Offset: 0x000202A5
		internal static bool HasAppPathDiscoveryPermission()
		{
			return HttpRuntime.HasPathDiscoveryPermission(HttpRuntime.AppDomainAppPathInternal);
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x000220B4 File Offset: 0x000202B4
		internal static string GetSafePath(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				return path;
			}
			try
			{
				if (HttpRuntime.HasPathDiscoveryPermission(path))
				{
					return path;
				}
			}
			catch
			{
			}
			return Path.GetFileName(path);
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x000220F8 File Offset: 0x000202F8
		internal static bool HasUnmanagedPermission()
		{
			if (HttpRuntime.NamedPermissionSet == null)
			{
				return true;
			}
			SecurityPermission securityPermission = (SecurityPermission)HttpRuntime.NamedPermissionSet.GetPermission(typeof(SecurityPermission));
			return securityPermission != null && (securityPermission.Flags & SecurityPermissionFlag.UnmanagedCode) > SecurityPermissionFlag.NoFlags;
		}

		// Token: 0x06000C65 RID: 3173 RVA: 0x00022138 File Offset: 0x00020338
		internal static bool HasAspNetHostingPermission(AspNetHostingPermissionLevel level)
		{
			if (HttpRuntime.NamedPermissionSet == null)
			{
				return true;
			}
			AspNetHostingPermission aspNetHostingPermission = (AspNetHostingPermission)HttpRuntime.NamedPermissionSet.GetPermission(typeof(AspNetHostingPermission));
			return aspNetHostingPermission != null && aspNetHostingPermission.Level >= level;
		}

		// Token: 0x06000C66 RID: 3174 RVA: 0x00022179 File Offset: 0x00020379
		internal static void CheckAspNetHostingPermission(AspNetHostingPermissionLevel level, string errorMessageId)
		{
			if (!HttpRuntime.HasAspNetHostingPermission(level))
			{
				throw new HttpException(SR.GetString(errorMessageId));
			}
		}

		// Token: 0x06000C67 RID: 3175 RVA: 0x00022190 File Offset: 0x00020390
		internal static void FailIfNoAPTCABit(Type t, ElementInformation elemInfo, string propertyName)
		{
			if (HttpRuntime.IsTypeAllowedInConfig(t))
			{
				return;
			}
			if (elemInfo != null)
			{
				PropertyInformation propertyInformation = elemInfo.Properties[propertyName];
				throw new ConfigurationErrorsException(SR.GetString("Type_from_untrusted_assembly", new object[]
				{
					t.FullName
				}), propertyInformation.Source, propertyInformation.LineNumber);
			}
			throw new ConfigurationErrorsException(SR.GetString("Type_from_untrusted_assembly", new object[]
			{
				t.FullName
			}));
		}

		// Token: 0x06000C68 RID: 3176 RVA: 0x000221FF File Offset: 0x000203FF
		internal static void FailIfNoAPTCABit(Type t, XmlNode node)
		{
			if (!HttpRuntime.IsTypeAllowedInConfig(t))
			{
				throw new ConfigurationErrorsException(SR.GetString("Type_from_untrusted_assembly", new object[]
				{
					t.FullName
				}), node);
			}
		}

		// Token: 0x06000C69 RID: 3177 RVA: 0x00022229 File Offset: 0x00020429
		private static bool HasAPTCABit(Assembly assembly)
		{
			return assembly.IsDefined(typeof(AllowPartiallyTrustedCallersAttribute), false);
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x0002223C File Offset: 0x0002043C
		internal static bool IsTypeAllowedInConfig(Type t)
		{
			return HttpRuntime.HasAspNetHostingPermission(AspNetHostingPermissionLevel.Unrestricted) || HttpRuntime.IsTypeAccessibleFromPartialTrust(t);
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x00022254 File Offset: 0x00020454
		internal static bool IsTypeAccessibleFromPartialTrust(Type t)
		{
			Assembly assembly = t.Assembly;
			if (assembly.SecurityRuleSet == SecurityRuleSet.Level1)
			{
				return !assembly.IsFullyTrusted || HttpRuntime.HasAPTCABit(assembly);
			}
			return HttpRuntime.HasAPTCABit(assembly) || t.IsSecurityTransparent || t.IsSecuritySafeCritical;
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06000C6C RID: 3180 RVA: 0x0002229C File Offset: 0x0002049C
		internal static FileChangesMonitor FileChangesMonitor
		{
			get
			{
				return HttpRuntime._theRuntime._fcm;
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06000C6D RID: 3181 RVA: 0x000222A8 File Offset: 0x000204A8
		internal static RequestTimeoutManager RequestTimeoutManager
		{
			get
			{
				return HttpRuntime._theRuntime._timeoutManager;
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06000C6E RID: 3182 RVA: 0x000222B4 File Offset: 0x000204B4
		public static Cache Cache
		{
			get
			{
				if (HttpRuntime.AspInstallDirectoryInternal == null)
				{
					throw new HttpException(SR.GetString("Aspnet_not_installed", new object[]
					{
						VersionInfo.SystemWebVersion
					}));
				}
				Cache cache = HttpRuntime._theRuntime._cachePublic;
				if (cache == null)
				{
					HttpRuntime theRuntime = HttpRuntime._theRuntime;
					lock (theRuntime)
					{
						cache = HttpRuntime._theRuntime._cachePublic;
						if (cache == null)
						{
							cache = new Cache(0);
							HttpRuntime._theRuntime._cachePublic = cache;
						}
					}
				}
				return cache;
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06000C6F RID: 3183 RVA: 0x00022344 File Offset: 0x00020544
		public static string AspInstallDirectory
		{
			get
			{
				string aspInstallDirectoryInternal = HttpRuntime.AspInstallDirectoryInternal;
				if (aspInstallDirectoryInternal == null)
				{
					throw new HttpException(SR.GetString("Aspnet_not_installed", new object[]
					{
						VersionInfo.SystemWebVersion
					}));
				}
				InternalSecurityPermissions.PathDiscovery(aspInstallDirectoryInternal).Demand();
				return aspInstallDirectoryInternal;
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06000C70 RID: 3184 RVA: 0x00022384 File Offset: 0x00020584
		internal static string AspInstallDirectoryInternal
		{
			get
			{
				return HttpRuntime.s_installDirectory;
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06000C71 RID: 3185 RVA: 0x0002238C File Offset: 0x0002058C
		public static string AspClientScriptVirtualPath
		{
			get
			{
				if (HttpRuntime._theRuntime._clientScriptVirtualPath == null)
				{
					string systemWebVersion = VersionInfo.SystemWebVersion;
					string clientScriptVirtualPath = "/aspnet_client/system_web/" + systemWebVersion.Substring(0, systemWebVersion.LastIndexOf('.')).Replace('.', '_');
					HttpRuntime._theRuntime._clientScriptVirtualPath = clientScriptVirtualPath;
				}
				return HttpRuntime._theRuntime._clientScriptVirtualPath;
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06000C72 RID: 3186 RVA: 0x000223E4 File Offset: 0x000205E4
		public static string AspClientScriptPhysicalPath
		{
			get
			{
				string aspClientScriptPhysicalPathInternal = HttpRuntime.AspClientScriptPhysicalPathInternal;
				if (aspClientScriptPhysicalPathInternal == null)
				{
					throw new HttpException(SR.GetString("Aspnet_not_installed", new object[]
					{
						VersionInfo.SystemWebVersion
					}));
				}
				return aspClientScriptPhysicalPathInternal;
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06000C73 RID: 3187 RVA: 0x0002241C File Offset: 0x0002061C
		internal static string AspClientScriptPhysicalPathInternal
		{
			get
			{
				if (HttpRuntime._theRuntime._clientScriptPhysicalPath == null)
				{
					string clientScriptPhysicalPath = Path.Combine(HttpRuntime.AspInstallDirectoryInternal, "asp.netclientfiles");
					HttpRuntime._theRuntime._clientScriptPhysicalPath = clientScriptPhysicalPath;
				}
				return HttpRuntime._theRuntime._clientScriptPhysicalPath;
			}
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06000C74 RID: 3188 RVA: 0x0002245C File Offset: 0x0002065C
		public static string ClrInstallDirectory
		{
			get
			{
				string clrInstallDirectoryInternal = HttpRuntime.ClrInstallDirectoryInternal;
				InternalSecurityPermissions.PathDiscovery(clrInstallDirectoryInternal).Demand();
				return clrInstallDirectoryInternal;
			}
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06000C75 RID: 3189 RVA: 0x0002247B File Offset: 0x0002067B
		internal static string ClrInstallDirectoryInternal
		{
			get
			{
				return HttpConfigurationSystem.MsCorLibDirectory;
			}
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06000C76 RID: 3190 RVA: 0x00022484 File Offset: 0x00020684
		public static string MachineConfigurationDirectory
		{
			get
			{
				string machineConfigurationDirectoryInternal = HttpRuntime.MachineConfigurationDirectoryInternal;
				InternalSecurityPermissions.PathDiscovery(machineConfigurationDirectoryInternal).Demand();
				return machineConfigurationDirectoryInternal;
			}
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06000C77 RID: 3191 RVA: 0x000224A3 File Offset: 0x000206A3
		internal static string MachineConfigurationDirectoryInternal
		{
			get
			{
				return HttpConfigurationSystem.MachineConfigurationDirectory;
			}
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06000C78 RID: 3192 RVA: 0x000224AA File Offset: 0x000206AA
		internal static bool IsEngineLoaded
		{
			get
			{
				return HttpRuntime.s_isEngineLoaded;
			}
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06000C79 RID: 3193 RVA: 0x000224B4 File Offset: 0x000206B4
		public static string CodegenDir
		{
			get
			{
				string codegenDirInternal = HttpRuntime.CodegenDirInternal;
				InternalSecurityPermissions.PathDiscovery(codegenDirInternal).Demand();
				return codegenDirInternal;
			}
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06000C7A RID: 3194 RVA: 0x000224D3 File Offset: 0x000206D3
		internal static string CodegenDirInternal
		{
			get
			{
				return HttpRuntime._theRuntime._codegenDir;
			}
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06000C7B RID: 3195 RVA: 0x000224DF File Offset: 0x000206DF
		internal static string TempDirInternal
		{
			get
			{
				return HttpRuntime._theRuntime._tempDir;
			}
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06000C7C RID: 3196 RVA: 0x000224EB File Offset: 0x000206EB
		public static string AppDomainAppId
		{
			get
			{
				return HttpRuntime._theRuntime._appDomainAppId;
			}
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06000C7D RID: 3197 RVA: 0x000224F7 File Offset: 0x000206F7
		internal static bool IsAspNetAppDomain
		{
			get
			{
				return HttpRuntime.AppDomainAppId != null;
			}
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06000C7E RID: 3198 RVA: 0x00022501 File Offset: 0x00020701
		public static string AppDomainAppPath
		{
			get
			{
				InternalSecurityPermissions.AppPathDiscovery.Demand();
				return HttpRuntime.AppDomainAppPathInternal;
			}
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06000C7F RID: 3199 RVA: 0x00022512 File Offset: 0x00020712
		internal static string AppDomainAppPathInternal
		{
			get
			{
				return HttpRuntime._theRuntime._appDomainAppPath;
			}
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06000C80 RID: 3200 RVA: 0x0002251E File Offset: 0x0002071E
		public static string AppDomainAppVirtualPath
		{
			get
			{
				return VirtualPath.GetVirtualPathStringNoTrailingSlash(HttpRuntime._theRuntime._appDomainAppVPath);
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06000C81 RID: 3201 RVA: 0x0002252F File Offset: 0x0002072F
		internal static string AppDomainAppVirtualPathString
		{
			get
			{
				return VirtualPath.GetVirtualPathString(HttpRuntime._theRuntime._appDomainAppVPath);
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06000C82 RID: 3202 RVA: 0x00022540 File Offset: 0x00020740
		internal static VirtualPath AppDomainAppVirtualPathObject
		{
			get
			{
				return HttpRuntime._theRuntime._appDomainAppVPath;
			}
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x0002254C File Offset: 0x0002074C
		internal static bool IsPathWithinAppRoot(string path)
		{
			return HttpRuntime.AppDomainIdInternal == null || UrlPath.IsEqualOrSubpath(HttpRuntime.AppDomainAppVirtualPathString, path);
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06000C84 RID: 3204 RVA: 0x00022562 File Offset: 0x00020762
		public static string AppDomainId
		{
			[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.High)]
			get
			{
				return HttpRuntime.AppDomainIdInternal;
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06000C85 RID: 3205 RVA: 0x00022569 File Offset: 0x00020769
		internal static string AppDomainIdInternal
		{
			get
			{
				return HttpRuntime._theRuntime._appDomainId;
			}
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06000C86 RID: 3206 RVA: 0x00022578 File Offset: 0x00020778
		public static string BinDirectory
		{
			get
			{
				string binDirectoryInternal = HttpRuntime.BinDirectoryInternal;
				InternalSecurityPermissions.PathDiscovery(binDirectoryInternal).Demand();
				return binDirectoryInternal;
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06000C87 RID: 3207 RVA: 0x00022598 File Offset: 0x00020798
		internal static string BinDirectoryInternal
		{
			get
			{
				return Path.Combine(HttpRuntime._theRuntime._appDomainAppPath, "bin") + Path.DirectorySeparatorChar.ToString();
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06000C88 RID: 3208 RVA: 0x000225CB File Offset: 0x000207CB
		internal static VirtualPath CodeDirectoryVirtualPath
		{
			get
			{
				return HttpRuntime._theRuntime._appDomainAppVPath.SimpleCombineWithDir("App_Code");
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06000C89 RID: 3209 RVA: 0x000225E1 File Offset: 0x000207E1
		internal static VirtualPath ResourcesDirectoryVirtualPath
		{
			get
			{
				return HttpRuntime._theRuntime._appDomainAppVPath.SimpleCombineWithDir("App_GlobalResources");
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06000C8A RID: 3210 RVA: 0x000225F7 File Offset: 0x000207F7
		internal static VirtualPath WebRefDirectoryVirtualPath
		{
			get
			{
				return HttpRuntime._theRuntime._appDomainAppVPath.SimpleCombineWithDir("App_WebReferences");
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06000C8B RID: 3211 RVA: 0x0002260D File Offset: 0x0002080D
		public static bool IsOnUNCShare
		{
			[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Low)]
			get
			{
				return HttpRuntime.IsOnUNCShareInternal;
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06000C8C RID: 3212 RVA: 0x00022614 File Offset: 0x00020814
		internal static bool IsOnUNCShareInternal
		{
			get
			{
				return HttpRuntime._theRuntime._isOnUNCShare;
			}
		}

		// Token: 0x06000C8D RID: 3213 RVA: 0x00022620 File Offset: 0x00020820
		private static string GetAppDomainString(string key)
		{
			object data = Thread.GetDomain().GetData(key);
			return data as string;
		}

		// Token: 0x06000C8E RID: 3214 RVA: 0x00022640 File Offset: 0x00020840
		internal static void AddAppDomainTraceMessage(string message)
		{
			AppDomain domain = Thread.GetDomain();
			string text = domain.GetData("ASP.NET Domain Trace") as string;
			domain.SetData("ASP.NET Domain Trace", (text != null) ? (text + " ... " + message) : message);
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x06000C8F RID: 3215 RVA: 0x00022681 File Offset: 0x00020881
		public static Version TargetFramework
		{
			get
			{
				return BinaryCompatibility.Current.TargetFramework;
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06000C90 RID: 3216 RVA: 0x0002268D File Offset: 0x0002088D
		internal static bool DebuggingEnabled
		{
			get
			{
				return HttpRuntime._theRuntime._debuggingEnabled;
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06000C91 RID: 3217 RVA: 0x00022699 File Offset: 0x00020899
		internal static bool ConfigInited
		{
			get
			{
				return HttpRuntime._theRuntime._configInited;
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06000C92 RID: 3218 RVA: 0x000226A5 File Offset: 0x000208A5
		internal static bool FusionInited
		{
			get
			{
				return HttpRuntime._theRuntime._fusionInited;
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06000C93 RID: 3219 RVA: 0x000226B1 File Offset: 0x000208B1
		internal static bool ApartmentThreading
		{
			get
			{
				return HttpRuntime._theRuntime._apartmentThreading;
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06000C94 RID: 3220 RVA: 0x000226BD File Offset: 0x000208BD
		internal static bool ShutdownInProgress
		{
			get
			{
				return HttpRuntime._theRuntime._shutdownInProgress;
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06000C95 RID: 3221 RVA: 0x000226C9 File Offset: 0x000208C9
		internal static string TrustLevel
		{
			get
			{
				return HttpRuntime._theRuntime._trustLevel;
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06000C96 RID: 3222 RVA: 0x000226D5 File Offset: 0x000208D5
		internal static string WpUserId
		{
			get
			{
				return HttpRuntime._theRuntime._wpUserId;
			}
		}

		// Token: 0x06000C97 RID: 3223 RVA: 0x000226E4 File Offset: 0x000208E4
		private void SetTrustLevel(TrustSection trustSection, SecurityPolicySection securityPolicySection)
		{
			string level = trustSection.Level;
			if (trustSection.Level == "Full")
			{
				this._trustLevel = level;
				return;
			}
			if (securityPolicySection == null || securityPolicySection.TrustLevels[trustSection.Level] == null)
			{
				throw new ConfigurationErrorsException(SR.GetString("Unable_to_get_policy_file", new object[]
				{
					trustSection.Level
				}), string.Empty, 0);
			}
			string text = null;
			if (trustSection.Level == "Minimal" || trustSection.Level == "Low" || trustSection.Level == "Medium" || trustSection.Level == "High")
			{
				text = securityPolicySection.TrustLevels[trustSection.Level].LegacyPolicyFileExpanded;
			}
			else
			{
				text = securityPolicySection.TrustLevels[trustSection.Level].PolicyFileExpanded;
			}
			if (text == null || !FileUtil.FileExists(text))
			{
				throw new HttpException(SR.GetString("Unable_to_get_policy_file", new object[]
				{
					trustSection.Level
				}));
			}
			bool flag = false;
			PolicyLevel policyLevel = HttpRuntime.CreatePolicyLevel(text, HttpRuntime.AppDomainAppPathInternal, HttpRuntime.CodegenDirInternal, trustSection.OriginUrl, out flag);
			if (flag)
			{
				CodeGroup rootCodeGroup = policyLevel.RootCodeGroup;
				bool flag2 = false;
				foreach (object obj in rootCodeGroup.Children)
				{
					CodeGroup codeGroup = (CodeGroup)obj;
					if (codeGroup.MembershipCondition is GacMembershipCondition)
					{
						flag2 = true;
						break;
					}
				}
				if (!flag2 && rootCodeGroup is FirstMatchCodeGroup)
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
			AppDomain.CurrentDomain.SetAppDomainPolicy(policyLevel);
			this._namedPermissionSet = policyLevel.GetNamedPermissionSet(trustSection.PermissionSetName);
			this._trustLevel = level;
			this._fcm.StartMonitoringFile(text, new FileChangeEventHandler(this.OnSecurityPolicyFileChange));
		}

		// Token: 0x06000C98 RID: 3224 RVA: 0x000229C4 File Offset: 0x00020BC4
		private static PolicyLevel CreatePolicyLevel(string configFile, string appDir, string binDir, string strOriginUrl, out bool foundGacToken)
		{
			FileStream stream = new FileStream(configFile, FileMode.Open, FileAccess.Read);
			StreamReader streamReader = new StreamReader(stream, Encoding.UTF8);
			string text = streamReader.ReadToEnd();
			streamReader.Close();
			appDir = FileUtil.RemoveTrailingDirectoryBackSlash(appDir);
			binDir = FileUtil.RemoveTrailingDirectoryBackSlash(binDir);
			text = text.Replace("$AppDir$", appDir);
			text = text.Replace("$AppDirUrl$", HttpRuntime.MakeFileUrl(appDir));
			text = text.Replace("$CodeGen$", HttpRuntime.MakeFileUrl(binDir));
			if (strOriginUrl == null)
			{
				strOriginUrl = string.Empty;
			}
			text = text.Replace("$OriginHost$", strOriginUrl);
			int num = text.IndexOf("$Gac$", StringComparison.Ordinal);
			if (num != -1)
			{
				string text2 = HttpRuntime.GetGacLocation();
				if (text2 != null)
				{
					text2 = HttpRuntime.MakeFileUrl(text2);
				}
				if (text2 == null)
				{
					text2 = string.Empty;
				}
				text = text.Replace("$Gac$", text2);
				foundGacToken = true;
			}
			else
			{
				foundGacToken = false;
			}
			return SecurityManager.LoadPolicyLevelFromString(text, PolicyLevelType.AppDomain);
		}

		// Token: 0x06000C99 RID: 3225 RVA: 0x00022A9C File Offset: 0x00020C9C
		private void SetTrustParameters(TrustSection trustSection, SecurityPolicySection securityPolicySection, PolicyLevel policyLevel)
		{
			this._trustLevel = trustSection.Level;
			if (this._trustLevel != "Full")
			{
				this._namedPermissionSet = policyLevel.GetNamedPermissionSet(trustSection.PermissionSetName);
				this._policyLevel = policyLevel;
				this._hostSecurityPolicyResolverType = trustSection.HostSecurityPolicyResolverType;
				string policyFileExpanded = securityPolicySection.TrustLevels[trustSection.Level].PolicyFileExpanded;
				this._fcm.StartMonitoringFile(policyFileExpanded, new FileChangeEventHandler(this.OnSecurityPolicyFileChange));
			}
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x00022B1C File Offset: 0x00020D1C
		private void OnSecurityPolicyFileChange(object sender, FileChangeEvent e)
		{
			string text = FileChangesMonitor.GenerateErrorMessage(e.Action, e.FileName);
			if (text == null)
			{
				text = "Change in code-access security policy file";
			}
			HttpRuntime.ShutdownAppDomain(ApplicationShutdownReason.ChangeInSecurityPolicyFile, text);
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x00022B4C File Offset: 0x00020D4C
		private void OnAppOfflineFileChange(object sender, FileChangeEvent e)
		{
			HttpRuntime.SetUserForcedShutdown();
			string text = FileChangesMonitor.GenerateErrorMessage(e.Action, "App_Offline.htm");
			if (text == null)
			{
				text = "Change in App_Offline.htm";
			}
			HttpRuntime.ShutdownAppDomain(ApplicationShutdownReason.ConfigurationChange, text);
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x00022B80 File Offset: 0x00020D80
		internal static string MakeFileUrl(string path)
		{
			Uri uri = new Uri(path);
			return uri.ToString();
		}

		// Token: 0x06000C9D RID: 3229 RVA: 0x00022B9C File Offset: 0x00020D9C
		internal static string GetGacLocation()
		{
			StringBuilder stringBuilder = new StringBuilder(262);
			int num = 260;
			if (UnsafeNativeMethods.GetCachePath(2, stringBuilder, ref num) >= 0)
			{
				return stringBuilder.ToString();
			}
			throw new HttpException(SR.GetString("GetGacLocaltion_failed"));
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x00022BDC File Offset: 0x00020DDC
		internal static void RestrictIISFolders(HttpContext context)
		{
			HttpWorkerRequest workerRequest = context.WorkerRequest;
			if (workerRequest == null || !(workerRequest is ISAPIWorkerRequest))
			{
				return;
			}
			if (!(workerRequest is ISAPIWorkerRequestInProcForIIS6))
			{
				byte[] bufOut = new byte[1];
				byte[] bytes = BitConverter.GetBytes(1);
				int num = context.CallISAPI(UnsafeNativeMethods.CallISAPIFunc.RestrictIISFolders, bytes, bufOut);
			}
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06000C9F RID: 3231 RVA: 0x00022C20 File Offset: 0x00020E20
		// (set) Token: 0x06000CA0 RID: 3232 RVA: 0x00022C27 File Offset: 0x00020E27
		public static IServiceProvider WebObjectActivator { get; set; }

		// Token: 0x06000CA1 RID: 3233 RVA: 0x00022C30 File Offset: 0x00020E30
		internal static object CreateNonPublicInstanceByWebObjectActivator(Type type)
		{
			IServiceProvider webObjectActivator = HttpRuntime.WebObjectActivator;
			if (webObjectActivator != null)
			{
				return webObjectActivator.GetService(type);
			}
			return HttpRuntime.CreateNonPublicInstance(type, null);
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x00022C58 File Offset: 0x00020E58
		internal static object CreatePublicInstanceByWebObjectActivator(Type type)
		{
			IServiceProvider webObjectActivator = HttpRuntime.WebObjectActivator;
			if (webObjectActivator != null)
			{
				return webObjectActivator.GetService(type);
			}
			return HttpRuntime.CreatePublicInstance(type);
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x00022C7C File Offset: 0x00020E7C
		internal static object CreateNonPublicInstance(Type type)
		{
			return HttpRuntime.CreateNonPublicInstance(type, null);
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x00022C85 File Offset: 0x00020E85
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		internal static object CreateNonPublicInstance(Type type, object[] args)
		{
			return Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, null, args, null);
		}

		// Token: 0x06000CA5 RID: 3237 RVA: 0x00022C95 File Offset: 0x00020E95
		internal static object CreatePublicInstance(Type type)
		{
			return Activator.CreateInstance(type);
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x00022CA0 File Offset: 0x00020EA0
		internal static object FastCreatePublicInstance(Type type)
		{
			if (!type.Assembly.GlobalAssemblyCache)
			{
				return HttpRuntime.CreatePublicInstance(type);
			}
			if (!HttpRuntime.s_initializedFactory)
			{
				object obj = HttpRuntime.s_factoryLock;
				lock (obj)
				{
					if (!HttpRuntime.s_initializedFactory)
					{
						HttpRuntime.s_factoryGenerator = new FactoryGenerator();
						HttpRuntime.s_factoryCache = Hashtable.Synchronized(new Hashtable());
						HttpRuntime.s_initializedFactory = true;
					}
				}
			}
			IWebObjectFactory webObjectFactory = (IWebObjectFactory)HttpRuntime.s_factoryCache[type];
			if (webObjectFactory == null)
			{
				webObjectFactory = HttpRuntime.s_factoryGenerator.CreateFactory(type);
				HttpRuntime.s_factoryCache[type] = webObjectFactory;
			}
			return webObjectFactory.CreateInstance();
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x00022D4C File Offset: 0x00020F4C
		internal static object CreatePublicInstance(Type type, object[] args)
		{
			if (args == null)
			{
				return Activator.CreateInstance(type);
			}
			return Activator.CreateInstance(type, args);
		}

		// Token: 0x06000CA8 RID: 3240 RVA: 0x00022D60 File Offset: 0x00020F60
		private static string GetCurrentUserName()
		{
			string result;
			try
			{
				result = WindowsIdentity.GetCurrent().Name;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000CA9 RID: 3241 RVA: 0x00022D90 File Offset: 0x00020F90
		private void RaiseShutdownWebEventOnce()
		{
			if (!this._shutdownWebEventRaised)
			{
				lock (this)
				{
					if (!this._shutdownWebEventRaised)
					{
						WebBaseEvent.RaiseSystemEvent(this, 1002, WebApplicationLifetimeEvent.DetailCodeFromShutdownReason(HttpRuntime.ShutdownReason));
						this._shutdownWebEventRaised = true;
					}
				}
			}
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x00022DF4 File Offset: 0x00020FF4
		private void RelaxMapPathIfRequired()
		{
			try
			{
				RuntimeConfig appConfig = RuntimeConfig.GetAppConfig();
				if (appConfig != null && appConfig.HttpRuntime != null && appConfig.HttpRuntime.RelaxedUrlToFileSystemMapping)
				{
					HttpRuntime._DefaultPhysicalPathOnMapPathFailure = Path.Combine(this._appDomainAppPath, "NOT_A_VALID_FILESYSTEM_PATH");
				}
			}
			catch
			{
			}
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06000CAB RID: 3243 RVA: 0x00022E4C File Offset: 0x0002104C
		internal static bool IsMapPathRelaxed
		{
			get
			{
				return HttpRuntime._DefaultPhysicalPathOnMapPathFailure != null;
			}
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x00022E58 File Offset: 0x00021058
		internal static string GetRelaxedMapPathResult(string originalResult)
		{
			if (!HttpRuntime.IsMapPathRelaxed)
			{
				return originalResult;
			}
			if (originalResult == null)
			{
				return HttpRuntime._DefaultPhysicalPathOnMapPathFailure;
			}
			if (originalResult.IndexOfAny(HttpRuntime.s_InvalidPhysicalPathChars) >= 0)
			{
				return HttpRuntime._DefaultPhysicalPathOnMapPathFailure;
			}
			try
			{
				bool flag;
				if (FileUtil.IsSuspiciousPhysicalPath(originalResult, out flag) || flag)
				{
					return HttpRuntime._DefaultPhysicalPathOnMapPathFailure;
				}
			}
			catch
			{
				return HttpRuntime._DefaultPhysicalPathOnMapPathFailure;
			}
			return originalResult;
		}

		// Token: 0x04000474 RID: 1140
		internal const string codegenDirName = "Temporary ASP.NET Files";

		// Token: 0x04000475 RID: 1141
		internal const string profileFileName = "profileoptimization.prof";

		// Token: 0x04000476 RID: 1142
		private static HttpRuntime _theRuntime;

		// Token: 0x04000477 RID: 1143
		internal static byte[] s_autogenKeys = new byte[1024];

		// Token: 0x04000478 RID: 1144
		internal const string BinDirectoryName = "bin";

		// Token: 0x04000479 RID: 1145
		internal const string CodeDirectoryName = "App_Code";

		// Token: 0x0400047A RID: 1146
		internal const string WebRefDirectoryName = "App_WebReferences";

		// Token: 0x0400047B RID: 1147
		internal const string ResourcesDirectoryName = "App_GlobalResources";

		// Token: 0x0400047C RID: 1148
		internal const string LocalResourcesDirectoryName = "App_LocalResources";

		// Token: 0x0400047D RID: 1149
		internal const string DataDirectoryName = "App_Data";

		// Token: 0x0400047E RID: 1150
		internal const string ThemesDirectoryName = "App_Themes";

		// Token: 0x0400047F RID: 1151
		internal const string GlobalThemesDirectoryName = "Themes";

		// Token: 0x04000480 RID: 1152
		internal const string BrowsersDirectoryName = "App_Browsers";

		// Token: 0x04000481 RID: 1153
		private static string DirectorySeparatorString = new string(Path.DirectorySeparatorChar, 1);

		// Token: 0x04000482 RID: 1154
		private static string DoubleDirectorySeparatorString = new string(Path.DirectorySeparatorChar, 2);

		// Token: 0x04000483 RID: 1155
		private static char[] s_InvalidPhysicalPathChars = new char[]
		{
			'/',
			'?',
			'*',
			'<',
			'>',
			'|',
			'"'
		};

		// Token: 0x04000484 RID: 1156
		private static bool s_initialized = false;

		// Token: 0x04000485 RID: 1157
		private static string s_installDirectory;

		// Token: 0x04000486 RID: 1158
		private static bool s_isEngineLoaded = false;

		// Token: 0x04000487 RID: 1159
		private NamedPermissionSet _namedPermissionSet;

		// Token: 0x04000488 RID: 1160
		private PolicyLevel _policyLevel;

		// Token: 0x04000489 RID: 1161
		private string _hostSecurityPolicyResolverType;

		// Token: 0x0400048A RID: 1162
		private FileChangesMonitor _fcm;

		// Token: 0x0400048B RID: 1163
		private Cache _cachePublic;

		// Token: 0x0400048C RID: 1164
		private bool _isOnUNCShare;

		// Token: 0x0400048D RID: 1165
		private Profiler _profiler;

		// Token: 0x0400048E RID: 1166
		private RequestTimeoutManager _timeoutManager;

		// Token: 0x0400048F RID: 1167
		private RequestQueue _requestQueue;

		// Token: 0x04000490 RID: 1168
		private bool _apartmentThreading;

		// Token: 0x04000491 RID: 1169
		private bool _processRequestInApplicationTrust;

		// Token: 0x04000492 RID: 1170
		private bool _disableProcessRequestInApplicationTrust;

		// Token: 0x04000493 RID: 1171
		private bool _isLegacyCas;

		// Token: 0x04000494 RID: 1172
		private bool _beforeFirstRequest = true;

		// Token: 0x04000495 RID: 1173
		private DateTime _firstRequestStartTime;

		// Token: 0x04000496 RID: 1174
		private bool _firstRequestCompleted;

		// Token: 0x04000497 RID: 1175
		private bool _userForcedShutdown;

		// Token: 0x04000498 RID: 1176
		private bool _configInited;

		// Token: 0x04000499 RID: 1177
		private bool _fusionInited;

		// Token: 0x0400049A RID: 1178
		private int _activeRequestCount;

		// Token: 0x0400049B RID: 1179
		private volatile bool _disposingHttpRuntime;

		// Token: 0x0400049C RID: 1180
		private DateTime _lastShutdownAttemptTime;

		// Token: 0x0400049D RID: 1181
		private bool _shutdownInProgress;

		// Token: 0x0400049E RID: 1182
		private string _shutDownStack;

		// Token: 0x0400049F RID: 1183
		private string _shutDownMessage;

		// Token: 0x040004A0 RID: 1184
		private ApplicationShutdownReason _shutdownReason;

		// Token: 0x040004A1 RID: 1185
		private string _trustLevel;

		// Token: 0x040004A2 RID: 1186
		private string _wpUserId;

		// Token: 0x040004A3 RID: 1187
		private bool _shutdownWebEventRaised;

		// Token: 0x040004A4 RID: 1188
		private bool _enableHeaderChecking;

		// Token: 0x040004A5 RID: 1189
		private AsyncCallback _requestNotificationCompletionCallback;

		// Token: 0x040004A6 RID: 1190
		private AsyncCallback _handlerCompletionCallback;

		// Token: 0x040004A7 RID: 1191
		private HttpWorkerRequest.EndOfSendNotification _asyncEndOfSendCallback;

		// Token: 0x040004A8 RID: 1192
		private WaitCallback _appDomainUnloadallback;

		// Token: 0x040004A9 RID: 1193
		private Exception _initializationError;

		// Token: 0x040004AA RID: 1194
		private bool _hostingInitFailed;

		// Token: 0x040004AB RID: 1195
		private Timer _appDomainShutdownTimer;

		// Token: 0x040004AC RID: 1196
		private string _tempDir;

		// Token: 0x040004AD RID: 1197
		private string _codegenDir;

		// Token: 0x040004AE RID: 1198
		private string _appDomainAppId;

		// Token: 0x040004AF RID: 1199
		private string _appDomainAppPath;

		// Token: 0x040004B0 RID: 1200
		private VirtualPath _appDomainAppVPath;

		// Token: 0x040004B1 RID: 1201
		private string _appDomainId;

		// Token: 0x040004B2 RID: 1202
		private bool _debuggingEnabled;

		// Token: 0x040004B3 RID: 1203
		private const string AppOfflineFileName = "App_Offline.htm";

		// Token: 0x040004B4 RID: 1204
		private const long MaxAppOfflineFileLength = 1048576L;

		// Token: 0x040004B5 RID: 1205
		private byte[] _appOfflineMessage;

		// Token: 0x040004B6 RID: 1206
		private const string AspNetClientFilesSubDirectory = "asp.netclientfiles";

		// Token: 0x040004B7 RID: 1207
		private const string AspNetClientFilesParentVirtualPath = "/aspnet_client/system_web/";

		// Token: 0x040004B8 RID: 1208
		private string _clientScriptVirtualPath;

		// Token: 0x040004B9 RID: 1209
		private string _clientScriptPhysicalPath;

		// Token: 0x040004BA RID: 1210
		private static Version _iisVersion;

		// Token: 0x040004BB RID: 1211
		private static bool _useIntegratedPipeline;

		// Token: 0x040004BC RID: 1212
		private static bool _enablePrefetchOptimization;

		// Token: 0x040004BF RID: 1215
		private static FactoryGenerator s_factoryGenerator;

		// Token: 0x040004C0 RID: 1216
		private static Hashtable s_factoryCache;

		// Token: 0x040004C1 RID: 1217
		private static bool s_initializedFactory;

		// Token: 0x040004C2 RID: 1218
		private static object s_factoryLock = new object();

		// Token: 0x040004C3 RID: 1219
		private static string _DefaultPhysicalPathOnMapPathFailure;
	}
}
