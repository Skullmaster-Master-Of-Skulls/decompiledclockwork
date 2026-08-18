using System;
using System.Text;
using NLog.Config;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000CD RID: 205
	[Obsolete("Use EventPropertiesLayoutRenderer instead.")]
	[LayoutRenderer("event-context")]
	public class EventContextLayoutRenderer : LayoutRenderer
	{
		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060005FE RID: 1534 RVA: 0x0000D5D5 File Offset: 0x0000B7D5
		// (set) Token: 0x060005FF RID: 1535 RVA: 0x0000D5DD File Offset: 0x0000B7DD
		[DefaultParameter]
		[RequiredParameter]
		public string Item { get; set; }

		// Token: 0x06000600 RID: 1536 RVA: 0x0000D5E8 File Offset: 0x0000B7E8
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			object value;
			if (logEvent.Properties.TryGetValue(this.Item, out value))
			{
				IFormatProvider formatProvider = base.GetFormatProvider(logEvent, null);
				builder.Append(Convert.ToString(value, formatProvider));
			}
		}
	}
}
