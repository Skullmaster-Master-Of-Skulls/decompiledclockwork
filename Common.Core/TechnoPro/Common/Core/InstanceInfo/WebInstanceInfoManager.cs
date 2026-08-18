using System;
using System.Collections.Generic;
using System.Linq;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.ICore.InstanceInfo;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Database;
using TechnoPro.Common.Public.Entities.InstanceInfo;
using TechnoPro.Common.Win32;

namespace TechnoPro.Common.Core.InstanceInfo
{
	// Token: 0x020000EF RID: 239
	public class WebInstanceInfoManager : IWebInstanceInfoManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000944 RID: 2372 RVA: 0x0003B62C File Offset: 0x0003982C
		public IList<WebInstanceInfo> GetWebInstancesInfo(DbConnectionInfo dbConnectionInfo)
		{
			List<WebInstanceInfo> list = new List<WebInstanceInfo>();
			RegistryHelper registryHelper = new RegistryHelper();
			string[] localMachineSubKeyNames = registryHelper.GetLocalMachineSubKeyNames(eRegWow64Options.KEY_WOW64_32KEY, new string[]
			{
				"ClockWorkWeb"
			});
			bool flag = localMachineSubKeyNames == null;
			IList<WebInstanceInfo> result;
			if (flag)
			{
				result = list;
			}
			else
			{
				foreach (string text in localMachineSubKeyNames)
				{
					int num = registryHelper.ReadLocalMachineRegistry<int>(eRegWow64Options.KEY_WOW64_32KEY, new string[]
					{
						"ClockWorkWeb",
						text,
						"Uninstalled"
					});
					bool flag2 = num > 0;
					if (!flag2)
					{
						DbConnectionInfo webInstanceDbConnectionInfo = this.GetWebInstanceDbConnectionInfo(text, registryHelper);
						bool flag3 = dbConnectionInfo.Equals(webInstanceDbConnectionInfo);
						if (flag3)
						{
							WebInstanceInfo webInstanceInfoFromRegistry = this.GetWebInstanceInfoFromRegistry(text, registryHelper, dbConnectionInfo);
							bool flag4 = webInstanceInfoFromRegistry != null;
							if (flag4)
							{
								list.Add(webInstanceInfoFromRegistry);
							}
						}
					}
				}
				result = list;
			}
			return result;
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x0003B70C File Offset: 0x0003990C
		public IList<WebInstanceInfo> GetWebInstancesInfo()
		{
			List<WebInstanceInfo> list = new List<WebInstanceInfo>();
			RegistryHelper registryHelper = new RegistryHelper();
			string[] localMachineSubKeyNames = registryHelper.GetLocalMachineSubKeyNames(eRegWow64Options.KEY_WOW64_32KEY, new string[]
			{
				"ClockWorkWeb"
			});
			bool flag = localMachineSubKeyNames == null || localMachineSubKeyNames.Length < 1;
			IList<WebInstanceInfo> result;
			if (flag)
			{
				result = list;
			}
			else
			{
				list.AddRange(from webAppName in localMachineSubKeyNames
				select this.GetWebInstanceInfoFromRegistry(webAppName, registryHelper, null) into webInstanceInfo
				where webInstanceInfo != null
				select webInstanceInfo);
				result = list;
			}
			return result;
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x0003B7B0 File Offset: 0x000399B0
		public WebInstanceInfo GetWebInstanceInfo(string webAppName)
		{
			RegistryHelper registryHelper = new RegistryHelper();
			string[] localMachineSubKeyNames = registryHelper.GetLocalMachineSubKeyNames(eRegWow64Options.KEY_WOW64_32KEY, new string[]
			{
				"ClockWorkWeb"
			});
			return (localMachineSubKeyNames == null || !localMachineSubKeyNames.Contains(webAppName)) ? null : this.GetWebInstanceInfoFromRegistry(webAppName, registryHelper, null);
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000947 RID: 2375 RVA: 0x0003B7FA File Offset: 0x000399FA
		// (set) Token: 0x06000948 RID: 2376 RVA: 0x0003B802 File Offset: 0x00039A02
		public OperationContext OpContext { get; set; }

		// Token: 0x06000949 RID: 2377 RVA: 0x0003B80C File Offset: 0x00039A0C
		private WebInstanceInfo GetWebInstanceInfoFromRegistry(string webAppName, RegistryHelper registryHelper, DbConnectionInfo dbConnection = null)
		{
			int num = registryHelper.ReadLocalMachineRegistry<int>(eRegWow64Options.KEY_WOW64_32KEY, new string[]
			{
				"ClockWorkWeb",
				webAppName,
				"Uninstalled"
			});
			bool flag = num > 0;
			WebInstanceInfo result;
			if (flag)
			{
				result = null;
			}
			else
			{
				WebInstanceInfo webInstanceInfo = new WebInstanceInfo();
				webInstanceInfo.AppPoolName = registryHelper.ReadLocalMachineRegistry<string>(eRegWow64Options.KEY_WOW64_32KEY, new string[]
				{
					"ClockWorkWeb",
					webAppName,
					"AppPoolName"
				});
				webInstanceInfo.InstanceName = webAppName;
				webInstanceInfo.InstallationPath = registryHelper.ReadLocalMachineRegistry<string>(eRegWow64Options.KEY_WOW64_32KEY, new string[]
				{
					"ClockWorkWeb",
					webAppName,
					"InstallPath"
				});
				InstanceInfo instanceInfo = webInstanceInfo;
				string text = registryHelper.ReadLocalMachineRegistry<string>(eRegWow64Options.KEY_WOW64_32KEY, new string[]
				{
					"ClockWorkWeb",
					webAppName,
					"Version"
				});
				instanceInfo.Version = ((text != null) ? text.FormatVersion() : null);
				webInstanceInfo.DbConnectionInfo = (dbConnection ?? this.GetWebInstanceDbConnectionInfo(webAppName, registryHelper));
				result = webInstanceInfo;
			}
			return result;
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x0003B908 File Offset: 0x00039B08
		private DbConnectionInfo GetWebInstanceDbConnectionInfo(string webAppName, RegistryHelper registryHelper)
		{
			string text = registryHelper.ReadLocalMachineRegistry<string>(eRegWow64Options.KEY_WOW64_32KEY, new string[]
			{
				"ClockWorkWeb",
				webAppName,
				string.Format("{0}_cs", eDatabaseConnectionStringName.ClockWork)
			});
			bool flag = string.IsNullOrEmpty(text);
			if (flag)
			{
				text = registryHelper.ReadLocalMachineRegistry<string>(eRegWow64Options.KEY_WOW64_32KEY, new string[]
				{
					"ClockWorkWeb",
					webAppName,
					"DbConn_cs"
				});
			}
			string text2 = registryHelper.ReadLocalMachineRegistry<string>(eRegWow64Options.KEY_WOW64_32KEY, new string[]
			{
				"ClockWorkWeb",
				webAppName,
				string.Format("{0}_k", eDatabaseConnectionStringName.ClockWork)
			});
			bool flag2 = string.IsNullOrEmpty(text2);
			if (flag2)
			{
				text2 = registryHelper.ReadLocalMachineRegistry<string>(eRegWow64Options.KEY_WOW64_32KEY, new string[]
				{
					"ClockWorkWeb",
					webAppName,
					"DbConn_k"
				});
			}
			string cs = string.IsNullOrEmpty(text) ? null : DPAPIEncryptionV2.UnProtectDataBase64String(text, ProtectionScope.LocalMachine);
			string k = string.IsNullOrEmpty(text2) ? null : DPAPIEncryptionV2.UnProtectDataBase64String(text2, ProtectionScope.LocalMachine);
			return new DbConnectionInfo(cs, k);
		}
	}
}
