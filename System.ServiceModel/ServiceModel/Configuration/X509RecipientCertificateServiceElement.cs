using System;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Security;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006AC RID: 1708
	public sealed class X509RecipientCertificateServiceElement : ConfigurationElement
	{
		// Token: 0x170010F9 RID: 4345
		// (get) Token: 0x0600422F RID: 16943 RVA: 0x000FAC00 File Offset: 0x000F8E00
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

		// Token: 0x170010FA RID: 4346
		// (get) Token: 0x06004231 RID: 16945 RVA: 0x000FACF3 File Offset: 0x000F8EF3
		// (set) Token: 0x06004232 RID: 16946 RVA: 0x000FAD05 File Offset: 0x000F8F05
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

		// Token: 0x170010FB RID: 4347
		// (get) Token: 0x06004233 RID: 16947 RVA: 0x000FAD22 File Offset: 0x000F8F22
		// (set) Token: 0x06004234 RID: 16948 RVA: 0x000FAD34 File Offset: 0x000F8F34
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

		// Token: 0x170010FC RID: 4348
		// (get) Token: 0x06004235 RID: 16949 RVA: 0x000FAD47 File Offset: 0x000F8F47
		// (set) Token: 0x06004236 RID: 16950 RVA: 0x000FAD59 File Offset: 0x000F8F59
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

		// Token: 0x170010FD RID: 4349
		// (get) Token: 0x06004237 RID: 16951 RVA: 0x000FAD6C File Offset: 0x000F8F6C
		// (set) Token: 0x06004238 RID: 16952 RVA: 0x000FAD7E File Offset: 0x000F8F7E
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

		// Token: 0x06004239 RID: 16953 RVA: 0x000FAD94 File Offset: 0x000F8F94
		public void Copy(X509RecipientCertificateServiceElement from)
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

		// Token: 0x0600423A RID: 16954 RVA: 0x000FAE08 File Offset: 0x000F9008
		internal void ApplyConfiguration(X509CertificateRecipientServiceCredential cert)
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

		// Token: 0x04002CFB RID: 11515
		private ConfigurationPropertyCollection properties;
	}
}
