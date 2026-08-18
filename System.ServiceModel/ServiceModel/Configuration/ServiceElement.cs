using System;
using System.Configuration;
using System.Security;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000686 RID: 1670
	public sealed class ServiceElement : ConfigurationElement, IConfigurationContextProviderInternal
	{
		// Token: 0x17001039 RID: 4153
		// (get) Token: 0x0600406A RID: 16490 RVA: 0x000F4BC4 File Offset: 0x000F2DC4
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("behaviorConfiguration", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("", typeof(ServiceEndpointElementCollection), null, null, null, ConfigurationPropertyOptions.IsDefaultCollection),
						new ConfigurationProperty("host", typeof(HostElement), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("name", typeof(string), null, null, new StringValidator(1, int.MaxValue, null), ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x0600406B RID: 16491 RVA: 0x000F4C81 File Offset: 0x000F2E81
		public ServiceElement()
		{
		}

		// Token: 0x0600406C RID: 16492 RVA: 0x000F4C89 File Offset: 0x000F2E89
		public ServiceElement(string serviceName) : this()
		{
			this.Name = serviceName;
		}

		// Token: 0x1700103A RID: 4154
		// (get) Token: 0x0600406D RID: 16493 RVA: 0x000F4C98 File Offset: 0x000F2E98
		// (set) Token: 0x0600406E RID: 16494 RVA: 0x000F4CAA File Offset: 0x000F2EAA
		[ConfigurationProperty("behaviorConfiguration", DefaultValue = "")]
		[StringValidator(MinLength = 0)]
		public string BehaviorConfiguration
		{
			get
			{
				return (string)base["behaviorConfiguration"];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					value = string.Empty;
				}
				base["behaviorConfiguration"] = value;
			}
		}

		// Token: 0x1700103B RID: 4155
		// (get) Token: 0x0600406F RID: 16495 RVA: 0x000F4CC7 File Offset: 0x000F2EC7
		[ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
		public ServiceEndpointElementCollection Endpoints
		{
			get
			{
				return (ServiceEndpointElementCollection)base[""];
			}
		}

		// Token: 0x1700103C RID: 4156
		// (get) Token: 0x06004070 RID: 16496 RVA: 0x000F4CD9 File Offset: 0x000F2ED9
		[ConfigurationProperty("host", Options = ConfigurationPropertyOptions.None)]
		public HostElement Host
		{
			get
			{
				return (HostElement)base["host"];
			}
		}

		// Token: 0x1700103D RID: 4157
		// (get) Token: 0x06004071 RID: 16497 RVA: 0x000F4CEB File Offset: 0x000F2EEB
		// (set) Token: 0x06004072 RID: 16498 RVA: 0x000F4CFD File Offset: 0x000F2EFD
		[ConfigurationProperty("name", Options = (ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey))]
		[StringValidator(MinLength = 1)]
		public string Name
		{
			get
			{
				return (string)base["name"];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					value = string.Empty;
				}
				base["name"] = value;
			}
		}

		// Token: 0x06004073 RID: 16499 RVA: 0x000F4D1A File Offset: 0x000F2F1A
		[SecurityCritical]
		protected override void Reset(ConfigurationElement parentElement)
		{
			this.contextHelper.OnReset(parentElement);
			base.Reset(parentElement);
		}

		// Token: 0x06004074 RID: 16500 RVA: 0x000F4D2F File Offset: 0x000F2F2F
		ContextInformation IConfigurationContextProviderInternal.GetEvaluationContext()
		{
			return base.EvaluationContext;
		}

		// Token: 0x06004075 RID: 16501 RVA: 0x000F4D37 File Offset: 0x000F2F37
		[SecurityCritical]
		ContextInformation IConfigurationContextProviderInternal.GetOriginalEvaluationContext()
		{
			return this.contextHelper.GetOriginalContext(this);
		}

		// Token: 0x04002CD1 RID: 11473
		private ConfigurationPropertyCollection properties;

		// Token: 0x04002CD2 RID: 11474
		[SecurityCritical]
		private EvaluationContextHelper contextHelper;
	}
}
