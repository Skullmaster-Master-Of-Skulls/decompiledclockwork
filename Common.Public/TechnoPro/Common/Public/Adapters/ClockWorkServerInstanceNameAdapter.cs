using System;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Win32;

namespace TechnoPro.Common.Public.Adapters
{
	// Token: 0x020005EB RID: 1515
	public static class ClockWorkServerInstanceNameAdapter
	{
		// Token: 0x060030C9 RID: 12489 RVA: 0x00042744 File Offset: 0x00040944
		public static string GetServerVirtualDirByInstanceName(this eClockWorkServerInstanceName clockWorkServerInstanceName)
		{
			RegistryHelper registryHelper = new RegistryHelper();
			bool flag = clockWorkServerInstanceName == eClockWorkServerInstanceName.ClockWorkServer;
			if (flag)
			{
				string text = registryHelper.ReadLocalMachineRegistry<string>(eRegWow64Options.KEY_WOW64_32KEY, new string[]
				{
					"ClockWorkServer Application",
					"ClockWorkServer vDir"
				});
				bool flag2 = !string.IsNullOrEmpty(text);
				if (flag2)
				{
					return text;
				}
			}
			string[] localMachineSubKeyNames = registryHelper.GetLocalMachineSubKeyNames(eRegWow64Options.KEY_WOW64_32KEY, new string[]
			{
				"ClockWorkServer Application"
			});
			foreach (string text2 in localMachineSubKeyNames)
			{
				bool flag3 = clockWorkServerInstanceName.ToString().Equals(text2);
				if (flag3)
				{
					return text2;
				}
				string value = registryHelper.ReadLocalMachineRegistry<string>(eRegWow64Options.KEY_WOW64_32KEY, new string[]
				{
					"ClockWorkServer Application",
					text2,
					"ClockWorkServerInstanceName"
				});
				bool flag4 = clockWorkServerInstanceName.ToString().Equals(value);
				if (flag4)
				{
					return text2;
				}
			}
			return null;
		}
	}
}
