using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Runtime;
using System.Security;
using System.Security.Permissions;
using System.ServiceModel.Activation;

namespace System.ServiceModel.Description
{
	// Token: 0x020003D9 RID: 985
	[DebuggerDisplay("ServiceType={serviceType}")]
	public class ServiceDescription
	{
		// Token: 0x0600251B RID: 9499 RVA: 0x000850A0 File Offset: 0x000832A0
		public ServiceDescription()
		{
		}

		// Token: 0x0600251C RID: 9500 RVA: 0x000850CC File Offset: 0x000832CC
		internal ServiceDescription(string serviceName)
		{
			if (string.IsNullOrEmpty(serviceName))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("serviceName");
			}
			this.Name = serviceName;
		}

		// Token: 0x0600251D RID: 9501 RVA: 0x00085120 File Offset: 0x00083320
		public ServiceDescription(IEnumerable<ServiceEndpoint> endpoints) : this()
		{
			if (endpoints == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("endpoints");
			}
			foreach (ServiceEndpoint item in endpoints)
			{
				this.endpoints.Add(item);
			}
		}

		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x0600251E RID: 9502 RVA: 0x00085188 File Offset: 0x00083388
		// (set) Token: 0x0600251F RID: 9503 RVA: 0x000851C8 File Offset: 0x000833C8
		public string Name
		{
			get
			{
				if (this.serviceName != null)
				{
					return this.serviceName.EncodedName;
				}
				if (this.ServiceType != null)
				{
					return NamingHelper.XmlName(this.ServiceType.Name);
				}
				return "service";
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					this.serviceName = null;
					return;
				}
				this.serviceName = new XmlName(value, true);
			}
		}

		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x06002520 RID: 9504 RVA: 0x000851E7 File Offset: 0x000833E7
		// (set) Token: 0x06002521 RID: 9505 RVA: 0x000851EF File Offset: 0x000833EF
		public string Namespace
		{
			get
			{
				return this.serviceNamespace;
			}
			set
			{
				this.serviceNamespace = value;
			}
		}

		// Token: 0x17000972 RID: 2418
		// (get) Token: 0x06002522 RID: 9506 RVA: 0x000851F8 File Offset: 0x000833F8
		public KeyedByTypeCollection<IServiceBehavior> Behaviors
		{
			get
			{
				return this.behaviors;
			}
		}

		// Token: 0x17000973 RID: 2419
		// (get) Token: 0x06002523 RID: 9507 RVA: 0x00085200 File Offset: 0x00083400
		// (set) Token: 0x06002524 RID: 9508 RVA: 0x00085208 File Offset: 0x00083408
		public string ConfigurationName
		{
			get
			{
				return this.configurationName;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this.configurationName = value;
			}
		}

		// Token: 0x17000974 RID: 2420
		// (get) Token: 0x06002525 RID: 9509 RVA: 0x00085224 File Offset: 0x00083424
		public ServiceEndpointCollection Endpoints
		{
			get
			{
				return this.endpoints;
			}
		}

		// Token: 0x17000975 RID: 2421
		// (get) Token: 0x06002526 RID: 9510 RVA: 0x0008522C File Offset: 0x0008342C
		// (set) Token: 0x06002527 RID: 9511 RVA: 0x00085234 File Offset: 0x00083434
		public Type ServiceType
		{
			get
			{
				return this.serviceType;
			}
			set
			{
				this.serviceType = value;
			}
		}

		// Token: 0x06002528 RID: 9512 RVA: 0x00085240 File Offset: 0x00083440
		private static void AddBehaviors(ServiceDescription serviceDescription)
		{
			Type type = serviceDescription.ServiceType;
			TypeLoader.ApplyServiceInheritance<IServiceBehavior, KeyedByTypeCollection<IServiceBehavior>>(type, serviceDescription.Behaviors, new TypeLoader.ServiceInheritanceCallback<IServiceBehavior, KeyedByTypeCollection<IServiceBehavior>>(ServiceDescription.GetIServiceBehaviorAttributes));
			ServiceBehaviorAttribute serviceBehaviorAttribute = ServiceDescription.EnsureBehaviorAttribute(serviceDescription);
			if (serviceBehaviorAttribute.Name != null)
			{
				serviceDescription.Name = new XmlName(serviceBehaviorAttribute.Name).EncodedName;
			}
			if (serviceBehaviorAttribute.Namespace != null)
			{
				serviceDescription.Namespace = serviceBehaviorAttribute.Namespace;
			}
			if (string.IsNullOrEmpty(serviceBehaviorAttribute.ConfigurationName))
			{
				serviceDescription.ConfigurationName = type.FullName;
			}
			else
			{
				serviceDescription.ConfigurationName = serviceBehaviorAttribute.ConfigurationName;
			}
			AspNetEnvironment.Current.EnsureCompatibilityRequirements(serviceDescription);
		}

		// Token: 0x06002529 RID: 9513 RVA: 0x000852D8 File Offset: 0x000834D8
		internal static object CreateImplementation(Type serviceType)
		{
			ConstructorInfo constructor = serviceType.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Type.EmptyTypes, null);
			if (constructor == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxNoDefaultConstructor")));
			}
			if (!PartialTrustHelpers.AppDomainFullyTrusted && (serviceType.IsNotPublic || !constructor.IsPublic) && serviceType.Assembly == typeof(ServiceDescription).Assembly)
			{
				PartialTrustHelpers.DemandForFullTrust();
			}
			object result;
			try
			{
				object obj = constructor.Invoke(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, null, CultureInfo.InvariantCulture);
				result = obj;
			}
			catch (MethodAccessException ex)
			{
				SecurityException ex2 = ex.InnerException as SecurityException;
				if (ex2 != null && ex2.PermissionType.Equals(typeof(ReflectionPermission)))
				{
					DiagnosticUtility.TraceHandledException(ex, TraceEventType.Warning);
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityException(SR.GetString("PartialTrustServiceCtorNotVisible", new object[]
					{
						serviceType.FullName
					})));
				}
				throw;
			}
			return result;
		}

		// Token: 0x0600252A RID: 9514 RVA: 0x000853D4 File Offset: 0x000835D4
		private static ServiceBehaviorAttribute EnsureBehaviorAttribute(ServiceDescription description)
		{
			ServiceBehaviorAttribute serviceBehaviorAttribute = description.Behaviors.Find<ServiceBehaviorAttribute>();
			if (serviceBehaviorAttribute == null)
			{
				serviceBehaviorAttribute = new ServiceBehaviorAttribute();
				description.Behaviors.Insert(0, serviceBehaviorAttribute);
			}
			return serviceBehaviorAttribute;
		}

		// Token: 0x0600252B RID: 9515 RVA: 0x00085404 File Offset: 0x00083604
		internal void EnsureInvariants()
		{
			for (int i = 0; i < this.Endpoints.Count; i++)
			{
				ServiceEndpoint serviceEndpoint = this.Endpoints[i];
				if (serviceEndpoint == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("AChannelServiceEndpointIsNull0")));
				}
				serviceEndpoint.EnsureInvariants();
			}
		}

		// Token: 0x0600252C RID: 9516 RVA: 0x00085458 File Offset: 0x00083658
		private static void GetIServiceBehaviorAttributes(Type currentServiceType, KeyedByTypeCollection<IServiceBehavior> behaviors)
		{
			foreach (IServiceBehavior item in ServiceReflector.GetCustomAttributes(currentServiceType, typeof(IServiceBehavior)))
			{
				behaviors.Add(item);
			}
		}

		// Token: 0x0600252D RID: 9517 RVA: 0x00085494 File Offset: 0x00083694
		public static ServiceDescription GetService(Type serviceType)
		{
			if (serviceType == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("serviceType");
			}
			if (!serviceType.IsClass)
			{
				throw new ArgumentException(SR.GetString("SFxServiceHostNeedsClass"));
			}
			ServiceDescription serviceDescription = new ServiceDescription();
			serviceDescription.ServiceType = serviceType;
			ServiceDescription.AddBehaviors(serviceDescription);
			ServiceDescription.SetupSingleton(serviceDescription, null, false);
			return serviceDescription;
		}

		// Token: 0x0600252E RID: 9518 RVA: 0x000854F0 File Offset: 0x000836F0
		public static ServiceDescription GetService(object serviceImplementation)
		{
			if (serviceImplementation == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("serviceImplementation");
			}
			Type type = serviceImplementation.GetType();
			ServiceDescription serviceDescription = new ServiceDescription();
			serviceDescription.ServiceType = type;
			if (serviceImplementation is IServiceBehavior)
			{
				serviceDescription.Behaviors.Add((IServiceBehavior)serviceImplementation);
			}
			ServiceDescription.AddBehaviors(serviceDescription);
			ServiceDescription.SetupSingleton(serviceDescription, serviceImplementation, true);
			return serviceDescription;
		}

		// Token: 0x0600252F RID: 9519 RVA: 0x0008554C File Offset: 0x0008374C
		private static void SetupSingleton(ServiceDescription serviceDescription, object implementation, bool isWellKnown)
		{
			ServiceBehaviorAttribute serviceBehaviorAttribute = ServiceDescription.EnsureBehaviorAttribute(serviceDescription);
			Type type = serviceDescription.ServiceType;
			if (implementation == null && serviceBehaviorAttribute.InstanceContextMode == InstanceContextMode.Single)
			{
				implementation = ServiceDescription.CreateImplementation(type);
			}
			if (isWellKnown)
			{
				serviceBehaviorAttribute.SetWellKnownSingleton(implementation);
				return;
			}
			if (implementation != null && serviceBehaviorAttribute.InstanceContextMode == InstanceContextMode.Single)
			{
				serviceBehaviorAttribute.SetHiddenSingleton(implementation);
			}
		}

		// Token: 0x040020AF RID: 8367
		private KeyedByTypeCollection<IServiceBehavior> behaviors = new KeyedByTypeCollection<IServiceBehavior>();

		// Token: 0x040020B0 RID: 8368
		private string configurationName;

		// Token: 0x040020B1 RID: 8369
		private ServiceEndpointCollection endpoints = new ServiceEndpointCollection();

		// Token: 0x040020B2 RID: 8370
		private Type serviceType;

		// Token: 0x040020B3 RID: 8371
		private XmlName serviceName;

		// Token: 0x040020B4 RID: 8372
		private string serviceNamespace = "http://tempuri.org/";
	}
}
