using System;
using System.Linq;
using System.Reflection;
using System.Security.Authentication;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.Common.WCF.Attributes;

namespace TechnoPro.Common.WCF
{
	// Token: 0x02000005 RID: 5
	public static class BindingHelper
	{
		// Token: 0x06000020 RID: 32 RVA: 0x00002394 File Offset: 0x00000594
		public static NetTcpBinding GetNetTcpBinding(this Type contractType, SecurityMode securityMode = SecurityMode.Message)
		{
			bool flag = contractType.GetCustomAttribute<StreamingServiceAttribute>() != null;
			NetTcpBinding netTcpBinding;
			if (flag)
			{
				netTcpBinding = BindingHelper.GetNetTcpBinding(contractType.GetBindingSettings<BindingSettings>(), SecurityMode.None);
			}
			else
			{
				bool flag2 = contractType.GetCustomAttribute<NoSslCertificateAttribute>() != null;
				if (flag2)
				{
					netTcpBinding = BindingHelper.GetNetTcpBinding(contractType.GetBindingSettings<BindingSettings>(), SecurityMode.None);
				}
				else
				{
					netTcpBinding = BindingHelper.GetNetTcpBinding(contractType.GetBindingSettings<BindingSettings>(), securityMode);
				}
			}
			return netTcpBinding;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000023EC File Offset: 0x000005EC
		private static NetTcpBinding GetNetTcpBinding(BindingSettings bSettings, SecurityMode securityMode = SecurityMode.Message)
		{
			BindingSettings bindingSettings = bSettings ?? BindingHelper.DefaultSettings;
			NetTcpBinding netTcpBinding = new NetTcpBinding
			{
				MaxConnections = 100,
				ListenBacklog = 100,
				OpenTimeout = bindingSettings.OpenTimeout,
				CloseTimeout = bindingSettings.CloseTimeout,
				SendTimeout = bindingSettings.SendTimeout,
				ReceiveTimeout = bindingSettings.ReceiveTimeout,
				MaxReceivedMessageSize = (long)bindingSettings.MaxReceivedMessageSize,
				MaxBufferSize = bindingSettings.MaxBufferSize,
				ReaderQuotas = 
				{
					MaxStringContentLength = bindingSettings.MaxStringContentLength,
					MaxArrayLength = bindingSettings.MaxArrayLength
				},
				Security = 
				{
					Mode = securityMode
				},
				TransferMode = bindingSettings.TransferMode
			};
			bool flag = securityMode == SecurityMode.Message;
			if (flag)
			{
				netTcpBinding.Security.Message.ClientCredentialType = MessageCredentialType.None;
			}
			else
			{
				bool flag2 = securityMode == SecurityMode.Transport;
				if (flag2)
				{
					netTcpBinding.Security.Transport.ClientCredentialType = TcpClientCredentialType.None;
				}
			}
			netTcpBinding.Security.Transport.SslProtocols = SslProtocols.None;
			return netTcpBinding;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002500 File Offset: 0x00000700
		public static Binding GetHttpBinding(this Type contractType)
		{
			bool flag = contractType.GetCustomAttribute<NoSslCertificateAttribute>() != null;
			Binding result;
			if (flag)
			{
				result = contractType.GetBasicHttpBinding();
			}
			else
			{
				bool flag2 = contractType.GetCustomAttribute<DualChannelServiceAttribute>() != null;
				if (flag2)
				{
					result = contractType.GetWsDualHttpBinding();
				}
				else
				{
					result = contractType.GetWsHttpBinding();
				}
			}
			return result;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002544 File Offset: 0x00000744
		public static WSHttpBinding GetWsHttpBinding(this Type contractType)
		{
			return BindingHelper.GetWsHttpBinding(contractType.GetBindingSettings<BindingSettings>(), (contractType.GetCustomAttribute<NoSslCertificateAttribute>() != null) ? SecurityMode.None : SecurityMode.Message);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002570 File Offset: 0x00000770
		private static WSHttpBinding GetWsHttpBinding(BindingSettings bSettings, SecurityMode securityMode = SecurityMode.Message)
		{
			BindingSettings bindingSettings = bSettings ?? BindingHelper.DefaultSettings;
			WSHttpBinding wshttpBinding = new WSHttpBinding
			{
				OpenTimeout = bindingSettings.OpenTimeout,
				CloseTimeout = bindingSettings.CloseTimeout,
				SendTimeout = bindingSettings.SendTimeout,
				ReceiveTimeout = bindingSettings.ReceiveTimeout,
				MaxReceivedMessageSize = (long)bindingSettings.MaxReceivedMessageSize,
				MaxBufferPoolSize = (long)bindingSettings.MaxBufferSize,
				ReaderQuotas = 
				{
					MaxStringContentLength = bindingSettings.MaxStringContentLength,
					MaxArrayLength = bindingSettings.MaxArrayLength
				},
				Security = 
				{
					Mode = securityMode
				}
			};
			bool flag = securityMode == SecurityMode.Message;
			if (flag)
			{
				wshttpBinding.Security.Message.ClientCredentialType = MessageCredentialType.None;
			}
			else
			{
				bool flag2 = securityMode == SecurityMode.Transport;
				if (flag2)
				{
					wshttpBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;
				}
			}
			return wshttpBinding;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002650 File Offset: 0x00000850
		private static BasicHttpBinding GetBasicHttpBinding(this Type contractType)
		{
			return BindingHelper.GetBasicHttpBinding(contractType.GetBindingSettings<BindingSettings>(), BasicHttpSecurityMode.None);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002670 File Offset: 0x00000870
		private static BasicHttpBinding GetBasicHttpBinding(BindingSettings bSettings, BasicHttpSecurityMode securityMode = BasicHttpSecurityMode.None)
		{
			BindingSettings bindingSettings = bSettings ?? BindingHelper.DefaultSettings;
			return new BasicHttpBinding
			{
				OpenTimeout = bindingSettings.OpenTimeout,
				CloseTimeout = bindingSettings.CloseTimeout,
				SendTimeout = bindingSettings.SendTimeout,
				ReceiveTimeout = bindingSettings.ReceiveTimeout,
				MaxReceivedMessageSize = (long)bindingSettings.MaxReceivedMessageSize,
				MaxBufferPoolSize = (long)bindingSettings.MaxBufferSize,
				ReaderQuotas = 
				{
					MaxStringContentLength = bindingSettings.MaxStringContentLength,
					MaxArrayLength = bindingSettings.MaxArrayLength
				},
				Security = 
				{
					Mode = securityMode
				},
				TransferMode = bindingSettings.TransferMode
			};
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002724 File Offset: 0x00000924
		private static WSDualHttpBinding GetWsDualHttpBinding(this Type contractType)
		{
			return BindingHelper.GetWsDualHttpBinding(contractType.GetBindingSettings<BindingSettings>(), (contractType.GetCustomAttribute<NoSslCertificateAttribute>() != null) ? WSDualHttpSecurityMode.None : WSDualHttpSecurityMode.Message);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002750 File Offset: 0x00000950
		private static WSDualHttpBinding GetWsDualHttpBinding(BindingSettings bSettings, WSDualHttpSecurityMode securityMode = WSDualHttpSecurityMode.Message)
		{
			BindingSettings bindingSettings = bSettings ?? BindingHelper.DefaultSettings;
			WSDualHttpBinding wsdualHttpBinding = new WSDualHttpBinding
			{
				OpenTimeout = bindingSettings.OpenTimeout,
				CloseTimeout = bindingSettings.CloseTimeout,
				SendTimeout = bindingSettings.SendTimeout,
				ReceiveTimeout = bindingSettings.ReceiveTimeout,
				MaxReceivedMessageSize = (long)bindingSettings.MaxReceivedMessageSize,
				MaxBufferPoolSize = (long)bindingSettings.MaxBufferSize,
				ReaderQuotas = 
				{
					MaxStringContentLength = bindingSettings.MaxStringContentLength,
					MaxArrayLength = bindingSettings.MaxArrayLength
				},
				Security = 
				{
					Mode = securityMode
				}
			};
			bool flag = securityMode == WSDualHttpSecurityMode.Message;
			if (flag)
			{
				wsdualHttpBinding.Security.Message.ClientCredentialType = MessageCredentialType.None;
			}
			return wsdualHttpBinding;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002810 File Offset: 0x00000A10
		public static NetMsmqBinding GetNetMsmqBinding(this Type contractType)
		{
			return BindingHelper.GetNetMsmqBinding(contractType.GetBindingSettings<MsmqBindingSettings>(), (contractType.GetCustomAttribute<NoSslCertificateAttribute>() != null) ? NetMsmqSecurityMode.None : NetMsmqSecurityMode.Message);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000283C File Offset: 0x00000A3C
		private static NetMsmqBinding GetNetMsmqBinding(MsmqBindingSettings bSettings, NetMsmqSecurityMode securityMode = NetMsmqSecurityMode.Message)
		{
			NetMsmqBinding netMsmqBinding = new NetMsmqBinding(securityMode)
			{
				ExactlyOnce = bSettings.ExactlyOne,
				OpenTimeout = bSettings.OpenTimeout,
				CloseTimeout = bSettings.CloseTimeout,
				SendTimeout = bSettings.SendTimeout,
				ReceiveTimeout = bSettings.ReceiveTimeout,
				MaxReceivedMessageSize = (long)bSettings.MaxReceivedMessageSize,
				MaxBufferPoolSize = (long)bSettings.MaxBufferSize,
				ReaderQuotas = 
				{
					MaxStringContentLength = bSettings.MaxStringContentLength,
					MaxArrayLength = bSettings.MaxArrayLength
				},
				Security = 
				{
					Mode = securityMode
				},
				TimeToLive = bSettings.TimeToLive,
				DeadLetterQueue = bSettings.DeadLetterQueue
			};
			bool flag = securityMode == NetMsmqSecurityMode.Message;
			if (flag)
			{
				netMsmqBinding.Security.Message.ClientCredentialType = MessageCredentialType.None;
			}
			return netMsmqBinding;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002918 File Offset: 0x00000B18
		public static NetNamedPipeBinding GetNetNamedPipeBinding()
		{
			return BindingHelper.GetNetNamedPipeBinding(new NetNamedBindingSettings(), NetNamedPipeSecurityMode.Transport);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002938 File Offset: 0x00000B38
		public static NetNamedPipeBinding GetNetNamedPipeBinding(this Type contractType)
		{
			return BindingHelper.GetNetNamedPipeBinding(contractType.GetBindingSettings<NetNamedBindingSettings>(), (contractType.GetCustomAttribute<NoSslCertificateAttribute>() != null) ? NetNamedPipeSecurityMode.None : NetNamedPipeSecurityMode.Transport);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002964 File Offset: 0x00000B64
		private static NetNamedPipeBinding GetNetNamedPipeBinding(NetNamedBindingSettings bSettings, NetNamedPipeSecurityMode securityMode = NetNamedPipeSecurityMode.Transport)
		{
			return new NetNamedPipeBinding
			{
				MaxConnections = 100,
				OpenTimeout = bSettings.OpenTimeout,
				CloseTimeout = bSettings.CloseTimeout,
				SendTimeout = bSettings.SendTimeout,
				ReceiveTimeout = bSettings.ReceiveTimeout,
				MaxReceivedMessageSize = (long)bSettings.MaxReceivedMessageSize,
				MaxBufferSize = bSettings.MaxBufferSize,
				ReaderQuotas = 
				{
					MaxStringContentLength = bSettings.MaxStringContentLength,
					MaxArrayLength = bSettings.MaxArrayLength
				},
				Security = 
				{
					Mode = securityMode,
					Transport = 
					{
						ProtectionLevel = bSettings.ProtectionLevel
					}
				}
			};
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002A1C File Offset: 0x00000C1C
		private static T GetBindingSettings<T>(this Type contractType) where T : BindingSettings
		{
			T t = Activator.CreateInstance<T>();
			t.ApplyBindingSettingsAttributes(contractType.GetCustomAttributes<BindingServiceAttribute>().ToArray<BindingServiceAttribute>());
			return t;
		}

		// Token: 0x0400000E RID: 14
		private static readonly BindingSettings DefaultSettings = new BindingSettings();
	}
}
