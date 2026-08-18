using System;
using System.Runtime.InteropServices;

namespace System.Configuration.Internal
{
	// Token: 0x020000B5 RID: 181
	[ComVisible(false)]
	public interface IInternalConfigRoot
	{
		// Token: 0x06000734 RID: 1844
		void Init(IInternalConfigHost host, bool isDesignTime);

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000735 RID: 1845
		bool IsDesignTime { get; }

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000736 RID: 1846
		// (remove) Token: 0x06000737 RID: 1847
		event InternalConfigEventHandler ConfigChanged;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000738 RID: 1848
		// (remove) Token: 0x06000739 RID: 1849
		event InternalConfigEventHandler ConfigRemoved;

		// Token: 0x0600073A RID: 1850
		object GetSection(string section, string configPath);

		// Token: 0x0600073B RID: 1851
		string GetUniqueConfigPath(string configPath);

		// Token: 0x0600073C RID: 1852
		IInternalConfigRecord GetUniqueConfigRecord(string configPath);

		// Token: 0x0600073D RID: 1853
		IInternalConfigRecord GetConfigRecord(string configPath);

		// Token: 0x0600073E RID: 1854
		void RemoveConfig(string configPath);
	}
}
