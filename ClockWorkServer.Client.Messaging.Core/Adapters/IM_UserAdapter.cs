using System;
using System.Text;
using TechnoPro.ClockWorkServer.Contracts;

namespace TechnoPro.ClockWorkServer.Client.Messaging.Core.Adapters
{
	// Token: 0x02000009 RID: 9
	public static class IM_UserAdapter
	{
		// Token: 0x0600002D RID: 45 RVA: 0x000027B4 File Offset: 0x000009B4
		public static string ToStringDescription(this IM_User user)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(string.Format("Username: {0}", user.Username));
			if (!string.IsNullOrEmpty(user.Email))
			{
				stringBuilder.AppendLine(string.Format("Email: {0}", user.Email));
			}
			if (!string.IsNullOrEmpty(user.Phone))
			{
				stringBuilder.AppendLine(string.Format("Phone: {0}", user.Phone));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600002E RID: 46 RVA: 0x0000282C File Offset: 0x00000A2C
		public static string ToHtmlDescription(this IM_User user)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(string.Format("Username: {0}<br/>", user.Username));
			if (!string.IsNullOrEmpty(user.Email))
			{
				stringBuilder.Append(string.Format("Email: {0}<br/>", user.Email));
			}
			if (!string.IsNullOrEmpty(user.Phone))
			{
				stringBuilder.Append(string.Format("Phone: {0}<br/>", user.Phone));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000028A4 File Offset: 0x00000AA4
		public static bool IsCurrentUser(this IM_User user)
		{
			return MessagingManager.CurrentInstance.MessagingClient.CurrentUser != null && user.Username.ToLower().Equals(MessagingManager.CurrentInstance.MessagingClient.CurrentUser.Username.ToLower());
		}
	}
}
