using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.DirectoryServices;
using System.DirectoryServices.Protocols;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Text;
using OracleInternal.Common;

namespace OracleInternal.Network
{
	// Token: 0x0200015F RID: 351
	[DirectoryServicesPermission(SecurityAction.Assert, Unrestricted = true)]
	internal class LDAP : INamingAdapter
	{
		// Token: 0x06000DE7 RID: 3559 RVA: 0x000930AC File Offset: 0x000912AC
		private static void _LDAP(Hashtable dsMap)
		{
			LdapConnection ldapConnection = null;
			dsMap.Clear();
			if (LdapConfig.LdapDirectoryIdentifiers == null)
			{
				return;
			}
			List<LdapDirectoryIdentifier> ldapDirectoryIdentifiers = LdapConfig.LdapDirectoryIdentifiers;
			string distinguishedName = "cn=OracleContext," + LdapConfig.DefaultAdminContext;
			SearchRequest searchRequest = new SearchRequest(distinguishedName, "(|(objectClass=orclNetService)(objectClass=orclService)(objectClass=orclNetServiceAlias))", SearchScope.Subtree, LDAP.m_AttributesToReturn);
			PageResultRequestControl pageResultRequestControl = new PageResultRequestControl(999);
			searchRequest.Controls.Add(pageResultRequestControl);
			for (int i = 0; i < ldapDirectoryIdentifiers.Count; i++)
			{
				try
				{
					ldapConnection = LDAP.CreateLdapConnection(ldapDirectoryIdentifiers[i]);
					bool flag = false;
					while (!flag)
					{
						SearchResponse searchResponse = (SearchResponse)ldapConnection.SendRequest(searchRequest);
						foreach (DirectoryControl directoryControl in searchResponse.Controls)
						{
							if (directoryControl is PageResultResponseControl)
							{
								pageResultRequestControl.Cookie = ((PageResultResponseControl)directoryControl).Cookie;
								break;
							}
						}
						foreach (object obj in searchResponse.Entries)
						{
							SearchResultEntry searchResultEntry = (SearchResultEntry)obj;
							string text = LDAP.ExtractKey(searchResultEntry.DistinguishedName);
							if (!string.IsNullOrEmpty(text))
							{
								string value = LDAP.Attribute2String(searchResultEntry.Attributes["orclNetDescString"]);
								if (!string.IsNullOrEmpty(value))
								{
									if (!dsMap.Contains(text))
									{
										dsMap[text] = value;
									}
								}
								else
								{
									string text2 = LDAP.Attribute2String(searchResultEntry.Attributes["aliasedobjectname"]);
									if (!string.IsNullOrEmpty(text2))
									{
										SearchRequest request = new SearchRequest(text2, "(|(objectClass=orclNetService)(objectClass=orclService)(objectClass=orclNetServiceAlias))", SearchScope.Base, LDAP.m_AttributesToReturn);
										SearchResponse searchResponse2 = (SearchResponse)ldapConnection.SendRequest(request);
										if (searchResponse2.Entries.Count > 0)
										{
											value = LDAP.Attribute2String(searchResponse2.Entries[0].Attributes["orclNetDescString"]);
											if (!string.IsNullOrEmpty(value) && !dsMap.Contains(text))
											{
												dsMap[text] = value;
											}
										}
									}
								}
							}
						}
						if (pageResultRequestControl.Cookie.Length == 0)
						{
							flag = true;
						}
					}
				}
				catch (Exception ex)
				{
					if (ProviderConfig.m_bTraceLevelNetwork)
					{
						LDAP._trace("exception occured  - " + ex.ToString());
					}
				}
				if (ldapConnection != null)
				{
					ldapConnection.Dispose();
					ldapConnection = null;
				}
			}
		}

		// Token: 0x06000DE8 RID: 3560 RVA: 0x00093330 File Offset: 0x00091530
		private static string ExtractKey(string distinguishedName)
		{
			if (string.IsNullOrEmpty(distinguishedName))
			{
				return null;
			}
			int num = distinguishedName.IndexOf('=') + 1;
			int num2 = distinguishedName.IndexOf(',');
			if (num2 > num)
			{
				return distinguishedName.Substring(num, num2 - num);
			}
			return null;
		}

