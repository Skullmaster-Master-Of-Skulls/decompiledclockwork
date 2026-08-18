using System;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.Common.ClientManager.Notifications.Entities;

namespace TechnoPro.Common.ClientManager.Notifications.Adapters
{
	// Token: 0x02000028 RID: 40
	public static class MessagingAdapter
	{
		// Token: 0x0600012B RID: 299 RVA: 0x000044BC File Offset: 0x000026BC
		public static eMessageTypeCode ConvertToMessageCode(this string codeString)
		{
			int num;
			if (int.TryParse(codeString ?? "", out num) && Enum.IsDefined(typeof(eMessageTypeCode), num))
			{
				return (eMessageTypeCode)num;
			}
			return eMessageTypeCode.Unknown;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x000044F6 File Offset: 0x000026F6
		public static eMessageTypeCode ConvertToMessageCode(this InstantMessage msg)
		{
			if (msg.Parameters.ContainsKey("code"))
			{
				return msg.Parameters["code"].ConvertToMessageCode();
			}
			return eMessageTypeCode.Unknown;
		}
	}
}
