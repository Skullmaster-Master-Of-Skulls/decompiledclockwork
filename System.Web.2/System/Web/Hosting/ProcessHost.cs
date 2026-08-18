using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Security.Permissions;
using System.Threading;
using System.Web.Configuration;
using System.Web.Util;

namespace System.Web.Hosting
{
	// Token: 0x020007E2 RID: 2018
	public sealed class ProcessHost : MarshalByRefObject, IProcessHost, IProcessHostLite, ICustomRuntimeManager, IAdphManager, IPphManager, IProcessHostIdleAndHealthCheck, IProcessSuspendListener, IApplicationPreloadManager
	{
		// Token: 0x17001B84 RID: 7044
		// (get) Token: 0x06006059 RID: 24665 RVA: 0x0014C7FC File Offset: 0x0014A9FC
		private ProtocolsSection ProtocolsConfig
		{
			get
			{
				if (this._protocolsConfig == null)
				{
					lock (this)
					{
						if (this._protocolsConfig == null)
						{
							if (HttpConfigurationSystem.IsSet)
							{
								this._protocolsConfig = RuntimeConfig.GetRootWebConfig().Protocols;
							}
							else
							{
								Configuration configuration = WebConfigurationManager.OpenWebConfiguration(null);
								this._protocolsConfig = (ProtocolsSection)configuration.GetSection("system.web/protocols");
							}
						}
					}
				}
				return this._protocolsConfig;
			}
		}

		// Token: 0x0600605A RID: 24666 RVA: 0x0014C880 File Offset: 0x0014AA80
		private ProcessHost(IProcessHostSupportFunctions functions)
		{
			try
			{
				this._functions = functions;
				HostingEnvironment.SupportFunctions = functions;
				this._appManager = ApplicationManager.GetApplicationManager();
				int num = (int)Misc.GetAspNetRegValue(null, "MaxPreloadConcurrency", 0);
				if (num > 0)
				{
					this._preloadingThrottle = new Semaphore(num, num);
				}
			}
			catch (Exception e)
			{
				using (new ProcessImpersonationContext())
				{
					Misc.ReportUnhandledException(e, new string[]
					{
						SR.GetString("Cant_Create_Process_Host")
					});
				}
				throw;
			}
		}

		// Token: 0x0600605B RID: 24667 RVA: 0x0014C938 File Offset: 0x0014AB38
		private Type ValidateAndGetType(ProtocolElement element, string typeName, Type assignableType, string elementPropertyName)
		{
			Type type;
			try
			{
				type = Type.GetType(typeName, true);
			}
			catch (Exception ex)
			{
				string filename = string.Empty;
				int line = 0;
				if (element != null && element.ElementInformation != null)
				{
					PropertyInformation propertyInformation = element.ElementInformation.Properties[elementPropertyName];
					if (propertyInformation != null)
					{
						filename = propertyInformation.Source;
						line = propertyInformation.LineNumber;
					}
				}
				throw new ConfigurationErrorsException(ex.Message, ex, filename, line);
			}
			ConfigUtil.CheckAssignableType(assignableType, type, element, elementPropertyName);
			return type;
		}

		// Token: 0x0600605C RID: 24668 RVA: 0x0014C9B8 File Offset: 0x0014ABB8
		private Type GetAppDomainProtocolHandlerType(string protocolId)
		{
			Type result = null;
			try
			{
				ProtocolElement protocolElement = this.ProtocolsConfig.Protocols[protocolId];
				if (protocolElement == null)
				{
					throw new ArgumentException(SR.GetString("Unknown_protocol_id", new object[]
					{
						protocolId
					}));
				}
				result = this.ValidateAndGetType(protocolElement, protocolElement.AppDomainHandlerType, typeof(AppDomainProtocolHandler), "AppDomainHandlerType");
			}
			catch (Exception e)
			{
				using (new ProcessImpersonationContext())
				{
					Misc.ReportUnhandledException(e, new string[]
					{
						SR.GetString("Invalid_AppDomain_Prot_Type")
					});
				}
			}
			return result;
		}

		// Token: 0x0600605D RID: 24669 RVA: 0x0000298D File Offset: 0x00000B8D
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		public override object InitializeLifetimeService()
		{
			return null;
		}

		// Token: 0x0600605E RID: 24670 RVA: 0x0014CA60 File Offset: 0x0014AC60
		internal static ProcessHost GetProcessHost(IProcessHostSupportFunctions functions)
		{
			if (ProcessHost._theProcessHost == null)
			{
				object processHostStaticLock = ProcessHost._processHostStaticLock;
				lock (processHostStaticLock)
				{
					if (ProcessHost._theProcessHost == null)
					{
						ProcessHost._theProcessHost = new ProcessHost(functions);
					}
				}
			}
			return ProcessHost._theProcessHost;
		}

