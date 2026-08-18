using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Reflection;
using System.Security;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Permissions;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace System.ServiceModel.Activation
{
	// Token: 0x020005BC RID: 1468
	internal class AspNetEnvironment
	{
		// Token: 0x06003943 RID: 14659 RVA: 0x000DE3AE File Offset: 0x000DC5AE
		protected AspNetEnvironment()
		{
		}

		// Token: 0x17000D73 RID: 3443
		// (get) Token: 0x06003944 RID: 14660 RVA: 0x000DE3B8 File Offset: 0x000DC5B8
		// (set) Token: 0x06003945 RID: 14661 RVA: 0x000DE418 File Offset: 0x000DC618
		public static AspNetEnvironment Current
		{
			get
			{
				if (AspNetEnvironment.current == null)
				{
					object obj = AspNetEnvironment.thisLock;
					lock (obj)
					{
						if (AspNetEnvironment.current == null)
						{
							AspNetEnvironment.current = new AspNetEnvironment();
						}
					}
				}
				return AspNetEnvironment.current;
			}
			protected set
			{
				AspNetEnvironment.current = value;
				AspNetEnvironment.isEnabled = true;
			}
		}

		// Token: 0x17000D74 RID: 3444
		// (get) Token: 0x06003946 RID: 14662 RVA: 0x000DE428 File Offset: 0x000DC628
		public static bool Enabled
		{
			get
			{
				return AspNetEnvironment.isEnabled;
			}
		}

		// Token: 0x17000D75 RID: 3445
		// (get) Token: 0x06003947 RID: 14663 RVA: 0x000DE42F File Offset: 0x000DC62F
		public bool RequiresImpersonation
		{
			get
			{
				return this.AspNetCompatibilityEnabled;
			}
		}

		// Token: 0x17000D76 RID: 3446
		// (get) Token: 0x06003948 RID: 14664 RVA: 0x000DE437 File Offset: 0x000DC637
		public virtual bool AspNetCompatibilityEnabled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D77 RID: 3447
		// (get) Token: 0x06003949 RID: 14665 RVA: 0x000DE43A File Offset: 0x000DC63A
		public virtual string ConfigurationPath
		{
			get
			{
				return AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
			}
		}

		// Token: 0x17000D78 RID: 3448
		// (get) Token: 0x0600394A RID: 14666 RVA: 0x000DE44B File Offset: 0x000DC64B
		public virtual bool IsConfigurationBased
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D79 RID: 3449
		// (get) Token: 0x0600394B RID: 14667 RVA: 0x000DE44E File Offset: 0x000DC64E
		public virtual string CurrentVirtualPath
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000D7A RID: 3450
		// (get) Token: 0x0600394C RID: 14668 RVA: 0x000DE451 File Offset: 0x000DC651
		public virtual string XamlFileBaseLocation
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000D7B RID: 3451
		// (get) Token: 0x0600394D RID: 14669 RVA: 0x000DE454 File Offset: 0x000DC654
		public virtual bool UsingIntegratedPipeline
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D7C RID: 3452
		// (get) Token: 0x0600394E RID: 14670 RVA: 0x000DE457 File Offset: 0x000DC657
		public virtual string WebSocketVersion
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000D7D RID: 3453
		// (get) Token: 0x0600394F RID: 14671 RVA: 0x000DE45A File Offset: 0x000DC65A
		public bool IsWebSocketModuleLoaded
		{
			get
			{
				return this.WebSocketVersion != null;
			}
		}

		// Token: 0x06003950 RID: 14672 RVA: 0x000DE465 File Offset: 0x000DC665
		public virtual void AddHostingBehavior(ServiceHostBase serviceHost, ServiceDescription description)
		{
		}

		// Token: 0x06003951 RID: 14673 RVA: 0x000DE467 File Offset: 0x000DC667
		public virtual bool IsWindowsAuthenticationConfigured()
		{
			return false;
		}

		// Token: 0x06003952 RID: 14674 RVA: 0x000DE46A File Offset: 0x000DC66A
		public virtual List<Uri> GetBaseAddresses(Uri addressTemplate)
		{
			return null;
		}

		// Token: 0x06003953 RID: 14675 RVA: 0x000DE46D File Offset: 0x000DC66D
		public virtual bool IsWebConfigAboveApplication(object configHostingContext)
		{
			return SystemWebHelper.IsWebConfigAboveApplication(configHostingContext);
		}

		// Token: 0x06003954 RID: 14676 RVA: 0x000DE475 File Offset: 0x000DC675
		public virtual void EnsureCompatibilityRequirements(ServiceDescription description)
		{
		}

		// Token: 0x06003955 RID: 14677 RVA: 0x000DE477 File Offset: 0x000DC677
		public virtual bool TryGetFullVirtualPath(out string virtualPath)
		{
			virtualPath = null;
			return false;
		}

		// Token: 0x06003956 RID: 14678 RVA: 0x000DE47D File Offset: 0x000DC67D
		public virtual string GetAnnotationFromHost(ServiceHostBase host)
		{
			return string.Empty;
		}

		// Token: 0x06003957 RID: 14679 RVA: 0x000DE484 File Offset: 0x000DC684
		public virtual void EnsureAllReferencedAssemblyLoaded()
		{
		}

		// Token: 0x06003958 RID: 14680 RVA: 0x000DE486 File Offset: 0x000DC686
		public virtual BaseUriWithWildcard GetBaseUri(string transportScheme, Uri listenUri)
		{
			return null;
		}

		// Token: 0x06003959 RID: 14681 RVA: 0x000DE489 File Offset: 0x000DC689
		public virtual void ValidateHttpSettings(string virtualPath, bool isMetadataListener, bool usingDefaultSpnList, ref AuthenticationSchemes supportedSchemes, ref ExtendedProtectionPolicy extendedProtectionPolicy, ref string realm)
		{
		}

		// Token: 0x0600395A RID: 14682 RVA: 0x000DE48B File Offset: 0x000DC68B
		public virtual bool ValidateHttpsSettings(string virtualPath, ref bool requireClientCertificate)
		{
			return false;
		}

		// Token: 0x0600395B RID: 14683 RVA: 0x000DE48E File Offset: 0x000DC68E
		public virtual void ProcessNotMatchedEndpointAddress(Uri uri, string endpointName)
		{
		}

		// Token: 0x0600395C RID: 14684 RVA: 0x000DE490 File Offset: 0x000DC690
		public virtual void ValidateCompatibilityRequirements(AspNetCompatibilityRequirementsMode compatibilityMode)
		{
			if (compatibilityMode == AspNetCompatibilityRequirementsMode.Required)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("Hosting_CompatibilityServiceNotHosted")));
			}
		}

		// Token: 0x0600395D RID: 14685 RVA: 0x000DE4B0 File Offset: 0x000DC6B0
		public virtual IAspNetMessageProperty GetHostingProperty(Message message)
		{
			return null;
		}

		// Token: 0x0600395E RID: 14686 RVA: 0x000DE4B3 File Offset: 0x000DC6B3
		public virtual IAspNetMessageProperty GetHostingProperty(Message message, bool removeFromMessage)
		{
			return null;
		}

		// Token: 0x0600395F RID: 14687 RVA: 0x000DE4B6 File Offset: 0x000DC6B6
		public virtual void PrepareMessageForDispatch(Message message)
		{
		}

		// Token: 0x06003960 RID: 14688 RVA: 0x000DE4B8 File Offset: 0x000DC6B8
		public virtual void ApplyHostedContext(TransportChannelListener listener, BindingContext context)
		{
		}

		// Token: 0x06003961 RID: 14689 RVA: 0x000DE4BA File Offset: 0x000DC6BA
		internal virtual void AddMetadataBindingParameters(Uri listenUri, KeyedByTypeCollection<IServiceBehavior> serviceBehaviors, BindingParameterCollection bindingParameters)
		{
			bindingParameters.Add(new ServiceMetadataExtension.MetadataBindingParameter());
		}

		// Token: 0x06003962 RID: 14690 RVA: 0x000DE4C7 File Offset: 0x000DC6C7
		internal virtual bool IsMetadataListener(BindingParameterCollection bindingParameters)
		{
			return bindingParameters.Find<ServiceMetadataExtension.MetadataBindingParameter>() != null;
		}

		// Token: 0x06003963 RID: 14691 RVA: 0x000DE4D2 File Offset: 0x000DC6D2
		public virtual void IncrementBusyCount()
		{
		}

		// Token: 0x06003964 RID: 14692 RVA: 0x000DE4D4 File Offset: 0x000DC6D4
		public virtual void DecrementBusyCount()
		{
		}

		// Token: 0x06003965 RID: 14693 RVA: 0x000DE4D6 File Offset: 0x000DC6D6
		public virtual bool TraceIncrementBusyCountIsEnabled()
		{
			return false;
		}

		// Token: 0x06003966 RID: 14694 RVA: 0x000DE4D9 File Offset: 0x000DC6D9
		public virtual bool TraceDecrementBusyCountIsEnabled()
		{
			return false;
		}

		// Token: 0x06003967 RID: 14695 RVA: 0x000DE4DC File Offset: 0x000DC6DC
		public virtual void TraceIncrementBusyCount(string data)
		{
		}

		// Token: 0x06003968 RID: 14696 RVA: 0x000DE4DE File Offset: 0x000DC6DE
		public virtual void TraceDecrementBusyCount(string data)
		{
		}

		// Token: 0x06003969 RID: 14697 RVA: 0x000DE4E0 File Offset: 0x000DC6E0
		public virtual object GetConfigurationSection(string sectionPath)
		{
			return ConfigurationManager.GetSection(sectionPath);
		}

		// Token: 0x0600396A RID: 14698 RVA: 0x000DE4E8 File Offset: 0x000DC6E8
		[SecurityCritical]
		public virtual object UnsafeGetConfigurationSection(string sectionPath)
		{
			return AspNetEnvironment.UnsafeGetSectionFromConfigurationManager(sectionPath);
		}

		// Token: 0x0600396B RID: 14699 RVA: 0x000DE4F0 File Offset: 0x000DC6F0
		public virtual AuthenticationSchemes GetAuthenticationSchemes(Uri baseAddress)
		{
			return AuthenticationSchemes.None;
		}

		// Token: 0x17000D7E RID: 3454
		// (get) Token: 0x0600396C RID: 14700 RVA: 0x000DE4F3 File Offset: 0x000DC6F3
		public virtual bool IsSimpleApplicationHost
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600396D RID: 14701 RVA: 0x000DE4F6 File Offset: 0x000DC6F6
		[SecurityCritical]
		[ConfigurationPermission(SecurityAction.Assert, Unrestricted = true)]
		private static object UnsafeGetSectionFromConfigurationManager(string sectionPath)
		{
			return ConfigurationManager.GetSection(sectionPath);
		}

		// Token: 0x0600396E RID: 14702 RVA: 0x000DE4FE File Offset: 0x000DC6FE
		public virtual bool IsWithinApp(string absoluteVirtualPath)
		{
			return true;
		}

		// Token: 0x0600396F RID: 14703 RVA: 0x000DE504 File Offset: 0x000DC704
		internal static bool IsApplicationDomainHosted()
		{
			if (AspNetEnvironment.isApplicationDomainHosted == null)
			{
				object obj = AspNetEnvironment.thisLock;
				lock (obj)
				{
					if (AspNetEnvironment.isApplicationDomainHosted == null)
					{
						bool value = false;
						if (AspNetEnvironment.Enabled)
						{
							value = AspNetEnvironment.IsSystemWebAssemblyLoaded();
						}
						AspNetEnvironment.isApplicationDomainHosted = new bool?(value);
					}
				}
			}
			return AspNetEnvironment.isApplicationDomainHosted.Value;
		}

		// Token: 0x06003970 RID: 14704 RVA: 0x000DE57C File Offset: 0x000DC77C
		private static bool IsSystemWebAssemblyLoaded()
		{
			foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
			{
				if (assembly.FullName.StartsWith("System.Web,", StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x040029E2 RID: 10722
		private static readonly object thisLock = new object();

		// Token: 0x040029E3 RID: 10723
		private static volatile AspNetEnvironment current;

		// Token: 0x040029E4 RID: 10724
		private static bool isEnabled;

		// Token: 0x040029E5 RID: 10725
		private static bool? isApplicationDomainHosted;
	}
}
