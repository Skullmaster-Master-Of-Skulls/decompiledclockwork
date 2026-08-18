using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI.HtmlChart
{
	// Token: 0x020003CE RID: 974
	public class SelectionZoom : SerializableChartElement
	{
		// Token: 0x060023D6 RID: 9174 RVA: 0x000776F0 File Offset: 0x000758F0
		public SelectionZoom()
		{
			base.RegisterConverters(new List<JavaScriptConverter>
			{
				new SelectionZoomConverter()
			});
		}

		// Token: 0x17000B9E RID: 2974
		// (get) Token: 0x060023D7 RID: 9175 RVA: 0x0007771B File Offset: 0x0007591B
		// (set) Token: 0x060023D8 RID: 9176 RVA: 0x0007773C File Offset: 0x0007593C
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

		// Token: 0x17000B9F RID: 2975
		// (get) Token: 0x060023D9 RID: 9177 RVA: 0x00077754 File Offset: 0x00075954
		// (set) Token: 0x060023DA RID: 9178 RVA: 0x00077775 File Offset: 0x00075975
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

		// Token: 0x17000BA0 RID: 2976
		// (get) Token: 0x060023DB RID: 9179 RVA: 0x0007778D File Offset: 0x0007598D
		// (set) Token: 0x060023DC RID: 9180 RVA: 0x000777AE File Offset: 0x000759AE
		[DefaultValue(ModifierKey.Shift)]
		public ModifierKey ModifierKey
		{
			get
			{
				return (ModifierKey)(base.ViewState["ModifierKey"] ?? ModifierKey.Shift);
			}
			set
			{
				base.ViewState["ModifierKey"] = value;
			}
		}
	}
}
