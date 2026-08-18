using System;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using NLog.Config;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000CB RID: 203
	[ThreadAgnostic]
	[LayoutRenderer("date")]
	public class DateLayoutRenderer : LayoutRenderer
	{
		// Token: 0x060005F0 RID: 1520 RVA: 0x0000D4AC File Offset: 0x0000B6AC
		public DateLayoutRenderer()
		{
			this.Format = "yyyy/MM/dd HH:mm:ss.fff";
			this.Culture = CultureInfo.InvariantCulture;
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060005F1 RID: 1521 RVA: 0x0000D4CA File Offset: 0x0000B6CA
		// (set) Token: 0x060005F2 RID: 1522 RVA: 0x0000D4D2 File Offset: 0x0000B6D2
		public CultureInfo Culture { get; set; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060005F3 RID: 1523 RVA: 0x0000D4DB File Offset: 0x0000B6DB
		// (set) Token: 0x060005F4 RID: 1524 RVA: 0x0000D4E3 File Offset: 0x0000B6E3
		[DefaultParameter]
		public string Format { get; set; }

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060005F5 RID: 1525 RVA: 0x0000D4EC File Offset: 0x0000B6EC
		// (set) Token: 0x060005F6 RID: 1526 RVA: 0x0000D4F4 File Offset: 0x0000B6F4
		[DefaultValue(false)]
		public bool UniversalTime { get; set; }

		// Token: 0x060005F7 RID: 1527 RVA: 0x0000D500 File Offset: 0x0000B700
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			DateTime dateTime = logEvent.TimeStamp;
			if (this.UniversalTime)
			{
				dateTime = dateTime.ToUniversalTime();
			}
			IFormatProvider formatProvider = base.GetFormatProvider(logEvent, this.Culture);
			builder.Append(dateTime.ToString(this.Format, formatProvider));
		}
	}
}