		// Token: 0x17001B85 RID: 7045
		// (get) Token: 0x0600605F RID: 24671 RVA: 0x0014CAB8 File Offset: 0x0014ACB8
		internal static ProcessHost DefaultHost
		{
			get
			{
				return ProcessHost._theProcessHost;
			}
		}

		// Token: 0x17001B86 RID: 7046
		// (get) Token: 0x06006060 RID: 24672 RVA: 0x0014CABF File Offset: 0x0014ACBF
		internal IProcessHostSupportFunctions SupportFunctions
		{
			get
			{
				return this._functions;
			}
		}

		// Token: 0x06006061 RID: 24673 RVA: 0x0014CAC8 File Offset: 0x0014ACC8
		public void StartProcessProtocolListenerChannel(string protocolId, IListenerChannelCallback listenerChannelCallback)
		{
			try
			{
				if (protocolId == null)
				{
					throw new ArgumentNullException("protocolId");
				}
				ProtocolElement protocolElement = this.ProtocolsConfig.Protocols[protocolId];
				if (protocolElement == null)
				{
					throw new ArgumentException(SR.GetString("Unknown_protocol_id", new object[]
					{
						protocolId
					}));
				}
				ProcessProtocolHandler processProtocolHandler = null;
				Type type = this.ValidateAndGetType(protocolElement, protocolElement.ProcessHandlerType, typeof(ProcessProtocolHandler), "ProcessHandlerType");
				lock (this)
				{
					processProtocolHandler = (this._protocolHandlers[protocolId] as ProcessProtocolHandler);
					if (processProtocolHandler == null)
					{
						processProtocolHandler = (ProcessProtocolHandler)Activator.CreateInstance(type);
						this._protocolHandlers[protocolId] = processProtocolHandler;
					}
				}
				if (processProtocolHandler != null)
				{
					processProtocolHandler.StartListenerChannel(listenerChannelCallback, this);
				}
			}
			catch (Exception e)
			{
				using (new ProcessImpersonationContext())
				{
					Misc.ReportUnhandledException(e, new string[]
					{
						SR.GetString("Invalid_Process_Prot_Type")
					});
				}
				throw;
			}
		}

		// Token: 0x06006062 RID: 24674 RVA: 0x0014CBE4 File Offset: 0x0014ADE4
		public void StopProcessProtocolListenerChannel(string protocolId, int listenerChannelId, bool immediate)
		{
			try
			{
				if (protocolId == null)
				{
					throw new ArgumentNullException("protocolId");
				}
				ProcessProtocolHandler processProtocolHandler = null;
				lock (this)
				{
					processProtocolHandler = (this._protocolHandlers[protocolId] as ProcessProtocolHandler);
				}
				if (processProtocolHandler != null)
				{
					processProtocolHandler.StopListenerChannel(listenerChannelId, immediate);
				}
			}
			catch (Exception e)
			{
				using (new ProcessImpersonationContext())
				{
					Misc.ReportUnhandledException(e, new string[]
					{
						SR.GetString("Failure_Stop_Listener_Channel")
					});
				}
				throw;
			}
		}

		// Token: 0x06006063 RID: 24675 RVA: 0x0014CC94 File Offset: 0x0014AE94
		public void StopProcessProtocol(string protocolId, bool immediate)
		{
			try
			{
				if (protocolId == null)
				{
					throw new ArgumentNullException("protocolId");
				}
				ProcessProtocolHandler processProtocolHandler = null;
				lock (this)
				{
					processProtocolHandler = (this._protocolHandlers[protocolId] as ProcessProtocolHandler);
					if (processProtocolHandler != null)
					{
						this._protocolHandlers.Remove(protocolId);
					}
				}
				if (processProtocolHandler != null)
				{
					processProtocolHandler.StopProtocol(immediate);
				}
			}
			catch (Exception e)
			{
				using (new ProcessImpersonationContext())
				{
					Misc.ReportUnhandledException(e, new string[]
					{
						SR.GetString("Failure_Stop_Process_Prot")
					});
				}
				throw;
			}
		}

