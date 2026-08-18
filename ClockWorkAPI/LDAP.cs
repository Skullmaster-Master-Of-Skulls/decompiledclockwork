using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.DirectoryServices.Protocols;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ClockWorkAPI
{
	// Token: 0x02000020 RID: 32
	public class LDAP : IDisposable
	{
		// Token: 0x06000132 RID: 306 RVA: 0x0000819C File Offset: 0x0000719C
		public LDAP(string host) : this(host, 389)
		{
		}

		// Token: 0x06000133 RID: 307 RVA: 0x000081AD File Offset: 0x000071AD
		public LDAP(string host, int port) : this(host, port, string.Empty)
		{
		}

		// Token: 0x06000134 RID: 308 RVA: 0x000081C0 File Offset: 0x000071C0
		public LDAP(string host, int port, string domain)
		{
			this.ldap_port = 389;
			base..ctor();
			this.ldap_server = host;
			this.ldap_port = port;
			this.ldap_domain = domain;
			this.attributes = new List<KeyValuePair<string, string>>();
			this.dirIdentifier = new LdapDirectoryIdentifier(this.ldap_server, this.ldap_port, true, false);
			this.connection = new LdapConnection(this.dirIdentifier);
			this.connection.SessionOptions.ProtocolVersion = 3;
			this.connection.AutoBind = false;
			this.connection.AuthType = AuthType.Basic;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00008258 File Offset: 0x00007258
		public LDAP(string host, int port, string baseDN, string lookupattribute, string username, string password, bool useSSL)
		{
			this.ldap_port = 389;
			base..ctor();
			this.useSSL = useSSL;
			this.credential = new NetworkCredential(string.Format("{0}={1},{2}", lookupattribute, username, baseDN), password);
			this.dirIdentifier = new LdapDirectoryIdentifier((port > 0) ? string.Format("{0}:{1}", host, port) : host, true, false);
			this.connection = new LdapConnection(this.dirIdentifier, this.credential, AuthType.Basic);
			this.connection.SessionOptions.ProtocolVersion = 3;
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000136 RID: 310 RVA: 0x000082EC File Offset: 0x000072EC
		public string AttributeString
		{
			get
			{
				return this.getQueryStringFromAttributes();
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00008304 File Offset: 0x00007304
		// (set) Token: 0x06000138 RID: 312 RVA: 0x00008321 File Offset: 0x00007321
		public string CommonName
		{
			get
			{
				return this.getAttribute("CN");
			}
			set
			{
				this.AddAttribute("CN", value);
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00008334 File Offset: 0x00007334
		// (set) Token: 0x0600013A RID: 314 RVA: 0x00008351 File Offset: 0x00007351
		public string LocalityName
		{
			get
			{
				return this.getAttribute("L");
			}
			set
			{
				this.AddAttribute("L", value);
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00008364 File Offset: 0x00007364
		// (set) Token: 0x0600013C RID: 316 RVA: 0x00008381 File Offset: 0x00007381
		public string StateOrProvinceName
		{
			get
			{
				return this.getAttribute("ST");
			}
			set
			{
				this.AddAttribute("ST", value);
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00008394 File Offset: 0x00007394
		// (set) Token: 0x0600013E RID: 318 RVA: 0x000083B1 File Offset: 0x000073B1
		public string OrganizationName
		{
			get
			{
				return this.getAttribute("O");
			}
			set
			{
				this.AddAttribute("O", value);
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600013F RID: 319 RVA: 0x000083C4 File Offset: 0x000073C4
		// (set) Token: 0x06000140 RID: 320 RVA: 0x000083E1 File Offset: 0x000073E1
		public string OrganizationUnitName
		{
			get
			{
				return this.getAttribute("OU");
			}
			set
			{
				this.AddAttribute("OU", value);
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000141 RID: 321 RVA: 0x000083F4 File Offset: 0x000073F4
		// (set) Token: 0x06000142 RID: 322 RVA: 0x00008411 File Offset: 0x00007411
		public string CountryName
		{
			get
			{
				return this.getAttribute("C");
			}
			set
			{
				this.AddAttribute("C", value);
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00008424 File Offset: 0x00007424
		// (set) Token: 0x06000144 RID: 324 RVA: 0x00008441 File Offset: 0x00007441
		public string StreetAddress
		{
			get
			{
				return this.getAttribute("STREET");
			}
			set
			{
				this.AddAttribute("STREET", value);
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00008454 File Offset: 0x00007454
		// (set) Token: 0x06000146 RID: 326 RVA: 0x00008471 File Offset: 0x00007471
		public string DomainComponent
		{
			get
			{
				return this.getAttribute("DC");
			}
			set
			{
				this.AddAttribute("DC", value);
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00008484 File Offset: 0x00007484
		// (set) Token: 0x06000148 RID: 328 RVA: 0x000084A1 File Offset: 0x000074A1
		public string UserId
		{
			get
			{
				return this.getAttribute("UID");
			}
			set
			{
				this.AddAttribute("UID", value);
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000149 RID: 329 RVA: 0x000084B4 File Offset: 0x000074B4
		// (set) Token: 0x0600014A RID: 330 RVA: 0x000084D6 File Offset: 0x000074D6
		public bool SSL
		{
			get
			{
				return this.connection.SessionOptions.SecureSocketLayer;
			}
			set
			{
				this.connection.SessionOptions.SecureSocketLayer = value;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600014B RID: 331 RVA: 0x000084EC File Offset: 0x000074EC
		// (set) Token: 0x0600014C RID: 332 RVA: 0x0000850E File Offset: 0x0000750E
		public bool Sealing
		{
			get
			{
				return this.connection.SessionOptions.Sealing;
			}
			set
			{
				this.connection.SessionOptions.Sealing = value;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600014D RID: 333 RVA: 0x00008524 File Offset: 0x00007524
		// (set) Token: 0x0600014E RID: 334 RVA: 0x00008546 File Offset: 0x00007546
		public bool Signing
		{
			get
			{
				return this.connection.SessionOptions.Signing;
			}
			set
			{
				this.connection.SessionOptions.Signing = value;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600014F RID: 335 RVA: 0x0000855C File Offset: 0x0000755C
		// (set) Token: 0x06000150 RID: 336 RVA: 0x00008579 File Offset: 0x00007579
		public AuthType AuthType
		{
			get
			{
				return this.connection.AuthType;
			}
			set
			{
				this.connection.AuthType = value;
			}
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0000858C File Offset: 0x0000758C
		private string getAttribute(string key)
		{
			foreach (KeyValuePair<string, string> keyValuePair in this.attributes)
			{
				if (keyValuePair.Key.ToUpper() == key.ToUpper())
				{
					return string.Format("{0}={1}", key.ToUpper(), keyValuePair.Value);
				}
			}
			return null;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00008620 File Offset: 0x00007620
		public void AddAttribute(string distinguishedName, string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				this.attributes.Add(new KeyValuePair<string, string>(distinguishedName, value));
			}
		}

		// Token: 0x06000153 RID: 339 RVA: 0x0000864C File Offset: 0x0000764C
		public bool Bind(string uPassword, out Exception ex)
		{
			ex = null;
			bool result;
			try
			{
				if (string.IsNullOrEmpty(uPassword))
				{
					ex = new ArgumentException("Password can not be empty.");
					result = false;
				}
				else
				{
					string queryStringFromAttributes = this.getQueryStringFromAttributes();
					this.connection.Credential = new NetworkCredential(queryStringFromAttributes, uPassword);
					this.connection.Bind();
					result = true;
				}
			}
			catch (Exception ex2)
			{
				ex = ex2;
				result = false;
			}
			return result;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x000086D8 File Offset: 0x000076D8
		public bool BindTLS(string uPassword, out Exception ex)
		{
			ex = null;
			bool result;
			try
			{
				if (string.IsNullOrEmpty(uPassword))
				{
					ex = new ArgumentException("Password can not be empty.");
					result = false;
				}
				else
				{
					string queryStringFromAttributes = this.getQueryStringFromAttributes();
					this.connection.Credential = new NetworkCredential(queryStringFromAttributes, uPassword);
					this.connection.SessionOptions.VerifyServerCertificate = ((LdapConnection conn, X509Certificate cert) => true);
					this.connection.SessionOptions.StartTransportLayerSecurity(null);
					this.useSSL = true;
					this.connection.Bind();
					result = true;
				}
			}
			catch (Exception ex2)
			{
				ex = ex2;
				result = false;
			}
			return result;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x000087AC File Offset: 0x000077AC
		public void Bind()
		{
			if (this.useSSL)
			{
				this.connection.SessionOptions.VerifyServerCertificate = ((LdapConnection ldapconn, X509Certificate cert) => true);
				this.connection.SessionOptions.StartTransportLayerSecurity(null);
				this.connection.Bind(this.credential);
			}
			else
			{
				this.connection.Bind(this.credential);
			}
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00008834 File Offset: 0x00007834
		private string getQueryStringFromAttributes()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.attributes.Count > 0)
			{
				KeyValuePair<string, string> keyValuePair = this.attributes[0];
				stringBuilder.Append(string.Format("{0}={1}", keyValuePair.Key, keyValuePair.Value));
				for (int i = 1; i < this.attributes.Count; i++)
				{
					keyValuePair = this.attributes[i];
					stringBuilder.Append(string.Format(",{0}={1}", keyValuePair.Key, keyValuePair.Value));
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000157 RID: 343 RVA: 0x000088E4 File Offset: 0x000078E4
		public SearchResponse Search(SearchRequest sRequest)
		{
			SearchResponse result;
			try
			{
				SearchResponse searchResponse = this.connection.SendRequest(sRequest) as SearchResponse;
				result = searchResponse;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00008924 File Offset: 0x00007924
		public SearchResponse Search(string distinguishedName, string ldapFilter, string returnAttributes)
		{
			SearchResponse result;
			try
			{
				string[] attributeList = null;
				if (!string.IsNullOrEmpty(returnAttributes))
				{
					attributeList = returnAttributes.Split(new char[]
					{
						','
					});
				}
				SearchRequest sRequest = new SearchRequest(distinguishedName, ldapFilter, SearchScope.Subtree, attributeList);
				result = this.Search(sRequest);
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00008984 File Offset: 0x00007984
		internal static StringDictionary ConvertToDictionary(SearchResponse sResponse)
		{
			StringDictionary stringDictionary = new StringDictionary();
			UTF8Encoding utf8Encoding = new UTF8Encoding(false, true);
			StringDictionary result;
			if (sResponse.Entries.Count > 0)
			{
				SearchResultEntry searchResultEntry = sResponse.Entries[0];
				foreach (object obj in searchResultEntry.Attributes.Values)
				{
					DirectoryAttribute directoryAttribute = (DirectoryAttribute)obj;
					foreach (object obj2 in directoryAttribute)
					{
						byte[] bytes = (byte[])obj2;
						try
						{
							string @string = utf8Encoding.GetString(bytes);
							stringDictionary.Add(directoryAttribute.Name, @string);
						}
						catch
						{
						}
					}
				}
				result = stringDictionary;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00008ABC File Offset: 0x00007ABC
		public void Dispose()
		{
			if (this.useSSL && this.connection != null)
			{
				this.connection.SessionOptions.StopTransportLayerSecurity();
			}
			if (this.connection != null)
			{
				this.connection.Dispose();
			}
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00008B0C File Offset: 0x00007B0C
		public static void ParseLdapSettings(string settings, out string server, out int port, out string domain, out string authenticationType, out string lookupAttribute, out string returnAttributes)
		{
			string[] array = settings.Split(new char[]
			{
				'`'
			});
			int num = array.Length;
			server = ((num > 0) ? array[0] : "");
			if (num > 1)
			{
				if (!int.TryParse(array[1], out port))
				{
					port = 389;
				}
			}
			else
			{
				port = 389;
			}
			domain = ((num > 2) ? array[2] : "");
			authenticationType = ((num > 3) ? array[3] : "");
			lookupAttribute = ((num > 4) ? array[4] : "");
			returnAttributes = ((num > 5) ? array[5] : "");
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00008BB0 File Offset: 0x00007BB0
		public static StringDictionary IsAuthenticatedV3(string serverName, int port, string dc, string lookupAttribute, string returnAttributes, string authTypeStr, string username, string pwd, out Exception ex)
		{
			ex = null;
			StringDictionary result;
			try
			{
				using (LDAP ldap = new LDAP(serverName, port))
				{
					ldap.AddAttribute(lookupAttribute.Trim().ToUpper(), username);
					LDAP.setAttributes(ldap, dc);
					LDAP.setAuthenticationType(ldap, authTypeStr);
					bool flag = LDAP.IsAuthenticatedV3(ldap, pwd, out ex);
					if (flag)
					{
						string ldapFilter = string.Concat(new string[]
						{
							"(",
							lookupAttribute,
							"=",
							username,
							")"
						});
						SearchResponse sResponse = ldap.Search(ldap.AttributeString, ldapFilter, returnAttributes);
						result = LDAP.ConvertToDictionary(sResponse);
					}
					else
					{
						result = null;
					}
				}
			}
			catch (Exception ex2)
			{
				ex = ex2;
				result = null;
			}
			return result;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00008C9C File Offset: 0x00007C9C
		public static StringDictionary IsAuthenticatedWithTLS_V3(string serverName, int port, string dc, string lookupAttribute, string returnAttributes, string authTypeStr, string username, string pwd, out Exception ex)
		{
			ex = null;
			StringDictionary result;
			try
			{
				using (LDAP ldap = new LDAP(serverName, port))
				{
					ldap.AddAttribute(lookupAttribute.Trim().ToUpper(), username);
					LDAP.setAttributes(ldap, dc);
					LDAP.setAuthenticationType(ldap, authTypeStr);
					bool flag = LDAP.IsAuthenticatedWithTLS_V3(ldap, pwd, out ex);
					if (flag)
					{
						string ldapFilter = string.Concat(new string[]
						{
							"(",
							lookupAttribute,
							"=",
							username,
							")"
						});
						SearchResponse sResponse = ldap.Search(ldap.AttributeString, ldapFilter, returnAttributes);
						result = LDAP.ConvertToDictionary(sResponse);
					}
					else
					{
						result = null;
					}
				}
			}
			catch (Exception ex2)
			{
				ex = ex2;
				result = null;
			}
			return result;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00008D88 File Offset: 0x00007D88
		public static bool IsAuthenticatedV3(LDAP ldap, string password, out Exception ex)
		{
			ex = null;
			bool result;
			try
			{
				result = ldap.Bind(password, out ex);
			}
			catch (Exception ex2)
			{
				ex = ex2;
				result = false;
			}
			return result;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00008DC0 File Offset: 0x00007DC0
		public static bool IsAuthenticatedWithTLS_V3(LDAP ldap, string password, out Exception ex)
		{
			ex = null;
			bool result;
			try
			{
				result = ldap.BindTLS(password, out ex);
			}
			catch (Exception ex2)
			{
				ex = ex2;
				result = false;
			}
			return result;
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00008DF8 File Offset: 0x00007DF8
		private static void setAuthenticationType(LDAP ldap, string authTypeStr)
		{
			if (!string.IsNullOrEmpty(authTypeStr))
			{
				ldap.AuthType = AuthType.Basic;
				string text = authTypeStr.Trim().ToLower();
				if (text != null)
				{
					if (!(text == "securesocketslayer"))
					{
						if (!(text == "sealing"))
						{
							if (text == "signing")
							{
								ldap.Signing = true;
							}
						}
						else
						{
							ldap.Sealing = true;
						}
					}
					else
					{
						ldap.SSL = true;
					}
				}
			}
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00008E74 File Offset: 0x00007E74
		private static void setAttributes(LDAP ldap, string dc)
		{
			if (!string.IsNullOrEmpty(dc))
			{
				string[] array = dc.Split(new char[]
				{
					','
				}, StringSplitOptions.RemoveEmptyEntries);
				foreach (string text in array)
				{
					string[] array3 = text.Split(new char[]
					{
						'='
					});
					if (array3.Length == 2)
					{
						ldap.AddAttribute(array3[0], array3[1]);
					}
				}
			}
		}

		// Token: 0x040000B2 RID: 178
		public const string ATT_COMMON_NAME = "CN";

		// Token: 0x040000B3 RID: 179
		public const string ATT_LOCALITY_NAME = "L";

		// Token: 0x040000B4 RID: 180
		public const string ATT_STATE_OR_PROVINCE_NAME = "ST";

		// Token: 0x040000B5 RID: 181
		public const string ATT_ORGANIZATION_NAME = "O";

		// Token: 0x040000B6 RID: 182
		public const string ATT_ORGANIZATIONAL_UNIT_NAME = "OU";

		// Token: 0x040000B7 RID: 183
		public const string ATT_COUNTRY_NAME = "C";

		// Token: 0x040000B8 RID: 184
		public const string ATT_STREET_ADDRESS = "STREET";

		// Token: 0x040000B9 RID: 185
		public const string ATT_DOMAIN_COMPONENT = "DC";

		// Token: 0x040000BA RID: 186
		public const string ATT_USER_ID = "UID";

		// Token: 0x040000BB RID: 187
		protected LdapConnection connection;

		// Token: 0x040000BC RID: 188
		protected LdapDirectoryIdentifier dirIdentifier;

		// Token: 0x040000BD RID: 189
		protected NetworkCredential credential;

		// Token: 0x040000BE RID: 190
		protected string ldap_server;

		// Token: 0x040000BF RID: 191
		protected int ldap_port;

		// Token: 0x040000C0 RID: 192
		protected string ldap_domain;

		// Token: 0x040000C1 RID: 193
		protected List<KeyValuePair<string, string>> attributes;

		// Token: 0x040000C2 RID: 194
		protected string certicate_path;

		// Token: 0x040000C3 RID: 195
		protected string certicate_password;

		// Token: 0x040000C4 RID: 196
		protected bool useSSL;
	}
}
