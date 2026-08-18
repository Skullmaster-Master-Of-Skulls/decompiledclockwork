using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using TechnoPro.Common.UI.ClientManager.Web.Core.Web;
using TechnoPro.Common.UI.Web.Entity.Web;

namespace TechnoPro.ClockWorkWeb.ctrls.Common.Captcha
{
	// Token: 0x02000152 RID: 338
	public class ctrls_Common_Captcha_CaptchaText : UserControl
	{
		// Token: 0x06000A6E RID: 2670 RVA: 0x00048028 File Offset: 0x00046228
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = !base.IsPostBack;
			if (flag)
			{
				this.LoadCaptcha();
			}
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x0004804C File Offset: 0x0004624C
		private void LoadCaptcha()
		{
			ICaptchaTextQuestionManager captchaTextQuestionManager = new CaptchaTextQuestionManager();
			CaptchaQuestionAndAnswer randomQuestion = captchaTextQuestionManager.GetRandomQuestion();
			base.Session.Add("captchatextQA", randomQuestion);
			this.lblQuestion.Text = randomQuestion.Question;
			this.T1.ToolTip = randomQuestion.Question;
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x0004809D File Offset: 0x0004629D
		protected void LoadAnother(object sender, EventArgs e)
		{
			this.LoadCaptcha();
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x000480A7 File Offset: 0x000462A7
		protected void cv_Validate(object source, ServerValidateEventArgs e)
		{
			e.IsValid = this.ValidateCaptcha();
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x000480B8 File Offset: 0x000462B8
		public bool ValidateCaptcha()
		{
			CaptchaQuestionAndAnswer captchaQuestionAndAnswer = (CaptchaQuestionAndAnswer)base.Session["captchatextQA"];
			bool flag = captchaQuestionAndAnswer != null;
			if (flag)
			{
				string b = this.T1.Text.Trim().ToLower();
				foreach (string a in captchaQuestionAndAnswer.PossibleAnswers)
				{
					bool flag2 = a == b;
					if (flag2)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x040007FE RID: 2046
		protected CustomValidator cv;

		// Token: 0x040007FF RID: 2047
		protected TextBox lblQuestion;

		// Token: 0x04000800 RID: 2048
		protected LinkButton B2;

		// Token: 0x04000801 RID: 2049
		protected Label lbl_instruction;

		// Token: 0x04000802 RID: 2050
		protected TextBox T1;
	}
}
