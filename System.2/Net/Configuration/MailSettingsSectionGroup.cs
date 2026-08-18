using System;
using System.Configuration;

namespace System.Net.Configuration
{
	// Token: 0x02000337 RID: 823
	public sealed class MailSettingsSectionGroup : ConfigurationSectionGroup
	{
		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x06001D7A RID: 7546 RVA: 0x0008BFC7 File Offset: 0x0008A1C7
		public SmtpSection Smtp
		{
			get
			{
				return (SmtpSection)base.Sections["smtp"];
			}
		}
	}
}
