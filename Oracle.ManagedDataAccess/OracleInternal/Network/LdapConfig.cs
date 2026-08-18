using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.DirectoryServices.Protocols;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using OracleInternal.Common;
using OracleInternal.Secure.Network;

namespace OracleInternal.Network
{
	// Token: 0x02000154 RID: 340
	[DnsPermission(SecurityAction.Assert, Unrestricted = true)]
	internal static class LdapConfig
	{
		// Token: 0x06000D87 RID: 3463 RVA: 0x00091718 File Offset: 0x0008F918
		static LdapConfig()
		{
			try
			{
				LdapConfig.m_bDirectoryType_accessed = false;
				LdapConfig.m_DACparsed = false;
				LdapConfig.m_ldapOraLoc = ProviderConfig.NewOraFileLoc(OraFiles.Ldap);
				ProviderConfig.NewOraFileParams(OraFiles.Ldap, LdapConfig.m_ldapOraLoc, ConfigBaseClass.m_LDAPconfigParameters);
				LdapConfig._LdapConfig();
			}
			catch (Exception)
			{
				LdapConfig.m_LdapDirectoryIdentifiers = null;
				LdapConfig.m_Credential = null;
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000D88 RID: 3464 RVA: 0x000917B4 File Offset: 0x0008F9B4
		internal static string DirectoryServers
		{
			get
			{
				return ConfigBaseClass.m_LDAPconfigParameters["directory_servers"] as string;
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000D89 RID: 3465 RVA: 0x000917CC File Offset: 0x0008F9CC
		internal static string DefaultAdminContext
		{
			get
			{
				string text = ConfigBaseClass.m_LDAPconfigParameters["default_admin_context"] as string;
				if (string.IsNullOrEmpty(text))
				{
					return null;
				}
				return text.Trim(LdapConfig.charsToTrim);
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000D8A RID: 3466 RVA: 0x00091804 File Offset: 0x0008FA04
		internal static LdapConfig.DirectoryTypes DirectoryType
		{
			get
			{
				if (!LdapConfig.m_bDirectoryType_accessed)
				{
					LdapConfig.m_bDirectoryType = ((string.IsNullOrEmpty(LdapConfig.DirectoryServers) || string.Compare(ConfigBaseClass.m_LDAPconfigParameters["directory_server_type"] as string, "oid", true) == 0) ? LdapConfig.DirectoryTypes.OID : LdapConfig.DirectoryTypes.AD);
					LdapConfig.m_bDirectoryType_accessed = true;
				}
				return LdapConfig.m_bDirectoryType;
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000D8B RID: 3467 RVA: 0x0009185C File Offset: 0x0008FA5C
		internal static List<LdapDirectoryIdentifier> LdapDirectoryIdentifiers
		{
			get
			{
				return LdapConfig.m_LdapDirectoryIdentifiers;
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000D8C RID: 3468 RVA: 0x00091864 File Offset: 0x0008FA64
		internal static AuthType Authtype
		{
			get
			{
				return LdapConfig.m_AuthType;
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000D8D RID: 3469 RVA: 0x0009186C File Offset: 0x0008FA6C
		internal static NetworkCredential Credential
		{
			get
			{
				return LdapConfig.m_Credential;
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000D8E RID: 3470 RVA: 0x00091874 File Offset: 0x0008FA74
		internal static X509CertificateCollection X509Collection
		{
			get
			{
				return LdapConfig.m_X509Collection;
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000D8F RID: 3471 RVA: 0x0009187C File Offset: 0x0008FA7C
		internal static bool useSSL
		{
			get
			{
				return LdapConfig.m_SSL;
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000D90 RID: 3472 RVA: 0x00091884 File Offset: 0x0008FA84
		internal static bool DACwithC
		{
			get
			{
				if (!LdapConfig.m_DACparsed)
				{
					LdapConfig.ParseDAC();
				}
				return LdapConfig.m_WithC;
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000D91 RID: 3473 RVA: 0x00091898 File Offset: 0x0008FA98
		internal static bool DACwithO
		{
			get
			{
				if (!LdapConfig.m_DACparsed)
				{
					LdapConfig.ParseDAC();
				}
				return LdapConfig.m_WithO;
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000D92 RID: 3474 RVA: 0x000918AC File Offset: 0x0008FAAC
		internal static bool DACwithDC
		{
			get
			{
				if (!LdapConfig.m_DACparsed)
				{
					LdapConfig.ParseDAC();
				}
				return LdapConfig.m_WithDC;
			}
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x000918C0 File Offset: 0x0008FAC0
		private static void ParseDAC()
		{
			string defaultAdminContext = LdapConfig.DefaultAdminContext;
			if (!string.IsNullOrEmpty(defaultAdminContext))
			{
				string[] array = defaultAdminContext.Split(new char[]
				{
					','
				});
				int num = array.Length - 1;
				array[num] = array[num].ToLowerInvariant();
				array[num] = array[num].Replace(" ", null);
				if (array[num].IndexOf("dc=") == 0)
				{
					LdapConfig.m_WithDC = true;
					LdapConfig.m_WithC = (LdapConfig.m_WithO = false);
				}
				else if (array[num].IndexOf("o=") == 0)
				{
					LdapConfig.m_WithO = true;
					LdapConfig.m_WithDC = (LdapConfig.m_WithC = false);
				}
				else if (array[num].IndexOf("c=") == 0)
				{
					int num2 = num - 1;
					array[num2] = array[num2].ToLowerInvariant();
					array[num2] = array[num2].Replace(" ", null);
					if (array[num2].IndexOf("o=") == 0)
					{
						LdapConfig.m_WithC = (LdapConfig.m_WithO = true);
						LdapConfig.m_WithDC = false;
					}
				}
			}
			LdapConfig.m_DACparsed = true;
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x000919B4 File Offset: 0x0008FBB4
		internal static void Refresh()
		{
			try
			{
				LdapConfig.m_bDirectoryType_accessed = false;
				LdapConfig.m_WithDC = (LdapConfig.m_WithO = (LdapConfig.m_WithC = false));
				LdapConfig.m_DACparsed = false;
				LdapConfig._LdapConfig();
			}
			catch (Exception)
			{
				LdapConfig.m_LdapDirectoryIdentifiers = null;
				LdapConfig.m_Credential = null;
			}
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x00091A08 File Offset: 0x0008FC08
		private static void _LdapConfig()
		{
			int num = 0;
			int num2 = 0;
			if (LdapConfig.m_LdapDirectoryIdentifiers == null)
			{
				LdapConfig.m_LdapDirectoryIdentifiers = new List<LdapDirectoryIdentifier>();
			}
			else
			{
				LdapConfig.m_LdapDirectoryIdentifiers.Clear();
			}
			string text = ConfigBaseClass.m_LDAPconfigParameters["directory_servers"] as string;
			if (!string.IsNullOrEmpty(text))
			{
				if (text[0] == '(' ^ text[text.Length - 1] == ')')
				{
					LdapConfig.ConfigError("DIRECTORY_SERVERS parameter has error.");
				}
				string text2 = (text[0] == '(') ? text.Substring(1, text.Length - 2) : text;
				if (text2.Length > 0)
				{
					string[] array = text2.Split(new char[]
					{
						','
					});
					for (int i = 0; i < array.Length; i++)
					{
						string[] array2 = array[i].Split(new char[]
						{
							':'
						});
						if (array2.Length > 3)
						{
							LdapConfig.ConfigError("DIRECTORY_SERVERS parameter has error.");
						}
						if (array2.Length == 3 && ((!string.IsNullOrEmpty(array2[2]) && !int.TryParse(array2[2], out num2)) || (!string.IsNullOrEmpty(array2[1]) && !int.TryParse(array2[1], out num))))
						{
							LdapConfig.ConfigError("DIRECTORY_SERVERS parameter has error.");
						}
						if (array2.Length == 2 && !int.TryParse(array2[1], out num))
						{
							LdapConfig.ConfigError("DIRECTORY_SERVERS parameter has error.");
						}
						if (i == 0)
						{
							if ((array2.Length == 3 && string.IsNullOrEmpty(array2[1])) || (SqlNetOraConfig.NamesLdapAuthenticateBind && LdapConfig.DirectoryType == LdapConfig.DirectoryTypes.OID))
							{
								LdapConfig.m_SSL = true;
							}
							else
							{
								LdapConfig.m_SSL = false;
							}
						}
						int portNumber;
						if (LdapConfig.m_SSL)
						{
							portNumber = ((array2.Length > 2) ? num2 : 636);
						}
						else
						{
							portNumber = ((array2.Length > 1) ? num : 389);
						}
						try
						{
							IPAddress[] hostAddresses = Dns.GetHostAddresses(array2[0]);
							if (hostAddresses.Length > 0)
							{
								for (int j = 0; j < hostAddresses.Length; j++)
								{
									try
									{
										LdapDirectoryIdentifier item = new LdapDirectoryIdentifier(hostAddresses[j].ToString(), portNumber, false, false);
										LdapConfig.m_LdapDirectoryIdentifiers.Add(item);
										if (ProviderConfig.m_bTraceLevelNetwork)
										{
											LdapConfig._trace("added " + array2[0] + " using " + hostAddresses[j].ToString());
										}
									}
									catch (Exception)
									{
									}
								}
							}
						}
						catch (Exception)
						{
						}
					}
				}
				else
				{
					LdapConfig.ADorOIDdiscovery();
				}
			}
			else
			{
				LdapConfig.ADorOIDdiscovery();
			}
			if (LdapConfig.m_LdapDirectoryIdentifiers.Count > 0)
			{
				LdapConfig.m_Credential = null;
				if (SqlNetOraConfig.NamesLdapAuthenticateBind)
				{
					if (ProviderConfig.m_bTraceLevelNetwork)
					{
						LdapConfig._trace("setting up authenticated bind to LDAP server");
					}
					if (LdapConfig.DirectoryType != LdapConfig.DirectoryTypes.OID)
					{
						LdapConfig.m_AuthType = AuthType.Negotiate;
						return;
					}
					string text3 = null;
					string password = null;
					if (ProviderConfig.m_bTraceLevelNetwork)
					{
						LdapConfig._trace("LDAP server is OID");
					}
					LdapConfig.m_AuthType = AuthType.External;
					Hashtable walletLocation = SqlNetOraConfig.WalletLocation;
					if (walletLocation != null)
					{
						string text4 = ((string)walletLocation["METHOD"]).ToUpperInvariant();
						if (text4 != null && text4 == "FILE")
						{
							text3 = (string)walletLocation["DIRECTORY"];
						}
						if (text4 == "FILE")
						{
							if (text3 == null)
							{
								LdapConfig.ConfigError("Wallet location is invalid.");
							}
							byte[] rawData = WalletReader.ReadWallet(text3, ref password);
							X509Certificate2 x509Certificate = new X509Certificate2(rawData, password, X509KeyStorageFlags.DefaultKeySet);
							LdapConfig.m_X509Collection = new X509CertificateCollection(new X509Certificate[]
							{
								x509Certificate
							});
						}
						else if (text4 == "MCS")
						{
							X509Store x509Store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
							x509Store.Open(OpenFlags.ReadOnly);
							LdapConfig.m_X509Collection = x509Store.Certificates;
						}
						if (LdapConfig.m_X509Collection == null || LdapConfig.m_X509Collection.Count == 0)
						{
							LdapConfig.ConfigError("No certificate found.");
							return;
						}
					}
				}
				else
				{
					LdapConfig.m_AuthType = AuthType.Anonymous;
				}
			}
		}

		// Token: 0x06000D96 RID: 3478 RVA: 0x00091DBC File Offset: 0x0008FFBC
		private static void ConfigError(string errMsg)
		{
			if (ProviderConfig.m_bTraceLevelNetwork)
			{
				StackFrame stackFrame = new StackFrame(1);
				string name = stackFrame.GetMethod().Name;
				string text = name + "(): " + errMsg;
				OracleInternal.Common.Trace.Write(OracleTraceLevel.Network, OracleTraceTag.None, new string[]
				{
					text
				});
			}
			LdapConfig.m_LdapDirectoryIdentifiers.Clear();
			throw new Exception(errMsg);
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x00091E14 File Offset: 0x00090014
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

		// Token: 0x06000D98 RID: 3480 RVA: 0x00091E54 File Offset: 0x00090054
		private static void ADorOIDdiscovery()
		{
			LdapConfig.m_LdapDirectoryIdentifiers.Clear();
			if (LdapConfig.DirectoryType == LdapConfig.DirectoryTypes.AD)
			{
				string server = null;
				LdapDirectoryIdentifier item = new LdapDirectoryIdentifier(server, 389, false, false);
				LdapConfig.m_LdapDirectoryIdentifiers.Add(item);
				return;
			}
			if (ProviderConfig.m_bTraceLevelNetwork)
			{
				LdapConfig._trace("OID discovery is not available");
			}
			throw new NotImplementedException();
		}

		// Token: 0x04000EFD RID: 3837
		internal const string DIRECTORY_SERVERS = "directory_servers";

		// Token: 0x04000EFE RID: 3838
		internal const string DIRECTORY_TYPE = "directory_server_type";

		// Token: 0x04000EFF RID: 3839
		internal const string DEFAULT_ADMIN_CONTEXT = "default_admin_context";

		// Token: 0x04000F00 RID: 3840
		internal const string CONTEXT_MAP = "contextg_map";

		// Token: 0x04000F01 RID: 3841
		private const string ACTIVE_DIRECTORY_TYPE = "ad";

		// Token: 0x04000F02 RID: 3842
		private const string OID_TYPE = "oid";

		// Token: 0x04000F03 RID: 3843
		private const int DefaultPort = 389;

		// Token: 0x04000F04 RID: 3844
		private const int DefaultSslPort = 636;

		// Token: 0x04000F05 RID: 3845
		private const string DCEQUALS = "dc=";

		// Token: 0x04000F06 RID: 3846
		private const string CEQUALS = "c=";

		// Token: 0x04000F07 RID: 3847
		private const string OEQUALS = "o=";

		// Token: 0x04000F08 RID: 3848
		private const string DIRECTORY_SERVERS_ERROR = "DIRECTORY_SERVERS parameter has error.";

		// Token: 0x04000F09 RID: 3849
		private const string WALLET_LOCATION_ERROR = "Wallet location is invalid.";

		// Token: 0x04000F0A RID: 3850
		private const string CERTIFICATE_ERROR = "No certificate found.";

		// Token: 0x04000F0B RID: 3851
		private static char[] charsToTrim = new char[]
		{
			'\\',
			'"'
		};

		// Token: 0x04000F0C RID: 3852
		private static string m_ldapOraLoc;

		// Token: 0x04000F0D RID: 3853
		private static List<LdapDirectoryIdentifier> m_LdapDirectoryIdentifiers;

		// Token: 0x04000F0E RID: 3854
		private static AuthType m_AuthType = AuthType.Anonymous;

		// Token: 0x04000F0F RID: 3855
		private static NetworkCredential m_Credential;

		// Token: 0x04000F10 RID: 3856
		private static X509CertificateCollection m_X509Collection;

		// Token: 0x04000F11 RID: 3857
		private static bool m_SSL = false;

		// Token: 0x04000F12 RID: 3858
		private static LdapConfig.DirectoryTypes m_bDirectoryType;

		// Token: 0x04000F13 RID: 3859
		private static bool m_bDirectoryType_accessed = false;

		// Token: 0x04000F14 RID: 3860
		private static bool m_WithC = false;

		// Token: 0x04000F15 RID: 3861
		private static bool m_WithO = false;

		// Token: 0x04000F16 RID: 3862
		private static bool m_WithDC = false;

		// Token: 0x04000F17 RID: 3863
		private static bool m_DACparsed = false;

		// Token: 0x02000155 RID: 341
		internal enum DirectoryTypes
		{
			// Token: 0x04000F19 RID: 3865
			AD,
			// Token: 0x04000F1A RID: 3866
			OID
		}
	}
}
