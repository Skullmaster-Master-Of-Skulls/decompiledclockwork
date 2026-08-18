using System;
using System.Configuration;
using System.ServiceModel.Description;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000690 RID: 1680
	public sealed class ServiceThrottlingElement : BehaviorExtensionElement
	{
		// Token: 0x17001079 RID: 4217
		// (get) Token: 0x06004101 RID: 16641 RVA: 0x000F7094 File Offset: 0x000F5294
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("maxConcurrentCalls", typeof(int), 16, null, new IntegerValidator(1, int.MaxValue, false), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("maxConcurrentSessions", typeof(int), 100, null, new IntegerValidator(1, int.MaxValue, false), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("maxConcurrentInstances", typeof(int), 116, null, new IntegerValidator(1, int.MaxValue, false), ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x1700107A RID: 4218
		// (get) Token: 0x06004103 RID: 16643 RVA: 0x000F7154 File Offset: 0x000F5354
		// (set) Token: 0x06004104 RID: 16644 RVA: 0x000F7166 File Offset: 0x000F5366
		[ConfigurationProperty("maxConcurrentCalls", DefaultValue = 16)]
		[IntegerValidator(MinValue = 1)]
		public int MaxConcurrentCalls
		{
			get
			{
				return (int)base["maxConcurrentCalls"];
			}
			set
			{
				base["maxConcurrentCalls"] = value;
			}
		}

		// Token: 0x1700107B RID: 4219
		// (get) Token: 0x06004105 RID: 16645 RVA: 0x000F7179 File Offset: 0x000F5379
		// (set) Token: 0x06004106 RID: 16646 RVA: 0x000F718B File Offset: 0x000F538B
		[ConfigurationProperty("maxConcurrentSessions", DefaultValue = 100)]
		[IntegerValidator(MinValue = 1)]
		public int MaxConcurrentSessions
		{
			get
			{
				return (int)base["maxConcurrentSessions"];
			}
			set
			{
				base["maxConcurrentSessions"] = value;
			}
		}

		// Token: 0x1700107C RID: 4220
		// (get) Token: 0x06004107 RID: 16647 RVA: 0x000F719E File Offset: 0x000F539E
		// (set) Token: 0x06004108 RID: 16648 RVA: 0x000F71B0 File Offset: 0x000F53B0
		[ConfigurationProperty("maxConcurrentInstances", DefaultValue = 116)]
		[IntegerValidator(MinValue = 1)]
		public int MaxConcurrentInstances
		{
			get
			{
				return (int)base["maxConcurrentInstances"];
			}
			set
			{
				base["maxConcurrentInstances"] = value;
			}
		}

		// Token: 0x06004109 RID: 16649 RVA: 0x000F71C4 File Offset: 0x000F53C4
		public override void CopyFrom(ServiceModelExtensionElement from)
		{
			base.CopyFrom(from);
			ServiceThrottlingElement serviceThrottlingElement = (ServiceThrottlingElement)from;
			this.MaxConcurrentCalls = serviceThrottlingElement.MaxConcurrentCalls;
			this.MaxConcurrentSessions = serviceThrottlingElement.MaxConcurrentSessions;
			this.MaxConcurrentInstances = serviceThrottlingElement.MaxConcurrentInstances;
		}

		// Token: 0x0600410A RID: 16650 RVA: 0x000F7204 File Offset: 0x000F5404
		protected internal override object CreateBehavior()
		{
			ServiceThrottlingBehavior serviceThrottlingBehavior = new ServiceThrottlingBehavior();
			PropertyInformationCollection propertyInformationCollection = base.ElementInformation.Properties;
			if (propertyInformationCollection["maxConcurrentCalls"].ValueOrigin != PropertyValueOrigin.Default)
			{
				serviceThrottlingBehavior.MaxConcurrentCalls = this.MaxConcurrentCalls;
			}
			if (propertyInformationCollection["maxConcurrentSessions"].ValueOrigin != PropertyValueOrigin.Default)
			{
				serviceThrottlingBehavior.MaxConcurrentSessions = this.MaxConcurrentSessions;
			}
			if (propertyInformationCollection["maxConcurrentInstances"].ValueOrigin != PropertyValueOrigin.Default)
			{
				serviceThrottlingBehavior.MaxConcurrentInstances = this.MaxConcurrentInstances;
			}
			return serviceThrottlingBehavior;
		}

		// Token: 0x1700107D RID: 4221
		// (get) Token: 0x0600410B RID: 16651 RVA: 0x000F727E File Offset: 0x000F547E
		public override Type BehaviorType
		{
			get
			{
				return typeof(ServiceThrottlingBehavior);
			}
		}

		// Token: 0x04002CDE RID: 11486
		private ConfigurationPropertyCollection properties;
	}
}
