using System;
using System.Collections;
using System.Linq;
using OracleInternal.Network;

namespace OracleInternal.Common
{
	// Token: 0x020000BE RID: 190
	internal class SqlNetOraConfig
	{
		// Token: 0x170001BD RID: 445
		internal string this[string key]
		{
			get
			{
				return ConfigBaseClass.m_configParameters[key] as string;
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000743 RID: 1859 RVA: 0x000444A0 File Offset: 0x000426A0
		internal static bool SSLClientAuthentication
		{
			get
			{
				if (!SqlNetOraConfig.m_bSSLClientAuthentication_accessed)
				{
					string text = ConfigBaseClass.m_configParameters["ssl_client_authentication"] as string;
					if (!string.IsNullOrEmpty(text))
					{
						SqlNetOraConfig.m_bSSLClientAuthentication = SqlNetOraConfig.ParamValueIsTrue(text);
					}
					SqlNetOraConfig.m_bSSLClientAuthentication_accessed = true;
				}
				return SqlNetOraConfig.m_bSSLClientAuthentication;
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000744 RID: 1860 RVA: 0x000444E8 File Offset: 0x000426E8
		internal static bool SSLServerDNMatch
		{
			get
			{
				if (!SqlNetOraConfig.m_bSSLServerDNMatch_accessed)
				{
					string text = ConfigBaseClass.m_configParameters["ssl_server_dn_match"] as string;
					if (!string.IsNullOrEmpty(text))
					{
						SqlNetOraConfig.m_bSSLServerDNMatch = SqlNetOraConfig.ParamValueIsTrue(text);
					}
					SqlNetOraConfig.m_bSSLServerDNMatch_accessed = true;
				}
				return SqlNetOraConfig.m_bSSLServerDNMatch;
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000745 RID: 1861 RVA: 0x00044530 File Offset: 0x00042730
		internal static string SSLVersion
		{
			get
			{
				if (!SqlNetOraConfig.m_bSSLVersion_accessed)
				{
					string text = ConfigBaseClass.m_configParameters["ssl_version"] as string;
					if (!string.IsNullOrEmpty(text))
					{
						SqlNetOraConfig.m_sslVersion = text;
					}
					SqlNetOraConfig.m_bSSLVersion_accessed = true;
				}
				return SqlNetOraConfig.m_sslVersion;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000746 RID: 1862 RVA: 0x00044574 File Offset: 0x00042774
		internal static string[] SSLCipherSuites
		{
			get
			{
				if (!SqlNetOraConfig.m_bSSLCipherSuites_accessed)
				{
					string text = ConfigBaseClass.m_configParameters["ssl_cipher_suites"] as string;
					if (!string.IsNullOrEmpty(text) && text[0] == '(' && text[text.Length - 1] == ')')
					{
						string text2 = text.Substring(1, text.Length - 2);
						SqlNetOraConfig.m_sslCipherSuites = (from s in text2.Split(new char[]
						{
							','
						})
						select s.Trim() into s
						where s != string.Empty
						select s).ToArray<string>();
					}
					SqlNetOraConfig.m_bSSLCipherSuites_accessed = true;
				}
				return SqlNetOraConfig.m_sslCipherSuites;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000747 RID: 1863 RVA: 0x00044648 File Offset: 0x00042848
		internal static string[] SqlNetAuthenticationServices
		{
			get
			{
				if (!SqlNetOraConfig.m_bSqlNetAuthenticationServices_accessed)
				{
					string text = ConfigBaseClass.m_configParameters["sqlnet.authentication_services"] as string;
					if (!string.IsNullOrEmpty(text) && text[0] == '(' && text[text.Length - 1] == ')')
					{
						string text2 = text.Substring(1, text.Length - 2);
						SqlNetOraConfig.m_sqlNetAuthenticationServices = (from s in text2.Split(new char[]
						{
							','
						})
						select s.Trim() into s
						where s != string.Empty
						select s).ToArray<string>();
					}
					SqlNetOraConfig.m_bSqlNetAuthenticationServices_accessed = true;
				}
				return SqlNetOraConfig.m_sqlNetAuthenticationServices;
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000748 RID: 1864 RVA: 0x0004471C File Offset: 0x0004291C
		internal static string[] SqlNetEncryptionTypesClient
		{
			get
			{
				if (!SqlNetOraConfig.m_bSqlNetEncryptionTypesClient_accessed)
				{
					string text = ConfigBaseClass.m_configParameters["sqlnet.encryption_types_client"] as string;
					if (!string.IsNullOrEmpty(text) && text[0] == '(' && text[text.Length - 1] == ')')
					{
						string text2 = text.Substring(1, text.Length - 2);
						SqlNetOraConfig.m_sqlNetEncryptionTypesClient = (from s in text2.Split(new char[]
						{
							','
						})
						select s.Trim() into s
						where s != string.Empty
						select s).ToArray<string>();
					}
					SqlNetOraConfig.m_bSqlNetEncryptionTypesClient_accessed = true;
				}
				return SqlNetOraConfig.m_sqlNetEncryptionTypesClient;
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000749 RID: 1865 RVA: 0x000447F0 File Offset: 0x000429F0
		internal static string SqlNetEncryptionClient
		{
			get
			{
				if (!SqlNetOraConfig.m_bSqlNetEncryptionClient_accessed)
				{
					string text = ConfigBaseClass.m_configParameters["sqlnet.encryption_client"] as string;
					if (!string.IsNullOrEmpty(text))
					{
						SqlNetOraConfig.m_sqlNetEncryptionClient = text;
						SqlNetOraConfig.m_bSqlNetEncryptionClient_accessed = true;
					}
				}
				return SqlNetOraConfig.m_sqlNetEncryptionClient;
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x0600074A RID: 1866 RVA: 0x00044834 File Offset: 0x00042A34
		internal static string[] SqlNetCryptoChecksumTypesClient
		{
			get
			{
				if (!SqlNetOraConfig.m_bSqlNetCryptoChecksumTypesClient_accessed)
				{
					string text = ConfigBaseClass.m_configParameters["sqlnet.crypto_checksum_types_client"] as string;
					if (!string.IsNullOrEmpty(text) && text[0] == '(' && text[text.Length - 1] == ')')
					{
						string text2 = text.Substring(1, text.Length - 2);
						SqlNetOraConfig.m_sqlNetCryptoChecksumTypesClient = (from s in text2.Split(new char[]
						{
							','
						})
						select s.Trim() into s
						where s != string.Empty
						select s).ToArray<string>();
					}
					SqlNetOraConfig.m_bSqlNetCryptoChecksumTypesClient_accessed = true;
				}
				return SqlNetOraConfig.m_sqlNetCryptoChecksumTypesClient;
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x0600074B RID: 1867 RVA: 0x00044908 File Offset: 0x00042B08
		internal static string SqlNetCryptoChecksumClient
		{
			get
			{
				if (!SqlNetOraConfig.m_bSqlNetCryptoChecksumClient_accessed)
				{
					string text = ConfigBaseClass.m_configParameters["sqlnet.crypto_checksum_client"] as string;
					if (!string.IsNullOrEmpty(text))
					{
						SqlNetOraConfig.m_sqlNetCryptoChecksumClient = text;
						SqlNetOraConfig.m_bSqlNetCryptoChecksumClient_accessed = true;
					}
				}
				return SqlNetOraConfig.m_sqlNetCryptoChecksumClient;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x0600074C RID: 1868 RVA: 0x0004494C File Offset: 0x00042B4C
		internal static string[] NamesDirectoryPath
		{
			get
			{
				if (!SqlNetOraConfig.m_bNamesDirectoryPath_accessed)
				{
					string text = ConfigBaseClass.m_configParameters["names.directory_path"] as string;
					if (!string.IsNullOrEmpty(text) && text[0] == '(' && text[text.Length - 1] == ')')
					{
						string text2 = text.Substring(1, text.Length - 2);
						SqlNetOraConfig.m_namesDirectoryPath = (from s in text2.Split(new char[]
						{
							','
						})
						select s.Trim() into s
						where s != string.Empty
						select s).ToArray<string>();
					}
					SqlNetOraConfig.m_bNamesDirectoryPath_accessed = true;
				}
				return SqlNetOraConfig.m_namesDirectoryPath;
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x0600074D RID: 1869 RVA: 0x00044A20 File Offset: 0x00042C20
		internal static Hashtable WalletLocation
		{
			get
			{
				if (!SqlNetOraConfig.m_walletLocationParameters_accessed)
				{
					string text = ConfigBaseClass.m_configParameters["wallet_location"] as string;
					if (!string.IsNullOrEmpty(text))
					{
						text = text.ToUpperInvariant();
						SqlNetOraConfig.ParseWalletLocation(text);
					}
					SqlNetOraConfig.m_walletLocationParameters_accessed = true;
				}
				return SqlNetOraConfig.m_walletLocationParameters;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x0600074E RID: 1870 RVA: 0x00044A6C File Offset: 0x00042C6C
		internal static bool DisableOOB
		{
			get
			{
				if (!SqlNetOraConfig.m_bDisableOOB_accessed)
				{
					string text = ConfigBaseClass.m_configParameters["disable_oob"] as string;
					if (!string.IsNullOrEmpty(text))
					{
						SqlNetOraConfig.m_bDisableOOB = SqlNetOraConfig.ParamValueIsTrue(text);
					}
					SqlNetOraConfig.m_bDisableOOB_accessed = true;
				}
				return SqlNetOraConfig.m_bDisableOOB;
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x0600074F RID: 1871 RVA: 0x00044AB4 File Offset: 0x00042CB4
		internal static bool NoDelay
		{
			get
			{
				if (!SqlNetOraConfig.m_bNoDelay_accessed)
				{
					string text = (string)ConfigBaseClass.m_configParameters["nodelay"];
					if (!string.IsNullOrEmpty(text))
					{
						text = text.ToUpperInvariant();
						SqlNetOraConfig.m_bNoDelay = SqlNetOraConfig.ParamValueIsTrue(text);
					}
					SqlNetOraConfig.m_bNoDelay_accessed = true;
				}
				return SqlNetOraConfig.m_bNoDelay;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000750 RID: 1872 RVA: 0x00044B04 File Offset: 0x00042D04
		internal static string NamesDefaultDomain
		{
			get
			{
				if (!SqlNetOraConfig.m_bNamesDefaultDomain_accessed)
				{
					string text = ConfigBaseClass.m_configParameters["names.default_domain"] as string;
					if (!string.IsNullOrEmpty(text))
					{
						SqlNetOraConfig.m_namesDefaultDomain = text;
					}
					SqlNetOraConfig.m_bNamesDefaultDomain_accessed = true;
				}
				return SqlNetOraConfig.m_namesDefaultDomain;
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000751 RID: 1873 RVA: 0x00044B48 File Offset: 0x00042D48
		internal static string TraceLevelClient
		{
			get
			{
				if (!SqlNetOraConfig.m_bTraceLevelClient_accessed)
				{
					SqlNetOraConfig.m_traceLevelClient = (ConfigBaseClass.m_configParameters["trace_level_client"] as string);
					SqlNetOraConfig.m_bTraceLevelClient_accessed = true;
				}
				return SqlNetOraConfig.m_traceLevelClient;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000752 RID: 1874 RVA: 0x00044B78 File Offset: 0x00042D78
		internal static bool TraceUniqueClient
		{
			get
			{
				if (!SqlNetOraConfig.m_bTraceUniqueClient_accessed)
				{
					string text = ConfigBaseClass.m_configParameters["trace_unique_client"] as string;
					if (!string.IsNullOrEmpty(text))
					{
						SqlNetOraConfig.m_bTraceUniqueClient = SqlNetOraConfig.ParamValueIsTrue(text);
					}
					SqlNetOraConfig.m_bTraceUniqueClient_accessed = true;
				}
				return SqlNetOraConfig.m_bTraceUniqueClient;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000753 RID: 1875 RVA: 0x00044BC0 File Offset: 0x00042DC0
		internal static bool TCPNoDelay
		{
			get
			{
				if (!SqlNetOraConfig.m_bTCPNoDelay_accessed)
				{
					string text = ConfigBaseClass.m_configParameters["tcp.nodelay"] as string;
					if (!string.IsNullOrEmpty(text))
					{
						SqlNetOraConfig.m_bTCPNoDelay = SqlNetOraConfig.ParamValueIsTrue(text);
					}
					SqlNetOraConfig.m_bTCPNoDelay_accessed = true;
				}
				return SqlNetOraConfig.m_bTCPNoDelay;
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000754 RID: 1876 RVA: 0x00044C08 File Offset: 0x00042E08
		internal static bool WalletOverride
		{
			get
			{
				if (!SqlNetOraConfig.m_bWalletOverride_accessed)
				{
					string text = ConfigBaseClass.m_configParameters["sqlnet.wallet_override"] as string;
					if (!string.IsNullOrEmpty(text))
					{
						SqlNetOraConfig.m_bWalletOverride = SqlNetOraConfig.ParamValueIsTrue(text);
					}
					SqlNetOraConfig.m_bWalletOverride_accessed = true;
				}
				return SqlNetOraConfig.m_bWalletOverride;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000755 RID: 1877 RVA: 0x00044C50 File Offset: 0x00042E50
		internal static int SendBufSize
		{
			get
			{
				try
				{
					if (!SqlNetOraConfig.m_SBS_accessed)
					{
						string text = ConfigBaseClass.m_configParameters["send_buf_size"] as string;
						if (!string.IsNullOrEmpty(text))
						{
							SqlNetOraConfig.m_SBS = int.Parse(text);
						}
						SqlNetOraConfig.m_SBS_accessed = true;
					}
				}
				catch (Exception)
				{
				}
				return SqlNetOraConfig.m_SBS;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000756 RID: 1878 RVA: 0x00044CAC File Offset: 0x00042EAC
		internal static int RecvBufSize
		{
			get
			{
				try
				{
					if (!SqlNetOraConfig.m_RBS_accessed)
					{
						string text = ConfigBaseClass.m_configParameters["recv_buf_size"] as string;
						if (!string.IsNullOrEmpty(text))
						{
							SqlNetOraConfig.m_RBS = int.Parse(text);
						}
						SqlNetOraConfig.m_RBS_accessed = true;
					}
				}
				catch (Exception)
				{
				}
				return SqlNetOraConfig.m_RBS;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000757 RID: 1879 RVA: 0x00044D08 File Offset: 0x00042F08
		internal static int TCPCTimeOut
		{
			get
			{
				if (!SqlNetOraConfig.m_TCPCTimeOut_accessed)
				{
					string text = ConfigBaseClass.m_configParameters["tcp.connect_timeout"] as string;
					if (!string.IsNullOrEmpty(text))
					{
						SqlNetOraConfig.m_TCPCTimeOut = int.Parse(text);
					}
					SqlNetOraConfig.m_TCPCTimeOut_accessed = true;
				}
				return SqlNetOraConfig.m_TCPCTimeOut;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000758 RID: 1880 RVA: 0x00044D50 File Offset: 0x00042F50
		internal static bool HostnameDefaultServiceIsHost
		{
			get
			{
				if (!SqlNetOraConfig.m_DefaultServiceIsHost_accessed)
				{
					string text = ConfigBaseClass.m_configParameters["hostname.default_service_is_host"] as string;
					if (!string.IsNullOrEmpty(text))
					{
						text = text.ToUpper();
						SqlNetOraConfig.m_DefaultServiceIsHost = (text == "1" || SqlNetOraConfig.ParamValueIsTrue(text));
					}
					SqlNetOraConfig.m_DefaultServiceIsHost_accessed = true;
				}
				return SqlNetOraConfig.m_DefaultServiceIsHost;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000759 RID: 1881 RVA: 0x00044DB0 File Offset: 0x00042FB0
		internal static bool NamesLdapAuthenticateBind
		{
			get
			{
				if (!SqlNetOraConfig.m_bNamesLdapAuthenticateBind_accessed)
				{
					string text = ConfigBaseClass.m_configParameters["names.ldap_authenticate_bind"] as string;
					if (!string.IsNullOrEmpty(text))
					{
						SqlNetOraConfig.m_bNamesLdapAuthenticateBind = SqlNetOraConfig.ParamValueIsTrue(text);
					}
					SqlNetOraConfig.m_bNamesLdapAuthenticateBind_accessed = true;
				}
				return SqlNetOraConfig.m_bNamesLdapAuthenticateBind;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x0600075A RID: 1882 RVA: 0x00044DF8 File Offset: 0x00042FF8
		internal static int LDAPCTimeout
		{
			get
			{
				if (!SqlNetOraConfig.m_LDAPCTimeOut_accessed)
				{
					string text = ConfigBaseClass.m_configParameters["names.ldap_conn_timeout"] as string;
					if (!string.IsNullOrEmpty(text))
					{
						SqlNetOraConfig.m_LDAPCTimeOut = int.Parse(text);
					}
					SqlNetOraConfig.m_LDAPCTimeOut_accessed = true;
				}
				return SqlNetOraConfig.m_LDAPCTimeOut;
			}
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x00044E40 File Offset: 0x00043040
		internal static void ParseWalletLocation(string wallet_location)
		{
			NVPair nvpair = NVFactory.CreateNVPair(wallet_location);
			if (nvpair != null)
			{
				NVPair nvpair2 = NVNavigator.FindNVPairRecurse(nvpair, "SOURCE");
				if (nvpair2 != null)
				{
					NVPair nvpair3 = NVNavigator.FindNVPair(nvpair2, "METHOD");
					if (nvpair3 != null)
					{
						SqlNetOraConfig.m_walletLocationParameters = new Hashtable(5);
						SqlNetOraConfig.m_walletLocationParameters[nvpair3.Name] = nvpair3.Atom;
						NVPair nvpair4 = NVNavigator.FindNVPair(nvpair2, "METHOD_DATA");
						if (nvpair4 != null)
						{
							SqlNetOraConfig.m_walletLocationParameters[nvpair3.Name] = nvpair3.Atom.ToUpperInvariant();
							string a;
							if ((a = nvpair3.Atom.ToUpperInvariant()) != null)
							{
								if (!(a == "FILE"))
								{
									if (!(a == "REG"))
									{
										if (!(a == "ENTR"))
										{
											if (!(a == "MCS"))
											{
												return;
											}
										}
										else
										{
											NVPair nvpair5 = NVNavigator.FindNVPair(nvpair4, " ");
											if (nvpair5 != null)
											{
												SqlNetOraConfig.m_walletLocationParameters[nvpair5.Name] = nvpair5.Atom;
											}
											NVPair nvpair6 = NVNavigator.FindNVPair(nvpair4, "INIFILE");
											if (nvpair6 != null)
											{
												SqlNetOraConfig.m_walletLocationParameters[nvpair6.Name] = nvpair6.Atom;
											}
										}
									}
									else
									{
										NVPair nvpair7 = NVNavigator.FindNVPair(nvpair4, "KEY");
										if (nvpair7 != null)
										{
											SqlNetOraConfig.m_walletLocationParameters[nvpair7.Name] = nvpair7.Atom;
											return;
										}
									}
								}
								else
								{
									NVPair nvpair8 = NVNavigator.FindNVPair(nvpair4, "DIRECTORY");
									if (nvpair8 != null)
									{
										SqlNetOraConfig.m_walletLocationParameters[nvpair8.Name] = nvpair8.Atom;
										return;
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x00044FC4 File Offset: 0x000431C4
		private static bool ParamValueIsTrue(string paramValue)
		{
			return string.Compare(paramValue, "1", StringComparison.InvariantCultureIgnoreCase) == 0 || string.Compare(paramValue, "on", StringComparison.InvariantCultureIgnoreCase) == 0 || string.Compare(paramValue, "yes", StringComparison.InvariantCultureIgnoreCase) == 0 || string.Compare(paramValue, "true", StringComparison.InvariantCultureIgnoreCase) == 0;
		}

		// Token: 0x040009C6 RID: 2502
		private const string SSL_CLIENT_AUTHENTICATION = "ssl_client_authentication";

		// Token: 0x040009C7 RID: 2503
		private const string SSL_SERVER_DN_MATCH = "ssl_server_dn_match";

		// Token: 0x040009C8 RID: 2504
		private const string SSL_VERSION = "ssl_version";

		// Token: 0x040009C9 RID: 2505
		private const string SSL_CIPHER_SUITES = "ssl_cipher_suites";

		// Token: 0x040009CA RID: 2506
		private const string SQLNET_AUTHENTICATION_SERVICES = "sqlnet.authentication_services";

		// Token: 0x040009CB RID: 2507
		private const string SQLNET_ENCRYPTION_TYPES_CLIENT = "sqlnet.encryption_types_client";

		// Token: 0x040009CC RID: 2508
		private const string SQLNET_ENCRYPTION_CLIENT = "sqlnet.encryption_client";

		// Token: 0x040009CD RID: 2509
		private const string SQLNET_CRYPTO_CHECKSUM_TYPES_CLIENT = "sqlnet.crypto_checksum_types_client";

		// Token: 0x040009CE RID: 2510
		private const string SQLNET_CRYPTO_CHECKSUM_CLIENT = "sqlnet.crypto_checksum_client";

		// Token: 0x040009CF RID: 2511
		private const string NAMES_DIRECTORY_PATH = "names.directory_path";

		// Token: 0x040009D0 RID: 2512
		private const string WALLET_LOCATION = "wallet_location";

		// Token: 0x040009D1 RID: 2513
		private const string DISABLE_OOB = "disable_oob";

		// Token: 0x040009D2 RID: 2514
		private const string NODELAY = "nodelay";

		// Token: 0x040009D3 RID: 2515
		private const string SBS = "send_buf_size";

		// Token: 0x040009D4 RID: 2516
		private const string RBS = "recv_buf_size";

		// Token: 0x040009D5 RID: 2517
		private const string NAMES_DEFAULT_DOMAIN = "names.default_domain";

		// Token: 0x040009D6 RID: 2518
		private const string TRACE_LEVEL_CLIENT = "trace_level_client";

		// Token: 0x040009D7 RID: 2519
		private const string TRACE_UNIQUE_CLIENT = "trace_unique_client";

		// Token: 0x040009D8 RID: 2520
		private const string TCP_NODELAY = "tcp.nodelay";

		// Token: 0x040009D9 RID: 2521
		private const string TCP_CONNECT_TIMEOUT = "tcp.connect_timeout";

		// Token: 0x040009DA RID: 2522
		private const string DEFAULT_SERVICE_IS_HOST = "hostname.default_service_is_host";

		// Token: 0x040009DB RID: 2523
		private const string NAMES_LDAP_AUTHENTICATE_BIND = "names.ldap_authenticate_bind";

		// Token: 0x040009DC RID: 2524
		private const string NAMES_LDAP_CONN_TIMEOUT = "names.ldap_conn_timeout";

		// Token: 0x040009DD RID: 2525
		internal const string SQLNET_KERBEROS5_CONF = "sqlnet.kerberos5_conf";

		// Token: 0x040009DE RID: 2526
		internal const string SQLNET_KERBEROS5_CC_NAME = "sqlnet.kerberos5_cc_name";

		// Token: 0x040009DF RID: 2527
		private const string SQLNET_WALLET_OVERRIDE = "sqlnet.wallet_override";

		// Token: 0x040009E0 RID: 2528
		private static bool m_bDisableOOB = false;

		// Token: 0x040009E1 RID: 2529
		private static bool m_bDisableOOB_accessed = false;

		// Token: 0x040009E2 RID: 2530
		private static bool m_bNoDelay = true;

		// Token: 0x040009E3 RID: 2531
		private static bool m_bNoDelay_accessed = false;

		// Token: 0x040009E4 RID: 2532
		private static int m_SBS = 0;

		// Token: 0x040009E5 RID: 2533
		private static bool m_SBS_accessed = false;

		// Token: 0x040009E6 RID: 2534
		private static int m_RBS = 0;

		// Token: 0x040009E7 RID: 2535
		private static bool m_RBS_accessed = false;

		// Token: 0x040009E8 RID: 2536
		private static string m_sslVersion = "0";

		// Token: 0x040009E9 RID: 2537
		private static bool m_bSSLVersion_accessed = false;

		// Token: 0x040009EA RID: 2538
		private static bool m_bSSLServerDNMatch = false;

		// Token: 0x040009EB RID: 2539
		private static bool m_bSSLServerDNMatch_accessed = false;

		// Token: 0x040009EC RID: 2540
		private static bool m_bSSLClientAuthentication = true;

		// Token: 0x040009ED RID: 2541
		private static bool m_bSSLClientAuthentication_accessed = true;

		// Token: 0x040009EE RID: 2542
		private static bool m_bTCPNoDelay = true;

		// Token: 0x040009EF RID: 2543
		private static bool m_bTCPNoDelay_accessed = false;

		// Token: 0x040009F0 RID: 2544
		private static int m_TCPCTimeOut = -1;

		// Token: 0x040009F1 RID: 2545
		private static bool m_TCPCTimeOut_accessed = false;

		// Token: 0x040009F2 RID: 2546
		private static bool m_DefaultServiceIsHost = false;

		// Token: 0x040009F3 RID: 2547
		private static bool m_DefaultServiceIsHost_accessed = false;

		// Token: 0x040009F4 RID: 2548
		private static string[] m_sslCipherSuites = new string[]
		{
			"SSL_RSA_WITH_3DES_EDE_CBC_SHA",
			"SSL_RSA_WITH_RC4_128_SHA",
			"SSL_RSA_WITH_RC4_128_MD5",
			"SSL_RSA_WITH_DES_CBC_SHA",
			"SSL_DH_anon_WITH_3DES_EDE_CBC_SHA",
			"SSL_DH_anon_WITH_RC4_128_MD5",
			"SSL_DH_anon_WITH_DES_CBC_SHA",
			"SSL_RSA_EXPORT_WITH_RC4_40_MD5",
			"SSL_RSA_EXPORT_WITH_DES40_CBC_SHA",
			"SSL_DH_anon_EXPORT_WITH_RC4_40_MD5",
			"SSL_DH_anon_EXPORT_WITH_DES40_CBC_SHA"
		};

		// Token: 0x040009F5 RID: 2549
		private static bool m_bSSLCipherSuites_accessed = false;

		// Token: 0x040009F6 RID: 2550
		private static string[] m_sqlNetAuthenticationServices = null;

		// Token: 0x040009F7 RID: 2551
		private static bool m_bSqlNetAuthenticationServices_accessed = false;

		// Token: 0x040009F8 RID: 2552
		private static string[] m_sqlNetEncryptionTypesClient = null;

		// Token: 0x040009F9 RID: 2553
		private static bool m_bSqlNetEncryptionTypesClient_accessed = false;

		// Token: 0x040009FA RID: 2554
		private static string m_sqlNetEncryptionClient = null;

		// Token: 0x040009FB RID: 2555
		private static bool m_bSqlNetEncryptionClient_accessed = false;

		// Token: 0x040009FC RID: 2556
		private static string[] m_sqlNetCryptoChecksumTypesClient = null;

		// Token: 0x040009FD RID: 2557
		private static bool m_bSqlNetCryptoChecksumTypesClient_accessed = false;

		// Token: 0x040009FE RID: 2558
		private static string m_sqlNetCryptoChecksumClient = null;

		// Token: 0x040009FF RID: 2559
		private static bool m_bSqlNetCryptoChecksumClient_accessed = false;

		// Token: 0x04000A00 RID: 2560
		private static string m_namesDefaultDomain = null;

		// Token: 0x04000A01 RID: 2561
		private static bool m_bNamesDefaultDomain_accessed = false;

		// Token: 0x04000A02 RID: 2562
		private static string[] m_namesDirectoryPath = new string[]
		{
			"tnsnames",
			"hostname"
		};

		// Token: 0x04000A03 RID: 2563
		private static bool m_bNamesDirectoryPath_accessed = false;

		// Token: 0x04000A04 RID: 2564
		private static bool m_bWalletOverride = false;

		// Token: 0x04000A05 RID: 2565
		private static bool m_bWalletOverride_accessed = false;

		// Token: 0x04000A06 RID: 2566
		private static Hashtable m_walletLocationParameters = null;

		// Token: 0x04000A07 RID: 2567
		private static bool m_walletLocationParameters_accessed = false;

		// Token: 0x04000A08 RID: 2568
		private static string m_traceLevelClient = "off";

		// Token: 0x04000A09 RID: 2569
		private static bool m_bTraceLevelClient_accessed = false;

		// Token: 0x04000A0A RID: 2570
		private static bool m_bTraceUniqueClient = true;

		// Token: 0x04000A0B RID: 2571
		private static bool m_bTraceUniqueClient_accessed = false;

		// Token: 0x04000A0C RID: 2572
		private static bool m_bNamesLdapAuthenticateBind = false;

		// Token: 0x04000A0D RID: 2573
		private static bool m_bNamesLdapAuthenticateBind_accessed = false;

		// Token: 0x04000A0E RID: 2574
		private static int m_LDAPCTimeOut = 15;

		// Token: 0x04000A0F RID: 2575
		private static bool m_LDAPCTimeOut_accessed = false;

		// Token: 0x04000A10 RID: 2576
		private static string m_traceDirectoryClient = string.Empty;
	}
}
