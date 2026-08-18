using System;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using NLog.Config;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000ED RID: 237
	[LayoutRenderer("shortdate")]
	[ThreadAgnostic]
	public class ShortDateLayoutRenderer : LayoutRenderer
	{
		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060006D1 RID: 1745 RVA: 0x0000F4FC File Offset: 0x0000D6FC
		// (set) Token: 0x060006D2 RID: 1746 RVA: 0x0000F504 File Offset: 0x0000D704
		[DefaultValue(false)]
		public bool UniversalTime { get; set; }

		// Token: 0x060006D3 RID: 1747 RVA: 0x0000F510 File Offset: 0x0000D710
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			DateTime timestamp = logEvent.TimeStamp;
			if (this.UniversalTime)
			{
				timestamp = timestamp.ToUniversalTime();
				ShortDateLayoutRenderer.CachedUtcDate.AppendDate(builder, timestamp);
				return;
			}
			ShortDateLayoutRenderer.CachedLocalDate.AppendDate(builder, timestamp);
		}

		// Token: 0x040001ED RID: 493
		private static readonly ShortDateLayoutRenderer.DateData CachedUtcDate = new ShortDateLayoutRenderer.DateData();

		// Token: 0x040001EE RID: 494
		private static readonly ShortDateLayoutRenderer.DateData CachedLocalDate = new ShortDateLayoutRenderer.DateData();

		// Token: 0x020000EE RID: 238
		private class DateData
		{
			// Token: 0x060006D6 RID: 1750 RVA: 0x0000F56C File Offset: 0x0000D76C
			public void AppendDate(StringBuilder builder, DateTime timestamp)
			{
				if (this.formattedDate == null || this.date.Day != timestamp.Day || this.date.Month != timestamp.Month || this.date.Year != timestamp.Year)
				{
					this.date = timestamp;
					this.formattedDate = timestamp.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
				}
				builder.Append(this.formattedDate);
			}

			// Token: 0x040001F0 RID: 496
			private DateTime date;

			// Token: 0x040001F1 RID: 497
			private string formattedDate;
		}
	}
}
