using System;
using Microsoft.Win32;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Win32;

namespace TechnoPro.Common.ICore
{
	// Token: 0x0200000D RID: 13
	[Obsolete("Deprecated, please use Common.Win32.RegistryHelper class instead")]
	public interface IRegistryManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600004F RID: 79
		RegistryKey StartLocalMachineRegistryKey { get; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000050 RID: 80
		RegistryKey StartCurrentUserRegistryKey { get; }

		// Token: 0x06000051 RID: 81
		RegistryKey GetStartLocalMachineRegistryKey(eRegWow64Options wow64Options);

		// Token: 0x06000052 RID: 82
		RegistryKey GetStartCurrentUserRegistryKey(eRegWow64Options wow64Options);

		// Token: 0x06000053 RID: 83
		T ReadLocalMachineRegistry<T>(params string[] keypath);

		// Token: 0x06000054 RID: 84
		void WriteLocalMachineRegistry<T>(T value, params string[] keypath);

		// Token: 0x06000055 RID: 85
		T ReadLocalMachineRegistry<T>(eRegWow64Options wow64Options, params string[] keypath);

		// Token: 0x06000056 RID: 86
		void WriteLocalMachineRegistry<T>(T value, eRegWow64Options wow64Options, params string[] keypath);

		// Token: 0x06000057 RID: 87
		T ReadCurrentUserRegistry<T>(params string[] keypath);

		// Token: 0x06000058 RID: 88
		void WriteCurrentUserRegistry<T>(T value, params string[] keypath);

		// Token: 0x06000059 RID: 89
		T ReadCurrentUserRegistry<T>(eRegWow64Options wow64Options, params string[] keypath);

		// Token: 0x0600005A RID: 90
		void WriteCurrentUserRegistry<T>(T value, eRegWow64Options wow64Options, params string[] keypath);

		// Token: 0x0600005B RID: 91
		string[] GetLocalMachineSubKeyNames(params string[] keypath);

		// Token: 0x0600005C RID: 92
		string[] GetCurrentUserSubKeyNames(params string[] keypath);

		// Token: 0x0600005D RID: 93
		string[] GetLocalMachineSubKeyNames(eRegWow64Options wow64Options, params string[] keypath);

		// Token: 0x0600005E RID: 94
		string[] GetCurrentUserSubKeyNames(eRegWow64Options wow64Options, params string[] keypath);
	}
}
