using System;
using System.Diagnostics;
using System.ServiceModel.Activation;
using System.ServiceModel.Description;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x02000211 RID: 529
	internal class ComPlusServiceLoader
	{
		// Token: 0x0600102B RID: 4139 RVA: 0x00039AC1 File Offset: 0x00037CC1
		public ComPlusServiceLoader(ServiceInfo info)
		{
			this.info = info;
			this.typeLoader = new ComPlusTypeLoader(info);
			this.configLoader = new ConfigLoader(this.typeLoader);
		}

		// Token: 0x0600102C RID: 4140 RVA: 0x00039AF0 File Offset: 0x00037CF0
		public ServiceDescription Load(ServiceHostBase host)
		{
			ServiceDescription serviceDescription = new ServiceDescription(this.info.ServiceName);
			this.AddBehaviors(serviceDescription);
			this.configLoader.LoadServiceDescription(host, serviceDescription, this.info.ServiceElement, new Action<Uri>(host.LoadConfigurationSectionHelper), false);
			this.ValidateConfigInstanceSettings(serviceDescription);
			ComPlusServiceHostTrace.Trace(TraceEventType.Information, 327685, "TraceCodeComIntegrationServiceHostCreatedServiceEndpoint", this.info, serviceDescription.Endpoints);
			return serviceDescription;
		}

		// Token: 0x0600102D RID: 4141 RVA: 0x00039B60 File Offset: 0x00037D60
		private void AddBehaviors(ServiceDescription service)
		{
			ServiceBehaviorAttribute serviceBehaviorAttribute = this.EnsureBehaviorAttribute(service);
			serviceBehaviorAttribute.InstanceProvider = new ComPlusInstanceProvider(this.info);
			serviceBehaviorAttribute.InstanceContextMode = InstanceContextMode.Single;
			serviceBehaviorAttribute.ConcurrencyMode = ConcurrencyMode.Multiple;
			serviceBehaviorAttribute.UseSynchronizationContext = false;
			service.Behaviors.Add(new SecurityCookieModeValidator());
			if (AspNetEnvironment.Enabled && service.Behaviors.Find<AspNetCompatibilityRequirementsAttribute>() == null)
			{
				AspNetCompatibilityRequirementsAttribute item = new AspNetCompatibilityRequirementsAttribute();
				service.Behaviors.Add(item);
			}
		}

		// Token: 0x0600102E RID: 4142 RVA: 0x00039BD4 File Offset: 0x00037DD4
		private ServiceBehaviorAttribute EnsureBehaviorAttribute(ServiceDescription service)
		{
			ServiceBehaviorAttribute serviceBehaviorAttribute;
			if (service.Behaviors.Contains(typeof(ServiceBehaviorAttribute)))
			{
				serviceBehaviorAttribute = (ServiceBehaviorAttribute)service.Behaviors[typeof(ServiceBehaviorAttribute)];
			}
			else
			{
				serviceBehaviorAttribute = new ServiceBehaviorAttribute();
				service.Behaviors.Insert(0, serviceBehaviorAttribute);
			}
			return serviceBehaviorAttribute;
		}

		// Token: 0x0600102F RID: 4143 RVA: 0x00039C2C File Offset: 0x00037E2C
		private void ValidateConfigInstanceSettings(ServiceDescription service)
		{
			ServiceBehaviorAttribute serviceBehaviorAttribute = this.EnsureBehaviorAttribute(service);
			foreach (ServiceEndpoint serviceEndpoint in service.Endpoints)
			{
				if (serviceEndpoint != null && !serviceEndpoint.InternalIsSystemEndpoint(service))
				{
					if (serviceEndpoint.Contract.SessionMode == SessionMode.Required)
					{
						if (serviceBehaviorAttribute.InstanceContextMode == InstanceContextMode.PerCall)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(Error.InconsistentSessionRequirements());
						}
						serviceBehaviorAttribute.InstanceContextMode = InstanceContextMode.PerSession;
					}
					else
					{
						if (serviceBehaviorAttribute.InstanceContextMode == InstanceContextMode.PerSession)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(Error.InconsistentSessionRequirements());
						}
						serviceBehaviorAttribute.InstanceContextMode = InstanceContextMode.PerCall;
					}
				}
			}
			if (serviceBehaviorAttribute.InstanceContextMode == InstanceContextMode.Single)
			{
				serviceBehaviorAttribute.InstanceContextMode = InstanceContextMode.PerSession;
			}
		}

		// Token: 0x0400185A RID: 6234
		private ServiceInfo info;

		// Token: 0x0400185B RID: 6235
		private ConfigLoader configLoader;

		// Token: 0x0400185C RID: 6236
		private ComPlusTypeLoader typeLoader;
	}
}
