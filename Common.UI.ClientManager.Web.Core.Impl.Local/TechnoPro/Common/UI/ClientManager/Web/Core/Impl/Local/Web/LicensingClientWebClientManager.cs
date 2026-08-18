using System;
using TechnoPro.ClockWorkServer.Contracts.DataContracts;
using TechnoPro.Common.ClientManager.Core.Licensing;
using TechnoPro.Common.ClientManager.ICore.Licensing;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Core.Web;

namespace TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web
{
	// Token: 0x0200000F RID: 15
	public class LicensingClientWebClientManager : ILicensingClientWebClientManager
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000049 RID: 73 RVA: 0x000030EC File Offset: 0x000012EC
		public static LicensingClientWebClientManager CurrentInstance
		{
			get
			{
				bool flag = LicensingClientWebClientManager._currentInstance == null;
				if (flag)
				{
					LicensingClientWebClientManager._currentInstance = new LicensingClientWebClientManager();
				}
				return LicensingClientWebClientManager._currentInstance;
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x0000311C File Offset: 0x0000131C
		public void CheckIsModuleLicensed(Group Group)
		{
			bool flag = this.IsModuleLicensed(Group);
			bool flag2 = flag;
			if (!flag2)
			{
				INavigatorClientManager navigatorClientManager = new NavigatorClientManager();
				navigatorClientManager.GotoModuleNotLicensedWarningPage(Group);
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003148 File Offset: 0x00001348
		public bool IsModuleLicensed(Group Group)
		{
			ILicensingClientManager licensingClientManager = new LicensingClientManager();
			LicensingProductStatusResp productStatus = licensingClientManager.GetProductStatus(Group);
			bool flag = productStatus == null;
			return flag || productStatus.LicenseStatus == ProductLicenseStatus.Licensed;
		}

		// Token: 0x04000011 RID: 17
		private static LicensingClientWebClientManager _currentInstance;
	}
}
