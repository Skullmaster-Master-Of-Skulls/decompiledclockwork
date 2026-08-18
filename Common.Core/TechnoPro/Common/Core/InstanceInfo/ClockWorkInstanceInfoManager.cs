using System;
using TechnoPro.Common.ICore.InstanceInfo;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.InstanceInfo;
using TechnoPro.Common.Win32;

namespace TechnoPro.Common.Core.InstanceInfo
{
	// Token: 0x020000EE RID: 238
	public class ClockWorkInstanceInfoManager : IClockWorkInstanceInfoManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000940 RID: 2368 RVA: 0x0003B583 File Offset: 0x00039783
		// (set) Token: 0x06000941 RID: 2369 RVA: 0x0003B58B File Offset: 0x0003978B
		public OperationContext OpContext { get; set; }

		// Token: 0x06000942 RID: 2370 RVA: 0x0003B594 File Offset: 0x00039794
		public ClockWorkInstanceInfoManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x0003B5A8 File Offset: 0x000397A8
		public ClockWorkInstanceInfo GetDefaultClockWorkInstanceInfo()
		{
			RegistryHelper registryHelper = new RegistryHelper();
			string text = registryHelper.ReadLocalMachineRegistry<string>(eRegWow64Options.KEY_WOW64_32KEY, new string[]
			{
				"ClockWork",
				"InstallPath"
			});
			bool flag = string.IsNullOrEmpty(text);
			ClockWorkInstanceInfo result;
			if (flag)
			{
				result = null;
			}
			else
			{
				string version = registryHelper.ReadLocalMachineRegistry<string>(eRegWow64Options.KEY_WOW64_32KEY, new string[]
				{
					"ClockWork",
					"Version"
				});
				result = new ClockWorkInstanceInfo
				{
					InstallationPath = text,
					Version = version
				};
			}
			return result;
		}
	}
}
