using System;
using TechnoPro.Common.DAO.Impl.Legacy;
using TechnoPro.Common.DAO.Legacy;
using TechnoPro.Common.ICore.Legacy;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.Core.Legacy
{
	// Token: 0x020000DE RID: 222
	public class LegacyWebSettingsManager : ILegacyWebSettingsManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600087E RID: 2174 RVA: 0x00038F25 File Offset: 0x00037125
		public LegacyWebSettingsManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x0600087F RID: 2175 RVA: 0x00038F37 File Offset: 0x00037137
		// (set) Token: 0x06000880 RID: 2176 RVA: 0x00038F3F File Offset: 0x0003713F
		public OperationContext OpContext { get; set; }

		// Token: 0x06000881 RID: 2177 RVA: 0x00038F48 File Offset: 0x00037148
		public string GetWebSettingValue(int webSetting, string instanceName)
		{
			ILegacyWebSettingsDAO legacyWebSettingsDAO = new LegacyWebSettingsDAO(this.OpContext);
			return legacyWebSettingsDAO.GetWebSettingValue(webSetting, instanceName);
		}
	}
}
