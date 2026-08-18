using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.DirectoryServices.Protocols;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using ClockWorkLogger;

namespace ClockWorkWebAPI
{
	// Token: 0x0200001A RID: 26
	public class LDAP : IDisposable
	{
		// Token: 0x06000170 RID: 368 RVA: 0x0000AAC0 File Offset: 0x00008CC0
		public LDAP(string host) : this(host, 389)
		{
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000AAD0 File Offset: 0x00008CD0
		public LDAP(string host, int port) : this(host, port, string.Empty)
		{
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000AAE4 File Offset: 0x00008CE4
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

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000173 RID: 371 RVA: 0x0000AB7C File Offset: 0x00008D7C
		public string AttributeString
		{
			get
			{
				return this.getQueryStringFromAttributes();
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000174 RID: 372 RVA: 0x0000AB94 File Offset: 0x00008D94
		// (set) Token: 0x06000175 RID: 373 RVA: 0x0000ABB1 File Offset: 0x00008DB1
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

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000176 RID: 374 RVA: 0x0000ABC4 File Offset: 0x00008DC4
		// (set) Token: 0x06000177 RID: 375 RVA: 0x0000ABE1 File Offset: 0x00008DE1
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

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000178 RID: 376 RVA: 0x0000ABF4 File Offset: 0x00008DF4
		// (set) Token: 0x06000179 RID: 377 RVA: 0x0000AC11 File Offset: 0x00008E11
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

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600017A RID: 378 RVA: 0x0000AC24 File Offset: 0x00008E24
		// (set) Token: 0x0600017B RID: 379 RVA: 0x0000AC41 File Offset: 0x00008E41
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

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600017C RID: 380 RVA: 0x0000AC54 File Offset: 0x00008E54
		// (set) Token: 0x0600017D RID: 381 RVA: 0x0000AC71 File Offset: 0x00008E71
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

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600017E RID: 382 RVA: 0x0000AC84 File Offset: 0x00008E84
		// (set) Token: 0x0600017F RID: 383 RVA: 0x0000ACA1 File Offset: 0x00008EA1
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

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000180 RID: 384 RVA: 0x0000ACB4 File Offset: 0x00008EB4
		// (set) Token: 0x06000181 RID: 385 RVA: 0x0000ACD1 File Offset: 0x00008ED1
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

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000182 RID: 386 RVA: 0x0000ACE4 File Offset: 0x00008EE4
		// (set) Token: 0x06000183 RID: 387 RVA: 0x0000AD01 File Offset: 0x00008F01
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

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000184 RID: 388 RVA: 0x0000AD14 File Offset: 0x00008F14
		// (set) Token: 0x06000185 RID: 389 RVA: 0x0000AD31 File Offset: 0x00008F31
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

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000186 RID: 390 RVA: 0x0000AD44 File Offset: 0x00008F44
		// (set) Token: 0x06000187 RID: 391 RVA: 0x0000AD66 File Offset: 0x00008F66
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

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000188 RID: 392 RVA: 0x0000AD7C File Offset: 0x00008F7C
		// (set) Token: 0x06000189 RID: 393 RVA: 0x0000AD9E File Offset: 0x00008F9E
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

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600018A RID: 394 RVA: 0x0000ADB4 File Offset: 0x00008FB4
		// (set) Token: 0x0600018B RID: 395 RVA: 0x0000ADD6 File Offset: 0x00008FD6
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

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600018C RID: 396 RVA: 0x0000ADEC File Offset: 0x00008FEC
		// (set) Token: 0x0600018D RID: 397 RVA: 0x0000AE09 File Offset: 0x00009009
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

		// Token: 0x0600018E RID: 398 RVA: 0x0000AE1C File Offset: 0x0000901C
		private string getAttribute(string key)
		{
			foreach (KeyValuePair<string, string> keyValuePair in this.attributes)
			{
				bool flag = keyValuePair.Key.ToUpper() == key.ToUpper();
				if (flag)
				{
					return string.Format("{0}={1}", key.ToUpper(), keyValuePair.Value);
				}
			}
			return null;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x0000AEA8 File Offset: 0x000090A8
		public void AddAttribute(string distinguishedName, string value)
		{
			bool flag = !string.IsNullOrEmpty(value);
			if (flag)
			{
				this.attributes.Add(new KeyValuePair<string, string>(distinguishedName, value));
			}
		}

		// Token: 0x06000190 RID: 400 RVA: 0x0000AED8 File Offset: 0x000090D8
		public bool Bind(string uPassword, out Exception ex)
		{
			ex = null;
			bool result;
			try
			{
				bool flag = string.IsNullOrEmpty(uPassword);
				if (flag)
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

		// Token: 0x06000191 RID: 401 RVA: 0x0000AF4C File Offset: 0x0000914C
		public bool BindTLS(string uPassword, out Exception ex)
		{
			ex = null;
			bool result;
			try
			{
				bool flag = string.IsNullOrEmpty(uPassword);
				if (flag)
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

		// Token: 0x06000192 RID: 402 RVA: 0x0000B014 File Offset: 0x00009214
		private string getQueryStringFromAttributes()
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = this.attributes.Count > 0;
			if (flag)
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

		// Token: 0x06000193 RID: 403 RVA: 0x0000B0C0 File Offset: 0x000092C0
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

		// Token: 0x06000194 RID: 404 RVA: 0x0000B0FC File Offset: 0x000092FC
		public SearchResponse Search(string distinguishedName, string ldapFilter, string returnAttributes)
		{
			SearchResponse result;
			try
			{
				string[] attributeList = null;
				bool flag = !string.IsNullOrEmpty(returnAttributes);
				if (flag)
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

		// Token: 0x06000195 RID: 405 RVA: 0x0000B15C File Offset: 0x0000935C
		internal static StringDictionary ConvertToDictionary(SearchResponse sResponse)
		{
			StringDictionary stringDictionary = new StringDictionary();
			UTF8Encoding utf8Encoding = new UTF8Encoding(false, true);
			bool flag = sResponse.Entries.Count > 0;
			StringDictionary result;
			if (flag)
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

		// Token: 0x06000196 RID: 406 RVA: 0x0000B278 File Offset: 0x00009478
		public void Dispose()
		{
			bool flag = this.connection != null;
			if (flag)
			{
				this.connection.Dispose();
			}
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000B2A0 File Offset: 0x000094A0
		public static void ParseLdapSettings(string settings, out string server, out int port, out string domain, out string authenticationType, out string lookupAttribute, out string returnAttributes)
		{
			string[] array = settings.Split(new char[]
			{
				'`'
			});
			int num = array.Length;
			server = ((num > 0) ? array[0] : "");
			bool flag = num > 1;
			if (flag)
			{
				bool flag2 = !int.TryParse(array[1], out port);
				if (flag2)
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

		// Token: 0x06000198 RID: 408 RVA: 0x0000B344 File Offset: 0x00009544
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
					bool flag2 = flag;
					if (flag2)
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

		// Token: 0x06000199 RID: 409 RVA: 0x0000B41C File Offset: 0x0000961C
		public static StringDictionary LdapAnonymous(string serverName, int port, string dc, string lookupAttribute, string returnAttributes, string username, out Exception ex)
		{
			ex = null;
			StringDictionary result;
			try
			{
				using (LDAP ldap = new LDAP(serverName, port))
				{
					LDAP.setAttributes(ldap, dc);
					try
					{
						ldap.connection.Bind();
					}
					catch (Exception ex2)
					{
						ex = ex2;
						return null;
					}
					string ldapFilter = string.Concat(new string[]
					{
						"(",
						lookupAttribute,
						"=",
						username,
						")"
					});
					SearchResponse sResponse = ldap.Search(ldap.AttributeString, ldapFilter, returnAttributes);
					ex = null;
					result = LDAP.ConvertToDictionary(sResponse);
				}
			}
			catch (Exception ex3)
			{
				ex = ex3;
				result = null;
			}
			return result;
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000B4E8 File Offset: 0x000096E8
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
					bool flag2 = flag;
					if (flag2)
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

		// Token: 0x0600019B RID: 411 RVA: 0x0000B5C0 File Offset: 0x000097C0
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

		// Token: 0x0600019C RID: 412 RVA: 0x0000B5F8 File Offset: 0x000097F8
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

		// Token: 0x0600019D RID: 413 RVA: 0x0000B630 File Offset: 0x00009830
		private static void setAuthenticationType(LDAP ldap, string authTypeStr)
		{
			bool flag = string.IsNullOrEmpty(authTypeStr);
			if (!flag)
			{
				ldap.AuthType = AuthType.Basic;
				string text = authTypeStr.Trim().ToLower();
				string a = text;
				if (!(a == "securesocketslayer"))
				{
					if (!(a == "sealing"))
					{
						if (a == "signing")
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

		// Token: 0x0600019E RID: 414 RVA: 0x0000B6A8 File Offset: 0x000098A8
		private static void setAttributes(LDAP ldap, string dc)
		{
			bool flag = !string.IsNullOrEmpty(dc);
			if (flag)
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
					bool flag2 = array3.Length == 2;
					if (flag2)
					{
						ldap.AddAttribute(array3[0], array3[1]);
					}
				}
			}
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000B720 File Offset: 0x00009920
		public static StringDictionary IsAuthenticatedV4(string serverName, int port, string dc, string lookupAttribute, string returnAttributesStr, string authTypeStr, string username, string pwd, out Exception ex)
		{
			string text = string.Format("{0}={1},{2}", lookupAttribute, username, dc);
			string[] attributeList = returnAttributesStr.Split(new char[]
			{
				','
			}, StringSplitOptions.RemoveEmptyEntries);
			StringDictionary result;
			try
			{
				CWLogger.Logger.Debug("/*********** TESTING LDAP ************/");
				CWLogger.Logger.Debug("DN: {0}", text);
				NetworkCredential networkCredential = new NetworkCredential(text, pwd);
				CWLogger.Logger.Debug("Path: {0}", serverName);
				LdapDirectoryIdentifier identifier = new LdapDirectoryIdentifier(serverName, true, false);
				using (LdapConnection ldapConnection = new LdapConnection(identifier, networkCredential, AuthType.Basic))
				{
					CWLogger.Logger.Debug("Ldap connection created");
					ldapConnection.SessionOptions.ProtocolVersion = 3;
					ldapConnection.SessionOptions.VerifyServerCertificate = ((LdapConnection connection, X509Certificate cert) => true);
					CWLogger.Logger.Debug("Starting transport layer security");
					try
					{
						ldapConnection.SessionOptions.StartTransportLayerSecurity(null);
						CWLogger.Logger.Debug("Transport Layer Security started properly");
					}
					catch (Exception)
					{
						CWLogger.Logger.Debug("Transport Layer Security failed");
					}
					try
					{
						CWLogger.Logger.Debug("Trying to authenticate to the server with the supplied credentials");
						ldapConnection.Bind(networkCredential);
						CWLogger.Logger.Debug("Authentication successfully");
						SearchRequest request = new SearchRequest(dc, string.Format("({0}={1})", lookupAttribute, username), SearchScope.Subtree, attributeList);
						CWLogger.Logger.Debug("asking for attributes {0}", returnAttributesStr);
						SearchResponse searchResponse = (SearchResponse)ldapConnection.SendRequest(request);
						bool flag = searchResponse != null;
						if (flag)
						{
							CWLogger.Logger.Debug("Got response");
							CWLogger.Logger.Debug("response matched DN = {0}", searchResponse.MatchedDN);
							CWLogger.Logger.Debug("count entries = {0}", searchResponse.Entries.Count);
							CWLogger.Logger.Debug("count references = {0}", searchResponse.References.Count);
							CWLogger.Logger.Debug("Error message = {0}", searchResponse.ErrorMessage);
							CWLogger.Logger.Debug("result code = {0}", searchResponse.ResultCode);
							CWLogger.Logger.Debug("request id = {0}", searchResponse.RequestId);
							StringDictionary stringDictionary = new StringDictionary();
							foreach (object obj in searchResponse.Entries)
							{
								SearchResultEntry searchResultEntry = (SearchResultEntry)obj;
								CWLogger.Logger.Debug("Entry atts count = {0}", searchResultEntry.Attributes.Count);
								foreach (object obj2 in searchResultEntry.Attributes)
								{
									DictionaryEntry dictionaryEntry = (DictionaryEntry)obj2;
									object key = dictionaryEntry.Key;
									DirectoryAttribute directoryAttribute = (DirectoryAttribute)dictionaryEntry.Value;
									CWLogger.Logger.Debug("     directory attribute name = {0}, count values = {1}", key.ToString(), directoryAttribute.Count);
									string text2 = string.Empty;
									foreach (object obj3 in directoryAttribute)
									{
										bool flag2 = obj3 is byte[];
										if (flag2)
										{
											text2 = Encoding.ASCII.GetString((byte[])obj3);
										}
										else
										{
											text2 = directoryAttribute.ToString();
										}
										CWLogger.Logger.Debug("        - {0}", text2);
									}
									string key2 = key.ToString();
									bool flag3 = !stringDictionary.ContainsKey(key2);
									if (flag3)
									{
										stringDictionary.Add(key2, text2 ?? "");
									}
								}
							}
							ex = null;
							result = stringDictionary;
						}
						else
						{
							ex = new Exception("null search response; login failed.");
							result = null;
						}
					}
					catch (LdapException ex2)
					{
						StringBuilder stringBuilder = new StringBuilder();
						stringBuilder.AppendFormat("Hostname: {0}", serverName);
						stringBuilder.AppendLine();
						stringBuilder.AppendFormat("DN: {0}", text);
						stringBuilder.AppendLine();
						stringBuilder.AppendFormat("Error message: {0}, {1}", ex2.ErrorCode, ex2.Message);
						CWLogger.Logger.DebugException(string.Format("Authentication failed: {0}", stringBuilder.ToString()), ex2);
						ex = ex2;
						result = null;
					}
					catch (Exception ex3)
					{
						StringBuilder stringBuilder2 = new StringBuilder();
						stringBuilder2.AppendFormat("Hostname: {0}", serverName);
						stringBuilder2.AppendLine();
						stringBuilder2.AppendFormat("DN: {0}", text);
						stringBuilder2.AppendLine();
						stringBuilder2.AppendFormat("Error message: {0}", ex3.Message);
						stringBuilder2.AppendLine(ex3.ToString());
						CWLogger.Logger.DebugException(string.Format("Authentication failed: {0}", stringBuilder2.ToString()), ex3);
						ex = ex3;
						result = null;
					}
				}
			}
			catch (Exception ex4)
			{
				StringBuilder stringBuilder3 = new StringBuilder();
				stringBuilder3.AppendFormat("Hostname: {0}", serverName);
				stringBuilder3.AppendLine();
				stringBuilder3.AppendFormat("DN: {0}", text);
				stringBuilder3.AppendLine();
				stringBuilder3.AppendFormat("Error message: {0}", ex4.Message);
				stringBuilder3.AppendLine(ex4.ToString());
				CWLogger.Logger.DebugException(string.Format("Ldap failed: {0}", stringBuilder3.ToString()), ex4);
				ex = ex4;
				result = null;
			}
			return result;
		}

		// Token: 0x04000079 RID: 121
		protected LdapConnection connection;

		// Token: 0x0400007A RID: 122
		protected LdapDirectoryIdentifier dirIdentifier;

		// Token: 0x0400007B RID: 123
		protected string ldap_server;

		// Token: 0x0400007C RID: 124
		protected int ldap_port = 389;

		// Token: 0x0400007D RID: 125
		protected string ldap_domain;

		// Token: 0x0400007E RID: 126
		protected List<KeyValuePair<string, string>> attributes;

		// Token: 0x0400007F RID: 127
		protected string certicate_path;

		// Token: 0x04000080 RID: 128
		protected string certicate_password;

		// Token: 0x04000081 RID: 129
		public const string ATT_COMMON_NAME = "CN";

		// Token: 0x04000082 RID: 130
		public const string ATT_LOCALITY_NAME = "L";

		// Token: 0x04000083 RID: 131
		public const string ATT_STATE_OR_PROVINCE_NAME = "ST";

		// Token: 0x04000084 RID: 132
		public const string ATT_ORGANIZATION_NAME = "O";

		// Token: 0x04000085 RID: 133
		public const string ATT_ORGANIZATIONAL_UNIT_NAME = "OU";

		// Token: 0x04000086 RID: 134
		public const string ATT_COUNTRY_NAME = "C";

		// Token: 0x04000087 RID: 135
		public const string ATT_STREET_ADDRESS = "STREET";

		// Token: 0x04000088 RID: 136
		public const string ATT_DOMAIN_COMPONENT = "DC";

		// Token: 0x04000089 RID: 137
		public const string ATT_USER_ID = "UID";
	}
}
