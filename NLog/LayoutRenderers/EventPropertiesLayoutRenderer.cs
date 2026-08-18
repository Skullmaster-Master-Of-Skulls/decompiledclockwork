using System;
using System.Globalization;
using System.Text;
using NLog.Config;
using NLog.Internal;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000CE RID: 206
	[LayoutRenderer("event-properties")]
	public class EventPropertiesLayoutRenderer : LayoutRenderer
	{
		// Token: 0x06000602 RID: 1538 RVA: 0x0000D629 File Offset: 0x0000B829
		public EventPropertiesLayoutRenderer()
		{
			this.Culture = CultureInfo.InvariantCulture;
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000603 RID: 1539 RVA: 0x0000D63C File Offset: 0x0000B83C
		// (set) Token: 0x06000604 RID: 1540 RVA: 0x0000D644 File Offset: 0x0000B844
		[DefaultParameter]
		[RequiredParameter]
		public string Item { get; set; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000605 RID: 1541 RVA: 0x0000D64D File Offset: 0x0000B84D
		// (set) Token: 0x06000606 RID: 1542 RVA: 0x0000D655 File Offset: 0x0000B855
		public string Format { get; set; }

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000607 RID: 1543 RVA: 0x0000D65E File Offset: 0x0000B85E
		// (set) Token: 0x06000608 RID: 1544 RVA: 0x0000D666 File Offset: 0x0000B866
		public CultureInfo Culture { get; set; }

		// Token: 0x06000609 RID: 1545 RVA: 0x0000D670 File Offset: 0x0000B870
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			object value;
			if (logEvent.Properties.TryGetValue(this.Item, out value))
			{
				IFormatProvider formatProvider = base.GetFormatProvider(logEvent, this.Culture);
				builder.Append(value.ToStringWithOptionalFormat(this.Format, formatProvider));
			}
		}
	}
}
