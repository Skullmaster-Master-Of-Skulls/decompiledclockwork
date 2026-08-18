using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Web.UI;
using System.Web.UI.WebControls;
using SpeechLib;
using TechnoPro.Common.UI.Web.Entity.Web;

namespace TechnoPro.ClockWorkWeb.ctrls.Common.Captcha
{
	// Token: 0x02000151 RID: 337
	public class ctrls_Common_Captcha_CaptchaControl : UserControl
	{
		// Token: 0x06000A52 RID: 2642 RVA: 0x00047A24 File Offset: 0x00045C24
		private Captcha GetCaptchaClass()
		{
			bool flag = base.Session["CaptchaClass"] != null;
			if (flag)
			{
				this.cc = (Captcha)base.Session["CaptchaClass"];
			}
			else
			{
				this.cc = new Captcha();
			}
			this.cc.FontSize = this.FontSize;
			this.cc.FontFamily = this.FontFamily;
			this.cc.BackgroundImagePath = this.BackgroundImagePath;
			this.cc.TextColor = this.TextColor;
			return this.cc;
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x00047AC3 File Offset: 0x00045CC3
		protected void cv_Validate(object source, ServerValidateEventArgs e)
		{
			e.IsValid = this.ValidateCaptcha();
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x00047AD4 File Offset: 0x00045CD4
		protected void Page_Load(object sender, EventArgs e)
		{
			this.BSpeak.Text = this.SpeakButtonText;
			this.SetValues();
			this.cc = this.GetCaptchaClass();
			bool flag = !base.IsPostBack;
			if (flag)
			{
				this.LoadCaptcha();
			}
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x00047B20 File Offset: 0x00045D20
		private string GetRandomText()
		{
			char[] array = this.CharacterSet.ToCharArray();
			string text = string.Empty;
			Random random = new Random(Guid.NewGuid().GetHashCode());
			for (int i = 0; i < this.CaptchaLength; i++)
			{
				int num = (int)(random.NextDouble() * (double)(array.Length - 1));
				text += array[num].ToString();
			}
			return text;
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x00047BA4 File Offset: 0x00045DA4
		public bool ValidateCaptcha()
		{
			string a = this.T1.Text.Trim().ToUpper();
			return a == (string)this.ViewState["captcha"];
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x00047BF1 File Offset: 0x00045DF1
		protected void LoadAnother(object sender, EventArgs e)
		{
			this.LoadCaptcha();
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x00047BFC File Offset: 0x00045DFC
		private void LoadCaptcha()
		{
			string randomText = this.GetRandomText();
			this.ViewState.Add("captcha", randomText);
			base.Session.Add("CaptchaClass", this.cc);
			base.Session.Add("captcha", randomText);
			this.Im1.ImageUrl = "CaptchaHandler.ashx";
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000A59 RID: 2649 RVA: 0x00047C60 File Offset: 0x00045E60
		// (set) Token: 0x06000A5A RID: 2650 RVA: 0x00047C7D File Offset: 0x00045E7D
		public string SpeakButtonText
		{
			get
			{
				return this.BSpeak.Text;
			}
			set
			{
				this.BSpeak.Text = value;
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000A5B RID: 2651 RVA: 0x00047C90 File Offset: 0x00045E90
		// (set) Token: 0x06000A5C RID: 2652 RVA: 0x00047CA8 File Offset: 0x00045EA8
		public string SuccessMessage
		{
			get
			{
				return this.successMessage;
			}
			set
			{
				this.successMessage = value;
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000A5D RID: 2653 RVA: 0x00047CB4 File Offset: 0x00045EB4
		// (set) Token: 0x06000A5E RID: 2654 RVA: 0x00047CCC File Offset: 0x00045ECC
		public string ErrorMessage
		{
			get
			{
				return this.errorMessage;
			}
			set
			{
				this.errorMessage = value;
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000A5F RID: 2655 RVA: 0x00047CD8 File Offset: 0x00045ED8
		// (set) Token: 0x06000A60 RID: 2656 RVA: 0x00047CF0 File Offset: 0x00045EF0
		public int CaptchaLength
		{
			get
			{
				return this.captchaLength;
			}
			set
			{
				try
				{
					int num = Convert.ToInt32(value);
					bool flag = num < 5 || num > 10;
					if (flag)
					{
						this.captchaLength = 6;
					}
					else
					{
						this.captchaLength = num;
					}
				}
				catch (Exception ex)
				{
					this.captchaLength = 6;
				}
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000A61 RID: 2657 RVA: 0x00047D48 File Offset: 0x00045F48
		// (set) Token: 0x06000A62 RID: 2658 RVA: 0x00047D60 File Offset: 0x00045F60
		public string FontFamily
		{
			get
			{
				return this.fontFamily;
			}
			set
			{
				bool flag = value != string.Empty && value != null;
				if (flag)
				{
					this.fontFamily = value;
				}
				else
				{
					this.fontFamily = "Arial";
				}
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000A63 RID: 2659 RVA: 0x00047D9C File Offset: 0x00045F9C
		// (set) Token: 0x06000A64 RID: 2660 RVA: 0x00047DB4 File Offset: 0x00045FB4
		public double FontSize
		{
			get
			{
				return this.fontSize;
			}
			set
			{
				try
				{
					this.fontSize = (double)Convert.ToInt32(value);
					bool flag = this.fontSize <= 10.0 && this.fontSize >= 24.0;
					if (flag)
					{
						this.fontSize = 16.0;
					}
				}
				catch (Exception ex)
				{
					this.fontSize = 16.0;
				}
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000A65 RID: 2661 RVA: 0x00047E34 File Offset: 0x00046034
		// (set) Token: 0x06000A66 RID: 2662 RVA: 0x00047E4C File Offset: 0x0004604C
		public string BackgroundImagePath
		{
			get
			{
				return this.backgroundImagePath;
			}
			set
			{
				bool flag = File.Exists(base.Server.MapPath(value));
				if (flag)
				{
					this.backgroundImagePath = value;
				}
				else
				{
					this.backgroundImagePath = "~/img/captcha/captcha2.png";
				}
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000A67 RID: 2663 RVA: 0x00047E84 File Offset: 0x00046084
		// (set) Token: 0x06000A68 RID: 2664 RVA: 0x00047E9C File Offset: 0x0004609C
		public string TextColor
		{
			get
			{
				return this.textColor;
			}
			set
			{
				bool flag = string.IsNullOrEmpty(value);
				if (flag)
				{
					this.textColor = "Black";
				}
				else
				{
					this.textColor = value;
				}
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000A69 RID: 2665 RVA: 0x00047EC8 File Offset: 0x000460C8
		// (set) Token: 0x06000A6A RID: 2666 RVA: 0x00047EE0 File Offset: 0x000460E0
		public string CharacterSet
		{
			get
			{
				return this.characterSet;
			}
			set
			{
				bool flag = string.IsNullOrEmpty(value);
				if (flag)
				{
					this.characterSet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ123456789";
				}
				else
				{
					this.characterSet = value;
				}
			}
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x00047F0C File Offset: 0x0004610C
		private void SetValues()
		{
			bool flag = this.CharacterSet == null;
			if (flag)
			{
				this.CharacterSet = "";
			}
			bool flag2 = this.CaptchaLength == 0;
			if (flag2)
			{
				this.CaptchaLength = 6;
			}
			bool flag3 = this.BackgroundImagePath == null;
			if (flag3)
			{
				this.BackgroundImagePath = "";
			}
			bool flag4 = this.FontFamily == null;
			if (flag4)
			{
				this.FontFamily = "";
			}
			bool flag5 = this.FontSize == 0.0;
			if (flag5)
			{
				this.FontSize = 0.0;
			}
			bool flag6 = this.TextColor == null;
			if (flag6)
			{
				this.TextColor = "";
			}
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x00047FBC File Offset: 0x000461BC
		protected void ReadCaptcha(object sender, EventArgs e)
		{
			SpVoice spVoice = (SpVoice)Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("96749377-3391-11D2-9EE3-00C04F797396")));
			char[] array = ((string)this.ViewState["captcha"]).ToCharArray();
			for (int i = 0; i < array.Length; i++)
			{
				spVoice.Speak(array[i].ToString(), SpeechVoiceSpeakFlags.SVSFDefault);
			}
		}

		// Token: 0x040007EF RID: 2031
		protected CustomValidator cv;

		// Token: 0x040007F0 RID: 2032
		protected Image Im1;

		// Token: 0x040007F1 RID: 2033
		protected LinkButton BSpeak;

		// Token: 0x040007F2 RID: 2034
		protected LinkButton B2;

		// Token: 0x040007F3 RID: 2035
		protected Label lbl_instruction;

		// Token: 0x040007F4 RID: 2036
		protected TextBox T1;

		// Token: 0x040007F5 RID: 2037
		public Captcha cc;

		// Token: 0x040007F6 RID: 2038
		private int captchaLength;

		// Token: 0x040007F7 RID: 2039
		private double fontSize;

		// Token: 0x040007F8 RID: 2040
		private string fontFamily;

		// Token: 0x040007F9 RID: 2041
		private string backgroundImagePath;

		// Token: 0x040007FA RID: 2042
		private string textColor;

		// Token: 0x040007FB RID: 2043
		private string successMessage;

		// Token: 0x040007FC RID: 2044
		private string errorMessage;

		// Token: 0x040007FD RID: 2045
		private string characterSet;
	}
}
