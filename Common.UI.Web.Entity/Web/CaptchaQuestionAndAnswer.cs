using System;

namespace TechnoPro.Common.UI.Web.Entity.Web
{
	// Token: 0x0200000F RID: 15
	[Serializable]
	public class CaptchaQuestionAndAnswer
	{
		// Token: 0x0600003E RID: 62 RVA: 0x000023E8 File Offset: 0x000005E8
		public CaptchaQuestionAndAnswer()
		{
			Random random = new Random();
			int e = random.Next(0, 12);
			CaptchaQuestionAttribute attribute = CaptchaQuestionAttribute.GetAttribute((eCaptchaQuestion)e);
			this.Question = attribute.Question;
			this.PossibleAnswers = attribute.PossibleAnswers;
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600003F RID: 63 RVA: 0x0000242E File Offset: 0x0000062E
		// (set) Token: 0x06000040 RID: 64 RVA: 0x00002436 File Offset: 0x00000636
		public string Question { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000041 RID: 65 RVA: 0x0000243F File Offset: 0x0000063F
		// (set) Token: 0x06000042 RID: 66 RVA: 0x00002447 File Offset: 0x00000647
		public string[] PossibleAnswers { get; set; }
	}
}
