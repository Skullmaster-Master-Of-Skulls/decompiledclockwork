using System;
using System.Configuration;
using System.Security;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000602 RID: 1538
	public sealed class ClientSection : ConfigurationSection, IConfigurationContextProviderInternal
	{
		// Token: 0x17000E22 RID: 3618
		// (get) Token: 0x06003B3F RID: 15167 RVA: 0x000E2EEF File Offset: 0x000E10EF
		[ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
		public ChannelEndpointElementCollection Endpoints
		{
			get
			{
				return (ChannelEndpointElementCollection)base[""];
			}
		}

		// Token: 0x17000E23 RID: 3619
		// (get) Token: 0x06003B40 RID: 15168 RVA: 0x000E2F01 File Offset: 0x000E1101
		[ConfigurationProperty("metadata")]
		public MetadataElement Metadata
		{
			get
			{
				return (MetadataElement)base["metadata"];
			}
		}

		// Token: 0x06003B41 RID: 15169 RVA: 0x000E2F13 File Offset: 0x000E1113
		internal static ClientSection GetSection()
		{
			return (ClientSection)ConfigurationHelpers.GetSection(ConfigurationStrings.ClientSectionPath);
		}

		// Token: 0x06003B42 RID: 15170 RVA: 0x000E2F24 File Offset: 0x000E1124
		[SecurityCritical]
		internal static ClientSection UnsafeGetSection()
		{
			return (ClientSection)ConfigurationHelpers.UnsafeGetSection(ConfigurationStrings.ClientSectionPath);
		}

		// Token: 0x06003B43 RID: 15171 RVA: 0x000E2F35 File Offset: 0x000E1135
		[SecurityCritical]
		internal static ClientSection UnsafeGetSection(ContextInformation contextInformation)
		{
			return (ClientSection)ConfigurationHelpers.UnsafeGetSectionFromContext(contextInformation, ConfigurationStrings.ClientSectionPath);
		}

		// Token: 0x06003B44 RID: 15172 RVA: 0x000E2F47 File Offset: 0x000E1147
		protected override void InitializeDefault()
		{
			this.Metadata.SetDefaults();
		}

		// Token: 0x06003B45 RID: 15173 RVA: 0x000E2F54 File Offset: 0x000E1154
		protected override void PostDeserialize()
		{
			this.ValidateSection();
			base.PostDeserialize();
		}

		// Token: 0x06003B46 RID: 15174 RVA: 0x000E2F64 File Offset: 0x000E1164
		private void ValidateSection()
		{
			ContextInformation evaluationContext = ConfigurationHelpers.GetEvaluationContext(this);
			if (evaluationContext != null)
			{
				foreach (object obj in this.Endpoints)
				{
					ChannelEndpointElement channelEndpointElement = (ChannelEndpointElement)obj;
					if (string.IsNullOrEmpty(channelEndpointElement.Kind))
					{
						if (!string.IsNullOrEmpty(channelEndpointElement.EndpointConfiguration))
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigInvalidAttribute", new object[]
							{
								"endpointConfiguration",
								"endpoint",
								"kind"
							})));
						}
						if (string.IsNullOrEmpty(channelEndpointElement.Binding))
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("RequiredAttributeMissing", new object[]
							{
								"binding",
								"endpoint"
							})));
						}
						if (string.IsNullOrEmpty(channelEndpointElement.Contract))
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("RequiredAttributeMissing", new object[]
							{
								"contract",
								"endpoint"
							})));
						}
					}
					if (string.IsNullOrEmpty(channelEndpointElement.Binding) && !string.IsNullOrEmpty(channelEndpointElement.BindingConfiguration))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigInvalidAttribute", new object[]
						{
							"bindingConfiguration",
							"endpoint",
							"binding"
						})));
					}
					BehaviorsSection.ValidateEndpointBehaviorReference(channelEndpointElement.BehaviorConfiguration, evaluationContext, channelEndpointElement);
					BindingsSection.ValidateBindingReference(channelEndpointElement.Binding, channelEndpointElement.BindingConfiguration, evaluationContext, channelEndpointElement);
					StandardEndpointsSection.ValidateEndpointReference(channelEndpointElement.Kind, channelEndpointElement.EndpointConfiguration, evaluationContext, channelEndpointElement);
				}
			}
		}

		// Token: 0x06003B47 RID: 15175 RVA: 0x000E312C File Offset: 0x000E132C
		ContextInformation IConfigurationContextProviderInternal.GetEvaluationContext()
		{
			return base.EvaluationContext;
		}

		// Token: 0x06003B48 RID: 15176 RVA: 0x000E3134 File Offset: 0x000E1334
		ContextInformation IConfigurationContextProviderInternal.GetOriginalEvaluationContext()
		{
			return null;
		}

		// Token: 0x17000E24 RID: 3620
		// (get) Token: 0x06003B49 RID: 15177 RVA: 0x000E3138 File Offset: 0x000E1338
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("", typeof(ChannelEndpointElementCollection), null, null, null, ConfigurationPropertyOptions.IsDefaultCollection),
						new ConfigurationProperty("metadata", typeof(MetadataElement), null, null, null, ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04002A82 RID: 10882
		private ConfigurationPropertyCollection properties;
	}
}
