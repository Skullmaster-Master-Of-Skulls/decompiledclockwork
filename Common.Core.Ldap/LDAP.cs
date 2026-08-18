using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.DirectoryServices;
using System.DirectoryServices.Protocols;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using ClockWorkLogger;

namespace TechnoPro.Common.Core.Ldap
{
	// Token: 0x02000002 RID: 2
	public class LDAP : IDisposable
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		public LdapConnection Connection
		{
			get
			{
				return this.connection;
			}
			set
			{
				this.connection = value;
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002061 File Offset: 0x00000261
		public LDAP(string host) : this(host, 389)
		{
		}

		// Token: 0x06000004 RID: 4 RVA: 0x0000206F File Offset: 0x0000026F
		public LDAP(string host, int port) : this(host, port, string.Empty)
		{
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002080 File Offset: 0x00000280
		public LDAP(string host, int port, string domain)
		{
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

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002111 File Offset: 0x00000311
		public string AttributeString
		{
			get
			{
				return this.getQueryStringFromAttributes();
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002119 File Offset: 0x00000319
		// (set) Token: 0x06000008 RID: 8 RVA: 0x00002126 File Offset: 0x00000326
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

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002134 File Offset: 0x00000334
		// (set) Token: 0x0600000A RID: 10 RVA: 0x00002141 File Offset: 0x00000341
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

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000B RID: 11 RVA: 0x0000214F File Offset: 0x0000034F
		// (set) Token: 0x0600000C RID: 12 RVA: 0x0000215C File Offset: 0x0000035C
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

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000D RID: 13 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x0600000E RID: 14 RVA: 0x00002177 File Offset: 0x00000377
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

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002185 File Offset: 0x00000385
		// (set) Token: 0x06000010 RID: 16 RVA: 0x00002192 File Offset: 0x00000392
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

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000021A0 File Offset: 0x000003A0
		// (set) Token: 0x06000012 RID: 18 RVA: 0x000021AD File Offset: 0x000003AD
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

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000021BB File Offset: 0x000003BB
		// (set) Token: 0x06000014 RID: 20 RVA: 0x000021C8 File Offset: 0x000003C8
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

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000021D6 File Offset: 0x000003D6
		// (set) Token: 0x06000016 RID: 22 RVA: 0x000021E3 File Offset: 0x000003E3
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

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000021F1 File Offset: 0x000003F1
		// (set) Token: 0x06000018 RID: 24 RVA: 0x000021FE File Offset: 0x000003FE
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

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000019 RID: 25 RVA: 0x0000220C File Offset: 0x0000040C
		// (set) Token: 0x0600001A RID: 26 RVA: 0x0000221E File Offset: 0x0000041E
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

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002231 File Offset: 0x00000431
		// (set) Token: 0x0600001C RID: 28 RVA: 0x00002243 File Offset: 0x00000443
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

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002256 File Offset: 0x00000456
		// (set) Token: 0x0600001E RID: 30 RVA: 0x00002268 File Offset: 0x00000468
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

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600001F RID: 31 RVA: 0x0000227B File Offset: 0x0000047B
		// (set) Token: 0x06000020 RID: 32 RVA: 0x00002288 File Offset: 0x00000488
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

		// Token: 0x06000021 RID: 33 RVA: 0x00002298 File Offset: 0x00000498
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

		// Token: 0x06000022 RID: 34 RVA: 0x0000231C File Offset: 0x0000051C
		public void AddAttribute(string distinguishedName, string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				this.attributes.Add(new KeyValuePair<string, string>(distinguishedName, value));
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002338 File Offset: 0x00000538
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

		// Token: 0x06000024 RID: 36 RVA: 0x000023A4 File Offset: 0x000005A4
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
					this.connection.Bind();
					this.connection.SessionOptions.StopTransportLayerSecurity();
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

		// Token: 0x06000025 RID: 37 RVA: 0x00002460 File Offset: 0x00000660
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

		// Token: 0x06000026 RID: 38 RVA: 0x000024F8 File Offset: 0x000006F8
		public SearchResponse Search(SearchRequest sRequest)
		{
			SearchResponse result;
			try
			{
				result = (this.connection.SendRequest(sRequest) as SearchResponse);
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002530 File Offset: 0x00000730
		public SearchResponse Search(string distinguishedName, string ldapFilter, string returnAttributes)
		{
			SearchResponse result;
			try
			{
				string[] array = null;
				if (!string.IsNullOrEmpty(returnAttributes))
				{
					array = returnAttributes.Split(new char[]
					{
						','
					});
				}
				CWLogger.Logger.Debug("LDAP:Search:ReturnAttributes={0}", (array == null) ? "NULL" : string.Join(", ", array.ToArray<string>()));
				SearchRequest sRequest = new SearchRequest(distinguishedName, ldapFilter, System.DirectoryServices.Protocols.SearchScope.Subtree, array);
				result = this.Search(sRequest);
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000025AC File Offset: 0x000007AC
		internal static Dictionary<string, string> ConvertToDictionary(SearchResponse sResponse)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			UTF8Encoding utf8Encoding = new UTF8Encoding(false, true);
			if (sResponse.Entries.Count > 0)
			{
				foreach (object obj in sResponse.Entries[0].Attributes.Values)
				{
					DirectoryAttribute directoryAttribute = (DirectoryAttribute)obj;
					foreach (object obj2 in directoryAttribute)
					{
						byte[] bytes = (byte[])obj2;
						try
						{
							string @string = utf8Encoding.GetString(bytes);
							dictionary.Add(directoryAttribute.Name, @string);
						}
						catch
						{
						}
					}
				}
				return dictionary;
			}
			return null;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000026A0 File Offset: 0x000008A0
		public void Dispose()
		{
			if (this.connection != null)
			{
				this.connection.Dispose();
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000026B8 File Offset: 0x000008B8
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

		// Token: 0x0600002B RID: 43 RVA: 0x00002750 File Offset: 0x00000950
		public static Dictionary<string, string> IsAuthenticatedV3(string serverName, int port, string dc, string lookupAttribute, string returnAttributes, string authTypeStr, string username, string pwd, out Exception ex)
		{
			ex = null;
			Dictionary<string, string> result;
			try
			{
				using (LDAP ldap = new LDAP(serverName, port))
				{
					ldap.AddAttribute(lookupAttribute.Trim().ToUpper(), username);
					LDAP.setAttributes(ldap, dc);
					LDAP.setAuthenticationType(ldap, authTypeStr);
					if (LDAP.IsAuthenticatedV3(ldap, pwd, out ex))
					{
						string ldapFilter = string.Concat(new string[]
						{
							"(",
							lookupAttribute,
							"=",
							username,
							")"
						});
						result = LDAP.ConvertToDictionary(ldap.Search(ldap.AttributeString, ldapFilter, returnAttributes));
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

		// Token: 0x0600002C RID: 44 RVA: 0x00002810 File Offset: 0x00000A10
		public static Dictionary<string, string> IsAuthenticatedWithTLS_V3(string serverName, int port, string dc, string lookupAttribute, string returnAttributes, string authTypeStr, string username, string pwd, out Exception ex)
		{
			ex = null;
			Dictionary<string, string> result;
			try
			{
				using (LDAP ldap = new LDAP(serverName, port))
				{
					ldap.AddAttribute(lookupAttribute.Trim().ToUpper(), username);
					LDAP.setAttributes(ldap, dc);
					LDAP.setAuthenticationType(ldap, authTypeStr);
					if (LDAP.IsAuthenticatedWithTLS_V3(ldap, pwd, out ex))
					{
						string ldapFilter = string.Concat(new string[]
						{
							"(",
							lookupAttribute,
							"=",
							username,
							")"
						});
						result = LDAP.ConvertToDictionary(ldap.Search(ldap.AttributeString, ldapFilter, returnAttributes));
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

		// Token: 0x0600002D RID: 45 RVA: 0x000028D0 File Offset: 0x00000AD0
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

		// Token: 0x0600002E RID: 46 RVA: 0x00002904 File Offset: 0x00000B04
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

		// Token: 0x0600002F RID: 47 RVA: 0x00002938 File Offset: 0x00000B38
		private static void setAuthenticationType(LDAP ldap, string authTypeStr)
		{
			if (string.IsNullOrEmpty(authTypeStr))
			{
				return;
			}
			ldap.AuthType = AuthType.Basic;
			string a = authTypeStr.Trim().ToLower();
			if (a == "securesocketslayer")
			{
				ldap.SSL = true;
				return;
			}
			if (a == "sealing")
			{
				ldap.Sealing = true;
				return;
			}
			if (!(a == "signing"))
			{
				return;
			}
			ldap.Signing = true;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000029A0 File Offset: 0x00000BA0
		private static void setAttributes(LDAP ldap, string dc)
		{
			if (!string.IsNullOrEmpty(dc))
			{
				string[] array = dc.Split(new char[]
				{
					','
				}, StringSplitOptions.RemoveEmptyEntries);
				for (int i = 0; i < array.Length; i++)
				{
					string[] array2 = array[i].Split(new char[]
					{
						'='
					});
					if (array2.Length == 2)
					{
						ldap.AddAttribute(array2[0], array2[1]);
					}
				}
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000029FC File Offset: 0x00000BFC
		public static StringDictionary IsAuthenticatedActiveDirectoryV2(string domain, string lookupAttribute, string[] returnAttributes, string username, string password, out Exception ex)
		{
			StringDictionary result;
			using (DirectoryEntry directoryEntry = new DirectoryEntry("LDAP://" + domain, username, password))
			{
				try
				{
					DirectorySearcher directorySearcher = new DirectorySearcher(directoryEntry)
					{
						PageSize = int.MaxValue,
						Filter = string.Concat(new string[]
						{
							"(",
							string.IsNullOrEmpty(lookupAttribute) ? "cn" : lookupAttribute,
							"=",
							username,
							")"
						}),
						SearchScope = System.DirectoryServices.SearchScope.Subtree
					};
					if (returnAttributes == null || returnAttributes.Length == 0)
					{
						directorySearcher.PropertiesToLoad.Add("*");
						directorySearcher.PropertiesToLoad.Add("+");
					}
					else
					{
						foreach (string value in returnAttributes)
						{
							directorySearcher.PropertiesToLoad.Add(value);
						}
					}
					SearchResult searchResult = directorySearcher.FindOne();
					bool flag;
					if (searchResult == null)
					{
						flag = (null != null);
					}
					else
					{
						ResultPropertyCollection properties = searchResult.Properties;
						flag = (((properties != null) ? properties.PropertyNames : null) != null);
					}
					if (!flag)
					{
						CWLogger.Logger.Error("LDAP: No results found for username={0}", username);
						ex = null;
						result = null;
					}
					else
					{
						StringDictionary stringDictionary = new StringDictionary();
						foreach (object obj in searchResult.Properties.PropertyNames)
						{
							string text = (string)obj;
							if (searchResult.Properties[text] != null && searchResult.Properties[text].Count > 0)
							{
								CWLogger.Logger.Debug("LDAP: {0}={1}", text, searchResult.Properties[text][0].ToString());
							}
							if (!stringDictionary.ContainsKey(text) && searchResult.Properties[text] != null && searchResult.Properties[text].Count > 0)
							{
								stringDictionary.Add(text, searchResult.Properties[text][0].ToString());
							}
						}
						ex = null;
						result = stringDictionary;
					}
				}
				catch (Exception ex2)
				{
					ex = ex2;
					result = null;
				}
			}
			return result;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002C68 File Offset: 0x00000E68
		public static StringDictionary IsAuthenticatedActiveDirectory(string domain, string username, string password, out Exception ex)
		{
			StringDictionary result;
			using (DirectoryEntry directoryEntry = new DirectoryEntry("LDAP://" + domain, username, password))
			{
				try
				{
					object nativeObject = directoryEntry.NativeObject;
					StringDictionary stringDictionary = new StringDictionary();
					stringDictionary.Add("username", directoryEntry.Username);
					ex = null;
					result = stringDictionary;
				}
				catch (Exception ex2)
				{
					ex = ex2;
					result = null;
				}
			}
			return result;
		}

		// Token: 0x04000001 RID: 1
		protected LdapConnection connection;

		// Token: 0x04000002 RID: 2
		protected LdapDirectoryIdentifier dirIdentifier;

		// Token: 0x04000003 RID: 3
		protected string ldap_server;

		// Token: 0x04000004 RID: 4
		protected int ldap_port = 389;

		// Token: 0x04000005 RID: 5
		protected string ldap_domain;

		// Token: 0x04000006 RID: 6
		protected List<KeyValuePair<string, string>> attributes;

		// Token: 0x04000007 RID: 7
		protected string certicate_path;

		// Token: 0x04000008 RID: 8
		protected string certicate_password;

		// Token: 0x04000009 RID: 9
		public const string ATT_COMMON_NAME = "CN";

		// Token: 0x0400000A RID: 10
		public const string ATT_LOCALITY_NAME = "L";

		// Token: 0x0400000B RID: 11
		public const string ATT_STATE_OR_PROVINCE_NAME = "ST";

		// Token: 0x0400000C RID: 12
		public const string ATT_ORGANIZATION_NAME = "O";

		// Token: 0x0400000D RID: 13
		public const string ATT_ORGANIZATIONAL_UNIT_NAME = "OU";

		// Token: 0x0400000E RID: 14
		public const string ATT_COUNTRY_NAME = "C";

		// Token: 0x0400000F RID: 15
		public const string ATT_STREET_ADDRESS = "STREET";

		// Token: 0x04000010 RID: 16
		public const string ATT_DOMAIN_COMPONENT = "DC";

		// Token: 0x04000011 RID: 17
		public const string ATT_USER_ID = "UID";
	}
}
