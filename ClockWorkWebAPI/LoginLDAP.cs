using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.DirectoryServices;
using System.DirectoryServices.Protocols;
using System.Net;
using System.Text;
using System.Web.Caching;
using Novell.Directory.Ldap;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Public.Entities.Settings;

namespace ClockWorkWebAPI
{
	// Token: 0x0200001C RID: 28
	public class LoginLDAP
	{
		// Token: 0x060001A4 RID: 420 RVA: 0x0000BFD8 File Offset: 0x0000A1D8
		public static StringDictionary IsAuthenticated2(db conn, Cache cache, string username, string password, out Exception ex)
		{
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			string settingValue = webSettingsClientManager.GetSettingValue<string>(Setting.LDAP_server);
			int settingValue2 = webSettingsClientManager.GetSettingValue<int>(Setting.LDAP_port);
			string settingValue3 = webSettingsClientManager.GetSettingValue<string>(Setting.LDAP_domain);
			string settingValue4 = webSettingsClientManager.GetSettingValue<string>(Setting.LDAP_lookupattribute);
			string settingValue5 = webSettingsClientManager.GetSettingValue<string>(Setting.LDAP_authtype);
			string settingValue6 = webSettingsClientManager.GetSettingValue<string>(Setting.LDAP_returnattribute);
			return LoginLDAP.IsAuthenticatedV3(settingValue, settingValue2, settingValue3, settingValue4, settingValue6, settingValue5, username, password, out ex);
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0000C054 File Offset: 0x0000A254
		public static Exception IsAuthenticated(string _path, string usernamePrefix, string usernamePostfix, string username, string pwd, out string _path2, out string _filterAttribute)
		{
			return LoginLDAP.IsAuthenticated(_path, usernamePrefix, usernamePostfix, "SAMAccountName", username, pwd, out _path2, out _filterAttribute);
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000C07C File Offset: 0x0000A27C
		public static Exception IsAuthenticated(string _path, string usernamePrefix, string usernamePostfix, string filter, string username, string pwd, out string _path2, out string _filterAttribute)
		{
			return LoginLDAP.IsAuthenticated(_path, usernamePrefix, usernamePostfix, "cn", filter, username, pwd, out _path2, out _filterAttribute);
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0000C0A4 File Offset: 0x0000A2A4
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

		// Token: 0x060001A8 RID: 424 RVA: 0x0000C120 File Offset: 0x0000A320
		public static StringDictionary IsAuthenticatedNovellLdap(string serverName, int port, string dc, string lookupAttribute, string returnAttributes, string authTypeStr, string username, string pwd, out Exception exception)
		{
			StringDictionary result;
			try
			{
				bool flag = port <= 0;
				if (flag)
				{
					port = 389;
				}
				string text = string.Concat(new string[]
				{
					lookupAttribute,
					"=",
					username,
					",",
					dc
				});
				string host = "LDAP://" + serverName;
				Novell.Directory.Ldap.LdapConnection ldapConnection = new Novell.Directory.Ldap.LdapConnection();
				ldapConnection.Connect(host, port);
				ldapConnection.Bind(username, pwd);
				exception = null;
				result = new StringDictionary
				{
					{
						"username",
						username
					}
				};
			}
			catch (Exception ex)
			{
				exception = ex;
				result = null;
			}
			return result;
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000C1D0 File Offset: 0x0000A3D0
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

		// Token: 0x060001AA RID: 426 RVA: 0x0000C208 File Offset: 0x0000A408
		public static StringDictionary IsAuthenticatedV3(string serverName, int port, string dc, string lookupAttribute, string returnAttributes, string authTypeStr, string username, string pwd, out Exception ex)
		{
			ex = null;
			StringDictionary result;
			try
			{
				using (LDAP ldap = new LDAP(serverName, port))
				{
					ldap.AddAttribute(lookupAttribute.Trim().ToUpper(), username);
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
					ldap.AuthType = AuthType.Basic;
					string text2 = authTypeStr.Trim().ToLower();
					string a = text2;
					if (!(a == "securesocketslayer"))
					{
						if (!(a == "sealing"))
						{
							if (!(a == "signing"))
							{
								ldap.Sealing = true;
							}
							else
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
					bool flag3 = LoginLDAP.IsAuthenticatedV3(ldap, pwd, out ex);
					bool flag4 = flag3;
					if (flag4)
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

		// Token: 0x060001AB RID: 427 RVA: 0x0000C3CC File Offset: 0x0000A5CC
		public static StringDictionary IsAuthenticated2(string serverName, int port, string dc, string lookupAttribute, string returnAttributes, string authTypeStr, string username, string pwd, out Exception exception)
		{
			StringDictionary result;
			try
			{
				AuthenticationTypes authenticationType = AuthenticationTypes.Sealing;
				Array values = Enum.GetValues(typeof(AuthenticationTypes));
				string text = authTypeStr.ToLower().Trim();
				bool flag = text.Equals("securesocketslayer");
				if (flag)
				{
					authenticationType = AuthenticationTypes.Encryption;
				}
				else
				{
					foreach (object obj in values)
					{
						AuthenticationTypes authenticationTypes = (AuthenticationTypes)obj;
						string name = Enum.GetName(typeof(AuthenticationTypes), authenticationTypes);
						bool flag2 = name.ToLower().Trim().CompareTo(text) == 0;
						if (flag2)
						{
							authenticationType = authenticationTypes;
							break;
						}
					}
				}
				string username2 = string.Concat(new string[]
				{
					lookupAttribute,
					"=",
					username,
					",",
					dc
				});
				string text2 = "LDAP://" + serverName;
				bool flag3 = port > 0;
				if (flag3)
				{
					text2 = text2 + ":" + port.ToString();
				}
				text2 = text2 + "/" + dc;
				DirectoryEntry searchRoot = new DirectoryEntry(text2, username2, pwd, authenticationType);
				DirectorySearcher directorySearcher = new DirectorySearcher(searchRoot);
				directorySearcher.Filter = string.Concat(new string[]
				{
					"(",
					lookupAttribute,
					"=",
					username,
					")"
				});
				directorySearcher.PropertyNamesOnly = true;
				bool flag4 = returnAttributes.Length < 1;
				if (flag4)
				{
					returnAttributes = lookupAttribute;
				}
				string[] array = returnAttributes.Split(new char[]
				{
					','
				});
				foreach (string value in array)
				{
					directorySearcher.PropertiesToLoad.Add(value);
				}
				SearchResult searchResult = directorySearcher.FindOne();
				bool flag5 = searchResult != null;
				if (flag5)
				{
					exception = null;
					StringDictionary stringDictionary = new StringDictionary();
					foreach (object obj2 in searchResult.Properties.PropertyNames)
					{
						string text3 = (string)obj2;
						ResultPropertyValueCollection resultPropertyValueCollection = searchResult.Properties[text3];
						bool flag6 = resultPropertyValueCollection != null && resultPropertyValueCollection.Count > 0;
						if (flag6)
						{
							object obj3 = resultPropertyValueCollection[0];
							stringDictionary.Add(text3, (obj3 == null) ? "" : obj3.ToString());
						}
					}
					result = stringDictionary;
				}
				else
				{
					exception = new Exception("No results found");
					result = null;
				}
			}
			catch (Exception ex)
			{
				exception = ex;
				result = null;
			}
			return result;
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000C6C8 File Offset: 0x0000A8C8
		public static Exception IsAuthenticated(string _path, string usernamePrefix, string usernamePostfix, string returnAttributeName, string filter, string username, string pwd, out string _path2, out string _filterAttribute)
		{
			string username2 = usernamePrefix + username + usernamePostfix;
			DirectoryEntry directoryEntry = new DirectoryEntry(_path, username2, pwd, AuthenticationTypes.Sealing);
			Exception result;
			try
			{
				object nativeObject = directoryEntry.NativeObject;
				SearchResult searchResult = new DirectorySearcher(directoryEntry)
				{
					Filter = string.Concat(new string[]
					{
						"(",
						filter,
						"=",
						username,
						")"
					}),
					PropertiesToLoad = 
					{
						returnAttributeName
					}
				}.FindOne();
				bool flag = searchResult != null;
				if (flag)
				{
					_path2 = searchResult.Path;
					_filterAttribute = (string)searchResult.Properties[returnAttributeName][0];
					result = null;
				}
				else
				{
					_path2 = "";
					_filterAttribute = "";
					result = new Exception("result == null");
				}
			}
			catch (Exception ex)
			{
				_path2 = "";
				_filterAttribute = "";
				result = ex;
			}
			return result;
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000C7C8 File Offset: 0x0000A9C8
		public static Exception LoginLdapDoubleBind(bool ssl, string serverName, int port, string preUsername, string prePassword, string preDomain, string preLookupAttribute, string username, string password, string domain, string lookupAttribute, string[] returnAttributes, out StringDictionary collectedArgs)
		{
			return LoginLDAP.LoginLdapDoubleBind(ssl, serverName, port, preUsername, prePassword, preDomain, preLookupAttribute, username, password, domain, lookupAttribute, returnAttributes, out collectedArgs, AuthType.Basic);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0000C7F8 File Offset: 0x0000A9F8
		public static Exception LoginLdapDoubleBind(bool ssl, string serverName, int port, string preUsername, string prePassword, string preDomain, string preLookupAttribute, string username, string password, string domain, string lookupAttribute, string[] returnAttributes, out StringDictionary collectedArgs, AuthType authType)
		{
			return LoginLDAP.LoginLdapDoubleBind(ssl, serverName, port, preUsername, prePassword, preDomain, preLookupAttribute, username, password, domain, lookupAttribute, returnAttributes, out collectedArgs, authType, 0);
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000C828 File Offset: 0x0000AA28
		public static Exception LoginLdapDoubleBind(bool ssl, string serverName, int port, string preUsername, string prePassword, string preDomain, string preLookupAttribute, string username, string password, string domain, string lookupAttribute, string[] returnAttributes, out StringDictionary collectedArgs, int protocolVersion)
		{
			return LoginLDAP.LoginLdapDoubleBind(ssl, serverName, port, preUsername, prePassword, preDomain, preLookupAttribute, username, password, domain, lookupAttribute, returnAttributes, out collectedArgs, AuthType.Basic, protocolVersion);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000C858 File Offset: 0x0000AA58
		public static Exception LoginLdapDoubleBind(bool ssl, string serverName, int port, string preUsername, string prePassword, string preDomain, string preLookupAttribute, string username, string password, string domain, string lookupAttribute, string[] returnAttributes, out StringDictionary collectedArgs, AuthType authType, int protocolVersion)
		{
			collectedArgs = new StringDictionary();
			string text = "0";
			Exception result;
			try
			{
				NetworkCredential networkCredential = new NetworkCredential(string.Concat(new string[]
				{
					preLookupAttribute,
					"=",
					preUsername,
					",",
					preDomain
				}), prePassword);
				bool flag = port > 0;
				LdapDirectoryIdentifier identifier;
				if (flag)
				{
					identifier = new LdapDirectoryIdentifier(serverName, port, false, false);
				}
				else
				{
					identifier = new LdapDirectoryIdentifier(serverName, false, false);
				}
				System.DirectoryServices.Protocols.LdapConnection ldapConnection = new System.DirectoryServices.Protocols.LdapConnection(identifier);
				bool flag2 = protocolVersion > 0;
				if (flag2)
				{
					ldapConnection.SessionOptions.ProtocolVersion = protocolVersion;
				}
				ldapConnection.AuthType = authType;
				ldapConnection.Credential = networkCredential;
				ldapConnection.SessionOptions.SecureSocketLayer = ssl;
				text = "1";
				ldapConnection.Bind(networkCredential);
				text = "2";
				string text2 = string.Concat(new string[]
				{
					"(",
					lookupAttribute,
					"=",
					username,
					")"
				});
				DirectoryRequest request = new SearchRequest(domain, text2, System.DirectoryServices.Protocols.SearchScope.Subtree, returnAttributes);
				SearchResponse searchResponse = (SearchResponse)ldapConnection.SendRequest(request);
				bool flag3 = searchResponse.Entries.Count < 1;
				if (flag3)
				{
					string str = (returnAttributes == null) ? "NULL" : returnAttributes.Length.ToString();
					string message = string.Format("Response.Entries.Count<1: Domain: [{0}]; LdapFilter: {1}; ReturnAttributes.Count=" + str, domain, text2);
					throw new Exception(message);
				}
				SearchResultEntry searchResultEntry = searchResponse.Entries[0];
				string distinguishedName = searchResultEntry.DistinguishedName;
				text = "5";
				ldapConnection.Bind(new NetworkCredential(distinguishedName, password));
				text = "6";
				foreach (object obj in searchResponse.Entries)
				{
					SearchResultEntry searchResultEntry2 = (SearchResultEntry)obj;
					text = "7";
					foreach (object obj2 in searchResultEntry2.Attributes)
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)obj2;
						text = "8";
						DirectoryAttribute directoryAttribute = (DirectoryAttribute)dictionaryEntry.Value;
						string name = directoryAttribute.Name;
						string value = "";
						foreach (object obj3 in directoryAttribute)
						{
							bool flag4 = obj3 is byte[];
							if (flag4)
							{
								value = Encoding.ASCII.GetString((byte[])obj3);
							}
							else
							{
								value = ((obj3 == null) ? "" : obj3.ToString());
							}
						}
						bool flag5 = !collectedArgs.ContainsKey(name);
						if (flag5)
						{
							collectedArgs.Add(name, value);
						}
						else
						{
							bool flag6 = !string.IsNullOrEmpty(value);
							if (flag6)
							{
								collectedArgs.Remove(name);
								collectedArgs.Add(name, value);
							}
						}
					}
				}
				result = null;
			}
			catch (Exception ex)
			{
				result = new Exception(text.ToString() + ": " + ex.ToString());
			}
			return result;
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000CBF4 File Offset: 0x0000ADF4
		public static List<KeyValuePair<string, string>> LoginLdapDoubleBindAnonymousFirstBind(bool ssl, string serverName, int port, string preDomain, string preLookupAttribute, string username, string password, string domain, string lookupAttribute, string[] returnAttributes, AuthType authType)
		{
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			LdapDirectoryIdentifier identifier = (port > 0) ? new LdapDirectoryIdentifier(serverName, port, false, false) : new LdapDirectoryIdentifier(serverName, false, false);
			System.DirectoryServices.Protocols.LdapConnection ldapConnection = new System.DirectoryServices.Protocols.LdapConnection(identifier)
			{
				AuthType = authType
			};
			ldapConnection.SessionOptions.SecureSocketLayer = ssl;
			ldapConnection.Bind();
			string ldapFilter = string.Concat(new string[]
			{
				"(",
				lookupAttribute,
				"=",
				username,
				")"
			});
			SearchRequest request = new SearchRequest(domain, ldapFilter, System.DirectoryServices.Protocols.SearchScope.Subtree, returnAttributes);
			SearchResponse searchResponse = (SearchResponse)ldapConnection.SendRequest(request);
			bool flag = searchResponse.Entries.Count < 1;
			List<KeyValuePair<string, string>> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				SearchResultEntry searchResultEntry = searchResponse.Entries[0];
				string distinguishedName = searchResultEntry.DistinguishedName;
				ldapConnection.Bind(new NetworkCredential(distinguishedName, password));
				foreach (object obj in searchResponse.Entries)
				{
					SearchResultEntry searchResultEntry2 = (SearchResultEntry)obj;
					foreach (object obj2 in searchResultEntry2.Attributes)
					{
						DirectoryAttribute directoryAttribute = (DirectoryAttribute)((DictionaryEntry)obj2).Value;
						string name = directoryAttribute.Name;
						foreach (object obj3 in directoryAttribute)
						{
							bool flag2 = obj3 is byte[];
							string value;
							if (flag2)
							{
								value = Encoding.ASCII.GetString((byte[])obj3);
							}
							else
							{
								value = ((obj3 == null) ? "" : obj3.ToString());
							}
							list.Add(new KeyValuePair<string, string>(name, value));
						}
					}
				}
				result = list;
			}
			return result;
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0000CE58 File Offset: 0x0000B058
		public static List<KeyValuePair<string, string>> LoginLdapDoubleBind(bool ssl, string serverName, int port, string preUsername, string prePassword, string preDomain, string preLookupAttribute, string username, string password, string domain, string lookupAttribute, string[] returnAttributes, AuthType authType)
		{
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			NetworkCredential networkCredential = new NetworkCredential(string.Concat(new string[]
			{
				preLookupAttribute,
				"=",
				preUsername,
				",",
				preDomain
			}), prePassword);
			LdapDirectoryIdentifier identifier = (port > 0) ? new LdapDirectoryIdentifier(serverName, port, false, false) : new LdapDirectoryIdentifier(serverName, false, false);
			System.DirectoryServices.Protocols.LdapConnection ldapConnection = new System.DirectoryServices.Protocols.LdapConnection(identifier)
			{
				AuthType = authType,
				Credential = networkCredential
			};
			ldapConnection.SessionOptions.SecureSocketLayer = ssl;
			ldapConnection.Bind(networkCredential);
			string ldapFilter = string.Concat(new string[]
			{
				"(",
				lookupAttribute,
				"=",
				username,
				")"
			});
			SearchRequest request = new SearchRequest(domain, ldapFilter, System.DirectoryServices.Protocols.SearchScope.Subtree, returnAttributes);
			SearchResponse searchResponse = (SearchResponse)ldapConnection.SendRequest(request);
			bool flag = searchResponse.Entries.Count < 1;
			List<KeyValuePair<string, string>> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				SearchResultEntry searchResultEntry = searchResponse.Entries[0];
				string distinguishedName = searchResultEntry.DistinguishedName;
				ldapConnection.Bind(new NetworkCredential(distinguishedName, password));
				foreach (object obj in searchResponse.Entries)
				{
					SearchResultEntry searchResultEntry2 = (SearchResultEntry)obj;
					foreach (object obj2 in searchResultEntry2.Attributes)
					{
						DirectoryAttribute directoryAttribute = (DirectoryAttribute)((DictionaryEntry)obj2).Value;
						string name = directoryAttribute.Name;
						foreach (object obj3 in directoryAttribute)
						{
							bool flag2 = obj3 is byte[];
							string value;
							if (flag2)
							{
								value = Encoding.ASCII.GetString((byte[])obj3);
							}
							else
							{
								value = ((obj3 == null) ? "" : obj3.ToString());
							}
							list.Add(new KeyValuePair<string, string>(name, value));
						}
					}
				}
				result = list;
			}
			return result;
		}
	}
}