		// Token: 0x06000DE9 RID: 3561 RVA: 0x0009336C File Offset: 0x0009156C
		private static LdapConnection CreateLdapConnection(LdapDirectoryIdentifier ldapDirectoryId)
		{
			LdapConnection ldapConnection = new LdapConnection(ldapDirectoryId);
			ldapConnection.Timeout = new TimeSpan(0, 0, SqlNetOraConfig.LDAPCTimeout);
			ldapConnection.AuthType = LdapConfig.Authtype;
			LdapSessionOptions sessionOptions = ldapConnection.SessionOptions;
			sessionOptions.ProtocolVersion = 3;
			if (LdapConfig.Authtype == AuthType.External || LdapConfig.useSSL)
			{
				sessionOptions.SecureSocketLayer = true;
				if (LdapConfig.Authtype == AuthType.External)
				{
					ldapConnection.ClientCertificates.AddRange(LdapConfig.X509Collection);
				}
				sessionOptions.VerifyServerCertificate = new VerifyServerCertificateCallback(LDAP.AnonymousVerifyServeCertificateCallback);
			}
			if (ProviderConfig.m_bTraceLevelNetwork)
			{
				LDAP._trace("binding to LDAP server " + ldapDirectoryId.Servers[0]);
			}
			ldapConnection.Bind();
			if (ProviderConfig.m_bTraceLevelNetwork)
			{
				LDAP._trace("binded to LDAP server");
			}
			return ldapConnection;
		}

		// Token: 0x06000DEA RID: 3562 RVA: 0x00093424 File Offset: 0x00091624
		private static string Attribute2String(DirectoryAttribute directoryAttribute)
		{
			if (directoryAttribute != null)
			{
				return directoryAttribute[0].ToString();
			}
			return null;
		}

