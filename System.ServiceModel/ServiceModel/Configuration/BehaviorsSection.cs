using System;
using System.Configuration;
using System.Security;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020005F4 RID: 1524
	public class BehaviorsSection : ConfigurationSection
	{
		// Token: 0x17000DE0 RID: 3552
		// (get) Token: 0x06003AAA RID: 15018 RVA: 0x000E15DD File Offset: 0x000DF7DD
		[ConfigurationProperty("endpointBehaviors", Options = ConfigurationPropertyOptions.None)]
		public EndpointBehaviorElementCollection EndpointBehaviors
		{
			get
			{
				return (EndpointBehaviorElementCollection)base["endpointBehaviors"];
			}
		}

		// Token: 0x17000DE1 RID: 3553
		// (get) Token: 0x06003AAB RID: 15019 RVA: 0x000E15EF File Offset: 0x000DF7EF
		[ConfigurationProperty("serviceBehaviors", Options = ConfigurationPropertyOptions.None)]
		public ServiceBehaviorElementCollection ServiceBehaviors
		{
			get
			{
				return (ServiceBehaviorElementCollection)base["serviceBehaviors"];
			}
		}

		// Token: 0x06003AAC RID: 15020 RVA: 0x000E1601 File Offset: 0x000DF801
		internal static BehaviorsSection GetSection()
		{
			return (BehaviorsSection)ConfigurationHelpers.GetSection(ConfigurationStrings.BehaviorsSectionPath);
		}

		// Token: 0x06003AAD RID: 15021 RVA: 0x000E1612 File Offset: 0x000DF812
		[SecurityCritical]
		internal static BehaviorsSection UnsafeGetSection()
		{
			return (BehaviorsSection)ConfigurationHelpers.UnsafeGetSection(ConfigurationStrings.BehaviorsSectionPath);
		}

		// Token: 0x06003AAE RID: 15022 RVA: 0x000E1623 File Offset: 0x000DF823
		[SecurityCritical]
		internal static BehaviorsSection UnsafeGetAssociatedSection(ContextInformation evalContext)
		{
			return (BehaviorsSection)ConfigurationHelpers.UnsafeGetAssociatedSection(evalContext, ConfigurationStrings.BehaviorsSectionPath);
		}

		// Token: 0x06003AAF RID: 15023 RVA: 0x000E1638 File Offset: 0x000DF838
		[SecuritySafeCritical]
		internal static void ValidateEndpointBehaviorReference(string behaviorConfiguration, ContextInformation evaluationContext, ConfigurationElement configurationElement)
		{
			if (evaluationContext == null)
			{
				DiagnosticUtility.FailFast("ValidateBehaviorReference() should only called with valid ContextInformation");
			}
			if (!string.IsNullOrEmpty(behaviorConfiguration))
			{
				BehaviorsSection behaviorsSection = (BehaviorsSection)ConfigurationHelpers.UnsafeGetAssociatedSection(evaluationContext, ConfigurationStrings.BehaviorsSectionPath);
				if (!behaviorsSection.EndpointBehaviors.ContainsKey(behaviorConfiguration))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigInvalidEndpointBehavior", new object[]
					{
						behaviorConfiguration
					}), configurationElement.ElementInformation.Source, configurationElement.ElementInformation.LineNumber));
				}
			}
		}

		// Token: 0x06003AB0 RID: 15024 RVA: 0x000E16B4 File Offset: 0x000DF8B4
		[SecuritySafeCritical]
		internal static void ValidateServiceBehaviorReference(string behaviorConfiguration, ContextInformation evaluationContext, ConfigurationElement configurationElement)
		{
			if (evaluationContext == null)
			{
				DiagnosticUtility.FailFast("ValidateBehaviorReference() should only called with valid ContextInformation");
			}
			if (!string.IsNullOrEmpty(behaviorConfiguration))
			{
				BehaviorsSection behaviorsSection = (BehaviorsSection)ConfigurationHelpers.UnsafeGetAssociatedSection(evaluationContext, ConfigurationStrings.BehaviorsSectionPath);
				if (!behaviorsSection.ServiceBehaviors.ContainsKey(behaviorConfiguration))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigInvalidServiceBehavior", new object[]
					{
						behaviorConfiguration
					}), configurationElement.ElementInformation.Source, configurationElement.ElementInformation.LineNumber));
				}
			}
		}

		// Token: 0x17000DE2 RID: 3554
		// (get) Token: 0x06003AB1 RID: 15025 RVA: 0x000E1730 File Offset: 0x000DF930
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("endpointBehaviors", typeof(EndpointBehaviorElementCollection), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("serviceBehaviors", typeof(ServiceBehaviorElementCollection), null, null, null, ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04002A75 RID: 10869
		private ConfigurationPropertyCollection properties;
	}
}