		// Token: 0x06006064 RID: 24676 RVA: 0x0014CD50 File Offset: 0x0014AF50
		public void StartAppDomainProtocolListenerChannel(string appId, string protocolId, IListenerChannelCallback listenerChannelCallback)
		{
			try
			{
				if (appId == null)
				{
					throw new ArgumentNullException("appId");
				}
				if (protocolId == null)
				{
					throw new ArgumentNullException("protocolId");
				}
				ISAPIApplicationHost isapiapplicationHost = this.CreateAppHost(appId, null);
				Type appDomainProtocolHandlerType = this.GetAppDomainProtocolHandlerType(protocolId);
				LockableAppDomainContext lockableAppDomainContext = this._appManager.GetLockableAppDomainContext(appId);
				LockableAppDomainContext obj = lockableAppDomainContext;
				lock (obj)
				{
					HostingEnvironmentParameters hostingEnvironmentParameters = new HostingEnvironmentParameters();
					hostingEnvironmentParameters.HostingFlags = HostingEnvironmentFlags.ThrowHostingInitErrors;
					this.PreloadApplicationIfRequired(appId, isapiapplicationHost, hostingEnvironmentParameters, lockableAppDomainContext);
					AppDomainProtocolHandler handler = (AppDomainProtocolHandler)this._appManager.CreateObjectInternal(appId, appDomainProtocolHandlerType, isapiapplicationHost, false, hostingEnvironmentParameters);
					ListenerAdapterDispatchShim listenerAdapterDispatchShim = (ListenerAdapterDispatchShim)this._appManager.CreateObjectInternal(appId, typeof(ListenerAdapterDispatchShim), isapiapplicationHost, false, hostingEnvironmentParameters);
					if (listenerAdapterDispatchShim == null)
					{
						throw new HttpException(SR.GetString("Failure_Create_Listener_Shim"));
					}
					listenerAdapterDispatchShim.StartListenerChannel(handler, listenerChannelCallback);
					((IRegisteredObject)listenerAdapterDispatchShim).Stop(true);
				}
			}
			catch (Exception e)
			{
				using (new ProcessImpersonationContext())
				{
					Misc.ReportUnhandledException(e, new string[]
					{
						SR.GetString("Failure_Start_AppDomain_Listener")
					});
				}
				throw;
			}
		}

		// Token: 0x06006065 RID: 24677 RVA: 0x0014CE8C File Offset: 0x0014B08C
		public void StopAppDomainProtocolListenerChannel(string appId, string protocolId, int listenerChannelId, bool immediate)
		{
			try
			{
				if (appId == null)
				{
					throw new ArgumentNullException("appId");
				}
				if (protocolId == null)
				{
					throw new ArgumentNullException("protocolId");
				}
				Type appDomainProtocolHandlerType = this.GetAppDomainProtocolHandlerType(protocolId);
				AppDomainProtocolHandler appDomainProtocolHandler = null;
				LockableAppDomainContext lockableAppDomainContext = this._appManager.GetLockableAppDomainContext(appId);
				LockableAppDomainContext obj = lockableAppDomainContext;
				lock (obj)
				{
					appDomainProtocolHandler = (AppDomainProtocolHandler)this._appManager.GetObject(appId, appDomainProtocolHandlerType);
				}
				if (appDomainProtocolHandler != null)
				{
					appDomainProtocolHandler.StopListenerChannel(listenerChannelId, immediate);
				}
			}
			catch (Exception e)
			{
				using (new ProcessImpersonationContext())
				{
					Misc.ReportUnhandledException(e, new string[]
					{
						SR.GetString("Failure_Stop_AppDomain_Listener")
					});
				}
				throw;
			}
		}

		// Token: 0x06006066 RID: 24678 RVA: 0x0014CF64 File Offset: 0x0014B164
		public void StopAppDomainProtocol(string appId, string protocolId, bool immediate)
		{
			try
			{
				if (appId == null)
				{
					throw new ArgumentNullException("appId");
				}
				if (protocolId == null)
				{
					throw new ArgumentNullException("protocolId");
				}
				Type appDomainProtocolHandlerType = this.GetAppDomainProtocolHandlerType(protocolId);
				AppDomainProtocolHandler appDomainProtocolHandler = null;
				LockableAppDomainContext lockableAppDomainContext = this._appManager.GetLockableAppDomainContext(appId);
				LockableAppDomainContext obj = lockableAppDomainContext;
				lock (obj)
				{
					appDomainProtocolHandler = (AppDomainProtocolHandler)this._appManager.GetObject(appId, appDomainProtocolHandlerType);
				}
				if (appDomainProtocolHandler != null)
				{
					appDomainProtocolHandler.StopProtocol(immediate);
				}
			}
			catch (Exception e)
			{
				using (new ProcessImpersonationContext())
				{
					Misc.ReportUnhandledException(e, new string[]
					{
						SR.GetString("Failure_Stop_AppDomain_Protocol")
					});
				}
				throw;
			}
		}

