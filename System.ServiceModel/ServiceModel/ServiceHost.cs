using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Reflection;
using System.Security;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.ServiceModel.Diagnostics;

namespace System.ServiceModel
{
	// Token: 0x02000101 RID: 257
	public class ServiceHost : ServiceHostBase
	{
		// Token: 0x060005AF RID: 1455 RVA: 0x00019D19 File Offset: 0x00017F19
		protected ServiceHost()
		{
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x00019D24 File Offset: 0x00017F24
		public ServiceHost(Type serviceType, params Uri[] baseAddresses)
		{
			if (serviceType == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("serviceType"));
			}
			this.serviceType = serviceType;
			using (ServiceModelActivity serviceModelActivity = DiagnosticUtility.ShouldUseActivity ? ServiceModelActivity.CreateBoundedActivity() : null)
			{
				if (DiagnosticUtility.ShouldUseActivity)
				{
					ServiceModelActivity.Start(serviceModelActivity, SR.GetString("ActivityConstructServiceHost", new object[]
					{
						serviceType.FullName
					}), ActivityType.Construct);
				}
				this.InitializeDescription(serviceType, new UriSchemeKeyedCollection(baseAddresses));
			}
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x00019DBC File Offset: 0x00017FBC
		public ServiceHost(object singletonInstance, params Uri[] baseAddresses)
		{
			if (singletonInstance == null)
			{
				throw new ArgumentNullException("singletonInstance");
			}
			this.singletonInstance = singletonInstance;
			this.serviceType = singletonInstance.GetType();
			using (ServiceModelActivity serviceModelActivity = DiagnosticUtility.ShouldUseActivity ? ServiceModelActivity.CreateBoundedActivity() : null)
			{
				if (DiagnosticUtility.ShouldUseActivity)
				{
					ServiceModelActivity.Start(serviceModelActivity, SR.GetString("ActivityConstructServiceHost", new object[]
					{
						this.serviceType.FullName
					}), ActivityType.Construct);
				}
				this.InitializeDescription(singletonInstance, new UriSchemeKeyedCollection(baseAddresses));
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060005B2 RID: 1458 RVA: 0x00019E58 File Offset: 0x00018058
		public object SingletonInstance
		{
			get
			{
				return this.singletonInstance;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060005B3 RID: 1459 RVA: 0x00019E60 File Offset: 0x00018060
		internal override object DisposableInstance
		{
			get
			{
				return this.disposableInstance;
			}
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x00019E68 File Offset: 0x00018068
		public ServiceEndpoint AddServiceEndpoint(Type implementedContract, Binding binding, string address)
		{
			return this.AddServiceEndpoint(implementedContract, binding, address, null);
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x00019E74 File Offset: 0x00018074
		public ServiceEndpoint AddServiceEndpoint(Type implementedContract, Binding binding, string address, Uri listenUri)
		{
			if (address == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("address"));
			}
			ServiceEndpoint serviceEndpoint = this.AddServiceEndpoint(implementedContract, binding, new Uri(address, UriKind.RelativeOrAbsolute));
			if (listenUri != null)
			{
				listenUri = base.MakeAbsoluteUri(listenUri, binding);
				serviceEndpoint.ListenUri = listenUri;
			}
			return serviceEndpoint;
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x00019EC7 File Offset: 0x000180C7
		public ServiceEndpoint AddServiceEndpoint(Type implementedContract, Binding binding, Uri address)
		{
			return this.AddServiceEndpoint(implementedContract, binding, address, null);
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x00019ED4 File Offset: 0x000180D4
		private void ValidateContractType(Type implementedContract, ServiceHost.ReflectedAndBehaviorContractCollection reflectedAndBehaviorContracts)
		{
			if (!implementedContract.IsDefined(typeof(ServiceContractAttribute), false))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SfxServiceContractAttributeNotFound", new object[]
				{
					implementedContract.FullName
				})));
			}
			if (reflectedAndBehaviorContracts.Contains(implementedContract))
			{
				return;
			}
			if (implementedContract == typeof(IMetadataExchange))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SfxReflectedContractKeyNotFoundIMetadataExchange", new object[]
				{
					this.serviceType.FullName
				})));
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SfxReflectedContractKeyNotFound2", new object[]
			{
				implementedContract.FullName,
				this.serviceType.FullName
			})));
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x00019FA0 File Offset: 0x000181A0
		public ServiceEndpoint AddServiceEndpoint(Type implementedContract, Binding binding, Uri address, Uri listenUri)
		{
			if (implementedContract == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("implementedContract"));
			}
			if (this.reflectedContracts == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SfxReflectedContractsNotInitialized1", new object[]
				{
					implementedContract.FullName
				})));
			}
			ServiceHost.ReflectedAndBehaviorContractCollection reflectedAndBehaviorContractCollection = new ServiceHost.ReflectedAndBehaviorContractCollection(this.reflectedContracts, base.Description.Behaviors);
			this.ValidateContractType(implementedContract, reflectedAndBehaviorContractCollection);
			ServiceEndpoint serviceEndpoint = base.AddServiceEndpoint(reflectedAndBehaviorContractCollection.GetConfigKey(implementedContract), binding, address);
			if (listenUri != null)
			{
				listenUri = base.MakeAbsoluteUri(listenUri, binding);
				serviceEndpoint.ListenUri = listenUri;
			}
			return serviceEndpoint;
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x0001A04C File Offset: 0x0001824C
		internal override void AddDefaultEndpoints(Binding defaultBinding, List<ServiceEndpoint> defaultEndpoints)
		{
			List<ContractDescription> list = new List<ContractDescription>();
			for (int i = 0; i < this.reflectedContracts.Count; i++)
			{
				bool flag = true;
				ContractDescription contractDescription = this.reflectedContracts[i];
				Type contractType = contractDescription.ContractType;
				if (contractType != null)
				{
					for (int j = 0; j < this.reflectedContracts.Count; j++)
					{
						ContractDescription contractDescription2 = this.reflectedContracts[j];
						Type contractType2 = contractDescription2.ContractType;
						if (i != j && !(contractType2 == null) && contractType.IsAssignableFrom(contractType2))
						{
							flag = false;
							break;
						}
					}
				}
				if (flag)
				{
					list.Add(contractDescription);
				}
			}
			foreach (ContractDescription contractDescription3 in list)
			{
				ServiceEndpoint serviceEndpoint = base.AddServiceEndpoint(contractDescription3.ConfigurationName, defaultBinding, string.Empty);
				ConfigLoader.LoadDefaultEndpointBehaviors(serviceEndpoint);
				defaultEndpoints.Add(serviceEndpoint);
			}
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x0001A154 File Offset: 0x00018354
		protected override void ApplyConfiguration()
		{
			Type left = base.Description.ServiceType;
			if (left != null)
			{
				MethodInfo configureMethod = ServiceHost.GetConfigureMethod(left);
				if (configureMethod != null)
				{
					ConfigLoader configLoader = new ConfigLoader(this.GetContractResolver(base.ImplementedContracts));
					this.LoadHostConfigurationInternal(configLoader, base.Description, base.Description.ConfigurationName);
					ServiceConfiguration configuration = new ServiceConfiguration(this);
					ServiceHost.InvokeConfigure(configureMethod, configuration);
					return;
				}
			}
			base.ApplyConfiguration();
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x0001A1C8 File Offset: 0x000183C8
		private static MethodInfo GetConfigureMethod(Type serviceType)
		{
			if (serviceType == typeof(object))
			{
				return null;
			}
			MethodInfo method = serviceType.GetMethod("Configure", BindingFlags.Static | BindingFlags.Public, null, new Type[]
			{
				typeof(ServiceConfiguration)
			}, null);
			if (method != null && method.ReturnType == typeof(void))
			{
				return method;
			}
			return ServiceHost.GetConfigureMethod(serviceType.BaseType);
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x0001A23C File Offset: 0x0001843C
		private static void InvokeConfigure(MethodInfo configureMethod, ServiceConfiguration configuration)
		{
			Action<ServiceConfiguration> action = Delegate.CreateDelegate(typeof(Action<ServiceConfiguration>), configureMethod) as Action<ServiceConfiguration>;
			action(configuration);
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x0001A268 File Offset: 0x00018468
		internal void LoadFromConfiguration()
		{
			if (base.Description == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxServiceHostBaseCannotApplyConfigurationWithoutDescription")));
			}
			ConfigLoader configLoader = new ConfigLoader(this.GetContractResolver(base.ImplementedContracts));
			this.LoadConfigurationSectionExceptHostInternal(configLoader, base.Description, base.Description.ConfigurationName);
			base.EnsureAuthenticationAuthorizationDebug(base.Description);
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x0001A2D0 File Offset: 0x000184D0
		internal void LoadFromConfiguration(Configuration configuration)
		{
			if (base.Description == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxServiceHostBaseCannotApplyConfigurationWithoutDescription")));
			}
			ConfigLoader configLoader = new ConfigLoader(this.GetContractResolver(base.ImplementedContracts));
			ServicesSection servicesSection = (ServicesSection)configuration.GetSection(ConfigurationStrings.ServicesSectionPath);
			ServiceElement serviceElement = configLoader.LookupService(base.Description.ConfigurationName, servicesSection);
			configLoader.LoadServiceDescription(this, base.Description, serviceElement, new Action<Uri>(base.LoadConfigurationSectionHelper), true);
			base.EnsureAuthenticationAuthorizationDebug(base.Description);
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x0001A35C File Offset: 0x0001855C
		[SecuritySafeCritical]
		private void LoadHostConfigurationInternal(ConfigLoader configLoader, ServiceDescription description, string configurationName)
		{
			ServiceElement serviceElement = configLoader.LookupService(configurationName);
			if (serviceElement != null)
			{
				configLoader.LoadHostConfig(serviceElement, this, delegate(Uri addr)
				{
					base.InternalBaseAddresses.Add(addr);
				});
			}
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x0001A388 File Offset: 0x00018588
		[SecuritySafeCritical]
		private void LoadConfigurationSectionExceptHostInternal(ConfigLoader configLoader, ServiceDescription description, string configurationName)
		{
			ServiceElement serviceElement = configLoader.LookupService(configurationName);
			configLoader.LoadServiceDescription(this, description, serviceElement, new Action<Uri>(base.LoadConfigurationSectionHelper), true);
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060005C1 RID: 1473 RVA: 0x0001A3B3 File Offset: 0x000185B3
		internal override string CloseActivityName
		{
			get
			{
				return SR.GetString("ActivityCloseServiceHost", new object[]
				{
					this.serviceType.FullName
				});
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060005C2 RID: 1474 RVA: 0x0001A3D3 File Offset: 0x000185D3
		internal override string OpenActivityName
		{
			get
			{
				return SR.GetString("ActivityOpenServiceHost", new object[]
				{
					this.serviceType.FullName
				});
			}
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x0001A3F4 File Offset: 0x000185F4
		protected override ServiceDescription CreateDescription(out IDictionary<string, ContractDescription> implementedContracts)
		{
			if (this.serviceType == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxServiceHostCannotCreateDescriptionWithoutServiceType")));
			}
			ServiceDescription service;
			if (this.SingletonInstance != null)
			{
				service = ServiceDescription.GetService(this.SingletonInstance);
			}
			else
			{
				service = ServiceDescription.GetService(this.serviceType);
			}
			ServiceBehaviorAttribute serviceBehaviorAttribute = service.Behaviors.Find<ServiceBehaviorAttribute>();
			object obj = serviceBehaviorAttribute.GetWellKnownSingleton();
			if (obj == null)
			{
				obj = serviceBehaviorAttribute.GetHiddenSingleton();
				this.disposableInstance = (obj as IDisposable);
			}
			if ((typeof(IServiceBehavior).IsAssignableFrom(this.serviceType) || typeof(IContractBehavior).IsAssignableFrom(this.serviceType)) && obj == null)
			{
				obj = ServiceDescription.CreateImplementation(this.serviceType);
				this.disposableInstance = (obj as IDisposable);
			}
			if (this.SingletonInstance == null && obj is IServiceBehavior)
			{
				service.Behaviors.Add((IServiceBehavior)obj);
			}
			ServiceHost.ReflectedContractCollection reflectedContractCollection = new ServiceHost.ReflectedContractCollection();
			List<Type> interfaces = ServiceReflector.GetInterfaces(this.serviceType);
			for (int i = 0; i < interfaces.Count; i++)
			{
				Type type = interfaces[i];
				if (!reflectedContractCollection.Contains(type))
				{
					ContractDescription contract;
					if (obj != null)
					{
						contract = ContractDescription.GetContract(type, obj);
					}
					else
					{
						contract = ContractDescription.GetContract(type, this.serviceType);
					}
					reflectedContractCollection.Add(contract);
					Collection<ContractDescription> inheritedContracts = contract.GetInheritedContracts();
					for (int j = 0; j < inheritedContracts.Count; j++)
					{
						ContractDescription contractDescription = inheritedContracts[j];
						if (!reflectedContractCollection.Contains(contractDescription.ContractType))
						{
							reflectedContractCollection.Add(contractDescription);
						}
					}
				}
			}
			this.reflectedContracts = reflectedContractCollection;
			implementedContracts = reflectedContractCollection.ToImplementedContracts();
			return service;
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x0001A59C File Offset: 0x0001879C
		protected void InitializeDescription(object singletonInstance, UriSchemeKeyedCollection baseAddresses)
		{
			if (singletonInstance == null)
			{
				throw new ArgumentNullException("singletonInstance");
			}
			this.singletonInstance = singletonInstance;
			this.InitializeDescription(singletonInstance.GetType(), baseAddresses);
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x0001A5C0 File Offset: 0x000187C0
		protected void InitializeDescription(Type serviceType, UriSchemeKeyedCollection baseAddresses)
		{
			if (serviceType == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("serviceType"));
			}
			this.serviceType = serviceType;
			base.InitializeDescription(baseAddresses);
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x0001A5EE File Offset: 0x000187EE
		protected override void OnClosed()
		{
			base.OnClosed();
			if (this.disposableInstance != null)
			{
				this.disposableInstance.Dispose();
			}
		}

		// Token: 0x04000A53 RID: 2643
		private object singletonInstance;

		// Token: 0x04000A54 RID: 2644
		private Type serviceType;

		// Token: 0x04000A55 RID: 2645
		private ServiceHost.ReflectedContractCollection reflectedContracts;

		// Token: 0x04000A56 RID: 2646
		private IDisposable disposableInstance;

		// Token: 0x02000ADC RID: 2780
		private class ReflectedContractCollection : KeyedCollection<Type, ContractDescription>
		{
			// Token: 0x06006EA0 RID: 28320 RVA: 0x0019C513 File Offset: 0x0019A713
			public ReflectedContractCollection() : base(null, 4)
			{
			}

			// Token: 0x06006EA1 RID: 28321 RVA: 0x0019C51D File Offset: 0x0019A71D
			protected override Type GetKeyForItem(ContractDescription item)
			{
				if (item == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("item");
				}
				return item.ContractType;
			}

			// Token: 0x06006EA2 RID: 28322 RVA: 0x0019C538 File Offset: 0x0019A738
			public IDictionary<string, ContractDescription> ToImplementedContracts()
			{
				Dictionary<string, ContractDescription> dictionary = new Dictionary<string, ContractDescription>();
				foreach (ContractDescription contractDescription in base.Items)
				{
					dictionary.Add(ServiceHost.ReflectedContractCollection.GetConfigKey(contractDescription), contractDescription);
				}
				return dictionary;
			}

			// Token: 0x06006EA3 RID: 28323 RVA: 0x0019C594 File Offset: 0x0019A794
			internal static string GetConfigKey(ContractDescription contract)
			{
				return contract.ConfigurationName;
			}
		}

		// Token: 0x02000ADD RID: 2781
		private class ReflectedAndBehaviorContractCollection
		{
			// Token: 0x06006EA4 RID: 28324 RVA: 0x0019C59C File Offset: 0x0019A79C
			public ReflectedAndBehaviorContractCollection(ServiceHost.ReflectedContractCollection reflectedContracts, KeyedByTypeCollection<IServiceBehavior> behaviors)
			{
				this.reflectedContracts = reflectedContracts;
				this.behaviors = behaviors;
			}

			// Token: 0x06006EA5 RID: 28325 RVA: 0x0019C5B2 File Offset: 0x0019A7B2
			internal bool Contains(Type implementedContract)
			{
				return this.reflectedContracts.Contains(implementedContract) || (this.behaviors.Contains(typeof(ServiceMetadataBehavior)) && ServiceMetadataBehavior.IsMetadataImplementedType(implementedContract));
			}

			// Token: 0x06006EA6 RID: 28326 RVA: 0x0019C5E8 File Offset: 0x0019A7E8
			internal string GetConfigKey(Type implementedContract)
			{
				if (this.reflectedContracts.Contains(implementedContract))
				{
					return ServiceHost.ReflectedContractCollection.GetConfigKey(this.reflectedContracts[implementedContract]);
				}
				if (this.behaviors.Contains(typeof(ServiceMetadataBehavior)) && ServiceMetadataBehavior.IsMetadataImplementedType(implementedContract))
				{
					return "IMetadataExchange";
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SfxReflectedContractKeyNotFound2", new object[]
				{
					implementedContract.FullName,
					string.Empty
				})));
			}

			// Token: 0x04003F20 RID: 16160
			private ServiceHost.ReflectedContractCollection reflectedContracts;

			// Token: 0x04003F21 RID: 16161
			private KeyedByTypeCollection<IServiceBehavior> behaviors;
		}
	}
}
