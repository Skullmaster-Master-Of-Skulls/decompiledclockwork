using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI.HtmlChart
{
	// Token: 0x020003CF RID: 975
	public class MouseWheelZoom : SerializableChartElement
	{
		// Token: 0x060023DD RID: 9181 RVA: 0x000777C8 File Offset: 0x000759C8
		public MouseWheelZoom()
		{
			base.RegisterConverters(new List<JavaScriptConverter>
			{
				new MouseWheelZoomConverter()
			});
		}

		// Token: 0x17000BA1 RID: 2977
		// (get) Token: 0x060023DE RID: 9182 RVA: 0x000777F3 File Offset: 0x000759F3
		// (set) Token: 0x060023DF RID: 9183 RVA: 0x00077814 File Offset: 0x00075A14
		[DefaultValue(false)]
		public bool Enabled
		{
			get
			{
				return (bool)(base.ViewState["Enabled"] ?? false);
			}
			set
			{
				base.ViewState["Enabled"] = value;
			}
		}

		// Token: 0x17000BA2 RID: 2978
		// (get) Token: 0x060023E0 RID: 9184 RVA: 0x0007782C File Offset: 0x00075A2C
		// (set) Token: 0x060023E1 RID: 9185 RVA: 0x0007784D File Offset: 0x00075A4D
		[DefaultValue(AxisLock.None)]
		public AxisLock Lock
		{
			get
			{
				return (AxisLock)(base.ViewState["Lock"] ?? AxisLock.None);
			}
			set
			{
				base.ViewState["Lock"] = value;
			}
		}
	}
}
