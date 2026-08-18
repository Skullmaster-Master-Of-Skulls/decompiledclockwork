using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Security.Tokens;
using TechnoPro.Common.WCF.Adapters;
using TechnoPro.Common.WCF.Attributes;
using TechnoPro.Common.WCF.Configuration;

namespace TechnoPro.Common.WCF
{
	// Token: 0x0200000F RID: 15
	public class ClockWorkServerBaseServiceHost : ServiceHost
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000053 RID: 83 RVA: 0x000033EA File Offset: 0x000015EA
		// (set) Token: 0x06000054 RID: 84 RVA: 0x000033F2 File Offset: 0x000015F2
		public string ServiceName { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000055 RID: 85 RVA: 0x000033FB File Offset: 0x000015FB
		public string BehaviorConfigurationName
		{
			get
			{
				return (this.ContractType.GetCustomAttribute<NoSslCertificateAttribute>() == null) ? "Certificate.Behavior" : "Default.Behavior";
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00003416 File Offset: 0x00001616
		public string ContractName
		{
			get
			{
				return this.ServiceName.GetContractName();
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00003423 File Offset: 0x00001623
		public Type ContractType
		{
			get
			{
				return this.ServiceName.GetContractType();
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003430 File Offset: 0x00001630
		public ClockWorkServerBaseServiceHost()
		{
		}

		// Token: 0x06000059 RID: 89 RVA: 0x0000343A File Offset: 0x0000163A
		public ClockWorkServerBaseServiceHost(Type serviceType, params Uri[] baseAddresses) : base(serviceType, baseAddresses)
		{
			this.ServiceName = serviceType.Name;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003453 File Offset: 0x00001653
		public ClockWorkServerBaseServiceHost(string serviceName, params Uri[] baseAddresses) : base(ClockWorkServerBaseServiceHost.CreateServiceType(serviceName), baseAddresses)
		{
			this.ServiceName = serviceName;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x0000346C File Offset: 0x0000166C
		protected static Type CreateServiceType(string serviceName)
		{
			return Type.GetType("TechnoPro.ClockWorkServer.Services." + serviceName + ", ClockWorkServer.Services");
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003493 File Offset: 0x00001693
		protected override void OnOpening()
		{
			this.AddBehaviors();
			this.AddEndpoints();
			this.ApplySecurityTransformations();
			base.OnOpening();
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000034B4 File Offset: 0x000016B4
		protected override void ApplyConfiguration()
		{
			base.ApplyConfiguration();
			bool flag = !string.IsNullOrEmpty(this.ServiceName);
			if (flag)
			{
				base.Description.ConfigurationName = this.ServiceName;
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000034F0 File Offset: 0x000016F0
		protected virtual void AddEndpoints()
		{
			bool flag = base.Description.Endpoints.All((ServiceEndpoint e) => e.Contract.Name != "IMetadataExchange");
			if (flag)
			{
				base.AddServiceEndpoint("IMetadataExchange", MetadataExchangeBindings.CreateMexHttpBinding(), "mex");
			}
			this.AddHttpEndpoint();
			this.AddNetTcpEndpoint();
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003558 File Offset: 0x00001758
		protected virtual void AddNetTcpEndpoint()
		{
			string contractName = this.ContractName;
			Type contractType = this.ContractType;
			bool flag = contractType != null && base.Description.Endpoints.All((ServiceEndpoint e) => e.Contract.Name != contractName || !e.Binding.Name.Equals("NetTcpBinding", StringComparison.OrdinalIgnoreCase));
			if (flag)
			{
				base.AddServiceEndpoint(contractType, contractType.GetNetTcpBinding(SecurityMode.Message), "netTcp");
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000035C0 File Offset: 0x000017C0
		protected virtual void AddHttpEndpoint()
		{
			string contractName = this.ContractName;
			Type contractType = this.ContractType;
			bool flag = contractType == null;
			if (!flag)
			{
				bool flag2 = contractType.GetCustomAttribute<DualChannelServiceAttribute>() != null;
				if (flag2)
				{
					bool flag3 = base.Description.Endpoints.All((ServiceEndpoint e) => e.Contract.Name != contractName || !e.Binding.Name.Equals("WSDualHttpBinding", StringComparison.OrdinalIgnoreCase));
					if (flag3)
					{
						base.AddServiceEndpoint(contractType, contractType.GetHttpBinding(), "wsDualHttp");
					}
				}
				else
				{
					bool flag4 = contractType.GetCustomAttribute<NoSslCertificateAttribute>() != null;
					if (flag4)
					{
						bool flag5 = base.Description.Endpoints.All((ServiceEndpoint e) => e.Contract.Name != contractName || !e.Binding.Name.Equals("BasicHttpBinding", StringComparison.OrdinalIgnoreCase));
						if (flag5)
						{
							base.AddServiceEndpoint(contractType, contractType.GetHttpBinding(), "basicHttp");
							bool flag6 = contractName.Equals("IClientStartup");
							if (flag6)
							{
								base.AddServiceEndpoint(contractType, contractType.GetWsHttpBinding(), "wsHttp");
							}
						}
					}
					else
					{
						bool flag7 = base.Description.Endpoints.All((ServiceEndpoint e) => e.Contract.Name != contractName || !e.Binding.Name.Equals("WSHttpBinding", StringComparison.OrdinalIgnoreCase));
						if (flag7)
						{
							base.AddServiceEndpoint(contractType, contractType.GetHttpBinding(), "wsHttp");
						}
					}
				}
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000036E8 File Offset: 0x000018E8
		protected virtual void AddBehaviors()
		{
			this.AddConfigurationFileBehaviors();
			base.Description.Behaviors.RemoveAll<ServiceMetadataBehavior>();
			ServiceMetadataBehavior item = new ServiceMetadataBehavior
			{
				HttpGetEnabled = true,
				MetadataExporter = 
				{
					PolicyVersion = PolicyVersion.Policy15
				}
			};
			base.Description.Behaviors.Add(item);
			ServiceBehaviorAttribute serviceBehaviorAttribute = base.Description.Behaviors.Find<ServiceBehaviorAttribute>();
			bool flag = serviceBehaviorAttribute == null;
			if (flag)
			{
				serviceBehaviorAttribute = new ServiceBehaviorAttribute
				{
					Namespace = "http://tpro.ca",
					IncludeExceptionDetailInFaults = true
				};
				base.Description.Behaviors.Add(serviceBehaviorAttribute);
			}
			serviceBehaviorAttribute.Namespace = "http://tpro.ca";
			serviceBehaviorAttribute.IncludeExceptionDetailInFaults = true;
			bool flag2 = base.Description.Behaviors.Find<ErrorHandlerBehaviorAttribute>() == null;
			if (flag2)
			{
				base.Description.Behaviors.Add(new ErrorHandlerBehaviorAttribute());
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000037CC File Offset: 0x000019CC
		protected virtual void AddConfigurationFileBehaviors()
		{
			IList<IServiceBehavior> serviceBehaviorsByName = WCFConfigurationHelper.GetServiceBehaviorsByName(this.BehaviorConfigurationName);
			bool flag = serviceBehaviorsByName != null && serviceBehaviorsByName.Count > 0;
			if (flag)
			{
				using (IEnumerator<IServiceBehavior> enumerator = serviceBehaviorsByName.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						IServiceBehavior behavior = enumerator.Current;
						IServiceBehavior serviceBehavior = base.Description.Behaviors.FirstOrDefault((IServiceBehavior b) => b.GetType() == behavior.GetType());
						bool flag2 = serviceBehavior != null;
						if (flag2)
						{
							base.Description.Behaviors.Remove(serviceBehavior);
						}
						base.Description.Behaviors.Add(behavior);
					}
				}
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003894 File Offset: 0x00001A94
		private void ApplySecurityTransformations()
		{
			foreach (ServiceEndpoint endpoint in base.Description.Endpoints)
			{
				this.AdjustMaxClockSkew(endpoint, ClockWorkServerBaseServiceHost.MaxClockSkewSeconds);
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000038F4 File Offset: 0x00001AF4
		private void ApplyMaxClockSkew(SecurityBindingElement securityBindingElement, int maxClockSkewSecond)
		{
			securityBindingElement.LocalClientSettings.MaxClockSkew = new TimeSpan(0, 0, maxClockSkewSecond);
			securityBindingElement.LocalClientSettings.DetectReplays = false;
			securityBindingElement.LocalClientSettings.SessionKeyRenewalInterval = TimeSpan.MaxValue;
			securityBindingElement.LocalServiceSettings.MaxClockSkew = new TimeSpan(0, 0, maxClockSkewSecond);
			securityBindingElement.LocalServiceSettings.DetectReplays = false;
			securityBindingElement.LocalServiceSettings.SessionKeyRenewalInterval = TimeSpan.MaxValue;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003968 File Offset: 0x00001B68
		private void AdjustMaxClockSkew(ServiceEndpoint endpoint, int maxClockSkewSecond)
		{
			CustomBinding customBinding = new CustomBinding(endpoint.Binding);
			SecurityBindingElement securityBindingElement = customBinding.Elements.Find<SecurityBindingElement>();
			bool flag = securityBindingElement != null;
			if (flag)
			{
				int index = customBinding.Elements.IndexOf(securityBindingElement);
				this.ApplyMaxClockSkew(securityBindingElement, maxClockSkewSecond);
				bool flag2 = securityBindingElement is SymmetricSecurityBindingElement;
				if (flag2)
				{
					SymmetricSecurityBindingElement symmetricSecurityBindingElement = securityBindingElement as SymmetricSecurityBindingElement;
					bool flag3 = symmetricSecurityBindingElement.ProtectionTokenParameters != null && symmetricSecurityBindingElement.ProtectionTokenParameters is SecureConversationSecurityTokenParameters;
					if (flag3)
					{
						this.ApplyMaxClockSkew((symmetricSecurityBindingElement.ProtectionTokenParameters as SecureConversationSecurityTokenParameters).BootstrapSecurityBindingElement, maxClockSkewSecond);
					}
				}
				else
				{
					bool flag4 = securityBindingElement is TransportSecurityBindingElement;
					if (flag4)
					{
						TransportSecurityBindingElement transportSecurityBindingElement = securityBindingElement as TransportSecurityBindingElement;
						bool flag5 = transportSecurityBindingElement.EndpointSupportingTokenParameters != null && transportSecurityBindingElement.EndpointSupportingTokenParameters.Endorsing != null && transportSecurityBindingElement.EndpointSupportingTokenParameters.Endorsing.Count > 0 && transportSecurityBindingElement.EndpointSupportingTokenParameters.Endorsing[0] is SecureConversationSecurityTokenParameters;
						if (flag5)
						{
							this.ApplyMaxClockSkew((transportSecurityBindingElement.EndpointSupportingTokenParameters.Endorsing[0] as SecureConversationSecurityTokenParameters).BootstrapSecurityBindingElement, maxClockSkewSecond);
						}
					}
				}
				customBinding.Elements[index] = securityBindingElement;
				endpoint.Binding = customBinding;
			}
		}

		// Token: 0x04000015 RID: 21
		public static readonly int MaxClockSkewSeconds = (int)TimeSpan.FromMinutes(20.0).TotalSeconds;
	}
}
