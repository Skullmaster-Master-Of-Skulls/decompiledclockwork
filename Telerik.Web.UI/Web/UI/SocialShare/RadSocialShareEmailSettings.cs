using System;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace Telerik.Web.UI.SocialShare
{
	// Token: 0x02000F02 RID: 3842
	public class RadSocialShareEmailSettings
	{
		// Token: 0x17002E2C RID: 11820
		// (get) Token: 0x06009229 RID: 37417 RVA: 0x0020F040 File Offset: 0x0020D240
		// (set) Token: 0x0600922A RID: 37418 RVA: 0x0020F048 File Offset: 0x0020D248
		[DefaultValue("")]
		[Category("Behavior")]
		[Description("Specifies the email address which sends the mail message.")]
		public string FromEmail
		{
			get
			{
				return this._fromMail;
			}
			set
			{
				Regex regex = new Regex("^(([\\w-]+\\.)+[\\w-]+|([a-zA-Z]{1}|[\\w-]{2,}))@((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\\.([0-1]?\r\n\t\t\t\t[0-9]{1,2}|25[0-5]|2[0-4][0-9])\\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\\.([0-1]?\r\n\t\t\t\t[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|([a-zA-Z]+[\\w-]+\\.)+[a-zA-Z]{2,4})$");
				Match match = regex.Match(value);
				if (match.Success)
				{
					this._fromMail = value;
					return;
				}
				throw new Exception("FromEmail property requires a valid email address!");
			}
		}

		// Token: 0x17002E2D RID: 11821
		// (get) Token: 0x0600922B RID: 37419 RVA: 0x0020F082 File Offset: 0x0020D282
		// (set) Token: 0x0600922C RID: 37420 RVA: 0x0020F08A File Offset: 0x0020D28A
		[Description("Specifies the SMTP server.")]
		[Category("Behavior")]
		[DefaultValue("")]
		public string SMTPServer { get; set; }

		// Token: 0x17002E2E RID: 11822
		// (get) Token: 0x0600922D RID: 37421 RVA: 0x0020F093 File Offset: 0x0020D293
		// (set) Token: 0x0600922E RID: 37422 RVA: 0x0020F09B File Offset: 0x0020D29B
		[Description("Specifies the user name for network credentials.")]
		[DefaultValue("")]
		[Category("Behavior")]
		public string UserName { get; set; }

		// Token: 0x17002E2F RID: 11823
		// (get) Token: 0x0600922F RID: 37423 RVA: 0x0020F0A4 File Offset: 0x0020D2A4
		// (set) Token: 0x06009230 RID: 37424 RVA: 0x0020F0AC File Offset: 0x0020D2AC
		[Description("Specifies the password for network credentials.")]
		[DefaultValue("")]
		[Category("Behavior")]
		public string Password { get; set; }

		// Token: 0x040029E5 RID: 10725
		private string _fromMail;
	}
}
