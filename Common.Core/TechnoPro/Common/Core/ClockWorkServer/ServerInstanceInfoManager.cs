using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.ICore.ClockWorkServer;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Database;
using TechnoPro.Common.Public.Entities.InstanceInfo;
using TechnoPro.Common.Win32;

namespace TechnoPro.Common.Core.ClockWorkServer
{
	// Token: 0x0200011D RID: 285
	public class ServerInstanceInfoManager : IServerInstanceInfoManager
	{
		// Token: 0x06000C0F RID: 3087 RVA: 0x000542D8 File Offset: 0x000524D8
		public IList<ServerInstanceInfo> GetServerInstancesInfo()
		{
			List<ServerInstanceInfo> list = new List<ServerInstanceInfo>();
			RegistryHelper registryHelper = new RegistryHelper();
			string[] localMachineSubKeyNames = registryHelper.GetLocalMachineSubKeyNames(eRegWow64Options.KEY_WOW64_32KEY, new string[]
			{
				"ClockWorkServer Application"
			});
			bool flag = localMachineSubKeyNames == null;
			IList<ServerInstanceInfo> result;
			if (flag)
			{
				result = list;
			}
			else
			{
				foreach (string serverVirtualDir in localMachineSubKeyNames)
				{
					ServerInstanceInfo serverInstanceInfoFromRegistry = this.GetServerInstanceInfoFromRegistry(registryHelper, serverVirtualDir);
					bool flag2 = serverInstanceInfoFromRegistry != null;
					if (flag2)
					{
						list.Add(serverInstanceInfoFromRegistry);
					}
				}
				result = list;
			}
			return result;
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x00054360 File Offset: 0x00052560
		public bool IsRunningFromClockWorkServerComputer()
		{
			RegistryHelper registryHelper = new RegistryHelper();
			string[] localMachineSubKeyNames = registryHelper.GetLocalMachineSubKeyNames(eRegWow64Options.KEY_WOW64_32KEY, new string[]
			{
				"ClockWorkServer Application"
			});
			bool flag = localMachineSubKeyNames == null || localMachineSubKeyNames.Length == 0;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				string text = registryHelper.ReadLocalMachineRegistry<string>(eRegWow64Options.KEY_WOW64_32KEY, new string[]
				{
					"ClockWorkServer Application",
					localMachineSubKeyNames[0],
					"InstallPath"
				});
				result = (!string.IsNullOrEmpty(text) && Directory.Exists(text) && File.Exists(Path.Combine(text, "Web.config")));
			}
			return result;
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x000543F4 File Offset: 0x000525F4
		public ServerInstanceInfo GetServerInstanceInfoByName(string serverVirtualDir)
		{
			RegistryHelper registryHelper = new RegistryHelper();
			string[] localMachineSubKeyNames = registryHelper.GetLocalMachineSubKeyNames(eRegWow64Options.KEY_WOW64_32KEY, new string[]
			{
				"ClockWorkServer Application"
			});
			bool flag = !localMachineSubKeyNames.Any((string s) => s.Equals(serverVirtualDir, StringComparison.OrdinalIgnoreCase));
			ServerInstanceInfo result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = this.GetServerInstanceInfoFromRegistry(registryHelper, serverVirtualDir);
			}
			return result;
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x00054460 File Offset: 0x00052660
		public ServerInstanceInfo GetServerInstanceInfoByInstanceName(eClockWorkServerInstanceName clockWorkServerInstanceName)
		{
			string serverVirtualDirByInstanceName = clockWorkServerInstanceName.GetServerVirtualDirByInstanceName();
			return string.IsNullOrEmpty(serverVirtualDirByInstanceName) ? null : this.GetServerInstanceInfoByName(serverVirtualDirByInstanceName);
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x0005448C File Offset: 0x0005268C
		private ServerInstanceInfo GetServerInstanceInfoFromRegistry(RegistryHelper registryManager, string serverVirtualDir)
		{
			int num = registryManager.ReadLocalMachineRegistry<int>(eRegWow64Options.KEY_WOW64_32KEY, new string[]
			{
				"ClockWorkServer Application",
				serverVirtualDir,
				"Uninstalled"
			});
			bool flag = num > 0;
			ServerInstanceInfo result;
			if (flag)
			{
				result = null;
			}
			else
			{
				string value = registryManager.ReadLocalMachineRegistry<string>(eRegWow64Options.KEY_WOW64_32KEY, new string[]
				{
					"ClockWorkServer Application",
					serverVirtualDir,
					"ClockWorkServerInstanceName"
				});
				string text = registryManager.ReadLocalMachineRegistry<string>(eRegWow64Options.KEY_WOW64_32KEY, new string[]
				{
					"ClockWorkServer Application",
					serverVirtualDir,
					"ProgramFilesFolder"
				});
				bool flag2 = string.IsNullOrEmpty(value) || !Enum.IsDefined(typeof(eClockWorkServerInstanceName), value);
				if (flag2)
				{
					value = this.GetClockWorkWorkServerInstanceNameFromPath(text);
				}
				bool flag3 = string.IsNullOrEmpty(value);
				if (flag3)
				{
					result = null;
				}
				else
				{
					eClockWorkServerInstanceName clockWorkServerInstanceName = eClockWorkServerInstanceName.ClockWorkServer;
					bool flag4 = !string.IsNullOrEmpty(value) && Enum.IsDefined(typeof(eClockWorkServerInstanceName), value);
					if (flag4)
					{
						clockWorkServerInstanceName = (eClockWorkServerInstanceName)Enum.Parse(typeof(eClockWorkServerInstanceName), value);
					}
					string text2 = registryManager.ReadLocalMachineRegistry<string>(eRegWow64Options.KEY_WOW64_32KEY, new string[]
					{
						"ClockWorkServer Application",
						serverVirtualDir,
						"Version"
					});
					bool flag5 = string.IsNullOrEmpty(text2);
					if (flag5)
					{
						result = null;
					}
					else
					{
						string appPoolName = registryManager.ReadLocalMachineRegistry<string>(eRegWow64Options.KEY_WOW64_32KEY, new string[]
						{
							"ClockWorkServer Application",
							serverVirtualDir,
							"AppPoolName"
						});
						string installationPath = registryManager.ReadLocalMachineRegistry<string>(eRegWow64Options.KEY_WOW64_32KEY, new string[]
						{
							"ClockWorkServer Application",
							serverVirtualDir,
							"InstallPath"
						});
						string sitename = registryManager.ReadLocalMachineRegistry<string>(eRegWow64Options.KEY_WOW64_32KEY, new string[]
						{
							"ClockWorkServer Application",
							serverVirtualDir,
							"Sitename"
						});
						string x509FindType = registryManager.ReadLocalMachineRegistry<string>(eRegWow64Options.KEY_WOW64_32KEY, new string[]
						{
							"ClockWorkServer Application",
							serverVirtualDir,
							"x509FindType"
						});
						string x509FindValue = registryManager.ReadLocalMachineRegistry<string>(eRegWow64Options.KEY_WOW64_32KEY, new string[]
						{
							"ClockWorkServer Application",
							serverVirtualDir,
							"x509FindValue"
						});
						string text3 = registryManager.ReadLocalMachineRegistry<string>(eRegWow64Options.KEY_WOW64_32KEY, new string[]
						{
							"ClockWorkServer Application",
							serverVirtualDir,
							eDatabaseConnectionStringName.ClockWork + "_cs"
						});
						bool flag6 = string.IsNullOrEmpty(text3);
						if (flag6)
						{
							text3 = registryManager.ReadLocalMachineRegistry<string>(eRegWow64Options.KEY_WOW64_32KEY, new string[]
							{
								"ClockWorkServer Application",
								serverVirtualDir,
								"ServerDb_cs"
							});
						}
						string text4 = registryManager.ReadLocalMachineRegistry<string>(eRegWow64Options.KEY_WOW64_32KEY, new string[]
						{
							"ClockWorkServer Application",
							serverVirtualDir,
							eDatabaseConnectionStringName.ClockWork + "_k"
						});
						bool flag7 = string.IsNullOrEmpty(text4);
						if (flag7)
						{
							text4 = registryManager.ReadLocalMachineRegistry<string>(eRegWow64Options.KEY_WOW64_32KEY, new string[]
							{
								"ClockWorkServer Application",
								serverVirtualDir,
								"ServerDb_k"
							});
						}
						string text5 = string.IsNullOrEmpty(text3) ? null : DPAPIEncryptionV2.UnProtectDataBase64String(text3, ProtectionScope.LocalMachine);
						string text6 = string.IsNullOrEmpty(text4) ? null : DPAPIEncryptionV2.UnProtectDataBase64String(text4, ProtectionScope.LocalMachine);
						DbConnectionInfo dbConnectionInfo = (string.IsNullOrEmpty(text5) || string.IsNullOrEmpty(text6)) ? new DbConnectionInfo() : new DbConnectionInfo(text5, text6);
						string text7 = registryManager.ReadLocalMachineRegistry<string>(eRegWow64Options.KEY_WOW64_32KEY, new string[]
						{
							"ClockWorkServer Application",
							serverVirtualDir,
							eDatabaseConnectionStringName.ClockWorkFiles + "_cs"
						});
						bool flag8 = string.IsNullOrEmpty(text7);
						if (flag8)
						{
							text7 = registryManager.ReadLocalMachineRegistry<string>(eRegWow64Options.KEY_WOW64_32KEY, new string[]
							{
								"ClockWorkServer Application",
								serverVirtualDir,
								"ServerDbFiles_cs"
							});
						}
						string text8 = registryManager.ReadLocalMachineRegistry<string>(eRegWow64Options.KEY_WOW64_32KEY, new string[]
						{
							"ClockWorkServer Application",
							serverVirtualDir,
							eDatabaseConnectionStringName.ClockWorkFiles + "_k"
						});
						bool flag9 = string.IsNullOrEmpty(text8);
						if (flag9)
						{
							text8 = registryManager.ReadLocalMachineRegistry<string>(eRegWow64Options.KEY_WOW64_32KEY, new string[]
							{
								"ClockWorkServer Application",
								serverVirtualDir,
								"ServerDbFiles_k"
							});
						}
						string text9 = string.IsNullOrEmpty(text7) ? null : DPAPIEncryptionV2.UnProtectDataBase64String(text7, ProtectionScope.LocalMachine);
						string text10 = string.IsNullOrEmpty(text8) ? null : DPAPIEncryptionV2.UnProtectDataBase64String(text8, ProtectionScope.LocalMachine);
						DbConnectionInfo clockWorkFilesDbConnectionInfo = (string.IsNullOrEmpty(text9) || string.IsNullOrEmpty(text10)) ? new DbConnectionInfo() : new DbConnectionInfo(text9, text10);
						string text11 = registryManager.ReadLocalMachineRegistry<string>(eRegWow64Options.KEY_WOW64_32KEY, new string[]
						{
							"ClockWorkServer Application",
							serverVirtualDir,
							eDatabaseConnectionStringName.ClockWorkTracking + "_cs"
						});
						string text12 = registryManager.ReadLocalMachineRegistry<string>(eRegWow64Options.KEY_WOW64_32KEY, new string[]
						{
							"ClockWorkServer Application",
							serverVirtualDir,
							eDatabaseConnectionStringName.ClockWorkTracking + "_k"
						});
						string text13 = string.IsNullOrEmpty(text11) ? null : DPAPIEncryptionV2.UnProtectDataBase64String(text11, ProtectionScope.LocalMachine);
						string text14 = string.IsNullOrEmpty(text12) ? null : DPAPIEncryptionV2.UnProtectDataBase64String(text12, ProtectionScope.LocalMachine);
						DbConnectionInfo clockWorkTrackingDbConnectionInfo = (string.IsNullOrEmpty(text13) || string.IsNullOrEmpty(text14)) ? new DbConnectionInfo() : new DbConnectionInfo(text13, text14);
						string text15 = registryManager.ReadLocalMachineRegistry<string>(eRegWow64Options.KEY_WOW64_32KEY, new string[]
						{
							"ClockWorkServer Application",
							serverVirtualDir,
							"patch_username"
						});
						string text16 = registryManager.ReadLocalMachineRegistry<string>(eRegWow64Options.KEY_WOW64_32KEY, new string[]
						{
							"ClockWorkServer Application",
							serverVirtualDir,
							"patch_password"
						});
						string text17 = string.IsNullOrEmpty(text15) ? null : DPAPIEncryptionV2.UnProtectDataBase64String(text15, ProtectionScope.LocalMachine);
						string text18 = string.IsNullOrEmpty(text16) ? null : DPAPIEncryptionV2.UnProtectDataBase64String(text16, ProtectionScope.LocalMachine);
						bool flag10 = string.IsNullOrEmpty(text17) || string.IsNullOrEmpty(text18);
						if (flag10)
						{
							string text19 = registryManager.ReadLocalMachineRegistry<string>(eRegWow64Options.KEY_WOW64_32KEY, new string[]
							{
								"ClockWorkServer Application",
								serverVirtualDir,
								"DbPatch_cs"
							});
							bool flag11 = !string.IsNullOrEmpty(text19);
							if (flag11)
							{
								DbConnectionStringBuilder dbConnectionStringBuilder = DbProviderFactories.GetFactory(ProviderNames.SqlClient).CreateConnectionStringBuilder();
								dbConnectionStringBuilder.ConnectionString = DPAPIEncryptionV2.UnProtectDataBase64String(text19, ProtectionScope.LocalMachine);
								text17 = ((dbConnectionStringBuilder["User ID"] != null) ? dbConnectionStringBuilder["User ID"].ToString() : null);
								text18 = ((dbConnectionStringBuilder["Password"] != null) ? dbConnectionStringBuilder["Password"].ToString() : null);
							}
						}
						result = new ServerInstanceInfo
						{
							ClockWorkServerInstanceName = clockWorkServerInstanceName,
							Sitename = sitename,
							AppPoolName = appPoolName,
							InstanceName = serverVirtualDir,
							InstallationPath = installationPath,
							Version = (string.IsNullOrEmpty(text2) ? string.Empty : text2.FormatVersion()),
							DbConnectionInfo = dbConnectionInfo,
							ProgramFilesFolder = text,
							X509FindType = x509FindType,
							X509FindValue = x509FindValue,
							ClockWorkServerDbConnectionInfo = dbConnectionInfo,
							ClockWorkFilesDbConnectionInfo = clockWorkFilesDbConnectionInfo,
							ClockWorkTrackingDbConnectionInfo = clockWorkTrackingDbConnectionInfo,
							PatchUsername = text17,
							PatchPassword = text18
						};
					}
				}
			}
			return result;
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x00054B68 File Offset: 0x00052D68
		private string GetClockWorkWorkServerInstanceNameFromPath(string installationPath)
		{
			bool flag = string.IsNullOrEmpty(installationPath);
			string result;
			if (flag)
			{
				result = null;
			}
			else
			{
				string[] array = installationPath.Split(new char[]
				{
					'\\'
				}, StringSplitOptions.RemoveEmptyEntries);
				result = ((array.Length != 0) ? array[array.Length - 1] : string.Empty);
			}
			return result;
		}
	}
}
