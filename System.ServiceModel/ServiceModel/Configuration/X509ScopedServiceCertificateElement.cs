using System;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Security;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006AD RID: 1709
	public sealed class X509ScopedServiceCertificateElement : ConfigurationElement
	{
		// Token: 0x170010FE RID: 4350
		// (get) Token: 0x0600423B RID: 16955 RVA: 0x000FAE9C File Offset: 0x000F909C
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("targetUri", typeof(Uri), null, null, null, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey),
						new ConfigurationProperty("findValue", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("storeLocation", typeof(StoreLocation), StoreLocation.CurrentUser, null, new StandardRuntimeEnumValidator(typeof(StoreLocation)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("storeName", typeof(StoreName), StoreName.My, null, new StandardRuntimeEnumValidator(typeof(StoreName)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("x509FindType", typeof(X509FindType), X509FindType.FindBySubjectDistinguishedName, null, new StandardRuntimeEnumValidator(typeof(X509FindType)), ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x170010FF RID: 4351
		// (get) Token: 0x0600423D RID: 16957 RVA: 0x000FAFAD File Offset: 0x000F91AD
		// (set) Token: 0x0600423E RID: 16958 RVA: 0x000FAFBF File Offset: 0x000F91BF
		[ConfigurationProperty("targetUri", DefaultValue = null, Options = (ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey))]
		public Uri TargetUri
		{
			get
			{
				return (Uri)base["targetUri"];
			}
			set
			{
				base["targetUri"] = value;
			}
		}

		// Token: 0x17001100 RID: 4352
		// (get) Token: 0x0600423F RID: 16959 RVA: 0x000FAFCD File Offset: 0x000F91CD
		// (set) Token: 0x06004240 RID: 16960 RVA: 0x000FAFDF File Offset: 0x000F91DF
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

		// Token: 0x17001101 RID: 4353
		// (get) Token: 0x06004241 RID: 16961 RVA: 0x000FAFFC File Offset: 0x000F91FC
		// (set) Token: 0x06004242 RID: 16962 RVA: 0x000FB00E File Offset: 0x000F920E
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

		// Token: 0x17001102 RID: 4354
		// (get) Token: 0x06004243 RID: 16963 RVA: 0x000FB021 File Offset: 0x000F9221
		// (set) Token: 0x06004244 RID: 16964 RVA: 0x000FB033 File Offset: 0x000F9233
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

		// Token: 0x17001103 RID: 4355
		// (get) Token: 0x06004245 RID: 16965 RVA: 0x000FB046 File Offset: 0x000F9246
		// (set) Token: 0x06004246 RID: 16966 RVA: 0x000FB058 File Offset: 0x000F9258
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

		// Token: 0x06004247 RID: 16967 RVA: 0x000FB06C File Offset: 0x000F926C
		public void Copy(X509ScopedServiceCertificateElement from)
		{
			if (this.IsReadOnly())
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigReadOnly")));
			}
			if (from == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("from");
			}
			this.TargetUri = from.TargetUri;
			this.FindValue = from.FindValue;
			this.StoreLocation = from.StoreLocation;
			this.StoreName = from.StoreName;
			this.X509FindType = from.X509FindType;
		}

		// Token: 0x06004248 RID: 16968 RVA: 0x000FB0EC File Offset: 0x000F92EC
		internal void ApplyConfiguration(X509CertificateRecipientClientCredential creds)
		{
			if (creds == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("creds");
			}
			PropertyInformationCollection propertyInformationCollection = base.ElementInformation.Properties;
			if (propertyInformationCollection["targetUri"].ValueOrigin != PropertyValueOrigin.Default || propertyInformationCollection["storeLocation"].ValueOrigin != PropertyValueOrigin.Default || propertyInformationCollection["storeName"].ValueOrigin != PropertyValueOrigin.Default || propertyInformationCollection["x509FindType"].ValueOrigin != PropertyValueOrigin.Default || propertyInformationCollection["findValue"].ValueOrigin != PropertyValueOrigin.Default)
			{
				creds.SetScopedCertificate(this.StoreLocation, this.StoreName, this.X509FindType, this.FindValue, this.TargetUri);
			}
		}

		// Token: 0x04002CFC RID: 11516
		private ConfigurationPropertyCollection properties;
	}
}
