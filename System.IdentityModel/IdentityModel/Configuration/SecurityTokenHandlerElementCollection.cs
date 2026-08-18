using System;
using System.Configuration;
using System.IdentityModel.Tokens;

namespace System.IdentityModel.Configuration
{
	// Token: 0x020001CD RID: 461
	[ConfigurationCollection(typeof(CustomTypeElement), CollectionType = ConfigurationElementCollectionType.BasicMap)]
	public sealed class SecurityTokenHandlerElementCollection : ConfigurationElementCollection
	{
		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06000F1D RID: 3869 RVA: 0x00043708 File Offset: 0x00041908
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("name", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.IsKey),
						new ConfigurationProperty("securityTokenHandlerConfiguration", typeof(SecurityTokenHandlerConfigurationElement), null, null, null, ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x06000F1F RID: 3871 RVA: 0x0004377B File Offset: 0x0004197B
		protected override ConfigurationElement CreateNewElement()
		{
			return new CustomTypeElement();
		}

		// Token: 0x06000F20 RID: 3872 RVA: 0x00043782 File Offset: 0x00041982
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((CustomTypeElement)element).Type;
		}

		// Token: 0x06000F21 RID: 3873 RVA: 0x00043790 File Offset: 0x00041990
		protected override void Init()
		{
			this.BaseAdd(new CustomTypeElement(typeof(SamlSecurityTokenHandler)));
			this.BaseAdd(new CustomTypeElement(typeof(Saml2SecurityTokenHandler)));
			this.BaseAdd(new CustomTypeElement(typeof(WindowsUserNameSecurityTokenHandler)));
			this.BaseAdd(new CustomTypeElement(typeof(X509SecurityTokenHandler)));
			this.BaseAdd(new CustomTypeElement(typeof(KerberosSecurityTokenHandler)));
			this.BaseAdd(new CustomTypeElement(typeof(RsaSecurityTokenHandler)));
			this.BaseAdd(new CustomTypeElement(typeof(SessionSecurityTokenHandler)));
			this.BaseAdd(new CustomTypeElement(typeof(EncryptedSecurityTokenHandler)));
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06000F22 RID: 3874 RVA: 0x00042D31 File Offset: 0x00040F31
		// (set) Token: 0x06000F23 RID: 3875 RVA: 0x00042D43 File Offset: 0x00040F43
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
				base["name"] = value;
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06000F24 RID: 3876 RVA: 0x00043845 File Offset: 0x00041A45
		// (set) Token: 0x06000F25 RID: 3877 RVA: 0x00043857 File Offset: 0x00041A57
		[ConfigurationProperty("securityTokenHandlerConfiguration", IsRequired = false)]
		public SecurityTokenHandlerConfigurationElement SecurityTokenHandlerConfiguration
		{
			get
			{
				return (SecurityTokenHandlerConfigurationElement)base["securityTokenHandlerConfiguration"];
			}
			set
			{
				base["securityTokenHandlerConfiguration"] = value;
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06000F26 RID: 3878 RVA: 0x00043865 File Offset: 0x00041A65
		internal bool IsConfigured
		{
			get
			{
				return base.ElementInformation.Properties["name"].ValueOrigin != PropertyValueOrigin.Default || this.SecurityTokenHandlerConfiguration.IsConfigured || base.Count > 0;
			}
		}

		// Token: 0x04000D81 RID: 3457
		private ConfigurationPropertyCollection properties;
	}
}
