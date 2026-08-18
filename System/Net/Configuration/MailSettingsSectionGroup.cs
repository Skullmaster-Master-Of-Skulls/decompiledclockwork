using System;
using System.Configuration;

namespace System.Net.Configuration
{
	// Token: 0x02000653 RID: 1619
	public sealed class MailSettingsSectionGroup : ConfigurationSectionGroup
	{
		// Token: 0x17000B8E RID: 2958
		// (get) Token: 0x06003224 RID: 12836 RVA: 0x000D5CB3 File Offset: 0x000D4CB3
		public SmtpSection Smtp
		{
			get
			{
				return (SmtpSection)base.Sections["smtp"];
			}
		}
	}
}
