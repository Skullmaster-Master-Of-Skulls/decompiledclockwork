using System;
using System.Text;
using NLog.Config;
using NLog.Internal;
using NLog.Layouts;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000CC RID: 204
	[LayoutRenderer("environment")]
	public class EnvironmentLayoutRenderer : LayoutRenderer
	{
		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060005F8 RID: 1528 RVA: 0x0000D547 File Offset: 0x0000B747
		// (set) Token: 0x060005F9 RID: 1529 RVA: 0x0000D54F File Offset: 0x0000B74F
		[DefaultParameter]
		[RequiredParameter]
		public string Variable { get; set; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060005FA RID: 1530 RVA: 0x0000D558 File Offset: 0x0000B758
		// (set) Token: 0x060005FB RID: 1531 RVA: 0x0000D560 File Offset: 0x0000B760
		public string Default { get; set; }

		// Token: 0x060005FC RID: 1532 RVA: 0x0000D56C File Offset: 0x0000B76C
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			if (this.Variable != null)
			{
				string safeEnvironmentVariable = EnvironmentHelper.GetSafeEnvironmentVariable(this.Variable);
				if (!string.IsNullOrEmpty(safeEnvironmentVariable))
				{
					SimpleLayout simpleLayout = new SimpleLayout(safeEnvironmentVariable);
					builder.Append(simpleLayout.Render(logEvent));
					return;
				}
				if (this.Default != null)
				{
					SimpleLayout simpleLayout2 = new SimpleLayout(this.Default);
					builder.Append(simpleLayout2.Render(logEvent));
				}
			}
		}
	}
}
