using System;

namespace Telerik.Web.UI.Notification
{
	// Token: 0x02000629 RID: 1577
	public class RadNotificationSettings
	{
		// Token: 0x170012E2 RID: 4834
		// (get) Token: 0x0600396C RID: 14700 RVA: 0x000BCA2A File Offset: 0x000BAC2A
		public static string[] BuiltInIcons
		{
			get
			{
				return RadNotificationSettings.builtInIcons;
			}
		}

		// Token: 0x04000F4D RID: 3917
		private static readonly string[] builtInIcons = new string[]
		{
			"info",
			"delete",
			"deny",
			"edit",
			"ok",
			"warning",
			"none"
		};
	}
}
