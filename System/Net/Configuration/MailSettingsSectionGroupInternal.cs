using System;

namespace System.Net.Configuration
{
	// Token: 0x02000654 RID: 1620
	internal sealed class MailSettingsSectionGroupInternal
	{
		// Token: 0x06003225 RID: 12837 RVA: 0x000D5CCA File Offset: 0x000D4CCA
		internal MailSettingsSectionGroupInternal()
		{
			this.smtp = SmtpSectionInternal.GetSection();
		}

		// Token: 0x17000B8F RID: 2959
		// (get) Token: 0x06003226 RID: 12838 RVA: 0x000D5CDD File Offset: 0x000D4CDD
		internal SmtpSectionInternal Smtp
		{
			get
			{
				return this.smtp;
			}
		}

		// Token: 0x06003227 RID: 12839 RVA: 0x000D5CE5 File Offset: 0x000D4CE5
		internal static MailSettingsSectionGroupInternal GetSection()
		{
			return new MailSettingsSectionGroupInternal();
		}

		// Token: 0x04002F10 RID: 12048
		private SmtpSectionInternal smtp;
	}
}
