using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using NLog.Config;
using NLog.Internal;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000C3 RID: 195
	[LayoutRenderer("asp-application")]
	public class AspApplicationValueLayoutRenderer : LayoutRenderer
	{
		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060005B2 RID: 1458 RVA: 0x0000CD50 File Offset: 0x0000AF50
		// (set) Token: 0x060005B3 RID: 1459 RVA: 0x0000CD58 File Offset: 0x0000AF58
		[DefaultParameter]
		[RequiredParameter]
		public string Variable { get; set; }

		// Token: 0x060005B4 RID: 1460 RVA: 0x0000CD64 File Offset: 0x0000AF64
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			AspHelper.IApplicationObject applicationObject = AspHelper.GetApplicationObject();
			if (applicationObject != null)
			{
				if (this.Variable != null)
				{
					object value = applicationObject.GetValue(this.Variable);
					builder.Append(Convert.ToString(value, CultureInfo.InvariantCulture));
				}
				Marshal.ReleaseComObject(applicationObject);
			}
		}
	}
}
