using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Permissions;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.ConnectionPool;
using OracleInternal.Network;
using OracleInternal.ServiceObjects;

namespace OracleInternal.Common
{
	// Token: 0x020000B0 RID: 176
	internal class ProviderConfig : ConfigBaseClass
	{
		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060006FD RID: 1789 RVA: 0x00040888 File Offset: 0x0003EA88
		internal static Hashtable ConfigDataSourcesMap
		{
			get
			{
				return ConfigBaseClass.m_configDataSourcesMap;
			}
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x00040890 File Offset: 0x0003EA90
		internal static void OnAppConfigFileChanged()
		{
			string traceFileLocation = ConfigBaseClass.m_traceFileLocation;
			int traceLevel = ConfigBaseClass.m_TraceLevel;
			bool flag = ConfigBaseClass.m_TraceOption == 1;
			try
			{
				XElement xelement = XElement.Load(ProviderConfig.s_appConfigFilePath).Element("oracle.manageddataaccess.client");
				Dictionary<string, XElement> dictionary = new Dictionary<string, XElement>();
				foreach (XElement xelement2 in xelement.Elements("version"))
				{
					if (dictionary.ContainsKey(xelement2.Attribute("number").Value))
					{
						dictionary[xelement2.Attribute("number").Value] = xelement2;
					}
					else
					{
						dictionary.Add(xelement2.Attribute("number").Value, xelement2);
					}
				}
				if (dictionary.Any<KeyValuePair<string, XElement>>())
				{
					XElement xelement3 = null;
					if (dictionary.TryGetValue("*", out xelement3) && xelement3 != null)
					{
						ProviderConfig.ReadTraceSettingsFromConfigVersionNode(xelement3, ref traceFileLocation, ref traceLevel, ref flag);
					}
					if (dictionary.TryGetValue(ConfigBaseClass.m_assemblyVersion.ToString(), out xelement3) && xelement3 != null)
					{
						ProviderConfig.ReadTraceSettingsFromConfigVersionNode(xelement3, ref traceFileLocation, ref traceLevel, ref flag);
					}
					bool flag2 = traceFileLocation != ConfigBaseClass.m_traceFileLocation;
					if (flag2 || traceLevel != ConfigBaseClass.m_TraceLevel || flag != ((ConfigBaseClass.m_TraceOption == 1) ? true : false))
					{
						ConfigBaseClass.m_TraceLevel = traceLevel;
						ConfigBaseClass.m_TraceOption = (flag ? 1 : 0);
						ConfigBaseClass.m_traceFileLocation = traceFileLocation;
						Trace.ReInit(flag2);
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x00040A48 File Offset: 0x0003EC48
		private static void ReadTraceSettingsFromConfigVersionNode(XElement versionNode, ref string traceFilePath, ref int traceLevel, ref bool traceOption)
		{
			XElement xelement = versionNode.Element("settings");
			if (xelement != null)
			{
				int num = 0;
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				foreach (XElement xelement2 in xelement.Elements())
				{
					if (dictionary.ContainsKey(xelement2.Attribute("name").Value))
					{
						dictionary[xelement2.Attribute("name").Value] = xelement2.Attribute("value").Value;
					}
					else
					{
						dictionary.Add(xelement2.Attribute("name").Value, xelement2.Attribute("value").Value);
					}
				}
				string s = null;
				if (dictionary.TryGetValue("TraceLevel", out s) && int.TryParse(s, out num))
				{
					traceLevel = num;
				}
				string s2 = null;
				if (dictionary.TryGetValue("TraceOption", out s2) && int.TryParse(s2, out num))
				{
					traceOption = (num != 0);
				}
				string text = null;
				if (dictionary.TryGetValue("TraceFileLocation", out text))
				{
					traceFilePath = text;
				}
			}
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x00040B94 File Offset: 0x0003ED94
		static ProviderConfig()
		{
			ConfigBaseClass.GetInstance(true);
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x00040BD4 File Offset: 0x0003EDD4
		[FileIOPermission(SecurityAction.Assert, Unrestricted = true)]
		private static Version GetExecutingAssemblyVersion()
		{
			return Assembly.GetExecutingAssembly().GetName().Version;
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x00040BE8 File Offset: 0x0003EDE8
		[ConfigurationPermission(SecurityAction.Assert, Unrestricted = true)]
		internal static void ReParseDataSourceSection()
		{
			ConfigBaseClass.m_ParseMode = ParseMode.ReParseTnsNames;
			CustomConfigFileReader customConfigFileReader = ConfigBaseClass.GetInstance(true) as CustomConfigFileReader;
			try
			{
				if (ConfigBaseClass.m_configDataSourcesMap != null && ConfigBaseClass.m_configDataSourcesMap.Count > 0)
				{
					List<string> dataSources = OracleConnectionDispenser<OraclePoolManager, OraclePool, OracleConnectionImpl>.GetDataSources();
					string[] array = new string[ConfigBaseClass.m_configDataSourcesMap.Count];
					ConfigBaseClass.m_configDataSourcesMap.Keys.CopyTo(array, 0);
					foreach (string text in array)
					{
						if (!dataSources.Contains(text))
						{
							ConfigBaseClass.m_configDataSourcesMap.Remove(text);
						}
					}
				}
				ConfigurationManager.RefreshSection("oracle.manageddataaccess.client");
				ConfigurationManager.GetSection("oracle.manageddataaccess.client");
				for (int j = 0; j < ConfigBaseClass.m_versionSpecificNodesList.Count; j++)
				{
					customConfigFileReader.ParseSubSection((XmlNode)ConfigBaseClass.m_versionSpecificNodesList[j], ref customConfigFileReader.s_storedProcInformation, new ArrayList
					{
						"dataSources"
					});
				}
			}
			catch
			{
				throw;
			}
			finally
			{
				ConfigBaseClass.m_versionSpecificNodesList.Clear();
				ConfigBaseClass.m_ParseMode = ParseMode.None;
			}
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x00040D08 File Offset: 0x0003EF08
		internal static void RefreshDataSources()
		{
			object syncObjForGetDataSources = OracleConnectionDispenser<OraclePoolManager, OraclePool, OracleConnectionImpl>.m_syncObjForGetDataSources;
			lock (syncObjForGetDataSources)
			{
				AddressResolution.RefreshNamingAdapters();
			}
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x00040D48 File Offset: 0x0003EF48
		[FileIOPermission(SecurityAction.Assert, Unrestricted = true)]
		[EnvironmentPermission(SecurityAction.Assert, Unrestricted = true)]
		internal static void NewOraFileParams(OraFiles file, string filePath, Hashtable theParams)
		{
			OracleTraceTag traceTag = (file == OraFiles.TnsNames) ? OracleTraceTag.Tnsnames : OracleTraceTag.Sqlnet;
			bool flag = false;
			List<string> list = null;
			if (file == OraFiles.TnsNames && ConfigBaseClass.m_ParseMode == ParseMode.ReParseTnsNames && theParams != null && theParams.Count > 0)
			{
				flag = true;
				list = OracleConnectionDispenser<OraclePoolManager, OraclePool, OracleConnectionImpl>.GetDataSources();
				string[] array = new string[theParams.Count];
				theParams.Keys.CopyTo(array, 0);
				foreach (string text in array)
				{
					if (!list.Contains(text))
					{
						theParams.Remove(text);
					}
				}
			}
			if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
			{
				return;
			}
			CustomConfigFileReader customConfigFileReader = ConfigBaseClass.GetInstance(true) as CustomConfigFileReader;
			using (StreamReader streamReader = new StreamReader(filePath))
			{
				bool flag2 = file == OraFiles.SqlNet;
				string leftover;
				string[] name = customConfigFileReader.GetName(streamReader, out leftover);
				while (name != null && name.Length != 0)
				{
					string value = customConfigFileReader.GetValue(streamReader, leftover);
					if (!string.IsNullOrEmpty(value))
					{
						foreach (string text2 in name)
						{
							if (!flag2 || ConfigBaseClass.m_configParamFrAppConfig[text2] == null || !(bool)ConfigBaseClass.m_configParamFrAppConfig[text2])
							{
								if (ConfigBaseClass.m_TraceLevel > 0)
								{
									Trace.Write(OracleTraceLevel.Config, traceTag, new string[]
									{
										text2 + " : " + value
									});
								}
								if (flag)
								{
									if (!list.Contains(text2))
									{
										theParams[text2] = value;
									}
								}
								else
								{
									theParams[text2] = value;
								}
							}
						}
					}
					name = customConfigFileReader.GetName(streamReader, out leftover);
				}
			}
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x00040EF4 File Offset: 0x0003F0F4
		private static string getFilePath(OraFiles file, string dir, string fileName)
		{
			dir = ConfigBaseClass.GetResolvedFileLocation(dir);
			if (file == OraFiles.TnsNames)
			{
				ConfigBaseClass.m_TNSConfigPath.Add(Path.Combine(dir, "tnsnames.ora"));
			}
			return ProviderConfig.FindFile(dir, fileName);
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x00040F20 File Offset: 0x0003F120
		[RegistryPermission(SecurityAction.Assert, Unrestricted = true)]
		[FileIOPermission(SecurityAction.Assert, Unrestricted = true)]
		[EnvironmentPermission(SecurityAction.Assert, Unrestricted = true)]
		internal static string NewOraFileLoc(OraFiles file)
		{
			string text = string.Empty;
			string fileName = string.Empty;
			if (file == OraFiles.TnsNames)
			{
				ConfigBaseClass.m_TNSConfigPath.Clear();
			}
			fileName = ((file == OraFiles.TnsNames) ? "tnsnames.ora" : ((file == OraFiles.SqlNet) ? "sqlnet.ora" : "ldap.ora"));
			if (!string.IsNullOrEmpty(ConfigBaseClass.m_TnsAdminLocation))
			{
				text = ProviderConfig.getFilePath(file, ConfigBaseClass.m_TnsAdminLocation, fileName);
				if (text.Length > 0)
				{
					return text;
				}
			}
			if (file == OraFiles.Ldap && !string.IsNullOrEmpty(ConfigBaseClass.m_LdapAdminLocation))
			{
				text = ProviderConfig.getFilePath(file, ConfigBaseClass.m_LdapAdminLocation, fileName);
				if (text.Length > 0)
				{
					return text;
				}
			}
			text = ProviderConfig.getFilePath(file, ".", fileName);
			if (text.Length > 0)
			{
				return text;
			}
			text = ProviderConfig.getFilePath(file, Directory.GetCurrentDirectory(), fileName);
			if (text.Length > 0)
			{
				return text;
			}
			if (!string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("TNS_ADMIN")))
			{
				text = ProviderConfig.getFilePath(file, Environment.GetEnvironmentVariable("TNS_ADMIN"), fileName);
				if (text.Length > 0)
				{
					return text;
				}
			}
			if (file == OraFiles.Ldap && !string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("LDAP_ADMIN")))
			{
				text = ProviderConfig.getFilePath(file, Environment.GetEnvironmentVariable("LDAP_ADMIN"), fileName);
				if (text.Length > 0)
				{
					return text;
				}
			}
			if (ConfigBaseClass.odpNetKey != null)
			{
				string text2 = ConfigBaseClass.odpNetKey.GetValue("TNS_ADMIN") as string;
				if (!string.IsNullOrWhiteSpace(text2))
				{
					text = ProviderConfig.getFilePath(file, text2, fileName);
					if (text.Length > 0)
					{
						return text;
					}
				}
			}
			if (!string.IsNullOrWhiteSpace(ProviderConfig.OracleHome))
			{
				text = ProviderConfig.getFilePath(file, Path.Combine(ProviderConfig.OracleHome, ProviderConfig.NETWORK_ADMIN), fileName);
				if (text.Length > 0)
				{
					return text;
				}
				if (file == OraFiles.Ldap)
				{
					text = ProviderConfig.FindFile(Path.Combine(ProviderConfig.OracleHome, ProviderConfig.LDAP_ADMIN_DIR), fileName);
				}
			}
			return text;
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x000410C0 File Offset: 0x0003F2C0
		private static string FindFile(string filePath, string fileName)
		{
			string text = string.Empty;
			if (!string.IsNullOrEmpty(filePath))
			{
				filePath.Trim();
				text = Path.Combine(filePath, fileName);
				if (!File.Exists(text))
				{
					text = string.Empty;
				}
			}
			return text;
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000708 RID: 1800 RVA: 0x000410FC File Offset: 0x0003F2FC
		private static string OracleHome
		{
			get
			{
				if (!ConfigBaseClass.m_OracleHomeSet)
				{
					ConfigBaseClass.m_OracleHome = Environment.GetEnvironmentVariable("ORACLE_HOME");
					if (!string.IsNullOrEmpty(ConfigBaseClass.m_OracleHome))
					{
						ConfigBaseClass.m_OracleHome.Trim();
					}
					else
					{
						ConfigBaseClass.m_OracleHome = string.Empty;
					}
					ConfigBaseClass.m_OracleHomeSet = true;
				}
				return ConfigBaseClass.m_OracleHome;
			}
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x00041150 File Offset: 0x0003F350
		internal void ParseConfigParamsForODT(XmlNode baseNode, ref Hashtable storedProcInformation)
		{
			ArrayList filterNodes = new ArrayList
			{
				"edmMappings",
				"implicitRefCursor"
			};
			ArrayList arrayList = new ArrayList();
			CustomConfigFileReader customConfigFileReader = ConfigBaseClass.GetInstance(true) as CustomConfigFileReader;
			customConfigFileReader.ParseClientXmlNode(baseNode, ref storedProcInformation, ref arrayList, filterNodes);
			for (int i = 0; i < arrayList.Count; i++)
			{
				customConfigFileReader.ParseSubSection((XmlNode)arrayList[i], ref storedProcInformation, filterNodes);
			}
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x000411C4 File Offset: 0x0003F3C4
		[ReflectionPermission(SecurityAction.Assert, Unrestricted = true)]
		internal static void ValidateBaseDocument(XmlDocument doc)
		{
			doc.Schemas.Add(null, new XmlTextReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("Oracle.ManagedDataAccess.src.Common.Resources.Oracle.DataAccess.Common.Configuration.Section.xsd")));
			doc.Schemas.Add(null, new XmlTextReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("Oracle.ManagedDataAccess.src.Common.Resources.Oracle.ManagedDataAccess.Client.Configuration.Section.xsd")));
			ValidationEventHandler validationEventHandler = new ValidationEventHandler(ProviderConfig.ValidationCallBack);
			doc.Validate(validationEventHandler);
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x00041228 File Offset: 0x0003F428
		private static void ValidationCallBack(object sender, ValidationEventArgs args)
		{
			if (args.Severity != XmlSeverityType.Warning)
			{
				throw new ConfigurationErrorsException(args.Message);
			}
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Error, new string[]
				{
					"Warning: Matching schema not found.  No validation occurred." + args.Message
				});
				return;
			}
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x00041278 File Offset: 0x0003F478
		internal static string GetPropertyFromONSConfig(string ONSConfigFile, string onsConfigProperty)
		{
			CustomConfigFileReader customConfigFileReader = ConfigBaseClass.GetInstance(true) as CustomConfigFileReader;
			return customConfigFileReader.GetPropertyFromONSConfig(ONSConfigFile, onsConfigProperty);
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x0004129C File Offset: 0x0003F49C
		private static void AddInfoForRefCursor(string storedProcKey, XmlNode refCursorNode, ref Hashtable storedProcInformation)
		{
			if (refCursorNode.Attributes != null)
			{
				bool isInfoBasedOnName = false;
				RefCursorInfo refCursorInfo = new RefCursorInfo();
				XmlAttribute xmlAttribute = refCursorNode.Attributes["name"];
				XmlAttribute xmlAttribute2 = refCursorNode.Attributes["position"];
				string s;
				string name;
				if (xmlAttribute2 != null && !string.IsNullOrWhiteSpace(s = xmlAttribute2.Value.Trim()))
				{
					refCursorInfo.position = int.Parse(s);
					isInfoBasedOnName = false;
				}
				else if (xmlAttribute != null && !string.IsNullOrWhiteSpace(name = xmlAttribute.Value.Trim()))
				{
					refCursorInfo.name = name;
					isInfoBasedOnName = true;
					refCursorInfo.position = -1;
				}
				else
				{
					string messageForTrace = "Neither RefCursor name nor position is present in " + storedProcKey;
					string messageForException = storedProcKey + "  refCursor";
					ProviderConfig.ThrowExceptionForRefCursor(messageForTrace, messageForException);
				}
				using (IEnumerator enumerator = refCursorNode.ChildNodes.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						XmlNode xmlNode = (XmlNode)obj;
						string name2;
						if ((name2 = xmlNode.Name) != null)
						{
							if (!(name2 == "bindInfo"))
							{
								if (name2 == "metadata")
								{
									ProviderConfig.AddMetadataForRefCursor(storedProcKey, xmlNode, ref refCursorInfo, isInfoBasedOnName, ref storedProcInformation);
								}
							}
							else
							{
								ProviderConfig.AddBindInfoForRefCursor(storedProcKey, xmlNode, ref refCursorInfo, isInfoBasedOnName, ref storedProcInformation);
							}
						}
					}
					return;
				}
			}
			string messageForTrace2 = "Neither RefCursor name nor position is present in stored procedure " + storedProcKey;
			string messageForException2 = storedProcKey + "  refCursor";
			ProviderConfig.ThrowExceptionForRefCursor(messageForTrace2, messageForException2);
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x0004140C File Offset: 0x0003F60C
		private static void AddBindInfoForRefCursor(string storedProcKey, XmlNode bindInfoNode, ref RefCursorInfo refCursorInfo, bool isInfoBasedOnName, ref Hashtable storedProcInformation)
		{
			string value = bindInfoNode.Attributes["mode"].Value;
			refCursorInfo.mode = (ParameterDirection)Enum.Parse(typeof(ParameterDirection), value, true);
			ConfigBaseClass.StoredProcedureInfo storedProcedureInfo = (ConfigBaseClass.StoredProcedureInfo)storedProcInformation[storedProcKey];
			int num = 0;
			foreach (RefCursorInfo refCursorInfo2 in storedProcedureInfo.m_refCursors)
			{
				if (isInfoBasedOnName)
				{
					if (refCursorInfo2.name.Length > 0 && refCursorInfo2.name.Equals(refCursorInfo.name))
					{
						storedProcedureInfo.m_refCursors.RemoveAt(num);
						break;
					}
				}
				else if (refCursorInfo2.position >= refCursorInfo.position)
				{
					if (refCursorInfo2.position == refCursorInfo.position)
					{
						storedProcedureInfo.m_refCursors.RemoveAt(num);
					}
					storedProcedureInfo.m_refCursors.Insert(num, refCursorInfo);
					return;
				}
				num++;
			}
			storedProcedureInfo.m_refCursors.Add(refCursorInfo);
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x00041520 File Offset: 0x0003F720
		private static void AddMetadataForRefCursor(string storedProcKey, XmlNode metadataNode, ref RefCursorInfo refCursorInfo, bool isInfoBasedOnName, ref Hashtable storedProcInformation)
		{
			string s = metadataNode.Attributes["columnOrdinal"].Value.Trim();
			int num = int.Parse(s);
			ConfigBaseClass.StoredProcedureInfo storedProcedureInfo = (ConfigBaseClass.StoredProcedureInfo)storedProcInformation[storedProcKey];
			DataRow dataRow = refCursorInfo.columnInfo.NewRow();
			CustomConfigFileReader customConfigFileReader = ConfigBaseClass.GetInstance(true) as CustomConfigFileReader;
			foreach (object obj in metadataNode.Attributes)
			{
				XmlAttribute xmlAttribute = (XmlAttribute)obj;
				string text = xmlAttribute.Value.Trim();
				string key;
				switch (key = xmlAttribute.Name.ToUpperInvariant())
				{
				case "COLUMNORDINAL":
					dataRow["ColumnOrdinal"] = num;
					break;
				case "COLUMNNAME":
					dataRow["ColumnName"] = customConfigFileReader.GetAttrValueInProperCase(text);
					break;
				case "COLUMNSIZE":
					dataRow["ColumnSize"] = int.Parse(text);
					break;
				case "NUMERICPRECISION":
					dataRow["NumericPrecision"] = int.Parse(text);
					break;
				case "NUMERICSCALE":
					dataRow["NumericScale"] = int.Parse(text);
					break;
				case "ISUNIQUE":
					dataRow["IsUnique"] = bool.Parse(text);
					break;
				case "ISKEY":
					dataRow["IsKey"] = bool.Parse(text);
					if ((bool)dataRow["IsKey"])
					{
						refCursorInfo.isPrimaryKeyPresent = true;
					}
					break;
				case "ISROWID":
					dataRow["IsRowID"] = bool.Parse(text);
					break;
				case "BASECOLUMNNAME":
					dataRow["BaseColumnName"] = customConfigFileReader.GetAttrValueInProperCase(text);
					break;
				case "BASESCHEMANAME":
					dataRow["BaseSchemaName"] = customConfigFileReader.GetAttrValueInProperCase(text);
					break;
				case "BASETABLENAME":
					dataRow["BaseTableName"] = customConfigFileReader.GetAttrValueInProperCase(text);
					break;
				case "DATATYPE":
					dataRow["DataType"] = Type.GetType(text);
					break;
				case "PROVIDERTYPE":
					dataRow["ProviderType"] = (OracleDbType)Enum.Parse(typeof(OracleDbType), text.Split(new char[]
					{
						'.'
					})[text.Split(new char[]
					{
						'.'
					}).Length - 1], true);
					break;
				case "ALLOWDBNULL":
					dataRow["AllowDBNull"] = bool.Parse(text);
					break;
				case "ISALIASED":
					dataRow["IsAliased"] = bool.Parse(text);
					break;
				case "ISBYTESEMANTIC":
					dataRow["IsByteSemantic"] = bool.Parse(text);
					break;
				case "ISEXPRESSION":
					dataRow["IsExpression"] = bool.Parse(text);
					break;
				case "ISHIDDEN":
					dataRow["IsHidden"] = bool.Parse(text);
					break;
				case "ISREADONLY":
					dataRow["IsReadOnly"] = bool.Parse(text);
					break;
				case "ISLONG":
					dataRow["IsLong"] = bool.Parse(text);
					break;
				case "UDTTYPENAME":
					dataRow["UdtTypeName"] = customConfigFileReader.GetAttrValueInProperCase(text);
					break;
				case "NATIVEDATATYPE":
					dataRow["NativeDataType"] = customConfigFileReader.GetAttrValueInProperCase(text);
					break;
				case "PROVIDERDBTYPE":
					dataRow["ProviderDBType"] = (DbType)Enum.Parse(typeof(DbType), text.Split(new char[]
					{
						'.'
					})[text.Split(new char[]
					{
						'.'
					}).Length - 1], true);
					break;
				case "OBJECTNAME":
					dataRow["ObjectName"] = customConfigFileReader.GetAttrValueInProperCase(text);
					break;
				}
			}
			if (refCursorInfo.columnInfo.Rows.Count > num)
			{
				refCursorInfo.columnInfo.Rows.InsertAt(dataRow, num);
			}
			else
			{
				refCursorInfo.columnInfo.Rows.Add(dataRow);
			}
			refCursorInfo.columnInfo.AcceptChanges();
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x00041B18 File Offset: 0x0003FD18
		private static void ThrowExceptionForRefCursor(string messageForTrace, string messageForException)
		{
			if (ProviderConfig.m_bTraceLevelConfig)
			{
				Trace.Write(OracleTraceLevel.Config, OracleTraceTag.REFCursor, new string[]
				{
					messageForTrace
				});
			}
			throw new ConfigurationErrorsException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.ODP_INVALID_VALUE, new string[]
			{
				messageForException
			}));
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x00041B64 File Offset: 0x0003FD64
		internal static string GetONSConfiguration(string databaseName)
		{
			if (ConfigBaseClass.m_ONSMapping == null || !ConfigBaseClass.m_ONSMapping.ContainsKey(databaseName.ToLowerInvariant()))
			{
				return string.Empty;
			}
			Dictionary<string, string> dictionary = ConfigBaseClass.m_ONSMapping[databaseName.ToLowerInvariant()];
			StringBuilder stringBuilder = new StringBuilder();
			string text = dictionary["nodeList"];
			if (text != null)
			{
				stringBuilder.AppendFormat("nodes.list={0}", text);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x00041BCC File Offset: 0x0003FDCC
		[FileIOPermission(SecurityAction.Assert, Unrestricted = true)]
		[EnvironmentPermission(SecurityAction.Assert, Unrestricted = true)]
		internal static void TraceConfigAndEnvParams()
		{
			Trace.Write(OracleTraceLevel.Config, OracleTraceTag.Environment, new string[]
			{
				"Machine Name : " + Environment.MachineName
			});
			Trace.Write(OracleTraceLevel.Config, OracleTraceTag.Environment, new string[]
			{
				"User Name : " + Environment.UserName
			});
			Trace.Write(OracleTraceLevel.Config, OracleTraceTag.Environment, new string[]
			{
				"OS Version : " + Environment.OSVersion
			});
			Trace.Write(OracleTraceLevel.Config, OracleTraceTag.Environment, new string[]
			{
				"64-bit OS : " + Environment.Is64BitOperatingSystem.ToString()
			});
			Trace.Write(OracleTraceLevel.Config, OracleTraceTag.Environment, new string[]
			{
				"64-bit Process : " + Environment.Is64BitProcess.ToString()
			});
			Trace.Write(OracleTraceLevel.Config, OracleTraceTag.Environment, new string[]
			{
				".NET Runtime Version : " + Environment.Version
			});
			Trace.Write(OracleTraceLevel.Config, OracleTraceTag.Environment, new string[]
			{
				"Application Directory : " + ConfigBaseClass.s_appDir
			});
			string str = ConfigBaseClass.m_assemblyVersion.ToString();
			Trace.Write(OracleTraceLevel.Config, OracleTraceTag.Version, new string[]
			{
				"Oracle Data Provider for .NET, Managed Driver Version : " + str
			});
			try
			{
				Assembly executingAssembly = Assembly.GetExecutingAssembly();
				FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(executingAssembly.Location);
				Trace.Write(OracleTraceLevel.Config, OracleTraceTag.Version, new string[]
				{
					"Oracle Data Provider for .NET, Managed Driver Informational Version : " + versionInfo.ProductVersion
				});
			}
			catch
			{
				Trace.Write(OracleTraceLevel.Config, OracleTraceTag.Version, new string[]
				{
					"Oracle Data Provider for .NET, Managed Driver Informational Version : Unable to retrieve."
				});
			}
			foreach (object obj in ConfigBaseClass.m_configParameters.Keys)
			{
				string text = (string)obj;
				Trace.Write(OracleTraceLevel.Config, OracleTraceTag.Config, new string[]
				{
					text + " : " + (string)ConfigBaseClass.m_configParameters[text]
				});
			}
			Trace.Write(OracleTraceLevel.Config, OracleTraceTag.Config, new string[]
			{
				"Resolved Trace File Location: " + ConfigBaseClass.m_singleTraceFileLocation
			});
			CustomConfigFileReader customConfigFileReader = ConfigBaseClass.GetInstance(true) as CustomConfigFileReader;
			if (customConfigFileReader != null)
			{
				ProviderConfig.TraceImplicitRefCursorParams(ref customConfigFileReader.s_storedProcInformation, false);
			}
			Trace.Write(OracleTraceLevel.Config, OracleTraceTag.Sqlnet, new string[]
			{
				"FilePath : " + (string.IsNullOrEmpty(ConfigBaseClass.m_sqlnetOraLoc) ? "(null)" : ConfigBaseClass.m_sqlnetOraLoc)
			});
			Trace.Write(OracleTraceLevel.Config, OracleTraceTag.Tnsnames, new string[]
			{
				"FilePath : " + (string.IsNullOrEmpty(ConfigBaseClass.m_TNSNamesoraloc) ? "(null)" : ConfigBaseClass.m_TNSNamesoraloc)
			});
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x00041EE0 File Offset: 0x000400E0
		private static void TraceImplicitRefCursorParams(ref Hashtable storedProcInformation, bool isODTCall)
		{
			if (storedProcInformation.Keys.Count > 0)
			{
				foreach (object obj in storedProcInformation.Keys)
				{
					string text = (string)obj;
					StringBuilder stringBuilder = new StringBuilder();
					List<RefCursorInfo> refCursors = ((ConfigBaseClass.StoredProcedureInfo)storedProcInformation[text]).m_refCursors;
					foreach (RefCursorInfo refCursorInfo in refCursors)
					{
						if (isODTCall)
						{
							stringBuilder.Append("Design-time Implicit Binding Info : [" + text + "]");
						}
						else
						{
							stringBuilder.Append("Run-time Implicit Binding Info : [" + text + "]");
						}
						stringBuilder.Append("[param name/pos=" + ((refCursorInfo.name == string.Empty) ? refCursorInfo.position.ToString() : refCursorInfo.name) + ";");
						stringBuilder.Append("mode=" + refCursorInfo.mode + "] Metadata : ");
						string value = stringBuilder.ToString();
						if (refCursorInfo.columnInfo == null)
						{
							stringBuilder.Append("[<none>])");
							Trace.Write(OracleTraceLevel.Config, OracleTraceTag.REFCursor, new string[]
							{
								stringBuilder.ToString()
							});
						}
						else
						{
							DataTable columnInfo = refCursorInfo.columnInfo;
							for (int i = 0; i < columnInfo.Rows.Count; i++)
							{
								stringBuilder.Append("[");
								for (int j = 0; j < columnInfo.Columns.Count; j++)
								{
									stringBuilder.Append(string.Concat(new object[]
									{
										(j != 0) ? ";" : string.Empty,
										columnInfo.Columns[j].ToString(),
										"=",
										columnInfo.Rows[i][j]
									}));
								}
								stringBuilder.Append("])");
								Trace.Write(OracleTraceLevel.Config, OracleTraceTag.REFCursor, new string[]
								{
									stringBuilder.ToString()
								});
								stringBuilder.Length = 0;
								stringBuilder.Append(value);
							}
							stringBuilder.Length = 0;
						}
					}
				}
			}
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x00042190 File Offset: 0x00040390
		internal override void setudtmapping(out Hashtable s_mapUdtNameToMappingObj)
		{
			s_mapUdtNameToMappingObj = null;
		}

		// Token: 0x04000956 RID: 2390
		private const string ORA_DEBUG_JDWP = "ORA_DEBUG_JDWP";

		// Token: 0x04000957 RID: 2391
		private const string TNS_ADMIN = "TNS_ADMIN";

		// Token: 0x04000958 RID: 2392
		private const string LDAP_ADMIN = "LDAP_ADMIN";

		// Token: 0x04000959 RID: 2393
		private const string ORACLE_HOME = "ORACLE_HOME";

		// Token: 0x0400095A RID: 2394
		private const string LDAPORA = "ldap.ora";

		// Token: 0x0400095B RID: 2395
		private const string TNSNAMESORA = "tnsnames.ora";

		// Token: 0x0400095C RID: 2396
		private const string SQLNETORA = "sqlnet.ora";

		// Token: 0x0400095D RID: 2397
		private static readonly string NETWORK_ADMIN = Path.Combine("network", "admin");

		// Token: 0x0400095E RID: 2398
		private static readonly string LDAP_ADMIN_DIR = Path.Combine("ldap", "admin");

		// Token: 0x0400095F RID: 2399
		internal static bool m_bTraceLevelPublic;

		// Token: 0x04000960 RID: 2400
		internal static bool m_bTraceLevelPrivate;

		// Token: 0x04000961 RID: 2401
		internal static bool m_bTraceLevelNetwork;

		// Token: 0x04000962 RID: 2402
		internal static bool m_bTraceLevelConfig;

		// Token: 0x04000963 RID: 2403
		internal static bool m_bTraceLevelPrivate_NoTrace = false;

		// Token: 0x04000964 RID: 2404
		private static string s_appConfigFilePath = null;

		// Token: 0x020000B1 RID: 177
		internal static class MaxStatementCacheSize
		{
			// Token: 0x170001B7 RID: 439
			// (get) Token: 0x06000716 RID: 1814 RVA: 0x000421A0 File Offset: 0x000403A0
			internal static int Value
			{
				get
				{
					if (ConfigBaseClass.m_MaxStatementCacheSize != -1)
					{
						return ConfigBaseClass.m_MaxStatementCacheSize;
					}
					return 200;
				}
			}

			// Token: 0x170001B8 RID: 440
			// (get) Token: 0x06000717 RID: 1815 RVA: 0x000421B8 File Offset: 0x000403B8
			internal static bool IsUserDefined
			{
				get
				{
					return ConfigBaseClass.m_MaxStatementCacheSize != -1;
				}
			}

			// Token: 0x04000965 RID: 2405
			private const int DEFAULT_MAX_STATEMENT_CACHE_SIZE = 200;
		}
	}
}
