using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using ClockWorkLogger;
using TechnoPro.ClockWorkServer.Client.Services.Adapters;
using TechnoPro.ClockWorkServer.Client.Services.Proxies;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServer;
using TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServerConnection;
using TechnoPro.Common.ClientManager.Core.Adapters;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.ClockWorkServerConnection;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.ClockWorkServerConnection;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Win32;

namespace TechnoPro.Common.ClientManager.Core.ClockWorkServerConnection
{
	// Token: 0x02000076 RID: 118
	public class ClockWorkServerConnectionsClientManager : IClockWorkServerConnectionsClientManager, IWebService
	{
		// Token: 0x06000459 RID: 1113 RVA: 0x00013E9C File Offset: 0x0001209C
		public ClockWorkServerPreferredConnectionInfoDTO GetClockWorkServerConnectionInfo(Uri uri)
		{
			ClockWorkServerPreferredConnectionInfoDTO result;
			try
			{
				eBindingType bindingType = uri.GetBindingType();
				Binding binding = BindingAdapter<IClockWorkServerDiscovery>.GetBinding(bindingType);
				EndpointAddress endpointAddress = new EndpointAddress(uri, Array.Empty<AddressHeader>());
				IClockWorkServerDiscovery reusableInstance = WCFClientProxy<IClockWorkServerDiscovery>.GetReusableInstance(binding, endpointAddress, false);
				ClockWorkServerPreferredConnectionInfoDTO serverConnectionInfo = reusableInstance.GetClockWorkServerConnectionInfo(ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetClockWorkServerConnectionInfoReq>()).ServerConnectionInfo;
				bool flag = serverConnectionInfo.BindingType == eBindingType.Unspecified;
				if (flag)
				{
					serverConnectionInfo.BindingType = bindingType;
				}
				result = serverConnectionInfo;
			}
			catch (Exception ex)
			{
				CWLogger.Logger.ErrorException(string.Format("ClockWorkServerConnectionsClientManager::GetClockWorkServerConnectionInfo: {0}", ex.ToString()), ex);
				result = null;
			}
			return result;
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x00013F3C File Offset: 0x0001213C
		public ClockWorkServerInfoDTO GetClockWorkServerInfo(Uri uri)
		{
			eBindingType bindingType = uri.GetBindingType();
			Binding binding = BindingAdapter<IClockWorkServerDiscovery>.GetBinding(bindingType);
			EndpointAddress endpointAddress = new EndpointAddress(uri, Array.Empty<AddressHeader>());
			IClockWorkServerDiscovery reusableInstance = WCFClientProxy<IClockWorkServerDiscovery>.GetReusableInstance(binding, endpointAddress, false);
			ClockWorkServerInfoDTO serverInfo = reusableInstance.GetClockWorkServerInfo(ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetClockWorkServerInfoReq>()).ServerInfo;
			serverInfo.DiscoveryEnpointAddress = ((serverInfo.PreferredBindingType != eBindingType.Unspecified && serverInfo.PreferredBindingType != bindingType) ? uri.ChangeDiscoveryEndpoint(serverInfo.PreferredBindingType) : uri);
			return serverInfo;
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x00013FB8 File Offset: 0x000121B8
		[DebuggerStepThrough]
		public Task<ClockWorkServerInfoDTO> GetClockWorkServerInfoAsync(Uri uri)
		{
			ClockWorkServerConnectionsClientManager.<GetClockWorkServerInfoAsync>d__2 <GetClockWorkServerInfoAsync>d__ = new ClockWorkServerConnectionsClientManager.<GetClockWorkServerInfoAsync>d__2();
			<GetClockWorkServerInfoAsync>d__.<>t__builder = AsyncTaskMethodBuilder<ClockWorkServerInfoDTO>.Create();
			<GetClockWorkServerInfoAsync>d__.<>4__this = this;
			<GetClockWorkServerInfoAsync>d__.uri = uri;
			<GetClockWorkServerInfoAsync>d__.<>1__state = -1;
			<GetClockWorkServerInfoAsync>d__.<>t__builder.Start<ClockWorkServerConnectionsClientManager.<GetClockWorkServerInfoAsync>d__2>(ref <GetClockWorkServerInfoAsync>d__);
			return <GetClockWorkServerInfoAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x00014004 File Offset: 0x00012204
		public ClockWorkServerPreferredConnectionInfoDTO GetClockWorkPreferedConnection(string appStartupPath)
		{
			ClockWorkServerPreferredConnectionInfoDTO result;
			try
			{
				RegistryHelper registryHelper = new RegistryHelper();
				string text = registryHelper.ReadCurrentUserRegistry<string>(new string[]
				{
					"ClockWork",
					"ClockWorkServer",
					"DiscoveryServiceEndpoints",
					appStartupPath + "\\PreferedEndpointConnection"
				});
				bool flag = string.IsNullOrEmpty(text);
				if (flag)
				{
					ApplicationContext applicationContext = ObjectFactory.Resolve<ApplicationContext>();
					bool flag2 = string.IsNullOrEmpty((applicationContext != null) ? applicationContext.ExecutingPath : null);
					if (flag2)
					{
						result = null;
					}
					else
					{
						string executingPath = applicationContext.ExecutingPath;
						string text2 = Path.Combine(executingPath, "ClockWork2.ini");
						bool flag3 = File.Exists(text2);
						if (flag3)
						{
							ClockWorkServerPreferredConnectionInfo clockWorkServerConnectionInfoFromFile = this.GetClockWorkServerConnectionInfoFromFile(text2);
							bool flag4 = clockWorkServerConnectionInfoFromFile != null;
							if (flag4)
							{
								ClockWorkServerPreferredConnectionInfoDTO clockWorkServerConnectionInfo = this.GetClockWorkServerConnectionInfo(new Uri(string.Format("{0}://{1}:{2}/{3}/ClockWorkServerDiscovery.svc/{4}", new object[]
								{
									clockWorkServerConnectionInfoFromFile.BindingType.GetUriScheme(),
									clockWorkServerConnectionInfoFromFile.Hostname,
									this.GetPort(clockWorkServerConnectionInfoFromFile.BindingType, clockWorkServerConnectionInfoFromFile.Port, clockWorkServerConnectionInfoFromFile.ExternalPort),
									clockWorkServerConnectionInfoFromFile.VirtualDirectory,
									(clockWorkServerConnectionInfoFromFile.BindingType == eBindingType.NetTcpBinding) ? "netTcp" : "basicHttp"
								})));
								text = string.Format("{0}://{1}:{2}/{3}/ClockWorkServerDiscovery.svc/{4}", new object[]
								{
									clockWorkServerConnectionInfo.BindingType.GetUriScheme(),
									clockWorkServerConnectionInfo.Hostname,
									this.GetPort(clockWorkServerConnectionInfo.BindingType, clockWorkServerConnectionInfo.Port, clockWorkServerConnectionInfo.ExternalPort),
									clockWorkServerConnectionInfo.VirtualDirectory,
									(clockWorkServerConnectionInfo.BindingType == eBindingType.NetTcpBinding) ? "netTcp" : "basicHttp"
								});
								registryHelper.WriteCurrentUserRegistry<string>(text, new string[]
								{
									"ClockWork",
									"ClockWorkServer",
									"DiscoveryServiceEndpoints",
									appStartupPath + "\\PreferedEndpointConnection"
								});
								registryHelper.WriteCurrentUserRegistry<string>(eRegWow64Options.KEY_WOW64_32KEY, (clockWorkServerConnectionInfo.BindingType == eBindingType.HttpBinding) ? "http://clockworks.ca" : "net.tcp://clockworks.ca", new string[]
								{
									"ClockWorkCommon",
									"DiscoveryScopeUri"
								});
								return clockWorkServerConnectionInfo;
							}
						}
						result = null;
					}
				}
				else
				{
					ClockWorkServerPreferredConnectionInfoDTO clockWorkServerConnectionInfo2 = this.GetClockWorkServerConnectionInfo(new Uri(text));
					text = string.Format("{0}://{1}:{2}/{3}/ClockWorkServerDiscovery.svc/{4}", new object[]
					{
						clockWorkServerConnectionInfo2.BindingType.GetUriScheme(),
						clockWorkServerConnectionInfo2.Hostname,
						this.GetPort(clockWorkServerConnectionInfo2.BindingType, clockWorkServerConnectionInfo2.Port, clockWorkServerConnectionInfo2.ExternalPort),
						clockWorkServerConnectionInfo2.VirtualDirectory,
						(clockWorkServerConnectionInfo2.BindingType == eBindingType.NetTcpBinding) ? "netTcp" : "basicHttp"
					});
					registryHelper.WriteCurrentUserRegistry<string>(text, new string[]
					{
						"ClockWork",
						"ClockWorkServer",
						"DiscoveryServiceEndpoints",
						appStartupPath + "\\PreferedEndpointConnection"
					});
					registryHelper.WriteCurrentUserRegistry<string>(eRegWow64Options.KEY_WOW64_32KEY, (clockWorkServerConnectionInfo2.BindingType == eBindingType.HttpBinding) ? "http://clockworks.ca" : "net.tcp://clockworks.ca", new string[]
					{
						"ClockWorkCommon",
						"DiscoveryScopeUri"
					});
					result = clockWorkServerConnectionInfo2;
				}
			}
			catch (Exception ex)
			{
				CWLogger.Logger.ErrorException(string.Format("ClockWorkServerConnectionsClientManager::GetClockWorkPreferedConnection: {0}", ex.ToString()), ex);
				result = null;
			}
			return result;
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x00014358 File Offset: 0x00012558
		private int GetPort(eBindingType bindingType, int netTcpPort, int httpPort)
		{
			bool flag = bindingType == eBindingType.NetTcpBinding;
			int result;
			if (flag)
			{
				result = ((netTcpPort > 0) ? netTcpPort : eBindingType.NetTcpBinding.GetUriScheme().GetDefaultPort());
			}
			else
			{
				bool flag2 = bindingType == eBindingType.HttpBinding;
				if (flag2)
				{
					result = ((httpPort > 0) ? httpPort : eBindingType.HttpBinding.GetUriScheme().GetDefaultPort());
				}
				else
				{
					result = 0;
				}
			}
			return result;
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x000143A8 File Offset: 0x000125A8
		private ClockWorkServerPreferredConnectionInfo GetClockWorkServerConnectionInfoFromFile(string fn)
		{
			IClockWorkClientConnectionInfoClientManager clockWorkClientConnectionInfoClientManager = new ClockWorkClientConnectionInfoClientManager();
			string storageString = File.ReadAllText(fn);
			return clockWorkClientConnectionInfoClientManager.GetConnectionInfoFromStorageString(storageString).ServerPreferredConnection;
		}
	}
}
