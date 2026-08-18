using System;
using System.Configuration;
using System.Security;
using System.Security.Permissions;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000689 RID: 1673
	public sealed class ServiceHostingEnvironmentSection : ConfigurationSection
	{
		// Token: 0x17001052 RID: 4178
		// (get) Token: 0x060040A2 RID: 16546 RVA: 0x000F55E0 File Offset: 0x000F37E0
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("", typeof(TransportConfigurationTypeElementCollection), null, null, null, ConfigurationPropertyOptions.IsDefaultCollection),
						new ConfigurationProperty("baseAddressPrefixFilters", typeof(BaseAddressPrefixFilterElementCollection), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("serviceActivations", typeof(ServiceActivationElementCollection), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("aspNetCompatibilityEnabled", typeof(bool), false, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("closeIdleServicesAtLowMemory", typeof(bool), false, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("minFreeMemoryPercentageToActivateService", typeof(int), 5, null, new IntegerValidator(0, 99, false), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("multipleSiteBindingsEnabled", typeof(bool), false, null, null, ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x060040A4 RID: 16548 RVA: 0x000F5704 File Offset: 0x000F3904
		protected override void PostDeserialize()
		{
			if (base.EvaluationContext.IsMachineLevel)
			{
				return;
			}
			if (PropertyValueOrigin.SetHere == base.ElementInformation.Properties["minFreeMemoryPercentageToActivateService"].ValueOrigin)
			{
				try
				{
					new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
				}
				catch (SecurityException)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("Hosting_MemoryGatesCheckFailedUnderPartialTrust")));
				}
			}
		}

		// Token: 0x17001053 RID: 4179
		// (get) Token: 0x060040A5 RID: 16549 RVA: 0x000F5774 File Offset: 0x000F3974
		[ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
		public TransportConfigurationTypeElementCollection TransportConfigurationTypes
		{
			get
			{
				return (TransportConfigurationTypeElementCollection)base[""];
			}
		}

		// Token: 0x17001054 RID: 4180
		// (get) Token: 0x060040A6 RID: 16550 RVA: 0x000F5786 File Offset: 0x000F3986
		[ConfigurationProperty("baseAddressPrefixFilters", Options = ConfigurationPropertyOptions.None)]
		public BaseAddressPrefixFilterElementCollection BaseAddressPrefixFilters
		{
			get
			{
				return (BaseAddressPrefixFilterElementCollection)base["baseAddressPrefixFilters"];
			}
		}

		// Token: 0x17001055 RID: 4181
		// (get) Token: 0x060040A7 RID: 16551 RVA: 0x000F5798 File Offset: 0x000F3998
		[ConfigurationProperty("serviceActivations", Options = ConfigurationPropertyOptions.None)]
		public ServiceActivationElementCollection ServiceActivations
		{
			get
			{
				return (ServiceActivationElementCollection)base["serviceActivations"];
			}
		}

		// Token: 0x17001056 RID: 4182
		// (get) Token: 0x060040A8 RID: 16552 RVA: 0x000F57AA File Offset: 0x000F39AA
		// (set) Token: 0x060040A9 RID: 16553 RVA: 0x000F57BC File Offset: 0x000F39BC
		[ConfigurationProperty("aspNetCompatibilityEnabled", DefaultValue = false)]
		public bool AspNetCompatibilityEnabled
		{
			get
			{
				return (bool)base["aspNetCompatibilityEnabled"];
			}
			set
			{
				base["aspNetCompatibilityEnabled"] = value;
			}
		}

		// Token: 0x17001057 RID: 4183
		// (get) Token: 0x060040AA RID: 16554 RVA: 0x000F57CF File Offset: 0x000F39CF
		// (set) Token: 0x060040AB RID: 16555 RVA: 0x000F57E1 File Offset: 0x000F39E1
		[ConfigurationProperty("closeIdleServicesAtLowMemory", DefaultValue = false)]
		public bool CloseIdleServicesAtLowMemory
		{
			get
			{
				return (bool)base["closeIdleServicesAtLowMemory"];
			}
			set
			{
				base["closeIdleServicesAtLowMemory"] = value;
			}
		}

		// Token: 0x17001058 RID: 4184
		// (get) Token: 0x060040AC RID: 16556 RVA: 0x000F57F4 File Offset: 0x000F39F4
		// (set) Token: 0x060040AD RID: 16557 RVA: 0x000F5806 File Offset: 0x000F3A06
		[ConfigurationProperty("minFreeMemoryPercentageToActivateService", DefaultValue = 5)]
		[IntegerValidator(MinValue = 0, MaxValue = 99)]
		public int MinFreeMemoryPercentageToActivateService
		{
			get
			{
				return (int)base["minFreeMemoryPercentageToActivateService"];
			}
			set
			{
				base["minFreeMemoryPercentageToActivateService"] = value;
			}
		}

		// Token: 0x17001059 RID: 4185
		// (get) Token: 0x060040AE RID: 16558 RVA: 0x000F5819 File Offset: 0x000F3A19
		// (set) Token: 0x060040AF RID: 16559 RVA: 0x000F582B File Offset: 0x000F3A2B
		[ConfigurationProperty("multipleSiteBindingsEnabled", DefaultValue = false)]
		public bool MultipleSiteBindingsEnabled
		{
			get
			{
				return (bool)base["multipleSiteBindingsEnabled"];
			}
			set
			{
				base["multipleSiteBindingsEnabled"] = value;
			}
		}

		// Token: 0x060040B0 RID: 16560 RVA: 0x000F583E File Offset: 0x000F3A3E
		internal static ServiceHostingEnvironmentSection GetSection()
		{
			return (ServiceHostingEnvironmentSection)ConfigurationHelpers.GetSection(ConfigurationStrings.ServiceHostingEnvironmentSectionPath);
		}

		// Token: 0x060040B1 RID: 16561 RVA: 0x000F584F File Offset: 0x000F3A4F
		[SecurityCritical]
		internal static ServiceHostingEnvironmentSection UnsafeGetSection()
		{
			return (ServiceHostingEnvironmentSection)ConfigurationHelpers.UnsafeGetSection(ConfigurationStrings.ServiceHostingEnvironmentSectionPath);
		}

		// Token: 0x04002CD6 RID: 11478
		private ConfigurationPropertyCollection properties;
	}
}
