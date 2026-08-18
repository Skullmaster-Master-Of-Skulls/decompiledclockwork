using System;
using TechnoPro.Common.Public.Entities.Settings;

namespace TechnoPro.Common.UI.ClientManager.Web.Core.MailMerging
{
	// Token: 0x02000010 RID: 16
	public interface IMailMergingDocWebClientManager
	{
		// Token: 0x0600002E RID: 46
		byte[] GenerateLetter(Setting englishTemplateSetting, Setting frenchTemplateSetting, bool inFrench, int pid, int lucid, out string filename);
	}
}