		// Token: 0x06006067 RID: 24679 RVA: 0x0014D038 File Offset: 0x0014B238
		public void StartApplication(string appId, string appPath, out object runtimeInterface)
		{
			try
			{
				if (appId == null)
				{
					throw new ArgumentNullException("appId");
				}
				if (appPath == null)
				{
					throw new ArgumentNullException("appPath");
				}
				runtimeInterface = null;
				PipelineRuntime pipelineRuntime = null;
				if (appPath[0] == '.')
				{
					FileInfo fileInfo = new FileInfo(appPath);
					appPath = fileInfo.FullName;
				}
				if (!StringUtil.StringEndsWith(appPath, '\\'))
				{
					appPath += "\\";
				}
				IApplicationHost applicationHost = this.CreateAppHost(appId, appPath);
				LockableAppDomainContext lockableAppDomainContext = this._appManager.GetLockableAppDomainContext(appId);
				LockableAppDomainContext obj = lockableAppDomainContext;
				lock (obj)
				{
					this._appManager.RemoveFromTableIfRuntimeExists(appId, typeof(PipelineRuntime));
					this.PreloadApplicationIfRequired(appId, applicationHost, null, lockableAppDomainContext);
					try
					{
						pipelineRuntime = (PipelineRuntime)this._appManager.CreateObjectInternal(appId, typeof(PipelineRuntime), applicationHost, true, null);
					}
					catch (AppDomainUnloadedException)
					{
					}
					if (pipelineRuntime != null)
					{
						pipelineRuntime.SetThisAppDomainsIsapiAppId(appId);
						pipelineRuntime.StartProcessing();
						runtimeInterface = new ObjectHandle(pipelineRuntime);
					}
				}
			}
			catch (Exception e)
			{
				using (new ProcessImpersonationContext())
				{
					Misc.ReportUnhandledException(e, new string[]
					{
						SR.GetString("Failure_Start_Integrated_App")
					});
				}
				throw;
			}
		}

		// Token: 0x06006068 RID: 24680 RVA: 0x0014D190 File Offset: 0x0014B390
		public void ShutdownApplication(string appId)
		{
			try
			{
				this._appManager.ShutdownApplication(appId);
			}
			catch (Exception e)
			{
				using (new ProcessImpersonationContext())
				{
					Misc.ReportUnhandledException(e, new string[]
					{
						SR.GetString("Failure_Stop_Integrated_App")
					});
				}
				throw;
			}
		}

		// Token: 0x06006069 RID: 24681 RVA: 0x0014D1F8 File Offset: 0x0014B3F8
		public void Shutdown()
		{
			try
			{
				ArrayList arrayList = new ArrayList();
				lock (this)
				{
					foreach (object obj in this._protocolHandlers)
					{
						arrayList.Add(((DictionaryEntry)obj).Value);
					}
					this._protocolHandlers = new Hashtable();
				}
				foreach (object obj2 in arrayList)
				{
					ProcessProtocolHandler processProtocolHandler = (ProcessProtocolHandler)obj2;
					processProtocolHandler.StopProtocol(true);
				}
				this._appManager.ShutdownAll();
				while (Marshal.ReleaseComObject(this._functions) != 0)
				{
				}
			}
			catch (Exception ex)
			{
				using (new ProcessImpersonationContext())
				{
					Misc.ReportUnhandledException(ex, new string[]
					{
						SR.GetString("Failure_Shutdown_ProcessHost"),
						ex.ToString()
					});
				}
				throw;
			}
		}

		// Token: 0x0600606A RID: 24682 RVA: 0x0014D354 File Offset: 0x0014B554
		IProcessResumeCallback IProcessSuspendListener.Suspend()
		{
			object resumeState = this._appManager.SuspendAllApplications();
			Action customRuntimeResumeCallback = this._customRuntimeManager.SuspendAllCustomRuntimes();
			IProcessResumeCallback result = new ProcessHost.SimpleProcessResumeCallbackDispatcher(delegate()
			{
				this._appManager.ResumeAllApplications(resumeState);
				if (customRuntimeResumeCallback != null)
				{
					customRuntimeResumeCallback();
				}
			});
			GC.Collect();
			return result;
		}

