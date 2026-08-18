using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Activation;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Diagnostics;

namespace System.ServiceModel.Administration
{
	// Token: 0x0200044F RID: 1103
	internal class ServiceInstanceProvider : ProviderBase, IWmiProvider
	{
		// Token: 0x06002ADF RID: 10975 RVA: 0x000A76F8 File Offset: 0x000A58F8
		void IWmiProvider.EnumInstances(IWmiInstances instances)
		{
			int processId = AppDomainInfo.Current.ProcessId;
			foreach (ServiceInfo serviceInfo in new ServiceInfoCollection(ManagementExtension.Services))
			{
				IWmiInstance wmiInstance = instances.NewInstance(null);
				wmiInstance.SetProperty("DistinguishedName", serviceInfo.DistinguishedName);
				wmiInstance.SetProperty("ProcessId", processId);
				this.FillServiceInfo(serviceInfo, wmiInstance);
				instances.AddInstance(wmiInstance);
			}
		}

		// Token: 0x06002AE0 RID: 10976 RVA: 0x000A7788 File Offset: 0x000A5988
		bool IWmiProvider.GetInstance(IWmiInstance instance)
		{
			bool result = false;
			if ((int)instance.GetProperty("ProcessId") == AppDomainInfo.Current.ProcessId)
			{
				foreach (ServiceInfo serviceInfo in new ServiceInfoCollection(ManagementExtension.Services))
				{
					if (string.Equals((string)instance.GetProperty("DistinguishedName"), serviceInfo.DistinguishedName, StringComparison.OrdinalIgnoreCase))
					{
						this.FillServiceInfo(serviceInfo, instance);
						result = true;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x06002AE1 RID: 10977 RVA: 0x000A781C File Offset: 0x000A5A1C
		internal static string GetReference(ServiceInfo serviceInfo)
		{
			return string.Format(CultureInfo.InvariantCulture, "Service.DistinguishedName='{0}',ProcessId={1}", new object[]
			{
				serviceInfo.DistinguishedName,
				AppDomainInfo.Current.ProcessId
			});
		}

		// Token: 0x06002AE2 RID: 10978 RVA: 0x000A7850 File Offset: 0x000A5A50
		internal static IWmiInstance GetAppDomainInfo(IWmiInstance instance)
		{
			IWmiInstance wmiInstance = instance.NewInstance("AppDomainInfo");
			if (wmiInstance != null)
			{
				AppDomainInstanceProvider.FillAppDomainInfo(wmiInstance);
			}
			return wmiInstance;
		}

		// Token: 0x06002AE3 RID: 10979 RVA: 0x000A7874 File Offset: 0x000A5A74
		private void FillBehaviorsInfo(ServiceInfo info, IWmiInstance instance)
		{
			List<IWmiInstance> list = new List<IWmiInstance>(info.Behaviors.Count);
			foreach (IServiceBehavior behavior in info.Behaviors)
			{
				IWmiInstance wmiInstance;
				this.FillBehaviorInfo(behavior, instance, out wmiInstance);
				if (wmiInstance != null)
				{
					list.Add(wmiInstance);
				}
			}
			instance.SetProperty("Behaviors", list.ToArray());
		}

		// Token: 0x06002AE4 RID: 10980 RVA: 0x000A78F0 File Offset: 0x000A5AF0
		private void FillChannelsInfo(ServiceInfo info, IWmiInstance instance)
		{
			int num = 0;
			List<IWmiInstance> list = new List<IWmiInstance>();
			IEnumerable<InstanceContext> instanceContexts = info.Service.GetInstanceContexts();
			foreach (InstanceContext instanceContext in instanceContexts)
			{
				object thisLock = instanceContext.ThisLock;
				lock (thisLock)
				{
					num += instanceContext.WmiChannels.Count;
					foreach (IChannel channel in instanceContext.WmiChannels)
					{
						IWmiInstance wmiInstance = instance.NewInstance("Channel");
						this.FillChannelInfo(channel, wmiInstance);
						list.Add(wmiInstance);
					}
				}
			}
			instance.SetProperty("OutgoingChannels", list.ToArray());
		}

		// Token: 0x06002AE5 RID: 10981 RVA: 0x000A79F4 File Offset: 0x000A5BF4
		private static void FillExtensionsInfo(ServiceInfo info, IWmiInstance instance)
		{
			ProviderBase.FillCollectionInfo(info.Service.Extensions, instance, "Extensions");
		}

		// Token: 0x06002AE6 RID: 10982 RVA: 0x000A7A0C File Offset: 0x000A5C0C
		private void FillServiceInfo(ServiceInfo info, IWmiInstance instance)
		{
			ProviderBase.FillCollectionInfo(info.Service.BaseAddresses, instance, "BaseAddresses");
			instance.SetProperty("CounterInstanceName", PerformanceCounters.PerformanceCountersEnabled ? ServicePerformanceCountersBase.GetFriendlyInstanceName(info.Service) : string.Empty);
			instance.SetProperty("ConfigurationName", info.ConfigurationName);
			instance.SetProperty("DistinguishedName", info.DistinguishedName);
			instance.SetProperty("Name", info.Name);
			instance.SetProperty("Namespace", info.Namespace);
			instance.SetProperty("Metadata", info.Metadata);
			instance.SetProperty("Opened", ManagementExtension.GetTimeOpened(info.Service));
			this.FillBehaviorsInfo(info, instance);
			ServiceInstanceProvider.FillExtensionsInfo(info, instance);
			this.FillChannelsInfo(info, instance);
		}

		// Token: 0x06002AE7 RID: 10983 RVA: 0x000A7ADC File Offset: 0x000A5CDC
		private void FillBehaviorInfo(IServiceBehavior behavior, IWmiInstance existingInstance, out IWmiInstance instance)
		{
			instance = null;
			if (behavior is AspNetCompatibilityRequirementsAttribute)
			{
				instance = existingInstance.NewInstance("AspNetCompatibilityRequirementsAttribute");
				AspNetCompatibilityRequirementsAttribute aspNetCompatibilityRequirementsAttribute = (AspNetCompatibilityRequirementsAttribute)behavior;
				instance.SetProperty("RequirementsMode", aspNetCompatibilityRequirementsAttribute.RequirementsMode.ToString());
			}
			else if (behavior is ServiceCredentials)
			{
				instance = existingInstance.NewInstance("ServiceCredentials");
				ServiceCredentials serviceCredentials = (ServiceCredentials)behavior;
				if (serviceCredentials.ClientCertificate != null && serviceCredentials.ClientCertificate.Certificate != null)
				{
					string text = string.Empty;
					text += string.Format(CultureInfo.InvariantCulture, "Certificate: {0}\n", new object[]
					{
						serviceCredentials.ClientCertificate.Certificate
					});
					instance.SetProperty("ClientCertificate", text);
				}
				if (serviceCredentials.IssuedTokenAuthentication != null && serviceCredentials.IssuedTokenAuthentication.KnownCertificates != null)
				{
					string text2 = string.Empty;
					text2 += string.Format(CultureInfo.InvariantCulture, "AllowUntrustedRsaIssuers: {0}\n", new object[]
					{
						serviceCredentials.IssuedTokenAuthentication.AllowUntrustedRsaIssuers
					});
					text2 += string.Format(CultureInfo.InvariantCulture, "CertificateValidationMode: {0}\n", new object[]
					{
						serviceCredentials.IssuedTokenAuthentication.CertificateValidationMode
					});
					text2 += string.Format(CultureInfo.InvariantCulture, "RevocationMode: {0}\n", new object[]
					{
						serviceCredentials.IssuedTokenAuthentication.RevocationMode
					});
					text2 += string.Format(CultureInfo.InvariantCulture, "TrustedStoreLocation: {0}\n", new object[]
					{
						serviceCredentials.IssuedTokenAuthentication.TrustedStoreLocation
					});
					foreach (X509Certificate2 x509Certificate in serviceCredentials.IssuedTokenAuthentication.KnownCertificates)
					{
						if (x509Certificate != null)
						{
							text2 += string.Format(CultureInfo.InvariantCulture, "Known certificate: {0}\n", new object[]
							{
								x509Certificate.FriendlyName
							});
						}
					}
					text2 += string.Format(CultureInfo.InvariantCulture, "AudienceUriMode: {0}\n", new object[]
					{
						serviceCredentials.IssuedTokenAuthentication.AudienceUriMode
					});
					if (serviceCredentials.IssuedTokenAuthentication.AllowedAudienceUris != null)
					{
						foreach (string text3 in serviceCredentials.IssuedTokenAuthentication.AllowedAudienceUris)
						{
							if (text3 != null)
							{
								text2 += string.Format(CultureInfo.InvariantCulture, "Allowed Uri: {0}\n", new object[]
								{
									text3
								});
							}
						}
					}
					instance.SetProperty("IssuedTokenAuthentication", text2);
				}
				if (serviceCredentials.Peer != null && serviceCredentials.Peer.Certificate != null)
				{
					string text4 = string.Empty;
					text4 += string.Format(CultureInfo.InvariantCulture, "Certificate: {0}\n", new object[]
					{
						serviceCredentials.Peer.Certificate.ToString(true)
					});
					instance.SetProperty("Peer", text4);
				}
				if (serviceCredentials.SecureConversationAuthentication != null && serviceCredentials.SecureConversationAuthentication.SecurityContextClaimTypes != null)
				{
					string text5 = string.Empty;
					foreach (Type type in serviceCredentials.SecureConversationAuthentication.SecurityContextClaimTypes)
					{
						if (type != null)
						{
							text5 += string.Format(CultureInfo.InvariantCulture, "ClaimType: {0}\n", new object[]
							{
								type
							});
						}
					}
					instance.SetProperty("SecureConversationAuthentication", text5);
				}
				if (serviceCredentials.ServiceCertificate != null && serviceCredentials.ServiceCertificate.Certificate != null)
				{
					instance.SetProperty("ServiceCertificate", serviceCredentials.ServiceCertificate.Certificate.ToString());
				}
				if (serviceCredentials.UserNameAuthentication != null)
				{
					instance.SetProperty("UserNameAuthentication", string.Format(CultureInfo.InvariantCulture, "{0}: {1}", new object[]
					{
						"ValidationMode",
						serviceCredentials.UserNameAuthentication.UserNamePasswordValidationMode.ToString()
					}));
				}
				if (serviceCredentials.WindowsAuthentication != null)
				{
					instance.SetProperty("WindowsAuthentication", string.Format(CultureInfo.InvariantCulture, "{0}: {1}", new object[]
					{
						"AllowAnonymous",
						serviceCredentials.WindowsAuthentication.AllowAnonymousLogons.ToString()
					}));
				}
			}
			else if (behavior is ServiceAuthorizationBehavior)
			{
				instance = existingInstance.NewInstance("ServiceAuthorizationBehavior");
				ServiceAuthorizationBehavior serviceAuthorizationBehavior = (ServiceAuthorizationBehavior)behavior;
				instance.SetProperty("ImpersonateCallerForAllOperations", serviceAuthorizationBehavior.ImpersonateCallerForAllOperations);
				instance.SetProperty("ImpersonateOnSerializingReply", serviceAuthorizationBehavior.ImpersonateOnSerializingReply);
				if (serviceAuthorizationBehavior.RoleProvider != null)
				{
					instance.SetProperty("RoleProvider", serviceAuthorizationBehavior.RoleProvider.ToString());
				}
				if (serviceAuthorizationBehavior.ServiceAuthorizationManager != null)
				{
					instance.SetProperty("ServiceAuthorizationManager", serviceAuthorizationBehavior.ServiceAuthorizationManager.ToString());
				}
				instance.SetProperty("PrincipalPermissionMode", serviceAuthorizationBehavior.PrincipalPermissionMode.ToString());
			}
			else if (behavior is ServiceSecurityAuditBehavior)
			{
				instance = existingInstance.NewInstance("ServiceSecurityAuditBehavior");
				ServiceSecurityAuditBehavior serviceSecurityAuditBehavior = (ServiceSecurityAuditBehavior)behavior;
				instance.SetProperty("AuditLogLocation", serviceSecurityAuditBehavior.AuditLogLocation.ToString());
				instance.SetProperty("SuppressAuditFailure", serviceSecurityAuditBehavior.SuppressAuditFailure);
				instance.SetProperty("ServiceAuthorizationAuditLevel", serviceSecurityAuditBehavior.ServiceAuthorizationAuditLevel.ToString());
				instance.SetProperty("MessageAuthenticationAuditLevel", serviceSecurityAuditBehavior.MessageAuthenticationAuditLevel.ToString());
			}
			else if (behavior is ServiceBehaviorAttribute)
			{
				instance = existingInstance.NewInstance("ServiceBehaviorAttribute");
				ServiceBehaviorAttribute serviceBehaviorAttribute = (ServiceBehaviorAttribute)behavior;
				instance.SetProperty("AddressFilterMode", serviceBehaviorAttribute.AddressFilterMode.ToString());
				instance.SetProperty("AutomaticSessionShutdown", serviceBehaviorAttribute.AutomaticSessionShutdown);
				instance.SetProperty("ConcurrencyMode", serviceBehaviorAttribute.ConcurrencyMode.ToString());
				instance.SetProperty("ConfigurationName", serviceBehaviorAttribute.ConfigurationName);
				instance.SetProperty("EnsureOrderedDispatch", serviceBehaviorAttribute.EnsureOrderedDispatch);
				instance.SetProperty("IgnoreExtensionDataObject", serviceBehaviorAttribute.IgnoreExtensionDataObject);
				instance.SetProperty("IncludeExceptionDetailInFaults", serviceBehaviorAttribute.IncludeExceptionDetailInFaults);
				instance.SetProperty("InstanceContextMode", serviceBehaviorAttribute.InstanceContextMode.ToString());
				instance.SetProperty("MaxItemsInObjectGraph", serviceBehaviorAttribute.MaxItemsInObjectGraph);
				instance.SetProperty("Name", serviceBehaviorAttribute.Name);
				instance.SetProperty("Namespace", serviceBehaviorAttribute.Namespace);
				instance.SetProperty("ReleaseServiceInstanceOnTransactionComplete", serviceBehaviorAttribute.ReleaseServiceInstanceOnTransactionComplete);
				instance.SetProperty("TransactionAutoCompleteOnSessionClose", serviceBehaviorAttribute.TransactionAutoCompleteOnSessionClose);
				instance.SetProperty("TransactionIsolationLevel", serviceBehaviorAttribute.TransactionIsolationLevel.ToString());
				if (serviceBehaviorAttribute.TransactionTimeoutSet)
				{
					instance.SetProperty("TransactionTimeout", serviceBehaviorAttribute.TransactionTimeoutTimespan);
				}
				instance.SetProperty("UseSynchronizationContext", serviceBehaviorAttribute.UseSynchronizationContext);
				instance.SetProperty("ValidateMustUnderstand", serviceBehaviorAttribute.ValidateMustUnderstand);
			}
			else if (behavior is ServiceDebugBehavior)
			{
				instance = existingInstance.NewInstance("ServiceDebugBehavior");
				ServiceDebugBehavior serviceDebugBehavior = (ServiceDebugBehavior)behavior;
				if (null != serviceDebugBehavior.HttpHelpPageUrl)
				{
					instance.SetProperty("HttpHelpPageUrl", serviceDebugBehavior.HttpHelpPageUrl.ToString());
				}
				instance.SetProperty("HttpHelpPageEnabled", serviceDebugBehavior.HttpHelpPageEnabled);
				if (null != serviceDebugBehavior.HttpsHelpPageUrl)
				{
					instance.SetProperty("HttpsHelpPageUrl", serviceDebugBehavior.HttpsHelpPageUrl.ToString());
				}
				instance.SetProperty("HttpsHelpPageEnabled", serviceDebugBehavior.HttpsHelpPageEnabled);
				instance.SetProperty("IncludeExceptionDetailInFaults", serviceDebugBehavior.IncludeExceptionDetailInFaults);
			}
			else if (behavior is ServiceMetadataBehavior)
			{
				instance = existingInstance.NewInstance("ServiceMetadataBehavior");
				ServiceMetadataBehavior serviceMetadataBehavior = (ServiceMetadataBehavior)behavior;
				if (null != serviceMetadataBehavior.ExternalMetadataLocation)
				{
					instance.SetProperty("ExternalMetadataLocation", serviceMetadataBehavior.ExternalMetadataLocation.ToString());
				}
				instance.SetProperty("HttpGetEnabled", serviceMetadataBehavior.HttpGetEnabled);
				if (null != serviceMetadataBehavior.HttpGetUrl)
				{
					instance.SetProperty("HttpGetUrl", serviceMetadataBehavior.HttpGetUrl.ToString());
				}
				instance.SetProperty("HttpsGetEnabled", serviceMetadataBehavior.HttpsGetEnabled);
				if (null != serviceMetadataBehavior.HttpsGetUrl)
				{
					instance.SetProperty("HttpsGetUrl", serviceMetadataBehavior.HttpsGetUrl.ToString());
				}
				this.FillMetadataExporterInfo(instance, serviceMetadataBehavior.MetadataExporter);
			}
			else if (behavior is ServiceThrottlingBehavior)
			{
				instance = existingInstance.NewInstance("ServiceThrottlingBehavior");
				ServiceThrottlingBehavior serviceThrottlingBehavior = (ServiceThrottlingBehavior)behavior;
				instance.SetProperty("MaxConcurrentCalls", serviceThrottlingBehavior.MaxConcurrentCalls);
				instance.SetProperty("MaxConcurrentSessions", serviceThrottlingBehavior.MaxConcurrentSessions);
				instance.SetProperty("MaxConcurrentInstances", serviceThrottlingBehavior.MaxConcurrentInstances);
			}
			else if (behavior is ServiceTimeoutsBehavior)
			{
				instance = existingInstance.NewInstance("ServiceTimeoutsBehavior");
				ServiceTimeoutsBehavior serviceTimeoutsBehavior = (ServiceTimeoutsBehavior)behavior;
				instance.SetProperty("TransactionTimeout", serviceTimeoutsBehavior.TransactionTimeout);
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

		// Token: 0x06002AE8 RID: 10984 RVA: 0x000A852C File Offset: 0x000A672C
		private void FillMetadataExporterInfo(IWmiInstance instance, MetadataExporter exporter)
		{
			DiagnosticUtility.EventLog.LogEvent(TraceEventType.Information, 9, 3221356552U, true, new string[]
			{
				"metadata exporter called"
			});
			IWmiInstance wmiInstance = instance.NewInstance("MetadataExporter");
			wmiInstance.SetProperty("PolicyVersion", exporter.PolicyVersion.ToString());
			instance.SetProperty("MetadataExportInfo", wmiInstance);
		}

		// Token: 0x06002AE9 RID: 10985 RVA: 0x000A8588 File Offset: 0x000A6788
		private void FillChannelInfo(IChannel channel, IWmiInstance instance)
		{
			instance.SetProperty("Type", channel.GetType().ToString());
			ServiceChannel serviceChannel = ServiceChannelFactory.GetServiceChannel(channel);
			if (serviceChannel != null)
			{
				string text = (serviceChannel.RemoteAddress == null) ? string.Empty : serviceChannel.RemoteAddress.ToString();
				instance.SetProperty("RemoteAddress", text);
				string contractName = (serviceChannel.ClientRuntime != null) ? serviceChannel.ClientRuntime.ContractName : string.Empty;
				string value = EndpointInstanceProvider.EndpointReference(text, contractName, false);
				instance.SetProperty("RemoteEndpoint", value);
				instance.SetProperty("LocalAddress", (serviceChannel.LocalAddress == null) ? string.Empty : serviceChannel.LocalAddress.ToString());
				instance.SetProperty("SessionId", ((IContextChannel)serviceChannel).SessionId);
			}
		}
	}
}
