using System;
using System.Web.Caching;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.MailMerging;
using TechnoPro.Common.UI.ClientManager.Web.Core.MailMerging;

namespace ClockWorkController
{
	// Token: 0x02000003 RID: 3
	public class AccommodationLetter
	{
		// Token: 0x06000004 RID: 4 RVA: 0x00002388 File Offset: 0x00000588
		public static byte[] GenerateLetter(Setting englishTemplateSetting, Setting frenchTemplateSetting, bool inFrench, Cache Cache, int pid, int lucid, out string filename)
		{
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			int num = 0;
			bool flag = !inFrench;
			if (flag)
			{
				num = webSettingsClientManager.GetSettingValue<int>(englishTemplateSetting);
			}
			bool flag2 = num <= 0;
			if (flag2)
			{
				num = webSettingsClientManager.GetSettingValue<int>(frenchTemplateSetting);
			}
			IMailMergingDocWebClientManager mailMergingDocWebClientManager = new MailMergingDocWebClientManager();
			return mailMergingDocWebClientManager.GenerateLetter(englishTemplateSetting, frenchTemplateSetting, inFrench, pid, lucid, out filename);
		}
	}
}
