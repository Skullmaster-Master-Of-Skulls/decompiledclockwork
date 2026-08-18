using System;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000637 RID: 1591
	public sealed class IssuedTokenParametersEndpointAddressElement : EndpointAddressElementBase, IConfigurationContextProviderInternal
	{
		// Token: 0x17000EE2 RID: 3810
		// (get) Token: 0x06003D16 RID: 15638 RVA: 0x000E9588 File Offset: 0x000E7788
		// (set) Token: 0x06003D17 RID: 15639 RVA: 0x000E959A File Offset: 0x000E779A
		[ConfigurationProperty("binding", DefaultValue = "")]
		[StringValidator(MinLength = 0)]
		public string Binding
		{
			get
			{
				return (string)base["binding"];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					value = string.Empty;
				}
				base["binding"] = value;
			}
		}

		// Token: 0x17000EE3 RID: 3811
		// (get) Token: 0x06003D18 RID: 15640 RVA: 0x000E95B7 File Offset: 0x000E77B7
		// (set) Token: 0x06003D19 RID: 15641 RVA: 0x000E95C9 File Offset: 0x000E77C9
		[ConfigurationProperty("bindingConfiguration", DefaultValue = "")]
		[StringValidator(MinLength = 0)]
		public string BindingConfiguration
		{
			get
			{
				return (string)base["bindingConfiguration"];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					value = string.Empty;
				}
				base["bindingConfiguration"] = value;
			}
		}

		// Token: 0x06003D1A RID: 15642 RVA: 0x000E95E6 File Offset: 0x000E77E6
		internal void Copy(IssuedTokenParametersEndpointAddressElement source)
		{
			base.Copy(source);
			this.BindingConfiguration = source.BindingConfiguration;
			this.Binding = source.Binding;
		}

		// Token: 0x06003D1B RID: 15643 RVA: 0x000E9608 File Offset: 0x000E7808
		internal void Validate()
		{
			ContextInformation evaluationContext = ConfigurationHelpers.GetEvaluationContext(this);
			if (evaluationContext != null && !string.IsNullOrEmpty(this.Binding))
			{
				BindingsSection.ValidateBindingReference(this.Binding, this.BindingConfiguration, evaluationContext, this);
			}
		}

		// Token: 0x06003D1C RID: 15644 RVA: 0x000E963F File Offset: 0x000E783F
		ContextInformation IConfigurationContextProviderInternal.GetEvaluationContext()
		{
			return base.EvaluationContext;
		}

		// Token: 0x06003D1D RID: 15645 RVA: 0x000E9647 File Offset: 0x000E7847
		ContextInformation IConfigurationContextProviderInternal.GetOriginalEvaluationContext()
		{
			return null;
		}

		// Token: 0x17000EE4 RID: 3812
		// (get) Token: 0x06003D1E RID: 15646 RVA: 0x000E964C File Offset: 0x000E784C
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					object lockObj = this.lockObj;
					lock (lockObj)
					{
						if (this.properties == null)
						{
							ConfigurationPropertyCollection configurationPropertyCollection = base.Properties;
							configurationPropertyCollection.Add(new ConfigurationProperty("binding", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("bindingConfiguration", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.None));
							this.properties = configurationPropertyCollection;
						}
					}
				}
				return this.properties;
			}
		}

		// Token: 0x04002C8C RID: 11404
		private ConfigurationPropertyCollection properties;
	}
}
