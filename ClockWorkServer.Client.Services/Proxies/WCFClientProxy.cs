using System;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Security.Tokens;
using ClockWorkLogger;
using TechnoPro.ClockWorkServer.Client.Services.Adapters;
using TechnoPro.ClockWorkServer.Client.Services.Pool;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.ClockWorkServerConnection;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.WCF;
using TechnoPro.Common.WinServices;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000167 RID: 359
	public static class WCFClientProxy<TInterface> where TInterface : class, IService
	{
		// Token: 0x06000DC7 RID: 3527 RVA: 0x0002234A File Offset: 0x0002054A
		static WCFClientProxy()
		{
			ServicePointManager.DefaultConnectionLimit = 80;
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000DC8 RID: 3528 RVA: 0x00022358 File Offset: 0x00020558
		private static Binding Binding
		{
			get
			{
				ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
				string key = typeof(TInterface).Name + ".Binding";
				object obj = cacheStorageManager[key];
				bool flag = obj != null;
				Binding result;
				if (flag)
				{
					result = (Binding)obj;
				}
				else
				{
					ClockWorkServerPreferredConnectionInfo serverConnectionInfo = WCFClientProxy<TInterface>.ServerConnectionInfo;
					bool flag2 = serverConnectionInfo == null;
					if (flag2)
					{
						result = null;
					}
					else
					{
						Type typeFromHandle = typeof(TInterface);
						Binding binding = WCFClientProxy<TInterface>.ServerConnectionInfo.GetBinding(typeFromHandle);
						cacheStorageManager.Insert(key, binding);
						result = binding;
					}
				}
				return result;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000DC9 RID: 3529 RVA: 0x000223E4 File Offset: 0x000205E4
		private static EndpointAddress EndpointAddress
		{
			get
			{
				ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
				string key = typeof(TInterface).Name + ".Endpoint";
				object obj = cacheStorageManager[key];
				bool flag = obj != null;
				EndpointAddress result;
				if (flag)
				{
					result = (EndpointAddress)obj;
				}
				else
				{
					ClockWorkServerPreferredConnectionInfo serverConnectionInfo = WCFClientProxy<TInterface>.ServerConnectionInfo;
					bool flag2 = serverConnectionInfo == null;
					if (flag2)
					{
						result = null;
					}
					else
					{
						EndpointAddress endpointAddress = serverConnectionInfo.GetEndpointAddress(typeof(TInterface));
						cacheStorageManager.Insert(key, endpointAddress);
						result = endpointAddress;
					}
				}
				return result;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000DCA RID: 3530 RVA: 0x00022468 File Offset: 0x00020668
		public static ClockWorkServerPreferredConnectionInfo ServerConnectionInfo
		{
			get
			{
				ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
				return (ClockWorkServerPreferredConnectionInfo)cacheStorageManager["cClockWorkServerPreferredConnectionInfo"];
			}
		}

		// Token: 0x06000DCB RID: 3531 RVA: 0x00022490 File Offset: 0x00020690
		public static TInterface GetReusableInstance()
		{
			Binding binding = WCFClientProxy<TInterface>.Binding;
			EndpointAddress endpointAddress = WCFClientProxy<TInterface>.EndpointAddress;
			return (binding != null && endpointAddress != null) ? WCFClientProxy<TInterface>.GetReusableInstance(binding, endpointAddress, true) : default(TInterface);
		}

		// Token: 0x06000DCC RID: 3532 RVA: 0x000224D0 File Offset: 0x000206D0
		public static TInterface GetMsmqInstance(ClockWorkServerPreferredConnectionInfo connInfo = null)
		{
			MsmqServiceStatus messagingQueueServiceStatus = MessaggingQueueAdapter.GetMessagingQueueServiceStatus();
			bool flag = messagingQueueServiceStatus != MsmqServiceStatus.Running;
			TInterface result;
			if (flag)
			{
				result = default(TInterface);
			}
			else
			{
				Binding binding = BindingAdapter<TInterface>.GetBinding(eBindingType.MsmqBinding);
				EndpointAddress endpointAddress = (connInfo != null) ? connInfo.GetMsmqEndpointAddress<TInterface>() : WCFClientProxy<TInterface>.ServerConnectionInfo.GetMsmqEndpointAddress<TInterface>();
				result = ((binding != null && endpointAddress != null) ? WCFClientProxy<TInterface>.GetReusableInstance(binding, endpointAddress, true) : default(TInterface));
			}
			return result;
		}

		// Token: 0x06000DCD RID: 3533 RVA: 0x00022540 File Offset: 0x00020740
		public static TInterface GetReusableInstance(Binding binding, EndpointAddress endpointAddress, bool useProxyPool = true)
		{
			string text = typeof(TInterface).Name.Substring(1);
			TInterface result;
			try
			{
				if (useProxyPool)
				{
					TInterface tinterface = ProxyConnectionPool.RequestFromPool<TInterface>();
					bool flag = tinterface != null;
					if (flag)
					{
						CWLogger.Logger.Trace("WCFClientProxy::GetReusableInstance:: getting '{0}' proxy from connection pool successfully", text);
						return tinterface;
					}
				}
				Type type = Type.GetType("TechnoPro.ClockWorkServer.Client.Services.Proxies." + text + "ReusableClientProxy");
				bool flag2 = type != null;
				if (flag2)
				{
					CustomBinding customBinding = WCFClientProxy<TInterface>.GetCustomBinding(binding);
					TInterface tinterface2 = (TInterface)((object)Activator.CreateInstance(type, new object[]
					{
						customBinding,
						endpointAddress
					}));
					ProxyConnectionPool.Register<TInterface>(tinterface2);
					IClientBase clientBase = tinterface2 as IClientBase;
					bool flag3 = clientBase != null;
					if (flag3)
					{
						foreach (OperationDescription operationDescription in clientBase.Endpoint.Contract.Operations)
						{
							DataContractSerializerOperationBehavior dataContractSerializerOperationBehavior = operationDescription.Behaviors.Find<DataContractSerializerOperationBehavior>();
							bool flag4 = dataContractSerializerOperationBehavior != null;
							if (flag4)
							{
								dataContractSerializerOperationBehavior.MaxItemsInObjectGraph = int.MaxValue;
							}
						}
					}
					result = tinterface2;
				}
				else
				{
					CWLogger.Logger.Error("WCFClientProxy::GetReusableInstance: Failed to return an instance of {0}", text);
					result = default(TInterface);
				}
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Error("WCFClientProxy::GetReusableInstance: Failed to return an instance of " + text + ": " + ex.ToString());
				result = default(TInterface);
			}
			return result;
		}

		// Token: 0x06000DCE RID: 3534 RVA: 0x00022700 File Offset: 0x00020900
		public static TInterface GetAsyncInstance()
		{
			return WCFClientProxy<TInterface>.GetAsyncInstance(WCFClientProxy<TInterface>.Binding, WCFClientProxy<TInterface>.EndpointAddress);
		}

		// Token: 0x06000DCF RID: 3535 RVA: 0x00022724 File Offset: 0x00020924
		public static Type GetInstanceType()
		{
			string str = typeof(TInterface).Name.Substring(1);
			return Type.GetType("TechnoPro.ClockWorkServer.Client.Services.Proxies." + str + "ClientBaseProxy");
		}

		// Token: 0x06000DD0 RID: 3536 RVA: 0x00022764 File Offset: 0x00020964
		private static CustomBinding GetCustomBinding(Binding binding)
		{
			CustomBinding customBinding = new CustomBinding(binding);
			WCFClientProxy<TInterface>.SetMaxClockSkew(customBinding);
			return customBinding;
		}

		// Token: 0x06000DD1 RID: 3537 RVA: 0x00022785 File Offset: 0x00020985
		private static void SetMaxClockSkew(CustomBinding binding)
		{
			WCFClientProxy<TInterface>.SetMaxClockSkew(binding, ClockWorkServerBaseServiceHost.MaxClockSkewSeconds);
		}

		// Token: 0x06000DD2 RID: 3538 RVA: 0x00022794 File Offset: 0x00020994
		private static void ApplyMaxClockSkew(SecurityBindingElement securityBindingElement, int maxClockSkewSecond)
		{
			securityBindingElement.LocalClientSettings.MaxClockSkew = new TimeSpan(0, 0, maxClockSkewSecond);
			securityBindingElement.LocalClientSettings.DetectReplays = false;
			securityBindingElement.LocalClientSettings.SessionKeyRenewalInterval = TimeSpan.MaxValue;
			securityBindingElement.LocalServiceSettings.MaxClockSkew = new TimeSpan(0, 0, maxClockSkewSecond);
			securityBindingElement.LocalServiceSettings.DetectReplays = false;
			securityBindingElement.LocalServiceSettings.SessionKeyRenewalInterval = TimeSpan.MaxValue;
		}

		// Token: 0x06000DD3 RID: 3539 RVA: 0x00022808 File Offset: 0x00020A08
		private static void SetMaxClockSkew(CustomBinding outputBinding, int maxClockSkewSeconds)
		{
			SecurityBindingElement securityBindingElement = outputBinding.Elements.Find<SecurityBindingElement>();
			bool flag = securityBindingElement != null;
			if (flag)
			{
				int index = outputBinding.Elements.IndexOf(securityBindingElement);
				WCFClientProxy<TInterface>.ApplyMaxClockSkew(securityBindingElement, maxClockSkewSeconds);
				bool flag2 = securityBindingElement is SymmetricSecurityBindingElement;
				if (flag2)
				{
					WCFClientProxy<TInterface>.SetMaxClockSkewForSymmetricBinding(securityBindingElement, maxClockSkewSeconds);
				}
				else
				{
					bool flag3 = securityBindingElement is TransportSecurityBindingElement;
					if (flag3)
					{
						WCFClientProxy<TInterface>.SetMaxClockSkewForTransportBinding(securityBindingElement, maxClockSkewSeconds);
					}
				}
				outputBinding.Elements[index] = securityBindingElement;
			}
		}

		// Token: 0x06000DD4 RID: 3540 RVA: 0x00022880 File Offset: 0x00020A80
		private static void SetMaxClockSkewForSymmetricBinding(SecurityBindingElement securityBindingElement, int maxClockSkewSeconds)
		{
			SymmetricSecurityBindingElement symmetricSecurityBindingElement = securityBindingElement as SymmetricSecurityBindingElement;
			bool flag = symmetricSecurityBindingElement.ProtectionTokenParameters is SecureConversationSecurityTokenParameters;
			if (flag)
			{
				SecureConversationSecurityTokenParameters secureConversationSecurityTokenParameters = symmetricSecurityBindingElement.ProtectionTokenParameters as SecureConversationSecurityTokenParameters;
				WCFClientProxy<TInterface>.ApplyMaxClockSkew(secureConversationSecurityTokenParameters.BootstrapSecurityBindingElement, maxClockSkewSeconds);
				bool flag2 = secureConversationSecurityTokenParameters.BootstrapSecurityBindingElement.EndpointSupportingTokenParameters != null && secureConversationSecurityTokenParameters.BootstrapSecurityBindingElement.EndpointSupportingTokenParameters.Endorsing != null && secureConversationSecurityTokenParameters.BootstrapSecurityBindingElement.EndpointSupportingTokenParameters.Endorsing.Count > 0 && secureConversationSecurityTokenParameters.BootstrapSecurityBindingElement.EndpointSupportingTokenParameters.Endorsing[0] != null && secureConversationSecurityTokenParameters.BootstrapSecurityBindingElement.EndpointSupportingTokenParameters.Endorsing[0] is IssuedSecurityTokenParameters;
				if (flag2)
				{
					CustomBinding customBinding = new CustomBinding((secureConversationSecurityTokenParameters.BootstrapSecurityBindingElement.EndpointSupportingTokenParameters.Endorsing[0] as IssuedSecurityTokenParameters).IssuerBinding.CreateBindingElements());
					WCFClientProxy<TInterface>.SetMaxClockSkew(customBinding, maxClockSkewSeconds);
					(secureConversationSecurityTokenParameters.BootstrapSecurityBindingElement.EndpointSupportingTokenParameters.Endorsing[0] as IssuedSecurityTokenParameters).IssuerBinding = customBinding;
				}
			}
			else
			{
				bool flag3 = symmetricSecurityBindingElement.EndpointSupportingTokenParameters != null && symmetricSecurityBindingElement.EndpointSupportingTokenParameters.Endorsing != null && symmetricSecurityBindingElement.EndpointSupportingTokenParameters.Endorsing.Count > 0 && symmetricSecurityBindingElement.EndpointSupportingTokenParameters.Endorsing[0] != null && symmetricSecurityBindingElement.EndpointSupportingTokenParameters.Endorsing[0] is IssuedSecurityTokenParameters;
				if (flag3)
				{
					CustomBinding customBinding2 = new CustomBinding((symmetricSecurityBindingElement.EndpointSupportingTokenParameters.Endorsing[0] as IssuedSecurityTokenParameters).IssuerBinding.CreateBindingElements());
					WCFClientProxy<TInterface>.SetMaxClockSkew(customBinding2, maxClockSkewSeconds);
					(symmetricSecurityBindingElement.EndpointSupportingTokenParameters.Endorsing[0] as IssuedSecurityTokenParameters).IssuerBinding = customBinding2;
				}
			}
		}

		// Token: 0x06000DD5 RID: 3541 RVA: 0x00022A48 File Offset: 0x00020C48
		private static void SetMaxClockSkewForTransportBinding(SecurityBindingElement securityBindingElement, int maxClockSkewSeconds)
		{
			TransportSecurityBindingElement transportSecurityBindingElement = securityBindingElement as TransportSecurityBindingElement;
			bool flag = transportSecurityBindingElement.EndpointSupportingTokenParameters != null && transportSecurityBindingElement.EndpointSupportingTokenParameters.Endorsing != null && transportSecurityBindingElement.EndpointSupportingTokenParameters.Endorsing.Count > 0;
			if (flag)
			{
				bool flag2 = transportSecurityBindingElement.EndpointSupportingTokenParameters.Endorsing[0] is SecureConversationSecurityTokenParameters;
				if (flag2)
				{
					SecureConversationSecurityTokenParameters secureConversationSecurityTokenParameters = transportSecurityBindingElement.EndpointSupportingTokenParameters.Endorsing[0] as SecureConversationSecurityTokenParameters;
					WCFClientProxy<TInterface>.ApplyMaxClockSkew(secureConversationSecurityTokenParameters.BootstrapSecurityBindingElement, maxClockSkewSeconds);
					bool flag3 = secureConversationSecurityTokenParameters.BootstrapSecurityBindingElement.EndpointSupportingTokenParameters != null && secureConversationSecurityTokenParameters.BootstrapSecurityBindingElement.EndpointSupportingTokenParameters.Endorsing != null && secureConversationSecurityTokenParameters.BootstrapSecurityBindingElement.EndpointSupportingTokenParameters.Endorsing.Count > 0 && secureConversationSecurityTokenParameters.BootstrapSecurityBindingElement.EndpointSupportingTokenParameters.Endorsing[0] != null && secureConversationSecurityTokenParameters.BootstrapSecurityBindingElement.EndpointSupportingTokenParameters.Endorsing[0] is IssuedSecurityTokenParameters;
					if (flag3)
					{
						CustomBinding customBinding = new CustomBinding((secureConversationSecurityTokenParameters.BootstrapSecurityBindingElement.EndpointSupportingTokenParameters.Endorsing[0] as IssuedSecurityTokenParameters).IssuerBinding.CreateBindingElements());
						WCFClientProxy<TInterface>.SetMaxClockSkew(customBinding, maxClockSkewSeconds);
						(secureConversationSecurityTokenParameters.BootstrapSecurityBindingElement.EndpointSupportingTokenParameters.Endorsing[0] as IssuedSecurityTokenParameters).IssuerBinding = customBinding;
					}
				}
				else
				{
					bool flag4 = transportSecurityBindingElement.EndpointSupportingTokenParameters.Endorsing[0] is IssuedSecurityTokenParameters;
					if (flag4)
					{
						CustomBinding customBinding2 = new CustomBinding((transportSecurityBindingElement.EndpointSupportingTokenParameters.Endorsing[0] as IssuedSecurityTokenParameters).IssuerBinding.CreateBindingElements());
						WCFClientProxy<TInterface>.SetMaxClockSkew(customBinding2, maxClockSkewSeconds);
						(transportSecurityBindingElement.EndpointSupportingTokenParameters.Endorsing[0] as IssuedSecurityTokenParameters).IssuerBinding = customBinding2;
					}
				}
			}
		}

		// Token: 0x06000DD6 RID: 3542 RVA: 0x00022C18 File Offset: 0x00020E18
		private static TInterface GetAsyncInstance(Binding binding, EndpointAddress endpointAddress)
		{
			string text = typeof(TInterface).Name.Substring(1);
			TInterface result;
			try
			{
				Type type = Type.GetType("TechnoPro.ClockWorkServer.Client.Services.Proxies." + text + "ClientProxy");
				bool flag = type != null;
				if (flag)
				{
					TInterface tinterface = (TInterface)((object)Activator.CreateInstance(type, new object[]
					{
						binding,
						endpointAddress
					}));
					CWLogger.Logger.Trace("WCFClientProxy::GetAsyncInstance:: Getting '{0}' proxy instance successfully", text);
					result = tinterface;
				}
				else
				{
					CWLogger.Logger.Error("WCFClientProxy::GetAsyncInstance: Failed to return an instance of {0}", text);
					result = default(TInterface);
				}
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Error("WCFClientProxy::GetAsyncInstance: Failed to return an instance of " + text + ": " + ex.ToString());
				result = default(TInterface);
			}
			return result;
		}
	}
}
