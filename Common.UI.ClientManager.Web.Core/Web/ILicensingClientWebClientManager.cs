using System;
using TechnoPro.Common.Public.Entities.Settings;

namespace TechnoPro.Common.UI.ClientManager.Web.Core.Web
{
	// Token: 0x02000006 RID: 6
	public interface ILicensingClientWebClientManager
	{
		// Token: 0x0600000D RID: 13
		bool IsModuleLicensed(Group Group);

		// Token: 0x0600000E RID: 14
		void CheckIsModuleLicensed(Group Group);
	}
}
