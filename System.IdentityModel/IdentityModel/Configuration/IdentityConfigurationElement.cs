using System;
using System.ComponentModel;
using System.Configuration;

namespace System.IdentityModel.Configuration
{
	// Token: 0x020001C6 RID: 454
	public sealed class IdentityConfigurationElement : ConfigurationElement
	{
		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000EC6 RID: 3782 RVA: 0x00042D31 File Offset: 0x00040F31
		// (set) Token: 0x06000EC7 RID: 3783 RVA: 0x00042D43 File Offset: 0x00040F43
		[ConfigurationProperty("name", Options = ConfigurationPropertyOptions.IsKey)]
		[StringValidator(MinLength = 0)]
		public string Name
		{
			get
			{
				return (string)base["name"];
			}
			set
			{
				base["name"] = value;
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000EC8 RID: 3784 RVA: 0x00042D51 File Offset: 0x00040F51
		[ConfigurationProperty("audienceUris", IsRequired = false)]
		public AudienceUriElementCollection AudienceUris
		{
			get
			{
				return (AudienceUriElementCollection)base["audienceUris"];
			}
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06000EC9 RID: 3785 RVA: 0x00042D63 File Offset: 0x00040F63
		// (set) Token: 0x06000ECA RID: 3786 RVA: 0x00042D75 File Offset: 0x00040F75
		[ConfigurationProperty("caches", IsRequired = false)]
		public IdentityModelCachesElement Caches
		{
			get
			{
				return (IdentityModelCachesElement)base["caches"];
			}
			set
			{
				base["caches"] = value;
			}
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06000ECB RID: 3787 RVA: 0x00042D83 File Offset: 0x00040F83
		// (set) Token: 0x06000ECC RID: 3788 RVA: 0x00042D95 File Offset: 0x00040F95
		[ConfigurationProperty("certificateValidation", IsRequired = false)]
		public X509CertificateValidationElement CertificateValidation
		{
			get
			{
				return (X509CertificateValidationElement)base["certificateValidation"];
			}
			set
			{
				base["certificateValidation"] = value;
			}
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06000ECD RID: 3789 RVA: 0x00042DA3 File Offset: 0x00040FA3
		// (set) Token: 0x06000ECE RID: 3790 RVA: 0x00042DB5 File Offset: 0x00040FB5
		[ConfigurationProperty("claimsAuthenticationManager", IsRequired = false)]
		public CustomTypeElement ClaimsAuthenticationManager
		{
			get
			{
				return (CustomTypeElement)base["claimsAuthenticationManager"];
			}
			set
			{
				base["claimsAuthenticationManager"] = value;
			}
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000ECF RID: 3791 RVA: 0x00042DC3 File Offset: 0x00040FC3
		// (set) Token: 0x06000ED0 RID: 3792 RVA: 0x00042DD5 File Offset: 0x00040FD5
		[ConfigurationProperty("claimsAuthorizationManager", IsRequired = false)]
		public CustomTypeElement ClaimsAuthorizationManager
		{
			get
			{
				return (CustomTypeElement)base["claimsAuthorizationManager"];
			}
			set
			{
				base["claimsAuthorizationManager"] = value;
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000ED1 RID: 3793 RVA: 0x00042DE3 File Offset: 0x00040FE3
		// (set) Token: 0x06000ED2 RID: 3794 RVA: 0x00042DF5 File Offset: 0x00040FF5
		[ConfigurationProperty("issuerNameRegistry", IsRequired = false)]
		public IssuerNameRegistryElement IssuerNameRegistry
		{
			get
			{
				return (IssuerNameRegistryElement)base["issuerNameRegistry"];
			}
			set
			{
				base["issuerNameRegistry"] = value;
			}
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06000ED3 RID: 3795 RVA: 0x00042E03 File Offset: 0x00041003
		// (set) Token: 0x06000ED4 RID: 3796 RVA: 0x00042E15 File Offset: 0x00041015
		[ConfigurationProperty("issuerTokenResolver", IsRequired = false)]
		public CustomTypeElement IssuerTokenResolver
		{
			get
			{
				return (CustomTypeElement)base["issuerTokenResolver"];
			}
			set
			{
				base["issuerTokenResolver"] = value;
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06000ED5 RID: 3797 RVA: 0x00042E23 File Offset: 0x00041023
		// (set) Token: 0x06000ED6 RID: 3798 RVA: 0x00042E35 File Offset: 0x00041035
		[ConfigurationProperty("maximumClockSkew", IsRequired = false, DefaultValue = "00:05:00")]
		[TypeConverter(typeof(TimeSpanOrInfiniteConverter))]
		[IdentityModelTimeSpanValidator(MinValueString = "00:00:00")]
		public TimeSpan MaximumClockSkew
		{
			get
			{
				return (TimeSpan)base["maximumClockSkew"];
			}
			set
			{
				base["maximumClockSkew"] = value;
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06000ED7 RID: 3799 RVA: 0x00042E48 File Offset: 0x00041048
		// (set) Token: 0x06000ED8 RID: 3800 RVA: 0x00042E5A File Offset: 0x0004105A
		[ConfigurationProperty("saveBootstrapContext", IsRequired = false, DefaultValue = false)]
		public bool SaveBootstrapContext
		{
			get
			{
				return (bool)base["saveBootstrapContext"];
			}
			set
			{
				base["saveBootstrapContext"] = value;
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06000ED9 RID: 3801 RVA: 0x00042E6D File Offset: 0x0004106D
		// (set) Token: 0x06000EDA RID: 3802 RVA: 0x00042E7F File Offset: 0x0004107F
		[ConfigurationProperty("serviceTokenResolver", IsRequired = false)]
		public CustomTypeElement ServiceTokenResolver
		{
			get
			{
				return (CustomTypeElement)base["serviceTokenResolver"];
			}
			set
			{
				base["serviceTokenResolver"] = value;
			}
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06000EDB RID: 3803 RVA: 0x00042E8D File Offset: 0x0004108D
		// (set) Token: 0x06000EDC RID: 3804 RVA: 0x00042E9F File Offset: 0x0004109F
		[ConfigurationProperty("tokenReplayDetection", IsRequired = false)]
		public TokenReplayDetectionElement TokenReplayDetection
		{
			get
			{
				return (TokenReplayDetectionElement)base["tokenReplayDetection"];
			}
			set
			{
				base["tokenReplayDetection"] = value;
			}
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06000EDD RID: 3805 RVA: 0x00042EAD File Offset: 0x000410AD
		[ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
		public SecurityTokenHandlerSetElementCollection SecurityTokenHandlerSets
		{
			get
			{
				return (SecurityTokenHandlerSetElementCollection)base[""];
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06000EDE RID: 3806 RVA: 0x00042EBF File Offset: 0x000410BF
		// (set) Token: 0x06000EDF RID: 3807 RVA: 0x00042ED1 File Offset: 0x000410D1
		[ConfigurationProperty("applicationService", IsRequired = false)]
		internal ApplicationServiceConfigurationElement ApplicationService
		{
			get
			{
				return (ApplicationServiceConfigurationElement)base["applicationService"];
			}
			set
			{
				base["applicationService"] = value;
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06000EE0 RID: 3808 RVA: 0x00042EE0 File Offset: 0x000410E0
		internal bool IsConfigured
		{
			get
			{
				return base.ElementInformation.Properties["name"].ValueOrigin != PropertyValueOrigin.Default || this.AudienceUris.IsConfigured || this.Caches.IsConfigured || this.CertificateValidation.IsConfigured || this.ClaimsAuthenticationManager.IsConfigured || this.ClaimsAuthorizationManager.IsConfigured || this.IssuerNameRegistry.IsConfigured || this.IssuerTokenResolver.IsConfigured || base.ElementInformation.Properties["saveBootstrapContext"].ValueOrigin != PropertyValueOrigin.Default || base.ElementInformation.Properties["maximumClockSkew"].ValueOrigin != PropertyValueOrigin.Default || this.ServiceTokenResolver.IsConfigured || this.TokenReplayDetection.IsConfigured || this.SecurityTokenHandlerSets.IsConfigured;
			}
		}
	}
}
