using System;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020005FA RID: 1530
	public sealed class CertificateReferenceElement : ConfigurationElement
	{
		// Token: 0x17000E04 RID: 3588
		// (get) Token: 0x06003AF6 RID: 15094 RVA: 0x000E2299 File Offset: 0x000E0499
		// (set) Token: 0x06003AF7 RID: 15095 RVA: 0x000E22AB File Offset: 0x000E04AB
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

		// Token: 0x17000E05 RID: 3589
		// (get) Token: 0x06003AF8 RID: 15096 RVA: 0x000E22BE File Offset: 0x000E04BE
		// (set) Token: 0x06003AF9 RID: 15097 RVA: 0x000E22D0 File Offset: 0x000E04D0
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

		// Token: 0x17000E06 RID: 3590
		// (get) Token: 0x06003AFA RID: 15098 RVA: 0x000E22E3 File Offset: 0x000E04E3
		// (set) Token: 0x06003AFB RID: 15099 RVA: 0x000E22F5 File Offset: 0x000E04F5
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

		// Token: 0x17000E07 RID: 3591
		// (get) Token: 0x06003AFC RID: 15100 RVA: 0x000E2308 File Offset: 0x000E0508
		// (set) Token: 0x06003AFD RID: 15101 RVA: 0x000E231A File Offset: 0x000E051A
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

		// Token: 0x17000E08 RID: 3592
		// (get) Token: 0x06003AFE RID: 15102 RVA: 0x000E2337 File Offset: 0x000E0537
		// (set) Token: 0x06003AFF RID: 15103 RVA: 0x000E2349 File Offset: 0x000E0549
		[ConfigurationProperty("isChainIncluded", DefaultValue = false)]
		public bool IsChainIncluded
		{
			get
			{
				return (bool)base["isChainIncluded"];
			}
			set
			{
				base["isChainIncluded"] = value;
			}
		}

		// Token: 0x17000E09 RID: 3593
		// (get) Token: 0x06003B00 RID: 15104 RVA: 0x000E235C File Offset: 0x000E055C
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("storeName", typeof(StoreName), StoreName.My, null, new StandardRuntimeEnumValidator(typeof(StoreName)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("storeLocation", typeof(StoreLocation), StoreLocation.LocalMachine, null, new StandardRuntimeEnumValidator(typeof(StoreLocation)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("x509FindType", typeof(X509FindType), X509FindType.FindBySubjectDistinguishedName, null, new StandardRuntimeEnumValidator(typeof(X509FindType)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("findValue", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("isChainIncluded", typeof(bool), false, null, null, ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04002A7B RID: 10875
		private ConfigurationPropertyCollection properties;
	}
}
