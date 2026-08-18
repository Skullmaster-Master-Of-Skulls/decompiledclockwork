using System;
using System.Configuration;
using System.Security;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000606 RID: 1542
	public class CommonBehaviorsSection : ConfigurationSection
	{
		// Token: 0x17000E34 RID: 3636
		// (get) Token: 0x06003B66 RID: 15206 RVA: 0x000E3752 File Offset: 0x000E1952
		[ConfigurationProperty("endpointBehaviors", Options = ConfigurationPropertyOptions.None)]
		public CommonEndpointBehaviorElement EndpointBehaviors
		{
			get
			{
				return (CommonEndpointBehaviorElement)base["endpointBehaviors"];
			}
		}

		// Token: 0x17000E35 RID: 3637
		// (get) Token: 0x06003B67 RID: 15207 RVA: 0x000E3764 File Offset: 0x000E1964
		[ConfigurationProperty("serviceBehaviors", Options = ConfigurationPropertyOptions.None)]
		public CommonServiceBehaviorElement ServiceBehaviors
		{
			get
			{
				return (CommonServiceBehaviorElement)base["serviceBehaviors"];
			}
		}

		// Token: 0x06003B68 RID: 15208 RVA: 0x000E3776 File Offset: 0x000E1976
		internal static CommonBehaviorsSection GetSection()
		{
			return (CommonBehaviorsSection)ConfigurationHelpers.GetSection(ConfigurationStrings.CommonBehaviorsSectionPath);
		}

		// Token: 0x06003B69 RID: 15209 RVA: 0x000E3787 File Offset: 0x000E1987
		[SecurityCritical]
		internal static CommonBehaviorsSection UnsafeGetSection()
		{
			return (CommonBehaviorsSection)ConfigurationHelpers.UnsafeGetSection(ConfigurationStrings.CommonBehaviorsSectionPath);
		}

		// Token: 0x06003B6A RID: 15210 RVA: 0x000E3798 File Offset: 0x000E1998
		[SecurityCritical]
		internal static CommonBehaviorsSection UnsafeGetAssociatedSection(ContextInformation contextEval)
		{
			return (CommonBehaviorsSection)ConfigurationHelpers.UnsafeGetAssociatedSection(contextEval, ConfigurationStrings.CommonBehaviorsSectionPath);
		}

		// Token: 0x17000E36 RID: 3638
		// (get) Token: 0x06003B6B RID: 15211 RVA: 0x000E37AC File Offset: 0x000E19AC
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("endpointBehaviors", typeof(CommonEndpointBehaviorElement), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("serviceBehaviors", typeof(CommonServiceBehaviorElement), null, null, null, ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04002A85 RID: 10885
		private ConfigurationPropertyCollection properties;
	}
}