		// Token: 0x0600606B RID: 24683 RVA: 0x0014D3A8 File Offset: 0x0014B5A8
		ICustomRuntimeRegistrationToken ICustomRuntimeManager.Register(ICustomRuntime customRuntime)
		{
			return this._customRuntimeManager.Register(customRuntime);
		}

		// Token: 0x0600606C RID: 24684 RVA: 0x0014D3B8 File Offset: 0x0014B5B8
		public void EnumerateAppDomains(out IAppDomainInfoEnum appDomainInfoEnum)
		{
			try
			{
				ApplicationManager applicationManager = ApplicationManager.GetApplicationManager();
				AppDomainInfo[] appDomainInfos = applicationManager.GetAppDomainInfos();
				appDomainInfoEnum = new AppDomainInfoEnum(appDomainInfos);
			}
			catch (Exception e)
			{
				using (new ProcessImpersonationContext())
				{
					Misc.ReportUnhandledException(e, new string[]
					{
						SR.GetString("Failure_AppDomain_Enum")
					});
				}
				throw;
			}
		}

		// Token: 0x0600606D RID: 24685 RVA: 0x0014D428 File Offset: 0x0014B628
		public bool IsIdle()
		{
			bool result = false;
			try
			{
				result = this._appManager.IsIdle();
			}
			catch (Exception e)
			{
				using (new ProcessImpersonationContext())
				{
					Misc.ReportUnhandledException(e, new string[]
					{
						SR.GetString("Failure_PMH_Idle")
					});
				}
				throw;
			}
			return result;
		}

		// Token: 0x0600606E RID: 24686 RVA: 0x0014D490 File Offset: 0x0014B690
		public void Ping(IProcessPingCallback callback)
		{
			try
			{
				if (callback != null)
				{
					this._appManager.Ping(callback);
				}
			}
			catch (Exception e)
			{
				using (new ProcessImpersonationContext())
				{
					Misc.ReportUnhandledException(e, new string[]
					{
						SR.GetString("Failure_PMH_Ping")
					});
				}
				throw;
			}
		}

		// Token: 0x0600606F RID: 24687 RVA: 0x0014D4F8 File Offset: 0x0014B6F8
		private ISAPIApplicationHost CreateAppHost(string appId, string appPath)
		{
			if (string.IsNullOrEmpty(appPath))
			{
				string text;
				string text2;
				string text3;
				string text4;
				this._functions.GetApplicationProperties(appId, out text, out text2, out text3, out text4);
				if (!StringUtil.StringEndsWith(text2, '\\'))
				{
					text2 += "\\";
				}
				appPath = text2;
			}
			return new ISAPIApplicationHost(appId, appPath, false, this._functions, null);
		}

		// Token: 0x06006070 RID: 24688 RVA: 0x0014D54B File Offset: 0x0014B74B
		public void SetApplicationPreloadUtil(IApplicationPreloadUtil applicationPreloadUtil)
		{
			if (this._preloadUtil != null)
			{
				throw new InvalidOperationException(SR.GetString("Failure_ApplicationPreloadUtil_Already_Set"));
			}
			this._preloadUtil = applicationPreloadUtil;
		}

		// Token: 0x06006071 RID: 24689 RVA: 0x0014D56C File Offset: 0x0014B76C
		public void SetApplicationPreloadState(string context, string appId, bool enabled)
		{
			if (string.IsNullOrEmpty(context))
			{
				throw ExceptionUtil.ParameterNullOrEmpty("context");
			}
			if (string.IsNullOrEmpty(appId))
			{
				throw ExceptionUtil.ParameterNullOrEmpty("appId");
			}
			if (enabled && this._preloadUtil == null)
			{
				throw new ArgumentException(SR.GetString("Invalid_Enabled_Preload_Parameter"), "enabled");
			}
			LockableAppDomainContext lockableAppDomainContext = this._appManager.GetLockableAppDomainContext(appId);
			LockableAppDomainContext obj = lockableAppDomainContext;
			lock (obj)
			{
				lockableAppDomainContext.PreloadContext = context;
				if (enabled)
				{
					this.PreloadApplicationIfRequired(appId, null, null, lockableAppDomainContext);
				}
			}
		}

