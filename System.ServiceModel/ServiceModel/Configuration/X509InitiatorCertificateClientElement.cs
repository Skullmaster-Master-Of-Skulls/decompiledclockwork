using System;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Security;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006A9 RID: 1705
	public sealed class X509InitiatorCertificateClientElement : ConfigurationElement
	{
		// Token: 0x170010EC RID: 4332
		// (get) Token: 0x06004215 RID: 16917 RVA: 0x000FA5B4 File Offset: 0x000F87B4
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("findValue", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("storeLocation", typeof(StoreLocation), StoreLocation.CurrentUser, null, new StandardRuntimeEnumValidator(typeof(StoreLocation)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("storeName", typeof(StoreName), StoreName.My, null, new StandardRuntimeEnumValidator(typeof(StoreName)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("x509FindType", typeof(X509FindType), X509FindType.FindBySubjectDistinguishedName, null, new StandardRuntimeEnumValidator(typeof(X509FindType)), ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x170010ED RID: 4333
		// (get) Token: 0x06004217 RID: 16919 RVA: 0x000FA6A7 File Offset: 0x000F88A7
		// (set) Token: 0x06004218 RID: 16920 RVA: 0x000FA6B9 File Offset: 0x000F88B9
		[ConfigurationProperty("findValue", DefaultValue = "")]
		[StringValidator(MinLength = 0)]
		public string FindValue
		{
			get
			{
				return (string)base["findValue"];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					value = string.Empty;
				}
				base["findValue"] = value;
			}
		}

		// Token: 0x170010EE RID: 4334
		// (get) Token: 0x06004219 RID: 16921 RVA: 0x000FA6D6 File Offset: 0x000F88D6
		// (set) Token: 0x0600421A RID: 16922 RVA: 0x000FA6E8 File Offset: 0x000F88E8
		[ConfigurationProperty("storeLocation", DefaultValue = StoreLocation.CurrentUser)]
		[StandardRuntimeEnumValidator(typeof(StoreLocation))]
		public StoreLocation StoreLocation
		{
			get
			{
				return (StoreLocation)base["storeLocation"];
			}
			set
			{
				base["storeLocation"] = value;
			}
		}

		// Token: 0x170010EF RID: 4335
		// (get) Token: 0x0600421B RID: 16923 RVA: 0x000FA6FB File Offset: 0x000F88FB
		// (set) Token: 0x0600421C RID: 16924 RVA: 0x000FA70D File Offset: 0x000F890D
		[ConfigurationProperty("storeName", DefaultValue = StoreName.My)]
		[StandardRuntimeEnumValidator(typeof(StoreName))]
		public StoreName StoreName
		{
			get
			{
				return (StoreName)base["storeName"];
			}
			set
			{
				base["storeName"] = value;
			}
		}

		// Token: 0x170010F0 RID: 4336
		// (get) Token: 0x0600421D RID: 16925 RVA: 0x000FA720 File Offset: 0x000F8920
		// (set) Token: 0x0600421E RID: 16926 RVA: 0x000FA732 File Offset: 0x000F8932
		[ConfigurationProperty("x509FindType", DefaultValue = X509FindType.FindBySubjectDistinguishedName)]
		[StandardRuntimeEnumValidator(typeof(X509FindType))]
		public X509FindType X509FindType
		{
			get
			{
				return (X509FindType)base["x509FindType"];
			}
			set
			{
				base["x509FindType"] = value;
			}
		}

		// Token: 0x0600421F RID: 16927 RVA: 0x000FA748 File Offset: 0x000F8948
		public void Copy(X509InitiatorCertificateClientElement from)
		{
			if (this.IsReadOnly())
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigReadOnly")));
			}
			if (from == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("from");
			}
			this.FindValue = from.FindValue;
			this.StoreLocation = from.StoreLocation;
			this.StoreName = from.StoreName;
			this.X509FindType = from.X509FindType;
		}

		// Token: 0x06004220 RID: 16928 RVA: 0x000FA7BC File Offset: 0x000F89BC
		internal void ApplyConfiguration(X509CertificateInitiatorClientCredential cert)
		{
			if (cert == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("cert");
			}
			PropertyInformationCollection propertyInformationCollection = base.ElementInformation.Properties;
			if (propertyInformationCollection["storeLocation"].ValueOrigin != PropertyValueOrigin.Default || propertyInformationCollection["storeName"].ValueOrigin != PropertyValueOrigin.Default || propertyInformationCollection["x509FindType"].ValueOrigin != PropertyValueOrigin.Default || propertyInformationCollection["findValue"].ValueOrigin != PropertyValueOrigin.Default)
			{
				cert.SetCertificate(this.StoreLocation, this.StoreName, this.X509FindType, this.FindValue);
			}
		}

		// Token: 0x04002CF8 RID: 11512
		private ConfigurationPropertyCollection properties;
	}
}
