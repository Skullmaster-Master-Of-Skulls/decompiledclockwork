using System;

namespace System.Net.Configuration
{
	// Token: 0x02000338 RID: 824
	internal sealed class MailSettingsSectionGroupInternal
	{
		// Token: 0x06001D7B RID: 7547 RVA: 0x0008BFDE File Offset: 0x0008A1DE
		internal MailSettingsSectionGroupInternal()
		{
			this.smtp = SmtpSectionInternal.GetSection();
		}

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x06001D7C RID: 7548 RVA: 0x0008BFF1 File Offset: 0x0008A1F1
		internal SmtpSectionInternal Smtp
		{
			get
			{
				return this.smtp;
			}
		}

		// Token: 0x06001D7D RID: 7549 RVA: 0x0008BFF9 File Offset: 0x0008A1F9
		internal static MailSettingsSectionGroupInternal GetSection()
		{
			return new MailSettingsSectionGroupInternal();
		}

		// Token: 0x04001C51 RID: 7249
		private SmtpSectionInternal smtp;
	}
}