		// Token: 0x06006072 RID: 24690 RVA: 0x0014D608 File Offset: 0x0014B808
		internal static void PreloadApplicationIfNotShuttingdown(string appId, LockableAppDomainContext ac)
		{
			if (ProcessHost.DefaultHost != null && !UnsafeIISMethods.MgdIsAppPoolShuttingDown())
			{
				ThreadPool.QueueUserWorkItem(delegate(object o)
				{
					LockableAppDomainContext ac2 = ac;
					lock (ac2)
					{
						try
						{
							ProcessHost.DefaultHost.PreloadApplicationIfRequired(appId, null, null, ac);
						}
						catch (Exception e)
						{
							ProcessHost.DefaultHost.ReportApplicationPreloadFailureWithAssert(ac.PreloadContext, -2147467259, Misc.FormatExceptionMessage(e, new string[]
							{
								SR.GetString("Failure_Preload_Application_Initialization")
							}));
						}
					}
				});
			}
		}

		// Token: 0x06006073 RID: 24691 RVA: 0x0014D64C File Offset: 0x0014B84C
		internal void PreloadApplicationIfRequired(string appId, IApplicationHost appHostParameter, HostingEnvironmentParameters hostingParameters, LockableAppDomainContext ac)
		{
			if (this._preloadUtil == null || ac.PreloadContext == null || ac.HostEnv != null)
			{
				return;
			}
			bool flag;
			string text;
			string[] paramsForStartupObj;
			this.GetApplicationPreloadInfoWithAssert(ac.PreloadContext, out flag, out text, out paramsForStartupObj);
			if (!flag || string.IsNullOrEmpty(text))
			{
				return;
			}
			if (this._preloadingThrottle != null)
			{
				this._preloadingThrottle.WaitOne();
			}
			try
			{
				IApplicationHost applicationHost;
				if (appHostParameter != null)
				{
					applicationHost = appHostParameter;
				}
				else
				{
					IApplicationHost applicationHost2 = this.CreateAppHost(appId, null);
					applicationHost = applicationHost2;
				}
				IApplicationHost appHost = applicationHost;
				PreloadHost preloadHost = (PreloadHost)this._appManager.CreateObjectInternal(appId, typeof(PreloadHost), appHost, true, hostingParameters);
				Exception initializationException = preloadHost.InitializationException;
				if (ProcessHost.GetInnerMostException(initializationException) is IOException)
				{
					try
					{
						ac.RetryingPreload = true;
						ac.HostEnv.InitiateShutdownInternal();
					}
					finally
					{
						ac.RetryingPreload = false;
					}
					IApplicationHost applicationHost3;
					if (appHostParameter != null)
					{
						applicationHost3 = appHostParameter;
					}
					else
					{
						IApplicationHost applicationHost2 = this.CreateAppHost(appId, null);
						applicationHost3 = applicationHost2;
					}
					appHost = applicationHost3;
					preloadHost = (PreloadHost)this._appManager.CreateObjectInternal(appId, typeof(PreloadHost), appHost, true, hostingParameters);
					initializationException = preloadHost.InitializationException;
				}
				if (initializationException != null)
				{
					this.ReportApplicationPreloadFailureWithAssert(ac.PreloadContext, -2147467259, Misc.FormatExceptionMessage(initializationException, new string[]
					{
						SR.GetString("Failure_Preload_Application_Initialization")
					}));
					throw initializationException;
				}
				try
				{
					preloadHost.CreateIProcessHostPreloadClientInstanceAndCallPreload(text, paramsForStartupObj);
				}
				catch (Exception e)
				{
					this.ReportApplicationPreloadFailureWithAssert(ac.PreloadContext, -2147467259, Misc.FormatExceptionMessage(e, new string[]
					{
						SR.GetString("Failure_Calling_Preload_Provider")
					}).ToString());
					throw;
				}
			}
			finally
			{
				if (this._preloadingThrottle != null)
				{
					this._preloadingThrottle.Release();
				}
			}
		}

		// Token: 0x06006074 RID: 24692 RVA: 0x0014D824 File Offset: 0x0014BA24
		private static Exception GetInnerMostException(Exception e)
		{
			if (e == null)
			{
				return null;
			}
			while (e.InnerException != null)
			{
				e = e.InnerException;
			}
			return e;
		}

		// Token: 0x06006075 RID: 24693 RVA: 0x0014D83C File Offset: 0x0014BA3C
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		private void GetApplicationPreloadInfoWithAssert(string context, out bool enabled, out string startupObjType, out string[] parametersForStartupObj)
		{
			this._preloadUtil.GetApplicationPreloadInfo(context, out enabled, out startupObjType, out parametersForStartupObj);
		}

		// Token: 0x06006076 RID: 24694 RVA: 0x0014D84E File Offset: 0x0014BA4E
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		private void ReportApplicationPreloadFailureWithAssert(string context, int errorCode, string errorMessage)
		{
			this._preloadUtil.ReportApplicationPreloadFailure(context, errorCode, errorMessage);
		}

