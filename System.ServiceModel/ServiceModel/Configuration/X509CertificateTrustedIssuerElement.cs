using System;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006A5 RID: 1701
	public sealed class X509CertificateTrustedIssuerElement : ConfigurationElement
	{
		// Token: 0x170010D6 RID: 4310
		// (get) Token: 0x060041E2 RID: 16866 RVA: 0x000F9AF0 File Offset: 0x000F7CF0
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("findValue", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.IsKey),
						new ConfigurationProperty("storeLocation", typeof(StoreLocation), StoreLocation.LocalMachine, null, new StandardRuntimeEnumValidator(typeof(StoreLocation)), ConfigurationPropertyOptions.IsKey),
						new ConfigurationProperty("storeName", typeof(StoreName), StoreName.My, null, new StandardRuntimeEnumValidator(typeof(StoreName)), ConfigurationPropertyOptions.IsKey),
						new ConfigurationProperty("x509FindType", typeof(X509FindType), X509FindType.FindBySubjectDistinguishedName, null, new StandardRuntimeEnumValidator(typeof(X509FindType)), ConfigurationPropertyOptions.IsKey)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x170010D7 RID: 4311
		// (get) Token: 0x060041E4 RID: 16868 RVA: 0x000F9BE3 File Offset: 0x000F7DE3
		// (set) Token: 0x060041E5 RID: 16869 RVA: 0x000F9BF5 File Offset: 0x000F7DF5
		[ConfigurationProperty("findValue", DefaultValue = "", Options = ConfigurationPropertyOptions.IsKey)]
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

		// Token: 0x170010D8 RID: 4312
		// (get) Token: 0x060041E6 RID: 16870 RVA: 0x000F9C12 File Offset: 0x000F7E12
		// (set) Token: 0x060041E7 RID: 16871 RVA: 0x000F9C24 File Offset: 0x000F7E24
		[ConfigurationProperty("storeLocation", DefaultValue = StoreLocation.LocalMachine, Options = ConfigurationPropertyOptions.IsKey)]
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

		// Token: 0x170010D9 RID: 4313
		// (get) Token: 0x060041E8 RID: 16872 RVA: 0x000F9C37 File Offset: 0x000F7E37
		// (set) Token: 0x060041E9 RID: 16873 RVA: 0x000F9C49 File Offset: 0x000F7E49
		[ConfigurationProperty("storeName", DefaultValue = StoreName.My, Options = ConfigurationPropertyOptions.IsKey)]
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

		// Token: 0x170010DA RID: 4314
		// (get) Token: 0x060041EA RID: 16874 RVA: 0x000F9C5C File Offset: 0x000F7E5C
		// (set) Token: 0x060041EB RID: 16875 RVA: 0x000F9C6E File Offset: 0x000F7E6E
		[ConfigurationProperty("x509FindType", DefaultValue = X509FindType.FindBySubjectDistinguishedName, Options = ConfigurationPropertyOptions.IsKey)]
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

		// Token: 0x060041EC RID: 16876 RVA: 0x000F9C84 File Offset: 0x000F7E84
		public void Copy(X509CertificateTrustedIssuerElement from)
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

		// Token: 0x04002CF4 RID: 11508
		private ConfigurationPropertyCollection properties;
	}
}