		// Token: 0x06000DEB RID: 3563 RVA: 0x00093444 File Offset: 0x00091644
		public static bool AnonymousVerifyServeCertificateCallback(LdapConnection ldapConn, X509Certificate serverCert)
		{
			return true;
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000DEC RID: 3564 RVA: 0x00093448 File Offset: 0x00091648
		public string ID
		{
			get
			{
				return "LDAP";
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000DED RID: 3565 RVA: 0x00093450 File Offset: 0x00091650
		public Hashtable Map
		{
			get
			{
				Hashtable hashtable = new Hashtable();
				LDAP._LDAP(hashtable);
				if (hashtable.Count <= 0)
				{
					return null;
				}
				return hashtable;
			}
		}

		// Token: 0x06000DEE RID: 3566 RVA: 0x00093478 File Offset: 0x00091678
		public string Resolve(string TNSname, out ConnectionOption CO, string IN = null)
		{
			CO = null;
			if (LdapConfig.LdapDirectoryIdentifiers == null)
			{
				throw new NetworkException(-6800);
			}
			SearchRequest searchRequest = null;
			bool flag = false;
			string text = string.Copy(TNSname);
			int num = text.IndexOf('=');
			if (num > 0)
			{
				string text2 = text.Substring(0, num + 1);
				text2 = text2.Replace(" ", null);
				if (string.Compare(text2, "cn=", StringComparison.OrdinalIgnoreCase) == 0)
				{
					searchRequest = new SearchRequest(TNSname, "(|(objectClass=orclNetService)(objectClass=orclService)(objectClass=orclNetServiceAlias))", SearchScope.Base, LDAP.m_AttributesToReturn);
					flag = true;
				}
			}
			if (!flag && num < 0)
			{
				string text3 = null;
				int num2 = text.IndexOf('@');
				int num3 = text.IndexOf('.');
				if (num2 < 0 && num3 < 0)
				{
					text3 = "cn=" + TNSname + ",cn=OracleContext";
					if (!string.IsNullOrEmpty(LdapConfig.DefaultAdminContext))
					{
						text3 = text3 + "," + LdapConfig.DefaultAdminContext;
					}
					searchRequest = new SearchRequest(text3, "(|(objectClass=orclNetService)(objectClass=orclService)(objectClass=orclNetServiceAlias))", SearchScope.Base, LDAP.m_AttributesToReturn);
					flag = true;
				}
				else if (num2 > 0 || num3 > 0)
				{
					bool useDC = false;
					if (num2 > 0)
					{
						useDC = true;
						string text4 = text.Substring(0, num2);
						text = text.Substring(num2 + 1);
						num3 = text4.IndexOf('.');
						if (num3 < 0)
						{
							text3 = "cn=" + text4 + ",cn=OracleContext";
						}
						else if (num3 > 0)
						{
							string[] array = text4.Split(new char[]
							{
								'.'
							});
							text3 = "cn=" + array[0] + ",cn=OracleContext";
							for (int i = 1; i < array.Length; i++)
							{
								if (string.IsNullOrEmpty(array[i]))
								{
									text3 = null;
									break;
								}
								text3 = text3 + ",ou=" + array[i];
							}
						}
					}
					else if (num2 < 0)
					{
						string text4 = text.Substring(0, num3);
						if (!string.IsNullOrEmpty(text4))
						{
							text3 = "cn=" + text4 + ",cn=OracleContext";
						}
						text = text.Substring(num3 + 1);
					}
					if (!string.IsNullOrEmpty(text3))
					{
						if (!string.IsNullOrEmpty(text))
						{
							string text5 = null;
							try
							{
								text5 = this.Convert2rdns(text, useDC);
							}
							catch (Exception ex)
							{
								if (ProviderConfig.m_bTraceLevelNetwork)
								{
									LDAP._trace("exception occured while resolving " + TNSname + " - " + ex.ToString());
								}
								return null;
							}
							if (!string.IsNullOrEmpty(text5))
							{
								text3 = text3 + "," + text5;
							}
						}
						searchRequest = new SearchRequest(text3, "(|(objectClass=orclNetService)(objectClass=orclService)(objectClass=orclNetServiceAlias))", SearchScope.Base, LDAP.m_AttributesToReturn);
						flag = true;
					}
				}
			}
			if (flag)
			{
				LdapConnection ldapConnection = null;
				List<LdapDirectoryIdentifier> ldapDirectoryIdentifiers = LdapConfig.LdapDirectoryIdentifiers;
				searchRequest.Aliases = DereferenceAlias.Always;
				for (int j = 0; j < ldapDirectoryIdentifiers.Count; j++)
				{
					try
					{
						ldapConnection = LDAP.CreateLdapConnection(ldapDirectoryIdentifiers[j]);
						if (ProviderConfig.m_bTraceLevelNetwork)
						{
							LDAP._trace("query " + ldapDirectoryIdentifiers[j].Servers[0] + " for " + searchRequest.DistinguishedName);
						}
						SearchResponse searchResponse = (SearchResponse)ldapConnection.SendRequest(searchRequest);
						if (searchResponse.Entries.Count > 0)
						{
							SearchResultEntry searchResultEntry = searchResponse.Entries[0];
							string result = LDAP.Attribute2String(searchResultEntry.Attributes["orclNetDescString"]);
							if (ldapConnection != null)
							{
								ldapConnection.Dispose();
								ldapConnection = null;
							}
							return result;
						}
					}
					catch (Exception ex2)
					{
						if (ProviderConfig.m_bTraceLevelNetwork)
						{
							LDAP._trace("Exception occured - " + ex2.ToString());
						}
					}
					if (ldapConnection != null)
					{
						ldapConnection.Dispose();
						ldapConnection = null;
					}
				}
			}
			if (ProviderConfig.m_bTraceLevelNetwork)
			{
				LDAP._trace(TNSname + " is not valid for LDAP Naming Adapter.");
			}
			return null;
		}

		// Token: 0x06000DEF RID: 3567 RVA: 0x00093820 File Offset: 0x00091A20
		private string Convert2rdns(string alias, bool useDC)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (alias.IndexOf('.') == 0)
			{
				throw new Exception("Syntax error.");
			}
			string[] array = alias.Split(new char[]
			{
				'.'
			});
			int num = array.Length - 1;
			int num2 = array.Length - 2;
			for (int i = 0; i < array.Length; i++)
			{
				if (string.IsNullOrEmpty(array[i]))
				{
					throw new Exception("Syntax error.");
				}
				if (useDC || LdapConfig.DACwithDC)
				{
					stringBuilder.Append("dc=");
				}
				else if (LdapConfig.DACwithC && i == num)
				{
					stringBuilder.Append("c=");
				}
				else if (LdapConfig.DACwithC && i == num2 && LdapConfig.DACwithO)
				{
					stringBuilder.Append("o=");
				}
				else if (!LdapConfig.DACwithC && i == num && LdapConfig.DACwithO)
				{
					stringBuilder.Append("o=");
				}
				else
				{
					stringBuilder.Append("ou=");
				}
				stringBuilder.Append(array[i]);
				if (i < num)
				{
					stringBuilder.Append(",");
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000DF0 RID: 3568 RVA: 0x0009393C File Offset: 0x00091B3C
		public void Refresh()
		{
		}

		// Token: 0x06000DF1 RID: 3569 RVA: 0x00093940 File Offset: 0x00091B40
		private static void _trace(string msg)
		{
			StackFrame stackFrame = new StackFrame(1);
			string name = stackFrame.GetMethod().Name;
			string text = name + "(): " + msg;
			OracleInternal.Common.Trace.Write(OracleTraceLevel.Network, OracleTraceTag.Sqlnet, new string[]
			{
				text
			});
		}

		// Token: 0x04000F5C RID: 3932
		private const string m_ID = "LDAP";

		// Token: 0x04000F5D RID: 3933
		private const string m_OracleContext = "cn=OracleContext";

		// Token: 0x04000F5E RID: 3934
		private const string m_LdapFilter = "(|(objectClass=orclNetService)(objectClass=orclService)(objectClass=orclNetServiceAlias))";

		// Token: 0x04000F5F RID: 3935
		private const string m_orclNetDescStringAttr = "orclNetDescString";

		// Token: 0x04000F60 RID: 3936
		private const string m_aliasObjectNameStringAttr = "aliasedobjectname";

		// Token: 0x04000F61 RID: 3937
		private const string LDAP_CREATELDAPCONNECTION = "LDAP.CreateLdapConnection(): ";

		// Token: 0x04000F62 RID: 3938
		private const string CNEQUALS = "cn=";

		// Token: 0x04000F63 RID: 3939
		private const string DCEQUALS = "dc=";

		// Token: 0x04000F64 RID: 3940
		private const string CEQUALS = "c=";

		// Token: 0x04000F65 RID: 3941
		private const string OEQUALS = "o=";

		// Token: 0x04000F66 RID: 3942
		private const string OUEQUALS = "ou=";

		// Token: 0x04000F67 RID: 3943
		private const char EQUALCHAR = '=';

		// Token: 0x04000F68 RID: 3944
		private const char ATCHAR = '@';

		// Token: 0x04000F69 RID: 3945
		private const char DOTCHAR = '.';

		// Token: 0x04000F6A RID: 3946
		private const int m_PageSize = 999;

		// Token: 0x04000F6B RID: 3947
		private const string SYNTAXERROR = "Syntax error.";

		// Token: 0x04000F6C RID: 3948
		private const string NOLDAPSERVERCONFIGURED = "No LDAP server is configured.";

		// Token: 0x04000F6D RID: 3949
		private static string[] m_AttributesToReturn = new string[]
		{
			"orclNetDescString",
			"aliasedobjectname"
		};

		// Token: 0x04000F6E RID: 3950
		private static Hashtable m_AliasesDescriptionsMap = new Hashtable(StringComparer.OrdinalIgnoreCase);
	}
}
