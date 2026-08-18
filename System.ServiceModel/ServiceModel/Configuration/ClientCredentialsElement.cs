using System;
using System.Configuration;
using System.ServiceModel.Description;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000605 RID: 1541
	public class ClientCredentialsElement : BehaviorExtensionElement
	{
		// Token: 0x17000E29 RID: 3625
		// (get) Token: 0x06003B55 RID: 15189 RVA: 0x000E32B5 File Offset: 0x000E14B5
		// (set) Token: 0x06003B56 RID: 15190 RVA: 0x000E32C7 File Offset: 0x000E14C7
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

		// Token: 0x17000E2A RID: 3626
		// (get) Token: 0x06003B57 RID: 15191 RVA: 0x000E32E4 File Offset: 0x000E14E4
		// (set) Token: 0x06003B58 RID: 15192 RVA: 0x000E32F6 File Offset: 0x000E14F6
		[ConfigurationProperty("useIdentityConfiguration", DefaultValue = false)]
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

		// Token: 0x17000E2B RID: 3627
		// (get) Token: 0x06003B59 RID: 15193 RVA: 0x000E3309 File Offset: 0x000E1509
		[ConfigurationProperty("clientCertificate")]
		public X509InitiatorCertificateClientElement ClientCertificate
		{
			get
			{
				return (X509InitiatorCertificateClientElement)base["clientCertificate"];
			}
		}

		// Token: 0x17000E2C RID: 3628
		// (get) Token: 0x06003B5A RID: 15194 RVA: 0x000E331B File Offset: 0x000E151B
		[ConfigurationProperty("serviceCertificate")]
		public X509RecipientCertificateClientElement ServiceCertificate
		{
			get
			{
				return (X509RecipientCertificateClientElement)base["serviceCertificate"];
			}
		}

		// Token: 0x17000E2D RID: 3629
		// (get) Token: 0x06003B5B RID: 15195 RVA: 0x000E332D File Offset: 0x000E152D
		[ConfigurationProperty("windows")]
		public WindowsClientElement Windows
		{
			get
			{
				return (WindowsClientElement)base["windows"];
			}
		}

		// Token: 0x17000E2E RID: 3630
		// (get) Token: 0x06003B5C RID: 15196 RVA: 0x000E333F File Offset: 0x000E153F
		[ConfigurationProperty("issuedToken")]
		public IssuedTokenClientElement IssuedToken
		{
			get
			{
				return (IssuedTokenClientElement)base["issuedToken"];
			}
		}

		// Token: 0x17000E2F RID: 3631
		// (get) Token: 0x06003B5D RID: 15197 RVA: 0x000E3351 File Offset: 0x000E1551
		[ConfigurationProperty("httpDigest")]
		public HttpDigestClientElement HttpDigest
		{
			get
			{
				return (HttpDigestClientElement)base["httpDigest"];
			}
		}

		// Token: 0x17000E30 RID: 3632
		// (get) Token: 0x06003B5E RID: 15198 RVA: 0x000E3363 File Offset: 0x000E1563
		[ConfigurationProperty("peer")]
		public PeerCredentialElement Peer
		{
			get
			{
				return (PeerCredentialElement)base["peer"];
			}
		}

		// Token: 0x17000E31 RID: 3633
		// (get) Token: 0x06003B5F RID: 15199 RVA: 0x000E3375 File Offset: 0x000E1575
		// (set) Token: 0x06003B60 RID: 15200 RVA: 0x000E3387 File Offset: 0x000E1587
		[ConfigurationProperty("supportInteractive", DefaultValue = true)]
		public bool SupportInteractive
		{
			get
			{
				return (bool)base["supportInteractive"];
			}
			set
			{
				base["supportInteractive"] = value;
			}
		}

		// Token: 0x06003B61 RID: 15201 RVA: 0x000E339C File Offset: 0x000E159C
		public override void CopyFrom(ServiceModelExtensionElement from)
		{
			base.CopyFrom(from);
			ClientCredentialsElement clientCredentialsElement = (ClientCredentialsElement)from;
			this.ClientCertificate.Copy(clientCredentialsElement.ClientCertificate);
			this.ServiceCertificate.Copy(clientCredentialsElement.ServiceCertificate);
			this.Windows.Copy(clientCredentialsElement.Windows);
			this.IssuedToken.Copy(clientCredentialsElement.IssuedToken);
			this.HttpDigest.Copy(clientCredentialsElement.HttpDigest);
			this.Peer.Copy(clientCredentialsElement.Peer);
			this.SupportInteractive = clientCredentialsElement.SupportInteractive;
			this.Type = clientCredentialsElement.Type;
			this.UseIdentityConfiguration = clientCredentialsElement.UseIdentityConfiguration;
		}

		// Token: 0x06003B62 RID: 15202 RVA: 0x000E3444 File Offset: 0x000E1644
		protected internal override object CreateBehavior()
		{
			ClientCredentials clientCredentials;
			if (string.IsNullOrEmpty(this.Type))
			{
				clientCredentials = new ClientCredentials();
			}
			else
			{
				Type type = System.Type.GetType(this.Type, true);
				if (!typeof(ClientCredentials).IsAssignableFrom(type))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigInvalidClientCredentialsType", new object[]
					{
						this.Type,
						type.AssemblyQualifiedName
					})));
				}
				clientCredentials = (ClientCredentials)Activator.CreateInstance(type);
			}
			this.ApplyConfiguration(clientCredentials);
			return clientCredentials;
		}

		// Token: 0x06003B63 RID: 15203 RVA: 0x000E34CC File Offset: 0x000E16CC
		protected internal void ApplyConfiguration(ClientCredentials behavior)
		{
			if (behavior == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("behavior");
			}
			PropertyInformationCollection propertyInformationCollection = base.ElementInformation.Properties;
			if (propertyInformationCollection["windows"].ValueOrigin != PropertyValueOrigin.Default)
			{
				this.Windows.ApplyConfiguration(behavior.Windows);
			}
			if (propertyInformationCollection["clientCertificate"].ValueOrigin != PropertyValueOrigin.Default)
			{
				this.ClientCertificate.ApplyConfiguration(behavior.ClientCertificate);
			}
			if (propertyInformationCollection["serviceCertificate"].ValueOrigin != PropertyValueOrigin.Default)
			{
				this.ServiceCertificate.ApplyConfiguration(behavior.ServiceCertificate);
			}
			if (propertyInformationCollection["issuedToken"].ValueOrigin != PropertyValueOrigin.Default)
			{
				this.IssuedToken.ApplyConfiguration(behavior.IssuedToken);
			}
			if (propertyInformationCollection["httpDigest"].ValueOrigin != PropertyValueOrigin.Default)
			{
				this.HttpDigest.ApplyConfiguration(behavior.HttpDigest);
			}
			if (propertyInformationCollection["peer"].ValueOrigin != PropertyValueOrigin.Default)
			{
				this.Peer.ApplyConfiguration(behavior.Peer);
			}
			if (propertyInformationCollection["useIdentityConfiguration"].ValueOrigin != PropertyValueOrigin.Default)
			{
				behavior.UseIdentityConfiguration = this.UseIdentityConfiguration;
			}
			behavior.SupportInteractive = this.SupportInteractive;
		}

		// Token: 0x17000E32 RID: 3634
		// (get) Token: 0x06003B64 RID: 15204 RVA: 0x000E35F4 File Offset: 0x000E17F4
		public override Type BehaviorType
		{
			get
			{
				return typeof(ClientCredentials);
			}
		}

		// Token: 0x17000E33 RID: 3635
		// (get) Token: 0x06003B65 RID: 15205 RVA: 0x000E3600 File Offset: 0x000E1800
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("type", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("useIdentityConfiguration", typeof(bool), false, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("clientCertificate", typeof(X509InitiatorCertificateClientElement), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("serviceCertificate", typeof(X509RecipientCertificateClientElement), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("windows", typeof(WindowsClientElement), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("issuedToken", typeof(IssuedTokenClientElement), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("httpDigest", typeof(HttpDigestClientElement), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("peer", typeof(PeerCredentialElement), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("supportInteractive", typeof(bool), true, null, null, ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04002A84 RID: 10884
		private ConfigurationPropertyCollection properties;
	}
}
