using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Permissions;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using Microsoft.Win32;
using Oracle.ManagedDataAccess.Client;

namespace OracleInternal.Common
{
	// Token: 0x0200003C RID: 60
	internal class CustomConfigFileReader : ConfigBaseClass
	{
		// Token: 0x060002D2 RID: 722 RVA: 0x00010914 File Offset: 0x0000EB14
		static CustomConfigFileReader()
		{
			AppDomain.CurrentDomain.AssemblyResolve += CustomConfigFileReader.LoadODPMDLL;
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0001092C File Offset: 0x0000EB2C
		[RegistryPermission(SecurityAction.Assert, Unrestricted = true)]
		public CustomConfigFileReader(bool bIsManaged = false)
		{
			ConfigBaseClass.m_bIsManaged = bIsManaged;
			ConfigBaseClass.m_assemblyVersion = this.GetExecutingAssemblyVersion();
			CustomConfigFileReader.GetProcessAndEnvInfo();
			ConfigBaseClass.m_ParseMode = ParseMode.FirstParse;
			RegistryKey localMachine = Registry.LocalMachine;
			string name;
			if (ConfigBaseClass.m_bIsManaged)
			{
				name = "SOFTWARE\\Oracle\\ODP.NET.Managed";
			}
			else
			{
				name = "SOFTWARE\\Oracle\\ODP.NET";
			}
			try
			{
				RegistryKey registryKey = localMachine.OpenSubKey(name);
				if (registryKey != null)
				{
					string[] subKeyNames = registryKey.GetSubKeyNames();
					string text = ConfigBaseClass.m_assemblyVersion.ToString();
					for (int i = 0; i < subKeyNames.Length; i++)
					{
						if (text == subKeyNames[i])
						{
							ConfigBaseClass.odpNetKey = registryKey.OpenSubKey(text);
						}
					}
				}
			}
			catch
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Config, OracleTraceTag.Environment, new string[]
					{
						"Unable to read from registry."
					});
				}
			}
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x00010A34 File Offset: 0x0000EC34
		[EnvironmentPermission(SecurityAction.Assert, Unrestricted = true)]
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		private static void GetProcessAndEnvInfo()
		{
			ConfigBaseClass.CurrentProcess = Process.GetCurrentProcess();
			ConfigBaseClass.m_recoveryServiceHost = Environment.MachineName;
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x00010A4C File Offset: 0x0000EC4C
		[FileIOPermission(SecurityAction.Assert, Unrestricted = true)]
		private Version GetExecutingAssemblyVersion()
		{
			return Assembly.GetExecutingAssembly().GetName().Version;
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x00010A60 File Offset: 0x0000EC60
		private static Assembly LoadODPMDLL(object sender, ResolveEventArgs args)
		{
			string name = new AssemblyName(args.Name).Name;
			if (name == "Oracle.ManagedDataAccess")
			{
				Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
				foreach (Assembly assembly in assemblies)
				{
					if (assembly.FullName.Equals(args.Name))
					{
						return assembly;
					}
				}
			}
			else if (name == "Oracle.ManagedDataAccessDTC" || name == "Oracle.ManagedDataAccessIOP")
			{
				Assembly assembly2 = null;
				string text = name;
				string path;
				if (IntPtr.Size == 8)
				{
					path = "x64";
				}
				else
				{
					path = "x86";
				}
				try
				{
					string directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
					string path2 = Path.Combine(directoryName, path);
					string uriString = Path.Combine(path2, name + ".dll");
					text = new Uri(uriString).LocalPath;
					assembly2 = Assembly.LoadFrom(text);
				}
				catch (Exception ex)
				{
					if (ProviderConfig.m_bTraceLevelPublic)
					{
						Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[]
						{
							string.Concat(new string[]
							{
								"AssemblyResolve(",
								text,
								") failed with [",
								ex.ToString(),
								"]"
							})
						});
					}
				}
				if (assembly2 != null)
				{
					return assembly2;
				}
			}
			return null;
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x00010BD8 File Offset: 0x0000EDD8
		internal string[] GetName(StreamReader strmRdr, out string leftover)
		{
			string[] result = null;
			int num = -1;
			leftover = "";
			while (num == -1 && !strmRdr.EndOfStream)
			{
				string text = strmRdr.ReadLine().Trim();
				if (text.Length <= 0 || text[0] != '#')
				{
					num = text.IndexOf('=');
					if (num != -1)
					{
						result = (from s in text.Substring(0, num).Split(new char[]
						{
							','
						})
						select s.Trim() into s
						where s != string.Empty
						select s).ToArray<string>();
						leftover = text.Substring(num + 1);
					}
				}
			}
			return result;
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00010CA4 File Offset: 0x0000EEA4
		internal string GetValue(StreamReader strmRdr, string leftover)
		{
			bool flag = false;
			string text = "";
			int num = 0;
			try
			{
				string text2 = leftover.Trim();
				while ((text2.Length == 0 || text2[0] == '#') && !strmRdr.EndOfStream)
				{
					text2 = strmRdr.ReadLine().Trim();
				}
				if (text2.Contains('#'))
				{
					int num2 = text2.IndexOf('#');
					if (num2 != -1)
					{
						text2 = text2.Substring(0, num2);
						text2.Trim();
					}
				}
				if (text2.Length <= 0 || text2[0] != '(')
				{
					return text2.Trim();
				}
				for (;;)
				{
					if (text2.Length > 0 && text2[0] != '#')
					{
						flag = false;
						int i;
						for (i = 0; i < text2.Length; i++)
						{
							if (flag)
							{
								flag = false;
							}
							else
							{
								char c = text2[i];
								if (c == '(')
								{
									num++;
								}
								else if (c == ')')
								{
									num--;
								}
								else if (c == '#')
								{
									text2 = text2.Remove(i--);
								}
								else if (c == '\\')
								{
									if (i + 1 == text2.Length)
									{
										text2 = text2.Remove(i--, 1);
										flag = true;
									}
									else if (ConfigBaseClass.m_parens.Contains(text2[i + 1]))
									{
										text2 = text2.Remove(i--, 1);
									}
									else
									{
										flag = true;
									}
								}
								if (num == 0 && i + 1 != text2.Length)
								{
									string text3 = text2.Substring(i + 1, text2.Length - (i + 1)).Trim();
									if (!string.IsNullOrEmpty(text3) && text3[0] != '#' && text3[0] != ')')
									{
										goto Block_20;
									}
									text2 = text2.Remove(i + 1);
								}
							}
						}
						text += text2.Substring(0, i);
					}
					int num3;
					if (num == 0 || (num3 = strmRdr.Peek()) == -1 || (!ConfigBaseClass.m_allowedCont.Contains((char)num3) && ((ushort)num3 != 41 || num != 1) && !flag) || strmRdr.EndOfStream || (text2 = strmRdr.ReadLine().Trim()).Length < 0)
					{
						goto IL_203;
					}
				}
				Block_20:
				return null;
				IL_203:;
			}
			catch (Exception)
			{
			}
			return text;
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00010EDC File Offset: 0x0000F0DC
		[EnvironmentPermission(SecurityAction.Assert, Unrestricted = true)]
		[FileIOPermission(SecurityAction.Assert, Unrestricted = true)]
		internal override void ParseConfigFile()
		{
			this.ParseAndCacheConfigParams();
			FieldInfo[] fields = typeof(ConfigBaseClass).GetFields(BindingFlags.Static | BindingFlags.NonPublic);
			foreach (FieldInfo fieldInfo in fields)
			{
				ConfigurationAttribute configurationAttribute = Attribute.GetCustomAttribute(fieldInfo, typeof(ConfigurationAttribute)) as ConfigurationAttribute;
				if (configurationAttribute != null)
				{
					string text = (string)ConfigBaseClass.m_configParameters[configurationAttribute.ConfigEntry];
					if (text != null && text.Length != 0)
					{
						if (fieldInfo.FieldType == typeof(ushort))
						{
							fieldInfo.SetValue(null, ushort.Parse(text));
						}
						else if (fieldInfo.FieldType == typeof(int))
						{
							fieldInfo.SetValue(null, int.Parse(text));
						}
						else if (fieldInfo.FieldType == typeof(uint))
						{
							fieldInfo.SetValue(null, uint.Parse(text));
						}
						else
						{
							if (fieldInfo.FieldType == typeof(string))
							{
								fieldInfo.SetValue(null, text);
								if (!(fieldInfo.Name == "m_serviceRelocationTimeout"))
								{
									goto IL_37B;
								}
								try
								{
									string[] array2 = ConfigBaseClass.m_serviceRelocationTimeout.Split(new char[]
									{
										'+'
									});
									if (array2.Length == 1)
									{
										array2[0] = array2[0].Trim();
										try
										{
											if (string.Equals(array2[0], "drain_timeout", StringComparison.InvariantCultureIgnoreCase))
											{
												ConfigBaseClass.s_bDrainTimeoutInSRCT = true;
											}
											else
											{
												ConfigBaseClass.srctOffset = Convert.ToInt32(array2[0]);
											}
											ConfigBaseClass.s_bFromConfigSRCT = true;
											goto IL_219;
										}
										catch
										{
											throw;
										}
									}
									if (array2.Length != 2)
									{
										throw new Exception();
									}
									array2[0] = array2[0].Trim();
									array2[1] = array2[1].Trim();
									if (string.Equals(array2[0], "drain_timeout", StringComparison.InvariantCultureIgnoreCase))
									{
										ConfigBaseClass.s_bDrainTimeoutInSRCT = true;
										ConfigBaseClass.s_bFromConfigSRCT = true;
										ConfigBaseClass.srctOffset = Convert.ToInt32(array2[1]);
									}
									else
									{
										if (!string.Equals(array2[1], "drain_timeout", StringComparison.InvariantCultureIgnoreCase))
										{
											throw new Exception();
										}
										ConfigBaseClass.s_bDrainTimeoutInSRCT = true;
										ConfigBaseClass.s_bFromConfigSRCT = true;
										ConfigBaseClass.srctOffset = Convert.ToInt32(array2[0]);
									}
									IL_219:
									goto IL_37B;
								}
								catch (Exception)
								{
									throw new OracleException(ResourceStringConstants.CON_STR_INVALID_VALUE, string.Empty, string.Empty, OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_STR_INVALID_VALUE, new string[]
									{
										configurationAttribute.ConfigEntry,
										text
									}));
								}
							}
							if (fieldInfo.FieldType == typeof(bool))
							{
								try
								{
									fieldInfo.SetValue(null, this.ParseBooleanConfigValue(text));
									goto IL_37B;
								}
								catch (Exception)
								{
									throw new OracleException(ResourceStringConstants.CON_STR_INVALID_VALUE, string.Empty, string.Empty, OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_STR_INVALID_VALUE, new string[]
									{
										configurationAttribute.ConfigEntry,
										text
									}));
								}
							}
							if (fieldInfo.FieldType == typeof(PromotableTransaction))
							{
								if (string.Equals(text, "local", StringComparison.OrdinalIgnoreCase))
								{
									fieldInfo.SetValue(null, PromotableTransaction.Local);
								}
								else if (string.Equals(text, "promotable", StringComparison.OrdinalIgnoreCase))
								{
									fieldInfo.SetValue(null, PromotableTransaction.Promotable);
								}
							}
							else
							{
								if (!(fieldInfo.FieldType == typeof(TimeSpan)))
								{
									throw new OracleException(ResourceStringConstants.CON_STR_INVALID_VALUE, string.Empty, string.Empty, OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_STR_INVALID_VALUE, new string[]
									{
										configurationAttribute.ConfigEntry,
										text
									}));
								}
								fieldInfo.SetValue(null, TimeSpan.FromSeconds(double.Parse(text)));
							}
						}
					}
				}
				IL_37B:;
			}
			if (ConfigBaseClass.m_TraceLevel < 0)
			{
				ConfigBaseClass.m_TraceLevel = 0;
			}
			if (ConfigBaseClass.m_TraceOption < 0)
			{
				ConfigBaseClass.m_TraceOption = 0;
			}
			ConfigBaseClass.m_sqlnetOraLoc = ProviderConfig.NewOraFileLoc(OraFiles.SqlNet);
			ProviderConfig.NewOraFileParams(OraFiles.SqlNet, ConfigBaseClass.m_sqlnetOraLoc, ConfigBaseClass.m_configParameters);
			try
			{
				if (!string.IsNullOrEmpty((string)ConfigBaseClass.m_configParameters["sqlnet.kerberos5_conf"]))
				{
					Environment.SetEnvironmentVariable("KRB5_CONFIG", (string)ConfigBaseClass.m_configParameters["sqlnet.kerberos5_conf"]);
				}
			}
			catch
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Config, OracleTraceTag.Environment, new string[]
					{
						"KRB5_CONFIG : Unable to set environment variable."
					});
				}
			}
			OraclePermission.m_startTracing = true;
			if (ConfigBaseClass.s_bLegacyEdmMappingPresent)
			{
				this.ValidateEdmMapping();
			}
			if (ConfigBaseClass.m_TraceLevel > 0)
			{
				ProviderConfig.TraceConfigAndEnvParams();
			}
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00011364 File Offset: 0x0000F564
		[ConfigurationPermission(SecurityAction.Assert, Unrestricted = true)]
		private void ParseAndCacheConfigParams()
		{
			if (ConfigBaseClass.m_bIsManaged)
			{
				ConfigurationManager.GetSection("oracle.manageddataaccess.client");
			}
			else
			{
				ConfigurationManager.GetSection("oracle.unmanageddataaccess.client");
			}
			for (int i = 0; i < ConfigBaseClass.m_versionSpecificNodesList.Count; i++)
			{
				this.ParseSubSection((XmlNode)ConfigBaseClass.m_versionSpecificNodesList[i], ref this.s_storedProcInformation, null);
			}
			if (ConfigBaseClass.m_bIsManaged)
			{
				ConfigBaseClass.m_versionSpecificNodesList.Clear();
				ConfigBaseClass.m_ParseMode = ParseMode.None;
			}
		}

		// Token: 0x060002DB RID: 731 RVA: 0x000113DC File Offset: 0x0000F5DC
		internal void ParseSubSection(XmlNode subSectionNode, ref Hashtable schemaTable, ArrayList filterNodes = null)
		{
			if (subSectionNode != null)
			{
				bool flag = filterNodes != null && filterNodes.Count > 0;
				foreach (object obj in subSectionNode.ChildNodes)
				{
					XmlNode xmlNode = (XmlNode)obj;
					string name = xmlNode.Name;
					string key;
					if ((!flag || filterNodes.Contains(name)) && (key = name) != null)
					{
						if (<PrivateImplementationDetails>{28A9BD3B-E95E-447F-A7DB-0C43D6EA795F}.$$method0x60002d4-1 == null)
						{
							<PrivateImplementationDetails>{28A9BD3B-E95E-447F-A7DB-0C43D6EA795F}.$$method0x60002d4-1 = new Dictionary<string, int>(8)
							{
								{
									"dataSources",
									0
								},
								{
									"onsConfig",
									1
								},
								{
									"LDAPsettings",
									2
								},
								{
									"settings",
									3
								},
								{
									"edmMappings",
									4
								},
								{
									"implicitRefCursor",
									5
								},
								{
									"distributedTransaction",
									6
								},
								{
									"connectionPools",
									7
								}
							};
						}
						int num;
						if (<PrivateImplementationDetails>{28A9BD3B-E95E-447F-A7DB-0C43D6EA795F}.$$method0x60002d4-1.TryGetValue(key, out num))
						{
							switch (num)
							{
							case 0:
								this.ParseDataSourcesElement(xmlNode);
								break;
							case 1:
								this.ParseONSConfigElement(xmlNode);
								break;
							case 2:
								this.ParseLDAPsettingsElement(xmlNode);
								break;
							case 3:
								this.ParseSettingsElement(xmlNode);
								break;
							case 4:
								this.ParseEdmMappingsElement(xmlNode);
								break;
							case 5:
								this.ParseImpRefCursorElement(xmlNode, ref schemaTable);
								break;
							case 6:
								this.ParseDistTxnElement(xmlNode);
								break;
							case 7:
								this.ParseConnectionPoolsElement(xmlNode);
								break;
							}
						}
					}
				}
			}
		}

		// Token: 0x060002DC RID: 732 RVA: 0x00011570 File Offset: 0x0000F770
		private void ParseConnectionPoolsElement(XmlNode connectionPoolsNode)
		{
			if (connectionPoolsNode != null)
			{
				foreach (object obj in connectionPoolsNode.ChildNodes)
				{
					XmlNode xmlNode = (XmlNode)obj;
					if (xmlNode.Attributes != null)
					{
						string key = xmlNode.Attributes["connectionString"].Value.Trim();
						ConfigBaseClass.m_connectionPoolNameMapping[key] = xmlNode.Attributes["poolName"].Value.Trim();
					}
				}
			}
		}

		// Token: 0x060002DD RID: 733 RVA: 0x00011610 File Offset: 0x0000F810
		private bool ParseBooleanConfigValue(string configValue)
		{
			int num;
			bool result;
			if (int.TryParse(configValue, out num))
			{
				result = (num != 0);
			}
			else
			{
				result = bool.Parse(configValue);
			}
			return result;
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0001163C File Offset: 0x0000F83C
		private void ParseDataSourcesElement(XmlNode dataSourceNode)
		{
			if (dataSourceNode != null)
			{
				foreach (object obj in dataSourceNode.ChildNodes)
				{
					XmlNode xmlNode = (XmlNode)obj;
					if (xmlNode.Attributes != null)
					{
						string[] array = (from s in xmlNode.Attributes["alias"].Value.Trim().Split(new char[]
						{
							','
						})
						select s.Trim() into s
						where s != string.Empty
						select s).ToArray<string>();
						foreach (string key in array)
						{
							if (ConfigBaseClass.m_ParseMode == ParseMode.ReParseTnsNames)
							{
								if (!ConfigBaseClass.m_configDataSourcesMap.Contains(key))
								{
									ConfigBaseClass.m_configDataSourcesMap[key] = xmlNode.Attributes["descriptor"].Value.Trim();
								}
							}
							else
							{
								ConfigBaseClass.m_configDataSourcesMap[key] = xmlNode.Attributes["descriptor"].Value.Trim();
							}
						}
					}
				}
			}
		}

		// Token: 0x060002DF RID: 735 RVA: 0x000117B0 File Offset: 0x0000F9B0
		private void ParseDistTxnElement(XmlNode distTxnNode)
		{
			if (distTxnNode != null)
			{
				foreach (object obj in distTxnNode.ChildNodes)
				{
					XmlNode xmlNode = (XmlNode)obj;
					if (xmlNode.Attributes != null)
					{
						string a = xmlNode.Attributes["name"].Value.Trim();
						string text = xmlNode.Attributes["value"].Value.Trim();
						if (string.Equals(a, "omtsreco_ip_address", StringComparison.InvariantCultureIgnoreCase))
						{
							if (!string.IsNullOrEmpty(text))
							{
								ConfigBaseClass.m_recoveryServiceHost = text;
							}
						}
						else
						{
							if (string.Equals(a, "omtsreco_port", StringComparison.InvariantCultureIgnoreCase))
							{
								try
								{
									ConfigBaseClass.m_recoveryServicePort = ushort.Parse(text);
									continue;
								}
								catch
								{
									continue;
								}
							}
							if (string.Equals(a, "oramts_sess_txntimetolive", StringComparison.InvariantCultureIgnoreCase))
							{
								try
								{
									ConfigBaseClass.m_dtcTxnTimeout = uint.Parse(text);
									continue;
								}
								catch
								{
									continue;
								}
							}
							if (string.Equals(a, "UseManagedDTC", StringComparison.InvariantCultureIgnoreCase))
							{
								try
								{
									ConfigBaseClass.m_dtcUseDTCDLL = this.ParseBooleanConfigValue(text);
								}
								catch
								{
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x000118F0 File Offset: 0x0000FAF0
		private void ParseUdtMappingsElement(XmlNode udtMappingsNode)
		{
			foreach (object obj in udtMappingsNode.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.Attributes != null)
				{
					string text = xmlNode.Attributes["typeName"].Value.Trim();
					if (text != null && text.Length > 0)
					{
						NameValueCollection nameValueCollection = new NameValueCollection();
						string text2 = "typeName='" + text + "'";
						nameValueCollection["typeName"] = text;
						nameValueCollection["factoryName"] = xmlNode.Attributes["factoryName"].Value.Trim();
						if (xmlNode.Attributes["schemaName"] != null && xmlNode.Attributes["schemaName"].Value.Length > 0)
						{
							string text3 = xmlNode.Attributes["schemaName"].Value.Trim();
							nameValueCollection["schemaName"] = text3;
							text2 = "schemaName='" + text3 + "' " + text2;
						}
						if (xmlNode.Attributes["dataSource"] != null && xmlNode.Attributes["dataSource"].Value.Length > 0)
						{
							string text4 = xmlNode.Attributes["dataSource"].Value.Trim().ToUpper();
							nameValueCollection["dataSource"] = text4;
							text2 = "dataSource='" + text4 + "' " + text2;
						}
						this.mapUdtNameToMappingObjConfig[text2] = nameValueCollection;
					}
				}
			}
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x00011AD8 File Offset: 0x0000FCD8
		private void ParseImpRefCursorElement(XmlNode node, ref Hashtable schemaTable)
		{
			foreach (object obj in node.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.Name == "storedProcedure")
				{
					string value = xmlNode.Attributes["name"].Value.Trim();
					string text = null;
					if (xmlNode.Attributes["schema"] != null)
					{
						text = xmlNode.Attributes["schema"].Value.Trim();
					}
					string text2 = new StringBuilder().Append(string.IsNullOrEmpty(text) ? "" : text).Append(string.IsNullOrEmpty(text) ? "" : ".").Append(value).ToString();
					base.GetKeyInProperCase(ref text2);
					if (!schemaTable.Contains(text2))
					{
						ConfigBaseClass.StoredProcedureInfo value2 = new ConfigBaseClass.StoredProcedureInfo();
						schemaTable.Add(text2, value2);
					}
					foreach (object obj2 in xmlNode.ChildNodes)
					{
						XmlNode xmlNode2 = (XmlNode)obj2;
						if (xmlNode2.Name == "refCursor")
						{
							this.AddInfoForRefCursor(text2, xmlNode2, ref schemaTable);
						}
					}
				}
			}
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00011C78 File Offset: 0x0000FE78
		private void ParseLDAPsettingsElement(XmlNode settingsNode)
		{
			foreach (object obj in settingsNode.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.Attributes != null)
				{
					string key = xmlNode.Attributes["name"].Value.Trim();
					ConfigBaseClass.m_LDAPconfigParameters[key] = xmlNode.Attributes["value"].Value.Trim();
				}
			}
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x00011D14 File Offset: 0x0000FF14
		private void ParseONSConfigElement(XmlNode ONSConfigNode)
		{
			if (ONSConfigNode != null)
			{
				ConfigBaseClass.m_ONSMode = (ONSConfigMode)Enum.Parse(typeof(ONSConfigMode), ONSConfigNode.Attributes[ConfigInfo.ONSMode].Value.Trim());
				if (ONSConfigNode.Attributes[ConfigInfo.ONSConfigFile] != null)
				{
					ConfigBaseClass.m_ONSConfigFile = ONSConfigNode.Attributes[ConfigInfo.ONSConfigFile].Value.Trim();
				}
				else if (ConfigBaseClass.m_ONSMode == ONSConfigMode.local)
				{
					throw new ArgumentNullException("onsConfig." + ConfigInfo.ONSConfigFile);
				}
				if (ConfigBaseClass.m_ONSMode == ONSConfigMode.remote)
				{
					if (!string.IsNullOrWhiteSpace(ConfigBaseClass.m_ONSConfigFile))
					{
						ConfigBaseClass.m_nodeListFromConfFile = this.GetPropertyFromONSConfig(ConfigBaseClass.m_ONSConfigFile, ConfigInfo.ONSNodes);
					}
					foreach (object obj in ONSConfigNode.ChildNodes)
					{
						XmlNode xmlNode = (XmlNode)obj;
						if (xmlNode.HasChildNodes)
						{
							this.ParseONSElement(xmlNode);
						}
					}
				}
			}
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x00011E28 File Offset: 0x00010028
		private void ParseONSElement(XmlNode ONSNode)
		{
			string text = null;
			string value = null;
			if (ONSNode.Attributes != null)
			{
				text = ONSNode.Attributes[ConfigInfo.ONSDatabase].Value.Trim();
			}
			foreach (object obj in ONSNode.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.Attributes != null)
				{
					string text2 = xmlNode.Attributes["name"].Value.Trim();
					string text3 = xmlNode.Attributes["value"].Value.Trim();
					if (text2.Equals("nodeList"))
					{
						value = text3;
					}
				}
			}
			if (string.IsNullOrWhiteSpace(value))
			{
				throw new ArgumentNullException("onsConfig.nodeList");
			}
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary.Add("nodeList", value);
			ConfigBaseClass.m_ONSMapping.Add(text.ToLowerInvariant(), dictionary);
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x00011F30 File Offset: 0x00010130
		internal string GetPropertyFromONSConfig(string ONSConfigFile, string onsConfigProperty)
		{
			string result = null;
			if (File.Exists(ONSConfigFile))
			{
				using (StreamReader streamReader = new StreamReader(ONSConfigFile))
				{
					string leftover;
					string[] name = this.GetName(streamReader, out leftover);
					while (name != null && name.Length != 0)
					{
						string value = this.GetValue(streamReader, leftover);
						foreach (string text in name)
						{
							if (text.ToLowerInvariant() == onsConfigProperty)
							{
								result = value;
							}
							name = this.GetName(streamReader, out leftover);
						}
					}
					return result;
				}
			}
			throw new FileNotFoundException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.ONS_FILE_NOT_EXIST, new string[]
			{
				"Config File",
				ONSConfigFile
			}));
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x00011FF0 File Offset: 0x000101F0
		private void AddInfoForRefCursor(string storedProcKey, XmlNode refCursorNode, ref Hashtable schemaTable)
		{
			if (refCursorNode.Attributes != null)
			{
				bool isInfoBasedOnName = false;
				RefCursorInfo refCursorInfo = new RefCursorInfo();
				XmlAttribute xmlAttribute = refCursorNode.Attributes["name"];
				XmlAttribute xmlAttribute2 = refCursorNode.Attributes["position"];
				string s;
				string name;
				if (xmlAttribute2 != null && !string.IsNullOrEmpty(s = xmlAttribute2.Value.Trim()))
				{
					refCursorInfo.position = int.Parse(s);
					isInfoBasedOnName = false;
				}
				else if (xmlAttribute != null && !string.IsNullOrEmpty(name = xmlAttribute.Value.Trim()))
				{
					refCursorInfo.name = name;
					isInfoBasedOnName = true;
					refCursorInfo.position = -1;
				}
				else
				{
					string messageForTrace = "Neither RefCursor name nor position is present in " + storedProcKey;
					string messageForException = storedProcKey + "  refCursor";
					this.ThrowExceptionForRefCursor(messageForTrace, messageForException);
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
									this.AddMetadataForRefCursor(storedProcKey, xmlNode, ref refCursorInfo, isInfoBasedOnName, ref schemaTable);
								}
							}
							else
							{
								this.AddBindInfoForRefCursor(storedProcKey, xmlNode, ref refCursorInfo, isInfoBasedOnName, ref schemaTable);
							}
						}
					}
					return;
				}
			}
			string messageForTrace2 = "Neither RefCursor name nor position is present in stored procedure " + storedProcKey;
			string messageForException2 = storedProcKey + "  refCursor";
			this.ThrowExceptionForRefCursor(messageForTrace2, messageForException2);
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x00012164 File Offset: 0x00010364
		private void AddBindInfoForRefCursor(string storedProcKey, XmlNode bindInfoNode, ref RefCursorInfo refCursorInfo, bool isInfoBasedOnName, ref Hashtable schemaTable)
		{
			string value = bindInfoNode.Attributes["mode"].Value;
			ConfigBaseClass.StoredProcedureInfo storedProcedureInfo = (ConfigBaseClass.StoredProcedureInfo)schemaTable[storedProcKey];
			int num = 0;
			if (value != "Implicit")
			{
				refCursorInfo.mode = (ParameterDirection)Enum.Parse(typeof(ParameterDirection), value, true);
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
				return;
			}
			foreach (RefCursorInfo refCursorInfo3 in storedProcedureInfo.m_implicitlyRetRefCursors)
			{
				if (isInfoBasedOnName)
				{
					if (refCursorInfo3.name.Length > 0 && refCursorInfo3.name.Equals(refCursorInfo.name))
					{
						storedProcedureInfo.m_implicitlyRetRefCursors.RemoveAt(num);
						break;
					}
				}
				else if (refCursorInfo3.position >= refCursorInfo.position)
				{
					if (refCursorInfo3.position == refCursorInfo.position)
					{
						storedProcedureInfo.m_implicitlyRetRefCursors.RemoveAt(num);
					}
					storedProcedureInfo.m_implicitlyRetRefCursors.Insert(num, refCursorInfo);
					return;
				}
				num++;
			}
			storedProcedureInfo.m_implicitlyRetRefCursors.Add(refCursorInfo);
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x00012350 File Offset: 0x00010550
		private void AddMetadataForRefCursor(string storedProcKey, XmlNode metadataNode, ref RefCursorInfo refCursorInfo, bool isInfoBasedOnName, ref Hashtable schemaTable)
		{
			string s = metadataNode.Attributes["columnOrdinal"].Value.Trim();
			int num = int.Parse(s);
			DataRow dataRow = refCursorInfo.columnInfo.NewRow();
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
					dataRow["ColumnName"] = base.GetAttrValueInProperCase(text);
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
					dataRow["BaseColumnName"] = base.GetAttrValueInProperCase(text);
					break;
				case "BASESCHEMANAME":
					dataRow["BaseSchemaName"] = base.GetAttrValueInProperCase(text);
					break;
				case "BASETABLENAME":
					dataRow["BaseTableName"] = base.GetAttrValueInProperCase(text);
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
					dataRow["UdtTypeName"] = base.GetAttrValueInProperCase(text);
					break;
				case "NATIVEDATATYPE":
					dataRow["NativeDataType"] = base.GetAttrValueInProperCase(text);
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
					dataRow["ObjectName"] = base.GetAttrValueInProperCase(text);
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

		// Token: 0x060002E9 RID: 745 RVA: 0x00012928 File Offset: 0x00010B28
		private void ThrowExceptionForRefCursor(string messageForTrace, string messageForException)
		{
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0001292C File Offset: 0x00010B2C
		private void ParseSettingsElement(XmlNode settingsNode)
		{
			foreach (object obj in settingsNode.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.Attributes != null)
				{
					string key = xmlNode.Attributes["name"].Value.Trim();
					ConfigBaseClass.m_configParameters[key] = xmlNode.Attributes["value"].Value.Trim();
					ConfigBaseClass.m_configParamFrAppConfig[key] = true;
				}
			}
		}

		// Token: 0x060002EB RID: 747 RVA: 0x000129D8 File Offset: 0x00010BD8
		private void ParseEdmMappingsElement(XmlNode edmMappingsNode)
		{
			this.InitEdmMapping();
			if (edmMappingsNode != null)
			{
				foreach (object obj in edmMappingsNode.ChildNodes)
				{
					XmlNode xmlNode = (XmlNode)obj;
					if (xmlNode.Attributes != null && xmlNode.HasChildNodes)
					{
						if (xmlNode.Name == "edmMapping")
						{
							ConfigBaseClass.s_bLegacyEdmMappingPresent = true;
							string text = xmlNode.Attributes["dataType"].Value.Trim();
							if (text.ToLowerInvariant() == "number")
							{
								using (IEnumerator enumerator2 = xmlNode.ChildNodes.GetEnumerator())
								{
									while (enumerator2.MoveNext())
									{
										object obj2 = enumerator2.Current;
										XmlNode xmlNode2 = (XmlNode)obj2;
										if (xmlNode2.Name == "add")
										{
											string key = xmlNode2.Attributes["name"].Value.Trim().ToUpperInvariant();
											if (ConfigBaseClass.s_edmMapping.ContainsKey(key))
											{
												ConfigBaseClass.s_edmMapping[key] = int.Parse(xmlNode2.Attributes["precision"].Value.Trim());
											}
										}
									}
									continue;
								}
							}
							if (ProviderConfig.m_bTraceLevelPublic)
							{
								Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Error, new string[]
								{
									"(EDMMAPPING) DataType '" + text + "' is invalid"
								});
							}
							throw new ConfigurationErrorsException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.ODP_INVALID_VALUE, new string[]
							{
								"DataType '" + text + "'"
							}));
						}
						if (xmlNode.Name == "edmNumberMapping")
						{
							ConfigBaseClass.s_bEdmNumberMappingPresent = true;
							foreach (object obj3 in xmlNode.ChildNodes)
							{
								XmlNode xmlNode3 = (XmlNode)obj3;
								if (xmlNode3.Name == "add")
								{
									string a = xmlNode3.Attributes["DBType"].Value.Trim().ToLowerInvariant();
									if (a == "number")
									{
										string key2 = xmlNode3.Attributes["NETType"].Value.Trim().ToUpperInvariant();
										int num = int.Parse(xmlNode3.Attributes["MinPrecision"].Value.Trim());
										int num2 = int.Parse(xmlNode3.Attributes["MaxPrecision"].Value.Trim());
										DbType dbType;
										if (ConfigBaseClass.s_EdmMappingToDbType.ContainsKey(key2) && ConfigBaseClass.s_EdmMappingToDbType.TryGetValue(key2, out dbType))
										{
											for (int i = num; i <= num2; i++)
											{
												ConfigBaseClass.s_edmPrecisonMapping[i] = dbType;
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060002EC RID: 748 RVA: 0x00012D38 File Offset: 0x00010F38
		internal void InitEdmMapping()
		{
			ConfigBaseClass.s_edmMapping["BOOL"] = -2;
			ConfigBaseClass.s_edmMapping["BYTE"] = -1;
			ConfigBaseClass.s_edmMapping["INT16"] = 5;
			ConfigBaseClass.s_edmMapping["INT32"] = 10;
			ConfigBaseClass.s_edmMapping["INT64"] = 19;
		}

		// Token: 0x060002ED RID: 749 RVA: 0x00012DB4 File Offset: 0x00010FB4
		[ReflectionPermission(SecurityAction.Assert, Unrestricted = true)]
		internal void ValidateBaseDocument(XmlDocument doc)
		{
			CustomConfigFileReader @object = new CustomConfigFileReader(ConfigBaseClass.m_bIsManaged);
			doc.Schemas.Add(null, new XmlTextReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("Oracle.ManagedDataAccess.src.Common.Resources.Oracle.DataAccess.Common.Configuration.Section.xsd")));
			doc.Schemas.Add(null, new XmlTextReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("Oracle.ManagedDataAccess.src.Common.Resources.Oracle.ManagedDataAccess.Client.Configuration.Section.xsd")));
			ValidationEventHandler validationEventHandler = new ValidationEventHandler(@object.ValidationCallBack);
			doc.Validate(validationEventHandler);
		}

		// Token: 0x060002EE RID: 750 RVA: 0x00012E24 File Offset: 0x00011024
		private void ValidationCallBack(object sender, ValidationEventArgs args)
		{
			if (args.Severity != XmlSeverityType.Warning)
			{
				throw new ConfigurationErrorsException(args.Message);
			}
		}

		// Token: 0x060002EF RID: 751 RVA: 0x00012E3C File Offset: 0x0001103C
		internal override void ParseClientXmlNode(XmlNode baseNode, ref Hashtable schemaTable, ref ArrayList versionSpecificNodesList, ArrayList filterNodes = null)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			foreach (object obj in baseNode.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.Attributes != null && xmlNode.Attributes["number"].Value != null)
				{
					string text = xmlNode.Attributes["number"].Value.Trim();
					ConfigBaseClass.m_sectionVersion = text;
					if (text == "*")
					{
						if (flag)
						{
							flag3 = true;
						}
						else
						{
							flag = true;
							this.ParseSubSection(xmlNode, ref schemaTable, filterNodes);
						}
					}
					else if (text == ConfigBaseClass.m_assemblyVersion.ToString())
					{
						if (flag2)
						{
							flag3 = true;
						}
						else
						{
							flag2 = true;
							versionSpecificNodesList.Add(xmlNode);
						}
					}
					if (flag3)
					{
						string message = string.Format("{0} number=\"{1}\" ", xmlNode.Name, text);
						throw new ConfigurationErrorsException(message);
					}
				}
			}
		}

		// Token: 0x04000419 RID: 1049
		public const string EdmMappingsElement = "edmMappings";

		// Token: 0x0400041A RID: 1050
		public const string DataSource = "dataSource";

		// Token: 0x0400041B RID: 1051
		public const string EdmMappingElement = "edmMapping";

		// Token: 0x0400041C RID: 1052
		public const string DataType = "dataType";

		// Token: 0x0400041D RID: 1053
		public const string Precision = "precision";

		// Token: 0x0400041E RID: 1054
		public const string Scale = "scale";

		// Token: 0x0400041F RID: 1055
		public const string Add = "add";

		// Token: 0x04000420 RID: 1056
		public const string NumberDataType = "number";

		// Token: 0x04000421 RID: 1057
		private const string ORA_DEBUG_JDWP = "ORA_DEBUG_JDWP";

		// Token: 0x04000422 RID: 1058
		private const string TNS_ADMIN = "TNS_ADMIN";

		// Token: 0x04000423 RID: 1059
		private const string LDAP_ADMIN = "LDAP_ADMIN";

		// Token: 0x04000424 RID: 1060
		private const string ORACLE_HOME = "ORACLE_HOME";

		// Token: 0x04000425 RID: 1061
		private const string LDAPORA = "ldap.ora";

		// Token: 0x04000426 RID: 1062
		private const string TNSNAMESORA = "tnsnames.ora";

		// Token: 0x04000427 RID: 1063
		private const string SQLNETORA = "sqlnet.ora";

		// Token: 0x04000428 RID: 1064
		private readonly string NETWORK_ADMIN = Path.Combine("network", "admin");

		// Token: 0x04000429 RID: 1065
		private readonly string LDAP_ADMIN_DIR = Path.Combine("ldap", "admin");

		// Token: 0x0400042A RID: 1066
		private Hashtable mapUdtNameToMappingObjConfig = new Hashtable();
	}
}
