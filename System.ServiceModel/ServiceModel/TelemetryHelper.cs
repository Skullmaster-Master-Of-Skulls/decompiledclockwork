using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime;
using System.ServiceModel.Activation;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.MsmqIntegration;
using System.Text;

namespace System.ServiceModel
{
	// Token: 0x020000A4 RID: 164
	internal class TelemetryHelper
	{
		// Token: 0x060002B9 RID: 697 RVA: 0x00010798 File Offset: 0x0000E998
		public string GetHostType()
		{
			if (!AspNetEnvironment.Enabled)
			{
				return "SelfHosted";
			}
			return "IISHosted";
		}

		// Token: 0x060002BA RID: 698 RVA: 0x000107AC File Offset: 0x0000E9AC
		public string GetEndpoints(ServiceDescription description)
		{
			string result = string.Empty;
			if (description != null)
			{
				List<string> list = new List<string>();
				foreach (ServiceEndpoint serviceEndpoint in description.Endpoints)
				{
					if (serviceEndpoint != null && serviceEndpoint.Binding != null)
					{
						list.Add(TelemetryHelper.GetDetails(serviceEndpoint.Binding));
					}
				}
				result = string.Join(";", list);
			}
			return result;
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0001082C File Offset: 0x0000EA2C
		public string GetServiceId(ServiceDescription description)
		{
			return StringUtil.GetNonRandomizedHashCode(description.ConfigurationName).ToString();
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0001084C File Offset: 0x0000EA4C
		public string GetAssemblyVersion()
		{
			return "4.8.9340.0";
		}

		// Token: 0x060002BD RID: 701 RVA: 0x00010854 File Offset: 0x0000EA54
		private static string GetDetails(Binding binding)
		{
			string text = null;
			string text2 = null;
			string text3 = null;
			try
			{
				if (binding is HttpBindingBase)
				{
					TelemetryHelper.GetHttpBindingBaseDetails((HttpBindingBase)binding, ref text3, ref text, ref text2);
				}
				else if (binding is MsmqIntegrationBinding)
				{
					TelemetryHelper.GetMsmqIntegrationBindingDetails((MsmqIntegrationBinding)binding, ref text3, ref text, ref text2);
				}
				else if (binding is NetMsmqBinding)
				{
					TelemetryHelper.GetNetMsmqBindingDetails((NetMsmqBinding)binding, ref text3, ref text, ref text2);
				}
				else if (binding is NetNamedPipeBinding)
				{
					TelemetryHelper.GetNetNamedPipeBindingDetails((NetNamedPipeBinding)binding, ref text3, ref text, ref text2);
				}
				else if (binding is NetTcpBinding)
				{
					TelemetryHelper.GetNetTcpBindingDetails((NetTcpBinding)binding, ref text3, ref text, ref text2);
				}
				else if (binding is WSDualHttpBinding)
				{
					TelemetryHelper.GetWSDualHttpBindingDetails((WSDualHttpBinding)binding, ref text3, ref text, ref text2);
				}
				else if (binding is WSFederationHttpBinding)
				{
					TelemetryHelper.GetWSFederationHttpBindingDetails((WSFederationHttpBinding)binding, ref text3, ref text, ref text2);
				}
				else if (binding is WSHttpBinding)
				{
					TelemetryHelper.GetWSHttpBindingDetails((WSHttpBinding)binding, ref text3, ref text, ref text2);
				}
				else if (binding is CustomBinding)
				{
					TelemetryHelper.GetCustomBindingDetails((CustomBinding)binding, ref text3, ref text, ref text2);
				}
				else if (binding is NetPeerTcpBinding)
				{
					TelemetryHelper.GetNetPeerTcpBindingDetails((NetPeerTcpBinding)binding, ref text3, ref text, ref text2);
				}
				else
				{
					text3 = (TelemetryHelper.IsKnownType(binding) ? binding.GetType().Name : "UserBinding");
				}
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
				DiagnosticUtility.TraceHandledException(exception, TraceEventType.Warning);
			}
			return string.Concat(new string[]
			{
				binding.Scheme,
				":",
				text3 ?? "unknown",
				":",
				text ?? "unknown",
				":",
				text2 ?? "unknown"
			});
		}

		// Token: 0x060002BE RID: 702 RVA: 0x00010A28 File Offset: 0x0000EC28
		private static void GetHttpBindingBaseDetails(HttpBindingBase binding, ref string name, ref string mode, ref string credentialType)
		{
			if (binding is BasicHttpContextBinding)
			{
				name = TelemetryHelper.GetBindingName<BasicHttpContextBinding>(binding);
			}
			else if (binding is BasicHttpBinding)
			{
				name = TelemetryHelper.GetBindingName<BasicHttpBinding>(binding);
			}
			else if (binding is NetHttpBinding)
			{
				name = TelemetryHelper.GetBindingName<NetHttpBinding>(binding);
			}
			else if (binding is NetHttpsBinding)
			{
				name = TelemetryHelper.GetBindingName<NetHttpsBinding>(binding);
			}
			else if (binding is BasicHttpsBinding)
			{
				name = TelemetryHelper.GetBindingName<BasicHttpsBinding>(binding);
			}
			else
			{
				name = TelemetryHelper.GetBindingName<HttpBindingBase>(binding);
			}
			BasicHttpSecurity basicHttpSecurity = binding.BasicHttpSecurity;
			mode = ((basicHttpSecurity != null) ? basicHttpSecurity.Mode.ToString() : null);
			BasicHttpSecurityMode? basicHttpSecurityMode = (basicHttpSecurity != null) ? new BasicHttpSecurityMode?(basicHttpSecurity.Mode) : null;
			if (basicHttpSecurityMode != null)
			{
				switch (basicHttpSecurityMode.GetValueOrDefault())
				{
				case BasicHttpSecurityMode.None:
					credentialType = "N/A";
					return;
				case BasicHttpSecurityMode.Transport:
				case BasicHttpSecurityMode.TransportCredentialOnly:
				{
					HttpTransportSecurity transport = basicHttpSecurity.Transport;
					credentialType = ((transport != null) ? transport.ClientCredentialType.ToString() : null);
					return;
				}
				case BasicHttpSecurityMode.Message:
				case BasicHttpSecurityMode.TransportWithMessageCredential:
				{
					HttpTransportSecurity transport2 = basicHttpSecurity.Transport;
					string str = (transport2 != null) ? transport2.ClientCredentialType.ToString() : null;
					string str2 = "+";
					BasicHttpMessageSecurity message = basicHttpSecurity.Message;
					credentialType = str + str2 + ((message != null) ? message.ClientCredentialType.ToString() : null);
					break;
				}
				default:
					return;
				}
			}
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00010B80 File Offset: 0x0000ED80
		private static void GetMsmqIntegrationBindingDetails(MsmqIntegrationBinding binding, ref string name, ref string mode, ref string credentialType)
		{
			name = TelemetryHelper.GetBindingName<MsmqIntegrationBinding>(binding);
			MsmqIntegrationSecurity security = binding.Security;
			mode = ((security != null) ? security.Mode.ToString() : null);
			MsmqIntegrationSecurityMode? msmqIntegrationSecurityMode = (security != null) ? new MsmqIntegrationSecurityMode?(security.Mode) : null;
			if (msmqIntegrationSecurityMode != null)
			{
				MsmqIntegrationSecurityMode valueOrDefault = msmqIntegrationSecurityMode.GetValueOrDefault();
				if (valueOrDefault == MsmqIntegrationSecurityMode.None)
				{
					credentialType = "N/A";
					return;
				}
				if (valueOrDefault != MsmqIntegrationSecurityMode.Transport)
				{
					return;
				}
				MsmqTransportSecurity transport = security.Transport;
				credentialType = ((transport != null) ? transport.MsmqAuthenticationMode.ToString() : null);
			}
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00010C18 File Offset: 0x0000EE18
		private static void GetNetMsmqBindingDetails(NetMsmqBinding binding, ref string name, ref string mode, ref string credentialType)
		{
			name = TelemetryHelper.GetBindingName<NetMsmqBinding>(binding);
			NetMsmqSecurity security = binding.Security;
			mode = ((security != null) ? security.Mode.ToString() : null);
			NetMsmqSecurityMode? netMsmqSecurityMode = (security != null) ? new NetMsmqSecurityMode?(security.Mode) : null;
			if (netMsmqSecurityMode != null)
			{
				NetMsmqSecurityMode valueOrDefault = netMsmqSecurityMode.GetValueOrDefault();
				if (valueOrDefault == NetMsmqSecurityMode.None)
				{
					credentialType = "N/A";
					return;
				}
				if (valueOrDefault - NetMsmqSecurityMode.Transport > 2)
				{
					return;
				}
				MsmqTransportSecurity transport = security.Transport;
				string str = (transport != null) ? transport.MsmqAuthenticationMode.ToString() : null;
				string str2 = "+";
				MessageSecurityOverMsmq message = security.Message;
				credentialType = str + str2 + ((message != null) ? message.ClientCredentialType.ToString() : null);
			}
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00010CDC File Offset: 0x0000EEDC
		private static void GetNetNamedPipeBindingDetails(NetNamedPipeBinding binding, ref string name, ref string mode, ref string credentialType)
		{
			name = TelemetryHelper.GetBindingName<NetNamedPipeBinding>(binding);
			NetNamedPipeSecurity security = binding.Security;
			mode = ((security != null) ? security.ToString() : null);
			NetNamedPipeSecurityMode? netNamedPipeSecurityMode = (security != null) ? new NetNamedPipeSecurityMode?(security.Mode) : null;
			if (netNamedPipeSecurityMode != null)
			{
				NetNamedPipeSecurityMode valueOrDefault = netNamedPipeSecurityMode.GetValueOrDefault();
				if (valueOrDefault == NetNamedPipeSecurityMode.None)
				{
					credentialType = "N/A";
					return;
				}
				if (valueOrDefault != NetNamedPipeSecurityMode.Transport)
				{
					return;
				}
				NamedPipeTransportSecurity transport = security.Transport;
				credentialType = ((transport != null) ? transport.ProtectionLevel.ToString() : null);
			}
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00010D64 File Offset: 0x0000EF64
		private static void GetNetTcpBindingDetails(NetTcpBinding binding, ref string name, ref string mode, ref string credentialType)
		{
			if (binding is NetTcpContextBinding)
			{
				name = TelemetryHelper.GetBindingName<NetTcpContextBinding>(binding);
			}
			else
			{
				name = TelemetryHelper.GetBindingName<NetTcpBinding>(binding);
			}
			NetTcpSecurity security = binding.Security;
			mode = ((security != null) ? security.Mode.ToString() : null);
			SecurityMode? securityMode = (security != null) ? new SecurityMode?(security.Mode) : null;
			if (securityMode != null)
			{
				switch (securityMode.GetValueOrDefault())
				{
				case SecurityMode.None:
					credentialType = "N/A";
					return;
				case SecurityMode.Transport:
				{
					TcpTransportSecurity transport = security.Transport;
					credentialType = ((transport != null) ? transport.ClientCredentialType.ToString() : null);
					return;
				}
				case SecurityMode.Message:
				{
					MessageSecurityOverTcp message = security.Message;
					credentialType = ((message != null) ? message.ClientCredentialType.ToString() : null);
					return;
				}
				case SecurityMode.TransportWithMessageCredential:
				{
					TcpTransportSecurity transport2 = security.Transport;
					string str = (transport2 != null) ? transport2.ClientCredentialType.ToString() : null;
					string str2 = "+";
					MessageSecurityOverTcp message2 = security.Message;
					credentialType = str + str2 + ((message2 != null) ? message2.ClientCredentialType.ToString() : null);
					break;
				}
				default:
					return;
				}
			}
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00010E94 File Offset: 0x0000F094
		private static void GetWSDualHttpBindingDetails(WSDualHttpBinding binding, ref string name, ref string mode, ref string credentialType)
		{
			name = TelemetryHelper.GetBindingName<WSDualHttpBinding>(binding);
			WSDualHttpSecurity security = binding.Security;
			mode = ((security != null) ? security.Mode.ToString() : null);
			WSDualHttpSecurityMode? wsdualHttpSecurityMode = (security != null) ? new WSDualHttpSecurityMode?(security.Mode) : null;
			if (wsdualHttpSecurityMode != null)
			{
				WSDualHttpSecurityMode valueOrDefault = wsdualHttpSecurityMode.GetValueOrDefault();
				if (valueOrDefault == WSDualHttpSecurityMode.None)
				{
					credentialType = "N/A";
					return;
				}
				if (valueOrDefault != WSDualHttpSecurityMode.Message)
				{
					return;
				}
				MessageSecurityOverHttp message = security.Message;
				credentialType = ((message != null) ? message.ClientCredentialType.ToString() : null);
			}
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00010F2C File Offset: 0x0000F12C
		private static void GetWSFederationHttpBindingDetails(WSFederationHttpBinding binding, ref string name, ref string mode, ref string credentialType)
		{
			if (binding is WS2007FederationHttpBinding)
			{
				name = TelemetryHelper.GetBindingName<WS2007FederationHttpBinding>(binding);
			}
			else
			{
				name = TelemetryHelper.GetBindingName<WSFederationHttpBinding>(binding);
			}
			WSFederationHttpSecurity security = binding.Security;
			mode = ((security != null) ? security.Mode.ToString() : null);
			WSFederationHttpSecurityMode? wsfederationHttpSecurityMode = (security != null) ? new WSFederationHttpSecurityMode?(security.Mode) : null;
			if (wsfederationHttpSecurityMode != null)
			{
				WSFederationHttpSecurityMode valueOrDefault = wsfederationHttpSecurityMode.GetValueOrDefault();
				if (valueOrDefault == WSFederationHttpSecurityMode.None)
				{
					credentialType = "N/A";
					return;
				}
				if (valueOrDefault - WSFederationHttpSecurityMode.Message > 1)
				{
					return;
				}
				FederatedMessageSecurityOverHttp message = security.Message;
				credentialType = (((message != null) ? message.IssuedTokenType : null) ?? "null");
			}
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00010FD4 File Offset: 0x0000F1D4
		private static void GetWSHttpBindingDetails(WSHttpBinding binding, ref string name, ref string mode, ref string credentialType)
		{
			if (binding is WSHttpContextBinding)
			{
				name = TelemetryHelper.GetBindingName<WSHttpContextBinding>(binding);
			}
			else if (binding is WS2007HttpBinding)
			{
				name = TelemetryHelper.GetBindingName<WS2007HttpBinding>(binding);
			}
			else
			{
				name = TelemetryHelper.GetBindingName<WSHttpBinding>(binding);
			}
			WSHttpSecurity security = binding.Security;
			mode = ((security != null) ? security.Mode.ToString() : null);
			SecurityMode? securityMode = (security != null) ? new SecurityMode?(security.Mode) : null;
			if (securityMode != null)
			{
				switch (securityMode.GetValueOrDefault())
				{
				case SecurityMode.None:
					credentialType = "N/A";
					return;
				case SecurityMode.Transport:
				{
					HttpTransportSecurity transport = security.Transport;
					credentialType = ((transport != null) ? transport.ClientCredentialType.ToString() : null);
					return;
				}
				case SecurityMode.Message:
				case SecurityMode.TransportWithMessageCredential:
				{
					HttpTransportSecurity transport2 = security.Transport;
					string str = (transport2 != null) ? transport2.ClientCredentialType.ToString() : null;
					string str2 = "+";
					NonDualMessageSecurityOverHttp message = security.Message;
					credentialType = str + str2 + ((message != null) ? message.ClientCredentialType.ToString() : null);
					break;
				}
				default:
					return;
				}
			}
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x000110F0 File Offset: 0x0000F2F0
		private static void GetCustomBindingDetails(CustomBinding binding, ref string name, ref string mode, ref string credentialType)
		{
			name = TelemetryHelper.GetBindingName<CustomBinding>(binding);
			StringBuilder stringBuilder = new StringBuilder();
			foreach (BindingElement bindingElement in binding.Elements)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append(TelemetryHelper.IsKnownType(bindingElement) ? bindingElement.GetType().Name : "UserBindingElement");
			}
			mode = stringBuilder.ToString();
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00011184 File Offset: 0x0000F384
		private static void GetNetPeerTcpBindingDetails(NetPeerTcpBinding binding, ref string name, ref string mode, ref string credentialType)
		{
			name = TelemetryHelper.GetBindingName<NetPeerTcpBinding>(binding);
			PeerSecuritySettings security = binding.Security;
			mode = ((security != null) ? security.Mode.ToString() : null);
			SecurityMode? securityMode = (security != null) ? new SecurityMode?(security.Mode) : null;
			if (securityMode != null)
			{
				SecurityMode valueOrDefault = securityMode.GetValueOrDefault();
				if (valueOrDefault == SecurityMode.None)
				{
					credentialType = "N/A";
					return;
				}
				if (valueOrDefault - SecurityMode.Transport > 2)
				{
					return;
				}
				PeerTransportSecuritySettings transport = security.Transport;
				credentialType = ((transport != null) ? transport.CredentialType.ToString() : null);
			}
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00011220 File Offset: 0x0000F420
		private static string GetBindingName<T>(Binding binding) where T : Binding
		{
			string name = typeof(T).Name;
			if (!(binding.GetType().Name == name))
			{
				return name + "*";
			}
			return name;
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00011260 File Offset: 0x0000F460
		private static bool IsKnownType(object obj)
		{
			byte[] publicKeyToken = obj.GetType().Assembly.GetName().GetPublicKeyToken();
			if (publicKeyToken != null && publicKeyToken.Length == 8)
			{
				foreach (byte[] other in TelemetryHelper.knownTokens)
				{
					if (((IStructuralEquatable)publicKeyToken).Equals(other, EqualityComparer<byte>.Default))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x04000942 RID: 2370
		private static readonly byte[][] knownTokens = new byte[][]
		{
			new byte[]
			{
				183,
				122,
				92,
				86,
				25,
				52,
				224,
				137
			},
			new byte[]
			{
				49,
				191,
				56,
				86,
				173,
				54,
				78,
				53
			},
			new byte[]
			{
				176,
				63,
				95,
				127,
				17,
				213,
				10,
				58
			}
		};
	}
}
