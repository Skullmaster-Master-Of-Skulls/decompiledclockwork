using System;
using System.Configuration;
using System.ServiceModel.Description;
using System.ServiceModel.PeerResolvers;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000669 RID: 1641
	public sealed class PeerCustomResolverElement : ServiceModelConfigurationElement
	{
		// Token: 0x17000FB3 RID: 4019
		// (get) Token: 0x06003F0F RID: 16143 RVA: 0x000EF9F6 File Offset: 0x000EDBF6
		// (set) Token: 0x06003F10 RID: 16144 RVA: 0x000EFA08 File Offset: 0x000EDC08
		[ConfigurationProperty("address", DefaultValue = null, Options = ConfigurationPropertyOptions.None)]
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

		// Token: 0x17000FB4 RID: 4020
		// (get) Token: 0x06003F11 RID: 16145 RVA: 0x000EFA16 File Offset: 0x000EDC16
		[ConfigurationProperty("headers")]
		public AddressHeaderCollectionElement Headers
		{
			get
			{
				return (AddressHeaderCollectionElement)base["headers"];
			}
		}

		// Token: 0x17000FB5 RID: 4021
		// (get) Token: 0x06003F12 RID: 16146 RVA: 0x000EFA28 File Offset: 0x000EDC28
		[ConfigurationProperty("identity")]
		public IdentityElement Identity
		{
			get
			{
				return (IdentityElement)base["identity"];
			}
		}

		// Token: 0x17000FB6 RID: 4022
		// (get) Token: 0x06003F13 RID: 16147 RVA: 0x000EFA3A File Offset: 0x000EDC3A
		// (set) Token: 0x06003F14 RID: 16148 RVA: 0x000EFA4C File Offset: 0x000EDC4C
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
				base["binding"] = value;
			}
		}

		// Token: 0x17000FB7 RID: 4023
		// (get) Token: 0x06003F15 RID: 16149 RVA: 0x000EFA5A File Offset: 0x000EDC5A
		// (set) Token: 0x06003F16 RID: 16150 RVA: 0x000EFA6C File Offset: 0x000EDC6C
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
				base["bindingConfiguration"] = value;
			}
		}

		// Token: 0x17000FB8 RID: 4024
		// (get) Token: 0x06003F17 RID: 16151 RVA: 0x000EFA7A File Offset: 0x000EDC7A
		// (set) Token: 0x06003F18 RID: 16152 RVA: 0x000EFA8C File Offset: 0x000EDC8C
		[ConfigurationProperty("resolverType", DefaultValue = "")]
		[StringValidator(MinLength = 0)]
		public string ResolverType
		{
			get
			{
				return (string)base["resolverType"];
			}
			set
			{
				base["resolverType"] = value;
			}
		}

		// Token: 0x06003F19 RID: 16153 RVA: 0x000EFA9C File Offset: 0x000EDC9C
		internal void ApplyConfiguration(PeerCustomResolverSettings settings)
		{
			if (settings == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("settings");
			}
			if (this.Address != null)
			{
				settings.Address = new EndpointAddress(this.Address, ConfigLoader.LoadIdentity(this.Identity), this.Headers.Headers);
			}
			settings.BindingSection = this.Binding;
			settings.BindingConfiguration = this.BindingConfiguration;
			if (!string.IsNullOrEmpty(this.Binding) && !string.IsNullOrEmpty(this.BindingConfiguration))
			{
				settings.Binding = ConfigLoader.LookupBinding(this.Binding, this.BindingConfiguration);
			}
			if (string.IsNullOrEmpty(this.ResolverType))
			{
				return;
			}
			Type type = Type.GetType(this.ResolverType, false);
			if (type != null)
			{
				settings.Resolver = (Activator.CreateInstance(type) as PeerResolver);
				return;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("PeerResolverInvalid", new object[]
			{
				this.ResolverType
			})));
		}

		// Token: 0x06003F1A RID: 16154 RVA: 0x000EFB98 File Offset: 0x000EDD98
		internal void InitializeFrom(PeerCustomResolverSettings settings)
		{
			if (settings == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("settings");
			}
			if (settings.Address != null)
			{
				base.SetPropertyValueIfNotDefaultValue<Uri>("address", settings.Address.Uri);
				this.Identity.InitializeFrom(settings.Address.Identity);
			}
			if (settings.Resolver != null)
			{
				base.SetPropertyValueIfNotDefaultValue<string>("resolverType", settings.Resolver.GetType().AssemblyQualifiedName);
			}
			if (settings.Binding != null)
			{
				base.SetPropertyValueIfNotDefaultValue<string>("bindingConfiguration", "PeerCustomResolver" + Guid.NewGuid().ToString());
				string binding;
				BindingsSection.TryAdd(this.BindingConfiguration, settings.Binding, out binding);
				this.Binding = binding;
			}
		}

		// Token: 0x17000FB9 RID: 4025
		// (get) Token: 0x06003F1B RID: 16155 RVA: 0x000EFC60 File Offset: 0x000EDE60
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("address", typeof(Uri), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("headers", typeof(AddressHeaderCollectionElement), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("identity", typeof(IdentityElement), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("binding", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("bindingConfiguration", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("resolverType", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04002CB0 RID: 11440
		private ConfigurationPropertyCollection properties;
	}
}
