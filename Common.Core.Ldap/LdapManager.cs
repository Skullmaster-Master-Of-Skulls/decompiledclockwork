using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.DirectoryServices.Protocols;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using ClockWorkLogger;
using TechnoPro.Common.ICore.Authentication;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Authentication;

namespace TechnoPro.Common.Core.Ldap
{
	// Token: 0x02000003 RID: 3
	public class LdapManager : ILdapManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002CDC File Offset: 0x00000EDC
		// (set) Token: 0x06000034 RID: 52 RVA: 0x00002CE4 File Offset: 0x00000EE4
		private LdapConnectionInfo LdapConnection { get; set; }

		// Token: 0x06000035 RID: 53 RVA: 0x00002CED File Offset: 0x00000EED
		public LdapManager()
		{
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002CF5 File Offset: 0x00000EF5
		public LdapManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002D04 File Offset: 0x00000F04
		public LdapManager(OperationContext opContext, LdapConnectionInfo connInfo)
		{
			this.OpContext = opContext;
			this.LdapConnection = connInfo;
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002D1A File Offset: 0x00000F1A
		// (set) Token: 0x06000039 RID: 57 RVA: 0x00002D22 File Offset: 0x00000F22
		public OperationContext OpContext { get; set; }

		// Token: 0x0600003A RID: 58 RVA: 0x00002D2C File Offset: 0x00000F2C
		public Dictionary<string, string> IsAuthenticated(string username, string password, string returnAttributes)
		{
			Dictionary<string, string> result;
			try
			{
				using (LDAP ldap = new LDAP(this.LdapConnection.ServerName, this.LdapConnection.Port))
				{
					ldap.AddAttribute(this.LdapConnection.LookupAttribute.Trim().ToUpper(), username);
					if (!string.IsNullOrEmpty(this.LdapConnection.Domain))
					{
						string[] array = this.LdapConnection.Domain.Split(new char[]
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
					ldap.AuthType = AuthType.Basic;
					string a = this.LdapConnection.AuthType.Trim().ToLower();
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
					if (this.LdapConnection.SSL)
					{
						ldap.Connection.SessionOptions.SecureSocketLayer = true;
					}
					if (this.LdapConnection.TLS)
					{
						ldap.Connection.SessionOptions.StartTransportLayerSecurity(null);
					}
					if ((this.LdapConnection.SSL || this.LdapConnection.TLS) && this.LdapConnection.DontVerifyServerCertificate)
					{
						LdapSessionOptions sessionOptions = ldap.Connection.SessionOptions;
						sessionOptions.VerifyServerCertificate = (VerifyServerCertificateCallback)Delegate.Combine(sessionOptions.VerifyServerCertificate, new VerifyServerCertificateCallback((LdapConnection connection, X509Certificate cert) => true));
					}
					Exception ex;
					if (ldap.Bind(password, out ex))
					{
						try
						{
							string ldapFilter = string.Concat(new string[]
							{
								"(",
								this.LdapConnection.LookupAttribute,
								"=",
								username,
								")"
							});
							return LDAP.ConvertToDictionary(ldap.Search(ldap.AttributeString, ldapFilter, string.IsNullOrEmpty(returnAttributes) ? null : returnAttributes));
						}
						catch (Exception ex2)
						{
							CWLogger.Logger.Error("Common.Core.Ldap.LdapManager:IsAuthenticated:Error5:" + ex2.ToString());
							throw ex2;
						}
						finally
						{
							if (this.LdapConnection.TLS)
							{
								try
								{
									ldap.Connection.SessionOptions.StopTransportLayerSecurity();
								}
								catch (Exception ex3)
								{
									CWLogger.Logger.Error("Common.Core.Ldap.LdapManager.IsAuthenticated:StopTransportLayerSecurity:Error={0}", ex3.ToString());
								}
							}
						}
					}
					if (ex != null)
					{
						throw ex;
					}
					result = null;
				}
			}
			catch (Exception ex4)
			{
				CWLogger.Logger.ErrorException(string.Format("LdapManager::IsAuthenticated:: {0}", ex4.ToString()), ex4);
				throw;
			}
			return result;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003060 File Offset: 0x00001260
		public Dictionary<string, string> IsAuthenticatedDoubleBinding(string username, string password, string[] returnAttributes)
		{
			CWLogger.Logger.Trace("LdapManager:IsAuthenticatedDoubleBinding:Start:Server={0}:Port={1}:domain={2}:authtype={3}:UseSSL={4}:UseTLS={5}:ProtocolVersion={6}:LookupAttr={7}:ReturnAttrs={8}:PreDomain={9}:PreLookupAttr={10}:PreUserName={11}:PrePasswordLENGTH={12}", new object[]
			{
				this.LdapConnection.ServerName ?? "",
				this.LdapConnection.Port.ToString(),
				this.LdapConnection.Domain ?? "",
				this.LdapConnection.AuthType ?? "",
				this.LdapConnection.SSL.ToString(),
				this.LdapConnection.TLS.ToString(),
				this.LdapConnection.ProtocolVersion.ToString(),
				this.LdapConnection.LookupAttribute ?? "",
				(this.LdapConnection.ReturnAttributes == null) ? "" : string.Join(",", this.LdapConnection.ReturnAttributes),
				this.LdapConnection.PreDomain ?? "",
				this.LdapConnection.PreLookupAttribute ?? "",
				this.LdapConnection.PreUsername ?? "",
				(this.LdapConnection.PrePassword ?? "").Length.ToString()
			});
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			Dictionary<string, string> result;
			try
			{
				NetworkCredential networkCredential = new NetworkCredential(string.Concat(new string[]
				{
					this.LdapConnection.PreLookupAttribute,
					"=",
					this.LdapConnection.PreUsername,
					",",
					this.LdapConnection.PreDomain
				}), this.LdapConnection.PrePassword);
				LdapConnection ldapConnection = new LdapConnection((this.LdapConnection.Port > 0) ? new LdapDirectoryIdentifier(this.LdapConnection.ServerName, this.LdapConnection.Port, false, false) : new LdapDirectoryIdentifier(this.LdapConnection.ServerName, false, false));
				if (this.LdapConnection.ProtocolVersion > 0)
				{
					ldapConnection.SessionOptions.ProtocolVersion = this.LdapConnection.ProtocolVersion;
				}
				ldapConnection.AuthType = ((!string.IsNullOrEmpty(this.LdapConnection.AuthType) && Enum.IsDefined(typeof(AuthType), this.LdapConnection.AuthType)) ? ((AuthType)Enum.Parse(typeof(AuthType), this.LdapConnection.AuthType)) : AuthType.Basic);
				ldapConnection.Credential = networkCredential;
				ldapConnection.SessionOptions.SecureSocketLayer = this.LdapConnection.SSL;
				if ((this.LdapConnection.SSL || this.LdapConnection.TLS) && this.LdapConnection.DontVerifyServerCertificate)
				{
					LdapSessionOptions sessionOptions = ldapConnection.SessionOptions;
					sessionOptions.VerifyServerCertificate = (VerifyServerCertificateCallback)Delegate.Combine(sessionOptions.VerifyServerCertificate, new VerifyServerCertificateCallback((LdapConnection connection, X509Certificate cert) => true));
				}
				if (this.LdapConnection.SSL)
				{
					ldapConnection.SessionOptions.SecureSocketLayer = true;
				}
				if (this.LdapConnection.TLS)
				{
					ldapConnection.SessionOptions.StartTransportLayerSecurity(null);
				}
				ldapConnection.Bind(networkCredential);
				string text = string.Concat(new string[]
				{
					"(",
					this.LdapConnection.LookupAttribute,
					"=",
					username,
					")"
				});
				DirectoryRequest request = new SearchRequest(this.LdapConnection.Domain, text, SearchScope.Subtree, returnAttributes);
				SearchResponse searchResponse = (SearchResponse)ldapConnection.SendRequest(request);
				if (searchResponse.Entries.Count < 1)
				{
					string str = (returnAttributes == null) ? "NULL" : returnAttributes.Length.ToString();
					throw new Exception(string.Format("Response.Entries.Count<1: Domain: [{0}]; LdapFilter: {1}; ReturnAttributes.Count=" + str, this.LdapConnection.Domain, text));
				}
				string distinguishedName = searchResponse.Entries[0].DistinguishedName;
				ldapConnection.Bind(new NetworkCredential(distinguishedName, password));
				foreach (object obj in searchResponse.Entries)
				{
					foreach (object obj2 in ((SearchResultEntry)obj).Attributes)
					{
						DirectoryAttribute directoryAttribute = (DirectoryAttribute)((DictionaryEntry)obj2).Value;
						string name = directoryAttribute.Name;
						string value = "";
						foreach (object obj3 in directoryAttribute)
						{
							if (obj3 is byte[])
							{
								value = Encoding.ASCII.GetString((byte[])obj3);
							}
							else
							{
								value = ((obj3 == null) ? "" : obj3.ToString());
							}
						}
						if (!dictionary.ContainsKey(name))
						{
							dictionary.Add(name, value);
						}
						else if (!string.IsNullOrEmpty(value))
						{
							dictionary.Remove(name);
							dictionary.Add(name, value);
						}
					}
				}
				if (this.LdapConnection.TLS)
				{
					try
					{
						ldapConnection.SessionOptions.StopTransportLayerSecurity();
					}
					catch (Exception ex)
					{
						CWLogger.Logger.Error("Common.Core.Ldap.LdapManager:IsAuthenticatedDoubleBinding:StopTransportLayerSecurity:Error={0}", ex.ToString());
					}
				}
				result = dictionary;
			}
			catch (Exception ex2)
			{
				CWLogger.Logger.ErrorException(string.Format("LdapManager::IsAuthenticated:: {0}", ex2.ToString()), ex2);
				throw;
			}
			return result;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00003680 File Offset: 0x00001880
		public bool IsAuthenticated(string username, string password, out Exception ex)
		{
			ex = null;
			bool result;
			try
			{
				result = ((this.LdapConnection.IsDoubleBinding ? this.IsAuthenticatedDoubleBinding(username, password, new string[0]) : this.IsAuthenticated(username, password, string.Empty)) != null);
			}
			catch (Exception ex2)
			{
				ex = ex2;
				result = false;
			}
			return result;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000036DC File Offset: 0x000018DC
		public LdapAuthenticationResult LdapLogin(LdapConnectionInfo ConnectionInfo, string UserName, string Password)
		{
			this.LdapConnection = ConnectionInfo;
			LdapAuthenticationResult result;
			try
			{
				if (ConnectionInfo.IsActiveDirectory)
				{
					Exception ex;
					StringDictionary stringDictionary = ConnectionInfo.UseLookupAttributeForActiveDirectory ? LDAP.IsAuthenticatedActiveDirectoryV2(ConnectionInfo.ServerName, ConnectionInfo.LookupAttribute, ConnectionInfo.ReturnAttributes, UserName, Password, out ex) : LDAP.IsAuthenticatedActiveDirectory(string.IsNullOrEmpty(ConnectionInfo.ServerName) ? ConnectionInfo.Domain : ConnectionInfo.ServerName, UserName, Password, out ex);
					if (ex != null)
					{
						throw ex;
					}
					Dictionary<string, string> dictionary = new Dictionary<string, string>();
					if (stringDictionary == null)
					{
						result = new LdapAuthenticationResult
						{
							IsAuthenticated = true,
							ReturnAttributes = dictionary
						};
					}
					else
					{
						foreach (object obj in stringDictionary)
						{
							DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
							dictionary.Add(dictionaryEntry.Key.ToString(), (dictionaryEntry.Value == null) ? null : dictionaryEntry.Value.ToString());
						}
						result = new LdapAuthenticationResult
						{
							IsAuthenticated = true,
							ReturnAttributes = dictionary
						};
					}
				}
				else if (ConnectionInfo.IsDoubleBinding)
				{
					Dictionary<string, string> dictionary2 = this.IsAuthenticatedDoubleBinding(UserName, Password, ConnectionInfo.ReturnAttributes);
					result = new LdapAuthenticationResult
					{
						IsAuthenticated = (dictionary2 != null),
						ReturnAttributes = dictionary2
					};
				}
				else
				{
					Dictionary<string, string> dictionary3 = this.IsAuthenticated(UserName, Password, string.Join(",", ConnectionInfo.ReturnAttributes.ToArray<string>()));
					result = new LdapAuthenticationResult
					{
						IsAuthenticated = (dictionary3 != null),
						ReturnAttributes = dictionary3
					};
				}
			}
			catch (Exception ex2)
			{
				CWLogger.Logger.Error("Common.Core.Ldap.LdapManager:LdapLogin:connectionInfo={0}:username={1}:err={2}", this.ConnectionInfoToString(ConnectionInfo), UserName ?? "NULL", ex2.ToString());
				result = new LdapAuthenticationResult
				{
					IsAuthenticated = false,
					ReturnAttributes = null,
					ErrorMessage = ex2.Message
				};
			}
			return result;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000038DC File Offset: 0x00001ADC
		private string ConnectionInfoToString(LdapConnectionInfo ci)
		{
			if (ci == null)
			{
				return "NULL";
			}
			PropertyInfo[] properties = typeof(LdapConnectionInfo).GetProperties();
			return string.Join("\r\n", (from g in properties
			where g.PropertyType == typeof(string)
			select g.Name + "=" + (((string)g.GetValue(ci, null)) ?? "")).ToArray<string>());
		}
	}
}
