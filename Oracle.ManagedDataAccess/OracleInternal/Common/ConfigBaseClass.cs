using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Security.Permissions;
using System.Text;
using System.Xml;
using Microsoft.Win32;
using Oracle.ManagedDataAccess.Client;

namespace OracleInternal.Common
{
	// Token: 0x0200003A RID: 58
	[EnvironmentPermission(SecurityAction.Assert, Unrestricted = true)]
	internal class ConfigBaseClass
	{
		// Token: 0x060002BA RID: 698 RVA: 0x0000F8D4 File Offset: 0x0000DAD4
		internal virtual void ValidateEdmMapping()
		{
			int num = -1;
			for (int i = 0; i < ConfigBaseClass.s_edmTypes.Length; i++)
			{
				int num2 = ConfigBaseClass.GetMaxPrecision(ConfigBaseClass.s_edmTypes[i], false);
				if (num2 < 0)
				{
					num2 = ConfigBaseClass.s_maxPrecision[i];
				}
				if (num2 > 0)
				{
					if (num > num2)
					{
						throw new ConfigurationErrorsException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.ODP_INVALID_VALUE, new string[]
						{
							ConfigBaseClass.s_edmTypes[i]
						}));
					}
					num = num2;
				}
			}
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000F93C File Offset: 0x0000DB3C
		internal virtual void setudtmapping(out Hashtable s_mapUdtNameToMappingObj)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000F944 File Offset: 0x0000DB44
		[FileIOPermission(SecurityAction.Assert, Unrestricted = true)]
		[EnvironmentPermission(SecurityAction.Assert, Unrestricted = true)]
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		static ConfigBaseClass()
		{
			try
			{
				bool flag = false;
				Assembly assembly = null;
				try
				{
					assembly = Assembly.GetEntryAssembly();
					if (assembly == null)
					{
						flag = true;
					}
				}
				catch
				{
				}
				if (flag)
				{
					try
					{
						string assemblyString = string.Format("System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", new object[0]);
						Assembly assembly2 = Assembly.Load(assemblyString);
						Type type = assembly2.GetType("System.Web.HttpRuntime");
						PropertyInfo property = type.GetProperty("AppDomainAppPath");
						ConfigBaseClass.s_appDir = Path.GetDirectoryName((string)property.GetValue(null, null));
					}
					catch
					{
						flag = false;
					}
				}
				if (!flag)
				{
					if (assembly != null)
					{
						try
						{
							ConfigBaseClass.s_appDir = Path.GetDirectoryName(assembly.Location);
						}
						catch
						{
						}
					}
					if (ConfigBaseClass.s_appDir == null)
					{
						try
						{
							ConfigBaseClass.s_appDir = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
						}
						catch
						{
						}
					}
				}
			}
			catch (Exception arg)
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[]
					{
						"ConfigBaseClass() : " + arg
					});
				}
			}
			finally
			{
				if (ConfigBaseClass.s_appDir == null)
				{
					ConfigBaseClass.s_appDir = string.Empty;
				}
			}
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000FF78 File Offset: 0x0000E178
		protected ConfigBaseClass()
		{
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000FFE4 File Offset: 0x0000E1E4
		[FileIOPermission(SecurityAction.Assert, Unrestricted = true)]
		private static bool IsDirExist(string str)
		{
			return Directory.Exists(str);
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000FFEC File Offset: 0x0000E1EC
		internal static string GetResolvedFileLocation(string path)
		{
			string result;
			try
			{
				if (string.IsNullOrEmpty(path.Trim()))
				{
					result = path;
				}
				else
				{
					string[] array = path.Trim().Split(ConfigBaseClass.s_dirSeparators);
					bool flag = path.Contains("%");
					bool flag2 = path.Contains("$");
					bool bStartsWithSeparator = path.StartsWith(Path.DirectorySeparatorChar.ToString());
					bool bEndsWithSeparator = path.EndsWith(Path.DirectorySeparatorChar.ToString());
					if (array.Length == 0)
					{
						result = path;
					}
					else
					{
						string str = string.Empty;
						if (array[0] == "." || array[0] == "..")
						{
							str = ConfigBaseClass.s_appDir + Path.DirectorySeparatorChar;
						}
						if (flag || flag2)
						{
							string text = ConfigBaseClass.ResolveEnvVariables(array, bStartsWithSeparator, bEndsWithSeparator);
							if (text == null)
							{
								result = null;
							}
							else
							{
								result = str + text;
							}
						}
						else
						{
							result = str + path;
						}
					}
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x000100F4 File Offset: 0x0000E2F4
		private static string ResolveEnvVariables(string[] folders, bool bStartsWithSeparator, bool bEndsWithSeparator)
		{
			string result;
			try
			{
				int i = 0;
				int num = 0;
				StringBuilder stringBuilder = new StringBuilder(256);
				if (bStartsWithSeparator)
				{
					stringBuilder.Append(Path.DirectorySeparatorChar);
				}
				foreach (string text in folders)
				{
					if (text.Length > 2 && text[0] == '%' && text[text.Length - 1] == '%')
					{
						for (int k = num; k < i; k++)
						{
							stringBuilder.Append(folders[k]);
							stringBuilder.Append(Path.DirectorySeparatorChar);
						}
						num = i;
						if (!ConfigBaseClass.IsDirExist(stringBuilder.ToString() + folders[i]))
						{
							string variable = text.Trim(ConfigBaseClass.s_percentage);
							string text2 = Environment.GetEnvironmentVariable(variable);
							if (text2 != null)
							{
								text2 = text2.Trim();
							}
							if (string.IsNullOrEmpty(text2))
							{
								return null;
							}
							folders[i] = text2;
						}
					}
					else if (text.Length > 1 && text[0] == '$')
					{
						for (int l = num; l < i; l++)
						{
							stringBuilder.Append(folders[l]);
							stringBuilder.Append(Path.DirectorySeparatorChar);
						}
						num = i;
						if (!ConfigBaseClass.IsDirExist(stringBuilder.ToString() + folders[i]))
						{
							string variable2 = text.TrimStart(ConfigBaseClass.s_dollar);
							string text3 = Environment.GetEnvironmentVariable(variable2);
							if (text3 != null)
							{
								text3 = text3.Trim();
							}
							if (string.IsNullOrEmpty(text3))
							{
								return null;
							}
							folders[i] = text3;
						}
					}
					i++;
				}
				StringBuilder stringBuilder2 = new StringBuilder(256);
				if (bStartsWithSeparator)
				{
					stringBuilder2.Append(Path.DirectorySeparatorChar);
				}
				for (i = 0; i < folders.Length; i++)
				{
					stringBuilder2.Append(folders[i]);
					if (i < folders.Length - 1)
					{
						stringBuilder2.Append(Path.DirectorySeparatorChar);
					}
				}
				if (bEndsWithSeparator)
				{
					stringBuilder2.Append(Path.DirectorySeparatorChar);
				}
				result = stringBuilder2.ToString();
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0001031C File Offset: 0x0000E51C
		internal static int GetMaxPrecision(string edmType, bool isEF6OrHigher = false)
		{
			bool flag = false;
			string text = edmType.Trim().ToUpperInvariant();
			if (isEF6OrHigher)
			{
				int result = -1;
				if (!flag)
				{
					ConfigBaseClass.PopulateMaxPrecisionArray();
				}
				if (text == "BOOL")
				{
					result = ConfigBaseClass.s_maxPrecision[0];
				}
				else if (text == "BYTE")
				{
					result = ConfigBaseClass.s_maxPrecision[1];
				}
				else if (text == "INT16")
				{
					result = ConfigBaseClass.s_maxPrecision[2];
				}
				else if (text == "INT32")
				{
					result = ConfigBaseClass.s_maxPrecision[3];
				}
				else if (text == "INT64")
				{
					result = ConfigBaseClass.s_maxPrecision[4];
				}
				return result;
			}
			object obj = ConfigBaseClass.s_edmMapping[text];
			if (obj == null)
			{
				return -1;
			}
			return (int)obj;
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x000103D0 File Offset: 0x0000E5D0
		internal static void PopulateMaxPrecisionArray()
		{
			int num = 0;
			for (int i = 0; i < ConfigBaseClass.s_edmTypes.Length; i++)
			{
				string key = ConfigBaseClass.s_edmTypes[i];
				if (ConfigBaseClass.s_EdmMappingToDbType.ContainsKey(key))
				{
					DbType dbType;
					ConfigBaseClass.s_EdmMappingToDbType.TryGetValue(key, out dbType);
					for (int j = ConfigBaseClass.s_edmPrecisonMapping.Length - 1; j > 0; j--)
					{
						if (ConfigBaseClass.s_edmPrecisonMapping[j] == dbType)
						{
							num = j;
							break;
						}
					}
					ConfigBaseClass.s_maxPrecision[i] = num;
					num = 0;
				}
			}
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00010448 File Offset: 0x0000E648
		[RegistryPermission(SecurityAction.Assert, Unrestricted = true)]
		public string RetrieveStringValue(string entryToBeSearched, object defaultValue, ref bool bFromConfigFile)
		{
			string text = null;
			if (this is CustomConfigFileReader)
			{
				try
				{
					if (ConfigBaseClass.m_configParameters.Count > 0 && ConfigBaseClass.m_configParameters[entryToBeSearched] != null)
					{
						text = (string)ConfigBaseClass.m_configParameters[entryToBeSearched];
						bFromConfigFile = true;
					}
					else if (ConfigBaseClass.odpNetKey != null)
					{
						text = (ConfigBaseClass.odpNetKey.GetValue(entryToBeSearched) as string);
					}
				}
				catch
				{
				}
				if ((text == null || text == string.Empty) && defaultValue != null)
				{
					text = defaultValue.ToString();
					bFromConfigFile = false;
				}
				if (text != null && text != string.Empty)
				{
					if (string.Equals(text, "false", StringComparison.InvariantCultureIgnoreCase))
					{
						text = "0";
					}
					else if (string.Equals(text, "true", StringComparison.InvariantCultureIgnoreCase))
					{
						text = "1";
					}
				}
			}
			return text;
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00010518 File Offset: 0x0000E718
		public int RetrieveIntValue(string entryToBeSearched, object defaultValue, bool bAcceptNegativeValues, ref bool bFromConfigFile)
		{
			int num = 0;
			try
			{
				num = int.Parse(this.RetrieveStringValue(entryToBeSearched, defaultValue, ref bFromConfigFile));
				if (!bAcceptNegativeValues && num < 0)
				{
					num = (int)defaultValue;
				}
			}
			catch
			{
				num = (int)defaultValue;
			}
			return num;
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00010564 File Offset: 0x0000E764
		internal ConfigBaseClass.StoredProcedureInfo GetStoredProcInfo(string commandText)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[]
				{
					"(REFCURSOR) GetRefCursorInfo(" + commandText + ")"
				});
			}
			if (commandText == null || commandText.Length == 0)
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[]
					{
						"(REFCURSOR) GetRefCursorInfo(" + commandText + ") : no match"
					});
				}
				return null;
			}
			commandText = commandText.Trim();
			ConfigBaseClass.StoredProcedureInfo storedProcedureInfo = null;
			string text = null;
			if (this.s_storedProcInformation.Count > 0)
			{
				storedProcedureInfo = (ConfigBaseClass.StoredProcedureInfo)this.s_storedProcInformation[commandText];
				if (storedProcedureInfo == null)
				{
					text = commandText;
					this.GetKeyInProperCase(ref text);
					storedProcedureInfo = (ConfigBaseClass.StoredProcedureInfo)this.s_storedProcInformation[text];
				}
			}
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				if (storedProcedureInfo == null)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[]
					{
						"(REFCURSOR) GetRefCursorInfo(" + commandText + ") : no match"
					});
				}
				else if (text == null)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[]
					{
						"(REFCURSOR) GetRefCursorInfo(" + commandText + ") : match found!"
					});
				}
				else
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[]
					{
						"(REFCURSOR) GetRefCursorInfo(" + text + ") : match found!"
					});
				}
			}
			return storedProcedureInfo;
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x000106B4 File Offset: 0x0000E8B4
		internal string GetAttrValueInProperCase(string attributeValue)
		{
			int length = attributeValue.Length;
			if (length > 0)
			{
				if (attributeValue[0] == '"' && attributeValue[length - 1] == '"')
				{
					attributeValue = attributeValue.Trim(new char[]
					{
						'"'
					});
				}
				else
				{
					attributeValue = attributeValue.ToUpperInvariant();
				}
			}
			return attributeValue;
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00010704 File Offset: 0x0000E904
		internal void GetKeyInProperCase(ref string storedProcKey)
		{
			string[] array = storedProcKey.Split(new char[]
			{
				'.'
			});
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < array.Length; i++)
			{
				if (i != 0)
				{
					stringBuilder.Append(".");
				}
				string attrValueInProperCase = this.GetAttrValueInProperCase(array[i].Trim());
				stringBuilder.Append(attrValueInProperCase);
			}
			storedProcKey = stringBuilder.ToString();
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0001076C File Offset: 0x0000E96C
		[ConfigurationPermission(SecurityAction.Assert, Unrestricted = true)]
		public static ConfigBaseClass GetInstance(bool bIsManaged = false)
		{
			if (ConfigBaseClass.instance == null)
			{
				CustomSectionHandler customSectionHandler = null;
				if (!bIsManaged)
				{
					ConfigBaseClass.instance = new CustomConfigFileReader(bIsManaged);
					customSectionHandler = (ConfigurationManager.GetSection("oracle.unmanageddataaccess.client") as CustomSectionHandler);
				}
				if ((customSectionHandler != null && customSectionHandler.m_bSectionExists) || bIsManaged)
				{
					if (bIsManaged)
					{
						ConfigBaseClass.instance = new CustomConfigFileReader(bIsManaged);
					}
					ConfigBaseClass.instance.ParseConfigFile();
				}
				else
				{
					ConfigBaseClass.instance = new RegAndConfigRdr();
				}
			}
			return ConfigBaseClass.instance;
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x000107D8 File Offset: 0x0000E9D8
		internal virtual void ParseConfigFile()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002CA RID: 714 RVA: 0x000107E0 File Offset: 0x0000E9E0
		internal static string CurOraFileLoc(OraFiles file)
		{
			if (file == OraFiles.TnsNames && string.IsNullOrEmpty(ConfigBaseClass.m_TNSNamesoraloc))
			{
				ConfigBaseClass.m_TNSNamesoraloc = ProviderConfig.NewOraFileLoc(OraFiles.TnsNames);
			}
			if (file != OraFiles.TnsNames)
			{
				return ConfigBaseClass.m_sqlnetOraLoc;
			}
			return ConfigBaseClass.m_TNSNamesoraloc;
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0001080C File Offset: 0x0000EA0C
		internal static Hashtable CurOraFileParams(OraFiles file)
		{
			if (file != OraFiles.TnsNames)
			{
				return ConfigBaseClass.m_configParameters;
			}
			return ConfigBaseClass.m_configDataSourcesMap;
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0001081C File Offset: 0x0000EA1C
		internal static ArrayList GetTnsNamesSearchPath(out bool isTnsnamesEnabled)
		{
			isTnsnamesEnabled = true;
			ConfigBaseClass.m_TNSNamesoraloc = ProviderConfig.NewOraFileLoc(OraFiles.TnsNames);
			return ConfigBaseClass.m_TNSConfigPath;
		}

		// Token: 0x060002CD RID: 717 RVA: 0x00010834 File Offset: 0x0000EA34
		internal virtual void ParseClientXmlNode(XmlNode xmlElement, ref Hashtable hashtable, ref ArrayList arrayList, ArrayList filterNodes = null)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0001083C File Offset: 0x0000EA3C
		[EnvironmentPermission(SecurityAction.Assert, Unrestricted = true)]
		internal static string OraDebugJDWP()
		{
			if (string.IsNullOrEmpty(ConfigBaseClass.m_OraDebugJDWP))
			{
				return Environment.GetEnvironmentVariable("ORA_DEBUG_JDWP");
			}
			return ConfigBaseClass.m_OraDebugJDWP;
		}

		// Token: 0x040003AD RID: 941
		internal const int DEFAULT_COMMAND_FETCHSIZE = 131072;

		// Token: 0x040003AE RID: 942
		internal const int DEFAULT_STMT_CACHE_SIZE = 0;

		// Token: 0x040003AF RID: 943
		internal const int DEFAULT_XMLTYPE_MAX_CACHE_ENTRIES = 70000;

		// Token: 0x040003B0 RID: 944
		internal const int DEFAULT_XMLTYPE_MAX_CACHE_SIZE = 1750000;

		// Token: 0x040003B1 RID: 945
		internal const int DEFAULT_XMLTYPE_OPTIMIZATION_LEVEL = 0;

		// Token: 0x040003B2 RID: 946
		internal const ushort DTC_DEFAULT_RECO_PORT = 2030;

		// Token: 0x040003B3 RID: 947
		internal const uint DTC_DEFAULT_TXNTIMETOLIVE = 120U;

		// Token: 0x040003B4 RID: 948
		[Configuration("CPVersion")]
		internal static string m_cpversion = "1.0";

		// Token: 0x040003B5 RID: 949
		internal static int m_cpMajorVersion = 0;

		// Token: 0x040003B6 RID: 950
		[Configuration("DbNotificationPort")]
		internal static int m_DBNotificationPort = -1;

		// Token: 0x040003B7 RID: 951
		[Configuration("DemandOraclePermission")]
		internal static bool m_DemandOraclePermission = false;

		// Token: 0x040003B8 RID: 952
		[Configuration("BindByName")]
		internal static bool m_BindByName = false;

		// Token: 0x040003B9 RID: 953
		[Configuration("FetchSize")]
		internal static int m_FetchSize = 131072;

		// Token: 0x040003BA RID: 954
		[Configuration("MaxStatementCacheSize")]
		internal static int m_MaxStatementCacheSize = -1;

		// Token: 0x040003BB RID: 955
		[Configuration("MetaDataXml")]
		internal static string m_MetaDataXml = null;

		// Token: 0x040003BC RID: 956
		[Configuration("PROMOTABLE")]
		internal static PromotableTransaction m_PromotableTransaction = PromotableTransaction.Promotable;

		// Token: 0x040003BD RID: 957
		[Configuration("LegacyIsolationLevelBehavior")]
		internal static bool m_bLegacyIsolationLevelBehavior = true;

		// Token: 0x040003BE RID: 958
		[Configuration("SelfTuning")]
		internal static bool m_SelfTuning = true;

		// Token: 0x040003BF RID: 959
		[Configuration("StatementCacheSize")]
		internal static int m_StatementCacheSize = 0;

		// Token: 0x040003C0 RID: 960
		[Configuration("RevertBatchUpdateErrorHandling")]
		internal static bool m_RevertBUErrHandling = false;

		// Token: 0x040003C1 RID: 961
		[Configuration("PerformanceCounters")]
		internal static ushort m_PerformanceCounters = 0;

		// Token: 0x040003C2 RID: 962
		[Configuration("InitialLOBFetchSize")]
		internal static int m_InitialLOBFetchSize = -1;

		// Token: 0x040003C3 RID: 963
		[Configuration("InitialLONGFetchSize")]
		internal static int m_InitialLONGFetchSize = -1;

		// Token: 0x040003C4 RID: 964
		[Configuration("TNS_ADMIN")]
		internal static string m_TnsAdminLocation = null;

		// Token: 0x040003C5 RID: 965
		[Configuration("ORA_DEBUG_JDWP")]
		internal static string m_OraDebugJDWP = null;

		// Token: 0x040003C6 RID: 966
		[Configuration("LDAP_ADMIN")]
		internal static string m_LdapAdminLocation = null;

		// Token: 0x040003C7 RID: 967
		[Configuration("AttemptConnectDuringSvcRelocation")]
		internal static int m_attemptConnectDuringSvcReloc = 0;

		// Token: 0x040003C8 RID: 968
		[Configuration("ServiceRelocationConnectionTimeout")]
		internal static string m_serviceRelocationTimeout = "90";

		// Token: 0x040003C9 RID: 969
		internal static bool m_XMLTypeClientSideDecoding = false;

		// Token: 0x040003CA RID: 970
		[Configuration("XMLTypeOpcodeDump")]
		internal static bool m_XMLTypeOpcodeDump = false;

		// Token: 0x040003CB RID: 971
		[Configuration("XMLTypeMaxCacheEntries")]
		internal static int m_XMLTypeMaxCacheEntries = 70000;

		// Token: 0x040003CC RID: 972
		[Configuration("XMLTypeMaxCacheSize")]
		internal static int m_XMLTypeMaxCacheSize = 1750000;

		// Token: 0x040003CD RID: 973
		[Configuration("XMLTypeParseAllXml")]
		internal static bool m_XMLTypeParseAllXml = false;

		// Token: 0x040003CE RID: 974
		[Configuration("XMLTypeParseXml")]
		internal static bool m_XMLTypeParseXml = false;

		// Token: 0x040003CF RID: 975
		[Configuration("XMLTypeUseHeaderEncodingFromServer")]
		internal static bool m_XMLTypeUseHeaderEncodingFromServer = false;

		// Token: 0x040003D0 RID: 976
		[Configuration("XMLTypeOptimizationLevel")]
		internal static int m_XMLTypeOptimizationLevel = 0;

		// Token: 0x040003D1 RID: 977
		[Configuration("TraceFileLocation")]
		internal static string m_traceFileLocation = null;

		// Token: 0x040003D2 RID: 978
		[Configuration("TraceLevel")]
		internal static int m_TraceLevel = 0;

		// Token: 0x040003D3 RID: 979
		[Configuration("TraceOption")]
		internal static int m_TraceOption = 0;

		// Token: 0x040003D4 RID: 980
		[Configuration("LegacyEntireLOBFetch")]
		internal static bool m_bLegacyNegativeOneILFSBehavior = false;

		// Token: 0x040003D5 RID: 981
		[Configuration("HAEvents")]
		internal static bool m_haEvents = true;

		// Token: 0x040003D6 RID: 982
		internal static bool m_bHAEventsConfigured = false;

		// Token: 0x040003D7 RID: 983
		[Configuration("LoadBalancing")]
		internal static bool m_loadBalancing = true;

		// Token: 0x040003D8 RID: 984
		internal static bool m_bLoadBalancingConfigured = false;

		// Token: 0x040003D9 RID: 985
		internal static bool m_bUseLegacyLocalParser = true;

		// Token: 0x040003DA RID: 986
		[Configuration("ColumnCacheSize")]
		internal static int m_ColumnCacheSize = 100;

		// Token: 0x040003DB RID: 987
		[Configuration("DRCPConnectionClass")]
		internal static string m_connectionClass = null;

		// Token: 0x040003DC RID: 988
		internal static object m_maxStatementCacheSizeLock = new object();

		// Token: 0x040003DD RID: 989
		[Configuration("Edition")]
		internal static string m_appEdition = "";

		// Token: 0x040003DE RID: 990
		internal static Process CurrentProcess = null;

		// Token: 0x040003DF RID: 991
		internal static Version m_assemblyVersion = null;

		// Token: 0x040003E0 RID: 992
		internal static string m_sectionVersion = null;

		// Token: 0x040003E1 RID: 993
		internal static string m_sqlnetOraLoc = null;

		// Token: 0x040003E2 RID: 994
		internal static string m_TNSNamesoraloc = null;

		// Token: 0x040003E3 RID: 995
		internal static ArrayList m_TNSConfigPath = new ArrayList();

		// Token: 0x040003E4 RID: 996
		internal static string m_OracleHome = null;

		// Token: 0x040003E5 RID: 997
		internal static bool m_OracleHomeSet = false;

		// Token: 0x040003E6 RID: 998
		internal static char[] m_allowedCont = new char[]
		{
			' ',
			'#',
			'\f',
			'\n',
			'\r',
			'\t',
			'\v'
		};

		// Token: 0x040003E7 RID: 999
		internal static char[] m_parens = new char[]
		{
			'(',
			')'
		};

		// Token: 0x040003E8 RID: 1000
		internal static Hashtable m_configParameters = new Hashtable(StringComparer.OrdinalIgnoreCase);

		// Token: 0x040003E9 RID: 1001
		internal static Hashtable m_LDAPconfigParameters = new Hashtable(StringComparer.OrdinalIgnoreCase);

		// Token: 0x040003EA RID: 1002
		internal static Hashtable m_configDataSourcesMap = new Hashtable(StringComparer.OrdinalIgnoreCase);

		// Token: 0x040003EB RID: 1003
		internal static Hashtable m_connectionPoolNameMapping = new Hashtable();

		// Token: 0x040003EC RID: 1004
		internal static Hashtable m_udtMappings = new Hashtable();

		// Token: 0x040003ED RID: 1005
		internal static string m_singleTraceFileLocation;

		// Token: 0x040003EE RID: 1006
		internal static char[] s_percentage = new char[]
		{
			'%'
		};

		// Token: 0x040003EF RID: 1007
		internal static char[] s_dollar = new char[]
		{
			'$'
		};

		// Token: 0x040003F0 RID: 1008
		internal static char[] s_space_and_tab = new char[]
		{
			' ',
			'\t'
		};

		// Token: 0x040003F1 RID: 1009
		internal static char[] s_dirSeparators = new char[]
		{
			Path.DirectorySeparatorChar,
			Path.AltDirectorySeparatorChar
		};

		// Token: 0x040003F2 RID: 1010
		internal static string m_ONSConfigFile = null;

		// Token: 0x040003F3 RID: 1011
		internal static ONSConfigMode m_ONSMode = ONSConfigMode.Unspecified;

		// Token: 0x040003F4 RID: 1012
		internal static Dictionary<string, Dictionary<string, string>> m_ONSMapping = new Dictionary<string, Dictionary<string, string>>();

		// Token: 0x040003F5 RID: 1013
		internal static string m_nodeListFromConfFile = null;

		// Token: 0x040003F6 RID: 1014
		internal static ParseMode m_ParseMode = ParseMode.None;

		// Token: 0x040003F7 RID: 1015
		internal static string m_recoveryServiceHost = Environment.MachineName;

		// Token: 0x040003F8 RID: 1016
		internal static ushort m_recoveryServicePort = 2030;

		// Token: 0x040003F9 RID: 1017
		internal static uint m_dtcTxnTimeout = 120U;

		// Token: 0x040003FA RID: 1018
		internal static bool m_dtcUseDTCDLL = false;

		// Token: 0x040003FB RID: 1019
		internal static bool m_dtcUseManagedDTC = false;

		// Token: 0x040003FC RID: 1020
		internal static bool m_bIsManaged = false;

		// Token: 0x040003FD RID: 1021
		internal static Hashtable m_configParamFrAppConfig = new Hashtable(StringComparer.OrdinalIgnoreCase);

		// Token: 0x040003FE RID: 1022
		internal static ArrayList m_versionSpecificNodesList = new ArrayList();

		// Token: 0x040003FF RID: 1023
		private static ConfigBaseClass instance;

		// Token: 0x04000400 RID: 1024
		internal StringBuilder mtsTrace = new StringBuilder();

		// Token: 0x04000401 RID: 1025
		internal static Hashtable s_odtConfigNamesToRefCursorInfo = new Hashtable();

		// Token: 0x04000402 RID: 1026
		internal static NameValueCollection m_configSection = null;

		// Token: 0x04000403 RID: 1027
		internal static RegistryKey odpNetKey = null;

		// Token: 0x04000404 RID: 1028
		internal static Hashtable s_edmMapping = new Hashtable
		{
			{
				"BOOL",
				-2
			},
			{
				"BYTE",
				-1
			},
			{
				"INT16",
				5
			},
			{
				"INT32",
				10
			},
			{
				"INT64",
				19
			}
		};

		// Token: 0x04000405 RID: 1029
		internal static string[] s_edmTypes = new string[]
		{
			"BOOL",
			"BYTE",
			"INT16",
			"INT32",
			"INT64"
		};

		// Token: 0x04000406 RID: 1030
		internal static int[] s_maxPrecision = new int[]
		{
			-2,
			-1,
			5,
			10,
			19
		};

		// Token: 0x04000407 RID: 1031
		internal static bool s_bEdmNumberMappingPresent = false;

		// Token: 0x04000408 RID: 1032
		internal static bool s_bLegacyEdmMappingPresent = false;

		// Token: 0x04000409 RID: 1033
		internal static bool s_bFromConfigSRCT = false;

		// Token: 0x0400040A RID: 1034
		internal static int srctOffset = 0;

		// Token: 0x0400040B RID: 1035
		internal static bool s_bDrainTimeoutInSRCT = false;

		// Token: 0x0400040C RID: 1036
		internal static Dictionary<string, DbType> s_EdmMappingToDbType = new Dictionary<string, DbType>
		{
			{
				"BOOL",
				DbType.Boolean
			},
			{
				"BYTE",
				DbType.Byte
			},
			{
				"INT16",
				DbType.Int16
			},
			{
				"INT32",
				DbType.Int32
			},
			{
				"INT64",
				DbType.Int64
			},
			{
				"DECIMAL",
				DbType.Decimal
			}
		};

		// Token: 0x0400040D RID: 1037
		internal static DbType[] s_edmPrecisonMapping = new DbType[]
		{
			DbType.Decimal,
			DbType.Boolean,
			DbType.Byte,
			DbType.Byte,
			DbType.Int16,
			DbType.Int16,
			DbType.Int32,
			DbType.Int32,
			DbType.Int32,
			DbType.Int32,
			DbType.Int32,
			DbType.Int64,
			DbType.Int64,
			DbType.Int64,
			DbType.Int64,
			DbType.Int64,
			DbType.Int64,
			DbType.Int64,
			DbType.Int64,
			DbType.Int64,
			DbType.Decimal,
			DbType.Decimal,
			DbType.Decimal,
			DbType.Decimal,
			DbType.Decimal,
			DbType.Decimal,
			DbType.Decimal,
			DbType.Decimal,
			DbType.Decimal,
			DbType.Decimal,
			DbType.Decimal,
			DbType.Decimal,
			DbType.Decimal,
			DbType.Decimal,
			DbType.Decimal,
			DbType.Decimal,
			DbType.Decimal,
			DbType.Decimal,
			DbType.Decimal
		};

		// Token: 0x0400040E RID: 1038
		internal string s_strReg = " (REGISTRY)";

		// Token: 0x0400040F RID: 1039
		internal string s_strCfg = " (CONFIG)  ";

		// Token: 0x04000410 RID: 1040
		internal string s_strVer = " (VERSION) ";

		// Token: 0x04000411 RID: 1041
		internal string s_strEnv = " (ENVIRONMENT)";

		// Token: 0x04000412 RID: 1042
		internal string s_strProdVer = " (PRODUCT VERSION) ";

		// Token: 0x04000413 RID: 1043
		internal string s_strTrm = ")\n";

		// Token: 0x04000414 RID: 1044
		internal Hashtable s_storedProcInformation = new Hashtable();

		// Token: 0x04000415 RID: 1045
		internal static string s_appDir;

		// Token: 0x0200003B RID: 59
		internal class StoredProcedureInfo
		{
			// Token: 0x060002CF RID: 719 RVA: 0x0001085C File Offset: 0x0000EA5C
			internal RefCursorInfo GetRefCursorInfo(int currentResultIndex)
			{
				RefCursorInfo result = null;
				if (currentResultIndex < this.m_numExplicitBoundRefCursors && this.m_refCursors.Count > 0)
				{
					result = this.m_refCursors[currentResultIndex];
				}
				else
				{
					int num = currentResultIndex - this.m_numExplicitBoundRefCursors;
					if (num >= 0 && this.m_implicitlyRetRefCursors.Count > num)
					{
						result = this.m_implicitlyRetRefCursors[num];
					}
				}
				return result;
			}

			// Token: 0x060002D0 RID: 720 RVA: 0x000108BC File Offset: 0x0000EABC
			internal DataTable GetColumnInfo(int currentResultIndex)
			{
				DataTable result = null;
				RefCursorInfo refCursorInfo = this.GetRefCursorInfo(currentResultIndex);
				if (refCursorInfo != null && refCursorInfo.columnInfo.Rows.Count > 0)
				{
					result = refCursorInfo.columnInfo;
				}
				return result;
			}

			// Token: 0x04000416 RID: 1046
			internal List<RefCursorInfo> m_refCursors = new List<RefCursorInfo>();

			// Token: 0x04000417 RID: 1047
			internal List<RefCursorInfo> m_implicitlyRetRefCursors = new List<RefCursorInfo>();

			// Token: 0x04000418 RID: 1048
			internal int m_numExplicitBoundRefCursors;
		}
	}
}
