using System;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Security;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006A8 RID: 1704
	public sealed class X509DefaultServiceCertificateElement : ConfigurationElement
	{
		// Token: 0x170010E7 RID: 4327
		// (get) Token: 0x06004209 RID: 16905 RVA: 0x000FA318 File Offset: 0x000F8518
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

		// Token: 0x170010E8 RID: 4328
		// (get) Token: 0x0600420B RID: 16907 RVA: 0x000FA40B File Offset: 0x000F860B
		// (set) Token: 0x0600420C RID: 16908 RVA: 0x000FA41D File Offset: 0x000F861D
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

		// Token: 0x170010E9 RID: 4329
		// (get) Token: 0x0600420D RID: 16909 RVA: 0x000FA43A File Offset: 0x000F863A
		// (set) Token: 0x0600420E RID: 16910 RVA: 0x000FA44C File Offset: 0x000F864C
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

		// Token: 0x170010EA RID: 4330
		// (get) Token: 0x0600420F RID: 16911 RVA: 0x000FA45F File Offset: 0x000F865F
		// (set) Token: 0x06004210 RID: 16912 RVA: 0x000FA471 File Offset: 0x000F8671
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

		// Token: 0x170010EB RID: 4331
		// (get) Token: 0x06004211 RID: 16913 RVA: 0x000FA484 File Offset: 0x000F8684
		// (set) Token: 0x06004212 RID: 16914 RVA: 0x000FA496 File Offset: 0x000F8696
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

		// Token: 0x06004213 RID: 16915 RVA: 0x000FA4AC File Offset: 0x000F86AC
		public void Copy(X509DefaultServiceCertificateElement from)
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

		// Token: 0x06004214 RID: 16916 RVA: 0x000FA520 File Offset: 0x000F8720
		internal void ApplyConfiguration(X509CertificateRecipientClientCredential creds)
		{
			if (creds == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("creds");
			}
			PropertyInformationCollection propertyInformationCollection = base.ElementInformation.Properties;
			if (propertyInformationCollection["storeLocation"].ValueOrigin != PropertyValueOrigin.Default || propertyInformationCollection["storeName"].ValueOrigin != PropertyValueOrigin.Default || propertyInformationCollection["x509FindType"].ValueOrigin != PropertyValueOrigin.Default || propertyInformationCollection["findValue"].ValueOrigin != PropertyValueOrigin.Default)
			{
				creds.SetDefaultCertificate(this.StoreLocation, this.StoreName, this.X509FindType, this.FindValue);
			}
		}

		// Token: 0x04002CF7 RID: 11511
		private ConfigurationPropertyCollection properties;
	}
}
