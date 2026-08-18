using System;
using TechnoPro.Common.UI.Web.Entity.Modules;

namespace TechnoPro.Common.UI.ClientManager.Web.Core.MailMerging
{
	// Token: 0x0200000F RID: 15
	public interface IMailMergeCodes
	{
		// Token: 0x0600002C RID: 44
		string GetDefaultFromAddress(eWebModule WebModule);

		// Token: 0x0600002D RID: 45
		string GetDefaultSignature(eWebModule WebModule);
	}
}
