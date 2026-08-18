using System;
using MailBee.Mime;
using TechnoPro.Common.Public.Entities.TPMailMan;

namespace TechnoPro.Common.DAO.MailBeeEmail.Adapters
{
	// Token: 0x02000003 RID: 3
	public static class eTPMessagePriorityAdapter
	{
		// Token: 0x06000015 RID: 21 RVA: 0x000030B6 File Offset: 0x000012B6
		public static MailPriority ConvertToMailPriority(this eTPMessagePriority priority)
		{
			switch (priority)
			{
			case eTPMessagePriority.Unknown:
				return MailPriority.None;
			case eTPMessagePriority.Highest:
				return MailPriority.Highest;
			case eTPMessagePriority.High:
				return MailPriority.High;
			case eTPMessagePriority.Normal:
				return MailPriority.Normal;
			case eTPMessagePriority.Low:
				return MailPriority.Low;
			case eTPMessagePriority.Lowest:
				return MailPriority.Lowest;
			default:
				return MailPriority.Normal;
			}
		}
	}
}
