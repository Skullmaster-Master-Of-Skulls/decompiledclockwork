using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities.TPMailMan;

namespace TechnoPro.Common.Public.Entities.Emailing
{
	// Token: 0x02000346 RID: 838
	public class EmailListSendCompletedInfo
	{
		// Token: 0x17000ACA RID: 2762
		// (get) Token: 0x06001A01 RID: 6657 RVA: 0x0001E43C File Offset: 0x0001C63C
		// (set) Token: 0x06001A02 RID: 6658 RVA: 0x0001E444 File Offset: 0x0001C644
		public TPMailResult SendEmailResult { get; set; }

		// Token: 0x17000ACB RID: 2763
		// (get) Token: 0x06001A03 RID: 6659 RVA: 0x0001E44D File Offset: 0x0001C64D
		// (set) Token: 0x06001A04 RID: 6660 RVA: 0x0001E455 File Offset: 0x0001C655
		public List<TPMailMessage> MailMessages { get; set; }
	}
}
