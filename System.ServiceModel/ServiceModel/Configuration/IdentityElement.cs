using System;
using System.Configuration;
using System.IdentityModel.Claims;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000631 RID: 1585
	public sealed class IdentityElement : ConfigurationElement
	{
		// Token: 0x17000EBB RID: 3771
		// (get) Token: 0x06003CC9 RID: 15561 RVA: 0x000E7B24 File Offset: 0x000E5D24
		[ConfigurationProperty("userPrincipalName")]
		public UserPrincipalNameElement UserPrincipalName
		{
			get
			{
				return (UserPrincipalNameElement)base["userPrincipalName"];
			}
		}

		// Token: 0x17000EBC RID: 3772
		// (get) Token: 0x06003CCA RID: 15562 RVA: 0x000E7B36 File Offset: 0x000E5D36
		[ConfigurationProperty("servicePrincipalName")]
		public ServicePrincipalNameElement ServicePrincipalName
		{
			get
			{
				return (ServicePrincipalNameElement)base["servicePrincipalName"];
			}
		}

		// Token: 0x17000EBD RID: 3773
		// (get) Token: 0x06003CCB RID: 15563 RVA: 0x000E7B48 File Offset: 0x000E5D48
		[ConfigurationProperty("dns")]
		public DnsElement Dns
		{
			get
			{
				return (DnsElement)base["dns"];
			}
		}

		// Token: 0x17000EBE RID: 3774
		// (get) Token: 0x06003CCC RID: 15564 RVA: 0x000E7B5A File Offset: 0x000E5D5A
		[ConfigurationProperty("rsa")]
		public RsaElement Rsa
		{
			get
			{
				return (RsaElement)base["rsa"];
			}
		}

		// Token: 0x17000EBF RID: 3775
		// (get) Token: 0x06003CCD RID: 15565 RVA: 0x000E7B6C File Offset: 0x000E5D6C
		[ConfigurationProperty("certificate")]
		public CertificateElement Certificate
		{
			get
			{
				return (CertificateElement)base["certificate"];
			}
		}

		// Token: 0x17000EC0 RID: 3776
		// (get) Token: 0x06003CCE RID: 15566 RVA: 0x000E7B7E File Offset: 0x000E5D7E
		[ConfigurationProperty("certificateReference")]
		public CertificateReferenceElement CertificateReference
		{
			get
			{
				return (CertificateReferenceElement)base["certificateReference"];
			}
		}

		// Token: 0x06003CCF RID: 15567 RVA: 0x000E7B90 File Offset: 0x000E5D90
		internal void Copy(IdentityElement source)
		{
			if (source == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("source");
			}
			PropertyInformationCollection propertyInformationCollection = source.ElementInformation.Properties;
			if (propertyInformationCollection["userPrincipalName"].ValueOrigin != PropertyValueOrigin.Default)
			{
				this.UserPrincipalName.Value = source.UserPrincipalName.Value;
			}
			if (propertyInformationCollection["servicePrincipalName"].ValueOrigin != PropertyValueOrigin.Default)
			{
				this.ServicePrincipalName.Value = source.ServicePrincipalName.Value;
			}
			if (propertyInformationCollection["certificate"].ValueOrigin != PropertyValueOrigin.Default)
			{
				this.Certificate.EncodedValue = source.Certificate.EncodedValue;
			}
			if (propertyInformationCollection["certificateReference"].ValueOrigin != PropertyValueOrigin.Default)
			{
				this.CertificateReference.StoreName = source.CertificateReference.StoreName;
				this.CertificateReference.StoreLocation = source.CertificateReference.StoreLocation;
				this.CertificateReference.X509FindType = source.CertificateReference.X509FindType;
				this.CertificateReference.FindValue = source.CertificateReference.FindValue;
			}
		}

		// Token: 0x06003CD0 RID: 15568 RVA: 0x000E7CA0 File Offset: 0x000E5EA0
		public void InitializeFrom(EndpointIdentity identity)
		{
			if (identity == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("identity");
			}
			Claim identityClaim = identity.IdentityClaim;
			if (ClaimTypes.Dns.Equals(identityClaim.ClaimType))
			{
				this.Dns.Value = (string)identityClaim.Resource;
				return;
			}
			if (ClaimTypes.Spn.Equals(identityClaim.ClaimType))
			{
				this.ServicePrincipalName.Value = (string)identityClaim.Resource;
				return;
			}
			if (ClaimTypes.Upn.Equals(identityClaim.ClaimType))
			{
				this.UserPrincipalName.Value = (string)identityClaim.Resource;
				return;
			}
			if (ClaimTypes.Rsa.Equals(identityClaim.ClaimType))
			{
				this.Rsa.Value = ((RSA)identityClaim.Resource).ToXmlString(false);
				return;
			}
			if (identity is X509CertificateEndpointIdentity)
			{
				X509Certificate2Collection certificates = ((X509CertificateEndpointIdentity)identity).Certificates;
				this.Certificate.EncodedValue = Convert.ToBase64String(certificates.Export((certificates.Count == 1) ? X509ContentType.SerializedCert : X509ContentType.SerializedStore));
			}
		}

		// Token: 0x17000EC1 RID: 3777
		// (get) Token: 0x06003CD1 RID: 15569 RVA: 0x000E7DA8 File Offset: 0x000E5FA8
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("userPrincipalName", typeof(UserPrincipalNameElement), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("servicePrincipalName", typeof(ServicePrincipalNameElement), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("dns", typeof(DnsElement), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("rsa", typeof(RsaElement), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("certificate", typeof(CertificateElement), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("certificateReference", typeof(CertificateReferenceElement), null, null, null, ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04002C86 RID: 11398
		private ConfigurationPropertyCollection properties;
	}
}
