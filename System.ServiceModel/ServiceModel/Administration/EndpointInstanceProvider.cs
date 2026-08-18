using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Authentication.ExtendedProtection;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.MsmqIntegration;
using System.Xml;

namespace System.ServiceModel.Administration
{
	// Token: 0x02000449 RID: 1097
	internal class EndpointInstanceProvider : ProviderBase, IWmiProvider
	{
		// Token: 0x06002AAC RID: 10924 RVA: 0x000A53FF File Offset: 0x000A35FF
		internal static string EndpointReference(Uri uri, string contractName)
		{
			return EndpointInstanceProvider.EndpointReference((null != uri) ? uri.ToString() : string.Empty, contractName, true);
		}

		// Token: 0x06002AAD RID: 10925 RVA: 0x000A5420 File Offset: 0x000A3620
		internal static string EndpointReference(string address, string contractName, bool local)
		{
			string text = string.Format(CultureInfo.InvariantCulture, "Endpoint.ListenUri='{0}',ContractName='{1}',AppDomainId='{2}',ProcessId={3}", new object[]
			{
				address,
				(contractName != null) ? contractName : string.Empty,
				AppDomainInfo.Current.Id,
				AppDomainInfo.Current.ProcessId
			});
			Uri uri;
			if (!local && Uri.TryCreate(address, UriKind.Absolute, out uri))
			{
				string host = uri.Host;
				if (!"localhost".Equals(host, StringComparison.OrdinalIgnoreCase) && !AppDomainInfo.Current.MachineName.Equals(host, StringComparison.OrdinalIgnoreCase))
				{
					string str = string.Format(CultureInfo.InvariantCulture, "\\\\{0}\\root\\ServiceModel:", new object[]
					{
						host
					});
					text = str + text;
				}
			}
			return text;
		}

		// Token: 0x06002AAE RID: 10926 RVA: 0x000A54D4 File Offset: 0x000A36D4
		private static void FillBindingInfo(EndpointInfo endpoint, IWmiInstance instance)
		{
			IWmiInstance wmiInstance = instance.NewInstance("Binding");
			IWmiInstance[] array = new IWmiInstance[endpoint.Binding.Elements.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = wmiInstance;
				EndpointInstanceProvider.FillBindingInfo(endpoint.Binding.Elements[i], ref array[i]);
			}
			wmiInstance.SetProperty("BindingElements", array);
			wmiInstance.SetProperty("Name", endpoint.Binding.Name);
			wmiInstance.SetProperty("Namespace", endpoint.Binding.Namespace);
			wmiInstance.SetProperty("CloseTimeout", endpoint.Binding.CloseTimeout);
			wmiInstance.SetProperty("Scheme", endpoint.Binding.Scheme);
			wmiInstance.SetProperty("OpenTimeout", endpoint.Binding.OpenTimeout);
			wmiInstance.SetProperty("ReceiveTimeout", endpoint.Binding.ReceiveTimeout);
			wmiInstance.SetProperty("SendTimeout", endpoint.Binding.SendTimeout);
			instance.SetProperty("Binding", wmiInstance);
		}

		// Token: 0x06002AAF RID: 10927 RVA: 0x000A55F8 File Offset: 0x000A37F8
		private static void FillAddressInfo(EndpointInfo endpoint, IWmiInstance instance)
		{
			string[] array = new string[endpoint.Headers.Count];
			int num = 0;
			foreach (AddressHeader addressHeader in endpoint.Headers)
			{
				PlainXmlWriter plainXmlWriter = new PlainXmlWriter();
				addressHeader.WriteAddressHeader(plainXmlWriter);
				array[num++] = plainXmlWriter.ToString();
			}
			ProviderBase.FillCollectionInfo(array, instance, "AddressHeaders");
			instance.SetProperty("Address", (endpoint.Address == null) ? string.Empty : endpoint.Address.ToString());
			instance.SetProperty("ListenUri", (endpoint.ListenUri == null) ? string.Empty : endpoint.ListenUri.ToString());
			instance.SetProperty("AddressIdentity", (endpoint.Identity == null) ? string.Empty : endpoint.Identity.ToString());
		}

		// Token: 0x06002AB0 RID: 10928 RVA: 0x000A56F8 File Offset: 0x000A38F8
		private static void FillContractInfo(EndpointInfo endpoint, IWmiInstance instance)
		{
			instance.SetProperty("Contract", ContractInstanceProvider.ContractReference(endpoint.Contract.Name));
		}

