using System;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x0200061A RID: 1562
	public class EndpointAddressElementBase : ServiceModelConfigurationElement
	{
		// Token: 0x06003C0A RID: 15370 RVA: 0x000E57C5 File Offset: 0x000E39C5
		protected EndpointAddressElementBase()
		{
		}

		// Token: 0x17000E6B RID: 3691
		// (get) Token: 0x06003C0B RID: 15371 RVA: 0x000E57CD File Offset: 0x000E39CD
		// (set) Token: 0x06003C0C RID: 15372 RVA: 0x000E57DF File Offset: 0x000E39DF
		[ConfigurationProperty("address", DefaultValue = null, Options = ConfigurationPropertyOptions.IsRequired)]
		public Uri Address
		{
			get
			{
				return (Uri)base["address"];
			}
			set
			{
				base["address"] = value;
			}
		}

		// Token: 0x17000E6C RID: 3692
		// (get) Token: 0x06003C0D RID: 15373 RVA: 0x000E57ED File Offset: 0x000E39ED
		[ConfigurationProperty("headers")]
		public AddressHeaderCollectionElement Headers
		{
			get
			{
				return (AddressHeaderCollectionElement)base["headers"];
			}
		}

		// Token: 0x17000E6D RID: 3693
		// (get) Token: 0x06003C0E RID: 15374 RVA: 0x000E57FF File Offset: 0x000E39FF
		[ConfigurationProperty("identity")]
		public IdentityElement Identity
		{
			get
			{
				return (IdentityElement)base["identity"];
			}
		}

		// Token: 0x06003C0F RID: 15375 RVA: 0x000E5814 File Offset: 0x000E3A14
		protected internal void Copy(EndpointAddressElementBase source)
		{
			if (this.IsReadOnly())
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigReadOnly")));
			}
			if (source == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("source");
			}
			this.Address = source.Address;
			this.Headers.Headers = source.Headers.Headers;
			PropertyInformationCollection propertyInformationCollection = source.ElementInformation.Properties;
			if (propertyInformationCollection["identity"].ValueOrigin != PropertyValueOrigin.Default)
			{
				this.Identity.Copy(source.Identity);
			}
		}

		// Token: 0x06003C10 RID: 15376 RVA: 0x000E58A8 File Offset: 0x000E3AA8
		public void InitializeFrom(EndpointAddress endpointAddress)
		{
			if (null == endpointAddress)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("endpointAddress");
			}
			base.SetPropertyValueIfNotDefaultValue<Uri>("address", endpointAddress.Uri);
			this.Headers.InitializeFrom(endpointAddress.Headers);
			if (endpointAddress.Identity != null)
			{
				this.Identity.InitializeFrom(endpointAddress.Identity);
			}
		}

		// Token: 0x17000E6E RID: 3694
		// (get) Token: 0x06003C11 RID: 15377 RVA: 0x000E590C File Offset: 0x000E3B0C
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("address", typeof(Uri), null, null, null, ConfigurationPropertyOptions.IsRequired),
						new ConfigurationProperty("headers", typeof(AddressHeaderCollectionElement), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("identity", typeof(IdentityElement), null, null, null, ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04002C7A RID: 11386
		private ConfigurationPropertyCollection properties;
	}
}
