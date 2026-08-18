using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using NLog.Config;
using NLog.Internal;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000C5 RID: 197
	[LayoutRenderer("asp-session")]
	public class AspSessionValueLayoutRenderer : LayoutRenderer
	{
		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060005C3 RID: 1475 RVA: 0x0000CF6B File Offset: 0x0000B16B
		// (set) Token: 0x060005C4 RID: 1476 RVA: 0x0000CF73 File Offset: 0x0000B173
		[DefaultParameter]
		[RequiredParameter]
		public string Variable { get; set; }

		// Token: 0x060005C5 RID: 1477 RVA: 0x0000CF7C File Offset: 0x0000B17C
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			AspHelper.ISessionObject sessionObject = AspHelper.GetSessionObject();
			if (sessionObject != null)
			{
				if (this.Variable != null)
				{
					object value = sessionObject.GetValue(this.Variable);
					builder.Append(Convert.ToString(value, CultureInfo.InvariantCulture));
				}
				Marshal.ReleaseComObject(sessionObject);
			}
		}
	}
}
