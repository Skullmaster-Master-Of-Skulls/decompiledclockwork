using System;
using System.Collections;
using System.Configuration;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Security.Permissions;
using System.Web.Configuration;
using System.Web.Util;

namespace System.Web.Hosting
{
	// Token: 0x020002BA RID: 698
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class ProcessHost : MarshalByRefObject, IProcessHost, IAdphManager, IPphManager, IProcessHostIdleAndHealthCheck
	{
		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x06002415 RID: 9237 RVA: 0x0009A4DC File Offset: 0x000994DC
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

		// Token: 0x06002416 RID: 9238 RVA: 0x0009A558 File Offset: 0x00099558
		[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal)]
		private ProcessHost(IProcessHostSupportFunctions functions)
		{
			try
			{
				this._functions = functions;
				HostingEnvironment.SupportFunctions = functions;
				this._appManager = ApplicationManager.GetApplicationManager();
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

		// Token: 0x06002417 RID: 9239 RVA: 0x0009A5DC File Offset: 0x000995DC
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

		// Token: 0x06002418 RID: 9240 RVA: 0x0009A65C File Offset: 0x0009965C
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

		// Token: 0x06002419 RID: 9241 RVA: 0x0009A710 File Offset: 0x00099710
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		public override object InitializeLifetimeService()
		{
			return null;
		}

		// Token: 0x0600241A RID: 9242 RVA: 0x0009A714 File Offset: 0x00099714
		internal static ProcessHost GetProcessHost(IProcessHostSupportFunctions functions)
		{
			if (ProcessHost._theProcessHost == null)
			{
				lock (ProcessHost._processHostStaticLock)
				{
					if (ProcessHost._theProcessHost == null)
					{
						ProcessHost._theProcessHost = new ProcessHost(functions);
					}
				}
			}
			return ProcessHost._theProcessHost;
		}

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x0600241B RID: 9243 RVA: 0x0009A764 File Offset: 0x00099764
		internal static ProcessHost DefaultHost
		{
			get
			{
				return ProcessHost._theProcessHost;
			}
		}

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x0600241C RID: 9244 RVA: 0x0009A76B File Offset: 0x0009976B
		internal IProcessHostSupportFunctions SupportFunctions
		{
			get
			{
				return this._functions;
			}
		}

		// Token: 0x0600241D RID: 9245 RVA: 0x0009A774 File Offset: 0x00099774
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

		// Token: 0x0600241E RID: 9246 RVA: 0x0009A890 File Offset: 0x00099890
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

		// Token: 0x0600241F RID: 9247 RVA: 0x0009A938 File Offset: 0x00099938
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

		// Token: 0x06002420 RID: 9248 RVA: 0x0009A9F0 File Offset: 0x000999F0
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
				ISAPIApplicationHost appHost = this.CreateAppHost(appId, null);
				Type appDomainProtocolHandlerType = this.GetAppDomainProtocolHandlerType(protocolId);
				lock (this._appManager)
				{
					HostingEnvironmentParameters hostingEnvironmentParameters = new HostingEnvironmentParameters();
					hostingEnvironmentParameters.HostingFlags = HostingEnvironmentFlags.ThrowHostingInitErrors;
					AppDomainProtocolHandler handler = (AppDomainProtocolHandler)this._appManager.CreateObjectInternal(appId, appDomainProtocolHandlerType, appHost, false, hostingEnvironmentParameters);
					ListenerAdapterDispatchShim listenerAdapterDispatchShim = (ListenerAdapterDispatchShim)this._appManager.CreateObjectInternal(appId, typeof(ListenerAdapterDispatchShim), appHost, false, hostingEnvironmentParameters);
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

		// Token: 0x06002421 RID: 9249 RVA: 0x0009AB10 File Offset: 0x00099B10
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
				lock (this._appManager)
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

		// Token: 0x06002422 RID: 9250 RVA: 0x0009ABD8 File Offset: 0x00099BD8
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
				lock (this._appManager)
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

		// Token: 0x06002423 RID: 9251 RVA: 0x0009ACA0 File Offset: 0x00099CA0
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
				IApplicationHost appHost = this.CreateAppHost(appId, appPath);
				lock (this._appManager)
				{
					this._appManager.RemoveFromTableIfRuntimeExists(appId, typeof(PipelineRuntime));
					try
					{
						pipelineRuntime = (PipelineRuntime)this._appManager.CreateObjectInternal(appId, typeof(PipelineRuntime), appHost, true, null);
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

		// Token: 0x06002424 RID: 9252 RVA: 0x0009ADE0 File Offset: 0x00099DE0
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

		// Token: 0x06002425 RID: 9253 RVA: 0x0009AE48 File Offset: 0x00099E48
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

		// Token: 0x06002426 RID: 9254 RVA: 0x0009AFA4 File Offset: 0x00099FA4
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

		// Token: 0x06002427 RID: 9255 RVA: 0x0009B018 File Offset: 0x0009A018
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

		// Token: 0x06002428 RID: 9256 RVA: 0x0009B084 File Offset: 0x0009A084
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

		// Token: 0x06002429 RID: 9257 RVA: 0x0009B0F0 File Offset: 0x0009A0F0
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
			return new ISAPIApplicationHost(appId, appPath, false, this._functions);
		}

		// Token: 0x04001C34 RID: 7220
		private static object _processHostStaticLock = new object();

		// Token: 0x04001C35 RID: 7221
		private static ProcessHost _theProcessHost;

		// Token: 0x04001C36 RID: 7222
		private IProcessHostSupportFunctions _functions;

		// Token: 0x04001C37 RID: 7223
		private ApplicationManager _appManager;

		// Token: 0x04001C38 RID: 7224
		private ProtocolsSection _protocolsConfig;

		// Token: 0x04001C39 RID: 7225
		private Hashtable _protocolHandlers = new Hashtable();
	}
}
