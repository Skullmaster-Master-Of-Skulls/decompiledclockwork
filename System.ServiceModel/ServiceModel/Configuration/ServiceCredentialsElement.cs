using System;
using System.Configuration;
using System.IdentityModel.Configuration;
using System.ServiceModel.Description;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000685 RID: 1669
	public class ServiceCredentialsElement : BehaviorExtensionElement
	{
		// Token: 0x1700102D RID: 4141
		// (get) Token: 0x06004057 RID: 16471 RVA: 0x000F46A0 File Offset: 0x000F28A0
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("type", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("clientCertificate", typeof(X509InitiatorCertificateServiceElement), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("serviceCertificate", typeof(X509RecipientCertificateServiceElement), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("userNameAuthentication", typeof(UserNameServiceElement), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("useIdentityConfiguration", typeof(bool), false, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("identityConfiguration", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("windowsAuthentication", typeof(WindowsServiceElement), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("peer", typeof(PeerCredentialElement), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("issuedTokenAuthentication", typeof(IssuedTokenServiceElement), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("secureConversationAuthentication", typeof(SecureConversationServiceElement), null, null, null, ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x1700102E RID: 4142
		// (get) Token: 0x06004059 RID: 16473 RVA: 0x000F4822 File Offset: 0x000F2A22
		// (set) Token: 0x0600405A RID: 16474 RVA: 0x000F4834 File Offset: 0x000F2A34
		[ConfigurationProperty("type", DefaultValue = "")]
		[StringValidator(MinLength = 0)]
		public string Type
		{
			get
			{
				return (string)base["type"];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					value = string.Empty;
				}
				base["type"] = value;
			}
		}

		// Token: 0x1700102F RID: 4143
		// (get) Token: 0x0600405B RID: 16475 RVA: 0x000F4851 File Offset: 0x000F2A51
		[ConfigurationProperty("clientCertificate")]
		public X509InitiatorCertificateServiceElement ClientCertificate
		{
			get
			{
				return (X509InitiatorCertificateServiceElement)base["clientCertificate"];
			}
		}

		// Token: 0x17001030 RID: 4144
		// (get) Token: 0x0600405C RID: 16476 RVA: 0x000F4863 File Offset: 0x000F2A63
		[ConfigurationProperty("serviceCertificate")]
		public X509RecipientCertificateServiceElement ServiceCertificate
		{
			get
			{
				return (X509RecipientCertificateServiceElement)base["serviceCertificate"];
			}
		}

		// Token: 0x17001031 RID: 4145
		// (get) Token: 0x0600405D RID: 16477 RVA: 0x000F4875 File Offset: 0x000F2A75
		[ConfigurationProperty("userNameAuthentication")]
		public UserNameServiceElement UserNameAuthentication
		{
			get
			{
				return (UserNameServiceElement)base["userNameAuthentication"];
			}
		}

		// Token: 0x17001032 RID: 4146
		// (get) Token: 0x0600405E RID: 16478 RVA: 0x000F4887 File Offset: 0x000F2A87
		// (set) Token: 0x0600405F RID: 16479 RVA: 0x000F4899 File Offset: 0x000F2A99
		[ConfigurationProperty("useIdentityConfiguration", DefaultValue = false, IsRequired = false)]
		public bool UseIdentityConfiguration
		{
			get
			{
				return (bool)base["useIdentityConfiguration"];
			}
			set
			{
				base["useIdentityConfiguration"] = value;
			}
		}

		// Token: 0x17001033 RID: 4147
		// (get) Token: 0x06004060 RID: 16480 RVA: 0x000F48AC File Offset: 0x000F2AAC
		// (set) Token: 0x06004061 RID: 16481 RVA: 0x000F48BE File Offset: 0x000F2ABE
		[ConfigurationProperty("identityConfiguration", IsRequired = false, DefaultValue = "")]
		[StringValidator(MinLength = 0)]
		public string IdentityConfiguration
		{
			get
			{
				return (string)base["identityConfiguration"];
			}
			set
			{
				base["identityConfiguration"] = value;
			}
		}

		// Token: 0x17001034 RID: 4148
		// (get) Token: 0x06004062 RID: 16482 RVA: 0x000F48CC File Offset: 0x000F2ACC
		[ConfigurationProperty("windowsAuthentication")]
		public WindowsServiceElement WindowsAuthentication
		{
			get
			{
				return (WindowsServiceElement)base["windowsAuthentication"];
			}
		}

		// Token: 0x17001035 RID: 4149
		// (get) Token: 0x06004063 RID: 16483 RVA: 0x000F48DE File Offset: 0x000F2ADE
		[ConfigurationProperty("peer")]
		public PeerCredentialElement Peer
		{
			get
			{
				return (PeerCredentialElement)base["peer"];
			}
		}

		// Token: 0x17001036 RID: 4150
		// (get) Token: 0x06004064 RID: 16484 RVA: 0x000F48F0 File Offset: 0x000F2AF0
		[ConfigurationProperty("issuedTokenAuthentication")]
		public IssuedTokenServiceElement IssuedTokenAuthentication
		{
			get
			{
				return (IssuedTokenServiceElement)base["issuedTokenAuthentication"];
			}
		}

		// Token: 0x17001037 RID: 4151
		// (get) Token: 0x06004065 RID: 16485 RVA: 0x000F4902 File Offset: 0x000F2B02
		[ConfigurationProperty("secureConversationAuthentication")]
		public SecureConversationServiceElement SecureConversationAuthentication
		{
			get
			{
				return (SecureConversationServiceElement)base["secureConversationAuthentication"];
			}
		}

		// Token: 0x06004066 RID: 16486 RVA: 0x000F4914 File Offset: 0x000F2B14
		public override void CopyFrom(ServiceModelExtensionElement from)
		{
			base.CopyFrom(from);
			ServiceCredentialsElement serviceCredentialsElement = (ServiceCredentialsElement)from;
			this.ClientCertificate.Copy(serviceCredentialsElement.ClientCertificate);
			this.ServiceCertificate.Copy(serviceCredentialsElement.ServiceCertificate);
			this.UserNameAuthentication.Copy(serviceCredentialsElement.UserNameAuthentication);
			this.WindowsAuthentication.Copy(serviceCredentialsElement.WindowsAuthentication);
			this.Peer.Copy(serviceCredentialsElement.Peer);
			this.IssuedTokenAuthentication.Copy(serviceCredentialsElement.IssuedTokenAuthentication);
			this.SecureConversationAuthentication.Copy(serviceCredentialsElement.SecureConversationAuthentication);
			this.Type = serviceCredentialsElement.Type;
			this.UseIdentityConfiguration = serviceCredentialsElement.UseIdentityConfiguration;
			this.IdentityConfiguration = serviceCredentialsElement.IdentityConfiguration;
		}

		// Token: 0x06004067 RID: 16487 RVA: 0x000F49CC File Offset: 0x000F2BCC
		protected internal override object CreateBehavior()
		{
			ServiceCredentials serviceCredentials;
			if (string.IsNullOrEmpty(this.Type))
			{
				serviceCredentials = new ServiceCredentials();
			}
			else
			{
				Type type = System.Type.GetType(this.Type, true);
				if (!typeof(ServiceCredentials).IsAssignableFrom(type))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigInvalidServiceCredentialsType", new object[]
					{
						this.Type,
						type.AssemblyQualifiedName
					})));
				}
				serviceCredentials = (ServiceCredentials)Activator.CreateInstance(type);
			}
			this.ApplyConfiguration(serviceCredentials);
			return serviceCredentials;
		}

		// Token: 0x06004068 RID: 16488 RVA: 0x000F4A54 File Offset: 0x000F2C54
		protected internal void ApplyConfiguration(ServiceCredentials behavior)
		{
			if (behavior == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("behavior");
			}
			PropertyInformationCollection propertyInformationCollection = base.ElementInformation.Properties;
			if (propertyInformationCollection["userNameAuthentication"].ValueOrigin != PropertyValueOrigin.Default)
			{
				this.UserNameAuthentication.ApplyConfiguration(behavior.UserNameAuthentication);
			}
			if (propertyInformationCollection["windowsAuthentication"].ValueOrigin != PropertyValueOrigin.Default)
			{
				this.WindowsAuthentication.ApplyConfiguration(behavior.WindowsAuthentication);
			}
			if (propertyInformationCollection["clientCertificate"].ValueOrigin != PropertyValueOrigin.Default)
			{
				this.ClientCertificate.ApplyConfiguration(behavior.ClientCertificate);
			}
			if (propertyInformationCollection["serviceCertificate"].ValueOrigin != PropertyValueOrigin.Default)
			{
				this.ServiceCertificate.ApplyConfiguration(behavior.ServiceCertificate);
			}
			if (propertyInformationCollection["peer"].ValueOrigin != PropertyValueOrigin.Default)
			{
				this.Peer.ApplyConfiguration(behavior.Peer);
			}
			if (propertyInformationCollection["issuedTokenAuthentication"].ValueOrigin != PropertyValueOrigin.Default)
			{
				this.IssuedTokenAuthentication.ApplyConfiguration(behavior.IssuedTokenAuthentication);
			}
			if (propertyInformationCollection["secureConversationAuthentication"].ValueOrigin != PropertyValueOrigin.Default)
			{
				this.SecureConversationAuthentication.ApplyConfiguration(behavior.SecureConversationAuthentication);
			}
			if (propertyInformationCollection["useIdentityConfiguration"].ValueOrigin != PropertyValueOrigin.Default)
			{
				behavior.UseIdentityConfiguration = this.UseIdentityConfiguration;
			}
			if (propertyInformationCollection["identityConfiguration"].ValueOrigin != PropertyValueOrigin.Default)
			{
				behavior.IdentityConfiguration = new IdentityConfiguration(this.IdentityConfiguration);
			}
		}

		// Token: 0x17001038 RID: 4152
		// (get) Token: 0x06004069 RID: 16489 RVA: 0x000F4BB6 File Offset: 0x000F2DB6
		public override Type BehaviorType
		{
			get
			{
				return typeof(ServiceCredentials);
			}
		}

		// Token: 0x04002CD0 RID: 11472
		private ConfigurationPropertyCollection properties;
	}
}
