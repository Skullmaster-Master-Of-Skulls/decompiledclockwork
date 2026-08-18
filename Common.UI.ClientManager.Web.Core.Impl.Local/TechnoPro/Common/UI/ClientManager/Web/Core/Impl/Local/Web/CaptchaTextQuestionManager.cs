using System;
using TechnoPro.Common.UI.ClientManager.Web.Core.Web;
using TechnoPro.Common.UI.Web.Entity.Web;

namespace TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web
{
	// Token: 0x0200000D RID: 13
	public class CaptchaTextQuestionManager : ICaptchaTextQuestionManager
	{
		// Token: 0x0600003A RID: 58 RVA: 0x00002D2C File Offset: 0x00000F2C
		public CaptchaQuestionAndAnswer GetRandomQuestion()
		{
			return new CaptchaQuestionAndAnswer();
		}
	}
}
