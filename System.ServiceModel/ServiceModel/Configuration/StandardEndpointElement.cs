using System;
using System.Configuration;
using System.Security;
using System.ServiceModel.Description;

namespace System.ServiceModel.Configuration
{
	// Token: 0x0200067B RID: 1659
	public abstract class StandardEndpointElement : ConfigurationElement, IConfigurationContextProviderInternal
	{
		// Token: 0x17000FF1 RID: 4081
		// (get) Token: 0x06003FB6 RID: 16310 RVA: 0x000F18E4 File Offset: 0x000EFAE4
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("name", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.IsKey)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x17000FF2 RID: 4082
		// (get) Token: 0x06003FB8 RID: 16312
		protected internal abstract Type EndpointType { get; }

		// Token: 0x17000FF3 RID: 4083
		// (get) Token: 0x06003FB9 RID: 16313 RVA: 0x000F194C File Offset: 0x000EFB4C
		// (set) Token: 0x06003FBA RID: 16314 RVA: 0x000F195E File Offset: 0x000EFB5E
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
				if (string.IsNullOrEmpty(value))
				{
					value = string.Empty;
				}
				base["name"] = value;
			}
		}

		// Token: 0x06003FBB RID: 16315 RVA: 0x000F197B File Offset: 0x000EFB7B
		public void InitializeAndValidate(ChannelEndpointElement channelEndpointElement)
		{
			if (channelEndpointElement == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("channelEndpointElement");
			}
			this.OnInitializeAndValidate(channelEndpointElement);
		}

		// Token: 0x06003FBC RID: 16316 RVA: 0x000F1997 File Offset: 0x000EFB97
		public void InitializeAndValidate(ServiceEndpointElement serviceEndpointElement)
		{
			if (serviceEndpointElement == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("serviceEndpointElement");
			}
			this.OnInitializeAndValidate(serviceEndpointElement);
		}

		// Token: 0x06003FBD RID: 16317 RVA: 0x000F19B4 File Offset: 0x000EFBB4
		public void ApplyConfiguration(ServiceEndpoint endpoint, ChannelEndpointElement channelEndpointElement)
		{
			if (endpoint == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("endpoint");
			}
			if (channelEndpointElement == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("channelEndpointElement");
			}
			if (endpoint.GetType() != this.EndpointType)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("ConfigInvalidTypeForEndpoint", new object[]
				{
					this.EndpointType.AssemblyQualifiedName,
					endpoint.GetType().AssemblyQualifiedName
				}));
			}
			this.OnApplyConfiguration(endpoint, channelEndpointElement);
		}

		// Token: 0x06003FBE RID: 16318 RVA: 0x000F1A3C File Offset: 0x000EFC3C
		public void ApplyConfiguration(ServiceEndpoint endpoint, ServiceEndpointElement serviceEndpointElement)
		{
			if (endpoint == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("endpoint");
			}
			if (serviceEndpointElement == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("serviceEndpointElement");
			}
			if (endpoint.GetType() != this.EndpointType)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("ConfigInvalidTypeForEndpoint", new object[]
				{
					(this.EndpointType == null) ? string.Empty : this.EndpointType.AssemblyQualifiedName,
					endpoint.GetType().AssemblyQualifiedName
				}));
			}
			this.OnApplyConfiguration(endpoint, serviceEndpointElement);
		}

		// Token: 0x06003FBF RID: 16319 RVA: 0x000F1AD8 File Offset: 0x000EFCD8
		protected internal virtual void InitializeFrom(ServiceEndpoint endpoint)
		{
			if (endpoint == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("endpoint");
			}
			if (endpoint.GetType() != this.EndpointType)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("ConfigInvalidTypeForEndpoint", new object[]
				{
					(this.EndpointType == null) ? string.Empty : this.EndpointType.AssemblyQualifiedName,
					endpoint.GetType().AssemblyQualifiedName
				}));
			}
		}

		// Token: 0x06003FC0 RID: 16320
		protected internal abstract ServiceEndpoint CreateServiceEndpoint(ContractDescription contractDescription);

		// Token: 0x06003FC1 RID: 16321
		protected abstract void OnApplyConfiguration(ServiceEndpoint endpoint, ChannelEndpointElement channelEndpointElement);

		// Token: 0x06003FC2 RID: 16322
		protected abstract void OnApplyConfiguration(ServiceEndpoint endpoint, ServiceEndpointElement serviceEndpointElement);

		// Token: 0x06003FC3 RID: 16323
		protected abstract void OnInitializeAndValidate(ChannelEndpointElement channelEndpointElement);

		// Token: 0x06003FC4 RID: 16324
		protected abstract void OnInitializeAndValidate(ServiceEndpointElement serviceEndpointElement);

		// Token: 0x06003FC5 RID: 16325 RVA: 0x000F1B57 File Offset: 0x000EFD57
		[SecurityCritical]
		protected override void Reset(ConfigurationElement parentElement)
		{
			this.contextHelper.OnReset(parentElement);
			base.Reset(parentElement);
		}

		// Token: 0x06003FC6 RID: 16326 RVA: 0x000F1B6C File Offset: 0x000EFD6C
		ContextInformation IConfigurationContextProviderInternal.GetEvaluationContext()
		{
			return base.EvaluationContext;
		}

		// Token: 0x06003FC7 RID: 16327 RVA: 0x000F1B74 File Offset: 0x000EFD74
		[SecurityCritical]
		ContextInformation IConfigurationContextProviderInternal.GetOriginalEvaluationContext()
		{
			return this.contextHelper.GetOriginalContext(this);
		}

		// Token: 0x04002CBD RID: 11453
		private ConfigurationPropertyCollection properties;

		// Token: 0x04002CBE RID: 11454
		[SecurityCritical]
		private EvaluationContextHelper contextHelper;

		// Token: 0x04002CBF RID: 11455
		internal object lockObj = new object();
	}
}
