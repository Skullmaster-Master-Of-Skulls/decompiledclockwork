using System;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Security;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006A7 RID: 1703
	public sealed class X509ClientCertificateCredentialsElement : ConfigurationElement
	{
		// Token: 0x170010E2 RID: 4322
		// (get) Token: 0x060041FD RID: 16893 RVA: 0x000FA07C File Offset: 0x000F827C
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("findValue", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("storeLocation", typeof(StoreLocation), StoreLocation.LocalMachine, null, new StandardRuntimeEnumValidator(typeof(StoreLocation)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("storeName", typeof(StoreName), StoreName.My, null, new StandardRuntimeEnumValidator(typeof(StoreName)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("x509FindType", typeof(X509FindType), X509FindType.FindBySubjectDistinguishedName, null, new StandardRuntimeEnumValidator(typeof(X509FindType)), ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x170010E3 RID: 4323
		// (get) Token: 0x060041FF RID: 16895 RVA: 0x000FA16F File Offset: 0x000F836F
		// (set) Token: 0x06004200 RID: 16896 RVA: 0x000FA181 File Offset: 0x000F8381
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

		// Token: 0x170010E4 RID: 4324
		// (get) Token: 0x06004201 RID: 16897 RVA: 0x000FA19E File Offset: 0x000F839E
		// (set) Token: 0x06004202 RID: 16898 RVA: 0x000FA1B0 File Offset: 0x000F83B0
		[ConfigurationProperty("storeLocation", DefaultValue = StoreLocation.LocalMachine)]
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

		// Token: 0x170010E5 RID: 4325
		// (get) Token: 0x06004203 RID: 16899 RVA: 0x000FA1C3 File Offset: 0x000F83C3
		// (set) Token: 0x06004204 RID: 16900 RVA: 0x000FA1D5 File Offset: 0x000F83D5
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

		// Token: 0x170010E6 RID: 4326
		// (get) Token: 0x06004205 RID: 16901 RVA: 0x000FA1E8 File Offset: 0x000F83E8
		// (set) Token: 0x06004206 RID: 16902 RVA: 0x000FA1FA File Offset: 0x000F83FA
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

		// Token: 0x06004207 RID: 16903 RVA: 0x000FA210 File Offset: 0x000F8410
		public void Copy(X509ClientCertificateCredentialsElement from)
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

		// Token: 0x06004208 RID: 16904 RVA: 0x000FA284 File Offset: 0x000F8484
		internal void ApplyConfiguration(X509CertificateInitiatorServiceCredential creds)
		{
			if (creds == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("creds");
			}
			PropertyInformationCollection propertyInformationCollection = base.ElementInformation.Properties;
			if (propertyInformationCollection["storeLocation"].ValueOrigin != PropertyValueOrigin.Default || propertyInformationCollection["storeName"].ValueOrigin != PropertyValueOrigin.Default || propertyInformationCollection["x509FindType"].ValueOrigin != PropertyValueOrigin.Default || propertyInformationCollection["findValue"].ValueOrigin != PropertyValueOrigin.Default)
			{
				creds.SetCertificate(this.StoreLocation, this.StoreName, this.X509FindType, this.FindValue);
			}
		}

		// Token: 0x04002CF6 RID: 11510
		private ConfigurationPropertyCollection properties;
	}
}
