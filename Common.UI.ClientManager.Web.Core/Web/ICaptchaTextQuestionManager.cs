using System;
using TechnoPro.Common.UI.Web.Entity.Web;

namespace TechnoPro.Common.UI.ClientManager.Web.Core.Web
{
	// Token: 0x02000004 RID: 4
	public interface ICaptchaTextQuestionManager
	{
		// Token: 0x06000004 RID: 4
		CaptchaQuestionAndAnswer GetRandomQuestion();
	}
}
