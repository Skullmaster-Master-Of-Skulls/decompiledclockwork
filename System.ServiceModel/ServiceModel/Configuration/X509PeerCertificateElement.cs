using System;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Security;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006B7 RID: 1719
	public sealed class X509PeerCertificateElement : ConfigurationElement
	{
		// Token: 0x17001129 RID: 4393
		// (get) Token: 0x0600429B RID: 17051 RVA: 0x000FBEBC File Offset: 0x000FA0BC
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

		// Token: 0x1700112A RID: 4394
		// (get) Token: 0x0600429D RID: 17053 RVA: 0x000FBFAF File Offset: 0x000FA1AF
		// (set) Token: 0x0600429E RID: 17054 RVA: 0x000FBFC1 File Offset: 0x000FA1C1
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

		// Token: 0x1700112B RID: 4395
		// (get) Token: 0x0600429F RID: 17055 RVA: 0x000FBFDE File Offset: 0x000FA1DE
		// (set) Token: 0x060042A0 RID: 17056 RVA: 0x000FBFF0 File Offset: 0x000FA1F0
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

		// Token: 0x1700112C RID: 4396
		// (get) Token: 0x060042A1 RID: 17057 RVA: 0x000FC003 File Offset: 0x000FA203
		// (set) Token: 0x060042A2 RID: 17058 RVA: 0x000FC015 File Offset: 0x000FA215
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

		// Token: 0x1700112D RID: 4397
		// (get) Token: 0x060042A3 RID: 17059 RVA: 0x000FC028 File Offset: 0x000FA228
		// (set) Token: 0x060042A4 RID: 17060 RVA: 0x000FC03A File Offset: 0x000FA23A
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

		// Token: 0x060042A5 RID: 17061 RVA: 0x000FC050 File Offset: 0x000FA250
		public void Copy(X509PeerCertificateElement from)
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

		// Token: 0x060042A6 RID: 17062 RVA: 0x000FC0C4 File Offset: 0x000FA2C4
		internal void ApplyConfiguration(PeerCredential cert)
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

		// Token: 0x04002D06 RID: 11526
		private ConfigurationPropertyCollection properties;
	}
}