		// Token: 0x06002AB1 RID: 10929 RVA: 0x000A5718 File Offset: 0x000A3918
		internal static void FillEndpointInfo(EndpointInfo endpoint, IWmiInstance instance)
		{
			instance.SetProperty("CounterInstanceName", PerformanceCounters.PerformanceCountersEnabled ? EndpointPerformanceCountersBase.GetFriendlyInstanceName(endpoint.ServiceName, endpoint.Contract.Name, endpoint.Address.AbsoluteUri.ToUpperInvariant()) : string.Empty);
			instance.SetProperty("Name", endpoint.Name);
			instance.SetProperty("ContractName", endpoint.Contract.Name);
			EndpointInstanceProvider.FillAddressInfo(endpoint, instance);
			EndpointInstanceProvider.FillContractInfo(endpoint, instance);
			EndpointInstanceProvider.FillBindingInfo(endpoint, instance);
			EndpointInstanceProvider.FillBehaviorsInfo(endpoint, instance);
		}

		// Token: 0x06002AB2 RID: 10930 RVA: 0x000A57A8 File Offset: 0x000A39A8
		private static void FillBindingInfo(BindingElement bindingElement, ref IWmiInstance instance)
		{
			if (bindingElement is IWmiInstanceProvider)
			{
				IWmiInstanceProvider wmiInstanceProvider = (IWmiInstanceProvider)bindingElement;
				instance = instance.NewInstance(wmiInstanceProvider.GetInstanceType());
				wmiInstanceProvider.FillInstance(instance);
				return;
			}
			Type serviceModelBaseType = AdministrationHelpers.GetServiceModelBaseType(bindingElement.GetType());
			if (null != serviceModelBaseType)
			{
				instance = instance.NewInstance(serviceModelBaseType.Name);
				if (bindingElement is TransportBindingElement)
				{
					TransportBindingElement transportBindingElement = (TransportBindingElement)bindingElement;
					instance.SetProperty("ManualAddressing", transportBindingElement.ManualAddressing);
					instance.SetProperty("MaxReceivedMessageSize", transportBindingElement.MaxReceivedMessageSize);
					instance.SetProperty("MaxBufferPoolSize", transportBindingElement.MaxBufferPoolSize);
					instance.SetProperty("Scheme", transportBindingElement.Scheme);
					if (bindingElement is ConnectionOrientedTransportBindingElement)
					{
						ConnectionOrientedTransportBindingElement connectionOrientedTransportBindingElement = (ConnectionOrientedTransportBindingElement)bindingElement;
						instance.SetProperty("ConnectionBufferSize", connectionOrientedTransportBindingElement.ConnectionBufferSize);
						instance.SetProperty("HostNameComparisonMode", connectionOrientedTransportBindingElement.HostNameComparisonMode.ToString());
						instance.SetProperty("ChannelInitializationTimeout", connectionOrientedTransportBindingElement.ChannelInitializationTimeout);
						instance.SetProperty("MaxBufferSize", connectionOrientedTransportBindingElement.MaxBufferSize);
						instance.SetProperty("MaxPendingConnections", connectionOrientedTransportBindingElement.MaxPendingConnections);
						instance.SetProperty("MaxOutputDelay", connectionOrientedTransportBindingElement.MaxOutputDelay);
						instance.SetProperty("MaxPendingAccepts", connectionOrientedTransportBindingElement.MaxPendingAccepts);
						instance.SetProperty("TransferMode", connectionOrientedTransportBindingElement.TransferMode.ToString());
						if (bindingElement is TcpTransportBindingElement)
						{
							TcpTransportBindingElement tcpTransportBindingElement = (TcpTransportBindingElement)bindingElement;
							instance.SetProperty("ListenBacklog", tcpTransportBindingElement.ListenBacklog);
							instance.SetProperty("PortSharingEnabled", tcpTransportBindingElement.PortSharingEnabled);
							instance.SetProperty("TeredoEnabled", tcpTransportBindingElement.TeredoEnabled);
							IWmiInstance wmiInstance = instance.NewInstance("TcpConnectionPoolSettings");
							wmiInstance.SetProperty("GroupName", tcpTransportBindingElement.ConnectionPoolSettings.GroupName);
							wmiInstance.SetProperty("IdleTimeout", tcpTransportBindingElement.ConnectionPoolSettings.IdleTimeout);
							wmiInstance.SetProperty("LeaseTimeout", tcpTransportBindingElement.ConnectionPoolSettings.LeaseTimeout);
							wmiInstance.SetProperty("MaxOutboundConnectionsPerEndpoint", tcpTransportBindingElement.ConnectionPoolSettings.MaxOutboundConnectionsPerEndpoint);
							instance.SetProperty("ConnectionPoolSettings", wmiInstance);
							EndpointInstanceProvider.FillExtendedProtectionPolicy(instance, tcpTransportBindingElement.ExtendedProtectionPolicy);
							return;
						}
						if (bindingElement is NamedPipeTransportBindingElement)
						{
							NamedPipeTransportBindingElement namedPipeTransportBindingElement = (NamedPipeTransportBindingElement)bindingElement;
							IWmiInstance wmiInstance2 = instance.NewInstance("NamedPipeConnectionPoolSettings");
							wmiInstance2.SetProperty("GroupName", namedPipeTransportBindingElement.ConnectionPoolSettings.GroupName);
							wmiInstance2.SetProperty("IdleTimeout", namedPipeTransportBindingElement.ConnectionPoolSettings.IdleTimeout);
							wmiInstance2.SetProperty("MaxOutboundConnectionsPerEndpoint", namedPipeTransportBindingElement.ConnectionPoolSettings.MaxOutboundConnectionsPerEndpoint);
							instance.SetProperty("ConnectionPoolSettings", wmiInstance2);
							return;
						}
					}
					else if (bindingElement is HttpTransportBindingElement)
					{
						HttpTransportBindingElement httpTransportBindingElement = (HttpTransportBindingElement)bindingElement;
						instance.SetProperty("AllowCookies", httpTransportBindingElement.AllowCookies);
						instance.SetProperty("AuthenticationScheme", httpTransportBindingElement.AuthenticationScheme.ToString());
						instance.SetProperty("BypassProxyOnLocal", httpTransportBindingElement.BypassProxyOnLocal);
						instance.SetProperty("DecompressionEnabled", httpTransportBindingElement.DecompressionEnabled);
						instance.SetProperty("HostNameComparisonMode", httpTransportBindingElement.HostNameComparisonMode.ToString());
						instance.SetProperty("KeepAliveEnabled", httpTransportBindingElement.KeepAliveEnabled);
						instance.SetProperty("MaxBufferSize", httpTransportBindingElement.MaxBufferSize);
						if (null != httpTransportBindingElement.ProxyAddress)
						{
							instance.SetProperty("ProxyAddress", httpTransportBindingElement.ProxyAddress.AbsoluteUri.ToString());
						}
						instance.SetProperty("ProxyAuthenticationScheme", httpTransportBindingElement.ProxyAuthenticationScheme.ToString());
						instance.SetProperty("Realm", httpTransportBindingElement.Realm);
						instance.SetProperty("TransferMode", httpTransportBindingElement.TransferMode.ToString());
						instance.SetProperty("UnsafeConnectionNtlmAuthentication", httpTransportBindingElement.UnsafeConnectionNtlmAuthentication);
						instance.SetProperty("UseDefaultWebProxy", httpTransportBindingElement.UseDefaultWebProxy);
						EndpointInstanceProvider.FillExtendedProtectionPolicy(instance, httpTransportBindingElement.ExtendedProtectionPolicy);
						if (bindingElement is HttpsTransportBindingElement)
						{
							HttpsTransportBindingElement httpsTransportBindingElement = (HttpsTransportBindingElement)bindingElement;
							instance.SetProperty("RequireClientCertificate", httpsTransportBindingElement.RequireClientCertificate);
							return;
						}
					}
					else if (bindingElement is MsmqBindingElementBase)
					{
						MsmqBindingElementBase msmqBindingElementBase = (MsmqBindingElementBase)bindingElement;
						if (null != msmqBindingElementBase.CustomDeadLetterQueue)
						{
							instance.SetProperty("CustomDeadLetterQueue", msmqBindingElementBase.CustomDeadLetterQueue.AbsoluteUri.ToString());
						}
						instance.SetProperty("DeadLetterQueue", msmqBindingElementBase.DeadLetterQueue);
						instance.SetProperty("Durable", msmqBindingElementBase.Durable);
						instance.SetProperty("ExactlyOnce", msmqBindingElementBase.ExactlyOnce);
						instance.SetProperty("MaxRetryCycles", msmqBindingElementBase.MaxRetryCycles);
						instance.SetProperty("ReceiveContextEnabled", msmqBindingElementBase.ReceiveContextEnabled);
						instance.SetProperty("ReceiveErrorHandling", msmqBindingElementBase.ReceiveErrorHandling);
						instance.SetProperty("ReceiveRetryCount", msmqBindingElementBase.ReceiveRetryCount);
						instance.SetProperty("RetryCycleDelay", msmqBindingElementBase.RetryCycleDelay);
						instance.SetProperty("TimeToLive", msmqBindingElementBase.TimeToLive);
						instance.SetProperty("UseSourceJournal", msmqBindingElementBase.UseSourceJournal);
						instance.SetProperty("UseMsmqTracing", msmqBindingElementBase.UseMsmqTracing);
						instance.SetProperty("ValidityDuration", msmqBindingElementBase.ValidityDuration);
						MsmqTransportBindingElement msmqTransportBindingElement = msmqBindingElementBase as MsmqTransportBindingElement;
						if (msmqTransportBindingElement != null)
						{
							instance.SetProperty("MaxPoolSize", msmqTransportBindingElement.MaxPoolSize);
							instance.SetProperty("QueueTransferProtocol", msmqTransportBindingElement.QueueTransferProtocol);
							instance.SetProperty("UseActiveDirectory", msmqTransportBindingElement.UseActiveDirectory);
						}
						MsmqIntegrationBindingElement msmqIntegrationBindingElement = msmqBindingElementBase as MsmqIntegrationBindingElement;
						if (msmqIntegrationBindingElement != null)
						{
							instance.SetProperty("SerializationFormat", msmqIntegrationBindingElement.SerializationFormat.ToString());
							return;
						}
					}
					else if (bindingElement is PeerTransportBindingElement)
					{
						PeerTransportBindingElement peerTransportBindingElement = (PeerTransportBindingElement)bindingElement;
						instance.SetProperty("ListenIPAddress", peerTransportBindingElement.ListenIPAddress);
						instance.SetProperty("Port", peerTransportBindingElement.Port);
						IWmiInstance wmiInstance3 = instance.NewInstance("PeerSecuritySettings");
						wmiInstance3.SetProperty("Mode", peerTransportBindingElement.Security.Mode.ToString());
						IWmiInstance wmiInstance4 = wmiInstance3.NewInstance("PeerTransportSecuritySettings");
						wmiInstance4.SetProperty("CredentialType", peerTransportBindingElement.Security.Transport.CredentialType.ToString());
						wmiInstance3.SetProperty("Transport", wmiInstance4);
						instance.SetProperty("Security", wmiInstance3);
						return;
					}
				}
				else if (bindingElement is PeerResolverBindingElement)
				{
					PeerResolverBindingElement peerResolverBindingElement = (PeerResolverBindingElement)bindingElement;
					instance.SetProperty("ReferralPolicy", peerResolverBindingElement.ReferralPolicy.ToString());
					if (bindingElement is PeerCustomResolverBindingElement)
					{
						PeerCustomResolverBindingElement peerCustomResolverBindingElement = (PeerCustomResolverBindingElement)bindingElement;
						if (peerCustomResolverBindingElement.Address != null)
						{
							instance.SetProperty("Address", peerCustomResolverBindingElement.Address.ToString());
						}
						if (peerCustomResolverBindingElement.Binding != null)
						{
							instance.SetProperty("Binding", peerCustomResolverBindingElement.Binding.ToString());
							return;
						}
					}
				}
				else
				{
					if (bindingElement is ReliableSessionBindingElement)
					{
						ReliableSessionBindingElement reliableSessionBindingElement = (ReliableSessionBindingElement)bindingElement;
						instance.SetProperty("AcknowledgementInterval", reliableSessionBindingElement.AcknowledgementInterval);
						instance.SetProperty("FlowControlEnabled", reliableSessionBindingElement.FlowControlEnabled);
						instance.SetProperty("InactivityTimeout", reliableSessionBindingElement.InactivityTimeout);
						instance.SetProperty("MaxPendingChannels", reliableSessionBindingElement.MaxPendingChannels);
						instance.SetProperty("MaxRetryCount", reliableSessionBindingElement.MaxRetryCount);
						instance.SetProperty("MaxTransferWindowSize", reliableSessionBindingElement.MaxTransferWindowSize);
						instance.SetProperty("Ordered", reliableSessionBindingElement.Ordered);
						instance.SetProperty("ReliableMessagingVersion", reliableSessionBindingElement.ReliableMessagingVersion.ToString());
						return;
					}
					if (bindingElement is SecurityBindingElement)
					{
						SecurityBindingElement securityBindingElement = (SecurityBindingElement)bindingElement;
						instance.SetProperty("AllowInsecureTransport", securityBindingElement.AllowInsecureTransport);
						instance.SetProperty("DefaultAlgorithmSuite", securityBindingElement.DefaultAlgorithmSuite.ToString());
						instance.SetProperty("EnableUnsecuredResponse", securityBindingElement.EnableUnsecuredResponse);
						instance.SetProperty("IncludeTimestamp", securityBindingElement.IncludeTimestamp);
						instance.SetProperty("KeyEntropyMode", securityBindingElement.KeyEntropyMode.ToString());
						instance.SetProperty("SecurityHeaderLayout", securityBindingElement.SecurityHeaderLayout.ToString());
						instance.SetProperty("MessageSecurityVersion", securityBindingElement.MessageSecurityVersion.ToString());
						IWmiInstance wmiInstance5 = instance.NewInstance("LocalServiceSecuritySettings");
						wmiInstance5.SetProperty("DetectReplays", securityBindingElement.LocalServiceSettings.DetectReplays);
						wmiInstance5.SetProperty("InactivityTimeout", securityBindingElement.LocalServiceSettings.InactivityTimeout);
						wmiInstance5.SetProperty("IssuedCookieLifetime", securityBindingElement.LocalServiceSettings.IssuedCookieLifetime);
						wmiInstance5.SetProperty("MaxCachedCookies", securityBindingElement.LocalServiceSettings.MaxCachedCookies);
						wmiInstance5.SetProperty("MaxClockSkew", securityBindingElement.LocalServiceSettings.MaxClockSkew);
						wmiInstance5.SetProperty("MaxPendingSessions", securityBindingElement.LocalServiceSettings.MaxPendingSessions);
						wmiInstance5.SetProperty("MaxStatefulNegotiations", securityBindingElement.LocalServiceSettings.MaxStatefulNegotiations);
						wmiInstance5.SetProperty("NegotiationTimeout", securityBindingElement.LocalServiceSettings.NegotiationTimeout);
						wmiInstance5.SetProperty("ReconnectTransportOnFailure", securityBindingElement.LocalServiceSettings.ReconnectTransportOnFailure);
						wmiInstance5.SetProperty("ReplayCacheSize", securityBindingElement.LocalServiceSettings.ReplayCacheSize);
						wmiInstance5.SetProperty("ReplayWindow", securityBindingElement.LocalServiceSettings.ReplayWindow);
						wmiInstance5.SetProperty("SessionKeyRenewalInterval", securityBindingElement.LocalServiceSettings.SessionKeyRenewalInterval);
						wmiInstance5.SetProperty("SessionKeyRolloverInterval", securityBindingElement.LocalServiceSettings.SessionKeyRolloverInterval);
						wmiInstance5.SetProperty("TimestampValidityDuration", securityBindingElement.LocalServiceSettings.TimestampValidityDuration);
						instance.SetProperty("LocalServiceSecuritySettings", wmiInstance5);
						if (bindingElement is AsymmetricSecurityBindingElement)
						{
							AsymmetricSecurityBindingElement asymmetricSecurityBindingElement = (AsymmetricSecurityBindingElement)bindingElement;
							instance.SetProperty("MessageProtectionOrder", asymmetricSecurityBindingElement.MessageProtectionOrder.ToString());
							instance.SetProperty("RequireSignatureConfirmation", asymmetricSecurityBindingElement.RequireSignatureConfirmation);
							return;
						}
						if (bindingElement is SymmetricSecurityBindingElement)
						{
							SymmetricSecurityBindingElement symmetricSecurityBindingElement = (SymmetricSecurityBindingElement)bindingElement;
							instance.SetProperty("MessageProtectionOrder", symmetricSecurityBindingElement.MessageProtectionOrder.ToString());
							instance.SetProperty("RequireSignatureConfirmation", symmetricSecurityBindingElement.RequireSignatureConfirmation);
							return;
						}
					}
					else
					{
						if (bindingElement is WindowsStreamSecurityBindingElement)
						{
							WindowsStreamSecurityBindingElement windowsStreamSecurityBindingElement = (WindowsStreamSecurityBindingElement)bindingElement;
							instance.SetProperty("ProtectionLevel", windowsStreamSecurityBindingElement.ProtectionLevel.ToString());
							return;
						}
						if (bindingElement is SslStreamSecurityBindingElement)
						{
							SslStreamSecurityBindingElement sslStreamSecurityBindingElement = (SslStreamSecurityBindingElement)bindingElement;
							instance.SetProperty("RequireClientCertificate", sslStreamSecurityBindingElement.RequireClientCertificate);
							return;
						}
						if (bindingElement is CompositeDuplexBindingElement)
						{
							CompositeDuplexBindingElement compositeDuplexBindingElement = (CompositeDuplexBindingElement)bindingElement;
							if (compositeDuplexBindingElement.ClientBaseAddress != null)
							{
								instance.SetProperty("ClientBaseAddress", compositeDuplexBindingElement.ClientBaseAddress.AbsoluteUri);
								return;
							}
						}
						else
						{
							if (bindingElement is OneWayBindingElement)
							{
								OneWayBindingElement oneWayBindingElement = (OneWayBindingElement)bindingElement;
								IWmiInstance wmiInstance6 = instance.NewInstance("ChannelPoolSettings");
								wmiInstance6.SetProperty("IdleTimeout", oneWayBindingElement.ChannelPoolSettings.IdleTimeout);
								wmiInstance6.SetProperty("LeaseTimeout", oneWayBindingElement.ChannelPoolSettings.LeaseTimeout);
								wmiInstance6.SetProperty("MaxOutboundChannelsPerEndpoint", oneWayBindingElement.ChannelPoolSettings.MaxOutboundChannelsPerEndpoint);
								instance.SetProperty("ChannelPoolSettings", wmiInstance6);
								instance.SetProperty("PacketRoutable", oneWayBindingElement.PacketRoutable);
								instance.SetProperty("MaxAcceptedChannels", oneWayBindingElement.MaxAcceptedChannels);
								return;
							}
							if (bindingElement is MessageEncodingBindingElement)
							{
								MessageEncodingBindingElement messageEncodingBindingElement = (MessageEncodingBindingElement)bindingElement;
								instance.SetProperty("MessageVersion", messageEncodingBindingElement.MessageVersion.ToString());
								if (bindingElement is BinaryMessageEncodingBindingElement)
								{
									BinaryMessageEncodingBindingElement binaryMessageEncodingBindingElement = (BinaryMessageEncodingBindingElement)bindingElement;
									instance.SetProperty("MaxSessionSize", binaryMessageEncodingBindingElement.MaxSessionSize);
									instance.SetProperty("MaxReadPoolSize", binaryMessageEncodingBindingElement.MaxReadPoolSize);
									instance.SetProperty("MaxWritePoolSize", binaryMessageEncodingBindingElement.MaxWritePoolSize);
									if (binaryMessageEncodingBindingElement.ReaderQuotas != null)
									{
										EndpointInstanceProvider.FillReaderQuotas(instance, binaryMessageEncodingBindingElement.ReaderQuotas);
									}
									instance.SetProperty("CompressionFormat", binaryMessageEncodingBindingElement.CompressionFormat.ToString());
									return;
								}
								if (bindingElement is TextMessageEncodingBindingElement)
								{
									TextMessageEncodingBindingElement textMessageEncodingBindingElement = (TextMessageEncodingBindingElement)bindingElement;
									instance.SetProperty("Encoding", textMessageEncodingBindingElement.WriteEncoding.WebName);
									instance.SetProperty("MaxReadPoolSize", textMessageEncodingBindingElement.MaxReadPoolSize);
									instance.SetProperty("MaxWritePoolSize", textMessageEncodingBindingElement.MaxWritePoolSize);
									if (textMessageEncodingBindingElement.ReaderQuotas != null)
									{
										EndpointInstanceProvider.FillReaderQuotas(instance, textMessageEncodingBindingElement.ReaderQuotas);
										return;
									}
								}
								else if (bindingElement is MtomMessageEncodingBindingElement)
								{
									MtomMessageEncodingBindingElement mtomMessageEncodingBindingElement = (MtomMessageEncodingBindingElement)bindingElement;
									instance.SetProperty("Encoding", mtomMessageEncodingBindingElement.WriteEncoding.WebName);
									instance.SetProperty("MessageVersion", mtomMessageEncodingBindingElement.MessageVersion.ToString());
									instance.SetProperty("MaxReadPoolSize", mtomMessageEncodingBindingElement.MaxReadPoolSize);
									instance.SetProperty("MaxWritePoolSize", mtomMessageEncodingBindingElement.MaxWritePoolSize);
									if (mtomMessageEncodingBindingElement.ReaderQuotas != null)
									{
										EndpointInstanceProvider.FillReaderQuotas(instance, mtomMessageEncodingBindingElement.ReaderQuotas);
										return;
									}
								}
							}
							else
							{
								if (bindingElement is TransactionFlowBindingElement)
								{
									TransactionFlowBindingElement transactionFlowBindingElement = (TransactionFlowBindingElement)bindingElement;
									instance.SetProperty("TransactionFlow", transactionFlowBindingElement.Transactions);
									instance.SetProperty("TransactionProtocol", transactionFlowBindingElement.TransactionProtocol.ToString());
									instance.SetProperty("AllowWildcardAction", transactionFlowBindingElement.AllowWildcardAction);
									return;
								}
								if (bindingElement is PrivacyNoticeBindingElement)
								{
									PrivacyNoticeBindingElement privacyNoticeBindingElement = (PrivacyNoticeBindingElement)bindingElement;
									instance.SetProperty("Url", privacyNoticeBindingElement.Url.ToString());
									instance.SetProperty("PrivacyNoticeVersion", privacyNoticeBindingElement.Version);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06002AB3 RID: 10931 RVA: 0x000A6758 File Offset: 0x000A4958
		private static void FillBehaviorsInfo(EndpointInfo info, IWmiInstance instance)
		{
			List<IWmiInstance> list = new List<IWmiInstance>(info.Behaviors.Count);
			foreach (IEndpointBehavior behavior in info.Behaviors)
			{
				IWmiInstance wmiInstance;
				EndpointInstanceProvider.FillBehaviorInfo(behavior, instance, out wmiInstance);
				if (wmiInstance != null)
				{
					list.Add(wmiInstance);
				}
			}
			instance.SetProperty("Behaviors", list.ToArray());
		}

		// Token: 0x06002AB4 RID: 10932 RVA: 0x000A67D4 File Offset: 0x000A49D4
		private static void FillBehaviorInfo(IEndpointBehavior behavior, IWmiInstance existingInstance, out IWmiInstance instance)
		{
			instance = null;
			if (behavior is ClientCredentials)
			{
				instance = existingInstance.NewInstance("ClientCredentials");
				ClientCredentials clientCredentials = (ClientCredentials)behavior;
				instance.SetProperty("SupportInteractive", clientCredentials.SupportInteractive);
				if (clientCredentials.ClientCertificate != null && clientCredentials.ClientCertificate.Certificate != null)
				{
					instance.SetProperty("ClientCertificate", clientCredentials.ClientCertificate.Certificate.ToString());
				}
				if (clientCredentials.IssuedToken != null)
				{
					string value = string.Format(CultureInfo.InvariantCulture, "{0}: {1}", new object[]
					{
						"CacheIssuedTokens",
						clientCredentials.IssuedToken.CacheIssuedTokens
					});
					instance.SetProperty("IssuedToken", value);
				}
				if (clientCredentials.HttpDigest != null)
				{
					string value2 = string.Format(CultureInfo.InvariantCulture, "{0}: {1}", new object[]
					{
						"AllowedImpersonationLevel",
						clientCredentials.HttpDigest.AllowedImpersonationLevel.ToString()
					});
					instance.SetProperty("HttpDigest", value2);
				}
				if (clientCredentials.Peer != null && clientCredentials.Peer.Certificate != null)
				{
					instance.SetProperty("Peer", clientCredentials.Peer.Certificate.ToString(true));
				}
				if (clientCredentials.UserName != null)
				{
					instance.SetProperty("UserName", "********");
				}
				if (clientCredentials.Windows != null)
				{
					string value3 = string.Format(CultureInfo.InvariantCulture, "{0}: {1}, {2}: {3}", new object[]
					{
						"AllowedImpersonationLevel",
						clientCredentials.Windows.AllowedImpersonationLevel.ToString(),
						"AllowNtlm",
						clientCredentials.Windows.AllowNtlm
					});
					instance.SetProperty("Windows", value3);
				}
			}
			else if (behavior is MustUnderstandBehavior)
			{
				instance = existingInstance.NewInstance("MustUnderstandBehavior");
			}
			else if (behavior is SynchronousReceiveBehavior)
			{
				instance = existingInstance.NewInstance("SynchronousReceiveBehavior");
			}
			else if (behavior is DispatcherSynchronizationBehavior)
			{
				instance = existingInstance.NewInstance("DispatcherSynchronizationBehavior");
			}
			else if (behavior is TransactedBatchingBehavior)
			{
				instance = existingInstance.NewInstance("TransactedBatchingBehavior");
				instance.SetProperty("MaxBatchSize", ((TransactedBatchingBehavior)behavior).MaxBatchSize);
			}
			else if (behavior is ClientViaBehavior)
			{
				instance = existingInstance.NewInstance("ClientViaBehavior");
				instance.SetProperty("Uri", ((ClientViaBehavior)behavior).Uri.ToString());
			}
			else if (behavior is IWmiInstanceProvider)
			{
				IWmiInstanceProvider wmiInstanceProvider = (IWmiInstanceProvider)behavior;
				instance = existingInstance.NewInstance(wmiInstanceProvider.GetInstanceType());
				wmiInstanceProvider.FillInstance(instance);
			}
			else
			{
				instance = existingInstance.NewInstance("Behavior");
			}
			if (instance != null)
			{
				instance.SetProperty("Type", behavior.GetType().FullName);
			}
		}

		// Token: 0x06002AB5 RID: 10933 RVA: 0x000A6AA8 File Offset: 0x000A4CA8
		private static void FillReaderQuotas(IWmiInstance instance, XmlDictionaryReaderQuotas readerQuotas)
		{
			IWmiInstance wmiInstance = instance.NewInstance("XmlDictionaryReaderQuotas");
			wmiInstance.SetProperty("MaxArrayLength", readerQuotas.MaxArrayLength);
			wmiInstance.SetProperty("MaxBytesPerRead", readerQuotas.MaxBytesPerRead);
			wmiInstance.SetProperty("MaxDepth", readerQuotas.MaxDepth);
			wmiInstance.SetProperty("MaxNameTableCharCount", readerQuotas.MaxNameTableCharCount);
			wmiInstance.SetProperty("MaxStringContentLength", readerQuotas.MaxStringContentLength);
			instance.SetProperty("ReaderQuotas", wmiInstance);
		}

		// Token: 0x06002AB6 RID: 10934 RVA: 0x000A6B3C File Offset: 0x000A4D3C
		private static void FillExtendedProtectionPolicy(IWmiInstance instance, ExtendedProtectionPolicy policy)
		{
			IWmiInstance wmiInstance = instance.NewInstance("ExtendedProtectionPolicy");
			wmiInstance.SetProperty("PolicyEnforcement", policy.PolicyEnforcement.ToString());
			wmiInstance.SetProperty("ProtectionScenario", policy.ProtectionScenario.ToString());
			if (policy.CustomServiceNames != null)
			{
				List<string> list = new List<string>(policy.CustomServiceNames.Count);
				foreach (object obj in policy.CustomServiceNames)
				{
					string item = (string)obj;
					list.Add(item);
				}
				wmiInstance.SetProperty("CustomServiceNames", list.ToArray());
			}
			if (policy.CustomChannelBinding != null)
			{
				wmiInstance.SetProperty("CustomChannelBinding", policy.CustomChannelBinding.GetType().ToString());
			}
			instance.SetProperty("ExtendedProtectionPolicy", wmiInstance);
		}

		// Token: 0x06002AB7 RID: 10935 RVA: 0x000A6C44 File Offset: 0x000A4E44
		void IWmiProvider.EnumInstances(IWmiInstances instances)
		{
			int processId = AppDomainInfo.Current.ProcessId;
			int id = AppDomainInfo.Current.Id;
			foreach (ServiceInfo serviceInfo in new ServiceInfoCollection(ManagementExtension.Services))
			{
				foreach (EndpointInfo endpoint in serviceInfo.Endpoints)
				{
					IWmiInstance wmiInstance = instances.NewInstance(null);
					wmiInstance.SetProperty("ProcessId", processId);
					wmiInstance.SetProperty("AppDomainId", id);
					EndpointInstanceProvider.FillEndpointInfo(endpoint, wmiInstance);
					instances.AddInstance(wmiInstance);
				}
			}
		}

		// Token: 0x06002AB8 RID: 10936 RVA: 0x000A6D20 File Offset: 0x000A4F20
		bool IWmiProvider.GetInstance(IWmiInstance instance)
		{
			bool result = false;
			if (this.OwnInstance(instance))
			{
				string address = (string)instance.GetProperty("ListenUri");
				string contractName = (string)instance.GetProperty("ContractName");
				EndpointInfo endpointInfo = this.FindEndpoint(address, contractName);
				if (endpointInfo != null)
				{
					EndpointInstanceProvider.FillEndpointInfo(endpointInfo, instance);
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06002AB9 RID: 10937 RVA: 0x000A6D70 File Offset: 0x000A4F70
		bool IWmiProvider.InvokeMethod(IWmiMethodContext method)
		{
			bool flag = this.OwnInstance(method.Instance);
			if (flag)
			{
				if (!(method.MethodName == "GetOperationCounterInstanceName"))
				{
					throw new WbemInvalidMethodException();
				}
				object parameter = method.GetParameter("Operation");
				string text = parameter as string;
				if (string.IsNullOrEmpty(text))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WbemInvalidParameterException("Operation"));
				}
				string operationCounterInstanceName = this.GetOperationCounterInstanceName(text, method.Instance);
				method.ReturnParameter = operationCounterInstanceName;
			}
			return flag;
		}

		// Token: 0x06002ABA RID: 10938 RVA: 0x000A6DF0 File Offset: 0x000A4FF0
		private EndpointInfo FindEndpoint(string address, string contractName)
		{
			foreach (ServiceInfo serviceInfo in new ServiceInfoCollection(ManagementExtension.Services))
			{
				foreach (EndpointInfo endpointInfo in serviceInfo.Endpoints)
				{
					if (null != endpointInfo.ListenUri && string.Equals(endpointInfo.ListenUri.ToString(), address, StringComparison.OrdinalIgnoreCase) && endpointInfo.Contract != null && endpointInfo.Contract.Name != null && string.CompareOrdinal(endpointInfo.Contract.Name, contractName) == 0)
					{
						return endpointInfo;
					}
				}
			}
			return null;
		}

		// Token: 0x06002ABB RID: 10939 RVA: 0x000A6EC8 File Offset: 0x000A50C8
		private string GetOperationCounterInstanceName(string operationName, IWmiInstance endpointInstance)
		{
			string address = (string)endpointInstance.GetProperty("ListenUri");
			string contractName = (string)endpointInstance.GetProperty("ContractName");
			EndpointInfo endpointInfo = this.FindEndpoint(address, contractName);
			string result = string.Empty;
			if (PerformanceCounters.PerformanceCountersEnabled && endpointInfo != null)
			{
				result = OperationPerformanceCountersBase.GetFriendlyInstanceName(endpointInfo.ServiceName, endpointInfo.Contract.Name, operationName, endpointInfo.Address.AbsoluteUri.ToUpperInvariant());
			}
			return result;
		}

		// Token: 0x06002ABC RID: 10940 RVA: 0x000A6F39 File Offset: 0x000A5139
		private bool OwnInstance(IWmiInstance instance)
		{
			return (int)instance.GetProperty("ProcessId") == AppDomainInfo.Current.ProcessId && (int)instance.GetProperty("AppDomainId") == AppDomainInfo.Current.Id;
		}
	}
}
