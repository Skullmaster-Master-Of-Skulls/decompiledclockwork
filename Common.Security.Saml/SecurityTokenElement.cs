using System;
using System.Configuration;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;

namespace TechnoPro.Common.Security.Saml
{
	// Token: 0x0200001A RID: 26
	public class SecurityTokenElement : ConfigurationElement
	{
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00004638 File Offset: 0x00002838
		// (set) Token: 0x060000DE RID: 222 RVA: 0x0000465A File Offset: 0x0000285A
		[ConfigurationProperty("name", DefaultValue = "urn:clockwork:web:sts:authority", IsRequired = true, IsKey = true)]
		public string Name
		{
			get
			{
				return (string)base["name"];
			}
			set
			{
				base["name"] = value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000DF RID: 223 RVA: 0x0000466C File Offset: 0x0000286C
		// (set) Token: 0x060000E0 RID: 224 RVA: 0x0000468E File Offset: 0x0000288E
		[ConfigurationProperty("uri", DefaultValue = null, IsRequired = true, IsKey = true)]
		public Uri UriToken
		{
			get
			{
				return (Uri)base["uri"];
			}
			set
			{
				base["uri"] = value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x000046A0 File Offset: 0x000028A0
		// (set) Token: 0x060000E2 RID: 226 RVA: 0x000046C2 File Offset: 0x000028C2
		[ConfigurationProperty("storeLocation", DefaultValue = StoreLocation.LocalMachine, IsRequired = false, IsKey = false)]
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

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x000046D8 File Offset: 0x000028D8
		// (set) Token: 0x060000E4 RID: 228 RVA: 0x000046FA File Offset: 0x000028FA
		[ConfigurationProperty("storeName", DefaultValue = StoreName.My, IsRequired = false, IsKey = false)]
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

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00004710 File Offset: 0x00002910
		// (set) Token: 0x060000E6 RID: 230 RVA: 0x00004732 File Offset: 0x00002932
		[ConfigurationProperty("findValue", DefaultValue = "", IsRequired = false, IsKey = false)]
		public string FindValue
		{
			get
			{
				return (string)base["findValue"];
			}
			set
			{
				base["findValue"] = value;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00004744 File Offset: 0x00002944
		// (set) Token: 0x060000E8 RID: 232 RVA: 0x00004766 File Offset: 0x00002966
		[ConfigurationProperty("findType", DefaultValue = X509FindType.FindBySubjectName, IsRequired = false, IsKey = false)]
		public X509FindType FindType
		{
			get
			{
				return (X509FindType)base["findType"];
			}
			set
			{
				base["findType"] = value;
			}
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x0000477C File Offset: 0x0000297C
		public X509SecurityToken GetServiceToken(EndpointAddress appliesTo)
		{
			bool flag = appliesTo == null;
			if (flag)
			{
				throw new ArgumentNullException("appliesTo");
			}
			bool flag2 = appliesTo.Uri != this.UriToken;
			if (flag2)
			{
				Uri uri = new Uri(this.UriToken.OriginalString + "/");
				bool flag3 = appliesTo.Uri != uri;
				if (flag3)
				{
					return null;
				}
			}
			return this.GetSecurityTokenFromDefinition();
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000047F4 File Offset: 0x000029F4
		private X509SecurityToken GetSecurityTokenFromDefinition()
		{
			X509Store x509Store = new X509Store(this.StoreName, this.StoreLocation);
			X509SecurityToken result;
			try
			{
				x509Store.Open(OpenFlags.OpenExistingOnly);
				X509Certificate2Collection x509Certificate2Collection = x509Store.Certificates.Find(this.FindType, this.FindValue, false);
				bool flag = x509Certificate2Collection.Count == 0;
				if (flag)
				{
					string text = string.Join(", ", (from X509Certificate2 cert in (x509Store != null) ? x509Store.Certificates : null
					select (cert.FriendlyName ?? "NULL") + ":" + (cert.Thumbprint ?? "NULL")) ?? new string[0]);
					throw new SecurityException(string.Format("The service could not locate the certificate specified in the configuration file with findtype={0} and value={1}.  The current certificate collection had these: {2}.  The store location is: {3}", new object[]
					{
						this.FindType,
						this.FindValue,
						text,
						(x509Store != null) ? x509Store.Location.ToString() : null
					}));
				}
				result = new X509SecurityToken(x509Certificate2Collection[0]);
			}
			finally
			{
				x509Store.Close();
			}
			return result;
		}
	}
}
