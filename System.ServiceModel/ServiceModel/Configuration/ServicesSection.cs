using System;
using System.Configuration;
using System.Security;

namespace System.ServiceModel.Configuration
{
	// Token: 0x0200068F RID: 1679
	public sealed class ServicesSection : ConfigurationSection, IConfigurationContextProviderInternal
	{
		// Token: 0x17001077 RID: 4215
		// (get) Token: 0x060040F7 RID: 16631 RVA: 0x000F6DDC File Offset: 0x000F4FDC
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("", typeof(ServiceElementCollection), null, null, null, ConfigurationPropertyOptions.IsDefaultCollection)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x17001078 RID: 4216
		// (get) Token: 0x060040F9 RID: 16633 RVA: 0x000F6E2A File Offset: 0x000F502A
		[ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
		public ServiceElementCollection Services
		{
			get
			{
				return (ServiceElementCollection)base[""];
			}
		}

		// Token: 0x060040FA RID: 16634 RVA: 0x000F6E3C File Offset: 0x000F503C
		internal static ServicesSection GetSection()
		{
			return (ServicesSection)ConfigurationHelpers.GetSection(ConfigurationStrings.ServicesSectionPath);
		}

		// Token: 0x060040FB RID: 16635 RVA: 0x000F6E4D File Offset: 0x000F504D
		[SecurityCritical]
		internal static ServicesSection UnsafeGetSection()
		{
			return (ServicesSection)ConfigurationHelpers.UnsafeGetSection(ConfigurationStrings.ServicesSectionPath);
		}

		// Token: 0x060040FC RID: 16636 RVA: 0x000F6E5E File Offset: 0x000F505E
		protected override void PostDeserialize()
		{
			this.ValidateSection();
			base.PostDeserialize();
		}

		// Token: 0x060040FD RID: 16637 RVA: 0x000F6E6C File Offset: 0x000F506C
		private void ValidateSection()
		{
			ContextInformation evaluationContext = ConfigurationHelpers.GetEvaluationContext(this);
			if (evaluationContext != null)
			{
				foreach (object obj in this.Services)
				{
					ServiceElement serviceElement = (ServiceElement)obj;
					BehaviorsSection.ValidateServiceBehaviorReference(serviceElement.BehaviorConfiguration, evaluationContext, serviceElement);
					foreach (object obj2 in serviceElement.Endpoints)
					{
						ServiceEndpointElement serviceEndpointElement = (ServiceEndpointElement)obj2;
						if (string.IsNullOrEmpty(serviceEndpointElement.Kind))
						{
							if (!string.IsNullOrEmpty(serviceEndpointElement.EndpointConfiguration))
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigInvalidAttribute", new object[]
								{
									"endpointConfiguration",
									"endpoint",
									"kind"
								})));
							}
							if (string.IsNullOrEmpty(serviceEndpointElement.Binding))
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("RequiredAttributeMissing", new object[]
								{
									"binding",
									"endpoint"
								})));
							}
						}
						if (string.IsNullOrEmpty(serviceEndpointElement.Binding) && !string.IsNullOrEmpty(serviceEndpointElement.BindingConfiguration))
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigInvalidAttribute", new object[]
							{
								"bindingConfiguration",
								"endpoint",
								"binding"
							})));
						}
						BehaviorsSection.ValidateEndpointBehaviorReference(serviceEndpointElement.BehaviorConfiguration, evaluationContext, serviceEndpointElement);
						BindingsSection.ValidateBindingReference(serviceEndpointElement.Binding, serviceEndpointElement.BindingConfiguration, evaluationContext, serviceEndpointElement);
						StandardEndpointsSection.ValidateEndpointReference(serviceEndpointElement.Kind, serviceEndpointElement.EndpointConfiguration, evaluationContext, serviceEndpointElement);
					}
				}
			}
		}

		// Token: 0x060040FE RID: 16638 RVA: 0x000F7068 File Offset: 0x000F5268
		[SecurityCritical]
		protected override void Reset(ConfigurationElement parentElement)
		{
			this.contextHelper.OnReset(parentElement);
			base.Reset(parentElement);
		}

		// Token: 0x060040FF RID: 16639 RVA: 0x000F707D File Offset: 0x000F527D
		ContextInformation IConfigurationContextProviderInternal.GetEvaluationContext()
		{
			return base.EvaluationContext;
		}

		// Token: 0x06004100 RID: 16640 RVA: 0x000F7085 File Offset: 0x000F5285
		[SecurityCritical]
		ContextInformation IConfigurationContextProviderInternal.GetOriginalEvaluationContext()
		{
			return this.contextHelper.GetOriginalContext(this);
		}

		// Token: 0x04002CDC RID: 11484
		private ConfigurationPropertyCollection properties;

		// Token: 0x04002CDD RID: 11485
		[SecurityCritical]
		private EvaluationContextHelper contextHelper;
	}
}