		// Token: 0x06006077 RID: 24695 RVA: 0x0014D860 File Offset: 0x0014BA60
		internal static ExceptionDispatchInfo GetExistingCustomLoaderFailureAndClear(string appId)
		{
			KeyValuePair<string, ExceptionDispatchInfo> customLoaderStartupError = ProcessHost._customLoaderStartupError;
			if (string.Equals(customLoaderStartupError.Key, appId, StringComparison.OrdinalIgnoreCase))
			{
				ProcessHost._customLoaderStartupError = default(KeyValuePair<string, ExceptionDispatchInfo>);
				return customLoaderStartupError.Value;
			}
			return null;
		}

		// Token: 0x06006078 RID: 24696 RVA: 0x0014D897 File Offset: 0x0014BA97
		private static void SetCustomLoaderFailure(string appId, ExceptionDispatchInfo error)
		{
			ProcessHost._customLoaderStartupError = new KeyValuePair<string, ExceptionDispatchInfo>(appId, error);
		}

		// Token: 0x06006079 RID: 24697 RVA: 0x0014D8A8 File Offset: 0x0014BAA8
		IObjectHandle IProcessHostLite.GetCustomLoader(string appId, string appConfigPath, out IProcessHostSupportFunctions supportFunctions, out AppDomain newlyCreatedAppDomain)
		{
			supportFunctions = null;
			newlyCreatedAppDomain = null;
			ProcessHost.CustomLoaderHelperFunctions customLoaderHelperFunctions = new ProcessHost.CustomLoaderHelperFunctions(this._functions, appId);
			string appVirtualPath = customLoaderHelperFunctions.AppVirtualPath;
			IObjectHandle result;
			try
			{
				string text = customLoaderHelperFunctions.MapPath("bin/AspNet.Loader.dll");
				if (!File.Exists(text))
				{
					result = null;
				}
				else
				{
					string appPhysicalPath = customLoaderHelperFunctions.AppPhysicalPath;
					string text2 = customLoaderHelperFunctions.MapPath("Web.config");
					bool flag = File.Exists(text2);
					supportFunctions = this._functions;
					result = CustomLoaderHelper.GetCustomLoader(customLoaderHelperFunctions, appConfigPath, flag ? text2 : null, text, out newlyCreatedAppDomain);
				}
			}
			catch (Exception source)
			{
				ProcessHost.SetCustomLoaderFailure(appId, ExceptionDispatchInfo.Capture(source));
				result = null;
			}
			return result;
		}

		// Token: 0x0600607A RID: 24698 RVA: 0x0014D94C File Offset: 0x0014BB4C
		void IProcessHostLite.ReportCustomLoaderError(string appId, int hr, AppDomain newlyCreatedAppDomain)
		{
			try
			{
				try
				{
					Marshal.ThrowExceptionForHR(hr);
				}
				finally
				{
					AppDomain.Unload(newlyCreatedAppDomain);
				}
			}
			catch (Exception source)
			{
				ProcessHost.SetCustomLoaderFailure(appId, ExceptionDispatchInfo.Capture(source));
			}
		}

		// Token: 0x0600607B RID: 24699 RVA: 0x0014D998 File Offset: 0x0014BB98
		string IProcessHostLite.GetFullExceptionMessage(int hr, IntPtr pErrorInfo)
		{
			Exception exceptionForHR = Marshal.GetExceptionForHR(hr, (pErrorInfo != IntPtr.Zero) ? pErrorInfo : ((IntPtr)(-1)));
			if (exceptionForHR != null)
			{
				return exceptionForHR.ToString();
			}
			return string.Empty;
		}

		// Token: 0x04003250 RID: 12880
		private static object _processHostStaticLock = new object();

		// Token: 0x04003251 RID: 12881
		private static ProcessHost _theProcessHost;

		// Token: 0x04003252 RID: 12882
		[ThreadStatic]
		private static KeyValuePair<string, ExceptionDispatchInfo> _customLoaderStartupError;

		// Token: 0x04003253 RID: 12883
		private readonly CustomRuntimeManager _customRuntimeManager = new CustomRuntimeManager();

		// Token: 0x04003254 RID: 12884
		private IProcessHostSupportFunctions _functions;

		// Token: 0x04003255 RID: 12885
		private ApplicationManager _appManager;

		// Token: 0x04003256 RID: 12886
		private ProtocolsSection _protocolsConfig;

		// Token: 0x04003257 RID: 12887
		private Hashtable _protocolHandlers = new Hashtable();

