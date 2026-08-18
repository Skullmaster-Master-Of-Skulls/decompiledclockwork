using System;
using System.Text;
using NLog.Config;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000D6 RID: 214
	[LayoutRenderer("install-context")]
	public class InstallContextLayoutRenderer : LayoutRenderer
	{
		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000640 RID: 1600 RVA: 0x0000DF5E File Offset: 0x0000C15E
		// (set) Token: 0x06000641 RID: 1601 RVA: 0x0000DF66 File Offset: 0x0000C166
		[DefaultParameter]
		[RequiredParameter]
		public string Parameter { get; set; }

		// Token: 0x06000642 RID: 1602 RVA: 0x0000DF70 File Offset: 0x0000C170
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			object value;
			if (logEvent.Properties.TryGetValue(this.Parameter, out value))
			{
				IFormatProvider formatProvider = base.GetFormatProvider(logEvent, null);
				builder.Append(Convert.ToString(value, formatProvider));
			}
		}
	}
}
