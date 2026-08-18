using System;
using System.Messaging;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;
using ClockWorkLogger;
using TechnoPro.Common.ClientManager.ClientCaching;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.ClockWorkServerConnection;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.WCF;
using TechnoPro.Common.WCF.Attributes;
using TechnoPro.Common.Win32;

namespace TechnoPro.ClockWorkServer.Client.Services.Adapters
{
	// Token: 0x02000177 RID: 375
	public static class ClockWorkServerConnectionInfoAdapter
	{
		// Token: 0x06000E7F RID: 3711 RVA: 0x00025ABC File Offset: 0x00023CBC
		public static Binding GetBinding(this ClockWorkServerPreferredConnectionInfo serverConnectionInfo, Type contractType)
		{
			bool flag = serverConnectionInfo.IISVersion < InternetInformationServicesVersion.IIS7;
			Binding result;
			if (flag)
			{
				result = contractType.GetHttpBinding();
			}
			else
			{
				bool flag2 = contractType.Name.Equals("IMessaging");
				if (flag2)
				{
					result = contractType.GetNetTcpBinding(SecurityMode.Message);
				}
				else
				{
					eBindingType bindingType = serverConnectionInfo.BindingType;
					eBindingType eBindingType = bindingType;
					if (eBindingType != eBindingType.NetTcpBinding)
					{
						if (eBindingType != eBindingType.HttpBinding)
						{
						}
						result = contractType.GetHttpBinding();
					}
					else
					{
						result = contractType.GetNetTcpBinding(SecurityMode.Message);
					}
				}
			}
			return result;
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x00025B2C File Offset: 0x00023D2C
		public static EndpointAddress GetEndpointAddress(this ClockWorkServerPreferredConnectionInfo serverConnectionInfo, Type contractType)
		{
			string hostname = ClockWorkServerConnectionInfoAdapter.GetHostname(serverConnectionInfo);
			bool flag = string.IsNullOrEmpty(hostname);
			EndpointAddress result;
			if (flag)
			{
				result = null;
			}
			else
			{
				string text = contractType.Name.Substring(1);
				string text2 = text.EndsWith("Async") ? text.Substring(0, text.Length - 5) : text;
				string wcfBindingProtocol = serverConnectionInfo.GetWcfBindingProtocol(contractType);
				string bindingAddress = wcfBindingProtocol.GetBindingAddress(contractType);
				int port = serverConnectionInfo.GetPort(contractType);
				string uriString = string.Format("{0}://{1}:{2}/{3}/{4}.svc/{5}", new object[]
				{
					wcfBindingProtocol,
					hostname,
					port,
					serverConnectionInfo.VirtualDirectory,
					text2,
					bindingAddress
				});
				EndpointIdentity endpointIdentity = (contractType.GetCustomAttribute<NoSslCertificateAttribute>() != null) ? null : serverConnectionInfo.GetCertificateEndpointIdentity();
				result = ((endpointIdentity != null) ? new EndpointAddress(new Uri(uriString), endpointIdentity, Array.Empty<AddressHeader>()) : new EndpointAddress(new Uri(uriString), Array.Empty<AddressHeader>()));
			}
			return result;
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x00025C18 File Offset: 0x00023E18
		public static EndpointAddress GetMsmqEndpointAddress<TInterface>(this ClockWorkServerPreferredConnectionInfo serverConnectionInfo) where TInterface : class
		{
			string text = typeof(TInterface).Name.Substring(1);
			string uriString = string.Concat(new string[]
			{
				"net.msmq://",
				serverConnectionInfo.Hostname ?? "localhost",
				"/private/",
				serverConnectionInfo.VirtualDirectory,
				"/",
				text,
				".svc"
			});
			EndpointAddress endpointAddress = new EndpointAddress(new Uri(uriString), serverConnectionInfo.GetCertificateEndpointIdentity(), Array.Empty<AddressHeader>());
			try
			{
				endpointAddress.VerifyQueue(false);
			}
			catch (MessageQueueException ex)
			{
				CWLogger.Logger.ErrorException("EndpointAddressAdapter::GetMsmqEndpointAddress: " + ex.ToString(), ex);
				return null;
			}
			return endpointAddress;
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x00025CE4 File Offset: 0x00023EE4
		public static void Clear()
		{
			ClockWorkServerConnectionInfoAdapter.eIdentity = null;
		}

		// Token: 0x06000E83 RID: 3715 RVA: 0x00025CF0 File Offset: 0x00023EF0
		private static string GetWcfBindingProtocol(this ClockWorkServerPreferredConnectionInfo connectionInfo, Type contractType)
		{
			bool flag = connectionInfo.IISVersion < InternetInformationServicesVersion.IIS7;
			string result;
			if (flag)
			{
				result = "http";
			}
			else
			{
				bool flag2 = contractType.Name.Equals("IMessaging");
				if (flag2)
				{
					result = "net.tcp";
				}
				else
				{
					string uriScheme = connectionInfo.BindingType.GetUriScheme();
					result = (string.IsNullOrEmpty(uriScheme) ? "http" : uriScheme);
				}
			}
			return result;
		}

		// Token: 0x06000E84 RID: 3716 RVA: 0x00025D50 File Offset: 0x00023F50
		private static string GetBindingAddress(this string bindingProtocol, Type contractType)
		{
			bool flag = bindingProtocol == "net.tcp" || contractType.Name.Equals("IMessaging");
			string result;
			if (flag)
			{
				result = "netTcp";
			}
			else
			{
				bool flag2 = contractType.GetCustomAttribute<NoSslCertificateAttribute>() != null;
				if (flag2)
				{
					result = "basicHttp";
				}
				else
				{
					result = "wsHttp";
				}
			}
			return result;
		}

		// Token: 0x06000E85 RID: 3717 RVA: 0x00025DA8 File Offset: 0x00023FA8
		private static EndpointIdentity GetDNSEndpointIdentity(this ClockWorkServerPreferredConnectionInfo serverConnectionInfo)
		{
			string dnsName = string.IsNullOrEmpty(serverConnectionInfo.IdentityDNS) ? serverConnectionInfo.Certificate.IdentityDNS : serverConnectionInfo.IdentityDNS;
			EndpointIdentity result;
			if ((result = ClockWorkServerConnectionInfoAdapter.eIdentity) == null)
			{
				result = (ClockWorkServerConnectionInfoAdapter.eIdentity = EndpointIdentity.CreateDnsIdentity(dnsName));
			}
			return result;
		}

		// Token: 0x06000E86 RID: 3718 RVA: 0x00025DF0 File Offset: 0x00023FF0
		private static EndpointIdentity GetCertificateEndpointIdentity(this ClockWorkServerPreferredConnectionInfo serverConnectionInfo)
		{
			bool flag = ClockWorkServerConnectionInfoAdapter.eIdentity != null;
			EndpointIdentity result;
			if (flag)
			{
				result = ClockWorkServerConnectionInfoAdapter.eIdentity;
			}
			else
			{
				X509Certificate2 serverCertificate = serverConnectionInfo.GetServerCertificate();
				result = (ClockWorkServerConnectionInfoAdapter.eIdentity = ((serverCertificate != null) ? EndpointIdentity.CreateX509CertificateIdentity(serverCertificate) : serverConnectionInfo.GetDNSEndpointIdentity()));
			}
			return result;
		}

		// Token: 0x06000E87 RID: 3719 RVA: 0x00025E34 File Offset: 0x00024034
		public static X509Certificate2 GetServerCertificate(this ClockWorkServerPreferredConnectionInfo serverConnectionInfo)
		{
			X509Certificate2 result = null;
			CertificateInfo certificate = serverConnectionInfo.Certificate;
			string text = (certificate != null) ? certificate.Thumbprint : null;
			bool flag = string.IsNullOrEmpty(text);
			if (flag)
			{
				ClientCache clientCache = ObjectFactory.Resolve<ClientCache>();
				CertificateInfo serverCertificateInfo = clientCache.ServerCertificateInfo;
				bool flag2 = !string.IsNullOrEmpty((serverCertificateInfo != null) ? serverCertificateInfo.Thumbprint : null);
				if (flag2)
				{
					text = serverCertificateInfo.Thumbprint;
				}
			}
			bool flag3 = string.IsNullOrEmpty(text);
			if (flag3)
			{
				ClientCache clientCache2 = ObjectFactory.Resolve<ClientCache>();
				string text2 = clientCache2.ServerCertificateString;
				bool flag4 = string.IsNullOrEmpty(text2) && serverConnectionInfo.Certificate != null;
				if (flag4)
				{
					text2 = serverConnectionInfo.Certificate.CertificatePublicKey;
				}
				bool flag5 = !string.IsNullOrEmpty(text2);
				if (flag5)
				{
					X509Certificate2 x509Certificate = new X509Certificate2(Convert.FromBase64String(text2));
					text = x509Certificate.Thumbprint;
				}
			}
			bool flag6 = !string.IsNullOrEmpty(text);
			if (flag6)
			{
				X509Store x509Store = new X509Store(StoreName.TrustedPeople, StoreLocation.LocalMachine);
				x509Store.Open(OpenFlags.ReadOnly);
				result = x509Store.GetCertificateByThumbprint(text, false);
				x509Store.Close();
			}
			return result;
		}

		// Token: 0x06000E88 RID: 3720 RVA: 0x00025F44 File Offset: 0x00024144
		private static string GetHostname(ClockWorkServerPreferredConnectionInfo clockWorkClientConnectionInfo)
		{
			eBindingType bindingType = clockWorkClientConnectionInfo.BindingType;
			eBindingType eBindingType = bindingType;
			string result;
			if (eBindingType > eBindingType.NetTcpBinding)
			{
				if (eBindingType != eBindingType.HttpBinding)
				{
					result = (clockWorkClientConnectionInfo.Hostname ?? string.Empty);
				}
				else
				{
					result = (clockWorkClientConnectionInfo.ExternalHostname ?? string.Empty);
				}
			}
			else
			{
				result = (clockWorkClientConnectionInfo.Hostname ?? string.Empty);
			}
			return result;
		}

		// Token: 0x06000E89 RID: 3721 RVA: 0x00025FA0 File Offset: 0x000241A0
		private static int GetPort(this ClockWorkServerPreferredConnectionInfo connectionInfo, Type contractType)
		{
			string wcfBindingProtocol = connectionInfo.GetWcfBindingProtocol(contractType);
			string text = wcfBindingProtocol;
			string a = text;
			int result;
			if (!(a == "net.tcp"))
			{
				if (!(a == "http"))
				{
					result = wcfBindingProtocol.GetDefaultPort();
				}
				else
				{
					result = ((connectionInfo.ExternalPort > 0) ? connectionInfo.ExternalPort : wcfBindingProtocol.GetDefaultPort());
				}
			}
			else
			{
				result = ((connectionInfo.Port > 0) ? connectionInfo.Port : wcfBindingProtocol.GetDefaultPort());
			}
			return result;
		}

		// Token: 0x0400002E RID: 46
		private static EndpointIdentity eIdentity;
	}
}