		// Token: 0x04003258 RID: 12888
		private IApplicationPreloadUtil _preloadUtil;

		// Token: 0x04003259 RID: 12889
		private Semaphore _preloadingThrottle;

		// Token: 0x02000A66 RID: 2662
		private sealed class SimpleProcessResumeCallbackDispatcher : IProcessResumeCallback
		{
			// Token: 0x06006F02 RID: 28418 RVA: 0x0018B523 File Offset: 0x00189723
			public SimpleProcessResumeCallbackDispatcher(Action callback)
			{
				this._callback = callback;
			}

			// Token: 0x06006F03 RID: 28419 RVA: 0x0018B532 File Offset: 0x00189732
			public void Resume()
			{
				this._callback();
			}

			// Token: 0x04003B91 RID: 15249
			private readonly Action _callback;
		}

		// Token: 0x02000A67 RID: 2663
		private sealed class CustomLoaderHelperFunctions : ICustomLoaderHelperFunctions
		{
			// Token: 0x06006F04 RID: 28420 RVA: 0x0018B540 File Offset: 0x00189740
			internal CustomLoaderHelperFunctions(IProcessHostSupportFunctions supportFunctions, string appId)
			{
				this._supportFunctions = supportFunctions;
				string appVirtualPath;
				string appPhysicalPath;
				string text;
				string text2;
				this._supportFunctions.GetApplicationProperties(appId, out appVirtualPath, out appPhysicalPath, out text, out text2);
				this.AppId = appId;
				this.AppVirtualPath = appVirtualPath;
				this.AppPhysicalPath = appPhysicalPath;
			}

			// Token: 0x17001E44 RID: 7748
			// (get) Token: 0x06006F05 RID: 28421 RVA: 0x0018B583 File Offset: 0x00189783
			// (set) Token: 0x06006F06 RID: 28422 RVA: 0x0018B58B File Offset: 0x0018978B
			public string AppId { get; private set; }

			// Token: 0x17001E45 RID: 7749
			// (get) Token: 0x06006F07 RID: 28423 RVA: 0x0018B594 File Offset: 0x00189794
			// (set) Token: 0x06006F08 RID: 28424 RVA: 0x0018B59C File Offset: 0x0018979C
			public string AppPhysicalPath { get; private set; }

			// Token: 0x17001E46 RID: 7750
			// (get) Token: 0x06006F09 RID: 28425 RVA: 0x0018B5A5 File Offset: 0x001897A5
			// (set) Token: 0x06006F0A RID: 28426 RVA: 0x0018B5AD File Offset: 0x001897AD
			public string AppVirtualPath { get; private set; }

			// Token: 0x17001E47 RID: 7751
			// (get) Token: 0x06006F0B RID: 28427 RVA: 0x0018B5B6 File Offset: 0x001897B6
			public bool? CustomLoaderIsEnabled
			{
				get
				{
					return ProcessHost.CustomLoaderHelperFunctions._isEnabled;
				}
			}

			// Token: 0x06006F0C RID: 28428 RVA: 0x0018B5C0 File Offset: 0x001897C0
			private static bool? GetIsEnabledValueFromRegistry()
			{
				bool? result = null;
				try
				{
					int num = (int)Misc.GetAspNetRegValue(null, "CustomLoaderEnabled", -1);
					if (num == 1)
					{
						result = new bool?(true);
					}
					else if (num == 0)
					{
						result = new bool?(false);
					}
				}
				catch
				{
				}
				return result;
			}

			// Token: 0x06006F0D RID: 28429 RVA: 0x0018B61C File Offset: 0x0018981C
			public string GetTrustLevel(string appConfigMetabasePath)
			{
				object obj;
				int errorCode = UnsafeIISMethods.MgdGetConfigProperty(appConfigMetabasePath, "system.web/trust", "level", out obj);
				Marshal.ThrowExceptionForHR(errorCode);
				return (string)obj;
			}

			// Token: 0x06006F0E RID: 28430 RVA: 0x0018B648 File Offset: 0x00189848
			public string MapPath(string relativePath)
			{
				return this._supportFunctions.MapPathInternal(this.AppId, this.AppVirtualPath, relativePath);
			}

			// Token: 0x04003B92 RID: 15250
			private static readonly bool? _isEnabled = ProcessHost.CustomLoaderHelperFunctions.GetIsEnabledValueFromRegistry();

			// Token: 0x04003B93 RID: 15251
			private readonly IProcessHostSupportFunctions _supportFunctions;
		}
	}
}
