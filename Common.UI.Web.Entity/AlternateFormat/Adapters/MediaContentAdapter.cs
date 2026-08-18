using System;

namespace TechnoPro.Common.UI.Web.Entity.AlternateFormat.Adapters
{
	// Token: 0x02000052 RID: 82
	public static class MediaContentAdapter
	{
		// Token: 0x06000266 RID: 614 RVA: 0x000052C0 File Offset: 0x000034C0
		public static string DisplayMediaContentTitle(this string mediaContentTitle, int length = 75)
		{
			return (mediaContentTitle.Length > length) ? (mediaContentTitle.Substring(0, length) + " ...") : mediaContentTitle;
		}
	}
}
